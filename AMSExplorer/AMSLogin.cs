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

namespace AMSExplorer
{
    public partial class AMSLogin : Form
    {
        ListCredentials CredentialList = new ListCredentials();
        CredentialsEntry CurrentCredential;

        // strings for field API
        private const string _Default = "Default";
        private const string _Partner = "Partner";
        private const string _Other = "Other";

        public readonly IList<EndPointMapping> Mappings = new List<EndPointMapping> {
            // Global
            new EndPointMapping() {Name=AMSExplorer.Properties.Resources.AMSLogin_AzureGlobal, APIServer= "https://media.windows.net/API/", Scope= "urn:WindowsAzureMediaServices", ACSBaseAddress ="https://wamsprodglobal001acs.accesscontrol.windows.net", AzureEndpoint= "windows.net",ManagementPortal="https://portal.azure.com"}, 
            // China
            new EndPointMapping() {Name=AMSExplorer.Properties.Resources.AMSLogin_AzureInChina,APIServer= "https://wamsbjbclus001rest-hs.chinacloudapp.cn/API/", Scope= "urn:WindowsAzureMediaServices", ACSBaseAddress ="https://wamsprodglobal001acs.accesscontrol.chinacloudapi.cn", AzureEndpoint= "chinacloudapi.cn",ManagementPortal="https://portal.azure.cn"}, 
            // Government
            new EndPointMapping() {Name=AMSExplorer.Properties.Resources.AMSLogin_AzureGovernment,APIServer= "https://ams-usge-1-hos-rest-1-1.usgovcloudapp.net/API/", Scope= "urn:WindowsAzureMediaServices", ACSBaseAddress ="https://ams-usge-0-acs-global-1-1.accesscontrol.usgovcloudapi.net", AzureEndpoint= "usgovcloudapi.net",ManagementPortal="https://portal.azure.us"}
        };

        public CredentialsEntry LoginCredentials
        {
            get
            {
                return new CredentialsEntry(
               textBoxAccountName.Text,
               textBoxAccountKey.Text,
               textBoxBlobKey.Text,
               textBoxAccountID.Text,
               textBoxDescription.Text,
               radioButtonPartner.Checked,
               radioButtonOther.Checked,
               textBoxAPIServer.Text,
               textBoxScope.Text,
               textBoxACSBaseAddress.Text,
               textBoxAzureEndpoint.Text,
               textBoxManagementPortal.Text
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
            var CredentialsList = Properties.Settings.Default.LoginList;

            // let's purge the old list now.
            if (CredentialsList != null && CredentialsList.Count > 0) Properties.Settings.Default.LoginList.Clear();

            // OLD MODE. XML properties for account entries
            if (!Properties.Settings.Default.MigratedLoginListToJSON && CredentialsList != null && CredentialsList.Count > 0)
            {
                try
                {
                    if (CredentialsList != null && CredentialsList.Count > 0)
                    {
                        for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                            listBoxAcounts.Items.Add(CredentialsList[i * CredentialsEntry.StringsCount]);
                        buttonExport.Enabled = (listBoxAcounts.Items.Count > 0);
                    }
                    else
                    {
                        // if null or empty, let's create it
                        CredentialsList = new StringCollection();
                    }
                }
                catch // error, let's purge all
                {
                    MessageBox.Show(AMSExplorer.Properties.Resources.AMSLogin_AMSLogin_Load_ErrorReadingCredentialsSettingsHaveBeenDeleted);
                    Properties.Settings.Default.LoginList.Clear();
                    Program.SaveAndProtectUserConfig();
                    listBoxAcounts.Items.Clear();
                }

                // Migration to JSON

                for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                {

                    CredentialList.MediaServicesAccounts.Add(new CredentialsEntry(
                        CredentialsList[i * CredentialsEntry.StringsCount],
                        CredentialsList[i * CredentialsEntry.StringsCount + 1],
                        CredentialsList[i * CredentialsEntry.StringsCount + 2],
                        string.Empty,
                        CredentialsList[i * CredentialsEntry.StringsCount + 3],
                        CredentialsList[i * CredentialsEntry.StringsCount + 4] == true.ToString() ? true : false,
                        CredentialsList[i * CredentialsEntry.StringsCount + 5] == true.ToString() ? true : false,
                        CredentialsList[i * CredentialsEntry.StringsCount + 6],
                        CredentialsList[i * CredentialsEntry.StringsCount + 7],
                        CredentialsList[i * CredentialsEntry.StringsCount + 8],
                        ReturnAzureEndpoint(CredentialsList[i * CredentialsEntry.StringsCount + 9]),
                        ReturnManagementPortal(CredentialsList[i * CredentialsEntry.StringsCount + 9])
                    ));
                }

                var NewCredentialListJSON = JsonConvert.SerializeObject(CredentialList);
                Properties.Settings.Default.LoginListJSON = NewCredentialListJSON;
                Properties.Settings.Default.MigratedLoginListToJSON = true;
                Program.SaveAndProtectUserConfig();

            }
            else // Standard mode. New installation or migration already done
            {
                if (!Properties.Settings.Default.MigratedLoginListToJSON)
                {
                    Properties.Settings.Default.MigratedLoginListToJSON = true;
                    Program.SaveAndProtectUserConfig();
                }

                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LoginListJSON))
                {
                    CredentialList = (ListCredentials)JsonConvert.DeserializeObject(Properties.Settings.Default.LoginListJSON, typeof(ListCredentials));
                    CredentialList.MediaServicesAccounts.ForEach(c => listBoxAcounts.Items.Add(c.AccountName));
                }
                buttonExport.Enabled = (listBoxAcounts.Items.Count > 0);
            }

            accountmgtlink.Links.Add(new LinkLabel.Link(0, accountmgtlink.Text.Length, Constants.LinkAMSCreateAccount));
            foreach (var map in Mappings)
            {
                comboBoxMappingList.Items.Add(map.Name);
            }
            comboBoxMappingList.SelectedIndex = 0;

            // version
            labelVersion.Text = String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version);
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
            /*
            if (string.IsNullOrEmpty(textBoxAccountName.Text))
            {
                MessageBox.Show("The account name cannot be empty.");
                return;
            }
            CredentialsEntry myCredentials = new CredentialsEntry(textBoxAccountName.Text, textBoxAccountKey.Text, textBoxBlobKey.Text, textBoxDescription.Text, radioButtonPartner.Checked.ToString(), radioButtonOther.Checked.ToString(), textBoxAPIServer.Text, textBoxScope.Text, textBoxACSBaseAddress.Text, textBoxAzureEndpoint.Text, textBoxManagementPortal.Text);
            if (CredentialsList == null) CredentialsList = new StringCollection();

            //let's find if the account name is already in the list
            int foundindex = -1;
            for (int i = 0; i < CredentialsList.Count; i += CredentialsEntry.StringsCount)
            {
                if (CredentialsList[i] == textBoxAccountName.Text)
                {
                    foundindex = i;
                    break;
                }
            }

            if (foundindex == -1) // not found
            {
                CredentialsList.AddRange(myCredentials.ToArray());
                Properties.Settings.Default.LoginList = CredentialsList;

                Program.SaveAndProtectUserConfig();

                listBoxAcounts.Items.Add(myCredentials.AccountName);
            }
            else
            {
                //found, let's update the entry et insert the new data
                for (int i = 0; i < CredentialsEntry.StringsCount; i++) CredentialsList.RemoveAt(foundindex);
                for (int i = 0; i < CredentialsEntry.StringsCount; i++) CredentialsList.Insert(foundindex + i, myCredentials.ToArray().Skip(i).Take(1).FirstOrDefault());
                Properties.Settings.Default.LoginList = CredentialsList;
                Program.SaveAndProtectUserConfig();
            }
            */

            // New code for JSON
            if (string.IsNullOrEmpty(textBoxAccountName.Text))
            {
                MessageBox.Show(AMSExplorer.Properties.Resources.AMSLogin_buttonSaveToList_Click_TheAccountNameCannotBeEmpty);
                return;
            }
            CredentialsEntry myCredentials = new CredentialsEntry(textBoxAccountName.Text, textBoxAccountKey.Text, textBoxBlobKey.Text, textBoxAccountID.Text, textBoxDescription.Text, radioButtonPartner.Checked, radioButtonOther.Checked, textBoxAPIServer.Text, textBoxScope.Text, textBoxACSBaseAddress.Text, textBoxAzureEndpoint.Text, textBoxManagementPortal.Text);

            var entryWithSameName = CredentialList.MediaServicesAccounts.Where(c => c.AccountName.ToLower().Trim() == textBoxAccountName.Text.ToLower().Trim()).FirstOrDefault();
            // if found the same name
            if (entryWithSameName != null)
            {
                CredentialList.MediaServicesAccounts[CredentialList.MediaServicesAccounts.IndexOf(entryWithSameName)] = myCredentials;
            }
            else
            {
                CredentialList.MediaServicesAccounts.Add(myCredentials);
            }
            Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
            Program.SaveAndProtectUserConfig();
        }

        private void buttonDeleteAccount_Click(object sender, EventArgs e)
        {
            /*
            int index = listBoxAcounts.SelectedIndex;
            try
            {
                for (int i = 0; i < CredentialsEntry.StringsCount; i++) CredentialsList.RemoveAt(index * CredentialsEntry.StringsCount);
                Properties.Settings.Default.LoginList = CredentialsList;
                Program.SaveAndProtectUserConfig();
                listBoxAcounts.Items.Clear();
                if (CredentialsList != null)
                {
                    for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                        listBoxAcounts.Items.Add(CredentialsList[i * CredentialsEntry.StringsCount]);

                }
                buttonDeleteAccountEntry.Enabled = false; // no selected item, so login button not active

            }
            catch // error, let's purge all
            {
                CredentialsList.Clear();
                Properties.Settings.Default.LoginList.Clear();
                Program.SaveAndProtectUserConfig();
                listBoxAcounts.Items.Clear();
            }
            */

            int index = listBoxAcounts.SelectedIndex;
            if (index > -1)
            {
                CredentialList.MediaServicesAccounts.RemoveAt(index);
                Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                Program.SaveAndProtectUserConfig();

                listBoxAcounts.Items.Clear();
                CredentialList.MediaServicesAccounts.ForEach(c => listBoxAcounts.Items.Add(c.AccountName));

                if (listBoxAcounts.Items.Count > 0)
                {
                    listBoxAcounts.SelectedIndex = 0;
                }
                else
                {
                    buttonDeleteAccountEntry.Enabled = false; // no selected item, so login button not active
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAccountName.Text))
            {
                MessageBox.Show("The account name cannot be empty.");
                return;
            }

            CredentialsEntry myCredentials = new CredentialsEntry(textBoxAccountName.Text, textBoxAccountKey.Text, textBoxBlobKey.Text, textBoxAccountID.Text, textBoxDescription.Text, radioButtonPartner.Checked, radioButtonOther.Checked, textBoxAPIServer.Text, textBoxScope.Text, textBoxACSBaseAddress.Text, textBoxAzureEndpoint.Text, textBoxManagementPortal.Text);

            var entryWithSameName = CredentialList.MediaServicesAccounts.Where(c => c.AccountName.ToLower().Trim() == textBoxAccountName.Text.ToLower().Trim()).FirstOrDefault();
            // if found the same name
            if (entryWithSameName == null)  // not found
            {
                //CredentialList.MediaServicesAccounts[CredentialList.MediaServicesAccounts.IndexOf(entryWithSameName)] = myCredentials;
                var result = MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_DoYouWantToSaveTheCredentialsFor0, textBoxAccountName.Text), AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_SaveCredentials, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) // ok to save
                {
                    CredentialList.MediaServicesAccounts.Add(myCredentials);
                    Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                    Program.SaveAndProtectUserConfig();

                    listBoxAcounts.Items.Add(myCredentials.AccountName);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else // found
            {
                if (!myCredentials.Equals(entryWithSameName)) // changed ?
                {
                    var result = MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_DoYouWantToUpdateTheCredentialsFor0, myCredentials.AccountName), AMSExplorer.Properties.Resources.AMSLogin_listBoxAccounts_SelectedIndexChanged_UpdateCredentials, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

            this.DialogResult = DialogResult.OK;  // form will close with OK result
                                                  // else --> form won't close...
        }



        private void listBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            buttonDeleteAccountEntry.Enabled = (listBoxAcounts.SelectedIndex > -1); // no selected item, so login button not active
            buttonExportAll.Enabled = (listBoxAcounts.Items.Count > 0);
            if (listBoxAcounts.SelectedIndex > -1) // one selected
            {
                textBoxAccountName.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount];
                textBoxAccountKey.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 1];
                textBoxBlobKey.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 2];
                textBoxDescription.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 3];
                radioButtonPartner.Checked = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 4] == true.ToString() ? true : false;
                radioButtonOther.Checked = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 5] == true.ToString() ? true : false;
                textBoxAPIServer.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 6];
                textBoxScope.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 7];
                textBoxACSBaseAddress.Text = CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 8];
                textBoxAzureEndpoint.Text = ReturnAzureEndpoint(CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 9]);
                textBoxManagementPortal.Text = ReturnManagementPortal(CredentialsList[listBoxAcounts.SelectedIndex * CredentialsEntry.StringsCount + 9]);

                // if not partner or other, then defaut
                if (!radioButtonPartner.Checked && !radioButtonOther.Checked) radioButtonProd.Checked = true;

                // to clear or set the error
                CheckTextBox((object)textBoxAccountName);
                CheckTextBox((object)textBoxAccountKey);
            }
            */

            buttonDeleteAccountEntry.Enabled = (listBoxAcounts.SelectedIndex > -1); // no selected item, so login button not active
            buttonExport.Enabled = (listBoxAcounts.Items.Count > 0);
            if (listBoxAcounts.SelectedIndex > -1) // one selected
            {
                if (CurrentCredential != null)
                {
                    var entryWithSameName = CredentialList.MediaServicesAccounts.Where(c => c.AccountName.ToLower().Trim() == CurrentCredential.AccountName.ToLower().Trim()).FirstOrDefault();

                    if (entryWithSameName != null && !LoginCredentials.Equals(CurrentCredential))
                    {
                        var result = MessageBox.Show(string.Format(AMSExplorer.Properties.Resources.AMSLogin_buttonLogin_Click_DoYouWantToUpdateTheCredentialsFor0, CurrentCredential.AccountName), AMSExplorer.Properties.Resources.AMSLogin_listBoxAccounts_SelectedIndexChanged_UpdateCredentials, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes) // ok to update the credentials
                        {
                            CredentialList.MediaServicesAccounts[CredentialList.MediaServicesAccounts.IndexOf(entryWithSameName)] = LoginCredentials;
                            Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                            Program.SaveAndProtectUserConfig();
                        }
                    }
                }

                var entry = CredentialList.MediaServicesAccounts[listBoxAcounts.SelectedIndex];

                textBoxAccountName.Text = entry.AccountName;
                textBoxAccountKey.Text = entry.AccountKey;
                textBoxBlobKey.Text = entry.DefaultStorageKey;
                textBoxAccountID.Text = entry.AccountId;
                textBoxDescription.Text = entry.Description;
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

                CurrentCredential = LoginCredentials;
            }
        }



        private void buttonClear_Click(object sender, EventArgs e)
        {
            DoClearFields();
        }

        private void DoClearFields()
        {
            textBoxAccountName.Text = string.Empty;
            textBoxAccountKey.Text = string.Empty;
            textBoxBlobKey.Text = string.Empty;
            textBoxAccountID.Text = string.Empty;
            textBoxDescription.Text = string.Empty;
            textBoxACSBaseAddress.Text = string.Empty; ;
            textBoxAPIServer.Text = string.Empty;
            textBoxScope.Text = string.Empty; ;
            textBoxAzureEndpoint.Text = string.Empty;
            radioButtonProd.Checked = true;
            listBoxAcounts.ClearSelected();
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
            /*     XDocument xmlexport = new XDocument();
                 xmlexport.Add(new XComment("Created by Azure Media Services Explorer"));
                 xmlexport.Add(new XElement("Credentials", new XAttribute("Version", "1.1")));

                 for (int i = 0; i < (CredentialsList.Count / CredentialsEntry.StringsCount); i++)
                 {
                     xmlexport.Descendants("Credentials").FirstOrDefault().Add(new XElement("Entry",
                         new XAttribute("AccountName", CredentialsList[i * CredentialsEntry.StringsCount]),
                           new XAttribute("AccountKey", CredentialsList[i * CredentialsEntry.StringsCount + 1]),
                        new XAttribute("StorageKey", CredentialsList[i * CredentialsEntry.StringsCount + 2]),
                        new XAttribute("Description", CredentialsList[i * CredentialsEntry.StringsCount + 3]),
                       new XAttribute("UsePartnerAPI", CredentialsList[i * CredentialsEntry.StringsCount + 4]),
                        new XAttribute("UseOtherAPI", CredentialsList[i * CredentialsEntry.StringsCount + 5]),
                        new XAttribute("OtherAPIServer", CredentialsList[i * CredentialsEntry.StringsCount + 6]),
                        new XAttribute("OtherScope", CredentialsList[i * CredentialsEntry.StringsCount + 7]),
                        new XAttribute("OtherACSBaseAddress", CredentialsList[i * CredentialsEntry.StringsCount + 8]),
                         new XAttribute("OtherAzureEndpoint", ReturnAzureEndpoint(CredentialsList[i * CredentialsEntry.StringsCount + 9])),
                           new XAttribute("OtherManagementPortal", ReturnManagementPortal(CredentialsList[i * CredentialsEntry.StringsCount + 9]))
                        ));

                 }
                 */

            bool exportAll = true;

            if (CredentialList.MediaServicesAccounts.Count > 1 && listBoxAcounts.SelectedIndex > -1) // There are more than one entry and one has been selected. Let's ask if user want to export all or not
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
                    if (exportAll)
                    {
                        System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(CredentialList, Newtonsoft.Json.Formatting.Indented));
                    }
                    else
                    {
                        var copyEntry = new ListCredentials();
                        copyEntry.MediaServicesAccounts.Add(CredentialList.MediaServicesAccounts[listBoxAcounts.SelectedIndex]);
                        System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(copyEntry, Newtonsoft.Json.Formatting.Indented));
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
                                        att.Attribute("StorageKey").Value.ToString(),
                                        string.Empty, // media services id not stored in XML
                                        att.Attribute("Description").Value.ToString(),
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

                        listBoxAcounts.Items.Clear();
                        DoClearFields();
                        CredentialList.MediaServicesAccounts.ForEach(c => listBoxAcounts.Items.Add(c.AccountName));
                        buttonExport.Enabled = (listBoxAcounts.Items.Count > 0);

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

                    listBoxAcounts.Items.Clear();
                    DoClearFields();
                    CredentialList.MediaServicesAccounts.ForEach(c => listBoxAcounts.Items.Add(c.AccountName));
                    buttonExport.Enabled = (listBoxAcounts.Items.Count > 0);

                    // let's save the list of credentials in settings
                    Properties.Settings.Default.LoginListJSON = JsonConvert.SerializeObject(CredentialList);
                    Program.SaveAndProtectUserConfig();
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
            EndPointMapping EPM = Mappings.Where(m => m.Name == comboBoxMappingList.Text).FirstOrDefault();

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
    }
}
