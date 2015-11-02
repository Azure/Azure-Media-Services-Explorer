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

        private List<IMediaProcessor> Procs;
        public List<IAsset> SelectedAssets;
        private CloudMediaContext _context;

        private const string defaultprofile = "H264 Multiple Bitrate 720p";
        bool usereditmode = false;
      
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


        public List<IMediaProcessor> EncodingProcessorsList
        {
            set
            {
                foreach (IMediaProcessor pr in value)
                {
                    comboBoxProcessor.Items.Add(string.Format("{0} {1} Version {2} ({3})", pr.Vendor, pr.Name, pr.Version, pr.Description));
                }
                if (comboBoxProcessor.Items.Count > 0)
                {
                    comboBoxProcessor.SelectedIndex = 0;
                }
                Procs = value;
            }
        }

        public IMediaProcessor EncodingProcessorSelected
        {
            get
            {
                return Procs[comboBoxProcessor.SelectedIndex];
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


        public EncodingAMEStandard(CloudMediaContext context, int nbInputAssets, SubClipConfiguration subclipConfig = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _subclipConfig = subclipConfig; // used for trimming
            buttonJobOptions.Initialize(_context);
            _nbInputAssets = nbInputAssets;
        }


        private void EncodingAMEStandard_Shown(object sender, EventArgs e)
        {
        }

        private void EncodingAMEStandard_Load(object sender, EventArgs e)
        {
            // presets list
            var filePaths = Directory.GetFiles(EncodingAMEStdPresetJSONFilesFolder, "*.json").Select(f => Path.GetFileNameWithoutExtension(f));
            listboxPresets.Items.AddRange(filePaths.ToArray());
            listboxPresets.SelectedIndex = listboxPresets.Items.IndexOf(defaultprofile);
            label4KWarning.Text = string.Empty;
            moreinfoame.Links.Add(new LinkLabel.Link(0, moreinfoame.Text.Length, Constants.LinkMoreInfoMES));
            moreinfopresetslink.Links.Add(new LinkLabel.Link(0, moreinfopresetslink.Text.Length, Constants.LinkMorePresetsMES));

            if (_subclipConfig != null && _subclipConfig.Trimming)
            {
                timeControlStartTime.SetTimeStamp(_subclipConfig.StartTimeForReencode);
                timeControlEndTime.SetTimeStamp(_subclipConfig.StartTimeForReencode + _subclipConfig.DurationForReencode);
                checkBoxSourceTrimming.Checked = true;
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
            else if (mode== TypeConfig.JSON) // JSON
            {
                var jo = JObject.Parse(jsondata);
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsondata);
                if (checkBoxAddAutomatic.Checked)
                {
                    // Cleaning
                    if (obj.Sources != null) obj.Sources.Parent.Remove();

                    if (checkBoxSourceTrimming.Checked)
                    {
                        if (obj.Sources == null)
                        {
                            obj.Sources = new JArray() as dynamic;
                        }

                        dynamic time = new JObject();
                        time.StartTime = timeControlStartTime.GetTimeStampAsTimeSpanWithOffset();
                        time.Duration = timeControlEndTime.GetTimeStampAsTimeSpanWithOffset() - timeControlStartTime.GetTimeStampAsTimeSpanWithOffset();
                        obj.Sources.Add(time);
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
                }
                textBoxConfiguration.Text = obj.ToString();
            }
            else // no xml and no Json !
            {
                textBoxConfiguration.Text = jsondata;
            }
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

            bool Error = false;
            var type = Program.AnalyseConfigurationString(textBoxConfiguration.Text);
            if (type==TypeConfig.JSON)
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
            else if(type == TypeConfig.XML) // XML 
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

        private void moreinfopresetslink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            textBoxSourceDurationTime.Text = (timeControlEndTime.GetTimeStampAsTimeSpanWithOffset() - timeControlStartTime.GetTimeStampAsTimeSpanWithOffset()).ToString();
        }

        private void checkBoxSourceTrimming_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStartTime.Enabled =
                timeControlEndTime.Enabled =
                textBoxSourceDurationTime.Enabled =
                checkBoxSourceTrimming.Checked;

            UpdateTextBoxJSON(textBoxConfiguration.Text);
        }

        private void checkBoxAddAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxTrim.Enabled = checkBoxAddAutomatic.Checked;
        }
    }

    enum TypeConfig
    {
        JSON,
        XML,
        Other
    }


}
