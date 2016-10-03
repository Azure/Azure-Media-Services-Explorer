
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
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.IO;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame2_CENCKeyConfig : Form
    {
        private readonly string _PlayReadyTestLAURL = "http://playready.directtaps.net/pr/svc/rightsmanager.asmx?PlayRight=1&UseSimpleNonPersistentLicense=1";
        private readonly string _PlayReadyTestKeySeed = "XVBovsmzhP9gRIZxWfFta3VVRPzVEWmJsazEJ46I";

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

        public string KeySeed
        {
            get
            {
                return string.IsNullOrWhiteSpace(textBoxkeyseed.Text) ? null : textBoxkeyseed.Text;
            }
            set
            {
                textBoxkeyseed.Text = value;
            }
        }


        public string CENCContentKey
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
        public Guid? KeyId
        {
            get
            {
                try
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





        public AddDynamicEncryptionFrame2_CENCKeyConfig(bool ForceUseToProvideKey, bool showPlayReadyTestButton = true)
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

            panelPlayReadyTest.Visible = showPlayReadyTestButton;
        }

        private void buttonPlayReadyTestSettings_Click(object sender, EventArgs e)
        {
            //textBoxLAurl.Text = _PlayReadyTestLAURL;
            radioButtonKeySeedBase64.Checked = true;
            textBoxkeyseed.Text = _PlayReadyTestKeySeed;
            textBoxcontentkey.Text = string.Empty;
        }

        private void buttonGenKeyID_Click_1(object sender, EventArgs e)
        {
            radioButtonKeyIDGuid.Checked = true;
            textBoxkeyid.Text = Guid.NewGuid().ToString();
            radioButtonContentKeyBase64.Checked = true;
        }

        private void moreinfotestserver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void PlayReadyExternalServer_Load(object sender, EventArgs e)
        {
            moreinfotestserver.Links.Add(new LinkLabel.Link(0, moreinfotestserver.Text.Length, "http://playready.directtaps.net/"));
        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            radioButtonContentKeyBase64.Checked = true;
            textBoxcontentkey.Text = Convert.ToBase64String(DynamicEncryption.GetRandomBuffer(16));
            textBoxkeyseed.Text = string.Empty;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            ValidateDataForButtonOk();
        }

        private void ValidateDataForButtonOk()
        {
            bool validation = false;

            if (
                radioButtonKeyRandomGeneration.Checked ||
                !string.IsNullOrEmpty(textBoxkeyseed.Text) ||
                (string.IsNullOrEmpty(textBoxkeyseed.Text) && !string.IsNullOrEmpty(textBoxkeyid.Text) && !string.IsNullOrEmpty(textBoxcontentkey.Text))
                )
            {
                validation = true;
            }

            UpdateCalculatedContentKey();
            buttonOk.Enabled = validation;

            //checkBoxEncodingSL.Enabled = (textBoxLAurl.Text.Contains("&"));
        }

        private void textBoxkeyseed_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxkeyseed.Text)) // if a seed is set, let's make sure no content key is defined
            {
                textBoxcontentkey.Text = string.Empty;
            }
            textBox_TextChanged(sender, e);
            UpdateCalculatedContentKey();
        }

        private void UpdateCalculatedContentKey()
        {
            textBoxContentKeyCalculated.Text = string.Empty;
            if (this.KeyId != null)
            {
                if (this.KeySeed != null)
                {
                    try
                    {
                        byte[] bytecontentkey = DynamicEncryption.GeneratePlayReadyContentKey(Convert.FromBase64String(this.KeySeed), (Guid)this.KeyId);
                        if (radioButtonContentKeyBase64.Checked) // base64
                        {
                            textBoxContentKeyCalculated.Text = Convert.ToBase64String(bytecontentkey);
                        }
                        else // HEX
                        {
                            textBoxContentKeyCalculated.Text = DynamicEncryption.ByteArrayToHexString(bytecontentkey);
                        }
                    }
                    catch
                    {
                        textBoxContentKeyCalculated.Text = "(error)";
                    }
                }
                else // seed empty, so calulated key is the content key
                {
                    textBoxContentKeyCalculated.Text = textBoxcontentkey.Text;
                }


            }
        }

        private void textBoxcontentkey_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxcontentkey.Text)) // if a content key, let's make sure no seed is defined
            {
                textBoxkeyseed.Text = string.Empty;
            }
            textBox_TextChanged(sender, e);
        }

        private void radioButtonHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyHex.Checked)
            {
                try
                {
                    textBoxcontentkey.Text = DynamicEncryption.ByteArrayToHexString(Convert.FromBase64String(textBoxcontentkey.Text));
                }
                catch
                {
                    textBoxcontentkey.Text = string.Empty;
                }
            }
        }

        private void radioButtonGuid_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyBase64.Checked)
            {
                try
                {
                    textBoxcontentkey.Text = Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxcontentkey.Text));
                }
                catch
                {
                    textBoxcontentkey.Text = string.Empty;
                }
            }
            UpdateCalculatedContentKey();

        }

        private void radioButtonKeySeedHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKeySeedHex.Checked)
            {
                try
                {
                    textBoxkeyseed.Text = DynamicEncryption.ByteArrayToHexString(Convert.FromBase64String(textBoxkeyseed.Text));
                }
                catch
                {
                    textBoxkeyseed.Text = string.Empty;
                }
            }
        }

        private void radioButtonKeySeedBase64_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKeySeedBase64.Checked)
            {
                try
                {
                    textBoxkeyseed.Text = Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxkeyseed.Text));
                }
                catch
                {
                    textBoxkeyseed.Text = string.Empty;
                }
            }

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

        private void radioButtonKeySpecifiedByUser_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxCrypto.Enabled = radioButtonKeySpecifiedByUser.Checked;
            ValidateDataForButtonOk();
        }


        private void LAUR_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            bool Error = false;
            if (!string.IsNullOrWhiteSpace(tb.Text))
            {
                try
                {
                    Uri testuri = new Uri(tb.Text);
                }
                catch
                {
                    Error = true;
                }
            }

            if (Error)
            {
                errorProvider1.SetError(tb, "Url not valid");
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }
    }
}
