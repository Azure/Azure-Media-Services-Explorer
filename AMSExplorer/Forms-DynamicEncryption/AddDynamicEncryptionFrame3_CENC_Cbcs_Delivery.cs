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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Diagnostics;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery : Form
    {
        public byte[] FairPlayASK
        {
            get
            {
                if (string.IsNullOrWhiteSpace(textBoxASK.Text))
                {
                    return null;
                }
                try
                {
                    if (radioButtonASKHex.Checked)
                    {
                        return DynamicEncryption.HexStringToByteArray(textBoxASK.Text);
                    }

                    else // Base 64
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
        public string FairPlayLAurl
        {
            get
            {
                try
                {
                    Uri myuri = new Uri(textBoxFairPlayLAurl.Text);
                    return textBoxFairPlayLAurl.Text;
                }
                catch
                {
                    return null;
                }

            }
            set
            {
                textBoxFairPlayLAurl.Text = value.ToString();
            }
        }

        public bool FairPlayFinalLAurl
        {
            get
            {
                return checkBoxFinalExtURL.Checked;
            }
        }


        public PFXCertificate FairPlayCertificate
        {
            get
            {
                return cert;
            }
        }

        public byte[] FairPlayIV
        {
            get
            {
                if (radioButtonDeliverFairPlayfromAMS.Checked || string.IsNullOrEmpty(textBoxIV.Text)) return null;

                try
                {
                    if (radioButtonIVHex.Checked)
                    {
                        return DynamicEncryption.HexStringToByteArray(textBoxIV.Text);
                    }

                    else // (radioButtonContentKeyBase64.Checked)
                    {
                        return Convert.FromBase64String(textBoxIV.Text);
                    }
                }

                catch
                {
                    return null;
                }
            }
        }



        public int GetNumberOfAuthorizationPolicyOptionsFairPlay // if 0, then no authorization policy. If > 0, then renturn the number of options
        {
            get
            {
                if (radioButtonExternalFairPlayServer.Checked)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptionsFairPlay.Value;
                }
            }
        }

        public bool AMSLAURLSchemeSKD // true if skd, false if https
        {
            get
            {
                return radioButtonSkd.Checked;
            }
        }


        private CloudMediaContext _context;
        private PFXCertificate cert = new PFXCertificate();

        public AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery(CloudMediaContext context)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
        }

        private void AddDynamicEncryptionFrame3_Load(object sender, EventArgs e)
        {
            moreinfoFairPlaylink.Links.Add(new LinkLabel.Link(0, moreinfoFairPlaylink.Text.Length, Constants.LinkMoreInfoFairPlay));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonExternalPRServer_CheckedChanged(object sender, EventArgs e)
        {
            panelExternalFairPlay.Enabled = radioButtonExternalFairPlayServer.Checked;
            panelFairPlayFromAMS.Enabled = !radioButtonExternalFairPlayServer.Checked;
            numericUpDownNbOptionsFairPlay.Enabled = !radioButtonExternalFairPlayServer.Checked;
            ValidateButtonOk();
        }

        private void buttonImportPFX_Click(object sender, EventArgs e)
        {
            cert = DynamicEncryption.GetCertificateFromFile(false, X509KeyStorageFlags.Exportable);

            TextBoxCertificateFile.Text = (cert.Certificate != null) ? cert.Certificate.SubjectName.Name : "(Error)";
            ValidateButtonOk();
        }

        private void ValidateButtonOk()
        {
            buttonOk.Enabled =
                (radioButtonDeliverFairPlayfromAMS.Checked && cert.Certificate != null && FairPlayASK != null)
                ||
                (radioButtonExternalFairPlayServer.Checked && !string.IsNullOrWhiteSpace(textBoxFairPlayLAurl.Text) && errorProvider1.GetError(textBoxFairPlayLAurl) == string.Empty)
               ;
        }

        private void textBoxFairPlayLAurl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Uri myuri = new Uri(textBoxFairPlayLAurl.Text);
                errorProvider1.SetError(textBoxFairPlayLAurl, String.Empty);
            }
            catch
            {
                errorProvider1.SetError(textBoxFairPlayLAurl, "The FairPlay server URL must be a valid URL");
            }
            ValidateButtonOk();
        }

        private void radioButtonDeliverPRfromAMS_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonIVBase64_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIVBase64.Checked)
            {
                try
                {
                    textBoxIV.Text = Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxIV.Text));
                }
                catch
                {
                    textBoxIV.Text = string.Empty;
                }
            }
        }

        private void radioButtonIVHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonIVHex.Checked)
            {
                try
                {
                    textBoxIV.Text = DynamicEncryption.ByteArrayToHexString(Convert.FromBase64String(textBoxIV.Text));
                }
                catch
                {
                    textBoxIV.Text = string.Empty;
                }
            }
        }

        private void textBoxIV_TextChanged(object sender, EventArgs e)
        {
            bool Error = false;
            try
            {
                if (radioButtonIVHex.Checked)
                {
                    var a = DynamicEncryption.HexStringToByteArray(textBoxIV.Text);
                }

                else // (radioButtonContentKeyBase64.Checked)
                {
                    var a = Convert.FromBase64String(textBoxIV.Text);
                }
            }

            catch
            {
                errorProvider1.SetError(textBoxIV, "The key must be a 16 bytes (128 bits) value");
                buttonOk.Enabled = false;
                Error = true;
            }

            if (!Error)
            {
                errorProvider1.SetError(textBoxIV, String.Empty);
                buttonOk.Enabled = true;
            }
        }

        private void textBoxASK_TextChanged(object sender, EventArgs e)
        {
            if (FairPlayASK == null)
            {
                errorProvider1.SetError(textBoxASK, "The key must be a 16 bytes (128 bit) value");
            }
            else
            {
                errorProvider1.SetError(textBoxASK, String.Empty);
            }
            ValidateButtonOk();
        }

        private void radioButtonASKBase64_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonASKBase64.Checked)
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

        private void radioButtonASKHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonASKHex.Checked)
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

        private void moreinfoFairPlaylink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

      
    }

}
