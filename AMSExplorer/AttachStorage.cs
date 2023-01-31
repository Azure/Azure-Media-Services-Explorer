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


using Azure;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AttachStorage : Form
    {
        private MediaServicesAccountResource mediaClient;
        private readonly AMSClientV3 _amsClient;

        public List<string> StorageResourceIdToDetach
        {
            get
            {
                List<string> storages = new();
                foreach (object stor in listViewDetachStorage.CheckedItems)
                {
                    ResourceIdentifier storeId = new((stor as ListViewItem).SubItems[1].Text);
                    storages.Add(storeId);
                }
                return storages;
            }
        }

        public List<ResourceIdentifier> StorageResourceIdToAttach

        {
            get
            {
                List<ResourceIdentifier> storages = new();
                foreach (object stor in listViewAttachStorage.CheckedItems)
                {
                    ResourceIdentifier storeId = new((stor as ListViewItem).SubItems[1].Text);
                    storages.Add(storeId);
                }
                var liststr = textBoxAttachStorage.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                storages.AddRange(liststr.Select(s => new ResourceIdentifier(s)));

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
                //mediaClient.LongRunningOperationRetryTimeout = 2;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when connecting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonAttach.Enabled = false;
                return;
            }

            var storages = mediaClient.Data.StorageAccounts.ToList();
            listViewDetachStorage.Items.Clear();

            storages.ForEach(s =>
            {
                if (s.AccountType == MediaServicesStorageAccountType.Secondary)
                {
                    string[] names = s.Id.ToString().Split('/');
                    ListViewItem lvitem = new(new string[] { names.Last(), s.Id })
                    {
                        ToolTipText = string.Format("Resource Group : {0}", AMSClientV3.GetStorageResourceName(s.Id))
                    };
                    listViewDetachStorage.Items.Add(lvitem);
                }
            }
            );

            // list locations in order to be able the long name of location 

            ArmClient armClient = new(_amsClient.credentialForArmClient);

            // Subcriptions listing
            var subscriptions = armClient.GetSubscriptions().ToList();

            SubscriptionResource subscription = subscriptions.Where(s => s.Data.SubscriptionId == _amsClient.AMSclient.Data.Id.SubscriptionId).First();
            var myLocations = subscription.GetLocations().AsEnumerable();

            string shortLocationName = myLocations.Where(l => l.Name == _amsClient.AMSclient.Data.Location.Name).FirstOrDefault()?.Name;

            // List storage accounts in subscription
            var storageaccounts = subscription.GetStorageAccounts();
            var storageAccountsInLoc = storageaccounts.Where(s => s.Data.Location.Name == shortLocationName).ToList();

            storageAccountsInLoc.ForEach(s =>
            {
                ListViewItem lvitem = new(new string[] { s.Data.Name, s.Id })
                {
                    ToolTipText = string.Format("Resource Group : {0} (Replication : {1})", AMSClientV3.GetStorageResourceName(s.Id), s.Data.Sku.Name)
                };
                listViewAttachStorage.Items.Add(lvitem);

            }
          );

            labelAttachFromList.Text = string.Format(labelAttachFromList.Text, _amsClient.AMSclient.Data.Location.DisplayName);
            buttonAttach.Enabled = true;
        }


        public async Task UpdateStorageAccountsAsync()
        {
            List<MediaServicesStorageAccount> listStorage = new();

            // storage to detach
            foreach (var stor in mediaClient.Data.StorageAccounts.ToList())
            {
                if (!StorageResourceIdToDetach.Contains(stor.Id))
                {
                    listStorage.Add(stor); // we only keep storage whch should not be attached
                }
            }

            // storage to attach
            foreach (var storId in StorageResourceIdToAttach)
            {
                listStorage.Add(new MediaServicesStorageAccount(MediaServicesStorageAccountType.Secondary) { Id = storId });
            }

            MediaServicesAccountPatch msUpdate = new();
            listStorage.ForEach(s => msUpdate.StorageAccounts.Add(s));

            await mediaClient.UpdateAsync(WaitUntil.Completed, msUpdate);
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