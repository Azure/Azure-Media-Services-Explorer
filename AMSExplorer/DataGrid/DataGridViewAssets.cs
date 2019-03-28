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
using Microsoft.Rest.Azure.OData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class DataGridViewAssets : DataGridView
    {
        public string _publication = "Publication";
        public string _dynEnc = "DynamicEncryption";
        public string _filter = "Filters";
        public string _publicationMouseOver = "PublicationMouseOver";
        public string _dynEncMouseOver = "DynamicEncryptionMouseOver";
        public string _filterMouseOver = "FiltersMouseOver";

        public string _locatorexpirationdate = "LocatorExpirationDate";
        public string _locatorexpirationdatewarning = "LocatorExpirationDateWarning";
        public string _assetwarning = "AssetWarning";

        static Dictionary<string, AssetEntryV3> cacheAssetentriesV3 = new Dictionary<string, AssetEntryV3>();

        static private int _currentPageNumber = 0;
        static private bool _currentPageNumberIsMax = false; // true when we reached the max
        static private bool _initialized = false;
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.AssetNameEquals, Text = "" };
        static private string _statefilter = "";
        static private string _timefilter = FilterTime.AllItems;
        static private TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        static string _orderassets = OrderAssets.CreatedDescending;
        static BackgroundWorker WorkerAnalyzeAssets;

        static Bitmap cancelimage = Bitmaps.cancel;
        static Bitmap envelopeencryptedimage = Bitmaps.envelope_encryption;
        static Bitmap storageencryptedimage = Bitmaps.storage_encryption;
        static Bitmap storagedecryptedimage = Bitmaps.storage_decryption;
        static Bitmap CENCencryptedimage = Bitmaps.DRM_protection;
        static Bitmap CENCcbcsEncryptedImage = Bitmaps.DRM_protection_Cbcs;
        static Bitmap unsupportedencryptedimage = Bitmaps.help;
        static Bitmap SASlocatorimage = Bitmaps.SAS_locator;
        static Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        static Bitmap AssetFilterImage = Bitmaps.filter;
        static Bitmap AssetFiltersImage = Bitmaps.filters;
        static Bitmap Redstreamimage = Program.MakeRed(Streaminglocatorimage);
        static Bitmap Reddownloadimage = Program.MakeRed(SASlocatorimage);
        static Bitmap Bluestreamimage = Program.MakeBlue(Streaminglocatorimage);
        static Bitmap Bluedownloadimage = Program.MakeBlue(SASlocatorimage);

        static AMSClientV3 _client;
        static BindingList<AssetEntryV3> _MyObservAssetV3;
        private IPage<Asset> firstpage;

        public int CurrentPage
        {
            get
            {
                return _currentPageNumber;
            }
        }

        public bool CurrentPageIsMax
        {
            get
            {
                return _currentPageNumberIsMax;
            }
        }

        public string OrderAssetsInGrid
        {
            get
            {
                return _orderassets;
            }
            set
            {
                _orderassets = value;
            }
        }
        public bool Initialized
        {
            get
            {
                return _initialized;
            }
        }
        public SearchObject SearchInName
        {
            get
            {
                return _searchinname;
            }
            set
            {
                _searchinname = value;
            }
        }


        public string StateFilter
        {
            get
            {
                return _statefilter;
            }
            set
            {
                _statefilter = value;
            }
        }
        public string TimeFilter
        {
            get
            {
                return _timefilter;
            }
            set
            {
                _timefilter = value;
            }
        }
        public TimeRangeValue TimeFilterTimeRange
        {
            get
            {
                return _timefilterTimeRange;
            }
            set
            {
                _timefilterTimeRange = value;
            }
        }
        public int DisplayedCount
        {
            get
            {
                return _MyObservAssetV3.Count();
            }
        }


        public void Init(AMSClientV3 client)
        {
            Debug.WriteLine("AssetsInit");

            client.RefreshTokenIfNeeded();

            _client = client;

            var assets = _client.AMSclient.Assets.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName).Select(a => new AssetEntryV3
            {
                Name = a.Name,
                AssetId = a.AssetId,
                Type = a.Type,
                AlternateId = a.AlternateId,
                Created = ((DateTime)a.Created).ToLocalTime().ToString("G"),
                StorageAccountName = a.StorageAccountName
            }
            );

            /*
            assetquery = from a in client.Assets.Take(0)
                         orderby a.LastModified descending
                         select new AssetEntry
                         {
                             Name = a.Name,
                             Id = a.Id,
                             AlternateId = a.AlternateId,
                             LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G"),
                             Storage = a.StorageAccountName
                         };
                         */

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            DataGridViewImageColumn imageCol = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _publication,
                DataPropertyName = _publication,
            };
            this.Columns.Add(imageCol);

            DataGridViewImageColumn imageCol3 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _dynEnc,
                DataPropertyName = _dynEnc,
            };
            this.Columns.Add(imageCol3);

            DataGridViewImageColumn imageCol4 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _filter,
                DataPropertyName = _filter,
            };
            this.Columns.Add(imageCol4);

            //BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(assetquery.Take(0).ToList()); // just to create columns
            BindingList<AssetEntryV3> MyObservAssethisPageV3 = new BindingList<AssetEntryV3>(assets.ToList());


            this.DataSource = MyObservAssethisPageV3;

            int lastColumn_sIndex = this.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).DisplayIndex;
            this.Columns[_dynEncMouseOver].Visible = false;
            this.Columns[_publicationMouseOver].Visible = false;
            this.Columns[_filterMouseOver].Visible = false;

            this.Columns[_locatorexpirationdatewarning].Visible = false; // used to store warning and put color in red
            this.Columns[_assetwarning].Visible = false; // used to store warning and put color in red
            this.Columns["Type"].HeaderText = "Type (streams nb)";
            this.Columns["Created"].HeaderText = "Created";
            this.Columns["AlternateId"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
            this.Columns["StorageAccountName"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
            this.Columns["SizeLong"].Visible = false;
            this.Columns[_filter].DisplayIndex = lastColumn_sIndex;
            this.Columns[_filter].DefaultCellStyle.NullValue = null;
            this.Columns[_publication].DisplayIndex = lastColumn_sIndex - 1;
            this.Columns[_publication].DefaultCellStyle.NullValue = null;
            this.Columns[_dynEnc].DisplayIndex = lastColumn_sIndex - 2;
            this.Columns[_dynEnc].DefaultCellStyle.NullValue = null;

            this.Columns[_dynEnc].HeaderText = "Dynamic Encryption";

            this.Columns["Type"].Width = 140;
            this.Columns["Size"].Width = 80;
            this.Columns[_dynEnc].Width = 80;
            this.Columns[_publication].Width = 90;
            this.Columns[_filter].Width = 50;
            this.Columns[_locatorexpirationdate].HeaderText = "Publication Expiration";
            this.Columns[_locatorexpirationdate].DisplayIndex = this.Columns.Count - 1;
            this.Columns[_locatorexpirationdate].Width = 130;
            this.Columns["Created"].Width = 140;
            this.Columns["AlternateId"].Width = 300;
            this.Columns["StorageAccountName"].Width = 140;

            WorkerAnalyzeAssets = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            WorkerAnalyzeAssets.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAnalyzeAssets_DoWork);

            _initialized = true;
        }

        private void WorkerAnalyzeAssets_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("WorkerAnalyzeAssets_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            Asset asset = null;

            var listae = _MyObservAssetV3.OrderBy(a => cacheAssetentriesV3.ContainsKey(a.Name)).ToList(); // as priority, assets not yet analyzed


            // test - let analyze only visible assets

            var visibleRowsCount = this.DisplayedRowCount(true);
            var firstDisplayedRowIndex = this.FirstDisplayedCell.RowIndex;
            var lastvisibleRowIndex = (firstDisplayedRowIndex + visibleRowsCount) - 1;
            var VisibleAssets = new List<String>();
            for (int rowIndex = firstDisplayedRowIndex; rowIndex <= lastvisibleRowIndex; rowIndex++)
            {
                var row = this.Rows[rowIndex];

                var assetname = row.Cells[this.Columns["Name"].Index].Value.ToString();
                VisibleAssets.Add(assetname);
            }

            var query = from ae in listae join visAsset in VisibleAssets on ae.Name equals visAsset select ae;
            var listae2 = query.ToList();

            _client.RefreshTokenIfNeeded();

            foreach (AssetEntryV3 AE in listae2)
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    asset = _client.AMSclient.Assets.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, AE.Name);

                    /*
                    var firstPage = await MediaServicesArmClient.Assets.ListAsync(TestSettings.CustomerResourceGroup, TestSettings.CustomerAccountName);

                    var currentPage = firstPage;
                    while (currentPage.NextPageLink != null)
                    {
                        currentPage = await MediaServicesArmClient.Assets.ListNextAsync(currentPage.NextPageLink);
                    }
                    */

                    if (asset != null)
                    {
                        //AssetInfo myAssetInfo = new AssetInfo(asset);
                        AE.AlternateId = asset.AlternateId;
                        AE.Description = asset.Description;
                        AE.Created = asset.Created.ToLocalTime().ToString("G");
                        AE.LastModified = asset.LastModified.ToLocalTime().ToString("G");
                        AE.Name = asset.Name;
                        // AE.StorageAccountName = asset.StorageAccountName;
                        // AE.StorageEncryptionFormat = asset.StorageEncryptionFormat;

                        //SASLoc = myAssetInfo.GetPublishedStatus(LocatorType.Sas);
                        //OrigLoc = myAssetInfo.GetPublishedStatus(LocatorType.OnDemandOrigin);

                        /*
                        AssetBitmapAndText assetBitmapAndText = ReturnStaticProtectedBitmap(asset);
                        AE.StaticEncryption = assetBitmapAndText.bitmap;
                        AE.StaticEncryptionMouseOver = assetBitmapAndText.MouseOverDesc;
                        */

                        var assetBitmapAndText = DataGridViewAssets.BuildBitmapPublication(asset.Name, _client);
                        AE.Publication = assetBitmapAndText.bitmap;
                        AE.PublicationMouseOver = assetBitmapAndText.MouseOverDesc;

                        // var assetfiles =  asset.AssetFiles.ToList();
                        var data = AssetInfo.GetAssetType(asset.Name, _client);
                        AE.Type = data.Type;
                        AE.SizeLong = data.Size;
                        AE.Size = AssetInfo.FormatByteSize(AE.SizeLong);
                        AE.AssetWarning = (AE.SizeLong == 0);// || assetfiles.Any(f => f.ContentFileSize == 0));

                        /*
                        AE.DynamicEncryption = assetBitmapAndText.bitmap;
                        AE.DynamicEncryptionMouseOver = assetBitmapAndText.MouseOverDesc;
                        */

                        /*
                        DateTime? LocDate = asset.Locators.Any() ? (DateTime?)asset.Locators.Min(l => l.ExpirationDateTime).ToLocalTime() : null;
                        AE.LocatorExpirationDate = LocDate.HasValue ? ((DateTime)LocDate).ToLocalTime().ToString() : null;
                        AE.LocatorExpirationDateWarning = LocDate.HasValue ? (LocDate < DateTime.Now.ToLocalTime()) : false;
                        */

                        /*
                        AE.Filters = assetBitmapAndText.bitmap;
                        AE.FiltersMouseOver = assetBitmapAndText.MouseOverDesc;
                        */

                        cacheAssetentriesV3[asset.Name] = AE; // let's put it in cache (or update the cache)
                    }
                }
                catch // in some case, we have a timeout on Assets.Where...
                {

                }
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }

            }
            this.BeginInvoke(new Action(() => this.Refresh()), null);
        }

        /*
        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if (_neveranalyzed) // first display of the page, let's analyzed the assets
            {
                _neveranalyzed = false;
                AnalyzeItemsInBackground();

            }
            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(_MyObservAsset.Skip(_assetsperpage * (page - 1)).Take(_assetsperpage).ToList());

                this.DataSource = MyObservAssethisPage;
            }
        }
        */

     
        public void PurgeCacheAssetsV3(List<Asset> assets)
        {
            assets.ToList().ForEach(a => cacheAssetentriesV3.Remove(a.Name));
        }


        public void PurgeCacheAsset(Asset asset)
        {
            cacheAssetentriesV3.Remove(asset.Name);
        }

        private static Object lockGuard = new Object();
        public void ReLaunchAnalyze()
        {
            if (!_initialized) return;

            lock (lockGuard)
            {
                if (WorkerAnalyzeAssets.IsBusy)
                {
                    // cancel the analyze.
                    WorkerAnalyzeAssets.CancelAsync();
                }
                AnalyzeItemsInBackground();
            }
        }

        public async Task RefreshAssetsAsync(int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;
            if (pagetodisplay == 1) _currentPageNumberIsMax = false;
            Debug.WriteLine("RefreshAssets Start");

            if (WorkerAnalyzeAssets.IsBusy)
            {
                // cancel the analyze.
                WorkerAnalyzeAssets.CancelAsync();
            }
            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));

            /*
             * 

Property
Name	Filtering	Ordering
	Equals	Greater than	Less Than	Ascending	Descending
Name	✓	✓	✓	✓	✓
Properties/AssetId	✓				
Properties/Created	✓	✓	✓	✓	✓
Properties/LastModified					
Properties/AlternateId	✓				
Properties/Description					
Properties/Container					
Properties/StorageId					


             */


            ///////////////////////
            // SORTING
            ///////////////////////
            var odataQuery = new ODataQuery<Asset>();

            switch (_orderassets)
            {
                case OrderAssets.CreatedDescending:
                    odataQuery.OrderBy = "Properties/Created desc";
                    break;

                case OrderAssets.CreatedAscending:
                    odataQuery.OrderBy = "Properties/Created";
                    break;

                case OrderAssets.NameAscending:
                    odataQuery.OrderBy = "Name";
                    break;

                case OrderAssets.NameDescending:
                    odataQuery.OrderBy = "Name desc";
                    break;

                default:
                    odataQuery.OrderBy = "Properties/Created desc";
                    break;
            }



            ///////////////////////
            // SEARCH
            ///////////////////////
            if (_searchinname != null && !string.IsNullOrEmpty(_searchinname.Text))
            {
                string search = "'" + _searchinname.Text + "'";
                switch (_searchinname.SearchType)
                {
                    // Search on Asset name Equals
                    case SearchIn.AssetNameEquals:
                        odataQuery.Filter = "name eq " + search;
                        break;

                    // Search on Asset name Greater than
                    case SearchIn.AssetNameGreaterThan:
                        odataQuery.Filter = "name gt " + search;
                        break;

                    // Search on Asset name Less than
                    case SearchIn.AssetNameLessThan:
                        odataQuery.Filter = "name lt " + search;
                        break;

                    // Search on Asset aternate id
                    case SearchIn.AssetAltId:
                        odataQuery.Filter = "Properties/AlternateId eq " + search;
                        break;

                    case SearchIn.AssetId:
                        odataQuery.Filter = "Properties/AssetId eq " + search;
                        break;

                    default:
                        break;
                }
            }

            // DAYS
            bool filterStartDate = false;
            bool filterEndDate = false;

            DateTime dateTimeStart = DateTime.UtcNow;
            DateTime dateTimeRangeEnd = DateTime.UtcNow.AddDays(1);

            int days = FilterTime.ReturnNumberOfDays(_timefilter);
            if (days > 0)
            {
                filterStartDate = true;
                dateTimeStart = (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)));
            }
            else if (days == -1) // TimeRange
            {
                filterStartDate = true;
                filterEndDate = true;
                dateTimeStart = _timefilterTimeRange.StartDate;
                if (_timefilterTimeRange.EndDate != null) // there is an end time
                {
                    dateTimeRangeEnd = (DateTime)_timefilterTimeRange.EndDate;
                }
            }
            if (filterStartDate)
            {
                if (odataQuery.Filter != null)
                {
                    odataQuery.Filter = odataQuery.Filter + " and ";
                }
                odataQuery.Filter = odataQuery.Filter + $"Properties/Created gt {dateTimeStart.ToString("o")}";
            }
            if (filterEndDate)
            {
                if (odataQuery.Filter != null)
                {
                    odataQuery.Filter = odataQuery.Filter + " and ";
                }
                odataQuery.Filter = odataQuery.Filter + $"Properties/Created lt {dateTimeRangeEnd.ToString("o")}";
            }

            IPage<Asset> currentPage = null;
            _client.RefreshTokenIfNeeded();


            if (pagetodisplay == 1)
            {
                firstpage = await _client.AMSclient.Assets.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, odataQuery);
                currentPage = firstpage;
            }
            else
            {
                currentPage = firstpage;
                _currentPageNumber = 1;
                while (currentPage.NextPageLink != null && pagetodisplay > _currentPageNumber)
                {
                    _currentPageNumber++;
                    currentPage = await _client.AMSclient.Assets.ListNextAsync(currentPage.NextPageLink);
                }
                if (currentPage.NextPageLink == null) _currentPageNumberIsMax = true; // we reached max
            }




            /*
            var assets = currentPage.Select(a => new AssetEntryV3
            {
                Name = a.Name,
                Description = a.Description,
                AssetId = a.AssetId,
                AlternateId = a.AlternateId,
                Created = ((DateTime)a.Created).ToLocalTime().ToString("G"),
                StorageAccountName = a.StorageAccountName
            }
            );
            */


            var assets = currentPage.Select(a =>
            (cacheAssetentriesV3.ContainsKey(a.Name)
               && cacheAssetentriesV3[a.Name].LastModified != null
               && (cacheAssetentriesV3[a.Name].LastModified == a.LastModified.ToLocalTime().ToString("G")) ?
               cacheAssetentriesV3[a.Name] :
            new AssetEntryV3
            {
                Name = a.Name,
                Description = a.Description,
                AssetId = a.AssetId,
                AlternateId = a.AlternateId,
                Created = ((DateTime)a.Created).ToLocalTime().ToString("G"),
                LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G"),
                StorageAccountName = a.StorageAccountName
            }
         ));

            _MyObservAssetV3 = new BindingList<AssetEntryV3>(assets.ToList());

            this.BeginInvoke(new Action(() => this.DataSource = _MyObservAssetV3));

            Debug.WriteLine("RefreshAssets End");
            AnalyzeItemsInBackground();

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));
        }


        public void AnalyzeItemsInBackground()
        {
            Task.Run(() =>
            {
                WorkerAnalyzeAssets.CancelAsync();
                // let's wait a little for the previous worker to cancel if needed
                System.Threading.Thread.Sleep(2000);

                if (WorkerAnalyzeAssets.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    try
                    {
                        WorkerAnalyzeAssets.RunWorkerAsync();
                    }
                    catch { }
                }
            });
        }


        public static AssetBitmapAndText BuildBitmapPublication(string assetName, AMSClientV3 client)
        {
            Bitmap returnedImage = null;
            string returnedText = null;
            client.RefreshTokenIfNeeded();

            foreach (var locator in client.AMSclient.Assets.ListStreamingLocators(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName, assetName).StreamingLocators)
            {
                Bitmap newbitmap = null;
                string newtext = null;
                PublishStatus Status = AssetInfo.GetPublishedStatusForLocator(locator);

                //switch (locator.StreamingPolicyName)
                {

                    //                    case (LocatorType.OnDemandOrigin):
                    switch (Status)
                    {
                        case PublishStatus.PublishedActive:
                            newbitmap = Streaminglocatorimage;
                            newtext = "Active Streaming locator";
                            break;

                        case PublishStatus.PublishedExpired:
                            newbitmap = Redstreamimage;
                            newtext = "Expired Streaming locator";
                            break;

                        case PublishStatus.PublishedFuture:
                            newbitmap = Bluestreamimage;
                            newtext = "Future Streaming locator";
                            break;

                        case PublishStatus.NotPublished:
                        default:
                            break;
                    }
                    //break;
                    /*
                                        case (LocatorType.Sas):
                                            switch (Status)
                                            {
                                                case PublishStatus.PublishedActive:
                                                    newbitmap = SASlocatorimage;
                                                    newtext = "Active SAS locator";
                                                    break;

                                                case PublishStatus.PublishedExpired:
                                                    newbitmap = Reddownloadimage;
                                                    newtext = "Expired SAS locator";
                                                    break;

                                                case PublishStatus.PublishedFuture:
                                                    newbitmap = Bluedownloadimage;
                                                    newtext = "Future SAS locator";
                                                    break;

                                                case PublishStatus.NotPublished:
                                                default:
                                                    break;
                                            }
                                            break;


                                        default:
                                            break;
                                            */
                }

                returnedImage = AddBitmap(returnedImage, newbitmap);
                returnedText += !string.IsNullOrEmpty(newtext) ? newtext + Constants.endline : string.Empty;
            }

            return new AssetBitmapAndText()
            {
                bitmap = returnedImage,
                MouseOverDesc = returnedText ?? "Not published"
            };
        }

        private static Bitmap AddBitmap(Bitmap bitmap1, Bitmap bitmap2)
        {
            Bitmap returnedbitmap;
            if (bitmap1 != null)
            {
                returnedbitmap = new Bitmap((bitmap1.Width + bitmap2.Width), bitmap1.Height);
                using (Graphics graphicsObject = Graphics.FromImage(returnedbitmap))
                {
                    graphicsObject.DrawImage(bitmap1, new Point(0, 0));
                    graphicsObject.DrawImage(bitmap2, new Point(bitmap1.Width, 0));
                }

            }
            else
            {
                returnedbitmap = bitmap2;
            }

            return returnedbitmap;
        }
    }
}
