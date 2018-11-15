//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
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

        static BindingList<AssetEntry> _MyObservAsset;
        public IEnumerable<IAsset> assets;
        static Dictionary<string, AssetEntry> cacheAssetentries = new Dictionary<string, AssetEntry>();
        static Dictionary<string, AssetEntryV3> cacheAssetentriesV3 = new Dictionary<string, AssetEntryV3>();

        static private int _assetsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static private bool _neveranalyzed = true;
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.AssetName, Text = "" };
        static private string _statefilter = "";
        static private string _timefilter = FilterTime.First1000Items;
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

        public int AssetsPerPage
        {
            get
            {
                return _assetsperpage;
            }
            set
            {
                _assetsperpage = value;
            }
        }
        public int PageCount
        {
            get
            {
                return _pagecount;
            }
        }
        public int CurrentPage
        {
            get
            {
                return _currentpage;
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

            //IEnumerable<AssetEntry> assetquery;
            _client = client;

            var assets = _client.AMSclient.Assets.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName).Select(a => new AssetEntryV3
            {
                Name = a.Name,
                AssetId = a.AssetId,
                Type = a.Type,
                AlternateId = a.AlternateId,
                LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G"),
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
            this.Columns["LastModified"].HeaderText = "Last modified";
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
            this.Columns["LastModified"].Width = 140;
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

            PublishStatus SASLoc;
            PublishStatus OrigLoc;

            var listae = _MyObservAssetV3.OrderBy(a => cacheAssetentriesV3.ContainsKey(a.Name)).ToList(); // as priority, assets not yet analyzed

            foreach (AssetEntryV3 AE in listae)
            {
                try
                {
                    //asset = _client.Assets.List(_resourceName,_accountName).Where(a => a.Id == AE.Id).FirstOrDefault();

                    //var odataQuery = new ODataQuery<Asset>("properties/Id eq "+AE.Id);
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
                        //AE.AssetId = asset.AssetId;
                        //AE.Created = asset.Created;
                        AE.Description = asset.Description;
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

        public void PurgeCacheAssets(List<IAsset> assets)
        {
            assets.ToList().ForEach(a => cacheAssetentries.Remove(a.Id));
        }

        public void PurgeCacheAssetsV3(List<Asset> assets)
        {
            assets.ToList().ForEach(a => cacheAssetentries.Remove(a.Name));
        }

        public void PurgeCacheAsset(IAsset asset)
        {
            cacheAssetentriesV3.Remove(asset.Name);
        }

        public void PurgeCacheAsset(Asset asset)
        {
            cacheAssetentriesV3.Remove(asset.Name);
        }



        public void RefreshAssets(int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;
            Debug.WriteLine("RefreshAssets Start");

            if (WorkerAnalyzeAssets.IsBusy)
            {
                // cancel the analyze.
                WorkerAnalyzeAssets.CancelAsync();
            }
            this.FindForm().Cursor = Cursors.WaitCursor;

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
                    odataQuery.OrderBy = "Properties/Created"; break;

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
                    // Search on Asset name
                    case SearchIn.AssetName:
                        odataQuery.Filter = "name eq "+ search;
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

            if (pagetodisplay == 0)
            {
                firstpage = _client.AMSclient.Assets.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, odataQuery);
                currentPage = firstpage;
            }
            else
            {
                currentPage = firstpage;
                int i = 0;
                while (currentPage.NextPageLink != null && pagetodisplay > i)
                {
                    i++;
                    currentPage = _client.AMSclient.Assets.ListNext(currentPage.NextPageLink);
                }
            }

            var assets = currentPage.Select(a => new AssetEntryV3
            {
                Name = a.Name,
                Description = a.Description,
                AssetId = a.AssetId,
                AlternateId = a.AlternateId,
                //Type = a.Type,
                LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G"),
                StorageAccountName = a.StorageAccountName
            }
            );

            _MyObservAssetV3 = new BindingList<AssetEntryV3>(assets.ToList());


            this.BeginInvoke(new Action(() => this.DataSource = _MyObservAssetV3));
            _refreshedatleastonetime = true;

            Debug.WriteLine("RefreshAssets End");
            AnalyzeItemsInBackground();

            this.FindForm().Cursor = Cursors.Default;

            return;






            //// DAYS
            //bool filterStartDate = false;
            //bool filterEndDate = false;

            //DateTime dateTimeStart = DateTime.UtcNow;
            //DateTime dateTimeRangeEnd = DateTime.UtcNow.AddDays(1);

            //int days = FilterTime.ReturnNumberOfDays(_timefilter);
            //if (days > 0)
            //{
            //    filterStartDate = true;
            //    dateTimeStart = (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)));
            //}
            //else if (days == -1) // TimeRange
            //{
            //    filterStartDate = true;
            //    filterEndDate = true;
            //    dateTimeStart = _timefilterTimeRange.StartDate;
            //    if (_timefilterTimeRange.EndDate != null) // there is an end time
            //    {
            //        dateTimeRangeEnd = (DateTime)_timefilterTimeRange.EndDate;
            //    }
            //}

            //IQueryable<IAsset> assetsServerQuery = null;// = context.Assets.AsQueryable(); ;
            //bool SwitchedToLocalQuery = false;

            /////////////////////////
            //// SEARCH
            /////////////////////////
            //if (_searchinname != null && !string.IsNullOrEmpty(_searchinname.Text))
            //{
            //    bool Error = false;
            //    int skipSize = 0;
            //    int batchSize = 1000;
            //    int currentSkipSize = 0;
            //    string strsearch = _searchinname.Text.ToLower();

            //    switch (_searchinname.SearchType)
            //    {
            //        // Search on Asset name
            //        case SearchIn.AssetName:

            //            assetsServerQuery = context.Assets.Where(a =>
            //                    (a.Name.Contains(_searchinname.Text))
            //                    &&
            //                    (!filterStartDate || a.LastModified > dateTimeStart)
            //                    &&
            //                    (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                    );

            //        break;

            //        // Search on Asset aternate id
            //        case SearchIn.AssetAltId:
            //            assetsServerQuery = context.Assets.Where(a =>
            //                      (a.AlternateId.Contains(_searchinname.Text))
            //                      &&
            //                      (!filterStartDate || a.LastModified > dateTimeStart)
            //                      &&
            //                      (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                      );
            //            break;

            //        // Search on Asset ID
            //        case SearchIn.AssetId:
            //            string assetguid = _searchinname.Text;
            //            if (assetguid.StartsWith(Constants.AssetIdPrefix))
            //            {
            //                assetguid = assetguid.Substring(Constants.AssetIdPrefix.Length);
            //            }
            //            try
            //            {
            //                var g = new Guid(assetguid);
            //            }
            //            catch
            //            {
            //                Error = true;
            //                MessageBox.Show("Error with asset Id. Is it a valid GUID or asset Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            if (!Error)
            //            {
            //                assetsServerQuery = context.Assets.Where(a =>
            //                                                 (a.Id == Constants.AssetIdPrefix + assetguid)
            //                                                 &&
            //                                                 (!filterStartDate || a.LastModified > dateTimeStart)
            //                                                 &&
            //                                                 (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                                                 );
            //            }
            //            break;


            //        // Search on Asset file name
            //        case SearchIn.AssetFileName:
            //            IList<string> assetFileListID = new List<string>();

            //            while (true)
            //            {
            //                // Enumerate through all asset files (1000 at a time)
            //                var filesq = context.Files
            //                    .Skip(skipSize).Take(batchSize).ToList();

            //                currentSkipSize += filesq.Count;
            //                var filesq2 = filesq.Where(f => f.Name.ToLower().Contains(strsearch)).Select(f => f.ParentAssetId);

            //                foreach (var a in filesq2)
            //                {
            //                    assetFileListID.Add(a);
            //                }

            //                if (currentSkipSize == batchSize)
            //                {
            //                    skipSize += batchSize;
            //                    currentSkipSize = 0;
            //                }
            //                else
            //                {
            //                    break;
            //                }
            //            }

            //            var assetlist = new List<IAsset>();
            //            foreach (var a in assetFileListID.Distinct())
            //            {
            //                assetlist.Add(AssetInfo.GetAsset(a, context));
            //            }

            //            SwitchedToLocalQuery = true;
            //            assets = assetlist.Where(a =>
            //                    (!filterStartDate || a.LastModified > dateTimeStart)
            //                    &&
            //                    (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                    );


            //            break;

            //        // Search on Asset file ID
            //        case SearchIn.AssetFileId:
            //            string fileguid = _searchinname.Text;
            //            if (fileguid.StartsWith(Constants.AssetFileIdPrefix))
            //            {
            //                fileguid = fileguid.Substring(Constants.AssetFileIdPrefix.Length);
            //            }
            //            try
            //            {
            //                var g = new Guid(fileguid);
            //            }
            //            catch
            //            {
            //                Error = true;
            //                MessageBox.Show("Error with file asset Id. Is it a valid GUID or asset Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            if (!Error)
            //            {
            //                var myfile = context.Files.Where(f => f.Id == Constants.AssetFileIdPrefix + fileguid).FirstOrDefault();
            //                if (myfile != null)
            //                {
            //                    assetsServerQuery = context.Assets.Where(a =>
            //                                                       (!filterStartDate || a.LastModified > dateTimeStart)
            //                                                       &&
            //                                                       (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                                                       &&
            //                                                       myfile.Asset.Id == a.Id
            //                                                       );
            //                }
            //                else
            //                {
            //                    MessageBox.Show("No file was found with this Id.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                }
            //            }
            //            break;

            //        // Search on Locator id / guid
            //        case SearchIn.LocatorId:
            //            string locatorguid = _searchinname.Text;
            //            if (locatorguid.StartsWith(Constants.LocatorIdPrefix))
            //            {
            //                locatorguid = locatorguid.Substring(Constants.LocatorIdPrefix.Length);
            //            }
            //            try
            //            {
            //                var g = new Guid(locatorguid);
            //            }
            //            catch
            //            {
            //                Error = true;
            //                MessageBox.Show("Error with locator Id. Is it a valid GUID or locator Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            if (!Error)
            //            {
            //                var myloc = context.Locators.Where(l => l.Id == Constants.LocatorIdPrefix + locatorguid).FirstOrDefault();
            //                if (myloc != null)
            //                {
            //                    assetsServerQuery = context.Assets.Where(a =>
            //                                                        (!filterStartDate || a.LastModified > dateTimeStart)
            //                                                        &&
            //                                                        (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                                                        &&
            //                                                        a.Id == myloc.AssetId
            //                                                        );
            //                }
            //                else
            //                {
            //                    MessageBox.Show("No locator was found using this locator Id.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                }
            //            }
            //            break;

            //        // Search on Program id / guid
            //        case SearchIn.ProgramId:
            //            string programguid = _searchinname.Text;
            //            if (programguid.StartsWith(Constants.ProgramIdPrefix))
            //            {
            //                programguid = programguid.Substring(Constants.ProgramIdPrefix.Length);
            //            }
            //            try
            //            {
            //                var g = new Guid(programguid);
            //            }
            //            catch
            //            {
            //                Error = true;
            //                MessageBox.Show("Error with program Id. Is it a valid GUID or program Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            if (!Error)
            //            {
            //                var queryprog = context.Programs.Where(p => p.Id == Constants.ProgramIdPrefix + programguid).FirstOrDefault();
            //                if (queryprog != null)
            //                {
            //                    assetsServerQuery = context.Assets.Where(a =>
            //                                                       (!filterStartDate || a.LastModified > dateTimeStart)
            //                                                       &&
            //                                                       (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                                                       &&
            //                                                       queryprog.AssetId == a.Id
            //                                                       );
            //                }
            //                else
            //                {
            //                    MessageBox.Show("No program was found with this Id.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                }
            //            }
            //            break;

            //        // Search on Program name
            //        case SearchIn.ProgramName:

            //            IList<string> assetFileListIDP = new List<string>();

            //            while (true)
            //            {
            //                // Enumerate through all programs (1000 at a time)
            //                var programsq = context.Programs
            //                    .Skip(skipSize).Take(batchSize).ToList();

            //                currentSkipSize += programsq.Count;
            //                var programsq2 = programsq.Where(p => p.Name.ToLower().Contains(strsearch)).Select(p => p.AssetId);

            //                foreach (var a in programsq2)
            //                {
            //                    assetFileListIDP.Add(a);
            //                }

            //                if (currentSkipSize == batchSize)
            //                {
            //                    skipSize += batchSize;
            //                    currentSkipSize = 0;
            //                }
            //                else
            //                {
            //                    break;
            //                }
            //            }

            //            var assetlist2 = new List<IAsset>();
            //            foreach (var a in assetFileListIDP)
            //            {
            //                assetlist2.Add(AssetInfo.GetAsset(a, context));
            //            }

            //            SwitchedToLocalQuery = true;
            //            assets = assetlist2.Where(a =>
            //                    (!filterStartDate || a.LastModified > dateTimeStart)
            //                    &&
            //                    (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                    );

            //            break;


            //        default:
            //            break;
            //    }
            //}
            //else // NO SEARCH
            //{
            //    assetsServerQuery = context.Assets.Where(a =>
            //                    (!filterStartDate || a.LastModified > dateTimeStart)
            //                    &&
            //                    (!filterEndDate || a.LastModified < dateTimeRangeEnd)
            //                    );
            //}

            //if (!SwitchedToLocalQuery && assetsServerQuery == null) // teh current query did not find asset (locator id search for example)
            //{
            //    assets = _client.Assets.AsEnumerable().Take(0);
            //    SwitchedToLocalQuery = true;
            //}


            //// SHORTCUT (needed for account with large number fo assets)
            //if (!SwitchedToLocalQuery && (_statefilter == StatusAssets.All || _statefilter == "") && _orderassets == OrderAssets.LastModifiedDescending)
            //{
            //    if (_timefilter == FilterTime.First50Items)
            //    {
            //        assets = assetsServerQuery.Take(50);
            //    }
            //    else if (_timefilter == FilterTime.First1000Items)
            //    {
            //        assets = assetsServerQuery.Take(1000);
            //    }
            //    else
            //    {
            //        assets = assetsServerQuery;
            //    }
            //}
            //else // general case

            //{

            //    ///////////////////////
            //    // STATE FILTER
            //    ///////////////////////
            //    // we need to do paging
            //    // not excuted for large account as state filter control is disabled

            //    IList<IAsset> aggregateListAssets = new List<IAsset>();
            //    int skipSize2 = 0;
            //    int batchSize2 = 1000;
            //    int currentSkipSize2 = 0;

            //    while (true)
            //    {
            //        // Enumerate through all assets (1000 at a time)
            //        IEnumerable<IAsset> listPagedAssets;
            //        IList<IAsset> fassets = new List<IAsset>();

            //        if (SwitchedToLocalQuery)
            //        {
            //            listPagedAssets = assets.Skip(skipSize2).Take(batchSize2).ToList();
            //        }
            //        else
            //        {
            //            listPagedAssets = assetsServerQuery.Skip(skipSize2).Take(batchSize2).ToList();
            //        }
            //        currentSkipSize2 += listPagedAssets.Count();

            //        switch (_statefilter)
            //        {
            //            case StatusAssets.Published:
            //            case StatusAssets.PublishedExpired:

            //                bool bexpired = _statefilter == StatusAssets.PublishedExpired;
            //                IList<IAsset> newListAssets1 = new List<IAsset>();

            //                int skipSizeLoc = 0;
            //                int batchSizeLoc = 1000;
            //                int currentSkipSizeLoc = 0;

            //                while (true)
            //                {
            //                    // Enumerate through all locators (1000 at a time)
            //                    var listlocators = context.Locators.Where(l => !bexpired || l.ExpirationDateTime < DateTime.UtcNow).Skip(skipSizeLoc).Take(batchSizeLoc).ToList().Select(l => l.AssetId).ToList();
            //                    currentSkipSizeLoc += listlocators.Count;

            //                    var assetexpired = listPagedAssets.Where(a => listlocators.Contains(a.Id));

            //                    foreach (var a in assetexpired)
            //                    {
            //                        newListAssets1.Add(a);
            //                    }

            //                    if (currentSkipSizeLoc == batchSizeLoc)
            //                    {
            //                        skipSizeLoc += batchSizeLoc;
            //                        currentSkipSizeLoc = 0;
            //                    }
            //                    else
            //                    {
            //                        break;
            //                    }
            //                }

            //                foreach (var a in newListAssets1)
            //                {
            //                    fassets.Add(a);
            //                }
            //                break;


            //            case StatusAssets.DynEnc:
            //                var assetwithDelPol = listPagedAssets.Where(a => a.DeliveryPolicies.Any());
            //                foreach (var a in assetwithDelPol)
            //                {
            //                    fassets.Add(a);
            //                }
            //                break;

            //            case StatusAssets.Empty:

            //                IList<IAsset> newListAssets2 = listPagedAssets.ToList();

            //                int skipSizeEmpty = 0;
            //                int batchSizeEmpty = 1000;
            //                int currentSkipSizeEmpty = 0;

            //                while (true)
            //                {
            //                    // Enumerate through all files (1000 at a time)
            //                    var listfiles = context.Files.Where(f => f.ContentFileSize > 0).Skip(skipSizeEmpty).Take(batchSizeEmpty).ToList().Select(f => f.ParentAssetId).ToList();
            //                    currentSkipSizeEmpty += listfiles.Count;

            //                    var assetsnotempty = listPagedAssets.Where(a => listfiles.Contains(a.Id)).ToList(); ;

            //                    foreach (var a in assetsnotempty)
            //                    {
            //                        newListAssets2.Remove(a); // if file with size >0, then we remove it parenrt id from the lis
            //                    }

            //                    if (currentSkipSizeEmpty == batchSizeEmpty)
            //                    {
            //                        skipSizeEmpty += batchSizeEmpty;
            //                        currentSkipSizeEmpty = 0;
            //                    }
            //                    else
            //                    {
            //                        break;
            //                    }
            //                }

            //                foreach (var a in newListAssets2)
            //                {
            //                    fassets.Add(a);
            //                }
            //                break;


            //            case StatusAssets.All: // we need this to parse all assets
            //            default:
            //                foreach (var a in listPagedAssets)
            //                {
            //                    fassets.Add(a);
            //                }
            //                break;
            //                /*

            //                // below is REMOVED  as queries are executed by the back-end in order to process all assets and be quick. Th equery below needs to be
            //                // executed locally and would be slow. Could be reintroduce if customer demand.

            //            case StatusAssets.NotPublished:
            //                fassets = listassets.Where(a => a.Locators.Count == 0);
            //                break;
            //            case StatusAssets.Storage:
            //                fassets = listassets.Where(a => a.Options == AssetCreationOptions.StorageEncrypted);
            //                break;
            //            case StatusAssets.CENC:
            //                fassets = listassets.Where(a => a.Options == AssetCreationOptions.CommonEncryptionProtected);
            //                break;
            //            case StatusAssets.Envelope:
            //                fassets = listassets.Where(a => a.Options == AssetCreationOptions.EnvelopeEncryptionProtected);
            //                break;
            //            case StatusAssets.NotEncrypted:
            //                fassets = listassets.Where(a => a.Options == AssetCreationOptions.None);
            //                break;
            //            case StatusAssets.DynEnc:
            //                fassets = listassets.Where(a => a.DeliveryPolicies.Any());
            //                break;
            //            case StatusAssets.Streamable:
            //                fassets = listassets.Where(a => a.IsStreamable);
            //                break;
            //            case StatusAssets.SupportDynEnc:
            //                fassets = listassets.Where(a => a.SupportsDynamicEncryption);
            //                break;
            //            case StatusAssets.Empty:
            //                fassets = listassets.Where(a => a.AssetFiles.Count() == 0);
            //                break;
            //            case StatusAssets.DefaultStorage:
            //                fassets = listassets.Where(a => a.StorageAccountName == _context.DefaultStorageAccount.Name);
            //                break;
            //            case StatusAssets.NotDefaultStorage:
            //                fassets = listassets.Where(a => a.StorageAccountName != _context.DefaultStorageAccount.Name);
            //                break;
            //                */
            //        }

            //        foreach (var a in fassets)
            //        {
            //            aggregateListAssets.Add(a);
            //        }

            //        if (currentSkipSize2 == batchSize2)
            //        {
            //            skipSize2 += batchSize2;
            //            currentSkipSize2 = 0;
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //    SwitchedToLocalQuery = true;
            //    assets = aggregateListAssets;

            //    ///////////////////////
            //    // SORTING
            //    ///////////////////////
            //    // let's sort the aggregate results
            //    var size = new Func<IAsset, long>(AssetInfo.GetSize);

            //    // client side only ! (a take is done otherwise)

            //    switch (_orderassets)
            //    {
            //        case OrderAssets.LastModifiedDescending:
            //            assets = from a in assets orderby a.LastModified descending select a;
            //            break;

            //        case OrderAssets.LastModifiedAscending:
            //            assets = from a in assets orderby a.LastModified ascending select a;
            //            break;

            //        case OrderAssets.NameAscending:
            //            assets = from a in assets orderby a.Name ascending select a;
            //            break;

            //        case OrderAssets.NameDescending:
            //            assets = from a in assets orderby a.Name descending select a;
            //            break;

            //        case OrderAssets.SizeDescending:
            //            assets = from a in assets orderby size(a) descending select a;
            //            break;

            //        case OrderAssets.SizeAscending:
            //            assets = from a in assets orderby size(a) ascending select a;
            //            break;

            //        case OrderAssets.LocatorExpirationAscending:
            //            assets = from a in assets where a.Locators.Any() orderby a.Locators.Min(l => l.ExpirationDateTime) ascending select a;
            //            break;

            //        case OrderAssets.LocatorExpirationDescending:
            //            assets = from a in assets where a.Locators.Any() orderby a.Locators.Min(l => l.ExpirationDateTime) descending select a;
            //            break;

            //        default:
            //            assets = from a in assets orderby a.LastModified descending select a;
            //            break;
            //    }

            //    if (_timefilter == FilterTime.First50Items)
            //    {
            //        assets = assets.Take(50);
            //    }
            //    else // if (_timefilter == FilterTime.First1000Items)
            //    {
            //        assets = assets.Take(1000);
            //    }

            //}// end of general case

            //_client = context;
            //_pagecount = (int)Math.Ceiling(((double)assets.Count()) / ((double)_assetsperpage));
            //if (_pagecount == 0) _pagecount = 1; // no asset but one page

            //if (pagetodisplay < 1) pagetodisplay = 1;
            //if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            //_currentpage = pagetodisplay;

            //try
            //{
            //    IEnumerable<AssetEntry> assetquery = assets.AsEnumerable().Select(a =>
            //   // let's return the data cached in memory of it exists and last modified time is the same
            //   (cacheAssetentries.ContainsKey(a.Id)
            //   && cacheAssetentries[a.Id].LastModified != null
            //   && (cacheAssetentries[a.Id].LastModified == a.LastModified.ToLocalTime().ToString("G")) ?
            //   cacheAssetentries[a.Id] :
            //                 new AssetEntry
            //                 {
            //                     Name = a.Name,
            //                     Id = a.Id,
            //                     AlternateId = a.AlternateId,
            //                     Type = null,
            //                     LastModified = a.LastModified.ToLocalTime().ToString("G"),
            //                     Storage = a.StorageAccountName
            //                 }
            //                  ));

            //    _MyObservAsset = new BindingList<AssetEntry>(assetquery.ToList());
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + Constants.endline + Program.GetErrorMessage(e));
            //    Environment.Exit(0);
            //}

            //BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(_MyObservAsset.Skip(_assetsperpage * (_currentpage - 1)).Take(_assetsperpage).ToList());
            //this.BeginInvoke(new Action(() => this.DataSource = MyObservAssethisPage));
            //_refreshedatleastonetime = true;

            //Debug.WriteLine("RefreshAssets End");
            //AnalyzeItemsInBackground();

            //this.FindForm().Cursor = Cursors.Default;
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




        private AssetBitmapAndText ReturnStaticProtectedBitmap(IAsset asset)
        {
            AssetBitmapAndText ABT = new AssetBitmapAndText();

            switch (asset.Options)
            {
                case AssetCreationOptions.StorageEncrypted:
                    ABT.bitmap = storageencryptedimage;
                    ABT.MouseOverDesc = "Storage encrypted";
                    break;

                case AssetCreationOptions.CommonEncryptionProtected:
                    ABT.bitmap = CENCencryptedimage;
                    ABT.MouseOverDesc = "CENC encrypted";
                    break;

                case AssetCreationOptions.EnvelopeEncryptionProtected:
                    ABT.bitmap = envelopeencryptedimage;
                    ABT.MouseOverDesc = "Envelope encrypted";
                    break;

                default:
                    break;
            }
            return ABT;
        }


        private static AssetBitmapAndText BuildBitmapAssetFilters(IAsset asset)
        {
            if (asset == null || asset.AssetFilters == null) return new AssetBitmapAndText();

            var filcount = asset.AssetFilters.Count();

            if (filcount == 0)
            {
                return new AssetBitmapAndText();
            }
            else if (filcount == 1)
            {
                return new AssetBitmapAndText()
                {
                    bitmap = AssetFilterImage,
                    MouseOverDesc = "1 filter"
                }; ;
            }
            else // >1
            {
                return new AssetBitmapAndText()
                {
                    bitmap = AssetFiltersImage,
                    MouseOverDesc = string.Format("{0} filters", filcount)
                };
            }


        }


        private static AssetBitmapAndText BuildBitmapDynEncryption(IAsset asset)
        {
            AssetBitmapAndText ABT = new AssetBitmapAndText();
            AssetEncryptionState assetEncryptionState = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.HLS | AssetDeliveryProtocol.Dash);

            switch (assetEncryptionState)
            {
                case AssetEncryptionState.DynamicCommonEncryption:
                    ABT.bitmap = CENCencryptedimage;
                    ABT.MouseOverDesc = "Dynamic Common Encryption (CENC)";
                    break;

                case AssetEncryptionState.DynamicCommonEncryptionCbcs:
                    ABT.bitmap = CENCcbcsEncryptedImage;
                    ABT.MouseOverDesc = "Dynamic Common Encryption (Cbcs)";
                    break;

                case AssetEncryptionState.DynamicEnvelopeEncryption:
                    ABT.bitmap = envelopeencryptedimage;
                    ABT.MouseOverDesc = "Dynamic Envelope Encryption (AES)";
                    break;

                case AssetEncryptionState.NoDynamicEncryption:
                    ABT.bitmap = storagedecryptedimage;
                    ABT.MouseOverDesc = "No Dynamic Encryption";
                    break;

                case AssetEncryptionState.NoSinglePolicyApplies:
                    AssetEncryptionState assetEncryptionStateHLS = asset.GetEncryptionState(AssetDeliveryProtocol.HLS);
                    AssetEncryptionState assetEncryptionStateSmooth = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming);
                    AssetEncryptionState assetEncryptionStateDash = asset.GetEncryptionState(AssetDeliveryProtocol.Dash);
                    bool CENCEnable = (assetEncryptionStateHLS == AssetEncryptionState.DynamicCommonEncryption || assetEncryptionStateSmooth == AssetEncryptionState.DynamicCommonEncryption || assetEncryptionStateDash == AssetEncryptionState.DynamicCommonEncryption);
                    bool CENCCbcsEnable = (assetEncryptionStateHLS == AssetEncryptionState.DynamicCommonEncryptionCbcs);
                    bool EnvelopeEnable = (assetEncryptionStateHLS == AssetEncryptionState.DynamicEnvelopeEncryption || assetEncryptionStateSmooth == AssetEncryptionState.DynamicEnvelopeEncryption || assetEncryptionStateDash == AssetEncryptionState.DynamicEnvelopeEncryption);
                    int count = (CENCEnable ? 1 : 0) + (CENCCbcsEnable ? 1 : 0) + (EnvelopeEnable ? 1 : 0);
                    ABT.bitmap = new Bitmap((envelopeencryptedimage.Width * count), envelopeencryptedimage.Height);
                    int x = 0;

                    using (Graphics graphicsObject = Graphics.FromImage(ABT.bitmap))
                    {
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

                    /*
                    if (CENCEnable && EnvelopeEnable)
                    {
                        ABT.bitmap = new Bitmap((envelopeencryptedimage.Width + CENCencryptedimage.Width), envelopeencryptedimage.Height);
                        using (Graphics graphicsObject = Graphics.FromImage(ABT.bitmap))
                        {
                            graphicsObject.DrawImage(envelopeencryptedimage, new Point(0, 0));
                            graphicsObject.DrawImage(CENCencryptedimage, new Point(envelopeencryptedimage.Width, 0));
                        }
                    }
                    else
                    {
                        ABT.bitmap = CENCEnable ? CENCencryptedimage : envelopeencryptedimage;
                    }
                    */
                    ABT.MouseOverDesc = "Multiple policies";
                    break;

                default:
                    break;
            }
            return ABT;
        }
    }

}
