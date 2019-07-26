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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class ImportHttp : Form
    {
        private readonly bool _AzureStorageContainerSASListMode;
        private readonly AMSClientV3 _amsClientV3;
        private readonly string _uniqueness;

        public Uri GetURL
        {
            get => new Uri(textBoxURL.Text);

            set => textBoxURL.Text = value.ToString();
        }

        public string GetAssetName => textBoxAssetName.Text;

        public string GetAssetDescription => textBoxDescription.Text;

        public string StorageSelected => ((Item)comboBoxStorage.SelectedItem).Value;

        public ImportHttp(AMSClientV3 amsClient, bool AzureStorageContainerSASListMode = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            _AzureStorageContainerSASListMode = AzureStorageContainerSASListMode;

            _amsClientV3 = amsClient;
            _uniqueness = Guid.NewGuid().ToString().Substring(0, 13);
        }

        private void ImportHttp_Load(object sender, EventArgs e)
        {
            labelURLFileNameWarning.Text = string.Empty;
            textBoxAssetName.Text = "import-" + _uniqueness;

            _amsClientV3.RefreshTokenIfNeeded();


            if (_AzureStorageContainerSASListMode)
            {
                labelExamples.Visible = false;
                labelSASListExample.Visible = true;
                labelTitle.Text = Text = AMSExplorer.Properties.Resources.ImportHttp_ImportHttp_Load_ImportFromSASContainerPath;
            }

            System.Collections.Generic.IList<StorageAccount> storAccounts = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).StorageAccounts;

            comboBoxStorage.Items.Clear();
            foreach (StorageAccount storage in storAccounts)
            {
                string sname = AMSClientV3.GetStorageName(storage.Id);
                bool primary = (storage.Type == StorageAccountType.Primary);
                comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", sname, primary ? "(primary)" : string.Empty), sname));
                if (primary)
                {
                    comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
                }
            }
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
            bool Error = false;
            try
            {
                Uri myUri = GetURL;
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = AMSExplorer.Properties.Resources.ImportHttp_textBoxURL_TextChanged_ErrorDetectedInTheURL;
                buttonImport.Enabled = false;
                return;
            }

            buttonImport.Enabled = true;

            if (!Error)
            {
                labelURLFileNameWarning.Text = string.Empty;
                textBoxDescription.Text = "Imported from : " + GetURL.AbsoluteUri;
            }
        }
    }
}