//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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


using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using Microsoft.Azure.Storage;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class NewAsset : Form
    {
        private readonly AMSClientV3 _amsClientV3;

        public string StorageSelected => comboBoxStorage.Enabled ? ((Item)comboBoxStorage.SelectedItem).Value : null;

        public string AssetName
        {
            get => textBoxAssetName.Text;
            set => textBoxAssetName.Text = value;
        }

        public string AssetDescription
        {
            get => string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text;
            set => textBoxDescription.Text = value;
        }

        public string AssetAltId
        {
            get => string.IsNullOrWhiteSpace(textBoxAltId.Text) ? null : textBoxAltId.Text;
            set => textBoxAltId.Text = value;
        }

        public string AssetContainer
        {
            get => string.IsNullOrWhiteSpace(textBoxContainer.Text) ? null : textBoxContainer.Text;
            set => textBoxContainer.Text = value;
        }


        public NewAsset(AMSClientV3 amsClient, bool displayAsAdvancedOptionWhenUpload = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClientV3 = amsClient;

            if (displayAsAdvancedOptionWhenUpload)
            {
                labelStorage.Visible = comboBoxStorage.Visible = labelStorage.Enabled = comboBoxStorage.Enabled = false;
                labelNewAsset.Text = "Asset creation options";
                buttonOk.Text = "Ok";
            }
            else
            {
                Task.Run(async () => await ControlsResetToDefaultAsync());
            }
        }

        private async Task ControlsResetToDefaultAsync()
        {
            IList<MediaServicesStorageAccount> storAccounts = _amsClientV3.AMSclient.Data.StorageAccounts;

            comboBoxStorage.Invoke(c => c.Items.Clear());

            //comboBoxStorage.Items.Clear();
            foreach (var storage in storAccounts)
            {
                string sname = AMSClientV3.GetStorageName(storage.Id);
                bool primary = (storage.AccountType == MediaServicesStorageAccountType.Primary);
                comboBoxStorage.Invoke(c => c.Items.Add(new Item(string.Format("{0} {1}", sname, primary ? "(primary)" : string.Empty), sname)));
                //comboBoxStorage.Items.Add(new Item(string.Format("{0} {1}", sname, primary ? "(primary)" : string.Empty), sname));
                if (primary)
                {
                    comboBoxStorage.Invoke(c => c.SelectedIndex = comboBoxStorage.Items.Count - 1);
                    //comboBoxStorage.SelectedIndex = comboBoxStorage.Items.Count - 1;
                }
            }
        }

        private void NewAsset_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelNewAsset, e);
        }

        private void NewAsset_Load(object sender, System.EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }

        private void TextBoxAssetName_TextChanged(object sender, System.EventArgs e)
        {
            checkAssetName();
        }

        private void checkAssetName()
        {
            TextBox tb = textBoxAssetName;

            if (!IsAssetNameValid(tb.Text))
            {
                errorProvider1.SetError(tb, "Asset name is not valid.");
            }
            else
            {
                errorProvider1.SetError(tb, string.Empty);
            }
        }

        internal static bool IsAssetNameValid(string name)
        {
            Regex reg = new(@"[<>%&:\\?/*+.']", RegexOptions.Compiled);
            return (name.Length > 0 && name.Length < 261 && !reg.IsMatch(name));

        }

        private void TextBoxContainer_TextChanged(object sender, System.EventArgs e)
        {
            checkContainerName();
        }

        private void checkContainerName()
        {
            TextBox tb = textBoxContainer;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                // it's ok to have it empty
                errorProvider1.SetError(tb, string.Empty); ;
                return;
            }

            try
            {
                NameValidator.ValidateContainerName(tb.Text);
                errorProvider1.SetError(tb, string.Empty);
            }
            catch
            {
                errorProvider1.SetError(tb, "Asset name is not valid.");
            }
        }

        private void NewAsset_Shown(object sender, System.EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}