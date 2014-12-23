//----------------------------------------------------------------------- 
// <copyright file="ImportFromAzureStorage.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Web;
using System.Configuration;
using System.IO;

namespace AMSExplorer
{
    public partial class ImportFromAzureStorage : Form
    {

        private static CloudMediaContext contextUpload = null;
        private static string MediaServicesStorageAccountKey;
        private CloudStorageAccount storageAccount;
        private CloudBlobClient cloudBlobClient;
        private List<IListBlobItem> ListBlobs = new List<IListBlobItem>();

        public List<IListBlobItem> SelectedBlobs = new List<IListBlobItem>();
        public string SelectedBlobContainer;
        public bool useDefaultStorage;
        public bool createNewAsset;
        private bool ErrorConnect = false;
        private IEnumerable<CloudBlobContainer> mediaBlobContainers;


        public bool ImportUseDefaultStorage
        {
            get
            {
                return radioButtonStorageDefault.Checked;
            }
            set
            {
                radioButtonStorageDefault.Checked = value;
            }
        }
        public string ImportLabelDefaultStorageName
        {
            get
            {
                return labelDefaultStorage.Text;
            }
            set
            {
                labelDefaultStorage.Text = value;
            }
        }
        public string ImportNewAssetName
        {
            get
            {
                return textBoxNewAssetName.Text;
            }
            set
            {
                textBoxNewAssetName.Text = value;
            }
        }
        public string ImportLabelExistingAssetName
        {
            get
            {
                return labelExistingAssetName.Text;
            }
            set
            {
                labelExistingAssetName.Text = value;
            }
        }

        public string ImportOptionToCopyFilesToExistingAssetLabel
        {
            set { labelSelectedAssetWarning.Text = value; }
        }
        public bool ImportOptionToCopyFilesToExistingAsset
        {
            set
            {
                radioButtonSelectedAsset.Enabled = value;
                if (!value) labelExistingAssetName.Text = string.Empty;
            }
        }
        public string ImporOtherStorageName
        {
            get
            {
                return textBoxStorageName.Text;
            }
            set
            {
                textBoxStorageName.Text = value;
            }
        }
        public string ImportOtherStorageKey
        {
            get
            {
                return textBoxStorageKey.Text;
            }
            set
            {
                textBoxStorageKey.Text = value;
            }
        }
        public bool ImportCreateNewAsset
        {
            get
            {
                return radioButtonNewAsset.Checked;
            }
            set
            {
                radioButtonNewAsset.Checked = value;
            }
        }



        public ImportFromAzureStorage(CloudMediaContext contextUploadArg, string MediaServicesStorageAccountKeyArg)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MediaServicesStorageAccountKey = MediaServicesStorageAccountKeyArg;
            contextUpload = contextUploadArg;
        }

        private void UploadFromBlob_Load(object sender, EventArgs e)
        {
            ConnectToStorage();
            if (ErrorConnect)
            {
                this.Close();
            }
            else
            {
                DoListBlobs(true);
                if (ErrorConnect) this.Close();
            }
            listViewBlobs.Tag = -1;
            listViewBlobs.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
            listViewFiles.Tag = -1;
            listViewFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
        }

        private void ConnectToStorage()
        {
            try
            {
                if (radioButtonStorageDefault.Checked)
                {
                    storageAccount = new CloudStorageAccount(new StorageCredentials(contextUpload.DefaultStorageAccount.Name, MediaServicesStorageAccountKey), true);
                }
                else
                {
                    storageAccount = new CloudStorageAccount(new StorageCredentials(textBoxStorageName.Text, textBoxStorageKey.Text), true);
                }
            }
            catch
            {
                MessageBox.Show("There is a problem when connecting to the storage account");
                ErrorConnect = true;
                return;
            }
            cloudBlobClient = storageAccount.CreateCloudBlobClient();
            mediaBlobContainers = cloudBlobClient.ListContainers();
            ErrorConnect = false;

        }

        private void DoListBlobs(bool ResetSearch)
        {

            // Fill the list of Blobs
            listViewBlobs.Items.Clear();
            listViewBlobs.BeginUpdate();

            if (ResetSearch) textBoxSearch.Text = "";

            try
            {

                foreach (CloudBlobContainer BlobContainer in mediaBlobContainers)
                {
                    if (BlobContainer.Name.Contains(textBoxSearch.Text))
                    {
                        ListViewItem item = new ListViewItem(Path.GetFileName(BlobContainer.Name), 0);
                        item.SubItems.Add(BlobContainer.Properties.LastModified.Value.UtcDateTime.ToLocalTime().ToString());
                        listViewBlobs.Items.Add(item);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There is a problem when connecting to the storage account");
                ErrorConnect = true;
                listViewBlobs.Items.Clear();
                listViewBlobs.EndUpdate();
                return;
            }

            listViewBlobs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewBlobs.EndUpdate();
            buttonUpload.Enabled = false;


        }


        private void DisplayFilesOfContainer(string containerName)
        {
            CloudBlobContainer Container = cloudBlobClient.ListContainers().Where(n => n.Name == containerName).FirstOrDefault();
            IEnumerable<IListBlobItem> mediaBlobs = Container.ListBlobs();

            ListBlobs.Clear();
            listViewFiles.Items.Clear();

            foreach (IListBlobItem b in mediaBlobs)
            {
                if (b.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob cloudBlockBlob = b as CloudBlockBlob;
                    string lastModified = "";
                    ListViewItem item = new ListViewItem(Path.GetFileName(b.Uri.LocalPath), 0);
                    lastModified = cloudBlockBlob.Properties.LastModified.Value.UtcDateTime.ToLocalTime().ToString();
                    item.SubItems.Add(lastModified);
                    item.SubItems.Add(AssetInfo.FormatByteSize(cloudBlockBlob.Properties.Length));
                    // Place a check mark next to the item.
                    listViewFiles.Items.Add(item);
                    ListBlobs.Add(b);
                }
                /*
                if (b.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory cloudBlockDir = b as CloudBlobDirectory;
                    ListViewItem item = new ListViewItem(cloudBlockDir.Prefix, 0);
                    listViewFiles.Items.Add(item);
                    ListBlobs.Add(b);
                }
               */

            }
            listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }


        private void buttonClose_Click(object sender, EventArgs e)
        {

            this.SelectedBlobs.Clear();
           
            foreach (ListViewItem item in listViewFiles.SelectedItems)
            {
                // let's find the file as control has perhaps been sorted
                IListBlobItem bitem = ListBlobs.Where(l => Path.GetFileName(l.Uri.LocalPath) == item.Text).FirstOrDefault();
                this.SelectedBlobs.Add(bitem);
            }
        }

        private void radioButtonStorageDefault_CheckedChanged(object sender, EventArgs e)
        {
            this.useDefaultStorage = radioButtonStorageDefault.Checked;
            if (radioButtonStorageDefault.Checked)
            {
                ConnectToStorage();
                DoListBlobs(true);
            }
            buttonConnect.Enabled = radioButtonOtherStorage.Checked;
        }

        private void radioButtonNewAsset_CheckedChanged(object sender, EventArgs e)
        {
            this.createNewAsset = radioButtonNewAsset.Checked;
            textBoxNewAssetName.Enabled = radioButtonNewAsset.Checked;
        }

        private void radioButtonOtherStorage_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStorageName.Enabled = radioButtonOtherStorage.Checked;
            textBoxStorageKey.Enabled = radioButtonOtherStorage.Checked;
            if (radioButtonOtherStorage.Checked) listViewBlobs.Items.Clear();
            listViewFiles.Items.Clear();
            buttonUpload.Enabled = false;
        }

        private void radioButtonSelectedAsset_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            ConnectToStorage();
            DoListBlobs(true);
        }

        private void UploadFromBlob_SizeChanged(object sender, EventArgs e)
        {

        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonUpload.Enabled = !(listViewFiles.SelectedItems.Count == 0);
        }

        private void listViewBlobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedBlobContainer = "";
            if (listViewBlobs.SelectedIndices.Count == 1)
            {
                DisplayFilesOfContainer(listViewBlobs.SelectedItems[0].Text);
                SelectedBlobContainer = listViewBlobs.SelectedItems[0].Text;
                buttonUpload.Enabled = false;
            }
        }



        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DoListBlobs(false);
        }
             
    }
}
