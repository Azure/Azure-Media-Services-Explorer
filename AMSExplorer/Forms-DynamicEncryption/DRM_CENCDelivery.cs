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
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_CENCDelivery : Form
    {
        public int GetNumberOfAuthorizationPolicyOptionsPlayReady // if 0, then no authorization policy. If > 0, then renturn the number of options
        {
            get
            {
                if (!checkBoxPlayReady.Checked || !_PlayReadyPackagingEnabled)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptionsPlayReady.Value;
                }
            }
        }

        public int GetNumberOfAuthorizationPolicyOptionsWidevine // if 0, then no authorization policy. If > 0, then renturn the number of options
        {
            get
            {
                if (!checkBoxWidevine.Checked || !_WidevinePackagingEnabled)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptionsWidevine.Value;
                }
            }
        }

        private bool _PlayReadyPackagingEnabled;
        private bool _WidevinePackagingEnabled;

        public DRM_CENCDelivery(bool PlayReadyPackagingEnabled, bool WidevinePackagingEnabled)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _PlayReadyPackagingEnabled = PlayReadyPackagingEnabled;
            _WidevinePackagingEnabled = WidevinePackagingEnabled;
        }


        private void DRM_CENCDelivery_Load(object sender, EventArgs e)
        {
            groupBoxPlayReady.Enabled = _PlayReadyPackagingEnabled;
            groupBoxWidevine.Enabled = _WidevinePackagingEnabled;
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {

        }
    }
}
