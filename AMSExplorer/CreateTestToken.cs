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
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;

namespace AMSExplorer
{
    public partial class CreateTestToken : Form
    {
        private IContentKeyAuthorizationPolicyOption SelectedOption;
        private IContentKey KeyFromSelectedOption;

        private IAsset MyAsset;
        private BindingList<MyTokenClaim> TokenClaimsList = new BindingList<MyTokenClaim>();
        private X509Certificate2 cert = null;
        private CloudMediaContext mycontext;

        private List<IContentKey> ContentKeyDisplayed = new List<IContentKey>();

        public DateTime? StartDate
        {
            get
            {
                return (checkBoxStartDate.Checked) ? (DateTime)dateTimePickerStartDate.Value.ToUniversalTime() : (Nullable<DateTime>)null;
            }
            set
            {
                dateTimePickerStartDate.Value = (DateTime)value;
                dateTimePickerStartTime.Value = (DateTime)value;
            }
        }



        public bool PutContentKeyIdentifier
        {
            get { return checkBoxAddContentKeyIdentifierClaim.Checked; }
        }

        public string GetAudienceUri
        {
            get
            {
                return textBoxAudience.Text;
            }
        }
        public string GetIssuerUri
        {
            get
            {
                return textBoxIssuer.Text;
            }
        }


        public DateTime? EndDate
        {
            get
            {
                return (DateTime)dateTimePickerEndDate.Value.ToUniversalTime();
            }
            set
            {
                dateTimePickerEndDate.Value = (DateTime)value;
                dateTimePickerEndTime.Value = (DateTime)value;
            }
        }




        public IContentKeyAuthorizationPolicyOption GetOption
        {
            get
            {
                if (listViewAutOptions.SelectedIndices.Count > 0)
                {
                    return SelectedOption;
                }
                else
                {
                    return null;
                }
            }
        }

        public IContentKey GetContentKeyFromSelectedOption
        {
            get
            {
                if (listViewAutOptions.SelectedIndices.Count > 0)
                {
                    return KeyFromSelectedOption;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<Claim> GetTokenRequiredClaims
        {
            get
            {
                IList<Claim> mylist = new List<Claim>();
                foreach (var j in TokenClaimsList)
                {
                    if (!string.IsNullOrEmpty(j.Type))
                    {
                        mylist.Add(new Claim(j.Type, j.Value));
                    }
                }
                return mylist;
            }
        }

        public IList<TokenClaim> GetTokenRequiredTokenClaims
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



        public X509Certificate2 GetX509Certificate
        {
            get
            {
                return panelJWTX509Cert.Enabled ? cert : null;
            }
        }



        public CreateTestToken(IAsset _asset, CloudMediaContext _context, ContentKeyType? keytype = null, string optionid = null)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            MyAsset = _asset;
            mycontext = _context;

            var query = from key in MyAsset.ContentKeys
                        join autpol in _context.ContentKeyAuthorizationPolicies on key.AuthorizationPolicyId equals autpol.Id
                        select new { keyname = key.Name, keytype = key.ContentKeyType, keyid = key.Id, aupolid = autpol.Id };

            listViewAutOptions.BeginUpdate();
            listViewAutOptions.Items.Clear();
            foreach (var key in query)
            {
                var queryoptions = _context.ContentKeyAuthorizationPolicies.Where(a => a.Id == key.aupolid).FirstOrDefault().Options;

                foreach (var option in queryoptions)
                {
                    if (option.Restrictions.FirstOrDefault().KeyRestrictionType == (int)ContentKeyRestrictionType.TokenRestricted)
                    {
                        ListViewItem item = new ListViewItem(key.keytype.ToString());

                        IContentKey keyj = MyAsset.ContentKeys.Where(k => k.Id == key.keyid).FirstOrDefault();
                        ContentKeyDisplayed.Add(keyj);

                        item.SubItems.Add(option.Name);
                        item.SubItems.Add(option.Id);
                        string tokenTemplateString = option.Restrictions.FirstOrDefault().Requirements;
                        if (!string.IsNullOrEmpty(tokenTemplateString))
                        {
                            TokenRestrictionTemplate tokenTemplate = TokenRestrictionTemplateSerializer.Deserialize(tokenTemplateString);

                            item.SubItems.Add(tokenTemplate.TokenType == TokenType.JWT ? "JWT" : "SWT");
                            if (tokenTemplate.PrimaryVerificationKey != null)
                            {
                                item.SubItems.Add(tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey) ? "Symmetric" : "Asymmetric X509");
                            }
                            else if (tokenTemplate.OpenIdConnectDiscoveryDocument != null)
                            {
                                item.SubItems.Add("OpenID");
                            }
                        }
                        listViewAutOptions.Items.Add(item);
                        if (optionid == option.Id) listViewAutOptions.Items[listViewAutOptions.Items.IndexOf(item)].Selected = true;
                    }
                }
            }

            if (listViewAutOptions.Items.Count > 0 && listViewAutOptions.SelectedItems.Count == 0) // no selection, in that case, first line selected
            {
                listViewAutOptions.Items[0].Selected = true;
            }

            listViewAutOptions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewAutOptions.EndUpdate();
        }



        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = dateTimePickerStartDate.Value;
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Value = dateTimePickerStartTime.Value;
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerEndDate.Value;
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDate.Value = dateTimePickerEndTime.Value;
        }

        private void checkBoxStartDate_CheckedChanged_1(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = dateTimePickerStartTime.Enabled = checkBoxStartDate.Checked;
        }

        private void CreateTestToken_Load(object sender, EventArgs e)
        {
            dataGridViewTokenClaims.DataSource = TokenClaimsList;

        }



        private void listViewAutOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool NoVerifKey = false;
            if (listViewAutOptions.SelectedIndices.Count > 0)
            {
                var it = listViewAutOptions.SelectedItems[0];
                string stringoptionid = it.SubItems[2].Text;
                SelectedOption = mycontext.ContentKeyAuthorizationPolicyOptions.Where(p => p.Id == stringoptionid).FirstOrDefault();

                KeyFromSelectedOption = ContentKeyDisplayed[listViewAutOptions.SelectedItems[0].Index];

                string tokenTemplateString = SelectedOption.Restrictions.FirstOrDefault().Requirements;
                if (!string.IsNullOrEmpty(tokenTemplateString))
                {
                    TokenRestrictionTemplate tokenTemplate = TokenRestrictionTemplateSerializer.Deserialize(tokenTemplateString);
                    textBoxAudience.Text = tokenTemplate.Audience.ToString();
                    textBoxIssuer.Text = tokenTemplate.Issuer.ToString();
                    checkBoxAddContentKeyIdentifierClaim.Checked = false;
                    groupBoxStartDate.Enabled = (tokenTemplate.TokenType == TokenType.JWT);
                    if (tokenTemplate.PrimaryVerificationKey != null)
                    {
                        panelJWTX509Cert.Enabled = !(tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey));
                    }
                    else
                    {
                        NoVerifKey = true; // Case for OpenId for example. No way to create a test token....
                    }
                    TokenClaimsList.Clear();
                    foreach (var claim in tokenTemplate.RequiredClaims)
                    {
                        if (claim.ClaimType == TokenClaim.ContentKeyIdentifierClaimType)
                        {
                            checkBoxAddContentKeyIdentifierClaim.Checked = true;
                        }
                        else
                        {
                            TokenClaimsList.Add(new MyTokenClaim()
                            {
                                Type = claim.ClaimType,
                                Value = claim.ClaimValue
                            });
                        }
                    }
                }
                UpdateButtonOk(NoVerifKey);
            }
        }

        private void buttonDelClaim_Click(object sender, EventArgs e)
        {
            if (dataGridViewTokenClaims.SelectedRows.Count == 1)
            {
                TokenClaimsList.RemoveAt(dataGridViewTokenClaims.SelectedRows[0].Index);
            }
        }

        private void buttonAddClaim_Click(object sender, EventArgs e)
        {
            TokenClaimsList.AddNew();
        }

        private void buttonImportPFX_Click(object sender, EventArgs e)
        {
            cert = DynamicEncryption.GetCertificateFromFile(false);
            labelCertificateFile.Text = (cert != null) ? cert.SubjectName.Name : "(Error)";
            UpdateButtonOk();
        }

        private void UpdateButtonOk(bool forceDisableButton = false)
        {
            if (forceDisableButton)
                buttonOk.Enabled = false;
            else
                buttonOk.Enabled = (!panelJWTX509Cert.Enabled || (panelJWTX509Cert.Enabled && cert != null));


            if (!buttonOk.Enabled)
            {
                errorProvider1.SetError(buttonOk, "Test token cannot be generated (OpenID or no X509 Certificate loaded");
            }
            else
            {
                errorProvider1.SetError(buttonOk, String.Empty);
            }
        }
    }
}
