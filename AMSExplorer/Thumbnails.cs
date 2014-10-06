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

namespace AMSExplorer
{
    public partial class Thumbnails : Form
    {

        public string ThumbnailsInputAssetName
        {
            get
            {
                return labelAssetName.Text;
            }
            set
            {
                labelAssetName.Text = value;
            }
        }
        public string ThumbnailsOutputAssetName
        {
            get
            {
                return textboxoutputassetname.Text;
            }
            set
            {
                textboxoutputassetname.Text = value;
            }
        }

        public string ThumbnailsJobName
        {
            get
            {
                return textBoxJobName.Text;
            }
            set
            {
                textBoxJobName.Text = value;
            }
        }

        public int ThumbnailsJobPriority
        {
            get
            {
                return (int)numericUpDownPriority.Value;
            }
            set
            {
                numericUpDownPriority.Value = value;
            }
        }

        public string ThumbnailsFileName
        {
            get
            {
                return textBoxFileName.Text;
            }
            set
            {
                textBoxFileName.Text = value;
            }
        }
        public string ThumbnailsTimeValue
        {
            get
            {
                return textBoxTimeValue.Text;
            }
            set
            {
                textBoxTimeValue.Text = value;
            }
        }

        public string ThumbnailsTimeStep
        {
            get
            {
                return textBoxTimeStep.Text;
            }
            set
            {
                textBoxTimeStep.Text = value;
            }
        }

        public string ThumbnailsTimeStop
        {
            get
            {
                return textBoxTimeStop.Text;
            }
            set
            {
                textBoxTimeStop.Text = value;
            }
        }

        public string ThumbnailsSize
        {
            get
            {
                return textBoxSize.Text;
            }
            set
            {
                textBoxSize.Text = value;
            }
        }

        public string ThumbnailsType
        {
            get
            {
                return comboBoxThumbnailFormat.SelectedItem.ToString();
            }
            set
            {
                comboBoxThumbnailFormat.SelectedIndex = comboBoxThumbnailFormat.Items.IndexOf(value);
            }
        }


        public string ThumbnailsProcessorName
        {
            get
            {
                return processorlabel.Text;
            }
            set
            {
                processorlabel.Text = value;
            }
        }

        public Thumbnails()
        {
            InitializeComponent();
        }


        private void Thumbnails_Load(object sender, EventArgs e)
        {

        }
    }
}
