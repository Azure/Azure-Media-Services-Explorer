//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//--------------------------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using AMSExplorer;


namespace AMSExplorer
{
    public partial class EncodingAMEStandard : Form
    {
        public string EncodingAMEStdPresetJSONFilesUserFolder;
        public string EncodingAMEStdPresetJSONFilesFolder;

        private SubClipConfiguration _subclipConfig;

        public List<IAsset> SelectedAssets;
        private CloudMediaContext _context;

        private const string strEditTimes = "Edit times";
        private const string strStitch = "Stitch";
        private const string strAudiooverlay = "Audio overlay";
        private const string strVisualoverlay = "Visual overlay";

        private const string defaultprofile = "H264 Multiple Bitrate 720p";
        bool usereditmode = false;
        private const string strBest = "{Best}";

        public readonly IList<Profile> Profiles = new List<Profile> {
            new Profile() {Prof=@"AAC Good Quality Audio", Desc="Produces a single MP4 file containing only stereo audio encoded at 192 kbps."},
            new Profile() {Prof=@"AAC Audio", Desc="Produces a single MP4 file containing only stereo audio encoded at 128 kbps."},
            new Profile() {Prof=@"H264 Multiple Bitrate 1080p Audio 5.1", Desc="Produces a set of 8 GOP-aligned MP4 files, ranging from 6000 kbps to 400 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 1080p", Desc="Produces a set of 8 GOP-aligned MP4 files, ranging from 6000 kbps to 400 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 16x9 for iOS", Desc="Produces a set of 8 GOP-aligned MP4 files, ranging from 8500 kbps to 200 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 16x9 SD Audio 5.1", Desc="Produces a set of 5 GOP-aligned MP4 files, ranging from 1900 kbps to 400 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 16x9 SD", Desc="Produces a set of 5 GOP-aligned MP4 files, ranging from 1900 kbps to 400 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 4K Audio 5.1", Desc="Produces a set of 12 GOP-aligned MP4 files, ranging from 20000 kbps to 1000 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 4K", Desc="Produces a set of 12 GOP-aligned MP4 files, ranging from 20000 kbps to 1000 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 4x3 for iOS", Desc="Produces a set of 8 GOP-aligned MP4 files, ranging from 8500 kbps to 200 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 4x3 SD Audio 5.1", Desc="Produces a set of 5 GOP-aligned MP4 files, ranging from 1600 kbps to 400 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 4x3 SD", Desc="Produces a set of 5 GOP-aligned MP4 files, ranging from 1600 kbps to 400 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 720p Audio 5.1", Desc="Produces a set of 6 GOP-aligned MP4 files, ranging from 3400 kbps to 400 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Multiple Bitrate 720p", Desc="Produces a set of 6 GOP-aligned MP4 files, ranging from 3400 kbps to 400 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate 1080p Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 6750 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 1080p", Desc="Produces a single MP4 file with a bitrate of 6750 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate 4K Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 18000 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 4K", Desc="Produces a single MP4 file with a bitrate of 18000 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate 4x3 SD Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 18000 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 4x3 SD", Desc="Produces a single MP4 file with a bitrate of 18000 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate 16x9 SD Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 2200 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 16x9 SD", Desc="Produces a single MP4 file with a bitrate of 2200 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate 720p Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 4500 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 720p for Android", Desc="Produces a single MP4 file with a bitrate of 2000 kbps, and stereo AAC."},
            new Profile() {Prof=@"H264 Single Bitrate 720p", Desc="Produces a single MP4 file with a bitrate of 4500 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate High Quality SD for Android", Desc="Produces a single MP4 file with a bitrate of 500 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate Low Quality SD for Android", Desc="Produces a single MP4 file with a bitrate of 56 kbps, and stereo AAC audio."}
           };

        private int _nbInputAssets;
        private string _processorVersion;
        private bool _ThumbnailsModeOnly;

        public string EncodingLabel
        {
            set
            {
                labelsummaryjob.Text = value;
            }
        }

        public string EncodingJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }

        public string EncodingOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }

        public string EncodingConfiguration
        {
            get
            {
                return textBoxConfiguration.Text;
            }
        }

        public JobOptionsVar JobOptions
        {
            get
            {
                return buttonJobOptions.GetSettings();
            }
            set
            {
                buttonJobOptions.SetSettings(value);
            }
        }


        public EncodingAMEStandard(CloudMediaContext context, int nbInputAssets, string processorVersion, SubClipConfiguration subclipConfig = null, bool ThumbnailsModeOnly = false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _processorVersion = processorVersion;
            _subclipConfig = subclipConfig; // used for trimming
            buttonJobOptions.Initialize(_context);
            _nbInputAssets = nbInputAssets;
            _ThumbnailsModeOnly = ThumbnailsModeOnly; // used for thumbnail only mode from the menu
        }


        private void EncodingAMEStandard_Shown(object sender, EventArgs e)
        {
        }



        private void EncodingAMEStandard_Load(object sender, EventArgs e)
        {
            // presets list
            var filePaths = Directory.GetFiles(EncodingAMEStdPresetJSONFilesFolder, "*.json").Select(f => Path.GetFileNameWithoutExtension(f));
            listboxPresets.Items.AddRange(filePaths.ToArray());
            if (!_ThumbnailsModeOnly)
            {
                listboxPresets.SelectedIndex = listboxPresets.Items.IndexOf(defaultprofile);
            }
            else // Thumbnail mode only
            {
                textBoxConfiguration.Text = "{}";
                tabControl1.SelectedTab = tabPageThPNG;
            }
            label4KWarning.Text = string.Empty;
            moreinfoame.Links.Add(new LinkLabel.Link(0, moreinfoame.Text.Length, Constants.LinkMoreInfoMES));
            moreinfopresetslink.Links.Add(new LinkLabel.Link(0, moreinfopresetslink.Text.Length, Constants.LinkMorePresetsMES));
            linkLabelThumbnail1.Links.Add(new LinkLabel.Link(0, linkLabelThumbnail1.Text.Length, Constants.LinkThumbnailsMES));
            linkLabelThumbnail2.Links.Add(new LinkLabel.Link(0, linkLabelThumbnail1.Text.Length, Constants.LinkThumbnailsMES));
            linkLabelThumbnail3.Links.Add(new LinkLabel.Link(0, linkLabelThumbnail1.Text.Length, Constants.LinkThumbnailsMES));
            linkLabelMoreInfoPreserveResRotation.Links.Add(new LinkLabel.Link(0, linkLabelMoreInfoPreserveResRotation.Text.Length, Constants.LinkPreserveResRotationMES));
            linkLabelInfoOverlay.Links.Add(new LinkLabel.Link(0, linkLabelInfoOverlay.Text.Length, Constants.LinkOverlayMES));

            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _processorVersion);

            if (_subclipConfig != null && _subclipConfig.Trimming)
            {
                // come from subclip UI
                timeControlStartTime.SetTimeStamp(_subclipConfig.StartTimeForReencode - _subclipConfig.OffsetForReencode);
                timeControlStartTime.TimeScale = timeControlEndTime.TimeScale = TimeSpan.TicksPerSecond;
                timeControlStartTime.ScaledFirstTimestampOffset = timeControlEndTime.ScaledFirstTimestampOffset = (ulong)_subclipConfig.OffsetForReencode.Ticks;
                timeControlEndTime.SetTimeStamp(_subclipConfig.StartTimeForReencode + _subclipConfig.DurationForReencode - _subclipConfig.OffsetForReencode);

                // let's display the offset
                labelOffset.Visible = textBoxOffset.Visible = true;
                textBoxOffset.Text = _subclipConfig.OffsetForReencode.ToString();

                checkBoxSourceTrimmingStart.Checked = checkBoxSourceTrimmingEnd.Checked = true;
            }

            if (_nbInputAssets > 1)
            {
                checkBoxOverlay.Enabled = false; // no overlay if several assets have been selected
                //tabControl1.TabPages.Remove(tabPageOverlay); // no overlay if several assets have been selected
            }
        }


        private void buttonLoadJSON_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(this.EncodingAMEStdPresetJSONFilesUserFolder))
                openFileDialogPreset.InitialDirectory = this.EncodingAMEStdPresetJSONFilesUserFolder;

            if (openFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                this.EncodingAMEStdPresetJSONFilesUserFolder = Path.GetDirectoryName(openFileDialogPreset.FileName); // let's save the folder
                try
                {
                    StreamReader streamReader = new StreamReader(openFileDialogPreset.FileName);
                    UpdateTextBoxJSON(streamReader.ReadToEnd());
                    streamReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                label4KWarning.Text = string.Empty;
                buttonOk.Enabled = true;
                richTextBoxDesc.Text = string.Empty;
            }
        }


        private void UpdateTextBoxJSON(string jsondata)
        {
            var mode = Program.AnalyseConfigurationString(jsondata);
            if (mode == TypeConfig.XML) // XML data
            {
                textBoxConfiguration.Text = jsondata;
            }
            else if (mode == TypeConfig.JSON) // JSON
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsondata);
                if (checkBoxAddAutomatic.Checked)
                {
                    ////////////////////////////
                    // Cleaning of JSON
                    ////////////////////////////

                    // clean auto deinterlaing
                    // clean all sources
                    //if (obj.Sources != null) obj.Sources.Parent.Remove();

                    // clean trimming
                    // clean deinterlace filter
                    // clean overlay
                    if (obj.Sources != null)
                    {
                        var listDelete = new List<dynamic>();
                        foreach (var source in obj.Sources)
                        {
                            if (
                                (source.StartTime != null && source.Duration != null)
                                || (source.Filters != null && source.Filters.Deinterlace != null)
                                || (source.Filters != null && source.Filters.VideoOverlay != null)
                                )
                            {
                                listDelete.Add(source);
                            }
                        }
                        listDelete.ForEach(c => c.Remove());
                        if (obj.Sources.Count == 0)
                        {
                            obj.Sources.Parent.Remove();
                        }
                    }

                    // Clean Insert silent audio track
                    if (obj.Codecs != null)
                    {
                        foreach (var codec in obj.Codecs)
                        {
                            if (codec.Type != null && codec.Type == "AACAudio" && codec.Condition != null && codec.Condition == "InsertSilenceIfNoAudio")
                            {
                                codec.Condition.Parent.Remove();
                            }
                        }
                    }

                    // Clean PreserveResolutionAfterRotation flag
                    if (obj.Codecs != null)
                    {
                        foreach (var codec in obj.Codecs)
                        {
                            if (codec.Type != null &&
                                (codec.Type == "H264Video" || codec.Type == "BmpImage" || codec.Type == "JpgImage" || codec.Type == "PngImage") &&
                                codec.PreserveResolutionAfterRotation != null)
                            {
                                codec.PreserveResolutionAfterRotation.Parent.Remove();
                            }
                        }
                    }

                    if (obj.Codecs != null) // clean thumbnail entry in Codecs
                    {
                        var listDelete = new List<dynamic>();
                        foreach (var codec in obj.Codecs)
                        {
                            if (codec.JpgLayers != null || codec.PngLayers != null || codec.BmpLayers != null)
                            {
                                listDelete.Add(codec);
                            }
                        }
                        listDelete.ForEach(c => c.Remove());
                    }
                    if (obj.Outputs != null) // clean thumbnail entry in Outputs
                    {
                        var listDelete = new List<dynamic>();
                        foreach (var output in obj.Outputs)
                        {
                            if (output.Format != null && output.Format.Type != null && output.Format.Type.Type == JTokenType.String)
                            {
                                string valuestr = (string)output.Format.Type;
                                if (valuestr == "JpgFormat" || valuestr == "PngFormat" || valuestr == "BmpFormat")
                                {
                                    listDelete.Add(output);
                                }
                            }
                        }
                        listDelete.ForEach(c => c.Remove());
                    }
                    ////////////////////////////
                    // End of Cleaning
                    ////////////////////////////


                    // Trimming
                    if (checkBoxSourceTrimmingStart.Checked || checkBoxSourceTrimmingEnd.Checked)
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        dynamic time = new JObject();
                        if (checkBoxSourceTrimmingStart.Checked)
                        {
                            time.StartTime = timeControlStartTime.GetTimeStampAsTimeSpanWithOffset();
                            if (checkBoxSourceTrimmingEnd.Checked)
                            {
                                time.Duration = timeControlEndTime.GetTimeStampAsTimeSpanWithOffset() - timeControlStartTime.GetTimeStampAsTimeSpanWithOffset();
                            }
                        }
                        else if (checkBoxSourceTrimmingEnd.Checked) // only end time specified
                        {
                            time.Duration = timeControlEndTime.GetTimeStampAsTimeSpanWithOffset() - timeControlStartTime.GetOffSetAsTimeSpan();
                        }
                        obj.Sources.Add(time);
                    }

                    // Overlay
                    if (checkBoxOverlay.Checked)
                    {
                        /*
                        "Sources": [
                            {
                              "Streams": [],
                              "Filters": {
                                "VideoOverlay": {
                                  "Position": {
                                    "X": 0,
                                    "Y": 0,
                                    "Width": 100,
                                    "Height": 100
                                  },
                                  "AudioGainLevel": 0.0,
                                  "MediaParams": [
                                    {
                                      "OverlayLoopCount": 1
                                    },
                                    {
                                      "IsOverlay": true,
                                      "OverlayLoopCount": 1,
                                      "InputLoop": true
                                    }
                                  ],
                                  "Source": "OverlayImage.png",
                                  "Clip": {
                                    "Duration": "00:00:05"
                                  },
                                  "FadeInDuration": {
                                    "StartTime": "00:00:00",
                                    "Duration": "00:00:01"
                                  },
                                  "FadeOutDuration": {
                                    "StartTime": "00:00:03",
                                    "Duration": "00:00:04"
                                  }
                                }
                              },
                              "Pad": true
                            }
                          ],
    */

                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        dynamic Source = new JObject();
                        obj.Sources.Add(Source);

                        Source.Streams = new JArray() as dynamic;


                        dynamic VideoOverlayEntry = new JObject();
                        Source.Filters = VideoOverlayEntry;

                        dynamic VideoOverlay = new JObject();
                        VideoOverlayEntry.VideoOverlay = VideoOverlay;

                        dynamic Position = new JObject();
                        Position.X = (int)numericUpDownVOverlayRectX.Value;
                        Position.Y = (int)numericUpDownVOverlayRectY.Value;
                        if (checkBoxOverlayResize.Checked)
                        {
                            Position.Width = (int)numericUpDownVOverlayRectW.Value;
                            Position.Height = (int)numericUpDownVOverlayRectH.Value;
                        }
                        VideoOverlay.Position = Position;
                        VideoOverlay.AudioGainLevel = (decimal)0;

                        // Mediaparams
                        dynamic MediaParams = new JArray() as dynamic;
                        VideoOverlay.MediaParams = MediaParams;

                        dynamic OverlayParamVideo = new JObject();
                        MediaParams.Add(OverlayParamVideo);

                        OverlayParamVideo.IsOverlay = false;
                        OverlayParamVideo.OverlayLoopCount = 1;

                        dynamic OverlayParamImage = new JObject();
                        MediaParams.Add(OverlayParamImage);

                        OverlayParamImage.IsOverlay = true;

                        if (checkBoxOverlayLoop.Checked) // loop checked
                        {
                            OverlayParamImage.OverlayLoopCount = (int)numericUpDownOverlayLoop.Value;
                        }
                        else
                        {
                            OverlayParamImage.OverlayLoopCount = 1;
                        }


                        VideoOverlay.Source = textBoxOverlayFileName.Text;

                        if (checkBoxOverlayDuration.Checked) // duration specified
                        {
                            dynamic Clip = new JObject();
                            VideoOverlay.Clip = Clip;
                            Clip.Duration = textBoxOverlayDuration.Text;
                        }

                        if (checkBoxOverlayFade.Checked) // fade in and out
                        {
                            dynamic FadeInDuration = new JObject();
                            VideoOverlay.FadeInDuration = FadeInDuration;

                            FadeInDuration.StartTime = textBoxVOverlayFadeInStartTime.Text;
                            FadeInDuration.Duration = textBoxVOverlayFadeInDuration.Text;

                            dynamic FadeOutDuration = new JObject();
                            VideoOverlay.FadeOutDuration = FadeOutDuration;

                            FadeOutDuration.StartTime = textBoxVOverlayFadeOutStartTime.Text;
                            FadeOutDuration.Duration = textBoxVOverlayFadeOutDuration.Text;

                            OverlayParamImage.InputLoop = true; // needed for fade in out
                        }
                    }

                    // Insert silent audio track
                    if (checkBoxInsertSilentAudioTrack.Checked)
                    {
                        if (obj.Codecs != null)
                        {
                            foreach (var codec in obj.Codecs)
                            {
                                if (codec.Type != null && codec.Type == "AACAudio")
                                {
                                    codec.Condition = "InsertSilenceIfNoAudio";
                                }
                            }
                        }
                    }

                    // Insert PreserveResolutionAfterRotation for video track
                    if (checkBoxPreserveResAfterRotation.Checked)
                    {
                        if (obj.Codecs != null)
                        {
                            foreach (var codec in obj.Codecs)
                            {
                                if (codec.Type != null && codec.Type == "H264Video")
                                {
                                    codec.PreserveResolutionAfterRotation = true;
                                }
                            }
                        }
                    }

                    // Insert disable auto deinterlacing
                    if (checkBoxDisableAutoDeinterlacing.Checked)
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        bool DeinterModeSet = false;
                        foreach (var source in obj.Sources)
                        {
                            if (source.Filters != null)
                            {
                                if (source.Filters.Deinterlace != null)
                                {
                                    source.Filters.Deinterlace.Mode = "Off";
                                }
                                else
                                {
                                    dynamic modeeentry = new JObject() as dynamic;
                                    modeeentry.Mode = "Off";
                                    source.Filters.Deinterlace = modeeentry;
                                }
                                DeinterModeSet = true;
                            }
                        }

                        if (!DeinterModeSet)
                        {
                            dynamic sourceentry = new JObject() as dynamic;
                            dynamic deinterlaceentry = new JObject() as dynamic;
                            dynamic modeeentry = new JObject() as dynamic;
                            modeeentry.Mode = "Off";
                            deinterlaceentry.Deinterlace = modeeentry;
                            sourceentry.Filters = deinterlaceentry;
                            obj.Sources.Add(sourceentry);
                        }
                    }


                    if (_subclipConfig != null) // subclipping. we need to add top bitrate values
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        dynamic entry = new JObject() as dynamic;

                        bool alreadyentry = false;
                        if (obj.Sources.Count > 0)
                        {
                            entry = obj.Sources[0];
                            alreadyentry = true;
                        }

                        entry.Streams = new JArray() as dynamic;

                        dynamic stream = new JObject();
                        stream.Type = "AudioStream";
                        stream.Value = "TopBitrate";
                        entry.Streams.Add(stream);

                        stream = new JObject();
                        stream.Type = "VideoStream";
                        stream.Value = "TopBitrate";
                        entry.Streams.Add(stream);

                        if (!alreadyentry) obj.Sources.Add(entry);
                    }


                    // Thumbnails settings
                    if (checkBoxGenThumbnailsJPG.Checked || checkBoxGenThumbnailsPNG.Checked || checkBoxGenThumbnailsBMP.Checked)
                    {
                        if (obj.Codecs == null)
                        {
                            obj.Codecs = new JArray() as dynamic;
                        }


                        if (obj.Outputs == null)
                        {
                            obj.Outputs = new JArray() as dynamic;
                        }

                        if (checkBoxGenThumbnailsJPG.Checked)
                        {
                            AddThumbnailJSON(ref obj, ThumbnailType.Jpg, textBoxThFileNameJPG.Text,
                                            checkBoxBestJPG.Checked ? strBest : textBoxThTimeStartJPG.Text,
                                            textBoxThTimeStepJPG.Text, textBoxThTimeRangeJPG.Text, (int)numericUpDownThWidthJPG.Value, (int)numericUpDownThHeightJPG.Value, checkBoxPresResRotJPG.Checked, radioButtonPixelsJPG.Checked, (int)numericUpDownThQuality.Value);
                        }
                        if (checkBoxGenThumbnailsPNG.Checked)
                        {
                            AddThumbnailJSON(ref obj, ThumbnailType.Png, textBoxThFileNamePNG.Text,
                                            checkBoxBestPNG.Checked ? strBest : textBoxThTimeStartPNG.Text,
                                            textBoxThTimeStepPNG.Text, textBoxThTimeRangePNG.Text, (int)numericUpDownThWidthPNG.Value, (int)numericUpDownThHeightPNG.Value, checkBoxPresResRotPNG.Checked, radioButtonPixelsPNG.Checked);
                        }
                        if (checkBoxGenThumbnailsBMP.Checked)
                        {
                            AddThumbnailJSON(ref obj, ThumbnailType.Bmp, textBoxThFileNameBMP.Text,
                                            checkBoxBestBMP.Checked ? strBest : textBoxThTimeStartBMP.Text,
                                            textBoxThTimeStepBMP.Text, textBoxThTimeRangeBMP.Text, (int)numericUpDownThWidthBMP.Value, (int)numericUpDownThHeightBMP.Value, checkBoxPresResRotBMP.Checked, radioButtonPixelsBMP.Checked);
                        }
                    }
                }
                textBoxConfiguration.Text = obj.ToString();
            }
            else // no xml and no Json !
            {
                textBoxConfiguration.Text = jsondata;
            }
        }

        private void AddThumbnailJSON(ref dynamic obj, ThumbnailType thtype, string fileName, string timeStart, string TimeStep, string TimeRange, int width, int height, bool preserveResolutionRotation, bool pixelmode, int quality = -1)
        {
            string extension = Enum.GetName(typeof(ThumbnailType), thtype); // to get Png, Bmp or Jpg

            dynamic thOutputEntry = new JObject();
            thOutputEntry.FileName = fileName;
            dynamic Format = new JObject();

            dynamic thEntry = new JObject();
            if (!string.IsNullOrWhiteSpace(timeStart)) thEntry.Start = timeStart;
            if (timeStart != strBest)
            {
                if (!string.IsNullOrWhiteSpace(TimeStep)) thEntry.Step = TimeStep;
                if (!string.IsNullOrWhiteSpace(TimeRange)) thEntry.Range = TimeRange;
            }

            thEntry.Type = extension + "Image";

            dynamic Layer = new JObject();
            if (quality != -1)
            {
                Layer.Quality = quality;
            }
            Layer.Type = extension + "Layer";

            if (pixelmode)
            {
                Layer.Width = width;
                Layer.Height = height;
            }
            else // percentage
            {
                Layer.Width = width.ToString() + "%";
                Layer.Height = height.ToString() + "%";

                if (preserveResolutionRotation)
                {
                    thEntry.PreserveResolutionAfterRotation = true;
                }
            }

            switch (thtype)
            {
                case ThumbnailType.Bmp:
                    thEntry.BmpLayers = new JArray() as dynamic;
                    thEntry.BmpLayers.Add(Layer);
                    break;

                case ThumbnailType.Png:
                    thEntry.PngLayers = new JArray() as dynamic;
                    thEntry.PngLayers.Add(Layer);
                    break;

                case ThumbnailType.Jpg:
                    thEntry.JpgLayers = new JArray() as dynamic;
                    thEntry.JpgLayers.Add(Layer);
                    break;
            }

            obj.Codecs.Add(thEntry);

            Format.Type = extension + "Format";
            thOutputEntry.Format = Format;
            obj.Outputs.Add(thOutputEntry);
        }

        enum ThumbnailType
        {
            Jpg = 0,
            Png,
            Bmp
        }

        private void buttonSaveXML_Click(object sender, EventArgs e)
        {
            if (saveFileDialogPreset.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialogPreset.FileName, textBoxConfiguration.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not save file to disk. Original error: " + ex.Message);
                }
            }
        }


        private void listboxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxPresets.SelectedItem != null)
            {
                try
                {
                    string filePath = Path.Combine(EncodingAMEStdPresetJSONFilesFolder, listboxPresets.SelectedItem.ToString() + ".json");
                    StreamReader streamReader = new StreamReader(filePath);
                    usereditmode = false;
                    UpdateTextBoxJSON(streamReader.ReadToEnd());
                    usereditmode = true;
                    streamReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk or process the JSON data. Original error:" + Constants.endline + ex.Message);
                    usereditmode = true;
                }

                if (listboxPresets.SelectedItem.ToString().Contains("4K") && _context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType != ReservedUnitType.Premium)
                {
                    label4KWarning.Text = (string)label4KWarning.Tag;
                    buttonOk.Enabled = false;
                }
                else
                {
                    label4KWarning.Text = string.Empty;
                    buttonOk.Enabled = true;
                }

                var profile = Profiles.Where(p => p.Prof == listboxPresets.SelectedItem.ToString()).FirstOrDefault();
                if (profile != null)
                {
                    richTextBoxDesc.Text = profile.Desc;
                }
                else
                {
                    richTextBoxDesc.Text = string.Empty;
                }
            }
        }

        private void textBoxConfiguration_TextChanged(object sender, EventArgs e)
        {
            if (usereditmode)
            {
                listboxPresets.SelectedIndex = -1;
                richTextBoxDesc.Text = string.Empty;
            }

            // let's normalize the line breaks
            textBoxConfiguration.Text = textBoxConfiguration.Text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

            bool Error = false;
            var type = Program.AnalyseConfigurationString(textBoxConfiguration.Text);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax

                try
                {
                    var jo = JObject.Parse(textBoxConfiguration.Text);
                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format((string)labelWarningJSON.Tag, ex.Message);
                    Error = true;
                }
            }
            else if (type == TypeConfig.XML) // XML 
            {
                try
                {
                    var xml = XElement.Load(new StringReader(textBoxConfiguration.Text));
                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format("Error in XML data: {0}", ex.Message);
                    Error = true;
                }
            }
            labelWarningJSON.Visible = Error;
        }

        private void moreinfoame_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
   
        private void timeControlStartTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
            UpdateDurationText();
        }

        private void timeControlDuration_ValueChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
            UpdateDurationText();
        }
        private void UpdateDurationText()
        {
            if (checkBoxSourceTrimmingStart.Checked)
            {
                if (checkBoxSourceTrimmingEnd.Checked)
                {
                    textBoxSourceDurationTime.Text = (timeControlEndTime.GetTimeStampAsTimeSpanWithOffset() - timeControlStartTime.GetTimeStampAsTimeSpanWithOffset()).ToString();
                }
                else
                {
                    textBoxSourceDurationTime.Text = string.Empty;
                }
            }
            else if (checkBoxSourceTrimmingEnd.Checked) // only end time specified
            {
                textBoxSourceDurationTime.Text = (timeControlEndTime.GetTimeStampAsTimeSpanWithOffset() - timeControlStartTime.GetOffSetAsTimeSpan()).ToString();
            }

        }

        private void checkBoxSourceTrimming_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStartTime.Enabled = checkBoxSourceTrimmingStart.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxAddAutomatic_CheckedChanged(object sender, EventArgs e)
        {
        }


        private void checkBoxGenThumbnails_CheckedChanged(object sender, EventArgs e)
        {
            panelThumbnailsJPG.Enabled = checkBoxGenThumbnailsJPG.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void ThumbnailSettingsChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxGenThumbnailsPNG_CheckedChanged(object sender, EventArgs e)
        {
            panelThumbnailsPNG.Enabled = checkBoxGenThumbnailsPNG.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxGenThumbnailsBMP_CheckedChanged(object sender, EventArgs e)
        {
            panelThumbnailsBMP.Enabled = checkBoxGenThumbnailsBMP.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void checkBoxInsertSilentAudioTrack_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxDisableAutoDeinterlacing_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxPreserveResAfterRotation_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void radioButtonPercentJPG_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPresResRotJPG.Visible = radioButtonPercentJPG.Checked;
            if (radioButtonPercentJPG.Checked)
            {
                numericUpDownThWidthJPG.Maximum = numericUpDownThHeightJPG.Maximum = 100;
                numericUpDownThWidthJPG.Value = numericUpDownThHeightJPG.Value = 100;
            }
            else
            {
                numericUpDownThWidthJPG.Maximum = numericUpDownThHeightJPG.Maximum = 10000;
            }
        }

        private void radioButtonPercentPNG_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPresResRotPNG.Visible = radioButtonPercentPNG.Checked;
            if (radioButtonPercentPNG.Checked)
            {
                numericUpDownThWidthPNG.Maximum = numericUpDownThHeightPNG.Maximum = 100;
                numericUpDownThWidthPNG.Value = numericUpDownThHeightPNG.Value = 100;
            }
            else
            {
                numericUpDownThWidthPNG.Maximum = numericUpDownThHeightPNG.Maximum = 10000;
            }
        }

        private void radioButtonPercentBMP_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPresResRotBMP.Visible = radioButtonPercentBMP.Checked;
            if (radioButtonPercentBMP.Checked)
            {
                numericUpDownThWidthBMP.Maximum = numericUpDownThHeightBMP.Maximum = 100;
                numericUpDownThWidthBMP.Value = numericUpDownThHeightBMP.Value = 100;
            }
            else
            {
                numericUpDownThWidthBMP.Maximum = numericUpDownThHeightBMP.Maximum = 10000;
            }
        }


        private void checkBoxBestJPG_CheckedChanged(object sender, EventArgs e)
        {
            textBoxThTimeStartJPG.Enabled = textBoxThTimeRangeJPG.Enabled = textBoxThTimeStepJPG.Enabled = !checkBoxBestJPG.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxBestPNG_CheckedChanged(object sender, EventArgs e)
        {
            textBoxThTimeStartPNG.Enabled = textBoxThTimeRangePNG.Enabled = textBoxThTimeStepPNG.Enabled = !checkBoxBestPNG.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxBestBMP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxThTimeStartBMP.Enabled = textBoxThTimeRangeBMP.Enabled = textBoxThTimeStepBMP.Enabled = !checkBoxBestBMP.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxOverlayDuration_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOverlayDuration.Enabled = checkBoxOverlayDuration.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxOverlayFade_CheckedChanged(object sender, EventArgs e)
        {
            panelFade.Enabled = checkBoxOverlayFade.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxOverlayLoop_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownOverlayLoop.Enabled = checkBoxOverlayLoop.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxOverlayResize_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownVOverlayRectW.Enabled = numericUpDownVOverlayRectH.Enabled = checkBoxOverlayResize.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoSelectFile();
        }

        private void DoSelectFile()
        {
            var form = new EncodingAMEStandardPickOverlay(SelectedAssets.FirstOrDefault());
            if (form.ShowDialog() == DialogResult.OK)
            {
                textBoxOverlayFileName.Text = form.SelectedAssetFile.Name;
                CheckOverlayFile();
                UpdateTextBoxJSON(textBoxConfiguration.Text);
            }
        }

        private void checkBoxSourceTrimmingEnd_CheckedChanged(object sender, EventArgs e)
        {
            timeControlEndTime.Enabled = textBoxSourceDurationTime.Enabled =
             checkBoxSourceTrimmingEnd.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxOverlay_CheckedChanged(object sender, EventArgs e)
        {
            panelOverlay.Enabled = checkBoxOverlay.Checked;
            CheckOverlayFile();
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void CheckOverlayFile()
        {
            if (string.IsNullOrWhiteSpace(textBoxOverlayFileName.Text) && checkBoxOverlay.Checked)
            {
                errorProvider1.SetError(textBoxOverlayFileName, "Select a file in the source asset");
            }
            else
            {
                errorProvider1.SetError(textBoxOverlayFileName, String.Empty);
            }
        }
    }

    enum TypeConfig
    {
        JSON,
        XML,
        Other
    }
}
