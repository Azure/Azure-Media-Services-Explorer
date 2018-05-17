//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// Azure Management dependencies
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.Rest;
using System.Threading.Tasks;

namespace AMSExplorer
{
    public partial class AttachStorage : Form
    {
        private CredentialsEntry _credentials;
        private IAzureMediaServicesClient mediaClient;
        private MediaService mediaService;

        public string AzureSubscriptionID
        {
            get
            {
                return textBoxSubId.Text;
            }
        }


        public string AMSResourceGroup
        {
            get
            {
                return textBoxAMSResourceGroup.Text;
            }
        }

        public List<string> StorageResourceIdToDetach
        {
            get
            {
                var storages = new List<string>();
                foreach (var stor in listViewStorage.CheckedItems)
                {
                    string storeId = (stor as ListViewItem).SubItems[1].Text;
                    storages.Add(storeId);
                }
                return storages;
            }
        }

        public List<string> StorageResourceIdToAttach
        {
            get
            {
                return textBoxAttachStorage.Text.Split(new[] { Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public AttachStorage(CredentialsEntry credentials)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _credentials = credentials;
        }

        private void AttachStorage_Load(object sender, EventArgs e)
        {
            groupBoxAMSAcct.Text = string.Format(groupBoxAMSAcct.Text, _credentials.ReturnAccountName());
        }

        private void textBoxStorageName_TextChanged(object sender, EventArgs e)
        {
            textBoxTXT_Validation(sender, e);
        }

        private void textBoxTXT_Validation(object sender, EventArgs e)
        {
            TextBox mytextbox = (TextBox)sender;
            mytextbox.BackColor = (string.IsNullOrWhiteSpace(mytextbox.Text.Trim())) ? Color.Pink : Color.White;
        }

        private string GetStorageResourceId(string subscriptionId, string resourceGroup, string storageName)
        {
            return string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Storage/storageAccounts/{2}",
                subscriptionId,
                resourceGroup,
                storageName
                );
        }

        /*
        private async Task<IAzureMediaServicesClient> GetMediaClient()
        {
            ServiceClientCredentials serviceCreds;
            if (_credentials.UseAADServicePrincipal)
            {
                serviceCreds = await ApplicationTokenProvider.LoginSilentAsync(_credentials.ADTenantDomain, _credentials.ADSPClientId, _credentials.ADSPClientSecret, _credentials.ReturnADSettings());
            }
            else
            {
                var formSP = new AMSLoginServicePrincipal();
                if (formSP.ShowDialog() == DialogResult.OK)
                {
                    serviceCreds = await ApplicationTokenProvider.LoginSilentAsync(_credentials.ADTenantDomain, formSP.ClientId, formSP.ClientSecret, _credentials.ReturnADSettings());
                }
                else
                {
                    return null;
                }
            }

            return new IAzureMediaServicesClient(serviceCreds);
        }
        */

        private void AskServicePrincipalCredentialsIfNeeded()
        {
            if (!_credentials.UseAADServicePrincipal)
            {
                var formSP = new AMSLoginServicePrincipal();
                if (formSP.ShowDialog() == DialogResult.OK)
                {
                    _credentials.ADSPClientId = formSP.ClientId;
                    _credentials.ADSPClientSecret = formSP.ClientSecret;
                }
            }
        }


        private static IAzureMediaServicesClient CreateMediaServicesClient(ConfigWrapper config)
        {
            ArmClientCredentials credentials = new ArmClientCredentials(config);

            return new AzureMediaServicesClient(config.ArmEndpoint, credentials)
            {
                SubscriptionId = config.SubscriptionId,
            };
        }


        public void UpdateStorageAccounts()
        {
            /*

            MediaService mediaServiceNew = new MediaService();
            mediaServiceNew.ApiEndpoints = mediaService.ApiEndpoints;
            mediaServiceNew.StorageAccounts = new List<StorageAccount>();

            // build the list of attached storage and remove from the list the ones the user wants to detach
            foreach (var stor in mediaService.StorageAccounts)
            {
                if ((bool)stor.IsPrimary || !StorageResourceIdToDetach.Contains(stor.Id)) // Default cannot be detached
                {
                    mediaServiceNew.StorageAccounts.Add(new StorageAccount(id: stor.Id, isPrimary: stor.IsPrimary));
                }
            }

            // storage attach
            foreach (var storId in StorageResourceIdToAttach)
            {
                mediaServiceNew.StorageAccounts.Add(new StorageAccount(id: storId, isPrimary: false));
            }

            string accn = _credentials.ReturnAccountName();
            mediaClient.MediaService.Update(AMSResourceGroup, _credentials.ReturnAccountName(), mediaServiceNew);

            */


           

           
            // storage to detach
            foreach (var stor in mediaService.StorageAccounts.ToList())
            {
                if (StorageResourceIdToDetach.Contains(stor.Id))
                {
                    mediaService.StorageAccounts.Remove(stor);
                }
            }
           

            // storage to attach
            foreach (var storId in StorageResourceIdToAttach)
            {
                mediaService.StorageAccounts.Add(new StorageAccount(StorageAccountType.Secondary, storId));
            }

            mediaClient.Mediaservices.Update(AMSResourceGroup, _credentials.ReturnAccountName(), mediaService);


        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                AskServicePrincipalCredentialsIfNeeded();
                ConfigWrapper config = new ConfigWrapper(_credentials, AzureSubscriptionID, AMSResourceGroup, "72f988bf-86f1-41af-91ab-2d7cd011db47", new Uri("https://management.core.windows.net/"), "West Europe", new Uri("https://login.microsoftonline.com"), new Uri("https://management.azure.com/"));
                mediaClient = CreateMediaServicesClient(config);
                // Set the polling interval for long running operations to 2 seconds.
                // The default value is 30 seconds for the .NET client SDK
                mediaClient.LongRunningOperationRetryTimeout = 2;

                mediaService = await mediaClient.Mediaservices.GetAsync(AMSResourceGroup, _credentials.ReturnAccountName());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonAttach.Enabled = groupBoxStorage.Enabled = false;
                return;
            }

            var storages = mediaService.StorageAccounts.ToList();
            listViewStorage.Items.Clear();

            storages.ForEach(s =>
            {
                // if (!(bool)s.IsPrimary)
                if (s.Type == StorageAccountType.Secondary)
                {
                    var names = s.Id.Split('/');
                    var lvitem = new ListViewItem(new string[] { names.Last(), s.Id });
                    lvitem.ToolTipText = s.Id;
                    listViewStorage.Items.Add(lvitem);
                }
            }
            );

            buttonAttach.Enabled = groupBoxStorage.Enabled = true;
        }
    }
}