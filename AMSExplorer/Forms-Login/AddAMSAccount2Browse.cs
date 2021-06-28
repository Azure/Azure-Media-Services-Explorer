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

using AMSExplorer.AMSLogin;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensibility;
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
        private Dictionary<string, IPublicClientApplication> _clientApplications = new();
        private readonly Prompt _prompt;
        private readonly IPublicClientApplication _app;
        private List<Subscription> subscriptions;
        private readonly Dictionary<string, List<MediaService>> allAMSAccountsPerSub = new();
        public MediaService selectedAccount = null;
        public string selectedTenantId = null;
        private string _selectText;
        private string _createText;

        public bool SelectMode;
        public Subscription SelectedSubscription;
        public AzureMediaServicesClient MediaServicesClient;

        public AddAMSAccount2Browse(TokenCredentials credentials, List<Subscription> subscriptions, AzureEnvironment environment, List<TenantIdDescription> myTenants, Prompt prompt, IPublicClientApplication app)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            this.credentials = credentials;
            this.subscriptions = subscriptions;
            this.environment = environment;
            _myTenants = myTenants;
            _prompt = prompt;
            _app = app;
        }

        private void AddAMSAccount2_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            _myTenants.ToList().ForEach(t => comboBoxTenants.Items.Add(new Item(string.Format("{0} ({1})", t.DisplayName, t.TenantId), t.TenantId)));

            if (_myTenants.Count > 0)
            {
                comboBoxTenants.SelectedIndex = 0;
            }

            _selectText = buttonNext.Text;
            _createText = (string)buttonNext.Tag;
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
            var scopes = new[] { environment.AADSettings.TokenAudience.ToString() + "/.default" };

            if (!_clientApplications.ContainsKey(selectedTenantId))
            {
                _clientApplications[selectedTenantId] = PublicClientApplicationBuilder.Create(environment.ClientApplicationId)
                .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", selectedTenantId))
                .WithRedirectUri("http://localhost")
                .Build();
            }

            IPublicClientApplication app = _clientApplications[selectedTenantId];

            AuthenticationResult accessToken = null;
            var accounts = await _app.GetAccountsAsync();
            try
            {
                accessToken = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (MsalUiRequiredException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                try
                {
                    accessToken = await app
                        .AcquireTokenInteractive(scopes)
                        .WithPrompt(_prompt)
                        .WithCustomWebUi(new EmbeddedBrowserCustomWebUI(this))
                        .ExecuteAsync();
                }
                catch (MsalException)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            credentials = new TokenCredentials(accessToken.AccessToken, "Bearer");
            SubscriptionClient subscriptionClient = new(environment.ArmEndpoint, credentials);

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
                TreeNode mySubNode = new(sub.DisplayName);
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

        private static string GetResourceGroupNameFromId(string Id)
        {
            string[] idParts = Id.Split('/');
            return idParts[4];
        }

        private static string GetStorageNameFromId(string Id)
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
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label2, e);
        }

        private void TreeViewAzureSub_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // user clicked on the '+' button

            Cursor = Cursors.WaitCursor;

            SelectedSubscription = subscriptions.Where(s => s.SubscriptionId == (string)e.Node.Tag).FirstOrDefault();

            // Getting Media Services accounts...
            MediaServicesClient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials)
            {
                SubscriptionId = SelectedSubscription.SubscriptionId
            };

            List<MediaService> mediaServicesAccounts = new();
            IPage<MediaService> mediaServicesAccountsPage = MediaServicesClient.Mediaservices.ListBySubscription();
            while (mediaServicesAccountsPage != null)
            {
                mediaServicesAccounts.AddRange(mediaServicesAccountsPage);
                if (mediaServicesAccountsPage.NextPageLink != null)
                {
                    mediaServicesAccountsPage = MediaServicesClient.Mediaservices.ListBySubscriptionNext(mediaServicesAccountsPage.NextPageLink);
                }
                else
                {
                    mediaServicesAccountsPage = null;
                }
            }

            // let's save the data
            allAMSAccountsPerSub[MediaServicesClient.SubscriptionId] = mediaServicesAccounts;

            treeViewAzureSub.BeginUpdate();
            e.Node.Nodes.Clear();
            foreach (MediaService mediaAcct in mediaServicesAccounts)
            {
                TreeNode node = new(mediaAcct.Name)
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
            if (e.Node.Level == 1) // AMS Account selected
            {
                List<MediaService> accounts = allAMSAccountsPerSub[(string)e.Node.Parent.Tag];
                MediaService account = accounts.Where(a => a.Id == (string)e.Node.Tag).FirstOrDefault();

                // let's display account info
                DisplayInfoAccount(account);
                selectedAccount = account;
                buttonNext.Text = _selectText;
                buttonNext.Enabled = true;
                SelectMode = true;
            }
            else if (e.Node.Level == 0) // Subscription selected
            {
                SelectedSubscription = subscriptions.Where(s => s.SubscriptionId == (string)e.Node.Tag).FirstOrDefault();

                ClearDisplayInfoAccount();
                selectedAccount = null;
                buttonNext.Enabled = true;
                buttonNext.Text = _createText;
                SelectMode = false;
            }
            else
            {
                ClearDisplayInfoAccount();
                buttonNext.Enabled = false;
            }
        }

        private void AddAMSAccount2Browse_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}