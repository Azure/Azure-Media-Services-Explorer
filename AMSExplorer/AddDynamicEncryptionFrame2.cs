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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame2 : Form
    {
        private BindingList<MyTokenClaim> TokenClaimsList = new BindingList<MyTokenClaim>();
        private X509Certificate2 cert = null;

        public ContentKeyRestrictionType? GetKeyRestrictionType
        {
            get
            {
                if (radioButtonOpenAuthPolicy.Checked)
                {
                    return ContentKeyRestrictionType.Open;
                }
                else if (radioButtonTokenAuthPolicy.Checked)
                {
                    return ContentKeyRestrictionType.TokenRestricted;
                }
                else // PlayReady but no license delivery from Azure Media Services
                {
                    return null;
                }
            }
        }


        public Uri GetAudienceUri
        {
            get
            {
                return new Uri(textBoxAudience.Text);
            }
        }
        public Uri GetIssuerUri
        {
            get
            {
                return new Uri(textBoxIssuer.Text);
            }
        }

        public IList<TokenClaim> GetTokenRequiredClaims
        {
            get
            {
                IList<TokenClaim> mylist = new List<TokenClaim>();
                foreach (var j in TokenClaimsList)
                {
                    if (!string.IsNullOrEmpty(j.Type) && !string.IsNullOrEmpty(j.Value))
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

        public bool IsJWTKeySymmetric
        {
            get
            {
                return radioButtonJWTSymmetric.Checked ? true : false;
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

        public AddDynamicEncryptionFrame2(CloudMediaContext context, bool IsAES)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            if (IsAES)
            {
                radioButtonNoAuthPolicy.Visible = radioButtonNoAuthPolicy.Enabled = false;
            }
        }



        private void SetupDynEnc_Load(object sender, EventArgs e)
        {
            dataGridViewTokenClaims.DataSource = TokenClaimsList;
            moreinfocGenX509.Links.Add(new LinkLabel.Link(0, moreinfocGenX509.Text.Length, "https://msdn.microsoft.com/en-us/library/azure/gg185932.aspx"));
        }



        private void radioButtonToken_CheckedChanged(object sender, EventArgs e)
        {
            panelAutPol.Enabled = radioButtonTokenAuthPolicy.Checked;
            UpdateButtonOk();

        }


        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonOpen_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtonOk();
        }


        private void radioButtonNoAuthPolicy_CheckedChanged(object sender, EventArgs e)
        {
            // if (radioButtonNoAuthPolicy.Checked) radioButtonKeySpecifiedByUser.Checked = true;
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
            UpdateButtonOk();
        }

        private void radioButtonSWT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateButtonOk()
        {
            buttonOk.Enabled = (!radioButtonTokenAuthPolicy.Checked || (radioButtonTokenAuthPolicy.Checked && (radioButtonSWT.Checked || (radioButtonJWTX509.Checked && cert != null))));
        }
    }
}
