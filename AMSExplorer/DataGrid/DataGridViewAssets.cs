//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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
        private static readonly Bitmap clearimage = Bitmaps.clear;
        private static readonly Bitmap envelopeencryptedimage = Bitmaps.envelope_encryption;
        private static readonly Bitmap CENCencryptedimage = Bitmaps.DRM_protection;
        private static readonly Bitmap CENCcbcsEncryptedImage = Bitmaps.DRM_protection_Cbcs;
        private static readonly Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        private static readonly Bitmap Redstreamimage = Program.MakeRed(Streaminglocatorimage);
        private static readonly Bitmap Bluestreamimage = Program.MakeBlue(Streaminglocatorimage);
        private static readonly Bitmap BitmapCancel = Program.MakeRed(Bitmaps.cancel);
        private static AMSClientV3 _amsClient;
        private static BindingList<AssetEntryV3> _MyObservAssetV3;
        private IPage<Asset> firstpage;
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
        public int DisplayedCount => _MyObservAssetV3.Count();


        public void Init(AMSClientV3 client, SynchronizationContext syncontext)
        {
            Debug.WriteLine("AssetsInit");

            _syncontext = syncontext;

            client.RefreshTokenIfNeeded();

            _amsClient = client;

            IPage<Asset> assetsList = Task.Run(() =>
                                        _amsClient.AMSclient.Assets.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)
                                        ).GetAwaiter().GetResult();

            IEnumerable<AssetEntryV3> assets = assetsList.Select(a => new AssetEntryV3(_syncontext)
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

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            DataGridViewImageColumn imageCol = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _publication,
                DataPropertyName = _publication
            };
            Columns.Add(imageCol);

            DataGridViewImageColumn imageCol3 = new DataGridViewImageColumn()
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
            Columns["AssetId"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
            Columns["AlternateId"].Visible = Properties.Settings.Default.DisplayAssetAltIDinGrid;
            Columns["StorageAccountName"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
            Columns["StorageAccountName"].HeaderText = "Storage account";
            Columns["SizeLong"].Visible = false;
            Columns["LastModified"].Visible = false;
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
            Columns["Created"].DisplayIndex = Columns.Count - 1;
            //Columns["Created"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["Created"].Width = 140;
            //Columns["AlternateId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["AlternateId"].Width = 300;
            //Columns["StorageAccountName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Columns["StorageAccountName"].Width = 140;

            WorkerAnalyzeAssets = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            WorkerAnalyzeAssets.DoWork += new System.ComponentModel.DoWorkEventHandler(WorkerAnalyzeAssets_DoWork);

            _initialized = true;
        }

        private void WorkerAnalyzeAssets_DoWork(object sender, DoWorkEventArgs e)
        {
            Task.Run(() => WorkerAnalyzeAssets_DoWorkAsync(sender, e)).ConfigureAwait(false);
        }


        private async Task WorkerAnalyzeAssets_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("WorkerAnalyzeAssets_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            Asset asset = null;

            if (_MyObservAssetV3 == null) return;

            List<AssetEntryV3> listae = _MyObservAssetV3.OrderBy(a => cacheAssetentriesV3.ContainsKey(a.Name)).ToList(); // as priority, assets not yet analyzed

            // test - let analyze only visible assets
            int visibleRowsCount = DisplayedRowCount(true);
            if (visibleRowsCount == 0) visibleRowsCount = RowCount; // in some cases, DisplayedCount returns 0 so let's use all rows
            int firstDisplayedRowIndex = (FirstDisplayedCell != null) ? FirstDisplayedCell.RowIndex : 0;
            int lastvisibleRowIndex = (firstDisplayedRowIndex + visibleRowsCount) - 1;
            List<string> VisibleAssets = new List<string>();
            for (int rowIndex = firstDisplayedRowIndex; rowIndex <= lastvisibleRowIndex; rowIndex++)
            {
                if (Rows.Count > rowIndex)
                {
                    DataGridViewRow row = Rows[rowIndex];

                    string assetname = row.Cells[Columns["Name"].Index].Value.ToString();
                    VisibleAssets.Add(assetname);
                }
            }

            IEnumerable<AssetEntryV3> query = from ae in listae join visAsset in VisibleAssets on ae.Name equals visAsset select ae;
            List<AssetEntryV3> listae2 = query.ToList();

            await _amsClient.RefreshTokenIfNeededAsync();
            float scale = DeviceDpi / 96f;

            foreach (AssetEntryV3 AE in listae2)
            {
                await Task.Delay(1000);
                try
                {
                    asset = await _amsClient.AMSclient.Assets.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, AE.Name);

                    if (asset != null)
                    {
                        Debug.WriteLine("analyze : " + asset.Name);

                        AE.AlternateId = asset.AlternateId;
                        AE.Description = asset.Description;
                        AE.Created = asset.Created.ToLocalTime().ToString("G");
                        AE.LastModified = asset.LastModified.ToLocalTime().ToString("G");
                        AE.Name = asset.Name;

                        IList<AssetStreamingLocator> locators = null;
                        try
                        {
                            locators = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name))
                                .StreamingLocators;
                        }
                        catch
                        {
                        }

                        AssetBitmapAndText assetBitmapAndText = await DataGridViewAssets.BuildBitmapPublicationAsync(asset.Name, _amsClient, locators);
                        if (assetBitmapAndText.bitmap != null)
                        {
                            AE.Publication = assetBitmapAndText.bitmap;
                        }
                        AE.PublicationMouseOver = assetBitmapAndText.MouseOverDesc;

                        AssetInfoData data = await AssetInfo.GetAssetTypeAsync(asset.Name, _amsClient);
                        if (data != null)
                        {
                            AE.Type = data.Type;
                            AE.SizeLong = data.Size;
                            AE.Size = AssetInfo.FormatByteSize(AE.SizeLong);
                            AE.AssetWarning = (AE.SizeLong == 0);
                        }

                        assetBitmapAndText = await BuildBitmapDynEncryptionAsync(asset.Name, _amsClient, locators);
                        if (assetBitmapAndText.bitmap != null)
                        {
                            AE.DynamicEncryption = assetBitmapAndText.bitmap;
                        }

                        if (assetBitmapAndText.Locators != null)
                        {
                            DateTime? LocDate = assetBitmapAndText.Locators.Any() ? (DateTime?)assetBitmapAndText.Locators.Min(l => l.EndTime).ToLocalTime() : null;
                            AE.LocatorExpirationDate = LocDate.HasValue ? ((DateTime)LocDate).ToLocalTime().ToString() : null;
                            AE.LocatorExpirationDateWarning = LocDate.HasValue ? (LocDate < DateTime.Now.ToLocalTime()) : false;
                        }

                        int? afcount = await ReturnNumberAssetFiltersAsync(asset.Name, _amsClient);
                        AE.Filters = afcount > 0 ? afcount : null;

                        cacheAssetentriesV3[asset.Name] = AE; // let's put it in cache (or update the cache)
                    }
                }
                catch // in some case, we have a timeout on Assets.Where...
                {
                }
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
            RefreshGridView();
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


        public void PurgeCacheAssets(List<Asset> assets)
        {
            assets.ToList().ForEach(a => cacheAssetentriesV3.Remove(a.Name));
        }


        public void PurgeCacheAsset(Asset asset)
        {
            cacheAssetentriesV3.Remove(asset.Name);
        }


        private static readonly SemaphoreLocker _locker = new SemaphoreLocker();
        private int i = 0;

        public async Task ReLaunchAnalyzeOfAssetsAsync()
        {
            if (!_initialized)
            {
                return;
            }

            await _locker.LockAsync(async () =>
             {
                 Debug.Print("relaunch" + i++);
                 if (WorkerAnalyzeAssets.IsBusy)
                 {
                     // cancel the analyze.
                     WorkerAnalyzeAssets.CancelAsync();
                     Debug.Print("ask for cancel");
                 }

                 while (WorkerAnalyzeAssets.IsBusy)
                 {
                     await Task.Delay(2000);
                 }

                 if (!WorkerAnalyzeAssets.IsBusy)
                 {
                     // Start the asynchronous operation.
                     try
                     {
                         Debug.Print("run again !" + i);

                         WorkerAnalyzeAssets.RunWorkerAsync();
                     }
                     catch { }
                 }
             });
        }


        public async Task RefreshAssetsAsync(int pagetodisplay) // all assets are refreshed
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

            IPage<Asset> currentPage = null;
            await _amsClient.RefreshTokenIfNeededAsync();

            if (pagetodisplay == 1)
            {
                firstpage = await _amsClient.AMSclient.Assets.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, odataQuery);
                currentPage = firstpage;
            }
            else
            {
                currentPage = firstpage;
                _currentPageNumber = 1;
                while (currentPage.NextPageLink != null && pagetodisplay > _currentPageNumber)
                {
                    _currentPageNumber++;
                    currentPage = await _amsClient.AMSclient.Assets.ListNextAsync(currentPage.NextPageLink);
                }
                if (currentPage.NextPageLink == null)
                {
                    _currentPageNumberIsMax = true; // we reached max
                }
            }

            IEnumerable<AssetEntryV3> assets = currentPage.Select(a =>
            (cacheAssetentriesV3.ContainsKey(a.Name)
               && cacheAssetentriesV3[a.Name].LastModified != null
               && (cacheAssetentriesV3[a.Name].LastModified == a.LastModified.ToLocalTime().ToString("G")) ?
               cacheAssetentriesV3[a.Name] :
            new AssetEntryV3(_syncontext)
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

            Invoke(new Action(() => DataSource = _MyObservAssetV3));

            Debug.WriteLine("RefreshAssets End");
            await ReLaunchAnalyzeOfAssetsAsync();

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
        }



        /// <summary>
        /// Returns a bitmap for publication
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="_amsClient"></param>
        /// <param name="locators"></param>Optional. Allow to not lts again the locators to reduce the number of calls
        /// <returns></returns>
        public static async Task<AssetBitmapAndText> BuildBitmapPublicationAsync(string assetName, AMSClientV3 _amsClient, IList<AssetStreamingLocator> locators = null)
        {
            Bitmap returnedImage = null;
            string returnedText = null;
            await _amsClient.RefreshTokenIfNeededAsync();

            if (locators == null)
            {
                try
                {
                    locators = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, assetName))
                               .StreamingLocators;
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

        /// <summary>
        /// Returns a bitmap regarding the Dyn Encryption
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="amsClient"></param>
        /// <param name="locators"></param>If specified, avoid the function to list again them (optimization)
        /// <returns></returns>
        public static async Task<AssetBitmapAndText> BuildBitmapDynEncryptionAsync(string assetName, AMSClientV3 amsClient, IList<AssetStreamingLocator> locators = null)
        {
            await amsClient.RefreshTokenIfNeededAsync();
            if (locators == null)
            {
                try
                {
                    locators = (await amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, assetName))
                        .StreamingLocators;
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


        public static async Task<int?> ReturnNumberAssetFiltersAsync(string assetName, AMSClientV3 client)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            // asset filters
            List<AssetFilter> assetFilters = new List<AssetFilter>();
            try
            {
                IPage<AssetFilter> assetFiltersPage = await client.AMSclient.AssetFilters.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, assetName);
                while (assetFiltersPage != null)
                {
                    assetFilters.AddRange(assetFiltersPage);
                    if (assetFiltersPage.NextPageLink != null)
                    {
                        assetFiltersPage = await _amsClient.AMSclient.AssetFilters.ListNextAsync(assetFiltersPage.NextPageLink);
                    }
                    else
                    {
                        assetFiltersPage = null;
                    }
                }
            }
            catch
            {
                return null;
            }

            return assetFilters.Count();
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
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

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
