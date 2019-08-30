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
        private readonly myTenant[] _myTenants;
        private readonly IPlatformParameters _parameters;
        private IPage<Subscription> subscriptions;
        private readonly Dictionary<string, IPage<SubscriptionMediaService>> allAMSAccountsPerSub = new Dictionary<string, IPage<SubscriptionMediaService>>();
        public SubscriptionMediaService selectedAccount = null;
        public string selectedTenantId = null;

        public AddAMSAccount2Browse(TokenCredentials credentials, IPage<Subscription> subscriptions, AzureEnvironment environment, myTenant[] myTenants, IPlatformParameters parameters)
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
            _myTenants.ToList().ForEach(t => comboBoxTenants.Items.Add(new Item(string.Format("{0} ({1})", t.displayName, t.tenantId), t.tenantId)));

            if (_myTenants.Count() > 0)
            {
                comboBoxTenants.SelectedIndex = 0;
            }
        }

        private async void BuildSubTree()
        {
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
            AuthenticationResult accessToken = null;
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
            subscriptions = subscriptionClient.Subscriptions.List();

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

        private void treeViewAzureSub_MouseClick(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo hitTest = treeViewAzureSub.HitTest(e.Location);
            if (hitTest.Location == TreeViewHitTestLocations.PlusMinus && hitTest.Node.IsExpanded)
            {
                // user clicked on the '+' button

                Cursor = Cursors.WaitCursor;

                Subscription selectedSubscription = subscriptions.Where(s => s.DisplayName == hitTest.Node.Text).FirstOrDefault();

                // Getting Media Services accounts...
                AzureMediaServicesClient mediaServicesClient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials)
                {
                    SubscriptionId = selectedSubscription.SubscriptionId
                };
                IPage<SubscriptionMediaService> mediaServicesAccounts = mediaServicesClient.Mediaservices.ListBySubscription();

                // let's save the data
                allAMSAccountsPerSub[mediaServicesClient.SubscriptionId] = mediaServicesAccounts;

                treeViewAzureSub.BeginUpdate();
                hitTest.Node.Nodes.Clear();
                foreach (SubscriptionMediaService mediaAcct in mediaServicesAccounts)
                {
                    TreeNode node = new TreeNode(mediaAcct.Name)
                    {
                        Tag = mediaAcct.Id
                    };
                    hitTest.Node.Nodes.Add(node);
                }
                treeViewAzureSub.EndUpdate();
                Cursor = Cursors.Arrow;
                buttonNext.Enabled = false;

            }
            else if (hitTest.Location == TreeViewHitTestLocations.Label && hitTest.Node.Level == 1)
            {
                IPage<SubscriptionMediaService> accounts = allAMSAccountsPerSub[(string)hitTest.Node.Parent.Tag];
                SubscriptionMediaService account = accounts.Where(a => a.Id == (string)hitTest.Node.Tag).FirstOrDefault();

                // let's display account info
                DisplayInfoAccount(account);
                selectedAccount = account;
                buttonNext.Enabled = true;
            }
            else
            {
                buttonNext.Enabled = false;
            }
        }

        private void DisplayInfoAccount(SubscriptionMediaService account)
        {
            DGAcct.Rows.Clear();
            DGAcct.ColumnCount = 2;
            DGAcct.ColumnCount = 2;
            DGAcct.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            // acount info
            DGAcct.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGAcct.Rows.Add("Name", account.Name);
            DGAcct.Rows.Add("Location", account.Location);
            DGAcct.Rows.Add("Id", account.Id);
            DGAcct.Rows.Add("MediaServiceId", account.MediaServiceId);
        }

        private void comboBoxTenants_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildSubTree();
        }

        private void AddAMSAccount2Browse_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            Program.UpdatedSizeFontAfterDPIChange(label2, e);
        }
    }
}
