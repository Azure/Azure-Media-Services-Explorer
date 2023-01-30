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
using Microsoft.Rest.Azure.OData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
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
        private static readonly Dictionary<string, AssetEntry> cacheAssetentriesV3 = new();

        private static int _currentPageNumber = 0;
        private static bool _currentPageNumberIsMax = false; // true when we reached the max
        private static bool _initialized = false;
        private static SearchObject _searchinname = new() { SearchType = SearchIn.AssetNameEquals, Text = "" };
        private static string _statefilter = string.Empty;
        private static string _timefilter = FilterTime.AllItems;
        private static TimeRangeValue _timefilterTimeRange = new(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private static string _orderassets = OrderAssets.CreatedDescending;
        private static readonly Bitmap clearimage = Bitmaps.clear;
        private static readonly Bitmap envelopeencryptedimage = Bitmaps.envelope_encryption;
        private static readonly Bitmap CENCencryptedimage = Bitmaps.DRM_protection;
        private static readonly Bitmap CENCcbcsEncryptedImage = Bitmaps.DRM_protection_Cbcs;
        private static readonly Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        private static readonly Bitmap Redstreamimage = Program.MakeRed(Streaminglocatorimage);
        private static readonly Bitmap Bluestreamimage = Program.MakeBlue(Streaminglocatorimage);
        private static readonly Bitmap BitmapCancel = Program.MakeRed(Bitmaps.cancel);
        private static BindingList<AssetEntry> _MyObservAssetV3;
        private IPage<MediaAssetResource> firstpage;
        private SynchronizationContext _syncontext;

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
        public int DisplayedCount => _MyObservAssetV3.Count;


        public void Init(AMSClientV3 client, SynchronizationContext syncontext)
        {
            Debug.WriteLine("AssetsInit");

            _syncontext = syncontext;

            Pageable<MediaAssetResource> assetsList = Task.Run(() =>
                                        client.AMSclient.GetMediaAssets().GetAll()
                                        ).GetAwaiter().GetResult();

            IEnumerable<AssetEntry> assets = assetsList.Select(a => new AssetEntry(_syncontext)
            {
                Name = a.Data.Name,
                AssetId = a.Data.AssetId,
                //Type = a.Data.Type, //TODO2023
                AlternateId = a.Data.AlternateId,
                CreatedOn = a.Data.CreatedOn?.DateTime.ToLocalTime().ToString("G"),
                StorageAccountName = a.Data.StorageAccountName,
                Filters = null
            }
            );

            DataGridViewCellStyle cellstyle = new()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            DataGridViewImageColumn imageCol = new()
            {
                DefaultCellStyle = cellstyle,
                Name = _publication,
                DataPropertyName = _publication
            };
            Columns.Add(imageCol);

            DataGridViewImageColumn imageCol3 = new()
            {
                DefaultCellStyle = cellstyle,
                Name = _dynEnc,
                DataPropertyName = _dynEnc
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
            BindingList<AssetEntry> MyObservAssethisPageV3 = new(assets.ToList());

            DataSource = MyObservAssethisPageV3;

            int lastColumn_sIndex = Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).DisplayIndex;
            Columns[_dynEncMouseOver].Visible = false;
            Columns[_publicationMouseOver].Visible = false;
            Columns[_filterMouseOver].Visible = false;

            Columns[_locatorexpirationdatewarning].Visible = false; // used to store warning and put color in red
            Columns[_assetwarning].Visible = false; // used to store warning and put color in red
            Columns["Type"].HeaderText = "Type (streams nb)";
            Columns["CreatedOn"].HeaderText = "CreatedOn";
            Columns["AssetId"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
            Columns["AlternateId"].Visible = Properties.Settings.Default.DisplayAssetAltIDinGrid;
            Columns["StorageAccountName"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
            Columns["StorageAccountName"].HeaderText = "Storage account";
            Columns["SizeLong"].Visible = false;
            Columns["LastModifiedOn"].Visible = false;
            Columns[_filter].DisplayIndex = lastColumn_sIndex;
            Columns[_filter].DefaultCellStyle.NullValue = null;
            Columns[_publication].DisplayIndex = lastColumn_sIndex - 1;
            Columns[_publication].DefaultCellStyle.NullValue = null;
            Columns[_dynEnc].DisplayIndex = lastColumn_sIndex - 2;
            Columns[_dynEnc].DefaultCellStyle.NullValue = null;

            Columns[_dynEnc].HeaderText = "Dynamic Encryption";

            //Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["Type"].Width = 140;
            //Columns["Size"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["Size"].Width = 80;
            Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Columns["Name"].Width = 200;
            Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Columns["Description"].Width = 200;
            Columns[_dynEnc].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Columns[_dynEnc].Width = 80;
            Columns[_publication].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Columns[_publication].Width = 90;
            Columns[_filter].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Columns[_filter].Width = 50;
            Columns[_locatorexpirationdate].HeaderText = "Publication Expiration";
            Columns[_locatorexpirationdate].DisplayIndex = Columns.Count - 3;
            Columns[_locatorexpirationdate].Width = 130;
            Columns["CreatedOn"].DisplayIndex = Columns.Count - 1;
            //Columns["Created"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["Created"].Width = 140;
            //Columns["AlternateId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["AlternateId"].Width = 300;
            //Columns["StorageAccountName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["StorageAccountName"].Width = 140;


            _initialized = true;
        }


        private async Task WorkerAnalyzeAssets_DoWorkAsync(AMSClientV3 amsClient)
        {
            Debug.WriteLine("WorkerAnalyzeAssets_DoWork");
            MediaAssetResource asset = null;

            if (_MyObservAssetV3 == null) return;

            List<AssetEntry> listae;
            lock (cacheAssetentriesV3)
            {
                listae = _MyObservAssetV3.OrderBy(a => cacheAssetentriesV3.ContainsKey(a.Name)).ToList(); // as priority, assets not yet analyzed
            }
            // test - let analyze only visible assets
            int visibleRowsCount = DisplayedRowCount(true);
            if (visibleRowsCount == 0) visibleRowsCount = RowCount; // in some cases, DisplayedCount returns 0 so let's use all rows
            int firstDisplayedRowIndex = (FirstDisplayedCell != null) ? FirstDisplayedCell.RowIndex : 0;
            int lastvisibleRowIndex = (firstDisplayedRowIndex + visibleRowsCount) - 1;
            List<string> VisibleAssets = new();
            for (int rowIndex = firstDisplayedRowIndex; rowIndex <= lastvisibleRowIndex; rowIndex++)
            {
                if (Rows.Count > rowIndex)
                {
                    DataGridViewRow row = Rows[rowIndex];

                    string assetname = row.Cells[Columns["Name"].Index].Value.ToString();
                    VisibleAssets.Add(assetname);
                }
            }

            IEnumerable<AssetEntry> query = from ae in listae join visAsset in VisibleAssets on ae.Name equals visAsset select ae;
            List<AssetEntry> listae2 = query.ToList();


            float scale = DeviceDpi / 96f;

            foreach (AssetEntry AE in listae2)
            {
                await Task.Delay(1000);
                try
                {
                    bool existAsset = true;
                    try
                    {
                        asset = await amsClient.GetAssetAsync(AE.Name);
                    }
                    catch (RequestFailedException ex) when (ex.Status == ((int)System.Net.HttpStatusCode.NotFound))
                    {
                        existAsset = false;
                    }

                    if (existAsset)
                    {
                        Debug.WriteLine("analyze : " + asset.Data.Name);

                        AE.AlternateId = asset.Data.AlternateId;
                        AE.Description = asset.Data.Description;
                        AE.CreatedOn = asset.Data.CreatedOn?.DateTime.ToLocalTime().ToString("G");
                        AE.LastModifiedOn = asset.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G");
                        AE.Name = asset.Data.Name;

                        IList<MediaAssetStreamingLocator> locators = null;
                        try
                        {
                            locators = asset.GetStreamingLocators().ToList();
                        }
                        catch
                        {
                        }

                        AssetBitmapAndText assetBitmapAndText = await DataGridViewAssets.BuildBitmapPublicationAsync(asset.Data.Name, amsClient, locators);
                        if (assetBitmapAndText.bitmap != null)
                        {
                            AE.Publication = assetBitmapAndText.bitmap;
                        }
                        AE.PublicationMouseOver = assetBitmapAndText.MouseOverDesc;

                        AssetInfoData data = await AssetTools.GetAssetTypeAsync(asset, amsClient);
                        if (data != null)
                        {
                            AE.Type = data.Type;
                            AE.SizeLong = data.Size;
                            AE.Size = AssetTools.FormatByteSize(AE.SizeLong);
                            AE.AssetWarning = (AE.SizeLong == 0);
                        }

                        assetBitmapAndText = await BuildBitmapDynEncryptionAsync(asset, amsClient, locators);
                        if (assetBitmapAndText.bitmap != null)
                        {
                            AE.DynamicEncryption = assetBitmapAndText.bitmap;
                        }

                        if (assetBitmapAndText.Locators != null)
                        {
                            DateTimeOffset? LocDate = assetBitmapAndText.Locators.Any() ? assetBitmapAndText.Locators.Min(l => l.EndOn) : null;
                            AE.LocatorExpirationDate = LocDate.HasValue ? LocDate?.DateTime.ToLocalTime().ToString() : null;
                            AE.LocatorExpirationDateWarning = LocDate.HasValue && (LocDate < DateTimeOffset.Now);
                        }

                        int? afcount = await ReturnNumberAssetFiltersAsync(asset, amsClient);
                        AE.Filters = afcount > 0 ? afcount : null;

                        lock (cacheAssetentriesV3)
                        {
                            cacheAssetentriesV3[asset.Data.Name] = AE; // let's put it in cache (or update the cache)
                        }
                    }
                }
                catch // in some case, we have a timeout on Assets.Where...
                {
                }
                if (_analyzeAssetTaskTokenSource.IsCancellationRequested)
                {
                    return;
                }
            }
            //RefreshGridView();
            //BeginInvoke(new Action(() => Refresh()), null);
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


        public static void PurgeCacheAssets(List<MediaAssetResource> assets)
        {
            lock (cacheAssetentriesV3)
            {
                assets.ToList().ForEach(a => cacheAssetentriesV3.Remove(a.Data.Name));
            }
        }


        public void PurgeCacheAsset(MediaAssetResource asset)
        {
            lock (cacheAssetentriesV3)
            {
                cacheAssetentriesV3.Remove(asset.Data.Name);
            }
        }

        private static Task _analyzeAssetTask;
        private static CancellationTokenSource _analyzeAssetTaskTokenSource;
        private static long instanceNumber = 0;

        public async Task ReLaunchAnalyzeOfAssetsAsync(AMSClientV3 amsClient)
        {
            if (!_initialized)
            {
                return;
            }

            if (_analyzeAssetTask != null && !_analyzeAssetTask.IsCompleted)
            {
                // cancel the analyze.
                _analyzeAssetTaskTokenSource.Cancel();
                Debug.Print("ask for cancel");
            }

            // let's wait if the same function is called many times
            instanceNumber++;
            long instanceNb = instanceNumber;
            await Task.Delay(2000);
            if (instanceNb != instanceNumber)
            {
                return;
            }

            Debug.Print("wait for complete " + instanceNumber);
            if (_analyzeAssetTask != null)
            {
                while (!_analyzeAssetTask.IsCompleted)
                {
                    await Task.Delay(1000);
                }
            }

            // Start the asynchronous operation.
            try
            {
                _analyzeAssetTaskTokenSource = new CancellationTokenSource();
                Debug.Print("run again ! " + instanceNumber);
                _analyzeAssetTask = Task.Run(() => WorkerAnalyzeAssets_DoWorkAsync(amsClient), _analyzeAssetTaskTokenSource.Token);
            }
            catch { }
        }


        public async Task RefreshAssetsAsync(int pagetodisplay, AMSClientV3 amsClient) // all assets are refreshed
        {

            Debug.WriteLine("RefreshAssetsAsync");
            if (!_initialized)
            {
                return;
            }

            if (pagetodisplay == 1)
            {
                _currentPageNumberIsMax = false;
            }

            Debug.WriteLine("RefreshAssets Start");

            if (_analyzeAssetTask != null && !_analyzeAssetTask.IsCompleted)
            {
                // cancel the analyze.
                _analyzeAssetTaskTokenSource.Cancel();
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
            ODataQuery<MediaAssetResource> odataQuery = new()
            {
                OrderBy = _orderassets switch
                {
                    OrderAssets.CreatedDescending => "Properties/Created desc",
                    OrderAssets.CreatedAscending => "Properties/Created",
                    OrderAssets.NameAscending => "Name",
                    OrderAssets.NameDescending => "Name desc",
                    _ => "Properties/Created desc",
                }
            };



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
                        odataQuery.Filter = "name gt " + search.Substring(0, search.Length - 2) + char.ConvertFromUtf32(char.ConvertToUtf32(search, search.Length - 2) - 1) + new string('z', 262 - search.Length) + "'" + " and name lt " + search.Substring(0, search.Length - 1) + new string('z', 262 - search.Length) + "'";

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
                    odataQuery.Filter += " and ";
                }
                odataQuery.Filter += $"properties/created gt {dateTimeStart:o}";
            }
            if (filterEndDate)
            {
                if (odataQuery.Filter != null)
                {
                    odataQuery.Filter += " and ";
                }
                odataQuery.Filter += $"properties/created lt {dateTimeRangeEnd:o}";
            }

            // Paging
                      
            IReadOnlyList<MediaAssetResource> currentPage = null;
            var assetsQuery = amsClient.AMSclient.GetMediaAssets().GetAllAsync(filter: odataQuery.Filter, orderby: odataQuery.OrderBy);

            if (pagetodisplay == 1)
            {
                //firstpage = await amsClient.AMSclient.Assets.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, odataQuery);
                currentPage = (await assetsQuery.AsPages(null).FirstAsync()).Values;
            }
            else
            {
                string continuationToken = null;

                _currentPageNumber = 1;
                do
                {
                    _currentPageNumber++;
                    await foreach (var item in assetsQuery.AsPages(continuationToken))
                    {
                        continuationToken = item.ContinuationToken;
                        currentPage = item.Values;
                    }
                }
                while (continuationToken != null && pagetodisplay > _currentPageNumber);

                if (continuationToken == null)
                {
                    _currentPageNumberIsMax = true; // we reached max
                }
            }

            IEnumerable<AssetEntry> assets;
            lock (cacheAssetentriesV3)
            {
                assets = currentPage.Select(a =>
           (cacheAssetentriesV3.ContainsKey(a.Data.Name)
              && cacheAssetentriesV3[a.Data.Name].LastModifiedOn != null
              && (cacheAssetentriesV3[a.Data.Name].LastModifiedOn == a.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G")) ?
              cacheAssetentriesV3[a.Data.Name] :
           new AssetEntry(_syncontext)
           {
               Name = a.Data.Name,
               Description = a.Data.Description,
               AssetId = a.Data.AssetId,
               AlternateId = a.Data.AlternateId,
               CreatedOn = a.Data.CreatedOn?.DateTime.ToLocalTime().ToString("G"),
               LastModifiedOn = a.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G"),
               StorageAccountName = a.Data.StorageAccountName
           }
        ));
            }

            _MyObservAssetV3 = new BindingList<AssetEntry>(assets.ToList());

            Invoke(new Action(() => DataSource = _MyObservAssetV3));

            Debug.WriteLine("RefreshAssets End");
            await ReLaunchAnalyzeOfAssetsAsync(amsClient);

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
        }



        /// <summary>
        /// Returns a bitmap for publication
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="_amsClient"></param>
        /// <param name="locators"></param>Optional. Allow to not lts again the locators to reduce the number of calls
        /// <returns></returns>
        public static async Task<AssetBitmapAndText> BuildBitmapPublicationAsync(string assetName, AMSClientV3 _amsClient, IList<MediaAssetStreamingLocator> locators = null)
        {
            Bitmap returnedImage = null;
            string returnedText = null;

            if (locators == null)
            {
                try
                {
                    var asset = (await _amsClient.AMSclient.GetMediaAssetAsync(assetName)).Value;
                    locators = await asset.GetStreamingLocatorsAsync().ToListAsync();
                }
                catch
                {
                    return new AssetBitmapAndText()
                    {
                        bitmap = BitmapCancel,
                        MouseOverDesc = "Error"
                    };
                }
            }

            foreach (var locator in locators)
            {
                Bitmap newbitmap = null;
                string newtext = null;
                PublishStatus Status = AssetTools.GetPublishedStatusForLocator(locator);
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

        /// <summary>
        /// Returns a bitmap regarding the Dyn Encryption
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="amsClient"></param>
        /// <param name="locators"></param>If specified, avoid the function to list again them (optimization)
        /// <returns></returns>
        public static async Task<AssetBitmapAndText> BuildBitmapDynEncryptionAsync(MediaAssetResource asset, AMSClientV3 amsClient, IList<MediaAssetStreamingLocator> locators = null)
        {
            if (locators == null)
            {
                try
                {
                    locators = asset.GetStreamingLocators().ToList();
                }
                catch
                {
                    return new AssetBitmapAndText()
                    {
                        bitmap = BitmapCancel,
                        MouseOverDesc = "Error"
                    };
                }
            }

            if (locators.Count == 0)
            {
                return new AssetBitmapAndText();
            }

            AssetBitmapAndText ABT = new() { Locators = locators };

            bool ClearEnable = locators.Any(l => l.StreamingPolicyName == "Predefined_ClearStreamingOnly" || l.StreamingPolicyName == "Predefined_DownloadAndClearStreaming");
            bool CENCEnable = locators.Any(l => l.StreamingPolicyName == "Predefined_MultiDrmCencStreaming" || l.StreamingPolicyName == "Predefined_MultiDrmStreaming");
            bool CENCCbcsEnable = locators.Any(l => l.StreamingPolicyName == "Predefined_MultiDrmStreaming");
            bool EnvelopeEnable = locators.Any(l => l.StreamingPolicyName == "Predefined_ClearKey");

            int count = (ClearEnable ? 1 : 0) + (CENCEnable ? 1 : 0) + (CENCCbcsEnable ? 1 : 0) + (EnvelopeEnable ? 1 : 0);

            if (count == 0)
            {
                return new AssetBitmapAndText();
            }

            Bitmap returnedImage = null;

            if (ClearEnable)
            {
                returnedImage = AddBitmap(returnedImage, clearimage);
            }

            if (EnvelopeEnable)
            {
                returnedImage = AddBitmap(returnedImage, envelopeencryptedimage);
            }

            if (CENCEnable)
            {
                returnedImage = AddBitmap(returnedImage, CENCencryptedimage);
            }

            if (CENCCbcsEnable)
            {
                returnedImage = AddBitmap(returnedImage, CENCcbcsEncryptedImage);
            }

            ABT.bitmap = returnedImage;

            return ABT;
        }


        public static async Task<int?> ReturnNumberAssetFiltersAsync(MediaAssetResource asset, AMSClientV3 client)
        {
            // asset filters
            int count = 0;
            try
            {
                //var asset = await client.AMSclient.GetMediaAssetAsync(assetName);
                var filters = asset.GetMediaAssetFilters().GetAllAsync();
                await foreach (var filter in filters)
                {
                    count++;
                }
            }
            catch
            {
                return null;
            }

            return count;
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

        private void RefreshGridView()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    RefreshGridView();
                });
            }
            else
                Refresh();
        }
    }


    public class SemaphoreLocker
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task LockAsync(Func<Task> worker)
        {
            await _semaphore.WaitAsync();
            try
            {
                await worker();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
