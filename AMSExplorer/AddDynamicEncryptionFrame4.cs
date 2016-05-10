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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Security.Claims;
using System.IdentityModel.Tokens;

namespace AMSExplorer
{
    public partial class AddDynamicEncryptionFrame4 : Form
    {
        private BindingList<MyTokenClaim> TokenClaimsList = new BindingList<MyTokenClaim>();
        private X509Certificate2 cert = null;

        public readonly List<ExplorerOpenIDSample> ListOpenIDSampleUris = new List<ExplorerOpenIDSample> {
                new ExplorerOpenIDSample() {Name= "Azure Active Directory", Uri="https://login.windows.net/common/.well-known/openid-configuration"},
                new ExplorerOpenIDSample() {Name= "Google", Uri="https://accounts.google.com/.well-known/openid-configuration"}
              };

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


        public ExplorerTokenType GetDetailedTokenType
        {
            get
            {
                if (radioButtonSWT.Checked)
                {
                    return ExplorerTokenType.SWT;
                }
                else if (radioButtonJWTSymmetric.Checked)
                {
                    return ExplorerTokenType.JWTSym;
                }
                else if (radioButtonJWTX509.Checked)
                {
                    return ExplorerTokenType.JWTX509;
                }
                else
                {
                    return ExplorerTokenType.JWTOpenID;
                }
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


        public string GetOpenIdDiscoveryDocument
        {
            get
            {
                return radioButtonJWTOpenId.Checked ? textBoxOpenIdDocument.Text : null;
            }
        }

        private CloudMediaContext _context;

        public AddDynamicEncryptionFrame4(CloudMediaContext context, int step, int option, string DRMName, string tokenSymmetricKey, bool laststep = true)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;

            this.Text = string.Format(this.Text, step);
            labelStep.Text = string.Format(labelStep.Text, step, option, DRMName);

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
            tabControlTokenType.TabPages.Remove(tabPageOpenId);

            foreach (var map in ListOpenIDSampleUris)
            {
                comboBoxMappingList.Items.Add(map.Name);
            }
            comboBoxMappingList.SelectedIndex = 0;

            textBoxIssuer.Text = Properties.Settings.Default.DynEncTokenIssuer;
            textBoxAudience.Text = Properties.Settings.Default.DynEncTokenAudience;
        }



        private void radioButtonToken_CheckedChanged(object sender, EventArgs e)
        {
            tabControlTokenProperties.Enabled = tabControlTokenType.Enabled = radioButtonTokenAuthPolicy.Checked;
            UpdateButtonOk();
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DynEncTokenIssuer = textBoxIssuer.Text;
            Properties.Settings.Default.DynEncTokenAudience = textBoxAudience.Text;
            Program.SaveAndProtectUserConfig();
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
            cert = DynamicEncryption.GetCertificateFromFile(false).Certificate;
            labelCertificateFile.Text = (cert != null) ? cert.SubjectName.Name : "(Error)";
            UpdateButtonOk();
        }

        private void radioButtonTokenType_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                panelJWT.Enabled = radioButtonJWTX509.Checked;
                panelSymKey.Enabled = !radioButtonJWTX509.Checked;

                if (radioButtonJWTX509.Checked)
                {
                    DisplayTab(tabPageTokenX509);
                }
                else if (radioButtonJWTOpenId.Checked)
                {
                    DisplayTab(tabPageOpenId);
                }
                else
                {
                    DisplayTab(tabPageTokenSymmetric);
                }
                UpdateButtonOk();
            }
        }

        private void DisplayTab(TabPage tabToAdd)
        {
            bool found = false;
            foreach (TabPage tab in tabControlTokenType.TabPages)
            {
                if (tab.Name == tabToAdd.Name)
                {
                    found = true;
                }
                else if (tab.Name != "tabPageTokenType")
                {
                    tabControlTokenType.TabPages.Remove(tab);
                }
            }
            if (!found)
                tabControlTokenType.TabPages.Add(tabToAdd);
        }

        private void UpdateButtonOk()
        {
            buttonOk.Enabled = (!radioButtonTokenAuthPolicy.Checked
                || (radioButtonTokenAuthPolicy.Checked && (radioButtonSWT.Checked || radioButtonJWTSymmetric.Checked || radioButtonJWTOpenId.Checked || (radioButtonJWTX509.Checked && cert != null))));
        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            GenerateSymKey();
        }

        private void GenerateSymKey()
        {
            radioButtonContentKeyBase64.Checked = true;
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

        private void buttonAddMapping_Click(object sender, EventArgs e)
        {
            var entry = ListOpenIDSampleUris.Where(m => m.Name == comboBoxMappingList.Text).FirstOrDefault();
            textBoxOpenIdDocument.Text = entry.Uri;
        }
    }
}
