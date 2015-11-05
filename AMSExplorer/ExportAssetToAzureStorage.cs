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
using System.Text.RegularExpressions;

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
    public partial class ExportAssetToAzureStorage : Form
    {

        private static CloudMediaContext contextUpload = null;
        private static string MediaServicesStorageAccountKey;
        private CloudStorageAccount storageAccount;
        private CloudBlobClient cloudBlobClient;
        private List<CloudBlobContainer> ListContainers = new List<CloudBlobContainer>();

        private List<IAssetFile> listassetfiles = new List<IAssetFile>();
        public string SelectedContainer = null;
        public List<IAssetFile> SelectedAssetFiles = new List<IAssetFile>();
        public bool useDefaultStorage;
        public bool createNewContainer;
        private bool ErrorConnect = false;
        private IEnumerable<CloudBlobContainer> mediaBlobContainers;
        private string myStorageSuffix;

        public bool BlobStorageDefault
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
        public string BlobLabelDefaultStorage
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
        public string BlobLabelWarning
        {
            get
            {
                return labelWarning.Text;
            }
            set
            {
                labelWarning.Text = value;
            }
        }

        public string BlobOtherStorageName
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

        public string BlobOtherStorageKey
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

        public bool BlobCreateNewContainer
        {
            get
            {
                return radioButtonNewContainer.Checked;
            }
            set
            {
                radioButtonNewContainer.Checked = value;
            }
        }

        public string BlobNewContainerName
        {
            get
            {
                return textBoxNewContainerName.Text;
            }
            set
            {
                textBoxNewContainerName.Text = value;
            }
        }


        public ExportAssetToAzureStorage(CloudMediaContext contextUploadArg, string MediaServicesStorageAccountKeyArg, IAsset sourceAsset, string StorageSuffix)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MediaServicesStorageAccountKey = MediaServicesStorageAccountKeyArg;
            contextUpload = contextUploadArg;

            // list asset files ///////////////////////
            bool bfileinasset = (sourceAsset.AssetFiles.Count() == 0) ? false : true;
            listViewAssetFiles.Items.Clear();
            if (bfileinasset)
            {
                listViewAssetFiles.BeginUpdate();
                foreach (IAssetFile file in sourceAsset.AssetFiles)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    if (file.IsPrimary) item.ForeColor = Color.Blue;
                    item.SubItems.Add(file.LastModified.ToLocalTime().ToString("G"));
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    (listViewAssetFiles.Items.Add(item)).Selected = true;
                    listassetfiles.Add(file);
                }

                listViewAssetFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewAssetFiles.EndUpdate();

            }
            myStorageSuffix = StorageSuffix;


        }

        private void UploadFromBlob_Load(object sender, EventArgs e)
        {
            labelContName.Text = string.Empty;
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
            listViewAssetFiles.Tag = -1;
            listViewAssetFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);

            listViewBlobs.Tag = -1;
            listViewBlobs.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);

        }

        private void ConnectToStorage()
        {
            try
            {
                if (radioButtonStorageDefault.Checked)
                {
                    storageAccount = new CloudStorageAccount(new StorageCredentials(contextUpload.DefaultStorageAccount.Name, MediaServicesStorageAccountKey), myStorageSuffix, true);
                }
                else
                {
                    storageAccount = new CloudStorageAccount(new StorageCredentials(textBoxStorageName.Text, textBoxStorageKey.Text), myStorageSuffix ,true);
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
        }

        private void DoListBlobs(bool ResetSearch)
        {
            // Fill the list of Blobs
            listViewBlobs.Items.Clear();
            ListContainers.Clear();
            listViewBlobs.BeginUpdate();

            if (ResetSearch) textBoxSearch.Text = "";

            try
            {
                foreach (CloudBlobContainer BlobContainer in mediaBlobContainers)
                {
                    if (BlobContainer.Name.Contains(textBoxSearch.Text))
                    {
                        ListViewItem item = new ListViewItem(Path.GetFileName(BlobContainer.Name), 0);
                        item.SubItems.Add(BlobContainer.Properties.LastModified.Value.UtcDateTime.ToLocalTime().ToString("G"));
                        listViewBlobs.Items.Add(item);
                        ListContainers.Add(BlobContainer);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There is a problem when connecting to the storage account");
                ErrorConnect = true;
                return;
            }

            listViewBlobs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewBlobs.EndUpdate();
            buttonExport.Enabled = false;

        }


        private void DisplayFilesOfContainer(string containerName)
        {
            CloudBlobContainer Container = cloudBlobClient.ListContainers().Where(n => n.Name == containerName).FirstOrDefault();
            IEnumerable<IListBlobItem> mediaBlobs = Container.ListBlobs();

            listViewFiles.Items.Clear();

            foreach (IListBlobItem b in mediaBlobs)
            {
                if (b.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob cloudBlockBlob = b as CloudBlockBlob;
                    string lastModified = "";
                    ListViewItem item = new ListViewItem(Path.GetFileName(b.Uri.ToString()), 0);
                    lastModified = cloudBlockBlob.Properties.LastModified.Value.UtcDateTime.ToLocalTime().ToString("G");
                    item.SubItems.Add(lastModified);
                    item.SubItems.Add(AssetInfo.FormatByteSize(cloudBlockBlob.Properties.Length));
                    listViewFiles.Items.Add(item);
                }
            }
            listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }


        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (radioButtonSelectedContainer.Checked) SelectedContainer = listViewBlobs.SelectedItems[0].Text;
            this.SelectedAssetFiles.Clear();
            foreach (ListViewItem item in listViewAssetFiles.SelectedItems)
            {
                // let's find the file as control has perhaps been sorted
                IAssetFile fitem = listassetfiles.Where(l => l.Name == item.Text).FirstOrDefault();
                this.SelectedAssetFiles.Add(fitem);
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

        private void radioButtonNewContainer_CheckedChanged(object sender, EventArgs e)
        {
            this.createNewContainer = radioButtonNewContainer.Checked;
            textBoxNewContainerName.Enabled = radioButtonNewContainer.Checked;
            UpdateButtonUploadEnable();
        }

        internal static bool IsBlobContainerNameValid(string name)
        {
            if (name.Equals("$root"))
            {
                return true;
            }
            Regex reg = new Regex("^(?-i)(?:[a-z0-9]|(?<=[0-9a-z])-(?=[0-9a-z])){3,63}$", RegexOptions.Compiled);
            if (reg.IsMatch(name))
            {
                return true;
            }
            return false;
        }

        private void UpdateButtonUploadEnable()
        {
            bool iscontainerok = false;
            labelContName.Text = string.Empty;

            if (radioButtonNewContainer.Checked)
            {
                if (textBoxNewContainerName.TextLength > 0)
                {
                    iscontainerok = IsBlobContainerNameValid(textBoxNewContainerName.Text);
                    if (!iscontainerok) labelContName.Text = "Container name is not valid";
                }
            }
            if (radioButtonSelectedContainer.Checked) iscontainerok = (listViewBlobs.SelectedIndices.Count > 0);
            buttonExport.Enabled = (listViewAssetFiles.SelectedItems.Count > 0 && iscontainerok);
        }

        private void radioButtonOtherStorage_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStorageName.Enabled = radioButtonOtherStorage.Checked;
            textBoxStorageKey.Enabled = radioButtonOtherStorage.Checked;
            listViewFiles.Items.Clear();
            UpdateButtonUploadEnable();
        }

        private void radioButtonSelectedContainer_CheckedChanged(object sender, EventArgs e)
        {
            listViewBlobs.Enabled = radioButtonSelectedContainer.Checked;
            textBoxSearch.Enabled = radioButtonSelectedContainer.Checked;
            listViewFiles.Enabled = radioButtonSelectedContainer.Checked;
            UpdateButtonUploadEnable();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            ConnectToStorage();
            DoListBlobs(true);
        }

        private void UploadFromBlob_SizeChanged(object sender, EventArgs e)
        {

        }


        private void listViewBlobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewBlobs.SelectedIndices.Count == 1)
            {
                DisplayFilesOfContainer(listViewBlobs.SelectedItems[0].Text);
            }
            UpdateButtonUploadEnable();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DoListBlobs(false);
        }

        private void textBoxNewAssetName_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonUploadEnable();
        }

        private void listViewAssetFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
