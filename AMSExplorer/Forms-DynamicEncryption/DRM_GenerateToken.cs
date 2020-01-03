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

using System;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_GenerateToken : Form
    {

        public int TokenDuration => decimal.ToInt32(numericUpDownTokenDuration.Value);

        public int? TokenUse => checkBoxTokenUse.Checked ? (int?)decimal.ToInt32(numericUpDownTokenUse.Value) : null;

        public DRM_GenerateToken()
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

        }

        private void DRM_WidevineLicense_Load(object sender, EventArgs e)
        {
            DpiUtils.InitPerMonitorDpi(this);
        }

        private void CheckBoxTokenUse_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownTokenUse.Enabled = checkBoxTokenUse.Checked;
        }

        private void DRM_GenerateToken_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            DpiUtils.UpdatedSizeFontAfterDPIChange(labelstep, e);
        }
    }
}
