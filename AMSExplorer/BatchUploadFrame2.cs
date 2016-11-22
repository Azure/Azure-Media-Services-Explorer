﻿//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//---------------------------------------------------------------------------------------------

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
using Microsoft.WindowsAzure.MediaServices.Client;


namespace AMSExplorer
{
    public partial class BatchUploadFrame2 : Form
    {
        private List<string> folders;
        private List<string> files;
        private bool ErrorConnect = false;
        private CloudMediaContext _context;

        public List<string> BatchSelectedFolders
        {
            get
            {
                List<string> selectedfolders = new List<string>();
                foreach (var f in checkedListBoxFolders.CheckedItems)
                {
                    selectedfolders.Add(folders[checkedListBoxFolders.Items.IndexOf((ListViewItem)f)]);
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
                    selectedfiles.Add(files[checkedListBoxFiles.Items.IndexOf((ListViewItem)f)]);
                }
                return selectedfiles;
            }
        }

        public string StorageSelected
        {
            get
            {
                return ((Item)comboBoxStorage.SelectedItem).Value;
            }
        }

        public BatchUploadFrame2(string BatchFolderPath, bool BatchProcessFiles, bool BatchProcessSubFolders, CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;

            folders = Directory.GetDirectories(BatchFolderPath).ToList();
            files = Directory.GetFiles(BatchFolderPath).ToList();

            try
            {
                if (BatchProcessFiles)
                {
                    foreach (var file in files)
                    {
                        var it = checkedListBoxFiles.Items.Add(Path.GetFileName(file));
                        it.Checked = true;
                        if (!AssetInfo.AssetFileNameIsOk(Path.GetFileName(file)))
                        {
                            it.ForeColor = Color.Red;
                        }
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
                        s = filecount > 1 ? AMSExplorer.Properties.Resources.BatchUploadFrame2_BatchUploadFrame2_01Files : AMSExplorer.Properties.Resources.BatchUploadFrame2_BatchUploadFrame2_01File;
                        var it = checkedListBoxFolders.Items.Add(string.Format(s, Path.GetFileName(folder), filecount));
                        it.Checked = true;
                        if (AssetInfo.ReturnFilenamesWithProblem(Directory.GetFiles(folder).ToList()).Count > 0)
                        {
                            it.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorConnect = true;
                this.DialogResult = DialogResult.None;
                MessageBox.Show(AMSExplorer.Properties.Resources.BatchUploadFrame2_BatchUploadFrame2_ErrorWhenReadingFilesOrFolders + Constants.endline + e.Message, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BathUploadFrame2_Load(object sender, EventArgs e)
        {
            if (ErrorConnect)
            {
                this.Close();
            }
            foreach (var storage in _context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? AMSExplorer.Properties.Resources.BatchUploadFrame2_BathUploadFrame2_Load_Default : ""), storage.Name));
                if (storage.Name == _context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
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
                checkedListBoxFolders.Items[i].Checked = true;
            }
        }

        private void buttonFolderDeselAll_Click(object sender, EventArgs e)
        {
            foreach (int i in checkedListBoxFolders.CheckedIndices)
            {
                checkedListBoxFolders.Items[i].Checked = false;
            }
        }

        private void buttonFileDeselAll_Click(object sender, EventArgs e)
        {
            foreach (int i in checkedListBoxFiles.CheckedIndices)
            {
                checkedListBoxFiles.Items[i].Checked = false;
            }
        }

        private void buttonFileSelAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxFiles.Items.Count; i++)
            {
                checkedListBoxFiles.Items[i].Checked = true;
            }
        }
    }
}
