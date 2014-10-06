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
    public partial class BathUploadFrame2 : Form
    {
        private List<string> folders;
        private List<string> files;
        private bool ErrorConnect = false;

        public List<string> BatchSelectedFolders
        {
            get
            {
                List<string> selectedfolders = new List<string>();
                foreach (var f in checkedListBoxFolders.CheckedItems)
                {
                    selectedfolders.Add(folders[checkedListBoxFolders.Items.IndexOf(f)]);
                }
                return selectedfolders;
            }
        }

        public List<string> BatchSelectedFiles
        {
            get
            {
                List<string> selectedfiles = new List<string>();
                foreach (var f in checkedListBoxFiles.CheckedItems)
                {
                    selectedfiles.Add(files[checkedListBoxFiles.Items.IndexOf(f)]);
                }
                return selectedfiles;
            }
        }

        public BathUploadFrame2(string BatchFolderPath, bool BatchProcessFiles, bool BatchProcessSubFolders)
        {
            InitializeComponent();

            folders = Directory.GetDirectories(BatchFolderPath).ToList();
            files = Directory.GetFiles(BatchFolderPath).ToList();

            try
            {
                if (BatchProcessFiles)
                {
                    foreach (var file in files)
                    {
                        checkedListBoxFiles.Items.Add(Path.GetFileName(file), true);
                    }
                }
                if (BatchProcessSubFolders)
                {
                    folders.RemoveAll(f => Directory.GetFiles(f).Count() == 0); // we remove all folder with 0 file in it at the root

                    string s;
                    int filecount;
                    foreach (var folder in folders)
                    {
                        filecount = Directory.GetFiles(folder).Count();
                        s = filecount > 1 ? "{0} ({1} files)" : "{0} ({1} file)";
                        checkedListBoxFolders.Items.Add(string.Format(s, Path.GetFileName(folder), filecount), true);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorConnect = true;
                this.DialogResult = DialogResult.None;
                MessageBox.Show("Error when reading files or folders." + Constants.endline + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BathUploadFrame2_Load(object sender, EventArgs e)
        {
            if (ErrorConnect)
            {
                this.Close();
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonFolderSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxFolders.Items.Count; i++)
            {
                checkedListBoxFolders.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void buttonFolderDeselAll_Click(object sender, EventArgs e)
        {
            foreach (int i in checkedListBoxFolders.CheckedIndices)
            {
                checkedListBoxFolders.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void buttonFileDeselAll_Click(object sender, EventArgs e)
        {
            foreach (int i in checkedListBoxFiles.CheckedIndices)
            {
                checkedListBoxFiles.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void buttonFileSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxFiles.Items.Count; i++)
            {
                checkedListBoxFiles.SetItemCheckState(i, CheckState.Checked);
            }
        }
    }
}
