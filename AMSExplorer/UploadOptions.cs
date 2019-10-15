﻿//----------------------------------------------------------------------------------------------
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
using Microsoft.Azure.Management.Storage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class UploadOptions : Form
    {
        private readonly AMSClientV3 _amsClientV3;
        private readonly bool _multifilesMode;
        private NewAsset newAssetForm = null;

        public string StorageSelected => ((Item)comboBoxStorage.SelectedItem).Value;

        public bool SingleAsset => radioButtonSingleAsset.Checked;

        public int BlockSize
        {
            get
            {
                bool success = int.TryParse(comboBoxBlockSize.Text, out int x);
                return success ? x : 4;
            }
        }

        public NewAsset assetCreationSetting
        {
            get
            {
                if (_multifilesMode) return null;
                return newAssetForm;
            }
        }


        public UploadOptions(AMSClientV3 amsClient, bool multifilesMode)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClientV3 = amsClient;
            _multifilesMode = multifilesMode;

            Task.Run(() => ControlsResetToDefaultAsync()).GetAwaiter().GetResult();

            if (multifilesMode)
            {
                groupBoxMultifiles.Visible = true;
                buttonAdvancedOptions.Visible = false;
            }

            List<int> listInt = new List<int>() { 1, 2, 4, 8, 16, 32, 64 };
            comboBoxBlockSize.Items.Clear();
            listInt.ForEach(l => comboBoxBlockSize.Items.Add(l.ToString()));
            comboBoxBlockSize.SelectedIndex = 3;
        }

        private async Task ControlsResetToDefaultAsync()
        {
            _amsClientV3.RefreshTokenIfNeeded();
            var storAccounts = (await _amsClientV3.AMSclient.Mediaservices.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName)).StorageAccounts;

            comboBoxStorage.Items.Clear();
            foreach (Microsoft.Azure.Management.Media.Models.StorageAccount storage in storAccounts)
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

        private void UploadOptions_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelJobOptions, e);
        }

        private void UploadOptions_Load(object sender, System.EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);
        }

        private void ButtonAdvancedOptions_Click(object sender, System.EventArgs e)
        {
            string altid = null, assetName = null, desc = null, container = null;

            if (newAssetForm == null)
            {
                string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);
                newAssetForm = new NewAsset(_amsClientV3, true) { AssetName = "upload-" + uniqueness };
            }
            else
            {
                //let's backup settings
                altid = newAssetForm.AssetAltId;
                desc = newAssetForm.AssetDescription;
                container = newAssetForm.AssetContainer;
            }
            assetName = newAssetForm.AssetName;


            if (newAssetForm.ShowDialog() != DialogResult.OK)
            {
                newAssetForm.AssetAltId = altid;
                newAssetForm.AssetName = assetName;
                newAssetForm.AssetDescription = desc;
                newAssetForm.AssetContainer = container;
            }
        }
    }
}