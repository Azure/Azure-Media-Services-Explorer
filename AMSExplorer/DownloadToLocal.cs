//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DownloadToLocal : Form
    {
        private readonly IEnumerable<Asset> _selassets;
        private readonly string _backupfolder;

        public string FolderPath
        {
            get => textBoxFolderPath.Text;
            set => textBoxFolderPath.Text = value;
        }

        public DownloadToFolderOption FolderOption => checkBoxCreateSubfolder.Checked ? DownloadToFolderOption.SubfolderAssetName : DownloadToFolderOption.DoNotCreateSubfolder;

        public bool OpenFolderAfterDownload => checkBoxOpenFileAfterExport.Checked;

        public DownloadToLocal(IEnumerable<Asset> selassets, string backupfolder)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _selassets = selassets;
            _backupfolder = backupfolder;
        }

        private void DownloadToLocal_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            /*
            if (string.IsNullOrEmpty(_backupfolder) || !Directory.Exists(_backupfolder))
            {
                textBoxFolderPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            }
            else
            {
                textBoxFolderPath.Text = _backupfolder;
            }
            */
            labelAssetName.Text = string.Format(labelAssetName.Text, _selassets.Count());
        }

        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog() { RootFolder = Environment.SpecialFolder.MyVideos };
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFolderPath.Text = openFolderDialog.SelectedPath;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
        }

        private void DownloadToLocal_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }
    }
}
