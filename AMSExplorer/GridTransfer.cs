//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Timers;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace AMSExplorer
{
    public class TransferEntryResponse
    {
        public int index;
        public CancellationToken token;
    }

    public class TransferEntry : INotifyPropertyChanged
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

        private TransferType _Type;
        public TransferType Type
        {
            get
            { return _Type; }
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

        private string _ProgressText;
        public string ProgressText
        {
            get
            { return _ProgressText; }
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
            get
            { return _SubmitTime; }
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
            get
            { return _StartTime; }
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
            get
            { return _EndTime; }
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
            get
            { return _DestLocation; }
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
        public int index { get; set; }

        private string _ErrorDescription;
        public string ErrorDescription
        {
            get
            { return _ErrorDescription; }
            set
            {
                if (value != _ErrorDescription)
                {
                    _ErrorDescription = value;
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

    public partial class Mainform : Form
    {
        private static BindingList<TransferEntry> _MyListTransfer; // list of upload/download
        private static List<int> _MyListTransferQueue; // List of transfers in the queue. It contains the index in the order of schedule

        private void DoGridTransferInit()
        {
            const string labelProgress = "Progress";

            _MyListTransfer = new BindingList<TransferEntry>();
            _MyListTransferQueue = new List<int>();

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
            dataGridViewTransfer.Columns["index"].Visible = false;

            dataGridViewTransfer.Columns["SubmitTime"].Width = 140;
            dataGridViewTransfer.Columns["SubmitTime"].HeaderText = "Submit time";

            dataGridViewTransfer.Columns["StartTime"].Width = 140;
            dataGridViewTransfer.Columns["StartTime"].HeaderText = "Start time";


            dataGridViewTransfer.Columns["EndTime"].Width = 140;
            dataGridViewTransfer.Columns["EndTime"].HeaderText = "End time";

            dataGridViewTransfer.Columns["ProgressText"].Width = 140;
            dataGridViewTransfer.Columns["ProgressText"].HeaderText = "Progress detail";

            dataGridViewTransfer.Columns["DestLocation"].Width = 140;
            dataGridViewTransfer.Columns["DestLocation"].HeaderText = "Destination";

            tabPageTransfers.Invoke(new Action(() => tabPageTransfers.Text = string.Format(Constants.TabTransfers + " ({0})", 0)));
        }
        public TransferEntryResponse DoGridTransferAddItem(string text, TransferType TType, bool PutInTheQueue)
        {
            TransferEntry myTE = new TransferEntry()
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
            myTE.index = _MyListTransfer.IndexOf(myTE);

            if (PutInTheQueue)
            {
                _MyListTransferQueue.Add(myTE.index);
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
            tabPageTransfers.Invoke(new Action(() => tabPageTransfers.Text = string.Format(Constants.TabTransfers + " ({0})", _MyListTransfer.Count())));

            // to cancel task if needed
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken tokenloc = tokenSource.Token;
            myTE.tokenSource = tokenSource;

            return new TransferEntryResponse() { index = myTE.index, token = tokenloc };
        }
            

        public void DoGridTransferCancelTask(int index)
        {
            _MyListTransfer[index].tokenSource.Cancel();
            _MyListTransfer[index].State = TransferState.Cancelling;
        }

        private void DoGridTransferUpdateProgressText(string progresstext, double progress, int index)
        {
            _MyListTransfer[index].ProgressText = progresstext;
            DoGridTransferUpdateProgress(progress, index);
        }
        private void DoGridTransferUpdateProgress(double progress, int index)
        {
            _MyListTransfer[index].Progress = progress;
            if (progress > 3 && _MyListTransfer[index].StartTime != null)
            {
                TimeSpan interval = (TimeSpan)(DateTime.UtcNow - ((DateTime)_MyListTransfer[index].StartTime).ToUniversalTime());
                DateTime ETA = DateTime.UtcNow.AddSeconds((100d / progress - 1d) * interval.TotalSeconds);
                _MyListTransfer[index].EndTime = ETA.ToLocalTime().ToString("G") + " ?";
            }
        }

        private void DoGridTransferDeclareCompleted(int index, string DestLocation)  // Process is completed
        {
          
                _MyListTransfer[index].Progress = 101d;
                _MyListTransfer[index].State = TransferState.Finished;
                _MyListTransfer[index].EndTime = DateTime.Now.ToString();
                _MyListTransfer[index].DestLocation = DestLocation;
                _MyListTransfer[index].ProgressText = string.Empty;
                if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);

                this.BeginInvoke(new Action(() =>
                {
                    this.Notify("Transfer completed", string.Format("{0}", _MyListTransfer[index].Name));
                    this.TextBoxLogWriteLine(string.Format("Transfer '{0}' completed.", _MyListTransfer[index].Name));
                }));
           
        }

        private void DoGridTransferDeclareCancelled(int index)  // Process is completed
        {
                _MyListTransfer[index].Progress = 101d;
                _MyListTransfer[index].State = TransferState.Cancelled;
                _MyListTransfer[index].EndTime = DateTime.Now.ToString();
                _MyListTransfer[index].ProgressText = string.Empty;
                if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);

            this.BeginInvoke(new Action(() =>
            {
                this.TextBoxLogWriteLine(string.Format("Transfer '{0}' cancelled by user.", _MyListTransfer[index].Name), true);
            }));
        }


        private void DoGridTransferDeclareError(int index, Exception e)  // Process is completed
        {
            string message = e.Message;
            if (e.InnerException != null)
            {
                message = message + Constants.endline + Program.GetErrorMessage(e);
            }
            DoGridTransferDeclareError(index, message);
        }

        private void DoGridTransferDeclareError(int index, string ErrorDesc = "")  // Process is completed
        {
            _MyListTransfer[index].Progress = 101d;
            _MyListTransfer[index].EndTime = DateTime.Now.ToString();
            _MyListTransfer[index].State = TransferState.Error;
            _MyListTransfer[index].ProgressText = "Error: " + ErrorDesc;
            _MyListTransfer[index].ErrorDescription = ErrorDesc;
            if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);
            //dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);

            this.BeginInvoke(new Action(() =>
            {
                this.Notify("Transfer Error", string.Format("{0}", _MyListTransfer[index].Name), true);
                this.TextBoxLogWriteLine(string.Format("Transfer '{0}': Error", _MyListTransfer[index].Name), true);
                this.TextBoxLogWriteLine(ErrorDesc, true);

            }));
        }

        private void DoGridTransferDeclareTransferStarted(int index)  // Process is started
        {
            _MyListTransfer[index].Progress = 0;
            _MyListTransfer[index].State = TransferState.Processing;
            _MyListTransfer[index].StartTime = DateTime.Now;
            //dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
            this.TextBoxLogWriteLine(string.Format("Transfer '{0}': started", _MyListTransfer[index].Name));
        }

        private bool DoGridTransferQueueOurTurn(int index)  // Return true if this is out turn
        {
            return (_MyListTransferQueue.Count > 0) ? (_MyListTransferQueue[0] == index) : true;
        }

        private bool DoGridTransferIsQueueRequested(int index)  // Return true trasfer is managed in the queue
        {
            return (_MyListTransfer[index].processedinqueue);
        }

        private void DoGridTransferWaitIfNeeded(int index)
        {
            // If upload in the queue, let's wait our turn
            if (DoGridTransferIsQueueRequested(index))
            {
                while (!DoGridTransferQueueOurTurn(index))
                {
                    Debug.Print("wait " + index);
                    Thread.Sleep(500);
                }
                DoGridTransferDeclareTransferStarted(index);
            }
        }
    }
}