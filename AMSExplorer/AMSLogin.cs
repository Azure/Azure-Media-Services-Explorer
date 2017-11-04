//----------------------------------------------------------------------------------------------
//    Copyright 2018 Microsoft Corporation
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
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace AMSExplorer
{
    public partial class AMSLogin : Form
    {
        ListCredentials CredentialList = new ListCredentials();
        //CredentialsEntry CurrentCredential;

        // strings for field API
        private const string _Default = "Default";
        private const string _Partner = "Partner";
        private const string _Other = "Other";
        private const string CustomString = "Custom";
        private bool pageTabAADPresent = true;
        private bool pageTabACSPresent = true;

        public CloudMediaContext context;
        public string accountName;

        private string[] labelEntry1;
        private string[] labelEntry2;

        public CredentialsEntry LoginCredentials;

        public CredentialsEntry GenerateLoginCredentials
        {
            get
            {
                string mgtprtal = "";
                if (radioButtonAADAut.Checked /*&& ReturnDeploymentName() == CustomString*/)
                {
                    mgtprtal = textBoxAADManagementPortal.Text;
                }
                else
                {
                    mgtprtal = textBoxManagementPortal.Text;
                }

                return new CredentialsEntry(
               textBoxAccountName.Text,
               textBoxAccountKey.Text,
               textBoxAADtenant.Text,
               textBoxRestAPIEndpoint.Text,
               textBoxBlobKey.Text,
               textBoxDescription.Text,
               radioButtonAADAut.Checked && radioButtonAADInteractive.Checked,
               radioButtonAADAut.Checked && radioButtonAADServicePrincipal.Checked,
               radioButtonPartner.Checked,
               radioButtonOther.Checked,
               textBoxAPIServer.Text,
               textBoxScope.Text,
               textBoxACSBaseAddress.Text,
               textBoxAzureEndpoint.Text,
               mgtprtal,
               (radioButtonAADOther.Checked && ReturnDeploymentName() != CustomString) ? ReturnDeploymentName() : null,
               (radioButtonAADOther.Checked && ReturnDeploymentName() == CustomString) ? ReturnADCustomSettings() : null
                             );
            }
        }


        public AMSLogin()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void AMSLogin_Load(object sender, EventArgs e)
        {
            labelEntry1 = labelE1.Text.Split('|');
            labelEntry2 = labelE2.Text.Split('|');

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LoginListJSON))
            {
                // JSon deserialize
                CredentialList = (ListCredentials)JsonConvert.DeserializeObject(Properties.Settings.Default.LoginListJSON, typeof(ListCredentials));
                // Display accounts in the list
                CredentialList.MediaServicesAccounts.ForEach(c =>
                    AddItemToListviewAccounts(c)
                );
            }
            buttonExport.Enabled = (listViewAccounts.Items.Count > 0);

            accountmgtlink.Links.Add(new LinkLabel.Link(0, accountmgtlink.Text.Length, Constants.LinkAMSCreateAccount));
            linkLabelAADAut.Links.Add(new LinkLabel.Link(0, linkLabelAADAut.Text.Length, Constants.LinkAMSAADAut));

            foreach (var map in CredentialsEntry.ACSMappings)
            {
                comboBoxMappingList.Items.Add(map.Name);
            }
            comboBoxMappingList.SelectedIndex = 0;

            comboBoxAADMappingList.Items.Add(new Item("Azure China", nameof(AzureEnvironments.AzureChinaCloudEnvironment)));
            comboBoxAADMappingList.Items.Add(new Item("Azure Germany", nameof(AzureEnvironments.AzureGermanCloudEnvironment)));
            comboBoxAADMappingList.Items.Add(new Item("Azure US Government", nameof(AzureEnvironments.AzureUsGovernmentEnvironment)));
            comboBoxAADMappingList.Items.Add(new Item(CustomString, CustomString));
            comboBoxAADMappingList.SelectedIndex = 0;

            // version
            labelVersion.Text = String.Format(labelVersion.Text, Assembly.GetExecutingAssembly().GetName().Version);

            UpdateTexboxUI();
            UpdateAADSettingsTextBoxes();
        }

        private void AddItemToListviewAccounts(CredentialsEntry c)
        {
            var item = listViewAccounts.Items.Add(c.ReturnAccountName());
            if (!c.UseAADInteract && !c.UseAADServicePrincipal)
            {
                listViewAccounts.Items[item.Index].ForeColor = Color.Red;
                listViewAccounts.Items[item.Index].ToolTipText = "Configured for deprecated ACS authentication. Please use Azure Active Directory.";
            }
            else
            {
                listViewAccounts.Items[item.Index].ForeColor = Color.Black;
                listViewAccounts.Items[item.Index].ToolTipText = null;

            }
        }

        private string ReturnDeploymentName()
        {
            if (radioButtonAADOther.Checked)
                return ((Item)comboBoxAADMappingList.SelectedItem).Value;
            else
                return null;
        }


        private AzureEnvironment ReturnADCustomSettings()
        {
            if (ReturnDeploymentName() == CustomString)
            {
                AzureEnvironment env = null;
                try
                {
                    env = new AzureEnvironment(new Uri(textBoxAADAzureEndpoint.Text), textBoxAADAMSResource.Text, textBoxAADClienid.Text, new Uri(textBoxAADRedirect.Text));
                }
                catch
                {
                    return null;
                }
                return env;
            }
            else
                return null;
        }

        private string ReturnAzureEndpoint(string mystring)
        {
            return mystring.Split("|".ToCharArray())[0];
        }

        private string ReturnManagementPortal(string mystring)
        {
            string[] temp = mystring.Split("|".ToCharArray());
            return temp.Count() > 1 ? temp[1] : string.Empty;
        }


        private void buttonSaveToList_Click(object sender, EventArgs e)
        {
            LoginCredentials = GenerateLoginCredentials;

            // New code for JSON
            if (string.IsNullOrEmpty(LoginCredentials.ReturnAccountName()))
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.AMSLogin_buttonSaveToList_Click_TheAccountNameCannotBeEmpty);
                return;
            }

            var entryWithSameName = CredentialList.MediaServicesAccounts.Where(c => c.ReturnAccountName().ToLower().Trim() == LoginCredentials.ReturnAccountName().ToLower().Trim()).FirstOrDefault();
            // if found the same name
            if (entryWithSameName != null)
            {
                CredentialList.MediaServicesAccounts[CredentialList.MediaServicesAccounts.IndexOf(entryWithSameName)] = LoginCredentials;
            }
            else
            {
                CredentialList.MediaServicesAccounts.Add(LoginCredentials);
                //listViewAccounts.Items.Add(ReturnAccountName(LoginCredentials));
                AddItemToListviewAccounts(LoginCredentials);

            }
            Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
            Program.SaveAndProtectUserConfig();
        }

        private void buttonDeleteAccount_Click(object sender, EventArgs e)
        {
            int index = listViewAccounts.SelectedIndices[0];
            if (index > -1)
            {
                CredentialList.MediaServicesAccounts.RemoveAt(index);
                Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                Program.SaveAndProtectUserConfig();

                listViewAccounts.Items.Clear();
                CredentialList.MediaServicesAccounts.ForEach(c => AddItemToListviewAccounts(c) /*listViewAccounts.Items.Add(ReturnAccountName(c))*/);

                if (listViewAccounts.Items.Count > 0)
                {
                    listViewAccounts.Items[0].Selected = true;
                }
                else
                {
                    buttonDeleteAccountEntry.Enabled = false; // no selected item, so login button not active
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LoginCredentials = GenerateLoginCredentials;

            if (string.IsNullOrEmpty(LoginCredentials.ReturnAccountName()))
            {
                MessageBox.Show(string.Format("The {0} cannot be empty.", labelE1.Text), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var accName = LoginCredentials.ReturnAccountName();

            var entryWithSameName = CredentialList.MediaServicesAccounts.Where(c => c.ReturnAccountName().ToLower().Trim() == accName.ToLower().Trim()).FirstOrDefault();
            // if found the same name
            if (entryWithSameName == null)  // not found
            {
                var result = MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_DoYouWantToSaveTheCredentialsFor0, accName), AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_SaveCredentials, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) // ok to save
                {
                    CredentialList.MediaServicesAccounts.Add(LoginCredentials);
                    Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                    Program.SaveAndProtectUserConfig();

                    AddItemToListviewAccounts(LoginCredentials);
                    //listViewAccounts.Items.Add(ReturnAccountName(LoginCredentials));
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else // found
            {
                if (!LoginCredentials.Equals(entryWithSameName)) // changed ?
                {
                    var result = MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_DoYouWantToUpdateTheCredentialsFor0, accName), AMSExplorer.Properties.Resources.AMSLogin_listBoxAccounts_SelectedIndexChanged_UpdateCredentials, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes) // ok to update the credentials
                    {
                        CredentialList.MediaServicesAccounts[CredentialList.MediaServicesAccounts.IndexOf(entryWithSameName)] = LoginCredentials;
                        Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                        Program.SaveAndProtectUserConfig();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            if (LoginCredentials.UseAADServicePrincipal)  // service principal mode
            {
                var spcrendentialsform = new AMSLoginServicePrincipal();
                if (spcrendentialsform.ShowDialog() == DialogResult.OK)
                {
                    LoginCredentials.ADSPClientId = spcrendentialsform.ClientId;
                    LoginCredentials.ADSPClientSecret = spcrendentialsform.ClientSecret;
                }
                else
                {
                    return;
                }
            }

            // OLD ACS Mode - let's warm the user
            if (!LoginCredentials.UseAADServicePrincipal && !LoginCredentials.UseAADInteract)
            {
                var f = new DisplayBox("Warning", AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_ACSAuthenticationWarning, 10);
                f.ShowDialog();
            }

            // Context creation
            this.Cursor = Cursors.WaitCursor;

            context = Program.ConnectAndGetNewContext(LoginCredentials, false, true);

            accName = LoginCredentials.ReturnAccountName();

            try
            {
                var a = context.Assets.FirstOrDefault();
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetErrorMessage(ex), "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }

            accountName = accName;
            this.DialogResult = DialogResult.OK;  // form will close with OK result
                                                  // else --> form won't close...
        }


        private void listViewAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoginCredentials = GenerateLoginCredentials;

            buttonDeleteAccountEntry.Enabled = (listViewAccounts.SelectedIndices.Count > 0); // no selected item, so login button not active
            buttonExport.Enabled = (listViewAccounts.Items.Count > 0);
            if (listViewAccounts.SelectedIndices.Count > 0) // one selected
            {
                if (LoginCredentials != null)
                {
                    var entryWithSameName = CredentialList.MediaServicesAccounts.Where(c => c.ReturnAccountName().ToLower().Trim() == LoginCredentials.ReturnAccountName().ToLower().Trim()).FirstOrDefault();

                    if (entryWithSameName != null && !LoginCredentials.Equals(entryWithSameName))
                    {
                        var result = MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_DoYouWantToUpdateTheCredentialsFor0, LoginCredentials.ReturnAccountName()), AMSExplorer.Properties.Resources.AMSLogin_listBoxAccounts_SelectedIndexChanged_UpdateCredentials, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes) // ok to update the credentials
                        {
                            CredentialList.MediaServicesAccounts[CredentialList.MediaServicesAccounts.IndexOf(entryWithSameName)] = LoginCredentials;
                            Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                            Program.SaveAndProtectUserConfig();
                        }
                    }
                }

                var entry = CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]];

                radioButtonAADAut.Checked = entry.UseAADInteract || entry.UseAADServicePrincipal;
                radioButtonAADServicePrincipal.Checked = entry.UseAADServicePrincipal;
                radioButtonAADInteractive.Checked = !radioButtonAADServicePrincipal.Checked;

                if (entry.ADCustomSettings != null)  // custom settings
                {
                    radioButtonAADOther.Checked = true;
                    comboBoxAADMappingList.SelectedIndex = comboBoxAADMappingList.Items.Count - 1; // last one

                    textBoxAADAMSResource.Text = entry.ADCustomSettings.MediaServicesResource;
                    textBoxAADClienid.Text = entry.ADCustomSettings.MediaServicesSdkClientId;
                    textBoxAADRedirect.Text = entry.ADCustomSettings.MediaServicesSdkRedirectUri.ToString();
                    textBoxAADAzureEndpoint.Text = entry.ADCustomSettings.ActiveDirectoryEndpoint.ToString();
                    textBoxAADManagementPortal.Text = entry.OtherManagementPortal;

                }
                else if (entry.ADDeploymentName != null)
                {
                    radioButtonAADOther.Checked = true;
                    int index = 0;
                    foreach (var it in comboBoxAADMappingList.Items)
                    {
                        if (((Item)it).Value == entry.ADDeploymentName)
                        {
                            break;
                        }
                        index++;
                    }
                    comboBoxAADMappingList.SelectedIndex = index;
                }
                else
                {
                    radioButtonAADProd.Checked = true;
                }

                textBoxAccountName.Text = entry.AccountName;
                textBoxAccountKey.Text = entry.AccountKey;
                textBoxAADtenant.Text = entry.ADTenantDomain;
                textBoxRestAPIEndpoint.Text = entry.ADRestAPIEndpoint;
                textBoxBlobKey.Text = entry.DefaultStorageKey;
                textBoxDescription.Text = entry.Description;
                radioButtonACSAut.Checked = !entry.UseAADInteract && !entry.UseAADServicePrincipal; ;
                radioButtonAADAut.Checked = entry.UseAADInteract || entry.UseAADServicePrincipal;
                radioButtonPartner.Checked = entry.UsePartnerAPI;
                radioButtonOther.Checked = entry.UseOtherAPI;
                textBoxAPIServer.Text = entry.OtherAPIServer;
                textBoxScope.Text = entry.OtherScope;
                textBoxACSBaseAddress.Text = entry.OtherACSBaseAddress;
                textBoxAzureEndpoint.Text = entry.OtherAzureEndpoint;
                textBoxManagementPortal.Text = entry.OtherManagementPortal;

                // if not partner or other, then defaut
                if (!radioButtonPartner.Checked && !radioButtonOther.Checked)
                {
                    radioButtonProd.Checked = true;
                }

                // to clear or set the error
                CheckTextBox((object)textBoxAccountName);
                CheckTextBox((object)textBoxAccountKey);
                CheckTextBox((object)textBoxAADtenant);
                CheckTextBox((object)textBoxRestAPIEndpoint);

                UpdateTexboxUI();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            DoClearFields();
        }

        private void DoClearFields()
        {
            textBoxAccountName.Text =
            textBoxAccountKey.Text =
            textBoxAADtenant.Text =
            textBoxRestAPIEndpoint.Text =
            textBoxBlobKey.Text =
            textBoxDescription.Text =
            textBoxACSBaseAddress.Text =
            textBoxAPIServer.Text =
            textBoxScope.Text =
            textBoxAzureEndpoint.Text = string.Empty;

            radioButtonProd.Checked = true;
            radioButtonAADAut.Checked = true;
            radioButtonACSAut.Checked = false;
            radioButtonAADInteractive.Checked = true;

            int i = 0;
            foreach (var item in listViewAccounts.Items)
            {
                listViewAccounts.Items[i].Selected = false;
                i++;
            }
        }

        private void radioButtonOther_CheckedChanged(object sender, EventArgs e)
        {
            textBoxACSBaseAddress.Enabled =
                                            textBoxAPIServer.Enabled =
                                            textBoxScope.Enabled =
                                            textBoxAzureEndpoint.Enabled =
                                            textBoxManagementPortal.Enabled =
                                            buttonAddMapping.Enabled =
                                            comboBoxMappingList.Enabled =
                                            radioButtonOther.Checked;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            bool exportAll = true;

            if (CredentialList.MediaServicesAccounts.Count > 1 && listViewAccounts.SelectedIndices.Count > 0) // There are more than one entry and one has been selected. Let's ask if user want to export all or not
            {
                var diag = System.Windows.Forms.MessageBox.Show(AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_DoYouWantToExportAllEntriesNNSelectYesToExportAllNoToExportTheSelection, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_ExportAllEntries, System.Windows.Forms.MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (diag)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        exportAll = false;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            DialogResult diares = saveFileDialog1.ShowDialog();
            if (diares == DialogResult.OK)
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.Formatting = Newtonsoft.Json.Formatting.Indented;

                    if (exportAll)
                    {
                        System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(CredentialList, settings));
                    }
                    else
                    {
                        var copyEntry = new ListCredentials();
                        copyEntry.MediaServicesAccounts.Add(CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]]);
                        System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(copyEntry, settings));
                    }
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonImportAll_Click(object sender, EventArgs e)
        {
            bool mergesentries = false;

            if (CredentialList.MediaServicesAccounts.Count > 0) // There are entries. Let's ask if user want to delete them or merge
            {
                if (System.Windows.Forms.MessageBox.Show(AMSExplorer.Properties.Resources.AMSLogin_buttonImportAll_Click_ThereAreCurrentEntriesInTheListNDoYouWantReplaceThemWithTheNewOnesOrDoAMergeNNSelectYesToReplaceThemNoToMergeThem, AMSExplorer.Properties.Resources.AMSLogin_buttonImportAll_Click_ImportAndReplace, System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    mergesentries = true;
                }
            }

            DialogResult diares = openFileDialog1.ShowDialog();
            if (diares == DialogResult.OK)
            {

                if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".xml")  // XML file
                {
                    XDocument xmlimport = new XDocument();

                    System.IO.Stream myStream = openFileDialog1.OpenFile();
                    xmlimport = XDocument.Load(myStream);

                    var test = xmlimport.Descendants("Credentials").FirstOrDefault();
                    Version version = new Version(xmlimport.Descendants("Credentials").Attributes("Version").FirstOrDefault().Value.ToString());

                    if ((test != null) && (version >= new Version("1.0")))
                    {
                        if (!mergesentries)
                        {
                            CredentialList.MediaServicesAccounts.Clear();
                            // let's purge entries if user does not want to keep them
                        }
                        try
                        {
                            foreach (var att in xmlimport.Descendants("Credentials").Elements("Entry"))
                            {
                                string OtherManagementPortal = "";
                                if ((version >= new Version("1.1")) && (att.Attribute("OtherManagementPortal")) != null)
                                {
                                    OtherManagementPortal = att.Attribute("OtherManagementPortal").Value.ToString();
                                }

                                string OtherAzureEndpoint = "";
                                if (att.Attribute("OtherAzureEndpoint") != null)
                                {
                                    OtherAzureEndpoint = att.Attribute("OtherAzureEndpoint").Value.ToString();
                                }

                                CredentialList.MediaServicesAccounts.Add(new CredentialsEntry(
                                        att.Attribute("AccountName").Value.ToString(),
                                        att.Attribute("AccountKey").Value.ToString(),
                                        string.Empty, // client id not stored in XML
                                        string.Empty, // client secret not stored in XML
                                        att.Attribute("StorageKey").Value.ToString(),
                                        att.Attribute("Description").Value.ToString(),
                                        false,
                                        false,
                                        att.Attribute("UsePartnerAPI").Value.ToString() == true.ToString() ? true : false,
                                        att.Attribute("UseOtherAPI").Value.ToString() == true.ToString() ? true : false,
                                        att.Attribute("OtherAPIServer").Value.ToString(),
                                        att.Attribute("OtherScope").Value.ToString(),
                                        att.Attribute("OtherACSBaseAddress").Value.ToString(),
                                        OtherAzureEndpoint,
                                        OtherManagementPortal
                                    ));
                            }
                        }
                        catch
                        {
                            MessageBox.Show(AMSExplorer.Properties.Resources.AMSLogin_buttonImportAll_Click_FileNotRecognizedOrIncorrect);
                            return;

                        }

                        listViewAccounts.Items.Clear();
                        DoClearFields();
                        CredentialList.MediaServicesAccounts.ForEach(c => AddItemToListviewAccounts(c) /* listViewAccounts.Items.Add(ReturnAccountName(c))*/);
                        buttonExport.Enabled = (listViewAccounts.Items.Count > 0);

                        // let's save the list of credentials in settings
                        Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                        Program.SaveAndProtectUserConfig();
                    }
                    else
                    {
                        MessageBox.Show("Wrong XML file.");
                        return;
                    }
                }

                else if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".json")
                {

                    string json = System.IO.File.ReadAllText(openFileDialog1.FileName);

                    if (!mergesentries)
                    {
                        CredentialList.MediaServicesAccounts.Clear();
                        // let's purge entries if user does not want to keep them
                    }

                    var ImportedCredentialList = (ListCredentials)JsonConvert.DeserializeObject(json, typeof(ListCredentials));
                    CredentialList.MediaServicesAccounts.AddRange(ImportedCredentialList.MediaServicesAccounts);

                    listViewAccounts.Items.Clear();
                    DoClearFields();
                    CredentialList.MediaServicesAccounts.ForEach(c => AddItemToListviewAccounts(c) /* listViewAccounts.Items.Add(ReturnAccountName(c))*/);
                    buttonExport.Enabled = (listViewAccounts.Items.Count > 0);

                    // let's save the list of credentials in settings
                    Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                    Program.SaveAndProtectUserConfig();
                    LoginCredentials = null;
                }
            }
        }

        private void accountmgtlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void AMSLogin_Shown(object sender, EventArgs e)
        {
            Program.CheckAMSEVersion();
        }


        private void textBoxURL_Validation(object sender, EventArgs e)
        {
            TextBox mytextbox = (TextBox)sender;
            mytextbox.BackColor = (Uri.IsWellFormedUriString(mytextbox.Text, UriKind.Absolute)) ? Color.White : Color.Pink;
        }

        private void textBoxTXT_Validation(object sender, EventArgs e)
        {
            TextBox mytextbox = (TextBox)sender;
            mytextbox.BackColor = (string.IsNullOrWhiteSpace(mytextbox.Text.Trim())) ? Color.Pink : Color.White;
        }


        private void buttonAddMapping_Click(object sender, EventArgs e)
        {
            ACSEndPointMapping EPM = CredentialsEntry.ACSMappings.Where(m => m.Name == comboBoxMappingList.Text).FirstOrDefault();

            textBoxAPIServer.Text = EPM.APIServer;
            textBoxACSBaseAddress.Text = EPM.ACSBaseAddress;
            textBoxScope.Text = EPM.Scope;
            textBoxAzureEndpoint.Text = EPM.AzureEndpoint;
            textBoxManagementPortal.Text = EPM.ManagementPortal;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxAccountName_Validating(object sender, CancelEventArgs e)
        {
            CheckTextBox(sender);
        }

        private void CheckTextBox(object sender)
        {
            TextBox tb = (TextBox)sender;

            if (string.IsNullOrEmpty(tb.Text))
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.AMSLogin_CheckTextBox_ThisFieldIsMandatory);
            }
            else
            {
                errorProvider1.SetError(tb, String.Empty);
            }
        }

        private void CheckTextBoxGuid(object sender)
        {
            TextBox tb = (TextBox)sender;

            try
            {
                if (!string.IsNullOrWhiteSpace(tb.Text))
                {
                    var g = new Guid(tb.Text);
                }
                errorProvider1.SetError(tb, String.Empty);
            }

            catch
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.AMSLogin_CheckTextBoxGuid_BadGUIDFormat);
            }
        }

        private void textBoxAccountKey_Validating(object sender, CancelEventArgs e)
        {
            CheckTextBox(sender);
        }

        private void CheckTextBoxGuid(object sender, CancelEventArgs e)
        {
            CheckTextBoxGuid(sender);
        }

        private void listBoxAcounts_DoubleClick(object sender, EventArgs e)
        {
            // Proceed to log in to the selected account in the listbox
            buttonLogin_Click(sender, e);
        }

        private void radioButtonAADInteract_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTexboxUI();
        }

        private void UpdateTexboxUI()
        {
            if (radioButtonAADAut.Checked)
            {
                labelE1.Text = labelEntry1[1];
                labelE2.Text = labelEntry2[1];
                if (!pageTabAADPresent)
                {
                    tabControlAMS.TabPages.Add(tabPageAAD);
                    pageTabAADPresent = true;
                }
                if (pageTabACSPresent)
                {
                    tabControlAMS.TabPages.Remove(tabPageACS);
                    pageTabACSPresent = false;
                }
            }
            else // ACS
            {
                labelE1.Text = labelEntry1[0];
                labelE2.Text = labelEntry2[0];
                if (pageTabAADPresent)
                {
                    tabControlAMS.TabPages.Remove(tabPageAAD);
                    pageTabAADPresent = false;
                }
                if (!pageTabACSPresent)
                {
                    tabControlAMS.TabPages.Add(tabPageACS);
                    pageTabACSPresent = true;
                }
            }

            linkLabelAADAut.Visible = textBoxAADtenant.Visible = textBoxRestAPIEndpoint.Visible = radioButtonAADAut.Checked;
            textBoxAccountName.Visible = textBoxAccountKey.Visible = radioButtonACSAut.Checked;
            groupBoxAADMode.Visible = radioButtonAADAut.Checked;
        }

        private void textBoxAADtenant_Validating(object sender, CancelEventArgs e)
        {
            CheckTextBox(sender);
        }

        private void textBoxRestAPIEndpoint_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (string.IsNullOrEmpty(tb.Text))
            {
                errorProvider1.SetError(tb, AMSExplorer.Properties.Resources.AMSLogin_CheckTextBox_ThisFieldIsMandatory);
            }
            else
            {
                bool Error = false;
                try
                {
                    var url = new Uri(tb.Text);
                }
                catch
                {
                    Error = true;
                }

                if (Error)
                {
                    errorProvider1.SetError(tb, "Please insert a valid URL");

                }
                else
                {
                    errorProvider1.SetError(tb, String.Empty);

                }
            }
        }


        private void radioButtonAADOther_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAADMappingList.Enabled = radioButtonAADOther.Checked;
            UpdateAADSettingsTextBoxes();
        }

        private void comboBoxAADMappingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAADSettingsTextBoxes();
        }

        private void UpdateAADSettingsTextBoxes()
        {
            if (radioButtonAADProd.Checked)
            {
                textBoxAADAMSResource.Enabled =
                     textBoxAADClienid.Enabled =
                     textBoxAADRedirect.Enabled =
                     textBoxAADAzureEndpoint.Enabled =
                     textBoxAADManagementPortal.Enabled = false;

                AADEndPointMapping entrymapping = CredentialsEntry.AADMappings.Where(m => m.Name == nameof(AzureEnvironments.AzureCloudEnvironment)).FirstOrDefault();

                var env = AzureEnvironments.AzureCloudEnvironment;
                textBoxAADAMSResource.Text = env.MediaServicesResource;
                textBoxAADClienid.Text = env.MediaServicesSdkClientId;
                textBoxAADRedirect.Text = env.MediaServicesSdkRedirectUri.ToString();
                textBoxAADAzureEndpoint.Text = env.ActiveDirectoryEndpoint.ToString();
                textBoxAADManagementPortal.Text = entrymapping.ManagementPortal;
            }
            else
            {
                if (((Item)comboBoxAADMappingList.SelectedItem).Value == CustomString)
                {
                    textBoxAADAMSResource.Enabled =
                    textBoxAADClienid.Enabled =
                    textBoxAADRedirect.Enabled =
                    textBoxAADAzureEndpoint.Enabled =
                    textBoxAADManagementPortal.Enabled = true;

                    textBoxAADAMSResource.Text =
                    textBoxAADClienid.Text =
                    textBoxAADRedirect.Text =
                    textBoxAADAzureEndpoint.Text =
                    textBoxAADManagementPortal.Text = "";
                }
                else
                {
                    textBoxAADAMSResource.Enabled =
                    textBoxAADClienid.Enabled =
                    textBoxAADRedirect.Enabled =
                    textBoxAADAzureEndpoint.Enabled =
                    textBoxAADManagementPortal.Enabled = false;

                    AADEndPointMapping entrymapping = CredentialsEntry.AADMappings.Where(m => m.Name == ((Item)comboBoxAADMappingList.SelectedItem).Value).FirstOrDefault();

                    var env = CredentialsEntry.ReturnADEnvironment(((Item)comboBoxAADMappingList.SelectedItem).Value);
                    textBoxAADAMSResource.Text = env.MediaServicesResource;
                    textBoxAADClienid.Text = env.MediaServicesSdkClientId;
                    textBoxAADRedirect.Text = env.MediaServicesSdkRedirectUri.ToString();
                    textBoxAADAzureEndpoint.Text = env.ActiveDirectoryEndpoint.ToString();
                    textBoxAADManagementPortal.Text = entrymapping.ManagementPortal;
                }
            }
        }

        private void radioButtonACSAut_CheckedChanged(object sender, EventArgs e)
        {
            buttonLogin.Visible = radioButtonAADAut.Checked;
        }
    }
}
