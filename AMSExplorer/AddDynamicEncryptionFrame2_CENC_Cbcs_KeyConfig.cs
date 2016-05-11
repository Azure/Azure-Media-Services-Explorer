
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.IO;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig : Form
    {


        public byte[] FairPlayASK
        {
            get
            {
                try
                {
                    if (radioButtonContentKeyHex.Checked)
                    {
                        return DynamicEncryption.HexStringToByteArray(textBoxASK.Text);
                    }

                    else // (radioButtonContentKeyBase64.Checked)
                    {
                        return Convert.FromBase64String(textBoxASK.Text);
                    }
                }

                catch
                {
                    return null;
                }
            }
        }



        public Guid KeyId
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(textBoxkeyid.Text))
                    {
                        return Guid.NewGuid();
                    }
                    else if (radioButtonKeyIDGuid.Checked) // GUID
                    {
                        return new Guid(textBoxkeyid.Text);
                    }
                    else // Base64
                    {
                        return new Guid(Convert.FromBase64String(textBoxkeyid.Text));
                    }
                }
                catch
                {
                    return Guid.NewGuid();
                }
            }
            set
            {
                textBoxkeyid.Text = value.ToString();
            }
        }


        public AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }


        private void buttonGenKeyID_Click_1(object sender, EventArgs e)
        {
            radioButtonKeyIDGuid.Checked = true;
            textBoxkeyid.Text = Guid.NewGuid().ToString();
            radioButtonContentKeyBase64.Checked = true;
        }


        private void AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig_Load(object sender, EventArgs e)
        {
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonKeyIDBase64_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKeyIDBase64.Checked)
            {
                try
                {
                    Guid myGuid = new Guid(textBoxkeyid.Text);
                    textBoxkeyid.Text = Convert.ToBase64String(myGuid.ToByteArray());
                }
                catch
                {
                    textBoxkeyid.Text = string.Empty;
                }
            }
        }

        private void radioButtonKeyIDGuid_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKeyIDGuid.Checked)
            {
                try
                {
                    Guid myGuid = new Guid(Convert.FromBase64String(textBoxkeyid.Text));
                    textBoxkeyid.Text = myGuid.ToString();
                }
                catch
                {
                    textBoxkeyid.Text = string.Empty;
                }

            }
        }


        private void radioButtonContentKeyBase64_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyBase64.Checked)
            {
                try
                {
                    textBoxASK.Text = Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxASK.Text));
                }
                catch
                {
                    textBoxASK.Text = string.Empty;
                }
            }
        }

        private void radioButtonContentKeyHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyHex.Checked)
            {
                try
                {
                    textBoxASK.Text = DynamicEncryption.ByteArrayToHexString(Convert.FromBase64String(textBoxASK.Text));
                }
                catch
                {
                    textBoxASK.Text = string.Empty;
                }
            }
        }

        private void textBoxASK_TextChanged(object sender, EventArgs e)
        {
            if (FairPlayASK == null)
            {
                errorProvider1.SetError(textBoxASK, "The key must be a 16 bytes (128 bits) value");
                buttonOk.Enabled = false;
            }
            else
            {
                errorProvider1.SetError(textBoxASK, String.Empty);
                buttonOk.Enabled = true;

            }
        }
    }
}
