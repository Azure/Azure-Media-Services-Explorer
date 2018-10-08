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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;


namespace AMSExplorer
{
    public partial class JobInformation : Form
    {
        public Job MyJob;
        private AzureMediaServicesClient _client;
        private Mainform _mainform;
        public IEnumerable<StreamingEndpoint> MyStreamingEndpoints;

        public JobInformation(Mainform mainform, AzureMediaServicesClient client)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _client = client;
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
            throw new NotImplementedException();

            /*
            JobInfo JR = new JobInfo(MyJob, _mainform._accountname);
            StringBuilder SB = JR.GetStats();
            var tokenDisplayForm = new EditorXMLJSON(AMSExplorer.Properties.Resources.JobInformation_DoJobStats_JobReport, SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
            */
        }

        private void JobInformation_Load(object sender, EventArgs e)
        {
            labelJobNameTitle.Text += MyJob.Name;

            DGJob.ColumnCount = 2;
            DGOutputs.ColumnCount = 2;
            DGOutputs.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            DGErrors.ColumnCount = 3;
            DGErrors.Columns[0].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Task;
            DGErrors.Columns[1].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_ErrorDetail;
            DGErrors.Columns[2].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Code;

            DGJob.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, MyJob.Name);
            DGJob.Rows.Add("Description", MyJob.Description);
            DGJob.Rows.Add("Id", MyJob.Id);
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_State, MyJob.State);
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Priority, MyJob.Priority);
            //DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_OverallProgress, MyJob.GetOverallProgress());

            // if (MyJob.StartTime != null) DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_StartTime, ((DateTime)MyJob.StartTime).ToLocalTime().ToString("G"));
            // if (MyJob.EndTime != null) DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_EndTime, ((DateTime)MyJob.EndTime).ToLocalTime().ToString("G"));

            /*
            if ((MyJob.StartTime != null) && (MyJob.EndTime != null))
            {
                DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_JobDuration, ((DateTime)MyJob.EndTime).Subtract((DateTime)MyJob.StartTime));
            }
            */

            // DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_CPUDuration, MyJob.RunningDuration);
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)MyJob.Created).ToLocalTime().ToString("G"));
            DGJob.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)MyJob.LastModified).ToLocalTime().ToString("G"));
            // DGJob.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_TemplateId, MyJob.TemplateId);

            /*

            TaskSizeAndPrice jobSizePrice = JobInfo.CalculateJobSizeAndPrice(MyJob);
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
            */

            bool boutoutsinjobs = (MyJob.Outputs.Count() > 0);

            int index = 1;
            if (boutoutsinjobs)
            {
                foreach (var output in MyJob.Outputs)
                {
                    // listBoxTasks.Items.Add(output..Name ?? Constants.stringNull);
                    var outputLabel = "output #" + index;
                    listBoxOutputs.Items.Add(outputLabel);
                    if (output.Error != null && output.Error.Details != null)
                    {
                        for (int i = 0; i < output.Error.Details.Count(); i++)
                        {
                            DGErrors.Rows.Add(outputLabel, output.Error.Details[i].Message, output.Error.Details[i].Code);
                        }
                    }


                }
                listBoxOutputs.SelectedIndex = 0;
            }

            ListJobAssets();
        }

        private void ListJobAssets()
        {
            listViewInputAssets.BeginUpdate();
            try
            {
                
                //ListViewItem item = new ListViewItem(MyJob.Input....Label, 0);
                //item.SubItems.Add(AssetInfo.GetAssetType(asset));
                //listViewInputAssets.Items.Add(item);

            }
            catch
            {
                //   ListViewItem item = new ListViewItem(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ErrorDeleted, 0);
                // listViewInputAssets.Items.Add(item);
            }
            listViewInputAssets.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewInputAssets.EndUpdate();

            listViewOutputs.BeginUpdate();
            try
            {
                int index = 1;
                foreach (var output in MyJob.Outputs)
                {
                    ListViewItem item = new ListViewItem("output #" + index, 0);
                    //item.SubItems.Add(output.Progress);
                    listViewOutputs.Items.Add(item);
                }
            }
            catch
            {
                //ListViewItem item = new ListViewItem(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_ErrorDeleted, 0);
                //listViewOutputs.Items.Add(item);
            }
            listViewOutputs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewOutputs.EndUpdate();
        }



        private void buttonCreateMail_Click(object sender, EventArgs e)
        {
            DoJobCreateMail();
        }

        private void DoJobCreateMail()
        {
            throw new NotImplementedException();
            /*
            JobInfo JR = new JobInfo(MyJob, _mainform._accountname);
            JR.CreateOutlookMail();
            */
        }

        private void listBoxOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var output = MyJob.Outputs.Skip(listBoxOutputs.SelectedIndex).Take(1).FirstOrDefault();

            DGOutputs.Rows.Clear();

            //  DGTasks.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, task.Name);

            /*
            int i = DGOutputs.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_Configuration, "");
            DataGridViewButtonCell btn = new DataGridViewButtonCell();
            DGOutputs.Rows[i].Cells[1] = btn;
            DGOutputs.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_SeeClearValue;
            DGOutputs.Rows[i].Cells[1].Tag =  task.GetClearConfiguration();
            */

            /*
            i = DGOutputs.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_Body, "");
            btn = new DataGridViewButtonCell();
            DGOutputs.Rows[i].Cells[1] = btn;
            DGOutputs.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_SeeValue;
            DGOutputs.Rows[i].Cells[1].Tag = task.TaskBody;
            */

            DGOutputs.Rows.Add("Progress", output.Progress);
            DGOutputs.Rows.Add("State", output.State);

            if (output.Error != null && output.Error.Details != null)
            {
                for (int j = 0; j < output.Error.Details.Count(); j++)
                {
                    DGOutputs.Rows.Add("Error", output.Error.Details[j].Code + ": " + output.Error.Details[j].Message);
                }
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
            throw new NotImplementedException();

            /*
            IAsset asset;

            if (input)
            {
                var index = listViewInputAssets.SelectedIndices[0];
                asset = MyJob.Input..InputMediaAssets[index];
            }
            else
            {
                var index = listViewOutputs.SelectedIndices[0];
                asset = MyJob.OutputMediaAssets[index];
            }

            AssetInformation form = new AssetInformation(_mainform, _context)
            {
                myAsset = asset,
                myStreamingEndpoints = MyStreamingEndpoints // we want to keep the same sorting
            };
            DialogResult dialogResult = form.ShowDialog(this);
            */

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DisplayAssetInfo(false);
        }
    }
}
