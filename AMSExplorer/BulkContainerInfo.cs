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
            DGBulkManifest.Rows.Add("Blob Storage URL For Upload", _manifest.BlobStorageUriForUpload);
            DGBulkManifest.Rows.Add("Name", _manifest.Name);
            DGBulkManifest.Rows.Add("Id", _manifest.Id);
            DGBulkManifest.Rows.Add("State", (IngestManifestState)_manifest.State);
            DGBulkManifest.Rows.Add("Created", ((DateTime)_manifest.Created).ToLocalTime().ToString("G"));
            DGBulkManifest.Rows.Add("Last Modified", ((DateTime)_manifest.LastModified).ToLocalTime().ToString("G"));
            DGBulkManifest.Rows.Add("Storage Account Name", _manifest.StorageAccountName);
            DGBulkManifest.Rows.Add("Pending Files Count", _manifest.Statistics.PendingFilesCount);
            DGBulkManifest.Rows.Add("Finished Files Count", _manifest.Statistics.FinishedFilesCount);
            DGBulkManifest.Rows.Add("Error Files Count", _manifest.Statistics.ErrorFilesCount);
            DGBulkManifest.Rows.Add("Error Files Details", _manifest.Statistics.ErrorFilesDetails);
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
        private void labelProgramName_Click(object sender, EventArgs e)
        {

        }


        private void ProgramInformation_Shown(object sender, EventArgs e)
        {

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
                DGAssetManifest.Rows.Add("Created", am.Created);
                DGAssetManifest.Rows.Add("Last Modified", am.LastModified.ToLocalTime().ToString("G"));
                DGAssetManifest.Rows.Add("Asset Name", am.Asset.Name);
                DGAssetManifest.Rows.Add("Asset Id", am.Asset.Id);


                var numberpending = am.IngestManifestFiles.AsEnumerable().Count(f => f.State == IngestManifestFileState.Pending);
                var numbererror = am.IngestManifestFiles.AsEnumerable().Count(f => f.State == IngestManifestFileState.Error);
                var numberfinished = am.IngestManifestFiles.AsEnumerable().Count(f => f.State == IngestManifestFileState.Finished);
                DGAssetManifest.Rows.Add("Pending files count", numberpending);
                DGAssetManifest.Rows.Add("Error files count", numbererror);
                DGAssetManifest.Rows.Add("Finished files count", numberfinished);

                int i = 0;
                foreach (var f in am.IngestManifestFiles)
                {
                    DGAssetManifest.Rows.Add(string.Format("File #{0} Name", i), f.Name);
                    DGAssetManifest.Rows.Add(string.Format("File #{0} State", i), (IngestManifestFileState)f.State);
                    DGAssetManifest.Rows.Add(string.Format("File #{0} Encrypted", i), f.IsEncrypted);
                }
            }
        }
    }
}
