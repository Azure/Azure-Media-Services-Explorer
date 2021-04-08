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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class JobSubmitFromTransform : Form
    {
        private readonly AMSClientV3 _client;
        private readonly List<Transform> _listPreSelectedTransforms;
        private readonly int _numberselectedassets = 0;
        private List<Asset> _listAssets;
        private readonly AMSExplorer.Mainform _myMainform;
        private readonly TimeSpan? _start;
        private readonly TimeSpan? _end;
        private readonly bool _multipleInputAssets;

        private BindingList<EDLEntryInOut> TimeCodeList = new BindingList<EDLEntryInOut>();
        public delegate void ChangedEventHandler(object sender, EventArgs e);

        public Transform SelectedTransform => listViewTransforms.GetSelectedTransform;

        public bool SelectedAssetsMode => radioButtonSelectedAssets.Checked;

        public bool HttpSourceMode => radioButtonHttpSource.Checked;

        public Uri GetURL
        {
            get => radioButtonHttpSource.Checked ? new Uri(textBoxURL.Text) : null;

            set => textBoxURL.Text = value.ToString();
        }

        public ClipTime StartClipTime => checkBoxSourceTrimmingStart.Checked ?

                    new AbsoluteClipTime()
                    {
                        Time = timeControlStartTime.TimeStampWithoutOffset
                    }
                    :
                    null;

        public ClipTime EndClipTime => checkBoxSourceTrimmingEnd.Checked ?

                   new AbsoluteClipTime()
                   {
                       Time = timeControlEndTime.TimeStampWithoutOffset
                   }
                   :
                   null;

        /// <summary>
        /// Return the selected asset object. Null if no asset.
        /// </summary>
        public Asset ExistingOutputAsset  // null if no asset
=> radioButtonExistingAsset.Checked ? listViewAssets1.GetSelectedAsset : null;

        public string OutputAssetNameSyntax  // null if no asset
=> !radioButtonExistingAsset.Checked ? textBoxNewAssetNameSyntax.Text : null;


        public JobInputSequence InputSequence
        {
            get
            {
                if (GetEDLEntries().Count == 0) return null; // maybe several assets, but not multiple assets for one job

                var myJobInputAsset = new List<JobInputAsset>();
                foreach (var entry in GetEDLEntries())
                {
                    var input = new JobInputAsset(
                        assetName: entry.AssetName,
                        start: entry.Start != null ? new AbsoluteClipTime((TimeSpan)entry.Start) : null,
                        end: entry.End != null ? new AbsoluteClipTime((TimeSpan)entry.End) : null
                        );
                    myJobInputAsset.Add(input);

                }
                return new JobInputSequence(inputs: myJobInputAsset.ToArray());
            }
        }


        public JobSubmitFromTransform(AMSClientV3 client, AMSExplorer.Mainform myMainForm, List<Asset> listAssets = null, List<Transform> listPreSelectedTransforms = null, TimeSpan? absoluteStart = null, TimeSpan? absoluteEnd = null, bool noHttpSourceMode = false, bool multipleInputAssets = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _client = client;
            _listPreSelectedTransforms = listPreSelectedTransforms;

            textBoxNewAssetNameSyntax.Text = Constants.NameconvInputasset + "-" + Constants.NameconvTransform + "-" + Constants.NameconvShortUniqueness;

            if (listAssets == null || listAssets.Count == 0)
            {
                radioButtonHttpSource.Checked = true;
                radioButtonSelectedAssets.Enabled = false;
            }
            else
            {
                _numberselectedassets = listAssets.Count;
                textBoxNewAssetNameSyntax.Text = Constants.NameconvInputasset + "-" + Constants.NameconvTransform + "-" + Constants.NameconvShortUniqueness;
            }

            if (noHttpSourceMode)
            {
                radioButtonHttpSource.Enabled = false;
            }

            //buttonJobOptions.Initialize(_context);

            _listAssets = listAssets;
            _myMainform = myMainForm;

            _start = absoluteStart;
            _end = absoluteEnd;

            _multipleInputAssets = multipleInputAssets;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {

        }

        private void Listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteTemplate.Enabled = listViewTransforms.SelectedItems.Count > 0;
            UpdateStatusButtonOk();
        }

        private void UpdateStatusButtonOk(bool additionalCondition = true)
        {
            buttonOk.Enabled = listViewTransforms.SelectedItems.Count > 0 && additionalCondition;

        }

        private async void JobSubmitFromTransform_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            dataGridViewEDL.DataSource = TimeCodeList;

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);

            await listViewTransforms.LoadTransformsAsync(_client, _listPreSelectedTransforms?.FirstOrDefault()?.Name);
            UpdateLabeltext();
            labelURLFileNameWarning.Text = string.Empty;
            UpdateStatusButtonOk();

            if (_listAssets != null) // we are not in http source only mode
            {
                if (_listAssets.Count > 1 && !_multipleInputAssets) // several jobs, one input asset per job
                {
                    comboBoxSourceAsset.Items.Add(new Item("(multiple assets were selected)", null));
                    comboBoxSourceAsset.Enabled = false;
                    buttonDelEntry.Visible = buttonUp.Visible = buttonDown.Visible = buttonAddEDLEntry.Visible = dataGridViewEDL.Visible = false;
                }
                else
                {
                    foreach (var a in _listAssets)
                    {
                        comboBoxSourceAsset.Items.Add(a.Name);
                        AddEDLEntry(new EDLEntryInOut()
                        {
                            AssetName = a.Name,
                            Start = _start,
                            End = _end,
                            Description = a.Description
                        });
                    }
                }
                comboBoxSourceAsset.SelectedIndex = 0;

                if (_start != null)
                {
                    timeControlStartTime.SetTimeStamp((TimeSpan)_start);
                    checkBoxSourceTrimmingStart.CheckState = CheckState.Checked;
                }
                if (_end != null)
                {
                    timeControlEndTime.SetTimeStamp((TimeSpan)_end);
                    checkBoxSourceTrimmingEnd.CheckState = CheckState.Checked;
                }

                if (!_multipleInputAssets)
                {
                    labelInfoSeveralAssetStitching.Visible = true;
                }
            }


        }


        private async void ButtonDeleteTemplate_Click(object sender, EventArgs e)
        {
            await listViewTransforms.DeleteSelectedTemplateAsync();
        }

        private void TimeControlStartTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateDurationText();
        }

        private void UpdateDurationText()
        {
            if (checkBoxSourceTrimmingStart.CheckState == CheckState.Checked)
            {
                if (checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked)
                {
                    textBoxSourceDurationTime.Text = (timeControlEndTime.TimeStampWithOffset - timeControlStartTime.TimeStampWithOffset).ToString();
                }
                else
                {
                    textBoxSourceDurationTime.Text = string.Empty;
                }
            }
            else if (checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked) // only end time specified
            {
                textBoxSourceDurationTime.Text = (timeControlEndTime.TimeStampWithOffset - timeControlStartTime.GetOffSetAsTimeSpan()).ToString();
            }
        }

        private void timeControlEndTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateDurationText();
        }


        private void checkBoxSourceTrimmingEnd_CheckStateChanged(object sender, EventArgs e)
        {
            timeControlEndTime.Enabled = textBoxSourceDurationTime.Enabled = (checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked);
        }

        private void checkBoxSourceTrimmingStart_CheckStateChanged(object sender, EventArgs e)
        {
            timeControlStartTime.Enabled = (checkBoxSourceTrimmingStart.CheckState == CheckState.Checked);
            //buttonAddEDLEntry.Enabled = (checkBoxUseEDL.CheckState == CheckState.Checked);
        }

        private void radioButtonHttpSource_CheckedChanged(object sender, EventArgs e)
        {
            textBoxURL.Enabled = radioButtonHttpSource.Checked;

            if (radioButtonHttpSource.Checked)
            {
                textBoxNewAssetNameSyntax.Text = "httpssource-" + Constants.NameconvTransform + "-" + Constants.NameconvShortUniqueness;

                // no way to access the EDL
                comboBoxSourceAsset.Visible = buttonDelEntry.Visible = buttonUp.Visible = buttonDown.Visible = buttonAddEDLEntry.Visible = dataGridViewEDL.Visible = labelInputAsset.Visible = textBoxAssetDescription.Visible = labelAssetDescription.Visible = false;
            }
            else
            {
                textBoxNewAssetNameSyntax.Text = Constants.NameconvInputasset + "-" + Constants.NameconvTransform + "-" + Constants.NameconvShortUniqueness;

                comboBoxSourceAsset.Visible = buttonDelEntry.Visible = buttonUp.Visible = buttonDown.Visible = buttonAddEDLEntry.Visible = dataGridViewEDL.Visible = labelInputAsset.Visible = textBoxAssetDescription.Visible = labelAssetDescription.Visible = true;
            }

            if (radioButtonSelectedAssets.Checked)
            {
                UpdateStatusButtonOk(SelectedTransform != null);
            }
            else
            {
                UpdateStatusButtonOk();
            }
            UpdateLabeltext();
        }

        private void UpdateLabeltext()
        {
            if (SelectedAssetsMode)
            {
                label.Text = (_listAssets.Count > 1) ? string.Format("{0} assets have been selected. 1 job will be submitted.", _listAssets.Count) : string.Format("Asset '{0}' will be encoded.", _listAssets.FirstOrDefault().Name);
            }
            else // http source mode
            {
                label.Text = "One http(s) source has been selected. 1 job will be submitted.";
            }
        }

        private void textBoxURL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Uri myUri = GetURL;
            }
            catch
            {
                labelURLFileNameWarning.Text = AMSExplorer.Properties.Resources.ImportHttp_textBoxURL_TextChanged_ErrorDetectedInTheURL;
                UpdateStatusButtonOk(false);
                return;
            }

            UpdateStatusButtonOk(SelectedTransform != null);
            labelURLFileNameWarning.Text = string.Empty;
        }

        private async void ButtonCreateNewTransform_Click(object sender, EventArgs e)
        {
            string transformName = await _myMainform.DoCreateOrUpdateATransformAsync();
            await listViewTransforms.LoadTransformsAsync(_client, transformName);
        }

        private void JobSubmitFromTransform_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelTitle, timeControlStartTime, timeControlEndTime }, e, this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesAfterDpiChange(panel1, e);
        }

        private async void radioButtonExistingAsset_CheckedChanged(object sender, EventArgs e)
        {
            listViewAssets1.Enabled = labelSelectAsset.Enabled = textBoxExactAssetName.Enabled = buttonSearchExactAssetName.Enabled = radioButtonExistingAsset.Checked;
            textBoxNewAssetNameSyntax.Enabled = radioButtonNewAsset.Checked;

            if (radioButtonExistingAsset.Checked)
            {
                // let's list the asset
                await listViewAssets1.LoadAssetsAsync(_client);
            }
            else
            {
            }
        }

        private async void buttonSearchExactAssetName_Click(object sender, EventArgs e)
        {
            if (radioButtonExistingAsset.Checked)
            {
                string searchName = string.IsNullOrWhiteSpace(textBoxExactAssetName.Text) ? null : textBoxExactAssetName.Text;
                // let's list the asset
                await listViewAssets1.LoadAssetsAsync(_client, searchName);
            }
            else
            {

            }
        }

        private void buttonAddEDLEntry_Click(object sender, EventArgs e)
        {
            string assetName = comboBoxSourceAsset.Text;
            AddEDLEntry(new EDLEntryInOut()
            {
                Start = (checkBoxSourceTrimmingStart.CheckState == CheckState.Checked) ? timeControlStartTime.TimeStampWithoutOffset : (TimeSpan?)null,
                End = (checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked) ? timeControlEndTime.TimeStampWithoutOffset : (TimeSpan?)null,
                AssetName = assetName,
                Description = _listAssets.Where(a => a.Name == assetName).First().Description
            });
            return;

            /*
            if ((checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked) && !(checkBoxSourceTrimmingStart.CheckState == CheckState.Checked))
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.EncodingMES_buttonAddEDLEntry_Click_YouCannotSpecifyOnlyAnEndTime, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (checkBoxSourceTrimmingStart.CheckState == CheckState.Checked)
            {

                AddEDLEntry(new EDLEntryInOut()
                {
                    Start = timeControlStartTime.TimeStampWithoutOffset,
                    End = (checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked) ? timeControlEndTime.TimeStampWithoutOffset : (TimeSpan?)null,
                    AssetName = ((Item)comboBoxSourceAsset.SelectedItem).Value,
                });
            }
            else if (!(checkBoxSourceTrimmingStart.CheckState == CheckState.Checked) && !(checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked))
            {
                AddEDLEntry(new EDLEntryInOut()
                {
                    Start = null,
                    AssetName = ((Item)comboBoxSourceAsset.SelectedItem).Value,
                });
            }
            */
        }

        private void checkBoxSourceTrimmingEnd_CheckedChanged(object sender, EventArgs e)
        {
            timeControlEndTime.Enabled = textBoxSourceDurationTime.Enabled = (checkBoxSourceTrimmingEnd.CheckState == CheckState.Checked);
            //buttonAddEDLEntry.Enabled = (checkBoxUseEDL.CheckState == CheckState.Checked);
        }

        private void dataGridViewEDL_SelectionChanged(object sender, EventArgs e)
        {
            buttonDelEntry.Enabled = dataGridViewEDL.SelectedRows.Count > 0;
            buttonUp.Enabled = dataGridViewEDL.SelectedRows.Count > 0 && dataGridViewEDL.SelectedRows[0].Index > 0;
            buttonDown.Enabled = dataGridViewEDL.SelectedRows.Count > 0 && dataGridViewEDL.SelectedRows[0].Index < dataGridViewEDL.Rows.Count - 1;
        }



        public void AddEDLEntry(EDLEntryInOut entry)
        {
            TimeCodeList.Add(entry);
        }

        public List<EDLEntryInOut> GetEDLEntries()
        {

            return TimeCodeList.ToList();
        }

        public void SetEDLEntries(List<EDLEntryInOut> list)
        {

            TimeCodeList = new BindingList<EDLEntryInOut>(list);

        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (dataGridViewEDL.SelectedRows.Count == 1 && dataGridViewEDL.SelectedRows[0].Index > 0)
            {
                int index = dataGridViewEDL.SelectedRows[0].Index;
                EDLEntryInOut backup = TimeCodeList[index - 1];
                TimeCodeList[index - 1] = TimeCodeList[index];
                TimeCodeList[index] = backup;
                dataGridViewEDL.ClearSelection();
                dataGridViewEDL.Rows[index - 1].Selected = true;

            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (dataGridViewEDL.SelectedRows.Count == 1 && dataGridViewEDL.SelectedRows[0].Index < TimeCodeList.Count - 1)
            {
                int index = dataGridViewEDL.SelectedRows[0].Index;
                EDLEntryInOut backup = TimeCodeList[index + 1];
                TimeCodeList[index + 1] = TimeCodeList[index];
                TimeCodeList[index] = backup;
                dataGridViewEDL.ClearSelection();
                dataGridViewEDL.Rows[index + 1].Selected = true;

            }
        }

        private void buttonDelEntry_Click(object sender, EventArgs e)
        {
            if (dataGridViewEDL.SelectedRows.Count == 1)
            {
                TimeCodeList.RemoveAt(dataGridViewEDL.SelectedRows[0].Index);
            }
        }

        private void comboBoxSourceAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            string assetName = comboBoxSourceAsset.Text;

            if (assetName != null)
            {
                var asset = _listAssets.Where(a => a.Name == assetName).FirstOrDefault();
                if (asset != null)
                {

                    textBoxAssetDescription.Text = _listAssets.Where(a => a.Name == assetName).First()?.Description;
                }
            }
        }

        private async void comboBoxSourceAsset_TextChanged(object sender, EventArgs e)
        {
            string assetName = comboBoxSourceAsset.Text;

            if (assetName != null)
            {
                var asset = _listAssets.Where(a => a.Name == assetName).FirstOrDefault();
                if (asset == null)
                {
                    try
                    {
                        asset = await _client.AMSclient.Assets.GetAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, assetName).ConfigureAwait(false);
                        _listAssets.Add(asset);
                        updateAssetDescription(asset.Description);
                    }
                    catch
                    {
                        updateAssetDescription("(no description, asset not found)");
                    }
                }
                else
                {
                    updateAssetDescription(_listAssets.Where(a => a.Name == assetName).First()?.Description);
                }

            }
        }

        private void updateAssetDescription(string desc)
        {
            if (textBoxAssetDescription.InvokeRequired)
            {
                textBoxAssetDescription.BeginInvoke(new Action(() =>
            {
                textBoxAssetDescription.Text = desc;
            }));
            }
            else
            {
                textBoxAssetDescription.Text = desc;
            }
        }

        private void buttonExportEDL_Click(object sender, EventArgs e)
        {
            PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ContractResolver = jsonResolver
            };

            DialogResult diares = saveFileDialog1.ShowDialog();
            if (diares == DialogResult.OK)
            {
                EDLImportExport export = new EDLImportExport();
                export.AMSE_EDL_Entries.AddRange(TimeCodeList);
                try
                {

                    System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(export, settings));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonImportEDL_Click(object sender, EventArgs e)
        {
            DialogResult diares = openFileDialog1.ShowDialog();
            if (diares == DialogResult.OK)
            {
                string json = System.IO.File.ReadAllText(openFileDialog1.FileName);

                EDLImportExport EDLImportExport = null;
                try
                {
                    EDLImportExport = (EDLImportExport)JsonConvert.DeserializeObject(json, typeof(EDLImportExport));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error when importing json file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TimeCodeList.Clear();

                /*
                if (ImportedCredentialList.Version < (new ListCredentialsRPv3()).Version)
                {
                    MessageBox.Show("This file was created with an older version of AMSE. Import is not possible.", "Wrong version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                */

                foreach (var entry in EDLImportExport.AMSE_EDL_Entries)
                {
                    TimeCodeList.Add(entry);
                }
            }
        }
    }


    public class EDLImportExport
    {
        public decimal Version = 1;
        public List<EDLEntryInOut> AMSE_EDL_Entries = new List<EDLEntryInOut>();
    }
}