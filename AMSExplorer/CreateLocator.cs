using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class CreateLocator : Form
    {

        public DateTime? LocStartDate
        {
            get
            {
                return (checkBoxStartDate.Checked) ? (DateTime)dateTimePickerStartDate.Value.ToUniversalTime() : (Nullable<DateTime>)null;
            }
            set
            {
                dateTimePickerStartDate.Value = (DateTime)value;
                dateTimePickerStartTime.Value = (DateTime)value;
            }
        }

        public bool LocHasStartDate
        {
            get { return checkBoxStartDate.Checked; }
            set { checkBoxStartDate.Checked = value; }
        }

        public DateTime LocEndDate
        {
            get
            {
                if (radioButtonEndCustom.Checked) return dateTimePickerEndDate.Value;
                else if (radioButtonEndYear.Checked) return DateTime.UtcNow.AddYears(1);
                else return DateTime.UtcNow.AddYears(100);
            }
            set
            {
                dateTimePickerEndDate.Value = value;
                dateTimePickerEndTime.Value = value;
            }
        }

        public LocatorType LocType
        {
            get
            {
                return (radioButtonOrigin.Checked) ? LocatorType.OnDemandOrigin : LocatorType.Sas;
            }

        }

        public string LocAssetName
        {
            set
            {
                labelAssetName.Text = value;
            }
        }

        public string LocWarning
        {
            set
            {
                labelWarning.Text = value;
            }
        }


        public CreateLocator()
        {
            InitializeComponent();
        }



        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
        }

        private void checkBoxStartDate_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = checkBoxStartDate.Checked;
            dateTimePickerStartTime.Enabled = checkBoxStartDate.Checked;
        }

        private void CreateLocator_Load(object sender, EventArgs e)
        {

        }

        private void radioButtonEndCustom_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Enabled = radioButtonEndCustom.Checked;
            dateTimePickerEndTime.Enabled = radioButtonEndCustom.Checked;
        }
    }
}
