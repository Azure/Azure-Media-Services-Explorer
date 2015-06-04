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
        private DataTable dataPropertyType;
        private DataTable dataProperty;
        private DataTable dataOperator;

        public DynManifestFilter(Filter filter)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _filter = filter;
        }



        private void DynManifestFilter_Load(object sender, EventArgs e)
        {
            textBoxFilterName.Text = _filter.Name;
            textBoxStartTimestamp.Text = _filter.PresentationTimeRange.StartTimestamp;
            textBoxEndTimestamp.Text = _filter.PresentationTimeRange.EndTimestamp;
            textBoxLiveBackoffDuration.Text = _filter.PresentationTimeRange.LiveBackoffDuration;
            textBoxPresentationWindowDuration.Text = _filter.PresentationTimeRange.PresentationWindowDuration;
            textBoxTimescale.Text = _filter.PresentationTimeRange.Timescale;

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
                listBoxTracks.Items.Add("Track" + i);
                i++;
            }
        }

        public Filter GetFilterData
        {
            get
            {
                _filter.Name = textBoxFilterName.Text;
                _filter.PresentationTimeRange.StartTimestamp = textBoxStartTimestamp.Text;
                _filter.PresentationTimeRange.EndTimestamp = null;// textBoxEndTimestamp.Text;
                _filter.PresentationTimeRange.LiveBackoffDuration = textBoxLiveBackoffDuration.Text;
                _filter.PresentationTimeRange.PresentationWindowDuration = textBoxPresentationWindowDuration.Text;
                _filter.PresentationTimeRange.Timescale = textBoxTimescale.Text;

                return _filter;
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
                    case 0: // propoerty
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

            var track = _filter.Tracks[listBoxTracks.SelectedIndex];
            foreach (var condition in track.PropertyConditions)
            {
                dataGridViewTracks.Rows.Add(condition.Property, condition.Operator, condition.Value);

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
    }
}
