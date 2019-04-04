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
    public class DataGridViewLiveOutput : DataGridView
    {
        private List<string> idsList = new List<string>();
        private List<StatusInfo> ListStatus = new List<StatusInfo>();
        static SortableBindingList<LiveOutputEntry> _MyObservLiveOutputs;

        static private int _itemssperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _statefilter = "All";
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.ProgramName, Text = string.Empty };
        static private string _timefilter = FilterTime.LastWeek;
        static private TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        static BackgroundWorker WorkerRefreshChannels;
        public string _published = "Published";
        static Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        static private enumDisplayProgram _anyChannel = enumDisplayProgram.Selected;
        private AMSClientV3 _client;

        public List<string> LiveEventSourceNames
        {
            get
            {
                return idsList;
            }
            set
            {
                idsList = value;
            }
        }

        public int ItemsPerPage
        {
            get
            {
                return _itemssperpage;
            }
            set
            {
                _itemssperpage = value;
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

        public enumDisplayProgram DisplayChannel
        {
            get
            {
                return _anyChannel;
            }
            set
            {
                _anyChannel = value;
            }
        }

        public string FilterState
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
        public bool Initialized
        {
            get
            {
                return _initialized;
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
                return _MyObservLiveOutputs != null ? _MyObservLiveOutputs.Count() : 0;
            }
        }



        public void Init(AMSClientV3 client)
        {
            IEnumerable<LiveOutputEntry> programquery;
            client.RefreshTokenIfNeeded();

            _client = client;
            _client.RefreshTokenIfNeeded();

            var ListEvents = _client.AMSclient.LiveEvents.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName).ToList();
            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();

            foreach (var le in ListEvents)
            {
                var plist = _client.AMSclient.LiveOutputs.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, le.Name).ToList();
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
            this.Columns.Add(imageCol);


            SortableBindingList<LiveOutputEntry> MyObservProgramInPage = new SortableBindingList<LiveOutputEntry>(programquery.Take(0).ToList());
            this.DataSource = MyObservProgramInPage;
            //this.Columns["LiveEventName"].Visible = false;
            this.Columns[_published].DisplayIndex = this.ColumnCount - 3;
            this.Columns[_published].DefaultCellStyle.NullValue = null;
            this.Columns[_published].HeaderText = _published;
            this.Columns["LastModified"].Width = 130;
            this.Columns["LastModified"].HeaderText = "Last modified";
            this.Columns["Description"].Width = 150;
            this.Columns["ArchiveWindowLength"].Width = 130;
            this.Columns["ArchiveWindowLength"].HeaderText = "Archive window";
            this.Columns["LiveEventName"].HeaderText = "Live event name";

            WorkerRefreshChannels = new BackgroundWorker();
            WorkerRefreshChannels.WorkerSupportsCancellation = true;
            WorkerRefreshChannels.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerRefreshChannels_DoWork);

            _initialized = true;
        }


        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                this.DataSource = new BindingList<LiveOutputEntry>(_MyObservLiveOutputs.Skip(_itemssperpage * (page - 1)).Take(_itemssperpage).ToList());
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
                        this.Refresh();
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
                        this.BeginInvoke(new Action(() => this.Refresh()), null);
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

        private void RefreshPrograms() // all assets are refreshed
        {
            Task.Run(async () => await RefreshLiveOutputsAsync(_currentpage));

        }

        public async Task RefreshLiveOutputsAsync(int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;
            if (idsList.Count == 0) return;

            Debug.WriteLine("RefreshPrograms : start");

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));

            _client.RefreshTokenIfNeeded();

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

            foreach (var le in ListEvents)
            {
                var plist = (await _client.AMSclient.LiveOutputs.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, le.Name))
                    .ToList();
                plist.ForEach(p => LOList.Add(new Program.LiveOutputExt() { LiveOutputItem = p, LiveEventName = le.Name }));
            }

            var programquery = from c in (LOList)
                                   //orderby c.LastModified descending
                               select new LiveOutputEntry
                               {
                                   Name = c.LiveOutputItem.Name,
                                   State = c.LiveOutputItem.ResourceState,
                                   Description = c.LiveOutputItem.Description,
                                   ArchiveWindowLength = c.LiveOutputItem.ArchiveWindowLength,
                                   LastModified = c.LiveOutputItem.LastModified != null ? (DateTime?)((DateTime)c.LiveOutputItem.LastModified).ToLocalTime() : null,
                                   Published = DataGridViewAssets.BuildBitmapPublication(c.LiveOutputItem.AssetName, _client).bitmap,
                                   LiveEventName = c.LiveEventName
                               };



            _MyObservLiveOutputs = new SortableBindingList<LiveOutputEntry>(programquery.ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservLiveOutputs));
            _refreshedatleastonetime = true;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));

            Debug.WriteLine("RefreshPrograms : end");
        }
    }

}
