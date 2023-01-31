//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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


using Azure.ResourceManager.Media.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class BatchUploadFrame2 : Form
    {
        private readonly AMSClientV3 _client;
        private readonly List<string> folders;
        private readonly List<string> files;
        private readonly bool ErrorConnect = false;

        public List<string> BatchSelectedFolders
        {
            get
            {
                List<string> selectedfolders = new();
                foreach (object f in checkedListBoxFolders.CheckedItems)
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
                List<string> selectedfiles = new();
                foreach (object f in checkedListBoxFiles.CheckedItems)
                {
                    selectedfiles.Add(files[checkedListBoxFiles.Items.IndexOf((ListViewItem)f)]);
                }
                return selectedfiles;
            }
        }

        public string StorageSelected => ((Item)comboBoxStorage.SelectedItem).Value;

        public int BlockSize
        {
            get
            {
                bool success = int.TryParse(comboBoxBlockSize.Text, out int x);
                return success ? x : 4;
            }
        }

        public BatchUploadFrame2(string BatchFolderPath, bool BatchProcessFiles, bool BatchProcessSubFolders, AMSClientV3 client)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _client = client;

            folders = Directory.GetDirectories(BatchFolderPath).ToList();
            files = Directory.GetFiles(BatchFolderPath).ToList();

            try
            {
                if (BatchProcessFiles)
                {
                    foreach (string file in files)
                    {
                        ListViewItem it = checkedListBoxFiles.Items.Add(Path.GetFileName(file));
                        it.Checked = true;
                        if (!AssetTools.BlobNameForAMSIsOk(Path.GetFileName(file)))
                        {
                            it.ForeColor = Color.Red;
                        }
                    }
                }
                if (BatchProcessSubFolders)
                {
                    folders.RemoveAll(f => Directory.GetFiles(f).Length == 0); // we remove all folder with 0 file in it at the root

                    string s;
                    int filecount;
                    foreach (string folder in folders)
                    {
                        filecount = Directory.GetFiles(folder).Length;
                        s = filecount > 1 ? AMSExplorer.Properties.Resources.BatchUploadFrame2_BatchUploadFrame2_01Files : AMSExplorer.Properties.Resources.BatchUploadFrame2_BatchUploadFrame2_01File;
                        ListViewItem it = checkedListBoxFolders.Items.Add(string.Format(s, Path.GetFileName(folder), filecount));
                        it.Checked = true;
                        if (AssetTools.ReturnFilenamesWithProblem(Directory.GetFiles(folder).ToList()).Count > 0)
                        {
                            it.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorConnect = true;
                DialogResult = DialogResult.None;
                MessageBox.Show(AMSExplorer.Properties.Resources.BatchUploadFrame2_BatchUploadFrame2_ErrorWhenReadingFilesOrFolders + Constants.endline + e.Message, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BathUploadFrame2_LoadAsync(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            // to scale the bitmap in the buttons

            if (ErrorConnect)
            {
                Close();
            }


            foreach (var storage in _client.AMSclient.Data.StorageAccounts)
            {
                string sname = AMSClientV3.GetStorageName(storage.Id);
                bool primary = (storage.AccountType == MediaServicesStorageAccountType.Primary);
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", sname, primary ? "(primary)" : ""), sname));
                if (primary)
                {
                    comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
                }
            }

            List<int> listInt = new() { 1, 2, 4, 8, 16, 32, 64 };
            comboBoxBlockSize.Items.Clear();
            listInt.ForEach(l => comboBoxBlockSize.Items.Add(l.ToString()));
            comboBoxBlockSize.SelectedIndex = 3;
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

        private void BatchUploadFrame2_DpiChanged(object sender, DpiChangedEventArgs e)
        {
        }

        private void BatchUploadFrame2_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
