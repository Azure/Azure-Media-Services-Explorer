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
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;


namespace AMSExplorer
{
    public partial class ImportHttp : Form
    {
        private bool _AzureStorageContainerSASListMode;
        private CloudMediaContext _context;

        public Uri GetURL
        {
            get
            {
                return new Uri(textBoxURL.Text);
            }

            set
            {
                textBoxURL.Text = value.ToString();
            }
        }

        public string GetAssetFileName
        {
            get
            {
                return textBoxAssetFileName.Text;
            }

        }

        public string GetAssetName
        {
            get
            {
                return textBoxAssetName.Text;
            }
        }

        public string StorageSelected
        {
            get
            {
                return ((Item)comboBoxStorage.SelectedItem).Value;
            }
        }

        public ImportHttp(CloudMediaContext context, bool AzureStorageContainerSASListMode = false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            _AzureStorageContainerSASListMode = AzureStorageContainerSASListMode;
            _context = context;
        }

        private void ImportHttp_Load(object sender, EventArgs e)
        {
            labelURLFileNameWarning.Text = string.Empty;

            if (_AzureStorageContainerSASListMode)
            {
                label4.Visible = textBoxAssetFileName.Visible = false;
                labelExamples.Visible = false;
                labelSASListExample.Visible = true;
                labelTitle.Text = this.Text = AMSExplorer.Properties.Resources.ImportHttp_ImportHttp_Load_ImportFromSASContainerPath;
            }

            foreach (var storage in _context.StorageAccounts)
            {
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", storage.Name, storage.IsDefault ? AMSExplorer.Properties.Resources.BatchUploadFrame2_BathUploadFrame2_Load_Default : ""), storage.Name));
                if (storage.Name == _context.DefaultStorageAccount.Name) comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
            }
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {

            bool Error = false;
            try
            {
                Uri myUri = this.GetURL;
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = AMSExplorer.Properties.Resources.ImportHttp_textBoxURL_TextChanged_ErrorDetectedInTheURL;
                buttonImport.Enabled = false;
                return;
            }

            buttonImport.Enabled = true;
            if (!_AzureStorageContainerSASListMode)
            {
                string filename = null;
                try
                {
                    filename = System.IO.Path.GetFileName(this.GetURL.LocalPath);
                }
                catch
                {
                    Error = true;
                    labelURLFileNameWarning.Text = AMSExplorer.Properties.Resources.ImportHttp_textBoxURL_TextChanged_FileNameNotFoundInTheURL;
                }
                if (!Error)
                {
                    textBoxAssetName.Text = filename;
                    textBoxAssetFileName.Text = filename;
                }
            }

            if (!Error)
            {
                labelURLFileNameWarning.Text = string.Empty;
            }
        }

        private void textBoxAssetFileName_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (!AssetInfo.AssetFileNameIsOk(tb.Text))
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.ImportHttp_textBoxAssetFileName_TextChanged_AssetFileNameIsNotCompatibleWithMediaServices);
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }
    }
}
