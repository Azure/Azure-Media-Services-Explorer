//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
using Microsoft.Rest.Azure.OData;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    internal class ListViewAssets : ListView
    {
        private AMSClientV3 _client;
        private string _searchExactAssetName;
        private IPage<Asset> _assets;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderAssetName;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderAssetDate;
        private readonly System.Windows.Forms.ColumnHeader columnHeaderAssetDescription;

        public Asset GetSelectedAsset
        {
            get
            {
                if (SelectedItems.Count > 0)
                {
                    int indexName = columnHeaderAssetName.Index;
                    return _assets.Where(t => t.Name == SelectedItems[0].SubItems[indexName].Text).FirstOrDefault();
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
            await _client.RefreshTokenIfNeededAsync();

            ODataQuery<Asset> odataQuery = null;
            odataQuery = new ODataQuery<Asset>();

            if (_searchExactAssetName != null)
            {
                odataQuery.Filter = "name eq " + "'" + _searchExactAssetName + "'";
            }
            else
            {
                odataQuery.OrderBy = "Properties/Created desc";
            }
            _assets = await _client.AMSclient.Assets.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, odataQuery);

            BeginUpdate();
            Items.Clear();

            foreach (Asset asset in _assets)
            {
                ListViewItem item = new ListViewItem(asset.Name);
                item.SubItems.Add(asset.Description);
                item.SubItems.Add(asset.LastModified.ToLocalTime().ToString("G"));
                if (_searchExactAssetName == asset.Name)
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
