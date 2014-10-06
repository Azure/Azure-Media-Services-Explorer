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
    public static class OrderPrograms
    {
        public const string LastModified = "Last modified";
        public const string Name = "Name";
        public const string State = "State";
    }


    public class DataGridViewLiveChannel : DataGridView
    {
        public int ChannelsPerPage
        {
            get
            {
                return _channelsperpage;
            }
            set
            {
                _channelsperpage = value;
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
        public string OrderJobsInGrid
        {
            get
            {
                return _orderjobs;
            }
            set
            {
                _orderjobs = value;
            }

        }
        public string FilterJobsState
        {
            get
            {
                return _filterjobsstate;
            }
            set
            {
                _filterjobsstate = value;
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
                return _MyObservChannels.Count();
            }

        }
        public IEnumerable<IChannel> DisplayedJobs
        {
            get
            {
                return channels;
            }

        }

        private List<StatusInfo> ListStatus = new List<StatusInfo>();

        static BindingList<ChannelEntry> _MyObservChannels;
        static BindingList<ChannelEntry> _MyObservChannelthisPage;

        static IEnumerable<IChannel> channels;
        static private int _channelsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderjobs = OrderJobs.LastModified;
        static string _filterjobsstate = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;
        static BackgroundWorker WorkerRefreshChannels;

        public void Init(CredentialsEntry credentials)
        {
            IEnumerable<ChannelEntry> channelquery;
            _credentials = credentials;

            _context = Program.ConnectAndGetNewContext(_credentials);
            channelquery = from c in _context.Channels
                           orderby c.LastModified descending
                           select new ChannelEntry
                           {
                               Name = c.Name,
                               Id = c.Id,
                               Description = c.Description,
                               InputProtocol = c.Input.StreamingProtocol,
                               IngestUrl = c.Input.Endpoints.FirstOrDefault().Url,
                               PreviewUrl = c.Preview.Endpoints.FirstOrDefault().Url,
                               State = c.State,
                               LastModified = c.LastModified.ToLocalTime()

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
            BindingList<ChannelEntry> MyObservJobInPage = new BindingList<ChannelEntry>(channelquery.Take(0).ToList());
            this.DataSource = MyObservJobInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
            /*this.Columns["Progress"].DisplayIndex = 6;
            this.Columns["Tasks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Tasks"].Width = 50;
            this.Columns["Priority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Priority"].Width = 50;
            Task.Run(() => RestoreJobProgress());*/

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
                this.DataSource = new BindingList<ChannelEntry>(_MyObservChannels.Skip(_channelsperpage * (page - 1)).Take(_channelsperpage).ToList());


            }
        }

        public void RefreshChannel(IChannel channel)
        {
            int index = -1;
            foreach (ChannelEntry CE in _MyObservChannels) // let's search for index
            {
                if (CE.Id == channel.Id)
                {
                    index = _MyObservChannels.IndexOf(CE);
                    break;
                }
            }


            if (index >= 0) // we found it
            { // we update the observation collection
                channel = _context.Channels.Where(c => c.Id == channel.Id).FirstOrDefault(); //refresh
                if (channel != null) _MyObservChannels[index].State = channel.State;
                Debug.WriteLine("Refresh channel status");
                // this.Refresh();
            }


        }

        private void WorkerRefreshChannels_DoWork(object sender, DoWorkEventArgs e)
        {

            Debug.WriteLine("WorkerRefreshChannels_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            IChannel channel;


            foreach (ChannelEntry CE in _MyObservChannels)
            {

                channel = null;
                try
                {
                    channel = _context.Channels.Where(a => a.Id == CE.Id).FirstOrDefault();
                    if (channel != null)
                    {

                        CE.State = channel.State;

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

        private void RefreshChannels() // all assets are refreshed
        {
            RefreshChannels(_context, _currentpage);
        }

        public void RefreshChannels(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;


            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));
            _context = context;

            //this.Invoke(new Action(() => this.Cursor = Cursors.WaitCursor));

            //   Task.Run(() =>    // REMOVE background task otherwise issue with page number in dropdown control
            //  {
            IEnumerable<ChannelEntry> channelquery;

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
            channels = context.Channels;


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
            _pagecount = (int)Math.Ceiling(((double)channels.Count()) / ((double)_channelsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            try
            {
                int c = channels.Count();
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
            channelquery = from c in channels
                           orderby c.LastModified descending
                           select new ChannelEntry
                           {
                               Name = c.Name,
                               Id = c.Id,
                               Description = c.Description,
                               InputProtocol = c.Input.StreamingProtocol,
                               IngestUrl = c.Input.Endpoints.FirstOrDefault().Url,
                               PreviewUrl = c.Preview.Endpoints.FirstOrDefault().Url,
                               State = c.State,
                               LastModified = c.LastModified.ToLocalTime()

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
            _MyObservChannels = new BindingList<ChannelEntry>(channelquery.ToList());
            _MyObservChannelthisPage = new BindingList<ChannelEntry>(_MyObservChannels.Skip(_channelsperpage * (_currentpage - 1)).Take(_channelsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservChannelthisPage));
            _refreshedatleastonetime = true;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));


        }

        private void RestoreJobProgress()  // when app is launched, we want to restore job progress updates
        {
            /*
            IEnumerable<IJob> ActiveJobs = _context.Jobs.Where(j => (j.State == JobState.Queued) | (j.State == JobState.Scheduled) | (j.State == JobState.Processing));

            foreach (IJob job in ActiveJobs)
            {
                this.DoJobProgress(job);
            }
             * */
        }

        //private List<StatusInfo> ListStatus = new List<StatusInfo>();

        public void AddChannelEvent(StatusInfo statusinfo)
        {
            ListStatus.Add(statusinfo);

        }

        public void DoChannelMonitor(IChannel channel, OperationType operationtype)
        {
            Task.Run(() =>
            {


                if (operationtype == OperationType.Delete)
                {

                    bool timeout = false;
                    bool Error = false;
                    List<StatusInfo> LSI;
                    DateTime starttime = DateTime.Now;


                    while (_context.Channels.Where(o => o.Id == channel.Id).FirstOrDefault() != null)
                    {

                        RefreshChannel(channel);
                        System.Threading.Thread.Sleep(1000);
                        if (DateTime.Now > starttime.AddMinutes(10))
                        {
                            timeout = true;
                            break;
                        }
                        LSI = ListStatus.Where(l => l.EntityName == channel.Name).ToList();
                        if (LSI.Count > 0)
                        {
                            Error = true;
                            MessageBox.Show(LSI.FirstOrDefault().ErrorMessage);
                            break;
                        }

                    }
                    RefreshChannels();
                }


                else
                {

                    ChannelState StateToReach;

                    switch (operationtype)
                    {
                        case OperationType.Create:
                            StateToReach = ChannelState.Stopped;
                            break;

                        case OperationType.Start:
                            StateToReach = ChannelState.Running;
                            break;

                        case OperationType.Stop:
                            StateToReach = ChannelState.Stopped;
                            break;

                        case OperationType.Reset:
                            StateToReach = ChannelState.Stopped;
                            break;


                        default:
                            StateToReach = ChannelState.Stopped;
                            break;


                    }




                    bool timeout = false;
                    bool Error = false;
                    List<StatusInfo> LSI;
                    DateTime starttime = DateTime.Now;

                    while (channel.State != StateToReach)
                    {
                        RefreshChannel(channel);
                        System.Threading.Thread.Sleep(500);
                        if (DateTime.Now > starttime.AddMinutes(10))
                        {
                            timeout = true;
                            break;
                        }
                        LSI = ListStatus.Where(l => l.EntityName == channel.Name).ToList();
                        if (LSI.Count > 0)
                        {
                            Error = true;
                            MessageBox.Show(LSI.FirstOrDefault().ErrorMessage);
                            break;
                        }

                    }
                    RefreshChannel(channel);

                }

            });
        }



    }

    public class DataGridViewLiveProgram : DataGridView
    {

        public List<string> ChannelSourceIDs
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
        public string OrderItemsInGrid
        {
            get
            {
                return _orderitems;
            }
            set
            {
                _orderitems = value;
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
                return _MyObservPrograms.Count();
            }

        }
        public IEnumerable<IProgram> DisplayedItems
        {
            get
            {
                return programs;
            }

        }


        private List<string> idsList = new List<string>();

        private List<StatusInfo> ListStatus = new List<StatusInfo>();

        static BindingList<ProgramEntry> _MyObservPrograms;
        static BindingList<ProgramEntry> _MyObservProgramsthisPage;

        static IEnumerable<IProgram> programs;
        static private int _itemssperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderitems = OrderPrograms.LastModified;
        static string _statefilter = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;
        static BackgroundWorker WorkerRefreshChannels;

        public void Init(CredentialsEntry credentials)
        {
            IEnumerable<ProgramEntry> programquery;
            _credentials = credentials;

            _context = Program.ConnectAndGetNewContext(_credentials);
            programquery = from c in _context.Programs
                           orderby c.LastModified descending
                           select new ProgramEntry
                           {
                               Name = c.Name,
                               Id = c.Id,
                               State = c.State,
                               Description = c.Description,
                               ArchiveWindowLength = c.ArchiveWindowLength,
                               LastModified = c.LastModified.ToLocalTime(),
                               ChannelName = c.Channel.Name,
                               ChannelId = c.Channel.Id


                           };



            BindingList<ProgramEntry> MyObservProgramInPage = new BindingList<ProgramEntry>(programquery.Take(0).ToList());
            this.DataSource = MyObservProgramInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
            this.Columns["ChannelId"].Visible = false;

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
                this.DataSource = new BindingList<ProgramEntry>(_MyObservPrograms.Skip(_itemssperpage * (page - 1)).Take(_itemssperpage).ToList());
            }
        }

        public void RefreshProgram(IProgram program)
        {
            int index = -1;
            foreach (ProgramEntry CE in _MyObservPrograms) // let's search for index
            {
                if (CE.Id == program.Id)
                {
                    index = _MyObservPrograms.IndexOf(CE);
                    break;
                }
            }


            if (index >= 0) // we found it
            { // we update the observation collection
                program = _context.Programs.Where(c => c.Id == program.Id).FirstOrDefault(); //refresh
                if (program != null)
                {
                    try // sometimes, index could be wrong id program has been deleted
                    {
                        _MyObservPrograms[index].State = program.State;
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
            IProgram program;


            foreach (ProgramEntry CE in _MyObservPrograms)
            {

                program = null;
                try
                {
                    program = _context.Programs.Where(a => a.Id == CE.Id).FirstOrDefault();
                    if (program != null)
                    {
                        CE.State = program.State;
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
            RefreshPrograms(_context, _currentpage);
        }

        public void RefreshPrograms(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;


            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));
            _context = context;

            IEnumerable<ProgramEntry> programquery;


            if (_timefilter != string.Empty && _timefilter != null && _timefilter != FilterTime.All)
            {
                switch (_timefilter)
                {
                    case FilterTime.LastDay:
                        programs = context.Programs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(1)))));
                        break;
                    case FilterTime.LastWeek:
                        programs = context.Programs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(7)))));
                        break;
                    case FilterTime.LastMonth:
                        programs = context.Programs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(30)))));
                        break;

                    default:
                        programs = context.Programs;

                        break;

                }

            }

            else
                programs = context.Programs;



            if (_searchinname != "" && _searchinname != null)
            {
                string searchlower = _searchinname.ToLower();
                programs = programs.Where(p => (p.Name.ToLower().Contains(searchlower)));
            }

            if (FilterState != "All")
            {
                programs = programs.Where(p => p.State == (ProgramState)Enum.Parse(typeof(ProgramState), _statefilter));
            }


            switch (_orderitems)
            {
                case OrderPrograms.LastModified:
                    programquery = programs.AsEnumerable().Where(p => idsList.Contains(p.ChannelId)).OrderByDescending(p => p.LastModified)
                 .Join(_context.Channels.AsEnumerable(), p => p.ChannelId, c => c.Id,
                    (p, c) =>
                        new ProgramEntry
                        {
                            Name = p.Name,
                            Id = p.Id,
                            Description = p.Description,
                            ArchiveWindowLength = p.ArchiveWindowLength,
                            State = p.State,
                            LastModified = p.LastModified.ToLocalTime(),
                            ChannelName = c.Name,
                            ChannelId = c.Id,
                        }).ToArray();
                    break;

                case OrderPrograms.Name:
                    programquery = programs.AsEnumerable().Where(p => idsList.Contains(p.ChannelId)).OrderBy(p => p.Name)
                 .Join(_context.Channels.AsEnumerable(), p => p.ChannelId, c => c.Id,
                    (p, c) =>
                        new ProgramEntry
                        {
                            Name = p.Name,
                            Id = p.Id,
                            Description = p.Description,
                            ArchiveWindowLength = p.ArchiveWindowLength,
                            State = p.State,
                            LastModified = p.LastModified.ToLocalTime(),
                            ChannelName = c.Name,
                            ChannelId = c.Id,
                        }).ToArray();
                    break;

                case OrderPrograms.State:
                    programquery = programs.AsEnumerable().Where(p => idsList.Contains(p.ChannelId)).OrderBy(p => p.State)
                 .Join(_context.Channels.AsEnumerable(), p => p.ChannelId, c => c.Id,
                    (p, c) =>
                        new ProgramEntry
                        {
                            Name = p.Name,
                            Id = p.Id,
                            Description = p.Description,
                            ArchiveWindowLength = p.ArchiveWindowLength,
                            State = p.State,
                            LastModified = p.LastModified.ToLocalTime(),
                            ChannelName = c.Name,
                            ChannelId = c.Id,
                        }).ToArray();
                    break;

                default:
                    programquery = programs.AsEnumerable().Where(p => idsList.Contains(p.ChannelId))
           .Join(_context.Channels.AsEnumerable(), p => p.ChannelId, c => c.Id,
              (p, c) =>
                  new ProgramEntry
                  {
                      Name = p.Name,
                      Id = p.Id,
                      Description = p.Description,
                      ArchiveWindowLength = p.ArchiveWindowLength,
                      State = p.State,
                      LastModified = p.LastModified.ToLocalTime(),
                      ChannelName = c.Name,
                      ChannelId = c.Id,
                  }).ToArray();
                    break;
            }


            _MyObservPrograms = new BindingList<ProgramEntry>(programquery.ToList());
            _MyObservProgramsthisPage = new BindingList<ProgramEntry>(_MyObservPrograms.Skip(_itemssperpage * (_currentpage - 1)).Take(_itemssperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservProgramsthisPage));
            _refreshedatleastonetime = true;
            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));


        }



        public void AddProgramEvent(StatusInfo statusinfo)
        {
            ListStatus.Add(statusinfo);

        }

        public void DoProgramMonitor(IProgram program, OperationType operationtype)
        {
            Task.Run(() =>
            {
                if (operationtype == OperationType.Delete)
                {
                    List<StatusInfo> LSI;
                    DateTime starttime = DateTime.Now;
                    while (_context.Programs.Where(p => p.Id == program.Id).FirstOrDefault() != null)
                    {
                        RefreshProgram(program);
                        System.Threading.Thread.Sleep(1000);
                        if (DateTime.Now > starttime.AddMinutes(10))
                        {
                            break;
                        }
                        LSI = ListStatus.Where(l => l.EntityName == program.Name).ToList();
                        if (LSI.Count > 0)
                        {
                            MessageBox.Show(LSI.FirstOrDefault().ErrorMessage);
                            break;
                        }

                    }
                    RefreshPrograms();
                }


                else
                {

                    ProgramState StateToReach;

                    switch (operationtype)
                    {
                        case OperationType.Create:
                            StateToReach = ProgramState.Stopped;
                            break;

                        case OperationType.Start:
                            StateToReach = ProgramState.Running;
                            break;

                        case OperationType.Stop:
                            StateToReach = ProgramState.Stopped;
                            break;

                        case OperationType.Reset:
                            StateToReach = ProgramState.Stopped;
                            break;


                        default:
                            StateToReach = ProgramState.Stopped;
                            break;


                    }

                    List<StatusInfo> LSI;
                    DateTime starttime = DateTime.Now;

                    while (program.State != StateToReach)
                    {
                        RefreshProgram(program);
                        System.Threading.Thread.Sleep(500);
                        if (DateTime.Now > starttime.AddMinutes(10))
                        {
                            break;
                        }
                        LSI = ListStatus.Where(l => l.EntityName == program.Name).ToList();
                        if (LSI.Count > 0)
                        {
                            MessageBox.Show(LSI.FirstOrDefault().ErrorMessage);
                            break;
                        }
                    }
                    RefreshProgram(program);
                }
            });
        }

    }


    public class ChannelEntry
    {
        //   select new { j.Name, j.Id, j.State, j.StartTime, j.EndTime, j.Tasks[0].PerfMessage, Progress=j.GetOverallProgress() };
        public string Name { get; set; }
        public string Id { get; set; }
        public ChannelState State { get; set; }
        public DateTime LastModified { get; set; }
        public string Description { get; set; }
        public StreamingProtocol InputProtocol { get; set; }
        public Uri IngestUrl { get; set; }
        public Uri PreviewUrl { get; set; }
    }

    public class ProgramEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public ProgramState State { get; set; }
        public DateTime LastModified { get; set; }
        public string Description { get; set; }
        public TimeSpan? ArchiveWindowLength { get; set; }
        public string ChannelName { get; set; }
        public string ChannelId { get; set; }
    }


    public enum OperationType
    {
        Create = 0,
        Delete,
        Start,
        Stop,
        Edit,
        Scale,
        SettingsUpdate,
        ResetAsset,
        Reset
    }

    public class StatusInfo
    {
        public string EntityName { get; set; }
        public string ErrorMessage { get; set; }

        public OperationType OperationType { get; set; }

        public DateTime TimeStamp { get; set; }

        public StatusInfo()
        {
            TimeStamp = DateTime.Now;
        }
    }

    public class EndpointSettingInfo
    {
        public string Name { get; set; }
        public string IpV4 { get; set; }
    }



    public class ProgramInfo
    {
        private List<IProgram> SelectedPrograms;
        private CloudMediaContext _context;

        public ProgramInfo(IProgram program, CloudMediaContext context)
        {
            SelectedPrograms = new List<IProgram>();
            SelectedPrograms.Add(program);
            _context = context;

        }
        public ProgramInfo(List<IProgram> MySelectedPrograms, CloudMediaContext context)
        {
            SelectedPrograms = MySelectedPrograms;
            _context = context;
        }

        public IEnumerable<Uri> GetValidURIs()
        {
            IEnumerable<Uri> ValidURIs;
            IAsset asset = _context.Assets.Where(a => a.Id == SelectedPrograms.FirstOrDefault().AssetId).Single();
            var ismFile = asset.AssetFiles.AsEnumerable().FirstOrDefault(f => f.Name.EndsWith(".ism"));
            if (ismFile != null)
            {
                var locators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);

                var template = new UriTemplate("{contentAccessComponent}/{ismFileName}/manifest");
                ValidURIs = locators.SelectMany(l =>
                    _context
                        .StreamingEndpoints
                        .AsEnumerable()
                          .Where(o => o.State == StreamingEndpointState.Running)
                        .Select(
                            o =>
                                template.BindByPosition(new Uri("http://" + o.HostName), l.ContentAccessComponent,
                                    ismFile.Name)))
                    .ToArray();

                return ValidURIs;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Uri> GetNotValidURIs()
        {
            IEnumerable<Uri> NotValidURIs;
            IAsset asset = _context.Assets.Where(a => a.Id == SelectedPrograms.FirstOrDefault().AssetId).Single();
            var ismFile = asset.AssetFiles.AsEnumerable().FirstOrDefault(f => f.Name.EndsWith(".ism"));
            if (ismFile != null)
            {
                var locators = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin);

                var template = new UriTemplate("{contentAccessComponent}/{ismFileName}/manifest");


                NotValidURIs = locators.SelectMany(l =>
                   _context
                       .StreamingEndpoints
                       .AsEnumerable()
                         .Where(o => o.State != StreamingEndpointState.Running)
                       .Select(
                           o =>
                               template.BindByPosition(new Uri("http://" + o.HostName), l.ContentAccessComponent,
                                   ismFile.Name)))
                   .ToArray();

                return NotValidURIs;

            }
            else
            {
                return null;
            }
        }
    }
}
