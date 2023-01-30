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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    internal class ListViewTransforms : ListView
    {
        private AMSClientV3 _client;
        private string _selectedTransformName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderTransformName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderTransformDate;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderTransformDescription;

        public MediaTransformResource GetSelectedTransform
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    int indexName = columnHeaderTransformName.Index;
                    return _client.AMSclient.GetMediaTransform(SelectedItems[0].SubItems[indexName].Text);
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
            columnHeaderTransformDate.Text = "Last Modified";
            // 
            // columnHeaderTransformDescription
            // 
            columnHeaderTransformDescription.Text = "Description";

        }

        public async Task LoadTransformsAsync(AMSClientV3 client, string selectedTransformName = null)
        {
            _client = client;
            _selectedTransformName = selectedTransformName;

            await LoadTransformsAsync();
        }
        private async Task LoadTransformsAsync()
        {
            var transforms = _client.AMSclient.GetMediaTransforms().GetAllAsync();
            //.Transforms.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName);

            BeginUpdate();
            Items.Clear();

            await foreach (var transform in transforms)
            {
                ListViewItem item = new(transform.Data.Name);
                item.SubItems.Add(transform.Data.Description);
                item.SubItems.Add(transform.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G"));
                if (_selectedTransformName == transform.Data.Name)
                {
                    item.Selected = true;
                }
                Items.Add(item);
            }
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            EndUpdate();
        }

        public async Task DeleteSelectedTemplateAsync()
        {
            var transform = GetSelectedTransform;
            if (transform != null)
            {
                if (MessageBox.Show(string.Format("Do you want to delete the transform '{0}' ?", transform.Data.Name), "Transform deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        await transform.DeleteAsync(WaitUntil.Completed);
                    }
                    catch
                    {

                    }
                    await LoadTransformsAsync();
                }
            }
        }
    }
}
