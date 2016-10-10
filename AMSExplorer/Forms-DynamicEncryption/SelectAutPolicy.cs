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


using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;

namespace AMSExplorer
{
    public partial class SelectAutPolicy : Form
    {
        private CloudMediaContext _context;
        private List<IContentKeyAuthorizationPolicy> autPolicies;
        private ContentKeyType _keyType;

        public IContentKeyAuthorizationPolicy SelectedPolicy
        {
            get
            {
                if (listViewPolicies.SelectedIndices.Count > 0)
                {
                    return autPolicies.Skip(listViewPolicies.SelectedIndices[0]).Take(1).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }


        public SelectAutPolicy(CloudMediaContext context, ContentKeyType keyType)
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
            _context = context;
            _keyType = keyType;
        }

        private void EncodingAMEStandardPickOverlay_Load(object sender, EventArgs e)
        {
            listViewPolicies.Tag = -1;
            listViewPolicies.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(ListViewItemComparerQuickNoDate.ListView_ColumnClick);

            ListPolicies();
        }

        private void ListPolicies()
        {
            this.Cursor = Cursors.WaitCursor;
            listViewPolicies.Items.Clear();
            dataGridViewAutPolOption.Rows.Clear();
            listViewAutPolOptions.Items.Clear();

            if (_keyType == ContentKeyType.EnvelopeEncryption)
            {
                autPolicies = _context.ContentKeyAuthorizationPolicies.ToList().Where(p => p.Options.Any(o => o.KeyDeliveryType == ContentKeyDeliveryType.BaselineHttp)).ToList();

            }
            else if (_keyType == ContentKeyType.CommonEncryption)
            {
                autPolicies = _context.ContentKeyAuthorizationPolicies.ToList().Where(p => p.Options.Any(o => o.KeyDeliveryType == ContentKeyDeliveryType.PlayReadyLicense || o.KeyDeliveryType == ContentKeyDeliveryType.Widevine)).ToList();

            }
            else if (_keyType == ContentKeyType.CommonEncryptionCbcs)
            {
                autPolicies = _context.ContentKeyAuthorizationPolicies.ToList().Where(p => p.Options.Any(o => o.KeyDeliveryType == ContentKeyDeliveryType.FairPlay)).ToList();
            }
            else
            {
                autPolicies = _context.ContentKeyAuthorizationPolicies.ToList();

            }

            listViewPolicies.BeginUpdate();
            foreach (var pol in autPolicies)
            {
                ListViewItem item = new ListViewItem(pol.Name, 0);
                item.SubItems.Add(pol.Id);
                listViewPolicies.Items.Add(item);
            }
            listViewPolicies.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewPolicies.EndUpdate();
            this.Cursor = Cursors.Default;
        }


        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDelete.Enabled = listViewPolicies.SelectedItems.Count > 0;
            buttonRename.Enabled = buttonSelect.Enabled= listViewPolicies.SelectedItems.Count ==1;
            DoDisplayKeyPropertiesAndAutOptions();
        }


        private void DoDisplayKeyPropertiesAndAutOptions()
        {
            listViewAutPolOptions.Items.Clear();
            dataGridViewAutPolOption.Rows.Clear();
            var myAuthPolicy = SelectedPolicy;
            if (myAuthPolicy != null)
            {
                listViewAutPolOptions.BeginUpdate();
                foreach (var option in myAuthPolicy.Options)
                {
                    ListViewItem item = new ListViewItem((string.IsNullOrEmpty(option.Name) ? "<no name>" : option.Name), 0);
                    listViewAutPolOptions.Items.Add(item);
                }
                listViewAutPolOptions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewAutPolOptions.EndUpdate();
                if (listViewAutPolOptions.Items.Count > 0) listViewAutPolOptions.Items[0].Selected = true;
            }
        }



        private void listViewAutPolOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoDisplayAuthorizationPolicyOption();
        }

        private void DoDisplayAuthorizationPolicyOption()
        {

            if (listViewAutPolOptions.SelectedItems.Count > 0 && SelectedPolicy != null)
            {
                dataGridViewAutPolOption.Rows.Clear();
                dataGridViewAutPolOption.ColumnCount = 2;
                dataGridViewAutPolOption.Columns[0].DefaultCellStyle.BackColor = Color.Gainsboro;


                IContentKeyAuthorizationPolicyOption option = SelectedPolicy.Options.Skip(listViewAutPolOptions.SelectedIndices[0]).Take(1).FirstOrDefault();
                if (option != null) // Token option
                {
                    dataGridViewAutPolOption.Rows.Add("Name", option.Name != null ? option.Name : "<no name>");
                    dataGridViewAutPolOption.Rows.Add("Id", option.Id);

                    // Key delivery configuration

                    int i = dataGridViewAutPolOption.Rows.Add("KeyDeliveryConfiguration", "<null>");
                    if (option.KeyDeliveryConfiguration != null)
                    {
                        DataGridViewButtonCell btn = new DataGridViewButtonCell();
                        dataGridViewAutPolOption.Rows[i].Cells[1] = btn;
                        dataGridViewAutPolOption.Rows[i].Cells[1].Value = "See value";
                        dataGridViewAutPolOption.Rows[i].Cells[1].Tag = option.KeyDeliveryConfiguration;
                    }

                    dataGridViewAutPolOption.Rows.Add("KeyDeliveryType", option.KeyDeliveryType);

                    List<ContentKeyAuthorizationPolicyRestriction> objList_restriction = option.Restrictions;
                    foreach (var restriction in objList_restriction)
                    {
                        dataGridViewAutPolOption.Rows.Add("Restriction Name", restriction.Name);
                        dataGridViewAutPolOption.Rows.Add("Restriction KeyRestrictionType", (ContentKeyRestrictionType)restriction.KeyRestrictionType);

                        if (restriction.Requirements != null)
                        {
                            // Restriction Requirements
                            i = dataGridViewAutPolOption.Rows.Add("Restriction Requirements", "<null>");
                            if (restriction.Requirements != null)
                            {
                                DataGridViewButtonCell btn2 = new DataGridViewButtonCell();
                                dataGridViewAutPolOption.Rows[i].Cells[1] = btn2;
                                dataGridViewAutPolOption.Rows[i].Cells[1].Value = "See value";
                                dataGridViewAutPolOption.Rows[i].Cells[1].Tag = restriction.Requirements;

                                TokenRestrictionTemplate tokenTemplate = TokenRestrictionTemplateSerializer.Deserialize(restriction.Requirements);
                                dataGridViewAutPolOption.Rows.Add("Token Type", tokenTemplate.TokenType);

                                i = dataGridViewAutPolOption.Rows.Add("Primary Verification Key", "<null>");
                                if (tokenTemplate.PrimaryVerificationKey != null)
                                {
                                    dataGridViewAutPolOption.Rows.Add("Token Verification Key Type", (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey)) ? "Symmetric" : "Asymmetric (X509)");
                                    if (tokenTemplate.PrimaryVerificationKey.GetType() == typeof(SymmetricVerificationKey))
                                    {
                                        var verifkey = (SymmetricVerificationKey)tokenTemplate.PrimaryVerificationKey;
                                        btn2 = new DataGridViewButtonCell();
                                        dataGridViewAutPolOption.Rows[i].Cells[1] = btn2;
                                        dataGridViewAutPolOption.Rows[i].Cells[1].Value = "See key value";
                                        dataGridViewAutPolOption.Rows[i].Cells[1].Tag = Convert.ToBase64String(verifkey.KeyValue);
                                    }
                                }


                                foreach (var verifkey in tokenTemplate.AlternateVerificationKeys)
                                {
                                    i = dataGridViewAutPolOption.Rows.Add("Alternate Verification Key", "<null>");
                                    if (verifkey != null)
                                    {
                                        dataGridViewAutPolOption.Rows.Add("Token Verification Key Type", (verifkey.GetType() == typeof(SymmetricVerificationKey)) ? "Symmetric" : "Asymmetric (X509)");
                                        if (verifkey.GetType() == typeof(SymmetricVerificationKey))
                                        {
                                            var verifkeySym = (SymmetricVerificationKey)verifkey;
                                            btn2 = new DataGridViewButtonCell();
                                            dataGridViewAutPolOption.Rows[i].Cells[1] = btn2;
                                            dataGridViewAutPolOption.Rows[i].Cells[1].Value = "See key value";
                                            dataGridViewAutPolOption.Rows[i].Cells[1].Tag = Convert.ToBase64String(verifkeySym.KeyValue);
                                        }
                                    }
                                }

                                if (tokenTemplate.OpenIdConnectDiscoveryDocument != null)
                                {
                                    dataGridViewAutPolOption.Rows.Add("OpenId Connect Discovery Document Uri", tokenTemplate.OpenIdConnectDiscoveryDocument.OpenIdDiscoveryUri);
                                }
                                dataGridViewAutPolOption.Rows.Add("Token Audience", tokenTemplate.Audience);
                                dataGridViewAutPolOption.Rows.Add("Token Issuer", tokenTemplate.Issuer);
                                foreach (var claim in tokenTemplate.RequiredClaims)
                                {
                                    dataGridViewAutPolOption.Rows.Add("Required Claim, Type", claim.ClaimType);
                                    dataGridViewAutPolOption.Rows.Add("Required Claim, Value", claim.ClaimValue);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DoDeleteAutPol();
        }

        private void DoDeleteAutPol()
        {

            if (listViewPolicies.SelectedIndices.Count > 0)
            {
                string question;
                int nbError = 0;
                string messagestr = "";

                if (listViewPolicies.SelectedIndices.Count == 1)
                {
                    question = "Are you sure that you want to DELETE this policy from the Azure Media Services account ?";
                }
                else
                {
                    question = string.Format("Are you sure that you want to DELETE these {0} policies from the Azure Media Services account ?", listViewPolicies.SelectedIndices.Count);
                }

                if (MessageBox.Show(question, "Authorization policy deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    foreach (var ind in listViewPolicies.SelectedIndices)
                    {
                        IContentKeyAuthorizationPolicy AuthPol = autPolicies.Skip((int)ind).Take(1).FirstOrDefault();

                        if (AuthPol != null)
                        {
                            try
                            {
                                AuthPol.Delete();

                            }
                            catch (Exception e)
                            {
                                nbError++;
                                if (e.InnerException != null)
                                {
                                    messagestr = Program.GetErrorMessage(e);
                                }
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;

                    if (nbError > 0)
                    {
                        messagestr = string.Format("Error when deleting {0} authorization policies.", nbError) + Constants.endline + messagestr;
                        MessageBox.Show(messagestr);
                    }

                    ListPolicies();

                }
            }
        }

        private void DoMenuRenamePolicy()
        {
            if (listViewPolicies.SelectedIndices.Count == 1)
            {
                IContentKeyAuthorizationPolicy AuthPol = autPolicies.Skip(listViewPolicies.SelectedIndices[0]).Take(1).FirstOrDefault();

                string value = AuthPol.Name;

                if (Program.InputBox("Policy rename", string.Format("Enter the new name for policy '{0}' :", AuthPol.Name), ref value) == DialogResult.OK)
                {
                    try
                    {
                        AuthPol.Name = value;
                        AuthPol.Update();
                        ListPolicies();
                    }
                    catch
                    {

                        MessageBox.Show("There is a problem when renaming the policy.");
                        return;
                    }
                }
            }
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            DoMenuRenamePolicy();
        }

        private void listViewPolicies_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewItemComparer.ListView_ColumnClick(sender, e);
        }

        private void SeeClearKey(string key)
        {
            var editform = new EditorXMLJSON("Value", key.ToString(), false, false);
            editform.Display();
        }

        private void dataGridViewAutPolOption_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0 && senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewButtonCell))
            {
                //TODO - Button Clicked - to see the key
                SeeClearKey(senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString());
            }
        }
    }
}
