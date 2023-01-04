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

using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DeleteLiveOutputEvent : Form
    {
        public bool DeleteAsset => checkBoxDeleteAsset.Checked;

        public DeleteLiveOutputEvent(string label, string title)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            labelmain.Text = label;
            Text = title;
            buttonOk.Text = title;
        }

        private void DeleteLiveOutputEvent_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);
        }

        private void DeleteLiveOutputEvent_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelTitle, e);
        }

        private void DeleteLiveOutputEvent_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}
