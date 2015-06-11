//----------------------------------------------------------------------- 
// <copyright file="DynManifestFilter.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.2
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

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
        private Filter _filter;
        private bool isGlobalFilter = true;
        private bool newfilter = true;
        private MediaServiceContextForDynManifest _contextdynman;
        CloudMediaContext _context;
        private DataTable dataPropertyType;
        private DataTable dataPropertyFourCC;
        private DataTable dataProperty;
        private DataTable dataOperator;
        private IAsset _parentAsset = null;
        private long _parentAssetDuration = -1;
        private long _parentAssetDurationInTicks = -1;
        private long _parentAssetTimeScale = -1;
        private long _timescale = Int64.MaxValue;

        public DynManifestFilter(MediaServiceContextForDynManifest contextdynman, CloudMediaContext context, IAsset parentAsset = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _contextdynman = contextdynman;
            _context = context;

            _filter = new Filter();
            _filter.SetContext(_contextdynman);
            _filter.PresentationTimeRange = new IFilterPresentationTimeRange();
            _filter.Tracks = new List<IFilterTrackSelect>();

            if (parentAsset != null)
            {
                isGlobalFilter = false;
                _parentAsset = parentAsset;
            }
        }

        private ILocator GetOnDemandLocator()
        {
            ILocator tempLocator = null;
            try
            {
                var locatorTask = Task.Factory.StartNew(() =>
                {
                    tempLocator = _context.Locators.Create(LocatorType.OnDemandOrigin, _parentAsset, AccessPermissions.Read, TimeSpan.FromHours(1));

                });
                locatorTask.Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when creating the temporary on-demand locator." + ex.Message);
            }

            return tempLocator;
        }


        private void DynManifestFilter_Load(object sender, EventArgs e)
        {


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

            dataOperator.Rows.Add(IOperator.Equal, IOperator.Equal);
            dataOperator.Rows.Add(IOperator.notEqual, IOperator.notEqual);

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
        }

        private void RefreshTracks()
        {
            listBoxTracks.Items.Clear();
            dataGridViewTracks.Rows.Clear();

            int i = 1;
            foreach (var track in _filter.Tracks)
            {
                listBoxTracks.Items.Add("Rule" + i);
                i++;
            }
        }

        public Filter DisplayedFilter
        {
            get
            {
                _filter.Name = textBoxFilterName.Text;
                _filter.PresentationTimeRange.StartTimestamp = checkBoxStartTime.Checked ? timeControlStart.Timestamp : null;
                _filter.PresentationTimeRange.EndTimestamp = checkBoxEndTime.Checked ? timeControlEnd.Timestamp : null;
                _filter.PresentationTimeRange.LiveBackoffDuration = checkBoxLiveBackoff.Checked ? ((long)((double)numericUpDownBackoffSeconds.Value * (double)_timescale)).ToString() : null;
                _filter.PresentationTimeRange.PresentationWindowDuration = checkBoxDVRWindow.Checked ? timeControlDVR.Timestamp : null;
                _filter.PresentationTimeRange.Timescale = _timescale.ToString();
                // _filter.PresentationTimeRange.StartTimestamp = string.IsNullOrEmpty(textBoxStartTimestamp.Text.Trim()) ? null : textBoxStartTimestamp.Text;
                // _filter.PresentationTimeRange.EndTimestamp = string.IsNullOrEmpty(textBoxEndTimestamp.Text.Trim()) ? null : textBoxEndTimestamp.Text;
                // _filter.PresentationTimeRange.LiveBackoffDuration = string.IsNullOrEmpty(textBoxLiveBackoffDuration.Text.Trim()) ? null : textBoxLiveBackoffDuration.Text;
                // _filter.PresentationTimeRange.PresentationWindowDuration = string.IsNullOrEmpty(textBoxPresentationWindowDuration.Text.Trim()) ? null : textBoxPresentationWindowDuration.Text;
                //_filter.PresentationTimeRange.Timescale = string.IsNullOrEmpty(textBoxTimeScale.Text.Trim()) ? null : textBoxTimeScale.Text;

                if (_filter.Tracks.Count == 0) _filter.Tracks = null; // to make sure it is null to avoid puting data in JSON

                return _filter;
            }
            set
            {
                // goal is to display an existing filter
                newfilter = false;
                _filter = value;
                _timescale = long.Parse(_filter.PresentationTimeRange.Timescale);
                buttonOk.Text = "Update Filter";
                buttonOk.Enabled = true; // we can enable the button
                textBoxFilterName.Enabled = false; // no way to change the filter name

                checkBoxStartTime.Checked = checkBoxEndTime.Checked = checkBoxLiveBackoff.Checked = checkBoxDVRWindow.Checked = true;
                timeControlStart.TimeScale = _timescale;
                timeControlStart.Timestamp = _filter.PresentationTimeRange.StartTimestamp;
                timeControlEnd.TimeScale = _timescale;
                timeControlEnd.Timestamp = _filter.PresentationTimeRange.EndTimestamp;
                timeControlDVR.TimeScale = _timescale;
                timeControlDVR.Timestamp = _filter.PresentationTimeRange.PresentationWindowDuration;
                numericUpDownBackoffSeconds.Value = (decimal)((double)(long.Parse(_filter.PresentationTimeRange.LiveBackoffDuration)) / (double)_timescale);

                if (value.GetType() == typeof(AssetFilter))
                {
                    IAsset parentasset = AssetInfo.GetAsset(((AssetFilter)value).ParentAssetId, _context);
                    string parentname = parentasset != null ? parentasset.Name : string.Empty;
                    textBoxAssetName.Text = parentasset.Name;
                }
            }
        }

        public string CreateAssetFilterFromAssetName
        {
            set
            {
                labelFilterTitle.Text = string.Format("Asset Filter");
                textBoxAssetName.Text = value;
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                isGlobalFilter = false;
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
                        _filter.Tracks[listBoxTracks.SelectedIndex].PropertyConditions[dataGridViewTracks.CurrentCell.RowIndex].Property = dataGridViewTracks.CurrentCell.Value.ToString();

                        break;

                    case 1: // operator
                        _filter.Tracks[listBoxTracks.SelectedIndex].PropertyConditions[dataGridViewTracks.CurrentCell.RowIndex].Operator = dataGridViewTracks.CurrentCell.Value.ToString();

                        break;

                    case 2: // value
                        _filter.Tracks[listBoxTracks.SelectedIndex].PropertyConditions[dataGridViewTracks.CurrentCell.RowIndex].Value = dataGridViewTracks.CurrentCell.Value.ToString();

                        break;
                }
            }
        }

        private void buttonAddTrack_Click(object sender, EventArgs e)
        {
            _filter.Tracks.Add(new IFilterTrackSelect() { PropertyConditions = new List<FilterTrackPropertyCondition>() });
            RefreshTracks();
        }

        private void buttonDeleteTrack_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1)
            {
                dataGridViewTracks.Rows.Clear();
                _filter.Tracks.RemoveAt(listBoxTracks.SelectedIndex);
                RefreshTracks();
            }
        }

        private void buttonAddCondition_Click(object sender, EventArgs e)
        {
            if (listBoxTracks.SelectedIndex > -1)
            {
                _filter.Tracks[listBoxTracks.SelectedIndex].PropertyConditions.Add(new FilterTrackPropertyCondition());
                RefreshTracksConditions();
            }
        }

        private void RefreshTracksConditions()
        {
            dataGridViewTracks.Rows.Clear();
            if (listBoxTracks.SelectedIndex > -1)
            {
                var track = _filter.Tracks[listBoxTracks.SelectedIndex];
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
                _filter.Tracks[listBoxTracks.SelectedIndex].PropertyConditions.RemoveAt(dataGridViewTracks.SelectedRows[0].Index);
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
            _filter.PresentationTimeRange = new IFilterPresentationTimeRange() { LiveBackoffDuration = string.Empty, StartTimestamp = string.Empty, PresentationWindowDuration = string.Empty, EndTimestamp = "300000000", Timescale = "10000000" };
            var conditions = new List<FilterTrackPropertyCondition>();
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.video });
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Bitrate, Value = "0-1048576" });
            var tracks = new List<IFilterTrackSelect>() { new IFilterTrackSelect() { PropertyConditions = conditions } };

            conditions = new List<FilterTrackPropertyCondition>();
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.audio });
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.FourCC, Value = "mp4a" });
            tracks.Add(new IFilterTrackSelect() { PropertyConditions = conditions });

            conditions = new List<FilterTrackPropertyCondition>();
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.text });
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Language, Value = "en" });
            tracks.Add(new IFilterTrackSelect() { PropertyConditions = conditions });

            _filter.Tracks = tracks;
            _filter.Name = textBoxFilterName.Text;

            RefreshPresentationTimes();
            RefreshTracks();
            RefreshTracksConditions();

        }

        private void RefreshPresentationTimes()
        {
            textBoxFilterName.Text = _filter.Name;
            if (_filter.PresentationTimeRange != null)
            {
                /*
                textBoxStartTimestamp.Text = _filter.PresentationTimeRange.StartTimestamp;
                textBoxEndTimestamp.Text = _filter.PresentationTimeRange.EndTimestamp;
                textBoxLiveBackoffDuration.Text = _filter.PresentationTimeRange.LiveBackoffDuration;
                textBoxPresentationWindowDuration.Text = _filter.PresentationTimeRange.PresentationWindowDuration;
                textBoxTimeScale.Text = _filter.PresentationTimeRange.Timescale;
                 */
            }
            else
            {
                /*
                textBoxStartTimestamp.Text = string.Empty;
                textBoxEndTimestamp.Text = string.Empty;
                textBoxLiveBackoffDuration.Text = string.Empty;
                textBoxPresentationWindowDuration.Text = string.Empty;
                textBoxTimeScale.Text = string.Empty;
                */
            }

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            /*
            double scale = 1;
            try
            {
                scale = Convert.ToDouble(textBoxTimeScale.Text) / 10000000;
            }
            catch
            {

            }

            long start = 0;
            try
            {
                start = Convert.ToInt64(textBoxStartTimestamp.Text);
            }
            catch
            {

            }

            long end = long.MaxValue;
            try
            {
                end = Convert.ToInt64(textBoxEndTimestamp.Text);
            }
            catch
            {

            }

            long dvr = long.MaxValue;
            try
            {
                dvr = Convert.ToInt64(textBoxPresentationWindowDuration.Text);
            }
            catch
            {

            }

            long live = 0;
            try
            {
                live = Convert.ToInt64(textBoxLiveBackoffDuration.Text);
            }
            catch
            {

            }
            textBoxLabelStart.Text = (start == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(start / scale)).ToString(@"d\.hh\:mm\:ss");
            textBoxLabelEnd.Text = (end == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(end / scale)).ToString(@"d\.hh\:mm\:ss");
            textBoxLabelDVR.Text = (dvr == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(dvr / scale)).ToString(@"d\.hh\:mm\:ss");
            textBoxLabelLiveBackoff.Text = (live == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(live / scale)).ToString(@"d\.hh\:mm\:ss");
             * */
        }

        private void moreinfoprofilelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }


        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);

            var rectangle = e.CellBounds;
            rectangle.Inflate(-1, -1);

            //ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.Flat, Border3DSide.All); // 3D border
            ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted); // 
        }

        private void textBoxFilterName_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = !string.IsNullOrWhiteSpace(textBoxFilterName.Text);
        }

        /*
        private void textBoxTimestamp_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (isGlobalFilter && !string.IsNullOrWhiteSpace(textBoxStartTimestamp.Text))
            {
                errorProvider1.SetError(tb, "It is not recommended to use a Global Filter to do time trimming. Consider creating an asset filter instead.");
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }
         * 
         * */






        private void CheckIfErrorTimeControls()
        {

            // time start control
            if (checkBoxStartTime.Checked && isGlobalFilter)
            {
                errorProvider1.SetError(timeControlStart, "It is not recommended to use a Global Filter to do time trimming. Consider creating an asset filter instead.");
            }
            else if (checkBoxStartTime.Checked && checkBoxEndTime.Checked && timeControlStart.TimestampAsTimeSpan > timeControlEnd.TimestampAsTimeSpan)
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
            else if (checkBoxEndTime.Checked && checkBoxStartTime.Checked && timeControlEnd.TimestampAsTimeSpan < timeControlStart.TimestampAsTimeSpan)
            {
                errorProvider1.SetError(timeControlEnd, "End time must be higher than start time");
            }
            else
            {
                errorProvider1.SetError(timeControlEnd, String.Empty);
            }


            // dvr
            if (checkBoxDVRWindow.Checked && timeControlDVR.TimestampAsTimeSpan < TimeSpan.FromMinutes(2))
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
            CheckIfErrorTimeControls();

        }

        private void checkBoxEndTime_CheckedChanged(object sender, EventArgs e)
        {
            timeControlEnd.Enabled = checkBoxEndTime.Checked;
            CheckIfErrorTimeControls();


        }

        private void checkBoxLiveBackoff_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownBackoffSeconds.Enabled = checkBoxLiveBackoff.Checked;
            CheckIfErrorTimeControls();

        }

        private void checkBoxDVRWindow_CheckedChanged(object sender, EventArgs e)
        {
            timeControlDVR.Enabled = checkBoxDVRWindow.Checked;
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
            if (!isGlobalFilter) // Asset Filter
            {
                labelFilterTitle.Text = "Asset Filter";
                textBoxAssetName.Visible = true;
                labelassetname.Visible = true;
                isGlobalFilter = false;

                bool Error = false;
                try
                {
                    var locator = GetOnDemandLocator();
                    var manifesturi = _parentAsset.GetSmoothStreamingUri();
                    XDocument manifest = XDocument.Load(manifesturi.ToString());
                    locator.Delete();

                    var smoothmedia = manifest.Element("SmoothStreamingMedia");
                    string timescalefrommanifest = smoothmedia.Attribute("TimeScale").Value;
                    _parentAssetDuration = long.Parse(smoothmedia.Attribute("Duration").Value);
                    _parentAssetTimeScale = _timescale = long.Parse(timescalefrommanifest);
                    _parentAssetDurationInTicks = (long)((double)_parentAssetDuration * (double)TimeSpan.TicksPerSecond / (double)_parentAssetTimeScale);
                }
                catch
                {
                    Error = true;
                }

                if (!Error && newfilter)
                {
                    timeControlStart.Max = timeControlEnd.Max = timeControlEnd.TimestampAsTimeSpan = timeControlDVR.Max = new TimeSpan(_parentAssetDurationInTicks);
                    timeControlStart.TotalDuration = timeControlEnd.TotalDuration = timeControlDVR.TotalDuration = _parentAssetDuration;
                    timeControlStart.TimeScale = timeControlEnd.TimeScale = _parentAssetTimeScale;
                    timeControlStart.DisplayTrackBar = timeControlEnd.DisplayTrackBar = timeControlDVR.DisplayTrackBar = true;

                }

                if (!Error)
                {
                    labelassetduration.Visible = textBoxAssetDuration.Visible = true;
                    textBoxAssetDuration.Text = new TimeSpan(_parentAssetDurationInTicks).ToString(@"d\.hh\:mm\:ss");
                }

            }

            moreinfoprofilelink.Links.Add(new LinkLabel.Link(0, moreinfoprofilelink.Text.Length, Constants.LinkHowIMoreInfoDynamicManifest));

            RefreshPresentationTimes();
            RefreshTracks();
        }



    }
}
