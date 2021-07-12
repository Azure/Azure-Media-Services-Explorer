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
        private List<string> idsList = new();
        private readonly List<StatusInfo> ListStatus = new();
        private static SortableBindingList<LiveOutputEntry> _MyObservLiveOutputs;

        private static int _itemssperpage = 50; //nb of items per page
        private static readonly int _pagecount = 1;
        private static int _currentpage = 1;
        private static bool _initialized = false;
        private static bool _refreshedatleastonetime = false;
        private static string _statefilter = "All";
        private static SearchObject _searchinname = new() { SearchType = SearchIn.LiveOutputName, Text = string.Empty };
        private static string _timefilter = FilterTime.LastWeek;
        private static TimeRangeValue _timefilterTimeRange = new(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        public string _published = "Published";
        private static readonly Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        private static enumDisplayProgram _anyChannel = enumDisplayProgram.Selected;

        public void SetLiveEventSourceNames(List<string> list)
        {
            idsList = list;
        }

        public List<string> GetLiveEventSourceNames()
        {
            return idsList;
        }

        public int ItemsPerPage
        {
            get => _itemssperpage;
            set => _itemssperpage = value;
        }

        public int PageCount => _pagecount;
        public int CurrentPage => _currentpage;

        public static enumDisplayProgram DisplayLiveEvent
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
        public int DisplayedCount => _MyObservLiveOutputs != null ? _MyObservLiveOutputs.Count : 0;

        public void Init(AMSClientV3 amsClient)
        {
            IEnumerable<LiveOutputEntry> programquery;

            List<LiveEvent> ListEvents = amsClient.AMSclient.LiveEvents.List(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName).ToList();
            List<Program.LiveOutputExt> LOList = new();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = amsClient.AMSclient.LiveOutputs.List(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, le.Name).ToList();
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

            DataGridViewCellStyle cellstyle = new()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            DataGridViewImageColumn imageCol = new()
            {
                DefaultCellStyle = cellstyle,
                Name = _published,
                DataPropertyName = _published,
            };
            Columns.Add(imageCol);


            SortableBindingList<LiveOutputEntry> MyObservProgramInPage = new(programquery.Take(0).ToList());
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

        public async Task RefreshLiveOutputAsync(string liveeventName, LiveOutput liveOutput, AMSClientV3 amsClient)
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

                liveOutput = await amsClient.GetLiveOutputAsync(liveeventName, liveOutput.Name); //refresh
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


        public async Task RefreshLiveOutputsAsync(AMSClientV3 amsClient) // all assets are refreshed
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

            IEnumerable<LiveEvent> ListEvents;
            if (_anyChannel == enumDisplayProgram.None)
            {
                ListEvents = new List<LiveEvent>();
            }
            else
            {
                ListEvents = (await amsClient.AMSclient.LiveEvents.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName))
                    .ToList()
                    .Where(l => _anyChannel == enumDisplayProgram.Any || (_anyChannel == enumDisplayProgram.Selected && GetLiveEventSourceNames().Contains(l.Name)));
            }


            List<Program.LiveOutputExt> LOList = new();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = (await amsClient.AMSclient.LiveOutputs.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, le.Name))
                    .ToList();
                plist.ForEach(p => LOList.Add(new Program.LiveOutputExt() { LiveOutputItem = p, LiveEventName = le.Name }));
            }

            float scale = DeviceDpi / 96f;

            var tasksBuildBitmaps = LOList.Select(
                                        async item => new
                                        {
                                            LOExt = item,
                                            LOBitmap = await DataGridViewAssets.BuildBitmapPublicationAsync(item.LiveOutputItem.AssetName, amsClient)
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
                                                              Published = c.LOBitmap.bitmap,
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
