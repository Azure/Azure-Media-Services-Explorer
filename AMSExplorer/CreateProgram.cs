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
    public partial class CreateProgram : Form
    {
        public string ChannelName;

        public string ProgramName
        {
            get { return textboxprogramname.Text; }
            set { textboxprogramname.Text = value; }
        }

        public string ProgramDescription
        {
            get { return textBoxDescription.Text; }
            set { textBoxDescription.Text = value; }
        }


        public TimeSpan archiveWindowLength
        {
            get
            {
                return new TimeSpan((int)numericUpDownArchiveDays.Value, (int)numericUpDownArchiveHours.Value, (int)numericUpDownArchiveMinutes.Value, 0); ;
            }
            set
            {
                numericUpDownArchiveDays.Value = value.Days;
                numericUpDownArchiveHours.Value = value.Hours;
                numericUpDownArchiveMinutes.Value = value.Minutes;
            }
        }

        public bool ProposeScaleUnit
        {
            set
            {
                checkBoxAddScaleUnit.Checked = value;
                checkBoxAddScaleUnit.Visible = value;
            }
        }

        public bool ScaleUnit
        {
            get
            {
                return checkBoxAddScaleUnit.Checked;
            }
        }



        public string AssetName
        {
            get { return textBoxAssetName.Text; }
            set { textBoxAssetName.Text = value; }
        }

        public bool CreateLocator
        {
            get { return checkBoxCreateLocator.Checked; }
            set { checkBoxCreateLocator.Checked = value; }
        }


        public CreateProgram()
        {
            InitializeComponent();
        }

        private void CreateLocator_Load(object sender, EventArgs e)
        {
            this.Text = string.Format(this.Text, ChannelName);
        }
    }
}
