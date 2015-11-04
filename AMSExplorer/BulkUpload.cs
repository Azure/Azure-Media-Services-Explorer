//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
using Microsoft.WindowsAzure.MediaServices.Client;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Reflection;
using System.IO;

namespace AMSExplorer
{
    public partial class BulkUpload : Form
    {
        private BindingList<BulkAssetFile> assetFiles = new BindingList<BulkAssetFile>();
        private CloudMediaContext _context;

        public string IngestName
        {
            get
            {
                return textBoxManifestName.Text;
            }
        }
        public string AssetName
        {
            get
            {
                return textBoxAssetName.Text;
            }
        }

        public string[] AssetFiles
        {
            get
            {
                return assetFiles.Where(a => !string.IsNullOrWhiteSpace(a.FileName)).Select(a => a.FileName).ToArray();
            }
        }

        public string AssetStorageSelected
        {
            get
            {
                return ((Item)comboBoxStorageAsset.SelectedItem).Value;
            }
        }

        public string IngestStorageSelected
        {
            get
            { 
                return ((Item)comboBoxStorageIngest.SelectedItem).Value;
            }
        }

        public bool EncryptAssetFiles
        {
            get
            {
                return checkBoxEncrypt.Checked;
            }
        }

        public string EncryptToFolder
        {
            get
            {
                return checkBoxEncrypt.Checked ? textBoxFolderPath.Text : null;
            }
        }

        public BulkUpload(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            dataGridAssetFiles.DataSource = assetFiles;
            _context = context;
        }

        private void UploadBulk_Load(object sender, EventArgs e)
        {
            comboBoxStorageAsset.Items.Clear();
            foreach (var storage in _context.StorageAccounts)
            {
                var it = new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? "(default)" : ""), storage.Name);
                comboBoxStorageIngest.Items.Add(it);
                comboBoxStorageAsset.Items.Add(it);
                if (storage.Name == _context.DefaultStorageAccount.Name)
                {
                    comboBoxStorageIngest.SelectedIndex = comboBoxStorageIngest.Items.Count - 1;
                    comboBoxStorageAsset.SelectedIndex = comboBoxStorageAsset.Items.Count - 1;
                }
            }
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            assetFiles.AddNew();
        }

        private void buttonDelFiles_Click(object sender, EventArgs e)
        {
            if (dataGridAssetFiles.SelectedRows.Count > 0)
            {
                List<BulkAssetFile> removeItems = new List<BulkAssetFile>();

                foreach (DataGridViewRow row in dataGridAssetFiles.SelectedRows)
                {
                    //assetFiles.RemoveAt(dataGridAssetFiles.SelectedRows[0].Index);
                    removeItems.Add(assetFiles[row.Index]);
                    //assetFiles.RemoveAt(row.Index);
                }

                foreach (BulkAssetFile item in removeItems)
                    assetFiles.Remove(item);
            }
        }


        class BulkAssetFile
        {
            private string _fileName;
            public string FileName { get { return _fileName; } set { _fileName = value; } }

            public BulkAssetFile()
            {
                _fileName = string.Empty;
            }
        }

        private void buttonSelectFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialogAssetFiles.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openFileDialogAssetFiles.FileNames)
                {
                    assetFiles.Add(new BulkAssetFile() { FileName = file });
                }
                textBoxFolderPath.Text = Path.GetDirectoryName(assetFiles[0].FileName) + @"\Encrypted";
            }
        }

        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxFolderPath.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            buttonBrowseFile.Enabled = textBoxFolderPath.Enabled = checkBoxEncrypt.Checked;
        }
    }


}
