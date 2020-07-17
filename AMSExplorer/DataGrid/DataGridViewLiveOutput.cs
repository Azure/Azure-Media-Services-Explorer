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
    public class DataGridViewLiveOutput : DataGridView
    {
        private List<string> idsList = new List<string>();
        private readonly List<StatusInfo> ListStatus = new List<StatusInfo>();
        private static SortableBindingList<LiveOutputEntry> _MyObservLiveOutputs;

        private static int _itemssperpage = 50; //nb of items per page
        private static readonly int _pagecount = 1;
        private static int _currentpage = 1;
        private static bool _initialized = false;
        private static bool _refreshedatleastonetime = false;
        private static string _statefilter = "All";
        private static SearchObject _searchinname = new SearchObject { SearchType = SearchIn.LiveOutputName, Text = string.Empty };
        private static string _timefilter = FilterTime.LastWeek;
        private static TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private static BackgroundWorker WorkerRefreshChannels;
        public string _published = "Published";
        private static readonly Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        private static enumDisplayProgram _anyChannel = enumDisplayProgram.Selected;
        private AMSClientV3 _amsClient;

        public List<string> LiveEventSourceNames
        {
            get => idsList;
            set => idsList = value;
        }

        public int ItemsPerPage
        {
            get => _itemssperpage;
            set => _itemssperpage = value;
        }


        public int PageCount => _pagecount;
        public int CurrentPage => _currentpage;

        public enumDisplayProgram DisplayLiveEvent
        {
            get => _anyChannel;
            set => _anyChannel = value;
        }

        public string FilterState
        {
            get => _statefilter;
            set => _statefilter = value;

        }
        public SearchObject SearchInName
        {
            get => _searchinname;
            set => _searchinname = value;
        }
        public bool Initialized => _initialized;
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
        public int DisplayedCount => _MyObservLiveOutputs != null ? _MyObservLiveOutputs.Count() : 0;



        public void Init(AMSClientV3 client)
        {
            IEnumerable<LiveOutputEntry> programquery;
            client.RefreshTokenIfNeeded();

            _amsClient = client;
            _amsClient.RefreshTokenIfNeeded();

            List<LiveEvent> ListEvents = _amsClient.AMSclient.LiveEvents.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName).ToList();
            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = _amsClient.AMSclient.LiveOutputs.List(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, le.Name).ToList();
                plist.ForEach(p => LOList.Add(new Program.LiveOutputExt() { LiveOutputItem = p, LiveEventName = le.Name }));
            }

            programquery = from c in (LOList.Take(0))
                               //orderby c.LastModified descending
                           select new LiveOutputEntry
                           {
                               Name = c.LiveOutputItem.Name,
                               State = c.LiveOutputItem.ResourceState,
                               Description = c.LiveOutputItem.Description,
                               ArchiveWindowLength = c.LiveOutputItem.ArchiveWindowLength,
                               LastModified = c.LiveOutputItem.LastModified != null ? (DateTime?)((DateTime)c.LiveOutputItem.LastModified).ToLocalTime() : null,
                               Published = null,
                               LiveEventName = c.LiveEventName
                           };

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _published,
                DataPropertyName = _published,
            };
            Columns.Add(imageCol);


            SortableBindingList<LiveOutputEntry> MyObservProgramInPage = new SortableBindingList<LiveOutputEntry>(programquery.Take(0).ToList());
            DataSource = MyObservProgramInPage;
            //this.Columns["LiveEventName"].Visible = false;
            Columns[_published].DisplayIndex = ColumnCount - 3;
            Columns[_published].DefaultCellStyle.NullValue = null;
            Columns[_published].HeaderText = _published;
            Columns["LastModified"].Width = 130;
            Columns["LastModified"].HeaderText = "Last Modified";
            Columns["Description"].Width = 150;
            Columns["ArchiveWindowLength"].Width = 130;
            Columns["ArchiveWindowLength"].HeaderText = "Archive window";
            Columns["LiveEventName"].HeaderText = "Live event name";

            WorkerRefreshChannels = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            WorkerRefreshChannels.DoWork += new System.ComponentModel.DoWorkEventHandler(WorkerRefreshLiveOutputs_DoWork);

            _initialized = true;
        }


        public void DisplayPage(int page)
        {
            if (!_initialized)
            {
                return;
            }

            if (!_refreshedatleastonetime)
            {
                return;
            }

            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                DataSource = new BindingList<LiveOutputEntry>(_MyObservLiveOutputs.Skip(_itemssperpage * (page - 1)).Take(_itemssperpage).ToList());
            }
        }

        public async Task RefreshLiveOutputAsync(string liveeventName, LiveOutput liveOutput)
        {
            int index = -1;

            if (_MyObservLiveOutputs != null)
            {
                foreach (LiveOutputEntry CE in _MyObservLiveOutputs) // let's search for index
                {
                    if (CE.Name == liveOutput.Name)
                    {
                        index = _MyObservLiveOutputs.IndexOf(CE);
                        break;
                    }
                }
            }

            if (index >= 0) // we found it
            { // we update the observation collection
                await _amsClient.RefreshTokenIfNeededAsync();
                liveOutput = await _amsClient.AMSclient.LiveOutputs.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, liveeventName, liveOutput.Name); //refresh
                if (liveOutput != null)
                {
                    try // sometimes, index could be wrong id program has been deleted
                    {
                        _MyObservLiveOutputs[index].State = liveOutput.ResourceState;
                        _MyObservLiveOutputs[index].Description = liveOutput.Description;
                        _MyObservLiveOutputs[index].ArchiveWindowLength = liveOutput.ArchiveWindowLength;
                        _MyObservLiveOutputs[index].LastModified = liveOutput.LastModified != null ? (DateTime?)((DateTime)liveOutput.LastModified).ToLocalTime() : null;
                        RefreshGridView();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void WorkerRefreshLiveOutputs_DoWork(object sender, DoWorkEventArgs e) // all assets are refreshed
        {
            Task.Run(() => WorkerRefreshLiveOutputs_DoWorkAsync(sender, e)).ConfigureAwait(false);
        }

        private async Task WorkerRefreshLiveOutputs_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("WorkerRefreshLiveOutputs_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            LiveOutput liveOutputItem;
            await _amsClient.RefreshTokenIfNeededAsync();

            foreach (LiveOutputEntry CE in _MyObservLiveOutputs)
            {

                liveOutputItem = null;
                try
                {
                    liveOutputItem = await _amsClient.AMSclient.LiveOutputs.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, CE.LiveEventName, CE.Name);
                    if (liveOutputItem != null)
                    {
                        CE.State = liveOutputItem.ResourceState;
                        RefreshGridView();
                        // BeginInvoke(new Action(() => Refresh()), null);
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
            RefreshGridView();
            // BeginInvoke(new Action(() => Refresh()), null);
        }

        private void RefreshPrograms() // all assets are refreshed
        {
            Task.Run(async () => await RefreshLiveOutputsAsync(_currentpage));

        }

        public async Task RefreshLiveOutputsAsync(int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized)
            {
                return;
            }

            if (idsList.Count == 0)
            {
                return;
            }

            Debug.WriteLine("RefreshPrograms : start");

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.WaitCursor));

            await _amsClient.RefreshTokenIfNeededAsync();

            IEnumerable<LiveEvent> ListEvents;
            if (_anyChannel == enumDisplayProgram.None)
            {
                ListEvents = new List<LiveEvent>();
            }
            else
            {
                ListEvents = (await _amsClient.AMSclient.LiveEvents.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName))
                    .ToList()
                    .Where(l => _anyChannel == enumDisplayProgram.Any || (_anyChannel == enumDisplayProgram.Selected && LiveEventSourceNames.Contains(l.Name)));
            }


            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = (await _amsClient.AMSclient.LiveOutputs.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, le.Name))
                    .ToList();
                plist.ForEach(p => LOList.Add(new Program.LiveOutputExt() { LiveOutputItem = p, LiveEventName = le.Name }));
            }

            float scale = DeviceDpi / 96f;
            /*
            IEnumerable<LiveOutputEntry> programquery = from c in (LOList)
                                                            //orderby c.LastModified descending
                                                        select new LiveOutputEntry
                                                        {
                                                            Name = c.LiveOutputItem.Name,
                                                            State = c.LiveOutputItem.ResourceState,
                                                            Description = c.LiveOutputItem.Description,
                                                            ArchiveWindowLength = c.LiveOutputItem.ArchiveWindowLength,
                                                            LastModified = c.LiveOutputItem.LastModified != null ? (DateTime?)((DateTime)c.LiveOutputItem.LastModified).ToLocalTime() : null,
                                                            Published = (Bitmap)HighDpiHelper.ScaleImage((await DataGridViewAssets.BuildBitmapPublicationAsync(c.LiveOutputItem.AssetName, _client)).bitmap, scale),
                                                            LiveEventName = c.LiveEventName
                                                        };
                                                        */

            var tasksBuildBitmaps = LOList.Select(
                                        async item => new
                                        {
                                            LOExt = item,
                                            LOBitmap = await DataGridViewAssets.BuildBitmapPublicationAsync(item.LiveOutputItem.AssetName, _amsClient)
                                        });

            IEnumerable<LiveOutputEntry> programquery = (await Task.WhenAll(tasksBuildBitmaps))
                                            .Select(c =>
                                                          new LiveOutputEntry
                                                          {
                                                              Name = c.LOExt.LiveOutputItem.Name,
                                                              State = c.LOExt.LiveOutputItem.ResourceState,
                                                              Description = c.LOExt.LiveOutputItem.Description,
                                                              ArchiveWindowLength = c.LOExt.LiveOutputItem.ArchiveWindowLength,
                                                              LastModified = c.LOExt.LiveOutputItem.LastModified != null ? (DateTime?)((DateTime)c.LOExt.LiveOutputItem.LastModified).ToLocalTime() : null,
                                                              Published = (Bitmap)HighDpiHelper.ScaleImage(c.LOBitmap.bitmap, scale),
                                                              LiveEventName = c.LOExt.LiveEventName
                                                          });

            _MyObservLiveOutputs = new SortableBindingList<LiveOutputEntry>(programquery.ToList());
            BeginInvoke(new Action(() => DataSource = _MyObservLiveOutputs));
            _refreshedatleastonetime = true;

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));

            Debug.WriteLine("RefreshPrograms : end");
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
}
