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
        private static readonly Dictionary<string, AssetEntryV3> cacheAssetentriesV3 = new Dictionary<string, AssetEntryV3>();

        private static int _currentPageNumber = 0;
        private static bool _currentPageNumberIsMax = false; // true when we reached the max
        private static bool _initialized = false;
        private static SearchObject _searchinname = new SearchObject { SearchType = SearchIn.AssetNameEquals, Text = "" };
        private static string _statefilter = string.Empty;
        private static string _timefilter = FilterTime.AllItems;
        private static TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private static string _orderassets = OrderAssets.CreatedDescending;
        private static BackgroundWorker WorkerAnalyzeAssets;
        private static readonly Bitmap cancelimage = Bitmaps.cancel;
        private static readonly Bitmap clearimage = Bitmaps.clear;
        private static readonly Bitmap envelopeencryptedimage = Bitmaps.envelope_encryption;
        private static readonly Bitmap storageencryptedimage = Bitmaps.storage_encryption;
        private static readonly Bitmap storagedecryptedimage = Bitmaps.storage_decryption;
        private static readonly Bitmap CENCencryptedimage = Bitmaps.DRM_protection;
        private static readonly Bitmap CENCcbcsEncryptedImage = Bitmaps.DRM_protection_Cbcs;
        private static readonly Bitmap unsupportedencryptedimage = Bitmaps.help;
        private static readonly Bitmap SASlocatorimage = Bitmaps.SAS_locator;
        private static readonly Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        private static readonly Bitmap AssetFilterImage = Bitmaps.filter;
        private static readonly Bitmap AssetFiltersImage = Bitmaps.filters;
        private static readonly Bitmap Redstreamimage = Program.MakeRed(Streaminglocatorimage);
        private static readonly Bitmap Reddownloadimage = Program.MakeRed(SASlocatorimage);
        private static readonly Bitmap Bluestreamimage = Program.MakeBlue(Streaminglocatorimage);
        private static readonly Bitmap Bluedownloadimage = Program.MakeBlue(SASlocatorimage);
        private static readonly Bitmap BitmapCancel = Program.MakeRed(Bitmaps.cancel);
        private static AMSClientV3 _client;
        private static BindingList<AssetEntryV3> _MyObservAssetV3;
        private IPage<Asset> firstpage;

        public int CurrentPage => _currentPageNumber;

        public bool CurrentPageIsMax => _currentPageNumberIsMax;

        public string OrderAssetsInGrid
        {
            get => _orderassets;
            set => _orderassets = value;
        }
        public bool Initialized => _initialized;
        public SearchObject SearchInName
        {
            get => _searchinname;
            set => _searchinname = value;
        }


        public string StateFilter
        {
            get => _statefilter;
            set => _statefilter = value;
        }
        public string TimeFilter
        {
            get => _timefilter;
            set => _timefilter = value;
        }
        public TimeRangeValue TimeFilterTimeRange
        {
            get => _timefilterTimeRange;
            set => _timefilterTimeRange = value;
        }
        public int DisplayedCount => _MyObservAssetV3.Count();


        public void Init(AMSClientV3 client)
        {
            Debug.WriteLine("AssetsInit");

            client.RefreshTokenIfNeeded();

            _client = client;

            IEnumerable<AssetEntryV3> assets = _client.AMSclient.Assets.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName).Select(a => new AssetEntryV3
            {
                Name = a.Name,
                AssetId = a.AssetId,
                Type = a.Type,
                AlternateId = a.AlternateId,
                Created = a.Created.ToLocalTime().ToString("G"),
                StorageAccountName = a.StorageAccountName,
                Filters = null
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
            Columns.Add(imageCol);

            DataGridViewImageColumn imageCol3 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _dynEnc,
                DataPropertyName = _dynEnc,
            };
            Columns.Add(imageCol3);

            /*
            DataGridViewImageColumn imageCol4 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _filter,
                DataPropertyName = _filter,
            };
            this.Columns.Add(imageCol4);
            */

            //BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(assetquery.Take(0).ToList()); // just to create columns
            BindingList<AssetEntryV3> MyObservAssethisPageV3 = new BindingList<AssetEntryV3>(assets.ToList());


            DataSource = MyObservAssethisPageV3;

            int lastColumn_sIndex = Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).DisplayIndex;
            Columns[_dynEncMouseOver].Visible = false;
            Columns[_publicationMouseOver].Visible = false;
            Columns[_filterMouseOver].Visible = false;

            Columns[_locatorexpirationdatewarning].Visible = false; // used to store warning and put color in red
            Columns[_assetwarning].Visible = false; // used to store warning and put color in red
            Columns["Type"].HeaderText = "Type (streams nb)";
            Columns["Created"].HeaderText = "Created";
            Columns["AlternateId"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
            Columns["StorageAccountName"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
            Columns["SizeLong"].Visible = false;
            Columns[_filter].DisplayIndex = lastColumn_sIndex;
            Columns[_filter].DefaultCellStyle.NullValue = null;
            Columns[_publication].DisplayIndex = lastColumn_sIndex - 1;
            Columns[_publication].DefaultCellStyle.NullValue = null;
            Columns[_dynEnc].DisplayIndex = lastColumn_sIndex - 2;
            Columns[_dynEnc].DefaultCellStyle.NullValue = null;

            Columns[_dynEnc].HeaderText = "Dynamic Encryption";

            Columns["Type"].Width = 140;
            Columns["Size"].Width = 80;
            Columns[_dynEnc].Width = 80;
            Columns[_publication].Width = 90;
            Columns[_filter].Width = 50;
            Columns[_locatorexpirationdate].HeaderText = "Publication Expiration";
            Columns[_locatorexpirationdate].DisplayIndex = Columns.Count - 1;
            Columns[_locatorexpirationdate].Width = 130;
            Columns["Created"].Width = 140;
            Columns["AlternateId"].Width = 300;
            Columns["StorageAccountName"].Width = 140;

            WorkerAnalyzeAssets = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            WorkerAnalyzeAssets.DoWork += new System.ComponentModel.DoWorkEventHandler(WorkerAnalyzeAssets_DoWork);

            _initialized = true;
        }

        private void WorkerAnalyzeAssets_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("WorkerAnalyzeAssets_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            Asset asset = null;

            List<AssetEntryV3> listae = _MyObservAssetV3.OrderBy(a => cacheAssetentriesV3.ContainsKey(a.Name)).ToList(); // as priority, assets not yet analyzed


            // test - let analyze only visible assets

            int visibleRowsCount = DisplayedRowCount(true);
            int firstDisplayedRowIndex = (FirstDisplayedCell != null) ? FirstDisplayedCell.RowIndex : 0;
            int lastvisibleRowIndex = (firstDisplayedRowIndex + visibleRowsCount) - 1;
            List<string> VisibleAssets = new List<string>();
            for (int rowIndex = firstDisplayedRowIndex; rowIndex <= lastvisibleRowIndex; rowIndex++)
            {
                DataGridViewRow row = Rows[rowIndex];

                string assetname = row.Cells[Columns["Name"].Index].Value.ToString();
                VisibleAssets.Add(assetname);
            }

            IEnumerable<AssetEntryV3> query = from ae in listae join visAsset in VisibleAssets on ae.Name equals visAsset select ae;
            List<AssetEntryV3> listae2 = query.ToList();

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

                        AssetBitmapAndText assetBitmapAndText = DataGridViewAssets.BuildBitmapPublication(asset.Name, _client);
                        AE.Publication = assetBitmapAndText.bitmap;
                        AE.PublicationMouseOver = assetBitmapAndText.MouseOverDesc;

                        // var assetfiles =  asset.AssetFiles.ToList();
                        AssetInfoData data = AssetInfo.GetAssetType(asset.Name, _client);
                        if (data != null)
                        {
                            AE.Type = data.Type;
                            AE.SizeLong = data.Size;
                            AE.Size = AssetInfo.FormatByteSize(AE.SizeLong);
                            AE.AssetWarning = (AE.SizeLong == 0);
                        }

                        assetBitmapAndText = BuildBitmapDynEncryption(asset.Name, _client);
                        AE.DynamicEncryption = assetBitmapAndText.bitmap;
                        //AE.DynamicEncryptionMouseOver = assetBitmapAndText.MouseOverDesc;

                        if (assetBitmapAndText.Locators != null)
                        {
                            DateTime? LocDate = assetBitmapAndText.Locators.Any() ? (DateTime?)assetBitmapAndText.Locators.Min(l => l.EndTime).ToLocalTime() : null;
                            AE.LocatorExpirationDate = LocDate.HasValue ? ((DateTime)LocDate).ToLocalTime().ToString() : null;
                            AE.LocatorExpirationDateWarning = LocDate.HasValue ? (LocDate < DateTime.Now.ToLocalTime()) : false;
                        }

                        //assetBitmapAndText = BuildBitmapAssetFilters(asset.Name, _client);
                        int? afcount = ReturnNumberAssetFilters(asset.Name, _client);
                        AE.Filters = afcount > 0 ? afcount : null;
                        //AE.FiltersMouseOver = assetBitmapAndText.MouseOverDesc;

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
            BeginInvoke(new Action(() => Refresh()), null);
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

        private static readonly object lockGuard = new object();
        public void ReLaunchAnalyze()
        {
            if (!_initialized)
            {
                return;
            }

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
            if (!_initialized)
            {
                return;
            }

            if (pagetodisplay == 1)
            {
                _currentPageNumberIsMax = false;
            }

            Debug.WriteLine("RefreshAssets Start");

            if (WorkerAnalyzeAssets.IsBusy)
            {
                // cancel the analyze.
                WorkerAnalyzeAssets.CancelAsync();
            }
            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.WaitCursor));

            /*
              

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
            ODataQuery<Asset> odataQuery = new ODataQuery<Asset>();

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
                string search = _searchinname.Text;

                switch (_searchinname.SearchType)
                {
                    // Search on Asset name Equals
                    case SearchIn.AssetNameEquals:
                        search = "'" + search + "'";
                        odataQuery.Filter = "name eq " + search;
                        break;

                    // Search on Asset name starts with
                    case SearchIn.AssetNameStartsWith:
                        search = "'" + search + "'";
                        odataQuery.Filter = "name gt " + search.Substring(0, search.Length - 2) + char.ConvertFromUtf32(char.ConvertToUtf32(search, search.Length - 2) - 1) + new string('z', 262 - search.Length) + "'" + " and name lt " + search.Substring(0, search.Length - 2) + char.ConvertFromUtf32(char.ConvertToUtf32(search, search.Length - 2) + 1) + "'";
                        break;

                    // Search on Asset name Greater than
                    case SearchIn.AssetNameGreaterThan:
                        search = "'" + search + "'";
                        odataQuery.Filter = "name gt " + search;
                        break;

                    // Search on Asset name Less than
                    case SearchIn.AssetNameLessThan:
                        search = "'" + search + "'";
                        odataQuery.Filter = "name lt " + search;
                        break;

                    // Search on Asset aternate id
                    case SearchIn.AssetAltId:
                        search = "'" + search + "'";
                        odataQuery.Filter = "properties/alternateid eq " + search;
                        break;

                    case SearchIn.AssetId:
                        odataQuery.Filter = "properties/assetid eq " + search;
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
                odataQuery.Filter = odataQuery.Filter + $"properties/created gt {dateTimeStart.ToString("o")}";
            }
            if (filterEndDate)
            {
                if (odataQuery.Filter != null)
                {
                    odataQuery.Filter = odataQuery.Filter + " and ";
                }
                odataQuery.Filter = odataQuery.Filter + $"properties/created lt {dateTimeRangeEnd.ToString("o")}";
            }

            IPage<Asset> currentPage = null;
            await _client.RefreshTokenIfNeededAsync();

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
                if (currentPage.NextPageLink == null)
                {
                    _currentPageNumberIsMax = true; // we reached max
                }
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


            IEnumerable<AssetEntryV3> assets = currentPage.Select(a =>
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
                Created = a.Created.ToLocalTime().ToString("G"),
                LastModified = a.LastModified.ToLocalTime().ToString("G"),
                StorageAccountName = a.StorageAccountName
            }
         ));

            _MyObservAssetV3 = new BindingList<AssetEntryV3>(assets.ToList());

            BeginInvoke(new Action(() => DataSource = _MyObservAssetV3));

            Debug.WriteLine("RefreshAssets End");
            AnalyzeItemsInBackground();

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
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
            IList<AssetStreamingLocator> locators;
            try
            {
                locators = client.AMSclient.Assets.ListStreamingLocators(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName, assetName).StreamingLocators;
            }
            catch
            {
                return new AssetBitmapAndText()
                {
                    bitmap = BitmapCancel,
                    MouseOverDesc = "Error"
                };
            }

            foreach (AssetStreamingLocator locator in locators)
            {
                Bitmap newbitmap = null;
                string newtext = null;
                PublishStatus Status = AssetInfo.GetPublishedStatusForLocator(locator);

                {

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


        public static AssetBitmapAndText BuildBitmapDynEncryption(string assetName, AMSClientV3 client)
        {
            client.RefreshTokenIfNeeded();
            IList<AssetStreamingLocator> locators;
            try
            {
                locators = client.AMSclient.Assets.ListStreamingLocators(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName, assetName).StreamingLocators;
            }
            catch
            {
                return new AssetBitmapAndText()
                {
                    bitmap = BitmapCancel,
                    MouseOverDesc = "Error"
                };
            }

            if (locators.Count == 0)
            {
                return new AssetBitmapAndText();
            }

            AssetBitmapAndText ABT = new AssetBitmapAndText() { Locators = locators };

            bool ClearEnable = locators.Any(l => l.StreamingPolicyName == PredefinedStreamingPolicy.ClearStreamingOnly || l.StreamingPolicyName == PredefinedStreamingPolicy.DownloadAndClearStreaming);
            bool CENCEnable = locators.Any(l => l.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || l.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming);
            bool CENCCbcsEnable = locators.Any(l => l.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming);
            bool EnvelopeEnable = locators.Any(l => l.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey);


            int count = (ClearEnable ? 1 : 0) + (CENCEnable ? 1 : 0) + (CENCCbcsEnable ? 1 : 0) + (EnvelopeEnable ? 1 : 0);

            if (count == 0)
            {
                return new AssetBitmapAndText();
            }

            ABT.bitmap = new Bitmap((envelopeencryptedimage.Width * count), envelopeencryptedimage.Height);
            int x = 0;

            using (Graphics graphicsObject = Graphics.FromImage(ABT.bitmap))
            {
                if (ClearEnable)
                {
                    graphicsObject.DrawImage(clearimage, new Point(x, 0));
                    x += envelopeencryptedimage.Width;
                }

                if (EnvelopeEnable)
                {
                    graphicsObject.DrawImage(envelopeencryptedimage, new Point(x, 0));
                    x += envelopeencryptedimage.Width;
                }

                if (CENCEnable)
                {
                    graphicsObject.DrawImage(CENCencryptedimage, new Point(x, 0));
                    x += CENCencryptedimage.Width;
                }

                if (CENCCbcsEnable)
                {
                    graphicsObject.DrawImage(CENCcbcsEncryptedImage, new Point(x, 0));
                    x += CENCcbcsEncryptedImage.Width;
                }
            }

            return ABT;
        }


        public static int? ReturnNumberAssetFilters(string assetName, AMSClientV3 client)
        {
            client.RefreshTokenIfNeeded();
            IPage<AssetFilter> filters;
            try
            {
                filters = client.AMSclient.AssetFilters.List(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName, assetName);
            }
            catch
            {
                return null;
            }

            return filters.Count();

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
