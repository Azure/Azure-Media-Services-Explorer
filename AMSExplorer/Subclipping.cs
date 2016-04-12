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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json.Linq;

namespace AMSExplorer
{
    public partial class Subclipping : Form
    {
        CloudMediaContext _context;
        private List<IAsset> _selectedAssets;
        private ManifestTimingData _parentassetmanifestdata;
        private ulong? _timescale = TimeSpan.TicksPerSecond;
        ILocator _tempLocator = null; // for preview
        Mainform _mainform;
        bool backupCheckboxTrim = false; // used when user select reencode to save the status of trim checkbox

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

        public Subclipping(CloudMediaContext context, List<IAsset> assetlist, Mainform mainform)
        {
            InitializeComponent();
            buttonJobOptions.Initialize(context);
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _parentassetmanifestdata = new ManifestTimingData();
            _selectedAssets = assetlist;
            _mainform = mainform;

            buttonShowEDL.Initialize();
            buttonShowEDL.EDLChanged += ButtonShowEDL_EDLChanged;
            buttonShowEDL.Offset = new TimeSpan(0);

            if (_selectedAssets.Count == 1 && _selectedAssets.FirstOrDefault() != null)  // one asset only
            {
                var myAsset = assetlist.FirstOrDefault();
                textBoxAssetName.Text = myAsset.Name;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(myAsset);

                if (!_parentassetmanifestdata.Error)  // we were able to read asset timings and not live
                {
                    _timescale = timeControlStart.TimeScale = timeControlEnd.TimeScale = _parentassetmanifestdata.TimeScale;
                    timeControlStart.ScaledFirstTimestampOffset = timeControlEnd.ScaledFirstTimestampOffset = _parentassetmanifestdata.TimestampOffset;
                    buttonShowEDL.Offset = timeControlStart.GetOffSetAsTimeSpan();

                    textBoxOffset.Text = _parentassetmanifestdata.TimestampOffset.ToString();
                    labelOffset.Visible = textBoxOffset.Visible = true;

                    textBoxFilterTimeScale.Text = _timescale.ToString();
                    textBoxFilterTimeScale.Visible = labelAssetTimescale.Visible = true;

                    timeControlStart.Max = timeControlEnd.Max = _parentassetmanifestdata.AssetDuration;

                    labelassetduration.Visible = textBoxAssetDuration.Visible = true;
                    textBoxAssetDuration.Text = timeControlStart.Max.ToString(@"d\.hh\:mm\:ss") + (_parentassetmanifestdata.IsLive ? " (LIVE)" : "");
                    // let set duration and active track bat
                    timeControlStart.TotalDuration = timeControlEnd.TotalDuration = _parentassetmanifestdata.AssetDuration;
                    timeControlStart.DisplayTrackBar = true;
                    timeControlEnd.DisplayTrackBar = true;
                    timeControlEnd.SetTimeStamp(timeControlEnd.Max);

                }

                else // one asset but not able to read asset timings
                {
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = false;
                    timeControlStart.TimeScale = timeControlEnd.TimeScale = _timescale;
                    timeControlStart.Max = timeControlEnd.Max = TimeSpan.MaxValue;
                    //timeControlEnd.SetTimeStamp(timeControlEnd.Max);
                }
            }
            else // several assets
            {
                groupBoxTrimming.Enabled = panelAssetInfo.Visible = false; // no trimming and no asset info
                radioButtonAssetFilter.Enabled = false; // no asset filter option
                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = false;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = _timescale;
                timeControlStart.Max = timeControlEnd.Max = TimeSpan.MaxValue;
                //timeControlEnd.SetTimeStamp(timeControlEnd.Max);
            }
        }

        private void ButtonShowEDL_EDLChanged(object sender, EventArgs e)
        {
            UpdateXMLData();
        }

        private void Subclipping_Load(object sender, EventArgs e)
        {
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoSubClipAMSE));
            CheckIfErrorTimeControls();
            DisplayAccuracy();
        }


        private SubClipTrimmingDataXMLSerialized GetSubClipTrimmingDataXMLSerialized()
        {
            var trimmingdata = new SubClipTrimmingDataXMLSerialized();
            if (checkBoxTrimming.Checked)
            {
                trimmingdata.StartTime = AssetInfo.GetXMLSerializedTimeSpan(timeControlStart.TimeStampWithOffset);
                trimmingdata.EndTime = AssetInfo.GetXMLSerializedTimeSpan(timeControlEnd.TimeStampWithOffset);
                trimmingdata.Duration = AssetInfo.GetXMLSerializedTimeSpan(timeControlEnd.TimeStampWithOffset - timeControlStart.TimeStampWithOffset);
            }
            return trimmingdata;
        }


        private SubClipTrimmingDataTimeSpan GetSubClipTrimmingDataTimeSpan()
        {
            var trimmingdata = new SubClipTrimmingDataTimeSpan();
            if (checkBoxTrimming.Checked)
            {
                trimmingdata.StartTime = timeControlStart.TimeStampWithOffset;
                trimmingdata.EndTime = timeControlEnd.TimeStampWithOffset;
                trimmingdata.Duration = trimmingdata.EndTime - trimmingdata.StartTime;
                trimmingdata.Offset = timeControlStart.GetOffSetAsTimeSpan();
            }
            return trimmingdata;
        }

        public SubClipConfiguration GetSubclippingConfiguration()
        {
            var config = GetSubclippingInternalConfiguration();
            if (!radioButtonClipWithReencode.Checked && !string.IsNullOrEmpty(textBoxConfiguration.Text))
            {
                config.Configuration = textBoxConfiguration.Text;
            }
            return config;
        }


        private SubClipConfiguration GetSubclippingInternalConfiguration()
        {
            if (radioButtonArchiveAllBitrate.Checked || radioButtonArchiveTopBitrate.Checked) // Archive, no reencoding
            {
                /*
                SAMPLE JSON

                                {
                  "Version": 1.0,
                  "Sources": [
                    {
                      "StartTime": "20.13:05:33.0520000",
                      "Duration": "00:00:44.7100000",
                      "Streams": [
                        {
                          "Type": "AudioStream",
                          "Value": "TopBitrate"
                        },
                        {
                          "Type": "VideoStream",
                          "Value": "TopBitrate"
                        }
                      ]
                    }
                  ],
                  "Outputs": [
                    {
                      "FileName": "ArchiveTopBitrate_{Basename}.mp4",
                      "Format": {
                        "Type": "MP4Format"
                      }
                    }
                  ],
                  "Codecs": [
                    {
                      "Type": "CopyVideo"
                    },
                    {
                      "Type": "CopyAudio"
                    }
                  ]
                }

                */


                var obj = new JObject() as dynamic;
                obj.Version = 1.0;

                // Sources
                obj.Sources = new JArray() as dynamic;

                string filter = radioButtonArchiveAllBitrate.Checked ? "*" : "TopBitrate";
                string mode = radioButtonArchiveAllBitrate.Checked ? "ArchiveAllBitrates" : "ArchiveTopBitrate";

                dynamic stream_a = new JObject();
                stream_a.Type = "AudioStream";
                stream_a.Value = filter;
                dynamic stream_v = new JObject();
                stream_v.Type = "VideoStream";
                stream_v.Value = filter;

                if (checkBoxTrimming.Checked && checkBoxUseEDL.Checked) // EDL
                {
                    foreach (var entry in buttonShowEDL.EDLEntries)
                    {
                        dynamic sourceEntry = new JObject() as dynamic;
                        sourceEntry.StartTime = entry.Start + buttonShowEDL.Offset;
                        sourceEntry.Duration = entry.Duration;
                        sourceEntry.Streams = new JArray() as dynamic;

                        sourceEntry.Streams.Add(stream_a);
                        sourceEntry.Streams.Add(stream_v);

                        obj.Sources.Add(sourceEntry);
                    }
                }
                else // No EDL
                {
                    dynamic sourceEntry = new JObject() as dynamic;

                    if (checkBoxTrimming.Checked) // with trimming
                    {
                        sourceEntry.StartTime = timeControlStart.TimeStampWithOffset;
                        sourceEntry.Duration = timeControlEnd.TimeStampWithOffset - timeControlStart.TimeStampWithOffset;
                    }
                    sourceEntry.Streams = new JArray() as dynamic;

                    sourceEntry.Streams.Add(stream_a);
                    sourceEntry.Streams.Add(stream_v);

                    obj.Sources.Add(sourceEntry);
                }


                obj.Outputs = new JArray() as dynamic;
                dynamic output = new JObject();
                output.FileName = mode + "_{Basename}.mp4";

                dynamic formatentry = new JObject();
                formatentry.Type = "MP4Format";

                output.Format = formatentry;

                obj.Outputs.Add(output);

                obj.Codecs = new JArray();
                dynamic streamcopy = new JObject();
                streamcopy.Type = "CopyVideo";
                obj.Codecs.Add(streamcopy);
                streamcopy = new JObject();
                streamcopy.Type = "CopyAudio";
                obj.Codecs.Add(streamcopy);

                return new SubClipConfiguration()
                {
                    Configuration = obj.ToString(),
                    Reencode = false,
                    Trimming = false,
                    CreateAssetFilter = false
                };

            }
            else if (radioButtonClipWithReencode.Checked) // means Reencoding
            {
                var config = new SubClipConfiguration()
                {
                    Reencode = true,
                    Trimming = false,
                    CreateAssetFilter = false
                };

                if (checkBoxTrimming.Checked)
                {
                    config.Trimming = true;
                    var list = new List<ExplorerEDLEntryInOut>();

                    if (checkBoxUseEDL.Checked) // EDL
                    {
                        foreach (var entry in buttonShowEDL.EDLEntries)
                        {
                            list.Add(new ExplorerEDLEntryInOut() { Start = entry.Start, End = entry.End });
                        }
                        config.OffsetForReencode = timeControlStart.GetOffSetAsTimeSpan();
                    }
                    else  // No EDL
                    {
                        var subdata = GetSubClipTrimmingDataTimeSpan();
                        list.Add(new ExplorerEDLEntryInOut() { Start = subdata.StartTime - subdata.Offset, End = subdata.EndTime - subdata.Offset });
                        config.OffsetForReencode = subdata.Offset;
                    }
                    config.InOutForReencode = list;

                }
                return config;
            }
            else  // means asset filter
            {
                var config = new SubClipConfiguration()
                {
                    Reencode = false,
                    Trimming = false,
                    CreateAssetFilter = true,
                };

                if (checkBoxTrimming.Checked)
                {
                    var subdata = GetSubClipTrimmingDataTimeSpan();
                    config.Trimming = true;
                    config.StartTimeForAssetFilter = subdata.StartTime;
                    config.EndTimeForAssetFilter = subdata.EndTime;
                }
                return config;
            }
        }


        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }


        private void CheckIfErrorTimeControls()
        {
            // time start control
            if (checkBoxTrimming.Checked && timeControlStart.TimeStampWithoutOffset > timeControlEnd.TimeStampWithoutOffset)
            {
                errorProvider1.SetError(timeControlStart, "Start time must be lower than end time");
            }
            else
            {
                errorProvider1.SetError(timeControlStart, String.Empty);
            }

            // time end control
            if (checkBoxTrimming.Checked && timeControlEnd.TimeStampWithoutOffset < timeControlStart.TimeStampWithoutOffset)
            {
                errorProvider1.SetError(timeControlEnd, "End time must be higher than start time");
            }
            else
            {
                errorProvider1.SetError(timeControlEnd, String.Empty);
            }
        }


        private void timeControlEnd_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
            ResetConfigXML();
            UpdateDurationText();
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void timeControlStart_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
            ResetConfigXML();
            UpdateDurationText();
        }

        private void UpdateDurationText()
        {
            textBoxDurationTime.Text = (timeControlEnd.TimeStampWithOffset - timeControlStart.TimeStampWithOffset).ToString();
        }

        private void checkBoxTrimming_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonClipWithReencode.Checked && !radioButtonAssetFilter.Checked)
            {
                backupCheckboxTrim = checkBoxTrimming.Checked; // let's save status
            }

            timeControlStart.Enabled =
            timeControlEnd.Enabled =
            textBoxDurationTime.Enabled =
            checkBoxPreviewStream.Enabled =
            checkBoxUseEDL.Enabled =
             checkBoxTrimming.Checked;

            CheckIfErrorTimeControls();
            ResetConfigXML();
            PlaybackAsset();
        }

        private void UpdateXMLData()
        {
            textBoxConfiguration.Text = GetSubclippingInternalConfiguration().Configuration;
        }

        private void tabPageXML_Enter(object sender, EventArgs e)
        {
            UpdateXMLData();
        }

        private void radioButtonClipWithReencode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton senderr = sender as RadioButton;

            if ((radioButtonClipWithReencode.Checked && senderr.Name == radioButtonClipWithReencode.Name)  // reencoding
                ||
                (radioButtonAssetFilter.Checked && senderr.Name == radioButtonAssetFilter.Name)) // asset filtering
            {
                checkBoxTrimming.Checked = true;
                checkBoxTrimming.Enabled = false;
                textBoxConfiguration.Enabled = panelJob.Visible = false;
                UpdateButtonOk();
                ResetConfigXML();
                DisplayAccuracy();
                labelNoJSONBecauseReencoding.Visible = true;
                label3.Visible = textBoxConfiguration.Visible = false;

                if (senderr.Name == radioButtonClipWithReencode.Name) //reencoding
                {
                    labelNoJSONBecauseReencoding.Text = @"No JSON shown yet in this scenario. Click ""Subclip..."" to submit a task, and then a dialog will pop up allowing you to modify the encode settings.";
                    panelEDL.Visible = true;
                }
                else
                {
                    labelNoJSONBecauseReencoding.Text = @"No JSON shown in this scenario. Click ""Create filter..."" to create an asset filter, and then a dialog will pop up to create the filter with the specified start and times.";
                    panelEDL.Visible = false;
                }
            }
            else if ((radioButtonArchiveAllBitrate.Checked && senderr.Name == radioButtonArchiveAllBitrate.Name) // archive all bitrate
                ||
                (radioButtonArchiveTopBitrate.Checked && senderr.Name == radioButtonArchiveTopBitrate.Name))  // archive top bitrate
            {
                checkBoxTrimming.Checked = backupCheckboxTrim;
                checkBoxTrimming.Enabled = true;
                textBoxConfiguration.Enabled = panelJob.Visible = true;
                UpdateButtonOk();
                ResetConfigXML();
                DisplayAccuracy();
                labelNoJSONBecauseReencoding.Visible = false;
                label3.Visible = textBoxConfiguration.Visible = true;
                panelEDL.Visible = true;
            }
        }

        private void UpdateButtonOk()
        {
            if (radioButtonArchiveAllBitrate.Checked || radioButtonArchiveTopBitrate.Checked)
            {
                buttonOk.Text = ((string)buttonOk.Tag);
            }
            else if (radioButtonClipWithReencode.Checked)
            {
                buttonOk.Text = ((string)buttonOk.Tag) + "...";
            }
            else if (radioButtonAssetFilter.Checked)
            {
                buttonOk.Text = "Create filter...";
            }
        }

        private void DisplayAccuracy()
        {
            labelAccurate.Text = string.Format((labelAccurate.Tag as string), radioButtonClipWithReencode.Checked ? "Frame" : "GOP");

            if (radioButtonAssetFilter.Checked)
            {
                labeloutoutputasset.Text = "Asset Filter Name :";
            }
            else
            {
                labeloutoutputasset.Text = (string)labeloutoutputasset.Tag;
            }

        }


        private void ResetConfigXML()
        {
            textBoxConfiguration.Text = string.Empty;
        }



        private void Subclipping_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_tempLocator != null)
            {
                try
                {
                    _tempLocator.Delete();
                }
                catch
                {

                }
            }

            // let's sure we dispose the webbrowser control
            webBrowserPreview.Url = null;
            webBrowserPreview.Dispose();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBoxPreviewStream_CheckedChanged_1(object sender, EventArgs e)
        {
            PlaybackAsset();
        }

        private void PlaybackAsset()
        {
            if (checkBoxPreviewStream.Checked && checkBoxTrimming.Checked)
            {
                IAsset myAsset = _selectedAssets.FirstOrDefault();

                Uri myuri = AssetInfo.GetValidOnDemandURI(myAsset);
                if (myuri == null)
                {
                    _tempLocator = AssetInfo.CreatedTemporaryOnDemandLocator(myAsset);
                    myuri = AssetInfo.GetValidOnDemandURI(myAsset);
                }
                if (myuri != null)
                {
                    string myurl = AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayerFrame, Urlstr: myuri.ToString(), DoNotRewriteURL: true, context: _context, formatamp: AzureMediaPlayerFormats.Auto, technology: AzureMediaPlayerTechnologies.Auto, launchbrowser: false, UISelectSEFiltersAndProtocols: false, mainForm: _mainform);
                    webBrowserPreview.Url = new Uri(myurl);
                }
            }
            else
            {
                webBrowserPreview.Url = null;
            }

        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            DoSubClip();
        }

        private void DoSubClip()
        {
            var subclipConfig = this.GetSubclippingConfiguration();

            if (subclipConfig.Reencode) // reencode the clip
            {
                var processor = Mainform.GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);
                EncodingAMEStandard form2 = new EncodingAMEStandard(_context, _selectedAssets.Count, processor.Version, subclipConfig)
                {
                    EncodingLabel = (_selectedAssets.Count > 1) ?
                                    string.Format("{0} asset{1} selected. You are going to submit {0} job{1} with 1 task.", _selectedAssets.Count, Program.ReturnS(_selectedAssets.Count), _selectedAssets.Count)
                                    :
                                    "Asset '" + _selectedAssets.FirstOrDefault().Name + "' will be encoded (1 job with 1 task).",

                    EncodingJobName = "Subclipping with reencoding of " + Constants.NameconvInputasset,
                    EncodingOutputAssetName = Constants.NameconvInputasset + "- Subclipped with reencoding",
                    EncodingAMEStdPresetJSONFilesUserFolder = Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder,
                    EncodingAMEStdPresetJSONFilesFolder = Application.StartupPath + Constants.PathAMEStdFiles,
                    SelectedAssets = _selectedAssets
                };

                if (form2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string taskname = "Subclipping with reencoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
                    _mainform.LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(
                       processor,
                       _selectedAssets,
                       form2.EncodingJobName,
                       form2.JobOptions.Priority,
                       taskname,
                       form2.EncodingOutputAssetName,
                       new List<string>() { form2.EncodingConfiguration },
                       form2.JobOptions.OutputAssetsCreationOptions,
                       form2.JobOptions.TasksOptionsSetting,
                       form2.JobOptions.StorageSelected);
                }
            }
            else if (subclipConfig.CreateAssetFilter) // create a asset filter
            {
                IAsset selasset = _selectedAssets.FirstOrDefault();
                DynManifestFilter formAF = new DynManifestFilter(_context, null, selasset, subclipConfig);
                if (formAF.ShowDialog() == DialogResult.OK)
                {
                    FilterCreationInfo filterinfo = null;
                    try
                    {
                        filterinfo = formAF.GetFilterInfo;
                        selasset.AssetFilters.Create(filterinfo.Name, filterinfo.Presentationtimerange, filterinfo.Trackconditions);
                        _mainform.TextBoxLogWriteLine("Asset filter '{0}' created.", filterinfo.Name);
                    }
                    catch (Exception ex)
                    {
                        _mainform.TextBoxLogWriteLine("Error when creating filter '{0}'.", (filterinfo != null && filterinfo.Name != null) ? filterinfo.Name : "unknown name", true);
                        _mainform.TextBoxLogWriteLine(ex);
                    }

                    _mainform.DoRefreshGridFiltersV(false);
                }

            }
            else // no reencode or asset filter but stream copy
            {
                string taskname = "Subclipping (archive extraction) of " + Constants.NameconvInputasset;
                IMediaProcessor Proc = Mainform.GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);

                _mainform.LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(
                    Proc,
                    _selectedAssets,
                    this.EncodingJobName,
                    this.JobOptions.Priority,
                    taskname,
                    this.EncodingOutputAssetName,
                    new List<string>() { subclipConfig.Configuration },
                    this.JobOptions.OutputAssetsCreationOptions,
                    this.JobOptions.TasksOptionsSetting,
                    this.JobOptions.StorageSelected);

                MessageBox.Show("Subclipping job(s) submitted", "Sublipping", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonShowEDL.AddEDLEntry(new ExplorerEDLEntryInOut()
            {
                Start = timeControlStart.TimeStampWithoutOffset,
                End = timeControlEnd.TimeStampWithoutOffset
            });
            //ResetConfigXML();
        }

        private void checkBoxUseEDL_CheckedChanged(object sender, EventArgs e)
        {
            buttonShowEDL.Enabled = buttonAddEDLEntry.Enabled = checkBoxUseEDL.Checked;
            ResetConfigXML();
        }
    }
}
