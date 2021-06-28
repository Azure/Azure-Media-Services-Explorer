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
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Azure.Management.Storage;
using Microsoft.Azure.Management.Storage.Models;
using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateAccount : Form
    {
        private List<Microsoft.Azure.Management.ResourceManager.Models.Location> _locations;
        private AzureMediaServicesClient _mediaServicesClient;
        private TokenCredentials _tokenCredentials;
        private string _questionMark;
        private string _checkedMark;
        private MediaService _mediaServiceCreated = null;

        private bool OkAMSAccount = false;
        private bool OkStorageAccount = false;
        private string _uniqueness;

        public string SelectedLocation => (comboBoxAzureLocations.SelectedItem as Item).Value;

        public string AccountName => textBoxAccountName.Text;

        public string ResourceGroupName => textBoxRG.Text;

        public MediaService MediaServiceCreated
        {
            get
            {
                return _mediaServiceCreated;
            }
        }

        public CreateAccount(List<Microsoft.Azure.Management.ResourceManager.Models.Location> locations, AzureMediaServicesClient mediaServicesClient, TokenCredentials tokenCredentials)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _locations = locations;
            _mediaServicesClient = mediaServicesClient;
            _tokenCredentials = tokenCredentials;

            _questionMark = labelOkAMSAccount.Text;
            _checkedMark = (string)labelOkAMSAccount.Tag;

        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            foreach (var loc in _locations)
            {
                comboBoxAzureLocations.Items.Add(new Item(loc.RegionalDisplayName, loc.Name));
            }
            comboBoxAzureLocations.SelectedIndex = 0;

            comboBoxStorageType.Items.Add(new Item("Locally-redundant storage (LRS)", SkuName.StandardLRS));
            comboBoxStorageType.Items.Add(new Item("Read-access geo-redundant storage (RA-GRS)", SkuName.StandardRAGRS));
            comboBoxStorageType.SelectedIndex = 0;

            _uniqueness = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 13); // Create a GUID for uniqueness.

            UpdateDefaultNames();
        }



        private void CreateAccount_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }

        private void textBoxAccountName_TextChanged(object sender, EventArgs e)
        {
            ChangeToAccountNameOrRegion();
        }

        private void buttonCheckAvail_Click(object sender, EventArgs e)
        {
            var availability = _mediaServicesClient.Locations.CheckNameAvailability(
                              type: "Microsoft.Media/mediaservices",
                              locationName: SelectedLocation,
                              name: AccountName);

            buttonCheckAvailAMS.Enabled = false;

            if (!availability.NameAvailable)
            {
                errorProvider1.SetError(textBoxAccountName, availability.Message);
                labelOkAMSAccount.Text = string.Empty;
                OkAMSAccount = false;
            }
            else
            {
                errorProvider1.SetError(textBoxAccountName, string.Empty);
                labelOkAMSAccount.Text = _checkedMark;
                OkAMSAccount = true;
            }

            UpdateButtonCreate();
        }

        private void comboBoxAzureLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeToAccountNameOrRegion();
        }

        private void UpdateDefaultNames()
        {
            textBoxAccountName.Text = "media" + _uniqueness;
            textBoxNewStorageName.Text = "storage" + _uniqueness;
            textBoxRG.Text = "rg-" + _uniqueness;
        }

        private void ChangeToAccountNameOrRegion()
        {
            buttonCheckAvailAMS.Enabled = true;
            labelOkAMSAccount.Text = _questionMark;
            errorProvider1.SetError(textBoxAccountName, string.Empty);
            OkAMSAccount = false;
            UpdateButtonCreate();
        }


        private void ChangeToStorageName()
        {
            buttonCheckAvailStorage.Enabled = true;
            labelOkStorageAccount.Text = _questionMark;
            errorProvider1.SetError(textBoxNewStorageName, string.Empty);
            OkStorageAccount = false;
            UpdateButtonCreate();
        }

        private void UpdateButtonCreate()
        {
            buttonCreate.Enabled = OkAMSAccount && (!checkBoxCreateStorage.Checked || OkStorageAccount);
        }


        private async void buttonNext_Click(object sender, EventArgs e)
        {
            await DoCreationAsync(e);
        }

        private async Task DoCreationAsync(EventArgs e)
        {
            progressBarCreation.Visible = true;
            buttonCreate.Enabled = false;

            try
            {
                if (checkBoxCreateRG.Checked)
                {
                    // create resource group if needed
                    var resourceClient = new ResourceManagementClient(_tokenCredentials) { SubscriptionId = _mediaServicesClient.SubscriptionId };
                    var resourceGroupsClient = resourceClient.ResourceGroups;
                    var resourceGroup = new ResourceGroup(SelectedLocation);
                    resourceGroup = await resourceGroupsClient.CreateOrUpdateAsync(ResourceGroupName, resourceGroup);
                }

                string storageId;
                if (checkBoxCreateStorage.Checked)
                {
                    // New storage account
                    var storageManagementClient = new StorageManagementClient(_tokenCredentials);
                    storageManagementClient.SubscriptionId = _mediaServicesClient.SubscriptionId;

                    StorageAccountCreateParameters parametersStorage = new StorageAccountCreateParameters
                    {
                        Location = SelectedLocation,
                        Sku = new Microsoft.Azure.Management.Storage.Models.Sku { Name = ((Item)comboBoxStorageType.SelectedItem).Value },
                        Kind = Kind.StorageV2,
                    };
                    var newStorage = await storageManagementClient.StorageAccounts.CreateAsync(ResourceGroupName, textBoxNewStorageName.Text, parametersStorage);
                    storageId = newStorage.Id;
                }
                else
                {
                    // existing storage account
                    storageId = textBoxStorageId.Text;
                }

                // Set up the values for your Media Services account 
                MediaService parameters = new(
                    location: SelectedLocation, // This is the location for the account to be created. 
                    storageAccounts: new List<Microsoft.Azure.Management.Media.Models.StorageAccount>(){
                    new Microsoft.Azure.Management.Media.Models.StorageAccount(
                        type: StorageAccountType.Primary,
                        // set this to the name of a storage account in your subscription using the full resource path formatting for Microsoft.Storage
                        id: storageId
                    )
                    }
                );

                // Create a new Media Services account
                _mediaServiceCreated = await _mediaServicesClient.Mediaservices.CreateOrUpdateAsync(ResourceGroupName, AccountName, parameters);

                MessageBox.Show($"Account '{AccountName}' has been successfully created.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                string message = $"Account '{AccountName}' has not been successfully created." + Environment.NewLine + $"{ex.Message}";

                if (ex.InnerException != null)
                {
                    message += Environment.NewLine + Program.GetErrorMessage(ex);
                }
                if (ex is ApiErrorException eApi)
                {
                    dynamic error = JsonConvert.DeserializeObject(eApi.Response.Content);
                    message += Environment.NewLine + (string)error?.error?.message;
                }

                MessageBox.Show(message, "Account creation failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Telemetry.TrackException(ex);
            }
            progressBarCreation.Visible = false;
            buttonCreate.Enabled = true;
        }

        private async void buttonCheckAvailStorage_Click(object sender, EventArgs e)
        {

            var storageManagementClient = new StorageManagementClient(_tokenCredentials);
            storageManagementClient.SubscriptionId = _mediaServicesClient.SubscriptionId;
            var availability = await storageManagementClient.StorageAccounts.CheckNameAvailabilityAsync(textBoxNewStorageName.Text);

            buttonCheckAvailStorage.Enabled = false;

            if (!(bool)availability.NameAvailable)
            {
                errorProvider1.SetError(textBoxNewStorageName, availability.Message);
                labelOkStorageAccount.Text = string.Empty;
                OkStorageAccount = false;
            }
            else
            {
                errorProvider1.SetError(textBoxNewStorageName, string.Empty);
                labelOkStorageAccount.Text = _checkedMark;
                OkStorageAccount = true;
            }

            UpdateButtonCreate();
        }

        private void checkBoxCreateStorage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCreateStorage.Checked)
            {
                textBoxNewStorageName.Enabled = true;
                comboBoxStorageType.Enabled = true;
                textBoxStorageId.Enabled = false;
                ChangeToStorageName();
            }
            else
            {
                textBoxNewStorageName.Enabled = false;
                comboBoxStorageType.Enabled = false;
                textBoxStorageId.Enabled = true;
                buttonCheckAvailStorage.Enabled = false;
                UpdateButtonCreate();
            }
        }

        private void textBoxNewStorageName_TextChanged(object sender, EventArgs e)
        {
            ChangeToStorageName();
        }
    }
}
