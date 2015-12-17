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
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace AMSExplorer
{
    public partial class BulkUpload : Form
    {
        private BindingList<BulkAssetFile> assetFiles = new BindingList<BulkAssetFile>();
        private CloudMediaContext _context;

        public readonly string SigniantGlobalServer = "global-az.cloud.signiant.com";

        public readonly IList<SigniantInfo> SigniantServers = new List<SigniantInfo> {
            new SigniantInfo() {AzureContainerInfo="East US (Virginia)", FlightServer="us-east-az.cloud.signiant.com", FlightRegion="us-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="East US 2 (Virginia)", FlightServer="us-east-2-az.cloud.signiant.com", FlightRegion="us-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Central US (Iowa)", FlightServer="us-central-az.cloud.signiant.com", FlightRegion="us-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="North Central US (Illinois)", FlightServer="us-northcentral-az.cloud.signiant.com", FlightRegion="us-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="South Central US (Texas)", FlightServer="us-southcentral-az.cloud.signiant.com", FlightRegion="us-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="West US (California)", FlightServer="us-west-az.cloud.signiant.com", FlightRegion="us-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="North Europe (Ireland)", FlightServer="eu-north-az.cloud.signiant.com", FlightRegion="eu-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="West Europe (Netherlands)", FlightServer="eu-west-az.cloud.signiant.com", FlightRegion="eu-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="East Asia (Hong Kong)", FlightServer="ap-east-az.cloud.signiant.com", FlightRegion="ap-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Southeast Asia (Singapore)", FlightServer="ap-south-az.cloud.signiant.com", FlightRegion="ap-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Japan East (Tokyo)", FlightServer="jp-east-az.cloud.signiant.com", FlightRegion="jp-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Japan West (Osaka)", FlightServer="jp-west-az.cloud.signiant.com", FlightRegion="jp-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Brazil South (Sao Paulo State)", FlightServer="sa-south-az.cloud.signiant.com", FlightRegion="sa-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Australia East (New South Wales)", FlightServer="aus-east-az.cloud.signiant.com", FlightRegion="aus-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Australia Southeast (Victoria)", FlightServer="aus-southeast-az.cloud.signiant.com", FlightRegion="aus-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="Central India (Pune)", FlightServer="ind-central-az.cloud.signiant.com", FlightRegion="ind-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="South India (Chennai)", FlightServer="ind-south-az.cloud.signiant.com", FlightRegion="ind-az.cloud.signiant.com"},
            new SigniantInfo() {AzureContainerInfo="West India (Mumbai)", FlightServer="ind-west-az.cloud.signiant.com", FlightRegion="ind-az.cloud.signiant.com"}
        };

        public string IngestName
        {
            get
            {
                return textBoxManifestName.Text;
            }
        }

        public List<BulkAsset> AssetFiles
        {
            get
            {
                var myList = new List<BulkAsset>();
                var listofguid = assetFiles.GroupBy(a => a.AssetGuid).ToList();
                foreach (var asset in listofguid)
                {
                    myList.Add(new BulkAsset() { AssetName = asset.FirstOrDefault().AssetName, AssetFiles = asset.Select(a => a.FileName).ToArray() });
                }
                return myList;
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

        public bool GenerateAzCopy
        {
            get
            {
                return checkBoxGenerateAzCopy.Checked;
            }
        }

        public bool GenerateSigniant
        {
            get
            {
                return checkBoxGenerateSigniant.Checked;
            }
        }

        public bool GenerateAspera
        {
            get
            {
                return checkBoxGenerateAspera.Checked;
            }
        }

        public string SigniantAPIKey
        {
            get
            {
                return textBoxSigniantAPIKey.Text;
            }
        }

        public List<string> SigniantServersSelected
        {
            get
            {
                var servers = new List<string>();
                string regionname = (string) comboBoxSigniantServer.SelectedItem;
                var flightentry = SigniantServers.Where(s => s.AzureContainerInfo == regionname).FirstOrDefault();
                servers.Add(flightentry.FlightServer);
                servers.Add(SigniantGlobalServer);

                return servers;
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
            dataGridAssetFiles.Columns["AssetGuid"].Visible = false;
            dataGridAssetFiles.Columns["AssetIndex"].ReadOnly = true;
            dataGridAssetFiles.Columns["FileName"].ReadOnly = true;
            labelWarningFiles.Text = "";

            foreach (var server in SigniantServers)
            {
                comboBoxSigniantServer.Items.Add(server.AzureContainerInfo);
            }
            comboBoxSigniantServer.SelectedIndex = 0;

            linkLabelSigniantRequestKey.Links.Add(new LinkLabel.Link(0, linkLabelSigniantRequestKey.Text.Length, Constants.LinkSigniantFlightRequestTrialKey));
            linklabelSigniantMarket.Links.Add(new LinkLabel.Link(0, linklabelSigniantMarket.Text.Length, Constants.LinkSigniantFlightMarketPlace));
            linkLabelInfoAzCopy.Links.Add(new LinkLabel.Link(0, linkLabelInfoAzCopy.Text.Length, Constants.LinkMoreInfoAzCopy));
            linkLabelAspera.Links.Add(new LinkLabel.Link(0, linkLabelAspera.Text.Length, Constants.LinkAspera));

        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
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

                ReindexAssetListAndDoSomeChecks();
            }
        }

        private void ReindexAssetListAndDoSomeChecks()
        {
            // reindex
            int index = -1;
            Guid g = Guid.NewGuid();
            string assetname = null;
            foreach (var f in assetFiles)
            {
                if (f.AssetGuid != g)
                {
                    index++;
                    assetname = f.AssetName;
                }
                else
                {
                    f.AssetName = assetname; // let's make sure all asset files from the same asset have the same asset name
                }
                f.AssetIndex = index;

                g = f.AssetGuid;
            }



            // let's check filename duplicates
            var listfilenames = assetFiles.Select(a => Path.GetFileName(a.FileName)).Distinct().ToList();
            if (listfilenames.Count != assetFiles.Count)
            {
                labelWarningFiles.Text = "Warning : two files have the same file name. This is not supported inside the same bulk ingest container.";
            }
            else
            {
                labelWarningFiles.Text = "";
            }
        }


        class BulkAssetFile : INotifyPropertyChanged
        {
            private Guid _guid;
            public Guid AssetGuid
            {
                get { return _guid; }
                set { _guid = value; }
            }

            private int _index;
            public int AssetIndex
            {
                get { return _index; }
                set
                {
                    if (value != _index)
                    {
                        _index = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            private string _assetname;
            public string AssetName
            {
                get { return _assetname; }
                set
                {
                    if (value != _assetname)
                    {
                        _assetname = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            private string _fileName;
            public string FileName
            {
                get { return _fileName; }
                set
                {
                    if (value != _fileName)
                    {
                        _fileName = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged([CallerMemberName] String p = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(p));
                }
            }

        }

        public class BulkAsset
        {
            public string[] AssetFiles;
            public string AssetName;
        }


        private void buttonSelectFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialogAssetFiles.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openFileDialogAssetFiles.FileNames)
                {
                    assetFiles.Add(new BulkAssetFile() { AssetName = Path.GetFileName(file), FileName = file });
                }
                if (string.IsNullOrWhiteSpace(textBoxFolderPath.Text))
                {
                    FileInfo file = new FileInfo(assetFiles[0].FileName);
                    textBoxFolderPath.Text = Path.Combine(file.Directory.Parent.FullName, file.Directory.Name + "_Encrypted");
                }
            }
            ReindexAssetListAndDoSomeChecks();
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

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(textBoxFolderPath.Text))
                {
                    textBoxFolderPath.Text = folderBrowserDialog1.SelectedPath + @"_Encrypted";
                }

                var folders = Directory.GetDirectories(folderBrowserDialog1.SelectedPath).ToList();
                var files = Directory.GetFiles(folderBrowserDialog1.SelectedPath).ToList();

                if (files.Count > 0)
                {
                    Guid g = Guid.NewGuid();
                    foreach (var file in files)
                    {
                        assetFiles.Add(new BulkAssetFile() { AssetGuid = g, AssetName = Path.GetFileName(folderBrowserDialog1.SelectedPath), FileName = file });
                    }
                }

                folders.RemoveAll(f => Directory.GetFiles(f).Count() == 0); // we remove all folder with 0 file in it at the root

                foreach (var folder in folders)
                {
                    var filesinfolder = Directory.GetFiles(folder).ToList();

                    if (filesinfolder.Count > 0)
                    {
                        Guid g = Guid.NewGuid();
                        string thisassetname = Path.GetFileNameWithoutExtension(filesinfolder[0]);
                        foreach (var file in filesinfolder)
                        {
                            assetFiles.Add(new BulkAssetFile() { AssetGuid = g, AssetName = Path.GetFileName(folder), FileName = file });
                        }
                    }
                }
                ReindexAssetListAndDoSomeChecks();
            }
        }

        private void dataGridAssetFiles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridAssetFiles.Columns["AssetName"].Index)  // user edited the asset name
            {
                string newAssetName = assetFiles[e.RowIndex].AssetName;
                var g = assetFiles[e.RowIndex].AssetGuid;
                var asset = assetFiles.Where(a => a.AssetGuid == g);
                foreach (var f in asset)
                {
                    f.AssetName = newAssetName;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            assetFiles.Clear();
        }

        private void buttonGroupSelectionInOneAsset_Click(object sender, EventArgs e)
        {
            if (dataGridAssetFiles.SelectedRows.Count > 0)
            {
                Guid g = Guid.NewGuid();

                foreach (DataGridViewRow row in dataGridAssetFiles.SelectedRows)
                {
                    assetFiles[row.Index].AssetGuid = g;
                }

                ReindexAssetListAndDoSomeChecks();
            }
        }

        private void buttonSplitSelection_Click(object sender, EventArgs e)
        {
            if (dataGridAssetFiles.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridAssetFiles.SelectedRows)
                {
                    assetFiles[row.Index].AssetGuid = Guid.NewGuid();
                }

                ReindexAssetListAndDoSomeChecks();
            }
        }

        private void linklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);

        }
    }

    public class SigniantInfo
    {
        public string AzureContainerInfo { get; set; }
        public string FlightServer { get; set; }
        public string FlightRegion { get; set; }

    }
}
