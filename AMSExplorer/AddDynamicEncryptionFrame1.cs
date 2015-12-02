//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
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
    public partial class AddDynamicEncryptionFrame1 : Form
    {

        public AssetDeliveryPolicyType GetDeliveryPolicyType
        {
            get
            {
                if (radioButtonAESClearKey.Checked)
                {
                    return AssetDeliveryPolicyType.DynamicEnvelopeEncryption;
                }
                else if (radioButtonCENCKey.Checked)
                {
                    return AssetDeliveryPolicyType.DynamicCommonEncryption;
                }
                else if (radioButtonDecryptStorage.Checked)
                {
                    return AssetDeliveryPolicyType.NoDynamicEncryption;
                }
                else
                {
                    return AssetDeliveryPolicyType.None;
                }
            }
        }

        public ContentKeyType GetContentKeyType
        {
            get
            {
                return radioButtonAESClearKey.Checked ? ContentKeyType.EnvelopeEncryption : ContentKeyType.CommonEncryption;
            }
        }


        public AssetDeliveryProtocol GetAssetDeliveryProtocol
        {
            get
            {
                return ((checkBoxProtocolDASH.Checked ? AssetDeliveryProtocol.Dash : AssetDeliveryProtocol.None) | (checkBoxProtocolHLS.Checked ? AssetDeliveryProtocol.HLS : AssetDeliveryProtocol.None) | (checkBoxProtocolSmooth.Checked ? AssetDeliveryProtocol.SmoothStreaming : AssetDeliveryProtocol.None));
            }
        }

        public string PlayReadyCustomAttributes
        {
            get
            {
                if (checkBoxPlayReadyPackaging.Checked && !string.IsNullOrEmpty(textBoxCustomAttributes.Text))
                {
                    return textBoxCustomAttributes.Text;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool WidevinePackaging
        {
            get
            {
                return (checkBoxProtocolDASH.Checked && EnableDynEnc) ? checkBoxWidevinePackaging.Checked : false;
            }
        }

        public bool PlayReadyPackaging
        {
            get
            {
                return EnableDynEnc ? checkBoxPlayReadyPackaging.Checked : false;
            }
        }

        public bool EnableDynEnc
        {
            get
            {
                return checkBoxEnableDynEnc.Checked;
            }
        }


        private CloudMediaContext _context;

        public AddDynamicEncryptionFrame1(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
        }



        private void SetupDynEnc_Load(object sender, EventArgs e)
        {
        }






        private void buttonOk_Click(object sender, EventArgs e)
        {

        }


        private void radioButtonDecryptStorage_CheckedChanged(object sender, EventArgs e)
        {
            panelPackaging.Visible = !radioButtonDecryptStorage.Checked;
            checkBoxEnableDynEnc.Checked = true;
            checkBoxEnableDynEnc.Visible = !radioButtonDecryptStorage.Checked;
        }

        private void radioButtonCENCKey_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonNoDynEnc_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDelPolProtocols.Visible = !radioButtonNoDynEnc.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panelDynEnc.Enabled = checkBoxEnableDynEnc.Checked;
        }

        private void checkBoxPlayReadyPackaging_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomAttributes.Enabled = checkBoxPlayReadyPackaging.Checked;
        }

        private void checkBoxProtocolDASH_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWidevinePackaging.Visible = checkBoxProtocolDASH.Checked;
        }

        private void radioButtonAESClearKey_CheckedChanged(object sender, EventArgs e)
        {
            panelPackaging.Visible = !radioButtonAESClearKey.Checked;
        }
    }
}
