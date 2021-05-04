//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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

using Microsoft.Azure.Management.Media.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class form_DRM_Config_TokenClaims : Form
    {
        private readonly BindingList<MyTokenClaim> TokenClaimsList = new();
        private X509Certificate2 cert = null;

        public readonly List<ExplorerOpenIDSample> ListOpenIDSampleUris = new()
        {
                new ExplorerOpenIDSample() {Name= "Azure Active Directory", Uri="https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration"},
                new ExplorerOpenIDSample() {Name= "Google", Uri="https://accounts.google.com/.well-known/openid-configuration"}
              };


        public bool NeedToken => !radioButtonOpenAuthPolicy.Checked;

        public ContentKeyPolicyRestriction GetContentKeyPolicyRestriction
        {
            get
            {
                if (radioButtonOpenAuthPolicy.Checked)
                {
                    return new ContentKeyPolicyOpenRestriction();
                }
                else // token
                {
                    List<ContentKeyPolicyRestrictionTokenKey> alternateKeys = null;

                    ContentKeyPolicyRestrictionTokenKey primarykey = null;
                    if (GetDetailedTokenType == ExplorerTokenType.JWTSym)
                    {
                        primarykey = new ContentKeyPolicySymmetricTokenKey(SymmetricKey);
                    }
                    else if (GetDetailedTokenType == ExplorerTokenType.JWTX509)
                    {
                        primarykey = new ContentKeyPolicyX509CertificateTokenKey(GetX509Certificate.RawData);
                    }
                    // if OpenID, primary key is null

                    return new ContentKeyPolicyTokenRestriction(Issuer, Audience, primarykey, TokenType, alternateKeys, GetTokenRequiredClaims, GetOpenIdDiscoveryDocument);
                }
            }
        }

        public string Audience => textBoxAudience.Text;
        public string Issuer => textBoxIssuer.Text;

        public List<ContentKeyPolicyTokenClaim> GetTokenRequiredClaims
        {
            get
            {
                List<ContentKeyPolicyTokenClaim> mylist = new();
                foreach (MyTokenClaim j in TokenClaimsList)
                {
                    if (!string.IsNullOrEmpty(j.Type))
                    {
                        mylist.Add(new ContentKeyPolicyTokenClaim(j.Type, j.Value));
                    }
                }
                if (checkBoxRequiresContentKeyIdentifierClaim.Checked)
                {
                    mylist.Add(ContentKeyPolicyTokenClaim.ContentKeyIdentifierClaim);
                };
                return mylist;
            }
        }


        public static ContentKeyPolicyRestrictionTokenType TokenType => ContentKeyPolicyRestrictionTokenType.Jwt;


        public ExplorerTokenType GetDetailedTokenType
        {
            get
            {
                if (radioButtonOpenAuthPolicy.Checked)
                {
                    return ExplorerTokenType.NoToken;
                }
                else
                {
                    if (radioButtonJWTSymmetric.Checked)
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
        }

        public byte[] SymmetricKey
        {
            get
            {
                if (radioButtonContentKeyHex.Checked)
                {
                    return DynamicEncryption.HexStringToByteArray(textBoxSymKey.Text);
                }
                else
                {
                    return Convert.FromBase64String(textBoxSymKey.Text);
                }
            }
        }

        public X509Certificate2 GetX509Certificate => radioButtonJWTX509.Checked ? cert : null;

        public string GetOpenIdDiscoveryDocument => radioButtonJWTOpenId.Checked ? textBoxOpenIdDocument.Text : null;

        public form_DRM_Config_TokenClaims(int step, int option, string DRMName, string tokenSymmetricKey, bool laststep = true)
        {
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;

            Text = string.Format(Text, step);
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


        public string GetTestToken(string keyIdentifier, List<ContentKeyPolicyTokenClaim> requiredClaims, int tokenDuration, int? tokenUse)
        {
            if (radioButtonOpenAuthPolicy.Checked)
            {
                return null; // open mode
            }

            SigningCredentials signingcredentials;
            if (GetDetailedTokenType == ExplorerTokenType.JWTSym)
            {
                SymmetricSecurityKey tokenSigningKey = new(SymmetricKey);
                signingcredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(tokenSigningKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.Sha256Digest);
            }
            else if (GetDetailedTokenType == ExplorerTokenType.JWTX509 && cert != null)
            {
                signingcredentials = new X509SigningCredentials(cert);
            }
            else
            {
                throw new Exception();
            }

            List<Claim> claims = new();

            if (requiredClaims.Any(c => c.ClaimType == ContentKeyPolicyTokenClaim.ContentKeyIdentifierClaimType))
            {
                claims.Add(new Claim(ContentKeyPolicyTokenClaim.ContentKeyIdentifierClaim.ClaimType, keyIdentifier));
            }

            if (tokenUse != null)
            {
                claims.Add(new Claim("urn:microsoft:azure:mediaservices:maxuses", ((int)tokenUse).ToString()));
            }


            JwtSecurityToken token = new(
                                                        issuer: Issuer,
                                                        audience: Audience,
                                                        claims: claims.Count > 0 ? claims : null,
                                                        notBefore: DateTime.Now.AddMinutes(-5),
                                                        expires: DateTime.Now.AddMinutes(tokenDuration),
                                                        signingCredentials: signingcredentials
                                                        );


            JwtSecurityTokenHandler handler = new();

            return handler.WriteToken(token);
        }



        private void DRM_Config_TokenClaims_Load(object sender, EventArgs e)
        {
            // DpiUtils.InitPerMonitorDpi(this);

            dataGridViewTokenClaims.DataSource = TokenClaimsList;
            moreinfocGenX509.Links.Add(new LinkLabel.Link(0, moreinfocGenX509.Text.Length, "https://msdn.microsoft.com/en-us/library/azure/gg185932.aspx"));
            tabControlTokenType.TabPages.Remove(tabPageTokenX509);
            tabControlTokenType.TabPages.Remove(tabPageOpenId);

            foreach (ExplorerOpenIDSample map in ListOpenIDSampleUris)
            {
                comboBoxMappingList.Items.Add(map.Name);
            }
            comboBoxMappingList.SelectedIndex = 0;

            textBoxIssuer.Text = Properties.Settings.Default.DynEncTokenIssuerv3;
            textBoxAudience.Text = Properties.Settings.Default.DynEncTokenAudiencev3;
        }

        private void radioButtonToken_CheckedChanged(object sender, EventArgs e)
        {
            tabControlTokenProperties.Enabled = tabControlTokenType.Enabled = radioButtonTokenAuthPolicy.Checked;
            UpdateButtonOk();
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DynEncTokenIssuerv3 = textBoxIssuer.Text;
            Properties.Settings.Default.DynEncTokenAudiencev3 = textBoxAudience.Text;
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
            {
                tabControlTokenType.TabPages.Add(tabToAdd);
            }
        }

        private void UpdateButtonOk()
        {
            buttonOk.Enabled = (!radioButtonTokenAuthPolicy.Checked
                || (radioButtonTokenAuthPolicy.Checked && (radioButtonJWTSymmetric.Checked || radioButtonJWTOpenId.Checked || (radioButtonJWTX509.Checked && cert != null))));
        }

        private void buttongenerateContentKey_Click(object sender, EventArgs e)
        {
            GenerateSymKey();
        }

        private void GenerateSymKey()
        {
            radioButtonContentKeyBase64.Checked = true;

            byte[] TokenSigningKey = new byte[40];
            RNGCryptoServiceProvider rng = new();
            rng.GetBytes(TokenSigningKey);
            textBoxSymKey.Text = Convert.ToBase64String(new ContentKeyPolicySymmetricTokenKey(TokenSigningKey).KeyValue);
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
            ExplorerOpenIDSample entry = ListOpenIDSampleUris.Where(m => m.Name == comboBoxMappingList.Text).FirstOrDefault();
            textBoxOpenIdDocument.Text = entry.Uri;
        }

        private void Form_DRM_Config_TokenClaims_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelStep, e);
        }
    }
}
