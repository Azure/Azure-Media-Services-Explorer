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



            var column = new DataGridViewComboBoxColumn();
            DataTable data = new DataTable();

            data.Columns.Add(new DataColumn("Value", typeof(string)));
            data.Columns.Add(new DataColumn("Description", typeof(string)));

            data.Rows.Add("5", "6");
            data.Rows.Add("51", "26");
            data.Rows.Add("531", "63");

            column.DataSource = data;
            column.ValueMember = "Value";
            column.DisplayMember = "Description";

            dataGridViewTracks.Columns.Add(column); 
        }

        public Filter GetFilterData
        {
            get
            {
                _filter.Name = textBoxFilterName.Text;
                _filter.PresentationTimeRange.StartTimestamp = textBoxStartTimestamp.Text;
                _filter.PresentationTimeRange.EndTimestamp = textBoxEndTimestamp.Text;
                _filter.PresentationTimeRange.LiveBackoffDuration = textBoxLiveBackoffDuration.Text;
                _filter.PresentationTimeRange.PresentationWindowDuration = textBoxPresentationWindowDuration.Text;
                _filter.PresentationTimeRange.Timescale = textBoxTimescale.Text;

                return _filter;
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
