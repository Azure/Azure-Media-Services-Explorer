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
    public partial class MediaAnalyticsPickVideoFileInAsset : Form
    {
        private IAsset myAsset;
        List<IAssetFile> selectableFiles;
        private string[] _mediaFileExtensions;

        public IAssetFile SelectedAssetFile
        {
            get
            {
                if (listViewFiles.SelectedIndices.Count > 0)
                {
                    return selectableFiles.Skip(listViewFiles.SelectedIndices[0]).Take(1).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }


        public MediaAnalyticsPickVideoFileInAsset(IAsset asset, string[] mediaFileExtensions, bool displayPrimaryText)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            myAsset = asset;
            _mediaFileExtensions = mediaFileExtensions;
            labelMakeItPrimary.Visible = displayPrimaryText;
        }

        private void MediaAnalyticsPickVideoFileInAsset_Load(object sender, EventArgs e)
        {
            ListAssetFiles();
        }

        private void ListAssetFiles()
        {
            listViewFiles.Items.Clear();

            selectableFiles = myAsset.AssetFiles.ToList().Where(f => IsVideoFile(f.Name)).ToList();

            if (selectableFiles.Count() > 0)
            {
                listViewFiles.BeginUpdate();
                foreach (IAssetFile file in selectableFiles)
                {
                    ListViewItem item = new ListViewItem(file.Name, 0);
                    if (file.IsPrimary) item.ForeColor = Color.Blue;
                    item.SubItems.Add(AssetInfo.FormatByteSize(file.ContentFileSize));
                    listViewFiles.Items.Add(item);
                }
                listViewFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewFiles.EndUpdate();
                listViewFiles.Items[0].Selected = true;
            }
        }

        private bool IsVideoFile(string filename)
        {
            return (_mediaFileExtensions.Contains(Path.GetExtension(filename).ToUpperInvariant()));
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSelect.Enabled = listViewFiles.SelectedItems.Count > 0;
        }
    }
}
