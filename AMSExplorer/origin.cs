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
        public DateTime LastModified { get; set; }
        public string Description { get; set; }
        public int? ScaleUnits { get; set; }


    }

    public class DataGridViewOrigins : DataGridView
    {
        public int OriginsPerPage
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
        public string OrderOriginsInGrid
        {
            get
            {
                return _orderorigins;
            }
            set
            {
                _orderorigins = value;
            }

        }
        public string FilterOriginsState
        {
            get
            {
                return _filteroriginsstate;
            }
            set
            {
                _filteroriginsstate = value;
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
                return _MyObservOrigins.Count();
            }

        }
        public IEnumerable<IStreamingEndpoint> DisplayedOrigins
        {
            get
            {
                return origins;
            }

        }

        private List<StatusInfo> ListStatus = new List<StatusInfo>();

        static BindingList<StreamingEndpointEntry> _MyObservOrigins;
        static BindingList<StreamingEndpointEntry> _MyObservOriginthisPage;

        static IEnumerable<IStreamingEndpoint> origins;
        static private int _originsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderorigins = OrderOrigins.LastModified;
        static string _filteroriginsstate = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;
        static BackgroundWorker WorkerRefreshOrigins;

        public void Init(CredentialsEntry credentials)
        {
            IEnumerable<StreamingEndpointEntry> originquery;
            _credentials = credentials;

            _context = Program.ConnectAndGetNewContext(_credentials);
            originquery = from o in _context.StreamingEndpoints
                          orderby o.LastModified descending
                          select new StreamingEndpointEntry
                          {
                              Name = o.Name,
                              Id = o.Id,
                              Description = o.Description,
                              ScaleUnits = o.ScaleUnits,
                              State = o.State,
                              LastModified = o.LastModified.ToLocalTime()

                          };

            /*
            try
            {
                int c = jobs.Count();
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + e.Message);
                Environment.Exit(0);
            }
             * */

            //DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn();
            //DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
            //col.Name = "Progress";
            //col.DataPropertyName = "Progress";

            //this.Columns.Add(col);
            BindingList<StreamingEndpointEntry> MyObservOriginInPage = new BindingList<StreamingEndpointEntry>(originquery.Take(0).ToList());
            this.DataSource = MyObservOriginInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayOriginIDinGrid;
            /*this.Columns["Progress"].DisplayIndex = 6;
            this.Columns["Tasks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Tasks"].Width = 50;
            this.Columns["Priority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Priority"].Width = 50;
            Task.Run(() => RestoreJobProgress());*/

            WorkerRefreshOrigins = new BackgroundWorker();
            WorkerRefreshOrigins.WorkerSupportsCancellation = true;
            WorkerRefreshOrigins.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerRefreshOrigins_DoWork);

            _initialized = true;
        }


        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                this.DataSource = new BindingList<StreamingEndpointEntry>(_MyObservOrigins.Skip(_originsperpage * (page - 1)).Take(_originsperpage).ToList());


            }
        }

        public void RefreshOrigin(IStreamingEndpoint origin)
        {
            int index = -1;
            foreach (StreamingEndpointEntry CE in _MyObservOrigins) // let's search for index
            {
                if (CE.Id == origin.Id)
                {
                    index = _MyObservOrigins.IndexOf(CE);
                    break;
                }
            }


            if (index >= 0) // we found it
            { // we update the observation collection
                origin = _context.StreamingEndpoints.Where(o => o.Id == origin.Id).FirstOrDefault(); //refresh
                if (origin != null)
                {
                    _MyObservOrigins[index].State = origin.State;
                    if (origin.ScaleUnits != null)
                    {
                        _MyObservOrigins[index].ScaleUnits = (int)origin.ScaleUnits;
                    }

                }

                Debug.WriteLine("Refresh origin status");
                // this.Refresh();
            }


        }

        private void WorkerRefreshOrigins_DoWork(object sender, DoWorkEventArgs e)
        {

            Debug.WriteLine("WorkerRefreshChannels_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            IStreamingEndpoint origin;


            foreach (StreamingEndpointEntry OE in _MyObservOrigins)
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

        private void RefreshOrigins()
        {
            RefreshOrigins(_context, _currentpage);
        }


        public void RefreshOrigins(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));
            //this.FindForm().Cursor = Cursors.WaitCursor;

            _context = context;

            //this.Invoke(new Action(() => this.Cursor = Cursors.WaitCursor));

            //   Task.Run(() =>    // REMOVE background task otherwise issue with page number in dropdown control
            //  {
            IEnumerable<StreamingEndpointEntry> originquery;

            /*
            if (_timefilter != "" && _timefilter != null && _timefilter != FilterTime.All)
            {
                switch (_timefilter)
                {
                    case FilterTime.LastDay:
                        channels = context.Channels.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(1)))));
                        break;
                    case FilterTime.LastWeek:
                        channels = context.Channels.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(7)))));
                        break;
                    case FilterTime.LastMonth:
                        channels = context.Channels.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(30)))));
                        break;

                    default:
                        channels = context.Jobs;

                        break;

                }

            }
             * 
            else*/
            origins = context.StreamingEndpoints;


            /*
            if (_filterjobsstate != "All")
            {
                channels = channels.Where(j => j.State == (JobState)Enum.Parse(typeof(JobState), _filterjobsstate));
            }



            if (_searchinname != "" && _searchinname != null)
            {
                string searchlower = _searchinname.ToLower();
                channels = channels.Where(j => (j.Name.ToLower().Contains(searchlower)));
            }
            */

            _context = context;
            _pagecount = (int)Math.Ceiling(((double)origins.Count()) / ((double)_originsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            try
            {
                int c = origins.Count();
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + e.Message);
                Environment.Exit(0);
            }


            /*
            switch (_orderjobs)
            {
                case OrderJobs.LastModified:*/
            originquery = from c in origins
                          orderby c.LastModified descending
                          select new StreamingEndpointEntry
                          {
                              Name = c.Name,
                              Id = c.Id,
                              Description = c.Description,
                              ScaleUnits = c.ScaleUnits,
                              State = c.State,
                              LastModified = c.LastModified.ToLocalTime(),

                          };/*
                    break;
                case OrderJobs.Name:
                    channelquery = from j in channels
                               orderby j.Name
                                   select new ChannelEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? (Nullable<DateTime>)((DateTime)j.EndTime).ToLocalTime() : null,
                                   //Duration = j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   // Running duration == 0 if job is processed so in that case, we calculate it
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.EndTime:
                    jobquery = from j in channels
                               orderby j.EndTime descending
                               select new ChannelEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? (Nullable<DateTime>)((DateTime)j.EndTime).ToLocalTime() : null,
                                   //Duration = j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   // Running duration == 0 if job is processed so in that case, we calculate it
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.ProcessTime:
                    jobquery = from j in channels
                               orderby j.RunningDuration descending
                               select new ChannelEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? (Nullable<DateTime>)((DateTime)j.EndTime).ToLocalTime() : null,
                                   //Duration = j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   // Running duration == 0 if job is processed so in that case, we calculate it
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.StartTime:
                    jobquery = from j in channels
                               orderby j.StartTime descending
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? (Nullable<DateTime>)((DateTime)j.EndTime).ToLocalTime() : null,
                                   //Duration = j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   // Running duration == 0 if job is processed so in that case, we calculate it
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                case OrderJobs.State:
                    jobquery = from j in channels
                               orderby j.State
                               select new ChannelEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? (Nullable<DateTime>)((DateTime)j.EndTime).ToLocalTime() : null,
                                   //Duration = j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   // Running durarion == 0 if job is processed so in that case, we calculate it
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
                default:
                    jobquery = from j in channels
                               orderby j.LastModified descending
                               select new JobEntry
                               {
                                   Name = j.Name,
                                   Id = j.Id,
                                   Tasks = j.Tasks.Count,
                                   Priority = j.Priority,
                                   State = j.State,
                                   StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                                   EndTime = j.EndTime.HasValue ? (Nullable<DateTime>)((DateTime)j.EndTime).ToLocalTime() : null,
                                   //Duration = j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   // Running durarion == 0 if job is processed so in that case, we calculate it
                                   Duration = (j.State == JobState.Processing) ? (j.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - j.StartTime)).ToString(@"hh\:mm\:ss") : null) : j.RunningDuration.ToString(@"hh\:mm\:ss"),
                                   Progress = j.GetOverallProgress()
                               };
                    break;
            }
            */
            _MyObservOrigins = new BindingList<StreamingEndpointEntry>(originquery.ToList());
            _MyObservOriginthisPage = new BindingList<StreamingEndpointEntry>(_MyObservOrigins.Skip(_originsperpage * (_currentpage - 1)).Take(_originsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservOriginthisPage));
            _refreshedatleastonetime = true;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));



        }




        public void AddOriginEvent(StatusInfo statusinfo)
        {
            ListStatus.Add(statusinfo);

        }

        public void DoOriginMonitor(IStreamingEndpoint origin, OperationType operationtype)
        {
            Task.Run(() =>
            {
                if (operationtype == OperationType.Scale)
                {
                    List<StatusInfo> LSI;
                    DateTime starttime = DateTime.Now;
                    System.Threading.Thread.Sleep(1000); // it take some time for the origin to switch to scaling mode

                    while (origin.State == StreamingEndpointState.Scaling)
                    {
                        RefreshOrigin(origin);
                        System.Threading.Thread.Sleep(500);
                        if (DateTime.Now > starttime.AddMinutes(10))
                        {
                            break;
                        }
                        LSI = ListStatus.Where(l => l.EntityName == origin.Name).ToList();
                        if (LSI.Count > 0)
                        {
                            MessageBox.Show(LSI.FirstOrDefault().ErrorMessage);
                            break;
                        }

                    }
                    RefreshOrigin(origin);
                }
                else if (operationtype == OperationType.Delete)
                {
                    string originid = origin.Id;
                    List<StatusInfo> LSI;
                    DateTime starttime = DateTime.Now;
                    while (_context.StreamingEndpoints.Where(o => o.Id == originid).FirstOrDefault() != null)
                    {
                        RefreshOrigin(origin);
                        System.Threading.Thread.Sleep(1000);
                        if (DateTime.Now > starttime.AddMinutes(10))
                        {
                            break;
                        }
                        LSI = ListStatus.Where(l => l.EntityName == origin.Name).ToList();
                        if (LSI.Count > 0)
                        {
                            MessageBox.Show(LSI.FirstOrDefault().ErrorMessage);
                            break;
                        }
                    }
                    RefreshOrigins();
                }
                else
                {
                    StreamingEndpointState StateToReach;

                    switch (operationtype)
                    {
                        case OperationType.Create:
                            StateToReach = StreamingEndpointState.Stopped;
                            break;

                        case OperationType.Start:
                            StateToReach = StreamingEndpointState.Running;
                            break;

                        case OperationType.Stop:
                            StateToReach = StreamingEndpointState.Stopped;
                            break;

                        default:
                            StateToReach = StreamingEndpointState.Stopped;
                            break;
                    }

                    List<StatusInfo> LSI;
                    DateTime starttime = DateTime.Now;

                    while (origin.State != StateToReach)
                    {
                        RefreshOrigin(origin);
                        System.Threading.Thread.Sleep(500);
                        if (DateTime.Now > starttime.AddMinutes(10))
                        {
                            break;
                        }
                        LSI = ListStatus.Where(l => l.EntityName == origin.Name).ToList();
                        if (LSI.Count > 0)
                        {
                            MessageBox.Show(LSI.FirstOrDefault().ErrorMessage);
                            break;
                        }
                    }
                    RefreshOrigin(origin);
                }
            });
        }

    }
}