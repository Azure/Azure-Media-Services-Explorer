//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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
    public partial class EncodingMES : Form
    {
        public string EncodingAMEStdPresetJSONFilesUserFolder;
        public string EncodingAMEStdPresetJSONFilesFolder;

        private SubClipConfiguration _subclipConfig;

        private List<IAsset> _selectedAssets;
        IAsset _firstAsset;
        private CloudMediaContext _context;

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
            new Profile() {Prof=@"H264 Single Bitrate 4x3 SD Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 1800 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 4x3 SD", Desc="Produces a single MP4 file with a bitrate of 1800 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate 16x9 SD Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 2200 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 16x9 SD", Desc="Produces a single MP4 file with a bitrate of 2200 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate 720p Audio 5.1", Desc="Produces a single MP4 file with a bitrate of 4500 kbps, and AAC 5.1 audio."},
            new Profile() {Prof=@"H264 Single Bitrate 720p for Android", Desc="Produces a single MP4 file with a bitrate of 2000 kbps, and stereo AAC."},
            new Profile() {Prof=@"H264 Single Bitrate 720p", Desc="Produces a single MP4 file with a bitrate of 4500 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate High Quality SD for Android", Desc="Produces a single MP4 file with a bitrate of 500 kbps, and stereo AAC audio."},
            new Profile() {Prof=@"H264 Single Bitrate Low Quality SD for Android", Desc="Produces a single MP4 file with a bitrate of 56 kbps, and stereo AAC audio."}
           };

        private string _processorVersion;
        private bool _ThumbnailsModeOnly;
        private bool _disableOverlay;
        private bool _disableSourceTrimming;
        private Mainform _main;
        private List<IAsset> _inputAssetsForJob;
        private string _multiplierlabel;
        private string _labelWarningJSON;

        public List<IAsset> SelectedAssets
        {
            set
            {
                _selectedAssets = value;
                _firstAsset = _selectedAssets.FirstOrDefault();
            }
            get
            {
                return _selectedAssets;
            }
        }

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


        public EncodingMES(CloudMediaContext context, List<IAsset> inputAssetsForJob, string processorVersion, Mainform main, SubClipConfiguration subclipConfig = null, bool ThumbnailsModeOnly = false, bool disableOverlay = false, bool disableSourceTrimming = false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _processorVersion = processorVersion;
            _subclipConfig = subclipConfig; // used for trimming
            buttonJobOptions.Initialize(_context);
            _ThumbnailsModeOnly = ThumbnailsModeOnly; // used for thumbnail only mode from the menu
            _disableOverlay = disableOverlay;
            _disableSourceTrimming = disableSourceTrimming;
            _inputAssetsForJob = inputAssetsForJob;
            if (_inputAssetsForJob.Count > 0) _disableOverlay = true; // if stiching, let's disable overlay

            _main = main;
            _multiplierlabel = labelOutputMinuteMultiplier.Text;
            _labelWarningJSON = labelWarningJSON.Text;
        }



        private void buttonRegionEditor_RegionsChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }




        private void EncodingAMEStandard_Shown(object sender, EventArgs e)
        {
        }



        private void EncodingAMEStandard_Load(object sender, EventArgs e)
        {
            buttonShowEDL.Initialize();
            buttonShowEDL.EDLChanged += ButtonShowEDL_EDLChanged;

            buttonRegionEditor.Initialize(_firstAsset, _main, false, 1, true, AMSExplorer.Properties.Resources.EncodingMES_EncodingAMEStandard_Load_CroppingEditor);
            buttonRegionEditor.RegionsChanged += buttonRegionEditor_RegionsChanged;

            label4KWarning.Text = string.Empty;
            moreinfoame.Links.Add(new LinkLabel.Link(0, moreinfoame.Text.Length, Constants.LinkMoreInfoMES));
            moreinfopresetslink.Links.Add(new LinkLabel.Link(0, moreinfopresetslink.Text.Length, Constants.LinkMorePresetsMES));
            linkLabelInfoCropping.Links.Add(new LinkLabel.Link(0, linkLabelInfoCropping.Text.Length, Constants.LinkCroppingMES));
            linkLabelThumbnail1.Links.Add(new LinkLabel.Link(0, linkLabelThumbnail1.Text.Length, Constants.LinkThumbnailsMES));
            linkLabelThumbnail2.Links.Add(new LinkLabel.Link(0, linkLabelThumbnail2.Text.Length, Constants.LinkThumbnailsMES));
            linkLabelThumbnail3.Links.Add(new LinkLabel.Link(0, linkLabelThumbnail3.Text.Length, Constants.LinkThumbnailsMES));
            linkLabelInfoOverlay.Links.Add(new LinkLabel.Link(0, linkLabelInfoOverlay.Text.Length, Constants.LinkOverlayMES));
            linkLabelMESFeatures.Links.Add(new LinkLabel.Link(0, linkLabelMESFeatures.Text.Length, Constants.LinkMESAdvFeatures));
            comboBoxRotation.SelectedIndex = 0;

            labelProcessorVersion.Text = string.Format(labelProcessorVersion.Text, _processorVersion);

            if (_subclipConfig != null && _subclipConfig.Trimming)
            {
                // come from subclip UI

                // offset
                buttonShowEDL.Offset = _subclipConfig.OffsetForReencode;
                timeControlStartTime.ScaledFirstTimestampOffset = timeControlEndTime.ScaledFirstTimestampOffset = (ulong)_subclipConfig.OffsetForReencode.Ticks;
                // let's display the offset
                labelOffset.Visible = textBoxOffset.Visible = true;
                textBoxOffset.Text = _subclipConfig.OffsetForReencode.ToString();

                if (_subclipConfig.InOutForReencode.Count == 1) // one entry only, let's display in the two visible controls
                {
                    if (_subclipConfig.InOutForReencode[0].Start != null)
                    {
                        timeControlStartTime.SetTimeStamp((TimeSpan)_subclipConfig.InOutForReencode[0].Start);
                        checkBoxSourceTrimmingStart.Checked = true;
                    }
                    else
                    {
                        checkBoxSourceTrimmingStart.Checked = false;

                    }

                    if (_subclipConfig.InOutForReencode[0].End != null)
                    {
                        timeControlEndTime.SetTimeStamp((TimeSpan)_subclipConfig.InOutForReencode[0].End);
                        checkBoxSourceTrimmingEnd.Checked = true;
                    }
                    else
                    {
                        checkBoxSourceTrimmingEnd.Checked = false;

                    }
                    timeControlStartTime.TimeScale = timeControlEndTime.TimeScale = TimeSpan.TicksPerSecond;
                }
                else if (_subclipConfig.InOutForReencode.Count > 1)// let's use EDL
                {
                    checkBoxUseEDL.Checked = true;
                    buttonShowEDL.SetEDLEntries(_subclipConfig.InOutForReencode);
                }

                _disableOverlay = true;
            }

            if (_disableOverlay)//_nbInputAssets > 1)
            {
                checkBoxOverlay.Enabled = false; // no overlay if several assets have been selected
            }

            // presets list
            var filePaths = Directory.GetFiles(EncodingAMEStdPresetJSONFilesFolder, "*.json").Select(f => Path.GetFileNameWithoutExtension(f));
            listboxPresets.Items.AddRange(filePaths.ToArray());
            if (!_ThumbnailsModeOnly)
            {
                listboxPresets.SelectedIndex = listboxPresets.Items.IndexOf(defaultprofile);
            }
            else // Thumbnail mode only
            {
                tabControl1.SelectedTab = tabPageThPNG;
            }

            if (_disableOverlay)
            {
                checkBoxOverlay.Enabled = false;
            }

            if (_disableSourceTrimming)
            {
                groupBox1.Enabled = false;
            }

            if (_inputAssetsForJob.Count > 1)
            {
                comboBoxSourceAsset.Visible = labelInputAsset.Visible = true;
                _inputAssetsForJob.ForEach(a => comboBoxSourceAsset.Items.Add(new Item(string.Format("{0} ({1})", a.Name, a.Id), a.Id)));
                comboBoxSourceAsset.SelectedIndex = 0;
            }
            else
            {
                labelInfoSeveralAssetStitching.Visible = true;
            }

            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void ButtonShowEDL_EDLChanged(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
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
                    MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.EncodingMES_buttonLoadJSON_Click_ErrorCouldNotReadFileFromDiskOriginalError0, ex.Message));
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
            else if (mode == TypeConfig.JSON || mode == TypeConfig.Empty) // JSON
            {

                if (checkBoxAddAutomatic.Checked)
                {
                    if (mode == TypeConfig.Empty)
                    {
                        jsondata = @"{""Version"": 1.0}";
                    }

                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsondata);

                    ////////////////////////////
                    // Cleaning of JSON
                    ////////////////////////////

                    // clean auto deinterlaing
                    // clean all sources
                    //if (obj.Sources != null) obj.Sources.Parent.Remove();

                    // clean trimming
                    // clean deinterlace filter
                    // clean overlay
                    // clean cropping
                    // clean rotation


                    if (obj.Sources != null)
                    {
                        var listDelete = new List<dynamic>();
                        foreach (var source in obj.Sources)
                        {
                            listDelete.Add(source);

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
                                (codec.Type == "H264Video") &&
                                codec.Condition != null
                                && (codec.Condition == "InsertBlackIfNoVideoBottomLayerOnly" || codec.Condition == "InsertBlackIfNoVideo"))
                            {
                                codec.Condition.Parent.Remove();
                            }
                        }
                    }

                    // Clean InsertBlackvideo
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

                    // Clean StretchMode
                    if (obj.Codecs != null)
                    {
                        foreach (var codec in obj.Codecs)
                        {
                            if (codec.Type != null &&
                                (codec.Type == "H264Video") &&
                                codec.StretchMode != null)
                            {
                                codec.StretchMode.Parent.Remove();
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
                    if (obj.Outputs != null) // clean noninterleave and thumbnail entry in Outputs
                    {
                        var listDelete = new List<dynamic>();
                        foreach (var output in obj.Outputs)
                        {
                            if (output.Format != null && output.Format.Type != null && output.Format.Type.Type == JTokenType.String)
                            {
                                string valuestr = (string)output.Format.Type;
                                if (valuestr == "MP4Format" && output.Condition != null && output.Condition == "NonInterleaved")
                                {
                                    output.Condition.Parent.Remove();
                                }
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
                    if (checkBoxUseEDL.Checked)  // EDL MODE
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        foreach (var entry in buttonShowEDL.GetEDLEntries())
                        {
                            dynamic time = new JObject();

                            bool insert = false;
                            if (entry.AssetID != null)
                            {
                                time.AssetID = entry.AssetID;
                                insert = true;
                            }
                            if (entry.Start != null)
                            {
                                time.StartTime = entry.Start + buttonShowEDL.Offset;
                                insert = true;

                            }
                            if (entry.Duration != null)
                            {
                                time.Duration = entry.Duration;
                                insert = true;
                            }

                            if (insert)
                            {
                                obj.Sources.Add(time);
                            }
                        }
                    }
                    else if (checkBoxSourceTrimmingStart.Checked || checkBoxSourceTrimmingEnd.Checked) // Only start or end or both (no edl)
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        dynamic time = new JObject();
                        if (comboBoxSourceAsset.Items.Count > 1)
                        {
                            time.AssetId = ((Item)comboBoxSourceAsset.SelectedItem).Value;
                        }

                        if (checkBoxSourceTrimmingStart.Checked)
                        {
                            time.StartTime = timeControlStartTime.TimeStampWithOffset;
                            if (checkBoxSourceTrimmingEnd.Checked)
                            {
                                time.Duration = timeControlEndTime.TimeStampWithOffset - timeControlStartTime.TimeStampWithOffset;
                            }
                        }
                        else if (checkBoxSourceTrimmingEnd.Checked) // only end time specified
                        {
                            time.Duration = timeControlEndTime.TimeStampWithOffset - timeControlStartTime.GetOffSetAsTimeSpan();
                        }
                        obj.Sources.Add(time);
                    }
                    else if (!checkBoxSourceTrimmingStart.Checked && !checkBoxSourceTrimmingEnd.Checked && comboBoxSourceAsset.Items.Count > 1) // No time selected but several input assets
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }
                        dynamic time = new JObject();
                        time.AssetId = ((Item)comboBoxSourceAsset.SelectedItem).Value;
                        obj.Sources.Add(time);
                    }

                    // Subcliping - we need to add top bitrate values
                    if (_subclipConfig != null) // subclipping. we need to add top bitrate values
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        dynamic streamA = new JObject();
                        streamA.Type = "AudioStream";
                        streamA.Value = "TopBitrate";

                        dynamic streamV = new JObject();
                        streamV.Type = "VideoStream";
                        streamV.Value = "TopBitrate";

                        if (obj.Sources.Count > 0)
                        {
                            foreach (dynamic entry in obj.Sources)
                            {
                                entry.Streams = new JArray() as dynamic;
                                entry.Streams.Add(streamA);
                                entry.Streams.Add(streamV);
                            }
                        }
                        else
                        {
                            dynamic entry = new JObject() as dynamic;
                            entry.Streams = new JArray() as dynamic;
                            entry.Streams.Add(streamA);
                            entry.Streams.Add(streamV);
                            obj.Sources.Add(entry);
                        }
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


                        // let's prepare objects
                        dynamic VideoOverlayEntry = new JObject();
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

                        if (_firstAsset != null)
                        {
                            for (int i = 2; i < _firstAsset.AssetFiles.Count(); i++) // if more than two files in the asset, we need to add Mediaparams for each extra file
                            {
                                dynamic OverlayParam = new JObject();
                                OverlayParam.IsOverlay = false;
                                OverlayParam.OverlayLoopCount = 1;
                                MediaParams.Add(OverlayParam);
                            }
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


                        // now we put these objects in the preset
                        if (obj.Sources.Count > 0)
                        {
                            foreach (dynamic entry in obj.Sources)
                            {
                                if (entry.Filters != null)
                                {
                                    entry.Filters.Add(VideoOverlayEntry);
                                }
                                else
                                {
                                    entry.Filters = VideoOverlayEntry;

                                }

                                if (entry.Streams == null)
                                {
                                    entry.Streams = new JArray() as dynamic;
                                }
                            }
                        }
                        else
                        {
                            dynamic Source = new JObject();
                            obj.Sources.Add(Source);

                            if (Source.Filters != null)
                            {
                                Source.Filters.Add(VideoOverlayEntry);
                            }
                            else
                            {
                                Source.Filters = VideoOverlayEntry;
                            }

                            if (Source.Streams == null)
                            {
                                Source.Streams = new JArray() as dynamic;
                            }
                        }
                    }

                    // Video Cropping
                    if (checkBoxCropVideo.Checked && buttonRegionEditor.GetSavedPolygonesDecimalMode().Count > 0)

                    {
                        /*
                       {
"Version": 1.0,
"Sources": [
{
  "Streams": [],
  "Filters": {
    "Crop": {
        "X": 240,
        "Y": 0,
        "Width": 1440,
        "Height": 1080
    }
  },
  "Pad": true
}
],
"Codecs": [
{

    */

                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }


                        // let's prepare objects
                        dynamic CropEntry = new JObject();
                        dynamic Crop = new JObject();
                        CropEntry.Crop = Crop;

                        var CroppingRectangle = buttonRegionEditor.GetSavedPolygonesAsRectangleResolutionMode().FirstOrDefault();
                        Crop.X = CroppingRectangle.X;
                        Crop.Y = CroppingRectangle.Y;
                        Crop.Width = CroppingRectangle.Width;
                        Crop.Height = CroppingRectangle.Height;

                        // now we put these objects in the preset
                        if (obj.Sources.Count > 0)
                        {
                            foreach (dynamic entry in obj.Sources)
                            {
                                if (entry.Filters != null)
                                {
                                    entry.Filters.Crop = Crop; // Add(CropEntry);
                                }
                                else
                                {
                                    entry.Filters = CropEntry;

                                }

                                if (entry.Streams == null)
                                {
                                    entry.Streams = new JArray() as dynamic;
                                }
                            }
                        }
                        else
                        {
                            dynamic Source = new JObject();
                            obj.Sources.Add(Source);

                            if (Source.Filters != null)
                            {
                                Source.Filters.Add(CropEntry);
                            }
                            else
                            {
                                Source.Filters = CropEntry;
                            }

                            if (Source.Streams == null)
                            {
                                Source.Streams = new JArray() as dynamic;
                            }
                        }
                    }


                    // Auto rotation
                    if (comboBoxRotation.SelectedIndex > 0)

                    {
                        /*
                       {
"Version": 1.0,
  "Sources": [
    {
      "Filters": {
        "Rotation": "90"
      }
    }
  ]
],
"Codecs": [
{

    */

                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        // let's prepare objects
                        dynamic RotEntry = new JObject();
                        RotEntry.Rotation = (string)comboBoxRotation.SelectedItem;


                        // now we put these objects in the preset
                        if (obj.Sources.Count > 0)
                        {
                            foreach (dynamic entry in obj.Sources)
                            {
                                if (entry.Filters != null)
                                {
                                    entry.Filters.Rotation = comboBoxRotation.SelectedText;
                                }
                                else
                                {
                                    entry.Filters = RotEntry;

                                }

                                if (entry.Streams == null)
                                {
                                    entry.Streams = new JArray() as dynamic;
                                }
                            }
                        }
                        else
                        {
                            dynamic Source = new JObject();
                            obj.Sources.Add(Source);
                            Source.Filters = RotEntry;


                            if (Source.Streams == null)
                            {
                                Source.Streams = new JArray() as dynamic;
                            }
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

                    // Insert video at only the lowest bitrate
                    if (checkBoxInsertVideo.Checked)
                    {
                        if (obj.Codecs != null)
                        {
                            foreach (var codec in obj.Codecs)
                            {
                                if (codec.Type != null && codec.Type == "H264Video")
                                {
                                    codec.Condition = radioButtonOnlyLowestBitrate.Checked ? "InsertBlackIfNoVideoBottomLayerOnly" : "InsertBlackIfNoVideo";
                                }
                            }
                        }
                    }

                    // Autostrech mode to none ?
                    if (checkBoxDisableAutoStretchMode.Checked)
                    {
                        if (obj.Codecs != null)
                        {
                            foreach (var codec in obj.Codecs)
                            {
                                if (codec.Type != null && codec.Type == "H264Video")
                                {
                                    codec.StretchMode = "None";
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

                        dynamic modeeentry = new JObject() as dynamic;
                        modeeentry.Mode = "Off";
                        dynamic deinterlaceentry = new JObject() as dynamic;
                        deinterlaceentry.Deinterlace = modeeentry;


                        if (obj.Sources.Count > 0)
                        {
                            foreach (dynamic source in obj.Sources)
                            {
                                bool DeinterModeSet = false;
                                if (source.Filters != null)
                                {
                                    if (source.Filters.Deinterlace != null)
                                    {
                                        source.Filters.Deinterlace.Mode = "Off";
                                    }
                                    else
                                    {
                                        source.Filters.Deinterlace = modeeentry;
                                    }
                                    DeinterModeSet = true;
                                }

                                if (!DeinterModeSet)
                                {
                                    source.Filters = deinterlaceentry;
                                }
                            }
                        }
                        else // no source
                        {
                            dynamic sourceentry = new JObject() as dynamic;
                            sourceentry.Filters = deinterlaceentry;
                            obj.Sources.Add(sourceentry);
                        }
                    }

                    // non interleave audio and video
                    if (checkBoxDoNotInterleave.Checked)
                    {
                        if (obj.Outputs != null)
                        {
                            foreach (var output in obj.Outputs)
                            {
                                if (output.Format != null && output.Format.Type != null && output.Format.Type.Type == JTokenType.String)
                                {
                                    string valuestr = (string)output.Format.Type;
                                    if (valuestr == "MP4Format")
                                    {
                                        output.Condition = "NonInterleaved";
                                    }
                                }
                            }
                        }
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
                    textBoxConfiguration.Text = obj.ToString();
                }
            }
            else // no xml and no Json !
            {
                textBoxConfiguration.Text = jsondata;
            }

           
        }

        private decimal calculateMultiplier(dynamic obj)
        {
            decimal multiplier = 0;

            if (obj.Codecs != null)
            {
                foreach (var codec in obj.Codecs)
                {
                    if (codec.Type != null && codec.Type == "H264Video" && codec.H264Layers != null)
                    {
                        foreach (var h264 in codec.H264Layers)
                        {
                            if (h264.Width != null && h264.Height != null)
                            {
                                int res = h264.Width * h264.Height;
                                int value;
                                if (res < 1280 * 720)
                                {
                                    value = 1;
                                }
                                else if (res < 1920 * 1080)
                                {
                                    value = 2;
                                }
                                else if (res < 4096 * 2304)
                                {
                                    value = 4;
                                }
                                else
                                {
                                    value = 4; // Can be changed in the future
                                }
                                multiplier += value;
                            }
                        }
                    }
                    else if (codec.Type != null && codec.Type == "AACAudio")
                    {
                        multiplier += 0.25m;
                    }
                }
            }

            return multiplier;
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
                    MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.EncodingAMEAdv_buttonSaveXML_Click_ErrorCouldNotSaveFileToDiskOriginalError0, ex.Message));
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
                    MessageBox.Show(AMSExplorer.Properties.Resources.EncodingMES_listboxPresets_SelectedIndexChanged_ErrorCouldNotReadFileFromDiskOrProcessTheJSONDataOriginalError + Constants.endline + ex.Message);
                    usereditmode = true;
                }

                if (listboxPresets.SelectedItem.ToString().Contains("4K") && _context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType != ReservedUnitType.Premium)
                {
                    label4KWarning.Visible = true;
                    buttonOk.Enabled = false;
                }
                else
                {
                    label4KWarning.Visible = false;
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
            decimal multiplier = 0;

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
                    multiplier = calculateMultiplier(jo);
                }
                catch (Exception ex)
                {
                    labelWarningJSON.Text = string.Format(_labelWarningJSON, ex.Message);
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
                    labelWarningJSON.Text = string.Format(AMSExplorer.Properties.Resources.EncodingMES_textBoxConfiguration_TextChanged_ErrorInXMLData0, ex.Message);
                    Error = true;
                }
            }
            labelWarningJSON.Visible = Error;

            // Display multiplier
            if (multiplier > 0)
            {
                labelOutputMinuteMultiplier.Text = string.Format(_multiplierlabel, multiplier, Properties.Settings.Default.Currency, multiplier* Properties.Settings.Default.MESPricePerMin);
            }
            else
            {
                labelOutputMinuteMultiplier.Text = "";
            }
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
                    textBoxSourceDurationTime.Text = (timeControlEndTime.TimeStampWithOffset - timeControlStartTime.TimeStampWithOffset).ToString();
                }
                else
                {
                    textBoxSourceDurationTime.Text = string.Empty;
                }
            }
            else if (checkBoxSourceTrimmingEnd.Checked) // only end time specified
            {
                textBoxSourceDurationTime.Text = (timeControlEndTime.TimeStampWithOffset - timeControlStartTime.GetOffSetAsTimeSpan()).ToString();
            }

        }

        private void checkBoxSourceTrimming_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStartTime.Enabled = checkBoxSourceTrimmingStart.Checked;
            buttonAddEDLEntry.Enabled = /* checkBoxSourceTrimmingStart.Checked && checkBoxSourceTrimmingEnd.Checked && */ checkBoxUseEDL.Checked;
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
            var form = new EncodingMESPickOverlay(_firstAsset);
            if (form.ShowDialog() == DialogResult.OK)
            {
                textBoxOverlayFileName.Text = form.SelectedAssetFile.Name;
                CheckOverlayFile();
                UpdateTextBoxJSON(textBoxConfiguration.Text);
            }
            _firstAsset = _context.Assets.Where(a => a.Id == _firstAsset.Id).FirstOrDefault(); // let's refresh if the user added one or several files to it
        }

        private void checkBoxSourceTrimmingEnd_CheckedChanged(object sender, EventArgs e)
        {
            timeControlEndTime.Enabled = textBoxSourceDurationTime.Enabled = checkBoxSourceTrimmingEnd.Checked;
            buttonAddEDLEntry.Enabled = /* checkBoxSourceTrimmingStart.Checked && checkBoxSourceTrimmingEnd.Checked && */ checkBoxUseEDL.Checked;
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
                errorProvider1.SetError(textBoxOverlayFileName, AMSExplorer.Properties.Resources.EncodingMES_CheckOverlayFile_SelectAFileInTheSourceAsset);
            }
            else
            {
                errorProvider1.SetError(textBoxOverlayFileName, String.Empty);
            }
        }

        private void buttonAddEDLEntry_Click(object sender, EventArgs e)
        {

            if (checkBoxSourceTrimmingEnd.Checked && !checkBoxSourceTrimmingStart.Checked)
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.EncodingMES_buttonAddEDLEntry_Click_YouCannotSpecifyOnlyAnEndTime, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (checkBoxSourceTrimmingStart.Checked)
            {

                buttonShowEDL.AddEDLEntry(new ExplorerEDLEntryInOut()
                {
                    Start = timeControlStartTime.TimeStampWithoutOffset,
                    End = checkBoxSourceTrimmingEnd.Checked ? timeControlEndTime.TimeStampWithoutOffset : (TimeSpan?)null,
                    AssetID = comboBoxSourceAsset.Items.Count > 1 ? ((Item)comboBoxSourceAsset.SelectedItem).Value : null,
                    Offset = _subclipConfig != null ? _subclipConfig.OffsetForReencode : (TimeSpan?)null
                });
            }
            else if (!checkBoxSourceTrimmingStart.Checked && !checkBoxSourceTrimmingEnd.Checked)
            {
                buttonShowEDL.AddEDLEntry(new ExplorerEDLEntryInOut()
                {
                    Start = new TimeSpan(0),
                    //End = checkBoxSourceTrimmingEnd.Checked ? timeControlEndTime.TimeStampWithoutOffset : (TimeSpan?)null,
                    AssetID = comboBoxSourceAsset.Items.Count > 1 ? ((Item)comboBoxSourceAsset.SelectedItem).Value : null,
                    Offset = _subclipConfig != null ? _subclipConfig.OffsetForReencode : (TimeSpan?)null
                });
            }

        }

        private void checkBoxUseEDL_CheckedChanged(object sender, EventArgs e)
        {
            buttonShowEDL.Enabled = buttonAddEDLEntry.Enabled = checkBoxUseEDL.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void numericUpDownThWidthPNG_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            var v = nud.Value;
        }

        private void checkBoxCropVideo_CheckedChanged(object sender, EventArgs e)
        {
            buttonRegionEditor.Enabled = checkBoxCropVideo.Checked;
        }

        private void buttonShowEDL_Click(object sender, EventArgs e)
        {

        }


        private void UpdateJSON(object sender, EventArgs e)
        {
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxInsertVideo_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonOnlyLowestBitrate.Enabled = radioButtonAllBitrates.Enabled = checkBoxInsertVideo.Checked;
            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }
    }

    enum TypeConfig
    {
        JSON,
        XML,
        Empty,
        Other
    }
}
