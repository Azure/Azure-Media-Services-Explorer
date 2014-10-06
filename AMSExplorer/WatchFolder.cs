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
    public partial class WatchFolder : Form
    {

        public string WatchFolderPath
        {
            get
            {
                return textBoxFolder.Text;
            }
            set
            {
                textBoxFolder.Text = value;
            }
        }
        public bool WatchOn
        {
            get
            {
                return radioButtonON.Checked;
            }
            set
            {
                radioButtonON.Checked = value;
            }
        }
        public bool WatchUseQueue
        {
            get
            {
                return checkBoxUseQueue.Checked;
            }
            set
            {
                checkBoxUseQueue.Checked = value;
            }
        }
        public bool WatchDeleteFile
        {
            get
            {
                return checkBoxDeleteFile.Checked;
            }
            set
            {
                checkBoxDeleteFile.Checked = value;
            }
        }

        public WatchFolder()
        {
            InitializeComponent();

        }

        private void checkBoxParallel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void WatchFolder_Load(object sender, EventArgs e)
        {
            buttonOk.Enabled = string.IsNullOrWhiteSpace(textBoxFolder.Text) ? false : true;

        }

        private void buttonSelFolder_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void WatchFolder_Shown(object sender, EventArgs e)
        {

        }

        private void textBoxFolder_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = string.IsNullOrWhiteSpace(textBoxFolder.Text) ? false : true;
        }



    }
}
