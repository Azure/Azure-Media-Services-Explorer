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
    public partial class TransformInformation : Form
    {
        public Transform MyTransform;
        private AzureMediaServicesClient _client;
        private Mainform _mainform;
        public IEnumerable<StreamingEndpoint> MyStreamingEndpoints;

        public TransformInformation(Mainform mainform, AzureMediaServicesClient client)
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
            labelJobNameTitle.Text += MyTransform.Name;

            DGTransform.ColumnCount = 2;
            DGOutputs.ColumnCount = 2;
            DGOutputs.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            DGErrors.ColumnCount = 3;
            DGErrors.Columns[0].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Task;
            DGErrors.Columns[1].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_ErrorDetail;
            DGErrors.Columns[2].HeaderText = AMSExplorer.Properties.Resources.JobInformation_JobInformation_Load_Code;

            DGTransform.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGTransform.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, MyTransform.Name);
            DGTransform.Rows.Add("Description", MyTransform.Description);
            DGTransform.Rows.Add("Id", MyTransform.Id);

            DGTransform.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, ((DateTime)MyTransform.Created).ToLocalTime().ToString("G"));
            DGTransform.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, ((DateTime)MyTransform.LastModified).ToLocalTime().ToString("G"));

           
           

            bool boutoutsinjobs = (MyTransform.Outputs.Count() > 0);

            int index = 1;
            if (boutoutsinjobs)
            {
                foreach (var output in MyTransform.Outputs)
                {
                    // listBoxTasks.Items.Add(output..Name ?? Constants.stringNull);
                    var outputLabel = "output #" + index;
                    listBoxOutputs.Items.Add(outputLabel);
                


                }
                listBoxOutputs.SelectedIndex = 0;
            }

            ListJobAssets();
        }

        private void ListJobAssets()
        {
           

            listViewOutputs.BeginUpdate();
            try
            {
                int index = 1;
                foreach (var output in MyTransform.Outputs)
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
            var output = MyTransform.Outputs.Skip(listBoxOutputs.SelectedIndex).Take(1).FirstOrDefault();

            DGOutputs.Rows.Clear();

            //  DGTasks.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, task.Name);

           /*
            int i = DGOutputs.Rows.Add("Preset", "");
            DataGridViewButtonCell btn = new DataGridViewButtonCell();
            DGOutputs.Rows[i].Cells[1] = btn;
            DGOutputs.Rows[i].Cells[1].Value = "see preset";
            DGOutputs.Rows[i].Cells[1].Tag = output.Preset;
            */

            /*
            i = DGOutputs.Rows.Add(AMSExplorer.Properties.Resources.JobInformation_listBoxTasks_SelectedIndexChanged_Body, "");
            btn = new DataGridViewButtonCell();
            DGOutputs.Rows[i].Cells[1] = btn;
            DGOutputs.Rows[i].Cells[1].Value = AMSExplorer.Properties.Resources.AssetInformation_DoDisplayAuthorizationPolicyOption_SeeValue;
            DGOutputs.Rows[i].Cells[1].Tag = task.TaskBody;
            */
            DGOutputs.Rows.Add("Preset type", output.Preset.GetType().ToString());
            
          
            if (output.Preset.GetType() == typeof(BuiltInStandardEncoderPreset))
            {
                var pmes = (BuiltInStandardEncoderPreset) output.Preset;
                DGOutputs.Rows.Add("Preset name", pmes.PresetName);
            }
            else if (output.Preset.GetType() == typeof(AudioAnalyzerPreset))
            {
                var pmes = (AudioAnalyzerPreset)output.Preset;
                DGOutputs.Rows.Add("Audio language", pmes.AudioLanguage);
            }
            else if (output.Preset.GetType() == typeof(StandardEncoderPreset))
            {
                var pmes = (StandardEncoderPreset)output.Preset;
               // DGOutputs.Rows.Add("Audio language", pmes.);
            }
            else if (output.Preset.GetType() == typeof(VideoAnalyzerPreset))
            {
                var pmes = (VideoAnalyzerPreset)output.Preset;
                 DGOutputs.Rows.Add("Audio language", pmes.AudioLanguage);
                DGOutputs.Rows.Add("Audio Insights Only", pmes.AudioInsightsOnly);
            }


            DGOutputs.Rows.Add("Relative Priority", output.RelativePriority);


         
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
