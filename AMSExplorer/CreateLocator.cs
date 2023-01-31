//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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


using Azure.ResourceManager.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class CreateLocator : Form
    {
        private readonly List<MediaAssetResource> _SelectedAssets;
        private readonly AMSClientV3 _client;

        public DateTime? LocatorStartDate
        {
            get => (checkBoxStartDate.Checked) ? dateTimePickerStartDate.Value.ToUniversalTime() : null;
            set
            {
                dateTimePickerStartDate.Value = ((DateTime)value).ToLocalTime();
                dateTimePickerStartTime.Value = ((DateTime)value).ToLocalTime();
            }
        }

        public bool LocatorHasStartDate
        {
            get => checkBoxStartDate.Checked;
            set => checkBoxStartDate.Checked = value;
        }

        public DateTime LocatorEndDate
        {
            get
            {
                if (radioButtonEndCustom.Checked)
                {
                    return dateTimePickerEndDate.Value.ToUniversalTime();
                }
                else if (radioButtonEndYear.Checked)
                {
                    return DateTime.UtcNow.AddYears(1);
                }
                else
                {
                    return DateTime.UtcNow.AddYears(100);
                }
            }
            set
            {
                dateTimePickerEndDate.Value = value.ToLocalTime();
                dateTimePickerEndTime.Value = value.ToLocalTime();
            }
        }


        public string ForceLocatorGuid
        {
            get
            {
                if (checkBoxForLocatorGUID.Checked)
                {
                    return textBoxLocatorGUID.Text;
                }
                else
                {
                    return null;
                }
            }
        }

        public string LocAssetName
        {
            set => labelAssetName.Text = value;
        }

        public string LocWarning
        {
            set => labelWarning.Text = value;
        }

        public string StreamingPolicyName
        {
            get
            {
                if (radioButtonDownload.Checked)
                {
                    return "Predefined_DownloadOnly";
                }
                else if (radioButtonDownloadAndClear.Checked)
                {
                    return "Predefined_DownloadAndClearStreaming";
                }
                else if (radioButtonClearKey.Checked)
                {
                    return "Predefined_ClearKey";
                }
                else if (radioButtonMultiDRMCENC.Checked)
                {
                    return "Predefined_MultiDrmCencStreaming";
                }
                else if (radioButtonMultiDRM.Checked)
                {
                    return "Predefined_MultiDrmStreaming";
                }
                else
                {
                    return "Predefined_ClearStreamingOnly";
                }
            }
        }



        public CreateLocator(AMSClientV3 client, List<MediaAssetResource> SelectedAssets, bool extendlocator = false)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _SelectedAssets = SelectedAssets;
            _client = client;
            if (extendlocator) // dialog box used to extend locator expiration date
            {
                buttonOk.Text = AMSExplorer.Properties.Resources.CreateLocator_CreateLocator_UpdateLocatorS;
                Text = AMSExplorer.Properties.Resources.CreateLocator_CreateLocator_UpdateLocators2;
                groupBoxStart.Enabled = false; // do not propose to specificy start date
            }
        }

        private void DateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
        }

        private void DateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
        }

        private void DateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
        }

        private void DateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
        }

        private void CheckBoxStartDate_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = checkBoxStartDate.Checked;
            dateTimePickerStartTime.Enabled = checkBoxStartDate.Checked;
        }

        public List<string> SelectedFilters
        {
            get
            {
                List<string> list = new();
                foreach (object f in listViewFilters.CheckedItems)
                {
                    string v = (f as ListViewItem).SubItems[1].Text;
                    if (v != string.Empty)
                    {
                        list.Add(v);
                    }
                }
                return list.Count > 0 ? list : null;
            }
        }

        private async void CreateLocator_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            // Filters
            List<string> afiltersnames = new();

            listViewFilters.BeginUpdate();
            // asset filters
            if (_SelectedAssets.Count == 1)
            {
                labelNoAssetFilter.Visible = false;

                List<MediaAssetFilterResource> assetFilters = new();

                var filters = _SelectedAssets.First().GetMediaAssetFilters().GetAllAsync();


                await foreach (var filter in filters)
                {
                    ListViewItem lvitem = new(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_AssetFilter + filter.Data.Name, filter.Data.Name });
                    listViewFilters.Items.Add(lvitem);
                    afiltersnames.Add(filter.Data.Name);
                }
            }

            // account filters
            var acctFilters = _client.AMSclient.GetMediaServicesAccountFilters().GetAllAsync();
            await foreach (var filter in acctFilters)
            {
                ListViewItem lvitem = new(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_GlobalFilter + filter.Data.Name, filter.Data.Name });

                if (afiltersnames.Contains(filter.Data.Name)) // global filter with same name than asset filter
                {
                    lvitem.ForeColor = System.Drawing.Color.Gray;
                }
                listViewFilters.Items.Add(lvitem);
            }

            listViewFilters.EndUpdate();
        }

        private void RadioButtonEndCustom_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Enabled = radioButtonEndCustom.Checked;
            dateTimePickerEndTime.Enabled = radioButtonEndCustom.Checked;
        }

        private void UpdateLocatorGUID_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxEnd.Visible =
            groupBoxStart.Visible =
            textBoxLocatorGUID.Enabled = checkBoxForLocatorGUID.Checked;
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void RadioButtonMultiDRM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RadioButtonMultiDRMCENC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RadioButtonClearKey_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CreateLocator_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label5, e);
        }

        private void CreateLocator_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
