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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class TransferEntryResponse
    {
        public Guid Id;
        public CancellationToken token;
    }

    public class TransferEntry : INotifyPropertyChanged
    {
        private SynchronizationContext syncContext;

        public TransferEntry(SynchronizationContext mysyncContext)
        {
            syncContext = mysyncContext;
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private TransferType _Type;
        public TransferType Type
        {
            get => _Type;
            set
            {
                if (value != _Type)
                {
                    _Type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private TransferState _State;
        public TransferState State
        {
            get => _State;
            set
            {
                if (value != _State)
                {
                    _State = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _Progress;
        public double Progress
        {
            get => _Progress;
            set
            {
                if (value != _Progress)
                {
                    _Progress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _ProgressText;
        public string ProgressText
        {
            get => _ProgressText;
            set
            {
                if (value != _ProgressText)
                {
                    _ProgressText = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Nullable<DateTime> _SubmitTime;
        public Nullable<DateTime> SubmitTime
        {
            get => _SubmitTime;
            set
            {
                if (value != _SubmitTime)
                {
                    _SubmitTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Nullable<DateTime> _StartTime;
        public Nullable<DateTime> StartTime
        {
            get => _StartTime;
            set
            {
                if (value != _StartTime)
                {
                    _StartTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _EndTime;
        public string EndTime
        {
            get => _EndTime;
            set
            {
                if (value != _EndTime)
                {
                    _EndTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _DestLocation;
        public string DestLocation
        {
            get => _DestLocation;
            set
            {
                if (value != _DestLocation)
                {
                    _DestLocation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool processedinqueue { get; set; }  // true if we want to process in the queue. Otherwise, we don't wait and we do paralell transfers
        public CancellationTokenSource tokenSource { get; set; }
        public Guid Id { get; set; }

        private string _ErrorDescription;
        public string ErrorDescription
        {
            get => _ErrorDescription;
            set
            {
                if (value != _ErrorDescription)
                {
                    _ErrorDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /*
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string p = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
        */

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string p = "")
        {
            if (PropertyChanged != null)
            {
                try
                {
                    var handler = PropertyChanged;

                    if (syncContext != null)
                        syncContext.Post(_ => handler(this, new PropertyChangedEventArgs(p)), null);
                    else
                        handler(this, new PropertyChangedEventArgs(p));

                }
                catch
                {

                }
            }
        }
    }

    public partial class Mainform : Form
    {
        private static BindingList<TransferEntry> _MyListTransfer; // list of upload/download
        private static List<Guid> _MyListTransferQueue; // List of transfers in the queue. It contains the index in the order of schedule

        private void DoGridTransferInit()
        {
            const string labelProgress = "Progress";

            _MyListTransfer = new BindingList<TransferEntry>();
            _MyListTransferQueue = new List<Guid>();

            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn();
            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
            col.Name = labelProgress;
            col.DataPropertyName = labelProgress;

            dataGridViewTransfer.Columns.Add(col);

            dataGridViewTransfer.DataSource = _MyListTransfer;

            dataGridViewTransfer.Columns[labelProgress].DisplayIndex = 3;
            dataGridViewTransfer.Columns[labelProgress].HeaderText = labelProgress;
            dataGridViewTransfer.Columns["processedinqueue"].Visible = false;
            dataGridViewTransfer.Columns["ErrorDescription"].Visible = false;
            dataGridViewTransfer.Columns["tokenSource"].Visible = false;
            dataGridViewTransfer.Columns["Id"].Visible = false;

            dataGridViewTransfer.Columns["SubmitTime"].Width = 140;
            dataGridViewTransfer.Columns["SubmitTime"].HeaderText = AMSExplorer.Properties.Resources.Mainform_DoGridTransferInit_SubmitTime;

            dataGridViewTransfer.Columns["StartTime"].Width = 140;
            dataGridViewTransfer.Columns["StartTime"].HeaderText = AMSExplorer.Properties.Resources.Mainform_DoGridTransferInit_StartTime;


            dataGridViewTransfer.Columns["EndTime"].Width = 140;
            dataGridViewTransfer.Columns["EndTime"].HeaderText = AMSExplorer.Properties.Resources.Mainform_DoGridTransferInit_EndTime;

            dataGridViewTransfer.Columns["ProgressText"].Width = 140;
            dataGridViewTransfer.Columns["ProgressText"].HeaderText = AMSExplorer.Properties.Resources.Mainform_DoGridTransferInit_ProgressDetail;

            dataGridViewTransfer.Columns["DestLocation"].Width = 140;
            dataGridViewTransfer.Columns["DestLocation"].HeaderText = AMSExplorer.Properties.Resources.Mainform_DoGridTransferInit_Destination;

//            tabPageTransfers.Invoke(new Action(() => tabPageTransfers.Text = string.Format(AMSExplorer.Properties.Resources.TabTransfers + " ({0})", 0)));

            tabPageTransfers.Invoke(t => t.Text = string.Format(AMSExplorer.Properties.Resources.TabTransfers + " ({0})", 0));
        }

        public TransferEntryResponse DoGridTransferAddItem(string text, TransferType TType, bool CanBePutInTheQueue)
        {
            TransferEntry myTE = new TransferEntry(SynchronizationContext.Current)
            {
                Name = text,
                SubmitTime = DateTime.Now,
                Type = TType
            };

         
            dataGridViewTransfer.Invoke(new Action(() =>
            {
                _MyListTransfer.Add(myTE);

            }
                ));
         

            myTE.Id = Guid.NewGuid();

            if (CanBePutInTheQueue)
            {
                _MyListTransferQueue.Add(myTE.Id);
                myTE.processedinqueue = true;
                myTE.State = TransferState.Queued;

            }
            else
            {
                myTE.processedinqueue = false;
                myTE.State = TransferState.Processing;
                myTE.StartTime = DateTime.Now;
            }

            // refresh number in tab
            //tabPageTransfers.Invoke(new Action(() => tabPageTransfers.Text = string.Format(AMSExplorer.Properties.Resources.TabTransfers + " ({0})", _MyListTransfer.Count())));
            tabPageTransfers.Invoke(t => t.Text = string.Format(AMSExplorer.Properties.Resources.TabTransfers + " ({0})", _MyListTransfer.Count()));

            // to cancel task if needed
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken tokenloc = tokenSource.Token;
            myTE.tokenSource = tokenSource;

            return new TransferEntryResponse() { Id = myTE.Id, token = tokenloc };
        }


        public void DoGridTransferCancelTask(Guid guid)
        {
            TransferEntry transfer = ReturnTransfer(guid);
            transfer.tokenSource.Cancel();
            transfer.State = TransferState.Cancelling;
        }

        private void DoGridTransferUpdateProgressText(string progresstext, double progress, Guid guid)
        {
            TransferEntry transfer = ReturnTransfer(guid);
            transfer.ProgressText = progresstext;
            DoGridTransferUpdateProgress(progress, guid);
        }

        private void DoGridTransferUpdateProgress(double progress, Guid guid)
        {
            TransferEntry transfer = ReturnTransfer(guid);

            transfer.Progress = progress;
            if (progress > 3 && transfer.StartTime != null)
            {
                TimeSpan interval = DateTime.UtcNow - ((DateTime)transfer.StartTime).ToUniversalTime();
                DateTime ETA = DateTime.UtcNow.AddSeconds((100d / progress - 1d) * interval.TotalSeconds);
                transfer.EndTime = ETA.ToLocalTime().ToString("G") + " ?";
            }
        }

        private TransferEntry ReturnTransfer(Guid guid)
        {
            return _MyListTransfer.ToList().Where(t => t.Id == guid).FirstOrDefault();
        }

        private void DoGridTransferDeclareCompleted(Guid guid, string DestLocation)  // Process is completed
        {
            TransferEntry transfer = ReturnTransfer(guid);

            transfer.Progress = 101d;
            transfer.State = TransferState.Finished;
            transfer.EndTime = DateTime.Now.ToString();
            transfer.DestLocation = DestLocation;
            transfer.ProgressText = string.Empty;

            BeginInvoke(new Action(() =>
            {
                Notify(AMSExplorer.Properties.Resources.Mainform_DoGridTransferDeclareCompleted_TransferCompleted, string.Format("{0}", transfer.Name));
                TextBoxLogWriteLine(string.Format(AMSExplorer.Properties.Resources.Mainform_DoGridTransferDeclareCompleted_Transfer0Completed, transfer.Name));
            }));
        }

        private void DoGridTransferDeclareCancelled(Guid guid)  // Process is completed
        {
            TransferEntry transfer = ReturnTransfer(guid);

            transfer.Progress = 101d;
            transfer.State = TransferState.Cancelled;
            transfer.EndTime = DateTime.Now.ToString();
            transfer.ProgressText = string.Empty;

            BeginInvoke(new Action(() =>
            {
                TextBoxLogWriteLine(string.Format(AMSExplorer.Properties.Resources.Mainform_DoGridTransferDeclareCancelled_Transfer0CancelledByUser, transfer.Name), true);
            }));
        }

        private void DoGridTransferDeclareError(Guid guid, Exception e)  // Process is completed
        {
            string message = e.Message;
            if (e.InnerException != null)
            {
                message = message + Constants.endline + Program.GetErrorMessage(e);
            }
            DoGridTransferDeclareError(guid, message);
        }

        private void DoGridTransferDeclareError(Guid guid, string ErrorDesc = "")  // Process is completed
        {
            TransferEntry transfer = ReturnTransfer(guid);

            transfer.Progress = 101d;
            transfer.EndTime = DateTime.Now.ToString();
            transfer.State = TransferState.Error;
            transfer.ProgressText = AMSExplorer.Properties.Resources.Mainform_DoGridTransferDeclareError_Error + ErrorDesc;
            transfer.ErrorDescription = ErrorDesc;

            BeginInvoke(new Action(() =>
            {
                Notify("Transfer Error", string.Format("{0}", transfer.Name), true);
                TextBoxLogWriteLine(string.Format(AMSExplorer.Properties.Resources.Mainform_DoGridTransferDeclareError_Transfer0Error, transfer.Name), true);
                TextBoxLogWriteLine(ErrorDesc, true);
            }));
        }

        private void DoGridTransferClearCompletedTransfers()
        {
            List<TransferEntry> list = _MyListTransfer.Where(l => l.State == TransferState.Cancelled || l.State == TransferState.Error || l.State == TransferState.Finished).ToList();
            foreach (TransferEntry l in list)
            {
                _MyListTransfer.Remove(l);
            }
        }

        private void DoGridTransferDeclareTransferStarted(Guid guid)  // Process is started
        {
            if (DoGridTransferIsQueueRequested(guid))
            {
                _MyListTransferQueue.Remove(guid);
            }

            TransferEntry transfer = ReturnTransfer(guid);
            transfer.Progress = 0;
            transfer.State = TransferState.Processing;
            transfer.StartTime = DateTime.Now;
            TextBoxLogWriteLine(string.Format(AMSExplorer.Properties.Resources.Mainform_DoGridTransferDeclareTransferStarted_Transfer0Started, transfer.Name));
        }

        private bool DoGridTransferQueueOurTurn(Guid guid)  // Return true if this is our turn
        {
            IEnumerable<TransferEntry> runningTransfers = _MyListTransfer.ToList().Where(t => t.processedinqueue && t.State == TransferState.Processing);

            if (runningTransfers.Count() < Properties.Settings.Default.ConcurrentTransfers && _MyListTransferQueue[0] == guid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoGridTransferIsQueueRequested(Guid guid)  // Return true trasfer is managed in the queue
        {
            return (ReturnTransfer(guid).processedinqueue);
        }

        private async Task DoGridTransferWaitIfNeededAsync(Guid guid)
        {
            // If upload in the queue, let's wait our turn
            if (DoGridTransferIsQueueRequested(guid))
            {
                while (!DoGridTransferQueueOurTurn(guid) && Properties.Settings.Default.ConcurrentTransfers < Constants.MaxTransfersAsUnlimited)
                {
                    Debug.Print("wait " + guid.ToString());
                    await Task.Delay(500);
                }
                DoGridTransferDeclareTransferStarted(guid);
            }
        }
    }
}