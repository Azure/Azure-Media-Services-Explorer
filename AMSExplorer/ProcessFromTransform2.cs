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
using System.Linq;
using System.Windows.Forms;
using Microsoft.Azure.Management.Media.Models;

namespace AMSExplorer
{
    public partial class ProcessFromTransform2 : Form
    {
        private AMSClientV3 _client;
        private List<Transform> _listPreSelectedTransforms;
        private int _numberselectedassets = 0;
        private List<Asset> _listAssets;

        public Transform SelectedTransform
        {
            get
            {
                return listViewTransforms.GetSelectedTransform;

            }
        }

        public bool SelectedAssetsMode
        {
            get
            {
                return radioButtonSelectedAssets.Checked;

            }
        }

        public bool HttpSourceMode
        {
            get
            {
                return radioButtonHttpSource.Checked;

            }
        }

        public Uri GetURL
        {
            get
            {
                return radioButtonHttpSource.Checked ? new Uri(textBoxURL.Text) : null;
            }

            set
            {
                textBoxURL.Text = value.ToString();
            }
        }



        public ClipTime StartClipTime
        {
            get
            {
                return checkBoxSourceTrimmingStart.Checked ?

                    new AbsoluteClipTime()
                    {
                        Time = timeControlStartTime.TimeStampWithoutOffset
                    }
                    :
                    null;
            }
        }

        public ClipTime EndClipTime
        {
            get
            {
                return checkBoxSourceTrimmingEnd.Checked ?

                   new AbsoluteClipTime()
                   {
                       Time = timeControlEndTime.TimeStampWithoutOffset
                   }
                   :
                   null;
            }
        }


        public ProcessFromTransform2(AMSClientV3 client, List<Asset> listAssets = null, List<Transform> listPreSelectedTransforms = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
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

            //buttonJobOptions.Initialize(_context);

            this.Text = "Template based processing";
            _listAssets = listAssets;
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

        private void ProcessFromJobTemplate_Load(object sender, EventArgs e)
        {
            listViewTransforms.LoadTransforms(_client, _listPreSelectedTransforms?.FirstOrDefault().Name);
            UpdateLabeltext();
            labelURLFileNameWarning.Text = string.Empty;
            UpdateStatusButtonOk();

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void buttonDeleteTemplate_Click(object sender, EventArgs e)
        {

            listViewTransforms.DeleteSelectedTemplate();
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
                Uri myUri = this.GetURL;
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
    }
}
