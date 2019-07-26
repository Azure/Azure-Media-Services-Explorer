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
using Microsoft.Rest.Azure;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    internal class ListViewTransforms : ListView
    {
        private AMSClientV3 _client;
        private string _selectedTransformName;
        private IPage<Transform> _transforms;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderTransformName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderTransformDate;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderTransformDescription;

        public Transform GetSelectedTransform
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    int indexName = columnHeaderTransformName.Index;
                    return _transforms.Where(t => t.Name == SelectedItems[0].SubItems[indexName].Text).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        public ListViewTransforms()
        {
            columnHeaderTransformName = new System.Windows.Forms.ColumnHeader();
            columnHeaderTransformDescription = new System.Windows.Forms.ColumnHeader();
            columnHeaderTransformDate = new System.Windows.Forms.ColumnHeader();

            Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderTransformName,
            columnHeaderTransformDescription,
            columnHeaderTransformDate});
            FullRowSelect = true;
            HideSelection = false;
            Location = new System.Drawing.Point(32, 89);
            MultiSelect = false;
            Name = "listViewTransforms";
            Size = new System.Drawing.Size(726, 194);
            TabIndex = 61;
            UseCompatibleStateImageBehavior = false;
            View = System.Windows.Forms.View.Details;
            Tag = -1;
            ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
            // 
            // columnHeaderTemplateName
            // 
            columnHeaderTransformName.Text = "Transform Name";
            // 
            // columnHeaderTemplateDate
            // 
            columnHeaderTransformDate.Text = "Last modified";
            // 
            // columnHeaderTransformDescription
            // 
            columnHeaderTransformDescription.Text = "Description";

        }

        public void LoadTransforms(AMSClientV3 client, string selectedTransformName = null)
        {
            _client = client;
            _selectedTransformName = selectedTransformName;

            LoadTransforms();
        }
        private void LoadTransforms()
        {
            _client.RefreshTokenIfNeeded();

            _transforms = _client.AMSclient.Transforms.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName);

            BeginUpdate();
            Items.Clear();

            foreach (Transform transform in _transforms)
            {
                ListViewItem item = new ListViewItem(transform.Name);
                item.SubItems.Add(transform.Description);
                item.SubItems.Add(transform.LastModified.ToLocalTime().ToString("G"));
                if (_selectedTransformName == transform.Name)
                {
                    item.Selected = true;
                }

                Items.Add(item);
            }
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            EndUpdate();
        }

        public void DeleteSelectedTemplate()
        {
            Transform transform = GetSelectedTransform;
            if (transform != null)
            {
                if (MessageBox.Show(string.Format("Do you want to delete the transform '{0}' ?", transform.Name), "Transform deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _client.RefreshTokenIfNeeded();
                        _client.AMSclient.Transforms.Delete(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, transform.Name);
                    }
                    catch
                    {

                    }
                    LoadTransforms();
                }
            }
        }
    }
}
