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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class UploadOptionsUI : Form
    {
        private readonly AMSClientV3 _amsClientV3;
        private readonly bool _multifilesMode;
        private NewAsset newAssetForm = null;

        public string StorageSelected => ((Item)comboBoxStorage.SelectedItem).Value;

        public bool SingleAsset => radioButtonSingleAsset.Checked;

        public NewAsset assetCreationSetting
        {
            get
            {
                if (_multifilesMode) return null;
                return newAssetForm;
            }
        }


        public UploadOptionsUI(AMSClientV3 amsClient, bool multifilesMode)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClientV3 = amsClient;
            _multifilesMode = multifilesMode;

            ControlsResetToDefault();

            if (multifilesMode)
            {
                groupBoxMultifiles.Visible = true;
                buttonAdvancedOptions.Visible = false;
            }
        }

        private void ControlsResetToDefault()
        {
            IList<StorageAccount> storAccounts = Task.Run(() => _amsClientV3.AMSclient.Mediaservices.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName)).GetAwaiter().GetResult().StorageAccounts;

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
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelJobOptions, e);
        }

        private void UploadOptions_Load(object sender, System.EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            var sizeText = AssetTools.FormatByteSize(1024 * 1024L * Properties.Settings.Default.DataMovementBlockSize * 50000);
            labelBlockSize.Text = string.Format(labelBlockSize.Text, Properties.Settings.Default.DataMovementBlockSize, sizeText);
        }

        private void ButtonAdvancedOptions_Click(object sender, System.EventArgs e)
        {
            string altid = null, desc = null, container = null;

            if (newAssetForm == null)
            {
                newAssetForm = new NewAsset(_amsClientV3, true)
                {
                    AssetName = "uploaded-" + Constants.NameconvShortUniqueness,
                    AssetDescription = Constants.NameconvFileName,
                    AssetAltId = Constants.NameconvFileName
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

        private void UploadOptionsUI_Shown(object sender, System.EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}