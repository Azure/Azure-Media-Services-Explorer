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


using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;

namespace AMSExplorer
{
    public partial class EncodingAMEStandardPickOverlay : Form
    {
        private IAsset myAsset;
        List<IAssetFile> overlayfiles;

        public IAssetFile SelectedAssetFile
        {
            get
            {
                if (listViewFiles.SelectedIndices.Count > 0)
                {
                    return overlayfiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }


        public EncodingAMEStandardPickOverlay(IAsset asset)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            myAsset = asset;
        }

        private void EncodingAMEStandardPickOverlay_Load(object sender, EventArgs e)
        {
            ListAssetFiles();
        }

        private void ListAssetFiles()
        {
            listViewFiles.Items.Clear();

            overlayfiles = myAsset.AssetFiles.ToList().Where(f => IsOverlayFile(f.Name)).ToList();

            if (overlayfiles.Count() > 0)
            {
                listViewFiles.BeginUpdate();
                foreach (IAssetFile file in overlayfiles)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    if (file.IsPrimary)
                    {
                        item.ForeColor = Color.Blue;
                    }
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    listViewFiles.Items.Add(item);
                }
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewFiles.EndUpdate();
            }
        }

        private bool IsOverlayFile(string filename)
        {
            var mediaFileExtensions = new[] { ".PNG", ".JPG", ".GIF", ".BMP", ".MP4" };
            return (mediaFileExtensions.Contains(Path.GetExtension(filename).ToUpperInvariant()));
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSelect.Enabled = listViewFiles.SelectedItems.Count > 0;
        }

        private async void DoUpload()
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Multiselect = true;
            Dialog.Filter = "Image Files|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
          
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                progressBarUpload.Maximum = 100 * Dialog.FileNames.Count();
                progressBarUpload.Visible = true;
                buttonSelect.Enabled = false;
                buttonUpload.Enabled = false;
                foreach (string file in Dialog.FileNames)
                {
                    await Task.Factory.StartNew(() => ProcessUploadFileToAsset(Path.GetFileName(file), file, myAsset));
                }
                // Refresh the asset.
                myAsset = Mainform._context.Assets.Where(a => a.Id == myAsset.Id).FirstOrDefault();
                progressBarUpload.Visible = false;
                buttonSelect.Enabled = true;
                buttonUpload.Enabled = true;
                ListAssetFiles();
            }
        }

        private void ProcessUploadFileToAsset(string SafeFileName, string FileName, IAsset MyAsset)
        {
            try
            {
                IAssetFile UploadedAssetFile = MyAsset.AssetFiles.Create(SafeFileName);
                UploadedAssetFile.UploadProgressChanged += MyUploadProgressChanged;
                UploadedAssetFile.Upload(FileName as string);
            }
            catch
            {
                MessageBox.Show("Error when uploading the file");
            }
        }

        private void MyUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            try
            {
                progressBarUpload.BeginInvoke(new Action(() => progressBarUpload.Value = (int)e.Progress), null);
            }
            catch
            {

            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            DoUpload();
        }
    }
}
