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
        private Mainform _mainform;
        public IEnumerable<IStreamingEndpoint> MyStreamingEndpoints;

        public JobInformation(Mainform mainform, CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _mainform = mainform;
            
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
            JobInfo JR = new JobInfo(MyJob, _mainform._accountname);
            StringBuilder SB = JR.GetStats();
            var tokenDisplayForm = new EditorXMLJSON(AMSExplorer.Properties.Resources.JobInformation_DoJobStats_JobReport, SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
        }

        private void JobInformation_Load(object sender, EventArgs e)
        {
            labelJobNameTitle.Text += MyJob.Name;

            DGJob.ColumnCount = 2;
            DGTasks.ColumnCount = 2;
            DGTasks.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            DGErrors.ColumnCount = 3;
            DGErrors.Columns[0].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Task;
            DGErrors.Columns[1].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_ErrorDetail;
            DGErrors.Columns[2].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Code;

            DGJob.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, MyJob.Name);
            DGJob.Rows.Add("Id", MyJob.Id);
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, MyJob.State);
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Priority, MyJob.Priority);
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_OverallProgress, MyJob.GetOverallProgress());

            if (MyJob.StartTime != null) DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_StartTime, ((DateTime)MyJob.StartTime).ToLocalTime().ToString("G"));
            if (MyJob.EndTime != null) DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_EndTime, ((DateTime)MyJob.EndTime).ToLocalTime().ToString("G"));

            if ((MyJob.StartTime != null) && (MyJob.EndTime != null))
            {
                DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_JobDuration, ((DateTime)MyJob.EndTime).Subtract((DateTime)MyJob.StartTime));
            }
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_CPUDuration, MyJob.RunningDuration);
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)MyJob.Created).ToLocalTime().ToString("G"));
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)MyJob.LastModified).ToLocalTime().ToString("G"));
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_TemplateId, MyJob.TemplateId);


            TaskSize jobSizePrice = JobInfo.CalculateJobSizeAndPrice(MyJob);
            if ((jobSizePrice.InputSize != -1) && (jobSizePrice.OutputSize != -1))
            {
                DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_InputSize, AssetInfo.FormatByteSize(jobSizePrice.InputSize));
                DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_OutputSize, AssetInfo.FormatByteSize(jobSizePrice.OutputSize));
                DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_ProcessedSize, AssetInfo.FormatByteSize(jobSizePrice.InputSize + jobSizePrice.OutputSize));
                //if (jobSizePrice.Price != -1) DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_EstimatedPrice, string.Format("{0} {1:0.00}", Properties.Settings.Default.Currency, jobSizePrice.Price));
            }
            else
            {
                DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_InputOutputSize, AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_UndefinedTaskDidNotFinishOrOneOfTheAssetsHasBeenDeleted);
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

            ListJobAssets();
        }

        private void ListJobAssets()
        {
            listViewInputAssets.BeginUpdate();
            try
            {
                foreach (IAsset asset in MyJob.InputMediaAssets)
                {
                    ListViewItem item = new ListViewItem(asset.Name, 0);
                    item.SubItems.Add(AssetInfo.GetAssetType(asset));
                    listViewInputAssets.Items.Add(item);
                }
            }
            catch
            {
                ListViewItem item = new ListViewItem(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ErrorDeleted, 0);
                listViewInputAssets.Items.Add(item);
            }
            listViewInputAssets.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewInputAssets.EndUpdate();

            listViewOutputAssets.BeginUpdate();
            try
            {
                foreach (IAsset asset in MyJob.OutputMediaAssets)
                {
                    ListViewItem item = new ListViewItem(asset.Name, 0);
                    item.SubItems.Add(AssetInfo.GetAssetType(asset));
                    listViewOutputAssets.Items.Add(item);
                }
            }
            catch
            {
                ListViewItem item = new ListViewItem(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ErrorDeleted, 0);
                listViewOutputAssets.Items.Add(item);
            }
            listViewOutputAssets.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewOutputAssets.EndUpdate();
        }



        private void buttonCreateMail_Click(object sender, EventArgs e)
        {
            DoJobCreateMail();
        }

        private void DoJobCreateMail()
        {
            JobInfo JR = new JobInfo(MyJob, _mainform._accountname);
            JR.CreateOutlookMail();
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITask task = MyJob.Tasks.Skip(listBoxTasks.SelectedIndex).Take(1).FirstOrDefault();

            DGTasks.Rows.Clear();

            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, task.Name);

            int i = DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_Configuration, "");
            DataGridViewButtonCell btn = new DataGridViewButtonCell();
            DGTasks.Rows[i].Cells[1] = btn;
            DGTasks.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_SeeClearValue;
            DGTasks.Rows[i].Cells[1].Tag = task.GetClearConfiguration();

            i = DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_Body, "");
            btn = new DataGridViewButtonCell();
            DGTasks.Rows[i].Cells[1] = btn;
            DGTasks.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_SeeValue;
            DGTasks.Rows[i].Cells[1].Tag = task.TaskBody;

            DGTasks.Rows.Add("Id", task.Id);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, task.State);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Priority, task.Priority);
            if (task.StartTime != null) DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_StartTime, ((DateTime)task.StartTime).ToLocalTime().ToString("G"));
            if (task.EndTime != null) DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_EndTime, ((DateTime)task.EndTime).ToLocalTime().ToString("G"));
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.DataGridViewIngestManifest_Init_Progress, task.Progress);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_Duration, task.RunningDuration);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_PerfMessage, task.PerfMessage);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_EncryptionKeyId, task.EncryptionKeyId);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_EncryptionScheme, task.EncryptionScheme);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_EncryptionVersion, task.EncryptionVersion);

            // let's get the name of the processor
            IMediaProcessor processor = JobInfo.GetMediaProcessorFromId(task.MediaProcessorId, _context);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_MediaprocessorId, task.MediaProcessorId);
            if (processor != null) DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_MediaprocessorName, processor.Name);

            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_DoDisplayFileProperties_Options, task.Options);
            DGTasks.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_InitializationVector, task.InitializationVector);

            string sid = "";
            try
            {
                if (task.InputAssets.Count() > 1) sid = " #{0}"; else sid = "";
                for (int j = 0; j < task.InputAssets.Count(); j++)
                {
                    var s = string.Format(sid, j + 1);
                    DGTasks.Rows.Add(string.Format("Input asset{0} Name", s), task.InputAssets[j].Name);
                    DGTasks.Rows.Add(string.Format("Input asset{0} Id", s), task.InputAssets[j].Id);
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
                    var s = string.Format(sid, j + 1);
                    DGTasks.Rows.Add(string.Format("Output asset{0} Name", s), task.OutputAssets[j].Name);
                    DGTasks.Rows.Add(string.Format("Output asset{0} Id", s), task.OutputAssets[j].Id);
                    DGTasks.Rows.Add(string.Format("Output asset{0} Format Option", s), task.OutputAssets[j].FormatOption);
                }
            }
            catch
            {
                DGTasks.Rows.Add("Output asset(s)", "<error, deleted?>");
            }

            TaskSize taskSizePrice = JobInfo.CalculateTaskSize(task, _context);
            if ((taskSizePrice.InputSize != -1) && (taskSizePrice.OutputSize != -1))
            {
                DGTasks.Rows.Add("Input size", AssetInfo.FormatByteSize(taskSizePrice.InputSize));
                DGTasks.Rows.Add("Output size", AssetInfo.FormatByteSize(taskSizePrice.OutputSize));
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

        private void assetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayAssetInfo(true);
        }

        private void DisplayAssetInfo(bool input)
        {
            IAsset asset;

            if (input)
            {
                var index = listViewInputAssets.SelectedIndices[0];
                asset = MyJob.InputMediaAssets[index];
            }
            else
            {
                var index = listViewOutputAssets.SelectedIndices[0];
                asset = MyJob.OutputMediaAssets[index];
            }

            AssetInformation form = new AssetInformation(_mainform, _context)
            {
                myAsset = asset,
                myStreamingEndpoints = MyStreamingEndpoints // we want to keep the same sorting
            };
            DialogResult dialogResult = form.ShowDialog(this);


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DisplayAssetInfo(false);
        }
    }
}
