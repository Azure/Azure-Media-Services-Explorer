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
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using System.Drawing;

namespace AMSExplorer
{
    public partial class AddAMSAccount2 : Form
    {
        private TokenCredentials credentials;
        private AzureEnvironmentV3 environment;
        private IPage<Subscription> subscriptions;
        private Dictionary<string, IPage<SubscriptionMediaService>> allAMSAccountsPerSub = new Dictionary<string, IPage<SubscriptionMediaService>>();
        public SubscriptionMediaService selectedAccount = null;

        public AddAMSAccount2(TokenCredentials credentials, IPage<Subscription> subscriptions, AzureEnvironmentV3 environment)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            this.credentials = credentials;
            this.subscriptions = subscriptions;
            this.environment = environment;
        }

        private void AddAMSAccount2_Load(object sender, EventArgs e)
        {
            BuildSubTree();
        }

        private void BuildSubTree()
        {
            treeViewAzureSub.BeginUpdate();
            treeViewAzureSub.Nodes.Clear();
            int indexSub = -1;

            foreach (var sub in subscriptions)
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
            var hitTest = treeViewAzureSub.HitTest(e.Location);
            if (hitTest.Location == TreeViewHitTestLocations.PlusMinus && hitTest.Node.IsExpanded)
            {
                // user clicked on the '+' button

                this.Cursor = Cursors.WaitCursor;

                var selectedSubscription = subscriptions.Where(s => s.DisplayName == hitTest.Node.Text).FirstOrDefault();

                // Getting Media Services accounts...
                var mediaServicesClient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials);

                mediaServicesClient.SubscriptionId = selectedSubscription.SubscriptionId;
                var mediaServicesAccounts = mediaServicesClient.Mediaservices.ListBySubscription();

                // let's save the data
                allAMSAccountsPerSub[mediaServicesClient.SubscriptionId] = mediaServicesAccounts;

                treeViewAzureSub.BeginUpdate();
                hitTest.Node.Nodes.Clear();
                foreach (var mediaAcct in mediaServicesAccounts)
                {
                    var node = new TreeNode(mediaAcct.Name);
                    node.Tag = mediaAcct.Id;
                    hitTest.Node.Nodes.Add(node);
                }
                treeViewAzureSub.EndUpdate();
                this.Cursor = Cursors.Arrow;
                buttonNext.Enabled = false;

            }
            else if (hitTest.Location == TreeViewHitTestLocations.Label && hitTest.Node.Level == 1)
            {
                var accounts = allAMSAccountsPerSub[(string)hitTest.Node.Parent.Tag];
                var account = accounts.Where(a => a.Id == (string)hitTest.Node.Tag).FirstOrDefault();

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
    }
}
