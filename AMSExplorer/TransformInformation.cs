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

using AMSExplorer.Rest;
using Microsoft.Azure.Management.Media.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class TransformInformation : Form
    {
        private readonly Transform _transform;
        private readonly TransformRestObject _transformRest;
        public IEnumerable<StreamingEndpoint> MyStreamingEndpoints;

        public TransformInformation(Transform transform, TransformRestObject transformRest)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _transform = transform;
            _transformRest = transformRest;
        }

        private void ContextMenuStrip_MouseClick(object sender, MouseEventArgs e)
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

        private void TransformInformation_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            labelJobNameTitle.Text += _transform.Name;

            DGTransform.ColumnCount = 2;
            DGOutputs.ColumnCount = 2;
            DGOutputs.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;

            DGTransform.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;
            DGTransform.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Name, _transform.Name);
            DGTransform.Rows.Add("Description", _transform.Description);
            DGTransform.Rows.Add("Id", _transform.Id);
            DGTransform.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_Created, _transform.Created.ToLocalTime().ToString("G"));
            DGTransform.Rows.Add(AMSExplorer.Properties.Resources.AssetInformation_AssetInformation_Load_LastModified, _transform.LastModified.ToLocalTime().ToString("G"));

            bool boutoutsintransform = (_transform.Outputs.Count > 0);

            int index = 0;
            if (boutoutsintransform)
            {
                foreach (TransformOutput output in _transform.Outputs)
                {
                    // listBoxTasks.Items.Add(output..Name ?? Constants.stringNull);
                    string outputLabel = "output #" + index;
                    listBoxOutputs.Items.Add(outputLabel);
                    index++;
                }
                listBoxOutputs.SelectedIndex = 0;
            }
        }

        private void ListBoxOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransformOutput output = _transform.Outputs.Skip(listBoxOutputs.SelectedIndex).Take(1).FirstOrDefault();
            DGOutputs.Rows.Clear();

            DGOutputs.Rows.Add("Preset type", output.Preset.GetType().ToString());

            var presetRest = _transformRest.Properties.Outputs.Skip(listBoxOutputs.SelectedIndex).Take(1).FirstOrDefault().Preset;
            string presetJson = JsonConvert.SerializeObject(presetRest, Formatting.Indented);
            /*
            if (output.Preset.GetType() == typeof(BuiltInStandardEncoderPreset))
            {
                BuiltInStandardEncoderPreset pmes = (BuiltInStandardEncoderPreset)output.Preset;
                presetJson = JsonConvert.SerializeObject(pmes, Newtonsoft.Json.Formatting.Indented);
            }
            else if (output.Preset.GetType() == typeof(AudioAnalyzerPreset))
            {
                AudioAnalyzerPreset pmes = (AudioAnalyzerPreset)output.Preset;
                presetJson = JsonConvert.SerializeObject(pmes, Newtonsoft.Json.Formatting.Indented);
            }
            else if (output.Preset.GetType() == typeof(StandardEncoderPreset))
            {
                StandardEncoderPreset pmes = (StandardEncoderPreset)output.Preset;
                presetJson = JsonConvert.SerializeObject(pmes, Newtonsoft.Json.Formatting.Indented);
            }
            else if (output.Preset.GetType() == typeof(VideoAnalyzerPreset))
            {
                VideoAnalyzerPreset pmes = (VideoAnalyzerPreset)output.Preset;
                presetJson = JsonConvert.SerializeObject(pmes, Newtonsoft.Json.Formatting.Indented);
            }
            else if (output.Preset.GetType() == typeof(FaceDetectorPreset))
            {
                FaceDetectorPreset pmes = (FaceDetectorPreset)output.Preset;
                presetJson = JsonConvert.SerializeObject(pmes, Newtonsoft.Json.Formatting.Indented);
            }
            else
            {
                presetJson = JsonConvert.SerializeObject(output.Preset, Newtonsoft.Json.Formatting.Indented);
            }
            */
            textBoxPresetJson.Text = presetJson;
            DGOutputs.Rows.Add("Relative Priority", output.RelativePriority);
        }

        private void DGTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;
            if (e.RowIndex >= 0 && senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {
                SeeValueInEditor(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString(), senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString());
            }
        }

        private void SeeValueInEditor(string dataname, string key)
        {
            EditorXMLJSON editform = new(dataname, key, false);
            editform.Display();
        }

        private void AssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DisplayAssetInfo(false);
        }

        private void TransformInformation_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            // DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelJobNameTitle, contextMenuStrip, contextMenuStripInputAsset, contextMenuStripOutputAsset }, e, this);
        }
    }
}