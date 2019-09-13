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
        private AMSClientV3 _client;

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

            _client = client;
            _client.RefreshTokenIfNeeded();

            List<LiveEvent> ListEvents = _client.AMSclient.LiveEvents.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName).ToList();
            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = _client.AMSclient.LiveOutputs.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, le.Name).ToList();
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
            WorkerRefreshChannels.DoWork += new System.ComponentModel.DoWorkEventHandler(WorkerRefreshChannels_DoWork);

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

        public void RefreshProgram(string liveeventName, LiveOutput program)
        {
            int index = -1;

            if (_MyObservLiveOutputs != null)
            {
                foreach (LiveOutputEntry CE in _MyObservLiveOutputs) // let's search for index
                {
                    if (CE.Name == program.Name)
                    {
                        index = _MyObservLiveOutputs.IndexOf(CE);
                        break;
                    }
                }
            }

            if (index >= 0) // we found it
            { // we update the observation collection
                _client.RefreshTokenIfNeeded();

                program = _client.AMSclient.LiveOutputs.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, liveeventName, program.Name); //refresh
                if (program != null)
                {
                    try // sometimes, index could be wrong id program has been deleted
                    {
                        _MyObservLiveOutputs[index].State = program.ResourceState;
                        _MyObservLiveOutputs[index].Description = program.Description;
                        _MyObservLiveOutputs[index].ArchiveWindowLength = program.ArchiveWindowLength;
                        _MyObservLiveOutputs[index].LastModified = program.LastModified != null ? (DateTime?)((DateTime)program.LastModified).ToLocalTime() : null;
                        Refresh();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void WorkerRefreshChannels_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("WorkerRefreshChannels_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            LiveOutput liveOutputItem;
            _client.RefreshTokenIfNeeded();

            foreach (LiveOutputEntry CE in _MyObservLiveOutputs)
            {

                liveOutputItem = null;
                try
                {
                    liveOutputItem = _client.AMSclient.LiveOutputs.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, CE.LiveEventName, CE.Name);
                    if (liveOutputItem != null)
                    {
                        CE.State = liveOutputItem.ResourceState;
                        BeginInvoke(new Action(() => Refresh()), null);
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

            await _client.RefreshTokenIfNeededAsync();

            IEnumerable<LiveEvent> ListEvents;
            if (_anyChannel == enumDisplayProgram.None)
            {
                ListEvents = new List<LiveEvent>();
            }
            else
            {
                ListEvents = (await _client.AMSclient.LiveEvents.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName))
                    .ToList()
                    .Where(l => _anyChannel == enumDisplayProgram.Any || (_anyChannel == enumDisplayProgram.Selected && LiveEventSourceNames.Contains(l.Name)));
            }


            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = (await _client.AMSclient.LiveOutputs.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, le.Name))
                    .ToList();
                plist.ForEach(p => LOList.Add(new Program.LiveOutputExt() { LiveOutputItem = p, LiveEventName = le.Name }));
            }

            float scale = DeviceDpi / 96f;

            IEnumerable<LiveOutputEntry> programquery = from c in (LOList)
                                                            //orderby c.LastModified descending
                                                        select new LiveOutputEntry
                                                        {
                                                            Name = c.LiveOutputItem.Name,
                                                            State = c.LiveOutputItem.ResourceState,
                                                            Description = c.LiveOutputItem.Description,
                                                            ArchiveWindowLength = c.LiveOutputItem.ArchiveWindowLength,
                                                            LastModified = c.LiveOutputItem.LastModified != null ? (DateTime?)((DateTime)c.LiveOutputItem.LastModified).ToLocalTime() : null,
                                                            Published = (Bitmap)HighDpiHelper.ScaleImage(DataGridViewAssets.BuildBitmapPublication(c.LiveOutputItem.AssetName, _client).bitmap, scale),
                                                            LiveEventName = c.LiveEventName
                                                        };



            _MyObservLiveOutputs = new SortableBindingList<LiveOutputEntry>(programquery.ToList());
            BeginInvoke(new Action(() => DataSource = _MyObservLiveOutputs));
            _refreshedatleastonetime = true;

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));

            Debug.WriteLine("RefreshPrograms : end");
        }
    }

}
