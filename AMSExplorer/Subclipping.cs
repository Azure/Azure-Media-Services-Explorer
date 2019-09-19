//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Management.Media;
using System.Threading.Tasks;

namespace AMSExplorer
{
    public partial class Subclipping : Form
    {
        AMSClientV3 _amsClientV3;
        private List<Asset> _selectedAssets;
        private ManifestTimingData _parentassetmanifestdata;
        private long? _timescale = TimeSpan.TicksPerSecond;
        Mainform _mainform;
        bool backupCheckboxTrim = false; // used when user select reencode to save the status of trim checkbox
        private string _buttonOk;
        private string _labelAccurate;
        private string _labeloutoutputasset;
        private StreamingLocator _tempStreamingLocator = null;

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

        public Subclipping(AMSClientV3 context, List<Asset> assetlist, Mainform mainform)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _amsClientV3 = context;
            _parentassetmanifestdata = new ManifestTimingData();
            _selectedAssets = assetlist;
            _mainform = mainform;

            buttonShowEDL.Initialize();
            buttonShowEDL.EDLChanged += ButtonShowEDL_EDLChanged;
            buttonShowEDL.Offset = new TimeSpan(0);

            // temp locator creation
            if (_selectedAssets.Count == 1 && MessageBox.Show("A temporary clear locator of 1 hour is going to be created to access content timing information. It will be deleted when you close the subclipping window.", "Locator creation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    _tempStreamingLocator = AssetInfo.CreateTemporaryOnDemandLocator(_selectedAssets.First(), _amsClientV3);
                }
                catch
                {

                }
            }

            if (_selectedAssets.Count == 1 && _selectedAssets.FirstOrDefault() != null)  // one asset only
            {
                var myAsset = assetlist.FirstOrDefault();
                textBoxAssetName.Text = myAsset.Name;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(myAsset, _amsClientV3, _tempStreamingLocator?.Name);

                labelDiscountinuity.Visible = _parentassetmanifestdata.DiscontinuityDetected;

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
            UpdateJSONInfo();
        }

        private void Subclipping_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);

            _buttonOk = buttonOk.Text;
            _labelAccurate = labelAccurate.Text;
            _labeloutoutputasset = labeloutputasset.Text;
            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkMoreInfoSubClipAMSE));
            CheckIfErrorTimeControls();
            DisplayAccuracy();
            GenerateUniqueNamesForJobAndOutput();
        }

        private void GenerateUniqueNamesForJobAndOutput()
        {
            string uniqueness = Guid.NewGuid().ToString("N");
            EncodingJobName = "subclipping-job-" + uniqueness;
            EncodingOutputAssetName = "subclipped-" + uniqueness;
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

        private SubClipConfiguration GetSubclippingInternalConfiguration()
        {
            if (radioButtonArchiveTopBitrate.Checked) // Archive, no reencoding
            {
                return new SubClipConfiguration()
                {
                    Reencode = false,
                    Trimming = false,
                    CreateAssetFilter = false,
                    StartTime = timeControlStart.TimeStampWithOffset,
                    EndTime = timeControlEnd.TimeStampWithOffset
                };

            }
            else if (radioButtonClipWithReencode.Checked) // means Reencoding
            {
                var config = new SubClipConfiguration()
                {
                    Reencode = true,
                    Trimming = false,
                    CreateAssetFilter = false,
                };

                if (checkBoxTrimming.Checked)
                {
                    config.Trimming = true;
                    var list = new List<ExplorerEDLEntryInOut>();
                    var subdata = GetSubClipTrimmingDataTimeSpan();
                    config.StartTime = timeControlStart.TimeStampWithOffset;
                    config.EndTime = timeControlEnd.TimeStampWithOffset;
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
                    config.StartTime = subdata.StartTime;
                    config.EndTime = subdata.EndTime;
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
            UpdateDurationText();
            UpdateJSONInfo();
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void timeControlStart_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
            UpdateDurationText();
            UpdateJSONInfo();
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
            PlaybackAsset();
        }

        private void UpdateJSONInfo()
        {
            var obj = new JObject() as dynamic;
            obj.Subclips = new JArray() as dynamic;

            if (checkBoxTrimming.Checked && checkBoxUseEDL.Checked) // EDL
            {
                foreach (var entry in buttonShowEDL.GetEDLEntries())
                {
                    dynamic sourceEntry = new JObject() as dynamic;
                    sourceEntry.StartTime = entry.Start + buttonShowEDL.Offset;
                    sourceEntry.Duration = entry.Duration;
                    sourceEntry.EndTime = entry.Start + entry.Duration;
                    obj.Subclips.Add(sourceEntry);
                }
            }
            else // No EDL
            {
                dynamic sourceEntry = new JObject() as dynamic;

                if (checkBoxTrimming.Checked) // with trimming
                {
                    sourceEntry.StartTime = timeControlStart.TimeStampWithOffset;
                    sourceEntry.EndTime = timeControlEnd.TimeStampWithOffset;
                    sourceEntry.Duration = timeControlEnd.TimeStampWithOffset - timeControlStart.TimeStampWithOffset;
                }
                obj.Subclips.Add(sourceEntry);
            }

            textBoxConfiguration.Text = obj.ToString();
        }

        private void tabPageXML_Enter(object sender, EventArgs e)
        {
            UpdateJSONInfo();
        }

        private void radioButtonClipWithReencode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton senderr = sender as RadioButton;

            if (radioButtonAssetFilter.Checked && senderr.Name == radioButtonAssetFilter.Name) // asset filtering
            {
                checkBoxTrimming.Checked = true;
                checkBoxTrimming.Enabled = false;
                panelJob.Visible = false;
            }
            else if (radioButtonClipWithReencode.Checked && senderr.Name == radioButtonClipWithReencode.Name)  // reencoding
            {
                checkBoxTrimming.Checked = true;
                checkBoxTrimming.Enabled = false;
                panelJob.Visible = true;
                /*
                if (senderr.Name == radioButtonClipWithReencode.Name) //reencoding
                {
                    panelEDL.Visible = false; // true;  // no EDL for now in AMS v3
                }
                else
                {
                    panelEDL.Visible = false;
                }
                */
            }
            else if (radioButtonArchiveTopBitrate.Checked && senderr.Name == radioButtonArchiveTopBitrate.Name) // archive top bitrate
            {
                checkBoxTrimming.Checked = backupCheckboxTrim;
                checkBoxTrimming.Enabled = true;
                panelJob.Visible = true;
                // panelEDL.Visible = true;
            }
            UpdateButtonOk();
            DisplayAccuracy();

        }

        private void UpdateButtonOk()
        {
            if (radioButtonArchiveTopBitrate.Checked)
            {
                buttonOk.Text = _buttonOk;
            }
            else if (radioButtonClipWithReencode.Checked)
            {
                buttonOk.Text = _buttonOk + "...";
            }
            else if (radioButtonAssetFilter.Checked)
            {
                buttonOk.Text = "Create filter...";
            }
        }

        private void DisplayAccuracy()
        {
            labelAccurate.Text = string.Format(_labelAccurate, radioButtonClipWithReencode.Checked ? "Frame" : "GOP");

            if (radioButtonAssetFilter.Checked)
            {
                labeloutputasset.Text = "Asset Filter Name :";
            }
            else
            {
                labeloutputasset.Text = _labeloutoutputasset;
            }
        }

        private void Subclipping_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_tempStreamingLocator != null)
            {
                try
                {
                    _amsClientV3.AMSclient.StreamingLocators.Delete(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, _tempStreamingLocator.Name);
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
            if (checkBoxPreviewStream.Checked && checkBoxTrimming.Checked && _tempStreamingLocator != null)
            {
                Asset myAsset = _selectedAssets.FirstOrDefault();

                Uri myuri = AssetInfo.GetValidOnDemandURI(myAsset, _amsClientV3, _tempStreamingLocator.Name);

                if (myuri != null)
                {
                    string myurl = AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: PlayerType.AzureMediaPlayerFrame, path: AssetInfo.RW(myuri, https: true).ToString(), DoNotRewriteURL: true, client: _amsClientV3, formatamp: AzureMediaPlayerFormats.Auto, technology: AzureMediaPlayerTechnologies.Auto, launchbrowser: false, UISelectSEFiltersAndProtocols: false, mainForm: _mainform);
                    webBrowserPreview.Url = new Uri(myurl);
                }
                else
                {
                    webBrowserPreview.Url = null;
                }
            }
            else
            {
                webBrowserPreview.Url = null;
            }
        }


        private async void buttonOk_Click(object sender, EventArgs e)
        {
            await DoSubClipAsync();
        }

        private async Task DoSubClipAsync()
        {
            var subclipConfig = this.GetSubclippingInternalConfiguration();

            if (subclipConfig.Reencode) // reencode the clip
            {
                ProcessFromTransform form = new ProcessFromTransform(_amsClientV3, _mainform, _selectedAssets, null, subclipConfig.StartTime, subclipConfig.EndTime, true);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    await _mainform.CreateAndSubmitJobsAsync(new List<Transform>() { form.SelectedTransform }, _selectedAssets, form.StartClipTime, form.EndClipTime);
                }

                /*
                var processor = Mainform.GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);
                EncodingMES form2 = new EncodingMES(_context, new List<IAsset>(), processor.Version, _mainform, subclipConfig, disableOverlay: true)
                {
                    EncodingLabel = (_selectedAssets.Count > 1) ?
                                    string.Format("{0} asset{1} selected. You are going to submit {0} job{1} with 1 task.", _selectedAssets.Count, Program.ReturnS(_selectedAssets.Count), _selectedAssets.Count)
                                    :
                                    "Asset '" + _selectedAssets.FirstOrDefault().Name + "' will be encoded (1 job with 1 task).",

                    EncodingJobName = "Subclipping with reencoding of " + Constants.NameconvInputasset,
                    EncodingOutputAssetName = Constants.NameconvInputasset + "- Subclipped with reencoding",
                    EncodingAMEStdPresetJSONFilesUserFolder = Properties.Settings.Default.MESPresetFilesCurrentFolder,
                    EncodingAMEStdPresetJSONFilesFolder = Application.StartupPath + Constants.PathMESFiles,
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
                       form2.JobOptions.OutputAssetsFormatOption,
                       form2.JobOptions.TasksOptionsSetting,
                       form2.JobOptions.StorageSelected);
                }
                */
            }
            else if (subclipConfig.CreateAssetFilter) // create a asset filter
            {
                Asset selasset = _selectedAssets.FirstOrDefault();
                DynManifestFilter formAF = new DynManifestFilter(_amsClientV3, null, selasset, subclipConfig);
                if (formAF.ShowDialog() == DialogResult.OK)
                {
                    FilterCreationInfo filterinfo = null;
                    try
                    {
                        filterinfo = formAF.GetFilterInfo;
                        AssetFilter assetFilter = new AssetFilter() { PresentationTimeRange = filterinfo.Presentationtimerange };

                        await _amsClientV3.AMSclient.AssetFilters.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, selasset.Name, filterinfo.Name, assetFilter);

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
                ClipTime startTime = null;
                ClipTime endTime = null;

                if (checkBoxTrimming.Checked)
                {
                    startTime = new AbsoluteClipTime()
                    {
                        Time = subclipConfig.StartTime
                    };

                    endTime = new AbsoluteClipTime()
                    {
                        Time = subclipConfig.EndTime
                    };
                }

                var transform = await _mainform.CreateAndGetCopyCodecTransformIfNeededAsync();
                await _mainform.CreateAndSubmitJobsAsync(new List<Transform>() { transform }, _selectedAssets, startTime, endTime, EncodingJobName, EncodingOutputAssetName);

                MessageBox.Show("Subclipping job(s) submitted", "Sublipping", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            GenerateUniqueNamesForJobAndOutput();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonShowEDL.AddEDLEntry(new ExplorerEDLEntryInOut()
            {
                Start = timeControlStart.TimeStampWithoutOffset,
                End = timeControlEnd.TimeStampWithoutOffset,
                Offset = timeControlStart.GetOffSetAsTimeSpan()
            });
        }

        private void checkBoxUseEDL_CheckedChanged(object sender, EventArgs e)
        {
            buttonShowEDL.Enabled = buttonAddEDLEntry.Enabled = checkBoxUseEDL.Checked;
        }

        private void Subclipping_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelGen, timeControlStart, timeControlEnd, textBoxConfiguration }, e, this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesAfterDpiChange(panel1, e);
        }

        private void Subclipping_Shown(object sender, EventArgs e)
        {

        }
    }


    public class SubClipTrimmingDataTimeSpan
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan Offset { get; set; }
    }
}
