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
    public partial class DownloadToLocal : Form
    {
        private IEnumerable<IAsset> _selassets;
        private string _backupfolder;

        public string FolderPath
        {
            get
            {
                return textBoxFolderPath.Text;
            }
            set
            {
                textBoxFolderPath.Text = value;
            }
        }

        public DownloadToFolderOption FolderOption
        {
            get
            {
                DownloadToFolderOption option = DownloadToFolderOption.DoNotCreateSubfolder;
                if (checkBoxCreateSubfolder.Checked)
                {
                    option = radioButtonAssetName.Checked ? DownloadToFolderOption.SubfolderAssetName : DownloadToFolderOption.SubfolderAssetId;
                }
                return option;
            }
            
        }

        public bool OpenFolderAfterDownload
        {
            get
            {
                return checkBoxOpenFileAfterExport.Checked;
            }

        }

        public DownloadToLocal(IEnumerable<IAsset> selassets, string backupfolder)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _selassets = selassets;
            _backupfolder = backupfolder;
        }

        private void DownloadToLocal_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_backupfolder) ||  !File.Exists(_backupfolder))
            {
                textBoxFolderPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            }
            labelAssetName.Text = string.Format(labelAssetName.Text, _selassets.Count(), _selassets.Count() > 1 ? "s" : "");
        }
      
        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxFolderPath.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
        }
    }
}
