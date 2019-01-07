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
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame3_ExistingPolicies : Form
    {


        public IContentKeyAuthorizationPolicy UseExistingAuthorizationPolicy
        {
            get
            {
                return _existingAuthorizationPolicy;
            }
        }

        public IAssetDeliveryPolicy UseExistingDeliveryPolicy
        {
            get
            {
                return _existingDeliveryPolicy;
            }
        }

        private CloudMediaContext _context;
        private IContentKeyAuthorizationPolicy _existingAuthorizationPolicy = null;
        private IAssetDeliveryPolicy _existingDeliveryPolicy = null;
        private AddDynamicEncryptionFrame1 _form1;

        public AddDynamicEncryptionFrame3_ExistingPolicies(CloudMediaContext context, AddDynamicEncryptionFrame1 form1)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _form1 = form1;
        }

        private void SetupDynEnc_Load(object sender, EventArgs e)
        {
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void ValidateButtonOk()
        {
            buttonOk.Enabled = true;

            /*
                (
                   _existingDeliveryPolicy != null
               &&
                _existingAuthorizationPolicy != null
                )
                ;
                */
        }

        private void buttonUseExistingAutpolicy_Click(object sender, EventArgs e)
        {
            var form = new SelectAutPolicy(_context, _form1.GetContentKeyType);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var pol = form.SelectedPolicy;
                if (pol != null)
                {
                    _existingAuthorizationPolicy = pol;
                    TextBoxAutPolicyId.Text = string.Format("{0} ({1})", pol.Name, pol.Id);
                    ValidateButtonOk();
                }
            }
        }

        private void buttonExistingDelPol_Click(object sender, EventArgs e)
        {
            AssetDeliveryPolicyType poltype = AssetDeliveryPolicyType.None;
            if (_form1.GetContentKeyType == ContentKeyType.CommonEncryption)
            {
                poltype = AssetDeliveryPolicyType.DynamicCommonEncryption;
            }
            else if (_form1.GetContentKeyType == ContentKeyType.CommonEncryptionCbcs)
            {
                poltype = AssetDeliveryPolicyType.DynamicCommonEncryptionCbcs;
            }
            else if (_form1.GetContentKeyType == ContentKeyType.EnvelopeEncryption)
            {
                poltype = AssetDeliveryPolicyType.DynamicEnvelopeEncryption;
            }

            var form = new SelectDeliveryPolicy(_context, poltype);

            if (form.ShowDialog() == DialogResult.OK)
            {
                var pol = form.SelectedPolicy;
                if (pol != null)
                {
                    _existingDeliveryPolicy = pol;
                    textBoxDelPolId.Text = string.Format("{0} ({1})", pol.Name, pol.Id);
                    ValidateButtonOk();
                }
            }
        }
    }
}
