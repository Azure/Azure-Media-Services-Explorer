//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class DRM_CENCCBSCDelivery : Form
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
                        byte[] key = Convert.FromBase64String(textBoxASK.Text);
                        return (key.Length == 16) ? key : null;
                    }
                }

                catch
                {
                    return null;
                }
            }
        }

        public PFXCertificate FairPlayCertificate => cert;

        public int GetNumberOfAuthorizationPolicyOptionsPlayReady // if 0, then no authorization policy. If > 0, then return the number of options
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

        public int GetNumberOfAuthorizationPolicyOptionsWidevine // if 0, then no authorization policy. If > 0, then return the number of options
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


        public int GetNumberOfAuthorizationPolicyOptionsFairPlay // if 0, then no authorization policy. If > 0, then return the number of options
        {
            get
            {
                if (!checkBoxFairPlay.Checked || !_FairPlayPackagingEnabled)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptionsFairPlay.Value;
                }
            }
        }

        private readonly bool _PlayReadyPackagingEnabled;
        private readonly bool _WidevinePackagingEnabled;
        private readonly bool _FairPlayPackagingEnabled;
        private PFXCertificate cert = new();

        public DRM_CENCCBSCDelivery(bool PlayReadyPackagingEnabled, bool WidevinePackagingEnabled, bool FairPlayPackagingEnabled)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
            _PlayReadyPackagingEnabled = PlayReadyPackagingEnabled;
            _WidevinePackagingEnabled = WidevinePackagingEnabled;
            _FairPlayPackagingEnabled = FairPlayPackagingEnabled;
        }


        private void DRM_CENCCBCSDelivery_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            groupBoxPlayReady.Visible = checkBoxPlayReady.Checked = _PlayReadyPackagingEnabled;
            groupBoxWidevine.Visible = checkBoxWidevine.Checked = _WidevinePackagingEnabled;
            groupBoxFairPlay.Visible = checkBoxFairPlay.Checked = _FairPlayPackagingEnabled;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void DRM_CENCCBCSDelivery_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(label1, e);
        }

        private void ValidateButtonOk()
        {
            bool okForFairPlay = (!checkBoxFairPlay.Checked) || (checkBoxFairPlay.Checked && cert.Certificate != null && FairPlayASK != null);
            buttonOk.Enabled = okForFairPlay && (checkBoxPlayReady.Checked || checkBoxWidevine.Checked || checkBoxFairPlay.Checked);
        }

        private void CheckBoxFairPlay_CheckedChanged(object sender, EventArgs e)
        {
            panelFairPlayFromAMS.Enabled = checkBoxFairPlay.Checked;
            ValidateButtonOk();
        }

        private void ButtonImportPFX_Click(object sender, EventArgs e)
        {
            cert = DynamicEncryption.GetCertificateFromFile(false, X509KeyStorageFlags.Exportable);

            TextBoxCertificateFile.Text = (cert.Certificate != null) ? cert.Certificate.SubjectName.Name : "(Error)";
            ValidateButtonOk();
        }

        private void RadioButtonASKBase64_CheckedChanged(object sender, EventArgs e)
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

        private void RadioButtonASKHex_CheckedChanged(object sender, EventArgs e)
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

        private void TextBoxASK_TextChanged(object sender, EventArgs e)
        {
            if (FairPlayASK == null)
            {
                errorProvider1.SetError(textBoxASK, "The key must be a 16 bytes (128 bit) value");
            }
            else
            {
                errorProvider1.SetError(textBoxASK, string.Empty);
            }
            ValidateButtonOk();
        }

        private void CheckBoxPlayReady_CheckedChanged(object sender, EventArgs e)
        {
            ValidateButtonOk();
        }

        private void CheckBoxWidevine_CheckedChanged(object sender, EventArgs e)
        {
            ValidateButtonOk();
        }

        private void DRM_CENCCBSCDelivery_Shown(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);
        }
    }
}