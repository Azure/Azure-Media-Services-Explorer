// <copyright file="Mainform.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
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


namespace AMSExplorer
{
    public class TransferEntry
    {
        public string Name { get; set; }
        public TransferType Type { get; set; }
        public TransferState State { get; set; }
        public double Progress { get; set; }
        public string ProgressText { get; set; }
        public Nullable<DateTime> SubmitTime { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public string EndTime { get; set; }
        public string DestLocation { get; set; }
        public bool processedinqueue { get; set; }  // true if we want to process in the queue. Otherwise, we don't wait and we do paralell transfers
        public string ErrorDescription { get; set; }
    }

    public partial class Mainform : Form
    {
        private BindingList<TransferEntry> _MyListTransfer; // list of upload/download
        private List<int> _MyListTransferQueue; // List of transfers in the queue. It contains the index in the order of schedule

        private void DoGridTransferInit()
        {
            const string labelProgress = "Progress";

            _MyListTransfer = new BindingList<TransferEntry>();
            _MyListTransferQueue = new List<int>();

            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn();
            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
            col.Name = labelProgress;
            col.DataPropertyName = labelProgress;
            dataGridViewTransfer.Invoke(new Action(() =>
            {
                dataGridViewTransfer.Columns.Add(col);
            }
            ));

            dataGridViewTransfer.Invoke(new Action(() =>
            {
                dataGridViewTransfer.DataSource = _MyListTransfer;
            }
          ));

            dataGridViewTransfer.Invoke(new Action(() =>
            {
                dataGridViewTransfer.Columns[labelProgress].DisplayIndex = 3;
                dataGridViewTransfer.Columns[labelProgress].HeaderText = labelProgress;
                dataGridViewTransfer.Columns["processedinqueue"].Visible = false;
                dataGridViewTransfer.Columns["ErrorDescription"].Visible = false;

            }
          ));
        }
        public int DoGridTransferAddItem(string text, TransferType TType, bool PutInTheQueue)
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
            int indexloc = _MyListTransfer.IndexOf(myTE);

            if (PutInTheQueue)
            {
                _MyListTransferQueue.Add(indexloc);
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
            return indexloc;
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
                _MyListTransfer[index].EndTime = ETA.ToLocalTime().ToString() + " ?";
            }

            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private void DoGridTransferDeclareCompleted(int index, string DestLocation)  // Process is completed
        {
            _MyListTransfer[index].Progress = 100;
            _MyListTransfer[index].State = TransferState.Finished;
            _MyListTransfer[index].EndTime = DateTime.Now.ToString();
            _MyListTransfer[index].DestLocation = DestLocation;
            _MyListTransfer[index].ProgressText = string.Empty;
            if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
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
            _MyListTransfer[index].Progress = 100;
            _MyListTransfer[index].EndTime = DateTime.Now.ToString();
            _MyListTransfer[index].State = TransferState.Error;
            _MyListTransfer[index].ProgressText = "Error: " + ErrorDesc;
            _MyListTransfer[index].ErrorDescription = ErrorDesc;
            if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private void DoGridTransferDeclareTransferStarted(int index)  // Process is started
        {
            _MyListTransfer[index].Progress = 0;
            _MyListTransfer[index].State = TransferState.Processing;
            _MyListTransfer[index].StartTime = DateTime.Now;
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
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
                    Thread.Sleep(500);
                }

                DoGridTransferDeclareTransferStarted(index);
            }
        }
    }
}