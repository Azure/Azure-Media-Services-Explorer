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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
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
        private IPage<ContentKeyPolicy> _contentKeyPolicies;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderPolicyName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderPolicyDate;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderPolicyDescription;

        public ContentKeyPolicy GetSelectedContentKeyPolicy
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    int indexName = columnHeaderPolicyName.Index;
                    return _contentKeyPolicies.Where(t => t.Name == SelectedItems[0].SubItems[indexName].Text).FirstOrDefault();
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
            _contentKeyPolicies = await _client.AMSclient.ContentKeyPolicies.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName);

            BeginUpdate();
            Items.Clear();

            foreach (ContentKeyPolicy policy in _contentKeyPolicies)
            {
                ListViewItem item = new(policy.Name);
                item.SubItems.Add(policy.Description);
                item.SubItems.Add(policy.LastModified.ToLocalTime().ToString("G"));
                if (_selectedContentKeyPolicyName == policy.Name)
                {
                    item.Selected = true;
                }
                Items.Add(item);
            }
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            EndUpdate();
        }

        public async Task DeleteSelectedPoliciyAsync()
        {
            ContentKeyPolicy policy = GetSelectedContentKeyPolicy;
            if (policy != null)
            {
                if (MessageBox.Show(string.Format("Do you want to delete the content key policy '{0}' ?", policy.Name), "Content key policy deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        await _client.AMSclient.ContentKeyPolicies.DeleteAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, policy.Name);
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
