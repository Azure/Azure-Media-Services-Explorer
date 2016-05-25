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
    public partial class AddDynamicEncryptionFrame3_AESDelivery : Form
    {
        public Uri AESLaUrl
        {
            get
            {
                Uri myuri = null;
                if (!string.IsNullOrWhiteSpace(textBoxLAURL.Text))
                {
                    try
                    {
                        myuri = new Uri(textBoxLAURL.Text);
                    }
                    catch
                    {

                    }
                }

                return myuri;
            }
            set
            {
                textBoxLAURL.Text = value.ToString();
            }
        }


        public int GetNumberOfAuthorizationPolicyOptions // if 0, then no authorization policy. If > 0, then renturn the number of options
        {
            get
            {
                if (radioButtonNoAuthPolicy.Checked)
                {
                    return 0;
                }
                else
                {
                    return (int)numericUpDownNbOptions.Value;
                }
            }
        }



        private CloudMediaContext _context;

        public AddDynamicEncryptionFrame3_AESDelivery(CloudMediaContext context)
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

        private void radioButtonDefineAuthPol_CheckedChanged(object sender, EventArgs e)
        {
            buttonOk.Text = radioButtonDefineAuthPol.Checked ? "Next" : "Ok";
            ValidateButtonOk();
        }

        private void textBoxLAURL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxLAURL.Text))
                {
                    Uri myuri = new Uri(textBoxLAURL.Text);
                }
                errorProvider1.SetError(textBoxLAURL, String.Empty);
            }
            catch
            {
                errorProvider1.SetError(textBoxLAURL, "The key acquisition URL must be a valid URL");
            }
            ValidateButtonOk();
        }


        private void ValidateButtonOk()
        {
            buttonOk.Enabled = (errorProvider1.GetError(textBoxLAURL) == string.Empty)
                && 
                (radioButtonDefineAuthPol.Checked 
                ||
                (radioButtonNoAuthPolicy.Checked && !string.IsNullOrWhiteSpace(textBoxLAURL.Text))) ;
        }
    }
}
