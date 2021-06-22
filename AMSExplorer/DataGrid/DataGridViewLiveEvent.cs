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
        private AMSClientV3 _amsClient;
        private static SearchObject _searchinname = new() { SearchType = SearchIn.LiveEventName, Text = string.Empty };
        private static string _timefilter = FilterTime.LastWeek;
        private static TimeRangeValue _timefilterTimeRange = new(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private static readonly Bitmap EncodingImage = Bitmaps.encoding;
        private static readonly Bitmap StandardEncodingImage = Bitmaps.encoding;
        private static readonly Bitmap PremiumEncodingImage = Bitmaps.encodingPremium;

        public string _encoded = "Encoding";
        public string _encodedPreset = "EncodingPreset";
        public int totalLiveEvents = 0;

        private Bitmap ReturnChannelBitmap(LiveEvent channel)
        {

            return (string)channel.Encoding.EncodingType switch
            {
                nameof(LiveEventEncodingType.None) => null,
                nameof(LiveEventEncodingType.Standard) => StandardEncodingImage,
                nameof(LiveEventEncodingType.Premium1080p) => PremiumEncodingImage,
                _ => null,
            };
        }

        public async Task InitAsync(AMSClientV3 client)
        {
            IEnumerable<LiveEventEntry> channelquery;

            _amsClient = client;
            float scale = DeviceDpi / 96f;

            // Listing live events
            List<LiveEvent> liveevents = new();
            IPage<LiveEvent> liveeventsPage = await _amsClient.AMSclient.LiveEvents.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (liveeventsPage != null)
            {
                liveevents.AddRange(liveeventsPage);
                if (liveeventsPage.NextPageLink != null)
                {
                    liveeventsPage = await _amsClient.AMSclient.LiveEvents.ListNextAsync(liveeventsPage.NextPageLink);
                }
                else
                {
                    liveeventsPage = null;
                }
            }

            channelquery = from c in liveevents.Take(0)
                           orderby c.LastModified descending
                           select new LiveEventEntry
                           {
                               Name = c.Name,
                               Description = c.Description,
                               InputProtocol = string.Format("{0} ({1})", c.Input.StreamingProtocol.ToString() /*Program.ReturnNameForProtocol(c.Input.StreamingProtocol)*/, c.Input.Endpoints.Count),
                               Encoding = ReturnChannelBitmap(c),
                               EncodingPreset = (c.Encoding != null && c.Encoding.EncodingType != LiveEventEncodingType.None) ? c.Encoding.PresetName : string.Empty,
                               InputUrl = c.Input.Endpoints.Count > 0 ? c.Input.Endpoints.FirstOrDefault().Url : string.Empty,
                               PreviewUrl = c.Preview.Endpoints.Count > 0 ? c.Preview.Endpoints.FirstOrDefault().Url : string.Empty,
                               State = c.ResourceState,
                               LastModified = c.LastModified != null ? (DateTime?)((DateTime)c.LastModified).ToLocalTime() : null
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
            Columns[_encoded].HeaderText = "Cloud Encoding";
            Columns[_encoded].Width = 100;

            Columns[_encodedPreset].DisplayIndex = ColumnCount - 3;
            Columns[_encodedPreset].DefaultCellStyle.NullValue = null;
            Columns[_encodedPreset].HeaderText = "Preset";
            Columns[_encodedPreset].Width = 100;

            Columns["LastModified"].Width = 140;
            Columns["LastModified"].HeaderText = "Last Modified";

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

        public async Task RefreshLiveEventAsync(LiveEvent liveEventItem)
        {
            int index = -1;
            foreach (LiveEventEntry CE in _MyObservLiveEvent) // let's search for index
            {
                if (CE.Name == liveEventItem.Name)
                {
                    index = _MyObservLiveEvent.IndexOf(CE);
                    break;
                }
            }

            if (index >= 0) // we found it
            { // we update the observation collection
                
                liveEventItem = await _amsClient.GetLiveEventAsync(liveEventItem.Name); //refresh
                if (liveEventItem != null)
                {
                    _MyObservLiveEvent[index].State = liveEventItem.ResourceState;
                    _MyObservLiveEvent[index].Description = liveEventItem.Description;
                    _MyObservLiveEvent[index].LastModified = liveEventItem.LastModified != null ? (DateTime?)((DateTime)liveEventItem.LastModified).ToLocalTime() : null;
                    RefreshGridView();
                }
            }
        }
      

        public async Task RefreshLiveEventAsync(int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized)
            {
                return;
            }

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.WaitCursor));

            // Listing live events
            List<LiveEvent> liveevents = new();
            IPage<LiveEvent> liveeventsPage = await _amsClient.AMSclient.LiveEvents.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (liveeventsPage != null)
            {
                liveevents.AddRange(liveeventsPage);
                if (liveeventsPage.NextPageLink != null)
                {
                    liveeventsPage = await _amsClient.AMSclient.LiveEvents.ListNextAsync(liveeventsPage.NextPageLink);
                }
                else
                {
                    liveeventsPage = null;
                }
            }


            totalLiveEvents = liveevents.Count;
            float scale = DeviceDpi / 96f;

            IEnumerable<LiveEventEntry> channelquery = liveevents.Select(c =>
                       new LiveEventEntry
                       {
                           Name = c.Name,
                           Description = c.Description,
                           InputProtocol = string.Format("{0} ({1})", c.Input.StreamingProtocol.ToString(), c.Input.Endpoints.Count),
                           Encoding = ReturnChannelBitmap(c),
                           EncodingPreset = (c.Encoding != null && c.Encoding.EncodingType != LiveEventEncodingType.None) ? c.Encoding.PresetName : string.Empty,
                           InputUrl = c.Input.Endpoints.Count > 0 ? c.Input.Endpoints.FirstOrDefault().Url : string.Empty,
                           PreviewUrl = c.Preview.Endpoints.Count > 0 ? c.Preview.Endpoints.FirstOrDefault().Url : string.Empty,
                           State = c.ResourceState,
                           LastModified = c.LastModified != null ? (DateTime?)((DateTime)c.LastModified).ToLocalTime() : null
                       }
            );

            _MyObservLiveEvent = new SortableBindingList<LiveEventEntry>(channelquery.ToList());
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
