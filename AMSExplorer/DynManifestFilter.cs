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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Xml;
using System.Xml.Linq;



namespace AMSExplorer
{
    public partial class DynManifestFilter : Form
    {
        private string _filter_name;
        private PresentationTimeRange _filter_presentationtimerange;
        private List<ExFilterTrack> filtertracks;

        private bool isGlobalFilter = true;
        private bool newfilter = true;
        CloudMediaContext _context;
        private DataTable dataPropertyType;
        private DataTable dataPropertyFourCC;
        private DataTable dataProperty;
        private DataTable dataOperator;
        private IAsset _parentAsset = null;
        private ManifestTimingData _parentassetmanifestdata;
        private ulong? _timescale = null;
        private SubClipConfiguration _subclipconfig;
        private IStreamingFilter _filterToDisplay;
        private string _labelStartTimeDefault;
        private string _labelDefaultEnd;
        private string _labelDefaultDVR;
        private string _labelDefaultBakckoff;

        public DynManifestFilter(CloudMediaContext context, IStreamingFilter filterToDisplay = null, IAsset parentAsset = null, SubClipConfiguration subclipconfig = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _filterToDisplay = filterToDisplay;
            _parentAsset = parentAsset;
            _subclipconfig = subclipconfig;
        }

        private void FillComboBoxImportFilters(IAsset asset)
        {
            // combobox for filters

            comboBoxLocatorsFilters.BeginUpdate();

            comboBoxLocatorsFilters.Items.Add(new Item(AMSExplorer.Properties.Resources.DynManifestFilter_FillComboBoxImportFilters_ImportTrackFilteringFrom, null));

            if (asset != null)
            {
                asset.AssetFilters.Where(g => g.Tracks.Count > 0).ToList().ForEach(g => comboBoxLocatorsFilters.Items.Add(new Item("Asset filter : " + g.Name, g.Id)));
            }
            // global filters
            _context.Filters.ToList().Where(g => g.Tracks.Count > 0).ToList().ForEach(g => comboBoxLocatorsFilters.Items.Add(new Item("Global filter : " + g.Name, g.Name)));
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
            // New Global Filter
            /////////////////////////////////////////////
            if (_filterToDisplay == null && _parentAsset == null)
            {
                newfilter = true;
                isGlobalFilter = true;
                tabControl1.TabPages.Remove(tabPageInformation);
                _filter_presentationtimerange = new PresentationTimeRange();
                filtertracks = new List<ExFilterTrack>();
                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                _timescale = timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _filter_presentationtimerange.Timescale;
                textBoxFilterTimeScale.Text = (_filter_presentationtimerange.Timescale == null) ? "(default)" : _filter_presentationtimerange.Timescale.ToString();
            }


            /////////////////////////////////////////////
            // Existing Global Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay != null && _parentAsset == null)
            {
                newfilter = false;
                isGlobalFilter = true;
                DisplayFilterInfo();
                _filter_name = _filterToDisplay.Name;
                _filter_presentationtimerange = _filterToDisplay.PresentationTimeRange;
                filtertracks = ConvertFilterTracksToInternalVar(_filterToDisplay.Tracks);

                _timescale = _filterToDisplay.PresentationTimeRange.Timescale;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _timescale;
                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                toolTip1.SetToolTip(this.buttonOk, AMSExplorer.Properties.Resources.DynManifestFilter_DynManifestFilter_Load_ItCanTakeUpTo2MinutesForStreamingEndpointToRefreshTheRules);

                textBoxFilterName.Enabled = false; // no way to change the filter name
                textBoxFilterName.Text = _filter_name;

                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                checkBoxStartTime.Checked = _filter_presentationtimerange.StartTimestamp != null;
                checkBoxEndTime.Checked = _filter_presentationtimerange.EndTimestamp != null;
                checkBoxDVRWindow.Checked = _filter_presentationtimerange.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = _filter_presentationtimerange.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(_filter_presentationtimerange.StartTimestamp);
                timeControlEnd.SetScaledTimeStamp(_filter_presentationtimerange.EndTimestamp);
                timeControlDVR.SetTimeStamp(_filter_presentationtimerange.PresentationWindowDuration ?? TimeSpan.FromMinutes(2));  // we don't want to pass the max value to the control (overflow)
                TimeSpan backoff = _filter_presentationtimerange.LiveBackoffDuration ?? new TimeSpan(0);
                numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);

                if (_filterToDisplay.FirstQuality != null)
                {
                    checkBoxFirstQualityBitrate.Checked = true;
                    numericUpDownFirstQualityBitrate.Value = _filterToDisplay.FirstQuality.Bitrate;
                }
            }


            /////////////////////////////////////////////
            // New Asset Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay == null && _parentAsset != null)
            {
                newfilter = true;
                isGlobalFilter = false;
                tabControl1.TabPages.Remove(tabPageInformation);

                _filter_presentationtimerange = new PresentationTimeRange();

                filtertracks = new List<ExFilterTrack>();

                labelFilterTitle.Text = "Asset Filter";
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                textBoxAssetName.Text = _parentAsset != null ? _parentAsset.Name : string.Empty;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(_parentAsset);

                if (!_parentassetmanifestdata.Error)  // we were able to read asset timings and not live
                {
                    // timescale
                    _timescale = timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _parentassetmanifestdata.TimeScale;
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
                    timeControlEnd.SetTimeStamp(timeControlEnd.Max);
                    labelassetduration.Visible = textBoxAssetDuration.Visible = false;
                }
            }

            /////////////////////////////////////////////
            // Existing Asset Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay != null && _parentAsset != null)
            {
                newfilter = false;
                isGlobalFilter = false;
                DisplayFilterInfo();

                _filter_name = _filterToDisplay.Name;
                _filter_presentationtimerange = _filterToDisplay.PresentationTimeRange;
                filtertracks = ConvertFilterTracksToInternalVar(_filterToDisplay.Tracks);

                _timescale = _filterToDisplay.PresentationTimeRange.Timescale;

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
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(_parentAsset);

                _timescale = timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _filterToDisplay.PresentationTimeRange.Timescale;

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


                checkBoxStartTime.Checked = _filter_presentationtimerange.StartTimestamp != null;
                checkBoxEndTime.Checked = _filter_presentationtimerange.EndTimestamp != null;
                checkBoxDVRWindow.Checked = _filter_presentationtimerange.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = _filter_presentationtimerange.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(_filter_presentationtimerange.StartTimestamp);
                timeControlEnd.SetScaledTimeStamp(_filter_presentationtimerange.EndTimestamp);  // we don't want to pass the max value to the control (overflow)
                timeControlDVR.SetTimeStamp(_filter_presentationtimerange.PresentationWindowDuration ?? TimeSpan.FromMinutes(2));  // we don't want to pass the max value to the control (overflow)
                TimeSpan backoff = _filter_presentationtimerange.LiveBackoffDuration ?? new TimeSpan(0);
                numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);

                if (_filterToDisplay.FirstQuality != null)
                {
                    checkBoxFirstQualityBitrate.Checked = true;
                    numericUpDownFirstQualityBitrate.Value = _filterToDisplay.FirstQuality.Bitrate;
                }
            }

            // Common code
            textBoxFilterTimeScale.Text = (_timescale == null) ? "(default)" : _timescale.ToString();

            // dataPropertyType
            dataPropertyType = new DataTable();
            dataPropertyType.Columns.Add(new DataColumn("Value", typeof(string)));
            dataPropertyType.Columns.Add(new DataColumn("Description", typeof(string)));

            dataPropertyType.Rows.Add(FilterTrackType.Audio.ToString(), FilterTrackType.Audio.ToString());
            dataPropertyType.Rows.Add(FilterTrackType.Video.ToString(), FilterTrackType.Video.ToString());
            dataPropertyType.Rows.Add(FilterTrackType.Text.ToString(), FilterTrackType.Text.ToString());


            // FilterPropertyFourCCValue
            dataPropertyFourCC = new DataTable();
            dataPropertyFourCC.Columns.Add(new DataColumn("Value", typeof(string)));
            dataPropertyFourCC.Columns.Add(new DataColumn("Description", typeof(string)));

            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.avc1, FilterPropertyFourCCValue.avc1);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.ec3, FilterPropertyFourCCValue.ec3);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.mp4a, FilterPropertyFourCCValue.mp4a);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.mp4v, FilterPropertyFourCCValue.mp4v);

            // dataProperty 
            dataProperty = new DataTable();
            dataProperty.Columns.Add(new DataColumn("Property", typeof(string)));
            dataProperty.Columns.Add(new DataColumn("Description", typeof(string)));
            dataProperty.Rows.Add(FilterProperty.Type, FilterProperty.Type);
            dataProperty.Rows.Add(FilterProperty.Bitrate, FilterProperty.Bitrate);
            dataProperty.Rows.Add(FilterProperty.FourCC, FilterProperty.FourCC);
            dataProperty.Rows.Add(FilterProperty.Language, FilterProperty.Language);
            dataProperty.Rows.Add(FilterProperty.Name, FilterProperty.Name);

            // dataOperator
            dataOperator = new DataTable();
            dataOperator.Columns.Add(new DataColumn("Operator", typeof(string)));
            dataOperator.Columns.Add(new DataColumn("Description", typeof(string)));
            dataOperator.Rows.Add(FilterTrackCompareOperator.Equal.ToString(), FilterTrackCompareOperator.Equal.ToString());
            dataOperator.Rows.Add(FilterTrackCompareOperator.NotEqual.ToString(), FilterTrackCompareOperator.NotEqual.ToString());

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
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, _filterToDisplay.Name);

            if (isGlobalFilter)
            {
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_GlobalFilter);
            }
            else
            {
                var assetfilter = (IStreamingAssetFilter)_filterToDisplay;
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Type, AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_AssetFilter);
                DGInfo.Rows.Add("Id", assetfilter.Id);
                DGInfo.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ParentAssetId, assetfilter.ParentAssetId);
            }
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_FirstQualityBitrate, _filterToDisplay.FirstQuality == null ? Constants.stringNull : _filterToDisplay.FirstQuality.Bitrate.ToString());
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_Timescale, _filterToDisplay.PresentationTimeRange.Timescale == null ? Constants.stringNull : _filterToDisplay.PresentationTimeRange.Timescale.ToString());
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_StartTimestamp, _filterToDisplay.PresentationTimeRange.StartTimestamp == null ? Constants.stringNull : _filterToDisplay.PresentationTimeRange.StartTimestamp.ToString());
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_EndTimestamp, _filterToDisplay.PresentationTimeRange.EndTimestamp == null ? Constants.stringNull : _filterToDisplay.PresentationTimeRange.EndTimestamp.ToString());
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_PresentationWindowDuration, _filterToDisplay.PresentationTimeRange.PresentationWindowDuration == null ? Constants.stringNull : _filterToDisplay.PresentationTimeRange.PresentationWindowDuration.ToString());
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_LiveBackoffDuration, _filterToDisplay.PresentationTimeRange.LiveBackoffDuration == null ? Constants.stringNull : _filterToDisplay.PresentationTimeRange.LiveBackoffDuration.ToString());
            DGInfo.Rows.Add(AMSExplorer.Properties.Resources.DynManifestFilter_DisplayFilterInfo_TrackCount, _filterToDisplay.Tracks.Count);
        }

        private List<ExFilterTrack> ConvertFilterTracksToInternalVar(IList<FilterTrackSelectStatement> tracks)
        // copy filter tracks to internal list used for display in grid
        {
            List<ExFilterTrack> targettracks = new List<ExFilterTrack>();
            foreach (var track in tracks)
            {
                var mytrack = new ExFilterTrack();
                var myconditions = new List<ExCondition>();
                foreach (var condition in track.PropertyConditions)
                {
                    var mycondition = new ExCondition();
                    mycondition.oper = condition.Operator.ToString();

                    if (condition.GetType() == typeof(FilterTrackTypeCondition)) // property type
                    {
                        FilterTrackTypeCondition conditionc = condition as FilterTrackTypeCondition;
                        mycondition.property = FilterProperty.Type;
                        mycondition.value = conditionc.Value.ToString();
                    }
                    else if (condition.GetType() == typeof(FilterTrackFourCCCondition)) // property FourCC 
                    {
                        FilterTrackFourCCCondition conditionc = condition as FilterTrackFourCCCondition;
                        mycondition.property = FilterProperty.FourCC;
                        mycondition.value = conditionc.Value;

                    }
                    else if (condition.GetType() == typeof(FilterTrackLanguageCondition)) // property Type - we want to propose supported FourCC
                    {
                        FilterTrackLanguageCondition conditionc = condition as FilterTrackLanguageCondition;
                        mycondition.property = FilterProperty.Language;
                        mycondition.value = conditionc.Value;
                    }
                    else if (condition.GetType() == typeof(FilterTrackBitrateRangeCondition)) // property Bitrange - we want to propose supported FourCC
                    {
                        FilterTrackBitrateRangeCondition conditionc = condition as FilterTrackBitrateRangeCondition;
                        mycondition.property = FilterProperty.Bitrate;
                        mycondition.value = ReturnFilterTrackBitrateRangeAsString(conditionc.Value);
                    }
                    else if (condition.GetType() == typeof(FilterTrackNameCondition)) // property Name - we want to propose supported FourCC
                    {
                        FilterTrackNameCondition conditionc = condition as FilterTrackNameCondition;
                        mycondition.property = FilterProperty.Name;
                        mycondition.value = conditionc.Value;
                    }

                    myconditions.Add(mycondition);
                }
                mytrack.conditions = myconditions;
                targettracks.Add(mytrack);
            }
            return targettracks;
        }


        private List<FilterTrackSelectStatement> CreateFilterTracks()
        // use internal list to create filter tracks
        {

            List<FilterTrackSelectStatement> filterTrackSelectStatements = new List<FilterTrackSelectStatement>();

            foreach (var track in filtertracks)
            {
                FilterTrackSelectStatement filterTrackSelectStatement = new FilterTrackSelectStatement();
                filterTrackSelectStatement.PropertyConditions = new List<IFilterTrackPropertyCondition>();

                foreach (var condition in track.conditions)
                {
                    FilterTrackCompareOperator oper = (FilterTrackCompareOperator)Enum.Parse(typeof(FilterTrackCompareOperator), condition.oper);

                    switch (condition.property)
                    {
                        case (FilterProperty.Bitrate):
                            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackBitrateRangeCondition(ReturnFilterTrackBitrateRangeFromString(condition.value), oper));
                            break;

                        case (FilterProperty.FourCC):
                            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackFourCCCondition(condition.value, oper));
                            break;
                        case (FilterProperty.Language):
                            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackLanguageCondition(condition.value, oper));
                            break;

                        case (FilterProperty.Name):
                            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackNameCondition(condition.value, oper));
                            break;

                        case (FilterProperty.Type):
                            var mytype = (FilterTrackType)Enum.Parse(typeof(FilterTrackType), condition.value);
                            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackTypeCondition(mytype, oper));
                            break;
                    }
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
                        var presentation = new PresentationTimeRange(
                                               string.IsNullOrWhiteSpace(textBoxRawTimescale.Text) ? null : (ulong?)ulong.Parse(textBoxRawTimescale.Text),
                                               string.IsNullOrWhiteSpace(textBoxRawStart.Text) ? null : (ulong?)ulong.Parse(textBoxRawStart.Text),
                                               string.IsNullOrWhiteSpace(textBoxRawEnd.Text) ? null : (ulong?)ulong.Parse(textBoxRawEnd.Text),
                                               string.IsNullOrWhiteSpace(textBoxRawDVR.Text) ? null : (TimeSpan?)TimeSpan.FromTicks(long.Parse(textBoxRawDVR.Text)),
                                               string.IsNullOrWhiteSpace(textBoxRawBackoff.Text) ? null : (TimeSpan?)TimeSpan.FromTicks(long.Parse(textBoxRawBackoff.Text))

                                               );
                        filterinfo.Presentationtimerange = presentation;
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
                filterinfo.Trackconditions = CreateFilterTracks();

                return filterinfo;
            }
        }

        private PresentationTimeRange GetFilterPresenTationTRDefaultMode
        {
            get
            {
                var ptr = new PresentationTimeRange(
                    start: checkBoxStartTime.Checked ? (ulong?)timeControlStart.ScaledTimeStampWithOffset : null,
                    end: checkBoxEndTime.Checked ? (ulong?)timeControlEnd.ScaledTimeStampWithOffset : null,
                    backoff: checkBoxLiveBackoff.Checked ? (TimeSpan?)TimeSpan.FromSeconds((double)numericUpDownBackoffSeconds.Value) : null,
                    pwDuration: checkBoxDVRWindow.Checked ? (TimeSpan?)timeControlDVR.TimeStampWithoutOffset : null,
                    timescale: (ulong?)_timescale
                );
                return ptr;
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
                    if (pstring == FilterProperty.Type) // property type
                    {
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyType;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else if (pstring == FilterProperty.FourCC) // property FourCC
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

        private FilterTrackBitrateRange ReturnFilterTrackBitrateRangeFromString(string s)
        {
            if (s.Contains("-")) // range
            {
                int? low = null;
                int? high = null;
                try
                {
                    low = Convert.ToInt32(s.Substring(0, s.IndexOf("-")));
                    high = Convert.ToInt32(s.Substring(s.IndexOf("-") + 1));
                }
                catch
                {

                }
                return (new FilterTrackBitrateRange(low, high));
            }
            else
            {
                int? value = null;
                try
                {
                    value = Convert.ToInt32(s);
                }
                catch
                {

                }

                return (new FilterTrackBitrateRange(value, value));
            }
        }


        private string ReturnFilterTrackBitrateRangeAsString(FilterTrackBitrateRange br)
        {
            if (br.HighBound != null && br.LowBound != null)
            {
                return string.Format("{0}-{1}", br.LowBound, br.HighBound);
            }
            else
            {
                if (br.HighBound != null)
                {
                    return br.HighBound.ToString();
                }
                else
                {
                    return br.LowBound.ToString();
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
                filtertracks[listBoxTracks.SelectedIndex].conditions.Add(new ExCondition() { oper = FilterTrackCompareOperator.Equal.ToString() });
                //_filter_tracks[listBoxTracks.SelectedIndex].PropertyConditions.Add(new FilterTrackTypeCondition(FilterTrackType.Video, FilterTrackCompareOperator.Equal));
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
                    if (condition.property == FilterProperty.Type) // property type - we want to propose audio, video or text dropbox
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterProperty.Type, condition.oper, condition.value);
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyType;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        cellValue.Value = condition.value;
                        dataGridViewTracks[2, index] = cellValue;
                    }
                    else if (condition.property == FilterProperty.FourCC) // property FourCC - we want to propose supported FourCC
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterProperty.FourCC, condition.oper, condition.value);
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyFourCC;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        cellValue.Value = condition.value;
                        dataGridViewTracks[2, index] = cellValue;
                    }
                    else if (condition.property == FilterProperty.Language) // property language
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterProperty.Language, condition.oper, condition.value);
                    }
                    else if (condition.property == FilterProperty.Bitrate) // property bitrate
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterProperty.Bitrate, condition.oper, condition.value);
                    }
                    else if (condition.property == FilterProperty.Name) // property Name - we want to propose supported FourCC
                    {
                        int index = dataGridViewTracks.Rows.Add(FilterProperty.Name, condition.oper, condition.value);
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
                //_filter_tracks[listBoxTracks.SelectedIndex].PropertyConditions.RemoveAt(dataGridViewTracks.SelectedRows[0].Index);
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
            // _filter.PresentationTimeRange = new IFilterPresentationTimeRange() { LiveBackoffDuration = string.Empty, StartTimestamp = string.Empty, PresentationWindowDuration = string.Empty, EndTimestamp = "300000000", Timescale = "10000000" };

            List<FilterTrackSelectStatement> filterTrackSelectStatements = new List<FilterTrackSelectStatement>();
            FilterTrackSelectStatement filterTrackSelectStatement = new FilterTrackSelectStatement();

            filterTrackSelectStatement.PropertyConditions = new List<IFilterTrackPropertyCondition>();
            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackTypeCondition(FilterTrackType.Video, FilterTrackCompareOperator.Equal));
            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackBitrateRangeCondition(new FilterTrackBitrateRange(0, 1048576), FilterTrackCompareOperator.Equal));
            filterTrackSelectStatements.Add(filterTrackSelectStatement);

            filterTrackSelectStatement = new FilterTrackSelectStatement();
            filterTrackSelectStatement.PropertyConditions = new List<IFilterTrackPropertyCondition>();
            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackTypeCondition(FilterTrackType.Audio, FilterTrackCompareOperator.Equal));
            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackFourCCCondition(FilterPropertyFourCCValue.mp4a, FilterTrackCompareOperator.Equal));
            filterTrackSelectStatements.Add(filterTrackSelectStatement);

            filterTrackSelectStatement = new FilterTrackSelectStatement();
            filterTrackSelectStatement.PropertyConditions = new List<IFilterTrackPropertyCondition>();
            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackTypeCondition(FilterTrackType.Text, FilterTrackCompareOperator.Equal));
            filterTrackSelectStatement.PropertyConditions.Add(new FilterTrackLanguageCondition("en", FilterTrackCompareOperator.Equal));
            filterTrackSelectStatements.Add(filterTrackSelectStatement);

            filtertracks = ConvertFilterTracksToInternalVar(filterTrackSelectStatements);

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
            if (checkBoxStartTime.Checked && isGlobalFilter)
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
            if (checkBoxEndTime.Checked && isGlobalFilter)
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
            if (checkBoxDVRWindow.Checked && timeControlDVR.TimeStampWithoutOffset < TimeSpan.FromMinutes(2))
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
            timeControlDVR.Enabled = checkBoxDVRWindow.Checked;
            labelDefaultDVR.Text = checkBoxDVRWindow.Checked ? string.Empty : _labelDefaultDVR;
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
                textBoxRawTimescale.Text = ptr.Timescale == null ? string.Empty : ptr.Timescale.ToString();
                textBoxRawStart.Text = ptr.StartTimestamp == null ? string.Empty : ptr.StartTimestamp.ToString();
                textBoxRawEnd.Text = ptr.EndTimestamp == null ? string.Empty : ptr.EndTimestamp.ToString();
                textBoxRawDVR.Text = ptr.PresentationWindowDuration == null ? string.Empty : ((TimeSpan)ptr.PresentationWindowDuration).Ticks.ToString();
                textBoxRawBackoff.Text = ptr.LiveBackoffDuration == null ? string.Empty : ((TimeSpan)ptr.LiveBackoffDuration).Ticks.ToString();
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

        private void comboBoxLocatorsFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtername = ((Item)comboBoxLocatorsFilters.SelectedItem).Value;
            if (filtername != null)
            {
                IStreamingFilter importfilter = null;
                if (filtername.StartsWith(Constants.AssetIdPrefix)) // asset filter
                {
                    importfilter = _parentAsset.AssetFilters.Where(f => f.Id == filtername).FirstOrDefault();

                }
                else // global filter
                {
                    importfilter = _context.Filters.Where(f => f.Name == filtername).FirstOrDefault();
                }
                if (importfilter != null)
                {
                    filtertracks = ConvertFilterTracksToInternalVar(importfilter.Tracks);
                    RefreshTracks();
                    RefreshTracksConditions();
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
