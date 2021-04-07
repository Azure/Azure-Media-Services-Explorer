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
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AddAMSAccount2Browse : Form
    {
        private TokenCredentials credentials;
        private readonly AzureEnvironment environment;
        private readonly List<TenantIdDescription> _myTenants;
        private readonly IPlatformParameters _parameters;
        private List<Subscription> subscriptions;
        private readonly Dictionary<string, List<MediaService>> allAMSAccountsPerSub = new Dictionary<string, List<MediaService>>();
        public MediaService selectedAccount = null;
        public string selectedTenantId = null;

        public AddAMSAccount2Browse(TokenCredentials credentials, List<Subscription> subscriptions, AzureEnvironment environment, List<TenantIdDescription> myTenants, IPlatformParameters parameters)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            this.credentials = credentials;
            this.subscriptions = subscriptions;
            this.environment = environment;
            _myTenants = myTenants;
            _parameters = parameters;
        }

        private void AddAMSAccount2_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            _myTenants.ToList().ForEach(t => comboBoxTenants.Items.Add(new Item(string.Format("{0} ({1})", t.DisplayName, t.TenantId), t.TenantId)));

            if (_myTenants.Count() > 0)
            {
                comboBoxTenants.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Build the tree of subscriptions.
        /// </summary>
        private async void BuildSubTree()
        {
            ClearDisplayInfoAccount();

            if (comboBoxTenants.SelectedItem == null)
            {
                return;
            }

            treeViewAzureSub.Nodes.Clear();
            // let's connect

            selectedTenantId = ((Item)comboBoxTenants.SelectedItem).Value;

            AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(
                                                               authority: environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", selectedTenantId ?? "common"),
                                                                   validateAuthority: true);
            AuthenticationResult accessToken;
            try
            {
                accessToken = await authContext.AcquireTokenAsync(
                                                                     resource: environment.AADSettings.TokenAudience.ToString(),
                                                                     clientId: environment.ClientApplicationId,
                                                                     redirectUri: new Uri("urn:ietf:wg:oauth:2.0:oob"),
                                                                     parameters: _parameters
                                                                     );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            credentials = new TokenCredentials(accessToken.AccessToken, "Bearer");

            SubscriptionClient subscriptionClient = new SubscriptionClient(environment.ArmEndpoint, credentials);

            // Subcriptions listing
            subscriptions = new List<Subscription>();
            IPage<Subscription> subscriptionsPage = subscriptionClient.Subscriptions.List();
            while (subscriptionsPage != null)
            {
                subscriptions.AddRange(subscriptionsPage);
                if (subscriptionsPage.NextPageLink != null)
                {
                    subscriptionsPage = subscriptionClient.Subscriptions.ListNext(subscriptionsPage.NextPageLink);
                }
                else
                {
                    subscriptionsPage = null;
                }
            }

            treeViewAzureSub.BeginUpdate();
            treeViewAzureSub.Nodes.Clear();
            int indexSub = -1;

            foreach (Subscription sub in subscriptions)
            {
                indexSub++;
                TreeNode mySubNode = new TreeNode(sub.DisplayName);
                mySubNode.Tag = mySubNode.ToolTipText = sub.SubscriptionId;
                treeViewAzureSub.Nodes.Add(mySubNode);
                treeViewAzureSub.Nodes[indexSub].Nodes.Add("");

            }
            treeViewAzureSub.EndUpdate();
        }

        /// <summary>
        /// Display the AMS account info on the right.
        /// </summary>
        /// <param name="account"></param>
        private void DisplayInfoAccount(MediaService account)
        {
            DGAcct.Rows.Clear();
            DGAcct.ColumnCount = 2;
            DGAcct.ColumnCount = 2;
            DGAcct.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            // account info
            DGAcct.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGAcct.Rows.Add("AMS Account Name", account.Name);
            DGAcct.Rows.Add("Location", account.Location);
            DGAcct.Rows.Add("Resource Group", GetResourceGroupNameFromId(account.Id));
            DGAcct.Rows.Add("MediaServiceId", account.MediaServiceId);

            int i = 1;
            foreach (StorageAccount stor in account.StorageAccounts)
            {
                string add = stor.Type == StorageAccountType.Primary ? " (primary)" : string.Empty;
                DGAcct.Rows.Add($"Storage account #{i}" + add, GetStorageNameFromId(stor.Id));
                i++;
            }
        }

        private string GetResourceGroupNameFromId(string Id)
        {
            string[] idParts = Id.Split('/');
            return idParts[4];
        }

        private string GetStorageNameFromId(string Id)
        {
            string[] idParts = Id.Split('/');
            return idParts.Last();
        }

        /// <summary>
        /// Clear the AMS account info on the right.
        /// </summary>
        private void ClearDisplayInfoAccount()
        {
            DGAcct.Rows.Clear();
        }

        /// <summary>
        /// Fired when the selected tenant has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxTenants_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildSubTree();
        }

        private void AddAMSAccount2Browse_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(label2, e);
        }

        private void TreeViewAzureSub_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // user clicked on the '+' button

            Cursor = Cursors.WaitCursor;

            Subscription selectedSubscription = subscriptions.Where(s => s.SubscriptionId == (string)e.Node.Tag).FirstOrDefault();

            // Getting Media Services accounts...
            AzureMediaServicesClient mediaServicesClient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials)
            {
                SubscriptionId = selectedSubscription.SubscriptionId
            };

            List<MediaService> mediaServicesAccounts = new List<MediaService>();
            IPage<MediaService> mediaServicesAccountsPage = mediaServicesClient.Mediaservices.ListBySubscription();
            while (mediaServicesAccountsPage != null)
            {
                mediaServicesAccounts.AddRange(mediaServicesAccountsPage);
                if (mediaServicesAccountsPage.NextPageLink != null)
                {
                    mediaServicesAccountsPage = mediaServicesClient.Mediaservices.ListBySubscriptionNext(mediaServicesAccountsPage.NextPageLink);
                }
                else
                {
                    mediaServicesAccountsPage = null;
                }
            }

            // let's save the data
            allAMSAccountsPerSub[mediaServicesClient.SubscriptionId] = mediaServicesAccounts;

            treeViewAzureSub.BeginUpdate();
            e.Node.Nodes.Clear();
            foreach (MediaService mediaAcct in mediaServicesAccounts)
            {
                TreeNode node = new TreeNode(mediaAcct.Name)
                {
                    Tag = mediaAcct.Id
                };
                e.Node.Nodes.Add(node);
            }
            treeViewAzureSub.EndUpdate();
            Cursor = Cursors.Arrow;
            buttonNext.Enabled = false;

        }

        private void TreeViewAzureSub_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                List<MediaService> accounts = allAMSAccountsPerSub[(string)e.Node.Parent.Tag];
                MediaService account = accounts.Where(a => a.Id == (string)e.Node.Tag).FirstOrDefault();

                // let's display account info
                DisplayInfoAccount(account);
                selectedAccount = account;
                buttonNext.Enabled = true;
            }
            else
            {
                ClearDisplayInfoAccount();
                buttonNext.Enabled = false;
            }
        }
    }
}