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
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;

namespace AMSExplorer
{
    public partial class CreateLocator : Form
    {
        private List<Asset> _SelectedAssets;
        private AMSClientV3 _client;

        public DateTime? LocatorStartDate
        {
            get
            {
                return (checkBoxStartDate.Checked) ? (DateTime)dateTimePickerStartDate.Value.ToUniversalTime() : (Nullable<DateTime>)null;
            }
            set
            {
                dateTimePickerStartDate.Value = ((DateTime)value).ToLocalTime();
                dateTimePickerStartTime.Value = ((DateTime)value).ToLocalTime();
            }
        }

        public bool LocatorHasStartDate
        {
            get { return checkBoxStartDate.Checked; }
            set { checkBoxStartDate.Checked = value; }
        }

        public DateTime LocatorEndDate
        {
            get
            {
                if (radioButtonEndCustom.Checked) return dateTimePickerEndDate.Value.ToUniversalTime();
                else if (radioButtonEndYear.Checked) return DateTime.UtcNow.AddYears(1);
                else return DateTime.UtcNow.AddYears(100);
            }
            set
            {
                dateTimePickerEndDate.Value = ((DateTime)value).ToLocalTime();
                dateTimePickerEndTime.Value = ((DateTime)value).ToLocalTime();
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
            set
            {
                labelAssetName.Text = value;
            }
        }

        public string LocWarning
        {
            set
            {
                labelWarning.Text = value;
            }
        }

        public string StreamingPolicyName
        {
            get
            {
                if (radioButtonClearStream.Checked)
                {
                    return PredefinedStreamingPolicy.ClearStreamingOnly;
                }
                else if (radioButtonDownload.Checked)
                {
                    return PredefinedStreamingPolicy.DownloadOnly;
                }
                else if (radioButtonDownloadAndClear.Checked)
                {
                    return PredefinedStreamingPolicy.DownloadAndClearStreaming;
                }
                else if (radioButtonClearKey.Checked)
                {
                    return PredefinedStreamingPolicy.ClearKey;
                }
                else if (radioButtonMultiDRMCENC.Checked)
                {
                    return PredefinedStreamingPolicy.MultiDrmCencStreaming;
                }
                else // if (radioButtonMultiDRM.Checked)
                {
                    return PredefinedStreamingPolicy.MultiDrmStreaming;
                }

            }
        }



        public CreateLocator(AMSClientV3 client, List<Asset> SelectedAssets, bool extendlocator = false)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _SelectedAssets = SelectedAssets;
            _client = client;
            if (extendlocator) // dialog box used to extend locator expiration date
            {
                buttonOk.Text = AMSExplorer.Properties.Resources.CreateLocator_CreateLocator_UpdateLocatorS;
                this.Text = AMSExplorer.Properties.Resources.CreateLocator_CreateLocator_UpdateLocators2;
                groupBoxStart.Enabled = false; // do not propose to specificy start date
            }
        }

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
        }

        private void checkBoxStartDate_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = checkBoxStartDate.Checked;
            dateTimePickerStartTime.Enabled = checkBoxStartDate.Checked;
        }

        public List<string> SelectedFilters
        {
            get
            {
                var list = new List<string>();
                string filters = string.Empty;
                foreach (var f in listViewFilters.CheckedItems)
                {
                    string v = (f as ListViewItem).SubItems[1].Text;
                    if (v != string.Empty)
                    {
                        list.Add(v);
                    }
                }
                if (string.IsNullOrEmpty(filters))
                {
                    filters = null;
                }
                return list.Count > 0 ? list : null;
            }
        }

        private async void CreateLocator_Load(object sender, EventArgs e)
        {
            // Filters

            var afiltersnames = new List<string>();

            // asset filters
            if (_SelectedAssets.Count == 1)
            {
                labelNoAssetFilter.Visible = false;
                var assetFilters = await _client.AMSclient.AssetFilters.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName, _SelectedAssets.First().Name);
                afiltersnames = assetFilters.Select(a => a.Name).ToList();

                assetFilters.ToList().ForEach(f =>
                {
                    var lvitem = new ListViewItem(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_AssetFilter + f.Name, f.Name });
                    listViewFilters.Items.Add(lvitem);
                }
               );
            }



            // account filters
            var acctFilters = await _client.AMSclient.AccountFilters.ListAsync(_client.credentialsEntry.ResourceGroup, _client.credentialsEntry.AccountName);

            acctFilters.ToList().ForEach(f =>
            {
                var lvitem = new ListViewItem(new string[] { AMSExplorer.Properties.Resources.ChooseStreamingEndpoint_ChooseStreamingEndpoint_Load_GlobalFilter + f.Name, f.Name });

                if (afiltersnames.Contains(f.Name)) // global filter with same name than asset filter
                {
                    lvitem.ForeColor = Color.Gray;
                }
                listViewFilters.Items.Add(lvitem);
            }
           );
        }

        private void radioButtonEndCustom_CheckedChanged(object sender, EventArgs e)
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

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonMultiDRM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonMultiDRMCENC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonClearKey_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
