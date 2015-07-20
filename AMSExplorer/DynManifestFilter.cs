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



namespace AMSExplorer
{
    public partial class DynManifestFilter : Form
    {
        private string _filter_name;
        private PresentationTimeRange _filter_presentationtimerange;
        private IList<FilterTrackSelectStatement> _filter_tracks;

        private bool isGlobalFilter = true;
        private bool newfilter = true;
        CloudMediaContext _context;
        private DataTable dataPropertyType;
        private DataTable dataPropertyFourCC;
        private DataTable dataProperty;
        private DataTable dataOperator;
        private IAsset _parentAsset = null;
        private ManifestTimingData _parentassetmanifestdata;
        private ulong _timescale = TimeSpan.TicksPerSecond;
        private SubClipConfiguration _subclipconfig;
        private IStreamingFilter _filterToDisplay;

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

            comboBoxLocatorsFilters.Items.Add(new Item("Import track filtering from :", null));

            if (asset != null)
            {
                asset.AssetFilters.Where(g => g.Tracks.Count > 0).ToList().ForEach(g => comboBoxLocatorsFilters.Items.Add(new Item("Asset filter : " + g.Name, g.Id)));
            }
            // global filters
            _context.Filters.Where(g => g.Tracks.Count > 0).ToList().ForEach(g => comboBoxLocatorsFilters.Items.Add(new Item("Global filter : " + g.Name, g.Name)));
            if (comboBoxLocatorsFilters.Items.Count > 1)
            {
                comboBoxLocatorsFilters.Enabled = true;
            }
            comboBoxLocatorsFilters.SelectedIndex = 0;
            comboBoxLocatorsFilters.EndUpdate();
        }

        private void DynManifestFilter_Load(object sender, EventArgs e)
        {
            _parentassetmanifestdata = new ManifestTimingData();
            tabControl1.TabPages.Remove(tabPageTRRaw);
            FillComboBoxImportFilters(_parentAsset);

            /////////////////////////////////////////////
            // New Global Filter
            /////////////////////////////////////////////
            if (_filterToDisplay == null && _parentAsset == null)
            {
                newfilter = true;
                isGlobalFilter = true;
                _filter_presentationtimerange = new PresentationTimeRange();
                _filter_tracks = new List<FilterTrackSelectStatement>();
                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = (ulong)_filter_presentationtimerange.Timescale;
                textBoxFilterTimeScale.Text = _filter_presentationtimerange.Timescale.ToString();
            }


            /////////////////////////////////////////////
            // Existing Global Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay != null && _parentAsset == null)
            {
                newfilter = false;
                isGlobalFilter = true;
                _filter_name = _filterToDisplay.Name;
                _filter_presentationtimerange = _filterToDisplay.PresentationTimeRange;
                _filter_tracks = _filterToDisplay.Tracks;

                _timescale = (ulong)_filterToDisplay.PresentationTimeRange.Timescale;
                timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = _timescale;
                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                toolTip1.SetToolTip(this.buttonOk, "It can take up to 2 minutes for streaming endpoint to refresh the rules");

                textBoxFilterName.Enabled = false; // no way to change the filter name
                textBoxFilterName.Text = _filter_name;
                textBoxFilterTimeScale.Text = _filter_presentationtimerange.Timescale.ToString();

                timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;

                checkBoxStartTime.Checked = _filter_presentationtimerange.StartTimestamp != null;
                checkBoxEndTime.Checked = _filter_presentationtimerange.EndTimestamp != null;
                checkBoxDVRWindow.Checked = _filter_presentationtimerange.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = _filter_presentationtimerange.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(_filter_presentationtimerange.Timescale);
                timeControlEnd.SetScaledTimeStamp((_filter_presentationtimerange.EndTimestamp == null) ? 0 : _filter_presentationtimerange.EndTimestamp);  // we don't want to pass the max value to the control (overflow)
                timeControlDVR.SetTimeStamp(_filter_presentationtimerange.PresentationWindowDuration ?? new TimeSpan(0));  // we don't want to pass the max value to the control (overflow)
                TimeSpan backoff = _filter_presentationtimerange.LiveBackoffDuration ?? new TimeSpan(0);
                numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);
            }


            /////////////////////////////////////////////
            // New Asset Filter
            /////////////////////////////////////////////
            else if (_filterToDisplay == null && _parentAsset != null)
            {
                newfilter = true;
                isGlobalFilter = false;
                _filter_presentationtimerange = new PresentationTimeRange();
                _filter_tracks = new List<FilterTrackSelectStatement>();
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

                    TimeSpan duration = new TimeSpan(AssetInfo.ReturnTimestampInTicks((long)_parentassetmanifestdata.AssetDuration, (long)_parentassetmanifestdata.TimeScale));
                    textBoxAssetDuration.Text = duration.ToString(@"d\.hh\:mm\:ss");
                    labelassetduration.Visible = textBoxAssetDuration.Visible = true;
                    textBoxFilterName.Text = "filter" + new Random().Next(9999).ToString();

                    if (!_parentassetmanifestdata.IsLive)  // Not a live content
                    {
                        // let set duration and active track bat
                        timeControlStart.ScaledTotalDuration = timeControlEnd.ScaledTotalDuration = timeControlDVR.ScaledTotalDuration = (ulong)_parentassetmanifestdata.AssetDuration;

                        timeControlStart.Max = timeControlEnd.Max = timeControlDVR.Max = duration;
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

                _filter_name = _filterToDisplay.Name;
                _filter_presentationtimerange = _filterToDisplay.PresentationTimeRange;
                _filter_tracks = _filterToDisplay.Tracks;

                _timescale = (ulong)_filterToDisplay.PresentationTimeRange.Timescale;


                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                toolTip1.SetToolTip(this.buttonOk, "It can take up to 2 minutes for streaming endpoint to refresh the rules");

                labelFilterTitle.Text = "Asset Filter";
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                textBoxAssetName.Text = _parentAsset != null ? _parentAsset.Name : string.Empty;

                textBoxFilterName.Enabled = false; // no way to change the filter name
                textBoxFilterName.Text = _filter_name;

                // let's try to read asset timing
                _parentassetmanifestdata = AssetInfo.GetManifestTimingData(_parentAsset);

                _timescale = timeControlStart.TimeScale = timeControlEnd.TimeScale = timeControlDVR.TimeScale = (ulong)_filterToDisplay.PresentationTimeRange.Timescale;

                if (!_parentassetmanifestdata.Error && _timescale == _parentassetmanifestdata.TimeScale)  // we were able to read asset timings and timescale between manifest and existing asset match
                {
                    // let's disable trackbars if this is live (duration is not fixed)
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = !_parentassetmanifestdata.IsLive;

                    timeControlStart.ScaledFirstTimestampOffset = timeControlEnd.ScaledFirstTimestampOffset = _parentassetmanifestdata.TimestampOffset;

                    textBoxOffset.Text = _parentassetmanifestdata.TimestampOffset.ToString();
                    labelOffset.Visible = textBoxOffset.Visible = true;

                    TimeSpan duration = new TimeSpan(AssetInfo.ReturnTimestampInTicks((long)_parentassetmanifestdata.AssetDuration, (long)_parentassetmanifestdata.TimeScale));
                    textBoxAssetDuration.Text = duration.ToString(@"d\.hh\:mm\:ss");
                    labelassetduration.Visible = textBoxAssetDuration.Visible = true;

                    if (!_parentassetmanifestdata.IsLive)
                    {
                        timeControlStart.Max = timeControlEnd.Max = timeControlDVR.Max = duration;
                        // let set duration and active track bat
                        timeControlStart.ScaledTotalDuration = timeControlEnd.ScaledTotalDuration = timeControlDVR.ScaledTotalDuration = _parentassetmanifestdata.AssetDuration;
                    }
                    else
                    {
                        textBoxAssetDuration.Text += " (LIVE)";
                    }
                }

                else // not able to read asset timings or mismatch in timescale
                {
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = false;
                    timeControlStart.Max = timeControlEnd.Max = timeControlDVR.Max = TimeSpan.MaxValue;
                    timeControlEnd.SetTimeStamp(timeControlEnd.Max);
                    labelassetduration.Visible = textBoxAssetDuration.Visible = false;
                }


                checkBoxStartTime.Checked = _filter_presentationtimerange.StartTimestamp != null;
                checkBoxEndTime.Checked = _filter_presentationtimerange.EndTimestamp != null;
                checkBoxDVRWindow.Checked = _filter_presentationtimerange.PresentationWindowDuration != null;
                checkBoxLiveBackoff.Checked = _filter_presentationtimerange.LiveBackoffDuration != null;

                timeControlStart.SetScaledTimeStamp(_filter_presentationtimerange.Timescale);
                timeControlEnd.SetScaledTimeStamp((_filter_presentationtimerange.EndTimestamp == null) ? 0 : _filter_presentationtimerange.EndTimestamp);  // we don't want to pass the max value to the control (overflow)
                timeControlDVR.SetTimeStamp(_filter_presentationtimerange.PresentationWindowDuration ?? new TimeSpan(0));  // we don't want to pass the max value to the control (overflow)
                TimeSpan backoff = _filter_presentationtimerange.LiveBackoffDuration ?? new TimeSpan(0);
                numericUpDownBackoffSeconds.Value = Convert.ToDecimal(backoff.TotalSeconds);



            }

            // Common code
            textBoxFilterTimeScale.Text = _timescale.ToString();

            // dataPropertyType
            dataPropertyType = new DataTable();
            dataPropertyType.Columns.Add(new DataColumn("Value", typeof(string)));
            dataPropertyType.Columns.Add(new DataColumn("Description", typeof(string)));

            dataPropertyType.Rows.Add(FilterPropertyTypeValue.audio, FilterPropertyTypeValue.audio);
            dataPropertyType.Rows.Add(FilterPropertyTypeValue.video, FilterPropertyTypeValue.video);
            dataPropertyType.Rows.Add(FilterPropertyTypeValue.text, FilterPropertyTypeValue.text);

            // FilterPropertyFourCCValue

            dataPropertyFourCC = new DataTable();
            dataPropertyFourCC.Columns.Add(new DataColumn("Value", typeof(string)));
            dataPropertyFourCC.Columns.Add(new DataColumn("Description", typeof(string)));

            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.avc1, FilterPropertyFourCCValue.avc1);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.ec3, FilterPropertyFourCCValue.ec3);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.mp4a, FilterPropertyFourCCValue.mp4a);
            dataPropertyFourCC.Rows.Add(FilterPropertyFourCCValue.mp4v, FilterPropertyFourCCValue.mp4v);

            // dataProperty dataOperator
            dataProperty = new DataTable();
            dataOperator = new DataTable();

            dataProperty.Columns.Add(new DataColumn("Property", typeof(string)));
            dataProperty.Columns.Add(new DataColumn("Description", typeof(string)));

            dataOperator.Columns.Add(new DataColumn("Operator", typeof(string)));
            dataOperator.Columns.Add(new DataColumn("Description", typeof(string)));

            dataProperty.Rows.Add(FilterProperty.Type, FilterProperty.Type);
            dataProperty.Rows.Add(FilterProperty.Bitrate, FilterProperty.Bitrate);
            dataProperty.Rows.Add(FilterProperty.FourCC, FilterProperty.FourCC);
            dataProperty.Rows.Add(FilterProperty.Language, FilterProperty.Language);
            dataProperty.Rows.Add(FilterProperty.Name, FilterProperty.Name);

            dataOperator.Rows.Add(FilterTrackCompareOperator.Equal, FilterTrackCompareOperator.Equal);
            dataOperator.Rows.Add(FilterTrackCompareOperator.NotEqual, FilterTrackCompareOperator.NotEqual);

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
        }

        private void RefreshTracks()
        {
            listBoxTracks.Items.Clear();
            dataGridViewTracks.Rows.Clear();

            int i = 1;
            foreach (var track in _filter_tracks)
            {
                listBoxTracks.Items.Add("Rule" + i);
                i++;
            }
            if (listBoxTracks.SelectedIndex == -1 && listBoxTracks.Items.Count > 0)
            {
                listBoxTracks.SelectedIndex = 0;
            }
        }

        public AssetCreationInfo GetFilterInfo
        {
            get
            {
                AssetCreationInfo filterinfo = new AssetCreationInfo();
                filterinfo.Name = newfilter ? textBoxFilterName.Text : _filter_name;

                if (checkBoxRawMode.Checked) // RAW Mode
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
                else  // Default mode
                {
                    filterinfo.Presentationtimerange = GetFilterPresenTationTRDefaultMode;
                }


                // to make sure it is null to avoid puting data in JSON
                filterinfo.Trackconditions = (_filter_tracks.Count == 0) ? null : _filter_tracks;

                return filterinfo;
            }
        }

        private PresentationTimeRange GetFilterPresenTationTRDefaultMode
        {
            get
            {
                var ptr = new PresentationTimeRange(
                    start: checkBoxStartTime.Checked ? (ulong?)timeControlStart.GetScaledTimeStampWithOffset() : null,
                    end: checkBoxEndTime.Checked ? (ulong?)timeControlEnd.GetScaledTimeStampWithOffset() : null,
                    backoff: checkBoxLiveBackoff.Checked ? (TimeSpan?)TimeSpan.FromSeconds((double)numericUpDownBackoffSeconds.Value) : null,
                    pwDuration: checkBoxDVRWindow.Checked ? (TimeSpan?)timeControlDVR.GetTimeStampAsTimeSpanWitoutOffset() : null,
                    timescale: (ulong?)_timescale
                );
                return ptr;
            }
        }

        private bool IsMax(string timestamp)
        {
            if (string.IsNullOrWhiteSpace(timestamp))
            {
                return false;
            }
            else
            {
                return Int64.MaxValue == Int64.Parse(timestamp);
            }
        }

        private bool IsMin(string timestamp)
        {
            if (string.IsNullOrWhiteSpace(timestamp))
            {
                return false;
            }
            else
            {
                return 0 == Int64.Parse(timestamp);
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
                    if (dataGridViewTracks.CurrentCell.Value.ToString() == FilterProperty.Type) // property type
                    {
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyType;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        dataGridViewTracks[2, dataGridViewTracks.CurrentCell.RowIndex] = cellValue;
                        dataGridViewTracks.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else if (dataGridViewTracks.CurrentCell.Value.ToString() == FilterProperty.FourCC) // property FourCC
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


                // let's update the filter
                switch (dataGridViewTracks.CurrentCell.ColumnIndex)
                {
                    case 0: // property
                        _filter_tracks[listBoxTracks.SelectedIndex].PropertyConditions[dataGridViewTracks.CurrentCell.RowIndex].Property = dataGridViewTracks.CurrentCell.Value.ToString();

                        break;

                    case 1: // operator
                        _filter_tracks[listBoxTracks.SelectedIndex].PropertyConditions[dataGridViewTracks.CurrentCell.RowIndex].Operator = Enum.Parse(typeof(FilterTrackCompareOperator), dataGridViewTracks.CurrentCell.Value.ToString());

                        break;

                    case 2: // value
                        _filter_tracks[listBoxTracks.SelectedIndex].PropertyConditions[dataGridViewTracks.CurrentCell.RowIndex].Value = dataGridViewTracks.CurrentCell.Value.ToString();

                        break;
                }
            }
        }

        private void buttonAddTrack_Click(object sender, EventArgs e)
        {
            var asset = _context.Assets.FirstOrDefault();
            var filter = asset.AssetFilters.FirstOrDefault();
            var statement = filter.Tracks.FirstOrDefault();
            statement.PropertyConditions.FirstOrDefault().
            _filter_tracks.Add(new FilterTrackSelectStatement() { PropertyConditions = new List<IFilterTrackPropertyCondition>() });
            RefreshTracks();
        }

        private void buttonDeleteTrack_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1)
            {
                dataGridViewTracks.Rows.Clear();
                _filter_tracks.RemoveAt(listBoxTracks.SelectedIndex);
                RefreshTracks();
            }
        }

        private void buttonAddCondition_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1)
            {
                _filter_tracks[listBoxTracks.SelectedIndex].PropertyConditions.Add(new IFilterTrackPropertyCondition());
                RefreshTracksConditions();
            }
        }

        private void RefreshTracksConditions()
        {
            dataGridViewTracks.Rows.Clear();
            if (listBoxTracks.SelectedIndex > -1)
            {
                var track = _filter_tracks[listBoxTracks.SelectedIndex];
                foreach (var condition in track.PropertyConditions)
                {
                    int index = dataGridViewTracks.Rows.Add(condition.Property, condition.Operator, condition.Value);
                    if (condition.Property == FilterProperty.Type) // property type - we want to propose audio, video or text dropbox
                    {
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyType;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        cellValue.Value = condition.Value;
                        dataGridViewTracks[2, index] = cellValue;
                    }
                    else if (condition.Property == FilterProperty.FourCC) // property FourCC - we want to propose supported FourCC
                    {
                        var cellValue = new DataGridViewComboBoxCell();
                        cellValue.DataSource = dataPropertyFourCC;
                        cellValue.ValueMember = "Value";
                        cellValue.DisplayMember = "Description";
                        cellValue.Value = condition.Value;
                        dataGridViewTracks[2, index] = cellValue;
                    }
                }
            }
        }

        private void buttonDeleteCondition_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1 && dataGridViewTracks.SelectedRows.Count > 0)
            {
                _filter_tracks[listBoxTracks.SelectedIndex].PropertyConditions.RemoveAt(dataGridViewTracks.SelectedRows[0].Index);
                RefreshTracksConditions();
            }
        }

        private void textBoxFilterName_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (string.IsNullOrEmpty(tb.Text))
            {
                errorProvider1.SetError(tb, "Please specify a filter name");
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
            var conditions = new List<IFilterTrackPropertyCondition>();
            conditions.Add(new IFilterTrackPropertyCondition() { Operator = FilterTrackCompareOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.video });
            conditions.Add(new IFilterTrackPropertyCondition() { Operator = FilterTrackCompareOperator.Equal, Property = FilterProperty.Bitrate, Value = "0-1048576" });
            var tracks = new List<FilterTrackSelectStatement>() { new FilterTrackSelectStatement() { PropertyConditions = conditions } };

            conditions = new List<IFilterTrackPropertyCondition>();
            conditions.Add(new IFilterTrackPropertyCondition() { Operator = FilterTrackCompareOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.audio });
            conditions.Add(new IFilterTrackPropertyCondition() { Operator = FilterTrackCompareOperator.Equal, Property = FilterProperty.FourCC, Value = "mp4a" });
            tracks.Add(new FilterTrackSelectStatement() { PropertyConditions = conditions });

            conditions = new List<IFilterTrackPropertyCondition>();
            conditions.Add(new IFilterTrackPropertyCondition() { Operator = FilterTrackCompareOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.text });
            conditions.Add(new IFilterTrackPropertyCondition() { Operator = FilterTrackCompareOperator.Equal, Property = FilterProperty.Language, Value = "en" });
            tracks.Add(new FilterTrackSelectStatement() { PropertyConditions = conditions });

            _filter_tracks = tracks;

            //RefreshPresentationTimes();
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
                errorProvider1.SetError(timeControlStart, "It is not recommended to use a Global Filter to do time trimming. Consider creating an asset filter instead.");
            }
            else if (checkBoxStartTime.Checked && checkBoxEndTime.Checked && timeControlStart.GetTimeStampAsTimeSpanWitoutOffset() > timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset())
            {
                errorProvider1.SetError(timeControlStart, "Start time must be lower than end time");
            }
            else
            {
                errorProvider1.SetError(timeControlStart, String.Empty);
            }




            // time end control
            if (checkBoxEndTime.Checked && isGlobalFilter)
            {
                errorProvider1.SetError(timeControlEnd, "It is not recommended to use a Global Filter to do time trimming. Consider creating an asset filter instead.");
            }
            else if (checkBoxEndTime.Checked && checkBoxStartTime.Checked && timeControlEnd.GetTimeStampAsTimeSpanWitoutOffset() < timeControlStart.GetTimeStampAsTimeSpanWitoutOffset())
            {
                errorProvider1.SetError(timeControlEnd, "End time must be higher than start time");
            }
            else
            {
                errorProvider1.SetError(timeControlEnd, String.Empty);
            }


            // dvr
            if (checkBoxDVRWindow.Checked && timeControlDVR.GetTimeStampAsTimeSpanWitoutOffset() < TimeSpan.FromMinutes(2))
            {
                errorProvider1.SetError(timeControlDVR, "The DVR Window must be at least 2 minutes (or more)");
            }
            else
            {
                errorProvider1.SetError(timeControlDVR, String.Empty);
            }

        }



        private void timeControlEnd_ValueChanged(object sender, EventArgs e)
        {
            CheckIfErrorTimeControls();
        }



        private void checkBoxStartTime_CheckedChanged(object sender, EventArgs e)
        {
            timeControlStart.Enabled = checkBoxStartTime.Checked;
            labelStartTimeDefault.Text = checkBoxStartTime.Checked ? string.Empty : (string)labelStartTimeDefault.Tag;
            CheckIfErrorTimeControls();

        }

        private void checkBoxEndTime_CheckedChanged(object sender, EventArgs e)
        {
            timeControlEnd.Enabled = checkBoxEndTime.Checked;
            labelDefaultEnd.Text = checkBoxEndTime.Checked ? string.Empty : (string)labelDefaultEnd.Tag;
            CheckIfErrorTimeControls();
        }

        private void checkBoxLiveBackoff_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownBackoffSeconds.Enabled = checkBoxLiveBackoff.Checked;
            labelDefaultBakckoff.Text = checkBoxLiveBackoff.Checked ? string.Empty : (string)labelDefaultBakckoff.Tag;
            CheckIfErrorTimeControls();
        }

        private void checkBoxDVRWindow_CheckedChanged(object sender, EventArgs e)
        {
            timeControlDVR.Enabled = checkBoxDVRWindow.Checked;
            labelDefaultDVR.Text = checkBoxDVRWindow.Checked ? string.Empty : (string)labelDefaultDVR.Tag;
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
                    _filter_tracks = importfilter.Tracks;
                    RefreshTracks();
                    RefreshTracksConditions();
                }
            }
        }
    }
}
