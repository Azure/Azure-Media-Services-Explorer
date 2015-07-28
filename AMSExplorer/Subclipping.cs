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


        private void Subclipping_Load(object sender, EventArgs e)
        {
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkHowIMoreInfoSubclipping));
            CheckIfErrorTimeControls();
            DisplayAccuracy();
        }


        private SubClipTrimmingDataXMLSerialized GetSubClipTrimmingDataXMLSerialized()
        {
            var trimmingdata = new SubClipTrimmingDataXMLSerialized();
            if (checkBoxTrimming.Checked)
            {
                trimmingdata.StartTime = AssetInfo.GetXMLSerializedTimeSpan(timeControlStart.GetTimeStampAsTimeSpanWithOffset());
                trimmingdata.EndTime = AssetInfo.GetXMLSerializedTimeSpan(timeControlEnd.GetTimeStampAsTimeSpanWithOffset());
                trimmingdata.Duration = AssetInfo.GetXMLSerializedTimeSpan(timeControlEnd.GetTimeStampAsTimeSpanWithOffset() - timeControlStart.GetTimeStampAsTimeSpanWithOffset());
            }
            return trimmingdata;
        }


        private SubClipTrimmingDataTimeSpan GetSubClipTrimmingDataTimeSpan()
        {
            var trimmingdata = new SubClipTrimmingDataTimeSpan();
            if (checkBoxTrimming.Checked)
            {
                trimmingdata.StartTime = timeControlStart.GetTimeStampAsTimeSpanWithOffset();
                trimmingdata.EndTime = timeControlEnd.GetTimeStampAsTimeSpanWithOffset();
                trimmingdata.Duration = trimmingdata.EndTime - trimmingdata.StartTime;
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
                // Prepare the Subclipping xml
                XDocument doc = XDocument.Load(Path.Combine(Application.StartupPath + Constants.PathConfigFiles, "RenderedSubclip.xml"));
                XNamespace ns = "http://www.windowsazure.com/media/encoding/Preset/2014/03";

                var presetxml = doc.Element(ns + "Preset");
                var sourcexml = presetxml.Element(ns + "Sources").Element(ns + "Source");
                var streamsxml = sourcexml.Element(ns + "Streams");
                var output = presetxml.Element(ns + "Outputs").Element(ns + "Output"); ;

                string filter = radioButtonArchiveAllBitrate.Checked ? "*" : "TopBitrate";
                string mode = radioButtonArchiveAllBitrate.Checked ? "ArchiveAllBitrates" : "ArchiveTopBitrate";

                streamsxml.Add(new XElement(ns + "VideoStream", filter));
                streamsxml.Add(new XElement(ns + "AudioStream", filter));
                output.Attribute("FileName").SetValue(mode + "_{Basename}.mp4");

                if (checkBoxTrimming.Checked)
                {
                    var subdata = GetSubClipTrimmingDataXMLSerialized();
                    sourcexml.SetAttributeValue("StartTime", subdata.StartTime);
                    sourcexml.SetAttributeValue("Duration", subdata.Duration);
                }

                return new SubClipConfiguration()
                {
                    Configuration = doc.Declaration.ToString() + doc.ToString(),
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
                    var subdata = GetSubClipTrimmingDataTimeSpan();
                    config.Trimming = true;
                    config.StartTimeForReencode = subdata.StartTime;
                    config.DurationForReencode = subdata.Duration;
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
            if (checkBoxTrimming.Checked && timeControlStart.GetTimeStampAsTimeSpanWitoutOffset() > timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset())
            {
                errorProvider1.SetError(timeControlStart, "Start time must be lower than end time");
            }
            else
            {
                errorProvider1.SetError(timeControlStart, String.Empty);
            }

            // time end control
            if (checkBoxTrimming.Checked && timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset() < timeControlStart.GetTimeStampAsTimeSpanWitoutOffset())
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
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void timeControlStart_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
            ResetConfigXML();
        }

        private void checkBoxTrimming_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStart.Enabled = checkBoxTrimming.Checked;
            timeControlEnd.Enabled = checkBoxTrimming.Checked;
            checkBoxPreviewStream.Enabled = checkBoxTrimming.Checked;
            CheckIfErrorTimeControls();
            ResetConfigXML();
            PlaybackAsset();
        }

        private void UpdateXMLData()
        {
            textBoxConfiguration.Text = GetSubclippingConfiguration().Configuration;
        }

        private void tabPageXML_Enter(object sender, EventArgs e)
        {
            UpdateXMLData();
        }

        private void radioButtonClipWithReencode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonClipWithReencode.Checked)  // reencoding
            {
                textBoxConfiguration.Enabled = panelJob.Visible = false;
                UpdateButtonOk();
                ResetConfigXML();
                DisplayAccuracy();
            }
            else if (radioButtonArchiveAllBitrate.Checked || radioButtonArchiveTopBitrate.Checked)  // archive
            {
                textBoxConfiguration.Enabled = panelJob.Visible = true;
                UpdateButtonOk();
                ResetConfigXML();
                DisplayAccuracy();
            }
            else if (radioButtonAssetFilter.Checked) // asset filter
            {
                textBoxConfiguration.Enabled = panelJob.Visible = false;
                UpdateButtonOk();
                ResetConfigXML();
                DisplayAccuracy();
            }

        }

        private void UpdateButtonOk()
        {

            buttonOk.Text = ((string)buttonOk.Tag) + ((radioButtonArchiveAllBitrate.Checked || radioButtonArchiveTopBitrate.Checked) ? "" : "...");

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
                    string myurl = AssetInfo.DoPlayBackWithBestStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayerFrame, Urlstr: myuri.ToString(), DoNotRewriteURL: true, context: _context, formatamp: AzureMediaPlayerFormats.Auto, technology: AzureMediaPlayerTechnologies.Auto, launchbrowser: false);
                    // string myurl = "http://nab2015-dev.azurewebsites.net/#/channels/LiveChannelStreamDemo/program-schedule/112485dc-2d26-422b-99e9-56240f6d70da
                    // webBrowserPreview2.Url = new Uri(myurl);

                    //string myurl = "http://whatsmyuseragent.com/";
                    //string myurl = "http://localhost:33270/dynamic_registerEvents.htm";
                    webBrowserPreview2.Url = new Uri(myurl);


                    //  webBrowserPreview2.DocumentText = File.ReadAllText(@"C:\Users\xpouyat\Documents\visual studio 2013\Projects\dynamic_registerEvents.htm");

                }
            }
            else
            {
                webBrowserPreview2.Url = null;
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
                List<IMediaProcessor> Procs = Mainform.GetMediaProcessorsByName(Constants.AzureMediaEncoderStandard);
                EncodingAMEStandard form2 = new EncodingAMEStandard(_context, subclipConfig)
                {
                    EncodingLabel = (_selectedAssets.Count > 1) ? _selectedAssets.Count + " assets have been selected. " + _selectedAssets.Count + " jobs will be submitted." : "Asset '" + _selectedAssets.FirstOrDefault().Name + "' will be encoded.",
                    EncodingProcessorsList = Procs,
                    EncodingJobName = "Subclipping with reencoding of " + Constants.NameconvInputasset,
                    EncodingOutputAssetName = Constants.NameconvInputasset + "- Subclipped with reencoding",
                    EncodingAMEStdPresetJSONFilesUserFolder = Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder,
                    EncodingAMEStdPresetJSONFilesFolder = Application.StartupPath + Constants.PathAMEStdFiles,
                    SelectedAssets = _selectedAssets
                };

                if (form2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string taskname = "Subclipping with reencoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
                    _mainform.LaunchJobs(
                       form2.EncodingProcessorSelected,
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
                    var filterinfo = formAF.GetFilterInfo;
                    try
                    {
                        selasset.AssetFilters.Create(filterinfo.Name, filterinfo.Presentationtimerange, filterinfo.Trackconditions);
                        _mainform.TextBoxLogWriteLine("Asset filter '{0}' created.", filterinfo.Name);
                    }
                    catch (Exception ex)
                    {
                        _mainform.TextBoxLogWriteLine("Error when creating filter '{0}'.", filterinfo.Name, true);
                        _mainform.TextBoxLogWriteLine(ex);
                    }

                    _mainform.DoRefreshGridFiltersV(false);
                }

            }
            else // no reencode or asset filter but stream copy
            {
                string taskname = "Subclipping of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
                IMediaProcessor Proc = Mainform.GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);

                _mainform.LaunchJobs(
                    Proc,
                    _selectedAssets,
                    this.EncodingJobName,
                    this.JobOptions.Priority,
                    taskname,
                    this.EncodingOutputAssetName,
                    new List<string>() { this.GetSubclippingConfiguration().Configuration },
                    this.JobOptions.OutputAssetsCreationOptions,
                    this.JobOptions.TasksOptionsSetting,
                    this.JobOptions.StorageSelected);

                MessageBox.Show("Subclipping job(s) submitted", "Sublipping", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
