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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class DataGridViewLiveEvent : DataGridView
    {
        public static int LiveEventsPerPage
        {
            get => _liveeventsperpage;
            set => _liveeventsperpage = value;
        }
        public int PageCount => _pagecount;
        public int CurrentPage => _currentpage;

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
        public int DisplayedCount => _MyObservLiveEvent.Count;

        private readonly List<StatusInfo> ListStatus = new();
        private static SortableBindingList<LiveEventEntry> _MyObservLiveEvent;

        private static int _liveeventsperpage = 50; //nb of items per page
        private static readonly int _pagecount = 1;
        private static int _currentpage = 1;
        private static bool _initialized = false;
        private static bool _refreshedatleastonetime = false;
        private static string _statefilter = "All";
        private static SearchObject _searchinname = new() { SearchType = SearchIn.LiveEventName, Text = string.Empty };
        private static string _timefilter = FilterTime.LastWeek;
        private static TimeRangeValue _timefilterTimeRange = new(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);

        public string _encoded = "Encoding";
        public string _encodedPreset = "EncodingPreset";
        public int totalLiveEvents = 0;

        public async Task InitAsync(AMSClientV3 amsClient)
        {
            IEnumerable<LiveEventEntry> channelquery;

            float scale = DeviceDpi / 96f;

            // Listing live events
            //List<MediaLiveEventResource> liveevents = new();

            var liveevents = amsClient.AMSclient.GetMediaLiveEvents().GetAllAsync();

            channelquery = from c in (await liveevents.AsPages(null).FirstAsync()).Values.Take(0)
                           orderby c.Data.LastModifiedOn descending
                           select new LiveEventEntry
                           {
                               Name = c.Data.Name,
                               Description = c.Data.Description,
                               InputProtocol = string.Format("{0} ({1})", c.Data.Input.StreamingProtocol.ToString() /*Program.ReturnNameForProtocol(c.Input.StreamingProtocol)*/, c.Data.Input.Endpoints.Count),
                               Encoding = LiveEventUtil.ReturnChannelBitmap(c),
                               EncodingPreset = (c.Data.Encoding != null && c.Data.Encoding.EncodingType != LiveEventEncodingType.PassthroughStandard && c.Data.Encoding.EncodingType != LiveEventEncodingType.PassthroughBasic) ? c.Data.Encoding.PresetName : string.Empty,
                               InputUrl = c.Data.Input.Endpoints.Count > 0 ? c.Data.Input.Endpoints.FirstOrDefault().Uri : null,
                               PreviewUrl = c.Data.Preview.Endpoints.Count > 0 ? c.Data.Preview.Endpoints.FirstOrDefault().Uri : null,
                               State = c.Data.ResourceState,
                               LastModifiedOn = c.Data.LastModifiedOn != null ? c.Data.LastModifiedOn?.DateTime.ToLocalTime() : null
                           };


            DataGridViewCellStyle cellstyle = new()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            DataGridViewImageColumn imageCol = new()
            {
                DefaultCellStyle = cellstyle,
                Name = _encoded,
                DataPropertyName = _encoded,
            };
            Columns.Add(imageCol);

            SortableBindingList<LiveEventEntry> MyObservChannelsInPage = new(channelquery.Take(0).ToList());
            DataSource = MyObservChannelsInPage;
            Columns["InputUrl"].HeaderText = "Primary Input Url";
            Columns["InputUrl"].Width = 140;
            Columns["InputUrl"].SortMode = DataGridViewColumnSortMode.NotSortable;
            Columns["InputProtocol"].HeaderText = "Input Protocol (input nb)";
            Columns["InputProtocol"].Width = 180;
            Columns["PreviewUrl"].Width = 120;
            Columns["PreviewUrl"].SortMode = DataGridViewColumnSortMode.NotSortable;

            Columns[_encoded].DisplayIndex = ColumnCount - 4;
            Columns[_encoded].DefaultCellStyle.NullValue = null;
            Columns[_encoded].HeaderText = "Type";
            Columns[_encoded].Width = 100;

            Columns[_encodedPreset].DisplayIndex = ColumnCount - 3;
            Columns[_encodedPreset].DefaultCellStyle.NullValue = null;
            Columns[_encodedPreset].HeaderText = "Preset";
            Columns[_encodedPreset].Width = 100;

            Columns["LastModifiedOn"].Width = 140;
            Columns["LastModifiedOn"].HeaderText = "Last Modified";

            Columns["State"].Width = 75;
            Columns["Description"].Width = 110;

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
                DataSource = new BindingList<LiveEventEntry>(_MyObservLiveEvent.Skip(_liveeventsperpage * (page - 1)).Take(_liveeventsperpage).ToList());
            }
        }

        public async Task RefreshLiveEventAsync(MediaLiveEventResource liveEventItem, AMSClientV3 amsClient)
        {
            int index = -1;
            foreach (LiveEventEntry CE in _MyObservLiveEvent) // let's search for index
            {
                if (CE.Name == liveEventItem.Data.Name)
                {
                    index = _MyObservLiveEvent.IndexOf(CE);
                    break;
                }
            }

            if (index >= 0) // we found it
            { // we update the observation collection

                try
                {
                    liveEventItem = await amsClient.GetLiveEventAsync(liveEventItem.Data.Name); //refresh
                    _MyObservLiveEvent[index].State = liveEventItem.Data.ResourceState;
                    _MyObservLiveEvent[index].Description = liveEventItem.Data.Description;
                    _MyObservLiveEvent[index].LastModifiedOn = liveEventItem.Data.LastModifiedOn != null ? liveEventItem.Data.LastModifiedOn?.DateTime.ToLocalTime() : null;
                    RefreshGridView();
                }
                catch (RequestFailedException ex) when (ex.Status == ((int)System.Net.HttpStatusCode.NotFound))
                {
                    // live event not found
                }
            }
        }


        public async Task RefreshLiveEventAsync(AMSClientV3 amsClient) // all assets are refreshed
        {
            if (!_initialized)
            {
                return;
            }

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.WaitCursor));

            // Listing live events
            //List<MediaLiveEventResource> liveevents = new();

            var liveevents = amsClient.AMSclient.GetMediaLiveEvents().GetAllAsync();

            /*
            IPage<MediaLiveEventResource> liveeventsPage = await amsClient.AMSclient.LiveEvents.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName);
            while (liveeventsPage != null)
            {
                liveevents.AddRange(liveeventsPage);
                if (liveeventsPage.NextPageLink != null)
                {
                    liveeventsPage = await amsClient.AMSclient.LiveEvents.ListNextAsync(liveeventsPage.NextPageLink);
                }
                else
                {
                    liveeventsPage = null;
                }
            }
            */
            int totalLiveEvents = 0;
            List<LiveEventEntry> channelquery = new();

            await foreach (var c in liveevents)
            {
                totalLiveEvents++;
                channelquery.Add(
                     new LiveEventEntry
                     {
                         Name = c.Data.Name,
                         Description = c.Data.Description,
                         InputProtocol = string.Format("{0} ({1})", c.Data.Input.StreamingProtocol.ToString(), c.Data.Input.Endpoints.Count),
                         Encoding = LiveEventUtil.ReturnChannelBitmap(c),
                         EncodingPreset = (c.Data.Encoding != null && c.Data.Encoding.EncodingType != LiveEventEncodingType.PassthroughStandard && c.Data.Encoding.EncodingType != LiveEventEncodingType.PassthroughBasic) ? c.Data.Encoding.PresetName : string.Empty,
                         InputUrl = c.Data.Input.Endpoints.Count > 0 ? c.Data.Input.Endpoints.FirstOrDefault().Uri : null,
                         PreviewUrl = c.Data.Preview.Endpoints.Count > 0 ? c.Data.Preview.Endpoints.FirstOrDefault().Uri : null,
                         State = c.Data.ResourceState,
                         LastModifiedOn = c.Data.LastModifiedOn?.DateTime.ToLocalTime()
                     }
                    );

            }

            float scale = DeviceDpi / 96f;

            _MyObservLiveEvent = new SortableBindingList<LiveEventEntry>(channelquery);
            BeginInvoke(new Action(() => DataSource = _MyObservLiveEvent));
            _refreshedatleastonetime = true;
            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
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
