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


using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Management.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AttachStorage : Form
    {
        private IAzureMediaServicesClient mediaClient;
        private MediaService mediaService;
        private readonly AMSClientV3 _amsClient;

        public List<string> StorageResourceIdToDetach
        {
            get
            {
                List<string> storages = new();
                foreach (object stor in listViewDetachStorage.CheckedItems)
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
                List<string> storages = new();
                foreach (object stor in listViewAttachStorage.CheckedItems)
                {
                    string storeId = (stor as ListViewItem).SubItems[1].Text;
                    storages.Add(storeId);
                }

                storages.AddRange(textBoxAttachStorage.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList());

                return storages;
            }
        }


        public AttachStorage(AMSClientV3 amsClient)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = amsClient;
        }

        private void AttachStorage_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            try
            {
                mediaClient = _amsClient.AMSclient;
                // Set the polling interval for long running operations to 2 seconds.
                // The default value is 30 seconds for the .NET client SDK
                mediaClient.LongRunningOperationRetryTimeout = 2;

                mediaService = mediaClient.Mediaservices.Get(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonAttach.Enabled = false;
                return;
            }

            List<StorageAccount> storages = mediaService.StorageAccounts.ToList();
            listViewDetachStorage.Items.Clear();

            storages.ForEach(s =>
            {
                if (s.Type == StorageAccountType.Secondary)
                {
                    string[] names = s.Id.Split('/');
                    ListViewItem lvitem = new(new string[] { names.Last(), s.Id })
                    {
                        ToolTipText = string.Format("Resource Group : {0}", AMSClientV3.GetStorageResourceName(s.Id))
                    };
                    listViewDetachStorage.Items.Add(lvitem);
                }
            }
            );

            // list locations in order to be able the long name of location 

            ArmClient armClient = new ArmClient(_amsClient.credentialForArmClient);

            // Subcriptions listing
            var subscriptions = armClient.GetSubscriptions().ToList();

            SubscriptionResource subscription = subscriptions.Where(s => s.Data.SubscriptionId == _amsClient.AMSclient.SubscriptionId).First();
            var myLocations = subscription.GetLocations().AsEnumerable();

            string shortLocationName = myLocations.Where(l => l.DisplayName == _amsClient.credentialsEntry.MediaService.Location).FirstOrDefault()?.Name;

            // List storage accounts in subscription
            var storageaccounts = subscription.GetStorageAccounts();
            var storageAccountsInLoc = storageaccounts.Where(s => s.Data.Location == shortLocationName).ToList();

            storageAccountsInLoc.ForEach(s =>
            {
                ListViewItem lvitem = new(new string[] { s.Data.Name, s.Id })
                {
                    ToolTipText = string.Format("Resource Group : {0} (Replication : {1})", AMSClientV3.GetStorageResourceName(s.Id), s.Data.Sku.Name)
                };
                listViewAttachStorage.Items.Add(lvitem);

            }
          );

            labelAttachFromList.Text = string.Format(labelAttachFromList.Text, _amsClient.credentialsEntry.MediaService.Location);
            buttonAttach.Enabled = true;
        }


        public async Task UpdateStorageAccountsAsync()
        {
            List<StorageAccount> listStorage = new List<StorageAccount>();

            // storage to detach
            foreach (StorageAccount stor in mediaService.StorageAccounts.ToList())
            {
                if (!StorageResourceIdToDetach.Contains(stor.Id))
                {
                    listStorage.Add(stor); // we only keep storage whch should not be attached
                }
            }

            // storage to attach
            foreach (string storId in StorageResourceIdToAttach)
            {
                listStorage.Add(new StorageAccount(StorageAccountType.Secondary, storId));
            }

            MediaServiceUpdate msUpdate = new() { StorageAccounts = listStorage };
            await mediaClient.Mediaservices.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, msUpdate);
        }

        private void AttachStorage_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelAssetCopy, e);
        }

        private void AttachStorage_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}