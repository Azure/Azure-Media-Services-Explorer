using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class IngestManifestEntry : INotifyPropertyChanged
    {
        private string _Name;
        public string Name
        {
            get
            { return _Name; }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Id;
        public string Id
        {
            get
            { return _Id; }
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private IngestManifestState _State;
        public IngestManifestState State
        {
            get
            { return _State; }
            set
            {
                if (value != _State)
                {
                    _State = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _URLForUpload;
        public string URLForUpload
        {
            get
            { return _URLForUpload; }
            set
            {
                if (value != _URLForUpload)
                {
                    _URLForUpload = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private int _PendingFiles;
        public int PendingFiles
        {
            get
            { return _PendingFiles; }
            set
            {
                if (value != _PendingFiles)
                {
                    _PendingFiles = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _FinishedFiles;
        public int FinishedFiles
        {
            get
            { return _FinishedFiles; }
            set
            {
                if (value != _FinishedFiles)
                {
                    _FinishedFiles = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private double _Progress;
        public double Progress
        {
            get
            { return _Progress; }
            set
            {
                if (value != _Progress)
                {
                    _Progress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _LastModified;
        public DateTime LastModified
        {
            get
            { return _LastModified; }
            set
            {
                if (value != _LastModified)
                {
                    _LastModified = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _StorageAccountName;
        public string StorageAccountName
        {
            get
            { return _StorageAccountName; }
            set
            {
                if (value != _StorageAccountName)
                {
                    _StorageAccountName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String p = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
    }

    public class DataGridViewIngestManifest : DataGridView
    {
        public bool Initialized
        {
            get
            {
                return _initialized;
            }
        }

        static BindingList<IngestManifestEntry> _MyObservIngestManifest;
        public IEnumerable<IIngestManifest> ingestmanifests;
        static List<string> _MyListIngestManifestsMonitored = new List<string>(); // List of ingest manifests monitored. It contains the jobs ids. Used when a new ingest manifest is discovered (created by another program) to activate the display of ingest manifest progress
        static private bool _initialized = false;
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static BackgroundWorker WorkerUpdateIngestManifest;

        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<IngestManifestEntry> ingestmanifestquery;
            _credentials = credentials;
            _context = context;// Program.ConnectAndGetNewContext(_credentials);
            ingestmanifestquery = from im in _context.IngestManifests.Take(0)
                                  orderby im.LastModified descending
                                  select new IngestManifestEntry
                                  {
                                      Name = im.Name,
                                      Id = im.Id,
                                      State = im.State,
                                      LastModified = im.LastModified,
                                      URLForUpload = im.BlobStorageUriForUpload
                                  };

            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
            {
                Name = "Progress",
                DataPropertyName = "Progress",
                HeaderText = "Progress"
            };

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();

            this.Columns.Add(col);
            BindingList<IngestManifestEntry> MyObservJobInPage = new BindingList<IngestManifestEntry>(ingestmanifestquery.Take(0).ToList());
            this.DataSource = MyObservJobInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayIngestManifestIDinGrid;
            this.Columns["Progress"].DisplayIndex = 6;
            this.Columns["Progress"].Width = 150;
            //this.Columns["URLForUpload"].DisplayIndex = 6;
            this.Columns["URLForUpload"].Width = 200;

            // this.Columns["Tasks"].Width = 50;
            //this.Columns["Priority"].Width = 50;
            // this.Columns["State"].Width = 80;
            // this.Columns["StartTime"].Width = 150;
            // this.Columns["EndTime"].Width = 150;
            // this.Columns["Duration"].Width = 90;

            WorkerUpdateIngestManifest = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            WorkerUpdateIngestManifest.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerUpdateIngestManifest_DoWork);

            _initialized = true;
        }

        public void AnalyzeItemsInBackground()
        {
            Task.Run(() =>
            {
                WorkerUpdateIngestManifest.CancelAsync();
                // let's wait a little for the previous worker to cancel if needed
                System.Threading.Thread.Sleep(2000);

                if (WorkerUpdateIngestManifest.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    try
                    {
                        WorkerUpdateIngestManifest.RunWorkerAsync();
                    }
                    catch { }
                }
            });

        }


        private void WorkerUpdateIngestManifest_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("WorkerUpdateIngestManifest_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;

            Mainform myform = (Mainform)this.FindForm();

            while (true)
            {
                var manifestsupdated = _context.IngestManifests.ToList();

                foreach (var im in _context.IngestManifests.AsEnumerable())
                {
                    var img = _MyObservIngestManifest.Where(i => i.Id == im.Id).FirstOrDefault();
                    if (img != null)
                    {
                        if (im.Statistics.PendingFilesCount == 0 && img.PendingFiles != im.Statistics.PendingFilesCount)
                        {
                            // Notify if upload completed for one bulk ingest container
                            myform.Notify(string.Format("Bulk ingest completed with {0} error(s)", im.Statistics.ErrorFilesCount), string.Format("Container '{0}'", im.Name), im.Statistics.ErrorFilesCount > 0);
                            myform.TextBoxLogWriteLine(string.Format("Bulk ingest on container '{0}' completed with {1} error(s)", im.Name, im.Statistics.ErrorFilesCount), im.Statistics.ErrorFilesCount > 0);
                        }

                        img.State = im.State;
                        img.LastModified = im.LastModified.ToLocalTime();
                        img.PendingFiles = im.Statistics.PendingFilesCount;
                        img.FinishedFiles = im.Statistics.FinishedFilesCount;

                        if (im.Statistics.FinishedFilesCount + im.Statistics.PendingFilesCount == 0)
                        {
                            img.Progress = 101;
                        }
                        else
                        {
                            img.Progress = (float)im.Statistics.FinishedFilesCount / (float)(im.Statistics.FinishedFilesCount + im.Statistics.PendingFilesCount) * 100;
                        }
                    }
                }

                System.Threading.Thread.Sleep(10000); // 10s
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }


        public void RefreshIngestManifests(CloudMediaContext context)
        {
            if (!_initialized || context == null) return;

            Debug.WriteLine("Refresh Jobs Start");

            this.FindForm().Cursor = Cursors.WaitCursor;
            _context = context;

            var imquery = from im in context.IngestManifests.ToList()
                          select new IngestManifestEntry
                          {
                              Name = im.Name,
                              Id = im.Id,
                              LastModified = im.LastModified.ToLocalTime(),
                              StorageAccountName = im.StorageAccountName,
                              URLForUpload = im.BlobStorageUriForUpload
                          };
            _MyObservIngestManifest = new BindingList<IngestManifestEntry>(imquery.ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservIngestManifest));

            if (_MyObservIngestManifest.Count > 0)
            {
                AnalyzeItemsInBackground();
            }
            else
            {
                WorkerUpdateIngestManifest.CancelAsync();
            }

            Debug.WriteLine("Refresh Ingest Manifest End");
            this.FindForm().Cursor = Cursors.Default;
        }
    }
}
