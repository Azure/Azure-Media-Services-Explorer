//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Web;

namespace AMSExplorer
{
    public partial class BulkContainerInfo : Form
    {
        private CloudMediaContext MyContext;
        private Mainform MyMainForm;
        private IIngestManifest _manifest;

        public string IngestManifestName
        {
            get { return textBoxName.Text; }
        }


        public BulkContainerInfo(Mainform mainform, CloudMediaContext context, IIngestManifest manifest)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            MyMainForm = mainform;
            MyContext = context;
            _manifest = manifest;
        }

        private void contextMenuStripDG_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            DataGridView DG = (DataGridView)contextmenu.SourceControl;

            if (DG.SelectedCells.Count == 1)
            {
                if (DG.SelectedCells[0].Value != null)
                {
                    System.Windows.Forms.Clipboard.SetText(DG.SelectedCells[0].Value.ToString());
                }
                else
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
            }
        }


        private void contextMenuStripDG_Opening(object sender, CancelEventArgs e)
        {

        }


        private void BulkContainerInfo_Load(object sender, EventArgs e)
        {
            labelPBulkName.Text += _manifest.Name;

            DGBulkManifest.ColumnCount = 2;
            DGBulkManifest.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            DGAssetManifest.ColumnCount = 2;
            DGAssetManifest.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            // Bulk manifest info
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_BulkContainerInfo_Load_BlobStorageURLForUpload, _manifest.BlobStorageUriForUpload);
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, _manifest.Name);
            DGBulkManifest.Rows.Add("Id", _manifest.Id);
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, (IngestManifestState)_manifest.State);
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)_manifest.Created).ToLocalTime().ToString("G"));
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)_manifest.LastModified).ToLocalTime().ToString("G"));
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_StorageAccountName, _manifest.StorageAccountName);
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_BulkContainerInfo_Load_PendingFilesCount, _manifest.Statistics.PendingFilesCount);
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_BulkContainerInfo_Load_FinishedFilesCount, _manifest.Statistics.FinishedFilesCount);
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_BulkContainerInfo_Load_ErrorFilesCount, _manifest.Statistics.ErrorFilesCount);
            DGBulkManifest.Rows.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_BulkContainerInfo_Load_ErrorFilesDetails, _manifest.Statistics.ErrorFilesDetails);
            ListAssetManifests();
            textBoxName.Text = _manifest.Name;
        }

        private void ListAssetManifests()
        {
            listViewAssetManifests.Items.Clear();
            DGAssetManifest.Rows.Clear();
            listViewAssetManifests.BeginUpdate();
            foreach (var im in _manifest.IngestManifestAssets)
            {
                ListViewItem item = new ListViewItem( im.Asset.Name, 0);
                listViewAssetManifests.Items.Add(item);
            }
            listViewAssetManifests.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewAssetManifests.EndUpdate();
        }

        private void listViewAssetManifest_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bSelect = listViewAssetManifests.SelectedItems.Count > 0 ? true : false;
            DoDisplayAssetManifest();
        }

        private void DoDisplayAssetManifest()
        {
            if (listViewAssetManifests.SelectedItems.Count > 0)
            {
                var am = _manifest.IngestManifestAssets.Skip(listViewAssetManifests.SelectedIndices[0]).Take(1).FirstOrDefault();

                DGAssetManifest.Rows.Clear();
                DGAssetManifest.Rows.Add("Id", am.Id);
                DGAssetManifest.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, am.Created);
                DGAssetManifest.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, am.LastModified.ToLocalTime().ToString("G"));
                DGAssetManifest.Rows.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_DoDisplayAssetManifest_AssetName, am.Asset.Name);
                DGAssetManifest.Rows.Add(AMSExplorer.Properties.Resources.BulkContainerInfo_DoDisplayAssetManifest_AssetId, am.Asset.Id);


                var numberpending = am.IngestManifestFiles.AsEnumerable().Count(f => f.State == IngestManifestFileState.Pending);
                var numbererror = am.IngestManifestFiles.AsEnumerable().Count(f => f.State == IngestManifestFileState.Error);
                var numberfinished = am.IngestManifestFiles.AsEnumerable().Count(f => f.State == IngestManifestFileState.Finished);
                DGAssetManifest.Rows.Add("Pending files count", numberpending);
                DGAssetManifest.Rows.Add("Error files count", numbererror);
                DGAssetManifest.Rows.Add("Finished files count", numberfinished);

                int i = 0;
                foreach (var f in am.IngestManifestFiles)
                {
                    DGAssetManifest.Rows.Add(string.Format(AMSExplorer.Properties.Resources.BulkContainerInfo_DoDisplayAssetManifest_File0Name, i), f.Name);
                    DGAssetManifest.Rows.Add(string.Format(AMSExplorer.Properties.Resources.BulkContainerInfo_DoDisplayAssetManifest_File0State, i), (IngestManifestFileState)f.State);
                    DGAssetManifest.Rows.Add(string.Format(AMSExplorer.Properties.Resources.BulkContainerInfo_DoDisplayAssetManifest_File0Encrypted, i), f.IsEncrypted);
                }
            }
        }
    }
}
