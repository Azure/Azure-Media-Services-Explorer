//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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
using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using Microsoft.Rest.Azure;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    internal class ListViewContentKeyPolicies : ListView
    {
        private AMSClientV3 _client;
        private string _selectedContentKeyPolicyName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderPolicyName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderPolicyDate;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderPolicyDescription;

        public ContentKeyPolicyResource GetSelectedContentKeyPolicy
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    int indexName = columnHeaderPolicyName.Index;
                    return _client.AMSclient.GetContentKeyPolicy(SelectedItems[0].SubItems[indexName].Text);
                }
                else
                {
                    return null;
                }
            }
        }

        public ListViewContentKeyPolicies()
        {
            columnHeaderPolicyName = new System.Windows.Forms.ColumnHeader();
            columnHeaderPolicyDescription = new System.Windows.Forms.ColumnHeader();
            columnHeaderPolicyDate = new System.Windows.Forms.ColumnHeader();

            Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderPolicyName,
            columnHeaderPolicyDescription,
            columnHeaderPolicyDate});
            FullRowSelect = true;
            HideSelection = false;
            Location = new System.Drawing.Point(32, 89);
            MultiSelect = false;
            Name = "listViewContentKeyPolicies";
            Size = new System.Drawing.Size(726, 194);
            TabIndex = 61;
            UseCompatibleStateImageBehavior = false;
            View = System.Windows.Forms.View.Details;
            Tag = -1;
            ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
            // 
            // columnHeaderPolicyName
            // 
            columnHeaderPolicyName.Text = "Content Key Policy Name";
            // 
            // columnHeaderPolicyDate
            // 
            columnHeaderPolicyDate.Text = "Last Modified";
            // 
            // columnHeaderPolicyDescription
            // 
            columnHeaderPolicyDescription.Text = "Description";

        }

        public async Task LoadContentKeyPoliciesAsync(AMSClientV3 client, string selectedContentKeyPolicyName = null)
        {
            _client = client;
            _selectedContentKeyPolicyName = selectedContentKeyPolicyName;

            await LoadContentKeyPoliciesAsync();
        }
        private async Task LoadContentKeyPoliciesAsync()
        {
            var contentKeyPolicies = _client.AMSclient.GetContentKeyPolicies().GetAllAsync();

            BeginUpdate();
            Items.Clear();

            await foreach (var policy in contentKeyPolicies)
            {
                ListViewItem item = new(policy.Data.Name);
                item.SubItems.Add(policy.Data.Description);
                item.SubItems.Add(policy.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G"));
                if (_selectedContentKeyPolicyName == policy.Data.Name)
                {
                    item.Selected = true;
                }
                Items.Add(item);
            }
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            EndUpdate();
        }

        public async Task DeleteSelectedPolicyAsync()
        {
            var policy = GetSelectedContentKeyPolicy;
            if (policy != null)
            {
                if (MessageBox.Show(string.Format("Do you want to delete the content key policy '{0}' ?", policy.Data.Name), "Content key policy deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        await policy.DeleteAsync(WaitUntil.Completed);
                        //await _client.AMSclient.ContentKeyPolicies.DeleteAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, policy.Name);
                    }
                    catch
                    {

                    }
                    await LoadContentKeyPoliciesAsync();
                }
            }
        }
    }
}
