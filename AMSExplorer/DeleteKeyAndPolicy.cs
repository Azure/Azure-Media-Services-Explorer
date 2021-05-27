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

using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DeleteKeyAndPolicy : Form
    {
        public bool DeleteKeys => checkBoxDeleteKeys.Checked;
        public bool DeleteDeliveryPolicies => checkBoxDeleteDeliveryPol.Checked;

        public bool DeleteAuthorizationPolicies => checkBoxDeleteAutPol.Checked;

        public DeleteKeyAndPolicy(int nbassets)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            buttonOk.Text = string.Format(buttonOk.Text, nbassets, nbassets > 1 ? "(s)" : string.Empty);
        }

        private void DeleteKeyAndPolicy_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }

        private void DeleteKeyAndPolicy_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }

        private void DeleteKeyAndPolicy_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
