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
using Microsoft.Azure.Management.ResourceManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateAccount : Form
    {
        private List<Microsoft.Azure.Management.ResourceManager.Models.Location> _locations;
        private AzureMediaServicesClient _mediaServicesClient;
        private MediaService _mediaServiceCreated = null;

        public string SelectedLocation => (comboBoxAzureLocations.SelectedItem as Item).Value;

        public string AccountName => textBoxAccountName.Text;

        public string StorageId => textBoxStorageId.Text;

        public string ResourceGroup => textBoxRG.Text;

        public MediaService MediaServiceCreated
        {
            get
            {
                return _mediaServiceCreated;
            }
        }

        public CreateAccount(List<Microsoft.Azure.Management.ResourceManager.Models.Location> locations, AzureMediaServicesClient mediaServicesClient)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _locations = locations;
            _mediaServicesClient = mediaServicesClient;
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);


            foreach (var loc in _locations)
            {
                comboBoxAzureLocations.Items.Add(new Item(loc.RegionalDisplayName, loc.Name));
            }

            comboBoxAzureLocations.SelectedIndex = 0;
            labelErrorMessage.Text = string.Empty;
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

            buttonCreate.Enabled = availability.NameAvailable;
            buttonCheckAvail.Enabled = false;

            if (!availability.NameAvailable)
            {
                labelErrorMessage.ForeColor = System.Drawing.Color.Red;
                labelErrorMessage.Text = availability.Message;
            }
            else
            {
                labelErrorMessage.ForeColor = System.Drawing.Color.Black;
                labelErrorMessage.Text = "Account name is available !";
            }
        }

        private void comboBoxAzureLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeToAccountNameOrRegion();
        }

        private void ChangeToAccountNameOrRegion()
        {
            buttonCreate.Enabled = false;
            buttonCheckAvail.Enabled = true;
            labelErrorMessage.Text = string.Empty;
        }

        private async void buttonNext_Click(object sender, EventArgs e)
        {
            await DoCreationAsync(e);
        }

        private async Task DoCreationAsync(EventArgs e)
        {
            progressBarCreation.Visible = true;
            buttonCreate.Enabled = false;

            // Set up the values for your Media Services account 
            MediaService parameters = new(
                location: SelectedLocation, // This is the location for the account to be created. 
                storageAccounts: new List<StorageAccount>(){
                    new StorageAccount(
                        type: StorageAccountType.Primary,
                        // set this to the name of a storage account in your subscription using the full resource path formatting for Microsoft.Storage
                        id: StorageId
                    )
                }
            );

            try
            {
                // Create a new Media Services account
                _mediaServiceCreated = await _mediaServicesClient.Mediaservices.CreateOrUpdateAsync(ResourceGroup, AccountName, parameters);

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
    }
}
