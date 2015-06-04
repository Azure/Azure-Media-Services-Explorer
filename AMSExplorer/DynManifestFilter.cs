using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{


    public partial class DynManifestFilter : Form
    {
        private Filter _filter;
        private MediaServiceContextForDynManifest _context;
        private DataTable dataPropertyType;
        private DataTable dataProperty;
        private DataTable dataOperator;

        public DynManifestFilter(MediaServiceContextForDynManifest context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
           
            _filter = new Filter();
            _filter.SetContext(_context);
            _filter.PresentationTimeRange = new IFilterPresentationTimeRange();
            _filter.Tracks = new List<IFilterTrackSelect>();
                       
        }



        private void DynManifestFilter_Load(object sender, EventArgs e)
        {
            RefreshPresentationTimes();
            RefreshTracks();

            // dataPropertyType
            dataPropertyType = new DataTable();
            dataPropertyType.Columns.Add(new DataColumn("Value", typeof(string)));
            dataPropertyType.Columns.Add(new DataColumn("Description", typeof(string)));

            dataPropertyType.Rows.Add(FilterPropertyTypeValue.audio, FilterPropertyTypeValue.audio);
            dataPropertyType.Rows.Add(FilterPropertyTypeValue.video, FilterPropertyTypeValue.video);
            dataPropertyType.Rows.Add(FilterPropertyTypeValue.text, FilterPropertyTypeValue.text);

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
                listBoxTracks.Items.Add("Group" + i);
                i++;
            }
        }

        public Filter DisplayedFilter
        {
            get
            {
                _filter.Name = textBoxFilterName.Text;
                _filter.PresentationTimeRange.StartTimestamp = string.IsNullOrEmpty(textBoxStartTimestamp.Text.Trim()) ? null : textBoxStartTimestamp.Text;
                _filter.PresentationTimeRange.EndTimestamp = string.IsNullOrEmpty(textBoxEndTimestamp.Text.Trim()) ? null : textBoxEndTimestamp.Text;
                _filter.PresentationTimeRange.LiveBackoffDuration = string.IsNullOrEmpty(textBoxLiveBackoffDuration.Text.Trim()) ? null : textBoxLiveBackoffDuration.Text;
                _filter.PresentationTimeRange.PresentationWindowDuration = string.IsNullOrEmpty(textBoxPresentationWindowDuration.Text.Trim()) ? null : textBoxPresentationWindowDuration.Text;
                _filter.PresentationTimeRange.Timescale = string.IsNullOrEmpty(textBoxTimescale.Text.Trim()) ? null : textBoxTimescale.Text;

                if (_filter.Tracks.Count == 0) _filter.Tracks = null; // to make sure it is null to avoid puting data in JSON

                return _filter;
            }
            set
            {
                // goal is to display an existing filer
                _filter = value;
                buttonOk.Text = "Update Filter";
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

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
                    dataGridViewTracks.Rows.Add(condition.Property, condition.Operator, condition.Value);

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

            _filter.PresentationTimeRange = new IFilterPresentationTimeRange() { LiveBackoffDuration = "0", StartTimestamp = "0", PresentationWindowDuration = "1300000000", EndTimestamp = "100000000", Timescale = "10000000" };
            var conditions = new List<FilterTrackPropertyCondition>();
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.video });
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Bitrate, Value = "0-2147483647" });
            var tracks = new List<IFilterTrackSelect>() { new IFilterTrackSelect() { PropertyConditions = conditions } };
           
            conditions = new List<FilterTrackPropertyCondition>();
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.audio });
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Bitrate, Value = "0-2147483647" });
            tracks.Add(new IFilterTrackSelect() { PropertyConditions = conditions });
            
            conditions = new List<FilterTrackPropertyCondition>();
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Type, Value = FilterPropertyTypeValue.text });
            conditions.Add(new FilterTrackPropertyCondition() { Operator = IOperator.Equal, Property = FilterProperty.Bitrate, Value = "0-2147483647" });
            tracks.Add(new IFilterTrackSelect() { PropertyConditions = conditions });
      
            _filter.Tracks = tracks;

            RefreshPresentationTimes();
            RefreshTracks();
            RefreshTracksConditions();

        }

        private void RefreshPresentationTimes()
        {
            textBoxFilterName.Text = _filter.Name;
            if (_filter.PresentationTimeRange!=null)
            {
                textBoxStartTimestamp.Text = _filter.PresentationTimeRange.StartTimestamp;
                textBoxEndTimestamp.Text = _filter.PresentationTimeRange.EndTimestamp;
                textBoxLiveBackoffDuration.Text = _filter.PresentationTimeRange.LiveBackoffDuration;
                textBoxPresentationWindowDuration.Text = _filter.PresentationTimeRange.PresentationWindowDuration;
                textBoxTimescale.Text = _filter.PresentationTimeRange.Timescale;
            }
            else
            {
                textBoxStartTimestamp.Text = string.Empty;
                textBoxEndTimestamp.Text = string.Empty;
                textBoxLiveBackoffDuration.Text = string.Empty;
                textBoxPresentationWindowDuration.Text = string.Empty;
                textBoxTimescale.Text = string.Empty;
            }
            
        }
    }
}
