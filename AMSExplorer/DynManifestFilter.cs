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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Azure.Management.Media.Models;
using System.Threading.Tasks;

namespace AMSExplorer
{
    public partial class DynManifestFilter : Form
    {
        private string _filter_name;
        private List<ExFilterTrack> filtertracks;

        private bool isAccountFilter = true;
        private bool newfilter = true;
        private DataTable dataPropertyType;
        private DataTable dataPropertyFourCC;
        private DataTable dataProperty;
        private DataTable dataOperator;
        private ManifestTimingData _parentassetmanifestdata;
        private long _timescale = 10000000;
        private Asset _parentAsset;
        private SubClipConfiguration _subclipconfig;
        private AMSClientV3 _amsClient;
        private string _labelStartTimeDefault;
        private string _labelDefaultEnd;
        private string _labelDefaultDVR;
        private string _labelDefaultBakckoff;

        private object _filterToDisplay;

        public DynManifestFilter(AMSClientV3 amsClient, object filterToDisplay = null, Asset parentAsset = null, SubClipConfiguration subclipconfig = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = amsClient;
            _filterToDisplay = filterToDisplay;
            _parentAsset = parentAsset;
            _subclipconfig = subclipconfig;
        }

        private async Task FillComboBoxImportFilters(Asset asset)
        {
            // combobox for filters

            comboBoxLocatorsFilters.BeginUpdate();

            comboBoxLocatorsFilters.Items.Add(new Item(AMSExplorer.Properties.Resources.DynManifestFilter_FillComboBoxImportFilters_ImportTrackFilteringFrom, null));

            _amsClient.RefreshTokenIfNeeded();
            if (asset != null)
            {
                var filters = (await _amsClient.AMSclient.AssetFilters.ListWithHttpMessagesAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name)).Body;
                filters.Where(g => g.Tracks.Count > 0).ToList().ForEach(g => comboBoxLocatorsFilters.Items.Add(new Item("Asset filter : " + g.Name, g.Id)));
            }
            // account filters

            var acFilters = (await _amsClient.AMSclient.AccountFilters.ListWithHttpMessagesAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).Body;

            acFilters.Where(g => g.Tracks.Count > 0).ToList().ForEach(g => comboBoxLocatorsFilters.Items.Add(new Item("Account filter : " + g.Name, g.Name)));

            if (comboBoxLocatorsFilters.Items.Count > 1)
            {
                comboBoxLocatorsFilters.Enabled = true;
            }
            comboBoxLocatorsFilters.SelectedIndex = 0;
            comboBoxLocatorsFilters.EndUpdate();
        }

        private void DynManifestFilter_Load(object sender, EventArgs e)
        {
            _labelStartTimeDefault = labelStartTimeDefault.Text;
            _labelDefaultEnd = labelDefaultEnd.Text;
            _labelDefaultDVR = labelDefaultDVR.Text;
            _labelDefaultBakckoff = labelDefaultBakckoff.Text;

            _parentassetmanifestdata = new ManifestTimingData();
            tabControl1.TabPages.Remove(tabPageTRRaw);
            FillComboBoxImportFilters(_parentAsset);

            timeControlDVR.TotalDuration = TimeSpan.FromHours(24);
            timeControlDVR.Max = TimeSpan.FromHours(24);


            /////////////////////////////////////////////
            // New Account Filter
            /////////////////////////////////////////////
            if (_filterToDisplay == null && _parentAsset == null)
            {
                newfilter = true;
                isAccountFilter = true;
                tabControl1.TabPages.Remove(tabPageInformation);
                var _filter_presentationtimerange = new PresentationTimeRange() { Timescale = 10000000 };
                filtertracks = new List<ExFilterTrack>();
                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                _timescale = (long)_filter_presentationtimerange.Timescale;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _filter_presentationtimerange.Timescale;

                textBoxFilterTimeScale.Text = (_filter_presentationtimerange.Timescale == null) ? "(default)" : _filter_presentationtimerange.Timescale.ToString();
            }


            /////////////////////////////////////////////
            // Existing Account Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay != null && _filterToDisplay.GetType() == typeof(AccountFilter) && _parentAsset == null)
            {
                var accFilter = (AccountFilter)_filterToDisplay;
                newfilter = false;
                isAccountFilter = true;
                DisplayFilterInfo();
                _filter_name = accFilter.Name;
                filtertracks = ConvertFilterTracksToInternalVar(accFilter.Tracks);

                _timescale = accFilter.PresentationTimeRange.Timescale ?? _timescale;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _timescale;
                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                toolTip1.SetToolTip(this.buttonOk, AMSExplorer.Properties.Resources.DynManifestFilter_DynManifestFilter_Load_ItCanTakeUpTo2MinutesForStreamingEndpointToRefreshTheRules);

                textBoxFilterName.Enabled = false; // no way to change the filter name
                textBoxFilterName.Text = _filter_name;

                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                checkBoxStartTime.Checked = accFilter.PresentationTimeRange.StartTimestamp != null;
                checkBoxEndTime.Checked = accFilter.PresentationTimeRange.EndTimestamp != null;
                checkBoxPresentationWindowDuration.Checked = accFilter.PresentationTimeRange.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = accFilter.PresentationTimeRange.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(accFilter.PresentationTimeRange.StartTimestamp, 0);
                timeControlEnd.SetScaledTimeStamp(accFilter.PresentationTimeRange.EndTimestamp, 0);
                timeControlDVR.SetScaledTimeStamp(accFilter.PresentationTimeRange.PresentationWindowDuration, 0);

                if (accFilter.PresentationTimeRange.LiveBackoffDuration != null)
                {
                    var backoff = TimeSpan.FromTicks((long)(accFilter.PresentationTimeRange.LiveBackoffDuration / _timescale) * TimeSpan.TicksPerSecond);
                    numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);
                }


                if (accFilter.FirstQuality != null)
                {
                    checkBoxFirstQualityBitrate.Checked = true;
                    numericUpDownFirstQualityBitrate.Value = accFilter.FirstQuality.Bitrate;
                }

                checkBoxForValueForLive.Checked = accFilter.PresentationTimeRange.ForceEndTimestamp ?? false;

            }


            /////////////////////////////////////////////
            // New Asset Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay == null && _parentAsset != null)
            {
                newfilter = true;
                isAccountFilter = false;
                tabControl1.TabPages.Remove(tabPageInformation);

                filtertracks = new List<ExFilterTrack>();

                labelFilterTitle.Text = "Asset Filter";
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                textBoxAssetName.Text = _parentAsset != null ? _parentAsset.Name : string.Empty;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(_parentAsset, _amsClient);

                if (!_parentassetmanifestdata.Error)  // we were able to read asset timings and not live
                {
                    // timescale
                    timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = (long?)_parentassetmanifestdata.TimeScale;
                    _timescale = _parentassetmanifestdata.TimeScale ?? TimeSpan.TicksPerSecond;

                    timeControlStart.ScaledFirstTimestampOffset = timeControlEnd.ScaledFirstTimestampOffset = _parentassetmanifestdata.TimestampOffset;

                    textBoxOffset.Text = _parentassetmanifestdata.TimestampOffset.ToString();
                    labelOffset.Visible = textBoxOffset.Visible = true;

                    // let's disable trackbars if this is live (duration is not fixed)
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = !_parentassetmanifestdata.IsLive;

                    TimeSpan duration = _parentassetmanifestdata.AssetDuration;
                    textBoxAssetDuration.Text = duration.ToString(@"d\.hh\:mm\:ss");
                    labelassetduration.Visible = textBoxAssetDuration.Visible = true;
                    textBoxFilterName.Text = "filter" + new Random().Next(9999).ToString();

                    if (!_parentassetmanifestdata.IsLive)  // Not a live content
                    {
                        // let set duration and active track bat
                        timeControlStart.TotalDuration = timeControlEnd.TotalDuration = timeControlDVR.TotalDuration = _parentassetmanifestdata.AssetDuration;
                        timeControlDVR.TotalDuration = TimeSpan.FromHours(24);

                        timeControlStart.Max = timeControlEnd.Max = duration;
                        timeControlEnd.SetTimeStamp(timeControlEnd.Max);

                    }
                    else
                    {
                        textBoxAssetDuration.Text += " (LIVE)";
                    }

                    if (_subclipconfig != null && _subclipconfig.Trimming) // user used the subclip UI before and timings are passed
                    {
                        timeControlStart.SetTimeStamp(_subclipconfig.StartTimeForAssetFilter - timeControlStart.GetOffSetAsTimeSpan());
                        timeControlEnd.SetTimeStamp(_subclipconfig.EndTimeForAssetFilter - timeControlStart.GetOffSetAsTimeSpan());
                        checkBoxStartTime.Checked = checkBoxEndTime.Checked = true;
                        textBoxFilterName.Text = "subclip" + new Random().Next(9999).ToString();
                    }
                }

                else // not able to read asset timings
                {
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;
                    timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _timescale;
                    timeControlStart.Max = timeControlEnd.Max = timeControlDVR.Max = TimeSpan.MaxValue;
                    timeControlEnd.SetTimeStamp(new TimeSpan(0));// timeControlEnd.Max);
                    labelassetduration.Visible = textBoxAssetDuration.Visible = false;
                }
            }

            /////////////////////////////////////////////
            // Existing Asset Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay != null && _filterToDisplay.GetType() == typeof(AssetFilter) && _parentAsset != null)
            {
                var assetFilter = (AssetFilter)_filterToDisplay;
                newfilter = false;
                isAccountFilter = false;
                DisplayFilterInfo();

                _filter_name = assetFilter.Name;
                filtertracks = ConvertFilterTracksToInternalVar(assetFilter.Tracks);

                _timescale = assetFilter.PresentationTimeRange.Timescale ?? _timescale;

                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                toolTip1.SetToolTip(this.buttonOk, AMSExplorer.Properties.Resources.DynManifestFilter_DynManifestFilter_Load_ItCanTakeUpTo2MinutesForStreamingEndpointToRefreshTheRules);

                labelFilterTitle.Text = "Asset Filter";
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                textBoxAssetName.Text = _parentAsset != null ? _parentAsset.Name : string.Empty;

                textBoxFilterName.Enabled = false; // no way to change the filter name
                textBoxFilterName.Text = _filter_name;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(_parentAsset, _amsClient);

                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _timescale;

                if (!_parentassetmanifestdata.Error && _timescale == _parentassetmanifestdata.TimeScale)  // we were able to read asset timings and timescale between manifest and existing asset match
                {
                    // let's disable trackbars if this is live (duration is not fixed)
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = !_parentassetmanifestdata.IsLive;
                    timeControlStart.ScaledFirstTimestampOffset = timeControlEnd.ScaledFirstTimestampOffset = _parentassetmanifestdata.TimestampOffset;

                    textBoxOffset.Text = _parentassetmanifestdata.TimestampOffset.ToString();
                    labelOffset.Visible = textBoxOffset.Visible = true;

                    TimeSpan duration = _parentassetmanifestdata.AssetDuration;
                    textBoxAssetDuration.Text = duration.ToString(@"d\.hh\:mm\:ss");
                    labelassetduration.Visible = textBoxAssetDuration.Visible = true;

                    if (!_parentassetmanifestdata.IsLive)
                    {
                        timeControlStart.Max = timeControlEnd.Max = duration;
                        // let set duration and active track bat
                        timeControlStart.TotalDuration = timeControlEnd.TotalDuration = duration;
                    }
                    else
                    {
                        textBoxAssetDuration.Text += " (LIVE)";
                    }
                }

                else // not able to read asset timings or mismatch in timescale
                {
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;
                    timeControlStart.Max = timeControlEnd.Max = TimeSpan.MaxValue;
                    labelassetduration.Visible = textBoxAssetDuration.Visible = false;
                }

                checkBoxStartTime.Checked = assetFilter.PresentationTimeRange.StartTimestamp != null;
                checkBoxEndTime.Checked = assetFilter.PresentationTimeRange.EndTimestamp != null;
                checkBoxPresentationWindowDuration.Checked = assetFilter.PresentationTimeRange.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = assetFilter.PresentationTimeRange.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(assetFilter.PresentationTimeRange.StartTimestamp, 0);
                timeControlEnd.SetScaledTimeStamp(assetFilter.PresentationTimeRange.EndTimestamp, 0);  // we don't want to pass the max value to the control (overflow)
                timeControlDVR.SetScaledTimeStamp(assetFilter.PresentationTimeRange.PresentationWindowDuration, 0);

                if (assetFilter.PresentationTimeRange.LiveBackoffDuration != null)
                {
                    var backoff = TimeSpan.FromTicks((long)(assetFilter.PresentationTimeRange.LiveBackoffDuration / _timescale) * TimeSpan.TicksPerSecond);
                    numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);
                }


                if (assetFilter.FirstQuality != null)
                {
                    checkBoxFirstQualityBitrate.Checked = true;
                    numericUpDownFirstQualityBitrate.Value = assetFilter.FirstQuality.Bitrate;
                }

                checkBoxForValueForLive.Checked = assetFilter.PresentationTimeRange.ForceEndTimestamp ?? false;
            }

            // Common code
            textBoxFilterTimeScale.Text = _timescale.ToString();

            // dataPropertyType
            dataPropertyType = new DataTable();
            dataPropertyType.Columns.Add(new DataColumn("Value", typeof(string)));
            dataPropertyType.Columns.Add(new DataColumn("Description", typeof(string)));

            dataPropertyType.Rows.Add(FilterPropertyTypeValue.Audio, FilterPropertyTypeValue.Audio);
            dataPropertyType.Rows.Add(FilterPropertyTypeValue.Video, FilterPropertyTypeValue.Video);
            dataPropertyType.Rows.Add(FilterPropertyTypeValue.Text, FilterPropertyTypeValue.Text);

            // FilterPropertyFourCCValue
            dataPropertyFourCC = new DataTable();
            dataPropertyFourCC.Columns.Add(new DataColumn("Value", typeof(string)));
            dataPropertyFourCC.Columns.Add(new DataColumn("Description", typeof(string)));

            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.avc1, FilterPropertyFourCCValue.avc1);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.ec3, FilterPropertyFourCCValue.ec3);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.mp4a, FilterPropertyFourCCValue.mp4a);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.hev1, FilterPropertyFourCCValue.hev1);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.hvc1, FilterPropertyFourCCValue.hvc1);

            // dataProperty 
            dataProperty = new DataTable();
            dataProperty.Columns.Add(new DataColumn("Property", typeof(string)));
            dataProperty.Columns.Add(new DataColumn("Description", typeof(string)));
            dataProperty.Rows.Add(FilterTrackPropertyType.Type.ToString(), FilterTrackPropertyType.Type.ToString());
            dataProperty.Rows.Add(FilterTrackPropertyType.Bitrate.ToString(), FilterTrackPropertyType.Bitrate.ToString());
            dataProperty.Rows.Add(FilterTrackPropertyType.FourCC.ToString(), FilterTrackPropertyType.FourCC.ToString());
            dataProperty.Rows.Add(FilterTrackPropertyType.Language.ToString(), FilterTrackPropertyType.Language.ToString());
            dataProperty.Rows.Add(FilterTrackPropertyType.Name.ToString(), FilterTrackPropertyType.Name.ToString());
            dataProperty.Rows.Add(FilterTrackPropertyType.Unknown.ToString(), FilterTrackPropertyType.Unknown.ToString());

            // dataOperator
            dataOperator = new DataTable();
            dataOperator.Columns.Add(new DataColumn("Operator", typeof(string)));
            dataOperator.Columns.Add(new DataColumn("Description", typeof(string)));
            dataOperator.Rows.Add(FilterTrackPropertyCompareOperation.Equal.ToString(), FilterTrackPropertyCompareOperation.Equal.ToString());
            dataOperator.Rows.Add(FilterTrackPropertyCompareOperation.NotEqual.ToString(), FilterTrackPropertyCompareOperation.NotEqual.ToString());

            var columnProperty = new DataGridViewComboBoxColumn();
            columnProperty.DataSource = dataProperty;
            columnProperty.ValueMember = "Property";
            columnProperty.DisplayMember = "Description";
            dataGridViewTracks.Columns.Add(columnProperty);

            var columnOperator = new DataGridViewComboBoxColumn();
            columnOperator.DataSource = dataOperator;
            columnOperator.ValueMember = "Operator";
            columnOperator.DisplayMember = "Description";
            dataGridViewTracks.Columns.Add(columnOperator);

            var columnValue = new DataGridViewTextBoxColumn();
            dataGridViewTracks.Columns.Add(columnValue);

            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkHowIMoreInfoDynamicManifest));

            RefreshTracks();
            CheckIfErrorTimeControls();
            UpdateDurationText();
        }

        private void DisplayFilterInfo()
        {
            DGInfo.ColumnCount = 2;
            // filter info
            DGInfo.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            if (isAccountFilter)
            {
                var accfilter = (AccountFilter)_filterToDisplay;
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, accfilter.Name);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_GlobalFilter);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_FirstQualityBitrate, accfilter.FirstQuality == null ? Constants.stringNull : accfilter.FirstQuality.Bitrate.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_Timescale, accfilter.PresentationTimeRange.Timescale.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_StartTimestamp, accfilter.PresentationTimeRange.StartTimestamp.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_EndTimestamp, accfilter.PresentationTimeRange.EndTimestamp.ToString());
                DGInfo.Rows.Add("Force end timestamp", accfilter.PresentationTimeRange.ForceEndTimestamp);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_PresentationWindowDuration, accfilter.PresentationTimeRange.PresentationWindowDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_LiveBackoffDuration, accfilter.PresentationTimeRange.LiveBackoffDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_TrackCount, accfilter.Tracks.Count);
            }
            else
            {
                var assetfilter = (AssetFilter)_filterToDisplay;
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, assetfilter.Name);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_AssetFilter);
                DGInfo.Rows.Add("Id", assetfilter.Id);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_FirstQualityBitrate, assetfilter.FirstQuality == null ? Constants.stringNull : assetfilter.FirstQuality.Bitrate.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_Timescale, assetfilter.PresentationTimeRange.Timescale.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_StartTimestamp, assetfilter.PresentationTimeRange.StartTimestamp.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_EndTimestamp, assetfilter.PresentationTimeRange.EndTimestamp.ToString());
                DGInfo.Rows.Add("Force end timestamp", assetfilter.PresentationTimeRange.ForceEndTimestamp);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_PresentationWindowDuration, assetfilter.PresentationTimeRange.PresentationWindowDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_LiveBackoffDuration, assetfilter.PresentationTimeRange.LiveBackoffDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_TrackCount, assetfilter.Tracks.Count);
            }
        }

        private List<ExFilterTrack> ConvertFilterTracksToInternalVar(IList<FilterTrackSelection> tracks)
        // copy filter tracks to internal list used for display in grid
        {
            List<ExFilterTrack> targettracks = new List<ExFilterTrack>();
            foreach (var track in tracks)
            {
                var mytrack = new ExFilterTrack();
                var myconditions = new List<ExCondition>();
                foreach (var condition in track.TrackSelections)
                {
                    var mycondition = new ExCondition();
                    mycondition.oper = condition.Operation.ToString();
                    mycondition.property = condition.Property.ToString();
                    mycondition.value = condition.Value;

                    myconditions.Add(mycondition);
                }
                mytrack.conditions = myconditions;
                targettracks.Add(mytrack);
            }
            return targettracks;
        }


        private List<FilterTrackSelection> CreateFilterTracks()
        // use internal list to create filter tracks
        {
            List<FilterTrackSelection> filterTrackSelectStatements = new List<FilterTrackSelection>();

            foreach (var track in filtertracks)
            {
                FilterTrackSelection filterTrackSelectStatement = new FilterTrackSelection();
                filterTrackSelectStatement.TrackSelections = new List<FilterTrackPropertyCondition>();

                foreach (var condition in track.conditions)
                {

                    FilterTrackPropertyCompareOperation op;
                    if (condition.oper == FilterTrackPropertyCompareOperation.Equal.ToString())
                    {
                        op = FilterTrackPropertyCompareOperation.Equal;
                    }
                    else
                    {
                        op = FilterTrackPropertyCompareOperation.NotEqual;
                    }

                    FilterTrackPropertyType prop;
                    if (condition.property == FilterTrackPropertyType.Bitrate.ToString())
                    {
                        prop = FilterTrackPropertyType.Bitrate;
                    }
                    else if (condition.property == FilterTrackPropertyType.FourCC.ToString())
                    {
                        prop = FilterTrackPropertyType.FourCC;
                    }
                    else if (condition.property == FilterTrackPropertyType.Language.ToString())
                    {
                        prop = FilterTrackPropertyType.Language;
                    }
                    else if (condition.property == FilterTrackPropertyType.Name.ToString())
                    {
                        prop = FilterTrackPropertyType.Name;
                    }
                    else if (condition.property == FilterTrackPropertyType.Type.ToString())
                    {
                        prop = FilterTrackPropertyType.Type;
                    }
                    else
                    {
                        prop = FilterTrackPropertyType.Unknown;
                    }

                    filterTrackSelectStatement.TrackSelections.Add(new FilterTrackPropertyCondition(prop, condition.value, op));
                }
                filterTrackSelectStatements.Add(filterTrackSelectStatement);
            }

            return filtertracks.Count > 0 ? filterTrackSelectStatements : null;
        }

        private void RefreshTracks()
        {
            listBoxTracks.Items.Clear();
            dataGridViewTracks.Rows.Clear();

            int i = 1;
            foreach (var track in filtertracks)
            {
                listBoxTracks.Items.Add("Rule" + i);
                i++;
            }
            if (listBoxTracks.SelectedIndex == -1 && listBoxTracks.Items.Count > 0)
            {
                listBoxTracks.SelectedIndex = 0;
            }
        }

        public FilterCreationInfo GetFilterInfo
        {
            get
            {
                FilterCreationInfo filterinfo = new FilterCreationInfo();
                filterinfo.Name = newfilter ? textBoxFilterName.Text : _filter_name;
                filterinfo.Firstquality = checkBoxFirstQualityBitrate.Checked ? new FirstQuality((int)numericUpDownFirstQualityBitrate.Value) : null;
                if (checkBoxRawMode.Checked) // RAW Mode
                {
                    try
                    {
                        filterinfo.Presentationtimerange = new PresentationTimeRange(
                                               string.IsNullOrWhiteSpace(textBoxRawStart.Text) ? (long?)null : long.Parse(textBoxRawStart.Text),
                                               string.IsNullOrWhiteSpace(textBoxRawEnd.Text) ? (long?)null : long.Parse(textBoxRawEnd.Text),
                                               string.IsNullOrWhiteSpace(textBoxRawDVR.Text) ? (long?)null : long.Parse(textBoxRawDVR.Text),
                                               string.IsNullOrWhiteSpace(textBoxRawBackoff.Text) ? (long?)null : long.Parse(textBoxRawBackoff.Text),
                                               string.IsNullOrWhiteSpace(textBoxRawTimescale.Text) ? (long?)null : long.Parse(textBoxRawTimescale.Text),
                                               checkBoxForValueLiveRaw.Checked ? (bool?)true : (bool?)null
                                               );
                    }
                    catch
                    {
                        throw;
                    }

                }
                else  // Default mode
                {
                    filterinfo.Presentationtimerange = GetFilterPresenTationTRDefaultMode;
                }
                // to make sure it is null to avoid puting data in JSON
                filterinfo.Tracks = CreateFilterTracks();

                return filterinfo;
            }
        }

        private PresentationTimeRange GetFilterPresenTationTRDefaultMode
        {
            get
            {

                return new PresentationTimeRange(
                     startTimestamp: checkBoxStartTime.Checked ? timeControlStart.ScaledTimeStampWithOffset : (long?)null,
                     endTimestamp: checkBoxEndTime.Checked ? timeControlEnd.ScaledTimeStampWithOffset : (long?)null,
                     presentationWindowDuration: checkBoxPresentationWindowDuration.Checked ? timeControlDVR.TimeStampWithoutOffset.Ticks : (long?)null,
                     liveBackoffDuration: checkBoxLiveBackoff.Checked ? (long)numericUpDownBackoffSeconds.Value : (long?)null,
                     timescale: _timescale,
                    forceEndTimestamp: checkBoxForValueForLive.Checked ? (bool?)true : (bool?)null
                 );
            }
        }


        private void listBoxTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTracksConditions();
        }

        private void dataGridViewTracks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewTracks_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewTracks.CurrentCell.Value != null)
            {
                if (dataGridViewTracks.CurrentCell.ColumnIndex == 0) // if first column
                {
                    string pstring = dataGridViewTracks.CurrentCell.Value.ToString();
                    if (pstring == nameof(FilterTrackPropertyType.Type)) // property type
                    {
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyType;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else if (pstring == nameof(FilterTrackPropertyType.FourCC)) // property FourCC
                    {
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyFourCC;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else
                    {
                        var cellValue = new DataGridViewTextBoxCell();
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }

                // let's update the internal var
                switch (dataGridViewTracks.CurrentCell.ColumnIndex)
                {
                    case 0: // property
                        filtertracks[listBoxTracks.SelectedIndex].conditions[dataGridViewTracks.CurrentCell.RowIndex].property = dataGridViewTracks.CurrentCell.Value.ToString();
                        break;

                    case 1: // operator
                        filtertracks[listBoxTracks.SelectedIndex].conditions[dataGridViewTracks.CurrentCell.RowIndex].oper = dataGridViewTracks.CurrentCell.Value.ToString();
                        break;

                    case 2: // value
                        filtertracks[listBoxTracks.SelectedIndex].conditions[dataGridViewTracks.CurrentCell.RowIndex].value = dataGridViewTracks.CurrentCell.Value.ToString();
                        break;
                }

            }
        }


        private void buttonAddTrack_Click(object sender, EventArgs e)
        {
            ExFilterTrack track = new ExFilterTrack() { conditions = new List<ExCondition>() };
            filtertracks.Add(track);
            RefreshTracks();
        }

        private void buttonDeleteTrack_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1)
            {
                dataGridViewTracks.Rows.Clear();
                filtertracks.RemoveAt(listBoxTracks.SelectedIndex);
                RefreshTracks();
            }
        }

        private void buttonAddCondition_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1)
            {
                filtertracks[listBoxTracks.SelectedIndex].conditions.Add(new ExCondition() { oper = FilterTrackPropertyCompareOperation.Equal.ToString() });
                RefreshTracksConditions();
            }
        }

        private void RefreshTracksConditions()
        {
            dataGridViewTracks.Rows.Clear();
            if (listBoxTracks.SelectedIndex > -1)
            {
                var track = filtertracks[listBoxTracks.SelectedIndex];
                foreach (var condition in track.conditions)
                {
                    if (condition.property == FilterTrackPropertyType.Type.ToString()) // property type - we want to propose audio, video or text dropbox
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Type.ToString(), condition.oper, condition.value);
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyType;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        cellValue.Value = condition.value;
                        dataGridViewTracks[2, index] = cellValue;
                    }
                    else if (condition.property == FilterTrackPropertyType.FourCC.ToString()) // property FourCC - we want to propose supported FourCC
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.FourCC.ToString(), condition.oper, condition.value);
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyFourCC;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        cellValue.Value = condition.value;
                        dataGridViewTracks[2, index] = cellValue;
                    }
                    else if (condition.property == FilterTrackPropertyType.Language.ToString()) // property language
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Language.ToString(), condition.oper, condition.value);
                    }
                    else if (condition.property == FilterTrackPropertyType.Bitrate.ToString()) // property bitrate
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Bitrate.ToString(), condition.oper, condition.value);
                    }
                    else if (condition.property == FilterTrackPropertyType.Name.ToString()) // property Name - we want to propose supported FourCC
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Name.ToString(), condition.oper, condition.value);
                    }
                    else if (condition.property == FilterTrackPropertyType.Unknown.ToString()) // property Name - we want to propose supported FourCC
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Unknown.ToString(), condition.oper, condition.value);
                    }
                    else
                    {
                        int index = dataGridViewTracks.Rows.Add(condition.property, condition.oper, condition.value);
                    }
                }
            }
        }

        private void buttonDeleteCondition_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1 && dataGridViewTracks.SelectedRows.Count > 0)
            {
                filtertracks[listBoxTracks.SelectedIndex].conditions.RemoveAt(dataGridViewTracks.SelectedRows[0].Index);
                RefreshTracksConditions();
            }
        }

        private void textBoxFilterName_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (string.IsNullOrEmpty(tb.Text))
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.DynManifestFilter_textBoxFilterName_Validating_PleaseSpecifyAFilterName);
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }


        private void buttonInsertSample_Click(object sender, EventArgs e)
        {
            // Filter sample

            List<FilterTrackSelection> filterTrackSelections = new List<FilterTrackSelection>()
            {
                new FilterTrackSelection
                {
                    TrackSelections = new List<FilterTrackPropertyCondition>()
                    {
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Type, FilterPropertyTypeValue.Video, FilterTrackPropertyCompareOperation.Equal),
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Bitrate, "0-1048576", FilterTrackPropertyCompareOperation.Equal)
                    }
                },
                 new FilterTrackSelection
                 {
                     TrackSelections = new List<FilterTrackPropertyCondition>()
                    {
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Type, FilterPropertyTypeValue.Audio, FilterTrackPropertyCompareOperation.Equal),
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.FourCC, FilterPropertyFourCCValue.mp4a, FilterTrackPropertyCompareOperation.Equal)
                    }
                 },
                 new FilterTrackSelection
                 {
                     TrackSelections = new List<FilterTrackPropertyCondition>()
                    {
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Type, FilterPropertyTypeValue.Text, FilterTrackPropertyCompareOperation.Equal),
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Language, "en", FilterTrackPropertyCompareOperation.Equal)
                    }
                 }
            };

            filtertracks = ConvertFilterTracksToInternalVar(filterTrackSelections);

            RefreshTracks();
            RefreshTracksConditions();
        }



        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }


        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            var rectangle = e.CellBounds;
            rectangle.Inflate(-1, -1);
            ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted); // 
        }

        private void textBoxFilterName_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = !string.IsNullOrWhiteSpace(textBoxFilterName.Text);
        }

        private void CheckIfErrorTimeControls()
        {

            // time start control
            if (checkBoxStartTime.Checked && isAccountFilter)
            {
                errorProvider1.SetError(timeControlStart, AMSExplorer.Properties.Resources.DynManifestFilter_CheckIfErrorTimeControls_ItIsNotRecommendedToUseAGlobalFilterToDoTimeTrimmingConsiderCreatingAnAssetFilterInstead);
            }
            else if (checkBoxStartTime.Checked && checkBoxEndTime.Checked && timeControlStart.TimeStampWithoutOffset > timeControlEnd.TimeStampWithoutOffset)
            {
                errorProvider1.SetError(timeControlStart, AMSExplorer.Properties.Resources.DynManifestFilter_CheckIfErrorTimeControls_StartTimeMustBeLowerThanEndTime);
            }
            else
            {
                errorProvider1.SetError(timeControlStart, String.Empty);
            }

            // time end control
            if (checkBoxEndTime.Checked && isAccountFilter)
            {
                errorProvider1.SetError(timeControlEnd, AMSExplorer.Properties.Resources.DynManifestFilter_CheckIfErrorTimeControls_ItIsNotRecommendedToUseAGlobalFilterToDoTimeTrimmingConsiderCreatingAnAssetFilterInstead);
            }
            else if (checkBoxEndTime.Checked && checkBoxStartTime.Checked && timeControlEnd.TimeStampWithoutOffset < timeControlStart.TimeStampWithoutOffset)
            {
                errorProvider1.SetError(timeControlEnd, AMSExplorer.Properties.Resources.DynManifestFilter_CheckIfErrorTimeControls_EndTimeMustBeHigherThanStartTime);
            }
            else
            {
                errorProvider1.SetError(timeControlEnd, String.Empty);
            }

            // dvr
            if (checkBoxPresentationWindowDuration.Checked && timeControlDVR.TimeStampWithoutOffset < TimeSpan.FromMinutes(1))
            {
                errorProvider1.SetError(timeControlDVR, AMSExplorer.Properties.Resources.DynManifestFilter_CheckIfErrorTimeControls_TheDVRWindowMustBeAtLeast2MinutesOrMore);
            }
            else
            {
                errorProvider1.SetError(timeControlDVR, String.Empty);
            }
        }


        private void timeControlEnd_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
            UpdateDurationText();
        }


        private void checkBoxStartTime_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStart.Enabled = checkBoxStartTime.Checked;
            labelStartTimeDefault.Text = checkBoxStartTime.Checked ? string.Empty : _labelStartTimeDefault;
            CheckIfErrorTimeControls();
            UpdateDurationText();
        }

        private void UpdateDurationText()
        {
            if (checkBoxStartTime.Checked && checkBoxEndTime.Checked)
            {
                textBoxDurationTime.Enabled = true;
                textBoxDurationTime.Text = (timeControlEnd.TimeStampWithOffset - timeControlStart.TimeStampWithOffset).ToString();

            }
            else
            {
                textBoxDurationTime.Enabled = false;
                textBoxDurationTime.Text = string.Empty;
            }
        }

        private void checkBoxEndTime_CheckedChanged(object sender, EventArgs e)
        {
            timeControlEnd.Enabled = checkBoxEndTime.Checked;
            labelDefaultEnd.Text = checkBoxEndTime.Checked ? string.Empty : _labelDefaultEnd;
            CheckIfErrorTimeControls();
            UpdateDurationText();
        }

        private void checkBoxLiveBackoff_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownBackoffSeconds.Enabled = checkBoxLiveBackoff.Checked;
            labelDefaultBakckoff.Text = checkBoxLiveBackoff.Checked ? string.Empty : _labelDefaultBakckoff;
            CheckIfErrorTimeControls();
        }

        private void checkBoxDVRWindow_CheckedChanged(object sender, EventArgs e)
        {
            timeControlDVR.Enabled = checkBoxPresentationWindowDuration.Checked;
            labelDefaultDVR.Text = checkBoxPresentationWindowDuration.Checked ? string.Empty : _labelDefaultDVR;
            CheckIfErrorTimeControls();
        }


        private void timeControlDVR_Load(object sender, EventArgs e)
        {

        }

        private void timeControlDVR_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
        }

        private void DynManifestFilter_Shown(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxRawMode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRawMode.Checked)
            {
                var seltab = tabControl1.SelectedTab;
                tabControl1.TabPages.Insert(0, tabPageTRRaw);
                tabControl1.TabPages.Remove(tabPageTR);
                if (seltab == tabPageTR) tabControl1.SelectedTab = tabPageTRRaw;

                var ptr = GetFilterPresenTationTRDefaultMode;
                textBoxRawTimescale.Text = ptr.Timescale.ToString();
                textBoxRawStart.Text = ptr.StartTimestamp.ToString();
                textBoxRawEnd.Text = ptr.EndTimestamp.ToString();
                textBoxRawDVR.Text = ptr.PresentationWindowDuration.ToString();
                textBoxRawBackoff.Text = ptr.LiveBackoffDuration.ToString();
                checkBoxForValueLiveRaw.Checked = ptr.ForceEndTimestamp ?? false;
            }
            else
            {
                var seltab = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(tabPageTRRaw);
                tabControl1.TabPages.Insert(0, tabPageTR);
                if (seltab == tabPageTRRaw) tabControl1.SelectedTab = tabPageTR;
            }
        }

        private void timeControlStart_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
            UpdateDurationText();
        }

        private async void comboBoxLocatorsFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtername = ((Item)comboBoxLocatorsFilters.SelectedItem).Value;
            _amsClient.RefreshTokenIfNeeded();

            if (filtername != null)
            {
                object importfilter = null;
                if (filtername.StartsWith(Constants.AssetIdPrefix)) // asset filter
                {
                    var filters = (await _amsClient.AMSclient.AssetFilters.ListWithHttpMessagesAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, _parentAsset.Name)).Body;
                    importfilter = filters.Where(f => f.Id == filtername).FirstOrDefault();

                    if (importfilter != null)
                    {
                        filtertracks = ConvertFilterTracksToInternalVar(((AssetFilter)importfilter).Tracks);
                        RefreshTracks();
                        RefreshTracksConditions();

                    }
                }
                else // global filter
                {
                    var filters = (await _amsClient.AMSclient.AccountFilters.ListWithHttpMessagesAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).Body;
                    importfilter = filters.Where(f => f.Name == filtername).FirstOrDefault();

                    if (importfilter != null)
                    {
                        filtertracks = ConvertFilterTracksToInternalVar(((AccountFilter)importfilter).Tracks);
                        RefreshTracks();
                        RefreshTracksConditions();

                    }
                }
            }
        }

        private void toolStripMenuItemFilesCopyClipboard_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStripInfo_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            DataGridView DG = (DataGridView)contextmenu.SourceControl;

            if (DG.SelectedCells.Count == 1)
            {
                if (DG.SelectedCells[0].Value != null)
                {
                    System.Windows.Forms.Clipboard.SetText(DG.SelectedCells[0].Value.ToString());

                }
                else
                {
                    System.Windows.Forms.Clipboard.Clear();
                }

            }
        }

        private void textBoxRawulong_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            bool Error = false;
            try
            {
                var value = string.IsNullOrWhiteSpace(tb.Text) ? null : (ulong?)ulong.Parse(tb.Text);
            }
            catch
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.DynManifestFilter_textBoxRawulong_Validating_IncorrectValue);
                Error = true;

            }
            if (!Error)
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        private void textBoxRawTimeSpan_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            bool Error = false;
            try
            {
                var value = string.IsNullOrWhiteSpace(tb.Text) ? null : (TimeSpan?)TimeSpan.FromTicks(long.Parse(tb.Text));
            }
            catch
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.DynManifestFilter_textBoxRawulong_Validating_IncorrectValue);
                Error = true;

            }
            if (!Error)
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        private void checkBoxFirstQualityBitrate_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownFirstQualityBitrate.Enabled = checkBoxFirstQualityBitrate.Checked;
        }
    }
}