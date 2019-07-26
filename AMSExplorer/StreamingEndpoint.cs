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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class StreamingEndpointEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public StreamingEndpointResourceState State { get; set; }
        public StreamingEndpointInformation.StreamEndpointType Type { get; set; }
        public string CDN { get; set; }
        public string Description { get; set; }
        public string ScaleUnits { get; set; }
        public DateTime LastModified { get; set; }

    }

    public class DataGridViewStreamingEndpoints : DataGridView
    {

        public string FilterStreamingEndpointsState
        {
            get => _filterstreamingendpointsstate;
            set => _filterstreamingendpointsstate = value;

        }
        public string SearchInName
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
        public int DisplayedCount => _MyObservStreamingEndpoints.Count();
        public List<StreamingEndpoint> DisplayedStreamingEndpoints
        {
            // we want to keep the sorting in display
            get
            {
                _client.RefreshTokenIfNeeded();
                List<StreamingEndpoint> list = new List<StreamingEndpoint>();
                foreach (StreamingEndpointEntry se in _MyObservStreamingEndpoints)
                {
                    StreamingEndpoint detailedSE = _client.AMSclient.StreamingEndpoints.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, se.Name);
                    if (detailedSE != null) // in some rare cases, SE is null in dev/test account
                    {
                        list.Add(detailedSE);
                    }
                }
                return list;
            }
        }

        private readonly List<StatusInfo> ListStatus = new List<StatusInfo>();
        private static SortableBindingList<StreamingEndpointEntry> _MyObservStreamingEndpoints;
        private static IEnumerable<StreamingEndpoint> streamingendpoints;
        private static bool _initialized = false;
        private static string _filterstreamingendpointsstate = "All";
        private static string _searchinname = string.Empty;
        private static string _timefilter = FilterTime.LastWeek;
        private static BackgroundWorker WorkerRefreshStreamingEndpoints;
        private AMSClientV3 _client;

        public void Init(AMSClientV3 client)
        {
            IEnumerable<StreamingEndpointEntry> originquery;
            _client = client;
            _client.RefreshTokenIfNeeded();
            originquery = _client.AMSclient.StreamingEndpoints.List(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName).Select(o => new
                          StreamingEndpointEntry
            {
                Name = o.Name,
                Id = o.Id,
                Description = o.Description,
                CDN = ((bool)o.CdnEnabled) ? StreamingEndpointInformation.ReturnDisplayedProvider(o.CdnProvider) ?? "CDN" : string.Empty,
                ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(o) != StreamingEndpointInformation.StreamEndpointType.Premium ? string.Empty : o.ScaleUnits.ToString(),
                Type = StreamingEndpointInformation.ReturnTypeSE(o),
                State = (StreamingEndpointResourceState)o.ResourceState,
                LastModified = ((DateTime)o.LastModified).ToLocalTime()
            });


            SortableBindingList<StreamingEndpointEntry> MyObservOriginInPage = new SortableBindingList<StreamingEndpointEntry>(originquery.Take(0).ToList());
            DataSource = MyObservOriginInPage;
            Columns["Name"].Width = 300;
            Columns["State"].Width = 100;
            Columns["CDN"].Width = 120;
            Columns["Description"].Width = 230;
            Columns["ScaleUnits"].Width = 100;
            Columns["ScaleUnits"].HeaderText = "Streaming Units";
            Columns["LastModified"].Width = 150;
            Columns["LastModified"].HeaderText = "Last modified";

            WorkerRefreshStreamingEndpoints = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            WorkerRefreshStreamingEndpoints.DoWork += new System.ComponentModel.DoWorkEventHandler(WorkerRefreshStreamingEndpoints_DoWork);

            _initialized = true;
        }


        public async Task RefreshStreamingEndpointAsync(StreamingEndpoint streamingEndpoint)
        {
            int index = -1;
            foreach (StreamingEndpointEntry CE in _MyObservStreamingEndpoints) // let's search for index
            {
                if (CE.Id == streamingEndpoint.Id)
                {
                    index = _MyObservStreamingEndpoints.IndexOf(CE);
                    break;
                }
            }

            if (index >= 0) // we found it
            { // we update the observation collection
                await _client.RefreshTokenIfNeededAsync();
                streamingEndpoint = await _client.AMSclient.StreamingEndpoints.GetAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, streamingEndpoint.Name); //refresh
                if (streamingEndpoint != null)
                {
                    _MyObservStreamingEndpoints[index].State = (StreamingEndpointResourceState)streamingEndpoint.ResourceState;
                    _MyObservStreamingEndpoints[index].Description = streamingEndpoint.Description;
                    _MyObservStreamingEndpoints[index].LastModified = ((DateTime)streamingEndpoint.LastModified).ToLocalTime();
                    _MyObservStreamingEndpoints[index].Type = StreamingEndpointInformation.ReturnTypeSE(streamingEndpoint);
                    _MyObservStreamingEndpoints[index].CDN = ((bool)streamingEndpoint.CdnEnabled) ? StreamingEndpointInformation.ReturnDisplayedProvider(streamingEndpoint.CdnProvider) ?? "CDN" : string.Empty;
                    _MyObservStreamingEndpoints[index].ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(streamingEndpoint) != StreamingEndpointInformation.StreamEndpointType.Premium ? string.Empty : streamingEndpoint.ScaleUnits.ToString();
                    BeginInvoke(new Action(() => Refresh()));
                }
            }
        }

        private void WorkerRefreshStreamingEndpoints_DoWork(object sender, DoWorkEventArgs e)
        {

            Debug.WriteLine("WorkerRefreshChannels_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            StreamingEndpoint origin;
            _client.RefreshTokenIfNeeded();

            foreach (StreamingEndpointEntry OE in _MyObservStreamingEndpoints)
            {
                origin = null;
                try
                {
                    origin = _client.AMSclient.StreamingEndpoints.Get(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, origin.Name); //refresh
                    if (origin != null)
                    {

                        OE.State = (StreamingEndpointResourceState)origin.ResourceState;

                        //if ((i % 5) == 0) this.BeginInvoke(new Action(() => this.Refresh()), null);
                        BeginInvoke(new Action(() => Refresh()), null);
                        //i++;
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


        public async Task RefreshStreamingEndpointsAsync()
        {
            if (!_initialized)
            {
                return;
            }

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.WaitCursor));

            IEnumerable<StreamingEndpointEntry> endpointquery;

            await _client.RefreshTokenIfNeededAsync();
            streamingendpoints = await _client.AMSclient.StreamingEndpoints.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName);

            try
            {
                int c = streamingendpoints.Count();
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + e.Message);
                Environment.Exit(0);
            }

            endpointquery = from c in streamingendpoints
                            select new StreamingEndpointEntry
                            {
                                Name = c.Name,
                                Id = c.Id,
                                Description = c.Description,
                                CDN = (bool)c.CdnEnabled ? StreamingEndpointInformation.ReturnDisplayedProvider(c.CdnProvider) ?? "CDN" : string.Empty,
                                ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(c) != StreamingEndpointInformation.StreamEndpointType.Premium ? string.Empty : c.ScaleUnits.ToString(),
                                State = (StreamingEndpointResourceState)c.ResourceState,
                                LastModified = ((DateTime)c.LastModified).ToLocalTime(),
                                Type = StreamingEndpointInformation.ReturnTypeSE(c)
                            };

            _MyObservStreamingEndpoints = new SortableBindingList<StreamingEndpointEntry>(endpointquery.ToList());
            BeginInvoke(new Action(() => DataSource = _MyObservStreamingEndpoints));
            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
        }
    }
}