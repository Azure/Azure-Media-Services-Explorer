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
using Microsoft.Rest.Azure.OData;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    internal class ListViewAssets : ListView
    {
        private AMSClientV3 _client;
        private string _searchExactAssetName;
        private AsyncPageable<MediaAssetResource> _assets;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderAssetName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderAssetDate;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderAssetDescription;

        public MediaAssetResource GetSelectedAsset
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    int indexName = columnHeaderAssetName.Index;
                    return _client.AMSclient.GetMediaAsset(SelectedItems[0].SubItems[indexName].Text);
                }
                else
                {
                    return null;
                }
            }
        }

        public ListViewAssets()
        {
            columnHeaderAssetName = new System.Windows.Forms.ColumnHeader();
            columnHeaderAssetDescription = new System.Windows.Forms.ColumnHeader();
            columnHeaderAssetDate = new System.Windows.Forms.ColumnHeader();

            Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderAssetName,
            columnHeaderAssetDescription,
            columnHeaderAssetDate});
            FullRowSelect = true;
            HideSelection = false;
            Location = new System.Drawing.Point(32, 89);
            MultiSelect = false;
            Name = "listViewAssets";
            Size = new System.Drawing.Size(726, 194);
            TabIndex = 61;
            UseCompatibleStateImageBehavior = false;
            View = System.Windows.Forms.View.Details;
            Tag = -1;
            ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparer.ListView_ColumnClick);
            // 
            // columnHeaderAssetName
            // 
            columnHeaderAssetName.Text = "Asset Name";
            // 
            // columnHeaderAssetDate
            // 
            columnHeaderAssetDate.Text = "Last Modified";
            // 
            // columnHeaderAssetDescription
            // 
            columnHeaderAssetDescription.Text = "Description";

        }

        public async Task LoadAssetsAsync(AMSClientV3 client, string searchExactAssetName = null)
        {
            _client = client;
            _searchExactAssetName = searchExactAssetName;

            await LoadAssetsAsync();
        }
        private async Task LoadAssetsAsync()
        {
            ODataQuery<MediaAssetResource> odataQuery = new();

            if (_searchExactAssetName != null)
            {
                odataQuery.Filter = "name eq " + "'" + _searchExactAssetName + "'";
            }
            else
            {
                odataQuery.OrderBy = "Properties/Created desc";
            }
            _assets = _client.AMSclient.GetMediaAssets().GetAllAsync(filter: odataQuery.Filter, orderby: odataQuery.OrderBy);

            BeginUpdate();
            Items.Clear();

            await foreach (var asset in _assets)
            {
                ListViewItem item = new(asset.Data.Name);
                item.SubItems.Add(asset.Data.Description);
                item.SubItems.Add(asset.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G"));
                if (_searchExactAssetName == asset.Data.Name)
                {
                    item.Selected = true;
                }
                Items.Add(item);
            }

            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            EndUpdate();
        }
    }
}
