//----------------------------------------------------------------------- 
// <copyright file="AddDynamicEncryption.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

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
                if (radioButtonEnvelope.Checked)
                {
                    return AssetDeliveryPolicyType.DynamicEnvelopeEncryption;
                }
                else if (radioButtonCENCKey.Checked)
                {
                    return AssetDeliveryPolicyType.DynamicCommonEncryption;
                }
                else
                {
                    return AssetDeliveryPolicyType.NoDynamicEncryption;
                }
            }
        }

        public ContentKeyType GetContentKeyType
        {
            get
            {
                return radioButtonEnvelope.Checked ? ContentKeyType.EnvelopeEncryption : ContentKeyType.CommonEncryption;
            }
        }


        public AssetDeliveryProtocol GetAssetDeliveryProtocol
        {
            get
            {
                return ((checkBoxProtocolDASH.Checked ? AssetDeliveryProtocol.Dash : AssetDeliveryProtocol.None) | (checkBoxProtocolHLS.Checked ? AssetDeliveryProtocol.HLS : AssetDeliveryProtocol.None) | (checkBoxProtocolSmooth.Checked ? AssetDeliveryProtocol.SmoothStreaming : AssetDeliveryProtocol.None));
            }
        }



        private CloudMediaContext _context;

        public AddDynamicEncryptionFrame1(CloudMediaContext context, bool IsLiveAsset, bool ForceUseToProvideKey)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            if (IsLiveAsset)
            {// only AES encryption is supported for Live today, so let's disable storage decryption
                radioButtonDecryptStorage.Enabled = false;
            }
        }



        private void SetupDynEnc_Load(object sender, EventArgs e)
        {

        }




        private void radioButtonEnvelope_CheckedChanged(object sender, EventArgs e)
        {

        }




        private void buttonOk_Click(object sender, EventArgs e)
        {

        }


        private void radioButtonDecryptStorage_CheckedChanged(object sender, EventArgs e)
        {
            // groupBoxAuthPol.Enabled = !radioButtonDecryptStorage.Checked;
        }

        private void radioButtonCENCKey_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  radioButtonNoAuthPolicy.Enabled = radioButtonCENCKey.Checked;
                  if (!radioButtonCENCKey.Checked && radioButtonNoAuthPolicy.Checked) // if not PlayReady mode, then let's uncheck no playreay lic server if it checked
                  {
                      radioButtonOpenAuthPolicy.Checked = true;
                  }
             * */
        }


    }
}
