//----------------------------------------------------------------------- 
// <copyright file="live.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.2
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

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
using System.Runtime.Serialization;



namespace AMSExplorer
{
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
        static string _orderitems = OrderJobs.LastModifiedDescending;
        static string _statefilter = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;
        static BackgroundWorker WorkerRefreshChannels;
        static Bitmap EncodingImage = Bitmaps.encoding;
        public string _encoded = "Encoding";

        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<ChannelEntry> channelquery;
            _credentials = credentials;

            _context = context;
            channelquery = from c in _context.Channels
                           orderby c.LastModified descending
                           select new ChannelEntry
                           {
                               Name = c.Name,
                               Id = c.Id,
                               Description = c.Description,
                               InputProtocol = string.Format("{0} ({1})", Program.ReturnNameForProtocol(c.Input.StreamingProtocol), c.Input.Endpoints.Count),
                               Encoding = c.EncodingType != ChannelEncodingType.None ? EncodingImage : null,
                               InputUrl = c.Input.Endpoints.FirstOrDefault().Url,
                               PreviewUrl = c.Preview.Endpoints.FirstOrDefault().Url,
                               State = c.State,
                               LastModified = c.LastModified.ToLocalTime()
                           };


            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _encoded,
                DataPropertyName = _encoded,
            };
            this.Columns.Add(imageCol);

            BindingList<ChannelEntry> MyObservJobInPage = new BindingList<ChannelEntry>(channelquery.Take(0).ToList());
            this.DataSource = MyObservJobInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
            this.Columns["InputUrl"].HeaderText = "Primary Input Url";
            this.Columns["InputProtocol"].HeaderText = "Input Protocol (input nb)";

            this.Columns[_encoded].DisplayIndex = this.ColumnCount - 3;
            this.Columns[_encoded].DefaultCellStyle.NullValue = null;
            this.Columns[_encoded].HeaderText = "Cloud Encoding";

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
                if (channel != null)
                {
                    _MyObservChannels[index].State = channel.State;
                    _MyObservChannels[index].Description = channel.Description;
                    _MyObservChannels[index].LastModified = channel.LastModified.ToLocalTime();
                    this.Refresh();
                }
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

        private void RefreshChannels() // all assets are refreshed
        {
            RefreshChannels(_context, _currentpage);
        }

        public void RefreshChannels(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.WaitCursor));
            _context = context;

            IEnumerable<ChannelEntry> channelquery;

            int days = FilterTime.ReturnNumberOfDays(_timefilter);
            channels = (days == -1) ? context.Channels : context.Channels.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)))));

            if (!string.IsNullOrEmpty(_searchinname))
            {
                string searchlower = _searchinname.ToLower();
                channels = channels.Where(c => (c.Name.ToLower().Contains(searchlower) || c.Id.ToLower().Contains(searchlower)));
            }

            if (FilterState != "All")
            {
                channels = channels.Where(c => c.State == (ChannelState)Enum.Parse(typeof(ChannelState), _statefilter));
            }


            switch (_orderitems)
            {
                case OrderChannels.LastModified:
                    channelquery = from c in channels
                                   orderby c.LastModified descending
                                   select new ChannelEntry
                                   {
                                       Name = c.Name,
                                       Id = c.Id,
                                       Description = c.Description,
                                       InputProtocol = string.Format("{0} ({1})", Program.ReturnNameForProtocol(c.Input.StreamingProtocol), c.Input.Endpoints.Count),
                                       Encoding = c.EncodingType != ChannelEncodingType.None ? EncodingImage : null,
                                       InputUrl = c.Input.Endpoints.FirstOrDefault().Url,
                                       PreviewUrl = c.Preview.Endpoints.FirstOrDefault().Url,
                                       State = c.State,
                                       LastModified = c.LastModified.ToLocalTime()
                                   };
                    break;



                case OrderChannels.Name:
                    channelquery = from c in channels
                                   orderby c.Name
                                   select new ChannelEntry
                                   {
                                       Name = c.Name,
                                       Id = c.Id,
                                       Description = c.Description,
                                       InputProtocol = string.Format("{0} ({1})", Program.ReturnNameForProtocol(c.Input.StreamingProtocol), c.Input.Endpoints.Count),
                                       Encoding = c.EncodingType != ChannelEncodingType.None ? EncodingImage : null,
                                       InputUrl = c.Input.Endpoints.FirstOrDefault().Url,
                                       PreviewUrl = c.Preview.Endpoints.FirstOrDefault().Url,
                                       State = c.State,
                                       LastModified = c.LastModified.ToLocalTime()
                                   };
                    break;

                case OrderChannels.State:
                    channelquery = from c in channels
                                   orderby c.State
                                   select new ChannelEntry
                                   {
                                       Name = c.Name,
                                       Id = c.Id,
                                       Description = c.Description,
                                       InputProtocol = string.Format("{0} ({1})", Program.ReturnNameForProtocol(c.Input.StreamingProtocol), c.Input.Endpoints.Count),
                                       Encoding = c.EncodingType != ChannelEncodingType.None ? EncodingImage : null,
                                       InputUrl = c.Input.Endpoints.FirstOrDefault().Url,
                                       PreviewUrl = c.Preview.Endpoints.FirstOrDefault().Url,
                                       State = c.State,
                                       LastModified = c.LastModified.ToLocalTime()
                                   };
                    break;

                default:
                    channelquery = from c in channels
                                   select new ChannelEntry
                                   {
                                       Name = c.Name,
                                       Id = c.Id,
                                       Description = c.Description,
                                       InputProtocol = string.Format("{0} ({1})", Program.ReturnNameForProtocol(c.Input.StreamingProtocol), c.Input.Endpoints.Count),
                                       Encoding = c.EncodingType != ChannelEncodingType.None ? EncodingImage : null,
                                       InputUrl = c.Input.Endpoints.FirstOrDefault().Url,
                                       PreviewUrl = c.Preview.Endpoints.FirstOrDefault().Url,
                                       State = c.State,
                                       LastModified = c.LastModified.ToLocalTime()
                                   };
                    break;
            }

            if ((!string.IsNullOrEmpty(_timefilter)) && _timefilter == FilterTime.First50Items)
            {
                channelquery = channelquery.Take(50);
            }

            _MyObservChannels = new BindingList<ChannelEntry>(channelquery.ToList());
            _MyObservChannelthisPage = new BindingList<ChannelEntry>(_MyObservChannels.Skip(_channelsperpage * (_currentpage - 1)).Take(_channelsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservChannelthisPage));
            _refreshedatleastonetime = true;

            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));
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
        public string _published = "Published";
        static Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;

        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<ProgramEntry> programquery;
            _credentials = credentials;

            _context = context;
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
                               Published = null,
                               ChannelName = c.Channel.Name,
                               ChannelId = c.Channel.Id
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


            BindingList<ProgramEntry> MyObservProgramInPage = new BindingList<ProgramEntry>(programquery.Take(0).ToList());
            this.DataSource = MyObservProgramInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
            this.Columns["ChannelId"].Visible = false;
            this.Columns[_published].DisplayIndex = this.ColumnCount - 3;
            this.Columns[_published].DefaultCellStyle.NullValue = null;
            this.Columns[_published].HeaderText = _published;

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
                        _MyObservPrograms[index].Description = program.Description;
                        _MyObservPrograms[index].ArchiveWindowLength = program.ArchiveWindowLength;
                        _MyObservPrograms[index].LastModified = program.LastModified;
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
            int days = FilterTime.ReturnNumberOfDays(_timefilter);

            programs = (days == -1) ? context.Programs : context.Programs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)))));

            if (!string.IsNullOrEmpty(_searchinname))
            {
                string searchlower = _searchinname.ToLower();
                programs = programs.Where(p => (p.Name.ToLower().Contains(searchlower) || p.Id.ToLower().Contains(searchlower) || p.Asset.Id.ToLower().Contains(searchlower)));
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
                            Published = p.Asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).Count() > 0 ? Streaminglocatorimage : null,
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
                            Published = p.Asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).Count() > 0 ? Streaminglocatorimage : null,

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
                            Published = p.Asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).Count() > 0 ? Streaminglocatorimage : null,

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
                      Published = p.Asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).Count() > 0 ? Streaminglocatorimage : null,

                  }).ToArray();
                    break;
            }

            if ((!string.IsNullOrEmpty(_timefilter)) && _timefilter == FilterTime.First50Items)
            {
                programquery = programquery.Take(50);
            }

            _MyObservPrograms = new BindingList<ProgramEntry>(programquery.ToList());
            _MyObservProgramsthisPage = new BindingList<ProgramEntry>(_MyObservPrograms.Skip(_itemssperpage * (_currentpage - 1)).Take(_itemssperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservProgramsthisPage));
            _refreshedatleastonetime = true;
            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));
        }
    }

    public static class OrderPrograms
    {
        public const string LastModified = "Last modified";
        public const string Name = "Name";
        public const string State = "State";
    }

    public static class OrderChannels
    {
        public const string LastModified = "Last modified";
        public const string Name = "Name";
        public const string State = "State";
    }
    public class ChannelInfo
    {
        public static async Task<IOperation> ChannelExecuteOperationAsync(Func<Task<IOperation>> fCall, IChannel channel, string strStatusSuccess, CloudMediaContext _context, Mainform mainform, DataGridViewLiveChannel dataGridViewChannelsV = null) //used for all except creation 
        {
            IOperation operation = null;

            try
            {
                var state = channel.State;
                var STask = fCall();
                operation = await STask;

                while (operation.State == OperationState.InProgress)
                {
                    //refresh the operation
                    operation = _context.Operations.GetOperation(operation.Id);
                    // refresh the channel
                    IChannel channelR = _context.Channels.Where(c => c.Id == channel.Id).FirstOrDefault();
                    if (channelR != null && state != channelR.State)
                    {
                        state = channelR.State;
                        if (dataGridViewChannelsV != null)
                            dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channelR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                if (operation.State == OperationState.Succeeded)
                {
                    mainform.TextBoxLogWriteLine("Channel '{0}' {1}.", channel.Name, strStatusSuccess);
                }
                else
                {
                    mainform.TextBoxLogWriteLine("Channel '{0}' NOT {1}. (Error {2})", channel.Name, strStatusSuccess, operation.ErrorCode, true);
                    mainform.TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
                if (dataGridViewChannelsV != null) dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channel)), null);
            }
            catch (Exception ex)
            {
                mainform.TextBoxLogWriteLine("Error with channel '{0}' : {1}", channel.Name, Program.GetErrorMessage(ex), true);
            }
            return operation;
        }




        public static async Task<IOperation> ChannelExecuteOperationAsync(Func<TimeSpan, int, bool, Task<IOperation>> fCall, TimeSpan ts, int i, bool b, IChannel channel, string strStatusSuccess, CloudMediaContext _context, Mainform mainform, DataGridViewLiveChannel dataGridViewChannelsV = null) //used for all except creation 
        {
            IOperation operation = null;

            try
            {
                var state = channel.State;
                var STask = fCall(ts, i, b);
                operation = await STask;

                while (operation.State == OperationState.InProgress)
                {
                    //refresh the operation
                    operation = _context.Operations.GetOperation(operation.Id);
                    // refresh the channel
                    IChannel channelR = _context.Channels.Where(c => c.Id == channel.Id).FirstOrDefault();
                    if (channelR != null && state != channelR.State)
                    {
                        state = channelR.State;
                        if (dataGridViewChannelsV != null)
                            dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channelR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                if (operation.State == OperationState.Succeeded)
                {
                    mainform.TextBoxLogWriteLine("Channel '{0}' {1}.", channel.Name, strStatusSuccess);
                }
                else
                {
                    mainform.TextBoxLogWriteLine("Channel '{0}' NOT {1}. (Error {2})", channel.Name, strStatusSuccess, operation.ErrorCode, true);
                    mainform.TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
                if (dataGridViewChannelsV != null) dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channel)), null);
            }
            catch (Exception ex)
            {
                mainform.TextBoxLogWriteLine("Error with channel '{0}' : {1}", channel.Name, Program.GetErrorMessage(ex), true);
            }
            return operation;
        }




        public static async Task<IOperation> ChannelExecuteOperationAsync(Func<TimeSpan, string, Task<IOperation>> fCall, TimeSpan ts, string s, IChannel channel, string strStatusSuccess, CloudMediaContext _context, Mainform mainform, DataGridViewLiveChannel dataGridViewChannelsV = null) //used for all except creation 
        {
            IOperation operation = null;

            try
            {
                var state = channel.State;
                var STask = fCall(ts, s);
                operation = await STask;

                while (operation.State == OperationState.InProgress)
                {
                    //refresh the operation
                    operation = _context.Operations.GetOperation(operation.Id);
                    // refresh the channel
                    IChannel channelR = _context.Channels.Where(c => c.Id == channel.Id).FirstOrDefault();
                    if (channelR != null && state != channelR.State)
                    {
                        state = channelR.State;
                        if (dataGridViewChannelsV != null)
                            dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channelR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                if (operation.State == OperationState.Succeeded)
                {
                    mainform.TextBoxLogWriteLine("Channel '{0}' {1}.", channel.Name, strStatusSuccess);
                }
                else
                {
                    mainform.TextBoxLogWriteLine("Channel '{0}' NOT {1}. (Error {2})", channel.Name, strStatusSuccess, operation.ErrorCode, true);
                    mainform.TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
                if (dataGridViewChannelsV != null) dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channel)), null);
            }
            catch (Exception ex)
            {
                mainform.TextBoxLogWriteLine("Error with channel '{0}' : {1}", channel.Name, Program.GetErrorMessage(ex), true);
            }
            return operation;
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
        public string InputProtocol { get; set; }
        public Bitmap Encoding { get; set; }
        public Uri InputUrl { get; set; }
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
        public Bitmap Published { get; set; }
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
                          .Where(o => (o.State == StreamingEndpointState.Running) && (o.ScaleUnits > 0))
                          .OrderByDescending(o => o.CdnEnabled)
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
                         .Where(o => (o.State != StreamingEndpointState.Running) || (o.ScaleUnits == 0))
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

    public static class AccessToken
    {
        public static string GetAccessToken(CloudMediaContext cloudMediaContext, string host)
        {
            using (var client = new WebClient())
            {
                client.BaseAddress = host;

                var values
                    = new NameValueCollection
                        {
                            {"grant_type", "client_credentials"},
                            {"client_id", cloudMediaContext.Credentials.ClientId},
                            {"client_secret", HttpUtility.HtmlEncode(cloudMediaContext.Credentials.ClientSecret)},
                            {"scope", HttpUtility.HtmlEncode("urn:WindowsAzureMediaServices")}
                        };

                using (var stream = new MemoryStream(client.UploadValues("/v2/OAuth2-13", "POST", values)))
                {
                    var response = (OAuth2TokenResponse)new DataContractJsonSerializer(typeof(OAuth2TokenResponse)).ReadObject(stream);
                    return response.AccessToken;
                }
            }
        }
    }

    [DataContract]
    internal class OAuth2TokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
