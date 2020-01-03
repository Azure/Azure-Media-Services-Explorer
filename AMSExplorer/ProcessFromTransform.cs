//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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

using Microsoft.Azure.Management.Media.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class ProcessFromTransform : Form
    {
        private readonly AMSClientV3 _client;
        private readonly List<Transform> _listPreSelectedTransforms;
        private readonly int _numberselectedassets = 0;
        private readonly List<Asset> _listAssets;
        private readonly AMSExplorer.Mainform _myMainform;
        private readonly TimeSpan? _start;
        private readonly TimeSpan? _end;

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
        {
            get => radioButtonExistingAsset.Checked ? listViewAssets1.GetSelectedAsset : null;

        }


        public ProcessFromTransform(AMSClientV3 client, AMSExplorer.Mainform myMainForm, List<Asset> listAssets = null, List<Transform> listPreSelectedTransforms = null, TimeSpan? start = null, TimeSpan? end = null, bool noHttpSourceMode = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _client = client;
            _listPreSelectedTransforms = listPreSelectedTransforms;

            if (listAssets == null || listAssets.Count == 0)
            {
                radioButtonHttpSource.Checked = true;
                radioButtonSelectedAssets.Enabled = false;
            }
            else
            {
                _numberselectedassets = listAssets.Count;
            }

            if (noHttpSourceMode)
            {
                radioButtonHttpSource.Enabled = false;
            }

            //buttonJobOptions.Initialize(_context);

            _listAssets = listAssets;
            _myMainform = myMainForm;
            _start = start;
            _end = end;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteTemplate.Enabled = listViewTransforms.SelectedItems.Count > 0;
            UpdateStatusButtonOk();
        }

        private void UpdateStatusButtonOk(bool additionalCondition = true)
        {
            buttonOk.Enabled = listViewTransforms.SelectedItems.Count > 0 && additionalCondition;

        }

        private async void ProcessFromJobTemplate_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(panel1);

            await listViewTransforms.LoadTransformsAsync(_client, _listPreSelectedTransforms?.FirstOrDefault().Name);
            UpdateLabeltext();
            labelURLFileNameWarning.Text = string.Empty;
            UpdateStatusButtonOk();

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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void buttonDeleteTemplate_Click(object sender, EventArgs e)
        {
            await listViewTransforms.DeleteSelectedTemplateAsync();
        }

        private void timeControlStartTime_ValueChanged(object sender, EventArgs e)
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
        }

        private void radioButtonHttpSource_CheckedChanged(object sender, EventArgs e)
        {
            textBoxURL.Enabled = radioButtonHttpSource.Checked;
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
            bool Error = false;
            try
            {
                Uri myUri = GetURL;
            }
            catch
            {
                Error = true;
                labelURLFileNameWarning.Text = AMSExplorer.Properties.Resources.ImportHttp_textBoxURL_TextChanged_ErrorDetectedInTheURL;
                UpdateStatusButtonOk(false);
                return;
            }

            UpdateStatusButtonOk(SelectedTransform != null);

            if (!Error)
            {
                labelURLFileNameWarning.Text = string.Empty;
            }
        }

        private async void ButtonCreateNewTransform_Click(object sender, EventArgs e)
        {
            string transformName = null;
            TransformTypeCreation form = new TransformTypeCreation();
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                switch (form.TransformType)
                {
                    case simpleTransformType.analyze:
                        transformName = await _myMainform.CreateVideoAnalyzerTransformAsync();
                        break;

                    case simpleTransformType.encode:
                        transformName = await _myMainform.CreateStandardEncoderTransformAsync();
                        break;

                    case simpleTransformType.facedetection:
                        transformName = await _myMainform.CreateFaceDetectorTransformAsync();
                        break;
                }
            }

            await listViewTransforms.LoadTransformsAsync(_client, transformName);
        }

        private void ProcessFromTransform_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelTitle, timeControlStartTime, timeControlEndTime }, e, this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesAfterDpiChange(panel1, e);
        }

        private async void radioButtonExistingAsset_CheckedChanged(object sender, EventArgs e)
        {
            panelSelectAsset.Enabled = radioButtonExistingAsset.Checked;

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
    }
}
