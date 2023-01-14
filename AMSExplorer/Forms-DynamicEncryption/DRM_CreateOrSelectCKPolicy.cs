//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_CreateOrSelectCKPolicy : Form
    {
        private AMSClientV3 _client;

        public bool CreateNewPolicy => radioButtonCreate.Checked;

        public string PolicyNameToUse
        {
            get
            {
                if (!CreateNewPolicy)
                    return listViewContentKeyPolicies.GetSelectedContentKeyPolicy.Data.Name;
                else
                    return textBoxPolicyName.Text;
            }
        }


        public DRM_CreateOrSelectCKPolicy(AMSClientV3 client)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _client = client;

        }

        private void DRM_CreateOrSelectCKPolicy_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }


        private void DRM_CreateOrSelectCKPolicy_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelstep, e);
        }

        private void DRM_CreateOrSelectCKPolicy_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
            UpdateStatusButtonOk();

            textBoxPolicyName.Text = "keypolicy-" + Program.GetUniqueness();
        }

        private async void radioButtonSelect_CheckedChanged(object sender, EventArgs e)
        {
            listViewContentKeyPolicies.Enabled = radioButtonSelect.Checked;
            textBoxPolicyName.Enabled = !radioButtonSelect.Checked;

            if (radioButtonSelect.Checked)
            {
                // let's list the asset
                await listViewContentKeyPolicies.LoadContentKeyPoliciesAsync(_client);
            }
            else
            {
            }
            UpdateStatusButtonOk();
        }

        private void listViewContentKeyPolicies_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatusButtonOk();
        }


        private void UpdateStatusButtonOk(bool additionalCondition = true)
        {
            buttonOk.Enabled = (radioButtonCreate.Checked || (radioButtonSelect.Checked && listViewContentKeyPolicies.SelectedItems.Count > 0)) && additionalCondition;
        }
    }
}
