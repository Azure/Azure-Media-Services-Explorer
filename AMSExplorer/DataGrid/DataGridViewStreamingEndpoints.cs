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


using Azure.ResourceManager.Media;
using Azure.ResourceManager.Media.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class DataGridViewStreamingEndpoints : DataGridView
    {
        private readonly List<StatusInfo> ListStatus = new();
        private static SortableBindingList<StreamingEndpointEntry> _MyObservStreamingEndpoints;
        //private static IEnumerable<StreamingEndpointResource> streamingendpoints;
        private static bool _initialized = false;
        private static string _filterstreamingendpointsstate = "All";
        private static string _searchinname = string.Empty;
        private static string _timefilter = FilterTime.LastWeek;

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
        public int DisplayedCount => _MyObservStreamingEndpoints.Count;
        public List<StreamingEndpointResource> GetDisplayedStreamingEndpoints(AMSClientV3 amsClient)
        {
            List<StreamingEndpointResource> list = new();
            foreach (StreamingEndpointEntry se in _MyObservStreamingEndpoints)
            {
                try
                {
                    StreamingEndpointResource detailedSE = Task.Run(() => amsClient.GetStreamingEndpointAsync(se.Name)).GetAwaiter().GetResult();
                    list.Add(detailedSE);
                }
                catch
                {

                }
            }
            return list;
        }

        public async Task InitAsync(AMSClientV3 amsClient)
        {
            IEnumerable<StreamingEndpointEntry> originquery;

            var sesQuery = amsClient.AMSclient.GetStreamingEndpoints().GetAllAsync();
            //Microsoft.Rest.Azure.IPage<StreamingEndpointResource> ses = await amsClient.AMSclient.StreamingEndpoints.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName);


            originquery = (await sesQuery.AsPages(null).FirstAsync()).Values.Take(0)
                .Select(o => new
                          StreamingEndpointEntry
                {
                    Name = o.Data.Name,
                    Id = o.Data.Id,
                    Description = o.Data.Description,
                    CDN = ((bool)o.Data.IsCdnEnabled) ? StreamingEndpointInformation.ReturnDisplayedProvider(o.Data.CdnProvider) ?? "CDN" : string.Empty,
                    ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(o) != StreamingEndpointInformation.StreamEndpointType.Premium ? string.Empty : o.Data.ScaleUnits.ToString(),
                    Type = StreamingEndpointInformation.ReturnTypeSE(o),
                    State = (StreamingEndpointResourceState)o.Data.ResourceState,
                    LastModifiedOn = o.Data?.LastModifiedOn?.DateTime.ToLocalTime()
                });

            SortableBindingList<StreamingEndpointEntry> MyObservOriginInPage = new(originquery.Take(0).ToList());
            DataSource = MyObservOriginInPage;
            Columns["Id"].Visible = false;
            Columns["Name"].Width = 300;
            Columns["State"].Width = 100;
            Columns["CDN"].Width = 120;
            Columns["Description"].Width = 230;
            Columns["ScaleUnits"].Width = 100;
            Columns["ScaleUnits"].HeaderText = "Streaming Units";
            Columns["LastModifiedOn"].Width = 150;
            Columns["LastModifiedOn"].HeaderText = "Last Modified";

            _initialized = true;
        }


        public async Task RefreshStreamingEndpointAsync(StreamingEndpointResource streamingEndpoint, AMSClientV3 amsClient)
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

                try
                {
                    streamingEndpoint = await amsClient.GetStreamingEndpointAsync(streamingEndpoint.Data.Name); //refresh
                    _MyObservStreamingEndpoints[index].State = (StreamingEndpointResourceState)streamingEndpoint.Data.ResourceState;
                    _MyObservStreamingEndpoints[index].Description = streamingEndpoint.Data.Description;
                    _MyObservStreamingEndpoints[index].LastModifiedOn = streamingEndpoint.Data.LastModifiedOn?.DateTime.ToLocalTime();
                    _MyObservStreamingEndpoints[index].Type = StreamingEndpointInformation.ReturnTypeSE(streamingEndpoint);
                    _MyObservStreamingEndpoints[index].CDN = ((bool)streamingEndpoint.Data.IsCdnEnabled) ? StreamingEndpointInformation.ReturnDisplayedProvider(streamingEndpoint.Data.CdnProvider) ?? "CDN" : string.Empty;
                    _MyObservStreamingEndpoints[index].ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(streamingEndpoint) != StreamingEndpointInformation.StreamEndpointType.Premium ? string.Empty : streamingEndpoint.Data.ScaleUnits.ToString();
                    BeginInvoke(new Action(Refresh));
                }
                catch
                {

                }
            }
        }


        public async Task RefreshStreamingEndpointsAsync(AMSClientV3 amsClient)
        {
            if (!_initialized)
            {
                return;
            }

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.WaitCursor));

            IEnumerable<StreamingEndpointEntry> endpointquery;

            var streamingendpointsPage = amsClient.AMSclient.GetStreamingEndpoints().GetAllAsync();
            List<StreamingEndpointResource> streamingEndpointResources = new();

            await foreach (var se in streamingendpointsPage)
            {
                streamingEndpointResources.Add(se);
            }
            //streamingendpoints = await amsClient.AMSclient.StreamingEndpoints.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName);

            try
            {
                int c = streamingEndpointResources.Count();
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + e.Message);
                Environment.Exit(0);
            }

            endpointquery = from c in streamingEndpointResources
                            select new StreamingEndpointEntry
                            {
                                Name = c.Data.Name,
                                Id = c.Data.Id,
                                Description = c.Data.Description,
                                CDN = (bool)c.Data.IsCdnEnabled ? StreamingEndpointInformation.ReturnDisplayedProvider(c.Data.CdnProvider) ?? "CDN" : string.Empty,
                                ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(c) != StreamingEndpointInformation.StreamEndpointType.Premium ? string.Empty : c.Data.ScaleUnits.ToString(),
                                State = (StreamingEndpointResourceState)c.Data.ResourceState,
                                LastModifiedOn = c.Data.LastModifiedOn?.DateTime.ToLocalTime(),
                                Type = StreamingEndpointInformation.ReturnTypeSE(c)
                            };

            _MyObservStreamingEndpoints = new SortableBindingList<StreamingEndpointEntry>(endpointquery.ToList());
            BeginInvoke(new Action(() => DataSource = _MyObservStreamingEndpoints));
            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
        }
    }
}