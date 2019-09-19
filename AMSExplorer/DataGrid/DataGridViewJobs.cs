﻿//----------------------------------------------------------------------------------------------
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
using Microsoft.Rest.Azure;
using Microsoft.Rest.Azure.OData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class DataGridViewJobs : DataGridView
    {
        private static BindingList<JobEntryV3> _MyObservJobV3 = new BindingList<JobEntryV3>();
        private static readonly Dictionary<string, CancellationTokenSource> _MyListJobsMonitored = new Dictionary<string, CancellationTokenSource>(); // List of jobds monitored. It contains the jobs ids. Used when a new job is discovered (created by another program) to activate the display of job progress

        private static int _jobsperpage = 50; //nb of items per page
        private static readonly int _pagecount = 1;
        private static readonly int _currentpage = 1;
        public bool _initialized = false;
        private static string _orderjobs = OrderJobs.CreatedDescending;
        private static string _filterjobsstate = "All";
        private static SearchObject _searchinname = new SearchObject { SearchType = SearchIn.JobName, Text = string.Empty };
        private static string _timefilter = FilterTime.LastWeek;
        private static TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private const int DefaultJobRefreshIntervalInMilliseconds = 2500;
        private static int JobRefreshIntervalInMilliseconds = DefaultJobRefreshIntervalInMilliseconds;
        private static AMSClientV3 _client;
        private List<string> _transformName = new List<string>();
        private bool _currentPageNumberIsMax;
        private int _currentPageNumber;
        private IPage<Job> firstpage;


        public bool CurrentPageIsMax => _currentPageNumberIsMax;

        public int JobssPerPage
        {
            get => _jobsperpage;
            set => _jobsperpage = value;
        }
        public int PageCount => _pagecount;
        public int CurrentPage => _currentpage;
        public string OrderJobsInGrid
        {
            get => _orderjobs;
            set => _orderjobs = value;

        }
        public string FilterJobsState
        {
            get => _filterjobsstate;
            set => _filterjobsstate = value;

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
        public int DisplayedCount => _MyObservJobV3.Count();

        public void Init(AMSClientV3 client)
        {
            // if (_transformName.Count == 0) return;  // no transform name set

            _client = client;

            List<JobEntryV3> jobs = new List<JobEntryV3>();

            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
            {
                Name = "Progress",
                DataPropertyName = "Progress",
                HeaderText = "Progress"
            };

            Columns.Add(col);

            /*

            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
            {
                Name = "Progress",
                DataPropertyName = "Progress",
                HeaderText = "Progress"
            };

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();

            this.Columns.Add(col);
            BindingList<JobEntry> MyObservJobInPage = new BindingList<JobEntry>(jobquery.Take(0).ToList());
            this.DataSource = MyObservJobInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayJobIDinGrid;
            this.Columns["Progress"].DisplayIndex = 5;
            this.Columns["Progress"].Width = 150;
            this.Columns["Tasks"].Width = 50;
            this.Columns["Priority"].Width = 50;
            this.Columns["State"].Width = 80;
            this.Columns["StartTime"].Width = 150;
            this.Columns["StartTime"].HeaderText = "Start time";
            this.Columns["EndTime"].Width = 150;
            this.Columns["EndTime"].HeaderText = "End time";
            this.Columns["Duration"].Width = 90;
            */

            BindingList<JobEntryV3> MyObservJobthisPageV3 = new BindingList<JobEntryV3>(jobs);
            IAsyncResult result = BeginInvoke(new Action(() => DataSource = MyObservJobthisPageV3));
            //this.DataSource = MyObservJobthisPageV3;

            Task myTask = Task.Factory.StartNew(() =>
            {
                result.AsyncWaitHandle.WaitOne();
                BeginInvoke(new Action(() =>
                {
                    Columns["TransformName"].Visible = false;
                    Columns["Progress"].DisplayIndex = 5;
                    Columns["Progress"].Width = 150;
                    Columns["Outputs"].Width = 80;
                    Columns["Priority"].Width = 50;
                    Columns["State"].Width = 80;

                }));
            });

            _initialized = true;
        }

        public List<JobExtension> ReturnSelectedJobs()
        {
            List<JobExtension> SelectedJobs = new List<JobExtension>();
            _client.RefreshTokenIfNeeded();

            foreach (DataGridViewRow Row in SelectedRows)
            {
                string tName = Row.Cells[Columns["TransformName"].Index].Value.ToString();
                // sometimes, the transform can be null (if just deleted)
                Job job = Task.Run(() =>
            _client.AMSclient.Jobs.GetAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, tName, Row.Cells[Columns["Name"].Index].Value.ToString())
            ).GetAwaiter().GetResult();

                if (job != null)
                {
                    SelectedJobs.Add(new JobExtension() { Job = job, TransformName = tName });
                }
            }
            SelectedJobs.Reverse();
            return SelectedJobs;
        }


        public List<string> TransformSourceNames
        {
            get => _transformName;
            set => _transformName = value;
        }


        public async Task RefreshjobsAsync(int pagetodisplay) // all jobs are refreshed
        {
            if ((!_initialized) || _transformName.Count == 0)
            {
                return;
            }

            Debug.WriteLine("Refresh Jobs Start");

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.WaitCursor));

            ///////////////////////
            // SORTING
            ///////////////////////
            ODataQuery<Job> odataQuery = new ODataQuery<Job>();

            switch (_orderjobs)
            {
                case OrderJobs.CreatedDescending:
                    odataQuery.OrderBy = "Properties/Created desc";
                    break;

                case OrderJobs.CreatedAscending:
                    odataQuery.OrderBy = "Properties/Created";
                    break;

                case OrderJobs.NameAscending:
                    odataQuery.OrderBy = "Name";
                    break;

                case OrderJobs.NameDescending:
                    odataQuery.OrderBy = "Name desc";
                    break;

                default:
                    odataQuery.OrderBy = "Properties/Created desc";
                    break;
            }



            ///////////////////////
            // Filter
            ///////////////////////

            switch (_filterjobsstate)
            {
                case "All":
                    break;

                default:
                    odataQuery.Filter = string.Format("Properties/state eq Microsoft.Media.JobState'{0}'", _filterjobsstate);
                    break;
            }



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
            if (filterStartDate)
            {
                if (odataQuery.Filter != null)
                {
                    odataQuery.Filter = odataQuery.Filter + " and ";
                }
                odataQuery.Filter = odataQuery.Filter + $"Properties/Created gt {dateTimeStart.ToString("o")}";
            }
            if (filterEndDate)
            {
                if (odataQuery.Filter != null)
                {
                    odataQuery.Filter = odataQuery.Filter + " and ";
                }
                odataQuery.Filter = odataQuery.Filter + $"Properties/Created lt {dateTimeRangeEnd.ToString("o")}";
            }


            // Paging
            await _client.RefreshTokenIfNeededAsync();

            IPage<Job> currentPage = null;
            string transform = _transformName.First();

            if (pagetodisplay == 1)
            {
                firstpage = await _client.AMSclient.Jobs.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, transform, odataQuery);
                currentPage = firstpage;
            }
            else
            {
                currentPage = firstpage;
                _currentPageNumber = 1;
                while (currentPage.NextPageLink != null && pagetodisplay > _currentPageNumber)
                {
                    _currentPageNumber++;
                    currentPage = await _client.AMSclient.Jobs.ListNextAsync(currentPage.NextPageLink);
                }
                if (currentPage.NextPageLink == null)
                {
                    _currentPageNumberIsMax = true; // we reached max
                }
            }


            IEnumerable<JobEntryV3> jobs = currentPage.Select(a => new JobEntryV3
            {
                Name = a.Name,
                Description = a.Description,
                LastModified = a.LastModified.ToLocalTime().ToString("G"),
                TransformName = transform,
                Outputs = a.Outputs.Count,
                Priority = a.Priority,
                State = a.State,
                Progress = ReturnProgressJob(a).progress
                // progress;  we don't want the progress bar to be displayed

            }
         );

            _MyObservJobV3 = new BindingList<JobEntryV3>(jobs.ToList());

            BeginInvoke(new Action(() => DataSource = _MyObservJobV3));

            Debug.WriteLine("RefreshJobs End");

            await RestoreJobProgressAsync(new List<string> { transform });

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
        }



        // Used to restore job progress. 2 cases: when app is launched or when a job has been created by an external program
        public async Task RestoreJobProgressAsync(List<string> transforms)  // when app is launched for example, we want to restore job progress updates
        {
            await _client.RefreshTokenIfNeededAsync();

            ODataQuery<Job> odataQuery = new ODataQuery<Job>
            {
                Filter = "Properties/State eq Microsoft.Media.JobState'Queued' or Properties/State eq Microsoft.Media.JobState'Scheduled' or Properties/State eq Microsoft.Media.JobState'Processing' "
            };

            List<JobExtension> ActiveAndVisibleJobs = new List<JobExtension>();
            foreach (string t in transforms)
            {
                IPage<Job> jobs = await _client.AMSclient.Jobs.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, t, odataQuery);
                ActiveAndVisibleJobs.AddRange(jobs.Select(j => new JobExtension() { Job = j, TransformName = t }));
            }

            // let's cancel monitor task of non visible jobs
            List<string> listToCancel = new List<string>();
            try
            {
                foreach (KeyValuePair<string, CancellationTokenSource> jobmonitored in _MyListJobsMonitored)
                {
                    if (ActiveAndVisibleJobs.Where(j => j.Job.Name == jobmonitored.Key).FirstOrDefault() == null)
                    {
                        jobmonitored.Value.Cancel();
                        listToCancel.Add(jobmonitored.Key);
                    }
                }
                listToCancel.ForEach(j => _MyListJobsMonitored.Remove(j));

                // let's adjust the JobRefreshIntervalInMilliseconds based on the number of jobs to monitor
                // 2500 ms if 5 jobs or less, 500ms*nbjobs otherwise
                JobRefreshIntervalInMilliseconds = Math.Max(DefaultJobRefreshIntervalInMilliseconds, Convert.ToInt32(DefaultJobRefreshIntervalInMilliseconds * ActiveAndVisibleJobs.Count() / 5d));

                // let's monitor job that are not yet monitored
                foreach (JobExtension job in ActiveAndVisibleJobs)
                {
                    if (!_MyListJobsMonitored.ContainsKey(job.Job.Name))
                    {
                        DoJobProgress(job); // token will be added to dictionnary in this function
                    }
                }
            }
            catch
            {

            }
        }

        public void DoJobProgress(JobExtension job)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            _MyListJobsMonitored.Add(job.Job.Name, tokenSource); // to track the task and be able to cancel it later

            Debug.WriteLine("launch job monitor : " + job.Job.Name);

            _client.RefreshTokenIfNeeded();

            Task.Run(() =>
            {
                try
                {
                    Job myJob = null;

                    do
                    {
                        myJob = Task.Run(() =>
            _client.AMSclient.Jobs.GetAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, job.TransformName, job.Job.Name)
            ).GetAwaiter().GetResult();

                        if (token.IsCancellationRequested == true)
                        {
                            return;
                        }

                        int index = -1;
                        foreach (JobEntryV3 je in _MyObservJobV3) // let's search for index
                        {
                            if (je.Name == myJob.Name)
                            {
                                index = _MyObservJobV3.IndexOf(je);
                                break;
                            }
                        }

                        if (index >= 0) // we found it
                        { // we update the observation collection
                            JobProgressInfo progress = ReturnProgressJob(myJob);

                            _MyObservJobV3[index].Progress = progress.progress;
                            _MyObservJobV3[index].Priority = myJob.Priority;
                            //_MyObservJobV3[index].StartTime = myJob...StartTime.HasValue ? ((DateTime)myJob.StartTime).ToLocalTime().ToString("G") : null;
                            //_MyObservJobV3[index].EndTime = myJob.EndTime.HasValue ? ((DateTime)myJob.EndTime).ToLocalTime().ToString("G") : null;

                            _MyObservJobV3[index].State = myJob.State;


                            /*
                            // let's calculate the estipated time
                            string ETAstr = "", Durationstr = "";
                            if (progress > 3)
                            {
                                DateTime startlocaltime = ((DateTime)myJob.StartTime).ToLocalTime();
                                TimeSpan interval = (TimeSpan)(DateTime.Now - startlocaltime);
                                DateTime ETA = DateTime.Now.AddSeconds((100d / progress - 1d) * interval.TotalSeconds);
                                TimeSpan estimatedduration = (TimeSpan)(ETA - startlocaltime);

                                ETAstr = "Estimated: " + ETA.ToString("G");
                                Durationstr = "Estimated: " + estimatedduration.ToString(@"d\.hh\:mm\:ss");
                                _MyObservJobV3[index].EndTime = ETA.ToString(@"G") + " ?";
                                _MyObservJobV3[index].Duration = myJob.EndTime.HasValue ?
                                             ((TimeSpan)((DateTime)myJob.EndTime - (DateTime)myJob.StartTime)).ToString(@"d\.hh\:mm\:ss")
                                             : estimatedduration.ToString(@"d\.hh\:mm\:ss") + " ?";
                            }
                            */

                            int indexdisplayed = -1;
                            foreach (JobEntryV3 je in _MyObservJobV3) // let's search for index in the page
                            {
                                if (je.Name == myJob.Name)
                                {
                                    indexdisplayed = _MyObservJobV3.IndexOf(je);
                                    try
                                    {
                                        BeginInvoke(new Action(() =>
                                        {
                                            Rows[indexdisplayed].Cells[Columns["Progress"].Index].ToolTipText = progress.sb.ToString(); // mouse hover info
                                            if (progress.progress != 0)
                                            {
                                                //  this.Rows[indexdisplayed].Cells[this.Columns["EndTime"].Index].ToolTipText = ETAstr;// mouse hover info
                                                //      this.Rows[indexdisplayed].Cells[this.Columns["Duration"].Index].ToolTipText = Durationstr;// mouse hover info
                                            }
                                            Refresh();
                                        }));
                                    }
                                    catch
                                    {

                                    }

                                    break;
                                }
                            }
                        }

                        if (myJob != null && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Finished && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Error && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Canceled)
                        {
                            Debug.WriteLine("wait for status : " + myJob.Name);
                            Task.Delay(JobRefreshIntervalInMilliseconds).Wait();
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Finished
                    && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Error
                    && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Canceled);

                    // job finished
                    _client.RefreshTokenIfNeeded();
                    myJob = Task.Run(() =>
           _client.AMSclient.Jobs.GetAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, job.TransformName, job.Job.Name)
           ).GetAwaiter().GetResult();

                    int index2 = -1;
                    foreach (JobEntryV3 je in _MyObservJobV3) // let's search for index
                    {
                        if (je.Name == myJob.Name)
                        {
                            index2 = _MyObservJobV3.IndexOf(je);
                            break;
                        }
                    }
                    if (index2 >= 0) // we found it
                    { // we update the observation collection
                        StringBuilder sb2 = new StringBuilder(); // display percentage for each task for mouse hover (tooltiptext)

                        double progress2 = 0;
                        for (int i = 0; i < myJob.Outputs.Count; i++)
                        {
                            JobOutput output = myJob.Outputs[i];

                            if (output.State == Microsoft.Azure.Management.Media.Models.JobState.Processing)
                            {
                                progress2 += output.Progress;

                                sb2.AppendLine(string.Format("{0} % ({1})", Convert.ToInt32(output.Progress).ToString(), output.Label));
                            }
                        }
                        if (myJob.Outputs.Count > 0)
                        {
                            progress2 = progress2 / myJob.Outputs.Count;
                        }

                        _MyObservJobV3[index2].Progress = 101d; // progress;  we don't want the progress bar to be displayed
                        _MyObservJobV3[index2].Priority = myJob.Priority;
                        _MyObservJobV3[index2].State = myJob.State;

                        if (_MyListJobsMonitored.ContainsKey(myJob.Name)) // we want to display only one time
                        {
                            _MyListJobsMonitored.Remove(myJob.Name); // let's remove from the list of monitored jobs
                            Mainform myform = (Mainform)FindForm();


                            // string status = Enum.GetName(typeof(Microsoft.Azure.Management.Media.Models.JobState), myJob.State).ToLower();
                            string status = myJob.State.ToString();

                            myform.BeginInvoke(new Action(() =>
                            {
                                myform.Notify(string.Format("Job {0}", status), string.Format("Job {0}", _MyObservJobV3[index2].Name), myJob.State == Microsoft.Azure.Management.Media.Models.JobState.Error);
                                myform.TextBoxLogWriteLine(string.Format("Job '{0}' : {1}.", _MyObservJobV3[index2].Name, status), myJob.State == Microsoft.Azure.Management.Media.Models.JobState.Error);
                                if (myJob.State == Microsoft.Azure.Management.Media.Models.JobState.Error)
                                {
                                    foreach (JobOutput output in myJob.Outputs)
                                    {

                                        if (output.Error != null && output.Error.Details != null)
                                        {
                                            for (int i = 0; i < output.Error.Details.Count(); i++)
                                            {
                                                myform.TextBoxLogWriteLine(string.Format("Output '{0}', Error : {1}", output.Label, output.Error + " : " + output.Error.Message), true);
                                            }
                                        }
                                    }
                                }
                                myform.DoRefreshGridAssetV(false);
                            }));

                            BeginInvoke(new Action(() =>
                            {
                                Refresh();
                            }));

                        }
                    }


                }
                catch (Exception ex)
                {
                    //MessageBox.Show(Program.GetErrorMessage(e), "Job Monitoring Error");
                    Debug.WriteLine("error job monitor : " + Program.GetErrorMessage(ex));
                }
            }, token);
        }

        private static JobProgressInfo ReturnProgressJob(Job myJob)
        {
            StringBuilder sb = new StringBuilder();
            double progress = 0;
            for (int i = 0; i < myJob.Outputs.Count; i++)
            {
                JobOutput output = myJob.Outputs[i];

                if (output.State == Microsoft.Azure.Management.Media.Models.JobState.Processing)
                {
                    progress += output.Progress;

                    sb.AppendLine(string.Format("{0} % ({1})", Convert.ToInt32(output.Progress).ToString(), output.Label));
                }
            }
            if (myJob.Outputs.Count > 0)
            {
                progress = progress / myJob.Outputs.Count;
            }

            if (myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Processing)
            {
                progress = 101d;
            }

            return new JobProgressInfo() { progress = progress, sb = sb };
        }

    }


    public class JobProgressInfo
    {
        public StringBuilder sb;
        public double progress;
    }
}
