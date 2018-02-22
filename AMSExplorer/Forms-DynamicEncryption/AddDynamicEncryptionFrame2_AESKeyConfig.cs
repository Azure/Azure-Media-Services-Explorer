
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame2_AESKeyConfig : Form
    {

        public bool ContentKeyRandomGeneration
        {
            get
            {
                return radioButtonKeyRandomGeneration.Checked;
            }
            set
            {
                radioButtonKeyRandomGeneration.Checked = value;
                radioButtonKeySpecifiedByUser.Checked = !value;
            }
        }


        public string AESContentKey
        {
            get
            {
                if (radioButtonContentKeyHex.Checked)
                {
                    return Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxcontentkey.Text));
                }
                else
                    return textBoxcontentkey.Text;
            }
            set
            {
                textBoxcontentkey.Text = value;
            }
        }

        public Guid? AESKeyId
        {
            get
            {
                try
                {
                    if (radioButtonKeySpecifiedByUser.Checked)
                    {
                        if (radioButtonKeyIDGuid.Checked) // GUID
                        {
                            return (Guid?)new Guid(textBoxkeyid.Text);
                        }
                        else // Base64
                        {
                            return (Guid?)new Guid(Convert.FromBase64String(textBoxkeyid.Text));
                        }
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                textBoxkeyid.Text = value.ToString();
            }
        }

     

        public AddDynamicEncryptionFrame2_AESKeyConfig(bool ForceUseToProvideKey)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            if (ForceUseToProvideKey) // code wants to force user to provide the key
            {
                radioButtonKeyRandomGeneration.Enabled = false;
                radioButtonKeyRandomGeneration.Checked = false;
                radioButtonKeySpecifiedByUser.Checked = true;
                groupBoxCrypto.Enabled = true;
            }

            /*
            if (!laststep)
            {
                buttonOk.Text = "Next";
                buttonOk.Image = null;
            }
            */
        }

        private void PlayReadyExternalServer_Load(object sender, EventArgs e)
        {
        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            radioButtonContentKeyBase64.Checked = true;
            textBoxcontentkey.Text = Convert.ToBase64String(DynamicEncryption.GetRandomBuffer(16));
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            /*
            bool validation = false;
            if (multiassets) // multi assets selectyed. We need at least the key seed
            {
                if (!string.IsNullOrEmpty(textBoxkeyseed.Text))
                {
                    validation = true;
                }
            }
            else // single asset selected
            {
                if (!string.IsNullOrEmpty(textBoxkeyid.Text) && (!string.IsNullOrEmpty(textBoxkeyseed.Text) || (!string.IsNullOrEmpty(textBoxcontentkey.Text))))
                {
                    validation = true;
                }
            }

            buttonOk.Enabled = validation;
             * */
            buttonOk.Enabled = true;
        }

        private void textBoxcontentkey_TextChanged(object sender, EventArgs e)
        {
            textBox_TextChanged(sender, e);
        }

        private void radioButtonHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyHex.Checked)
                textBoxcontentkey.Text = DynamicEncryption.ByteArrayToHexString(Convert.FromBase64String(textBoxcontentkey.Text));
        }

        private void radioButtonGuid_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyBase64.Checked)
                textBoxcontentkey.Text = Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxcontentkey.Text));
        }


        private void radioButtonKeyRandomGeneration_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxCrypto.Enabled = radioButtonKeySpecifiedByUser.Checked;
        }

        private void buttonGenKeyID_Click(object sender, EventArgs e)
        {
            radioButtonKeyIDGuid.Checked = true;
            textBoxkeyid.Text = Guid.NewGuid().ToString();
            radioButtonContentKeyBase64.Checked = true;
        }
    }
}
