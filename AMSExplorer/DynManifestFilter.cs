﻿//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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

using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AMSExplorer
{
    public partial class DynManifestFilter : Form
    {
        private const string TextCreateTempLoc = "A temporary clear locator is going to be created to access content timing information. It will be deleted a few seconds after. ";
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
        private readonly MediaAssetResource _parentAsset;
        private readonly SubClipConfiguration _subclipconfig;
        private readonly AMSClientV3 _amsClient;
        private string _labelStartTimeDefault;
        private string _labelDefaultEnd;
        private string _labelDefaultDVR;
        private string _labelDefaultBakckoff;
        private readonly object _filterToDisplay;

        public DynManifestFilter(AMSClientV3 amsClient, object filterToDisplay = null, MediaAssetResource parentAsset = null, SubClipConfiguration subclipconfig = null)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = amsClient;
            _filterToDisplay = filterToDisplay;
            _parentAsset = parentAsset;
            _subclipconfig = subclipconfig;
        }

        private async void FillComboBoxImportFilters(MediaAssetResource asset)
        {
            // combobox for filters

            comboBoxLocatorsFilters.BeginUpdate();
            comboBoxLocatorsFilters.Items.Add(new Item(AMSExplorer.Properties.Resources.DynManifestFilter_FillComboBoxImportFilters_ImportTrackFilteringFrom, null));

            if (asset != null)
            {
                // asset filters
                var filters = asset.GetMediaAssetFilters().GetAllAsync();
                await foreach (var filter in filters)
                {
                    if (filter.Data.Tracks.Count > 0)
                    {
                        comboBoxLocatorsFilters.Items.Add(new Item("Asset filter : " + filter.Data.Name, filter.Data.Id));
                    }
                }
            }

            // account filters
            var acctFilters = _amsClient.AMSclient.GetMediaServicesAccountFilters().GetAllAsync();
            await foreach (var filter in acctFilters)
            {
                if (filter.Data.Tracks.Count > 0)
                {
                    comboBoxLocatorsFilters.Items.Add(new Item("Account filter : " + filter.Data.Name, filter.Data.Name));
                }
            }

            if (comboBoxLocatorsFilters.Items.Count > 1)
            {
                comboBoxLocatorsFilters.Enabled = true;
            }
            comboBoxLocatorsFilters.SelectedIndex = 0;
            comboBoxLocatorsFilters.EndUpdate();
        }

        private async void DynManifestFilter_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

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
                PresentationTimeRange _filter_presentationtimerange = new() { Timescale = 10000000 };
                filtertracks = new List<ExFilterTrack>();
                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                _timescale = (long)_filter_presentationtimerange.Timescale;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _filter_presentationtimerange.Timescale;

                textBoxFilterTimeScale.Text = (_filter_presentationtimerange.Timescale == null) ? "(default)" : _filter_presentationtimerange.Timescale.ToString();
            }


            /////////////////////////////////////////////
            // Existing Account Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay != null && _filterToDisplay is MediaServicesAccountFilterResource accFilter && _parentAsset == null)
            {
                newfilter = false;
                isAccountFilter = true;
                DisplayFilterInfo();
                _filter_name = accFilter.Data.Name;
                filtertracks = ConvertFilterTracksToInternalVar(accFilter.Data.Tracks);

                _timescale = accFilter.Data.PresentationTimeRange?.Timescale ?? _timescale;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _timescale;
                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                toolTip1.SetToolTip(buttonOk, AMSExplorer.Properties.Resources.DynManifestFilter_DynManifestFilter_Load_ItCanTakeUpTo2MinutesForStreamingEndpointToRefreshTheRules);

                textBoxFilterName.Enabled = false; // no way to change the filter name
                textBoxFilterName.Text = _filter_name;

                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                checkBoxStartTime.Checked = accFilter.Data.PresentationTimeRange?.StartTimestamp != null;
                checkBoxEndTime.Checked = accFilter.Data.PresentationTimeRange?.EndTimestamp != null;
                checkBoxPresentationWindowDuration.Checked = accFilter.Data.PresentationTimeRange?.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = accFilter.Data.PresentationTimeRange?.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(accFilter.Data.PresentationTimeRange?.StartTimestamp, 0);
                timeControlEnd.SetScaledTimeStamp(accFilter.Data.PresentationTimeRange?.EndTimestamp, 0);
                timeControlDVR.SetScaledTimeStamp(accFilter.Data.PresentationTimeRange?.PresentationWindowDuration, 0);

                if (accFilter.Data.PresentationTimeRange?.LiveBackoffDuration != null)
                {
                    TimeSpan backoff = TimeSpan.FromTicks((long)(accFilter.Data.PresentationTimeRange.LiveBackoffDuration / _timescale) * TimeSpan.TicksPerSecond);
                    numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);
                }


                if (accFilter.Data.FirstQualityBitrate != null)
                {
                    checkBoxFirstQualityBitrate.Checked = true;
                    numericUpDownFirstQualityBitrate.Value = (int)accFilter.Data.FirstQualityBitrate;
                }

                checkBoxForValueForLive.Checked = accFilter.Data.PresentationTimeRange?.ForceEndTimestamp ?? false;

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
                textBoxAssetName.Text = _parentAsset != null ? _parentAsset.Data.Name : string.Empty;


                // let's try to read asset timing
                XDocument manifest = null;
                try
                {
                    manifest = await AssetTools.TryToGetClientManifestContentAsABlobAsync(_parentAsset, _amsClient);
                }
                catch
                {
                }

                if (manifest == null)
                {
                    try
                    {
                        if (MessageBox.Show(TextCreateTempLoc, "Locator creation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            manifest = await AssetTools.TryToGetClientManifestContentUsingStreamingLocatorAsync(_parentAsset, _amsClient);
                        }
                    }
                    catch
                    {
                    }
                }

                if (manifest != null)
                {
                    _parentassetmanifestdata = AssetTools.GetManifestTimingData(manifest);
                }

                if (!_parentassetmanifestdata.Error)  // we were able to read asset timings and not live
                {
                    // timescale
                    timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _parentassetmanifestdata.TimeScale;
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
                        timeControlStart.SetTimeStamp(_subclipconfig.AbsoluteStartTime - timeControlStart.GetOffSetAsTimeSpan());
                        timeControlEnd.SetTimeStamp(_subclipconfig.AbsoluteEndTime - timeControlStart.GetOffSetAsTimeSpan());
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
            else if (_filterToDisplay != null && _filterToDisplay is MediaAssetFilterResource assetFilter && _parentAsset != null)
            {
                newfilter = false;
                isAccountFilter = false;
                DisplayFilterInfo();

                _filter_name = assetFilter.Data.Name;
                filtertracks = ConvertFilterTracksToInternalVar(assetFilter.Data.Tracks);

                _timescale = assetFilter.Data.PresentationTimeRange?.Timescale ?? _timescale;

                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                toolTip1.SetToolTip(buttonOk, AMSExplorer.Properties.Resources.DynManifestFilter_DynManifestFilter_Load_ItCanTakeUpTo2MinutesForStreamingEndpointToRefreshTheRules);

                labelFilterTitle.Text = "Asset Filter";
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                textBoxAssetName.Text = _parentAsset != null ? _parentAsset.Data.Name : string.Empty;

                textBoxFilterName.Enabled = false; // no way to change the filter name
                textBoxFilterName.Text = _filter_name;


                // let's try to read asset timing
                XDocument manifest = null;
                try
                {
                    manifest = await AssetTools.TryToGetClientManifestContentAsABlobAsync(_parentAsset, _amsClient);
                }
                catch
                {
                }

                if (manifest == null)
                {
                    try
                    {
                        if (MessageBox.Show(TextCreateTempLoc, "Locator creation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            manifest = await AssetTools.TryToGetClientManifestContentUsingStreamingLocatorAsync(_parentAsset, _amsClient);
                        }
                    }
                    catch
                    {
                    }
                }

                if (manifest != null)
                {
                    _parentassetmanifestdata = AssetTools.GetManifestTimingData(manifest);
                }

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

                checkBoxStartTime.Checked = assetFilter.Data.PresentationTimeRange?.StartTimestamp != null;
                checkBoxEndTime.Checked = assetFilter.Data.PresentationTimeRange?.EndTimestamp != null;
                checkBoxPresentationWindowDuration.Checked = assetFilter.Data.PresentationTimeRange?.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = assetFilter.Data.PresentationTimeRange?.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(assetFilter.Data.PresentationTimeRange?.StartTimestamp, 0);
                timeControlEnd.SetScaledTimeStamp(assetFilter.Data.PresentationTimeRange?.EndTimestamp, 0);  // we don't want to pass the max value to the control (overflow)
                timeControlDVR.SetScaledTimeStamp(assetFilter.Data.PresentationTimeRange?.PresentationWindowDuration, 0);

                if (assetFilter.Data.PresentationTimeRange?.LiveBackoffDuration != null)
                {
                    TimeSpan backoff = TimeSpan.FromTicks((long)(assetFilter.Data.PresentationTimeRange.LiveBackoffDuration / _timescale) * TimeSpan.TicksPerSecond);
                    numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);
                }


                if (assetFilter.Data.FirstQualityBitrate != null)
                {
                    checkBoxFirstQualityBitrate.Checked = true;
                    numericUpDownFirstQualityBitrate.Value = (int)assetFilter.Data.FirstQualityBitrate;
                }

                checkBoxForValueForLive.Checked = assetFilter.Data.PresentationTimeRange?.ForceEndTimestamp ?? false;
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

            DataGridViewComboBoxColumn columnProperty = new()
            {
                DataSource = dataProperty,
                ValueMember = "Property",
                DisplayMember = "Description"
            };
            dataGridViewTracks.Columns.Add(columnProperty);

            DataGridViewComboBoxColumn columnOperator = new()
            {
                DataSource = dataOperator,
                ValueMember = "Operator",
                DisplayMember = "Description"
            };
            dataGridViewTracks.Columns.Add(columnOperator);

            DataGridViewTextBoxColumn columnValue = new();
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
            DGInfo.Columns[0].DefaultCellStyle.BackColor = System.Drawing.Color.Gainsboro;

            if (isAccountFilter)
            {
                MediaServicesAccountFilterResource accfilter = (MediaServicesAccountFilterResource)_filterToDisplay;
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, accfilter.Data.Name);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_GlobalFilter);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_FirstQualityBitrate, accfilter.Data.FirstQualityBitrate == null ? Constants.stringNull : accfilter.Data.FirstQualityBitrate.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_Timescale, accfilter.Data.PresentationTimeRange?.Timescale.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_StartTimestamp, accfilter.Data.PresentationTimeRange?.StartTimestamp.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_EndTimestamp, accfilter.Data.PresentationTimeRange?.EndTimestamp.ToString());
                DGInfo.Rows.Add("Force end timestamp", accfilter.Data.PresentationTimeRange?.ForceEndTimestamp);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_PresentationWindowDuration, accfilter.Data.PresentationTimeRange?.PresentationWindowDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_LiveBackoffDuration, accfilter.Data.PresentationTimeRange?.LiveBackoffDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_TrackCount, accfilter.Data.Tracks.Count);
            }
            else
            {
                MediaAssetFilterResource assetfilter = (MediaAssetFilterResource)_filterToDisplay;
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, assetfilter.Data.Name);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_AssetFilter);
                DGInfo.Rows.Add("Id", assetfilter.Id);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_FirstQualityBitrate, assetfilter.Data.FirstQualityBitrate == null ? Constants.stringNull : assetfilter.Data.FirstQualityBitrate.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_Timescale, assetfilter.Data.PresentationTimeRange?.Timescale.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_StartTimestamp, assetfilter.Data.PresentationTimeRange?.StartTimestamp.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_EndTimestamp, assetfilter.Data.PresentationTimeRange?.EndTimestamp.ToString());
                DGInfo.Rows.Add("Force end timestamp", assetfilter.Data.PresentationTimeRange?.ForceEndTimestamp);

                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_PresentationWindowDuration, assetfilter.Data.PresentationTimeRange?.PresentationWindowDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_LiveBackoffDuration, assetfilter.Data.PresentationTimeRange?.LiveBackoffDuration.ToString());
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_TrackCount, assetfilter.Data.Tracks.Count);
            }
        }

        private List<ExFilterTrack> ConvertFilterTracksToInternalVar(IList<FilterTrackSelection> tracks)
        // copy filter tracks to internal list used for display in grid
        {
            List<ExFilterTrack> targettracks = new();
            foreach (FilterTrackSelection track in tracks)
            {
                ExFilterTrack mytrack = new();
                List<ExCondition> myconditions = new();
                foreach (FilterTrackPropertyCondition condition in track.TrackSelections)
                {
                    ExCondition mycondition = new()
                    {
                        Oper = condition.Operation.ToString(),
                        Property = condition.Property.ToString(),
                        Value = condition.Value
                    };

                    myconditions.Add(mycondition);
                }
                mytrack.Conditions = myconditions;
                targettracks.Add(mytrack);
            }
            return targettracks;
        }


        private List<FilterTrackSelection> CreateFilterTracks()
        // use internal list to create filter tracks
        {
            List<FilterTrackSelection> filterTrackSelectStatements = new();

            foreach (ExFilterTrack track in filtertracks)
            {
                FilterTrackSelection filterTrackSelectStatement = new(new List<FilterTrackPropertyCondition>());

                foreach (ExCondition condition in track.Conditions)
                {
                    FilterTrackPropertyCompareOperation op;
                    if (condition.Oper == FilterTrackPropertyCompareOperation.Equal.ToString())
                    {
                        op = FilterTrackPropertyCompareOperation.Equal;
                    }
                    else
                    {
                        op = FilterTrackPropertyCompareOperation.NotEqual;
                    }

                    FilterTrackPropertyType prop;
                    if (condition.Property == FilterTrackPropertyType.Bitrate.ToString())
                    {
                        prop = FilterTrackPropertyType.Bitrate;
                    }
                    else if (condition.Property == FilterTrackPropertyType.FourCC.ToString())
                    {
                        prop = FilterTrackPropertyType.FourCC;
                    }
                    else if (condition.Property == FilterTrackPropertyType.Language.ToString())
                    {
                        prop = FilterTrackPropertyType.Language;
                    }
                    else if (condition.Property == FilterTrackPropertyType.Name.ToString())
                    {
                        prop = FilterTrackPropertyType.Name;
                    }
                    else if (condition.Property == FilterTrackPropertyType.Type.ToString())
                    {
                        prop = FilterTrackPropertyType.Type;
                    }
                    else
                    {
                        prop = FilterTrackPropertyType.Unknown;
                    }

                    filterTrackSelectStatement.TrackSelections.Add(new FilterTrackPropertyCondition(prop, condition.Value, op));
                }
                filterTrackSelectStatements.Add(filterTrackSelectStatement);
            }

            return filterTrackSelectStatements;
        }

        private void RefreshTracks()
        {
            listBoxTracks.Items.Clear();
            dataGridViewTracks.Rows.Clear();

            int i = 1;
            foreach (ExFilterTrack track in filtertracks)
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
                FilterCreationInfo filterinfo = new()
                {
                    Name = newfilter ? textBoxFilterName.Text : _filter_name,
                    Firstquality = checkBoxFirstQualityBitrate.Checked ? (int)numericUpDownFirstQualityBitrate.Value : null
                };
                if (checkBoxRawMode.Checked) // RAW Mode
                {
                    try
                    {
                        filterinfo.Presentationtimerange = new PresentationTimeRange()
                        {
                            StartTimestamp = string.IsNullOrWhiteSpace(textBoxRawStart.Text) ? null : long.Parse(textBoxRawStart.Text),
                            EndTimestamp = string.IsNullOrWhiteSpace(textBoxRawEnd.Text) ? null : long.Parse(textBoxRawEnd.Text),
                            PresentationWindowDuration = string.IsNullOrWhiteSpace(textBoxRawDVR.Text) ? null : long.Parse(textBoxRawDVR.Text),
                            LiveBackoffDuration = string.IsNullOrWhiteSpace(textBoxRawBackoff.Text) ? null : long.Parse(textBoxRawBackoff.Text),
                            Timescale = string.IsNullOrWhiteSpace(textBoxRawTimescale.Text) ? null : long.Parse(textBoxRawTimescale.Text),
                            ForceEndTimestamp = checkBoxForValueLiveRaw.Checked ? true : null
                        };

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

        private PresentationTimeRange GetFilterPresenTationTRDefaultMode => new()
        {
            StartTimestamp = checkBoxStartTime.Checked ? timeControlStart.ScaledTimeStampWithOffset : null,
            EndTimestamp = checkBoxEndTime.Checked ? timeControlEnd.ScaledTimeStampWithOffset : null,
            PresentationWindowDuration = checkBoxPresentationWindowDuration.Checked ? timeControlDVR.TimeStampWithoutOffset.Ticks : null,
            LiveBackoffDuration = checkBoxLiveBackoff.Checked ? (long)numericUpDownBackoffSeconds.Value : null,
            Timescale = _timescale,
            ForceEndTimestamp = checkBoxForValueForLive.Checked ? true : null
        };



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
                        DataGridViewComboBoxCell cellValue = new()
                        {
                            DataSource = dataPropertyType,
                            ValueMember = "Value",
                            DisplayMember = "Description"
                        };
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else if (pstring == nameof(FilterTrackPropertyType.FourCC)) // property FourCC
                    {
                        DataGridViewComboBoxCell cellValue = new()
                        {
                            DataSource = dataPropertyFourCC,
                            ValueMember = "Value",
                            DisplayMember = "Description"
                        };
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else
                    {
                        DataGridViewTextBoxCell cellValue = new();
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }

                // let's update the internal var
                switch (dataGridViewTracks.CurrentCell.ColumnIndex)
                {
                    case 0: // property
                        filtertracks[listBoxTracks.SelectedIndex].Conditions[dataGridViewTracks.CurrentCell.RowIndex].Property = dataGridViewTracks.CurrentCell.Value.ToString();
                        break;

                    case 1: // operator
                        filtertracks[listBoxTracks.SelectedIndex].Conditions[dataGridViewTracks.CurrentCell.RowIndex].Oper = dataGridViewTracks.CurrentCell.Value.ToString();
                        break;

                    case 2: // value
                        filtertracks[listBoxTracks.SelectedIndex].Conditions[dataGridViewTracks.CurrentCell.RowIndex].Value = dataGridViewTracks.CurrentCell.Value.ToString();
                        break;
                }

            }
        }


        private void buttonAddTrack_Click(object sender, EventArgs e)
        {
            ExFilterTrack track = new() { Conditions = new List<ExCondition>() };
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
                filtertracks[listBoxTracks.SelectedIndex].Conditions.Add(new ExCondition() { Oper = FilterTrackPropertyCompareOperation.Equal.ToString() });
                RefreshTracksConditions();
            }
        }

        private void RefreshTracksConditions()
        {
            dataGridViewTracks.Rows.Clear();
            if (listBoxTracks.SelectedIndex > -1)
            {
                ExFilterTrack track = filtertracks[listBoxTracks.SelectedIndex];
                foreach (ExCondition condition in track.Conditions)
                {
                    if (condition.Property == FilterTrackPropertyType.Type.ToString()) // property type - we want to propose audio, video or text dropbox
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Type.ToString(), condition.Oper, condition.Value);
                        DataGridViewComboBoxCell cellValue = new()
                        {
                            DataSource = dataPropertyType,
                            ValueMember = "Value",
                            DisplayMember = "Description",
                            Value = condition.Value
                        };
                        dataGridViewTracks[2, index] = cellValue;
                    }
                    else if (condition.Property == FilterTrackPropertyType.FourCC.ToString()) // property FourCC - we want to propose supported FourCC
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterTrackPropertyType.FourCC.ToString(), condition.Oper, condition.Value);
                        DataGridViewComboBoxCell cellValue = new()
                        {
                            DataSource = dataPropertyFourCC,
                            ValueMember = "Value",
                            DisplayMember = "Description",
                            Value = condition.Value
                        };
                        dataGridViewTracks[2, index] = cellValue;
                    }
                    else if (condition.Property == FilterTrackPropertyType.Language.ToString()) // property language
                    {
                        dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Language.ToString(), condition.Oper, condition.Value);
                    }
                    else if (condition.Property == FilterTrackPropertyType.Bitrate.ToString()) // property bitrate
                    {
                        dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Bitrate.ToString(), condition.Oper, condition.Value);
                    }
                    else if (condition.Property == FilterTrackPropertyType.Name.ToString()) // property Name - we want to propose supported FourCC
                    {
                        dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Name.ToString(), condition.Oper, condition.Value);
                    }
                    else if (condition.Property == FilterTrackPropertyType.Unknown.ToString()) // property Name - we want to propose supported FourCC
                    {
                        dataGridViewTracks.Rows.Add(FilterTrackPropertyType.Unknown.ToString(), condition.Oper, condition.Value);
                    }
                    else
                    {
                        dataGridViewTracks.Rows.Add(condition.Property, condition.Oper, condition.Value);
                    }
                }
            }
        }

        private void buttonDeleteCondition_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1 && dataGridViewTracks.SelectedRows.Count > 0)
            {
                filtertracks[listBoxTracks.SelectedIndex].Conditions.RemoveAt(dataGridViewTracks.SelectedRows[0].Index);
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
                errorProvider1.SetError(tb, string.Empty);
            }
        }


        private void buttonInsertSample_Click(object sender, EventArgs e)
        {
            // Filter sample

            List<FilterTrackSelection> filterTrackSelections = new()
            {
                new FilterTrackSelection
                (
                    new List<FilterTrackPropertyCondition>()
                    {
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Type, FilterPropertyTypeValue.Video, FilterTrackPropertyCompareOperation.Equal),
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Bitrate, "0-1048576", FilterTrackPropertyCompareOperation.Equal)
                    }
                ),
                new FilterTrackSelection
                (
                    new List<FilterTrackPropertyCondition>()
                    {
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Type, FilterPropertyTypeValue.Audio, FilterTrackPropertyCompareOperation.Equal),
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.FourCC, FilterPropertyFourCCValue.mp4a, FilterTrackPropertyCompareOperation.Equal)
                    }
                ),
                new FilterTrackSelection
                (
                    new List<FilterTrackPropertyCondition>()
                    {
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Type, FilterPropertyTypeValue.Text, FilterTrackPropertyCompareOperation.Equal),
                        new FilterTrackPropertyCondition(FilterTrackPropertyType.Language, "en", FilterTrackPropertyCompareOperation.Equal)
                    }
                )
            };

            filtertracks = ConvertFilterTracksToInternalVar(filterTrackSelections);

            RefreshTracks();
            RefreshTracksConditions();
        }



        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo { FileName = e.Link.LinkData as string, UseShellExecute = true }
            }; p.Start();
        }


        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = e.CellBounds;
            rectangle.Inflate(-1, -1);
            ControlPaint.DrawBorder(e.Graphics, rectangle, System.Drawing.Color.Gray, ButtonBorderStyle.Dotted); // 
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
                errorProvider1.SetError(timeControlStart, string.Empty);
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
                errorProvider1.SetError(timeControlEnd, string.Empty);
            }

            // dvr
            if (checkBoxPresentationWindowDuration.Checked && timeControlDVR.TimeStampWithoutOffset < TimeSpan.FromMinutes(1))
            {
                errorProvider1.SetError(timeControlDVR, AMSExplorer.Properties.Resources.DynManifestFilter_CheckIfErrorTimeControls_TheDVRWindowMustBeAtLeast2MinutesOrMore);
            }
            else
            {
                errorProvider1.SetError(timeControlDVR, string.Empty);
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
            Telemetry.TrackPageView(this.Name);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxRawMode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRawMode.Checked)
            {
                TabPage seltab = tabControl1.SelectedTab;
                tabControl1.TabPages.Insert(0, tabPageTRRaw);
                tabControl1.TabPages.Remove(tabPageTR);
                if (seltab == tabPageTR)
                {
                    tabControl1.SelectedTab = tabPageTRRaw;
                }

                PresentationTimeRange ptr = GetFilterPresenTationTRDefaultMode;
                textBoxRawTimescale.Text = ptr.Timescale.ToString();
                textBoxRawStart.Text = ptr.StartTimestamp.ToString();
                textBoxRawEnd.Text = ptr.EndTimestamp.ToString();
                textBoxRawDVR.Text = ptr.PresentationWindowDuration.ToString();
                textBoxRawBackoff.Text = ptr.LiveBackoffDuration.ToString();
                checkBoxForValueLiveRaw.Checked = ptr.ForceEndTimestamp ?? false;
            }
            else
            {
                TabPage seltab = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(tabPageTRRaw);
                tabControl1.TabPages.Insert(0, tabPageTR);
                if (seltab == tabPageTRRaw)
                {
                    tabControl1.SelectedTab = tabPageTR;
                }
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


            if (filtername != null)
            {

                if (filtername.StartsWith(Constants.AssetIdPrefix)) // asset filter
                {
                    var assetFilters = _parentAsset.GetMediaAssetFilters().GetAllAsync();
                    await foreach (var filter in assetFilters)
                    {
                        if (filter.Data.Id == filtername)
                        {
                            filtertracks = ConvertFilterTracksToInternalVar(filter.Data.Tracks);
                            RefreshTracks();
                            RefreshTracksConditions();
                            break;
                        }
                    }
                }
                else // global filter
                {
                    // account filters
                    try
                    {
                        var acctFilter = await _amsClient.AMSclient.GetMediaServicesAccountFilterAsync(filtername);
                        filtertracks = ConvertFilterTracksToInternalVar(acctFilter.Value.Data.Tracks);
                        RefreshTracks();
                        RefreshTracksConditions();
                    }
                    catch
                    {

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
                ulong? value = string.IsNullOrWhiteSpace(tb.Text) ? null : ulong.Parse(tb.Text);
            }
            catch
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.DynManifestFilter_textBoxRawulong_Validating_IncorrectValue);
                Error = true;

            }
            if (!Error)
            {
                errorProvider1.SetError(tb, string.Empty);
            }
        }

        private void textBoxRawTimeSpan_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            bool Error = false;
            try
            {
                TimeSpan? value = string.IsNullOrWhiteSpace(tb.Text) ? null : TimeSpan.FromTicks(long.Parse(tb.Text));
            }
            catch
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.DynManifestFilter_textBoxRawulong_Validating_IncorrectValue);
                Error = true;

            }
            if (!Error)
            {
                errorProvider1.SetError(tb, string.Empty);
            }
        }

        private void checkBoxFirstQualityBitrate_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownFirstQualityBitrate.Enabled = checkBoxFirstQualityBitrate.Checked;
        }

        private void DynManifestFilter_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            // DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelFilterTitle, labeltime1, labeltime2, labeltime3, labeltime4, labeltime5, checkBoxLiveBackoff, timeControlStart, timeControlEnd, timeControlDVR, contextMenuStripInfo }, e, this);
        }
    }
}