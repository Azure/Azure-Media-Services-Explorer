//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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

namespace AMSExplorer
{
    public partial class DeleteKeyAndPolicy : Form
    {
        public bool DeleteKeys
        {
            get
            {
                return checkBoxDeleteKeys.Checked;
            }
        }
        public bool DeleteDeliveryPolicies
        {
            get
            {
                return checkBoxDeleteDeliveryPol.Checked;
            }
        }

        public bool DeleteAuthorizationPolicies
        {
            get
            {
                return checkBoxDeleteAutPol.Checked;
            }
        }

        public DeleteKeyAndPolicy(int nbassets)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            buttonOk.Text = string.Format(buttonOk.Text, nbassets, nbassets > 1 ? "(s)" : "");
        }

        private void DeleteKeyAndPolicy_Load(object sender, EventArgs e)
        {
        }
    }
}
