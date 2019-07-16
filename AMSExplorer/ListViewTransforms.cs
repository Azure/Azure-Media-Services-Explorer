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
    class ListViewTransforms : ListView
    {
        private AMSClientV3 _client;
        private string _selectedTransformName;
        private IPage<Transform> _transforms;
        private System.Windows.Forms.ColumnHeader columnHeaderTransformName;
        private System.Windows.Forms.ColumnHeader columnHeaderTransformDate;
        private System.Windows.Forms.ColumnHeader columnHeaderTransformDescription;

        public Transform GetSelectedTransform
        {
            get
            {
                if (this.SelectedItems.Count > 0)
                {
                    int indexName = columnHeaderTransformName.Index;
                    return _transforms.Where(t => t.Name == this.SelectedItems[0].SubItems[indexName].Text).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        public ListViewTransforms()
        {
            this.columnHeaderTransformName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTransformDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTransformDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTransformName,
            this.columnHeaderTransformDescription,
            this.columnHeaderTransformDate});
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.Location = new System.Drawing.Point(32, 89);
            this.MultiSelect = false;
            this.Name = "listViewTransforms";
            this.Size = new System.Drawing.Size(726, 194);
            this.TabIndex = 61;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.Tag = -1;
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
            // 
            // columnHeaderTemplateName
            // 
            this.columnHeaderTransformName.Text = "Transform Name";
            // 
            // columnHeaderTemplateDate
            // 
            this.columnHeaderTransformDate.Text = "Last modified";
            // 
            // columnHeaderTransformDescription
            // 
            this.columnHeaderTransformDescription.Text = "Description";

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

            this.BeginUpdate();
            this.Items.Clear();

            foreach (var transform in _transforms)
            {
                ListViewItem item = new ListViewItem(transform.Name);
                item.SubItems.Add(transform.Description);
                item.SubItems.Add(transform.LastModified.ToLocalTime().ToString("G"));
                if (_selectedTransformName == transform.Name) item.Selected = true;
                this.Items.Add(item);
            }
            this.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.EndUpdate();
        }

        public void DeleteSelectedTemplate()
        {
            var transform = this.GetSelectedTransform;
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
                    this.LoadTransforms();
                }
            }
        }
    }
}
