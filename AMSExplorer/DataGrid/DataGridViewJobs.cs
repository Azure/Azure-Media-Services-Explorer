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
        private static BindingList<JobEntryV3> _MyObservJobV3 = new();
        private static readonly Dictionary<string, CancellationTokenSource> _MyListJobsMonitored = new(); // List of jobs monitored. It contains the jobs ids. Used when a new job is discovered (created by another program) to activate the display of job progress

        private static int _jobsperpage = 50; //nb of items per page
        private static readonly int _pagecount = 1;
        private static readonly int _currentpage = 1;
        public bool _initialized = false;
        private static string _orderjobs = OrderJobs.CreatedDescending;
        private static string _filterjobsstate = "All";
        private static SearchObject _searchinname = new() { SearchType = SearchIn.JobName, Text = string.Empty };
        private static string _timefilter = FilterTime.LastWeek;
        private static TimeRangeValue _timefilterTimeRange = new(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private const int DefaultJobRefreshIntervalInMilliseconds = 2500;
        private static int JobRefreshIntervalInMilliseconds = DefaultJobRefreshIntervalInMilliseconds;
        private List<string> _transformName = new();
        private bool _currentPageNumberIsMax;
        private int _currentPageNumber;

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
        public int DisplayedCount => _MyObservJobV3.Count;

        public void Init(AMSClientV3 client)
        {
            // if (_transformName.Count == 0) return;  // no transform name set

            List<JobEntryV3> jobs = new();

            DataGridViewProgressBarColumn col = new()
            {
                Name = "Progress",
                DataPropertyName = "Progress",
                HeaderText = "Progress"
            };

            Columns.Add(col);


            BindingList<JobEntryV3> MyObservJobthisPageV3 = new(jobs);
            IAsyncResult result = BeginInvoke(new Action(() => DataSource = MyObservJobthisPageV3));
            //this.DataSource = MyObservJobthisPageV3;

            Task myTask = Task.Factory.StartNew(() =>
            {
                result.AsyncWaitHandle.WaitOne();
                BeginInvoke(new Action(() =>
                {
                    Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    Columns["Name"].Width = 200;
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

        public List<JobExtension> ReturnSelectedJobs(AMSClientV3 amsClient)
        {
            List<JobExtension> SelectedJobs = new();

            foreach (DataGridViewRow Row in SelectedRows)
            {
                string tName = Row.Cells[Columns["TransformName"].Index].Value.ToString();
                // sometimes, the transform can be null (if just deleted)
                MediaJobResource job;
                try
                {
                    job = Task.Run(() =>
                        amsClient.GetJobAsync(tName, Row.Cells[Columns["Name"].Index].Value.ToString())
                        ).GetAwaiter().GetResult();
                    SelectedJobs.Add(new JobExtension() { Job = job, TransformName = tName });
                }
                catch
                {

                }
            }
            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        public List<string> GetTransformSourceNames()
        {
            return _transformName;

        }

        public void SetTransformSourceNames(List<string> list)
        {
            _transformName = list;
            return;
        }


        public async Task RefreshjobsAsync(int pagetodisplay, AMSClientV3 amsClient) // all jobs are refreshed
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
            ODataQuery<MediaJobResource> odataQuery = new();

            odataQuery.OrderBy = _orderjobs switch
            {
                OrderJobs.CreatedDescending => "Properties/Created desc",
                OrderJobs.CreatedAscending => "Properties/Created",
                OrderJobs.NameAscending => "Name",
                OrderJobs.NameDescending => "Name desc",
                _ => "Properties/Created desc",
            };



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
                    odataQuery.Filter += " and ";
                }
                odataQuery.Filter += $"Properties/Created gt {dateTimeStart:o}";
            }
            if (filterEndDate)
            {
                if (odataQuery.Filter != null)
                {
                    odataQuery.Filter += " and ";
                }
                odataQuery.Filter += $"Properties/Created lt {dateTimeRangeEnd:o}";
            }


            // Paging
            string transformName = _transformName.First();
            MediaTransformResource transformResource;
            try
            {
                transformResource = (await amsClient.AMSclient.GetMediaTransformAsync(transformName)).Value;
            }
            catch (Exception)
            {
                // transform no there anymore
                return;
            }


            IReadOnlyList<MediaJobResource> currentPage = null;

            var jobsQuery = transformResource.GetMediaJobs().GetAllAsync(filter: odataQuery.Filter, orderby: odataQuery.OrderBy);

            if (pagetodisplay == 1)
            {
                //firstpage = await amsClient.AMSclient.Jobs.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, transform, odataQuery);
                currentPage = (await jobsQuery.AsPages(null).FirstAsync()).Values;
            }
            else
            {
                string continuationToken = null;

                _currentPageNumber = 1;
                do
                {
                    _currentPageNumber++;
                    await foreach (var item in jobsQuery.AsPages(continuationToken))
                    {
                        continuationToken = item.ContinuationToken;
                        currentPage = item.Values;
                    }
                }
                while (continuationToken != null && pagetodisplay > _currentPageNumber);

                if (continuationToken == null)
                {
                    _currentPageNumberIsMax = true; // we reached max
                }
            }


            IEnumerable<JobEntryV3> jobs = currentPage.Select(job => new JobEntryV3
            {
                Name = job.Data.Name,
                Description = job.Data.Description,
                LastModified = job.Data.LastModifiedOn?.DateTime.ToLocalTime().ToString("G"),
                TransformName = transformName,
                Outputs = job.Data.Outputs.Count,
                Priority = job.Data.Priority,
                State = job.Data.State,
                Progress = ReturnProgressJob(job.Data).progress,
                StartOn = ReportLocalTime(job.Data.StartOn),
                EndOn = ReportLocalTime(job.Data.EndOn),
                Duration = ReportDuration(job.Data)
                // progress;  we don't want the progress bar to be displayed
            }
         );

            _MyObservJobV3 = new BindingList<JobEntryV3>(jobs.ToList());

            BeginInvoke(new Action(() => DataSource = _MyObservJobV3));

            Debug.WriteLine("RefreshJobs End");

            await RestoreJobProgressAsync(new List<string> { transformName }, amsClient);

            BeginInvoke(new Action(() => FindForm().Cursor = Cursors.Default));
        }



        // Used to restore job progress. 2 cases: when app is launched or when a job has been created by an external program
        public async Task RestoreJobProgressAsync(List<string> transforms, AMSClientV3 amsClient)  // when app is launched for example, we want to restore job progress updates
        {
            ODataQuery<MediaJobResource> odataQuery = new()
            {
                Filter = "Properties/State eq Microsoft.Media.JobState'Queued' or Properties/State eq Microsoft.Media.JobState'Scheduled' or Properties/State eq Microsoft.Media.JobState'Processing' "
            };

            List<JobExtension> ActiveAndVisibleJobs = new();
            foreach (string t in transforms)
            {
                var transformResource = await amsClient.AMSclient.GetMediaTransformAsync(t);
                var jobsQuery = transformResource.Value.GetMediaJobs().GetAllAsync(filter: odataQuery.Filter, orderby: odataQuery.OrderBy);
                //IPage<Job> jobsPage = await amsClient.AMSclient.Jobs.ListAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, t, odataQuery);

                await foreach (var job in jobsQuery)
                {
                    ActiveAndVisibleJobs.Add(new JobExtension() { Job = job, TransformName = t });
                }
            }

            // let's cancel monitor task of non visible jobs
            List<string> listToCancel = new();
            try
            {
                lock (_MyListJobsMonitored)
                {
                    foreach (KeyValuePair<string, CancellationTokenSource> jobmonitored in _MyListJobsMonitored)
                    {
                        if (ActiveAndVisibleJobs.Where(j => j.Job.Data.Name == jobmonitored.Key).FirstOrDefault() == null)
                        {
                            jobmonitored.Value.Cancel();
                            listToCancel.Add(jobmonitored.Key);
                        }
                    }
                    listToCancel.ForEach(j => _MyListJobsMonitored.Remove(j));
                }

                // let's adjust the JobRefreshIntervalInMilliseconds based on the number of jobs to monitor
                // 2500 ms if 5 jobs or less, 500ms*nbjobs otherwise
                JobRefreshIntervalInMilliseconds = Math.Max(DefaultJobRefreshIntervalInMilliseconds, Convert.ToInt32(DefaultJobRefreshIntervalInMilliseconds * ActiveAndVisibleJobs.Count / 5d));

                // let's monitor job that are not yet monitored

                lock (_MyListJobsMonitored)
                {
                    foreach (JobExtension job in ActiveAndVisibleJobs)
                    {
                        if (!_MyListJobsMonitored.ContainsKey(job.Job.Data.Name))
                        {
                            DoJobProgress(job, amsClient); // token will be added to dictionnary in this function
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public void DoJobProgress(JobExtension job, AMSClientV3 amsClient)
        {
            CancellationTokenSource tokenSource = new();
            CancellationToken token = tokenSource.Token;

            lock (_MyListJobsMonitored)
            {
                if (!_MyListJobsMonitored.ContainsKey(job.Job.Data.Name))
                {
                    _MyListJobsMonitored.Add(job.Job.Data.Name, tokenSource); // to track the task and be able to cancel it later
                }
                else
                {
                    _MyListJobsMonitored[job.Job.Data.Name] = tokenSource; // to track the task and be able to cancel it later
                }
            }

            Debug.WriteLine("launch job monitor : " + job.Job.Data.Name);

            _ = Task.Run(() =>
              {
                  try
                  {
                      MediaJobResource myJob = null;

                      do
                      {

                          myJob = Task.Run(() =>
              amsClient.GetJobAsync(job.TransformName, job.Job.Data.Name)
              ).GetAwaiter().GetResult();

                          if (token.IsCancellationRequested == true)
                          {
                              return;
                          }

                          int index = -1;
                          foreach (JobEntryV3 je in _MyObservJobV3) // let's search for index
                          {
                              if (je.Name == myJob.Data.Name)
                              {
                                  index = _MyObservJobV3.IndexOf(je);
                                  break;
                              }
                          }

                          if (index >= 0) // we found it
                          { // we update the observation collection
                              JobProgressInfo progress = ReturnProgressJob(myJob.Data);

                              _MyObservJobV3[index].Progress = progress.progress;
                              _MyObservJobV3[index].Priority = myJob.Data.Priority;
                              _MyObservJobV3[index].StartOn = ReportLocalTime(myJob.Data.StartOn.Value.UtcDateTime);
                              _MyObservJobV3[index].EndOn = ReportLocalTime(myJob.Data.EndOn.Value.UtcDateTime);
                              _MyObservJobV3[index].State = myJob.Data.State;

                              // let's calculate the estimated time
                              string ETAstr = "", Durationstr = "";
                              if (progress.progress > 3d && progress.progress < 101d)
                              {
                                  DateTimeOffset startlocaltime = ((DateTimeOffset)myJob.Data.StartOn).ToLocalTime();
                                  TimeSpan interval = DateTime.Now - startlocaltime;
                                  DateTime ETA = DateTime.Now.AddSeconds((100d / progress.progress - 1d) * interval.TotalSeconds);
                                  TimeSpan estimatedduration = ETA - startlocaltime;

                                  ETAstr = "Estimated: " + ETA.ToString("G");
                                  Durationstr = "Estimated: " + estimatedduration.ToString(@"d\.hh\:mm\:ss");
                                  _MyObservJobV3[index].EndOn = ETA.ToString(@"G") + " ?";
                                  _MyObservJobV3[index].Duration = ReportDuration(myJob.Data) ?? estimatedduration.ToString(@"d\.hh\:mm\:ss") + " ?";
                              }

                              int indexdisplayed = -1;
                              foreach (JobEntryV3 je in _MyObservJobV3) // let's search for index in the page
                              {
                                  if (je.Name == myJob.Data.Name)
                                  {
                                      indexdisplayed = _MyObservJobV3.IndexOf(je);
                                      try
                                      {
                                          BeginInvoke(new Action(() =>
                                          {
                                              Rows[indexdisplayed].Cells[Columns["Progress"].Index].ToolTipText = progress.sb.ToString(); // mouse hover info
                                              if (progress.progress != 0)
                                              {
                                                  Rows[indexdisplayed].Cells[Columns["EndTime"].Index].ToolTipText = ETAstr;// mouse hover info
                                                  Rows[indexdisplayed].Cells[Columns["Duration"].Index].ToolTipText = Durationstr;// mouse hover info
                                              }
                                              //base.Refresh();
                                              RefreshGridView();
                                          }));
                                      }
                                      catch
                                      {

                                      }

                                      break;
                                  }
                              }
                          }

                          if (myJob.Data.State != MediaJobState.Finished && myJob.Data.State != MediaJobState.Error && myJob.Data.State != MediaJobState.Canceled)
                          {
                              Debug.WriteLine("wait for status : " + myJob.Data.Name);
                              Task.Delay(JobRefreshIntervalInMilliseconds).Wait();
                          }
                          else
                          {
                              break;
                          }
                      }
                      while (myJob.Data.State != MediaJobState.Finished
                      && myJob.Data.State != MediaJobState.Error
                      && myJob.Data.State != MediaJobState.Canceled);

                      // job finished

                      myJob = Task.Run(() =>
                                             amsClient.GetJobAsync(job.TransformName, job.Job.Data.Name)
                                             ).GetAwaiter().GetResult();

                      int index2 = -1;
                      foreach (JobEntryV3 je in _MyObservJobV3) // let's search for index
                      {
                          if (je.Name == myJob.Data.Name)
                          {
                              index2 = _MyObservJobV3.IndexOf(je);
                              break;
                          }
                      }
                      if (index2 >= 0) // we found it
                      { // we update the observation collection
                          StringBuilder sb2 = new(); // display percentage for each task for mouse hover (tooltiptext)

                          int progress2 = 0;
                          for (int i = 0; i < myJob.Data.Outputs.Count; i++)
                          {
                              MediaJobOutput output = myJob.Data.Outputs[i];

                              if (output.State == MediaJobState.Processing)
                              {
                                  progress2 += (int)output.Progress;

                                  sb2.AppendLine(string.Format("{0} % ({1})", Convert.ToInt32(output.Progress).ToString(), output.Label));
                              }
                          }
                          if (myJob.Data.Outputs.Count > 0)
                          {
                              progress2 /= myJob.Data.Outputs.Count;
                          }

                          _MyObservJobV3[index2].Progress = 101d; // progress;  we don't want the progress bar to be displayed
                          _MyObservJobV3[index2].Priority = myJob.Data.Priority;
                          _MyObservJobV3[index2].State = myJob.Data.State;
                          _MyObservJobV3[index2].EndOn = ReportLocalTime(myJob.Data.EndOn.Value.UtcDateTime);
                          _MyObservJobV3[index2].Duration = ReportDuration(myJob.Data);

                          lock (_MyListJobsMonitored)
                          {
                              if (_MyListJobsMonitored.ContainsKey(myJob.Data.Name)) // we want to display only one time
                              {
                                  _MyListJobsMonitored.Remove(myJob.Data.Name); // let's remove from the list of monitored jobs
                                  Mainform myform = (Mainform)FindForm();

                                  // string status = Enum.GetName(typeof(Microsoft.Azure.Management.Media.Models.JobState), myJob.State).ToLower();
                                  string status = myJob.Data.State.ToString();

                                  myform.BeginInvoke(new Action(() =>
                                  {
                                      myform.Notify(string.Format("Job {0}", status), string.Format("Job {0}", _MyObservJobV3[index2].Name), myJob.Data.State == MediaJobState.Error);
                                      myform.TextBoxLogWriteLine(string.Format("Job '{0}' : {1}.", _MyObservJobV3[index2].Name, status), myJob.Data.State == MediaJobState.Error);
                                      if (myJob.Data.State == MediaJobState.Error)
                                      {
                                          foreach (MediaJobOutput output in myJob.Data.Outputs)
                                          {

                                              if (output.Error != null && output.Error.Details != null)
                                              {
                                                  for (int i = 0; i < output.Error.Details.Count; i++)
                                                  {
                                                      myform.TextBoxLogWriteLine(string.Format("Output '{0}', Error : {1}", output.Label, output.Error + " : " + output.Error.Message), true);
                                                  }
                                              }
                                          }
                                      }
                                      myform.DoRefreshGridAssetV(false);
                                  }));

                                  RefreshGridView();
                                  /*
                                  BeginInvoke(new Action(() =>
                                  {
                                      base.Refresh();
                                  }));
                                  */
                              }
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

        private static string ReportLocalTime(DateTimeOffset? dateTime)
        {
            return dateTime.HasValue ? ((DateTimeOffset)dateTime).ToLocalTime().ToString("G") : null;
        }

        private static string ReportDuration(MediaJobData myJob)
        {
            return !myJob.EndOn.HasValue || !myJob.StartOn.HasValue ? null : (myJob.EndOn - myJob.StartOn).Value.ToString(@"d\.hh\:mm\:ss");
        }

        private static JobProgressInfo ReturnProgressJob(MediaJobData myJob)
        {
            StringBuilder sb = new();
            int progress = 0;
            for (int i = 0; i < myJob.Outputs.Count; i++)
            {
                MediaJobOutput output = myJob.Outputs[i];

                if (output.State == MediaJobState.Processing)
                {
                    progress += (int)output.Progress;
                    sb.AppendLine(string.Format("{0} % ({1})", Convert.ToInt32(output.Progress).ToString(), output.Label));
                }
            }
            if (myJob.Outputs.Count > 0)
            {
                progress /= myJob.Outputs.Count;
            }

            if (myJob.State != MediaJobState.Processing)
            {
                progress = 101;
            }

            return new JobProgressInfo() { progress = progress, sb = sb };
        }

        private void RefreshGridView()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    RefreshGridView();
                });
            }
            else
                Refresh();
        }
    }


    public class JobProgressInfo
    {
        public StringBuilder sb;
        public int progress;
    }
}
