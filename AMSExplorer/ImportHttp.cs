//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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
        private NewAsset newAssetForm = null;


        public Uri GetURL
        {
            get
            {
                try
                {
                    Uri myUri = new(textBoxURL.Text);
                    return myUri;
                }
                catch
                {
                    return null;
                }
            }
            set => textBoxURL.Text = value.ToString();
        }


        public string StorageSelected => ((Item)comboBoxStorage.SelectedItem).Value;

        public NewAsset assetCreationSetting => newAssetForm;

        public ImportHttp(AMSClientV3 amsClient, bool AzureStorageContainerSASListMode = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            _AzureStorageContainerSASListMode = AzureStorageContainerSASListMode;

            _amsClientV3 = amsClient;
            _uniqueness = Program.GetUniqueness();
        }


        private async void ImportHttp_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
            labelURLFileNameWarning.Text = string.Empty;

            if (_AzureStorageContainerSASListMode)
            {
                labelSASListExample.Visible = true;
                labelTitle.Text = Text = AMSExplorer.Properties.Resources.ImportHttp_ImportHttp_Load_ImportFromSASContainerPath;
            }

            System.Collections.Generic.IList<StorageAccount> storAccounts = (await _amsClientV3.AMSclient.Mediaservices.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName)).StorageAccounts;

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
                Uri myUri = new(textBoxURL.Text);
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = AMSExplorer.Properties.Resources.ImportHttp_textBoxURL_TextChanged_ErrorDetectedInTheURL;
                buttonImport.Enabled = false;
            }

            if (!Error)
            {
                buttonImport.Enabled = true;
                labelURLFileNameWarning.Text = string.Empty;
            }
        }

        private void ImportHttp_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }

        private void ButtonAdvancedOptions_Click(object sender, EventArgs e)
        {
            string altid = null, desc = null, container = null;

            if (newAssetForm == null)
            {
                newAssetForm = new NewAsset(_amsClientV3, true)
                {
                    AssetName = "uploaded-" + Constants.NameconvShortUniqueness,
                    AssetDescription = "Imported from : " + Constants.NameconvUrl,
                    AssetAltId = _AzureStorageContainerSASListMode ? string.Empty : Constants.NameconvFileName
                };
            }
            else
            {
                //let's backup settings
                altid = newAssetForm.AssetAltId;
                desc = newAssetForm.AssetDescription;
                container = newAssetForm.AssetContainer;
            }
            string assetName = newAssetForm.AssetName;


            if (newAssetForm.ShowDialog() != DialogResult.OK)
            {
                newAssetForm.AssetAltId = altid;
                newAssetForm.AssetName = assetName;
                newAssetForm.AssetDescription = desc;
                newAssetForm.AssetContainer = container;
            }
        }

        private void ImportHttp_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}