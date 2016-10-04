//----------------------------------------------------------------------------------------------
//    Copyright 2016 Microsoft Corporation
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
                else if (radioButtonCENCCbcsKey.Checked)
                {
                    return AssetDeliveryPolicyType.DynamicCommonEncryptionCbcs;
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
                if (radioButtonAESClearKey.Checked)
                {
                    return ContentKeyType.EnvelopeEncryption;
                }
                else if (radioButtonCENCCbcsKey.Checked)
                {
                    return ContentKeyType.CommonEncryptionCbcs;
                }
                else // (radioButtonCENCKey.Checked)
                {
                    return ContentKeyType.CommonEncryption;
                }
            }
        }


        public AssetDeliveryProtocol GetAssetDeliveryProtocol
        {
            get
            {
                return (
                    ((!radioButtonCENCCbcsKey.Checked && checkBoxProtocolDASH.Checked) ? AssetDeliveryProtocol.Dash : AssetDeliveryProtocol.None)
                    |
                    (checkBoxProtocolHLS.Checked ? AssetDeliveryProtocol.HLS : AssetDeliveryProtocol.None)
                    |
                    ((!radioButtonCENCCbcsKey.Checked && checkBoxProtocolSmooth.Checked) ? AssetDeliveryProtocol.SmoothStreaming : AssetDeliveryProtocol.None)
                    |
                    // progressive download only available for dyn decryption
                    ((radioButtonDecryptStorage.Checked && checkBoxProtocolProgressiveDownload.Checked) ? AssetDeliveryProtocol.ProgressiveDownload : AssetDeliveryProtocol.None)
                    );
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
                return (checkBoxProtocolDASH.Checked && EnableDynEnc && radioButtonCENCKey.Checked) ? checkBoxWidevinePackaging.Checked : false;
            }
        }

        public bool PlayReadyPackaging
        {
            get
            {
                return (EnableDynEnc && radioButtonCENCKey.Checked) ? checkBoxPlayReadyPackaging.Checked : false;
            }
        }

        public bool FairPlayPackaging
        {
            get
            {
                return (EnableDynEnc && radioButtonCENCCbcsKey.Checked) ? true : false;
            }
        }

        public bool EnableDynEnc
        {
            get
            {
                return checkBoxEnableDynEnc.Checked;
            }
        }

        public bool SelectExistingPolicies
        {
            get
            {
                return checkBoxSelectPolicies.Checked;
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


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panelDynEnc.Visible = checkBoxEnableDynEnc.Checked;
        }

        private void checkBoxPlayReadyPackaging_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCustomAttributes.Enabled = checkBoxPlayReadyPackaging.Checked;
        }

        private void checkBoxProtocolDASH_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWidevinePackaging.Visible = checkBoxProtocolDASH.Checked;
        }


        private void radioButtonControl_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAESClearKey.Checked)
            {
                panelPackaging.Visible = false;

                checkBoxProtocolProgressiveDownload.Visible = false;
                checkBoxProtocolDASH.Visible = true;
                checkBoxProtocolHLS.Visible = true;
                checkBoxProtocolHLS.Checked = true;
                checkBoxProtocolHLS.Enabled = true;
                checkBoxProtocolSmooth.Visible = true;

                checkBoxEnableDynEnc.Visible = true;

                groupBoxDelivery.Visible = true;

                checkBoxSelectPolicies.Visible = true;
            }
            else if (radioButtonCENCCbcsKey.Checked)
            {
                panelPackaging.Visible = true;
                panelPackagingCENC.Visible = false;
                checkBoxFairPlayPackaging.Visible = true;

                checkBoxProtocolProgressiveDownload.Visible = false;
                checkBoxProtocolDASH.Visible = false;
                checkBoxProtocolHLS.Visible = true;
                checkBoxProtocolHLS.Checked = true;
                checkBoxProtocolHLS.Enabled = false;

                checkBoxProtocolSmooth.Visible = false;

                checkBoxEnableDynEnc.Visible = true;

                groupBoxDelivery.Visible = true;

                checkBoxSelectPolicies.Visible = true;
            }
            else if (radioButtonCENCKey.Checked)
            {
                panelPackaging.Visible = true;
                panelPackagingCENC.Visible = true;
                checkBoxFairPlayPackaging.Visible = false;

                checkBoxProtocolProgressiveDownload.Visible = false;
                checkBoxProtocolDASH.Visible = true;
                checkBoxProtocolHLS.Visible = true;
                checkBoxProtocolHLS.Checked = true;
                checkBoxProtocolHLS.Enabled = true;
                checkBoxProtocolSmooth.Visible = true;

                checkBoxEnableDynEnc.Visible = true;

                groupBoxDelivery.Visible = true;

                checkBoxSelectPolicies.Visible = true;
            }
            else if (radioButtonDecryptStorage.Checked)
            {
                panelPackaging.Visible = false;

                checkBoxProtocolProgressiveDownload.Visible = true;
                checkBoxProtocolDASH.Visible = true;
                checkBoxProtocolHLS.Visible = true;
                checkBoxProtocolHLS.Checked = true;
                checkBoxProtocolHLS.Enabled = true;
                checkBoxProtocolSmooth.Visible = true;

                checkBoxEnableDynEnc.Checked = true;
                checkBoxEnableDynEnc.Visible = false;

                groupBoxDelivery.Visible = true;

                checkBoxSelectPolicies.Visible = false;
            }
            else if (radioButtonNoDynEnc.Checked)
            {
                panelPackaging.Visible = false;

                checkBoxEnableDynEnc.Visible = true;

                groupBoxDelivery.Visible = false;

                checkBoxSelectPolicies.Visible = false;
            }

            if (checkBoxSelectPolicies.Checked && checkBoxSelectPolicies.Visible)
            {
                groupBoxDelivery.Visible = false;
            }

        }

        private void checkBoxSelectPolicies_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxDelivery.Enabled = !checkBoxSelectPolicies.Checked;
        }
    }
}
