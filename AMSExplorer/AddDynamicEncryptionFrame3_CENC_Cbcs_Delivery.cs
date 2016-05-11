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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery : Form
    {
        public Uri FairPlayLAurl
        {
            get
            {
                Uri myuri = null;
                try
                {
                    myuri = new Uri(textBoxFairPlayLAurl.Text);
                }
                catch
                {

                }
                return myuri;
            }
            set
            {
                textBoxFairPlayLAurl.Text = value.ToString();
            }
        }


        public PFXCertificate FairPlayCertificate
        {
            get
            {
                return cert;
            }
        }




        public int GetNumberOfAuthorizationPolicyOptionsFairPlay // if 0, then no authorization policy. If > 0, then renturn the number of options
        {
            get
            {
                if (radioButtonExternalPRServer.Checked && radioButtonExternalPRServer.Checked || !_FairPlayPackagingEnabled)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptionsPlayReady.Value;
                }
            }
        }


        private CloudMediaContext _context;
        private bool _FairPlayPackagingEnabled;
        private PFXCertificate cert;

        public AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery(CloudMediaContext context, bool FairPlayPackagingEnabled)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _FairPlayPackagingEnabled = FairPlayPackagingEnabled;
        }


        private void AddDynamicEncryptionFrame3_Load(object sender, EventArgs e)
        {
            groupBoxPlayReady.Enabled = _FairPlayPackagingEnabled;
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {

        }



        private void radioButtonExternalPRServer_CheckedChanged(object sender, EventArgs e)
        {
            panelExternalFairPlay.Enabled = radioButtonExternalPRServer.Checked;
            panelFairPlayFromAMS.Enabled = !radioButtonExternalPRServer.Checked;
            numericUpDownNbOptionsPlayReady.Enabled = !radioButtonExternalPRServer.Checked;
        }

        private void buttonImportPFX_Click(object sender, EventArgs e)
        {
            cert = DynamicEncryption.GetCertificateFromFile(false, X509KeyStorageFlags.Exportable);

            labelCertificateFile.Text = (cert.Certificate != null) ? cert.Certificate.SubjectName.Name : "(Error)";
            buttonOk.Enabled = (cert.Certificate != null);
        }

    }
}
