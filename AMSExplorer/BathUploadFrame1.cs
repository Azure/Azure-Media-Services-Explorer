using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace AMSExplorer
{
    public partial class BathUploadFrame1 : Form
    {
        public string BatchFolder
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

        public bool BatchProcessSubFolders
        {
            get
            {
                return checkBoxSubFolder.Checked;
            }
            set
            {
                checkBoxSubFolder.Checked = value;
            }
        }
        public bool BatchProcessFiles
        {
            get
            {
                return checkBoxProcessFiles.Checked;
            }
            set
            {
                checkBoxProcessFiles.Checked = value;
            }
        }



        public BathUploadFrame1()
        {
            InitializeComponent();
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderDialog = new FolderBrowserDialog();
            if (FolderDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = FolderDialog.SelectedPath;
            }
        }

        private void BathUploadFrame1_Load(object sender, EventArgs e)
        {
            checkBoxOneUpDownload.Checked = Properties.Settings.Default.useTransferQueue;
            checkBoxUseStorageEncryption.Checked = Properties.Settings.Default.useStorageEncryption;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxFolder.Text))
            {
                MessageBox.Show("Folder does not exist", "Folder", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.DialogResult = DialogResult.None;
            }

        }
    }
}
