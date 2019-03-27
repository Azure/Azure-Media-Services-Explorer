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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections.Specialized;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;

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
            get
            {
                return _filterstreamingendpointsstate;
            }
            set
            {
                _filterstreamingendpointsstate = value;
            }

        }
        public string SearchInName
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
        public int DisplayedCount
        {
            get
            {
                return _MyObservStreamingEndpoints.Count();
            }

        }
        public List<StreamingEndpoint> DisplayedStreamingEndpoints
        {
            // we want to keep the sorting in display
            get
            {
                var list = new List<StreamingEndpoint>();
                foreach (var se in _MyObservStreamingEndpoints)
                {
                    var detailedSE = _client.StreamingEndpoints.Get(_resourceName, _accountName, se.Name);
                    if (detailedSE != null) // in some rare cases, SE is null in dev/test account
                        list.Add(detailedSE);
                }
                return list;
            }
        }

        private List<StatusInfo> ListStatus = new List<StatusInfo>();
        static SortableBindingList<StreamingEndpointEntry> _MyObservStreamingEndpoints;
        static IEnumerable<StreamingEndpoint> streamingendpoints;
        static private bool _initialized = false;
        static string _filterstreamingendpointsstate = "All";
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;
        static BackgroundWorker WorkerRefreshStreamingEndpoints;
        private AzureMediaServicesClient _client;
        private string _resourceName;
        private string _accountName;

        public void Init(AzureMediaServicesClient client, CredentialsEntryV3 cred)
        {
            IEnumerable<StreamingEndpointEntry> originquery;
            _client = client;
            _resourceName = cred.ResourceGroup;
            _accountName = cred.AccountName;
            originquery = _client.StreamingEndpoints.List(_resourceName, _accountName).Select(o => new
                          StreamingEndpointEntry
            {
                Name = o.Name,
                Id = o.Id,
                Description = o.Description,
                CDN = ((bool)o.CdnEnabled) ? StreamingEndpointInformation.ReturnDisplayedProvider(o.CdnProvider) ?? "CDN" : string.Empty,
                ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(o) != StreamingEndpointInformation.StreamEndpointType.Premium ? "" : ((int)o.ScaleUnits).ToString(),
                Type = StreamingEndpointInformation.ReturnTypeSE(o),
                State = (StreamingEndpointResourceState)o.ResourceState,
                LastModified = ((DateTime)o.LastModified).ToLocalTime()
            });


            SortableBindingList<StreamingEndpointEntry> MyObservOriginInPage = new SortableBindingList<StreamingEndpointEntry>(originquery.Take(0).ToList());
            this.DataSource = MyObservOriginInPage;
            this.Columns["Name"].Width = 300;
            this.Columns["State"].Width = 100;
            this.Columns["CDN"].Width = 120;
            this.Columns["Description"].Width = 230;
            this.Columns["ScaleUnits"].Width = 100;
            this.Columns["ScaleUnits"].HeaderText = "Streaming Units";
            this.Columns["LastModified"].Width = 150;
            this.Columns["LastModified"].HeaderText = "Last modified";

            WorkerRefreshStreamingEndpoints = new BackgroundWorker();
            WorkerRefreshStreamingEndpoints.WorkerSupportsCancellation = true;
            WorkerRefreshStreamingEndpoints.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerRefreshStreamingEndpoints_DoWork);

            _initialized = true;
        }


        public void RefreshStreamingEndpoint(StreamingEndpoint origin)
        {
            int index = -1;
            foreach (StreamingEndpointEntry CE in _MyObservStreamingEndpoints) // let's search for index
            {
                if (CE.Id == origin.Id)
                {
                    index = _MyObservStreamingEndpoints.IndexOf(CE);
                    break;
                }
            }

            if (index >= 0) // we found it
            { // we update the observation collection
                origin = _client.StreamingEndpoints.Get(_resourceName, _accountName, origin.Name); //refresh
                if (origin != null)
                {
                    _MyObservStreamingEndpoints[index].State = (StreamingEndpointResourceState)origin.ResourceState;
                    _MyObservStreamingEndpoints[index].Description = origin.Description;
                    _MyObservStreamingEndpoints[index].LastModified = ((DateTime)origin.LastModified).ToLocalTime();
                    _MyObservStreamingEndpoints[index].Type = StreamingEndpointInformation.ReturnTypeSE(origin);
                    _MyObservStreamingEndpoints[index].CDN = ((bool)origin.CdnEnabled) ? StreamingEndpointInformation.ReturnDisplayedProvider(origin.CdnProvider) ?? "CDN" : string.Empty;
                    _MyObservStreamingEndpoints[index].ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(origin) != StreamingEndpointInformation.StreamEndpointType.Premium ? "" : ((int)origin.ScaleUnits).ToString();
                    this.Refresh();

                }
            }
        }

        private void WorkerRefreshStreamingEndpoints_DoWork(object sender, DoWorkEventArgs e)
        {

            Debug.WriteLine("WorkerRefreshChannels_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            StreamingEndpoint origin;

            foreach (StreamingEndpointEntry OE in _MyObservStreamingEndpoints)
            {

                origin = null;
                try
                {
                    origin = _client.StreamingEndpoints.Get(_resourceName, _accountName, origin.Name); //refresh
                    if (origin != null)
                    {

                        OE.State = (StreamingEndpointResourceState)origin.ResourceState;

                        //if ((i % 5) == 0) this.BeginInvoke(new Action(() => this.Refresh()), null);
                        this.BeginInvoke(new Action(() => this.Refresh()), null);
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
            this.BeginInvoke(new Action(() => this.Refresh()), null);
        }


        public void RefreshStreamingEndpoints()
        {
            if (!_initialized) return;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));

            IEnumerable<StreamingEndpointEntry> endpointquery;

            streamingendpoints = _client.StreamingEndpoints.List(_resourceName, _accountName);


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
                                ScaleUnits = StreamingEndpointInformation.ReturnTypeSE(c) != StreamingEndpointInformation.StreamEndpointType.Premium ? "" : ((int)c.ScaleUnits).ToString(),
                                State = (StreamingEndpointResourceState)c.ResourceState,
                                LastModified = ((DateTime)c.LastModified).ToLocalTime(),
                                Type = StreamingEndpointInformation.ReturnTypeSE(c)
                            };

            _MyObservStreamingEndpoints = new SortableBindingList<StreamingEndpointEntry>(endpointquery.ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservStreamingEndpoints));
            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));
        }
    }
}