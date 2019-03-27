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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;

namespace AMSExplorer
{
    public partial class AttachStorage : Form
    {
        private IAzureMediaServicesClient mediaClient;
        private MediaService mediaService;
        private AMSClientV3 _amsClient;

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

        public AttachStorage(AMSClientV3 amsClient)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _amsClient = amsClient;
        }

        private void AttachStorage_Load(object sender, EventArgs e)
        {
            try
            {
                _amsClient.RefreshTokenIfNeeded();

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

            var storages = mediaService.StorageAccounts.ToList();
            listViewStorage.Items.Clear();

            storages.ForEach(s =>
            {
                if (s.Type == StorageAccountType.Secondary)
                {
                    var names = s.Id.Split('/');
                    var lvitem = new ListViewItem(new string[] { names.Last(), s.Id });
                    lvitem.ToolTipText = s.Id;
                    listViewStorage.Items.Add(lvitem);
                }
            }
            );
            buttonAttach.Enabled = true;
        }

   
        public async Task UpdateStorageAccountsAsync()
        {
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

           await mediaClient.Mediaservices.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, mediaService);
        }
    }
}