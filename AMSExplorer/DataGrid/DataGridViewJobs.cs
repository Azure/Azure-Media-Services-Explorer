//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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
using Microsoft.Rest.Azure.OData;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class DataGridViewJobs : DataGridView
    {

        public int JobssPerPage
        {
            get
            {
                return _jobsperpage;
            }
            set
            {
                _jobsperpage = value;
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
                return _MyObservJobV3.Count();
            }

        }

        static BindingList<JobEntryV3> _MyObservJobV3;
        static Dictionary<string, CancellationTokenSource> _MyListJobsMonitored = new Dictionary<string, CancellationTokenSource>(); // List of jobds monitored. It contains the jobs ids. Used when a new job is discovered (created by another program) to activate the display of job progress

        static private int _jobsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        public bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderjobs = OrderJobs.LastModifiedDescending;
        static string _filterjobsstate = "All";
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.JobName, Text = "" };
        static private string _timefilter = FilterTime.LastWeek;
        static private TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private const int DefaultJobRefreshIntervalInMilliseconds = 2500;
        static int JobRefreshIntervalInMilliseconds = DefaultJobRefreshIntervalInMilliseconds;

        static AzureMediaServicesClient _client;
        private string _resourceName;
        private string _accountName;
        private List<string> _transformName = new List<string>();
        private List<JobEntry> _MyObservJob;
        private List<JobEntry> _MyObservAssethisPage;
        private IEnumerable<IJob> jobs;

        public void Init(AzureMediaServicesClient client, string resourceName, string accountName)
        {
            if (_transformName.Count == 0) return;  // no transform name set

            _client = client;
            _resourceName = resourceName;
            _accountName = accountName;

            List<JobEntryV3> jobs = new List<JobEntryV3>();
            jobs.Add(new JobEntryV3());
            /*

            foreach (var t in _transformName)
            {
                jobs.AddRange(_client.Jobs.List(_resourceName, _accountName, t).Select(a => new JobEntryV3
                {
                    Name = a.Name,
                    Description = a.Description,
                    LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G"),
                    TransformName = t,
                    Outputs = a.Outputs.Count
                }
         ));
            }
            */


            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
            {
                Name = "Progress",
                DataPropertyName = "Progress",
                HeaderText = "Progress"
            };

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
            this.Columns.Add(col);


            /*
            jobquery = from j in _context.Jobs.Take(0)
                       orderby j.LastModified descending
                       select new JobEntry
                       {
                           Name = j.Name,
                           Id = j.Id,
                           Tasks = j.Tasks.Count,
                           Priority = j.Priority,
                           State = j.State,
                           StartTime = j.StartTime.HasValue ? ((DateTime)j.StartTime).ToLocalTime().ToString("G") : null,
                           EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString("G") : null,
                           Duration = (j.StartTime.HasValue && j.EndTime.HasValue) ? ((DateTime)j.EndTime).Subtract((DateTime)j.StartTime).ToString(@"d\.hh\:mm\:ss") : string.Empty,
                           Progress = (j.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Scheduled || j.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Processing || j.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Queued) ? j.GetOverallProgress() : 101d
                       };
                       */

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
            // this.BeginInvoke(new Action(() => this.DataSource = MyObservJobthisPageV3));
            this.DataSource = MyObservJobthisPageV3;

            this.Columns["Id"].Visible = false;
            this.Columns["TransformName"].Visible = false;

            this.Columns["Progress"].DisplayIndex = 5;
            this.Columns["Progress"].Width = 150;
            this.Columns["Outputs"].Width = 80;
            this.Columns["Priority"].Width = 50;
            this.Columns["State"].Width = 80;


            _initialized = true;
        }

        public List<JobExtension> ReturnSelectedJobs()
        {
            var SelectedJobs = new List<JobExtension>();
            foreach (DataGridViewRow Row in this.SelectedRows)
            {
                string tName = Row.Cells[this.Columns["TransformName"].Index].Value.ToString();
                // sometimes, the transform can be null (if just deleted)
                var job = _client.Jobs.Get(_resourceName, _accountName, tName, Row.Cells[this.Columns["Name"].Index].Value.ToString());
                if (job != null)
                {
                    SelectedJobs.Add(new JobExtension() { Job = job, TransformName = tName });
                }
            }
            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                this.DataSource = new BindingList<JobEntry>(_MyObservJob.Skip(_jobsperpage * (page - 1)).Take(_jobsperpage).ToList());
            }
        }

        public List<string> TransformSourceNames
        {
            get
            {
                return _transformName;
            }
            set
            {
                _transformName = value;
            }
        }




        public void Refreshjobs(int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;

            Debug.WriteLine("Refresh Jobs Start");

            this.FindForm().Cursor = Cursors.WaitCursor;

            List<JobEntryV3> jobs = new List<JobEntryV3>();

            foreach (var t in _transformName)
            {
                jobs.AddRange(_client.Jobs.List(_resourceName, _accountName, t).Select(a => new JobEntryV3
                {
                    Name = a.Name,
                    Description = a.Description,
                    LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G"),
                    TransformName = t,
                    Outputs = a.Outputs.Count,
                    Priority = a.Priority,
                    State = a.State
                }
         ));
            }

            _MyObservJobV3 = new BindingList<JobEntryV3>(jobs);

            this.BeginInvoke(new Action(() => this.DataSource = _MyObservJobV3));
            _refreshedatleastonetime = true;

            Debug.WriteLine("RefreshJobs End");

            this.FindForm().Cursor = Cursors.Default;
        }



        // Used to restore job progress. 2 cases: when app is launched or when a job has been created by an external program
        public void RestoreJobProgress()  // when app is launched for example, we want to restore job progress updates
        {
            Task.Run(() =>
            {
                // IEnumerable<IJob> ActiveAndVisibleJobs = jobs.Where(j => (j.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Queued) || (j.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Scheduled) || (j.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Processing));

                var transforms = _client.Transforms.List(_resourceName, _accountName);

                var odataQuery = new ODataQuery<Job>();


                odataQuery.Filter = "Properties/State eq Microsoft.Media.JobState'Queued' or Properties/State eq Microsoft.Media.JobState'Scheduled' or Properties/State eq Microsoft.Media.JobState'Processing' ";

                List<JobExtension> ActiveAndVisibleJobs = new List<JobExtension>();
                foreach (var t in transforms)
                {
                    ActiveAndVisibleJobs.AddRange(_client.Jobs.List(_resourceName, _accountName, t.Name, odataQuery).Select(j => new JobExtension() { Job = j, TransformName = t.Name }));
                }

                /*
            foreach (var t in transforms)
            {
                jobs.AddRange(_client.Jobs.List(_resourceName, _accountName, t).Select(a => new JobEntryV3
                {
                */

                // let's cancel monitor task of non visible jobs
                List<string> listToCancel = new List<string>();
                try
                {
                    foreach (var jobmonitored in _MyListJobsMonitored)
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
                    foreach (var job in ActiveAndVisibleJobs)
                    {
                        if (!_MyListJobsMonitored.ContainsKey(job.Job.Name))
                        {
                            this.DoJobProgress(job); // token will be added to dictionnary in this function
                        }
                    }
                }
                catch
                {

                }
            });
        }

        public void DoJobProgress(JobExtension job)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            const int SleepIntervalMs = 60 * 1000;

            _MyListJobsMonitored.Add(job.Job.Name, tokenSource); // to track the task and be able to cancel it later

            Task.Run(() =>
            {
                try
                {
                    Job myJob = null;

                    do
                    {
                        myJob = _client.Jobs.Get(_resourceName, _accountName, job.TransformName, job.Job.Name);

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

                        StringBuilder sb = new StringBuilder(); // display percentage for each task for mouse hover (tooltiptext)

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
                        if (myJob.Outputs.Count > 0) progress = progress / myJob.Outputs.Count;

                        _MyObservJobV3[index].Progress = progress;
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
                                    this.BeginInvoke(new Action(() =>
                                    {
                                        this.Rows[indexdisplayed].Cells[this.Columns["Progress"].Index].ToolTipText = sb.ToString(); // mouse hover info
                                        if (progress != 0)
                                        {
                                            //  this.Rows[indexdisplayed].Cells[this.Columns["EndTime"].Index].ToolTipText = ETAstr;// mouse hover info
                                            //      this.Rows[indexdisplayed].Cells[this.Columns["Duration"].Index].ToolTipText = Durationstr;// mouse hover info
                                        }
                                        this.Refresh();
                                    }));
                                }
                                catch
                                {

                                }

                                break;
                            }
                        }



                        if (myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Finished && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Error && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Canceled)
                        {
                            Task.Delay(SleepIntervalMs);
                        }
                    }
                    while (myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Finished
                    && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Error
                    && myJob.State != Microsoft.Azure.Management.Media.Models.JobState.Canceled);



                    // job finished
                    myJob = _client.Jobs.Get(_resourceName, _accountName, job.TransformName, job.Job.Name);

                    int index2 = -1;
                    foreach (JobEntryV3 je in _MyObservJobV3) // let's search for index
                    {
                        if (je.Name == myJob.Name)
                        {
                            index2 = _MyObservJobV3.IndexOf(je);
                            break;
                        }
                    }

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
                    if (myJob.Outputs.Count > 0) progress2 = progress2 / myJob.Outputs.Count;

                    _MyObservJobV3[index2].Progress = progress2;
                    _MyObservJobV3[index2].Priority = myJob.Priority;
                    _MyObservJobV3[index2].State = myJob.State;

                    if (_MyListJobsMonitored.ContainsKey(myJob.Name)) // we want to display only one time
                    {
                        _MyListJobsMonitored.Remove(myJob.Name); // let's remove from the list of monitored jobs
                        Mainform myform = (Mainform)this.FindForm();


                        string status = Enum.GetName(typeof(Microsoft.Azure.Management.Media.Models.JobState), myJob.State).ToLower();

                        myform.BeginInvoke(new Action(() =>
                        {

                            myform.Notify(string.Format("Job {0}", status), string.Format("Job {0}", _MyObservJob[index2].Name), myJob.State == Microsoft.Azure.Management.Media.Models.JobState.Error);
                            myform.TextBoxLogWriteLine(string.Format("Job '{0}' : {1}.", _MyObservJob[index2].Name, status), myJob.State == Microsoft.Azure.Management.Media.Models.JobState.Error);
                            if (myJob.State == Microsoft.Azure.Management.Media.Models.JobState.Error)
                            {
                                foreach (var output in myJob.Outputs)
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
                            //myform.DoRefreshGridAssetV(false);
                        }));

                        this.BeginInvoke(new Action(() =>
                        {
                            this.Refresh();
                        }));

                    }
                }
                catch
                {
                    //MessageBox.Show(Program.GetErrorMessage(e), "Job Monitoring Error");
                }
            }, token);

        }


        public void DoJobProgress(IJob job, bool CopySubtitlesToSourceAsset = false)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            _MyListJobsMonitored.Add(job.Id, tokenSource); // to track the task and be able to cancel it later

            Task.Run(() =>
            {
                try
                {
                    job = job.StartExecutionProgressTask(JobRefreshIntervalInMilliseconds,
                       j =>
                       {
                           // Was cancellation already requested? 
                           if (token.IsCancellationRequested == true)
                           {
                               return;
                           }

                           // refesh context and job
                           //_context = Program.ConnectAndGetNewContext(_credentials); // needed to get overallprogress
                           /// NET TO RESTORE CONTEXT
                           IJob JobRefreshed = GetJob(j.Id);
                           JobRefreshed.Refresh();
                           int index = -1;

                           foreach (JobEntry je in _MyObservJob) // let's search for index
                           {
                               if (je.Id == JobRefreshed.Id)
                               {
                                   index = _MyObservJob.IndexOf(je);
                                   break;
                               }
                           }


                           if (index >= 0) // we found it
                           { // we update the observation collection

                               if (JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Scheduled || JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Processing || JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Queued || JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Canceling) // in progress
                               {
                                   double progress = JobRefreshed.GetOverallProgress();
                                   _MyObservJob[index].Progress = progress;
                                   _MyObservJob[index].Priority = JobRefreshed.Priority;
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? ((DateTime)JobRefreshed.StartTime).ToLocalTime().ToString("G") : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString("G") : null;

                                   _MyObservJob[index].State = JobRefreshed.State;
                                   Debug.WriteLine(index.ToString() + JobRefreshed.State);

                                   StringBuilder sb = new StringBuilder(); // display percentage for each task for mouse hover (tooltiptext)
                                   foreach (ITask task in JobRefreshed.Tasks) sb.AppendLine(string.Format("{0} % ({1})", Convert.ToInt32(task.Progress).ToString(), task.Name));

                                   // let's calculate the estipated time
                                   string ETAstr = "", Durationstr = "";
                                   if (progress > 3)
                                   {
                                       DateTime startlocaltime = ((DateTime)JobRefreshed.StartTime).ToLocalTime();
                                       TimeSpan interval = (TimeSpan)(DateTime.Now - startlocaltime);
                                       DateTime ETA = DateTime.Now.AddSeconds((100d / progress - 1d) * interval.TotalSeconds);
                                       TimeSpan estimatedduration = (TimeSpan)(ETA - startlocaltime);

                                       ETAstr = "Estimated: " + ETA.ToString("G");
                                       Durationstr = "Estimated: " + estimatedduration.ToString(@"d\.hh\:mm\:ss");
                                       _MyObservJob[index].EndTime = ETA.ToString(@"G") + " ?";
                                       _MyObservJob[index].Duration = JobRefreshed.EndTime.HasValue ?
                                                    ((TimeSpan)((DateTime)JobRefreshed.EndTime - (DateTime)JobRefreshed.StartTime)).ToString(@"d\.hh\:mm\:ss")
                                                    : estimatedduration.ToString(@"d\.hh\:mm\:ss") + " ?";
                                   }

                                   int indexdisplayed = -1;
                                   foreach (JobEntry je in _MyObservAssethisPage) // let's search for index in the page
                                   {
                                       if (je.Id == JobRefreshed.Id)
                                       {
                                           indexdisplayed = _MyObservAssethisPage.IndexOf(je);
                                           try
                                           {
                                               this.BeginInvoke(new Action(() =>
                                               {
                                                   this.Rows[indexdisplayed].Cells[this.Columns["Progress"].Index].ToolTipText = sb.ToString(); // mouse hover info
                                                   if (progress != 0)
                                                   {
                                                       this.Rows[indexdisplayed].Cells[this.Columns["EndTime"].Index].ToolTipText = ETAstr;// mouse hover info
                                                       this.Rows[indexdisplayed].Cells[this.Columns["Duration"].Index].ToolTipText = Durationstr;// mouse hover info
                                                   }
                                                   this.Refresh();
                                               }));
                                           }
                                           catch
                                           {

                                           }

                                           break;
                                       }
                                   }
                               }
                               else // no progress anymore (cancelled, finished or failed)
                               {
                                   double progress = JobRefreshed.GetOverallProgress();
                                   _MyObservJob[index].Duration = JobRefreshed.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - JobRefreshed.StartTime)).ToString(@"d\.hh\:mm\:ss") : null;
                                   _MyObservJob[index].Progress = 101d;// progress;  we don't want the progress bar to be displayed
                                   _MyObservJob[index].Priority = JobRefreshed.Priority;
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? ((DateTime)JobRefreshed.StartTime).ToLocalTime().ToString("G") : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString("G") : null;
                                   _MyObservJob[index].State = JobRefreshed.State;

                                   if (_MyListJobsMonitored.ContainsKey(JobRefreshed.Id)) // we want to display only one time
                                   {
                                       _MyListJobsMonitored.Remove(JobRefreshed.Id); // let's remove from the list of monitored jobs
                                       Mainform myform = (Mainform)this.FindForm();

                                       // For indexer, there is an option to copy subtitles files to input asset
                                       if (CopySubtitlesToSourceAsset && JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Finished && JobRefreshed.Tasks.Count == 1)
                                       {
                                           var subtitlesFiles = JobRefreshed.Tasks.FirstOrDefault().OutputAssets.FirstOrDefault().AssetFiles.ToList().Where(f => f.Name.EndsWith(".vtt", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".ttml", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".smi", StringComparison.OrdinalIgnoreCase));
                                           var inputAsset = JobRefreshed.Tasks.FirstOrDefault().InputAssets.FirstOrDefault();
                                           foreach (var file in subtitlesFiles)
                                           {
                                               string filePath = Path.Combine(System.IO.Path.GetTempPath(), file.Name);

                                               if (File.Exists(filePath))
                                               {
                                                   File.Delete(filePath);
                                               }

                                               try
                                               {
                                                   file.Download(filePath);
                                                   IAssetFile InputAssetFile = inputAsset.AssetFiles.Create(file.Name);
                                                   InputAssetFile.Upload(filePath);
                                               }
                                               catch
                                               {
                                               }

                                               if (File.Exists(filePath))
                                               {
                                                   File.Delete(filePath);
                                               }
                                           }
                                           myform.BeginInvoke(new Action(() =>
                                           {
                                               myform.DoPurgeAssetInfoFromCache(inputAsset);
                                           }));
                                       }

                                       string status = Enum.GetName(typeof(Microsoft.WindowsAzure.MediaServices.Client.JobState), JobRefreshed.State).ToLower();

                                       myform.BeginInvoke(new Action(() =>
                                       {

                                           myform.Notify(string.Format("Job {0}", status), string.Format("Job {0}", _MyObservJob[index].Name), JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Error);
                                           myform.TextBoxLogWriteLine(string.Format("Job '{0}' : {1}.", _MyObservJob[index].Name, status), JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Error);
                                           if (JobRefreshed.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Error)
                                           {
                                               foreach (var task in JobRefreshed.Tasks)
                                               {
                                                   foreach (var error in task.ErrorDetails)
                                                   {
                                                       myform.TextBoxLogWriteLine(string.Format("Task '{0}', Error : {1}", task.Name, error.Code + " : " + error.Message), true);
                                                   }
                                               }
                                           }
                                           myform.DoRefreshGridAssetV(false);
                                       }));

                                       this.BeginInvoke(new Action(() =>
                                       {
                                           this.Refresh();
                                       }));

                                   }
                               }
                           }
                       },
                       token).Result;

                }
                catch
                {
                    //MessageBox.Show(Program.GetErrorMessage(e), "Job Monitoring Error");
                }
            }, token);

        }

        private IJob GetJob(string id)
        {
            throw new NotImplementedException();
        }
    }
}
