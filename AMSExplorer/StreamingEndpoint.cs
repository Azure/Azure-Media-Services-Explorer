﻿//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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

namespace AMSExplorer
{
    public class StreamingEndpointEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public StreamingEndpointState State { get; set; }
        public string CDN { get; set; }
        public string Description { get; set; }
        public int? ScaleUnits { get; set; }
        public string LastModified { get; set; }

    }

    public class DataGridViewStreamingEndpoints : DataGridView
    {
        public int ItemsPerPage
        {
            get
            {
                return _originsperpage;
            }
            set
            {
                _originsperpage = value;
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
        public string OrderStreamingEndpointsInGrid
        {
            get
            {
                return _orderstreamingendpoints;
            }
            set
            {
                _orderstreamingendpoints = value;
            }

        }
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
        public IEnumerable<IStreamingEndpoint> DisplayedStreamingEndpoints
        {
            get
            {
                return streamingendpoints;
            }

        }

        private List<StatusInfo> ListStatus = new List<StatusInfo>();

        static BindingList<StreamingEndpointEntry> _MyObservStreamingEndpoints;
        static BindingList<StreamingEndpointEntry> _MyObservStreamingEndpointthisPage;

        static IEnumerable<IStreamingEndpoint> streamingendpoints;
        static private int _originsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderstreamingendpoints = OrderStreamingEndpoints.LastModified;
        static string _filterstreamingendpointsstate = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;
        static BackgroundWorker WorkerRefreshStreamingEndpoints;

        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<StreamingEndpointEntry> originquery;
            _credentials = credentials;

            _context = context;
            originquery = from o in _context.StreamingEndpoints
                          orderby o.LastModified descending
                          select new StreamingEndpointEntry
                          {
                              Name = o.Name,
                              Id = o.Id,
                              Description = o.Description,
                              ScaleUnits = o.ScaleUnits,
                              State = o.State,
                              LastModified = o.LastModified.ToLocalTime().ToString("G")
                          };


            BindingList<StreamingEndpointEntry> MyObservOriginInPage = new BindingList<StreamingEndpointEntry>(originquery.Take(0).ToList());
            this.DataSource = MyObservOriginInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayOriginIDinGrid;
            this.Columns["Name"].Width = 300;
            this.Columns["State"].Width = 100;
            this.Columns["CDN"].Width = 100;
            this.Columns["Description"].Width = 230;
            this.Columns["ScaleUnits"].Width = 100;
            this.Columns["LastModified"].Width = 150;

            WorkerRefreshStreamingEndpoints = new BackgroundWorker();
            WorkerRefreshStreamingEndpoints.WorkerSupportsCancellation = true;
            WorkerRefreshStreamingEndpoints.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerRefreshStreamingEndpoints_DoWork);

            _initialized = true;
        }


        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                this.DataSource = new BindingList<StreamingEndpointEntry>(_MyObservStreamingEndpoints.Skip(_originsperpage * (page - 1)).Take(_originsperpage).ToList());


            }
        }

        public void RefreshStreamingEndpoint(IStreamingEndpoint origin)
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
                origin = _context.StreamingEndpoints.Where(o => o.Id == origin.Id).FirstOrDefault(); //refresh
                if (origin != null)
                {
                    _MyObservStreamingEndpoints[index].State = origin.State;
                    _MyObservStreamingEndpoints[index].Description = origin.Description;
                    _MyObservStreamingEndpoints[index].LastModified = origin.LastModified.ToLocalTime().ToString("G");
                    if (origin.ScaleUnits != null)
                    {
                        _MyObservStreamingEndpoints[index].ScaleUnits = (int)origin.ScaleUnits;
                        this.Refresh();
                    }
                }
            }
        }

        private void WorkerRefreshStreamingEndpoints_DoWork(object sender, DoWorkEventArgs e)
        {

            Debug.WriteLine("WorkerRefreshChannels_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            IStreamingEndpoint origin;

            foreach (StreamingEndpointEntry OE in _MyObservStreamingEndpoints)
            {

                origin = null;
                try
                {
                    origin = _context.StreamingEndpoints.Where(a => a.Id == OE.Id).FirstOrDefault();
                    if (origin != null)
                    {

                        OE.State = origin.State;

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

        private void RefreshStreamingEndpoints()
        {
            RefreshStreamingEndpoints(_context, _currentpage);
        }


        public void RefreshStreamingEndpoints(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));
            //this.FindForm().Cursor = Cursors.WaitCursor;

            _context = context;

            IEnumerable<StreamingEndpointEntry> endpointquery;

            streamingendpoints = context.StreamingEndpoints;

            _context = context;
            _pagecount = (int)Math.Ceiling(((double)streamingendpoints.Count()) / ((double)_originsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            try
            {
                int c = streamingendpoints.Count();
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + e.Message);
                Environment.Exit(0);
            }


            switch (_orderstreamingendpoints)
            {
                case OrderStreamingEndpoints.LastModified:
                default:
                    streamingendpoints = from c in streamingendpoints
                                         orderby c.LastModified descending
                                         select c;
                    break;

                case OrderStreamingEndpoints.Name:
                    streamingendpoints = from c in streamingendpoints
                                         orderby c.Name
                                         select c;
                    break;

                case OrderStreamingEndpoints.State:
                    streamingendpoints = from c in streamingendpoints
                                         orderby c.State
                                         select c;
                    break;

                case OrderStreamingEndpoints.ScaleUnits:
                    streamingendpoints = from c in streamingendpoints
                                         orderby c.ScaleUnits
                                         select c;
                    break;
            }

            endpointquery = from c in streamingendpoints
                            select new StreamingEndpointEntry
                            {
                                Name = c.Name,
                                Id = c.Id,
                                Description = c.Description,
                                CDN = c.CdnEnabled ? "CDN" : string.Empty,
                                ScaleUnits = c.ScaleUnits,
                                State = c.State,
                                LastModified = c.LastModified.ToLocalTime().ToString("G"),
                            };


            _MyObservStreamingEndpoints = new BindingList<StreamingEndpointEntry>(endpointquery.ToList());
            _MyObservStreamingEndpointthisPage = new BindingList<StreamingEndpointEntry>(_MyObservStreamingEndpoints.Skip(_originsperpage * (_currentpage - 1)).Take(_originsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservStreamingEndpointthisPage));
            _refreshedatleastonetime = true;
            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));
        }
    }
}