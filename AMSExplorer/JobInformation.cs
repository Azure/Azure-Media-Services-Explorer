//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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
using Microsoft.WindowsAzure.MediaServices.Client;


namespace AMSExplorer
{
    public partial class JobInformation : Form
    {
        public IJob MyJob;
        private CloudMediaContext _context;
        public JobInformation(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
        }

        private void contextMenuStrip_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            DataGridView DG = (DataGridView)contextmenu.SourceControl;

            if (DG.SelectedCells.Count == 1)
            {
                if (DG.SelectedCells[0].Value != null)
                {
                    System.Windows.Forms.Clipboard.SetText(DG.SelectedCells[0].Value.ToString());

                }
                else
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
            }
        }

        private void buttonCopyStats_Click(object sender, EventArgs e)
        {
            DoJobStats();
        }

        public void DoJobStats()
        {
            JobInfo JR = new JobInfo(MyJob);
            JR.CopyStatsToClipBoard();
        }

        private void JobInformation_Load(object sender, EventArgs e)
        {
            labelJobNameTitle.Text += MyJob.Name;

            DGJob.ColumnCount = 2;
            DGTasks.ColumnCount = 2;
            DGTasks.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            DGErrors.ColumnCount = 3;
            DGErrors.Columns[0].HeaderText = "Task";
            DGErrors.Columns[1].HeaderText = "Error detail";
            DGErrors.Columns[2].HeaderText = "Code";

            DGJob.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGJob.Rows.Add("Name", MyJob.Name);
            DGJob.Rows.Add("Id", MyJob.Id);
            DGJob.Rows.Add("State", MyJob.State);
            DGJob.Rows.Add("Priority", MyJob.Priority);
            DGJob.Rows.Add("Overall progress", MyJob.GetOverallProgress());

            if (MyJob.StartTime != null) DGJob.Rows.Add("Start Time", ((DateTime)MyJob.StartTime).ToLocalTime().ToString("G"));
            if (MyJob.EndTime != null) DGJob.Rows.Add("End Time", ((DateTime)MyJob.EndTime).ToLocalTime().ToString("G"));

            if ((MyJob.StartTime != null) && (MyJob.EndTime != null))
            {
                DGJob.Rows.Add("Job Duration", ((DateTime)MyJob.EndTime).Subtract((DateTime)MyJob.StartTime));
            }
            DGJob.Rows.Add("CPU Duration", MyJob.RunningDuration);
            DGJob.Rows.Add("Created", ((DateTime)MyJob.Created).ToLocalTime().ToString("G"));
            DGJob.Rows.Add("Last Modified", ((DateTime)MyJob.LastModified).ToLocalTime().ToString("G"));
            DGJob.Rows.Add("Template Id", MyJob.TemplateId);

            string sid = "";

            try
            {
                var iassets = MyJob.InputMediaAssets; // exception if input asset was deleted

                if (MyJob.InputMediaAssets.Count() > 1) sid = " #{0}"; else sid = "";
                for (int i = 0; i < MyJob.InputMediaAssets.Count(); i++)
                {
                    DGJob.Rows.Add("Input asset" + string.Format(sid, i) + " Name", MyJob.InputMediaAssets[i].Name);
                    DGJob.Rows.Add("Input asset" + string.Format(sid, i) + " Id", MyJob.InputMediaAssets[i].Id);
                }
            }

            catch
            {
                DGJob.Rows.Add("Input asset(s)", "<error, deleted?>");
            }


            try
            {
                var oassets = MyJob.OutputMediaAssets; // exception if output asset was deleted

                if (MyJob.OutputMediaAssets.Count() > 1) sid = " #{0}"; else sid = "";
                for (int i = 0; i < MyJob.OutputMediaAssets.Count(); i++)
                {
                    DGJob.Rows.Add("Output asset" + string.Format(sid, i) + " Name", MyJob.OutputMediaAssets[i].Name);
                    DGJob.Rows.Add("Output asset" + string.Format(sid, i) + " Id", MyJob.OutputMediaAssets[i].Id);
                }
            }
            catch
            {
                DGJob.Rows.Add("Output asset(s)", "<error, deleted?>");
            }


            TaskSizeAndPrice jobSizePrice = JobInfo.CalculateJobSizeAndPrice(MyJob);
            if ((jobSizePrice.InputSize != -1) && (jobSizePrice.OutputSize != -1))
            {
                DGJob.Rows.Add("Input size", AssetInfo.FormatByteSize(jobSizePrice.InputSize));
                DGJob.Rows.Add("Output size", AssetInfo.FormatByteSize(jobSizePrice.OutputSize));
                DGJob.Rows.Add("Processed size", AssetInfo.FormatByteSize(jobSizePrice.InputSize + jobSizePrice.OutputSize));
                if (jobSizePrice.Price != -1) DGJob.Rows.Add("Estimated price", string.Format("{0} {1:0.00}", Properties.Settings.Default.Currency, jobSizePrice.Price));
            }
            else
            {
                DGJob.Rows.Add("Input/output size", "undefined, task did not finish or one of the assets has been deleted");
            }

            bool btaskinjob = (MyJob.Tasks.Count() > 0);

            if (btaskinjob)
            {
                foreach (ITask task in MyJob.Tasks)
                {
                    listBoxTasks.Items.Add(task.Name ?? Constants.stringNull);

                    for (int i = 0; i < task.ErrorDetails.Count(); i++)
                    {
                        DGErrors.Rows.Add(task.Name, task.ErrorDetails[i].Message, task.ErrorDetails[i].Code);
                    }

                }
                listBoxTasks.SelectedIndex = 0;
            }
        }

        private void buttonCreateMail_Click(object sender, EventArgs e)
        {
            DoJobCreateMail();
        }

        private void DoJobCreateMail()
        {
            JobInfo JR = new JobInfo(MyJob);
            JR.CreateOutlookMail();
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITask task = MyJob.Tasks.Skip(listBoxTasks.SelectedIndex).Take(1).FirstOrDefault();

            DGTasks.Rows.Clear();

            DGTasks.Rows.Add("Name", task.Name);

            int i = DGTasks.Rows.Add("Configuration", "");
            DataGridViewButtonCell btn = new DataGridViewButtonCell();
            DGTasks.Rows[i].Cells[1] = btn;
            DGTasks.Rows[i].Cells[1].Value = "See clear value";
            DGTasks.Rows[i].Cells[1].Tag = task.GetClearConfiguration();

            i = DGTasks.Rows.Add("Body", "");
            btn = new DataGridViewButtonCell();
            DGTasks.Rows[i].Cells[1] = btn;
            DGTasks.Rows[i].Cells[1].Value = "See value";
            DGTasks.Rows[i].Cells[1].Tag = task.TaskBody;

            DGTasks.Rows.Add("Id", task.Id);
            DGTasks.Rows.Add("State", task.State);
            DGTasks.Rows.Add("Priority", task.Priority);
            if (task.StartTime != null) DGTasks.Rows.Add("Start Time", ((DateTime)task.StartTime).ToLocalTime().ToString("G"));
            if (task.EndTime != null) DGTasks.Rows.Add("End Time", ((DateTime)task.EndTime).ToLocalTime().ToString("G"));
            DGTasks.Rows.Add("Progress", task.Progress);
            DGTasks.Rows.Add("Duration", task.RunningDuration);
            DGTasks.Rows.Add("Perf Message", task.PerfMessage);
            DGTasks.Rows.Add("Encryption Key Id", task.EncryptionKeyId);
            DGTasks.Rows.Add("Encryption Scheme", task.EncryptionScheme);
            DGTasks.Rows.Add("Encryption Version", task.EncryptionVersion);

            // let's get the name of the processor
            IMediaProcessor processor = JobInfo.GetMediaProcessorFromId(task.MediaProcessorId, _context);
            DGTasks.Rows.Add("Mediaprocessor Id", task.MediaProcessorId);
            if (processor != null) DGTasks.Rows.Add("Mediaprocessor Name", processor.Name);

            DGTasks.Rows.Add("Options", task.Options);
            DGTasks.Rows.Add("Initialization Vector", task.InitializationVector);

            string sid = "";
            try
            {
                if (task.InputAssets.Count() > 1) sid = " #{0}"; else sid = "";
                for (int j = 0; j < task.InputAssets.Count(); j++)
                {
                    DGTasks.Rows.Add("Input asset" + string.Format(sid, j + 1) + " Name", task.InputAssets[j].Name);
                    DGTasks.Rows.Add("Input asset" + string.Format(sid, j + 1) + " Id", task.InputAssets[j].Id);
                }
            }
            catch
            {
                DGTasks.Rows.Add("Input asset(s)", "<error, deleted?>");
            }

            try
            {
                if (task.OutputAssets.Count() > 1) sid = " #{0}"; else sid = "";
                for (int j = 0; j < task.OutputAssets.Count(); j++)
                {
                    DGTasks.Rows.Add("Output asset" + string.Format(sid, j + 1) + " Name", task.OutputAssets[j].Name);
                    DGTasks.Rows.Add("Output asset" + string.Format(sid, j + 1) + " Id", task.OutputAssets[j].Id);
                }
            }
            catch
            {
                DGTasks.Rows.Add("Output asset(s)", "<error, deleted?>");
            }

            TaskSizeAndPrice taskSizePrice = JobInfo.CalculateTaskSizeAndPrice(task, _context);
            if ((taskSizePrice.InputSize != -1) && (taskSizePrice.OutputSize != -1))
            {
                DGTasks.Rows.Add("Input size", AssetInfo.FormatByteSize(taskSizePrice.InputSize));
                DGTasks.Rows.Add("Output size", AssetInfo.FormatByteSize(taskSizePrice.OutputSize));
                DGTasks.Rows.Add("Processed size", AssetInfo.FormatByteSize(taskSizePrice.InputSize + taskSizePrice.OutputSize));
                if (taskSizePrice.Price != -1) DGTasks.Rows.Add("Estimated price", string.Format("{0} {1:0.00}", Properties.Settings.Default.Currency, taskSizePrice.Price));
            }
            else
            {
                DGTasks.Rows.Add("Input/output size", "undefined, task did not finish or one of the assets has been deleted");
            }

            for (int j = 0; j < task.ErrorDetails.Count(); j++)
            {
                DGTasks.Rows.Add("Error", task.ErrorDetails[j].Code + ": " + task.ErrorDetails[j].Message);
            }
        }

        private void DGTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex >= 0 && senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewButtonCell))
            {
                SeeValueInEditor(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString(), senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString());
            }
        }

        private void SeeValueInEditor(string dataname, string key)
        {
            var editform = new EditorXMLJSON(dataname, key, false, false);
            editform.Display();
        }
    }
}
