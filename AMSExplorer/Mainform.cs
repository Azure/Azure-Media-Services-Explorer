//----------------------------------------------------------------------------------------------
//    Copyright 2019 Microsoft Corporation
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

// Azure Management dependencies
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace AMSExplorer
{
    public partial class Mainform : Form
    {
        // XML Configuration files path.
        public static string _configurationXMLFiles;
        private static string _HelpFiles;
        public static bool havestoragecredentials = true;

        // Field for service context.
        public static string Salt;
        private string _backuprootfolderupload = string.Empty;
        private string _backuprootfolderdownload = string.Empty;
        private readonly StringBuilder sbuilder = new StringBuilder(); // used for locator copy to clipboard
        private AssetStreamingLocator PlayBackLocator = null;

        //Watch folder vars
        private readonly Dictionary<string, DateTime> seen = new Dictionary<string, DateTime>();

        private readonly System.Timers.Timer TimerAutoRefresh;
        private bool DisplaySplashDuringLoading;

        private enumDisplayProgram backupCheckboxAnyLiveEvent = enumDisplayProgram.Selected;
        private bool CheckboxAnyLiveEventChangedByCode = false;

        private readonly bool largeAccount = false; // if nb assets > trigger
        private readonly int triggerForLargeAccountNbAssets = 10000; // account with more than 10000 assets is considered as large account. Some queries will be disabled
        private const int maxNbAssets = 1000000;
        private const int maxNbJobs = 50000;
        private readonly bool enableTelemetry = true;

#pragma warning disable CS0414 // The field 'Mainform.OneGB' is assigned but its value is never used
        private static readonly long OneGB = 1000L * 1000L * 1000L;
#pragma warning restore CS0414 // The field 'Mainform.OneGB' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'Mainform.S1AssetSizeLimit' is assigned but its value is never used
        private static readonly int S1AssetSizeLimit = 26; // GBytes
#pragma warning restore CS0414 // The field 'Mainform.S1AssetSizeLimit' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'Mainform.S2AssetSizeLimit' is assigned but its value is never used
        private static readonly int S2AssetSizeLimit = 60; // GBytes
#pragma warning restore CS0414 // The field 'Mainform.S2AssetSizeLimit' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'Mainform.S3AssetSizeLimit' is assigned but its value is never used
        private static readonly int S3AssetSizeLimit = 260; // GBytes
#pragma warning restore CS0414 // The field 'Mainform.S3AssetSizeLimit' is assigned but its value is never used
        public string _accountname;
        private static AMSClientV3 _amsClientV3;
        private readonly MediaRU _mediaRUContext;
        private const string resetcredentials = "/resetcredentials";

        public bool MediaRUFeatureOn = true;

        public Mainform(string[] args)
        {
            InitializeComponent();

            // for player control embedded in UI
            Program.SetWebBrowserFeatures();

            Icon = Bitmaps.Azure_Explorer_ico;

            // USER SETTINSG CHECKS & UPDATES
            // upgrade settings from previous version
            if (Properties.Settings.Default.CallUpgrade)
            {
                // let's migrate data 
                Properties.Settings.Default.Upgrade();

                // we remove temporary the upgrade as schema has changed
                Properties.Settings.Default.CallUpgrade = false;
            }

            if (args.Length > 0 && args.Any(a => a.ToLower() == resetcredentials))
            {
                // let's clean the list
                Properties.Settings.Default.LoginListRPv3JSON = string.Empty;
            }

            // if installation file has been downloaded, let's delete it now
            if (!string.IsNullOrEmpty(Properties.Settings.Default.DeleteInstallationFile))
            {
                try
                {
                    File.Delete(Properties.Settings.Default.DeleteInstallationFile);
                    Properties.Settings.Default.DeleteInstallationFile = string.Empty;
                    Properties.Settings.Default.Save();
                }
                catch
                {

                }
            }
            _configurationXMLFiles = Application.StartupPath + Constants.PathConfigFiles;

            // AME Premium Workflow preset folder
            if ((Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder)))
            {
                Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathPremiumWorkflowFiles;
            }

            // Default Slate Image
            if ((Properties.Settings.Default.DefaultSlateCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.DefaultSlateCurrentFolder)))
            {
                Properties.Settings.Default.DefaultSlateCurrentFolder = Application.StartupPath + Constants.PathDefaultSlateJPG;
            }

            Program.SaveAndProtectUserConfig(); // to save settings 

            _HelpFiles = Application.StartupPath + Constants.PathHelpFiles;

            AmsLogin formLogin = new AmsLogin();

            if (formLogin.ShowDialog() == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }

            // Get the service context.
            _amsClientV3 = formLogin.AmsClient;

            _accountname = _amsClientV3.credentialsEntry.AccountName;
            DisplaySplashDuringLoading = true;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (Splash splashForm = new Splash(_accountname))
                {
                    splashForm.Show();
                    while (DisplaySplashDuringLoading)
                    {
                        Application.DoEvents();
                    }

                    splashForm.Close();
                }
            });

            // mainform title
            toolStripStatusLabelConnection.Text = string.Format("Version {0} for Media Services v3", Assembly.GetExecutingAssembly().GetName().Version) + " - Connected to " + _accountname;

            // notification title
            notifyIcon1.Text = string.Format(notifyIcon1.Text, _accountname);

            // name of the ams acount in the title of the form - useful when several instances to navigate with icons
            Text = string.Format(Text, _accountname);

            // Timer Auto Refresh
            TimerAutoRefresh = new System.Timers.Timer(Properties.Settings.Default.AutoRefreshTime * 1000);
            TimerAutoRefresh.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Let's check if there is one streaming unit running
            try
            {
                Microsoft.Rest.Azure.IPage<StreamingEndpoint> se = _amsClientV3.AMSclient.StreamingEndpoints.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);

                if (se.AsEnumerable().Where(o => o.ResourceState == StreamingEndpointResourceState.Running).ToList().Count == 0)
                {
                    TextBoxLogWriteLine("There is no streaming endpoint running in this account.", true); // Warning
                }

                double nbLiveEvents = _amsClientV3.AMSclient.LiveEvents.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).Count();
                double nbse = se.Count();
                if (nbse > 0 && nbLiveEvents > 0 && (nbLiveEvents / nbse) > 5)
                {
                    TextBoxLogWriteLine("There are {0} live events and {1} streaming endpoint(s). Recommandation is to provision at least 1 streaming endpoint per group of 5 live events.", nbLiveEvents, nbse, true); // Warning
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Program.GetErrorMessage(ex) + "\n\nAMS Explorer will exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }


            // nb assets limits
            int nbassets = _amsClientV3.AMSclient.Assets.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).Count();
            largeAccount = nbassets > triggerForLargeAccountNbAssets;
            if (largeAccount)
            {
                TextBoxLogWriteLine("This account contains a lot of assets. Some queries are disabled."); // Warning
            }
            if (nbassets > (0.75 * maxNbAssets))
            {
                TextBoxLogWriteLine("This account contains {0} assets. Warning, the limit is {1}.", nbassets, maxNbAssets, true); // Warning
            }




            // let's check the encoding reserved unit and type
            try
            {
                // Management of media reserved units
                _mediaRUContext = new MediaRU(_amsClientV3);
                InfoMediaRU result = _mediaRUContext.GetInfoMediaRU(_amsClientV3).GetAwaiter().GetResult();

                if ((result.CurrentReservedUnits == 0) && (result.ReservedUnitType != 0))
                {
                    TextBoxLogWriteLine("There is no Media Reserved Unit (encoding will use a shared pool) but unit type is not set to S1.", true); // Warning
                }

                comboBoxEncodingRU.Items.Add(new Item("S1", "0"));
                comboBoxEncodingRU.Items.Add(new Item("S2", "1"));
                comboBoxEncodingRU.Items.Add(new Item("S3", "2"));
                comboBoxEncodingRU.SelectedIndex = result.ReservedUnitType;
            }
            catch // can occur on test account
            {
                MediaRUFeatureOn = false;
                comboBoxEncodingRU.Enabled = trackBarEncodingRU.Enabled = buttonUpdateEncodingRU.Enabled = false;
                TextBoxLogWriteLine("There is an error when accessing to the Media Reserved Units v2 API. Some controls are disabled in the transforms/jobs tab.", true); // Warning
            }
        }



        // Test
        private async Task<object> ListEventsRest()
        {
            // tenants browsing
            // GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Media/mediaservices/{accountName}/liveEvents?api-version=2018-07-01
            string URL = _amsClientV3.environment.ArmEndpoint + string.Format("subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/liveEvents?api-version=2018-07-01",
                _amsClientV3.credentialsEntry.AzureSubscriptionId,
                _amsClientV3.credentialsEntry.ResourceGroup,
                  _amsClientV3.credentialsEntry.AccountName
                );

            string token = _amsClientV3.accessTokenForRestV2 != null ? _amsClientV3.accessTokenForRestV2.AccessToken :
                TokenCache.DefaultShared.ReadItems()
        .Where(t => t.ClientId == _amsClientV3.credentialsEntry.ADSPClientId)
        .OrderByDescending(t => t.ExpiresOn)
        .First().AccessToken;


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = await client.GetAsync(URL);

            object dynObject = null;
            if (response.IsSuccessStatusCode)
            {
                string str = await response.Content.ReadAsStringAsync();
                object list = JsonConvert.DeserializeObject(str);
                dynObject = JsonConvert.DeserializeObject(str);

            }
            return dynObject;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            DoRefresh();
        }

        public void Notify(string title, string text, bool Error = false)
        {
            if (Properties.Settings.Default.HideTaskbarNotifications == false)
            {
                notifyIcon1.ShowBalloonTip(3000, title, text, Error ? ToolTipIcon.Error : ToolTipIcon.Info);
            }
        }

        private static async Task<Asset> GetAssetAsync(string assetName)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();
            return await _amsClientV3.AMSclient.Assets.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, assetName);
        }

        private static async Task<Job> GetJobAsync(string transformName, string jobName)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();
            return await _amsClientV3.AMSclient.Jobs.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transformName, jobName);
        }

        private static async Task<Transform> GetTransformAsync(string transformName)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();
            return await _amsClientV3.AMSclient.Transforms.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transformName);
        }

        private static async Task<LiveEvent> GetLiveEventAsync(string liveEventName)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();
            return await _amsClientV3.AMSclient.LiveEvents.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, liveEventName);
        }

        private static async Task<LiveOutput> GetLiveOutputAsync(string liveEventName, string liveOutputName)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();
            return await _amsClientV3.AMSclient.LiveOutputs.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, liveEventName, liveOutputName);
        }

        private static async Task<StreamingEndpoint> GetStreamingEndpointAsync(string seName)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();
            return await _amsClientV3.AMSclient.StreamingEndpoints.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, seName);
        }


        public void DeleteLocatorsForAsset(Asset asset)
        {
            if (asset != null)
            {
                _amsClientV3.RefreshTokenIfNeeded();
                IList<AssetStreamingLocator> locators = _amsClientV3.AMSclient.Assets.ListStreamingLocators(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, asset.Name).StreamingLocators;

                foreach (AssetStreamingLocator locator in locators)
                {
                    TextBoxLogWriteLine("Deleting locator {0} for asset {1}", locator.Name, asset.Name);
                    try
                    {
                        _amsClientV3.AMSclient.StreamingLocators.Delete(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, locator.Name);
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void TextBoxLogWriteLine(string message, object o1, bool Error = false)
        {
            TextBoxLogWriteLine(string.Format(message, o1), Error);
        }

        public void TextBoxLogWriteLine(string message, object o1, object o2, bool Error = false)
        {
            TextBoxLogWriteLine(string.Format(message, o1, o2), Error);
        }

        public void TextBoxLogWriteLine(string message, object o1, object o2, object o3, bool Error = false)
        {
            TextBoxLogWriteLine(string.Format(message, o1, o2, o3), Error);
        }

        public void TextBoxLogWriteLine(string message, object o1, object o2, object o3, object o4, bool Error = false)
        {
            TextBoxLogWriteLine(string.Format(message, o1, o2, o3, o4), Error);
        }

        public void TextBoxLogWriteLine(Exception e)
        {
            TextBoxLogWriteLine(e.Message, true);
            if (e.InnerException != null)
            {
                TextBoxLogWriteLine(Program.GetErrorMessage(e), true);
            }
            if (e.GetType() == typeof(ApiErrorException))
            {
                ApiErrorException eApi = (ApiErrorException)e;
                dynamic error = JsonConvert.DeserializeObject(eApi.Response.Content);
                TextBoxLogWriteLine((string)error?.error?.message, true);
            }
        }

        public void TextBoxLogWriteLine()
        {
            TextBoxLogWriteLine(string.Empty);
        }

        public void TextBoxLogWriteLine(string text, bool Error = false)
        {
            bool stringEmpty = string.IsNullOrEmpty(text);
            text += Environment.NewLine;
            string date = string.Format("[{0}] ", string.Format("{0:G}", DateTime.Now));

            if (richTextBoxLog.InvokeRequired)
            {
                richTextBoxLog.BeginInvoke(new Action(() =>
                {
                    if (!stringEmpty)
                    {
                        richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                        richTextBoxLog.SelectionLength = 0;

                        richTextBoxLog.SelectionColor = Color.Gray;
                        richTextBoxLog.AppendText(date);

                        richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                        richTextBoxLog.SelectionLength = 0;

                        richTextBoxLog.SelectionColor = Error ? Color.Red : Color.Black;
                    }
                    richTextBoxLog.AppendText(text);
                    if (!stringEmpty)
                    {
                        richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor;
                    }
                }));
            }
            else
            {
                if (!stringEmpty)
                {
                    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                    richTextBoxLog.SelectionLength = 0;

                    richTextBoxLog.SelectionColor = Color.Gray;
                    richTextBoxLog.AppendText(date);

                    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                    richTextBoxLog.SelectionLength = 0;

                    richTextBoxLog.SelectionColor = Error ? Color.Red : Color.Black;
                }
                richTextBoxLog.AppendText(text);
                if (!stringEmpty)
                {
                    richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor;
                }
            }
        }


        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }

        private void buttonRefreshTab_Click(object sender, EventArgs e)
        {
            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageAssets":
                    DoRefreshGridAssetV(false);
                    break;
                case "tabPageFilters":
                    DoRefreshGridFiltersV(false);
                    break;
                case "tabPageTransfers":
                    break;
                case "tabPageJobs":
                    DoRefreshGridTransformV(false);
                    DoRefreshGridJobV(false);
                    break;
                case "tabPageLive":
                    DoRefreshGridLiveEventV(false);
                    DoRefreshGridLiveOutputV(false);
                    break;
                case "tabPageOrigins":
                    DoRefreshGridStreamingEndpointV(false);
                    break;
                case "tabPageStorage":
                    DoRefreshGridStorageV(false);
                    break;
            }
        }

        private void DoRefresh()
        {
            DoRefreshGridJobV(false);
            DoRefreshGridTransformV(false);
            DoRefreshGridAssetV(false);
            DoRefreshGridLiveEventV(false);
            DoRefreshGridStreamingEndpointV(false);
            DoRefreshGridStorageV(false);
            DoRefreshGridFiltersV(false);
        }

        public void DoRefreshGridAssetV(bool firstime)
        {
            if (firstime)
            {
                SetTextBoxAssetsPageNumber(1);

                dataGridViewAssetsV.Init(_amsClientV3);
                Debug.WriteLine("DoRefreshGridAssetforsttime");
            }

            Debug.WriteLine("DoRefreshGridAssetNotforsttime");

            int page = GetTextBoxAssetsPageNumber();
            Task.Run(async () =>
            {
                await dataGridViewAssetsV.RefreshAssetsAsync(page);
            });

            //tabPageAssets.Invoke(new Action(() => tabPageAssets.Text = string.Format(AMSExplorer.Properties.Resources.TabAssets + " ({0}/{1})", dataGridViewAssetsV.DisplayedCount, 10 /*_context.Assets.Count()*/)));
        }

        public void DoPurgeAssetInfoFromCache(Asset asset)
        {
            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.PurgeCacheAsset(asset)));
        }


        private void DoRefreshGridTransformV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewTransformsV.Init(_amsClientV3);
            }

            Debug.WriteLine("DoRefreshGridTransformVNotforsttime");

            Task.Run(async () =>
            {
                await dataGridViewTransformsV.RefreshTransformsAsync();
            });
        }


        private void DoRefreshGridJobV(bool firstime)
        {
            if (!dataGridViewJobsV._initialized)
            {
                if (firstime)
                {
                    SetTextBoxJobsPageNumber(1);
                    dataGridViewJobsV.Init(_amsClientV3);
                }
            }

            Debug.WriteLine("DoRefreshGridJobVNotforsttime");

            int page = GetTextBoxJobsPageNumber();
            Task.Run(async () =>
            {
                await dataGridViewJobsV.RefreshjobsAsync(page);
                if (MediaRUFeatureOn)
                {
                    InfoMediaRU mediaReserverdUnitsInfo = null;
                    try
                    {
                        mediaReserverdUnitsInfo = await _mediaRUContext.GetInfoMediaRU(_amsClientV3);
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine(ex);
                    }
                    if (mediaReserverdUnitsInfo != null)
                    {
                        BeginInvoke(new Action(() => trackBarEncodingRU.Maximum = mediaReserverdUnitsInfo.MaxReservableUnits));
                        BeginInvoke(new Action(() => trackBarEncodingRU.Value = mediaReserverdUnitsInfo.CurrentReservedUnits));
                        BeginInvoke(new Action(() => UpdateLabelProcessorUnits()));
                    }
                }
            });
        }


        private void fromASingleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuUploadFromSingleFiles_Step1();
        }

        private void DoMenuUploadFromSingleFiles_Step1()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoMenuUploadFromSingleFileS_Step2(openFileDialog.FileNames);
            }
        }

        private void DoMenuUploadFromSingleFileS_Step2(string[] FileNames)
        {
            List<string> listpb = AssetInfo.ReturnFilenamesWithProblem(FileNames.ToList());
            if (listpb.Count > 0)
            {
                MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UploadOptions form = new UploadOptions(_amsClientV3, FileNames.Count() > 1);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            if (FileNames.Count() > 1 && form.SingleAsset) // all files in one asset
            {
                try
                {
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of {0} files into a single asset", FileNames.Count()), TransferType.UploadFromFile, true);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(FileNames.ToList(), response.Id, response.token, storageaccount: form.StorageSelected, blocksize: form.BlockSize), response.token);
                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
            else // one asset per file
            {
                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                int i = 0;
                // Each file goes in a individual asset
                foreach (string file in FileNames)
                {
                    try
                    {
                        i++;
                        TransferEntryResponse response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, true);
                        // Start a worker thread that does uploading.
                        Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(new List<string>() { file }, response.Id, response.token, form.StorageSelected, blocksize: form.BlockSize), response.token);

                        if (i == 10) // let's use a batch of 10 threads at the same time
                        {
                            do
                            {
                                Task.Delay(1000).Wait();
                            }
                            while (ReturnTransfer(response.Id).State == TransferState.Queued);
                            i = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                        TextBoxLogWriteLine(ex);
                    }
                }
            }
        }


        private async Task ProcessUploadFileAndMoreV3(List<string> filenames, Guid guidTransfer, CancellationToken token, string storageaccount = null, string destAssetName = null, int blocksize = 4)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(guidTransfer);
            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }
            await _amsClientV3.RefreshTokenIfNeededAsync();
            IList<StorageAccount> storAccounts = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).StorageAccounts;

            if (storageaccount == null)
            {
                storageaccount = AMSClientV3.GetStorageName(storAccounts.Where(s => s.Type == StorageAccountType.Primary).First().Id);
                // no storage account or null, then let's take the default one
            }

            bool Error = false;
            Asset asset = null;

            List<string> listpb = AssetInfo.ReturnFilenamesWithProblem(filenames);
            if (listpb.Count > 0)
            {
                TextBoxLogWriteLine(AssetInfo.FileNameProblemMessage(listpb), true);
                DoGridTransferDeclareError(guidTransfer);
                Error = true;
            }
            else
            {
                TextBoxLogWriteLine("Starting upload of file '{0}'", filenames[0]);
                try
                {
                    if (destAssetName == null) // let create a new asset
                    {
                        string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);
                        destAssetName = "uploaded-" + uniqueness;
                        asset = await _amsClientV3.AMSclient.Assets.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, new Asset() { StorageAccountName = storageaccount, Description = Path.GetFileName(filenames[0]) }, token);
                    }
                    else // let's reusing existing asset
                    {
                        asset = await _amsClientV3.AMSclient.Assets.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, token);
                    }

                    ListContainerSasInput input = new ListContainerSasInput()
                    {
                        Permissions = AssetContainerPermission.ReadWrite,
                        ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                    };

                    AssetContainerSas response = await _amsClientV3.AMSclient.Assets.ListContainerSasAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime);

                    string uploadSasUrl = response.AssetContainerSasUrls.First();

                    Uri sasUri = new Uri(uploadSasUrl);
                    CloudBlobContainer container = new CloudBlobContainer(sasUri);

                    foreach (string file in filenames)
                    {
                        if (token.IsCancellationRequested)
                        {
                            return;
                        }

                        string filename = Path.GetFileName(file);

                        CloudBlockBlob blob = container.GetBlockBlobReference(filename);
                        if (filename.ToLower().EndsWith(".mp4"))
                        {
                            blob.Properties.ContentType = "video/mp4";
                        }

                        blob.StreamWriteSizeInBytes = blocksize * 1024 * 1024; // blocksize

                        await blob.UploadFromFileAsync(file, token);

                        MyUploadFileProgressChanged(guidTransfer, filename.IndexOf(file), filenames.Count);
                    }
                }
                catch (Exception e)
                {
                    Error = true;
                    DoGridTransferDeclareError(guidTransfer, e);
                    TextBoxLogWriteLine("Error when uploading '{0}'.", string.Join(", ", filenames), true);
                    TextBoxLogWriteLine(e);
                }
            }


            if (!Error && !token.IsCancellationRequested)
            {
                DoGridTransferDeclareCompleted(guidTransfer, destAssetName);
            }
            else if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
            }
            DoRefreshGridAssetV(false);
        }


        private async Task ProcessHttpSourceV3(Uri source, Guid guidTransfer, CancellationToken token, string storageaccount = null, string destAssetName = null, string destAssetDescription = null)
        {

            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }
            await _amsClientV3.RefreshTokenIfNeededAsync();
            IList<StorageAccount> storAccounts = (await _amsClientV3.AMSclient.Mediaservices.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName)).StorageAccounts;

            if (storageaccount == null)
            {
                storageaccount = AMSClientV3.GetStorageName(storAccounts.Where(s => s.Type == StorageAccountType.Primary).First().Id);
                // no storage account or null, then let's take the default one
            }

            bool Error = false;
            Asset asset = null;

            try
            {

                if (destAssetName == null)
                {
                    destAssetName = "uploaded-" + Guid.NewGuid().ToString().Substring(0, 13);
                }

                asset = await _amsClientV3.AMSclient.Assets.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, new Asset() { StorageAccountName = storageaccount, Description = destAssetDescription }, token);


                ListContainerSasInput input = new ListContainerSasInput()
                {
                    Permissions = AssetContainerPermission.ReadWrite,
                    ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                };

                AssetContainerSas response = Task.Run(async () => await _amsClientV3.AMSclient.Assets.ListContainerSasAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime)).Result;

                string uploadSasUrl = response.AssetContainerSasUrls.First();

                Uri sasUri = new Uri(uploadSasUrl);
                CloudBlobContainer container = new CloudBlobContainer(sasUri);

                if (token.IsCancellationRequested)
                {
                    return;
                }

                string filename = Path.GetFileName(source.LocalPath);

                CloudBlockBlob blob = container.GetBlockBlobReference(filename);
                if (filename.ToLower().EndsWith(".mp4"))
                {
                    blob.Properties.ContentType = "video/mp4";
                }

                string stringOperation = await blob.StartCopyAsync(source, token);

                bool Cancelled = false;

                bool continueLoop = true;

                while (continueLoop)// && !token.IsCancellationRequested)
                {
                    if (token.IsCancellationRequested && !Cancelled)
                    {
                        await blob.AbortCopyAsync(stringOperation);
                        Cancelled = true;
                    }

                    blob.FetchAttributes();
                    CopyState copyStatus = blob.CopyState;
                    if (copyStatus != null)
                    {
                        double percentComplete = 100 * (long)copyStatus.BytesCopied / (long)copyStatus.TotalBytes;

                        DoGridTransferUpdateProgress(percentComplete, guidTransfer);

                        if (copyStatus.Status != CopyStatus.Pending)
                        {
                            continueLoop = false;
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }


                if (blob.CopyState.Status == CopyStatus.Failed)
                {
                    DoGridTransferDeclareError(guidTransfer, blob.CopyState.StatusDescription);
                    Error = true;
                }

                if (blob.CopyState.Status == CopyStatus.Aborted)
                {
                    DoGridTransferDeclareCancelled(guidTransfer);
                    Error = true;
                }

                //   MyUploadFileProgressChanged(guidTransfer, filename.IndexOf(file), filenames.Count);

            }
            catch (Exception e)
            {
                Error = true;
                DoGridTransferDeclareError(guidTransfer, e);
                TextBoxLogWriteLine("Error when importing '{0}'.", source.ToString());
                TextBoxLogWriteLine(e);
            }



            if (!Error && !token.IsCancellationRequested)
            {
                DoGridTransferDeclareCompleted(guidTransfer, destAssetName);
            }
            else if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
            }
            DoRefreshGridAssetV(false);
        }


        private async Task ProcessHttpSASV3(Uri ObjectUrl, Guid guidTransfer, CancellationToken token, string storageaccount = null, string destAssetName = null, string destAssetDescription = null)
        {

            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }
            await _amsClientV3.RefreshTokenIfNeededAsync();
            IList<StorageAccount> storAccounts = (await _amsClientV3.AMSclient.Mediaservices.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName)).StorageAccounts;

            if (storageaccount == null)
            {
                storageaccount = AMSClientV3.GetStorageName(storAccounts.Where(s => s.Type == StorageAccountType.Primary).First().Id);
                // no storage account or null, then let's take the default one
            }

            bool Error = false;
            bool Canceled = false;

            string ErrorMessage = string.Empty;
            Asset asset = null;
            CloudBlobContainer Container = new CloudBlobContainer(ObjectUrl);
            CloudBlockBlob blockBlob;

            try
            {

                if (destAssetName == null)
                {
                    destAssetName = "uploaded-" + Guid.NewGuid().ToString().Substring(0, 13);
                }

                asset = await _amsClientV3.AMSclient.Assets.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, new Asset() { StorageAccountName = storageaccount, Description = destAssetDescription }, token);


                ListContainerSasInput input = new ListContainerSasInput()
                {
                    Permissions = AssetContainerPermission.ReadWrite,
                    ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                };

                AssetContainerSas response = Task.Run(async () => await _amsClientV3.AMSclient.Assets.ListContainerSasAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime)).Result;

                string uploadSasUrl = response.AssetContainerSasUrls.First();

                Uri sasUri = new Uri(uploadSasUrl);
                CloudBlobContainer destinationContainer = new CloudBlobContainer(sasUri);

                if (token.IsCancellationRequested)
                {
                    return;
                }

                long Length = 0;
                foreach (IListBlobItem blob in Container.ListBlobs())
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blobblock = (CloudBlockBlob)blob;
                        Length += blobblock.Properties.Length;
                    }
                }

                IEnumerable<IListBlobItem> blobsblock = Container.ListBlobs().Where(b => b.GetType() == typeof(CloudBlockBlob));
                int nbtotalblobblock = blobsblock.Count();
                int nbblob = 0;
                long BytesCopied = 0;
                foreach (IListBlobItem blob in blobsblock)
                {
                    nbblob++;
                    string fileName = Path.GetFileName(blob.Uri.ToString());

                    blockBlob = destinationContainer.GetBlockBlobReference(fileName);
                    TextBoxLogWriteLine("Copying file '{0}'....", fileName);

                    UriBuilder urib = new UriBuilder(ObjectUrl);
                    urib.Path = urib.Path + "/" + Path.GetFileName(blob.Uri.ToString());

                    string stringOperation = await blockBlob.StartCopyAsync(urib.Uri, token);
                    bool Cancelled = false;

                    DateTime startTime = DateTime.UtcNow;

                    bool continueLoop = true;

                    while (continueLoop)
                    {
                        if (token.IsCancellationRequested && !Cancelled)
                        {
                            await blockBlob.AbortCopyAsync(stringOperation);
                            Cancelled = true;
                        }

                        blockBlob.FetchAttributes();
                        CopyState copyStatus = blockBlob.CopyState;
                        if (copyStatus != null)
                        {
                            double percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(nbtotalblobblock)) * 100d * (long)(BytesCopied + copyStatus.BytesCopied) / Length;

                            DoGridTransferUpdateProgress(percentComplete, guidTransfer);

                            if (copyStatus.Status != CopyStatus.Pending)
                            {
                                continueLoop = false;
                                if (copyStatus.Status == CopyStatus.Failed)
                                {
                                    Error = true;
                                    ErrorMessage = copyStatus.StatusDescription;
                                }
                                if (copyStatus.Status == CopyStatus.Aborted)
                                {
                                    Canceled = true;
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(1000);
                    }

                    blockBlob.FetchAttributes();


                    DateTime endTime = DateTime.UtcNow;
                    TimeSpan diffTime = endTime - startTime;

                    BytesCopied += blockBlob.Properties.Length;
                }


                List<CloudBlobDirectory> ListDirectories = new List<CloudBlobDirectory>();
                List<Task> mylistresults = new List<Task>();

                IEnumerable<IListBlobItem> blobsdir = Container.ListBlobs().Where(b => b.GetType() == typeof(CloudBlobDirectory));
                int nbtotalblobdir = blobsdir.Count();
                int nbblobdir = 0;
                foreach (IListBlobItem blob in blobsdir)
                {
                    nbblobdir++;
                    string fileName = blob.Uri.Segments[2];

                    CloudBlobDirectory blobdir = (CloudBlobDirectory)blob;
                    ListDirectories.Add(blobdir);
                    TextBoxLogWriteLine("Fragblobs detected (live archive) '{0}'.", blobdir.Prefix);

                    List<IListBlobItem> srcBlobList = blobdir.ListBlobs(
                           useFlatBlobListing: true,
                           blobListingDetails: BlobListingDetails.None).ToList();

                    IEnumerable<IListBlobItem> subblocks = srcBlobList.Where(s => s.GetType() == typeof(CloudBlockBlob));
                    long size = 0;
                    if (subblocks.Count() > 0)
                    {
                        size = subblocks.Sum(s => ((CloudBlockBlob)s).Properties.Length);
                    }
                }


                // let's launch the copy of fragblobs
                double ind = 0;
                foreach (CloudBlobDirectory dir in ListDirectories)
                {
                    TextBoxLogWriteLine("Copying fragblobs directory '{0}'....", dir.Prefix);

                    mylistresults.AddRange(AssetInfo.CopyBlobDirectory(dir, destinationContainer, ObjectUrl.Query, token));

                    if (mylistresults.Count > 0)
                    {
                        while (!mylistresults.All(r => r.IsCompleted))
                        {
                            Task.Delay(TimeSpan.FromSeconds(3d)).Wait();
                            double percentComplete = 100d * (ind + Convert.ToDouble(mylistresults.Where(c => c.IsCompleted).Count()) / Convert.ToDouble(mylistresults.Count)) / Convert.ToDouble(ListDirectories.Count);
                            DoGridTransferUpdateProgressText(string.Format("fragblobs directory '{0}' ({1}/{2})", dir.Prefix, mylistresults.Where(r => r.IsCompleted).Count(), mylistresults.Count), (int)percentComplete, guidTransfer);
                        }
                    }
                    ind++;
                    mylistresults.Clear();
                }

                if (!Error && !Canceled)
                {

                    DoGridTransferDeclareCompleted(guidTransfer, asset.Id);
                    DoRefreshGridAssetV(false);
                }
                else if (Canceled)
                {

                    DoGridTransferDeclareCancelled(guidTransfer);
                    DoRefreshGridAssetV(false);
                }
                else // Error!
                {
                    DoGridTransferDeclareError(guidTransfer, "Error during import. " + ErrorMessage);

                }
            }

            catch (Exception ex)
            {
                Error = true;
                TextBoxLogWriteLine("Error during file import.", true);
                TextBoxLogWriteLine(ex);
                DoGridTransferDeclareError(guidTransfer, ex);
            }

            /*
            if (!Error && !token.IsCancellationRequested)
            {
                DoGridTransferDeclareCompleted(guidTransfer, destAssetName);
            }
            else if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
            }
            */
            DoRefreshGridAssetV(false);
        }


        private void MyUploadFileProgressChanged(Guid guidTransfer, int indexfile, int nbfiles)
        {
            double progress = 100 * (double)indexfile / nbfiles;
            DoGridTransferUpdateProgress(progress, guidTransfer);
        }

        private void DoMenuUploadFileToAsset_Step1()
        {
            List<Asset> assets = ReturnSelectedAssetsV3();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoMenuUploadFileToAsset_Step2(openFileDialog.FileNames, assets);
            }
        }

        private void DoMenuUploadFileToAsset_Step2(string[] FileNames, List<Asset> assets)
        {
            List<string> listpb = AssetInfo.ReturnFilenamesWithProblem(FileNames.ToList());
            if (listpb.Count > 0)
            {
                MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
            int i = 0;
            foreach (Asset asset in assets)
            {
                try
                {
                    i++;
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of {0} file{1} to asset '{2}'", FileNames.Count(), FileNames.Count() > 1 ? "s" : string.Empty, asset.Name), TransferType.UploadFromFile, true);
                    // Start a worker thread that does uploading.
                    //Task.Factory.StartNew(() => ProcessUploadFilesToAsset(FileNames, asset, response.Id, response.token), response.token);
                    Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(FileNames.ToList(), response.Id, response.token, null, asset.Name), response.token);

                    if (i == 10) // let's use a batch of 10 threads at the same time
                    {
                        do
                        {
                            Task.Delay(1000).Wait();
                        }
                        while (ReturnTransfer(response.Id).State == TransferState.Queued);
                        i = 0;
                    }
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
        }



        public async Task DownloadOutputAssetAsync(AMSClientV3 client, string assetName, string outputFolderName, TransferEntryResponse response, DownloadToFolderOption downloadOption, bool openFileExplorer, List<string> onlySomeBlobsName = null)
        {
            // If download is in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(response.Id);
            if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
                return;
            }

            const int ListBlobsSegmentMaxResult = 5;

            if (!Directory.Exists(outputFolderName))
            {
                Directory.CreateDirectory(outputFolderName);
            }
            await _amsClientV3.RefreshTokenIfNeededAsync();
            AssetContainerSas assetContainerSas = await client.AMSclient.Assets.ListContainerSasAsync(
        client.credentialsEntry.ResourceGroup,
        client.credentialsEntry.AccountName,
        assetName,
        permissions: AssetContainerPermission.Read,
        expiryTime: DateTime.UtcNow.AddHours(5).ToUniversalTime());

            Uri containerSasUrl = new Uri(assetContainerSas.AssetContainerSasUrls.FirstOrDefault());
            CloudBlobContainer container = new CloudBlobContainer(containerSasUrl);

            //string directory = Path.Combine(outputFolderName, assetName);
            //Directory.CreateDirectory(directory);

            if (downloadOption == DownloadToFolderOption.SubfolderAssetName)
            {
                outputFolderName += "\\" + assetName;
                Directory.CreateDirectory(outputFolderName);
            }

            TextBoxLogWriteLine($"Downloading blobs to '{outputFolderName}'...");

            BlobContinuationToken continuationToken = null;
            IList<Task> downloadTasks = new List<Task>();

            Task<Task> myTask = Task.Factory.StartNew(async () =>
            {
                try
                {
                    do
                    {
                        BlobResultSegment segment = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, ListBlobsSegmentMaxResult, continuationToken, null, null);

                        foreach (IListBlobItem blobItem in segment.Results)
                        {
                            CloudBlockBlob blob = blobItem as CloudBlockBlob;
                            if (blob != null && (onlySomeBlobsName == null || (onlySomeBlobsName != null && onlySomeBlobsName.Contains(blob.Name))))
                            {
                                string path = Path.Combine(outputFolderName, blob.Name);

                                downloadTasks.Add(blob.DownloadToFileAsync(path, FileMode.Create));
                            }
                        }

                        continuationToken = segment.ContinuationToken;
                    }
                    while (continuationToken != null);

                    await Task.WhenAll(downloadTasks);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine(string.Format("Download of blobs from asset '{0}' failed !", assetName), true);
                    TextBoxLogWriteLine(e);
                    DoGridTransferDeclareError(response.Id, e);
                    return;
                }


                if (!response.token.IsCancellationRequested)
                {
                    TextBoxLogWriteLine("Download complete.");
                    DoGridTransferDeclareCompleted(response.Id, outputFolderName);
                    if (openFileExplorer)
                    {
                        Process.Start(outputFolderName);
                    }
                }
                else
                {
                    DoGridTransferDeclareCancelled(response.Id);
                }
            }, response.token);
        }


        private void fromMultipleFilesToolStripMenuItem_Click(object sender, EventArgs e) // upload from multiple files
        {
            DoMenuUploadFromFolder_Step1();
        }

        private void DoMenuUploadFromFolder_Step1()
        {
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog() { IsFolderPicker = true };

            if (!string.IsNullOrEmpty(_backuprootfolderupload))
            {
                openFolderDialog.DefaultDirectory = _backuprootfolderupload;
            }

            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DoMenuUploadFromFolder_Step2(openFolderDialog.FileName);
            }
        }

        private void DoMenuUploadFromFolder_Step2(string SelectedPath)
        {
            try
            {
                if (SelectedPath != null)
                {

                    List<string> listpb = AssetInfo.ReturnFilenamesWithProblem(Directory.GetFiles(SelectedPath).ToList());
                    if (listpb.Count > 0)
                    {
                        MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UploadOptions form = new UploadOptions(_amsClientV3, true);
                    if (form.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }

                    _backuprootfolderupload = SelectedPath;

                    IEnumerable<string> filePaths = Directory.EnumerateFiles(SelectedPath as string);

                    TextBoxLogWriteLine("There are {0} files in {1}", filePaths.Count().ToString(), (SelectedPath as string));
                    if (!filePaths.Any())
                    {
                        throw new FileNotFoundException(string.Format("No files in directory, check folderPath: {0}", SelectedPath));
                    }


                    if (form.SingleAsset)
                    {
                        TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(SelectedPath)), TransferType.UploadFromFolder, true);

                        Task<Task> myTask = Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                              filePaths.ToList(),
                              response.Id,
                              response.token,
                              storageaccount: form.StorageSelected,
                              blocksize: form.BlockSize
                              ), response.token);

                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {

                            int i = 0;
                            foreach (string f in filePaths.ToList())
                            {
                                try
                                {
                                    i++;
                                    TransferEntryResponse response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(f) + "'", TransferType.UploadFromFile, true);
                                    // Start a worker thread that does uploading.
                                    Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                              new List<string>() { f },
                              response.Id,
                              response.token,
                              storageaccount: form.StorageSelected,
                              blocksize: form.BlockSize
                              ), response.token);

                                    if (i == 10) // let's use a batch of 10 threads at the same time
                                    {
                                        do
                                        {
                                            Task.Delay(1000).Wait();
                                        }
                                        while (ReturnTransfer(response.Id).State == TransferState.Queued);
                                        i = 0;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                                    TextBoxLogWriteLine(ex);
                                }
                            }


                        });


                    }

                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + Constants.endline + Program.GetErrorMessage(ex));
                TextBoxLogWriteLine("Error: Could not read file or folder '{0}' from disk.", SelectedPath, true);
                TextBoxLogWriteLine(ex);
            }
        }


        private void DoMenuImportFromHttp()
        {
            ImportHttp form = new ImportHttp(_amsClientV3);

            if (form.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Import from Http of '{0}'", Path.GetFileName(form.GetURL.LocalPath)), TransferType.ImportFromHttp, false);
                    // Start a worker thread that does uploading.
                    // ProcessHttpSourceV3
                    Task.Factory.StartNew(() => ProcessHttpSourceV3(form.GetURL, response.Id, response.token, form.StorageSelected, form.GetAssetName, form.GetAssetDescription), response.token);

                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
        }



        private void DotabControlMainSwitch(string tab)
        {
            foreach (TabPage page in tabControlMain.TabPages)
            {
                if (page.Text.Contains(tab))
                {
                    tabControlMain.BeginInvoke(new Action(() => tabControlMain.SelectedTab = page), null);
                    break;
                }
            }
        }


        public DialogResult? DisplayInfo(Asset asset)
        {
            DialogResult? dialogResult = null;
            if (asset != null)
            {
                // Refresh the asset.
                _amsClientV3.RefreshTokenIfNeeded();
                asset = Task.Run(async () => await GetAssetAsync(asset.Name)).Result;
                if (asset != null)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        AssetInformation form = new AssetInformation(this, _amsClientV3)
                        {
                            myAssetV3 = asset,

                            myStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints // we want to keep the same sorting
                        };

                        dialogResult = form.ShowDialog(this);

                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                        dataGridViewAssetsV.PurgeCacheAsset(asset);
                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                    }
                }

            }
            else
            {
                MessageBox.Show("Asset not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dialogResult;
        }

        /*
        public static DialogResult CopyAssetToAzure(ref bool UseDefaultStorage, ref string containername, ref string otherstoragename, ref string otherstoragekey, ref List<IAssetFile> SelectedFiles, ref bool CreateNewContainer, IAsset sourceAsset)
        {
            ExportAssetToAzureStorage form = new ExportAssetToAzureStorage(_context, _credentials.DefaultStorageKey, sourceAsset, _credentials.ReturnStorageSuffix())
            {
                BlobStorageDefault = UseDefaultStorage,
                BlobLabelDefaultStorage = _context.DefaultStorageAccount.Name,
                BlobLabelWarning = sourceAsset.Options == AssetCreationOptions.StorageEncrypted ? "Note: asset is storage encrypted" : ""
            };
            DialogResult dialogResult = form.ShowDialog();

            UseDefaultStorage = form.BlobStorageDefault;
            if (!UseDefaultStorage)
            {
                otherstoragename = form.BlobOtherStorageName;
                otherstoragekey = form.BlobOtherStorageKey;
            }
            CreateNewContainer = form.BlobCreateNewContainer;
            containername = CreateNewContainer ? form.BlobNewContainerName : form.SelectedContainer;
            SelectedFiles = form.SelectedAssetFiles;
            return dialogResult;
        }
        */

        public DialogResult? DisplayInfo(JobExtension job)
        {
            DialogResult? dialogResult = null;
            if (job != null)
            {


                try
                {
                    Cursor = Cursors.WaitCursor;
                    JobInformation form = new JobInformation(this, _amsClientV3)
                    {
                        MyJob = job.Job
                        //  MyStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints, // we pass this information if user open asset info from the job info dialog box
                    };
                    dialogResult = form.ShowDialog(this);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }

            }
            return dialogResult;
        }

        public DialogResult? DisplayInfo(Transform t)
        {
            DialogResult? dialogResult = null;
            if (t != null)
            {


                try
                {
                    Cursor = Cursors.WaitCursor;
                    TransformInformation form = new TransformInformation(this, _amsClientV3.AMSclient, t);

                    dialogResult = form.ShowDialog(this);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }

            }
            return dialogResult;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)  // RENAME ASSET
        {
            DoMenuChangeAssetDescription();
        }


        private void DoMenuChangeAssetDescription()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsV3();

            if (SelectedAssets.Count > 0)
            {
                Asset AssetTORename = SelectedAssets.FirstOrDefault();

                if (AssetTORename != null)
                {
                    string value = AssetTORename.Description;

                    if (Program.InputBox("Asset description", string.Format("Enter the new description for asset '{0}' :", AssetTORename.Name), ref value) == DialogResult.OK)
                    {
                        try
                        {
                            AssetTORename.Description = value;
                            _amsClientV3.AMSclient.Assets.Update(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, AssetTORename.Name, AssetTORename);
                        }
                        catch
                        {
                            TextBoxLogWriteLine("There is a problem when changing the asset description.", true);
                            return;
                        }
                        TextBoxLogWriteLine("Description of asset '{0}' updated.", AssetTORename.Name);
                        dataGridViewAssetsV.PurgeCacheAsset(AssetTORename);
                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                    }
                }
            }
        }

        private void DoMenuEditAssetAltId()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsV3();

            if (SelectedAssets.Count > 0)
            {
                Asset AssetToEditAltId = SelectedAssets.FirstOrDefault();

                if (AssetToEditAltId != null)
                {
                    string value = AssetToEditAltId.AlternateId;

                    if (Program.InputBox("Asset Alternate Id", string.Format("Enter the new alternate Id for asset '{0}' :", AssetToEditAltId.Name), ref value) == DialogResult.OK)
                    {
                        try
                        {
                            AssetToEditAltId.AlternateId = value;
                            _amsClientV3.AMSclient.Assets.Update(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, AssetToEditAltId.Name, AssetToEditAltId);
                        }
                        catch
                        {
                            TextBoxLogWriteLine("There is a problem when editing the alternate Id.", true);
                            return;
                        }
                        TextBoxLogWriteLine("Alternate Id for Asset Id '{0}' is now '{1}'.", AssetToEditAltId.Id, AssetToEditAltId.AlternateId);
                        dataGridViewAssetsV.PurgeCacheAsset(AssetToEditAltId);
                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                    }
                }
            }
        }


        private void DoMenuDownloadToLocal()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsV3();
            if (SelectedAssets.Count == 0)
            {
                return;
            }

            Asset mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null)
            {
                return;
            }

            DownloadToLocal form = new DownloadToLocal(SelectedAssets, _backuprootfolderdownload);

            if (form.ShowDialog() == DialogResult.OK)
            {
                bool ErrorFolderCreation = false;
                _backuprootfolderdownload = form.FolderPath; // for reuse later
                if (!Directory.Exists(form.FolderPath))
                {
                    if (MessageBox.Show(string.Format("Folder '{0}' does not exist." + Constants.endline + "Do you want to create it ?", form.FolderPath), "Folder does not exist", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        try
                        {
                            Directory.CreateDirectory(form.FolderPath);
                        }
                        catch
                        {
                            ErrorFolderCreation = true;
                            MessageBox.Show(string.Format("Error when creating folder '{0}'.", form.FolderPath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TextBoxLogWriteLine("Error when creating folder '{0}'.", form.FolderPath, true);
                        }
                    }
                    else
                    {
                        ErrorFolderCreation = true;
                        TextBoxLogWriteLine("User cancelled the folder creation.", true);
                    }
                }
                if (!ErrorFolderCreation)
                {
                    List<string> listfiles = new List<string>(); // let's see if some files exist in the destination
                    foreach (Asset asset in SelectedAssets)
                    {
                        string path = form.FolderPath;
                        if (form.FolderOption == DownloadToFolderOption.SubfolderAssetName)
                        {
                            path = Path.Combine(path, asset.Name);
                        }

                        //listfiles.AddRange(asset.AssetFiles.ToList().Where(f => File.Exists(path + @"\\" + f.Name)).Select(f => path + @"\\" + f.Name).ToList());
                    }
                    /*
                    if (listfiles.Count > 0)
                    {
                        string text;
                        if (listfiles.Count > 20)
                        {
                            text = string.Format(
                                                "{0} files are already in the folder(s)\n\nOverwite the files ?",
                                                listfiles.Count
                                                );
                        }
                        else if (listfiles.Count > 1)
                        {
                            text = string.Format(
                                                "The following files are already in the folder(s)\n\n{0}\n\nOverwite the files ?",
                                                string.Join("\n", listfiles.Select(f => Path.GetFileName(f)).ToArray())
                                                );
                        }
                        else
                        {
                            text = string.Format(
                                                 "The following file is already in the folder\n\n{0}\n\nOverwite the file ?",
                                                 string.Join("\n", listfiles.Select(f => Path.GetFileName(f)).ToArray())
                                                 );
                        }

                        if (MessageBox.Show(text, "File(s) overwrite", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                        {
                            return;
                        }
                        try
                        {
                            listfiles.ForEach(f => File.Delete(f));
                        }
                        catch
                        {
                            MessageBox.Show("Error when deleting files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    */
                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);

                    int i = 0;
                    foreach (Asset asset in SelectedAssets)
                    {
                        i++;
                        string label = string.Format("Download of asset '{0}'", asset.Name);
                        TransferEntryResponse response = DoGridTransferAddItem(label, TransferType.DownloadToLocal, true);
                        // if (SelectedAssets.Count > 1) label = string.Format("Download of {0} assets", SelectedAssets.Count);
                        Task<Task> myTask = Task.Factory.StartNew(() =>

                        //ProcessDownloadAsset(SelectedAssets, form.FolderPath, response.Id, form.FolderOption, form.OpenFolderAfterDownload, response.token)
                        DownloadOutputAssetAsync(_amsClientV3, asset.Name, form.FolderPath, response, form.FolderOption, form.OpenFolderAfterDownload)
                    , response.token);


                        if (i == 10) // let's use a batch of 10 threads at the same time
                        {
                            do
                            {
                                Task.Delay(1000).Wait();
                            }
                            while (ReturnTransfer(response.Id).State == TransferState.Queued);
                            i = 0;
                        }

                    }

                    // Start a worker thread that does downloading.


                }
            }
        }


        private void cancelJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCancelJobs();
        }


        private void DoCancelJobs()
        {
            List<JobExtension> SelectedJobs = ReturnSelectedJobsV3();

            if (SelectedJobs.Count > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                string question = "Cancel these " + SelectedJobs.Count + " jobs ?";
                if (SelectedJobs.Count == 1)
                {
                    question = "Cancel " + SelectedJobs[0].Job.Name + " ?";
                }

                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) cancelation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (JobExtension JobToCancel in SelectedJobs)
                    {
                        if (JobToCancel != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Canceling job '{0}'...", JobToCancel.Job.Name);

                            try
                            {
                                _amsClientV3.AMSclient.Jobs.CancelJob(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, JobToCancel.TransformName, JobToCancel.Job.Name);
                                TextBoxLogWriteLine("Job '{0}' canceled.", JobToCancel.Job.Name);

                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("Error when canceling job '{0}'.", JobToCancel.Job.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                    }
                    DoRefreshGridJobV(false);
                }
            }
        }

        private void assetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private async void DoCreateLocator(List<Asset> SelectedAssets, string LiveAssetManifest = null)
        {
            string labelAssetName;
            if (SelectedAssets.Count > 0)
            {
                if (SelectedAssets.Count == 1 && SelectedAssets.FirstOrDefault() == null)
                {
                    MessageBox.Show("Asset not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = "A locator will be created for the " + SelectedAssets.Count.ToString() + " selected assets.";
                }
                else
                {
                    labelAssetName = "A locator will be created for Asset '" + SelectedAssets.FirstOrDefault().Name + "'.";
                }

                CreateLocator formCreateLocator = new CreateLocator(_amsClientV3, SelectedAssets)
                {
                    LocatorStartDate = DateTime.UtcNow.AddMinutes(-5),
                    LocatorEndDate = DateTime.UtcNow.AddDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew),
                    LocAssetName = labelAssetName,
                    LocatorHasStartDate = false,
                    LocWarning = string.Empty
                };

                if (formCreateLocator.ShowDialog() == DialogResult.OK)
                {
                    await _amsClientV3.RefreshTokenIfNeededAsync();

                    // The duration for the locator's access policy.
                    TimeSpan accessPolicyDuration = formCreateLocator.LocatorEndDate.Subtract(DateTime.UtcNow);
                    if (formCreateLocator.LocatorStartDate != null)
                    {
                        accessPolicyDuration = formCreateLocator.LocatorEndDate.Subtract((DateTime)formCreateLocator.LocatorStartDate);
                    }

                    // DRM
                    ContentKeyPolicy keyPolicy = null;
                    List<DRM_Config_TokenClaims> formPlayreadyTokenClaims = new List<DRM_Config_TokenClaims>();
                    List<DRM_Config_TokenClaims> formWidevineTokenClaims = new List<DRM_Config_TokenClaims>();
                    List<DRM_Config_TokenClaims> formClearKeyTokenClaims = new List<DRM_Config_TokenClaims>();
                    List<ContentKeyPolicyOption> options = new List<ContentKeyPolicyOption>();

                    // let's preserve location of windows
                    int left = formCreateLocator.Left;
                    int top = formCreateLocator.Top;

                    if (formCreateLocator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey || formCreateLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || formCreateLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming)
                    {
                        string tokenSymKey = Properties.Settings.Default.DynEncTokenSymKeyv3;
                        if (string.IsNullOrWhiteSpace(tokenSymKey))
                        {
                            tokenSymKey = null;
                        }

                        if (formCreateLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || formCreateLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming)
                        {
                            DRM_CENCDelivery formCencDelivery = new DRM_CENCDelivery(true, true) { Left = left, Top = top };
                            if (formCencDelivery.ShowDialog() != DialogResult.OK)
                            {
                                return;
                            }

                            int step = 1;
                            SavePositionOfForm(formCencDelivery, out left, out top);

                            // for each PlayReady option
                            List<DRM_PlayReadyLicense> formPlayready = new List<DRM_PlayReadyLicense>();

                            for (int i = 0; i < formCencDelivery.GetNumberOfAuthorizationPolicyOptionsPlayReady; i++)
                            {
                                bool laststep = (i == formCencDelivery.GetNumberOfAuthorizationPolicyOptionsPlayReady - 1) && (formCencDelivery.GetNumberOfAuthorizationPolicyOptionsWidevine == 0);

                                formPlayreadyTokenClaims.Add(new DRM_Config_TokenClaims(step++, i + 1, "PlayReady", tokenSymKey, false)
                                { Left = left, Top = top });

                                if (formPlayreadyTokenClaims[i].ShowDialog() != DialogResult.OK)
                                {
                                    return;
                                }

                                tokenSymKey = formPlayreadyTokenClaims[i].textBoxSymKey.Text; // let's reuse the same key if the user wants
                                SavePositionOfForm(formPlayreadyTokenClaims[i], out left, out top);

                                formPlayready.Add(new DRM_PlayReadyLicense(step++, i + 1, laststep) { Left = left, Top = top });
                                if (formPlayready[i].ShowDialog() != DialogResult.OK)
                                {
                                    return;
                                }

                                SavePositionOfForm(formPlayready[i], out left, out top);

                                options.Add(
                                               new ContentKeyPolicyOption()
                                               {
                                                   Configuration = formPlayready[i].GetPlayReadyConfiguration,
                                                   Restriction = formPlayreadyTokenClaims[i].GetContentKeyPolicyRestriction,
                                                   Name = formPlayready[i].PlayReadOptionName
                                               });
                            }

                            // for each Widevine option
                            List<DRM_WidevineLicense> formWidevine = new List<DRM_WidevineLicense>();

                            for (int i = 0; i < formCencDelivery.GetNumberOfAuthorizationPolicyOptionsWidevine; i++)
                            {
                                bool laststep = (i == formCencDelivery.GetNumberOfAuthorizationPolicyOptionsWidevine - 1);

                                formWidevineTokenClaims.Add(new DRM_Config_TokenClaims(step++, i + 1, "Widevine", tokenSymKey, false) { Left = left, Top = top });
                                if (formWidevineTokenClaims[i].ShowDialog() != DialogResult.OK)
                                {
                                    return;
                                }

                                tokenSymKey = formWidevineTokenClaims[i].textBoxSymKey.Text; // let's reuse the same key if the user wants
                                SavePositionOfForm(formWidevineTokenClaims[i], out left, out top);

                                formWidevine.Add(new DRM_WidevineLicense(step++, i + 1, laststep) { Left = left, Top = top });
                                if (formWidevine[i].ShowDialog() != DialogResult.OK)
                                {
                                    return;
                                }

                                SavePositionOfForm(formWidevine[i], out left, out top);

                                options.Add(
                                            new ContentKeyPolicyOption()
                                            {
                                                Configuration = formWidevine[i].GetWidevineConfiguration,
                                                Restriction = formWidevineTokenClaims[i].GetContentKeyPolicyRestriction,
                                                Name = formWidevine[i].WidevinePolicyName
                                            });
                            }

                        }
                        else if (formCreateLocator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey)
                        {
                            formClearKeyTokenClaims.Add(new DRM_Config_TokenClaims(1, 1, "Clear Key", tokenSymKey, true) { Left = left, Top = top });
                            if (formClearKeyTokenClaims[0].ShowDialog() != DialogResult.OK)
                            {
                                return;
                            }

                            options.Add(
                                           new ContentKeyPolicyOption()
                                           {
                                               Configuration = new ContentKeyPolicyClearKeyConfiguration(),
                                               Restriction = formClearKeyTokenClaims[0].GetContentKeyPolicyRestriction
                                           });

                        }

                        Properties.Settings.Default.DynEncTokenSymKeyv3 = tokenSymKey;
                        Program.SaveAndProtectUserConfig();

                        try
                        {
                            keyPolicy = await _amsClientV3.AMSclient.ContentKeyPolicies.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup,
                        _amsClientV3.credentialsEntry.AccountName,
                        "keypolicy-" + Guid.NewGuid().ToString().Substring(0, 13),
                        options);
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when creating the content key policy.", true);
                            TextBoxLogWriteLine(e);
                        }
                    }

                    sbuilder.Clear();

                    try
                    {
                        List<LocatorAndUrls> listLocators = await Task.Run(() =>
                        ProcessCreateLocatorV3(formCreateLocator.StreamingPolicyName, SelectedAssets, formCreateLocator.LocatorStartDate, formCreateLocator.LocatorEndDate, formCreateLocator.ForceLocatorGuid, keyPolicy, formCreateLocator.SelectedFilters));

                        Microsoft.Rest.Azure.IPage<StreamingEndpoint> SEList = await _amsClientV3.AMSclient.StreamingEndpoints.ListAsync(
                             _amsClientV3.credentialsEntry.ResourceGroup,
                              _amsClientV3.credentialsEntry.AccountName
                             );

                        foreach (LocatorAndUrls loc in listLocators)
                        {
                            ListPathsResponse paths = await _amsClientV3.AMSclient.StreamingLocators.ListPathsAsync(
                            _amsClientV3.credentialsEntry.ResourceGroup,
                             _amsClientV3.credentialsEntry.AccountName,
                             loc.LocatorName
                            );

                            sbuilder.AppendLine(string.Format("Asset name : {0}", loc.AssetName));
                            sbuilder.AppendLine("===============" + new string('=', loc.AssetName.Length));
                            sbuilder.AppendLine(string.Format("Locator name : {0}", loc.LocatorName));
                            sbuilder.AppendLine(string.Empty);


                            foreach (StreamingPath path in paths.StreamingPaths)
                            {
                                string appendExtension = string.Empty;

                                sbuilder.AppendLine(path.StreamingProtocol + " :");
                                foreach (StreamingEndpoint se in SEList)
                                {
                                    if (path.Paths.Count == 0 && LiveAssetManifest != null) // live output without data yet so API does not return valid URLs. Let's build them as we know the manifest name
                                    {
                                        string formatSyntax = null;
                                        string syntax = "(format={0})";
                                        if (path.StreamingProtocol == StreamingPolicyStreamingProtocol.Dash)
                                        {
                                            formatSyntax = AssetInfo.format_dash_csf;
                                            appendExtension = Constants.mpd;
                                        }
                                        else if (path.StreamingProtocol == StreamingPolicyStreamingProtocol.Hls)
                                        {
                                            formatSyntax = AssetInfo.format_hls_v4;
                                            appendExtension = Constants.m3u8;
                                        }
                                        else
                                        {
                                            appendExtension = syntax = string.Empty;
                                        }
                                        sbuilder.AppendLine("https://" + se.HostName + "/" + loc.LocatorId.ToString() + "/" + LiveAssetManifest + ".ism/manifest" + string.Format(syntax, formatSyntax) + appendExtension);
                                    }
                                    else
                                    {
                                        foreach (var p in path.Paths)
                                        {
                                            appendExtension = string.Empty;
                                            if (path.StreamingProtocol == StreamingPolicyStreamingProtocol.Dash && !p.EndsWith(Constants.mpd))
                                            {
                                                appendExtension = Constants.mpd;
                                            }
                                            else if (path.StreamingProtocol == StreamingPolicyStreamingProtocol.Hls && !p.EndsWith(Constants.m3u8))
                                            {
                                                appendExtension = Constants.m3u8;
                                            }
                                            sbuilder.AppendLine("https://" + se.HostName + p + appendExtension);
                                        }

                                    }
                                }
                                sbuilder.AppendLine(string.Empty);
                            }

                            foreach (string path in paths.DownloadPaths)
                            {
                                sbuilder.AppendLine("Download :");
                                foreach (StreamingEndpoint se in SEList)
                                {
                                    sbuilder.AppendLine("https://" + se.HostName + path);
                                }
                                sbuilder.AppendLine(string.Empty);
                            }
                            sbuilder.AppendLine(string.Empty);
                        }


                        // need test token ?
                        if (formPlayreadyTokenClaims.Any(t => t.NeedToken) || formWidevineTokenClaims.Any(t => t.NeedToken) || formClearKeyTokenClaims.Any(t => t.NeedToken))
                        {
                            DRM_GenerateToken formTokenProperties = new DRM_GenerateToken();
                            formTokenProperties.ShowDialog();
                            if (formTokenProperties.DialogResult == DialogResult.OK)
                            {
                                await AddTestTokenToSbuilder(formPlayreadyTokenClaims, listLocators, "PlayReady", formTokenProperties.TokenDuration, formTokenProperties.TokenUse);
                                await AddTestTokenToSbuilder(formWidevineTokenClaims, listLocators, "Widevine", formTokenProperties.TokenDuration, formTokenProperties.TokenUse);
                                await AddTestTokenToSbuilder(formClearKeyTokenClaims, listLocators, "Clear Key", formTokenProperties.TokenDuration, formTokenProperties.TokenUse);
                            }
                        }

                        EditorXMLJSON displayResult = new EditorXMLJSON("Locator information", sbuilder.ToString(), false, false, false);
                        displayResult.ShowDialog();
                    }

                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when creating a locator", true);
                        TextBoxLogWriteLine(e);
                    }
                }
            }
        }

        private static void SavePositionOfForm(Form myForm, out int left, out int top)
        {
            left = myForm.Left;
            top = myForm.Top;
        }

        private async Task AddTestTokenToSbuilder(List<DRM_Config_TokenClaims> formTokenClaims, List<LocatorAndUrls> listLocators, string DRMTechnology, int tokenDuration, int? tokenUse)
        {
            foreach (DRM_Config_TokenClaims tokenClaims in formTokenClaims)
            {
                if (tokenClaims.GetDetailedTokenType == ExplorerTokenType.JWTSym)
                {
                    // We are using the ContentKeyIdentifierClaim in the ContentKeyPolicy which means that the token presented
                    // to the Key Delivery Component must have the identifier of the content key in it.  Since we didn't specify
                    // a content key when creating the StreamingLocator, the system created a random one for us.  In order to 
                    // generate our test token we must get the ContentKeyId to put in the ContentKeyIdentifierClaim claim.
                    ListContentKeysResponse response = await _amsClientV3.AMSclient.StreamingLocators.ListContentKeysAsync(_amsClientV3.credentialsEntry.ResourceGroup,
                            _amsClientV3.credentialsEntry.AccountName, listLocators.FirstOrDefault().LocatorName);
                    string keyIdentifier = response.ContentKeys.First().Id.ToString();

                    sbuilder.AppendLine(string.Format("Test token for {0}, Option {1} (valid {2} min) :",
                        DRMTechnology,
                        formTokenClaims.IndexOf(tokenClaims) + 1,
                        tokenDuration));
                    sbuilder.AppendLine(Constants.Bearer + tokenClaims.GetTestToken(keyIdentifier, tokenClaims.GetTokenRequiredClaims, tokenDuration, tokenUse));
                    sbuilder.AppendLine(string.Empty);
                }
            }
        }

        private async void DoCreateSASUrl(List<Asset> SelectedAssets)
        {
            if (SelectedAssets.Count > 0)
            {
                if (SelectedAssets.Count == 1 && SelectedAssets.FirstOrDefault() == null)
                {
                    MessageBox.Show("Asset not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show(string.Format("Create a SAS Container Path Url and files SAS Urls valid for {0} hours ?", Properties.Settings.Default.DefaultSASDurationInHours), "SAS Urls", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    await _amsClientV3.RefreshTokenIfNeededAsync();

                    string result = await Task.Run(() =>
                    ProcessCreateLocatorSAS(SelectedAssets)
                    );

                    EditorXMLJSON displayResult = new EditorXMLJSON("SAS Urls", result, false, false, false);
                    displayResult.ShowDialog();
                }
            }
        }


        private List<LocatorAndUrls> ProcessCreateLocatorV3(string streamingPolicyName, List<Asset> assets, Nullable<DateTime> startTime, Nullable<DateTime> endTime, string ForceLocatorGUID, ContentKeyPolicy keyPolicy, List<string> listFilters = null)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            List<LocatorAndUrls> listLocatorNames = new List<LocatorAndUrls>();

            foreach (Asset AssetToP in assets)
            {
                StreamingLocator locator = null;
                string keyPolicyName = keyPolicy?.Name;

                try
                {
                    string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);
                    string streamingLocatorName = "locator-" + uniqueness;

                    locator = new StreamingLocator(
                        assetName: AssetToP.Name,
                        streamingPolicyName: streamingPolicyName,
                        streamingLocatorId: string.IsNullOrEmpty(ForceLocatorGUID) ? (Guid?)null : Guid.Parse(ForceLocatorGUID),
                        startTime: startTime,
                        endTime: endTime,
                        defaultContentKeyPolicyName: keyPolicyName,
                        filters: listFilters
                        );

                    locator = _amsClientV3.AMSclient.StreamingLocators.Create(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, streamingLocatorName, locator);

                    TextBoxLogWriteLine("Locator created : {0}", locator.Name);
                    IList<StreamingPath> streamingPaths = _amsClientV3.AMSclient.StreamingLocators.ListPaths(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, locator.Name).StreamingPaths;
                    listLocatorNames.Add(new LocatorAndUrls() { AssetName = AssetToP.Name, LocatorName = streamingLocatorName, LocatorId = locator.StreamingLocatorId, Paths = streamingPaths.ToList() });

                }

                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error. Could not create a locator for '{0}' ", AssetToP.Name, true);
                    TextBoxLogWriteLine(ex);
                    return null;
                }
            }

            dataGridViewAssetsV.PurgeCacheAssetsV3(assets);
            dataGridViewAssetsV.AnalyzeItemsInBackground();

            return listLocatorNames;
        }

        private async Task<string> ProcessCreateLocatorSAS(List<Asset> assets)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();

            StringBuilder stringLines = new StringBuilder();

            const int ListBlobsSegmentMaxResult = 100;


            foreach (Asset AssetToP in assets)
            {
                AssetContainerSas assetContainerSas = await _amsClientV3.AMSclient.Assets.ListContainerSasAsync(
                                                               _amsClientV3.credentialsEntry.ResourceGroup,
                                                               _amsClientV3.credentialsEntry.AccountName,
                                                               AssetToP.Name,
                                                               permissions: AssetContainerPermission.Read,
                                                               expiryTime: DateTime.Now.AddHours(Properties.Settings.Default.DefaultSASDurationInHours).ToUniversalTime()
                                                               );

                Uri containerSasUrl = new Uri(assetContainerSas.AssetContainerSasUrls.FirstOrDefault());
                CloudBlobContainer container = new CloudBlobContainer(containerSasUrl);

                stringLines.AppendLine("Asset : " + AssetToP.Name);
                stringLines.AppendLine("========" + new string('=', AssetToP.Name.Length));
                stringLines.AppendLine(string.Empty);
                stringLines.AppendLine("SAS Container Path");
                stringLines.AppendLine(containerSasUrl.ToString());
                stringLines.AppendLine(string.Empty);

                BlobResultSegment segment = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, ListBlobsSegmentMaxResult, null, null, null);

                foreach (IListBlobItem blobItem in segment.Results)
                {
                    CloudBlockBlob blob = blobItem as CloudBlockBlob;
                    if (blob != null)
                    {
                        UriBuilder bloburl = new UriBuilder(containerSasUrl);
                        bloburl.Path += "/" + blob.Name;
                        stringLines.AppendLine(blob.Name);
                        stringLines.AppendLine(bloburl.ToString());
                        stringLines.AppendLine(string.Empty);
                    }
                }
            }

            return stringLines.ToString();
        }


        public string AddBracket(string url)
        {
            return "<" + url + ">";
        }

        public void DoCopyClipboard(object text)
        {
            Clipboard.SetText((string)text);
        }


        private void DoDeleteAllLocatorsOnAssets(List<Asset> SelectedAssets)
        {
            if (SelectedAssets.Count > 0)
            {
                if (SelectedAssets.Count == 1 && SelectedAssets[0] == null)
                {
                    MessageBox.Show("Asset not found !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string question = "Delete all locators of these " + SelectedAssets.Count + " assets ?";
                if (SelectedAssets.Count == 1)
                {
                    question = "Delete all the locators of " + SelectedAssets[0].Name + " ?";
                }

                if (System.Windows.Forms.MessageBox.Show(question, "Locators deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (Asset AssetToProcess in SelectedAssets)
                    {
                        if (AssetToProcess != null)
                        {
                            //delete locators
                            TextBoxLogWriteLine("Deleting locators of asset '{0}'", AssetToProcess.Name);
                            try
                            {
                                DeleteLocatorsForAsset(AssetToProcess);
                                TextBoxLogWriteLine("Deletion done.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting locators of the asset {0}.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(ex);
                            }
                            dataGridViewAssetsV.PurgeCacheAssetsV3(SelectedAssets);
                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                        }
                    }
                }
            }
        }

        private List<Asset> ReturnSelectedAssetsFromProgramsOrAssetsV3()
        {
            if (tabControlMain.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabAssets)) // we are in the asset tab
            {
                return ReturnSelectedAssetsV3();
            }
            else if (tabControlMain.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabLive)) // we are in the live tab
            {
                _amsClientV3.RefreshTokenIfNeeded();

                return ReturnSelectedLiveOutputs()
                        .Select(p => _amsClientV3.AMSclient.Assets.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, p.AssetName))
                        .ToList();
            }
            else
            {
                return null;
            }
        }


        private List<Asset> ReturnSelectedAssetsV3()
        {
            List<Asset> SelectedAssets = new List<Asset>();
            _amsClientV3.RefreshTokenIfNeeded();

            try
            {
                foreach (DataGridViewRow Row in dataGridViewAssetsV.SelectedRows)
                {
                    Asset asset = _amsClientV3.AMSclient.Assets.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, Row.Cells[dataGridViewAssetsV.Columns["Name"].Index].Value.ToString());
                    if (asset != null)
                    {
                        SelectedAssets.Add(asset);
                    }
                }
                SelectedAssets.Reverse();
            }
            catch (Exception ex)
            {
                // connection error ?
                TextBoxLogWriteLine(ex);
            }

            return SelectedAssets;
        }



        private List<JobExtension> ReturnSelectedJobsV3()
        {
            List<JobExtension> SelectedJobs = new List<JobExtension>();
            foreach (DataGridViewRow Row in dataGridViewJobsV.SelectedRows)
            {
                Job job = Task.Run(async () => await GetJobAsync(Row.Cells["TransformName"].Value.ToString(), Row.Cells["Name"].Value.ToString())).Result;
                SelectedJobs.Add(new JobExtension()
                {
                    Job = job,
                    TransformName = Row.Cells["TransformName"].Value.ToString()
                });
            }

            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        private List<Transform> ReturnSelectedTransforms()
        {
            List<Transform> SelectedTransforms = new List<Transform>();
            _amsClientV3.RefreshTokenIfNeeded();

            Microsoft.Rest.Azure.IPage<Transform> Transforms = _amsClientV3.AMSclient.Transforms.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);

            foreach (DataGridViewRow Row in dataGridViewTransformsV.SelectedRows)
            {
                string transformName = Row.Cells[dataGridViewTransformsV.Columns["Name"].Index].Value.ToString();
                Transform myTransform = Transforms.Where(f => f.Name == transformName).FirstOrDefault();
                if (myTransform != null)
                {
                    SelectedTransforms.Add(myTransform);
                }
            }
            return SelectedTransforms;
        }


        private StorageAccount ReturnSelectedStorage()
        {
            StorageAccount SelectedStorage = null;
            if (dataGridViewStorage.SelectedRows.Count == 1)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                DataGridViewRow row = dataGridViewStorage.SelectedRows[0];
                int index = dataGridViewStorage.Columns["Id"].Index;
                string storagename = AMSClientV3.GetStorageName(row.Cells[index].Value.ToString());
                SelectedStorage = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).StorageAccounts.Where(s => AMSClientV3.GetStorageName(s.Id) == storagename).FirstOrDefault();
            }

            return SelectedStorage;
        }

        private List<AccountFilter> ReturnSelectedAccountFilters()
        {
            List<AccountFilter> SelectedFilters = new List<AccountFilter>();
            _amsClientV3.RefreshTokenIfNeeded();

            Microsoft.Rest.Azure.IPage<AccountFilter> aFilters = _amsClientV3.AMSclient.AccountFilters.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);
            foreach (DataGridViewRow Row in dataGridViewFilters.SelectedRows)
            {
                string filtername = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                AccountFilter myfilter = aFilters.Where(f => f.Name == filtername).FirstOrDefault();
                if (myfilter != null)
                {
                    SelectedFilters.Add(myfilter);
                }
            }

            return SelectedFilters;
        }



        private void selectedAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDeleteSelectedAssets();

        }

        private void DoMenuDeleteSelectedAssets()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsV3();
            DoDeleteAssets(SelectedAssets);
        }

        private void DoDeleteAssets(List<Asset> SelectedAssets)
        {
            if (SelectedAssets.Count > 0)
            {
                //var form = new DeleteKeyAndPolicy(SelectedAssets.Count);
                string question = SelectedAssets.Count > 1 ?
                    string.Format("Do you want to delete these {0} assets ?", SelectedAssets.Count)
                    : string.Format("Do you want to delete asset '{0}' ?", SelectedAssets[0].Name);

                if (MessageBox.Show(question, "Asset deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _amsClientV3.RefreshTokenIfNeeded();
                    Task.Run(() =>
                    {
                        bool Error = false;
                        try
                        {
                            TextBoxLogWriteLine("Deleting asset(s)...");
                            Task[] deleteTasks = SelectedAssets.Select(a => _amsClientV3.AMSclient.Assets.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, a.Name)).ToArray();

                            //Task[] deleteTasks = SelectedAssets.Select(a => DynamicEncryption.DeleteAssetAsync(_context, a, form.DeleteDeliveryPolicies, form.DeleteKeys, form.DeleteAuthorizationPolicies)).ToArray();
                            Task.WaitAll(deleteTasks);
                        }
                        catch (Exception ex)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when deleting the asset(s)", true);
                            TextBoxLogWriteLine(ex);
                            Error = true;
                        }
                        if (!Error)
                        {
                            TextBoxLogWriteLine("Asset(s) deleted.");
                        }

                        DoRefreshGridAssetV(false);
                    }
          );

                }
            }
        }


        private void allAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        /*
        private void DoMenuImportFromAzureStorage()
        {
            string valuekey = "";
            string targetAssetID = "";

            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count > 0) targetAssetID = SelectedAssets.FirstOrDefault().Id;

            if (!havestoragecredentials)
            { // No blob credentials. Let's ask the user
                if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey, true) == DialogResult.OK)
                {
                    _credentials.DefaultStorageKey = valuekey;
                    havestoragecredentials = true;
                }
            }
            if (havestoragecredentials) // if we have the storage credentials
            {
                ImportFromAzureStorage form = new ImportFromAzureStorage(_context, _credentials.DefaultStorageKey, _credentials.ReturnStorageSuffix())
                {
                    ImportLabelDefaultStorageName = _context.DefaultStorageAccount.Name,
                    ImportNewAssetName = Constants.NameconvUploadasset,
                    ImportCreateNewAsset = true
                };

                if (!string.IsNullOrEmpty(targetAssetID))
                {
                    if (SelectedAssets.FirstOrDefault().Options == AssetCreationOptions.None && SelectedAssets.FirstOrDefault().StorageAccountName == _context.DefaultStorageAccount.Name) // Ok, the selected asset is not encrypted and is in the default storage account
                    {
                        form.ImportOptionToCopyFilesToExistingAsset = true;
                        form.ImportLabelExistingAssetName = AssetInfo.GetAsset(targetAssetID, _context).Name;
                        form.ImportOptionToCopyFilesToExistingAssetLabel = string.Empty;
                    }
                    else // selected asset is encrypted or not in the default storage account, so we disable it and display a warning
                    {
                        form.ImportOptionToCopyFilesToExistingAsset = false;
                        form.ImportOptionToCopyFilesToExistingAssetLabel = (SelectedAssets.FirstOrDefault().StorageAccountName != _context.DefaultStorageAccount.Name) ? "(Selected asset is not in the defaut storage)" : "(Selected asset seems to be encrypted)";
                    }
                }

                else  // no selected asset so we disable the option to copy file into an existing asset
                {
                    form.ImportOptionToCopyFilesToExistingAsset = false;
                    form.ImportOptionToCopyFilesToExistingAssetLabel = string.Empty;
                }

                if (form.ShowDialog() == DialogResult.OK)
                {
                    var response = DoGridTransferAddItem("Import from Azure Storage " + (form.ImportCreateNewAsset ? "to a new asset" : "to an existing asset"), TransferType.ImportFromAzureStorage, false);
                    // Start a worker thread that does uploading.
                    var myTask = Task.Factory.StartNew(() => ProcessImportFromAzureStorage(form.ImportUseDefaultStorage, form.SelectedBlobContainer, form.ImporOtherStorageName, form.ImportOtherStorageKey, form.SelectedBlobs, form.ImportCreateNewAsset, form.ImportNewAssetName, form.CreateOneAssetPerFile, targetAssetID, response), response.token);
                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
        }
        */

        /*
        private async void ProcessImportFromAzureStorage(bool UseDefaultStorage, string containername, string otherstoragename, string otherstoragekey, List<IListBlobItem> SelectedBlobs, bool CreateNewAsset, string newassetname, bool CreateOneAssetPerFile, string targetAssetID, TransferEntryResponse response)
        {
            bool Error = false;

            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(response.Id);
            if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
                return;
            }

            List<IAsset> assets = new List<IAsset>();
            if (CreateNewAsset)
            {
                if (CreateOneAssetPerFile) // one asset per file
                {
                    foreach (var file in SelectedBlobs)
                    {
                        string currentassetname = newassetname.Replace(Constants.NameconvUploadasset, HttpUtility.UrlDecode(Path.GetFileName(file.Uri.AbsoluteUri)));
                        assets.Add(_context.Assets.Create(currentassetname, AssetCreationOptions.None));
                    }
                }
                else // one asset for all files
                {
                    // Create a new asset.
                    string currentassetname = newassetname.Replace(Constants.NameconvUploadasset, HttpUtility.UrlDecode(Path.GetFileName(SelectedBlobs[0].Uri.AbsoluteUri)));
                    assets.Add(_context.Assets.Create(currentassetname, AssetCreationOptions.None));
                }
            }
            else //copy files in an existing asset
            {
                assets.Add(AssetInfo.GetAsset(targetAssetID, _context));
            }

            CloudStorageAccount sourceStorageAccount;
            if (UseDefaultStorage)
            {
                sourceStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey), _credentials.ReturnStorageSuffix(), true);
            }
            else
            {
                sourceStorageAccount = new CloudStorageAccount(new StorageCredentials(otherstoragename, otherstoragekey), _credentials.ReturnStorageSuffix(), true);
            }

            var sourceCloudBlobClient = sourceStorageAccount.CreateCloudBlobClient();
            var sourceMediaBlobContainer = sourceCloudBlobClient.GetContainerReference(containername);

            TextBoxLogWriteLine("Starting the Azure Storage copy process.");

            sourceMediaBlobContainer.CreateIfNotExists();

            // Get the SAS token to use for all blobs if dealing with multiple accounts
            string blobToken = sourceMediaBlobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                // Specify the expiration time for the signature.
                SharedAccessExpiryTime = DateTime.Now.AddDays(1),
                // Specify the permissions granted by the signature.
                Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read
            });

            IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy",
              TimeSpan.FromDays(1), AccessPermissions.Write);

            int assetindex = 0;
            string fileName;

            long BytesCopied = 0;
            double percentComplete;

            CloudBlockBlob sourceCloudBlob, destinationBlob;

            //calculate size of all files
            long Length = 0;
            foreach (var sourceBlob in SelectedBlobs)
            {
                fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                sourceCloudBlob = sourceMediaBlobContainer.GetBlockBlobReference(fileName);
                sourceCloudBlob.FetchAttributes();

                Length += sourceCloudBlob.Properties.Length;
            }


            foreach (var asset in assets)
            {
                if (response.token.IsCancellationRequested) break;

                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);

                var destinationStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey), _credentials.ReturnStorageSuffix(), true);
                var destBlobStorage = destinationStorageAccount.CreateCloudBlobClient();

                // Get the asset container URI and Blob copy from mediaContainer to assetContainer.
                string destinationContainerName = (new Uri(destinationLocator.Path)).Segments[1];

                CloudBlobContainer assetContainer =
                    destBlobStorage.GetContainerReference(destinationContainerName);

                if (CreateOneAssetPerFile)
                {
                    // do the copy
                    var sourceBlob = SelectedBlobs[assetindex];
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                    sourceCloudBlob = sourceMediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    if (sourceCloudBlob.Properties.Length > 0)
                    {
                        try
                        {
                            IAssetFile assetFile = asset.AssetFiles.Create(fileName);
                            destinationBlob = assetContainer.GetBlockBlobReference(fileName);

                            destinationBlob.DeleteIfExists();
                            string copyOperation = await destinationBlob.StartCopyAsync(new Uri(sourceBlob.Uri.AbsoluteUri + blobToken), response.token);
                            bool Cancelled = false;

                            while (destinationBlob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                destinationBlob.FetchAttributes();
                                percentComplete = (Convert.ToDouble(assetindex + 1) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + destinationBlob.CopyState.BytesCopied) / (long)Length;
                                DoGridTransferUpdateProgress(percentComplete, response.Id);

                                if (response.token.IsCancellationRequested && !Cancelled)
                                {
                                    await destinationBlob.AbortCopyAsync(copyOperation);
                                    Cancelled = true;
                                }
                            }

                            if (destinationBlob.CopyState.Status == CopyStatus.Failed)
                            {
                                DoGridTransferDeclareError(response.Id, destinationBlob.CopyState.StatusDescription);
                                Error = true;
                                break;
                            }

                            if (destinationBlob.CopyState.Status == CopyStatus.Aborted)
                            {
                                DoGridTransferDeclareCancelled(response.Id);
                                Error = true;
                                break;
                            }

                            destinationBlob.FetchAttributes();
                            assetFile.ContentFileSize = sourceCloudBlob.Properties.Length;
                            assetFile.Update();
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine("Failed to copy '{0}'", fileName, true);
                            DoGridTransferDeclareError(response.Id, ex);
                            Error = true;
                            break;

                        }
                        BytesCopied += sourceCloudBlob.Properties.Length;
                        percentComplete = 100d * BytesCopied / Length;
                        if (!Error) DoGridTransferUpdateProgress(percentComplete, response.Id);
                    }
                }
                else // all files in the same asset
                {
                    // do the copy
                    int nbblob = 0;

                    foreach (var sourceBlob in SelectedBlobs)
                    {
                        if (response.token.IsCancellationRequested) break;
                        nbblob++;
                        fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                        sourceCloudBlob = sourceMediaBlobContainer.GetBlockBlobReference(fileName);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {
                            try
                            {
                                IAssetFile assetFile = asset.AssetFiles.Create(fileName);
                                destinationBlob = assetContainer.GetBlockBlobReference(fileName);

                                try
                                {
                                    destinationBlob.DeleteIfExists();
                                }
                                catch
                                {

                                }

                                string copyOperation = await destinationBlob.StartCopyAsync(new Uri(sourceBlob.Uri.AbsoluteUri + blobToken), response.token);
                                bool Cancelled = false;

                                while (destinationBlob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    destinationBlob.FetchAttributes();
                                    percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + destinationBlob.CopyState.BytesCopied) / (long)Length;
                                    DoGridTransferUpdateProgress(percentComplete, response.Id);

                                    if (response.token.IsCancellationRequested && !Cancelled)
                                    {
                                        await destinationBlob.AbortCopyAsync(copyOperation);
                                        Cancelled = true;
                                    }

                                }

                                if (destinationBlob.CopyState.Status == CopyStatus.Failed)
                                {
                                    DoGridTransferDeclareError(response.Id, destinationBlob.CopyState.StatusDescription);
                                    Error = true;
                                    break;
                                }

                                if (destinationBlob.CopyState.Status == CopyStatus.Aborted)
                                {
                                    DoGridTransferDeclareCancelled(response.Id);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();
                                assetFile.ContentFileSize = sourceCloudBlob.Properties.Length;
                                assetFile.Update();
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine("Failed to copy '{0}'", fileName, true);
                                DoGridTransferDeclareError(response.Id, ex);
                                Error = true;
                                break;

                            }
                            BytesCopied += sourceCloudBlob.Properties.Length;
                            percentComplete = 100d * BytesCopied / Length;
                            if (!Error) DoGridTransferUpdateProgress(percentComplete, response.Id);

                        }
                    }
                }

                asset.Update();
                destinationLocator.Delete();

                // Refresh the asset.
                IAsset asset_refreshed = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();
                if (asset_refreshed.AssetFiles.Count() == 1)
                {
                    AssetInfo.SetFileAsPrimary(asset_refreshed, asset_refreshed.AssetFiles.FirstOrDefault().Name);
                }
                else
                {
                    AssetInfo.SetISMFileAsPrimary(asset_refreshed);
                }

                assetindex++;
            }

            writePolicy.Delete();

            if (!Error)
            {
                if (CreateOneAssetPerFile)
                {
                    DoGridTransferDeclareCompleted(response.Id, "");
                }
                else
                {
                    DoGridTransferDeclareCompleted(response.Id, assets[0].Id);
                }
            }

            DoRefreshGridAssetV(false);
        }
        */

        /*
        private async void ProcessExportAssetToAzureStorage(bool UseDefaultStorage, string containername, string otherstoragename, string otherstoragekey, List<IAssetFile> SelectedFiles, bool CreateNewContainer, TransferEntryResponse response)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(response.Id);
            if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
                return;
            }

            bool Error = false;
            if (UseDefaultStorage) // The default storage is used
            {
                TextBoxLogWriteLine("Starting the Azure export process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey), _credentials.ReturnStorageSuffix(), true);
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                IAccessPolicy readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                ILocator sourcelocator = _context.Locators.CreateLocator(LocatorType.Sas, SelectedFiles[0].Asset, readpolicy);

                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                Uri sourceUri = new Uri(sourcelocator.Path);
                CloudBlobContainer assetSourceContainer = cloudBlobClient.GetContainerReference(sourceUri.Segments[1]);

                // let's get cloudblobcontainer for target
                CloudBlobContainer TargetContainer = cloudBlobClient.GetContainerReference(containername); ;

                if (CreateNewContainer)
                {
                    try
                    {
                        TargetContainer.CreateIfNotExists();
                    }
                    catch (Exception ex)
                    {
                        DoGridTransferDeclareError(response.Id, string.Format("Failed to create container '{0}'. {1}", TargetContainer.Name, ex.Message));
                        Error = true;
                    }
                }

                if (!Error)
                {
                    Error = false;
                    CloudBlockBlob sourceCloudBlob, destinationBlob;
                    long Length = 0;
                    long BytesCopied = 0;
                    long percentComplete;

                    //calculate size
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        Length += file.ContentFileSize;
                    }

                    // do the copy
                    int nbblob = 0;
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        if (response.token.IsCancellationRequested) break;

                        nbblob++;
                        sourceCloudBlob = assetSourceContainer.GetBlockBlobReference(file.Name);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {
                            DoGridTransferUpdateProgress(100d * nbblob / SelectedFiles.Count, response.Id);
                            try
                            {
                                destinationBlob = TargetContainer.GetBlockBlobReference(file.Name);
                                destinationBlob.DeleteIfExists();
                                string stringOperation = await destinationBlob.StartCopyAsync(sourceCloudBlob, response.token);
                                bool Cancelled = false;

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    if (response.token.IsCancellationRequested && !Cancelled)
                                    {
                                        await destinationBlob.AbortCopyAsync(stringOperation);
                                        Cancelled = true;
                                    }
                                    blob.FetchAttributes();
                                    percentComplete = (long)100 * (long)(BytesCopied + blob.CopyState.BytesCopied) / (long)Length;
                                    DoGridTransferUpdateProgress((int)percentComplete, response.Id);
                                }

                                if (blob.CopyState.Status == CopyStatus.Failed)
                                {
                                    DoGridTransferDeclareError(response.Id, blob.CopyState.StatusDescription);
                                    Error = true;
                                    break;
                                }

                                if (blob.CopyState.Status == CopyStatus.Aborted)
                                {
                                    DoGridTransferDeclareCancelled(response.Id);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    DoGridTransferDeclareError(response.Id, "Error during blob copy.");
                                    Error = true;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                DoGridTransferDeclareError(response.Id, ex);
                                Error = true;
                            }
                            BytesCopied += sourceCloudBlob.Properties.Length;
                            percentComplete = (long)100 * (long)BytesCopied / (long)Length;
                            if (!Error) DoGridTransferUpdateProgress((int)percentComplete, response.Id);
                        }
                    }

                    sourcelocator.Delete();

                    if (!Error && !response.token.IsCancellationRequested)
                    {
                        DoGridTransferDeclareCompleted(response.Id, TargetContainer.Uri.AbsoluteUri);
                    }
                    DoRefreshGridAssetV(false);
                }
            }
            else // Another storage is used
            {
                TextBoxLogWriteLine("Starting the blob copy process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount SourceStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey), _credentials.ReturnStorageSuffix(), true);
                CloudStorageAccount TargetStorageAccount = new CloudStorageAccount(new StorageCredentials(otherstoragename, otherstoragekey), _credentials.ReturnStorageSuffix(), true);

                var SourceCloudBlobClient = SourceStorageAccount.CreateCloudBlobClient();
                var TargetCloudBlobClient = TargetStorageAccount.CreateCloudBlobClient();
                IAccessPolicy readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                ILocator sourcelocator = _context.Locators.CreateLocator(LocatorType.Sas, SelectedFiles[0].Asset, readpolicy);

                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                Uri sourceUri = new Uri(sourcelocator.Path);
                CloudBlobContainer assetSourceContainer = SourceCloudBlobClient.GetContainerReference(sourceUri.Segments[1]);

                // let's get cloudblobcontainer for target
                CloudBlobContainer TargetContainer = TargetCloudBlobClient.GetContainerReference(containername);

                // Get the SAS token to use for all blobs if dealing with multiple accounts
                string blobToken = assetSourceContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    // Specify the expiration time for the signature.
                    SharedAccessExpiryTime = DateTime.Now.AddDays(1),
                    // Specify the permissions granted by the signature.
                    Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read
                });
                if (CreateNewContainer)
                {
                    try
                    {
                        TargetContainer.CreateIfNotExists();
                    }
                    catch (Exception e)
                    {
                        TextBoxLogWriteLine("Failed to create container '{0}' ", TargetContainer.Name, true);
                        DoGridTransferDeclareError(response.Id, e);
                        Error = true;
                    }
                }

                if (!Error)
                {
                    CloudBlockBlob sourceCloudBlob, destinationBlob;
                    long Length = 0;
                    long BytesCopied = 0;
                    double percentComplete;
                    Error = false;

                    //calculate size
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        Length += file.ContentFileSize;
                    }

                    // do the copy
                    int nbblob = 0;
                    foreach (IAssetFile file in SelectedFiles)
                    {
                        if (response.token.IsCancellationRequested) break;

                        nbblob++;
                        sourceCloudBlob = assetSourceContainer.GetBlockBlobReference(file.Name);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {
                            DoGridTransferUpdateProgress(100d * nbblob / SelectedFiles.Count, response.Id);
                            try
                            {
                                destinationBlob = TargetContainer.GetBlockBlobReference(file.Name);
                                destinationBlob.DeleteIfExists();
                                string stringOperation = await destinationBlob.StartCopyAsync(new Uri(sourceCloudBlob.Uri.AbsoluteUri + blobToken), response.token);
                                bool Cancelled = false;

                                while (destinationBlob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();

                                    if (response.token.IsCancellationRequested && !Cancelled)
                                    {
                                        await destinationBlob.AbortCopyAsync(stringOperation);
                                        Cancelled = true;
                                    }

                                    destinationBlob.FetchAttributes();
                                    percentComplete = 100d * (long)(BytesCopied + destinationBlob.CopyState.BytesCopied) / Length;
                                    DoGridTransferUpdateProgress(percentComplete, response.Id);
                                }

                                if (destinationBlob.CopyState.Status == CopyStatus.Failed)
                                {
                                    DoGridTransferDeclareError(response.Id, destinationBlob.CopyState.StatusDescription);
                                    Error = true;
                                    break;
                                }

                                if (destinationBlob.CopyState.Status == CopyStatus.Aborted)
                                {
                                    DoGridTransferDeclareCancelled(response.Id);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    DoGridTransferDeclareError(response.Id, string.Format("Failed to copy file '{0}'", file.Name));
                                    Error = true;
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                DoGridTransferDeclareError(response.Id, e);
                                Error = true;
                            }

                            BytesCopied += sourceCloudBlob.Properties.Length;
                            percentComplete = 100d * BytesCopied / Length;
                            if (!Error) DoGridTransferUpdateProgress(percentComplete, response.Id);
                        }
                    }
                    sourcelocator.Delete();


                    if (!Error && !response.token.IsCancellationRequested)
                    {
                        DoGridTransferDeclareCompleted(response.Id, TargetContainer.Uri.AbsoluteUri);
                    }
                    DoRefreshGridAssetV(false);
                }
            }
        }
        */


        /*
        private void CheckListArchiveBlobs(Dictionary<string, string> storagekeys, IAsset SourceAsset, AssetInfo.ManifestSegmentsResponse manifestdata)
        {
            if (manifestdata.audioBitrates == null && manifestdata.videoBitrates.Count == 0 && manifestdata.audioSegments == null && manifestdata.videoSegments.Count == 0)
            {
                TextBoxLogWriteLine("Error. Impossible to get manifest data for '{0}'. Is a streaming endpoint with dynamic packaging running?", SourceAsset.Name, true);
                return;
            }
            if (storagekeys.ContainsKey(SourceAsset.StorageAccountName))
            {
                TextBoxLogWriteLine("Starting the integrity check for asset '{0}'.", SourceAsset.Name);
                bool Error = false;
                bool codeIssue = false;
                int nbErrorsAudioManifest = 0;
                int nbErrorsVideoManifest = 0;

                // Video segments in manifest
                TextBoxLogWriteLine("Checking video track segments in manifest...");
                int index = 0;
                foreach (var seg in manifestdata.videoSegments)
                {
                    if (seg.timestamp_mismatch)
                    {
                        if (nbErrorsVideoManifest < 10)
                        {
                            TextBoxLogWriteLine("Warning: Overlap or gap issue in video track. Timestamp {0} calculation mismatch in manifest, index {1}", seg.timestamp, index, true);
                            Error = true;
                        }
                        nbErrorsVideoManifest++;
                    }
                    index++;
                }
                if (nbErrorsVideoManifest >= 10)
                {
                    TextBoxLogWriteLine("Warning: Overlap or gap issue in video track. {0} more errors.", nbErrorsVideoManifest - 10, true);
                }

                // Audio segments in manifest
                TextBoxLogWriteLine("Checking audio track segments in manifest...");
                index = 0;
                int a_index = 0;
                foreach (var audiotrack in manifestdata.audioSegments)
                {
                    foreach (var seg in audiotrack)
                    {
                        if (seg.timestamp_mismatch)
                        {
                            if (nbErrorsAudioManifest < 10)
                            {
                                TextBoxLogWriteLine("Warning: Overlap or gap issue in audio track #{0} '{1}'. Timestamp {2} calculation mismatch in manifest, index {3}", a_index, manifestdata.audioName[a_index], seg.timestamp, index, true);
                                Error = true;
                            }
                            nbErrorsAudioManifest++;
                        }
                        index++;
                    }
                    if (nbErrorsAudioManifest >= 10)
                    {
                        TextBoxLogWriteLine("Warning: Overlap or gap issue in audio track #{0} '{1}'. {2} more errors.", a_index, manifestdata.audioName[a_index], nbErrorsAudioManifest - 10, true);
                    }
                    a_index++;
                }

                TextBoxLogWriteLine("Checking blobs in storage...");

                // let's get cloudblobcontainer for source
                CloudStorageAccount SourceCloudStorageAccount = new CloudStorageAccount(new StorageCredentials(SourceAsset.StorageAccountName, storagekeys[SourceAsset.StorageAccountName]), _credentials.ReturnStorageSuffix(), true);
                var SourceCloudBlobClient = SourceCloudStorageAccount.CreateCloudBlobClient();
                IAccessPolicy readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                ILocator SourceLocator = _context.Locators.CreateLocator(LocatorType.Sas, SourceAsset, readpolicy);

                try
                {
                    // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                    Uri sourceUri = new Uri(SourceLocator.Path);
                    CloudBlobContainer SourceCloudBlobContainer = SourceCloudBlobClient.GetContainerReference(sourceUri.Segments[1]);


                    List<CloudBlobDirectory> ListDirectories = new List<CloudBlobDirectory>();

                    var mediablobs = SourceCloudBlobContainer.ListBlobs();
                    if (mediablobs.ToList().Any(b => b.GetType() == typeof(CloudBlobDirectory))) // there are fragblobs
                    {
                        foreach (var blob in mediablobs)
                        {
                            if (blob.GetType() == typeof(CloudBlobDirectory))
                            {
                                CloudBlobDirectory blobdir = (CloudBlobDirectory)blob;
                                ListDirectories.Add(blobdir);
                                TextBoxLogWriteLine("Fragblobs detected (live archive) '{0}'.", blobdir.Prefix);
                            }
                        }

                        // let's check the presence of all audio_ and video_ directories
                        var audiodir = ListDirectories.Where(d => manifestdata.audioName.Any(an => d.Prefix.Contains(an))); //ListDirectories.Where(d => d.Prefix.StartsWith("audio"));
                                                                                                                            // var videodir = ListDirectories.Where(d => d.Prefix.StartsWith("video_")).Select(d => int.Parse(d.Prefix.Substring(6, d.Prefix.Length - 7)));
                        var videodir = ListDirectories.Where(d => d.Prefix.Contains(manifestdata.videoName)).Select(d => int.Parse(d.Prefix.Substring(manifestdata.videoName.Length + 1, d.Prefix.Length - manifestdata.videoName.Length - 2))); //ListDirectories.Where(d => d.Prefix.StartsWith("audio"));

                        if (videodir.Count() != manifestdata.videoBitrates.Count)
                        {
                            TextBoxLogWriteLine("Warning: {0} video tracks in the manifest but {1} video directories in storage", manifestdata.videoBitrates.Count(), videodir.Count(), true);
                            Error = true;
                        }

                        if (audiodir.Count() != manifestdata.audioBitrates.GetLength(0))
                        {
                            TextBoxLogWriteLine("Warning: {0} audio tracks in the manifest but {1} audio directories in storage", manifestdata.audioBitrates.GetLength(0), audiodir.Count(), true);
                            Error = true;
                        }

                        var except = videodir.Except(manifestdata.videoBitrates);
                        if (except.Count() > 0)
                        {
                            TextBoxLogWriteLine("Warning: Some video directories in storage are not referenced as bitrate in the manifest. Bitrates : {0}", string.Join(",", except), true);
                            Error = true;
                        }
                        var exceptb = manifestdata.videoBitrates.Except(videodir);
                        if (exceptb.Count() > 0)
                        {
                            TextBoxLogWriteLine("Issue: Some bitrates in manifest cannot be found in storage as video directories. Bitrates : {0}", string.Join(",", exceptb), true);
                            Error = true;
                        }


                        // let's check the fragblobs
                        foreach (var dir in ListDirectories)
                        {
                            if (manifestdata.audioName.Any(an => dir.Prefix.Contains(an)) || dir.Prefix.Contains(manifestdata.videoName))
                            {
                                TextBoxLogWriteLine("Checking fragblobs in directory '{0}'....", dir.Prefix);

                                BlobResultSegment blobResultSegment = dir.ListBlobsSegmented(null);
                                var listblobtimestampsTemp = blobResultSegment.Results.Select(b => b.Uri.LocalPath).ToList();


                                while (blobResultSegment.ContinuationToken != null)
                                {
                                    TextBoxLogWriteLine("Checking fragblobs in directory '{0}' ({1} segments retrieved...)", dir.Prefix, listblobtimestampsTemp.Count);
                                    blobResultSegment = dir.ListBlobsSegmented(blobResultSegment.ContinuationToken);
                                    listblobtimestampsTemp.AddRange(blobResultSegment.Results.Select(b => b.Uri.LocalPath));
                                }
                                TextBoxLogWriteLine("Checking fragblobs in directory '{0}' ({1} segments retrieved)", dir.Prefix, listblobtimestampsTemp.Count);

                                var listblobtimestamps = listblobtimestampsTemp.Where(b => System.IO.Path.GetFileName(b) != "header").Select(b => ulong.Parse(System.IO.Path.GetFileName(b))).OrderBy(t => t).ToList();

                                List<AssetInfo.ManifestSegmentData> manifestdatacurrenttrack;

                                if (dir.Prefix.Contains(manifestdata.videoName))//dir.Prefix.StartsWith("video_"))
                                {
                                    manifestdatacurrenttrack = manifestdata.videoSegments;
                                }
                                else // audio
                                {
                                    int i = 0;
                                    manifestdatacurrenttrack = manifestdata.audioSegments[0].ToList();
                                    foreach (var audiob in manifestdata.audioBitrates)
                                    {
                                        if (dir.Prefix.Equals(manifestdata.audioName[i] + "_" + audiob[0].ToString() + "/"))
                                        {
                                            manifestdatacurrenttrack = manifestdata.audioSegments[i].ToList();
                                            break;
                                        }
                                        i++;
                                    }

                                }

                                var timestampsinmanifest = manifestdatacurrenttrack.Select(a => a.timestamp).ToList();
                                var except2 = listblobtimestamps.Except(timestampsinmanifest);
                                const int maxSegDisplayed = 20;

                                if (except2.Count() > 0)
                                {
                                    int count = except2.Count();
                                    TextBoxLogWriteLine("Information: {0} segments in directory {1} are not in the manifest. This could occur if live is running. Segments with timestamp: {2}", count, dir.Prefix, string.Join(",", except2.Take(maxSegDisplayed)) + ((count > maxSegDisplayed) ? "..." : ""), true);
                                }

                                var except3 = timestampsinmanifest.Except(listblobtimestamps);
                                if (except3.Count() > 0)
                                {
                                    int count = except3.Count();
                                    TextBoxLogWriteLine("Issue: {0} segments in manifest are not in directory '{1}'. Segments with timestamp: {2}", count, dir.Prefix, string.Join(",", except3.Take(maxSegDisplayed)) + ((count > maxSegDisplayed) ? "..." : ""), true);
                                    Error = true;
                                }

                                if (listblobtimestamps.Count < manifestdatacurrenttrack.Count) // mising blob in storage (header file)
                                {
                                    TextBoxLogWriteLine("Issue: {0} segments in the manifest but only {1} segments in directory '{2}'", manifestdatacurrenttrack.Count, listblobtimestamps.Count, dir.Prefix, true);
                                    Error = true;
                                }
                                else if (manifestdatacurrenttrack.Count > 0)
                                {
                                    index = 0;

                                    // list timestamps from blob
                                    ulong timestampinblob;
                                    foreach (var seg in manifestdatacurrenttrack)
                                    {
                                        timestampinblob = listblobtimestamps[index];
                                        if (timestampinblob != seg.timestamp && !seg.calculated)
                                        {
                                            TextBoxLogWriteLine("Issue: Timestamp {0} in blob is different from defined timestamp {1} in manifest, in directory '{2}', index {3}", timestampinblob, seg.timestamp, dir.Prefix, index, true);
                                            Error = true;
                                            break;
                                        }
                                        else if (timestampinblob != seg.timestamp && seg.calculated)
                                        {
                                            TextBoxLogWriteLine("Issue: Timestamp {0} in blob is different from calculated timestamp {1} in manifest, in directory '{2}', index {3}", timestampinblob, seg.timestamp, dir.Prefix, index, true);
                                            Error = true;
                                            break;
                                        }
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when analyzing the archive.", true);
                    TextBoxLogWriteLine(ex);
                    codeIssue = true;
                }

                try
                {
                    SourceLocator.Delete();
                    readpolicy.Delete();
                }

                catch
                {

                }


                if (codeIssue)
                {
                    TextBoxLogWriteLine("End of integrity check for asset '{0}'. Code fails.", SourceAsset.Name);
                }
                else if (Error)
                {
                    TextBoxLogWriteLine("End of integrity check for asset '{0}'. Error(s) detected.", SourceAsset.Name);
                }
                else
                {
                    TextBoxLogWriteLine("End of integrity check for asset '{0}'. No error detected.", SourceAsset.Name);
                }
            }
            else
            {
                TextBoxLogWriteLine("Error storage key not found for asset '{0}'.", SourceAsset.Name, true);
            }
        }
        */


        private void allJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAllJobs();
        }

        private void selectedJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteSelectedJobs();
        }

        private void DoDeleteSelectedJobs()
        {
            DoDeleteJobs(dataGridViewJobsV.ReturnSelectedJobs());
        }

        private void DoDeleteJobs(List<JobExtension> SelectedJobs)
        {
            if (SelectedJobs.Count > 0)
            {
                string question = (SelectedJobs.Count == 1) ? "Delete " + SelectedJobs[0].Job.Name + " ?" : "Delete these " + SelectedJobs.Count + " jobs ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    Task.Run(() =>
                    {
                        bool Error = false;
                        Task[] deleteTasks = SelectedJobs.ToList().Select(j => _amsClientV3.AMSclient.Jobs.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, j.TransformName, j.Job.Name)).ToArray();
                        TextBoxLogWriteLine("Deleting job(s)");
                        try
                        {
                            Task.WaitAll(deleteTasks);
                        }
                        catch (Exception ex)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when deleting the job(s)", true);
                            TextBoxLogWriteLine(ex);
                            Error = true;
                        }
                        if (!Error)
                        {
                            TextBoxLogWriteLine("Job(s) deleted.");
                        }

                        DoRefreshGridJobV(false);
                    }
           );
                }
            }
        }


        private void DoDeleteAllJobs()
        {
            if (dataGridViewTransformsV.ReturnSelectedTransforms().Count > 1)
            {
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete ALL the jobs from the selected transform?", "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                Task.Run(() =>
                {
                    bool Error = false;


                    // let's build the tasks list
                    TextBoxLogWriteLine("Listing the jobs...");
                    List<Task> deleteTasks = new List<Task>();

                    //   foreach (var transform in dataGridViewTransformsV.ReturnSelectedTransforms())
                    {
                        Transform transform = dataGridViewTransformsV.ReturnSelectedTransforms().First();
                        Microsoft.Rest.Azure.IPage<Job> listjobs = _amsClientV3.AMSclient.Jobs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transform.Name);
                        deleteTasks.AddRange(listjobs.ToList().Select(j => _amsClientV3.AMSclient.Jobs.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transform.Name, j.Name)));
                    }

                    TextBoxLogWriteLine(string.Format("Deleting {0} job(s)", deleteTasks.Count));
                    try
                    {
                        Task.WaitAll(deleteTasks.ToArray());
                    }
                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when deleting the job(s)", true);
                        TextBoxLogWriteLine(ex);
                        Error = true;
                    }

                    if (!Error)
                    {
                        TextBoxLogWriteLine("Job(s) deleted.");
                    }

                    DoRefreshGridJobV(false);
                }
          );

            }
        }



        private void DoCancelAllJobs()
        {
            if (dataGridViewTransformsV.ReturnSelectedTransforms().Count > 1)
            {
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to cancel ALL the jobs from the selected transform ?", "Job cancelation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                Task.Run(() =>
                {
                    bool Error = false;

                    // let's build the tasks list
                    TextBoxLogWriteLine("Listing the jobs...");
                    List<Task> deleteTasks = new List<Task>();

                    //  foreach (var transform in dataGridViewTransformsV.ReturnSelectedTransforms())
                    {
                        Transform transform = dataGridViewTransformsV.ReturnSelectedTransforms().First();
                        Microsoft.Rest.Azure.IPage<Job> listjobs = _amsClientV3.AMSclient.Jobs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transform.Name);
                        deleteTasks.AddRange(listjobs.ToList()
                            .Where(j => j.State == Microsoft.Azure.Management.Media.Models.JobState.Processing || j.State == Microsoft.Azure.Management.Media.Models.JobState.Queued || j.State == Microsoft.Azure.Management.Media.Models.JobState.Scheduled)
                            .Select(j => _amsClientV3.AMSclient.Jobs.CancelJobAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transform.Name, j.Name)));
                    }

                    TextBoxLogWriteLine(string.Format("Canceling {0} job(s)", deleteTasks.Count));
                    try
                    {
                        Task.WaitAll(deleteTasks.ToArray());
                    }
                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when canceling the job(s)", true);
                        TextBoxLogWriteLine(ex);
                        Error = true;
                    }

                    if (!Error)
                    {
                        TextBoxLogWriteLine("Job(s) canceled.");
                    }

                    DoRefreshGridJobV(false);
                }
        );
            }
        }

        private void DoDeleteTransforms(List<Transform> SelectedTransforms)
        {
            if (SelectedTransforms.Count > 0)
            {
                string question = (SelectedTransforms.Count == 1) ? "Delete " + SelectedTransforms[0].Name + " ?" : "Delete these " + SelectedTransforms.Count + " transforms ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Transform deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    Task.Run(() =>
                    {
                        bool Error = false;
                        Task[] deleteTasks = SelectedTransforms.ToList().Select(t => _amsClientV3.AMSclient.Transforms.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, t.Name)).ToArray();
                        TextBoxLogWriteLine("Deleting transform(s)");
                        try
                        {
                            Task.WaitAll(deleteTasks);
                        }
                        catch (Exception ex)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when deleting the transform(s)", true);
                            TextBoxLogWriteLine(ex);
                            Error = true;
                        }
                        if (!Error)
                        {
                            TextBoxLogWriteLine("Transform(s) deleted.");
                        }

                        DoRefreshGridTransformV(false);
                    }
           );
                }
            }
        }


        private void dASHIFHTML5ReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerDASHIFList);
        }

        private void iVXHLSPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.Player3IVXHLS);
        }


        private void Mainform_Shown(object sender, EventArgs e)
        {
            // display the update message if a new version is available
            if (!string.IsNullOrEmpty(Program.MessageNewVersion))
            {
                TextBoxLogWriteLine(Program.MessageNewVersion);
            }
        }



        /*      
        private static void CheckAssetSizeRegardingMediaUnit(List<IAsset> SelectedAssets, bool Indexer = false)
        {
           bool Warning = false;

           // let's find the limit
           var unitype = SelectedAssets.FirstOrDefault().GetMediaContext().EncodingReservedUnits.FirstOrDefault().ReservedUnitType;
           long limit = S1AssetSizeLimit * OneGB;
           string unitname = "S1";

           if (!Indexer)
           {
               if (unitype == ReservedUnitType.Standard)
               {
                   limit = S2AssetSizeLimit * OneGB;
                   unitname = "S2";
               }
               else if (unitype == ReservedUnitType.Premium)
               {
                   limit = S3AssetSizeLimit * OneGB;
                   unitname = "S3";
               }
           }

           foreach (var asset in SelectedAssets)
           {
               if (AssetInfo.GetSize(asset) >= limit)
               {
                   Warning = true;
               }
           }

           if (Warning)
           {
               if (!Indexer)
               {
                   MessageBox.Show(string.Format("You are using {0} media unit(s).\nAt least one of the source assets has a size over {1}.\n\nLimits are :\n{2} GB with S1 media unit\n{3} GB with S2 media unit\n{4} GB with S3 media unit", unitname, AssetInfo.FormatByteSize(limit), S1AssetSizeLimit, S2AssetSizeLimit, S3AssetSizeLimit), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }
               else
               {
                   MessageBox.Show(string.Format("At least one of the source assets has a size over {0}, which is the maximum supported by Indexer.", AssetInfo.FormatByteSize(limit)), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }

           }
        }
        */



        private void azureMediaServicesPlayerPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerAMP);
        }

        private void hTML5VideoElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerInfoHTML5Video);
        }


        private void Mainform_Load(object sender, EventArgs e)
        {
            Hide();

            linkLabelFeedbackAMS.Links.Add(new LinkLabel.Link(0, linkLabelFeedbackAMS.Text.Length, Constants.LinkFeedbackAMS));
            //linkLabelMoreInfoMediaUnits.Links.Add(new LinkLabel.Link(0, linkLabelMoreInfoMediaUnits.Text.Length, Constants.LinkInfoMediaUnit));

            //comboBoxOrderJobs.Enabled = _context.Jobs.Count() < triggerForLargeAccountNbJobs;

            toolStripStatusLabelWatchFolder.Visible = false;

            comboBoxSearchAssetOption.Items.Add(new Item("Asset name (equals) :", SearchIn.AssetNameEquals.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Asset name (starts with) :", SearchIn.AssetNameStartsWith.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Asset name (greater than) :", SearchIn.AssetNameGreaterThan.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Asset name (less than) :", SearchIn.AssetNameLessThan.ToString()));

            comboBoxSearchAssetOption.Items.Add(new Item("Asset Id (equals) :", SearchIn.AssetId.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Asset alt Id (equals) :", SearchIn.AssetAltId.ToString()));
            comboBoxSearchAssetOption.SelectedIndex = 0;

            comboBoxSearchJobOption.Items.Add(new Item("Search in job name :", SearchIn.JobName.ToString()));
            comboBoxSearchJobOption.Items.Add(new Item("Search for job Id :", SearchIn.JobId.ToString()));
            comboBoxSearchJobOption.SelectedIndex = 0;

            comboBoxSearchLiveEventOption.Items.Add(new Item("Search in live event name :", SearchIn.LiveEventName.ToString()));
            comboBoxSearchLiveEventOption.Items.Add(new Item("Search for live event Id :", SearchIn.LiveEventId.ToString()));
            comboBoxSearchLiveEventOption.SelectedIndex = 0;

            comboBoxSearchProgramOption.Items.Add(new Item("Search in live output name :", SearchIn.LiveOutputName.ToString()));
            comboBoxSearchProgramOption.Items.Add(new Item("Search for live output Id :", SearchIn.LiveOutputId.ToString()));
            comboBoxSearchProgramOption.SelectedIndex = 0;

            comboBoxOrderAssets.Items.AddRange(
           typeof(OrderAssets)
           .GetFields()
           .Select(i => i.GetValue(null) as string)
           .ToArray()
           );
            comboBoxOrderAssets.SelectedIndex = 0;

            comboBoxOrderJobs.Items.AddRange(
            typeof(OrderJobs)
            .GetFields()
            .Select(i => i.GetValue(null) as string)
            .ToArray()
            );
            comboBoxOrderJobs.SelectedIndex = 0;

            comboBoxStateJobs.Items.Add("All");
            comboBoxStateJobs.Items.AddRange(
            typeof(Microsoft.Azure.Management.Media.Models.JobState)
            .GetFields()
            .Select(i => i.Name as string)
            .ToArray()
            );
            comboBoxStateJobs.Items[0] = "All";
            comboBoxStateJobs.SelectedIndex = 0;

            comboBoxFilterAssetsTime.Items.AddRange(
                 typeof(FilterTime)
                 .GetFields()
                 .Select(i => i.GetValue(null) as string)
                 .ToArray()
                 );
            comboBoxFilterAssetsTime.SelectedIndex = 0; // last 50 items

            comboBoxFilterJobsTime.Items.AddRange(
                 typeof(FilterTime)
                 .GetFields()
                 .Select(i => i.GetValue(null) as string)
                 .ToArray()
                 );
            comboBoxFilterJobsTime.SelectedIndex = 0; // last 50 items

            comboBoxFilterTimeProgram.Items.AddRange(
                typeof(FilterTime)
                .GetFields()
                .Select(i => i.GetValue(null) as string)
                .ToArray()
                );
            comboBoxFilterTimeProgram.SelectedIndex = 0;


            comboBoxFilterTimeLiveEvent.Items.AddRange(
                typeof(FilterTime)
                .GetFields()
                .Select(i => i.GetValue(null) as string)
                .ToArray()
                );
            comboBoxFilterTimeLiveEvent.SelectedIndex = 0;


            comboBoxStatusProgram.Items.AddRange(
                typeof(LiveOutputResourceState)
                .GetFields()
                .Select(i => i.Name as string)
                .ToArray()
                );
            comboBoxStatusProgram.Items[0] = "All";
            comboBoxStatusProgram.SelectedIndex = 0;

            comboBoxStatusLiveEvent.Items.AddRange(
              typeof(LiveEventResourceState)
              .GetFields()
              .Select(i => i.Name as string)
              .ToArray()
              );
            comboBoxStatusLiveEvent.Items[0] = "All";
            comboBoxStatusLiveEvent.SelectedIndex = 0;

            AddButtonsToSearchTextBox();

            // List of state and numbers of jobs per state

            DoRefreshGridTransformV(true);
            DoRefreshGridJobV(true);
            DoGridTransferInit();
            DoRefreshGridAssetV(true);
            DoRefreshGridLiveEventV(true);
            DoRefreshGridLiveOutputV(true);
            DoRefreshGridStreamingEndpointV(true);
            DoRefreshGridStorageV(true);
            DoRefreshGridFiltersV(true);

            DisplaySplashDuringLoading = false;

            UpdateLabelConcurrentTransfers();

            // making sure the visible assets are analyzed
            dataGridViewAssetsV.ReLaunchAnalyze();

            Show();
        }

        private void AddButtonsToSearchTextBox()
        {
            // let's add a button to asset textbox search
            Button btna = new Button
            {
                Size = new Size(18, textBoxAssetSearch.ClientSize.Height + 2),
            };
            btna.Anchor = AnchorStyles.Right;
            btna.Cursor = Cursors.Default;
            btna.Text = "X";
            btna.BackColor = SystemColors.Window;
            btna.Location = new Point(textBoxAssetSearch.ClientSize.Width - btna.Width, -1);
            btna.Click += Btna_Click;
            textBoxAssetSearch.Controls.Add(btna);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(textBoxAssetSearch.Handle, 0xd3, (IntPtr)2, (IntPtr)(btna.Width << 16));

            // let's add a button to job textbox search
            Button btnj = new Button
            {
                Size = new Size(18, textBoxJobSearch.ClientSize.Height + 2)
            };
            btnj.Location = new Point(textBoxJobSearch.ClientSize.Width - btnj.Width, -1);
            btnj.Anchor = AnchorStyles.Right;
            btnj.Cursor = Cursors.Default;
            btnj.Text = "X";
            btnj.BackColor = SystemColors.Window;
            btnj.Click += Btnj_Click;
            textBoxJobSearch.Controls.Add(btnj);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(textBoxJobSearch.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnj.Width << 16));

            // let's add a button to live event textbox search
            Button btnc = new Button
            {
                Size = new Size(18, textBoxSearchNameLiveEvent.ClientSize.Height + 2)
            };
            btnc.Location = new Point(textBoxSearchNameLiveEvent.ClientSize.Width - btnc.Width, -1);
            btnc.Anchor = AnchorStyles.Right;
            btnc.Cursor = Cursors.Default;
            btnc.Text = "X";
            btnc.BackColor = SystemColors.Window;
            btnc.Click += Btnc_Click;
            textBoxSearchNameLiveEvent.Controls.Add(btnc);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(textBoxSearchNameLiveEvent.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnc.Width << 16));

            // let's add a button to program textbox search
            Button btnp = new Button
            {
                Size = new Size(18, textBoxSearchNameProgram.ClientSize.Height + 2)
            };
            btnp.Location = new Point(textBoxSearchNameProgram.ClientSize.Width - btnp.Width, -1);
            btnp.Anchor = AnchorStyles.Right;
            btnp.Cursor = Cursors.Default;
            btnp.Text = "X";
            btnp.BackColor = SystemColors.Window;
            btnp.Click += Btnp_Click;
            textBoxSearchNameProgram.Controls.Add(btnp);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(textBoxSearchNameProgram.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnp.Width << 16));
        }

        private void Btna_Click(object sender, EventArgs e)
        {
            textBoxAssetSearch.Text = string.Empty;
            DoAssetSearch();
        }
        private void Btnj_Click(object sender, EventArgs e)
        {
            textBoxJobSearch.Text = string.Empty;
            DoJobSearch();
        }
        private void Btnc_Click(object sender, EventArgs e)
        {
            textBoxSearchNameLiveEvent.Text = string.Empty;
            DoLiveEventSearch();
        }
        private void Btnp_Click(object sender, EventArgs e)
        {
            textBoxSearchNameProgram.Text = string.Empty;
            DoProgramSearch();
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);



        private int GetTextBoxAssetsPageNumber()
        {
            return int.Parse(textBoxAssetsPageNumber.Text);
        }
        private void SetTextBoxAssetsPageNumber(int number)
        {
            textBoxAssetsPageNumber.Text = number.ToString();
        }

        private int GetTextBoxJobsPageNumber()
        {
            return int.Parse(textBoxJobsPageNumber.Text);
        }
        private void SetTextBoxJobsPageNumber(int number)
        {
            textBoxJobsPageNumber.Text = number.ToString();
        }

        private void butNextPageAsset_Click(object sender, EventArgs e)
        {
            int page = GetTextBoxAssetsPageNumber() + 1;
            Task.Run(async () =>
            {
                await dataGridViewAssetsV.RefreshAssetsAsync(page);
            });
            if (!dataGridViewAssetsV.CurrentPageIsMax)
            {
                SetTextBoxAssetsPageNumber(page);
            }
        }

        private void butPrevPageAsset_Click(object sender, EventArgs e)
        {
            if (GetTextBoxAssetsPageNumber() > 1)
            {
                int page = GetTextBoxAssetsPageNumber() - 1;
                Task.Run(async () =>
                {
                    await dataGridViewAssetsV.RefreshAssetsAsync(page);
                });

                SetTextBoxAssetsPageNumber(page);
            }
        }

        private void butNextPageJob_Click(object sender, EventArgs e)
        {
            int page = GetTextBoxJobsPageNumber() + 1;
            Task.Run(async () =>
            {
                await dataGridViewJobsV.RefreshjobsAsync(page);
            });
            if (!dataGridViewJobsV.CurrentPageIsMax)
            {
                SetTextBoxJobsPageNumber(page);
            }
        }

        private void butPrevPageJob_Click(object sender, EventArgs e)
        {
            if (GetTextBoxJobsPageNumber() > 1)
            {
                int page = GetTextBoxJobsPageNumber() - 1;
                Task.Run(async () =>
                {
                    await dataGridViewJobsV.RefreshjobsAsync(page);
                });

                SetTextBoxJobsPageNumber(page);
            }
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            int TransferUncompleted = _MyListTransfer.Where(t => (t.State == TransferState.Processing) || (t.State == TransferState.Queued)).Count();
            if (TransferUncompleted > 0)
            {
                if (System.Windows.Forms.MessageBox.Show("One or several transfers are in the queue or in progress and will be interrupted." + Constants.endline + "Are you sure that you want to quit the application?", "Caution: transfer(s) in progress", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            if (e.Cancel == false)
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Dispose();
            }
        }


        private async void dataGridViewAssetsV_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                await _amsClientV3.RefreshTokenIfNeededAsync();
                Asset asset = await _amsClientV3.AMSclient.Assets.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV.Columns["Name"].Index].Value.ToString());
                DisplayInfo(asset);
            }
        }

        private void comboBoxOrderAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewAssetsV.OrderAssetsInGrid = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewAssetsV.Initialized)
            {
                DoRefreshGridAssetV(false);
            }
        }

        private void dataGridViewJobsV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < dataGridViewJobsV.Columns["Progress"].Index)
            {
                object celljobstatevalue = dataGridViewJobsV.Rows[e.RowIndex].Cells[dataGridViewJobsV.Columns["State"].Index].Value;

                if (celljobstatevalue != null)
                {
                    JobState JS = (Microsoft.Azure.Management.Media.Models.JobState)celljobstatevalue;
                    Color mycolor;

                    //switch (JS)
                    //{
                    //    case Microsoft.Azure.Management.Media.Models.JobState.Error:
                    //        mycolor = Color.Red;
                    //        break;
                    //    case Microsoft.Azure.Management.Media.Models.JobState.Canceled:
                    //        mycolor = Color.Blue;
                    //        break;
                    //    case Microsoft.Azure.Management.Media.Models.JobState.Canceling:
                    //        mycolor = Color.Blue;
                    //        break;
                    //    case Microsoft.Azure.Management.Media.Models.JobState.Processing:
                    //        mycolor = Color.DarkGreen;
                    //        break;
                    //    case Microsoft.Azure.Management.Media.Models.JobState.Queued:
                    //        mycolor = Color.Green;
                    //        break;
                    //    default:
                    //        mycolor = Color.Black;
                    //        break;
                    //}

                    if (JS == Microsoft.Azure.Management.Media.Models.JobState.Error)
                    {
                        mycolor = Color.Red;
                    }
                    else if (JS == Microsoft.Azure.Management.Media.Models.JobState.Canceled)
                    {
                        mycolor = Color.Blue;
                    }
                    else if (JS == Microsoft.Azure.Management.Media.Models.JobState.Canceling)
                    {
                        mycolor = Color.Blue;
                    }
                    else if (JS == Microsoft.Azure.Management.Media.Models.JobState.Processing)
                    {
                        mycolor = Color.DarkGreen;
                    }
                    else if (JS == Microsoft.Azure.Management.Media.Models.JobState.Queued)
                    {
                        mycolor = Color.Green;
                    }
                    else
                    {
                        mycolor = Color.Black;
                    }

                    e.CellStyle.ForeColor = mycolor;
                }
            }
        }

        private void dataGridViewJobsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dataGridViewJobsV.Rows[e.RowIndex];
                Job job = Task.Run(async () => await GetJobAsync(row.Cells[dataGridViewJobsV.Columns["TransformName"].Index].Value.ToString(), row.Cells[dataGridViewJobsV.Columns["Name"].Index].Value.ToString())).Result;

                JobExtension jobExt = new JobExtension()
                {
                    Job = job,
                    TransformName = row.Cells[dataGridViewJobsV.Columns["TransformName"].Index].Value.ToString()
                };

                if (job != null)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        if (DisplayInfo(jobExt) == DialogResult.OK)
                        {
                        }
                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                    }
                }
            }
        }

        private void comboBoxOrderJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewJobsV.Initialized)
            {
                Debug.WriteLine("comboBoxOrderJobs_SelectedIndexChanged");
                dataGridViewJobsV.OrderJobsInGrid = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridJobV(false);
            }
        }

        private void dataGridViewAssetsV_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int indextype = dataGridViewAssetsV.Columns["Type"].Index;//2
            int indexsize = dataGridViewAssetsV.Columns["Size"].Index;//4
            int indexlocalexp = dataGridViewAssetsV.Columns[dataGridViewAssetsV._locatorexpirationdate].Index; //13
            int indexassetwarning = dataGridViewAssetsV.Columns[dataGridViewAssetsV._assetwarning].Index;

            /*

            var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[indextype];  // Type cell
            if (cell.Value != null)
            {
                string TypeStr = (string)cell.Value;
                if (TypeStr.Equals(AssetInfo.Type_Empty)) e.CellStyle.ForeColor = Color.Red;
                else if (TypeStr.Contains(AssetInfo.Type_Workflow)) e.CellStyle.ForeColor = Color.Blue;
            }

            var cell2 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[indexsize];  //Size
            if (cell2.Value != null)
            {
                string TypeStr = (string)cell2.Value;
                if (TypeStr.Equals("0 B")) e.CellStyle.ForeColor = Color.Red;
            }

            */
            DataGridViewCell cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[indextype];  // Type cell
            if (cell.Value != null)
            {
                string TypeStr = (string)cell.Value;
                if (TypeStr.Contains(AssetInfo.Type_Workflow))
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }

            DataGridViewCell cell1 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[indexassetwarning];  // warning
            if (cell1.Value != null)
            {
                bool warning = (bool)cell1.Value;
                if (warning)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }


            if (e.ColumnIndex == indexlocalexp)  // locator expiration,
            {
                object value = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._locatorexpirationdatewarning].Value;
                if (value != null && (((bool)value) == true))
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            else if (e.ColumnIndex == indexlocalexp)  // locator expiration,
            {
                object value = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._locatorexpirationdatewarning].Value;
                if (value != null && (((bool)value) == true))
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._dynEnc].Index)// Mouseover for icons
            {
                DataGridViewCell cell4 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value != null)
                {
                    cell4.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value.ToString();
                }
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._publication].Index)// Mouseover for icons
            {
                DataGridViewCell cell5 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value != null)
                {
                    cell5.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value.ToString();
                }
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._filter].Index)// Mouseover for icon filter
            {
                DataGridViewCell cell6 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._filterMouseOver].Value != null)
                {
                    cell6.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._filterMouseOver].Value.ToString();
                }
            }

        }

        private void toolStripMenuItemDisplayInfo_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedAssetsV3().FirstOrDefault());
        }

        private void contextMenuStripAssets_Opening(object sender, CancelEventArgs e)
        {
            List<Asset> assets = ReturnSelectedAssetsV3();
            bool singleitem = (assets.Count == 1);
            Asset firstAsset = assets.FirstOrDefault();

            ContextMenuItemAssetDisplayInfo.Enabled =
            ContextMenuItemAssetEditDescription.Enabled =
            editAlternateIdToolStripMenuItem.Enabled =
            contextMenuExportFilesToStorage.Enabled =
            createAnAssetFilterToolStripMenuItem.Enabled = singleitem;

            /*
            if (singleitem && firstAsset != null && firstAsset.AssetFiles.Count() == 1)
            {
                var assetfile = firstAsset.AssetFiles.FirstOrDefault();
                if (assetfile != null && assetfile.Name.EndsWith(".ism") && assetfile.ContentFileSize == 0)
                {
                    // live archive
                    contextMenuExportFilesToStorage.Enabled = false;
                    toolStripMenuItemDownloadToLocal.Enabled = false;
                }
            }
            */
        }


        private void toolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            DoMenuChangeAssetDescription();
        }


        private void toolStripMenuAsset_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void toolStripMenuJobDisplayInfo_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedJobsV3().FirstOrDefault());
        }

        private void contextMenuStripJobs_Opening(object sender, CancelEventArgs e)
        {
            bool singleitem = (ReturnSelectedJobsV3().Count == 1);
            ContextMenuItemJobDisplayInfo.Enabled = singleitem;
        }

        private void richTextBoxLog_TextChanged(object sender, EventArgs e)
        {
            // we want to scroll down the textBox
            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
            richTextBoxLog.ScrollToCaret();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBoxStateJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewJobsV.Initialized)
            {
                Debug.WriteLine("comboBoxStateJobs_SelectedIndexChanged");
                const string p = "  (";
                string filter = ((ComboBox)sender).SelectedItem.ToString();
                if (filter.Contains(p))
                {
                    filter = filter.Substring(0, filter.IndexOf(p));
                }

                dataGridViewJobsV.FilterJobsState = filter;
                DoRefreshGridJobV(false);
            }
        }


        private void DoDisplayJobReport()
        {
            /*
            JobInfo JR = new JobInfo(ReturnSelectedJobs(), _accountname);
            StringBuilder SB = JR.GetStats();
            var tokenDisplayForm = new EditorXMLJSON("Job report", SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
            */
        }


        private void dataGridViewTransfer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == dataGridViewTransfer.Columns["State"].Index) // state column
            {
                if (dataGridViewTransfer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {

                    TransferState JS = (TransferState)dataGridViewTransfer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (JS)
                    {
                        case TransferState.Error:
                            mycolor = Color.Red;
                            break;

                        case TransferState.Processing:
                            mycolor = Color.DarkGreen;
                            break;

                        case TransferState.Queued:
                            mycolor = Color.Green;
                            break;

                        case TransferState.Cancelled:
                            mycolor = Color.Blue;
                            break;

                        default:
                            mycolor = Color.Black;
                            break;

                    }
                    for (int i = 0; i < dataGridViewTransfer.Columns.Count; i++)
                    {
                        dataGridViewTransfer.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;
                    }
                }
            }
        }


        private void buttonJobSearch_Click(object sender, EventArgs e)
        {
            DoJobSearch();
        }

        private void buttonAssetSearch_Click(object sender, EventArgs e)
        {
            DoAssetSearch();
        }

        private void DoAssetSearch()
        {
            if (dataGridViewAssetsV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchAssetOption.SelectedItem as Item).Value);
                dataGridViewAssetsV.SearchInName = new SearchObject { Text = textBoxAssetSearch.Text, SearchType = stype };
                DoRefreshGridAssetV(false);
            }
        }

        private void DoJobSearch()
        {
            if (dataGridViewJobsV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchJobOption.SelectedItem as Item).Value);
                dataGridViewJobsV.SearchInName = new SearchObject { Text = textBoxJobSearch.Text, SearchType = stype };
                DoRefreshGridJobV(false);
            }
        }


        private void toolStripMenuItemOpenDest_Click(object sender, EventArgs e)
        {
            DoOpenTransferDestLocation();
        }

        private async void DoOpenTransferDestLocation()
        {
            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                if ((TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value == TransferState.Finished)
                {
                    string location = dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["DestLocation"].Index].Value.ToString();

                    switch ((TransferType)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["Type"].Index].Value)
                    {
                        case TransferType.DownloadToLocal:
                            if (!string.IsNullOrEmpty(location) && location != null)
                            {
                                Process.Start(location);
                            }

                            break;

                        case TransferType.ImportFromAzureStorage:
                        case TransferType.ImportFromHttp:
                        case TransferType.UploadFromFile:
                        case TransferType.UploadFromFolder:
                        case TransferType.UploadWithExternalTool:
                            await _amsClientV3.RefreshTokenIfNeededAsync();
                            Asset asset = await _amsClientV3.AMSclient.Assets.GetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, location);
                            if (asset != null)
                            {
                                DisplayInfo(asset);
                            }

                            break;

                        case TransferType.ExportToAzureStorage:
                        default:
                            break;

                    }
                }
            }
        }

        private void DoDisplayTransferError()
        {
            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                if ((TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value == TransferState.Error)
                {
                    string ErrorMessage = dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["ErrorDescription"].Index].Value.ToString();
                    MessageBox.Show(ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void openDestinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenTransferDestLocation();
        }

        private void dataGridViewTransfer_DoubleClick(object sender, EventArgs e)
        {
            DoOpenTransferDestLocation();
        }

        private void contextMenuStripTransfers_Opening(object sender, CancelEventArgs e)
        {
            ToolStrip toolStripMenuItemOpenDest = (ToolStrip)sender;

            bool bFinished = false;
            bool bCancel = false;

            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                TransferState status = (TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value;

                if (status == TransferState.Finished)
                {
                    bFinished = true;

                }
                else if (status == TransferState.Processing || status == TransferState.Queued)
                {
                    bCancel = true;
                }
            }

            ContextMenuItemTransferOpenDest.Enabled = displayErrorToolStripMenuItem.Enabled = bFinished;
            cancelToolStripMenuItem.Enabled = bCancel;

        }

        private void DoChangeJobPriority()
        {
            List<JobExtension> SelectedJobs = ReturnSelectedJobsV3();

            if (SelectedJobs.Count > 0)
            {
                PriorityForm form = new PriorityForm()
                {
                    JobPriority = (SelectedJobs.Count == 1) ? SelectedJobs[0].Job.Priority : Priority.Normal // if only one job so we pass the current priority to dialog box
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    foreach (JobExtension JobToProcess in SelectedJobs)
                    {
                        if (JobToProcess != null)
                        {
                            //delete
                            TextBoxLogWriteLine(string.Format("Changing priority to {0} for job '{1}'.", form.JobPriority, JobToProcess.Job.Name));
                            try
                            {
                                JobToProcess.Job.Priority = form.JobPriority;
                                _amsClientV3.RefreshTokenIfNeeded();
                                _amsClientV3.AMSclient.Jobs.Update(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, JobToProcess.TransformName, JobToProcess.Job.Name, JobToProcess.Job);
                                TextBoxLogWriteLine(string.Format("Job '{0}' updated.", JobToProcess.Job.Name));
                            }

                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when changing priority for {0}.", JobToProcess.Job.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                    }
                }
            }
        }

        private void changePriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoChangeJobPriority();
        }

        private void comboBoxFilterTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewAssetsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewAssetsV.TimeFilter == FilterTime.TimeRange)
            {
                TimeRangeSelection form = new TimeRangeSelection()
                {
                    TimeRange = dataGridViewAssetsV.TimeFilterTimeRange,
                    LabelMain = "Created Time Range of Assets"
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewAssetsV.TimeFilterTimeRange = form.TimeRange;
                }
                else
                {
                    // user cancelled timerange box TODO
                }
            }

            if (dataGridViewAssetsV.Initialized)
            {
                DoRefreshGridAssetV(false);
            }
        }

        private void comboBoxFilterJobsTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewJobsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewJobsV.TimeFilter == FilterTime.TimeRange)
            {
                TimeRangeSelection form = new TimeRangeSelection()
                {
                    TimeRange = dataGridViewJobsV.TimeFilterTimeRange,
                    LabelMain = "Last Modified Time Range of Jobs"
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewJobsV.TimeFilterTimeRange = form.TimeRange;
                }
                else
                {
                    // user cancelled timerange box TODO
                }
            }

            if (dataGridViewJobsV.Initialized)
            {
                DoRefreshGridJobV(false);
            }
        }


        private bool IsThereALocatorValid(Asset asset, ref AssetStreamingLocator locator, AMSClientV3 client)
        {

            bool valid = false;
            client.RefreshTokenIfNeeded();
            IList<AssetStreamingLocator> locators = client.AMSclient.Assets.ListStreamingLocators(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName, asset.Name).StreamingLocators;
            if (asset != null && locators.Count > 0)
            {
                AssetStreamingLocator LocatorQuery = locators.Where(l => ((l.StartTime < DateTime.UtcNow) || (l.StartTime == null)) && (l.EndTime > DateTime.UtcNow)).FirstOrDefault();
                if (LocatorQuery != null)
                {
                    //OK we can play the content
                    locator = LocatorQuery;
                    valid = true;
                }

            }
            return valid;

        }


        private void withMPEGDASHIFReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.DASHIFRefPlayer);
        }


        private void DoCreateAssetReportEmail()
        {
            AssetInfo AR = new AssetInfo(ReturnSelectedAssetsV3(), _amsClientV3);

        }

        private void DoDisplayAssetReport()
        {
            AssetInfo AR = new AssetInfo(ReturnSelectedAssetsV3(), _amsClientV3);
            StringBuilder SB = AR.GetStats();
            EditorXMLJSON tokenDisplayForm = new EditorXMLJSON("Asset report", SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
        }

        private void createOutlookReportEmailToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }


        private void openOutputAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(false);
        }

        private void DoOpenJobAsset(bool inputasset) // if false, then display first outputasset
        {
            List<JobExtension> SelectedJobs = ReturnSelectedJobsV3();
            if (SelectedJobs.Count != 0)
            {
                JobExtension jobToDisplay = SelectedJobs.FirstOrDefault();
                if (jobToDisplay != null)
                {
                    try
                    {
                        if (inputasset) // input
                        {
                            if (jobToDisplay.Job.Input.GetType() == typeof(JobInputAsset))
                            {
                                JobInputAsset jobinputasset = (JobInputAsset)jobToDisplay.Job.Input;
                                Task<Asset> asset = GetAssetAsync(jobinputasset.AssetName);
                                if (asset != null)
                                {
                                    Asset assetIn = Task.Run(async () => await GetAssetAsync(jobinputasset.AssetName)).Result;
                                    DisplayInfo(assetIn);
                                }

                                else
                                {
                                    MessageBox.Show($"Input asset '{jobinputasset.AssetName}' not found.", "Asset error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }
                        else // output
                        {
                            if (jobToDisplay.Job.Outputs.FirstOrDefault() != null && (jobToDisplay.Job.Outputs.FirstOrDefault().GetType() == typeof(JobOutputAsset)))
                            {
                                JobOutputAsset joboutputasset = (JobOutputAsset)jobToDisplay.Job.Outputs.FirstOrDefault();
                                Task<Asset> asset = GetAssetAsync(joboutputasset.AssetName);
                                if (asset != null)
                                {
                                    Asset assetOut = Task.Run(async () => await GetAssetAsync(joboutputasset.AssetName)).Result;
                                    DisplayInfo(assetOut);
                                }
                                else
                                {
                                    MessageBox.Show($"Output asset '{joboutputasset.AssetName}' not found.", "Asset error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error when accessing the asset", "Asset error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void inputAssetInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(true);
        }

        /*
        private void DoExportAssetToAzureStorage()
        {

            string valuekey = "";
            bool UseDefaultStorage = true;
            string containername = "";
            string otherstoragename = "";
            string otherstoragekey = "";
            List<IAssetFile> SelectedFiles = new List<IAssetFile>();
            bool CreateNewContainer = false;

            List<IAsset> SelectedAssets = new List<IAsset>(); //ReturnSelectedAssets();
            if (SelectedAssets.Count == 1)
            {
                if (!havestoragecredentials)
                { // No blob credentials. Let's ask the user
                    if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey, true) == DialogResult.OK)
                    {
                        _credentials.DefaultStorageKey = valuekey;
                        havestoragecredentials = true;
                    }
                }
                if (havestoragecredentials) // if we have the storage credentials
                {
                    if (SelectedAssets.FirstOrDefault().Options == AssetCreationOptions.None && SelectedAssets.FirstOrDefault().StorageAccountName == _context.DefaultStorageAccount.Name) // Ok, the selected asset is not encrypyted
                    {
                        if (CopyAssetToAzure(ref UseDefaultStorage, ref containername, ref otherstoragename, ref otherstoragekey, ref SelectedFiles, ref CreateNewContainer, SelectedAssets.FirstOrDefault()) == DialogResult.OK)
                        {
                            var response = DoGridTransferAddItem("Export to Azure Storage " + (CreateNewContainer ? "to a new container" : "to an existing container"), TransferType.ExportToAzureStorage, false);
                            // Start a worker thread that does copy.
                            Task.Factory.StartNew(() => ProcessExportAssetToAzureStorage(UseDefaultStorage, containername, otherstoragename, otherstoragekey, SelectedFiles, CreateNewContainer, response), response.token);
                            DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                            DoRefreshGridAssetV(false);
                        }
                    }
                    else if (SelectedAssets.FirstOrDefault().StorageAccountName != _context.DefaultStorageAccount.Name)
                    {
                        MessageBox.Show("Asset cannot be exported as it is not in the default storage acount. Feature not implemented yet.", "Asset storage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else // selected asset is encrypted, so we warn the user
                    {
                        MessageBox.Show("Asset cannot be exported as it is storage encrypted.", "Asset encrypted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
        */

        private void fromAzureStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fromASingleHTTPURLAmazonS3EtcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromHttp();
        }

        private void toAzureStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void azureMediaServicesDocumentationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkMoreInfoDocAMS);
        }

        private void azureMediaServicesForumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkForumAMS);
        }

        private void azureMediaHelpFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(_HelpFiles + "AMSv3doc.pdf");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox myabout = new AboutBox();
            myabout.Show();
        }

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            TabControl tabcontrol = (TabControl)sender;

            EnableChildItems(ref contextMenuStripTransfers, (tabcontrol.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabTransfers)));

            EnableChildItems(ref originToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabOrigins)));
            EnableChildItems(ref contextMenuStripStreaminEndpoints, (tabcontrol.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabOrigins)));

            EnableChildItems(ref liveLiveEventToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabLive)));
            EnableChildItems(ref contextMenuStripLiveEvents, (tabcontrol.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabLive)));
            EnableChildItems(ref contextMenuStripLiveOutputs, (tabcontrol.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabLive)));



            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageChart":
                    buttonRefreshTab.Enabled = false;
                    break;
                default:
                    buttonRefreshTab.Enabled = true;
                    break;
            }
        }

        private void EnableChildItems(ref ToolStripMenuItem menuitem, bool bflag)
        {
            menuitem.Enabled = bflag;
            foreach (ToolStripItem item in menuitem.DropDownItems)
            {
                item.Enabled = bflag;

                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem itemt = (ToolStripMenuItem)item;
                    if (itemt.HasDropDownItems)
                    {
                        foreach (ToolStripItem itemd in itemt.DropDownItems)
                        {
                            itemd.Enabled = bflag;
                        }
                    }
                }
            }
        }

        private void EnableChildItems(ref ContextMenuStrip menuitem, bool bflag)
        {
            menuitem.Enabled = bflag;
            foreach (ToolStripItem item in menuitem.Items)
            {
                item.Enabled = bflag;

                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem itemt = (ToolStripMenuItem)item;
                    if (itemt.HasDropDownItems)
                    {
                        foreach (ToolStripItem itemd in itemt.DropDownItems)
                        {
                            itemd.Enabled = bflag;
                        }
                    }
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRefresh();
        }


        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoManageOptions();
        }

        private void DoManageOptions()
        {
            Options myForm = new Options();
            if (myForm.ShowDialog() == DialogResult.OK)
            {
                ApplySettingsOptions();
            }
        }

        private void ApplySettingsOptions(bool init = false)
        {
            if (!init)
            {
                dataGridViewAssetsV.Columns["AssetId"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
                dataGridViewAssetsV.Columns["AlternateId"].Visible = Properties.Settings.Default.DisplayAssetAltIDinGrid;
                dataGridViewAssetsV.Columns["StorageAccountName"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
            }

            dataGridViewJobsV.JobssPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid;

            TimerAutoRefresh.Interval = Properties.Settings.Default.AutoRefreshTime * 1000;
            TimerAutoRefresh.Enabled = Properties.Settings.Default.AutoRefresh;
            withCustomPlayerToolStripMenuItem1.Visible = Properties.Settings.Default.CustomPlayerEnabled;
            withCustomPlayerToolStripMenuItem2.Visible = Properties.Settings.Default.CustomPlayerEnabled;
        }


        private void DoRefreshGridLiveEventV(bool firstime)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            if (firstime)
            {
                dataGridViewLiveEventsV.Init(_amsClientV3);
            }

            Task.Run(async () =>
            {
                await dataGridViewLiveEventsV.RefreshLiveEventAsync(1);
                tabPageLive.Invoke(new Action(() => tabPageLive.Text = string.Format(AMSExplorer.Properties.Resources.TabLive + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, dataGridViewLiveEventsV.totalLiveEvents)));
                labelLiveEvents.Invoke(new Action(() => labelLiveEvents.Text = string.Format(AMSExplorer.Properties.Resources.LabelChannel + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, dataGridViewLiveEventsV.totalLiveEvents)));
            });

        }

        private void DoRefreshGridLiveOutputV(bool firstime)
        {

            if (firstime)
            {
                Debug.WriteLine("DoRefreshGridProgramVforsttime");
                dataGridViewLiveOutputV.Init(_amsClientV3);
            }
            else
            {
                Debug.WriteLine("DoRefreshGridProgramVNotforsttime");
            }

            Task.Run(async () =>
            {
                await dataGridViewLiveOutputV.RefreshLiveOutputsAsync(1);
                labelPrograms.Invoke(new Action(() => labelPrograms.Text = string.Format(AMSExplorer.Properties.Resources.LabelProgram + " ({0})", dataGridViewLiveOutputV.DisplayedCount)));
            });
        }

        private void DoRefreshGridStreamingEndpointV(bool firstime)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            if (firstime)
            {
                dataGridViewStreamingEndpointsV.Init(_amsClientV3);
            }
            Debug.WriteLine("DoRefreshGridOriginsVNotforsttime");
            Task.Run(async () =>
            {
                await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointsAsync();
                tabPageAssets.Invoke(new Action(() => tabPageOrigins.Text = string.Format(AMSExplorer.Properties.Resources.TabOrigins + " ({0})", dataGridViewStreamingEndpointsV.DisplayedCount)));
            });
        }


        private void DoRefreshGridStorageV(bool firstime)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            MediaService amsaccount = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);

            if (firstime)
            {
                // Storage tab
                dataGridViewStorage.ColumnCount = 3;

                /*
                DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
                {
                    Name = "% used",
                    DataPropertyName = "% used",
                    HeaderText = "% used"
                };
                dataGridViewStorage.Columns.Add(col);
                */

                dataGridViewStorage.Columns[0].Name = "Name";
                dataGridViewStorage.Columns[0].HeaderText = "Name";
                dataGridViewStorage.Columns[0].Width = 150;
                dataGridViewStorage.Columns[1].Name = "Capacity";
                dataGridViewStorage.Columns[1].HeaderText = "Capacity";
                dataGridViewStorage.Columns[1].Width = 80;
                dataGridViewStorage.Columns[2].Name = "Id";
                dataGridViewStorage.Columns[2].HeaderText = "Id";
                dataGridViewStorage.Columns[2].Width = 700;
                /*
                dataGridViewStorage.Columns[2].Name = "StrictName";
                dataGridViewStorage.Columns[2].Visible = false;
                dataGridViewStorage.Columns[3].Width = 600;
                */
            }
            dataGridViewStorage.Rows.Clear();

            foreach (StorageAccount storage in amsaccount.StorageAccounts)
            {

                long? capacity = _amsClientV3.GetStorageCapacity(storage.Id);

                /*
                double? capacityPercentageFullTmp = null;
                if (storage.BytesUsed != null)
                {
                    displaycapacity = true;
                    capacityPercentageFullTmp = (double)((100 * (double)storage.BytesUsed) / (double)TotalStorageInBytes);
                }
                */

                string name = AMSClientV3.GetStorageName(storage.Id);
                string append = string.Empty;
                if (storage.Type == StorageAccountType.Primary)
                {
                    append = " (primary)";
                }
                // int rowi = dataGridViewStorage.Rows.Add(name + append, storage.Id);

                int rowi = dataGridViewStorage.Rows.Add(name + append, capacity != null ? AssetInfo.FormatByteSize(capacity) : "(are the metrics enabled ?)", storage.Id);
                if (storage.Type == StorageAccountType.Primary)
                {
                    dataGridViewStorage.Rows[rowi].Cells[0].Style.ForeColor = Color.Blue;
                    dataGridViewStorage.Rows[rowi].Cells[0].ToolTipText = "Primary storage account";

                }
                if (capacity == null)
                {
                    dataGridViewStorage.Rows[rowi].Cells[1].ToolTipText = "Storage Account Metrics are not enabled or no data is available";
                }
            }
            tabPageStorage.Text = string.Format(AMSExplorer.Properties.Resources.TabStorage + " ({0})", amsaccount.StorageAccounts.Count());
        }


        public void DoRefreshGridFiltersV(bool firstime)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            if (firstime)
            {
                // Storage tab
                dataGridViewFilters.ColumnCount = 6;
                dataGridViewFilters.Columns[0].HeaderText = "Name";
                dataGridViewFilters.Columns[0].Name = "Name";
                dataGridViewFilters.Columns[0].ReadOnly = true;
                dataGridViewFilters.Columns[0].Width = 100;
                dataGridViewFilters.Columns[1].HeaderText = "Track Filtering Rules";
                dataGridViewFilters.Columns[1].Name = "Rules";
                dataGridViewFilters.Columns[1].Width = 135;
                dataGridViewFilters.Columns[2].HeaderText = "Start (d.h:m:s)";
                dataGridViewFilters.Columns[2].Name = "Start";
                dataGridViewFilters.Columns[2].Width = 110;
                dataGridViewFilters.Columns[3].HeaderText = "End (d.h:m:s)";
                dataGridViewFilters.Columns[3].Name = "End";
                dataGridViewFilters.Columns[3].Width = 110;
                dataGridViewFilters.Columns[4].HeaderText = "DVR (d.h:m:s)";
                dataGridViewFilters.Columns[4].Name = "DVR";
                dataGridViewFilters.Columns[4].Width = 110;
                dataGridViewFilters.Columns[5].HeaderText = "Live backoff (d.h:m:s)";
                dataGridViewFilters.Columns[5].Name = "LiveBackoff";
                dataGridViewFilters.Columns[5].Width = 144;
            }
            dataGridViewFilters.Rows.Clear();

            Microsoft.Rest.Azure.IPage<AccountFilter> filters = _amsClientV3.AMSclient.AccountFilters.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);
            foreach (AccountFilter filter in filters)
            {
                string s = null;
                string e = null;
                string d = null;
                string l = null;

                if (filter.PresentationTimeRange != null)
                {
                    long? start = filter.PresentationTimeRange.StartTimestamp;
                    long? end = filter.PresentationTimeRange.EndTimestamp;
                    long? dvr = filter.PresentationTimeRange.PresentationWindowDuration;
                    long? backoff = filter.PresentationTimeRange.LiveBackoffDuration;

                    if (true)//filter.PresentationTimeRange.Timescale != null)
                    {
                        double dscale = (double)filter.PresentationTimeRange.Timescale / TimeSpan.TicksPerSecond;
                        if (start != null)
                        {
                            start = (long)((double)start / dscale);
                        }
                        if (end != null)
                        {
                            end = (long)((double)end / dscale);
                        }
                        if (dvr != null)
                        {
                            dvr = (long)((double)dvr / dscale);
                        }
                        if (backoff != null)
                        {
                            backoff = (long)((double)backoff / dscale);
                        }
                    }

                    s = (start != null) ? TimeSpan.FromTicks((long)start).ToString(@"d\.hh\:mm\:ss") : "min";
                    e = (end != null) ? TimeSpan.FromTicks((long)end).ToString(@"d\.hh\:mm\:ss") : "max";

                    d = (dvr != null) ? TimeSpan.FromTicks((long)dvr).ToString(@"d\.hh\:mm\:ss") : "max";
                    l = (backoff != null) ? TimeSpan.FromTicks((long)backoff).ToString(@"d\.hh\:mm\:ss") : "min";
                }
                try
                {
                    int nbtracks = filter.Tracks.Count;
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, filter.Tracks.Count, s, e, d, l);
                }
                catch
                {
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, "Error", s, e, d, l);
                }
            }

            tabPageFilters.Text = string.Format(AMSExplorer.Properties.Resources.TabFilters + " ({0})", filters.Count());
        }


        private List<LiveEvent> ReturnSelectedLiveEvents()
        {
            List<LiveEvent> SelectedLiveEvents = new List<LiveEvent>();
            foreach (DataGridViewRow Row in dataGridViewLiveEventsV.SelectedRows)
            {
                // sometimes, the live event can be null (if just deleted)
                LiveEvent liveEvent = Task.Run(async () => await GetLiveEventAsync(Row.Cells[dataGridViewLiveEventsV.Columns["Name"].Index].Value.ToString())).Result;
                if (liveEvent != null)
                {
                    SelectedLiveEvents.Add(liveEvent);
                }
            }
            SelectedLiveEvents.Reverse();
            return SelectedLiveEvents;
        }

        private List<StreamingEndpoint> ReturnSelectedStreamingEndpoints()
        {
            List<StreamingEndpoint> SelectedOrigins = new List<StreamingEndpoint>();
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (DataGridViewRow Row in dataGridViewStreamingEndpointsV.SelectedRows)
            {
                string seName = Row.Cells[dataGridViewStreamingEndpointsV.Columns["Name"].Index].Value.ToString();
                StreamingEndpoint se = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, seName);
                if (se != null)
                {
                    SelectedOrigins.Add(se);
                }
            }
            SelectedOrigins.Reverse();
            return SelectedOrigins;
        }


        private List<LiveOutput> ReturnSelectedLiveOutputs()
        {
            List<LiveOutput> SelectedLiveOutputs = new List<LiveOutput>();
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (DataGridViewRow Row in dataGridViewLiveOutputV.SelectedRows)
            {
                LiveOutput liveOutput = _amsClientV3.AMSclient.LiveOutputs.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, Row.Cells[dataGridViewLiveOutputV.Columns["LiveEventName"].Index].Value.ToString(), Row.Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString());
                if (liveOutput != null)
                {
                    SelectedLiveOutputs.Add(liveOutput);
                }
            }
            SelectedLiveOutputs.Reverse();
            return SelectedLiveOutputs;
        }

        private void DoStartLiveEvents()
        {
            // let's start the live events

            Task.Run(() =>
            {
                DoStartLiveEventsEngine(ReturnSelectedLiveEvents());
            }
                    );
        }


        private async void DoStopOrDeleteLiveEvents(bool deleteLiveEvents)
        {
            // delete also if delete = true
            List<LiveEvent> ListEvents = ReturnSelectedLiveEvents();
            List<LiveOutput> LOList = new List<LiveOutput>();
            await _amsClientV3.RefreshTokenIfNeededAsync();

            foreach (LiveEvent le in ListEvents)
            {
                LOList.AddRange(_amsClientV3.AMSclient.LiveOutputs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, le.Name).ToList());
            }

            string liveEventStr = ListEvents.Count > 1 ? "live events" : "live event";

            if (ListEvents.Count > 0)
            {
                if (LOList.Count > 0) // There are live outputs associated to the live event(s) to be deleted
                {
                    string leaction = deleteLiveEvents ? "Delete" : "Stop";
                    string question = (LOList.Count == 1) ? string.Format("There is one live output associated to the {0}.\n{1} the {0} and delete live output '{2}' ?", liveEventStr, leaction, LOList[0].Name)
                                                        : string.Format("There are {0} live outputs associated to the {1}.\n{2} the c{1} and delete these live outputs ?", LOList.Count, liveEventStr, leaction);

                    DeleteLiveOutputEvent form = new DeleteLiveOutputEvent(question, "Delete");
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await Task.Factory.StartNew(() =>
                           DoDeleteLiveOutputsEngineAsync(LOList, form.DeleteAsset)
                            );
                    }
                    else
                    {
                        return;
                    }
                }

                else // No live output associated to the live event(s) to be deleted
                {
                    string question;
                    if (deleteLiveEvents)
                    {
                        question = (ListEvents.Count == 1) ? "Delete live event " + ListEvents[0].Name + " ?" : "Delete these " + ListEvents.Count + " live events ?";
                    }
                    else
                    {
                        question = (ListEvents.Count == 1) ? "Stop live event " + ListEvents[0].Name + " ?" : "Stop these " + ListEvents.Count + " live events ?";
                    }

                    if (System.Windows.Forms.MessageBox.Show(question, "C" + liveEventStr + " deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                }

                Task myTask = Task.Factory.StartNew(() =>
                                    DoStopOrDeleteLiveEventsEngine(ListEvents, deleteLiveEvents)
                                     );

            }
        }


        private void dataGridViewLiveV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            object cellLiveEventStateValue = dataGridViewLiveEventsV.Rows[e.RowIndex].Cells[dataGridViewLiveEventsV.Columns["State"].Index].Value;

            if (cellLiveEventStateValue != null)
            {
                LiveEventResourceState CS = (LiveEventResourceState)cellLiveEventStateValue;
                Color mycolor;


                switch (CS)
                {
                    case nameof(LiveEventResourceState.Deleting):
                        mycolor = Color.Red;
                        break;
                    case nameof(LiveEventResourceState.Stopping):
                        mycolor = Color.OrangeRed;
                        break;
                    case nameof(LiveEventResourceState.Starting):
                        mycolor = Color.DarkCyan;
                        break;
                    case nameof(LiveEventResourceState.Stopped):
                        mycolor = Color.Blue;
                        break;
                    case nameof(LiveEventResourceState.Running):
                        mycolor = Color.Green;
                        break;
                    default:
                        mycolor = Color.Black;
                        break;
                }





                e.CellStyle.ForeColor = mycolor;
            }
        }

        private void DoResetLiveEvents()
        {
            List<LiveEvent> ListEvents = ReturnSelectedLiveEvents();
            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = _amsClientV3.AMSclient.LiveOutputs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, le.Name).ToList();
                plist.ForEach(p => LOList.Add(new Program.LiveOutputExt() { LiveOutputItem = p, LiveEventName = le.Name }));
            }

            IEnumerable<Program.LiveOutputExt> liveOutputRunningQuery = LOList.Where(p => p.LiveOutputItem.ResourceState == LiveOutputResourceState.Running);

            if (LOList.Where(p => p.LiveOutputItem.ResourceState == LiveOutputResourceState.Creating || p.LiveOutputItem.ResourceState == LiveOutputResourceState.Deleting).Count() > 0) // live outputs are in creation or deletion mode
            {
                MessageBox.Show("Some live outputs are being created or deleted. Live event(s) cannot be reset now.", "Live event(s) stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (liveOutputRunningQuery.Count() > 0) // some output exists
                {
                    if (MessageBox.Show("One or several live outputs are running which prevents the live event(s) reset. Do you want to delete the live output(s) and then reset the live event(s) ?", "Live event reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Task.Run(async () =>
                         {
                             try
                             {
                                 await DoDeleteLiveOutputs(liveOutputRunningQuery.Select(o => o.LiveOutputItem).ToList());

                                 // let's reset the live events now that running output are stopped
                                 ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Reseting live event '{0}'...", e.Name));
                                 Task[] tasksreset = ListEvents.Select(c => _amsClientV3.AMSclient.LiveEvents.ResetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                                 await Task.WhenAll(tasksreset);
                                 ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Live event '{0}' reset.", e.Name));
                                 ListEvents.ToList().ForEach(e => Notify("Live event reset", string.Format("Live event '{0}' has been reset.", e.Name), false));
                             }
                             catch (Exception ex)
                             {
                                 TextBoxLogWriteLine("Error when reseting live events.", true);
                                 TextBoxLogWriteLine(ex);
                             }
                         }
                      );

                    }
                }
                else
                {
                    string question = (ListEvents.Count == 1) ? string.Format("Reset live event '{0}' ?", ListEvents[0].Name) : string.Format("Reset these {0} live event(s) ?", ListEvents.Count);

                    if (System.Windows.Forms.MessageBox.Show(question, "Live event reset", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // let's reset the events
                        Task.Run(async () =>
                       {
                           try
                           {
                               // let's reset the live events now that live outputs are deleted
                               ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Reseting live event '{0}'...", e.Name));
                               Task[] tasksreset = ListEvents.Select(c => _amsClientV3.AMSclient.LiveEvents.ResetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                               await Task.WhenAll(tasksreset);
                               ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Live event '{0}' reset.", e.Name));
                               ListEvents.ToList().ForEach(e => Notify("Live event reset", string.Format("Live event '{0}' has been reset.", e.Name), false));
                           }
                           catch (Exception ex)
                           {
                               TextBoxLogWriteLine("Error when reseting live events.", true);
                               TextBoxLogWriteLine(ex);
                           }

                       });
                    }
                }
            }
        }


        private void createLiveEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateLiveEvent();
        }

        private async void DoCreateLiveEvent()
        {
            LiveEventCreation form = new LiveEventCreation(_amsClientV3)
            {
                KeyframeIntervalSerialized = XmlConvert.ToString(Properties.Settings.Default.LiveKeyFrameInterval),
                StartLiveEventNow = true
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                await _amsClientV3.RefreshTokenIfNeededAsync();

                TextBoxLogWriteLine("Live event '{0}' : creating...", form.LiveEventName);

                bool Error = false;
                LiveEvent liveEvent = new LiveEvent();
                try
                {

                    LiveEventPreview liveEventPreview = new LiveEventPreview
                    {
                        AccessControl = new LiveEventPreviewAccessControl(
                                                                            ip: new IPAccessControl
                                                                            (
                                                                                allow: form.inputIPAllow
                                                                            )
                                                                         )
                    };

                    LiveEventInput liveEventInput = new LiveEventInput
                    {
                        StreamingProtocol = form.Protocol,
                        AccessToken = form.AccessToken,
                        KeyFrameIntervalDuration = form.KeyframeIntervalSerialized,
                        AccessControl = new LiveEventInputAccessControl(
                                                                            ip: new IPAccessControl
                                                                            (
                                                                                allow: form.inputIPAllow
                                                                            )
                                                                       )
                    };

                    liveEvent = new LiveEvent(
                                              name: form.LiveEventName,
                                              location: _amsClientV3.credentialsEntry.MediaService.Location,
                                              description: form.LiveEventDescription,
                                              vanityUrl: form.VanityUrl,
                                              encoding: form.Encoding,
                                              input: liveEventInput,
                                              preview: liveEventPreview,
                                              streamOptions: new List<StreamOptionsFlag?>()
                                                          {
                                                // Set this to Default or Low Latency
                                               form.LowLatencyMode? StreamOptionsFlag.LowLatency: StreamOptionsFlag.Default
                                                          }
                                                            );

                }

                catch (Exception ex)
                {
                    Error = true;
                    TextBoxLogWriteLine("Error with live event settings.", true);
                    TextBoxLogWriteLine(ex);
                }

                if (!Error)
                {
                    try
                    {
                        await Task.Run(() =>
                         _amsClientV3.AMSclient.LiveEvents.CreateAsync(
                                                                         _amsClientV3.credentialsEntry.ResourceGroup,
                                                                         _amsClientV3.credentialsEntry.AccountName,
                                                                         form.LiveEventName,
                                                                         liveEvent,
                                                                         autoStart: form.StartLiveEventNow ? true : false)
                                                                      );

                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error with live event creation.", true);
                        TextBoxLogWriteLine(ex);
                    }

                    DoRefreshGridLiveEventV(false);

                }
            }
        }


        private void DoDisplayLiveEventInfo()
        {
            DoDisplayLiveEventInfo(ReturnSelectedLiveEvents());
        }


        private void DoDisplayLiveEventInfo(List<LiveEvent> liveEvents)
        {
            LiveEvent firstLiveEvent = liveEvents.FirstOrDefault();
            bool multiselection = liveEvents.Count > 1;

            if (firstLiveEvent != null)
            {
                LiveEventInformation form = new LiveEventInformation(this, _amsClientV3)
                {
                    MyLiveEvent = firstLiveEvent,
                    MultipleSelection = multiselection
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    ExplorerLiveEventModifications modifications = form.Modifications;
                    if (multiselection)
                    {
                        SettingsSelection formSettings = new SettingsSelection("live events", modifications);
                        if (formSettings.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        else
                        {
                            modifications = (ExplorerLiveEventModifications)formSettings.SettingsObject;
                        }
                    }

                    foreach (LiveEvent liveEvent in liveEvents)
                    {
                        TextBoxLogWriteLine("Live event '{0}' : updating...", liveEvent.Name);

                        if (modifications.Description) // let' update description if needed
                        {
                            liveEvent.Description = form.GetLiveEventDescription;
                        }
                        if (modifications.KeyFrameInterval)
                        {
                            liveEvent.Input.KeyFrameIntervalDuration = form.KeyframeIntervalSerialized;
                        }

                        if (liveEvent.Encoding.EncodingType == firstLiveEvent.Encoding.EncodingType)
                        {

                            if (liveEvent.Encoding.EncodingType != LiveEventEncodingType.None && liveEvent.Encoding != null && liveEvent.ResourceState == LiveEventResourceState.Stopped)
                            {
                                if (modifications.SystemPreset)
                                {
                                    liveEvent.Encoding.PresetName = form.PresetName; // we update the system preset
                                }

                            }
                            else if (liveEvent.Encoding.EncodingType != LiveEventEncodingType.None && liveEvent.ResourceState != LiveEventResourceState.Stopped)
                            {
                                TextBoxLogWriteLine("Live event '{0}' : must be stoped to update the encoding settings", liveEvent.Name);
                            }
                            else if (liveEvent.Encoding.EncodingType != LiveEventEncodingType.None && liveEvent.Encoding == null)
                            {
                                TextBoxLogWriteLine("Live event '{0}' : configured as encoding live event but settings are null", liveEvent.Name, true);
                            }
                        }



                        if (modifications.InputIPAllowList)
                        {
                            // Input allow list
                            if (form.GetInputAllowList != null)
                            {
                                if (liveEvent.Input.AccessControl == null)
                                {
                                    liveEvent.Input.AccessControl = new LiveEventInputAccessControl();
                                }
                                liveEvent.Input.AccessControl.Ip = form.GetInputAllowList;
                            }
                            else
                            {
                                if (liveEvent.Input.AccessControl != null)
                                {
                                    liveEvent.Input.AccessControl.Ip = null;
                                }
                            }
                        }


                        if (modifications.PreviewIPAllowList)
                        {
                            // Preview allow list
                            if (form.GetPreviewAllowList != null)
                            {
                                if (liveEvent.Preview.AccessControl == null)
                                {
                                    liveEvent.Preview.AccessControl = new LiveEventPreviewAccessControl();
                                }
                                liveEvent.Preview.AccessControl.Ip = form.GetPreviewAllowList;
                            }
                            else
                            {
                                if (liveEvent.Preview.AccessControl != null)
                                {
                                    liveEvent.Preview.AccessControl.Ip = null;
                                }
                            }
                        }


                        if (modifications.ClientAccessPolicy)
                        {
                            // Client Access Policy
                            if (form.GetLiveEventClientPolicy != null)
                            {
                                if (liveEvent.CrossSiteAccessPolicies == null)
                                {
                                    liveEvent.CrossSiteAccessPolicies = new Microsoft.Azure.Management.Media.Models.CrossSiteAccessPolicies();
                                }
                                liveEvent.CrossSiteAccessPolicies.ClientAccessPolicy = form.GetLiveEventClientPolicy;

                            }
                            else
                            {
                                if (liveEvent.CrossSiteAccessPolicies != null)
                                {
                                    liveEvent.CrossSiteAccessPolicies.ClientAccessPolicy = null;
                                }
                            }
                        }

                        if (modifications.CrossDomainPolicy)
                        {
                            // Cross domain  Policy
                            if (form.GetLiveEventCrossdomainPolicy != null)
                            {
                                if (liveEvent.CrossSiteAccessPolicies == null)
                                {
                                    liveEvent.CrossSiteAccessPolicies = new Microsoft.Azure.Management.Media.Models.CrossSiteAccessPolicies();
                                }
                                liveEvent.CrossSiteAccessPolicies.CrossDomainPolicy = form.GetLiveEventCrossdomainPolicy;

                            }
                            else
                            {
                                if (liveEvent.CrossSiteAccessPolicies != null)
                                {
                                    liveEvent.CrossSiteAccessPolicies.CrossDomainPolicy = null;
                                }
                            }
                        }
                        _amsClientV3.RefreshTokenIfNeeded();

                        Task.Run(async () =>
                        {
                            try
                            {
                                await _amsClientV3.AMSclient.LiveEvents.UpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, liveEvent.Name, liveEvent);
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshLiveEvent(liveEvent)), null);
                                TextBoxLogWriteLine("Live event '{0}' : updated.", liveEvent.Name);
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when updating a live event.", true);
                                TextBoxLogWriteLine(ex);
                            }
                        }
             );
                    }
                }

            }
        }

        private void dataGridViewLiveV_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonChSelected.Checked) // only in select mode
            {
                Debug.WriteLine("live event selection changed : begin");
                List<LiveEvent> SelectedLiveEvents = ReturnSelectedLiveEvents();
                if (SelectedLiveEvents.Count > 0)
                {

                    dataGridViewLiveOutputV.LiveEventSourceNames = SelectedLiveEvents.Select(c => c.Name).ToList();

                    Task.Run(() =>
                    {
                        Debug.WriteLine("live event selection changed : before refresh");
                        DoRefreshGridLiveOutputV(false);
                    });
                }
            }
        }

        private void DoStopOrDeleteLiveEventsEngine(List<LiveEvent> ListEvents, bool deleteLiveEvents)
        {

            // Stop the live events which run
            List<LiveEvent> liveeventsrunning = ListEvents.Where(p => p.ResourceState == LiveEventResourceState.Running).ToList();
            string names = string.Join(", ", liveeventsrunning.Select(le => le.Name).ToArray());

            if (liveeventsrunning.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Stopping live event(s) : {0}...", names));
                    List<LiveEventResourceState?> states = liveeventsrunning.Select(p => p.ResourceState).ToList();
                    Task[] taskcstop = liveeventsrunning.Select(c => _amsClientV3.AMSclient.LiveEvents.StopAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    int complete = 0;
                    while (!taskcstop.All(t => t.IsCompleted) && complete != liveeventsrunning.Count)
                    {
                        // refresh the live events

                        foreach (LiveEvent loitem in liveeventsrunning)
                        {
                            LiveEvent loitemR = Task.Run(async () => await GetLiveEventAsync(loitem.Name)).Result;

                            if (loitemR != null && states[liveeventsrunning.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[liveeventsrunning.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshLiveEvent(loitemR)), null);
                                if (loitemR.ResourceState == LiveEventResourceState.Stopped)
                                {
                                    TextBoxLogWriteLine(string.Format("Live event stopped : {0}.", loitemR.Name));
                                    complete++;
                                }
                            }

                        }
                        System.Threading.Thread.Sleep(2000);
                    }
                    Task.WaitAll(taskcstop);

                    //TextBoxLogWriteLine(string.Format("Live event(s) stopped : {0}.", names));
                }


                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when stopping a live event.", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            if (deleteLiveEvents)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                // delete the live events
                try
                {
                    string names2 = string.Join(", ", ListEvents.Select(le => le.Name).ToArray());

                    TextBoxLogWriteLine(string.Format("Deleting live event(s) : {0}...", names2));
                    List<LiveEventResourceState?> states = ListEvents.Select(p => p.ResourceState).ToList();
                    Task[] taskcdel = ListEvents.Select(c => _amsClientV3.AMSclient.LiveEvents.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    while (!taskcdel.All(t => t.IsCompleted))
                    {
                        // refresh the live events

                        foreach (LiveEvent loitem in ListEvents)
                        {
                            LiveEvent loitemR = Task.Run(async () => await GetLiveEventAsync(loitem.Name)).Result;
                            if (loitemR != null && states[ListEvents.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[ListEvents.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshLiveEvent(loitemR)), null);
                            }
                            else if (loitemR != null)
                            {
                                DoRefreshGridLiveEventV(false);
                            }
                        }
                        System.Threading.Thread.Sleep(2000);
                    }
                    Task.WaitAll(taskcdel);
                    TextBoxLogWriteLine(string.Format("Live event(s) deleted : {0}.", names2));
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when deleting a live event", true);
                    TextBoxLogWriteLine(ex);
                }
            }
            DoRefreshGridLiveEventV(false);
        }


        private void DoStartLiveEventsEngine(List<LiveEvent> ListEvents)
        {
            // Start the live events which are stopped
            List<LiveEvent> liveevntsstopped = ListEvents.Where(p => p.ResourceState == LiveEventResourceState.Stopped).ToList();
            string names = string.Join(", ", liveevntsstopped.Select(le => le.Name).ToArray());
            if (liveevntsstopped.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Starting live event(s) : {0}...", names));
                    List<LiveEventResourceState?> states = liveevntsstopped.Select(p => p.ResourceState).ToList();
                    Task[] taskLEStart = liveevntsstopped.Select(c => _amsClientV3.AMSclient.LiveEvents.StartAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                    int complete = 0;

                    while (!taskLEStart.All(t => t.IsCompleted) && complete != liveevntsstopped.Count)
                    {
                        // refresh the live events

                        foreach (LiveEvent loitem in liveevntsstopped)
                        {
                            LiveEvent loitemR = Task.Run(async () => await GetLiveEventAsync(loitem.Name)).Result;
                            if (loitemR != null && states[liveevntsstopped.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[liveevntsstopped.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshLiveEvent(loitemR)), null);
                                if (loitemR.ResourceState == LiveEventResourceState.Running)
                                {
                                    TextBoxLogWriteLine(string.Format("Live event started : {0}.", loitemR.Name));
                                    complete++;
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(2000);
                    }
                    Task.WaitAll(taskLEStart);
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when starting a live event.", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            DoRefreshGridLiveEventV(false);
        }


        private async Task DoDeleteLiveOutputs(List<LiveOutput> ListOutputs = null)
        {
            // delete also if delete = true
            if (ListOutputs == null)
            {
                ListOutputs = ReturnSelectedLiveOutputs();
            }

            if (ListOutputs.Count > 0)
            {
                string question = (ListOutputs.Count == 1) ? string.Format("Delete the live output '{0}' ?", ListOutputs[0].Name)
                                                        : string.Format("Delete these {0} live outputs ?", ListOutputs.Count);

                DeleteLiveOutputEvent form = new DeleteLiveOutputEvent(question, "Delete");
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await DoDeleteLiveOutputsEngineAsync(ListOutputs, form.DeleteAsset);
                }
            }
        }


        private async Task DoDeleteLiveOutputsEngineAsync(List<LiveOutput> ListOutputs, bool DeleteAsset)
        {
            string[] assets = ListOutputs.Select(p => p.AssetName).ToArray();

            bool Error = false;
            await _amsClientV3.RefreshTokenIfNeededAsync();

            try
            {   // delete programs
                ListOutputs.ToList().ForEach(p => TextBoxLogWriteLine("Live output '{0}' : deleting...", p.Name));
                List<LiveOutputResourceState?> states = ListOutputs.Select(p => p.ResourceState).ToList();
                Task[] tasks = ListOutputs.Select(p => _amsClientV3.AMSclient.LiveOutputs.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, LiveOutputUtil.ReturnLiveEventFromOutput(p), p.Name)).ToArray();

                while (!tasks.All(t => t.IsCompleted))
                {
                    // refresh the programs

                    foreach (LiveOutput loitem in ListOutputs)
                    {
                        LiveOutput loitemR = Task.Run(async () => await GetLiveOutputAsync(LiveOutputUtil.ReturnLiveEventFromOutput(loitem), loitem.Name)).Result;
                        if (loitemR != null && states[ListOutputs.IndexOf(loitem)] != loitemR.ResourceState)
                        {
                            states[ListOutputs.IndexOf(loitem)] = loitemR.ResourceState;
                            dataGridViewLiveOutputV.BeginInvoke(new Action(() => dataGridViewLiveOutputV.RefreshProgram(LiveOutputUtil.ReturnLiveEventFromOutput(loitemR), loitemR)), null);
                        }
                        else if (loitemR != null)
                        {
                            //DoRefreshGridLiveOutputV(false);
                        }
                    }
                    System.Threading.Thread.Sleep(2000);
                }
                Task.WaitAll(tasks);
                TextBoxLogWriteLine("Live output(s) deleted.");
            }
            catch (Exception ex)
            {
                // Add useful information to the exception
                TextBoxLogWriteLine("There is a problem when deleting a live output", true);
                TextBoxLogWriteLine(ex);
                //Error = true;
            }
            DoRefreshGridLiveOutputV(false);


            if (DeleteAsset && Error == false)
            {
                assets.ToList().ForEach(a => TextBoxLogWriteLine("Asset '{0}' : deleting...", a));
                Task[] tasksassets = assets.Select(a => _amsClientV3.AMSclient.Assets.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, a)).ToArray();
                try
                {
                    await Task.WhenAll(tasksassets);
                    TextBoxLogWriteLine("Asset(s) deletion done.");
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when deleting an asset", true);
                    TextBoxLogWriteLine(ex);
                }
                DoRefreshGridAssetV(false);
            }
        }

        private void DoStartStreamingEndpointEngine(List<StreamingEndpoint> ListStreamingEndpoints)
        {
            // Start the streaming endpoint which are stopped
            List<StreamingEndpoint> streamingendpointsstopped = ListStreamingEndpoints.Where(p => p.ResourceState == StreamingEndpointResourceState.Stopped).ToList();
            string names = string.Join(", ", streamingendpointsstopped.Select(le => le.Name).ToArray());
            if (streamingendpointsstopped.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Starting streaming endpoint(s) : {0}...", names));
                    List<StreamingEndpointResourceState?> states = streamingendpointsstopped.Select(p => p.ResourceState).ToList();
                    Task[] taskSEStart = streamingendpointsstopped.Select(c => _amsClientV3.AMSclient.StreamingEndpoints.StartAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                    int complete = 0;

                    while (!taskSEStart.All(t => t.IsCompleted) && complete != streamingendpointsstopped.Count)
                    {
                        // refresh the live events

                        foreach (StreamingEndpoint loitem in streamingendpointsstopped)
                        {
                            StreamingEndpoint loitemR = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, loitem.Name);
                            if (loitemR != null && states[streamingendpointsstopped.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[streamingendpointsstopped.IndexOf(loitem)] = loitemR.ResourceState;
                                Task.Run(async () =>
                                {
                                    await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointAsync(loitemR);
                                });
                                if (loitemR.ResourceState == StreamingEndpointResourceState.Running)
                                {
                                    TextBoxLogWriteLine(string.Format("Streaming endpoint started : {0}.", loitemR.Name));
                                    complete++;
                                }
                            }
                        }
                        System.Threading.Thread.Sleep(2000);
                    }
                    Task.WaitAll(taskSEStart);

                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when starting a streaming endpoint.", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            DoRefreshGridStreamingEndpointV(false);
        }




        private async Task DoUpdateAndScaleStreamingEndpointEngineAsync(StreamingEndpoint se, int? units = null)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();

            try
            {
                TextBoxLogWriteLine(string.Format("updating streaming endpoint : {0}...", se.Name));
                await _amsClientV3.AMSclient.StreamingEndpoints.UpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, se.Name, se);
                TextBoxLogWriteLine(string.Format("Streaming endpoint updated : {0}.", se.Name));

                if (units != null)
                {
                    TextBoxLogWriteLine(string.Format("scaling streaming endpoint : {0}...", se.Name));
                    await _amsClientV3.AMSclient.StreamingEndpoints.ScaleAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, se.Name, units);
                    TextBoxLogWriteLine(string.Format("Streaming endpoint scaled : {0}.", se.Name));
                }

            }
            catch (Exception ex)
            {
                // Add useful information to the exception
                TextBoxLogWriteLine("There is a problem when updating/scaling a streaming endpoint.", true);
                TextBoxLogWriteLine(ex);
            }
            DoRefreshGridStreamingEndpointV(false);
        }


        private void DoStopOrDeleteStreamingEndpointsEngine(List<StreamingEndpoint> ListStreamingEndpoints, bool deleteStreamingEndpoints)
        {

            // Stop the streaming endpoints which run
            List<StreamingEndpoint> sesrunning = ListStreamingEndpoints.Where(p => p.ResourceState == StreamingEndpointResourceState.Running).ToList();
            string names = string.Join(", ", sesrunning.Select(le => le.Name).ToArray());

            if (sesrunning.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Stopping streaming endpoints(s) : {0}...", names));
                    List<StreamingEndpointResourceState?> states = sesrunning.Select(p => p.ResourceState).ToList();
                    Task[] taskSEstop = sesrunning.Select(c => _amsClientV3.AMSclient.StreamingEndpoints.StopAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    int complete = 0;
                    while (!taskSEstop.All(t => t.IsCompleted) && complete != sesrunning.Count)
                    {
                        // refresh the streaming endpoints

                        foreach (StreamingEndpoint loitem in sesrunning)
                        {
                            StreamingEndpoint loitemR = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, loitem.Name);
                            if (loitemR != null && states[sesrunning.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[sesrunning.IndexOf(loitem)] = loitemR.ResourceState;
                                Task.Run(async () =>
                                {
                                    await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointAsync(loitemR);
                                });

                                if (loitemR.ResourceState == StreamingEndpointResourceState.Stopped)
                                {
                                    TextBoxLogWriteLine(string.Format("Streaming endpoint stopped : {0}.", loitemR.Name));
                                    complete++;
                                }
                            }

                        }
                        System.Threading.Thread.Sleep(2000);
                    }
                    Task.WaitAll(taskSEstop);

                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when stopping a streaming endpoint.", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            if (deleteStreamingEndpoints)
            {
                // delete the ses
                try
                {
                    string names2 = string.Join(", ", ListStreamingEndpoints.Select(le => le.Name).ToArray());

                    TextBoxLogWriteLine(string.Format("Deleting streaming endpoints(s) : {0}...", names2));
                    List<StreamingEndpointResourceState?> states = ListStreamingEndpoints.Select(p => p.ResourceState).ToList();
                    Task[] taskSEdel = ListStreamingEndpoints.Select(c => _amsClientV3.AMSclient.StreamingEndpoints.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    while (!taskSEdel.All(t => t.IsCompleted))
                    {
                        // refresh the live events

                        foreach (StreamingEndpoint loitem in ListStreamingEndpoints)
                        {
                            StreamingEndpoint loitemR = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, loitem.Name);
                            if (loitemR != null && states[ListStreamingEndpoints.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[ListStreamingEndpoints.IndexOf(loitem)] = loitemR.ResourceState;
                                Task.Run(async () =>
                                {
                                    await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointAsync(loitemR);
                                });
                            }
                            else if (loitemR != null)
                            {
                                DoRefreshGridStreamingEndpointV(false);
                            }
                        }
                        System.Threading.Thread.Sleep(2000);
                    }
                    Task.WaitAll(taskSEdel);
                    TextBoxLogWriteLine(string.Format("Streaming endpoint(s) deleted : {0}.", names2));
                }

                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when deleting a streaming endpoint", true);
                    TextBoxLogWriteLine(ex);
                }
            }
            DoRefreshGridStreamingEndpointV(false);
        }


        private void DoCreateLiveOutput()
        {
            LiveEvent liveEvent = ReturnSelectedLiveEvents().FirstOrDefault();
            if (liveEvent != null)
            {
                string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);

                LiveOutputCreation form = new LiveOutputCreation(_amsClientV3)
                {
                    LiveEventName = liveEvent.Name,
                    ArchiveWindowLength = new TimeSpan(0, 5, 0),
                    CreateLocator = true,
                    AssetName = Constants.NameconvLiveEvent + "-" + Constants.NameconvLiveOutput,
                    ProgramName = "LiveOutput-" + uniqueness,
                    HLSFragmentPerSegment = Properties.Settings.Default.LiveHLSFragmentsPerSegment,
                    ManifestName = uniqueness
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    string assetname = form.AssetName.Replace(Constants.NameconvLiveOutput, form.ProgramName).Replace(Constants.NameconvLiveEvent, form.LiveEventName);
                    Asset newAsset = new Asset() { StorageAccountName = form.StorageSelected };

                    Asset asset = null;
                    Task.Run(async () =>
                    {
                        try
                        {
                            TextBoxLogWriteLine("Asset creation...");
                            asset = await _amsClientV3.AMSclient.Assets.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, assetname, newAsset);
                            TextBoxLogWriteLine("Asset created.");

                            TextBoxLogWriteLine("Live output creation...");

                            Hls hlsParam = null;
                            if (form.HLSFragmentPerSegment != null)
                            {
                                hlsParam = new Hls(fragmentsPerTsSegment: form.HLSFragmentPerSegment);
                            }

                            LiveOutput liveOutput = new LiveOutput(
                                asset.Name,
                                form.ArchiveWindowLength,
                                null,
                                form.ProgramName,
                                null,
                                form.ProgramDescription,
                                form.ManifestName ?? uniqueness,
                                hlsParam,
                                form.StartRecordTimestamp
                                );

                            LiveOutput liveOutput2 = await _amsClientV3.AMSclient.LiveOutputs.CreateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, liveEvent.Name, form.ProgramName, liveOutput);
                            TextBoxLogWriteLine("Live output created.");

                            if (form.CreateLocator)
                            {
                                try
                                {
                                    DoCreateLocator(new List<Asset> { asset }, form.ManifestName);
                                }
                                catch (Exception ex)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when publishing the asset of the live output", true);
                                    TextBoxLogWriteLine(ex);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when creating a live output", true);
                            TextBoxLogWriteLine(ex);
                        }

                        DoRefreshGridLiveOutputV(false);
                    });
                }
            }
        }

        private void createProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateLiveOutput();
        }

        private void dataGridViewProgramV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            object cellprogramstatevalue = dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["State"].Index].Value;

            if (cellprogramstatevalue != null)
            {
                LiveOutputResourceState PS = (LiveOutputResourceState)cellprogramstatevalue;
                Color mycolor;

                switch (PS)
                {
                    case nameof(LiveOutputResourceState.Deleting):
                        mycolor = Color.OrangeRed;
                        break;
                    case nameof(LiveOutputResourceState.Creating):
                        mycolor = Color.DarkCyan;
                        break;
                    case nameof(LiveOutputResourceState.Running):
                        mycolor = Color.Green;
                        break;

                    default:
                        mycolor = Color.Black;
                        break;
                }
                e.CellStyle.ForeColor = mycolor;
            }
        }


        private void DoDisplayLiveOutputInfo()
        {
            DoDisplayLiveOutputInfo(ReturnSelectedLiveOutputs());
        }

        private void DoDisplayLiveOutputInfo(List<LiveOutput> liveoutputs)
        {
            bool multiselection = liveoutputs.Count > 1;
            if (liveoutputs.FirstOrDefault() != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    LiveOutputInformation form = new LiveOutputInformation(this, _amsClientV3)
                    {
                        MyLiveOutput = liveoutputs.FirstOrDefault(),
                        MyStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints, // we pass this information if user open asset info from the program info dialog box
                        MultipleSelection = multiselection
                    };
                    form.ShowDialog();
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }


        private void dataGridViewOriginsV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            object cellSEstatevalue = dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[dataGridViewStreamingEndpointsV.Columns["State"].Index].Value;

            if (cellSEstatevalue != null)
            {
                StreamingEndpointResourceState SES = (StreamingEndpointResourceState)cellSEstatevalue;
                Color mycolor;

                switch (SES)
                {
                    case nameof(StreamingEndpointResourceState.Deleting):
                        mycolor = Color.Red;
                        break;
                    case nameof(StreamingEndpointResourceState.Stopping):
                        mycolor = Color.OrangeRed;
                        break;
                    case nameof(StreamingEndpointResourceState.Starting):
                        mycolor = Color.DarkCyan;
                        break;
                    case nameof(StreamingEndpointResourceState.Stopped):
                        mycolor = Color.Red;
                        break;
                    case nameof(StreamingEndpointResourceState.Running):
                        mycolor = Color.Green;
                        break;
                    default:
                        mycolor = Color.Black;
                        break;

                }
                e.CellStyle.ForeColor = mycolor;
            }
        }

        private void DoDisplayStreamingEndpointInfo()
        {
            DoDisplayStreamingEndpointInfo(ReturnSelectedStreamingEndpoints());
        }
        private void DoDisplayStreamingEndpointInfo(List<StreamingEndpoint> streamingendpoints)
        {
            if (streamingendpoints.Count == 0)
            {
                return;
            }

            bool multiselection = streamingendpoints.Count > 1;

            StreamingEndpointInformation form = new StreamingEndpointInformation(streamingendpoints.FirstOrDefault())
            {
                MultipleSelection = multiselection
            };


            if (form.ShowDialog() == DialogResult.OK)
            {
                ExplorerSEModifications modifications = form.Modifications;
                if (multiselection)
                {
                    SettingsSelection formSettings = new SettingsSelection("streaming endpoints", modifications);

                    if (formSettings.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        modifications = (ExplorerSEModifications)formSettings.SettingsObject;
                    }
                }

                foreach (StreamingEndpoint streamingendpoint in streamingendpoints)
                {
                    if (modifications.CustomHostNames)
                    {
                        streamingendpoint.CustomHostNames = form.GetStreamingCustomHostnames;
                    }

                    if (modifications.StreamingAllowedIPAddresses)
                    {
                        if (form.GetStreamingAllowList != null)
                        {
                            if (streamingendpoint.AccessControl == null)
                            {
                                streamingendpoint.AccessControl = new Microsoft.Azure.Management.Media.Models.StreamingEndpointAccessControl();
                            }
                            streamingendpoint.AccessControl.Ip = form.GetStreamingAllowList;
                        }
                        else
                        {
                            if (streamingendpoint.AccessControl != null)
                            {
                                streamingendpoint.AccessControl.Ip = null;
                            }
                        }
                    }

                    if (modifications.AkamaiSignatureHeaderAuthentication)
                    {

                        if (form.GetStreamingAkamaiList != null)
                        {
                            if (streamingendpoint.AccessControl == null)
                            {
                                streamingendpoint.AccessControl = new Microsoft.Azure.Management.Media.Models.StreamingEndpointAccessControl();
                            }
                            streamingendpoint.AccessControl.Akamai = form.GetStreamingAkamaiList;

                        }
                        else
                        {
                            if (streamingendpoint.AccessControl != null)
                            {
                                streamingendpoint.AccessControl.Akamai = null;
                            }

                        }
                    }

                    if (modifications.MaxCacheAge)
                    {
                        streamingendpoint.MaxCacheAge = form.MaxCacheAge;

                    }

                    // Client Access Policy
                    if (modifications.ClientAccessPolicy)
                    {
                        if (form.GetOriginClientPolicy != null)
                        {
                            if (streamingendpoint.CrossSiteAccessPolicies == null)
                            {
                                streamingendpoint.CrossSiteAccessPolicies = new Microsoft.Azure.Management.Media.Models.CrossSiteAccessPolicies();
                            }
                            streamingendpoint.CrossSiteAccessPolicies.ClientAccessPolicy = form.GetOriginClientPolicy;

                        }
                        else
                        {
                            if (streamingendpoint.CrossSiteAccessPolicies != null)
                            {
                                streamingendpoint.CrossSiteAccessPolicies.ClientAccessPolicy = null;
                            }
                        }
                    }

                    // Cross domain  Policy
                    if (modifications.CrossDomainPolicy)
                    {
                        if (form.GetOriginCrossdomaintPolicy != null)
                        {
                            if (streamingendpoint.CrossSiteAccessPolicies == null)
                            {
                                streamingendpoint.CrossSiteAccessPolicies = new Microsoft.Azure.Management.Media.Models.CrossSiteAccessPolicies();
                            }
                            streamingendpoint.CrossSiteAccessPolicies.CrossDomainPolicy = form.GetOriginCrossdomaintPolicy;

                        }
                        else
                        {
                            if (streamingendpoint.CrossSiteAccessPolicies != null)
                            {
                                streamingendpoint.CrossSiteAccessPolicies.CrossDomainPolicy = null;
                            }
                        }
                    }

                    if (modifications.Description)
                    {
                        streamingendpoint.Description = form.GetOriginDescription;
                    }

                    // Let's take actions now

                    if (modifications.StreamingUnits && streamingendpoint.ScaleUnits != form.GetScaleUnits)
                    {
                        Task.Run(async () =>
                       {
                           await DoUpdateAndScaleStreamingEndpointEngineAsync(streamingendpoint, form.GetScaleUnits);
                       });


                    }
                    else // no scaling
                    {
                        Task.Run(async () =>
                       {
                           await DoUpdateAndScaleStreamingEndpointEngineAsync(streamingendpoint);
                       });
                    }
                }
            }
        }


        private void DoStartStreamingEndpoints()
        {
            Task.Run(() =>
            {
                DoStartStreamingEndpointEngine(ReturnSelectedStreamingEndpoints());
            }
                   );
        }

        private void DoStopStreamingEndpoints()
        {
            Task.Run(() =>
            {
                DoStopOrDeleteStreamingEndpointsEngine(ReturnSelectedStreamingEndpoints(), false);
            }
                   );
        }

        private void DoDeleteStreamingEndpoints()
        {
            List<StreamingEndpoint> SelectedOrigins = ReturnSelectedStreamingEndpoints();
            if (SelectedOrigins.Count > 0)
            {
                string question = (SelectedOrigins.Count == 1) ? "Delete streaming endpoint " + SelectedOrigins[0].Name + " ?" : "Delete these " + SelectedOrigins.Count + " streaming endpoints ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Streaming endpoint(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Task.Run(() =>
                    {
                        DoStopOrDeleteStreamingEndpointsEngine(ReturnSelectedStreamingEndpoints(), true);
                    }
                  );
                }
            }
        }

        private void createOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateStreamingEndpoint();
        }

        private void DoCreateStreamingEndpoint()
        {
            CreateStreamingEndpoint form = new CreateStreamingEndpoint();
            StreamingEndpointCDNEnable cdnform = new StreamingEndpointCDNEnable();

            if (form.ShowDialog() == DialogResult.OK)
            {

                if (form.EnableAzureCDN)
                {
                    if (cdnform.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
                TextBoxLogWriteLine("Creating streaming endpoint {0}...", form.StreamingEndpointName);


                StreamingEndpoint newStreamingEndpoint = new StreamingEndpoint(name: form.StreamingEndpointName,
                                                                 scaleUnits: form.scaleUnits,
                                                                 description: form.StreamingEndpointDescription,
                                                                 cdnEnabled: form.EnableAzureCDN,
                                                                 cdnProvider: (form.EnableAzureCDN ? cdnform.ProviderSelected.ToString() : null),
                                                                 cdnProfile: (form.EnableAzureCDN ? cdnform.Profile : null),
                                                                 location: _amsClientV3.credentialsEntry.MediaService.Location
                                                                 );
                _amsClientV3.RefreshTokenIfNeeded();

                Task.Run(async () =>
                {

                    try
                    {
                        TextBoxLogWriteLine("Streaming endpoint creation...");
                        StreamingEndpoint secreated = await _amsClientV3.AMSclient.StreamingEndpoints.CreateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, form.StreamingEndpointName, newStreamingEndpoint);
                        TextBoxLogWriteLine("Streaming endpoint created.");

                    }
                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when creating a streaming endpoint", true);
                        TextBoxLogWriteLine(ex);
                    }
                    DoRefreshGridStreamingEndpointV(false);
                }
              );
            }
        }


        private void displayLiveEventInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayLiveEventInfo();
        }

        private void displayProgramInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDisplayLiveOutputInfo();
        }

        private void dataGridViewLiveV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                LiveEvent liveEvent = Task.Run(async () => await GetLiveEventAsync(dataGridViewLiveEventsV.Rows[e.RowIndex].Cells[dataGridViewLiveEventsV.Columns["Name"].Index].Value.ToString())).Result;

                if (liveEvent != null)
                {
                    DoDisplayLiveEventInfo((new List<LiveEvent>() { liveEvent }));
                }
            }
        }

        private void dataGridViewProgramV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                LiveOutput liveoutput = _amsClientV3.AMSclient.LiveOutputs.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["LiveEventName"].Index].Value.ToString(), dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString());
                if (liveoutput != null)
                {
                    DoDisplayLiveOutputInfo(new List<LiveOutput>() { liveoutput });
                }
            }
        }

        private void startLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartLiveEvents();
        }

        private void stopLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopOrDeleteLiveEvents(false);
        }

        private void resetLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoResetLiveEvents();
        }

        private void deleteLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopOrDeleteLiveEvents(true);
        }


        private void deleteProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await DoDeleteLiveOutputs();
            });
        }

        private void displayOriginInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDisplayStreamingEndpointInfo();
        }

        private void startOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartStreamingEndpoints();
        }

        private void stopOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopStreamingEndpoints();
        }

        private void deleteOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteStreamingEndpoints();
        }

        private void dataGridViewOriginsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                StreamingEndpoint se = Task.Run(async () => await GetStreamingEndpointAsync(dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[dataGridViewStreamingEndpointsV.Columns["Name"].Index].Value.ToString())).Result;

                if (se != null)
                {
                    DoDisplayStreamingEndpointInfo(new List<StreamingEndpoint>() { se });
                }
            }
        }

        private void DoPlaybackLiveEventPreview(PlayerType ptype)
        {
            foreach (LiveEvent liveEvent in ReturnSelectedLiveEvents())
            {
                if (liveEvent != null && liveEvent.Preview != null)
                {
                    if (liveEvent.Preview.Endpoints.FirstOrDefault() != null && liveEvent.Preview.Endpoints.FirstOrDefault().Url != null)
                    {
                        AssetInfo.DoPlayBackWithStreamingEndpoint(
                            typeplayer: ptype,
                            path: liveEvent.Preview.Endpoints.FirstOrDefault().Url,
                            DoNotRewriteURL: true,
                            client: _amsClientV3,
                            formatamp: AzureMediaPlayerFormats.Auto,
                            UISelectSEFiltersAndProtocols: false,
                            mainForm: this,
                            //selectedBrowser: Constants.BrowserIE[1],
                            launchbrowser: true
                            );
                    }
                    else
                    {
                        MessageBox.Show($"There is no active preview URL for live event '{liveEvent.Name}'. Maybe no data has arrived so no manifest is available.", "No preview URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }


        private void copyPreviewURLToClipboard_Click(object sender, EventArgs e)
        {
            LiveEvent liveEvent = ReturnSelectedLiveEvents().FirstOrDefault();
            if (liveEvent != null && liveEvent.Preview != null)
            {
                if (liveEvent.Preview.Endpoints.FirstOrDefault() != null && liveEvent.Preview.Endpoints.FirstOrDefault().Url != null)
                {
                    string preview = liveEvent.Preview.Endpoints.FirstOrDefault().Url;
                    EditorXMLJSON DisplayForm = new EditorXMLJSON("Preview URL", preview, false, false, false);
                    DisplayForm.Display();
                }
                else
                {
                    MessageBox.Show($"There is no active preview URL for live event '{liveEvent.Name}'. Maybe no data has arrived so no manifest is available.", "No preview URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void batchUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoBatchUpload();
        }

        private void DoBatchUpload()
        {
            BatchUploadFrame1 form = new BatchUploadFrame1();
            if (form.ShowDialog() == DialogResult.OK)
            {
                BatchUploadFrame2 form2 = new BatchUploadFrame2(form.BatchFolder, form.BatchProcessFiles, form.BatchProcessSubFolders, _amsClientV3) { Left = form.Left, Top = form.Top };
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);

                    Task.Run(async () =>
                    {
                        List<Task> MyTasks = new List<Task>();
                        int i = 0;
                        foreach (string folder in form2.BatchSelectedFolders)
                        {
                            i++;
                            TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(folder)), TransferType.UploadFromFolder, true);
                            //var myTask = Task.Factory.StartNew(() => ProcessUploadFromFolder(folder, response.Id, AssetCreationOptions.None, response.token, form2.StorageSelected), response.token);


                            IEnumerable<string> filePaths = Directory.EnumerateFiles(folder as string);

                            Task<Task> myTask = Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                                      filePaths.ToList(),
                                      response.Id,
                                      response.token,
                                      storageaccount: form2.StorageSelected,
                                      blocksize: form2.BlockSize
                                      ), response.token);

                            MyTasks.Add(myTask);

                            if (i == 10) // let's use a batch of 10 threads at the same time
                            {
                                do
                                {
                                    Task.Delay(1000).Wait();
                                }
                                while (ReturnTransfer(response.Id).State == TransferState.Queued);
                                i = 0;
                            }
                        }

                        foreach (string file in form2.BatchSelectedFiles)
                        {
                            i++;
                            TransferEntryResponse response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, true);

                            Task<Task> myTask = Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                                      new List<string>() { file },
                                      response.Id,
                                      response.token,
                                      storageaccount: form2.StorageSelected,
                                      blocksize: form2.BlockSize
                                      ), response.token);

                            MyTasks.Add(myTask);

                            if (i >= 10) // let's use a batch of 10 threads at the same time
                            {
                                do
                                {
                                    Task.Delay(1000).Wait();
                                }
                                while (ReturnTransfer(response.Id).State == TransferState.Queued);
                                i = 0;
                            }
                        }

                        try
                        {
                            await Task.WhenAll(MyTasks);
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine(ex);
                        }

                        // DoRefreshGridAssetV(false);
                    }
                       );
                }
            }
        }

        private void azureMediaBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkBlogAMS);
        }

        private void createProgramToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoCreateLiveOutput();
        }

        private void createLiveEventToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateLiveEvent();
        }

        private void comboBoxTimeProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewLiveOutputV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewLiveOutputV.TimeFilter == FilterTime.TimeRange)
            {
                TimeRangeSelection form = new TimeRangeSelection()
                {
                    TimeRange = dataGridViewLiveOutputV.TimeFilterTimeRange,
                    LabelMain = "Last Modified Time Range of Programs"
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewLiveOutputV.TimeFilterTimeRange = form.TimeRange;
                }
                else
                {
                    // user cancelled timerange box TODO
                }
            }

            if (dataGridViewLiveOutputV.Initialized)
            {
                DoRefreshGridLiveOutputV(false);
            }
        }

        private void buttonSetFilterProgram_Click(object sender, EventArgs e)
        {
            DoProgramSearch();
        }

        private void DoProgramSearch()
        {
            if (dataGridViewLiveOutputV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchProgramOption.SelectedItem as Item).Value);
                dataGridViewLiveOutputV.SearchInName = new SearchObject { Text = textBoxSearchNameProgram.Text, SearchType = stype };
                DoRefreshGridLiveOutputV(false);
            }
        }

        private void comboBoxStatusProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewLiveOutputV.Initialized)
            {
                dataGridViewLiveOutputV.FilterState = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridLiveOutputV(false);
            }
        }

        private void createStreamingEndpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateStreamingEndpoint();
        }


        private void richTextBoxLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void clearTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxLog.SelectionLength > 0)
            {
                System.Windows.Forms.Clipboard.SetText(richTextBoxLog.SelectedText.Replace("\n", "\r\n"));
            }
            else
            {
                System.Windows.Forms.Clipboard.SetText(richTextBoxLog.Text.Replace("\n", "\r\n"));
            }

        }



        private void createALocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateLocator(ReturnSelectedAssetsFromProgramsOrAssetsV3());
        }

        private void deleteAllLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsV3();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }


        private void DoDisplayOutputURLAssetOrProgramToWindow()
        {
            Asset asset = ReturnSelectedAssetsFromProgramsOrAssetsV3().FirstOrDefault();
            if (asset != null)
            {
                AssetInfo AI = new AssetInfo(asset);
                Uri ValidURI = AssetInfo.GetValidOnDemandURI(asset, _amsClientV3);
                if (ValidURI != null)
                {
                    string url = ValidURI.AbsoluteUri;
                    ChooseStreamingEndpoint form = new ChooseStreamingEndpoint(_amsClientV3, asset, url);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        url = AssetInfo.RW(form.UpdatedPath, form.SelectStreamingEndpoint, form.SelectedFilters, form.ReturnHttps, form.ReturnSelectCustomHostName, form.ReturnStreamingProtocol, form.ReturnHLSAudioTrackName, form.ReturnHLSNoAudioOnlyMode).ToString();
                    }
                    else
                    {
                        return;
                    }

                    EditorXMLJSON tokenDisplayForm = new EditorXMLJSON("Output URL", url, false, false, false);
                    tokenDisplayForm.Display();
                }
                else
                {
                    MessageBox.Show(string.Format("No valid URL is available for asset '{0}'.", asset.Name), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Asset not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void jwPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerJWPlayerPartnership);
        }

        private void withCustomPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.CustomPlayer);
        }

        private void DoMenuCreateLocatorOnPrograms()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssetsV3();
            DoCreateLocator(SelectedAssets);
            DoRefreshGridLiveOutputV(false);
        }

        private void createALocatorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoMenuCreateLocatorOnPrograms();
        }

        private void deleteAllLocatorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuDeleteAllLocatorsOnPrograms();
        }

        private void DoMenuDeleteAllLocatorsOnPrograms()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssetsV3();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
            DoRefreshGridLiveOutputV(false);
        }

        private void displayRelatedAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoOfProgram();
        }

        private void DoMenuDisplayAssetInfoOfProgram()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssetsV3();
            if (SelectedAssets.Count > 0)
            {
                DisplayInfo(SelectedAssets.FirstOrDefault());
            }
        }

        private void withCustomPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.CustomPlayer);
        }


        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoRefreshGridAssetV(false);
        }

        private void refreshToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoRefreshGridJobV(false);
        }

        private void refreshToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoRefreshGridLiveEventV(false);
        }

        private void refreshToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DoRefreshGridLiveOutputV(false);
        }

        private void refreshToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DoRefreshGridStreamingEndpointV(false);
        }

        private void displayErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayTransferError();
        }

        private void displayErrorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDisplayTransferError();
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private async void extendExistingLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            //await DoRefreshStreamingLocators();
        }


        private void attachAnotherStoragheAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoAttachAnotherStorageAccount();
        }

        private void DoAttachAnotherStorageAccount()
        {
            AttachStorage form = new AttachStorage(_amsClientV3);

            if (form.ShowDialog() == DialogResult.OK)
            {

                // Update storage accounts
                try
                {
                    TextBoxLogWriteLine("Processing Attach/Detach Storage account(s)...");
                    Task.Run(async () =>
                    {
                        await form.UpdateStorageAccountsAsync();
                    }
                    );

                    TextBoxLogWriteLine("Storage account attached/detached.");
                    DoRefreshGridStorageV(false);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when processing storage account attach/detach.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
        }

        private void DoDisplayJobError()
        {
            List<JobExtension> SelectedJobs = ReturnSelectedJobsV3();
            if (SelectedJobs.Count == 1)
            {
                JobExtension JobToDisplayP = SelectedJobs.FirstOrDefault();

                if (JobToDisplayP != null)
                {
                    IEnumerable<JobOutput> outputsError = JobToDisplayP.Job.Outputs.Where(o => o.State == Microsoft.Azure.Management.Media.Models.JobState.Error);
                    if (outputsError.Count() > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (JobOutput output in outputsError)
                        {
                            sb.AppendLine(output.Error.Code.ToString());
                            sb.AppendLine(output.Error.Message);
                        }
                        MessageBox.Show(sb.ToString(), "Error message(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void displayErrorToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoDisplayJobError();
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void azureManagementPortalToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void resubmitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void DoSelectTransformAndSubmitJob()
        {
            List<Asset> SelectedAssets = ReturnSelectedAssetsV3();

            //CheckAssetSizeRegardingMediaUnit(SelectedAssets);
            ProcessFromTransform2 form = new ProcessFromTransform2(_amsClientV3, this, SelectedAssets);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.SelectedAssetsMode) // assets selected
                {
                    CreateAndSubmitJobs(new List<Transform>() { form.SelectedTransform }, SelectedAssets, form.StartClipTime, form.EndClipTime);
                }
                else // http source url instead
                {
                    CreateAndSubmitJobs(new List<Transform>() { form.SelectedTransform }, form.GetURL.OriginalString, form.StartClipTime, form.EndClipTime);
                }

                // DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs);
            }
        }


        private void dataGridViewAssetsV_DragDrop(object sender, DragEventArgs e)
        {
            // Handle FileDrop data. 
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in  
                // case the user has selected multiple files. 
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop);

                List<string> folders = objects.Where(f => Directory.Exists(f)).ToList();
                List<string> files = objects.Where(f => !Directory.Exists(f)).ToList();

                foreach (string fold in folders)
                {
                    DoMenuUploadFromFolder_Step2(fold); // it's a folder
                }

                if (files.Count > 0)
                {
                    DoMenuUploadFromSingleFileS_Step2(files.ToArray()); // let's upload the objects as files, each file as an individual asset
                }
            }
        }

        private void dataGridViewAssetsV_DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file display the copy cursor. 
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoRefreshGridStorageV(false);
        }

        private void attachAnotherStorageAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoAttachAnotherStorageAccount();
        }

        private void dataGridViewV_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // on line on two is blue
            if (e.RowIndex % 2 == 0)
            {
                foreach (DataGridViewCell c in ((DataGridView)sender).Rows[e.RowIndex].Cells)
                {
                    c.Style.BackColor = Color.AliceBlue;
                }
            }
        }


        private void withAzureMediaPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer);
        }


        public void DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType playertype, List<Asset> listassets, string filter = null)
        {
            foreach (Asset myAsset in listassets)
            {
                if (myAsset != null)
                {
                    bool Error = false;
                    if (!IsThereALocatorValid(myAsset, ref PlayBackLocator, _amsClientV3)) // No streaming locator valid
                    {

                        if (MessageBox.Show(string.Format("There is no valid streaming locator for asset '{0}'.\nDo you want to create one (clear streaming) ?", myAsset.Name), "Streaming locator", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            _amsClientV3.RefreshTokenIfNeeded();

                            TextBoxLogWriteLine("Creating locator for asset '{0}'", myAsset.Name);
                            try
                            {
                                string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);

                                StreamingLocator locator = new StreamingLocator(
                                                                                assetName: myAsset.Name,
                                                                                streamingPolicyName: PredefinedStreamingPolicy.ClearStreamingOnly,
                                                                                defaultContentKeyPolicyName: null,
                                                                                streamingLocatorId: null
                                                                                );

                                locator = _amsClientV3.AMSclient.StreamingLocators.Create(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, "loc" + uniqueness, locator);

                                PlayBackLocator = _amsClientV3.AMSclient.Assets.ListStreamingLocators(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, myAsset.Name).StreamingLocators.Where(l => l.Name == locator.Name).FirstOrDefault();

                                dataGridViewAssetsV.PurgeCacheAsset(myAsset);
                                dataGridViewAssetsV.AnalyzeItemsInBackground();
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine("Error when creating locator for asset '{0}'", myAsset.Name, true); // this could happen if asset is storage protected with no delivery policy
                                TextBoxLogWriteLine(ex);
                                Error = true;
                            }
                        }
                    }

                    if (!Error && IsThereALocatorValid(myAsset, ref PlayBackLocator, _amsClientV3)) // There is a streaming locator valid
                    {
                        string MyUri = _amsClientV3.AMSclient.StreamingLocators.ListPaths(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, PlayBackLocator.Name)
                            .StreamingPaths.Where(p => p.StreamingProtocol == StreamingPolicyStreamingProtocol.SmoothStreaming)
                            .FirstOrDefault().Paths.FirstOrDefault();

                        if (MyUri != null)
                        {
                            AssetInfo.DoPlayBackWithStreamingEndpoint(playertype, MyUri, _amsClientV3, this, myAsset, false, filter, locator: PlayBackLocator);
                        }
                        else
                        {
                            /* v3 migration

                            // there is a streaming locator but the asset cannot be played back with adaptive streaming. It could be a single file in the asset.
                            // if this is a single MP4 file, we can play it with the streaming locator but as progressive download
                            if (myAsset.AssetFiles.Count() == 1 && myAsset.AssetFiles.FirstOrDefault().Name.ToLower().EndsWith(".mp4") && (playertype == PlayerType.AzureMediaPlayer))
                            {
                                MessageBox.Show(string.Format("The asset '{0}' in a single MP4 file and cannot be played with adaptive streaming as there is no manifest file.\nThe MP4 file will be played through progressive download.", myAsset.Name), "Single MP4 file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AssetInfo.DoPlayBackWithStreamingEndpoint(PlayerType.AzureMediaPlayer, PlayBackLocator.Path + myAsset.AssetFiles.FirstOrDefault().Name, _context, this, myAsset, formatamp: AzureMediaPlayerFormats.VideoMP4, UISelectSEFiltersAndProtocols: false);
                            }
                            else
                            {
                                MessageBox.Show(string.Format("The asset '{0}' does not seem to be playable with adaptive streaming.", myAsset.Name), "Adaptive streaming", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            */
                        }
                    }
                }
            }
        }

        private void DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType playertype)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(playertype, ReturnSelectedAssetsFromProgramsOrAssetsV3());
        }

        private void withAzureMediaPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer);
        }


        private void withAzureMediaPlayerToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DoPlaybackLiveEventPreview(PlayerType.AzureMediaPlayerClear);
        }

        private void hTML5CaptionMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.DemoCaptionMaker);
        }


        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }


        private void toAnotherAzureMediaServicesAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //   DoCopyAssetToAnotherAMSAccount();
        }
        /*
        private void DoCopyAssetToAnotherAMSAccount()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Any(a => AssetInfo.GetAssetType(a).StartsWith(AssetInfo.Type_LiveArchive) || AssetInfo.GetAssetType(a).StartsWith(AssetInfo.Type_Fragmented)))
            {
                MessageBox.Show("One of the source asset is fragmented (live stream, live archive or pre-fragmented asset)." + Constants.endline
                    + "It is not recommended to copy such asset with this command. While the copied asset will be streamable, you could have issues to download it or run a processor on it because some asset files will not be tagged as fragments containers." + Constants.endline + Constants.endline
                    + "It is recommended to use subclipping (all bitrates) and then to copy the multiple MP4 files asset with this command." + Constants.endline
                    , "Fragmented asset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            CopyAsset form = new CopyAsset(_context, SelectedAssets.Count, CopyAssetBoxMode.CopyAsset, _accountname)
            {
                CopyAssetName = string.Format("Copy of {0}", Constants.NameconvAsset),
                EnableSingleDestinationAsset = SelectedAssets.Count > 1
            };


            if (form.ShowDialog() == DialogResult.OK)
            {
                var newdestinationcredentials = form.DestinationLoginCredentials;

                // for service principal, the SP crednetials are asked in the previous form


                bool usercanceled = false;
                var storagekeys = BuildStorageKeyDictionary(SelectedAssets, newdestinationcredentials, ref usercanceled, _context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey, form.DestinationStorageAccount);
                if (!usercanceled)
                {
                    CloudMediaContext DestinationContext;
                    try
                    {
                        DestinationContext = Program.ConnectAndGetNewContext(newdestinationcredentials);
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error", true);
                        TextBoxLogWriteLine(ex);
                        return;
                    }

                    if (!form.SingleDestinationAsset) // standard mode: 1:1 asset copy
                    {
                        foreach (IAsset asset in SelectedAssets)
                        {
                            var response = DoGridTransferAddItem(string.Format("Copy asset '{0}' to account '{1}'", asset.Name, form.DestinationLoginCredentials.ReturnAccountName()), TransferType.ExportToOtherAMSAccount, false);
                            // Start a worker thread that does asset copy.
                            Task.Factory.StartNew(() =>
                            ProcessExportAssetToAnotherAMSAccount(newdestinationcredentials, form.DestinationStorageAccount, storagekeys, new List<IAsset>() { asset }, form.CopyAssetName.Replace(Constants.NameconvAsset, asset.Name), response, DestinationContext, form.DeleteSourceAsset, form.CopyDynEnc, form.RewriteLAURL, form.CloneAssetFilters, form.CloneLocators, form.UnpublishSourceAsset, form.CopyAlternateId), response.token);
                        }
                    }
                    else // merge all assets into a single asset
                    {
                        if (SelectedAssets.Any(a => a.Options != AssetCreationOptions.None))
                        {
                            MessageBox.Show("Assets cannot be merged as at least one asset is encrypted.", "Asset encrypted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            var response = DoGridTransferAddItem(string.Format("Copy several assets to account '{0}'", form.DestinationLoginCredentials.ReturnAccountName()), TransferType.ExportToOtherAMSAccount, false);
                            // Start a worker thread that does asset copy.
                            Task.Factory.StartNew(() =>
                            ProcessExportAssetToAnotherAMSAccount(newdestinationcredentials, form.DestinationStorageAccount, storagekeys, SelectedAssets, form.CopyAssetName.Replace(Constants.NameconvAsset, SelectedAssets.FirstOrDefault().Name), response, DestinationContext, form.DeleteSourceAsset), response.token);
                        }
                    }
                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                }
            }
        }
        */

        private void enableAzureCDNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeAzureCDN(true);
        }

        private void ChangeAzureCDN(bool enable)
        {
            StreamingEndpoint streamingendpoint = ReturnSelectedStreamingEndpoints().FirstOrDefault();

            if (streamingendpoint.ResourceState != StreamingEndpointResourceState.Stopped)
            {
                MessageBox.Show(string.Format("Streaming endpoint must be stopped in order to {0} CDN.", enable ? "enable" : "disable"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!enable)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to disable CDN on Streaming Endpoint '{0}' ?", streamingendpoint.Name), "Azure CDN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Task.Run(async () =>
                   {
                       streamingendpoint.CdnEnabled = false;
                       await DoUpdateAndScaleStreamingEndpointEngineAsync(streamingendpoint);
                   });
                }
            }
            else // enable
            {
                StreamingEndpointCDNEnable form = new StreamingEndpointCDNEnable();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Task.Run(async () =>
                    {
                        streamingendpoint.CdnEnabled = true;
                        streamingendpoint.CdnProvider = form.ProviderSelectedString;
                        streamingendpoint.CdnProfile = form.Profile;
                        await DoUpdateAndScaleStreamingEndpointEngineAsync(streamingendpoint);
                    });

                }
            }
        }

        private void disableAzureCDNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeAzureCDN(false);
        }


        private void contextMenuStripStreaminEndpoints_Opening(object sender, CancelEventArgs e)
        {
            // enable Azure CDN operation if one se selected and in stopped state
            ManageMenuOptionsAzureCDN(disableAzureCDNToolStripMenuItem, enableAzureCDNToolStripMenuItem);

            // telemetry
            loadToolStripMenuItem.Enabled = enableTelemetry;
        }

        private void originToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void ManageMenuOptionsAzureCDN(ToolStripMenuItem disableAzureCDNToolStripMenuItem1, ToolStripMenuItem enableAzureCDNToolStripMenuItem1)
        {
            // enable Azure CDN operation if one se selected and in stopped state
            List<StreamingEndpoint> streamingendpoints = ReturnSelectedStreamingEndpoints();

            if (streamingendpoints.Count == 1)
            {
                StreamingEndpoint se = streamingendpoints.FirstOrDefault();
                bool sestopped = (se.ResourceState == StreamingEndpointResourceState.Stopped);
                bool cdnenabled = (bool)se.CdnEnabled;

                disableAzureCDNToolStripMenuItem1.Enabled = sestopped && cdnenabled;
                enableAzureCDNToolStripMenuItem1.Enabled = sestopped && !cdnenabled;
                enableAzureCDNToolStripMenuItem1.Visible = !cdnenabled;
                disableAzureCDNToolStripMenuItem1.Visible = cdnenabled;
            }
            else // so the user can see the feature
            {
                disableAzureCDNToolStripMenuItem1.Enabled = false;
                enableAzureCDNToolStripMenuItem1.Enabled = false;
                enableAzureCDNToolStripMenuItem1.Visible = true;
                disableAzureCDNToolStripMenuItem1.Visible = true;
            }
        }

        private void toAnotherAzureMediaServicesAccountToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            //DoCopyAssetToAnotherAMSAccount();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            // DoExportAssetToAzureStorage();

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            // DoMenuImportFromAzureStorage();

        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            DoMenuUploadFromSingleFiles_Step1();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            DoMenuUploadFromFolder_Step1();
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            DoBatchUpload();
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
        }

        private void runALocalEncoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LiveEventRunOnPremisesLiveEncoder();
        }

        private void LiveEventRunOnPremisesLiveEncoder()
        {
            //  ChannelRunOnPremisesEncoder form = new ChannelRunOnPremisesEncoder(_context, ReturnSelectedChannels());
            //  form.ShowDialog();
        }

        private void runAnOnpremisesLiveEncoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LiveEventRunOnPremisesLiveEncoder();
        }


        private void DoCopyLiveEventInputURLToClipboard(object sender, EventArgs e)
        {
            int index = 0;
            if (sender.GetType() == typeof(ToolStrip))
            {
                ToolStrip send = (ToolStrip)sender;
                index = Convert.ToInt32(send.Name.Last().ToString()) - 1;
            }
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                ToolStripMenuItem send = (ToolStripMenuItem)sender;
                index = Convert.ToInt32(send.Name.Last().ToString()) - 1;
            }

            LiveEvent liveEvent = ReturnSelectedLiveEvents().FirstOrDefault();

            string absuri;
            if (index == 1 && liveEvent.Input.Endpoints.Count == 1 && liveEvent.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4) // Smooth https
            {
                absuri = liveEvent.Input.Endpoints[0].Url.Replace("http://", "https://");
            }
            else
            {
                absuri = liveEvent.Input.Endpoints[index].Url;
            }

            string label = string.Format("Input URL ({0})", index);
            EditorXMLJSON DisplayForm = new EditorXMLJSON(label, absuri, false, false, false);
            DisplayForm.Display();
        }


        private void ContextMenuItemLiveEventCopyIngestURLToClipboard_DropDownOpening(object sender, EventArgs e)
        {
            ContextMenuOpeningLiveEventCopyInputUrl();
        }

        private void ContextMenuOpeningLiveEventCopyInputUrl()
        {
            LiveEvent liveEvent = ReturnSelectedLiveEvents().FirstOrDefault();

            inputURLMToolStripMenuItem1.Visible = (liveEvent.Input.Endpoints.Count > 0);
            inputURLMToolStripMenuItem2.Visible = (liveEvent.Input.Endpoints.Count > 1) || (liveEvent.Input.Endpoints.Count == 1 && liveEvent.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4);
            inputURLMToolStripMenuItem3.Visible = (liveEvent.Input.Endpoints.Count > 2);
            inputURLMToolStripMenuItem4.Visible = (liveEvent.Input.Endpoints.Count > 3);

            inputURLMToolStripMenuItem1.Text = (liveEvent.Input.Endpoints.Count > 0) ? string.Format((string)inputURLMToolStripMenuItem1.Tag, new Uri(liveEvent.Input.Endpoints[0].Url).Scheme) : string.Empty;
            inputURLMToolStripMenuItem2.Text = (liveEvent.Input.Endpoints.Count > 1) ? string.Format((string)inputURLMToolStripMenuItem2.Tag, new Uri(liveEvent.Input.Endpoints[1].Url).Scheme) : string.Empty;
            inputURLMToolStripMenuItem3.Text = (liveEvent.Input.Endpoints.Count > 2) ? string.Format((string)inputURLMToolStripMenuItem3.Tag, new Uri(liveEvent.Input.Endpoints[2].Url).Scheme) : string.Empty;
            inputURLMToolStripMenuItem4.Text = (liveEvent.Input.Endpoints.Count > 3) ? string.Format((string)inputURLMToolStripMenuItem4.Tag, new Uri(liveEvent.Input.Endpoints[3].Url).Scheme) : string.Empty;

            if (liveEvent.Input.Endpoints.Count == 1 && liveEvent.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4) //Smooth https
            {
                inputURLMToolStripMenuItem2.Text = string.Format((string)inputURLMToolStripMenuItem2.Tag, new Uri(liveEvent.Input.Endpoints[0].Url.Replace("http://", "https://")).Scheme);
            }
        }


        private void contextMenuStripLiveEvents_Opening(object sender, CancelEventArgs e)
        {
            List<LiveEvent> liveEvents = ReturnSelectedLiveEvents();
            bool single = liveEvents.Count == 1;
            bool oneOrMore = liveEvents.Count > 0;

            // live event info
            ContextMenuItemLiveEventDisplayInfomation.Enabled = oneOrMore;

            // copy input url if only one live event
            ContextMenuItemLiveEventCopyIngestURLToClipboard.Enabled = single;

            // on premises encoder if only one live event
            ContextMenuItemLiveEventRunOnPremisesLiveEncoder.Enabled = single;

            // copy preview url if only one live event and preview is available
            ContextMenuItemLiveEventCopyPreviewURLToClipboard.Enabled = single && liveEvents.FirstOrDefault().Preview != null;

            // start, stop, reset, delete, clone live event
            ContextMenuItemLiveEventStart.Enabled = oneOrMore;
            ContextMenuItemLiveEventStop.Enabled = oneOrMore;
            ContextMenuItemLiveEventReset.Enabled = oneOrMore;
            cloneLiveEventsToolStripMenuItem.Enabled = false;// oneOrMore;
            ContextMenuItemLiveEventDelete.Enabled = oneOrMore;

            // playback preview
            playbackTheProgramToolStripMenuItem.Enabled = oneOrMore;

            // telemetry
            loadMetricsToolStripMenuItem.Enabled = false;// enableTelemetry;
        }

        private void liveLiveEventToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void contextMenuStripPrograms_Opening(object sender, CancelEventArgs e)
        {
            List<LiveOutput> liveOutputs = ReturnSelectedLiveOutputs();
            bool single = liveOutputs.Count == 1;
            bool oneOrMore = liveOutputs.Count > 0;

            // live output info if only one live output
            ContextMenuItemProgramDisplayInformation.Enabled = oneOrMore;

            // asset info if only one live output
            ContextMenuItemProgramDisplayRelatedAssetInformation.Enabled = single;

            // copy live output url if only one live output
            ContextMenuItemProgramCopyTheOutputURLToClipboard.Enabled = single;

            // delete live output
            ContextMenuItemProgramDelete.Enabled = oneOrMore;

            // publish
            publishToolStripMenuItem2.Enabled = oneOrMore;

            // playback
            ContextMenuItemProgramPlayback.Enabled = oneOrMore;

        }

        private void ContextMenuItemProgramCopyTheOutputURLToClipboard_Click(object sender, EventArgs e)
        {
            DoDisplayOutputURLAssetOrProgramToWindow();
        }

        private void buttonSetFilterLiveEvent_Click(object sender, EventArgs e)
        {
            DoLiveEventSearch();
        }

        private void DoLiveEventSearch()
        {
            if (dataGridViewLiveEventsV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchLiveEventOption.SelectedItem as Item).Value);
                dataGridViewLiveEventsV.SearchInName = new SearchObject { Text = textBoxSearchNameLiveEvent.Text, SearchType = stype };
                DoRefreshGridLiveEventV(false);
            }
        }

        private void comboBoxFilterTimeLiveEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewLiveEventsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewLiveEventsV.TimeFilter == FilterTime.TimeRange)
            {
                TimeRangeSelection form = new TimeRangeSelection()
                {
                    TimeRange = dataGridViewLiveEventsV.TimeFilterTimeRange,
                    LabelMain = "Last Modified Time Range of live events"
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewLiveEventsV.TimeFilterTimeRange = form.TimeRange;
                }
                else
                {
                    // user cancelled timerange box TODO
                }
            }

            if (dataGridViewLiveEventsV.Initialized)
            {
                DoRefreshGridLiveEventV(false);
            }
        }

        private void comboBoxStatusLiveEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewLiveEventsV.Initialized)
            {
                dataGridViewLiveEventsV.FilterState = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridLiveEventV(false);
            }
        }


        private void contextMenuStripStorage_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem12_Click_1(object sender, EventArgs e)
        {
            DoRefreshGridFiltersV(false);
        }

        private void toolStripMenuItem16_Click_1(object sender, EventArgs e)
        {
            DoCreateFilter();
        }

        private void DoCreateFilter()
        {
            DynManifestFilter form = new DynManifestFilter(_amsClientV3);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;
                    _amsClientV3.AMSclient.AccountFilters.CreateOrUpdate(
                        _amsClientV3.credentialsEntry.ResourceGroup,
                        _amsClientV3.credentialsEntry.AccountName,
                        filterinfo.Name,
                        new AccountFilter(presentationTimeRange: filterinfo.Presentationtimerange, firstQuality: filterinfo.Firstquality, tracks: filterinfo.Tracks)
                        );
                    // _context.Filters.Create(filterinfo.Name, filterinfo.Presentationtimerange, filterinfo.Tracks, filterinfo.Firstquality);
                    TextBoxLogWriteLine("Account filter '{0}' created.", filterinfo.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", (filterinfo != null && filterinfo.Name != null) ? filterinfo.Name : "unknown name", true);
                    TextBoxLogWriteLine(e);
                }
                DoRefreshGridFiltersV(false);
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteFilter();
        }

        private void DoDeleteFilter()
        {
            _amsClientV3.RefreshTokenIfNeeded();

            try
            {
                ReturnSelectedAccountFilters().ForEach(f => _amsClientV3.AMSclient.AccountFilters.Delete(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, f.Name));
            }
            catch (Exception e)
            {
                TextBoxLogWriteLine(e);
            }
            DoRefreshGridFiltersV(false);
        }

        private void filterInfoupdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoUpdateFilter();
        }

        private void DoUpdateFilter()
        {
            AccountFilter filter = ReturnSelectedAccountFilters().FirstOrDefault();
            DynManifestFilter form = new DynManifestFilter(_amsClientV3, filter);

            if (form.ShowDialog() == DialogResult.OK)
            {
                FilterCreationInfo filterinfotoupdate = null;
                try
                {
                    filterinfotoupdate = form.GetFilterInfo;

                    _amsClientV3.AMSclient.AccountFilters.CreateOrUpdate(
                        _amsClientV3.credentialsEntry.ResourceGroup,
                        _amsClientV3.credentialsEntry.AccountName,
                        filter.Name,
                        new AccountFilter(presentationTimeRange: filterinfotoupdate.Presentationtimerange, firstQuality: filterinfotoupdate.Firstquality, tracks: filterinfotoupdate.Tracks)
                        );
                    TextBoxLogWriteLine("Account filter '{0}' updated.", filter.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when updating filter '{0}'.", filter.Name, true);
                    TextBoxLogWriteLine(e);
                }
                DoRefreshGridFiltersV(false);
            }
        }

        private void dataGridViewFilters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DoUpdateFilter();
            }
        }

        private void contextMenuStripFilters_Opening(object sender, CancelEventArgs e)
        {
            List<AccountFilter> filters = ReturnSelectedAccountFilters();
            bool singleitem = (filters.Count == 1);
            filterInfoupdateToolStripMenuItem.Enabled = singleitem;
        }


        private void dataGridViewTransfer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void withAzureMediaPlayerToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }


        private void DoCreateAssetFilter()
        {
            Asset selasset = ReturnSelectedAssetsFromProgramsOrAssetsV3().FirstOrDefault();

            DynManifestFilter form = new DynManifestFilter(_amsClientV3, null, selasset);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;
                    _amsClientV3.AMSclient.AssetFilters.CreateOrUpdate
                        (
                        _amsClientV3.credentialsEntry.ResourceGroup,
                        _amsClientV3.credentialsEntry.AccountName,
                        selasset.Name,
                        filterinfo.Name,
                        new AssetFilter(presentationTimeRange: filterinfo.Presentationtimerange, firstQuality: filterinfo.Firstquality, tracks: filterinfo.Tracks)
                        )
                        ;
                    TextBoxLogWriteLine("Asset filter '{0}' created.", filterinfo.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", (filterinfo != null && filterinfo.Name != null) ? filterinfo.Name : "unknown name", true);
                    TextBoxLogWriteLine(e);
                }
                dataGridViewAssetsV.PurgeCacheAsset(selasset);
                dataGridViewAssetsV.AnalyzeItemsInBackground();
            }
        }


        private void DoDuplicateFilter()
        {
            List<AccountFilter> filters = ReturnSelectedAccountFilters();
            if (filters.Count == 1)
            {
                AccountFilter sourcefilter = filters.FirstOrDefault();

                string newfiltername = sourcefilter.Name + "Copy";
                if (Program.InputBox("New name", "Enter the name of the new duplicate filter:", ref newfiltername) == DialogResult.OK)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    try
                    {
                        _amsClientV3.AMSclient.AccountFilters.CreateOrUpdate(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, newfiltername, sourcefilter);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error when duplicating asset filter." + Constants.endline + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    DoRefreshGridFiltersV(false);
                }
            }
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDuplicateFilter();
        }

        private void createAnAssetFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateAssetFilter();
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            DoCreateAssetFilter();
        }

        private void withAzureMediaPlayerToolStripMenuItem2_DropDownOpening(object sender, EventArgs e)
        {

        }


        private void dataGridViewV_Resize(object sender, EventArgs e)
        {
            Program.dataGridViewV_Resize(sender);
        }

        private void cloneLiveEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewV_VisibleChanged(object sender, EventArgs e)
        {
            Program.dataGridViewV_Resize(sender);
        }

        private void subclipToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DoExportMetadata()
        {
            // ExportToExcel form = new ExportToExcel(_context, _accountname, ReturnSelectedAssets(), dataGridViewAssetsV.assets);
            // if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void informationToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoExportMetadata();
        }

        private void exportAssetsInformationToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoExportMetadata();
        }


        private void DoStorageVersion(string storageId = null)
        {
            string valuekey = string.Empty;
            bool Error = false;
            ServiceProperties serviceProperties = null;
            CloudBlobClient blobClient = null;

            if (storageId == null)
            {
                storageId = ReturnSelectedStorage().Id;
            }

            try
            {
                valuekey = _amsClientV3.GetStorageKey(storageId);
                if (valuekey == null)
                {
                    if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + AMSClientV3.GetStorageName(storageId) + ":", ref valuekey, true) != DialogResult.OK)
                    {
                        Error = true;
                    }
                }
                if (!Error)
                {
                    CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(AMSClientV3.GetStorageName(storageId), valuekey), _amsClientV3.environment.ReturnStorageSuffix(), true);
                    blobClient = storageAccount.CreateCloudBlobClient();

                    // Get the current service properties
                    serviceProperties = blobClient.GetServiceProperties();
                }
            }
            catch (Exception ex)
            {
                Error = true;
                MessageBox.Show(ex.Message, "Error accessing the storage account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextBoxLogWriteLine(ex);
            }

            if (!Error)
            {
                StorageSettings form = new StorageSettings(AMSClientV3.GetStorageName(storageId), serviceProperties);

                if (form.ShowDialog() == DialogResult.OK)
                {

                    // Set the default service version to 2011-08-18 (or a higher version like 2012-03-01)
                    //serviceProperties.DefaultServiceVersion = "2011-08-18";
                    try
                    {
                        TextBoxLogWriteLine("Setting storage version to '{0}', Metrics to level '{1}' and {2} days retention  ...",
                            form.RequestedStorageVersion ?? StorageSettings.noversion,
                            form.RequestedMetricsLevel.ToString(),
                            form.RequestedMetricsRetention ?? 0
                            );
                        serviceProperties.DefaultServiceVersion = form.RequestedStorageVersion;
                        serviceProperties.HourMetrics.MetricsLevel = form.RequestedMetricsLevel;
                        serviceProperties.HourMetrics.RetentionDays = form.RequestedMetricsRetention;

                        // Save the updated service properties
                        blobClient.SetServiceProperties(serviceProperties);
                        TextBoxLogWriteLine("Storage settings applied.");
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error when setting the storage version.", true);
                        TextBoxLogWriteLine(ex);
                    }
                }
            }
        }


        private void helpToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            explorerReleaseNotesToolStripMenuItem.Enabled = (Constants.LinkAMSEReleaseNotes != null);
        }

        private void explorerReleaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process.Start(Constants.LinkAMSEReleaseNotes);
        }


        private void copyReportToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayJobReport();
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }

        private void copyToClipboardToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoDisplayAssetReport();
        }

        private void visibleAssetsInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DoDeleteAssets(dataGridViewAssetsV.assets.ToList());
        }

        private void deleteVisibleAssetsInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DoDeleteAssets(dataGridViewAssetsV.assets.ToList());
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDeleteSelectedAssets();
        }

        private void deleteAllAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DoDeleteAllAssets();
        }

        private void visibleJobsInGridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteJobs(dataGridViewJobsV.ReturnSelectedJobs());
        }

        private void allJobsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteAllJobs();
        }

        private void selectedJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteJobs(dataGridViewJobsV.ReturnSelectedJobs());
        }

        private void dataGridViewStorage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string storageId = dataGridViewStorage.Rows[e.RowIndex].Cells[dataGridViewStorage.Columns["Id"].Index].Value.ToString();
                DoStorageVersion(storageId);
            }
        }

        private void textBoxSearchNameProgram_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearchNameProgram.Text))
            {
                CheckboxAnyLiveEventChangedByCode = true;
                SetRadiobuttonDisplayProgram(backupCheckboxAnyLiveEvent);
                radioButtonChAll.Enabled = radioButtonChNone.Enabled = radioButtonChSelected.Enabled = true;
            }
            else if (radioButtonChAll.Checked) // not empty and checkbox is still enabled
            {
                CheckboxAnyLiveEventChangedByCode = true;
                backupCheckboxAnyLiveEvent = ReturnDisplayProgram();
                SetRadiobuttonDisplayProgram(enumDisplayProgram.Any);
                radioButtonChAll.Enabled = radioButtonChNone.Enabled = radioButtonChSelected.Enabled = false;
            }
        }

        private void linkLabelFeedbackAMS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }


        private void dataGridViewV_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridView DG = (DataGridView)sender;

            if (DG.SortedColumn != null && DG.SortOrder != SortOrder.None)
            {
                SortOrder sortOrder = DG.SortOrder;
                DataGridViewColumn dataGridViewColumn = DG.Columns[DG.SortedColumn.Name];
                if (dataGridViewColumn != null)
                {
                    string strColumnName = dataGridViewColumn.Name;
                    DataGridViewColumn col = DG.Columns[strColumnName];
                    DG.Sort(col,
                        sortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
                }
            }
            else
            {
                DG.Sort(DG.Columns["Name"], ListSortDirection.Ascending);
            }
        }


        private void DoCheckIntegrityLiveArchive()
        {
            List<Asset> assets = ReturnSelectedAssetsFromProgramsOrAssetsV3();

            string question = (assets.Count == 1) ? string.Format("Check the integrity of '{0}' ?", assets[0].Name) : string.Format("Check the integrity of these {0} archives ?", assets.Count);
            if (System.Windows.Forms.MessageBox.Show(question, "Integrity check", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                bool usercanceled = false;
                //var storagekeys = BuildStorageKeyDictionary(assets, null, ref usercanceled, _context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey, null);

                if (!usercanceled)
                {
                    Task.Run(() =>
                    {
                        //assets.ForEach(asset => CheckListArchiveBlobs(storagekeys, asset, AssetInfo.GetManifestSegmentsList(asset)));
                    });
                }
            }
        }

        private void toolStripMenuItem37_Click_1(object sender, EventArgs e)
        {
            DoMenuDownloadToLocal();
        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e)
        {
            DoMenuDownloadToLocal();

        }


        private void editAlternateIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEditAssetAltId();
        }


        private enumDisplayProgram ReturnDisplayProgram()
        {
            if (radioButtonChAll.Checked)
            {
                return enumDisplayProgram.Any;
            }
            else if (radioButtonChNone.Checked)
            {
                return enumDisplayProgram.None;
            }
            else
            {
                return enumDisplayProgram.Selected;
            }
        }

        private void SetRadiobuttonDisplayProgram(enumDisplayProgram value)
        {
            switch (value)
            {
                case enumDisplayProgram.Any:
                    radioButtonChAll.Checked = true;
                    break;

                case enumDisplayProgram.None:
                    radioButtonChNone.Checked = true;
                    break;

                case enumDisplayProgram.Selected:
                    radioButtonChSelected.Checked = true;
                    break;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridViewLiveOutputV.Initialized && !CheckboxAnyLiveEventChangedByCode)
            {
                dataGridViewLiveOutputV.DisplayLiveEvent = ReturnDisplayProgram();

                Task.Run(() =>
                {
                    DoRefreshGridLiveOutputV(false);
                });
            }
            CheckboxAnyLiveEventChangedByCode = false;
        }


        private void toolStripMenuItem38_Click_2(object sender, EventArgs e)
        {
            DoDisplayOutputURLAssetOrProgramToWindow();

        }


        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            DoCheckIntegrityLiveArchive();
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            // DoCopyAssetToAnotherAMSAccount();
        }


        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCancelTransfer();
        }

        private void DoCancelTransfer()
        {
            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selRow in dataGridViewTransfer.SelectedRows)
                {
                    Guid guid = (Guid)selRow.Cells[dataGridViewTransfer.Columns["Id"].Index].Value;
                    DoGridTransferCancelTask(guid);
                }
            }
        }

        private void cancelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCancelTransfer();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoClearTransferts();
        }

        private void DoClearTransferts()
        {
            DoGridTransferClearCompletedTransfers();
        }

        private void clearCompletedTransfersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoGridTransferClearCompletedTransfers();
        }


        private void filesToSelectedAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuUploadFileToAsset_Step1();
        }

        private void trackBarConcurrentTransfers_Scroll(object sender, EventArgs e)
        {
            UpdateLabelConcurrentTransfers();
        }

        private void UpdateLabelConcurrentTransfers()
        {
            labelConcurrentTransfers.Text = string.Format(Constants.strTransfers, trackBarConcurrentTransfers.Value == Constants.MaxTransfersAsUnlimited ? "Unlimited" : "Limited to " + trackBarConcurrentTransfers.Value.ToString(), trackBarConcurrentTransfers.Value > 1 ? "s" : string.Empty);
            Properties.Settings.Default.ConcurrentTransfers = trackBarConcurrentTransfers.Value;
            Program.SaveAndProtectUserConfig();
        }

        private void analyzeAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            DoMenuImportFromHttp();
        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamingEndpoint SE = ReturnSelectedStreamingEndpoints().FirstOrDefault();
            if (SE != null)
            {
                //var form = new DisplayTelemetry(this, SE, _context, _credentials);
                //form.Show();
            }
        }

        private void loadMetricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void fromAzureStoragecontainerSASUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorageSASContainer();
        }

        private void fromAzureStorageSASContainerPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorageSASContainer();
        }

        private void DoMenuImportFromAzureStorageSASContainer()
        {
            ImportHttp form = new ImportHttp(_amsClientV3, true);

            if (form.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Import from SAS Container Path '{0}'", form.GetURL.LocalPath), TransferType.ImportFromHttp, false);
                    // Start a worker thread that does uploading.
                    // ProcessHttpSourceV3
                    Task<Task> myTask = Task.Factory.StartNew(() => ProcessHttpSASV3(form.GetURL, response.Id, response.token, form.StorageSelected, form.GetAssetName, form.GetAssetDescription), response.token);

                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }

            }
        }

        private void tHEOPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerTHEOplayerPartnership);
        }

        private void azureMediaServicesReleaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkMoreInfoAMSReleaseNotes);
        }

        private void selectedJobsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoCancelJobs();
        }

        private void allJobsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoCancelAllJobs();
        }

        private void linkLabelMoreInfoMediaUnits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void textBoxAssetSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonAssetSearch_Click(this, new EventArgs());
            }
        }

        private void textBoxJobSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonJobSearch_Click(this, new EventArgs());
            }
        }

        private void textBoxSearchNameLiveEvent_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetFilterLiveEvent_Click(this, new EventArgs());
            }
        }

        private void textBoxSearchNameProgram_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetFilterProgram_Click(this, new EventArgs());
            }
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkReportBugAMSE);
        }

        private void dataGridViewTransformsV_SelectionChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("transform selection changed : begin");
            List<Transform> SelectedTransforms = dataGridViewTransformsV.ReturnSelectedTransforms();
            if (SelectedTransforms.Count == 1)
            {
                dataGridViewJobsV.TransformSourceNames = SelectedTransforms.Select(c => c.Name).ToList();

                Task.Run(() =>
                {
                    Debug.WriteLine("transform selection changed : before refresh");
                    DoRefreshGridJobV(false);
                });
            }
        }

        private void dataGridViewTransformsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow row = dataGridViewTransformsV.Rows[e.RowIndex];
                Transform transform = Task.Run(async () => await GetTransformAsync(row.Cells[dataGridViewTransformsV.Columns["Name"].Index].Value.ToString())).Result;

                if (transform != null)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        if (DisplayInfo(transform) == DialogResult.OK)
                        {
                        }
                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                    }
                }
            }
        }


        public void CreateVideoAnalyzerTransform()
        {
            PresetVideoAnalyzer form = new PresetVideoAnalyzer();

            if (form.ShowDialog() == DialogResult.OK)
            {
                TransformOutput[] outputs;

                if (form.AudioOnlyMode)
                {
                    outputs = new TransformOutput[]
                                                     {
                                                                new TransformOutput( new AudioAnalyzerPreset( ){ AudioLanguage=form.Language  }),
                                                     };
                }
                else // video mode
                {
                    outputs = new TransformOutput[]
                                                       {
                                                                new TransformOutput( new VideoAnalyzerPreset( ){ AudioLanguage=form.Language  }),
                                                       };
                }

                try
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    // Create the Transform with the output defined above
                    Transform transform = _amsClientV3.AMSclient.Transforms.CreateOrUpdate(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform {0} created.", transform.Name); // Warning

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", ex); // Warning
                }

                DoRefreshGridTransformV(false);
            }
        }

        public void CreateFaceDetectorTransform()
        {
            PresetFaceDetector form = new PresetFaceDetector();

            if (form.ShowDialog() == DialogResult.OK)
            {
                TransformOutput[] outputs;

                outputs = new TransformOutput[]
                                                 {
                                                                new TransformOutput( new FaceDetectorPreset( ){ Resolution =  form.AnalysisResolutionMode    }),
                                                 };

                try
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    // Create the Transform with the output defined above
                    Transform transform = _amsClientV3.AMSclient.Transforms.CreateOrUpdate(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform {0} created.", transform.Name); // Warning

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", ex); // Warning
                }

                DoRefreshGridTransformV(false);
            }
        }

        public void CreateStandardEncoderTransform()
        {
            PresetStandardEncoder form = new PresetStandardEncoder();

            if (form.ShowDialog() == DialogResult.OK)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                TransformOutput[] outputs;

                outputs = new TransformOutput[]
                                                 {
                                                                new TransformOutput( new BuiltInStandardEncoderPreset( ){ PresetName= form.BuiltInPreset }),
                                                 };

                try
                {
                    // Create the Transform with the output defined above
                    Transform transform = _amsClientV3.AMSclient.Transforms.CreateOrUpdate(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform {0} created.", transform.Name); // Warning

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", ex); // Warning
                }

                DoRefreshGridTransformV(false);
            }
        }

        private void toolStripMenuItem32_DropDownOpening(object sender, EventArgs e)
        {
            /*
            var sel = ReturnSelectedTransforms();
            if (sel.Count > 0)
            {
                fromHttpsSourceWithSelectedTransformToolStripMenuItem.Text = "From http(s) source with selected transform : " + string.Join(", ", ReturnSelectedTransforms().Select(t => t.Name));
            }
            else
            {
                fromHttpsSourceWithSelectedTransformToolStripMenuItem.Text = "From http(s) source with selected transform : (no selection)";
            }
            */
        }

        private void CreateAndSubmitJobs(List<Transform> sel, List<Asset> assets, ClipTime start = null, ClipTime end = null)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (Asset asset in assets)
            {
                foreach (Transform transform in sel)
                {
                    string uniqueness = Guid.NewGuid().ToString("N");
                    string jobName = $"job-{uniqueness}";
                    string outputAssetName = $"output-{uniqueness}";

                    JobInputAsset jobInput = new JobInputAsset(asset.Name, start: start, end: end);

                    try
                    {


                        Asset outputAsset = _amsClientV3.AMSclient.Assets.CreateOrUpdate(
                                                                    _amsClientV3.credentialsEntry.ResourceGroup,
                                                                    _amsClientV3.credentialsEntry.AccountName,
                                                                    outputAssetName,
                                                                    new Asset()
                                                                    );


                        JobOutput[] jobOutputs =
                         {
                    new JobOutputAsset(outputAsset.Name),
                };
                        Job job = _amsClientV3.AMSclient.Jobs.Create(
                                                                    _amsClientV3.credentialsEntry.ResourceGroup,
                                                                    _amsClientV3.credentialsEntry.AccountName,
                                                                    transform.Name,
                                                                    jobName,
                                                                    new Job
                                                                    {
                                                                        Input = jobInput,
                                                                        Outputs = jobOutputs,
                                                                    });
                        TextBoxLogWriteLine("Job {0} created.", job.Name); // Warning

                        dataGridViewJobsV.DoJobProgress(new JobExtension() { Job = job, TransformName = transform.Name });
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error when creating output asset or submitting the job.", ex); // Warning
                    }
                }
            }
            DoRefreshGridJobV(false);
        }

        private void CreateAndSubmitJobs(List<Transform> sel, string url, ClipTime start = null, ClipTime end = null)
        {
            _amsClientV3.RefreshTokenIfNeeded();


            foreach (Transform transform in sel)
            {
                string uniqueness = Guid.NewGuid().ToString("N");
                string jobName = $"job-{uniqueness}";
                string outputAssetName = $"output-{uniqueness}";

                JobInputHttp jobInput = new JobInputHttp(files: new[] { url }, start: start, end: end);

                try
                {


                    Asset outputAsset = _amsClientV3.AMSclient.Assets.CreateOrUpdate(
                                                                _amsClientV3.credentialsEntry.ResourceGroup,
                                                                _amsClientV3.credentialsEntry.AccountName,
                                                                outputAssetName,
                                                                new Asset()
                                                                );


                    JobOutput[] jobOutputs =
                     {
                    new JobOutputAsset(outputAsset.Name),
                };
                    Job job = _amsClientV3.AMSclient.Jobs.Create(
                                                                _amsClientV3.credentialsEntry.ResourceGroup,
                                                                _amsClientV3.credentialsEntry.AccountName,
                                                                transform.Name,
                                                                jobName,
                                                                new Job
                                                                {
                                                                    Input = jobInput,
                                                                    Outputs = jobOutputs,
                                                                });
                    TextBoxLogWriteLine("Job {0} created.", job.Name); // Warning

                    dataGridViewJobsV.DoJobProgress(new JobExtension() { Job = job, TransformName = transform.Name });
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating output asset or submitting the job.", ex); // Warning
                }
            }

            DoRefreshGridJobV(false);
        }


        private void deleteTransformsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteTransforms(ReturnSelectedTransforms());
        }

        private void dataGridViewTransformsV_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // on line on two is blue
            if (e.RowIndex % 2 == 0)
            {
                foreach (DataGridViewCell c in ((DataGridView)sender).Rows[e.RowIndex].Cells)
                {
                    c.Style.BackColor = Color.AliceBlue;
                }
            }
        }


        private void videoAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateVideoAnalyzerTransform();
        }

        private void mediaEncoderStandardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateStandardEncoderTransform();
        }

        private void createJobUsingAnHttpSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateJobFromTransformUsingHttpSource();
        }

        private void CreateJobFromTransformUsingHttpSource()
        {
            List<Transform> sel = ReturnSelectedTransforms();

            //CheckAssetSizeRegardingMediaUnit(SelectedAssets);
            ProcessFromTransform2 form = new ProcessFromTransform2(_amsClientV3, this, null, sel)
            {
                //ProcessingPromptText = (SelectedAssets.Count > 1) ? string.Format("{0} assets have been selected. 1 job will be submitted.", SelectedAssets.Count) : string.Format("Asset '{0}' will be encoded.", SelectedAssets.FirstOrDefault().Name),
                Text = "Template based processing"
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                CreateAndSubmitJobs(new List<Transform>() { form.SelectedTransform }, form.GetURL.OriginalString, form.StartClipTime, form.EndClipTime);

                // DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs);
            }
        }

        private void fromHttpsSourceWithSelectedTransformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateJobFromTransformUsingHttpSource();
        }

        private void selectATransformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSelectTransformAndSubmitJob();
        }

        private void storageSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStorageVersion();
        }

        private void dataGridViewAssetsV_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridViewAssetsV.ReLaunchAnalyze();

        }

        private void dataGridViewAssetsV_SizeChanged(object sender, EventArgs e)
        {
            dataGridViewAssetsV.ReLaunchAnalyze();

        }

        private void createASASUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateSASUrl(ReturnSelectedAssetsFromProgramsOrAssetsV3());

        }

        private void faceDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFaceDetectorTransform();
        }


        private void toolStripMenuItemAzureUpdates_Click_1(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkAzureUpdates);

        }

        private void ButtonUpdateEncodingRU_Click(object sender, EventArgs e)
        {

            DoUpdateMediaRU();
        }

        private async void DoUpdateMediaRU()
        {
            bool oktocontinue = true;

            if (trackBarEncodingRU.Value == 0 && (((Item)comboBoxEncodingRU.SelectedItem).Name != "S1"))
            // user selected 0 with a non S1 hardware...
            {
                if (MessageBox.Show("You selected 0 unit but the encoding type is not S1. Are you sure you want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    oktocontinue = false;
                }
            }

            if (oktocontinue)
            {
                TextBoxLogWriteLine(string.Format("Updating to {0} {1} Media Reserved Unit{2}...", trackBarEncodingRU.Value, ((Item)comboBoxEncodingRU.SelectedItem).Name, trackBarEncodingRU.Value > 1 ? "s" : string.Empty));

                trackBarEncodingRU.Enabled = false;
                comboBoxEncodingRU.Enabled = false;
                buttonUpdateEncodingRU.Enabled = false;


                try
                {
                    await _mediaRUContext.SetMediaRU(_amsClientV3, trackBarEncodingRU.Value, int.Parse(((Item)comboBoxEncodingRU.SelectedItem).Value));
                    TextBoxLogWriteLine("Media Reserved Unit(s) updated.");

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when updating Media Reserved Unit(s).", true);
                    TextBoxLogWriteLine(ex);
                }

                DoRefreshGridJobV(false);
                trackBarEncodingRU.Enabled = true;
                comboBoxEncodingRU.Enabled = true;
                buttonUpdateEncodingRU.Enabled = true;
            }
        }

        private void TrackBarEncodingRU_Scroll(object sender, EventArgs e)
        {
            UpdateLabelProcessorUnits();
        }

        private void UpdateLabelProcessorUnits()
        {
            labelnbunits.Text = string.Format(Constants.strUnits, trackBarEncodingRU.Value, trackBarEncodingRU.Value > 1 ? "s" : string.Empty);
        }

        private void TrackBarEncodingRU_ValueChanged(object sender, EventArgs e)
        {
            RUEncodingUpdateControls();
        }

        private void RUEncodingUpdateControls()
        {
            // If RU is set to 0, let's switch to S1
            if (trackBarEncodingRU.Value == 0)
            {

                comboBoxEncodingRU.SelectedIndex = 0;


            }
        }
    }
}



namespace AMSExplorer
{
    public static class OrderAssets
    {
        public const string CreatedDescending = "Created >";
        public const string CreatedAscending = "Created <";
        public const string NameDescending = "Name >";
        public const string NameAscending = "Name <";

    }

    public static class OrderJobs
    {
        public const string CreatedDescending = "Created >";
        public const string CreatedAscending = "Created <";
        public const string NameDescending = "Name >";
        public const string NameAscending = "Name <";
    }


    public static class FilterTime
    {
        // public const string First50Items = "First 50 items";
        public const string AllItems = "All items";
        public const string LastDay = "Last 24 hours";
        public const string LastWeek = "Last week";
        public const string LastMonth = "Last month";
        public const string LastYear = "Last year";
        public const string TimeRange = "Time Range";

        public static int ReturnNumberOfDays(string timeFilter)
        {
            int days = -2;
            if (timeFilter != null)
            {
                switch (timeFilter)
                {
                    case FilterTime.LastDay:
                        days = 1;
                        break;
                    case FilterTime.LastWeek:
                        days = 7;
                        break;
                    case FilterTime.LastMonth:
                        days = 30;
                        break;
                    case FilterTime.LastYear:
                        days = 365;
                        break;

                    case FilterTime.TimeRange:
                        days = -1;
                        break;

                    default:
                        break;
                }
            }
            return days;
        }
    }

    public class TimeRangeValue
    {
        public DateTime StartDate;
        public DateTime? EndDate;

        public TimeRangeValue(DateTime start, DateTime? end = null)
        {
            StartDate = start;
            EndDate = end;
        }
    }


    public enum TransferState
    {
        Queued = 0,
        Processing,
        Cancelling,
        Cancelled,
        Finished,
        Error
    }

    public enum TransferType
    {
        UploadFromFile = 0,
        UploadFromFolder,
        ImportFromAzureStorage,
        ImportFromHttp,
        ExportToOtherAMSAccount,
        ExportToAzureStorage,
        DownloadToLocal,
        UploadWithExternalTool
    }
}
