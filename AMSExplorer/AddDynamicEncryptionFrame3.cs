//----------------------------------------------------------------------- 
// <copyright file="AddDynamicEncryptionFrame3.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Security.Claims;
using System.IdentityModel.Tokens;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame3 : Form
    {
        private BindingList<MyTokenClaim> TokenClaimsList = new BindingList<MyTokenClaim>();
        private X509Certificate2 cert = null;

        public ContentKeyRestrictionType GetKeyRestrictionType
        {
            get
            {
                if (radioButtonOpenAuthPolicy.Checked)
                {
                    return ContentKeyRestrictionType.Open;
                }
                else // token
                {
                    return ContentKeyRestrictionType.TokenRestricted;
                }

            }
        }


        public string GetAudience
        {
            get
            {
                return textBoxAudience.Text;
            }
        }
        public string GetIssuer
        {
            get
            {
                return textBoxIssuer.Text;
            }
        }

        public bool AddContentKeyIdentifierClaim
        {
            get
            {
                return checkBoxAddContentKeyIdentifierClaim.Checked;
            }

        }

        public IList<TokenClaim> GetTokenRequiredClaims
        {
            get
            {
                IList<TokenClaim> mylist = new List<TokenClaim>();
                foreach (var j in TokenClaimsList)
                {
                    if (!string.IsNullOrEmpty(j.Type))
                    {
                        mylist.Add(new TokenClaim(j.Type, j.Value));
                    }
                }
                return mylist;
            }
        }

        public TokenType GetTokenType
        {
            get
            {
                return radioButtonSWT.Checked ? TokenType.SWT : TokenType.JWT;
            }
        }

        public bool IsKeySymmetric
        {
            get
            {
                return (radioButtonJWTSymmetric.Checked || radioButtonSWT.Checked) ? true : false;
            }
        }

        public string SymmetricKey
        {
            get
            {
                if (radioButtonContentKeyHex.Checked)
                {
                    return Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxSymKey.Text));
                }
                else
                    return textBoxSymKey.Text;
            }

        }


        public X509Certificate2 GetX509Certificate
        {
            get
            {
                return radioButtonJWTX509.Checked ? cert : null;
            }
        }

        private CloudMediaContext _context;

        public AddDynamicEncryptionFrame3(CloudMediaContext context, int step, int option, string tokenSymmetricKey, bool laststep = true)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;

            this.Text = string.Format(this.Text, step);
            labelStep.Text = string.Format(labelStep.Text, step, option);

            if (!laststep)
            {
                buttonOk.Text = "Next";
                buttonOk.Image = null;
            }
            if (tokenSymmetricKey == null)
            {
                GenerateSymKey();
            }
            else
            {
                textBoxSymKey.Text = tokenSymmetricKey;
            }
        }



        private void SetupDynEnc_Load(object sender, EventArgs e)
        {
            dataGridViewTokenClaims.DataSource = TokenClaimsList;
            moreinfocGenX509.Links.Add(new LinkLabel.Link(0, moreinfocGenX509.Text.Length, "https://msdn.microsoft.com/en-us/library/azure/gg185932.aspx"));
            tabControlTokenType.TabPages.Remove(tabPageTokenX509);
        }



        private void radioButtonToken_CheckedChanged(object sender, EventArgs e)
        {
            tabControlTokenProperties.Enabled = tabControlTokenType.Enabled = radioButtonTokenAuthPolicy.Checked;
            UpdateButtonOk();
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonOpen_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtonOk();
        }


        private void buttonAddClaim_Click(object sender, EventArgs e)
        {
            TokenClaimsList.AddNew();

        }

        private void buttonDelClaim_Click(object sender, EventArgs e)
        {
            if (dataGridViewTokenClaims.SelectedRows.Count == 1)
            {
                TokenClaimsList.RemoveAt(dataGridViewTokenClaims.SelectedRows[0].Index);
            }
        }

        private void buttonImportPFX_Click(object sender, EventArgs e)
        {
            cert = DynamicEncryption.GetCertificateFromFile(false);
            labelCertificateFile.Text = (cert != null) ? cert.SubjectName.Name : "(Error)";
            UpdateButtonOk();
        }

        private void radioButtonJWT_CheckedChanged(object sender, EventArgs e)
        {
            panelJWT.Enabled = radioButtonJWTX509.Checked;
            panelSymKey.Enabled = !radioButtonJWTX509.Checked;


            if (radioButtonJWTX509.Checked)
            {
                tabControlTokenType.TabPages.Remove(tabPageTokenSymmetric);
                tabControlTokenType.TabPages.Add(tabPageTokenX509);
            }
            else
            {
                tabControlTokenType.TabPages.Add(tabPageTokenSymmetric);
                tabControlTokenType.TabPages.Remove(tabPageTokenX509);
            }


            UpdateButtonOk();


            /*            //
                        tabControl.TabPages.Remove(tabPage);

            To put it back:
            tabControl.TabPages.Insert(index, tabPage);
             * */

        }

        private void radioButtonSWT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateButtonOk()
        {
            buttonOk.Enabled = (!radioButtonTokenAuthPolicy.Checked || (radioButtonTokenAuthPolicy.Checked && (radioButtonSWT.Checked || radioButtonJWTSymmetric.Checked || (radioButtonJWTX509.Checked && cert != null))));
        }

        private void radioButtonJWTSymmetric_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            GenerateSymKey();
        }

        private void GenerateSymKey()
        {
            textBoxSymKey.Text = Convert.ToBase64String(new SymmetricVerificationKey().KeyValue);

        }

        private void radioButtonContentKeyBase64_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyBase64.Checked)
            {
                try
                {
                    textBoxSymKey.Text = Convert.ToBase64String(DynamicEncryption.HexStringToByteArray(textBoxSymKey.Text));
                }
                catch
                {
                    textBoxSymKey.Text = string.Empty;
                }
            }

        }


        private void radioButtonContentKeyHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContentKeyHex.Checked)
            {
                try
                {
                    textBoxSymKey.Text = DynamicEncryption.ByteArrayToHexString(Convert.FromBase64String(textBoxSymKey.Text));
                }
                catch
                {
                    textBoxSymKey.Text = string.Empty;
                }
            }
        }
    }
}
