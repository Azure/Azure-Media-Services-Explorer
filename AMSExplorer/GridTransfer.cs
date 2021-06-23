//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{

    public partial class Mainform : Form
    {
        private static BindingList<TransferEntry> _MyListTransfer; // list of upload/download
        private static List<Guid> _MyListTransferQueue; // List of transfers in the queue. It contains the index in the order of schedule

        private void DoGridTransferInit()
        {
            const string labelProgress = "Progress";

            _MyListTransfer = new BindingList<TransferEntry>();
            _MyListTransferQueue = new List<Guid>();

            DataGridViewProgressBarColumn col = new();
            DataGridViewCellStyle cellstyle = new();
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
            TransferEntry myTE = new(SynchronizationContext.Current)
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
            tabPageTransfers.Invoke(t => t.Text = string.Format(AMSExplorer.Properties.Resources.TabTransfers + " ({0})", _MyListTransfer.Count));

            // to cancel task if needed
            CancellationTokenSource tokenSource = new();
            CancellationToken tokenloc = tokenSource.Token;
            myTE.tokenSource = tokenSource;

            return new TransferEntryResponse() { Id = myTE.Id, token = tokenloc };
        }

        public static void DoGridTransferRetryTask(Guid guid)
        {
            TransferEntry transfer = ReturnTransfer(guid);
            transfer.tokenSource = new();
            transfer.State = TransferState.Processing;
            DoGridTransferUpdateProgress(0, guid);
        }


        public static void DoGridTransferCancelTask(Guid guid)
        {
            TransferEntry transfer = ReturnTransfer(guid);
            transfer.tokenSource.Cancel();
            transfer.State = TransferState.Cancelling;
        }

        private static void DoGridTransferUpdateProgressText(string progresstext, double progress, Guid guid)
        {
            TransferEntry transfer = ReturnTransfer(guid);

            if (transfer.State != TransferState.Finished && transfer.State != TransferState.Cancelled && transfer.State != TransferState.Error)
            {
                transfer.ProgressText = progresstext;
                DoGridTransferUpdateProgress(progress, guid);
            }
        }

        private static void DoGridTransferUpdateProgress(double progress, Guid guid)
        {
            TransferEntry transfer = ReturnTransfer(guid);

            if (transfer.State != TransferState.Finished && transfer.State != TransferState.Cancelled && transfer.State != TransferState.Error)
            {
                transfer.Progress = progress;
                if (progress > 3 && transfer.StartTime != null)
                {
                    TimeSpan interval = DateTime.UtcNow - ((DateTime)transfer.StartTime).ToUniversalTime();
                    DateTime ETA = DateTime.UtcNow.AddSeconds((100d / progress - 1d) * interval.TotalSeconds);
                    transfer.EndTime = ETA.ToLocalTime().ToString("G") + " ?";
                }
            }
        }

        private static TransferEntry ReturnTransfer(Guid guid)
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

        private static void DoGridTransferClearCompletedTransfers()
        {
            Telemetry.TrackEvent("GridTransfer DoGridTransferClearCompletedTransfers");

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

        private static bool DoGridTransferQueueOurTurn(Guid guid)  // Return true if this is our turn
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

        private static bool DoGridTransferIsQueueRequested(Guid guid)  // Return true transfer is managed in the queue
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