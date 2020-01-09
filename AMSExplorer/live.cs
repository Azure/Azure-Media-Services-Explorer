//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Linq.Expressions;

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
        public SearchObject SearchInName
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
        public TimeRangeValue TimeFilterTimeRange
        {
            get
            {
                return _timefilterTimeRange;
            }
            set
            {
                _timefilterTimeRange = value;
            }
        }
        public int DisplayedCount
        {
            get
            {
                return _MyObservChannels.Count();
            }
        }

        private List<StatusInfo> ListStatus = new List<StatusInfo>();
        static SortableBindingList<ChannelEntry> _MyObservChannels;

        static private int _channelsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _statefilter = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.ChannelName, Text = "" };
        static private string _timefilter = FilterTime.LastWeek;
        static private TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        static BackgroundWorker WorkerRefreshChannels;
        static Bitmap EncodingImage = Bitmaps.encoding;
        static Bitmap PremiumEncodingImage = Bitmaps.encodingPremium;
        public string _encoded = "Encoding";
        public string _encodedPreset = "EncodingPreset";

        private Bitmap ReturnChannelBitmap(IChannel channel)
        {
            switch (channel.EncodingType)
            {
                case ChannelEncodingType.None:
                    return null;

                case ChannelEncodingType.Standard:
                    return EncodingImage;

                case ChannelEncodingType.Premium:
                    return PremiumEncodingImage;

                default:
                    return null;
            }
        }

        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<ChannelEntry> channelquery;
            _credentials = credentials;

            _context = context;
            channelquery = from c in _context.Channels.Take(0)
                           orderby c.LastModified descending
                           select new ChannelEntry
                           {
                               Name = c.Name,
                               Id = c.Id,
                               Description = c.Description,
                               InputProtocol = string.Format("{0} ({1})", Program.ReturnNameForProtocol(c.Input.StreamingProtocol), c.Input.Endpoints.Count),
                               Encoding = ReturnChannelBitmap(c),
                               EncodingPreset = (c.EncodingType != ChannelEncodingType.None && c.Encoding != null) ? c.Encoding.SystemPreset : string.Empty,
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

            SortableBindingList<ChannelEntry> MyObservChannelsInPage = new SortableBindingList<ChannelEntry>(channelquery.Take(0).ToList());
            this.DataSource = MyObservChannelsInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
            this.Columns["InputUrl"].HeaderText = "Primary Input Url";
            this.Columns["InputUrl"].Width = 140;
            this.Columns["InputUrl"].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Columns["InputProtocol"].HeaderText = "Input Protocol (input nb)";
            this.Columns["InputProtocol"].Width = 180;
            this.Columns["PreviewUrl"].Width = 120;
            this.Columns["PreviewUrl"].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.Columns[_encoded].DisplayIndex = this.ColumnCount - 4;
            this.Columns[_encoded].DefaultCellStyle.NullValue = null;
            this.Columns[_encoded].HeaderText = "Cloud Encoding";
            this.Columns[_encoded].Width = 100;

            this.Columns[_encodedPreset].DisplayIndex = this.ColumnCount - 3;
            this.Columns[_encodedPreset].DefaultCellStyle.NullValue = null;
            this.Columns[_encodedPreset].HeaderText = "Preset";
            this.Columns[_encodedPreset].Width = 100;

            this.Columns["LastModified"].Width = 140;
            this.Columns["LastModified"].HeaderText = "Last modified";

            this.Columns["State"].Width = 75;
            this.Columns["Description"].Width = 110;

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

            // DAYS
            bool filterStartDate = false;
            bool filterEndDate = false;

            DateTime dateTimeStart = DateTime.UtcNow;
            DateTime dateTimeRangeEnd = DateTime.UtcNow.AddDays(1);

            int days = FilterTime.ReturnNumberOfDays(_timefilter);

            if (days > 0)
            {
                filterStartDate = true;
                dateTimeStart = (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)));
            }
            else if (days == -1) // TimeRange
            {
                filterStartDate = true;
                filterEndDate = true;
                dateTimeStart = _timefilterTimeRange.StartDate;
                if (_timefilterTimeRange.EndDate != null) // there is an end time
                {
                    dateTimeRangeEnd = (DateTime)_timefilterTimeRange.EndDate;
                }
            }

            // STATE
            bool filterstate = FilterState != "All";
            ChannelState channelstate = ChannelState.Running;
            if (filterstate)
            {
                channelstate = (ChannelState)Enum.Parse(typeof(ChannelState), FilterState);
            }

            IQueryable<IChannel> channelssrv = context.Channels;

            // search
            if (_searchinname != null && !string.IsNullOrEmpty(_searchinname.Text))
            {
                bool Error = false;

                switch (_searchinname.SearchType)
                {
                    case SearchIn.ChannelName:
                        channelssrv = context.Channels.Where(c =>
                                                 (c.Name.ToLower().Contains(_searchinname.Text.ToLower()))
                                                 &&
                                                 (!filterStartDate || c.LastModified > dateTimeStart)
                                                 &&
                                                 (!filterEndDate || c.LastModified < dateTimeRangeEnd)
                                                 );
                        break;

                    case SearchIn.ChannelId:
                        string channelguid = _searchinname.Text;
                        if (channelguid.StartsWith(Constants.ChannelIdPrefix))
                        {
                            channelguid = channelguid.Substring(Constants.ChannelIdPrefix.Length);
                        }
                        try
                        {
                            var g = new Guid(channelguid);
                        }
                        catch
                        {
                            Error = true;
                            MessageBox.Show("Error with channel Id. Is it a valid GUID or channel Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (!Error)
                        {
                            channelssrv = context.Channels.Where(c =>
                                                    (c.Id == Constants.ChannelIdPrefix + channelguid)
                                                    &&
                                                    (!filterStartDate || c.LastModified > dateTimeStart)
                                                    &&
                                                    (!filterEndDate || c.LastModified < dateTimeRangeEnd)
                                                    );
                        }
                        break;

                    default:
                        break;

                }
            }
            else
            {
                channelssrv = context.Channels.Where(c =>
                                                 (!filterStartDate || c.LastModified > dateTimeStart)
                                                 &&
                                                 (!filterEndDate || c.LastModified < dateTimeRangeEnd)
                                                 );
            }

            /*
            switch (_orderitems)
            {
                case OrderChannels.LastModified:
                    channelssrv = channelssrv.OrderByDescending(p => p.LastModified);
                    break;

                case OrderChannels.Name:
                    channelssrv = channelssrv.OrderBy(p => p.Name);
                    break;

                case OrderChannels.State:
                    channelssrv = channelssrv.OrderBy(p => p.State);
                    break;

                default:
                    break;
            }
            */

            IEnumerable<IChannel> channels = channelssrv.AsEnumerable(); // local query now

            if (filterstate)
            {
                channels = channels.Where(c => c.State == channelstate); // this query has to be locally. Not supported on the server
            }

            if ((!string.IsNullOrEmpty(_timefilter)) && _timefilter == FilterTime.First50Items)
            {
                channels = channels.Take(50);
            }

            channelquery = channels.Select(c =>
                       new ChannelEntry
                       {
                           Name = c.Name,
                           Id = c.Id,
                           Description = c.Description,
                           InputProtocol = string.Format("{0} ({1})", Program.ReturnNameForProtocol(c.Input.StreamingProtocol), c.Input.Endpoints.Count),
                           Encoding = ReturnChannelBitmap(c),
                           EncodingPreset = (c.EncodingType != ChannelEncodingType.None && c.Encoding != null) ? c.Encoding.SystemPreset : string.Empty,
                           InputUrl = c.Input.Endpoints.First().Url,
                           PreviewUrl = c.Preview != null ? c.Preview.Endpoints.First().Url : null,
                           State = c.State,
                           LastModified = c.LastModified.ToLocalTime()
                       });

            _MyObservChannels = new SortableBindingList<ChannelEntry>(channelquery.ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservChannels));
            _refreshedatleastonetime = true;
            this.BeginInvoke(new Action(() => this.FindForm().Cursor = Cursors.Default));
        }
    }

    public class DataGridViewLiveProgram : DataGridView
    {
        private List<string> idsList = new List<string>();
        private List<StatusInfo> ListStatus = new List<StatusInfo>();
        static SortableBindingList<ProgramEntry> _MyObservPrograms;

        static private int _itemssperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _statefilter = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.ProgramName, Text = "" };
        static private string _timefilter = FilterTime.LastWeek;
        static private TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        static BackgroundWorker WorkerRefreshChannels;
        public string _published = "Published";
        static Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        static private enumDisplayProgram _anyChannel = enumDisplayProgram.Selected;

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

        public enumDisplayProgram DisplayChannel
        {
            get
            {
                return _anyChannel;
            }
            set
            {
                _anyChannel = value;
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
        public SearchObject SearchInName
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
        public TimeRangeValue TimeFilterTimeRange
        {
            get
            {
                return _timefilterTimeRange;
            }
            set
            {
                _timefilterTimeRange = value;
            }
        }
        public int DisplayedCount
        {
            get
            {
                return _MyObservPrograms != null ? _MyObservPrograms.Count() : 0;
            }
        }



        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<ProgramEntry> programquery;
            _credentials = credentials;

            _context = context;
            programquery = from c in _context.Programs.Take(0)
                               //orderby c.LastModified descending
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


            SortableBindingList<ProgramEntry> MyObservProgramInPage = new SortableBindingList<ProgramEntry>(programquery.Take(0).ToList());
            this.DataSource = MyObservProgramInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
            this.Columns["ChannelId"].Visible = false;
            this.Columns[_published].DisplayIndex = this.ColumnCount - 3;
            this.Columns[_published].DefaultCellStyle.NullValue = null;
            this.Columns[_published].HeaderText = _published;
            this.Columns["LastModified"].Width = 130;
            this.Columns["LastModified"].HeaderText = "Last modified";
            this.Columns["Description"].Width = 150;
            this.Columns["ArchiveWindowLength"].Width = 130;
            this.Columns["ArchiveWindowLength"].HeaderText = "Archive window";
            this.Columns["ChannelName"].HeaderText = "Channel name";

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

            if (_MyObservPrograms!=null)
            {
                foreach (ProgramEntry CE in _MyObservPrograms) // let's search for index
                {
                    if (CE.Id == program.Id)
                    {
                        index = _MyObservPrograms.IndexOf(CE);
                        break;
                    }
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
                        _MyObservPrograms[index].LastModified = program.LastModified.ToLocalTime();
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
            if (idsList.Count == 0) return;

            Debug.WriteLine("RefreshPrograms : start");

            this.FindForm().Cursor = Cursors.WaitCursor;
            _context = context;

            IEnumerable<ProgramEntry> programquery;
            IQueryable<IProgram> programssrv = context.Programs;
            IEnumerable<IProgram> programs;

            if (_anyChannel == enumDisplayProgram.None)
            {
                programs = new List<IProgram>();
            }
            else
            {
                // DAYS
                bool filterStartDate = false;
                bool filterEndDate = false;

                DateTime dateTimeStart = DateTime.UtcNow;
                DateTime dateTimeRangeEnd = DateTime.UtcNow.AddDays(1);

                int days = FilterTime.ReturnNumberOfDays(_timefilter);

                if (days > 0)
                {
                    filterStartDate = true;
                    dateTimeStart = (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)));
                }
                else if (days == -1) // TimeRange
                {
                    filterStartDate = true;
                    filterEndDate = true;
                    dateTimeStart = _timefilterTimeRange.StartDate;
                    if (_timefilterTimeRange.EndDate != null) // there is an end time
                    {
                        dateTimeRangeEnd = (DateTime)_timefilterTimeRange.EndDate;
                    }
                }

                // STATE
                bool pFilterOnState = FilterState != "All";
                ProgramState myStateFilter = ProgramState.Running;
                if (pFilterOnState)
                {
                    myStateFilter = (ProgramState)Enum.Parse(typeof(ProgramState), _statefilter);
                }

                bool bListEmpty = (idsList.Count == 0);

                // search
                if (_searchinname != null && !string.IsNullOrEmpty(_searchinname.Text))
                {
                    bool Error = false;

                    switch (_searchinname.SearchType)
                    {
                        case SearchIn.ProgramName:
                            programssrv = context.Programs.Where(p =>
                                                    (p.Name.ToLower().Contains(_searchinname.Text.ToLower()))
                                                      &&
                                                     (!filterStartDate || p.LastModified > dateTimeStart)
                                                     &&
                                                     (!filterEndDate || p.LastModified < dateTimeRangeEnd)
                                                      );
                            break;

                        case SearchIn.ProgramId:
                            string programguid = _searchinname.Text;
                            if (programguid.StartsWith(Constants.ProgramIdPrefix))
                            {
                                programguid = programguid.Substring(Constants.ProgramIdPrefix.Length);
                            }
                            try
                            {
                                var g = new Guid(programguid);
                            }
                            catch
                            {
                                Error = true;
                                MessageBox.Show("Error with program Id. Is it a valid GUID or program Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (!Error)
                            {
                                programssrv = context.Programs.Where(p =>
                                                        (p.Id == Constants.ProgramIdPrefix + programguid)
                                                        // no need to filter the date or ID as user request a specific ID
                                                        );
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    programssrv = context.Programs.Where(p =>
                                                     (!filterStartDate || p.LastModified > dateTimeStart)
                                                     &&
                                                     (!filterEndDate || p.LastModified < dateTimeRangeEnd)
                                                    );

                    if (idsList.Count == 1 && _anyChannel == enumDisplayProgram.Selected)
                    {
                        programssrv = programssrv.Where(p => p.ChannelId == idsList[0]);
                    }
                    else if (idsList.Count > 1 && _anyChannel == enumDisplayProgram.Selected)
                    {
                        // let's build the query for all the IDs
                        // The IQueryable data to query.
                        IQueryable<IProgram> queryableData = programssrv.AsQueryable<IProgram>();

                        // Compose the expression tree that represents the parameter to the predicate.
                        ParameterExpression pe = Expression.Parameter(typeof(IProgram), "p");

                        List<Expression> exp = new List<Expression>();
                        foreach (var s in idsList)
                        {
                            // ***** Where(p => p.ChannelId == "nb:chid:UUID:29aae99e-66d9-4a54-8cf0-8f652fd0f0ff" || p.ChannelId == "nb:chid:UUID:....)) *****
                            // Create an expression tree that represents the expression 'p.ChannelId == "nb:chid:UUID:2....
                            Expression left = Expression.Property(pe, typeof(IProgram).GetProperty("ChannelId"));
                            Expression right = Expression.Constant(s);
                            exp.Add(Expression.Equal(left, right));
                        }
                        // Combine the expression trees to create an expression tree that represents the
                        Expression predicateBody = Expression.OrElse(exp[0], exp[1]);
                        for (int i = 2; i < idsList.Count; i++)
                        {
                            predicateBody = Expression.OrElse(predicateBody, exp[i]);
                        }

                        // Create an expression tree that represents the expression
                        MethodCallExpression whereCallExpression = Expression.Call(
                           typeof(Queryable),
                           "Where",
                           new Type[] { queryableData.ElementType },
                           queryableData.Expression,
                           Expression.Lambda<Func<IProgram, bool>>(predicateBody, new ParameterExpression[] { pe }));
                        // ***** End Where *****

                        // Create an executable query from the expression tree.
                        programssrv = queryableData.Provider.CreateQuery<IProgram>(whereCallExpression);
                    }
                }

                // let's get all the results locally

                IList<IProgram> aggregateListPrograms = new List<IProgram>();
                int skipSize = 0;
                int batchSize = 1000;
                int currentSkipSize = 0;
                while (true)
                {
                    // Enumerate through all jobs (1000 at a time)
                    var programsq = programssrv
                        .Skip(skipSize).Take(batchSize).ToList();

                    currentSkipSize += programsq.Count;

                    foreach (var j in programsq)
                    {
                        aggregateListPrograms.Add(j);
                    }

                    if (currentSkipSize == batchSize)
                    {
                        skipSize += batchSize;
                        currentSkipSize = 0;
                    }
                    else
                    {
                        break;
                    }
                }
                //programs = programssrv.AsEnumerable(); // local query now
                programs = aggregateListPrograms;


                if (pFilterOnState)
                {
                    programs = programs.Where(p => p.State.Equals(myStateFilter)); // this query has to be locally. Not supported on the server
                }

                if ((!string.IsNullOrEmpty(_timefilter)))
                {
                    if (_timefilter == FilterTime.First50Items)
                    {
                        programs = programs.Take(50);

                    }
                    else if (_timefilter == FilterTime.First1000Items)
                    {
                        programs = programs.Take(1000);
                    }
                }
            }

            programquery = programs.Select(p =>
                         new ProgramEntry
                         {
                             Name = p.Name,
                             Id = p.Id,
                             Description = p.Description,
                             ArchiveWindowLength = p.ArchiveWindowLength,
                             State = p.State,
                             LastModified = p.LastModified.ToLocalTime(),
                             ChannelName = p.Channel.Name,
                             ChannelId = p.Channel.Id,
                             Published = p.Asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).Count() > 0 ? Streaminglocatorimage : null,
                         });

            _MyObservPrograms = new SortableBindingList<ProgramEntry>(programquery.ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservPrograms));
            _refreshedatleastonetime = true;
            this.FindForm().Cursor = Cursors.Default;

            Debug.WriteLine("RefreshPrograms : end");
        }
    }


    public class ChannelInfo
    {
        public static async Task<IOperation> ChannelExecuteOperationAsync(Func<Task<IOperation>> fCall, IChannel channel, string strStatusSuccess, CloudMediaContext _context, Mainform mainform, DataGridViewLiveChannel dataGridViewChannelsV = null) //used for all except creation 
        {
            IOperation operation = null;
            if (channel != null)
            {
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
                        mainform.TextBoxLogWriteLine("Channel '{0}' : {1}.", channel.Name, strStatusSuccess);
                        IChannel channelR = _context.Channels.Where(c => c.Id == channel.Id).FirstOrDefault();
                        // we display a notification is taskbar for channel started or reset
                        if (channelR != null && (strStatusSuccess == "started" || strStatusSuccess == "reset"))
                        {
                            mainform.BeginInvoke(new Action(() =>
                            {
                                mainform.Notify("Channel " + strStatusSuccess, string.Format("{0}", channelR.Name), false);
                            }));
                        }
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
                    mainform.TextBoxLogWriteLine("Channel '{0}' : Error! {1}", channel.Name, Program.GetErrorMessage(ex), true);
                }
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
                    mainform.TextBoxLogWriteLine("Channel '{0}' : {1}.", channel.Name, strStatusSuccess);
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
                mainform.TextBoxLogWriteLine("Channel '{0}' : Error! {1}", channel.Name, Program.GetErrorMessage(ex), true);
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
                    mainform.TextBoxLogWriteLine("Channel '{0}' : {1}.", channel.Name, strStatusSuccess);
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
                mainform.TextBoxLogWriteLine("Channel '{0}' : Error! {1}", channel.Name, Program.GetErrorMessage(ex), true);
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
        public string EncodingPreset { get; set; }
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
                          .Where(o => (o.State == StreamingEndpointState.Running) && (StreamingEndpointInformation.CanDoDynPackaging(o)))
                          .OrderByDescending(o => o.CdnEnabled)
                        .Select(
                            o =>
                                template.BindByPosition(new Uri("https://" + o.HostName), l.ContentAccessComponent,
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
                         .Where(o => (o.State != StreamingEndpointState.Running) || (!StreamingEndpointInformation.CanDoDynPackaging(o)))
                       .Select(
                           o =>
                               template.BindByPosition(new Uri("https://" + o.HostName), l.ContentAccessComponent,
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


    public enum enumDisplayProgram
    {
        Selected = 0,
        Any,
        None
    }

    public static class AMSEXPlorerLiveProfile
    {
        public class LiveVideoProfile
        {
            public string Codec { get; set; }
            public int Bitrate { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string Profile { get; set; }
            public string OutputStreamName { get; set; }
        }

        public class LiveAudioProfile
        {
            public string Language { get; set; }
            public string Codec { get; set; }
            public int Bitrate { get; set; }
            public double SamplingRate { get; set; }
            public string Channels { get; set; }
        }



        public class LiveProfile
        {
            public string Name { get; set; }
            public ChannelEncodingType Type { get; set; }
            public List<LiveVideoProfile> Video { get; set; }
            public LiveAudioProfile Audio { get; set; }
        }

        public static readonly List<LiveProfile> Profiles = new List<LiveProfile>
        {
            new LiveProfile()
            {
                Type = ChannelEncodingType.Standard,
                Name ="Default720p",
                Video = new List<LiveVideoProfile>()
                {
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 3500, Width= 1280, Height= 720, Profile= "High", OutputStreamName= "Video_1280x720_3500kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 2200, Width= 960, Height= 540, Profile= "Main", OutputStreamName= "Video_960x540_2200kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 1350, Width= 704, Height= 396, Profile= "Main", OutputStreamName= "Video_704x396_1350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 850, Width= 512, Height= 288, Profile= "Main", OutputStreamName= "Video_512x288_850kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 550, Width= 384, Height= 216, Profile= "Main", OutputStreamName= "Video_384x216_550kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 350, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                    Codec= "AAC-LC", Bitrate= 64, SamplingRate= 44.1, Channels= "Stereo"
                    }
            },
             new LiveProfile()
            {
                Type = ChannelEncodingType.Premium,
                Name ="Default1080p",
                Video = new List<LiveVideoProfile>()
                {
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 6000, Width= 1920, Height= 1080, Profile= "High", OutputStreamName= "Video_1920x1080_6000kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 3500, Width= 1280, Height= 720, Profile= "High", OutputStreamName= "Video_1280x720_3500kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 2200, Width= 960, Height= 540, Profile= "Main", OutputStreamName= "Video_960x540_2200kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 1350, Width= 704, Height= 396, Profile= "Main", OutputStreamName= "Video_704x396_1350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 850, Width= 512, Height= 288, Profile= "Main", OutputStreamName= "Video_512x288_850kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 550, Width= 384, Height= 216, Profile= "Main", OutputStreamName= "Video_384x216_550kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 350, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_350kbps"},
                    new LiveVideoProfile(){Codec = "H.264", Bitrate= 200, Width= 340, Height= 192, Profile= "Baseline", OutputStreamName= "Video_340x192_200kbps"},
                        },
                Audio = new LiveAudioProfile()
                    {
                    Codec= "AAC-LC", Bitrate= 64, SamplingRate= 44.1, Channels= "Stereo"
                    }
            }
        };
    }


    [DataContract]
    internal class OAuth2TokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
