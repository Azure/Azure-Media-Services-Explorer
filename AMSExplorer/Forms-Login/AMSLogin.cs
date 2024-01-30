//----------------------------------------------------------------------------------------------
//    Copyright 2023 Microsoft Corporation
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

using AMSClient;
using AMSExplorer.Properties;
using AMSExplorer.Ravnur;
using AMSExplorer.Rest;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Media;
using Microsoft.Identity.Client;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMSExplorer
{
    public partial class AmsLogin : Form
    {
        private ListCredentialsRPv4 CredentialList = new();

        public string accountName;

        private CredentialsEntryV4 LoginInfo;
        private AzureEnvironment environment;

        public AMSClientV3 AmsClient { get; private set; }

        public AmsLogin()
        {
            Font = new Font("Segoe UI", 9);
            InitializeComponent();
            Icon = Bitmaps.Azure_Explorer_ico;
        }

        private void AMSLogin_Load(object sender, EventArgs e)
        {
            //Properties.Settings.Default.LoginListRPv3JSON = "";
            //// DpiUtils.InitPerMonitorDpi(this);

            // Add a dummy column     
            ColumnHeader header = new()
            {
                Text = "",
                Name = "col1"
            };
            listViewAccounts.Columns.Add(header);
            // Then
            listViewAccounts.Scrollable = true;
            listViewAccounts.View = View.Details;


            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LoginListRPv4JSON))
            {

                var obj = (ListCredentialsRPv4)JsonConvert.DeserializeObject(Properties.Settings.Default.LoginListRPv4JSON, typeof(ListCredentialsRPv4));

                if (obj.Version >= 4)
                {
                    // new list v4
                    // JSon deserialize
                    CredentialList = (ListCredentialsRPv4)JsonConvert.DeserializeObject(Properties.Settings.Default.LoginListRPv4JSON, typeof(ListCredentialsRPv4));

                    // Display accounts in the list
                    CredentialList.MediaServicesAccounts.ForEach(c =>
                        AddItemToListviewAccounts(c)
                    );
                }
            }

            buttonExport.Enabled = (listViewAccounts.Items.Count > 0);

            linkLabelAMSRetire.Links.Add(new LinkLabel.Link(0, linkLabelAMSRetire.Text.Length, Constants.LinkAMSRetirement));

            // version
            labelVersion.Text = string.Format(labelVersion.Text, Assembly.GetExecutingAssembly().GetName().Version);

            DoEnableManualFields(false);
        }

        private void AddItemToListviewAccounts(CredentialsEntryV4 c)
        {
            ListViewItem item = listViewAccounts.Items.Add(c.AccountName);
            listViewAccounts.Items[item.Index].ForeColor = Color.Black;
            listViewAccounts.Items[item.Index].ToolTipText = null;
        }

        private void ButtonDeleteAccount_Click(object sender, EventArgs e)
        {
            Telemetry.TrackEvent("AMSLogin ButtonDeleteAccount_Click");

            // let's remove the selected items

            for (int i = listViewAccounts.Items.Count - 1; i >= 0; i--)
            {
                if (listViewAccounts.Items[i].Selected)
                {
                    listViewAccounts.Items[i].Remove();
                    CredentialList.MediaServicesAccounts.RemoveAt(i);
                }
            }

            SaveCredentialsToSettings();
        }

        private async void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedIndices.Count != 1)
            {
                return;
            }
            // code when used from pick-up
            LoginInfo = CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]];

            if (LoginInfo == null)
            {
                MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show(string.Format("The {0} cannot be empty.", labelE1.Text), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            AmsClient = new AMSClientV3(LoginInfo.Environment, LoginInfo.SubscriptionId, LoginInfo, this);

            MediaServicesAccountResource response;
            try
            {
                response = await AmsClient.ConnectAndGetNewClientV3Async(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (response == null)
            {
                return;
            }

            // let's save the credentials (SP) and MK/IO settings. They may be updated by the user when connecting
            CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]] = AmsClient.credentialsEntry;
            SaveCredentialsToSettings();

            try
            {   // let's refresh storage accounts
                // TODO2023 : to check if storing storage accounts is needed
                //AmsClient.credentialsEntry.MediaService.StorageAccounts = response.Data.StorageAccounts;

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetErrorMessage(ex), "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                return;
            }

            DialogResult = DialogResult.OK;  // form will close with OK result
                                             // else --> form won't close...
        }


        private void ListViewAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonLogin.Enabled = (listViewAccounts.SelectedIndices.Count == 1); // no selected item, so login button not active
            buttonDeleteAccountEntry.Enabled = (listViewAccounts.SelectedIndices.Count > 0); // no selected item, so login button not active
            buttonExport.Enabled = (listViewAccounts.Items.Count > 0);

            if (listViewAccounts.SelectedIndices.Count > 0) // one selected
            {
                LoginInfo = CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]];

                textBoxDescription.Text = LoginInfo.Description;
                textBoxResourceGroup.Text = LoginInfo.ResourceGroupName;
                textBoxAADtenantId.Text = LoginInfo.AadTenantId;
                textBoxSubscription.Text = LoginInfo.SubscriptionId;

                DoEnableManualFields(false);
                groupBoxAADAutMode.Visible = true;

                radioButtonAADInteractive.Checked = !LoginInfo.UseSPAuth;
                radioButtonAADServicePrincipal.Checked = LoginInfo.UseSPAuth;
            }
        }


        private void DoEnableManualFields(bool enable)
        {
            textBoxAADtenantId.Enabled =
            textBoxResourceGroup.Enabled =
                                    enable;
        }


        private void ButtonExport_Click(object sender, EventArgs e)
        {
            Telemetry.TrackEvent("AMSLogin ButtonExport_Click");

            bool exportSPSecrets = false;
            ExportSettings form = new();

            // There are more than one entry and one has been selected. 
            form.radioButtonAllEntries.Enabled = CredentialList.MediaServicesAccounts.Count > 1 && listViewAccounts.SelectedIndices.Count > 0;
            form.checkBoxIncludeSPSecrets.Checked = exportSPSecrets;


            if (form.ShowDialog() == DialogResult.OK)
            {
                bool exportAll = form.radioButtonAllEntries.Checked;
                exportSPSecrets = form.checkBoxIncludeSPSecrets.Checked;

                PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new();
                List<string> properties = new() { "EncryptedADSPClientSecret", "MKIOEncryptedToken", "RavnurEncryptedApiKey" };
                if (!exportSPSecrets)
                {
                    properties.Add("ClearADSPClientSecret");
                    properties.Add("MKIOClearToken");
                    properties.Add("RavnurClearApiKey");
                }

                jsonResolver.IgnoreProperty(typeof(CredentialsEntryV4), properties.ToArray()); // let's not export encrypted secret and may be clear secret

                JsonSerializerSettings settings = new()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    ContractResolver = jsonResolver
                };

                DialogResult diares = saveFileDialog1.ShowDialog();
                if (diares == DialogResult.OK)
                {
                    try
                    {
                        if (exportAll)
                        {
                            System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(CredentialList, settings));
                        }
                        else
                        {
                            ListCredentialsRPv4 copyEntry = new();

                            for (int i = 0; i < listViewAccounts.SelectedIndices.Count; i++)
                            {
                                copyEntry.MediaServicesAccounts.Add(CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[i]]);
                            }

                            System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(copyEntry, settings));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, AMSExplorer.Properties.Resources.AMSLogin_buttonExport_Click_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ButtonImportAll_Click(object sender, EventArgs e)
        {
            Telemetry.TrackEvent("AMSLogin ButtonImportAll_Click");

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
                if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".json")
                {
                    string json = System.IO.File.ReadAllText(openFileDialog1.FileName);

                    if (!mergesentries)
                    {
                        CredentialList.MediaServicesAccounts.Clear();
                        // let's purge entries if user does not want to keep them
                    }

                    ListCredentialsRPv4 ImportedCredentialList = null;
                    try
                    {
                        ImportedCredentialList = (ListCredentialsRPv4)JsonConvert.DeserializeObject(json, typeof(ListCredentialsRPv4));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error when importing json file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (ImportedCredentialList.Version < (new ListCredentialsRPv4()).Version)
                    {
                        MessageBox.Show("This file was created with an older version of AMSE. Import is not possible.", "Wrong version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    CredentialList.MediaServicesAccounts.AddRange(ImportedCredentialList.MediaServicesAccounts);

                    listViewAccounts.Items.Clear();
                    CredentialList.MediaServicesAccounts.ForEach(c => AddItemToListviewAccounts(c));
                    buttonExport.Enabled = (listViewAccounts.Items.Count > 0);

                    // let's save the list of credentials in settings
                    SaveCredentialsToSettings();
                }
            }
        }

        private void SaveCredentialsToSettings()
        {
            PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new();
            jsonResolver.IgnoreProperty(typeof(CredentialsEntryV4), "ClearADSPClientSecret"); // let's not save the clear SP secret
            jsonResolver.IgnoreProperty(typeof(CredentialsEntryV4), "MKIOClearToken"); // let's not save the MK/IO token secret
            jsonResolver.IgnoreProperty(typeof(CredentialsEntryV4), "RavnurClearApiKey"); // let's not save the clear Ravnur API Key
            JsonSerializerSettings settings = new() { ContractResolver = jsonResolver };
            Properties.Settings.Default.LoginListRPv4JSON = JsonConvert.SerializeObject(CredentialList, settings);
            Program.SaveAndProtectUserConfig();
        }

        private void Accountmgtlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = e.Link.LinkData as string,
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private async void AMSLogin_ShownAsync(object sender, EventArgs e)
        {
            Telemetry.TrackPageView(this.Name);

            ScaleListViewColumns(listViewAccounts);
            await Program.CheckWebView2VersionAsync();
            await Program.CheckAMSEVersionAsync();

            DisplayAMSRetirementNotice();
        }

        private void DisplayAMSRetirementNotice()
        {
            int days = new DateTime(2024, 6, 30).Subtract(DateTime.Now).Days;

            // we display the message only once a week
            if (Settings.Default.RetirementNotifDays - days >= 7)
            {
                MessageBox.Show("Azure Media Services will be retired on 30 June 2024.\r\n\r\nYou can continue to use Azure Media Services without any disruptions. After 30 June 2024, Azure Media Services won’t be supported, and customers won’t have access to their Azure Media Services accounts.\r\n\r\nTo avoid any service disruptions, you’ll need to transition to Azure Video Indexer for on-demand video and audio analysis workflows or to a Microsoft partner solution for all other media services workflows before 30 June 2024\r\n\r\nThis tool supports the migration of your assets to MK/IO and connecting to you Ravnur Media Services instance. More features may be added in the future to help your migration.", $"Retirement notice - {days} days left", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Settings.Default.RetirementNotifDays = days;
                Settings.Default.Save();
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

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
                errorProvider1.SetError(tb, string.Empty);
            }
        }


        private void ListBoxAcounts_DoubleClick(object sender, EventArgs e)
        {
            // Proceed to log in to the selected account in the listbox
            ButtonLogin_Click(sender, e);
        }


        private void TextBoxAADtenant_Validating(object sender, CancelEventArgs e)
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
                    Uri url = new(tb.Text);
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
                    errorProvider1.SetError(tb, string.Empty);

                }
            }
        }


        private async void buttonPickupAccount_Click(object sender, EventArgs e)
        {
            await PickUpAcountAsync();
        }

        private async Task PickUpAcountAsync()
        {
            AddAMSAccount1 addaccount1 = new();

            if (addaccount1.ShowDialog() == DialogResult.OK)
            {

                if (addaccount1.SelectedMode == AddAccountMode.BrowseSubscriptions)
                {
                    Telemetry.TrackEvent("AMSLogin buttonPickupAccount_Click BrowseSubscriptions");

                    Cursor = Cursors.WaitCursor;
                    Prompt prompt = addaccount1.SelectUser ? Prompt.ForceLogin : Prompt.NoPrompt;

                    environment = addaccount1.GetEnvironment();
                    var scopes = new[] { environment.AADSettings.TokenAudience.ToString() + "/user_impersonation" };
                    IPublicClientApplication appPickUp = PublicClientApplicationBuilder.Create(environment.ClientApplicationId)
                        .WithAuthority(environment.AADSettings.AuthenticationEndpoint.ToString() + "organizations")
                        .WithDefaultRedirectUri()
                        //.WithRedirectUri("http://localhost")
                        .WithBroker(true)
                        .Build();

                    AuthenticationResult accessToken = null;
                    var accounts = await appPickUp.GetAccountsAsync();
                    try
                    {
                        accessToken = await appPickUp.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
                    }
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (MsalUiRequiredException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                    {
                        try
                        {
                            accessToken = await appPickUp.AcquireTokenInteractive(scopes)
                                //.WithPrompt(addaccount1.SelectUser ? Prompt.ForceLogin : Prompt.SelectAccount)
                                //.WithCustomWebUi(new EmbeddedBrowserCustomWebUI(this))
                                //.WithAccount(null)
                                .WithParentActivityOrWindow(this.Handle)
                                .ExecuteAsync();
                        }
                        catch (MsalException exMsal)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(exMsal.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (accessToken == null)// User cancelled ?
                    {
                        Cursor = Cursors.Default;
                        return;
                    }

                    TokenCredentials credentials = new(accessToken.AccessToken, "Bearer");
                    var credentialForArmClient = new BearerTokenCredential(accessToken.AccessToken);
                    ArmClient armClient = new(credentialForArmClient);

                    // Subcriptions listing
                    var subscriptions = armClient.GetSubscriptions().ToList();

                    // Tenants listing
                    var tenants = armClient.GetTenants().ToList();
                    Cursor = Cursors.Default;

                    AddAMSAccount2Browse addaccount2 = new(credentials, subscriptions, environment, tenants, prompt, appPickUp, accessToken);

                    if (addaccount2.ShowDialog() == DialogResult.OK)
                    {
                        if (addaccount2.SelectMode) // An account has been selected
                        {
                            // Getting Media Services accounts...

                            CredentialsEntryV4 entry = new(
                                addaccount2.selectedAccount.Data.Name,
                                addaccount2.selectedAccount.Data.Id.SubscriptionId,
                                addaccount2.selectedAccount.Data.Id.ResourceGroupName,
                                environment,
                                addaccount1.SelectUser,
                                false,
                                addaccount2.selectedTenantId,
                                false
                               );

                            CredentialList.MediaServicesAccounts.Add(entry);
                            AddItemToListviewAccounts(entry);
                            SaveCredentialsToSettings();
                        }
                        else // creation mode
                        {
                            var subscription = addaccount2.SelectedSubscription;
                            var myLocations = subscription.GetLocations().AsEnumerable();
                            var accessTokenToUse = addaccount2.AccessTokenForTenants[subscription.Data.TenantId.ToString()];
                            var armClientToUse = addaccount2.ArmClientForTenants[subscription.Data.TenantId.ToString()];

                            // Getting Media Services accounts...
                            var listMediaServices = subscription.GetMediaServicesAccounts().ToList();

                            // let's get the list of availability zones
                            AzureProviders aP = new(environment.ArmEndpoint);

                            var list = await aP.GetProvidersAsync(subscription.Data.SubscriptionId, "Microsoft.Network", addaccount2.AccessTokenForTenants[subscription.Data.TenantId.ToString()]);
                            // we cannot use yet the ARM client as we need REST 2020-06-01
                            // var list = await armClientToUse.GetTenantResourceProviderAsync("Microsoft.Network");

                            // we cannot use yet the ARM client as we need REST 2020-06-01
                            // var listNatGateways = list.Value.ResourceTypes.Where(r => r.ResourceType == "natGateways").FirstOrDefault();
                            var listNatGateways = list.ResourceTypes.Where(r => r.ResourceType == "natGateways").FirstOrDefault();

                            List<string> listRegionWithAvailabilityZone = new();
                            if (listNatGateways != null)
                            {
                                listRegionWithAvailabilityZone = listNatGateways.ZoneMappings.Where(z => z.Zones.Count >= 3).Select(z => z.Location).ToList();
                            }

                            //var listMediaServicesLocations = listMediaServices.First().GetAvailableLocations().Value;
                            //var myLocationsWithMS = myLocations.Where(l => listMediaServicesLocations.Contains(l.DisplayName)).ToList();


                            var listAMS = await armClientToUse.GetTenantResourceProviderAsync("Microsoft.Media");
                            // var listAMS = await aP.GetProvidersAsync(subscription.Data.SubscriptionId, "Microsoft.Media", addaccount2.AccessTokenForTenants[subscription.Data.TenantId.ToString()]);
                            var myLocationsWithMS = listAMS.Value.ResourceTypes.Where(r => r.ResourceType == "mediaservices").First().Locations.ToList();

                            CreateAccount createAccount = new(myLocationsWithMS, new TokenCredentials(accessTokenToUse, "Bearer"), listRegionWithAvailabilityZone, subscription);
                            if (createAccount.ShowDialog() == DialogResult.OK)
                            {
                                // Add account to list
                                CredentialsEntryV4 entry = new(
                                  createAccount.MediaServiceCreated.Data.Id.Name,
                                  createAccount.MediaServiceCreated.Data.Id.SubscriptionId,
                                  createAccount.MediaServiceCreated.Data.Id.ResourceGroupName,
                                  environment,
                                  addaccount1.SelectUser,
                                  false,
                                  addaccount2.selectedTenantId,
                                  false
                                  );

                                CredentialList.MediaServicesAccounts.Add(entry);
                                AddItemToListviewAccounts(entry);
                                SaveCredentialsToSettings();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }


                // Get info from the Portal or Azure CLI JSON
                else if (addaccount1.SelectedMode == AddAccountMode.FromAzurePortalJson)
                {
                    Telemetry.TrackEvent("AMSLogin buttonPickupAccount_Click FromAzureCliOrPortalJson");

                    string example = @"{";


                    if (addaccount1.JsonWithServicePrincipal)
                    {
                        example += @"
  ""AZURE_CLIENT_ID"": ""00000000-0000-0000-0000-000000000000"",
  ""AZURE_CLIENT_SECRET"": ""00000000-0000-0000-0000-000000000000"",";
                    }

                    example += @"
  ""AZURE_TENANT_DOMAIN"": ""microsoft.onmicrosoft.com"",
  ""AZURE_TENANT_ID"": ""00000000-0000-0000-0000-000000000000"",
  ""AZURE_MEDIA_SERVICES_ACCOUNT_NAME"": ""amsaccount"",
  ""AZURE_RESOURCE_GROUP"": ""amsResourceGroup"",
  ""AZURE_SUBSCRIPTION_ID"": ""00000000-0000-0000-0000-000000000000"",
  ""AZURE_ARM_TOKEN_AUDIENCE"": ""https://management.core.windows.net/"",
  ""AZURE_ARM_ENDPOINT"": ""https://management.azure.com/"",
  ""AZURE_AAD_ENDPOINT"": ""https://login.microsoftonline.com""
}";
                    EditorXMLJSON form = new("Enter the JSON output of the Azure Portal or Azure Cli Service Principal creation", example, true, ShowSampleMode.None, true, "The Service Principal secret is stored encrypted in the application settings.");

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        JsonFromAzurePortal json;
                        try
                        {
                            json = (JsonFromAzurePortal)JsonConvert.DeserializeObject(form.TextData, typeof(JsonFromAzurePortal));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error reading the json", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string resourceId = string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}", json.SubscriptionId, json.ResourceGroup, json.AccountName);

                        // environment
                        ActiveDirectoryServiceSettings aadSettings = new()
                        {
                            AuthenticationEndpoint = json.AadEndpoint ?? addaccount1.GetEnvironment().AADSettings.AuthenticationEndpoint,
                            TokenAudience = json.ArmTokenAudience,
                            ValidateAuthority = true
                        };

                        AzureEnvironment env = new(AzureEnvType.Custom) { AADSettings = aadSettings, ArmEndpoint = json.ArmEndpoint };

                        CredentialsEntryV4 entry = new(
                                                        json.AccountName,
                                                        json.SubscriptionId,
                                                        json.ResourceGroup,
                                                        env,
                                                        true,
                                                        addaccount1.JsonWithServicePrincipal,
                                                        json.AadTenantId,
                                                        false,
                                                        json.AadClientId,
                                                        json.AadSecret
                                                        );

                        CredentialList.MediaServicesAccounts.Add(entry);
                        AddItemToListviewAccounts(entry);

                        SaveCredentialsToSettings();
                    }
                    else
                    {
                        return;
                    }

                }
                else if (addaccount1.SelectedMode == AddAccountMode.ManualEntry)
                {
                    Telemetry.TrackEvent("AMSLogin buttonPickupAccount_Click ManualEntry");

                    AddAMSAccount2Manual form = new();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var res = new ResourceIdentifier(form.textBoxAMSResourceId.Text);

                        string accountnamecc = form.textBoxAMSResourceId.Text.Split('/').Last();

                        CredentialsEntryV4 entry = new(
                            res.Name,
                            res.SubscriptionId,
                            res.ResourceGroupName,
                            addaccount1.GetEnvironment(),
                            true,
                            form.radioButtonAADServicePrincipal.Checked,
                            form.textBoxAADtenantId.Text,
                            true
                            );

                        CredentialList.MediaServicesAccounts.Add(entry);
                        AddItemToListviewAccounts(entry);

                        SaveCredentialsToSettings();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            CredentialList.MediaServicesAccounts[listViewAccounts.SelectedIndices[0]].Description = textBoxDescription.Text;
            SaveCredentialsToSettings();
        }

        private void linkLabelAMSOfflineDoc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Application.StartupPath, @"HelpFiles\", @"AMSv3doc.pdf"),
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private static void ScaleListViewColumns(ListView listview)
        {
            listview.Columns[0].Width = listview.Width - 4 - SystemInformation.VerticalScrollBarWidth;
        }

        private void AmsLogin_DpiChangedAfterParent(object sender, EventArgs e)
        {
            ScaleListViewColumns(listViewAccounts);
        }

        private void AmsLogin_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // DpiUtils.UpdatedSizeFontAfterDPIChange(labelenteramsacct, e);
        }

        private void buttonConnectRavnur_Click(object sender, EventArgs e)
        {
            string example = @"{
  ""AZURE_SUBSCRIPTION_ID"": ""00000000-0000-0000-0000-000000000000"",
  ""AZURE_RESOURCE_GROUP"": ""rmsResourceGroup"",
  ""RAVNUR_MEDIA_SERVICES_ACCOUNT_NAME"": ""rmsAccount"",
  ""RAVNUR_API_ENDPOINT"": ""https://rms.myaccount.ravnur.net/"",
  ""RAVNUR_API_KEY"": ""rmsApiKey""
}";
            EditorXMLJSON form = new("Enter the configuration of your Ravnur instance", example, true, ShowSampleMode.None, true);

            if (form.ShowDialog() == DialogResult.OK)
            {
                RavnurConfigurationOptions json;
                try
                {
                    json = (RavnurConfigurationOptions)JsonConvert.DeserializeObject(form.TextData, typeof(RavnurConfigurationOptions));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error reading the json", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Azure environment settings
                ActiveDirectoryServiceSettings aadSettings = new()
                {
                    AuthenticationEndpoint = new Uri("https://login.microsoftonline.com"),
                    TokenAudience = new Uri("https://management.core.windows.net/"),
                    ValidateAuthority = true
                };

                AzureEnvironment env = new(AzureEnvType.Custom)
                {
                    AADSettings = aadSettings,
                    ArmEndpoint = new Uri("https://management.azure.com/")
                };

                CredentialsEntryV4 entry = new(
                                                accountName: json.AccountName,
                                                subscriptionId: json.SubscriptionId,
                                                resourceGroupName: json.ResourceGroup,
                                                environment: env,
                                                promptUser: true,
                                                useSPAuth: false,
                                                tenantId: null)
                {
                    RavnurApiEndpoint = json.ApiEndpoint,
                    RavnurClearApiKey = json.ApiKey,
                };

                CredentialList.MediaServicesAccounts.Add(entry);
                AddItemToListviewAccounts(entry);

                SaveCredentialsToSettings();
            }
            else
            {
                return;
            }
        }
    }
}