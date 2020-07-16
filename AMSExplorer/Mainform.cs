//----------------------------------------------------------------------------------------------
//    Copyright 2020 Microsoft Corporation
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
using Microsoft.Rest.Azure;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Azure.Storage.Blob;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Azure.Storage.Shared.Protocol;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage;

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
        private AssetStreamingLocator PlayBackLocator = null;

        //Watch folder vars
        private readonly Dictionary<string, DateTime> seen = new Dictionary<string, DateTime>();

        private readonly System.Timers.Timer TimerAutoRefresh;
        private bool DisplaySplashDuringLoading;

        private bool CheckboxAnyLiveEventChangedByCode = false;

        private const int maxNbJobs = 50000;
        private readonly bool enableTelemetry = true;

#pragma warning disable CS0414 // The field 'Mainform.OneGB' is assigned but its value is never used
        private static readonly long OneGB = 1000L * 1000L * 1000L;
#pragma warning disable CS0414 // The field 'Mainform.S1AssetSizeLimit' is assigned but its value is never used
        private static readonly int S1AssetSizeLimit = 26; // GBytes
#pragma warning restore CS0414 // The field 'Mainform.S1AssetSizeLimit' is assigned but its value is never used
        private static readonly int S2AssetSizeLimit = 60; // GBytes
#pragma warning restore CS0414 // The field 'Mainform.S2AssetSizeLimit' is assigned but its value is never used
        private static readonly int S3AssetSizeLimit = 260; // GBytes
#pragma warning restore CS0414 // The field 'Mainform.S3AssetSizeLimit' is assigned but its value is never used
        public string _accountname;
        private static AMSClientV3 _amsClient;
        private readonly MediaRU _mediaRUContext;
        private const string resetcredentials = "/resetcredentials";

        public bool MediaRUFeatureOn = true;

        public Mainform(string[] args)
        {
            this.Font = new Font("Segoe UI", 9);
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
            _amsClient = formLogin.AmsClient;

            _accountname = _amsClient.credentialsEntry.AccountName;
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
                var seResults = Task.Run(() => _amsClient.AMSclient.StreamingEndpoints.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).GetAwaiter().GetResult();

                //Microsoft.Rest.Azure.IPage<StreamingEndpoint> se = _amsClientV3.AMSclient.StreamingEndpoints.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);

                if (seResults.AsEnumerable().Where(o => o.ResourceState == StreamingEndpointResourceState.Running).ToList().Count == 0)
                {
                    TextBoxLogWriteLine("There is no streaming endpoint running in this account.", true); // Warning
                }

                var leResults = Task.Run(() => _amsClient.AMSclient.LiveEvents.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).GetAwaiter().GetResult();
                double nbLiveEvents = leResults.Count();
                double nbse = seResults.Count();
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

            // let's check the encoding reserved unit and type
            try
            {
                // Management of media reserved units
                _mediaRUContext = new MediaRU();
                InfoMediaRU result = _mediaRUContext.GetInfoMediaRU(_amsClient);

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

            string mes = @"To use Azure CLI with this account, use a syntax like : ""az ams asset list -g {0} -a {1}""";
            TextBoxLogWriteLine(mes, _amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
        }



        // Test
        private async Task<object> ListEventsRest()
        {
            // tenants browsing
            // GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Media/mediaservices/{accountName}/liveEvents?api-version=2018-07-01
            string URL = _amsClient.environment.ArmEndpoint + string.Format("subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Media/mediaservices/{2}/liveEvents?api-version=2018-07-01",
                _amsClient.credentialsEntry.AzureSubscriptionId,
                _amsClient.credentialsEntry.ResourceGroup,
                  _amsClient.credentialsEntry.AccountName
                );

            string token = _amsClient.accessTokenForRestV2 != null ? _amsClient.accessTokenForRestV2.AccessToken :
                TokenCache.DefaultShared.ReadItems()
        .Where(t => t.ClientId == _amsClient.credentialsEntry.ADSPClientId)
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

        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            await DoRefreshAsync();
        }

        public void Notify(string title, string text, bool Error = false)
        {
            if (Properties.Settings.Default.HideTaskbarNotifications == false)
            {
                notifyIcon1.ShowBalloonTip(3000, title, text, Error ? ToolTipIcon.Error : ToolTipIcon.Info);
            }
        }

        private static async Task<Asset> GetAssetAsync(string assetName, CancellationToken token = default)
        {
            await _amsClient.RefreshTokenIfNeededAsync().ConfigureAwait(false);
            return await _amsClient.AMSclient.Assets.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, assetName, token).ConfigureAwait(false);
        }

        private static async Task<Job> GetJobAsync(string transformName, string jobName)
        {
            await _amsClient.RefreshTokenIfNeededAsync().ConfigureAwait(false); ;
            return await _amsClient.AMSclient.Jobs.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, transformName, jobName).ConfigureAwait(false);
        }

        private static async Task<Transform> GetTransformAsync(string transformName)
        {
            await _amsClient.RefreshTokenIfNeededAsync().ConfigureAwait(false); ;
            return await _amsClient.AMSclient.Transforms.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, transformName).ConfigureAwait(false);
        }

        private static async Task<LiveEvent> GetLiveEventAsync(string liveEventName)
        {
            await _amsClient.RefreshTokenIfNeededAsync().ConfigureAwait(false); ;
            return await _amsClient.AMSclient.LiveEvents.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, liveEventName).ConfigureAwait(false);
        }

        private static async Task<LiveOutput> GetLiveOutputAsync(string liveEventName, string liveOutputName)
        {
            await _amsClient.RefreshTokenIfNeededAsync().ConfigureAwait(false); ;
            return await _amsClient.AMSclient.LiveOutputs.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, liveEventName, liveOutputName).ConfigureAwait(false);
        }

        private static async Task<StreamingEndpoint> GetStreamingEndpointAsync(string seName)
        {
            await _amsClient.RefreshTokenIfNeededAsync().ConfigureAwait(false); ;
            return await _amsClient.AMSclient.StreamingEndpoints.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, seName).ConfigureAwait(false);
        }


        public async Task DeleteLocatorsForAssetAsync(Asset asset)
        {
            if (asset != null)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                IList<AssetStreamingLocator> locators = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name)).StreamingLocators;

                List<Task> deleteTasks = new List<Task>();
                foreach (AssetStreamingLocator locator in locators)
                {
                    TextBoxLogWriteLine("Deleting locator {0} for asset {1}", locator.Name, asset.Name);
                    deleteTasks.Add(_amsClient.AMSclient.StreamingLocators.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name));
                }

                try
                {
                    await Task.WhenAll(deleteTasks.ToArray());
                    TextBoxLogWriteLine("Locator(s) deleted.");
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when deleting locator(s)", true);
                    TextBoxLogWriteLine(e);
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

        public void TextBoxLogWriteLine(string message, object o1, string o2, object o3, bool Error = false)
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


        private async void buttonRefresh_Click(object sender, EventArgs e)
        {
            await DoRefreshAsync();
        }

        private async void buttonRefreshTab_Click(object sender, EventArgs e)
        {
            switch (tabControlMain.SelectedTab.Name)
            {
                case "tabPageAssets":
                    DoRefreshGridAssetV(false);
                    break;
                case "tabPageFilters":
                    await DoRefreshGridFiltersVAsync(false);
                    break;
                case "tabPageTransfers":
                    break;
                case "tabPageJobs":
                    DoRefreshGridTransformV(false);
                    DoRefreshGridJobV(false);
                    break;
                case "tabPageLive":
                    await DoRefreshGridLiveEventVAsync(false);
                    DoRefreshGridLiveOutputV(false);
                    break;
                case "tabPageOrigins":
                    await DoRefreshGridStreamingEndpointVAsync(false);
                    break;
                case "tabPageStorage":
                    await DoRefreshGridStorageVAsync(false);
                    break;
                case "tabPageCKPol":
                    await DoRefreshGridCKPoliciesVAsync(false);
                    break;
            }
        }

        private async Task DoRefreshAsync()
        {
            DoRefreshGridJobV(false);
            DoRefreshGridTransformV(false);
            DoRefreshGridAssetV(false);
            await DoRefreshGridLiveEventVAsync(false);
            await DoRefreshGridStreamingEndpointVAsync(false);
            await DoRefreshGridStorageVAsync(false);
            await DoRefreshGridFiltersVAsync(false);
            await DoRefreshGridCKPoliciesVAsync(false);
        }

        public void DoRefreshGridAssetV(bool firstime)
        {
            if (firstime)
            {
                SetTextBoxAssetsPageNumber(1);

                dataGridViewAssetsV.Init(_amsClient, SynchronizationContext.Current);
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
            //dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.PurgeCacheAsset(asset)));
            dataGridViewAssetsV.Invoke(d => d.PurgeCacheAsset(asset));

        }


        private void DoRefreshGridTransformV(bool firstime)
        {
            if (firstime)
            {
                //dataGridViewTransformsV.Init(_amsClient);
                Task.Run(() => dataGridViewTransformsV.InitAsync(_amsClient, SynchronizationContext.Current)).GetAwaiter().GetResult();

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
                    dataGridViewJobsV.Init(_amsClient);
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
                        mediaReserverdUnitsInfo = await _mediaRUContext.GetInfoMediaRUAsync(_amsClient);
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


        private async void fromASingleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoMenuUploadFromSingleFiles_Step1Async();
        }

        private async Task DoMenuUploadFromSingleFiles_Step1Async()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                await DoMenuUploadFromSingleFileS_Step2Async(openFileDialog.FileNames);
            }
        }

        private async Task DoMenuUploadFromSingleFileS_Step2Async(string[] FileNames)
        {
            List<string> listpb = AssetInfo.ReturnFilenamesWithProblem(FileNames.ToList());
            if (listpb.Count > 0)
            {
                MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UploadOptions form = new UploadOptions(_amsClient, FileNames.Count() > 1);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            if ((FileNames.Count() > 1 && form.SingleAsset) || (FileNames.Count() == 1)) // one file only, or all files in one asset
            {
                try
                {
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of {0} files into a single asset", FileNames.Count()), TransferType.UploadFromFile, true);
                    // Start a worker thread that does uploading.
                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                    await ProcessUploadFileAndMoreV3Async(FileNames.ToList(), response.Id, response.token, storageaccount: form.StorageSelected, blocksize: form.BlockSize, newAssetCreationSettings: form.assetCreationSetting);
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
                List<Task> MyTasks = new List<Task>();
                // Each file goes in a individual asset
                foreach (string file in FileNames)
                {
                    i++;
                    TransferEntryResponse response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, true);
                    // Start a worker thread that does uploading.
                    var myTask = ProcessUploadFileAndMoreV3Async(new List<string>() { file }, response.Id, response.token, form.StorageSelected, blocksize: form.BlockSize);
                    MyTasks.Add(myTask);
                    if (i == 10) // let's use a batch of 10 threads at the same time
                    {
                        try
                        {
                            await Task.WhenAll(MyTasks);
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine(ex);
                        }
                        /*
                        do
                        {
                            await Task.Delay(1000);
                        }
                        while (ReturnTransfer(response.Id).State == TransferState.Queued);
                        */
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
            }
            DoRefreshGridAssetV(false);
        }


        private async Task ProcessUploadFileAndMoreV3Async(List<string> filenames, Guid guidTransfer, CancellationToken token, string storageaccount = null, string destAssetName = null, int blocksize = 4, NewAsset newAssetCreationSettings = null)
        {

            // If upload in the queue, let's wait our turn
            await DoGridTransferWaitIfNeededAsync(guidTransfer);
            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }
            await _amsClient.RefreshTokenIfNeededAsync();
            IList<StorageAccount> storAccounts = null;
            try
            {
                //à fixer
                var response2 = await _amsClient.AMSclient.Mediaservices.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);//.ConfigureAwait(false);
                storAccounts = response2.StorageAccounts;
            }
            catch
            {

            }

            if (storageaccount == null)
            {
                storageaccount = AMSClientV3.GetStorageName(storAccounts.Where(s => s.Type == StorageAccountType.Primary).First().Id);
                // no storage account or null, then let's take the default one
            }

            string ErrorMessage = string.Empty;
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
                try
                {
                    if (destAssetName == null) // let create a new asset
                    {
                        var assetToCreateSettings = new Asset()
                        {
                            StorageAccountName = storageaccount
                        };

                        if (newAssetCreationSettings != null)
                        {
                            destAssetName = newAssetCreationSettings.AssetName.Replace(Constants.NameconvShortUniqueness, Program.GetUniqueness());
                            assetToCreateSettings.AlternateId = newAssetCreationSettings.AssetAltId;
                            assetToCreateSettings.Container = newAssetCreationSettings.AssetContainer;
                            assetToCreateSettings.Description = newAssetCreationSettings.AssetDescription.Replace(Constants.NameconvFileName, Path.GetFileName(filenames[0]));
                        }
                        else
                        {
                            destAssetName = "uploaded-" + Program.GetUniqueness();
                            assetToCreateSettings.Description = Path.GetFileName(filenames[0]);
                        }

                        asset = await _amsClient.AMSclient.Assets.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, destAssetName, assetToCreateSettings, token);
                    }
                    else // let's reusing existing asset
                    {
                        asset = await GetAssetAsync(destAssetName, token);
                    }

                    ListContainerSasInput input = new ListContainerSasInput()
                    {
                        Permissions = AssetContainerPermission.ReadWrite,
                        ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                    };

                    AssetContainerSas response = await _amsClient.AMSclient.Assets.ListContainerSasAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime);

                    string uploadSasUrl = response.AssetContainerSasUrls.First();

                    Uri sasUri = new Uri(uploadSasUrl);
                    CloudBlobContainer container = new CloudBlobContainer(sasUri);

                    long LengthAllFiles = 0;
                    long BytesCopiedForAllFiles = 0;

                    // size calculation
                    foreach (string file in filenames)
                    {
                        LengthAllFiles += new System.IO.FileInfo(file).Length;
                    }


                    foreach (string fileWithPath in filenames)
                    {

                        if (token.IsCancellationRequested)
                        {
                            return;
                        }

                        string filename = Path.GetFileName(fileWithPath);
                        TextBoxLogWriteLine("Starting upload of file '{0}'", fileWithPath);

                        /*
                        CloudBlockBlob blob = container.GetBlockBlobReference(filename);
                        if (filename.ToLower().EndsWith(".mp4"))
                        {
                            blob.Properties.ContentType = "video/mp4";
                        }

                        blob.StreamWriteSizeInBytes = blocksize * 1024 * 1024; // blocksize

                        copyOperationsDict.Add(blob, blob.UploadFromFileAsync(file, token));
                        Length += new System.IO.FileInfo(file).Length;
                        //MyUploadFileProgressChanged(guidTransfer, filename.IndexOf(file), filenames.Count);
                        */
                        CloudBlockBlob blob = container.GetBlockBlobReference(filename);
                        long bytesToUpload = (new FileInfo(fileWithPath)).Length;

                        if (new System.IO.FileInfo(fileWithPath).Length < blocksize * 1024 * 1024) // file is smaller than block size
                        {
                            if (filename.ToLower().EndsWith(".mp4"))
                            {
                                blob.Properties.ContentType = "video/mp4";
                            }

                            var ado = blob.UploadFromFileAsync(fileWithPath, token);
                            Console.WriteLine(ado.Status);
                            await ado.ContinueWith(t =>
                             {

                                 if (t.Status != TaskStatus.RanToCompletion)
                                 {
                                     Error = true;
                                 }
                                 else
                                 {
                                     BytesCopiedForAllFiles += bytesToUpload;
                                 }
                             });
                        }
                        else
                        {
                            List<string> blockIds = new List<string>();
                            int index = 1;
                            long startPosition = 0;
                            long bytesUploaded = 0;
                            do
                            {
                                var bytesToRead = Math.Min(blocksize * 1024 * 1024, bytesToUpload);
                                var blobContents = new byte[bytesToRead];
                                using (FileStream fs = new FileStream(fileWithPath, FileMode.Open))
                                {
                                    fs.Position = startPosition;
                                    fs.Read(blobContents, 0, (int)bytesToRead);
                                }
                                ManualResetEvent manualResetEvent = new ManualResetEvent(false);
                                var blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(index.ToString("d6")));
                                blockIds.Add(blockId);
                                var blockAsync = blob.PutBlockAsync(blockId, new MemoryStream(blobContents), null);
                                await blockAsync.ContinueWith(t =>
                                {
                                    bytesUploaded += bytesToRead;
                                    bytesToUpload -= bytesToRead;
                                    startPosition += bytesToRead;
                                    index++;
                                    double percentComplete = 100d * (long)(BytesCopiedForAllFiles + bytesUploaded) / LengthAllFiles;
                                    DoGridTransferUpdateProgress(percentComplete, guidTransfer);
                                    manualResetEvent.Set();
                                });
                                manualResetEvent.WaitOne();
                            }
                            while (bytesToUpload > 0);
                            var blockListAsync = blob.PutBlockListAsync(blockIds);
                            await blockListAsync.ContinueWith(t =>
                            {

                                if (t.Status != TaskStatus.RanToCompletion)
                                {
                                    Error = true;
                                }
                                else
                                {
                                    BytesCopiedForAllFiles += bytesUploaded;
                                }
                            });
                        }
                    }
                }
                catch (OperationCanceledException)
                {
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
            else // Error!
            {
                DoGridTransferDeclareError(guidTransfer, "Error during import. " + ErrorMessage);
            }
        }


        private async Task ProcessHttpSourceV3(Uri source, Guid guidTransfer, CancellationToken token, string storageaccount = null, NewAsset assetCreationSettings = null)
        {

            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }
            await _amsClient.RefreshTokenIfNeededAsync();
            IList<StorageAccount> storAccounts = (await _amsClient.AMSclient.Mediaservices.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).StorageAccounts;

            if (storageaccount == null)
            {
                storageaccount = AMSClientV3.GetStorageName(storAccounts.Where(s => s.Type == StorageAccountType.Primary).First().Id);
                // no storage account or null, then let's take the default one
            }

            bool Error = false;
            Asset asset = null;
            string destAssetName = string.Empty;

            try
            {
                var assetSettings = new Asset()
                {
                    StorageAccountName = storageaccount,
                    Description = assetCreationSettings?.AssetDescription.Replace(Constants.NameconvUrl, source.AbsoluteUri) ?? "Imported from : " + source.AbsoluteUri,
                    AlternateId = assetCreationSettings?.AssetAltId,
                    Container = assetCreationSettings?.AssetContainer
                };

                destAssetName = assetCreationSettings?.AssetName.Replace(Constants.NameconvShortUniqueness, Program.GetUniqueness()) ?? "uploaded-" + Program.GetUniqueness();

                asset = await _amsClient.AMSclient.Assets.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, destAssetName, assetSettings, token);

                ListContainerSasInput input = new ListContainerSasInput()
                {
                    Permissions = AssetContainerPermission.ReadWrite,
                    ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                };

                AssetContainerSas response = Task.Run(async () => await _amsClient.AMSclient.Assets.ListContainerSasAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime)).Result;

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
                    await Task.Delay(1000);
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


        private async Task ProcessHttpSASV3Async(Uri ObjectUrl, Guid guidTransfer, CancellationToken token, string storageaccount = null, NewAsset assetCreationSettings = null)
        {

            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }
            await _amsClient.RefreshTokenIfNeededAsync();
            IList<StorageAccount> storAccounts = (await _amsClient.AMSclient.Mediaservices.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName)).StorageAccounts;

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
            string destAssetName = string.Empty;

            try
            {
                var assetSettings = new Asset()
                {
                    StorageAccountName = storageaccount,
                    Description = assetCreationSettings?.AssetDescription.Replace(Constants.NameconvUrl, ObjectUrl.AbsoluteUri) ?? "Imported from : " + ObjectUrl.AbsoluteUri,
                    AlternateId = assetCreationSettings?.AssetAltId,
                    Container = assetCreationSettings?.AssetContainer
                };

                destAssetName = assetCreationSettings?.AssetName.Replace(Constants.NameconvShortUniqueness, Program.GetUniqueness()) ?? "uploaded-" + Program.GetUniqueness();

                asset = await _amsClient.AMSclient.Assets.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, destAssetName, assetSettings, token);

                ListContainerSasInput input = new ListContainerSasInput()
                {
                    Permissions = AssetContainerPermission.ReadWrite,
                    ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                };

                AssetContainerSas response = Task.Run(async () => await _amsClient.AMSclient.Assets.ListContainerSasAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime)).Result;

                string uploadSasUrl = response.AssetContainerSasUrls.First();

                Uri sasUri = new Uri(uploadSasUrl);
                CloudBlobContainer destinationContainer = new CloudBlobContainer(sasUri);

                if (token.IsCancellationRequested)
                {
                    return;
                }

                // Let's list the blobs
                BlobContinuationToken continuationToken = null;
                List<IListBlobItem> blobs = new List<IListBlobItem>();
                do
                {
                    BlobResultSegment segment = await Container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, null, continuationToken, null, null);
                    blobs.AddRange(segment.Results);
                    continuationToken = segment.ContinuationToken;
                }
                while (continuationToken != null);


                // size calculation
                long Length = 0;
                foreach (IListBlobItem blob in blobs)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blobblock = (CloudBlockBlob)blob;
                        Length += blobblock.Properties.Length;
                    }
                }

                IEnumerable<IListBlobItem> blobsblock = blobs.Where(b => b.GetType() == typeof(CloudBlockBlob));
                int nbtotalblobblock = blobsblock.Count();
                int nbblob = 0;
                long BytesCopied = 0;
                Dictionary<CloudBlockBlob, Task<string>> copyOperationsDict = new Dictionary<CloudBlockBlob, Task<string>>();

                // let's parallelize the copy
                foreach (IListBlobItem blob in blobsblock)
                {
                    nbblob++;
                    string fileName = Path.GetFileName(blob.Uri.ToString());

                    blockBlob = destinationContainer.GetBlockBlobReference(fileName);
                    TextBoxLogWriteLine("Copying file '{0}'....", fileName);

                    UriBuilder urib = new UriBuilder(ObjectUrl);
                    urib.Path = urib.Path + "/" + Path.GetFileName(blob.Uri.ToString());

                    copyOperationsDict.Add(blockBlob, blockBlob.StartCopyAsync(urib.Uri, token));
                }
                bool Cancelled = false;

                DateTime startTime = DateTime.UtcNow;

                // let's check the copy status
                while ((copyOperationsDict.Count > 0) && (!Cancelled))
                {
                    if (token.IsCancellationRequested && !Cancelled)
                    {
                        foreach (KeyValuePair<CloudBlockBlob, Task<string>> entry in copyOperationsDict)
                        {
                            // do something with entry.Value or entry.Key
                            await entry.Key.AbortCopyAsync(entry.Value.Result);
                            Cancelled = true;
                        }
                    }

                    await Task.Delay(1000);

                    List<CloudBlockBlob> copyCompleted = new List<CloudBlockBlob>();
                    foreach (KeyValuePair<CloudBlockBlob, Task<string>> entry in copyOperationsDict)
                    {
                        CloudBlockBlob blockBlobToCheck = entry.Key;
                        try
                        {
                            await blockBlobToCheck.FetchAttributesAsync();
                        }
                        catch { }
                        CopyState copyStatus = blockBlobToCheck.CopyState;
                        if (copyStatus != null)
                        {
                            double percentComplete = 100d * (long)(BytesCopied + copyStatus.BytesCopied) / Length;

                            DoGridTransferUpdateProgress(percentComplete, guidTransfer);

                            if (copyStatus.Status != CopyStatus.Pending)
                            {
                                if (copyStatus.Status == CopyStatus.Failed)
                                {
                                    Error = true;
                                    ErrorMessage = copyStatus.StatusDescription;
                                }
                                if (copyStatus.Status == CopyStatus.Aborted)
                                {
                                    Canceled = true;
                                }
                                if (copyStatus.Status == CopyStatus.Success)
                                {
                                    BytesCopied += blockBlobToCheck.Properties.Length;
                                }
                                copyCompleted.Add(blockBlobToCheck);
                            }
                        }
                    }
                    copyCompleted.ForEach(b => copyOperationsDict.Remove(b));
                }

                DateTime endTime = DateTime.UtcNow;
                TimeSpan diffTime = endTime - startTime;

                List<CloudBlobDirectory> ListDirectories = new List<CloudBlobDirectory>();
                List<Task> mylistresults = new List<Task>();

                IEnumerable<IListBlobItem> blobsdir = blobs.Where(b => b.GetType() == typeof(CloudBlobDirectory));
                int nbtotalblobdir = blobsdir.Count();
                int nbblobdir = 0;
                foreach (IListBlobItem blob in blobsdir)
                {
                    nbblobdir++;
                    string fileName = blob.Uri.Segments[2];

                    CloudBlobDirectory blobdir = (CloudBlobDirectory)blob;
                    ListDirectories.Add(blobdir);
                    TextBoxLogWriteLine("Fragblobs detected (live archive) '{0}'.", blobdir.Prefix);



                    // Let's list the blobs in the directory
                    continuationToken = null;
                    List<IListBlobItem> srcBlobList = new List<IListBlobItem>();
                    do
                    {
                        BlobResultSegment segment = await blobdir.ListBlobsSegmentedAsync(true, BlobListingDetails.None, null, continuationToken, null, null);
                        srcBlobList.AddRange(segment.Results);
                        continuationToken = segment.ContinuationToken;
                    }
                    while (continuationToken != null);


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
        }


        private void MyUploadFileProgressChanged(Guid guidTransfer, int indexfile, int nbfiles)
        {
            double progress = 100 * (double)indexfile / nbfiles;
            DoGridTransferUpdateProgress(progress, guidTransfer);
        }

        private async Task DoMenuUploadFileToAsset_Step1Async()
        {
            List<Asset> assets = await ReturnSelectedAssetsV3Async();

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                await DoMenuUploadFileToAsset_Step2Async(openFileDialog.FileNames, assets);
            }
        }

        private async Task DoMenuUploadFileToAsset_Step2Async(string[] FileNames, List<Asset> assets)
        {
            List<string> listpb = AssetInfo.ReturnFilenamesWithProblem(FileNames.ToList());
            if (listpb.Count > 0)
            {
                MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
            int i = 0;
            List<Task> MyTasks = new List<Task>();

            foreach (Asset asset in assets)
            {
                try
                {
                    i++;
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of {0} file{1} to asset '{2}'", FileNames.Count(), FileNames.Count() > 1 ? "s" : string.Empty, asset.Name), TransferType.UploadFromFile, true);
                    // Start a worker thread that does uploading.
                    //Task.Factory.StartNew(async () => await ProcessUploadFileAndMoreV3Async(FileNames.ToList(), response.Id, response.token, null, asset.Name), response.token);
                    MyTasks.Add(ProcessUploadFileAndMoreV3Async(FileNames.ToList(), response.Id, response.token, null, asset.Name));

                    if (i == 10) // let's use a batch of 10 threads at the same time
                    {
                        try
                        {
                            await Task.WhenAll(MyTasks);
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine(ex);
                        }
                        /*
                        do
                        {
                            Task.Delay(1000).Wait();
                        }
                        while (ReturnTransfer(response.Id).State == TransferState.Queued);
                        */
                        i = 0;
                    }
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine(ex);
                }
            }
        }



        public async Task DownloadOutputAssetAsync(AMSClientV3 client, string assetName, string outputFolderName, TransferEntryResponse response, DownloadToFolderOption downloadOption, bool openFileExplorer, List<string> onlySomeBlobsName = null)
        {
            // If download is in the queue, let's wait our turn
            await DoGridTransferWaitIfNeededAsync(response.Id);
            if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
                return;
            }

            //const int ListBlobsSegmentMaxResult = 5;

            if (!Directory.Exists(outputFolderName))
            {
                Directory.CreateDirectory(outputFolderName);
            }
            await _amsClient.RefreshTokenIfNeededAsync();
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

            try
            {
                do
                {
                    BlobResultSegment segment = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, null, continuationToken, null, null);

                    foreach (IListBlobItem blobItem in segment.Results)
                    {
                        if (blobItem is CloudBlockBlob blob && (onlySomeBlobsName == null || (onlySomeBlobsName != null && onlySomeBlobsName.Contains(blob.Name))))
                        {
                            string path = Path.Combine(outputFolderName, blob.Name);

                            downloadTasks.Add(blob.DownloadToFileAsync(path, FileMode.Create, response.token));
                        }
                    }

                    continuationToken = segment.ContinuationToken;
                }
                while (continuationToken != null);

                await Task.WhenAll(downloadTasks);
            }
            catch (OperationCanceledException)
            {
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

        }


        private async void fromMultipleFilesToolStripMenuItem_Click(object sender, EventArgs e) // upload from multiple files
        {
            await DoMenuUploadFromFolder_Step1Async();
        }

        private async Task DoMenuUploadFromFolder_Step1Async()
        {
            CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog() { IsFolderPicker = true };

            if (!string.IsNullOrEmpty(_backuprootfolderupload))
            {
                openFolderDialog.DefaultDirectory = _backuprootfolderupload;
            }

            if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                await DoMenuUploadFromFolder_Step2Async(openFolderDialog.FileName);
            }
        }

        private async Task DoMenuUploadFromFolder_Step2Async(string SelectedPath)
        {
            if (SelectedPath != null)
            {
                List<string> listpb = AssetInfo.ReturnFilenamesWithProblem(Directory.GetFiles(SelectedPath).ToList());
                if (listpb.Count > 0)
                {
                    MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _backuprootfolderupload = SelectedPath;

                IEnumerable<string> filePaths = Directory.EnumerateFiles(SelectedPath as string);

                TextBoxLogWriteLine("There are {0} files in {1}", filePaths.Count().ToString(), (SelectedPath as string));
                if (!filePaths.Any())
                {
                    throw new FileNotFoundException(string.Format("No files in directory, check folderPath: {0}", SelectedPath));
                }

                UploadOptions form = new UploadOptions(_amsClient, filePaths.Count() > 1);
                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                if (form.SingleAsset)
                {
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(SelectedPath)), TransferType.UploadFromFolder, true);
                    await ProcessUploadFileAndMoreV3Async(
                                                          filePaths.ToList(),
                                                          response.Id,
                                                          response.token,
                                                          storageaccount: form.StorageSelected,
                                                          blocksize: form.BlockSize,
                                                          newAssetCreationSettings: form.assetCreationSetting
                                                         );
                }
                else
                {
                    List<Task> MyTasks = new List<Task>();
                    int i = 0;
                    foreach (string f in filePaths.ToList())
                    {
                        i++;
                        TransferEntryResponse response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(f) + "'", TransferType.UploadFromFile, true);
                        // Start a worker thread that does uploading.
                        var myTask = ProcessUploadFileAndMoreV3Async(
                                                                      new List<string>() { f },
                                                                      response.Id,
                                                                      response.token,
                                                                      storageaccount: form.StorageSelected,
                                                                      blocksize: form.BlockSize
                                                                      );

                        MyTasks.Add(myTask);

                        if (i == 10) // let's use a batch of 10 threads at the same time
                        {
                            try
                            {
                                await Task.WhenAll(MyTasks);
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine(ex);
                            }
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
                }
                DoRefreshGridAssetV(false);
            }
        }


        private void DoMenuImportFromHttp()
        {
            ImportHttp form = new ImportHttp(_amsClient);

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Import from Http of '{0}'", Path.GetFileName(form.GetURL.LocalPath)), TransferType.ImportFromHttp, false);
                    // Start a worker thread that does uploading.
                    // ProcessHttpSourceV3
                    Task.Factory.StartNew(() => ProcessHttpSourceV3(form.GetURL, response.Id, response.token, form.StorageSelected, form.assetCreationSetting), response.token);

                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
        }



        private void DotabControlMainSwitch(string tab, Transform transform = null)
        {
            foreach (TabPage page in tabControlMain.TabPages)
            {
                if (page.Text.Contains(tab))
                {
                    if (transform != null)
                    {
                        dataGridViewTransformsV.SelectTransform(transform);
                    }
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
                _amsClient.RefreshTokenIfNeeded();
                asset = Task.Run(async () => await GetAssetAsync(asset.Name)).Result;
                if (asset != null)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        AssetInformation form = new AssetInformation(this, _amsClient)
                        {
                            myAsset = asset,

                            myStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints // we want to keep the same sorting
                        };

                        dialogResult = form.ShowDialog(this);

                    }
                    finally
                    {
                        Cursor = Cursors.Arrow;
                        dataGridViewAssetsV.PurgeCacheAsset(asset);
                        Task.Run(async () => await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync());
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
                    JobInformation form = new JobInformation(this, _amsClient)
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
                    TransformInformation form = new TransformInformation(t);

                    dialogResult = form.ShowDialog(this);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
            return dialogResult;
        }

        private async void renameToolStripMenuItem_Click(object sender, EventArgs e)  // RENAME ASSET
        {
            await DoMenuChangeAssetDescriptionAsync();
        }


        private async Task DoMenuChangeAssetDescriptionAsync()
        {
            List<Asset> SelectedAssets = await ReturnSelectedAssetsV3Async();

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
                            TextBoxLogWriteLine("Updating description of asset '{0}'...", AssetTORename.Name);
                            await _amsClient.AMSclient.Assets.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, AssetTORename.Name, AssetTORename);
                            TextBoxLogWriteLine("Description of asset '{0}' updated.", AssetTORename.Name);
                        }
                        catch
                        {
                            TextBoxLogWriteLine("There is a problem when updating the asset description.", true);
                        }
                        dataGridViewAssetsV.PurgeCacheAsset(AssetTORename);
                        await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();
                    }
                }
            }
        }

        private async Task DoMenuEditAssetAltIdAsync()
        {
            List<Asset> SelectedAssets = await ReturnSelectedAssetsV3Async();

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
                            await _amsClient.AMSclient.Assets.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, AssetToEditAltId.Name, AssetToEditAltId);
                        }
                        catch
                        {
                            TextBoxLogWriteLine("There is a problem when editing the alternate Id.", true);
                            return;
                        }
                        TextBoxLogWriteLine("Alternate Id for Asset Id '{0}' is now '{1}'.", AssetToEditAltId.Id, AssetToEditAltId.AlternateId);
                        dataGridViewAssetsV.PurgeCacheAsset(AssetToEditAltId);
                        await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();
                    }
                }
            }
        }


        private async Task DoMenuDownloadToLocalAsync()
        {
            List<Asset> SelectedAssets = await ReturnSelectedAssetsV3Async();
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
                    List<Task> myTasks = new List<Task>();
                    foreach (Asset asset in SelectedAssets)
                    {
                        i++;
                        string label = string.Format("Download of asset '{0}'", asset.Name);
                        TransferEntryResponse response = DoGridTransferAddItem(label, TransferType.DownloadToLocal, true);
                        myTasks.Add(DownloadOutputAssetAsync(_amsClient, asset.Name, form.FolderPath, response, form.FolderOption, form.OpenFolderAfterDownload));

                        if (i == 10) // let's use a batch of 10 threads at the same time
                        {
                            try
                            {
                                await Task.WhenAll(myTasks.ToArray());
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine(ex);
                            }
                            /*
                            do
                            {
                                await Task.Delay(1000);
                            }
                            while (ReturnTransfer(response.Id).State == TransferState.Queued);
                            */
                            i = 0;
                        }
                    }
                    await Task.WhenAll(myTasks.ToArray());
                    TextBoxLogWriteLine("Download finished");
                }
            }
        }

        private async void cancelJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoCancelJobsAsync();
        }


        private async Task DoCancelJobsAsync()
        {
            List<JobExtension> SelectedJobs = await ReturnSelectedJobsV3Async();

            if (SelectedJobs.Count > 0)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                string question = "Cancel these " + SelectedJobs.Count + " jobs ?";
                if (SelectedJobs.Count == 1)
                {
                    question = "Cancel " + SelectedJobs[0].Job.Name + " ?";
                }

                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) cancelation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    List<Task> cancelTasks = new List<Task>();
                    foreach (JobExtension JobToCancel in SelectedJobs)
                    {
                        if (JobToCancel != null)
                        {
                            TextBoxLogWriteLine("Canceling job '{0}'...", JobToCancel.Job.Name);
                            cancelTasks.Add(_amsClient.AMSclient.Jobs.CancelJobAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, JobToCancel.TransformName, JobToCancel.Job.Name));
                        }
                    }
                    try
                    {
                        await Task.WhenAll(cancelTasks.ToArray());
                        TextBoxLogWriteLine("Job(s) cancelled.");
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("Error when canceling job(s).", true);
                        TextBoxLogWriteLine(e);
                    }
                    DoRefreshGridJobV(false);
                }
            }
        }

        private void assetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private async Task DoCreateLocatorAsync(List<Asset> SelectedAssets, string LiveAssetManifest = null)
        {
            string labelAssetName;
            StringBuilder sbuilder = new StringBuilder(); // used for locator copy to clipboard

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

                CreateLocator formLocator = new CreateLocator(_amsClient, SelectedAssets)
                {
                    LocatorStartDate = DateTime.UtcNow.AddMinutes(-5),
                    LocatorEndDate = DateTime.UtcNow.AddDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew),
                    LocAssetName = labelAssetName,
                    LocatorHasStartDate = false,
                    LocWarning = string.Empty
                };

                if (formLocator.ShowDialog() == DialogResult.OK)
                {
                    await _amsClient.RefreshTokenIfNeededAsync();

                    // The duration for the locator's access policy.
                    TimeSpan accessPolicyDuration = formLocator.LocatorEndDate.Subtract(DateTime.UtcNow);
                    if (formLocator.LocatorStartDate != null)
                    {
                        accessPolicyDuration = formLocator.LocatorEndDate.Subtract((DateTime)formLocator.LocatorStartDate);
                    }

                    // DRM
                    ContentKeyPolicy keyPolicy = null;
                    List<form_DRM_Config_TokenClaims> formPlayreadyTokenClaims = new List<form_DRM_Config_TokenClaims>();
                    List<form_DRM_Config_TokenClaims> formWidevineTokenClaims = new List<form_DRM_Config_TokenClaims>();
                    List<form_DRM_Config_TokenClaims> formFairPlayTokenClaims = new List<form_DRM_Config_TokenClaims>();
                    List<form_DRM_Config_TokenClaims> formClearKeyTokenClaims = new List<form_DRM_Config_TokenClaims>();
                    List<ContentKeyPolicyOption> options = new List<ContentKeyPolicyOption>();

                    // let's preserve location of windows
                    int left = formLocator.Left;
                    int top = formLocator.Top;

                    if (formLocator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey || formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming)
                    {
                        string tokenSymKey = Properties.Settings.Default.DynEncTokenSymKeyv3;
                        if (string.IsNullOrWhiteSpace(tokenSymKey))
                        {
                            tokenSymKey = null;
                        }

                        if (formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming)
                        {
                            DRM_CENCCBSCDelivery formCencDelivery = new DRM_CENCCBSCDelivery(
                                                                    true,
                                                                    true,
                                                                    formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming
                                                                    )
                            { Left = left, Top = top };
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

                                formPlayreadyTokenClaims.Add(new form_DRM_Config_TokenClaims(step++, i + 1, "PlayReady", tokenSymKey, false)
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

                                formWidevineTokenClaims.Add(new form_DRM_Config_TokenClaims(step++, i + 1, "Widevine", tokenSymKey, false) { Left = left, Top = top });
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

                            // for each FairPlay option
                            List<DRM_FairPlayLicense> formFairPlay = new List<DRM_FairPlayLicense>();

                            for (int i = 0; i < formCencDelivery.GetNumberOfAuthorizationPolicyOptionsFairPlay; i++)
                            {
                                bool laststep = (i == formCencDelivery.GetNumberOfAuthorizationPolicyOptionsFairPlay - 1);

                                formFairPlayTokenClaims.Add(new form_DRM_Config_TokenClaims(step++, i + 1, "FairPlay", tokenSymKey, false) { Left = left, Top = top });
                                if (formFairPlayTokenClaims[i].ShowDialog() != DialogResult.OK)
                                {
                                    return;
                                }

                                tokenSymKey = formFairPlayTokenClaims[i].textBoxSymKey.Text; // let's reuse the same key if the user wants
                                SavePositionOfForm(formFairPlayTokenClaims[i], out left, out top);

                                formFairPlay.Add(new DRM_FairPlayLicense(step++, i + 1, laststep) { Left = left, Top = top });
                                if (formFairPlay[i].ShowDialog() != DialogResult.OK)
                                {
                                    return;
                                }

                                SavePositionOfForm(formFairPlay[i], out left, out top);

                                options.Add(
                                            new ContentKeyPolicyOption()
                                            {
                                                Configuration = new ContentKeyPolicyFairPlayConfiguration()
                                                {
                                                    RentalAndLeaseKeyType = formFairPlay[i].FairPlayRentalAndLeaseKeyType,
                                                    RentalDuration = formFairPlay[i].RentalDuration,
                                                    Ask = formCencDelivery.FairPlayASK,
                                                    FairPlayPfxPassword = formCencDelivery.FairPlayCertificate.Password,
                                                    FairPlayPfx = Convert.ToBase64String(formCencDelivery.FairPlayCertificate.Certificate.Export(X509ContentType.Pfx, formCencDelivery.FairPlayCertificate.Password)),
                                                    OfflineRentalConfiguration = formFairPlay[i].FairPlayOfflineRentalConfig
                                                },
                                                Restriction = formFairPlayTokenClaims[i].GetContentKeyPolicyRestriction,
                                                Name = formFairPlay[i].FairPlayPolicyName
                                            });
                            }

                        }
                        else if (formLocator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey)
                        {
                            formClearKeyTokenClaims.Add(new form_DRM_Config_TokenClaims(1, 1, "Clear Key", tokenSymKey, true) { Left = left, Top = top });
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
                            keyPolicy = await _amsClient.AMSclient.ContentKeyPolicies.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup,
                        _amsClient.credentialsEntry.AccountName,
                        "keypolicy-" + Program.GetUniqueness(),
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
                        List<LocatorAndUrls> listLocators = await ProcessCreateLocatorV3Async(formLocator.StreamingPolicyName, SelectedAssets, formLocator.LocatorStartDate, formLocator.LocatorEndDate, formLocator.ForceLocatorGuid, keyPolicy, formLocator.SelectedFilters);

                        Microsoft.Rest.Azure.IPage<StreamingEndpoint> SEList = await _amsClient.AMSclient.StreamingEndpoints.ListAsync(
                             _amsClient.credentialsEntry.ResourceGroup,
                              _amsClient.credentialsEntry.AccountName
                             );

                        foreach (LocatorAndUrls loc in listLocators)
                        {
                            ListPathsResponse paths = await _amsClient.AMSclient.StreamingLocators.ListPathsAsync(
                            _amsClient.credentialsEntry.ResourceGroup,
                             _amsClient.credentialsEntry.AccountName,
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
                        if (formPlayreadyTokenClaims.Any(t => t.NeedToken) || formWidevineTokenClaims.Any(t => t.NeedToken) || formFairPlayTokenClaims.Any(t => t.NeedToken) || formClearKeyTokenClaims.Any(t => t.NeedToken))
                        {
                            DRM_GenerateToken formTokenProperties = new DRM_GenerateToken();
                            formTokenProperties.ShowDialog();
                            if (formTokenProperties.DialogResult == DialogResult.OK)
                            {
                                sbuilder.Append(await AddTestTokenToSbuilderAsync(formPlayreadyTokenClaims, listLocators.FirstOrDefault(), "PlayReady", formTokenProperties.TokenDuration, formTokenProperties.TokenUse));
                                sbuilder.Append(await AddTestTokenToSbuilderAsync(formWidevineTokenClaims, listLocators.FirstOrDefault(), "Widevine", formTokenProperties.TokenDuration, formTokenProperties.TokenUse));
                                sbuilder.Append(await AddTestTokenToSbuilderAsync(formFairPlayTokenClaims, listLocators.FirstOrDefault(), "FairPlay", formTokenProperties.TokenDuration, formTokenProperties.TokenUse));
                                sbuilder.Append(await AddTestTokenToSbuilderAsync(formClearKeyTokenClaims, listLocators.FirstOrDefault(), "Clear Key", formTokenProperties.TokenDuration, formTokenProperties.TokenUse));
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

        public async Task<StringBuilder> AddTestTokenToSbuilderAsync(List<form_DRM_Config_TokenClaims> formTokenClaims, LocatorAndUrls myLocator, string DRMTechnology, int tokenDuration, int? tokenUse)
        {
            StringBuilder sbuilder = new StringBuilder();
            foreach (form_DRM_Config_TokenClaims tokenClaims in formTokenClaims)
            {
                if (tokenClaims.GetDetailedTokenType == ExplorerTokenType.JWTSym || tokenClaims.GetDetailedTokenType == ExplorerTokenType.JWTX509)
                {
                    // We are using the ContentKeyIdentifierClaim in the ContentKeyPolicy which means that the token presented
                    // to the Key Delivery Component must have the identifier of the content key in it.  Since we didn't specify
                    // a content key when creating the StreamingLocator, the system created a random one for us.  In order to 
                    // generate our test token we must get the ContentKeyId to put in the ContentKeyIdentifierClaim claim.
                    ListContentKeysResponse response = await _amsClient.AMSclient.StreamingLocators.ListContentKeysAsync(_amsClient.credentialsEntry.ResourceGroup,
                            _amsClient.credentialsEntry.AccountName, myLocator.LocatorName);
                    string keyIdentifier = response.ContentKeys.First().Id.ToString();

                    sbuilder.AppendLine(string.Format("Test token for {0}, Option {1} (valid {2} min) :",
                        DRMTechnology,
                        formTokenClaims.IndexOf(tokenClaims) + 1,
                        tokenDuration));
                    sbuilder.AppendLine(Constants.Bearer + tokenClaims.GetTestToken(keyIdentifier, tokenClaims.GetTokenRequiredClaims, tokenDuration, tokenUse));
                    sbuilder.AppendLine(string.Empty);
                }
            }
            return sbuilder;
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
                    await _amsClient.RefreshTokenIfNeededAsync();

                    string result = await Task.Run(() =>
                    ProcessCreateLocatorSAS(SelectedAssets)
                    );

                    EditorXMLJSON displayResult = new EditorXMLJSON("SAS Urls", result, false, false, false);
                    displayResult.ShowDialog();
                }
            }
        }


        private async Task<List<LocatorAndUrls>> ProcessCreateLocatorV3Async(string streamingPolicyName, List<Asset> assets, Nullable<DateTime> startTime, Nullable<DateTime> endTime, string ForceLocatorGUID, ContentKeyPolicy keyPolicy, List<string> listFilters = null)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            List<LocatorAndUrls> listLocatorNames = new List<LocatorAndUrls>();

            foreach (Asset AssetToP in assets)
            {
                StreamingLocator locator = null;
                string keyPolicyName = keyPolicy?.Name;

                try
                {
                    string uniqueness = Program.GetUniqueness();
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

                    locator = await _amsClient.AMSclient.StreamingLocators.CreateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, streamingLocatorName, locator);

                    TextBoxLogWriteLine("Locator created : {0}", locator.Name);
                    IList<StreamingPath> streamingPaths = (await _amsClient.AMSclient.StreamingLocators.ListPathsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locator.Name)).StreamingPaths;
                    listLocatorNames.Add(new LocatorAndUrls() { AssetName = AssetToP.Name, LocatorName = streamingLocatorName, LocatorId = locator.StreamingLocatorId, Paths = streamingPaths.ToList() });

                }

                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error. Could not create a locator for '{0}' ", AssetToP.Name, true);
                    TextBoxLogWriteLine(ex);
                    return null;
                }
            }
            dataGridViewAssetsV.PurgeCacheAssets(assets);
            await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();

            return listLocatorNames;
        }

        private async Task<string> ProcessCreateLocatorSAS(List<Asset> assets)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            StringBuilder stringLines = new StringBuilder();

            foreach (Asset AssetToP in assets)
            {
                AssetContainerSas assetContainerSas = await _amsClient.AMSclient.Assets.ListContainerSasAsync(
                                                               _amsClient.credentialsEntry.ResourceGroup,
                                                               _amsClient.credentialsEntry.AccountName,
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

                BlobContinuationToken continuationToken = null;
                do
                {
                    var response = await container.ListBlobsSegmentedAsync(null, false, BlobListingDetails.None, null, continuationToken, null, null);
                    continuationToken = response.ContinuationToken;
                    foreach (IListBlobItem blobItem in response.Results)
                    {
                        if (blobItem.GetType() == typeof(CloudBlockBlob))
                        {
                            if (blobItem is CloudBlockBlob blob)
                            {
                                UriBuilder bloburl = new UriBuilder(containerSasUrl);
                                bloburl.Path += "/" + blob.Name;
                                stringLines.AppendLine(blob.Name);
                                stringLines.AppendLine(bloburl.ToString());
                                stringLines.AppendLine(string.Empty);
                            }
                        }
                    }
                }
                while (continuationToken != null);
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


        private async Task DoDeleteAllLocatorsOnAssetsAsync(List<Asset> SelectedAssets)
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
                                await DeleteLocatorsForAssetAsync(AssetToProcess);
                                TextBoxLogWriteLine("Deletion done.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting locators of the asset {0}.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(ex);
                            }
                            dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                            await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();
                        }
                    }
                }
            }
        }

        private async Task<List<Asset>> ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync()
        {
            if (tabControlMain.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabAssets)) // we are in the asset tab
            {
                return await ReturnSelectedAssetsV3Async();
            }
            else if (tabControlMain.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabLive)) // we are in the live tab
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                return (await ReturnSelectedLiveOutputsAsync())
                        .Select(p =>
                            Task.Run(() =>
                                        _amsClient.AMSclient.Assets.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, p.AssetName))
                                        .GetAwaiter().GetResult()
                        )
                        .ToList();
            }
            else
            {
                return null;
            }
        }

        private async Task<List<Asset>> ReturnSelectedAssetsV3Async()
        {
            List<Asset> SelectedAssets = new List<Asset>();
            await _amsClient.RefreshTokenIfNeededAsync();

            try
            {
                foreach (DataGridViewRow Row in dataGridViewAssetsV.SelectedRows)
                {
                    Asset asset = await _amsClient.AMSclient.Assets.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, Row.Cells[dataGridViewAssetsV.Columns["Name"].Index].Value.ToString());

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

        private async Task<List<JobExtension>> ReturnSelectedJobsV3Async()
        {
            List<JobExtension> SelectedJobs = new List<JobExtension>();
            foreach (DataGridViewRow Row in dataGridViewJobsV.SelectedRows)
            {
                Job job = await GetJobAsync(Row.Cells["TransformName"].Value.ToString(), Row.Cells["Name"].Value.ToString());
                SelectedJobs.Add(new JobExtension()
                {
                    Job = job,
                    TransformName = Row.Cells["TransformName"].Value.ToString()
                });
            }

            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        private async Task<List<Transform>> ReturnSelectedTransformsAsync()
        {
            List<Transform> SelectedTransforms = new List<Transform>();
            await _amsClient.RefreshTokenIfNeededAsync();

            List<Transform> transforms = new List<Transform>();
            IPage<Transform> transformsPage = await _amsClient.AMSclient.Transforms.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (transformsPage != null)
            {
                transforms.AddRange(transformsPage);
                if (transformsPage.NextPageLink != null)
                {
                    transformsPage = await _amsClient.AMSclient.Transforms.ListNextAsync(transformsPage.NextPageLink);
                }
                else
                {
                    transformsPage = null;
                }
            }

            foreach (DataGridViewRow Row in dataGridViewTransformsV.SelectedRows)
            {
                string transformName = Row.Cells[dataGridViewTransformsV.Columns["Name"].Index].Value.ToString();
                Transform myTransform = transforms.Where(f => f.Name == transformName).FirstOrDefault();
                if (myTransform != null)
                {
                    SelectedTransforms.Add(myTransform);
                }
            }
            return SelectedTransforms;
        }


        private async Task<StorageAccount> ReturnSelectedStorageAsync()
        {
            StorageAccount SelectedStorage = null;
            if (dataGridViewStorage.SelectedRows.Count == 1)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                DataGridViewRow row = dataGridViewStorage.SelectedRows[0];
                int index = dataGridViewStorage.Columns["Id"].Index;
                string storagename = AMSClientV3.GetStorageName(row.Cells[index].Value.ToString());
                SelectedStorage = (await _amsClient.AMSclient.Mediaservices.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName))
                    .StorageAccounts.Where(s => AMSClientV3.GetStorageName(s.Id) == storagename).FirstOrDefault();
            }

            return SelectedStorage;
        }

        private async Task<List<AccountFilter>> ReturnSelectedAccountFiltersAsync()
        {
            List<AccountFilter> SelectedFilters = new List<AccountFilter>();
            await _amsClient.RefreshTokenIfNeededAsync();

            // account filters
            List<AccountFilter> acctFilters = new List<AccountFilter>();
            IPage<AccountFilter> acctFiltersPage = await _amsClient.AMSclient.AccountFilters.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (acctFiltersPage != null)
            {
                acctFilters.AddRange(acctFiltersPage);
                if (acctFiltersPage.NextPageLink != null)
                {
                    acctFiltersPage = await _amsClient.AMSclient.AccountFilters.ListNextAsync(acctFiltersPage.NextPageLink);
                }
                else
                {
                    acctFiltersPage = null;
                }
            }

            foreach (DataGridViewRow Row in dataGridViewFilters.SelectedRows)
            {
                string filtername = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                AccountFilter myfilter = acctFilters.Where(f => f.Name == filtername).FirstOrDefault();
                if (myfilter != null)
                {
                    SelectedFilters.Add(myfilter);
                }
            }

            return SelectedFilters;
        }

        private async Task<List<ContentKeyPolicy>> ReturnSelectedCKPoliciessAsync()
        {
            List<ContentKeyPolicy> SelectedCKPolicies = new List<ContentKeyPolicy>();
            await _amsClient.RefreshTokenIfNeededAsync();

            Microsoft.Rest.Azure.IPage<ContentKeyPolicy> ckPolicies = await _amsClient.AMSclient.ContentKeyPolicies.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            foreach (DataGridViewRow Row in dataGridViewCKPolicies.SelectedRows)
            {
                string ckpolName = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                ContentKeyPolicy myPolicy = ckPolicies.Where(f => f.Name == ckpolName).FirstOrDefault();
                if (myPolicy != null)
                {
                    SelectedCKPolicies.Add(myPolicy);
                }
            }

            return SelectedCKPolicies;
        }




        private async void selectedAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoMenuDeleteSelectedAssetsAsync();

        }

        private async Task DoMenuDeleteSelectedAssetsAsync()
        {
            List<Asset> SelectedAssets = await ReturnSelectedAssetsV3Async();
            await DoDeleteAssetsAsync(SelectedAssets);
        }

        private async Task DoDeleteAssetsAsync(List<Asset> SelectedAssets)
        {
            if (SelectedAssets.Count > 0)
            {
                //var form = new DeleteKeyAndPolicy(SelectedAssets.Count);
                string question = SelectedAssets.Count > 1 ?
                    string.Format("Do you want to delete these {0} assets ?", SelectedAssets.Count)
                    : string.Format("Do you want to delete asset '{0}' ?", SelectedAssets[0].Name);

                if (MessageBox.Show(question, "Asset deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    await _amsClient.RefreshTokenIfNeededAsync();

                    bool Error = false;
                    try
                    {
                        TextBoxLogWriteLine("Deleting asset(s)...");
                        Task[] deleteTasks = SelectedAssets.Select(a => _amsClient.AMSclient.Assets.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, a.Name)).ToArray();

                        //Task[] deleteTasks = SelectedAssets.Select(a => DynamicEncryption.DeleteAssetAsync(_context, a, form.DeleteDeliveryPolicies, form.DeleteKeys, form.DeleteAuthorizationPolicies)).ToArray();
                        await Task.WhenAll(deleteTasks);
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
            }
        }


        private void allAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private async void allJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDeleteAllJobsAsync();
        }

        private async Task selectedJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDeleteSelectedJobsAsync();
        }

        private async Task DoDeleteSelectedJobsAsync()
        {
            await DoDeleteJobsAsync(dataGridViewJobsV.ReturnSelectedJobs());
        }

        private async Task DoDeleteJobsAsync(List<JobExtension> SelectedJobs)
        {
            if (SelectedJobs.Count > 0)
            {
                string question = (SelectedJobs.Count == 1) ? "Delete " + SelectedJobs[0].Job.Name + " ?" : "Delete these " + SelectedJobs.Count + " jobs ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    await _amsClient.RefreshTokenIfNeededAsync();

                    bool Error = false;
                    Task[] deleteTasks = SelectedJobs.ToList().Select(j => _amsClient.AMSclient.Jobs.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, j.TransformName, j.Job.Name)).ToArray();
                    TextBoxLogWriteLine("Deleting job(s)");
                    try
                    {
                        await Task.WhenAll(deleteTasks);
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
            }
        }


        private async Task DoDeleteAllJobsAsync()
        {
            var transforms = await dataGridViewTransformsV.ReturnSelectedTransformsAsync();
            if (transforms.Count > 1)
            {
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete ALL the jobs from the selected transform?", "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                bool Error = false;

                // let's build the tasks list
                TextBoxLogWriteLine("Listing the jobs...");
                List<Task> deleteTasks = new List<Task>();

                //   foreach (var transform in dataGridViewTransformsV.ReturnSelectedTransforms())
                {
                    Transform transform = transforms.First();

                    List<Job> listjobs = new List<Job>();
                    IPage<Job> jobsPage = await _amsClient.AMSclient.Jobs.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, transform.Name);
                    while (jobsPage != null)
                    {
                        listjobs.AddRange(jobsPage);
                        if (jobsPage.NextPageLink != null)
                        {
                            jobsPage = await _amsClient.AMSclient.Jobs.ListNextAsync(jobsPage.NextPageLink);
                        }
                        else
                        {
                            jobsPage = null;
                        }
                    }

                    deleteTasks.AddRange(listjobs.ToList().Select(j => _amsClient.AMSclient.Jobs.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, transform.Name, j.Name)));
                }

                TextBoxLogWriteLine(string.Format("Deleting {0} job(s)", deleteTasks.Count));
                try
                {
                    await Task.WhenAll(deleteTasks.ToArray());
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
        }


        private async Task DoCancelAllJobsAsync()
        {
            var transforms = await dataGridViewTransformsV.ReturnSelectedTransformsAsync();
            if (transforms.Count > 1)
            {
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to cancel ALL the jobs from the selected transform ?", "Job cancelation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                bool Error = false;

                // let's build the tasks list
                TextBoxLogWriteLine("Listing the jobs...");
                List<Task> deleteTasks = new List<Task>();

                //  foreach (var transform in dataGridViewTransformsV.ReturnSelectedTransforms())
                {
                    Transform transform = transforms.First();

                    List<Job> listjobs = new List<Job>();
                    IPage<Job> jobsPage = await _amsClient.AMSclient.Jobs.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, transform.Name);
                    while (jobsPage != null)
                    {
                        listjobs.AddRange(jobsPage);
                        if (jobsPage.NextPageLink != null)
                        {
                            jobsPage = await _amsClient.AMSclient.Jobs.ListNextAsync(jobsPage.NextPageLink);
                        }
                        else
                        {
                            jobsPage = null;
                        }
                    }

                    deleteTasks.AddRange(listjobs.ToList()
                        .Where(j => j.State == Microsoft.Azure.Management.Media.Models.JobState.Processing || j.State == Microsoft.Azure.Management.Media.Models.JobState.Queued || j.State == Microsoft.Azure.Management.Media.Models.JobState.Scheduled)
                        .Select(j => _amsClient.AMSclient.Jobs.CancelJobAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, transform.Name, j.Name)));
                }

                TextBoxLogWriteLine("Canceling {0} job(s)...", deleteTasks.Count);
                try
                {
                    await Task.WhenAll(deleteTasks.ToArray());
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when canceling the job(s).", true);
                    TextBoxLogWriteLine(ex);
                    Error = true;
                }

                if (!Error)
                {
                    TextBoxLogWriteLine("Job(s) canceled.");
                }

                DoRefreshGridJobV(false);
            }
        }

        private async Task DoDeleteTransformsAsync(List<Transform> SelectedTransforms)
        {
            if (SelectedTransforms.Count > 0)
            {
                string question = (SelectedTransforms.Count == 1) ? "Delete " + SelectedTransforms[0].Name + " ?" : "Delete these " + SelectedTransforms.Count + " transforms ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Transform deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    await _amsClient.RefreshTokenIfNeededAsync();
                    bool Error = false;
                    Task[] deleteTasks = SelectedTransforms.ToList().Select(t => _amsClient.AMSclient.Transforms.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, t.Name)).ToArray();
                    TextBoxLogWriteLine("Deleting transform(s)...");
                    try
                    {
                        await Task.WhenAll(deleteTasks);
                    }
                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when deleting the transform(s).", true);
                        TextBoxLogWriteLine(ex);
                        Error = true;
                    }
                    if (!Error)
                    {
                        TextBoxLogWriteLine("Transform(s) deleted.");
                    }
                    DoRefreshGridTransformV(false);
                }
            }
        }

        private void dASHIFHTML5ReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerDASHIFList);
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
            DpiUtils.InitPerMonitorDpi(this);
            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesDpiScale(this);

            Hide();

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

            comboBoxFilterTimeLiveEvent.Items.AddRange(
                typeof(FilterTime)
                .GetFields()
                .Select(i => i.GetValue(null) as string)
                .ToArray()
                );
            comboBoxFilterTimeLiveEvent.SelectedIndex = 0;

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
            Task.Run(async () => await DoRefreshGridLiveEventVAsync(true));
            DoRefreshGridLiveOutputV(true);
            Task.Run(async () => await DoRefreshGridStreamingEndpointVAsync(true));
            Task.Run(async () => await DoRefreshGridStorageVAsync(true));
            Task.Run(async () => await DoRefreshGridFiltersVAsync(true));
            Task.Run(async () => await DoRefreshGridCKPoliciesVAsync(true));

            DisplaySplashDuringLoading = false;

            UpdateLabelConcurrentTransfers();

            // making sure the visible assets are analyzed
            //Task.Run(async () => await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync());

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
        private async void Btnc_Click(object sender, EventArgs e)
        {
            textBoxSearchNameLiveEvent.Text = string.Empty;
            await DoLiveEventSearchAsync();
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
                Asset asset = await GetAssetAsync(dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV.Columns["Name"].Index].Value.ToString());
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

        private void DataGridViewAssetsV_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int indextype = dataGridViewAssetsV.Columns["Type"].Index;//2
            int indexlocalexp = dataGridViewAssetsV.Columns[dataGridViewAssetsV._locatorexpirationdate].Index; //13
            int indexassetwarning = dataGridViewAssetsV.Columns[dataGridViewAssetsV._assetwarning].Index;

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

        private async void toolStripMenuItemDisplayInfo_Click(object sender, EventArgs e)
        {
            DisplayInfo((await ReturnSelectedAssetsV3Async()).FirstOrDefault());
        }

        private async void contextMenuStripAssets_Opening(object sender, CancelEventArgs e)
        {
            List<Asset> assets = await ReturnSelectedAssetsV3Async();
            bool singleitem = (assets.Count == 1);

            ContextMenuItemAssetDisplayInfo.Enabled =
            ContextMenuItemAssetEditDescription.Enabled =
            editAlternateIdToolStripMenuItem.Enabled =
            contextMenuExportFilesToStorage.Enabled =
            createAnAssetFilterToolStripMenuItem.Enabled = singleitem;
        }


        private async void toolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            await DoMenuChangeAssetDescriptionAsync();
        }


        private void toolStripMenuAsset_DropDownOpening(object sender, EventArgs e)
        {

        }

        private async void toolStripMenuJobDisplayInfo_Click(object sender, EventArgs e)
        {
            DisplayInfo((await ReturnSelectedJobsV3Async()).FirstOrDefault());
        }

        private async void contextMenuStripJobs_Opening(object sender, CancelEventArgs e)
        {
            bool singleitem = (await ReturnSelectedJobsV3Async()).Count == 1;
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
                            await _amsClient.RefreshTokenIfNeededAsync();
                            Asset asset = await GetAssetAsync(location);
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

        private async Task DoChangeJobPriorityAsync()
        {
            List<JobExtension> SelectedJobs = await ReturnSelectedJobsV3Async();

            if (SelectedJobs.Count > 0)
            {
                PriorityForm form = new PriorityForm()
                {
                    JobPriority = (SelectedJobs.Count == 1) ? SelectedJobs[0].Job.Priority : Priority.Normal // if only one job so we pass the current priority to dialog box
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    List<Task> prioTasks = new List<Task>();
                    await _amsClient.RefreshTokenIfNeededAsync();

                    foreach (JobExtension JobToProcess in SelectedJobs)
                    {
                        if (JobToProcess != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Changing priority to {0} for job '{1}'...", form.JobPriority, JobToProcess.Job.Name);
                            JobToProcess.Job.Priority = form.JobPriority;
                            prioTasks.Add(_amsClient.AMSclient.Jobs.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, JobToProcess.TransformName, JobToProcess.Job.Name, JobToProcess.Job));
                        }
                    }
                    try
                    {
                        await Task.WhenAll(prioTasks.ToArray());
                        TextBoxLogWriteLine("Job(s) updated.");
                    }

                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when changing priority for job(s).", true);
                        TextBoxLogWriteLine(e);
                    }
                }
            }
        }

        private async void changePriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoChangeJobPriorityAsync();
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



        private async void withMPEGDASHIFReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType.DASHIFRefPlayer);
        }


        private async Task DoCreateAssetReportEmailAsync()
        {
            AssetInfo AR = new AssetInfo(await ReturnSelectedAssetsV3Async(), _amsClient);
        }

        private async Task DoDisplayAssetReportAsync()
        {
            AssetInfo AR = new AssetInfo(await ReturnSelectedAssetsV3Async(), _amsClient);
            StringBuilder SB = await AR.GetStatsAsync();
            EditorXMLJSON tokenDisplayForm = new EditorXMLJSON("Asset report", SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
        }

        private async void createOutlookReportEmailToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            await DoCreateAssetReportEmailAsync();
        }


        private async void openOutputAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoOpenJobAssetAsync(false);
        }

        private async Task DoOpenJobAssetAsync(bool inputasset) // if false, then display first outputasset
        {
            List<JobExtension> SelectedJobs = await ReturnSelectedJobsV3Async();
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
                                    Asset assetIn = await GetAssetAsync(jobinputasset.AssetName);
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
                                    Asset assetOut = await GetAssetAsync(joboutputasset.AssetName);
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


        private async void inputAssetInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoOpenJobAssetAsync(true);
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

        private async void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoRefreshAsync();
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


        private async Task DoRefreshGridLiveEventVAsync(bool firstime)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            if (firstime)
            {
                await dataGridViewLiveEventsV.InitAsync(_amsClient);
            }

            await Task.Run(async () =>
             {
                 await dataGridViewLiveEventsV.RefreshLiveEventAsync(1);
                 //tabPageLive.Invoke(new Action(() => tabPageLive.Text = string.Format(AMSExplorer.Properties.Resources.TabLive + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, dataGridViewLiveEventsV.totalLiveEvents)));
                 tabPageLive.Invoke(t => t.Text = string.Format(AMSExplorer.Properties.Resources.TabLive + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, dataGridViewLiveEventsV.totalLiveEvents));
                 //labelLiveEvents.Invoke(new Action(() => labelLiveEvents.Text = string.Format(AMSExplorer.Properties.Resources.LabelChannel + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, dataGridViewLiveEventsV.totalLiveEvents)));
                 labelLiveEvents.Invoke(l => l.Text = string.Format(AMSExplorer.Properties.Resources.LabelChannel + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, dataGridViewLiveEventsV.totalLiveEvents));
             });
        }

        private void DoRefreshGridLiveOutputV(bool firstime)
        {

            if (firstime)
            {
                Debug.WriteLine("DoRefreshGridProgramVforsttime");
                dataGridViewLiveOutputV.Init(_amsClient);
            }
            else
            {
                Debug.WriteLine("DoRefreshGridProgramVNotforsttime");
            }

            Task.Run(async () =>
            {
                await dataGridViewLiveOutputV.RefreshLiveOutputsAsync(1);
                //labelPrograms.Invoke(new Action(() => labelPrograms.Text = string.Format(AMSExplorer.Properties.Resources.LabelProgram + " ({0})", dataGridViewLiveOutputV.DisplayedCount)));
                labelPrograms.Invoke(l => l.Text = string.Format(AMSExplorer.Properties.Resources.LabelProgram + " ({0})", dataGridViewLiveOutputV.DisplayedCount));
            });
        }

        private async Task DoRefreshGridStreamingEndpointVAsync(bool firstime)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            if (firstime)
            {
                await dataGridViewStreamingEndpointsV.InitAsync(_amsClient);
            }
            Debug.WriteLine("DoRefreshGridOriginsVNotforsttime");

            await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointsAsync();
            //tabPageAssets.Invoke(new Action(() => tabPageOrigins.Text = string.Format(AMSExplorer.Properties.Resources.TabOrigins + " ({0})", dataGridViewStreamingEndpointsV.DisplayedCount)));
            tabPageOrigins.Invoke(t => t.Text = string.Format(AMSExplorer.Properties.Resources.TabOrigins + " ({0})", dataGridViewStreamingEndpointsV.DisplayedCount));

        }


        private async Task DoRefreshGridStorageVAsync(bool firstime)
        {
            await _amsClient.RefreshTokenIfNeededAsync();
            MediaService amsaccount = await _amsClient.AMSclient.Mediaservices.GetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);

            if (firstime)
            {
                // Storage tab
                dataGridViewStorage.ColumnCount = 2;

                dataGridViewStorage.Columns[0].Name = "Name";
                dataGridViewStorage.Columns[0].HeaderText = "Name";
                dataGridViewStorage.Columns[0].Width = 200;
                dataGridViewStorage.Columns[1].Name = "Id";
                dataGridViewStorage.Columns[1].Visible = false;
                dataGridViewStorage.Columns[1].HeaderText = "Id";
                dataGridViewStorage.Columns[1].Width = 700;
            }
            dataGridViewStorage.Rows.Clear();

            foreach (StorageAccount storage in amsaccount.StorageAccounts)
            {
                string name = AMSClientV3.GetStorageName(storage.Id);
                string append = string.Empty;
                if (storage.Type == StorageAccountType.Primary)
                {
                    append = " (primary)";
                }

                int rowi = dataGridViewStorage.Rows.Add(name + append, storage.Id);
                if (storage.Type == StorageAccountType.Primary)
                {
                    dataGridViewStorage.Rows[rowi].Cells[0].Style.ForeColor = Color.Blue;
                    dataGridViewStorage.Rows[rowi].Cells[0].ToolTipText = "Primary storage account";
                }
            }
            tabPageStorage.Invoke(t => t.Text = string.Format(AMSExplorer.Properties.Resources.TabStorage + " ({0})", amsaccount.StorageAccounts.Count()));
        }


        public async Task DoRefreshGridFiltersVAsync(bool firstime)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

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

            // account filters
            List<AccountFilter> acctFilters = new List<AccountFilter>();
            IPage<AccountFilter> acctFiltersPage = await _amsClient.AMSclient.AccountFilters.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (acctFiltersPage != null)
            {
                acctFilters.AddRange(acctFiltersPage);
                if (acctFiltersPage.NextPageLink != null)
                {
                    acctFiltersPage = await _amsClient.AMSclient.AccountFilters.ListNextAsync(acctFiltersPage.NextPageLink);
                }
                else
                {
                    acctFiltersPage = null;
                }
            }

            foreach (AccountFilter filter in acctFilters)
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
            tabPageFilters.Invoke(t => t.Text = string.Format(AMSExplorer.Properties.Resources.TabFilters + " ({0})", acctFilters.Count()));
            //tabPageFilters.Text = string.Format(AMSExplorer.Properties.Resources.TabFilters + " ({0})", filters.Count());
        }


        public async Task DoRefreshGridCKPoliciesVAsync(bool firstime)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            if (firstime)
            {
                // Storage tab
                dataGridViewCKPolicies.ColumnCount = 5;
                dataGridViewCKPolicies.Columns[0].HeaderText = "Name";
                dataGridViewCKPolicies.Columns[0].Name = "Name";
                dataGridViewCKPolicies.Columns[0].ReadOnly = true;
                dataGridViewCKPolicies.Columns[0].Width = 100;
                dataGridViewCKPolicies.Columns[1].HeaderText = "Description";
                dataGridViewCKPolicies.Columns[1].Name = "Description";
                dataGridViewCKPolicies.Columns[1].Width = 135;
                dataGridViewCKPolicies.Columns[2].HeaderText = "Type";
                dataGridViewCKPolicies.Columns[2].Name = "Type";
                dataGridViewCKPolicies.Columns[2].Width = 135;
                dataGridViewCKPolicies.Columns[3].HeaderText = "Options";
                dataGridViewCKPolicies.Columns[3].Name = "Options";
                dataGridViewCKPolicies.Columns[3].Width = 110;
                dataGridViewCKPolicies.Columns[4].HeaderText = "Last modified";
                dataGridViewCKPolicies.Columns[4].Name = "LastModified";
                dataGridViewCKPolicies.Columns[4].Width = 110;
            }
            dataGridViewCKPolicies.Rows.Clear();


            int nbPol = 0;

            IPage<ContentKeyPolicy> ckPolicies = await _amsClient.AMSclient.ContentKeyPolicies.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);
            while (ckPolicies != null)
            {
                nbPol += ckPolicies.Count();
                foreach (ContentKeyPolicy ckPolicy in ckPolicies)
                {
                    string typeStr = null;

                    if (ckPolicy.Options != null && ckPolicy.Options.Count > 0)
                    {
                        List<string> listTypeConfig = new List<string>();
                        foreach (var option in ckPolicy.Options)
                        {
                            var typeConfig = option.Configuration.GetType();
                            if (typeConfig == typeof(ContentKeyPolicyPlayReadyConfiguration))
                            {
                                listTypeConfig.Add("PlayReady");
                            }
                            else if (typeConfig == typeof(ContentKeyPolicyWidevineConfiguration))
                            {
                                listTypeConfig.Add("Widevine");
                            }
                            else if (typeConfig == typeof(ContentKeyPolicyFairPlayConfiguration))
                            {
                                listTypeConfig.Add("FairPlay");
                            }
                        }
                        typeStr = string.Join(", ", listTypeConfig.Distinct());
                    }
                    try
                    {
                        int nbOptions = ckPolicy.Options.Count;
                        int rowi = dataGridViewCKPolicies.Rows.Add(ckPolicy.Name, ckPolicy.Description, typeStr, nbOptions, ckPolicy.LastModified);
                    }
                    catch
                    {
                        int rowi = dataGridViewCKPolicies.Rows.Add(ckPolicy.Name, "Error");
                    }
                }
                if (ckPolicies.NextPageLink != null)
                {
                    ckPolicies = await _amsClient.AMSclient.ContentKeyPolicies.ListNextAsync(ckPolicies.NextPageLink);
                }
                else
                {
                    ckPolicies = null;
                }
            }
            tabPageCKPol.Invoke(t => t.Text = string.Format("Content key policies ({0})", nbPol));
        }


        private async Task<List<LiveEvent>> ReturnSelectedLiveEventsAsync()
        {
            List<LiveEvent> SelectedLiveEvents = new List<LiveEvent>();
            foreach (DataGridViewRow Row in dataGridViewLiveEventsV.SelectedRows)
            {
                string liveEventName = string.Empty;
                try
                {
                    liveEventName = Row.Cells[dataGridViewLiveEventsV.Columns["Name"].Index].Value.ToString();
                    LiveEvent liveEvent = await GetLiveEventAsync(liveEventName);
                    // sometimes, the live event can be null (if just deleted)
                    if (liveEvent != null)
                    {
                        SelectedLiveEvents.Add(liveEvent);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error getting the live event : '{0}'.", liveEventName) + Constants.endline + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SelectedLiveEvents.Reverse();
            return SelectedLiveEvents;
        }

        private async Task<List<StreamingEndpoint>> ReturnSelectedStreamingEndpointsAsync()
        {
            List<StreamingEndpoint> SelectedOrigins = new List<StreamingEndpoint>();

            foreach (DataGridViewRow Row in dataGridViewStreamingEndpointsV.SelectedRows)
            {
                string seName = Row.Cells[dataGridViewStreamingEndpointsV.Columns["Name"].Index].Value.ToString();
                StreamingEndpoint se = await GetStreamingEndpointAsync(seName);
                if (se != null)
                {
                    SelectedOrigins.Add(se);
                }
            }
            SelectedOrigins.Reverse();
            return SelectedOrigins;
        }

        private async Task<List<LiveOutput>> ReturnSelectedLiveOutputsAsync()
        {
            List<LiveOutput> SelectedLiveOutputs = new List<LiveOutput>();

            foreach (DataGridViewRow Row in dataGridViewLiveOutputV.SelectedRows)
            {
                string liveOutputName = string.Empty;
                try
                {
                    string eventName = Row.Cells[dataGridViewLiveOutputV.Columns["LiveEventName"].Index].Value.ToString();
                    liveOutputName = Row.Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString();
                    LiveOutput liveOutput = await GetLiveOutputAsync(eventName, liveOutputName);
                    if (liveOutput != null)
                    {
                        SelectedLiveOutputs.Add(liveOutput);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error getting the live output : '{0}'.", liveOutputName) + Constants.endline + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SelectedLiveOutputs.Reverse();
            return SelectedLiveOutputs;
        }

        private async Task DoStartLiveEventsAsync()
        {
            // let's start the live events
            await DoStartLiveEventsEngineAsync(await ReturnSelectedLiveEventsAsync());

        }

        private async Task DoStopOrDeleteLiveEventsAsync(bool deleteLiveEvents)
        {
            // delete also if delete = true
            List<LiveEvent> ListEvents = await ReturnSelectedLiveEventsAsync();
            List<LiveOutput> LOList = new List<LiveOutput>();
            await _amsClient.RefreshTokenIfNeededAsync();

            foreach (LiveEvent le in ListEvents)
            {
                var listOutputs = await _amsClient.AMSclient.LiveOutputs.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, le.Name);
                LOList.AddRange(listOutputs.ToList());
            }

            string liveEventStr = ListEvents.Count > 1 ? "live events" : "live event";

            if (ListEvents.Count > 0)
            {
                if (LOList.Count > 0) // There are live outputs associated to the live event(s) to be deleted
                {
                    string leaction = deleteLiveEvents ? "Delete" : "Stop";
                    string question = (LOList.Count == 1) ? string.Format("There is one live output associated to the {0}.\n{1} the {0} and delete live output '{2}' ?", liveEventStr, leaction, LOList[0].Name)
                                                        : string.Format("There are {0} live outputs associated to the {1}.\n{2} the {1} and delete these live outputs ?", LOList.Count, liveEventStr, leaction);

                    DeleteLiveOutputEvent form = new DeleteLiveOutputEvent(question, "Delete");
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await DoDeleteLiveOutputsEngineAsync(LOList, form.DeleteAsset);
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
                await DoStopOrDeleteLiveEventsEngineAsync(ListEvents, deleteLiveEvents);
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

        private async Task DoResetLiveEventsAsync()
        {
            List<LiveEvent> ListEvents = await ReturnSelectedLiveEventsAsync();
            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();
            await _amsClient.RefreshTokenIfNeededAsync();

            foreach (LiveEvent le in ListEvents)
            {
                List<LiveOutput> plist = (await _amsClient.AMSclient.LiveOutputs.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, le.Name)).ToList();
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
                        try
                        {
                            await DoDeleteLiveOutputsAsync(liveOutputRunningQuery.Select(o => o.LiveOutputItem).ToList());

                            // let's reset the live events now that running output are stopped
                            ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Reseting live event '{0}'...", e.Name));
                            Task[] tasksreset = ListEvents.Select(c => _amsClient.AMSclient.LiveEvents.ResetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();
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
                }
                else
                {
                    string question = (ListEvents.Count == 1) ? string.Format("Reset live event '{0}' ?", ListEvents[0].Name) : string.Format("Reset these {0} live event(s) ?", ListEvents.Count);

                    if (System.Windows.Forms.MessageBox.Show(question, "Live event reset", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // let's reset the events

                        try
                        {
                            // let's reset the live events now that live outputs are deleted
                            ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Reseting live event '{0}'...", e.Name));
                            Task[] tasksreset = ListEvents.Select(c => _amsClient.AMSclient.LiveEvents.ResetAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();
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
                }
            }
        }

        private async void createLiveEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoCreateLiveEventAsync();
        }

        private async Task DoCreateLiveEventAsync()
        {
            LiveEventCreation form = new LiveEventCreation(_amsClient)
            {
                KeyframeIntervalSerialized = XmlConvert.ToString(Properties.Settings.Default.LiveKeyFrameInterval),
                StartLiveEventNow = true
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                TextBoxLogWriteLine("Creating live event '{0}'...", form.LiveEventName);

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
                                                                 location: _amsClient.credentialsEntry.MediaService.Location,
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
                    if (form.LiveTranscript)
                    {
                        // let's use REST call
                        var client = new AmsClientRestLiveTranscript(_amsClient);

                        var liveEventForREst = new LiveEventForRest(
                                              name: liveEvent.Name,
                                              location: liveEvent.Location,
                                              transcriptions: form.LiveTranscriptionList,
                                              description: liveEvent.Description,
                                              vanityUrl: liveEvent.VanityUrl,
                                              encoding: liveEvent.Encoding,
                                              input: liveEvent.Input,
                                              preview: liveEvent.Preview,
                                              streamOptions: liveEvent.StreamOptions
                                                            );

                        try
                        {
                            await client.CreateLiveEventAsync(liveEventForREst, form.StartLiveEventNow);
                            TextBoxLogWriteLine("Live event '{0}' created using REST call.", form.LiveEventName);
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine("Error with live event creation using REST call.", true);
                            TextBoxLogWriteLine(ex);
                        }
                    }

                    else
                    {
                        // let's use the SDK

                        try
                        {
                            await Task.Run(() =>
                             _amsClient.AMSclient.LiveEvents.CreateAsync(
                                                                             _amsClient.credentialsEntry.ResourceGroup,
                                                                             _amsClient.credentialsEntry.AccountName,
                                                                             form.LiveEventName,
                                                                             liveEvent,
                                                                             autoStart: form.StartLiveEventNow ? true : false)
                                                                          );
                            TextBoxLogWriteLine("Live event '{0}' created.", form.LiveEventName);
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine("Error with live event creation.", true);
                            TextBoxLogWriteLine(ex);
                        }
                    }
                    await DoRefreshGridLiveEventVAsync(false);
                }
            }
        }


        private async Task DoDisplayLiveEventInfoAsync()
        {
            await DoDisplayLiveEventInfoAsync(await ReturnSelectedLiveEventsAsync());
        }


        private async Task DoDisplayLiveEventInfoAsync(List<LiveEvent> liveEvents)
        {
            LiveEvent firstLiveEvent = liveEvents.FirstOrDefault();
            bool multiselection = liveEvents.Count > 1;

            if (firstLiveEvent != null)
            {
                LiveEventInformation form = new LiveEventInformation(this, _amsClient)
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
                        await _amsClient.RefreshTokenIfNeededAsync();

                        try
                        {
                            await _amsClient.AMSclient.LiveEvents.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, liveEvent.Name, liveEvent);
                            dataGridViewLiveEventsV.BeginInvoke(new Action(async () => await dataGridViewLiveEventsV.RefreshLiveEventAsync(liveEvent)), null);
                            TextBoxLogWriteLine("Live event '{0}' : updated.", liveEvent.Name);
                        }

                        catch (Exception ex)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when updating a live event.", true);
                            TextBoxLogWriteLine(ex);
                        }
                    }
                }
            }
        }

        private async void dataGridViewLiveV_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonChSelected.Checked) // only in select mode
            {
                Debug.WriteLine("live event selection changed : begin");
                List<LiveEvent> SelectedLiveEvents = await ReturnSelectedLiveEventsAsync();
                if (SelectedLiveEvents.Count > 0)
                {
                    dataGridViewLiveOutputV.LiveEventSourceNames = SelectedLiveEvents.Select(c => c.Name).ToList();
                    Debug.WriteLine("live event selection changed : before refresh");
                    DoRefreshGridLiveOutputV(false);
                }
            }
        }

        private async Task DoStopOrDeleteLiveEventsEngineAsync(List<LiveEvent> ListEvents, bool deleteLiveEvents)
        {
            // Stop the live events which run
            List<LiveEvent> liveeventsrunning = ListEvents.Where(p => p.ResourceState == LiveEventResourceState.Running).ToList();
            string names = string.Join(", ", liveeventsrunning.Select(le => le.Name).ToArray());

            if (liveeventsrunning.Count() > 0)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                try
                {
                    TextBoxLogWriteLine("Stopping live event(s) : {0}...", names);
                    List<LiveEventResourceState?> states = liveeventsrunning.Select(p => p.ResourceState).ToList();
                    Task[] taskcstop = liveeventsrunning.Select(c => _amsClient.AMSclient.LiveEvents.StopAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();

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
                                dataGridViewLiveEventsV.BeginInvoke(new Action(async () => await dataGridViewLiveEventsV.RefreshLiveEventAsync(loitemR)), null);
                                if (loitemR.ResourceState == LiveEventResourceState.Stopped)
                                {
                                    TextBoxLogWriteLine("Live event stopped : {0}.", loitemR.Name);
                                    complete++;
                                }
                            }
                        }
                        await Task.Delay(2000);
                    }
                    await Task.WhenAll(taskcstop);

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
                await _amsClient.RefreshTokenIfNeededAsync();

                // delete the live events
                try
                {
                    string names2 = string.Join(", ", ListEvents.Select(le => le.Name).ToArray());

                    TextBoxLogWriteLine("Deleting live event(s) : {0}...", names2);
                    List<LiveEventResourceState?> states = ListEvents.Select(p => p.ResourceState).ToList();
                    Task[] taskcdel = ListEvents.Select(c => _amsClient.AMSclient.LiveEvents.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();

                    while (!taskcdel.All(t => t.IsCompleted))
                    {
                        // refresh the live events

                        foreach (LiveEvent loitem in ListEvents)
                        {
                            LiveEvent loitemR = Task.Run(async () => await GetLiveEventAsync(loitem.Name)).Result;
                            if (loitemR != null && states[ListEvents.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[ListEvents.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(async () => await dataGridViewLiveEventsV.RefreshLiveEventAsync(loitemR)), null);
                            }
                            else if (loitemR != null)
                            {
                                await DoRefreshGridLiveEventVAsync(false);
                            }
                        }
                        await Task.Delay(2000);
                    }
                    await Task.WhenAll(taskcdel);
                    TextBoxLogWriteLine("Live event(s) deleted : {0}.", names2);
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when deleting a live event.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
            await DoRefreshGridLiveEventVAsync(false);
        }


        private async Task DoStartLiveEventsEngineAsync(List<LiveEvent> ListEvents)
        {
            // Start the live events which are stopped
            List<LiveEvent> liveevntsstopped = ListEvents.Where(p => p.ResourceState == LiveEventResourceState.Stopped).ToList();
            string names = string.Join(", ", liveevntsstopped.Select(le => le.Name).ToArray());
            if (liveevntsstopped.Count() > 0)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                try
                {
                    TextBoxLogWriteLine("Starting live event(s) : {0}...", names);
                    List<LiveEventResourceState?> states = liveevntsstopped.Select(p => p.ResourceState).ToList();
                    Task[] taskLEStart = liveevntsstopped.Select(c => _amsClient.AMSclient.LiveEvents.StartAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();
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
                                dataGridViewLiveEventsV.BeginInvoke(new Action(async () => await dataGridViewLiveEventsV.RefreshLiveEventAsync(loitemR)), null);
                                if (loitemR.ResourceState == LiveEventResourceState.Running)
                                {
                                    TextBoxLogWriteLine("Live event started : {0}.", loitemR.Name);
                                    complete++;
                                }
                            }
                        }
                        await Task.Delay(2000);
                    }
                    await Task.WhenAll(taskLEStart);
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when starting a live event.", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            await DoRefreshGridLiveEventVAsync(false);
        }


        private async Task DoDeleteLiveOutputsAsync(List<LiveOutput> ListOutputs = null)
        {
            // delete also if delete = true
            if (ListOutputs == null)
            {
                ListOutputs = await ReturnSelectedLiveOutputsAsync();
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
            await _amsClient.RefreshTokenIfNeededAsync();

            try
            {   // delete programs
                ListOutputs.ToList().ForEach(p => TextBoxLogWriteLine("Live output '{0}' : deleting...", p.Name));
                List<LiveOutputResourceState?> states = ListOutputs.Select(p => p.ResourceState).ToList();
                Task[] tasks = ListOutputs.Select(p => _amsClient.AMSclient.LiveOutputs.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, LiveOutputUtil.ReturnLiveEventFromOutput(p), p.Name)).ToArray();

                while (!tasks.All(t => t.IsCompleted))
                {
                    // refresh the programs

                    foreach (LiveOutput loitem in ListOutputs)
                    {
                        LiveOutput loitemR = Task.Run(async () => await GetLiveOutputAsync(LiveOutputUtil.ReturnLiveEventFromOutput(loitem), loitem.Name)).Result;
                        if (loitemR != null && states[ListOutputs.IndexOf(loitem)] != loitemR.ResourceState)
                        {
                            states[ListOutputs.IndexOf(loitem)] = loitemR.ResourceState;
                            dataGridViewLiveOutputV.BeginInvoke(new Action(async () => await dataGridViewLiveOutputV.RefreshLiveOutputAsync(LiveOutputUtil.ReturnLiveEventFromOutput(loitemR), loitemR)), null);
                        }
                        else if (loitemR != null)
                        {
                            //DoRefreshGridLiveOutputV(false);
                        }
                    }
                    await Task.Delay(2000);
                }
                await Task.WhenAll(tasks);
                TextBoxLogWriteLine("Live output(s) deleted.");
            }
            catch (Exception ex)
            {
                // Add useful information to the exception
                TextBoxLogWriteLine("There is a problem when deleting a live output.", true);
                TextBoxLogWriteLine(ex);
                //Error = true;
            }
            DoRefreshGridLiveOutputV(false);


            if (DeleteAsset && Error == false)
            {
                assets.ToList().ForEach(a => TextBoxLogWriteLine("Asset '{0}' : deleting...", a));
                Task[] tasksassets = assets.Select(a => _amsClient.AMSclient.Assets.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, a)).ToArray();
                try
                {
                    await Task.WhenAll(tasksassets);
                    TextBoxLogWriteLine("Asset(s) deletion done.");
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when deleting an asset.", true);
                    TextBoxLogWriteLine(ex);
                }
                DoRefreshGridAssetV(false);
            }
        }

        private async Task DoStartStreamingEndpointEngineAsync(List<StreamingEndpoint> ListStreamingEndpoints)
        {
            // Start the streaming endpoint which are stopped
            List<StreamingEndpoint> streamingendpointsstopped = ListStreamingEndpoints.Where(p => p.ResourceState == StreamingEndpointResourceState.Stopped).ToList();
            string names = string.Join(", ", streamingendpointsstopped.Select(le => le.Name).ToArray());
            if (streamingendpointsstopped.Count() > 0)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                try
                {
                    TextBoxLogWriteLine("Starting streaming endpoint(s) : {0}...", names);
                    List<StreamingEndpointResourceState?> states = streamingendpointsstopped.Select(p => p.ResourceState).ToList();
                    Task[] taskSEStart = streamingendpointsstopped.Select(c => _amsClient.AMSclient.StreamingEndpoints.StartAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();
                    int complete = 0;

                    while (!taskSEStart.All(t => t.IsCompleted) && complete != streamingendpointsstopped.Count)
                    {
                        // refresh the live events

                        foreach (StreamingEndpoint loitem in streamingendpointsstopped)
                        {
                            StreamingEndpoint loitemR = await GetStreamingEndpointAsync(loitem.Name);

                            if (loitemR != null && states[streamingendpointsstopped.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[streamingendpointsstopped.IndexOf(loitem)] = loitemR.ResourceState;

                                await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointAsync(loitemR);

                                if (loitemR.ResourceState == StreamingEndpointResourceState.Running)
                                {
                                    TextBoxLogWriteLine("Streaming endpoint started : {0}.", loitemR.Name);
                                    complete++;
                                }
                            }
                        }
                        await Task.Delay(2000);
                    }
                    await Task.WhenAll(taskSEStart);

                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when starting a streaming endpoint.", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            await DoRefreshGridStreamingEndpointVAsync(false);
        }


        private async Task DoUpdateAndScaleStreamingEndpointEngineAsync(StreamingEndpoint se, int? units = null)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            try
            {
                TextBoxLogWriteLine("Updating streaming endpoint '{0}'...", se.Name);
                await _amsClient.AMSclient.StreamingEndpoints.UpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, se.Name, se);
                TextBoxLogWriteLine("Streaming endpoint '{0}' updated.", se.Name);

                if (units != null)
                {
                    TextBoxLogWriteLine("Scaling streaming endpoint '{0}'...", se.Name);
                    await _amsClient.AMSclient.StreamingEndpoints.ScaleAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, se.Name, units);
                    TextBoxLogWriteLine("Streaming endpoint '{0}' scaled.", se.Name);
                }

            }
            catch (Exception ex)
            {
                // Add useful information to the exception
                TextBoxLogWriteLine("There is a problem when updating/scaling a streaming endpoint.", true);
                TextBoxLogWriteLine(ex);
            }
            await DoRefreshGridStreamingEndpointVAsync(false);
        }


        private async Task DoStopOrDeleteStreamingEndpointsEngineAsync(List<StreamingEndpoint> ListStreamingEndpoints, bool deleteStreamingEndpoints)
        {

            // Stop the streaming endpoints which run
            List<StreamingEndpoint> sesrunning = ListStreamingEndpoints.Where(p => p.ResourceState == StreamingEndpointResourceState.Running).ToList();
            string names = string.Join(", ", sesrunning.Select(le => le.Name).ToArray());

            if (sesrunning.Count() > 0)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                try
                {
                    TextBoxLogWriteLine("Stopping streaming endpoints(s) : {0}...", names);
                    List<StreamingEndpointResourceState?> states = sesrunning.Select(p => p.ResourceState).ToList();
                    Task[] taskSEstop = sesrunning.Select(c => _amsClient.AMSclient.StreamingEndpoints.StopAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();

                    int complete = 0;
                    while (!taskSEstop.All(t => t.IsCompleted) && complete != sesrunning.Count)
                    {
                        // refresh the streaming endpoints

                        foreach (StreamingEndpoint loitem in sesrunning)
                        {
                            StreamingEndpoint loitemR = await GetStreamingEndpointAsync(loitem.Name);
                            if (loitemR != null && states[sesrunning.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[sesrunning.IndexOf(loitem)] = loitemR.ResourceState;
                                await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointAsync(loitemR);

                                if (loitemR.ResourceState == StreamingEndpointResourceState.Stopped)
                                {
                                    TextBoxLogWriteLine("Streaming endpoint '{0}' stopped.", loitemR.Name);
                                    complete++;
                                }
                            }
                        }
                        await Task.Delay(2000);
                    }
                    await Task.WhenAll(taskSEstop);
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
                    TextBoxLogWriteLine("Deleting streaming endpoints(s) : {0}...", names2);

                    List<StreamingEndpointResourceState?> states = ListStreamingEndpoints.Select(p => p.ResourceState).ToList();
                    Task[] taskSEdel = ListStreamingEndpoints.Select(c => _amsClient.AMSclient.StreamingEndpoints.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, c.Name)).ToArray();

                    while (!taskSEdel.All(t => t.IsCompleted))
                    {
                        // refresh
                        foreach (StreamingEndpoint loitem in ListStreamingEndpoints)
                        {
                            StreamingEndpoint loitemR = await GetStreamingEndpointAsync(loitem.Name);
                            if (loitemR != null && states[ListStreamingEndpoints.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[ListStreamingEndpoints.IndexOf(loitem)] = loitemR.ResourceState;
                                await dataGridViewStreamingEndpointsV.RefreshStreamingEndpointAsync(loitemR);
                            }
                            else if (loitemR != null)
                            {
                                await DoRefreshGridStreamingEndpointVAsync(false);
                            }
                        }
                        await Task.Delay(2000);
                    }
                    await Task.WhenAll(taskSEdel);
                    TextBoxLogWriteLine("Streaming endpoint(s) deleted : {0}.", names2);
                }

                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when deleting a streaming endpoint.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
            await DoRefreshGridStreamingEndpointVAsync(false);
        }


        private async Task DoCreateLiveOutputAsync()
        {
            LiveEvent liveEvent = (await ReturnSelectedLiveEventsAsync()).FirstOrDefault();
            if (liveEvent != null)
            {
                string uniqueness = Program.GetUniqueness();

                LiveOutputCreation form = new LiveOutputCreation(_amsClient)
                {
                    LiveEventName = liveEvent.Name,
                    ArchiveWindowLength = new TimeSpan(0, 5, 0),
                    CreateLocator = true,
                    AssetName = Constants.NameconvLiveEvent + "-" + Constants.NameconvLiveOutput,
                    LiveOutputName = "LiveOutput-" + uniqueness,
                    HLSFragmentPerSegment = Properties.Settings.Default.LiveHLSFragmentsPerSegment,
                    ManifestName = uniqueness
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await _amsClient.RefreshTokenIfNeededAsync();

                    string assetname = form.AssetName.Replace(Constants.NameconvLiveOutput, form.LiveOutputName).Replace(Constants.NameconvLiveEvent, form.LiveEventName);
                    Asset newAsset = new Asset() { StorageAccountName = form.StorageSelected };

                    Asset asset;

                    try
                    {
                        TextBoxLogWriteLine("Creating asset '{0}'...", assetname);
                        asset = await _amsClient.AMSclient.Assets.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, assetname, newAsset);
                        TextBoxLogWriteLine("Asset '{0}' created.", assetname);

                        TextBoxLogWriteLine("Creating live output '{0}'...", form.LiveOutputName);

                        Hls hlsParam = null;
                        if (form.HLSFragmentPerSegment != null)
                        {
                            hlsParam = new Hls(fragmentsPerTsSegment: form.HLSFragmentPerSegment);
                        }

                        LiveOutput liveOutput = new LiveOutput(
                            asset.Name,
                            form.ArchiveWindowLength,
                            null,
                            form.LiveOutputName,
                            null,
                            form.ProgramDescription,
                            form.ManifestName ?? uniqueness,
                            hlsParam,
                            form.StartRecordTimestamp
                            );

                        LiveOutput liveOutput2 = await _amsClient.AMSclient.LiveOutputs.CreateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, liveEvent.Name, form.LiveOutputName, liveOutput);
                        TextBoxLogWriteLine("Live output '{0}' created.", liveOutput2.Name);

                        if (form.CreateLocator)
                        {
                            try
                            {
                                await DoCreateLocatorAsync(new List<Asset> { asset }, form.ManifestName);
                            }
                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when publishing the asset of the live output.", true);
                                TextBoxLogWriteLine(ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when creating a live output.", true);
                        TextBoxLogWriteLine(ex);
                    }

                    DoRefreshGridLiveOutputV(false);

                }
            }
        }

        private async void createProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoCreateLiveOutputAsync();
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


        private async Task DoDisplayLiveOutputInfoAsync()
        {
            DoDisplayLiveOutputInfo(await ReturnSelectedLiveOutputsAsync());
        }

        private void DoDisplayLiveOutputInfo(List<LiveOutput> liveoutputs)
        {
            bool multiselection = liveoutputs.Count > 1;
            if (liveoutputs.FirstOrDefault() != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    LiveOutputInformation form = new LiveOutputInformation(this, _amsClient)
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

        private async Task DoDisplayStreamingEndpointInfoAsync()
        {
            DoDisplayStreamingEndpointInfo(await ReturnSelectedStreamingEndpointsAsync());
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


        private async Task DoStartStreamingEndpointsAsync()
        {
            await DoStartStreamingEndpointEngineAsync(await ReturnSelectedStreamingEndpointsAsync());
        }

        private async Task DoStopStreamingEndpointsAsync()
        {
            await DoStopOrDeleteStreamingEndpointsEngineAsync(await ReturnSelectedStreamingEndpointsAsync(), false);
        }

        private async Task DoDeleteStreamingEndpointsAsync()
        {
            List<StreamingEndpoint> SelectedOrigins = await ReturnSelectedStreamingEndpointsAsync();
            if (SelectedOrigins.Count > 0)
            {
                string question = (SelectedOrigins.Count == 1) ? "Delete streaming endpoint " + SelectedOrigins[0].Name + " ?" : "Delete these " + SelectedOrigins.Count + " streaming endpoints ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Streaming endpoint(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    await DoStopOrDeleteStreamingEndpointsEngineAsync(await ReturnSelectedStreamingEndpointsAsync(), true);
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
                                                                 location: _amsClient.credentialsEntry.MediaService.Location
                                                                 );
                _amsClient.RefreshTokenIfNeeded();

                Task.Run(async () =>
                {

                    try
                    {
                        TextBoxLogWriteLine("Streaming endpoint creation...");
                        StreamingEndpoint secreated = await _amsClient.AMSclient.StreamingEndpoints.CreateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, form.StreamingEndpointName, newStreamingEndpoint);
                        TextBoxLogWriteLine("Streaming endpoint created.");

                    }
                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when creating a streaming endpoint.", true);
                        TextBoxLogWriteLine(ex);
                    }
                    await DoRefreshGridStreamingEndpointVAsync(false);
                }
              );
            }
        }


        private async void displayLiveEventInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDisplayLiveEventInfoAsync();
        }

        private async void displayProgramInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoDisplayLiveOutputInfoAsync();
        }

        private async void dataGridViewLiveV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string liveEventName = string.Empty;
                try
                {
                    liveEventName = dataGridViewLiveEventsV.Rows[e.RowIndex].Cells[dataGridViewLiveEventsV.Columns["Name"].Index].Value.ToString();
                    LiveEvent liveEvent = await GetLiveEventAsync(liveEventName);
                    // sometimes, the live event can be null (if just deleted)
                    if (liveEvent != null)
                    {
                        await DoDisplayLiveEventInfoAsync((new List<LiveEvent>() { liveEvent }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error getting the live event : '{0}'.", liveEventName) + Constants.endline + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void dataGridViewProgramV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //_amsClient.RefreshTokenIfNeeded();
                string liveEventName = dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["LiveEventName"].Index].Value.ToString();
                string liveOutputName = dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString();

                try
                {
                    LiveOutput liveoutput = await GetLiveOutputAsync(liveEventName, liveOutputName);
                    if (liveoutput != null)
                    {
                        DoDisplayLiveOutputInfo(new List<LiveOutput>() { liveoutput });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error getting the live output : '{0}'.", liveOutputName) + Constants.endline + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private async void startLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoStartLiveEventsAsync();
        }

        private async void stopLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoStopOrDeleteLiveEventsAsync(false);
        }

        private async void resetLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoResetLiveEventsAsync();
        }

        private async void deleteLiveEventsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoStopOrDeleteLiveEventsAsync(true);
        }

        private async void deleteProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDeleteLiveOutputsAsync();
        }

        private async void displayOriginInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoDisplayStreamingEndpointInfoAsync();
        }

        private async void startOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoStartStreamingEndpointsAsync();
        }

        private async void stopOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoStopStreamingEndpointsAsync();
        }

        private async void deleteOriginsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoDeleteStreamingEndpointsAsync();
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

        private async Task DoPlaybackLiveEventPreviewAsync(PlayerType ptype)
        {
            foreach (LiveEvent liveEvent in (await ReturnSelectedLiveEventsAsync()))
            {
                if (liveEvent != null && liveEvent.Preview != null)
                {
                    if (liveEvent.Preview.Endpoints.FirstOrDefault() != null && liveEvent.Preview.Endpoints.FirstOrDefault().Url != null)
                    {
                        await AssetInfo.DoPlayBackWithStreamingEndpointAsync(
                               typeplayer: ptype,
                               path: liveEvent.Preview.Endpoints.FirstOrDefault().Url,
                               DoNotRewriteURL: true,
                               client: _amsClient,
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


        private async void copyPreviewURLToClipboard_Click(object sender, EventArgs e)
        {
            LiveEvent liveEvent = (await ReturnSelectedLiveEventsAsync()).FirstOrDefault();
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

        private async void batchUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoBatchUploadAsync();
        }

        private async Task DoBatchUploadAsync()
        {
            BatchUploadFrame1 form = new BatchUploadFrame1();
            if (form.ShowDialog() == DialogResult.OK)
            {
                BatchUploadFrame2 form2 = new BatchUploadFrame2(form.BatchFolder, form.BatchProcessFiles, form.BatchProcessSubFolders, _amsClient) { Left = form.Left, Top = form.Top };
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);

                    List<Task> MyTasks = new List<Task>();
                    int i = 0;
                    foreach (string folder in form2.BatchSelectedFolders)
                    {
                        i++;
                        TransferEntryResponse response = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(folder)), TransferType.UploadFromFolder, true);

                        IEnumerable<string> filePaths = Directory.EnumerateFiles(folder as string);

                        var myTask = ProcessUploadFileAndMoreV3Async(
                                  filePaths.ToList(),
                                  response.Id,
                                  response.token,
                                  storageaccount: form2.StorageSelected,
                                  blocksize: form2.BlockSize
                                  );

                        MyTasks.Add(myTask);

                        if (i == 10) // let's use a batch of 10 threads at the same time
                        {
                            try
                            {
                                await Task.WhenAll(MyTasks.ToArray());
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine(ex);
                            }
                            /*
                            do
                            {
                                await Task.Delay(1000);
                            }
                            while (ReturnTransfer(response.Id).State == TransferState.Queued);
                            */
                            i = 0;
                        }
                    }

                    foreach (string file in form2.BatchSelectedFiles)
                    {
                        i++;
                        TransferEntryResponse response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, true);

                        Task myTask = ProcessUploadFileAndMoreV3Async(
                                  new List<string>() { file },
                                  response.Id,
                                  response.token,
                                  storageaccount: form2.StorageSelected,
                                  blocksize: form2.BlockSize
                                  );

                        MyTasks.Add(myTask);

                        if (i == 10) // let's use a batch of 10 threads at the same time
                        {
                            try
                            {
                                await Task.WhenAll(MyTasks.ToArray());
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine(ex);
                            }
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

                    DoRefreshGridAssetV(false);
                }
            }
        }

        private void azureMediaBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkBlogAMS);
        }

        private async void createProgramToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            await DoCreateLiveOutputAsync();
        }

        private async void createLiveEventToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoCreateLiveEventAsync();
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



        private async void createALocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoCreateLocatorAsync(await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync());
        }

        private async void deleteAllLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDeleteAllLocatorsOnAssetsAsync(await ReturnSelectedAssetsV3Async());
        }


        private async Task DoDisplayOutputURLAssetOrProgramToWindowAsync()
        {
            Asset asset = (await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync()).FirstOrDefault();
            if (asset != null)
            {
                Uri ValidURI = await AssetInfo.GetValidOnDemandSmoothURIAsync(asset, _amsClient);
                if (ValidURI != null)
                {
                    string url = ValidURI.AbsoluteUri;
                    ChooseStreamingEndpoint form = new ChooseStreamingEndpoint(_amsClient, asset, url);
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

        private async void withCustomPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType.CustomPlayer);
        }

        private async Task DoMenuCreateLocatorOnProgramsAsync()
        {
            List<Asset> SelectedAssets = await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync();
            await DoCreateLocatorAsync(SelectedAssets);
            DoRefreshGridLiveOutputV(false);
        }

        private async void createALocatorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            await DoMenuCreateLocatorOnProgramsAsync();
        }

        private async void deleteAllLocatorsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoMenuDeleteAllLocatorsOnProgramsAsync();
        }

        private async Task DoMenuDeleteAllLocatorsOnProgramsAsync()
        {
            await DoDeleteAllLocatorsOnAssetsAsync(await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync());
            DoRefreshGridLiveOutputV(false);
        }

        private async void displayRelatedAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoMenuDisplayAssetInfoOfProgramAsync();
        }

        private async Task DoMenuDisplayAssetInfoOfProgramAsync()
        {
            List<Asset> SelectedAssets = await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync();
            if (SelectedAssets.Count > 0)
            {
                DisplayInfo(SelectedAssets.FirstOrDefault());
            }
        }

        private async void withCustomPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType.CustomPlayer);
        }


        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoRefreshGridAssetV(false);
        }

        private void refreshToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoRefreshGridJobV(false);
        }

        private async void refreshToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            await DoRefreshGridLiveEventVAsync(false);
        }

        private void refreshToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DoRefreshGridLiveOutputV(false);
        }

        private async void refreshToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            await DoRefreshGridStreamingEndpointVAsync(false);
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


        private async void attachAnotherStoragheAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoAttachAnotherStorageAccountAsync();
        }

        private async Task DoAttachAnotherStorageAccountAsync()
        {
            AttachStorage form = new AttachStorage(_amsClient);

            if (form.ShowDialog() == DialogResult.OK)
            {

                // Update storage accounts
                try
                {
                    TextBoxLogWriteLine("Processing Attach/Detach Storage account(s)...");

                    await form.UpdateStorageAccountsAsync();

                    TextBoxLogWriteLine("Storage account attached/detached.");
                    await DoRefreshGridStorageVAsync(false);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when processing storage account attach/detach.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
        }

        private async Task DoDisplayJobErrorAsync()
        {
            List<JobExtension> SelectedJobs = await ReturnSelectedJobsV3Async();
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

        private async void displayErrorToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            await DoDisplayJobErrorAsync();
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

        private async Task DoSelectTransformAndSubmitJobAsync()
        {
            List<Asset> SelectedAssets = await ReturnSelectedAssetsV3Async();

            //CheckAssetSizeRegardingMediaUnit(SelectedAssets);
            JobSubmitFromTransform form = new JobSubmitFromTransform(_amsClient, this, SelectedAssets);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.SelectedAssetsMode) // assets selected
                {
                    await CreateAndSubmitJobsAsync(new List<Transform>() { form.SelectedTransform }, SelectedAssets, form.StartClipTime, form.EndClipTime, null, form.ExistingOutputAsset, form.OutputAssetNameSyntax);
                }
                else // http source url instead
                {
                    await CreateAndSubmitJobsAsync(new List<Transform>() { form.SelectedTransform }, form.GetURL.OriginalString, form.StartClipTime, form.EndClipTime, form.ExistingOutputAsset, form.OutputAssetNameSyntax);
                }

                //await dataGridViewTransformsV.RefreshTransformsAsync();
                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs, form.SelectedTransform);
            }
        }


        private async void dataGridViewAssetsV_DragDrop(object sender, DragEventArgs e)
        {
            await DoDragAndDropUploadAsync(e);
        }

        private async Task DoDragAndDropUploadAsync(DragEventArgs e)
        {
            // Handle FileDrop data. 
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in  
                // case the user has selected multiple files. 
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop);

                List<string> folders = objects.Where(f => Directory.Exists(f)).ToList();
                List<string> files = objects.Where(f => !Directory.Exists(f)).ToList();

                List<Task> listTasks = new List<Task>();
                foreach (string fold in folders)
                {
                    listTasks.Add(DoMenuUploadFromFolder_Step2Async(fold)); // it's a folder
                }
                await Task.WhenAll(listTasks.ToArray());

                if (files.Count > 0)
                {
                    await DoMenuUploadFromSingleFileS_Step2Async(files.ToArray()); // let's upload the objects as files, each file as an individual asset
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


        private async void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            await DoRefreshGridStorageVAsync(false);
        }

        private async void attachAnotherStorageAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoAttachAnotherStorageAccountAsync();
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


        private async void withAzureMediaPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType.AzureMediaPlayer);
        }


        public async Task DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType playertype, List<Asset> listassets, string filter = null, string subtitletracklanguage = null)
        {
            foreach (Asset myAsset in listassets)
            {
                if (myAsset != null)
                {
                    bool Error = false;
                    PlayBackLocator = await AssetInfo.IsThereALocatorValidAsync(myAsset, _amsClient);
                    if (PlayBackLocator == null) // No streaming locator valid
                    {
                        if (MessageBox.Show(string.Format("There is no valid streaming locator for asset '{0}'.\nDo you want to create one (clear streaming) ?", myAsset.Name), "Streaming locator", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            await _amsClient.RefreshTokenIfNeededAsync();

                            TextBoxLogWriteLine("Creating locator for asset '{0}'", myAsset.Name);
                            try
                            {
                                string uniqueness = Program.GetUniqueness();

                                StreamingLocator locator = new StreamingLocator(
                                                                                assetName: myAsset.Name,
                                                                                streamingPolicyName: PredefinedStreamingPolicy.ClearStreamingOnly,
                                                                                defaultContentKeyPolicyName: null,
                                                                                streamingLocatorId: null
                                                                                );

                                locator = await _amsClient.AMSclient.StreamingLocators.CreateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, "loc" + uniqueness, locator);

                                PlayBackLocator = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myAsset.Name))
                                            .StreamingLocators
                                            .Where(l => l.Name == locator.Name).FirstOrDefault();

                                dataGridViewAssetsV.PurgeCacheAsset(myAsset);
                                await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine("Error when creating locator for asset '{0}'", myAsset.Name, true); // this could happen if asset is storage protected with no delivery policy
                                TextBoxLogWriteLine(ex);
                                Error = true;
                            }
                        }
                    }

                    PlayBackLocator = await AssetInfo.IsThereALocatorValidAsync(myAsset, _amsClient);

                    if (!Error && PlayBackLocator != null) // There is a streaming locator valid
                    {
                        string MyUri = (await _amsClient.AMSclient.StreamingLocators.ListPathsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, PlayBackLocator.Name))
                            .StreamingPaths.Where(p => p.StreamingProtocol == StreamingPolicyStreamingProtocol.SmoothStreaming)
                            .FirstOrDefault().Paths.FirstOrDefault();

                        if (MyUri != null)
                        {
                            await AssetInfo.DoPlayBackWithStreamingEndpointAsync(playertype, MyUri, _amsClient, this, myAsset, false, filter, locator: PlayBackLocator, subtitleLanguageCode: subtitletracklanguage);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The asset '{0}' does not seem to be playable with adaptive streaming.", myAsset.Name), "Adaptive streaming", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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

        private async Task DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType playertype)
        {
            string language = null;
            var le = await ReturnSelectedLiveEventsAsync();

            if (le.Count > 0)
            {
                // let's try to use preview REST to get live transcript setting
                try
                {
                    var clientRest = new AmsClientRestLiveTranscript(_amsClient);
                    var liveEventRestProp = clientRest.GetLiveEvent(le.FirstOrDefault().Name).Properties;

                    if (liveEventRestProp.Transcriptions != null && liveEventRestProp.Transcriptions.Count > 0)
                    {
                        language = liveEventRestProp.Transcriptions.FirstOrDefault()?.Language;
                    }
                }

                catch
                {

                }
            }

            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(playertype, await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync(), null, language);
        }

        private async void withAzureMediaPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType.AzureMediaPlayer);
        }


        private async void withAzureMediaPlayerToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            await DoPlaybackLiveEventPreviewAsync(PlayerType.AzureMediaPlayerClear);
        }

        private void hTML5CaptionMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.DemoCaptionMaker);
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

        private async void enableAzureCDNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ChangeAzureCDNAsync(true);
        }

        private async Task ChangeAzureCDNAsync(bool enable)
        {
            StreamingEndpoint streamingendpoint = (await ReturnSelectedStreamingEndpointsAsync()).FirstOrDefault();

            if (streamingendpoint.ResourceState != StreamingEndpointResourceState.Stopped)
            {
                MessageBox.Show(string.Format("Streaming endpoint must be stopped in order to {0} CDN.", enable ? "enable" : "disable"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!enable)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to disable CDN on Streaming Endpoint '{0}' ?", streamingendpoint.Name), "Azure CDN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    streamingendpoint.CdnEnabled = false;
                    await DoUpdateAndScaleStreamingEndpointEngineAsync(streamingendpoint);
                }
            }
            else // enable
            {
                StreamingEndpointCDNEnable form = new StreamingEndpointCDNEnable();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    streamingendpoint.CdnEnabled = true;
                    streamingendpoint.CdnProvider = form.ProviderSelectedString;
                    streamingendpoint.CdnProfile = form.Profile;
                    await DoUpdateAndScaleStreamingEndpointEngineAsync(streamingendpoint);
                }
            }
        }

        private async void disableAzureCDNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ChangeAzureCDNAsync(false);
        }


        private async void contextMenuStripStreaminEndpoints_Opening(object sender, CancelEventArgs e)
        {
            // enable Azure CDN operation if one se selected and in stopped state
            await ManageMenuOptionsAzureCDNAsync(disableAzureCDNToolStripMenuItem, enableAzureCDNToolStripMenuItem);

            // telemetry
            loadToolStripMenuItem.Enabled = enableTelemetry;
        }

        private void originToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }

        private async Task ManageMenuOptionsAzureCDNAsync(ToolStripMenuItem disableAzureCDNToolStripMenuItem1, ToolStripMenuItem enableAzureCDNToolStripMenuItem1)
        {
            // enable Azure CDN operation if one se selected and in stopped state
            List<StreamingEndpoint> streamingendpoints = await ReturnSelectedStreamingEndpointsAsync();

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

        private async void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            await DoMenuUploadFromSingleFiles_Step1Async();
        }

        private async void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            await DoMenuUploadFromFolder_Step1Async();
        }

        private async void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            await DoBatchUploadAsync();
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


        private async Task DoCopyLiveEventInputURLsToClipboardAsync()
        {
            LiveEvent liveEvent = (await ReturnSelectedLiveEventsAsync()).FirstOrDefault();

            StringBuilder sbuilder = new StringBuilder();
            sbuilder.AppendLine(string.Format("Input URLs for live event name : {0}", liveEvent.Name));
            sbuilder.AppendLine("=================================" + new string('=', liveEvent.Name.Length));

            foreach (var endpoint in liveEvent.Input.Endpoints)
            {
                sbuilder.AppendLine(string.Empty);
                sbuilder.AppendLine(endpoint.Url);
                if (liveEvent.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4)
                {
                    sbuilder.AppendLine(string.Empty);
                    sbuilder.AppendLine(endpoint.Url.Replace("http://", "https://"));
                }
            }

            EditorXMLJSON DisplayForm = new EditorXMLJSON("Input URLs", sbuilder.ToString(), false, false, false);
            DisplayForm.Display();
        }



        private async void contextMenuStripLiveEvents_Opening(object sender, CancelEventArgs e)
        {
            List<LiveEvent> liveEvents = await ReturnSelectedLiveEventsAsync();
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
        }

        private void liveLiveEventToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }

        private async void contextMenuStripPrograms_Opening(object sender, CancelEventArgs e)
        {
            List<LiveOutput> liveOutputs = await ReturnSelectedLiveOutputsAsync();
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

        private async void ContextMenuItemProgramCopyTheOutputURLToClipboard_Click(object sender, EventArgs e)
        {
            await DoDisplayOutputURLAssetOrProgramToWindowAsync();
        }

        private async void buttonSetFilterLiveEvent_Click(object sender, EventArgs e)
        {
            await DoLiveEventSearchAsync();
        }

        private async Task DoLiveEventSearchAsync()
        {
            if (dataGridViewLiveEventsV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchLiveEventOption.SelectedItem as Item).Value);
                dataGridViewLiveEventsV.SearchInName = new SearchObject { Text = textBoxSearchNameLiveEvent.Text, SearchType = stype };
                await DoRefreshGridLiveEventVAsync(false);
            }
        }

        private async void comboBoxFilterTimeLiveEvent_SelectedIndexChanged(object sender, EventArgs e)
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
                await DoRefreshGridLiveEventVAsync(false);
            }
        }

        private async void comboBoxStatusLiveEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewLiveEventsV.Initialized)
            {
                dataGridViewLiveEventsV.FilterState = ((ComboBox)sender).SelectedItem.ToString();
                await DoRefreshGridLiveEventVAsync(false);
            }
        }


        private void contextMenuStripStorage_Opening(object sender, CancelEventArgs e)
        {

        }

        private async void toolStripMenuItem12_Click_1(object sender, EventArgs e)
        {
            await DoRefreshGridFiltersVAsync(false);
        }

        private async void toolStripMenuItem16_Click_1(object sender, EventArgs e)
        {
            await DoCreateFilterAsync();
        }

        private async Task DoCreateFilterAsync()
        {
            DynManifestFilter form = new DynManifestFilter(_amsClient);

            if (form.ShowDialog() == DialogResult.OK)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;
                    TextBoxLogWriteLine("Creating acount filter '{0}'...", filterinfo.Name);
                    await _amsClient.AMSclient.AccountFilters.CreateOrUpdateAsync(
                         _amsClient.credentialsEntry.ResourceGroup,
                         _amsClient.credentialsEntry.AccountName,
                         filterinfo.Name,
                         new AccountFilter(presentationTimeRange: filterinfo.Presentationtimerange, firstQuality: filterinfo.Firstquality, tracks: filterinfo.Tracks)
                         );
                    TextBoxLogWriteLine("Account filter '{0}' created.", filterinfo.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", (filterinfo != null && filterinfo.Name != null) ? filterinfo.Name : "unknown name", true);
                    TextBoxLogWriteLine(e);
                }
                await DoRefreshGridFiltersVAsync(false);
            }
        }

        private async void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoDeleteFilterAsync();
        }

        private async Task DoDeleteFilterAsync()
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            var filters = await ReturnSelectedAccountFiltersAsync();
            Task[] deleteTasks = filters.Select(f => _amsClient.AMSclient.AccountFilters.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, f.Name)).ToArray();

            try
            {
                TextBoxLogWriteLine("Deleting {0} filter(s)...", deleteTasks.Count());
                await Task.WhenAll(deleteTasks);
                TextBoxLogWriteLine("Filter(s) deleted.");
            }

            catch (Exception e)
            {
                TextBoxLogWriteLine("Error when deleting filter(s)", true);
                TextBoxLogWriteLine(e);
            }
            await DoRefreshGridFiltersVAsync(false);
        }

        private async void filterInfoupdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoUpdateFilterAsync();
        }

        private async Task DoUpdateFilterAsync()
        {
            AccountFilter filter = (await ReturnSelectedAccountFiltersAsync()).FirstOrDefault();
            DynManifestFilter form = new DynManifestFilter(_amsClient, filter);

            if (form.ShowDialog() == DialogResult.OK)
            {
                FilterCreationInfo filterinfotoupdate = null;
                try
                {
                    filterinfotoupdate = form.GetFilterInfo;
                    TextBoxLogWriteLine("Updating account filter '{0}'...", filter.Name);

                    await _amsClient.AMSclient.AccountFilters.CreateOrUpdateAsync(
                        _amsClient.credentialsEntry.ResourceGroup,
                        _amsClient.credentialsEntry.AccountName,
                        filter.Name,
                        new AccountFilter(presentationTimeRange: filterinfotoupdate.Presentationtimerange, firstQuality: filterinfotoupdate.Firstquality, tracks: filterinfotoupdate.Tracks)
                        );
                    TextBoxLogWriteLine("Account filter '{0}' updated.", filter.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when updating account filter '{0}'.", filter.Name, true);
                    TextBoxLogWriteLine(e);
                }
                await DoRefreshGridFiltersVAsync(false);
            }
        }

        private async void dataGridViewFilters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                await DoUpdateFilterAsync();
            }
        }

        private async void contextMenuStripFilters_Opening(object sender, CancelEventArgs e)
        {
            List<AccountFilter> filters = await ReturnSelectedAccountFiltersAsync();
            bool singleitem = (filters.Count == 1);
            filterInfoupdateToolStripMenuItem.Enabled = singleitem;
        }


        private void dataGridViewTransfer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void withAzureMediaPlayerToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }


        private async Task DoCreateAssetFilterAsync()
        {
            Asset selasset = (await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync()).FirstOrDefault();

            DynManifestFilter form = new DynManifestFilter(_amsClient, null, selasset);

            if (form.ShowDialog() == DialogResult.OK)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;

                    await _amsClient.AMSclient.AssetFilters.CreateOrUpdateAsync
                           (
                           _amsClient.credentialsEntry.ResourceGroup,
                           _amsClient.credentialsEntry.AccountName,
                           selasset.Name,
                           filterinfo.Name,
                           new AssetFilter(presentationTimeRange: filterinfo.Presentationtimerange, firstQuality: filterinfo.Firstquality, tracks: filterinfo.Tracks)
                          );


                    TextBoxLogWriteLine("Asset filter '{0}' created.", filterinfo.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", (filterinfo != null && filterinfo.Name != null) ? filterinfo.Name : "unknown name", true);
                    TextBoxLogWriteLine(e);
                }
                dataGridViewAssetsV.PurgeCacheAsset(selasset);
                await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();
            }
        }


        private async Task DoDuplicateFilterAsync()
        {
            List<AccountFilter> filters = await ReturnSelectedAccountFiltersAsync();
            if (filters.Count == 1)
            {
                AccountFilter sourcefilter = filters.FirstOrDefault();

                string newfiltername = sourcefilter.Name + "Copy";
                if (Program.InputBox("New name", "Enter the name of the new duplicate filter:", ref newfiltername) == DialogResult.OK)
                {
                    await _amsClient.RefreshTokenIfNeededAsync();

                    try
                    {
                        await _amsClient.AMSclient.AccountFilters.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, newfiltername, sourcefilter);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error when duplicating asset filter." + Constants.endline + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    await DoRefreshGridFiltersVAsync(false);
                }
            }
        }

        private async void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDuplicateFilterAsync();
        }

        private async void createAnAssetFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoCreateAssetFilterAsync();
        }

        private async void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            await DoCreateAssetFilterAsync();
        }

        private void withAzureMediaPlayerToolStripMenuItem2_DropDownOpening(object sender, EventArgs e)
        {

        }


        private void dataGridViewV_Resize(object sender, EventArgs e)
        {
            Program.DataGridViewV_Resize(sender);
        }

        private void cloneLiveEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewV_VisibleChanged(object sender, EventArgs e)
        {
            Program.DataGridViewV_Resize(sender);
        }

        private async void subclipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoSubClipAsync();
        }

        private async Task DoSubClipAsync()
        {
            var selectedAssets = await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync();
            if (selectedAssets.Count > 0)
            {
                // let's get the list of asset types
                Task<AssetInfoData>[] gettypeTasks = selectedAssets.Select(a => AssetInfo.GetAssetTypeAsync(a.Name, _amsClient)).ToArray();
                await Task.WhenAll(gettypeTasks);

                if (!gettypeTasks.All(a => a.Result.Type.StartsWith(AssetInfo.Type_LiveArchive) || a.Result.Type.StartsWith(AssetInfo.Type_Fragmented)))
                //if (!selectedAssets.All(a => (await AssetInfo.GetAssetTypeAsync(a.Name, _amsClient)).Type.StartsWith(AssetInfo.Type_LiveArchive) || (AssetInfo.GetAssetType(a.Name, _amsClient)).Type.StartsWith(AssetInfo.Type_Fragmented)))
                {
                    MessageBox.Show("Asset(s) should be a live, live archive or pre-fragmented asset." + Constants.endline + "Subclipping other types of assets is unpredictable.", "Format issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Subclipping form = new Subclipping(_amsClient, selectedAssets, this);

                form.ShowDialog();
            }
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


        private async Task DoStorageVersionAsync(string storageId = null)
        {
            string valuekey;
            bool Error = false;
            ServiceProperties serviceProperties = null;
            CloudBlobClient blobClient = null;

            if (storageId == null)
            {
                storageId = (await ReturnSelectedStorageAsync()).Id;
            }

            try
            {
                valuekey = await _amsClient.GetStorageKeyAsync(storageId);
                if (valuekey == null)
                {
                    if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + AMSClientV3.GetStorageName(storageId) + ":", ref valuekey, true) != DialogResult.OK)
                    {
                        Error = true;
                    }
                }
                if (!Error)
                {
                    CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(AMSClientV3.GetStorageName(storageId), valuekey), _amsClient.environment.ReturnStorageSuffix(), true);
                    blobClient = storageAccount.CreateCloudBlobClient();

                    // Get the current service properties
                    serviceProperties = await blobClient.GetServicePropertiesAsync();
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
                StorageSettings form = new StorageSettings(AMSClientV3.GetStorageName(storageId), storageId, serviceProperties);

                if (form.ShowDialog() == DialogResult.OK)
                {

                    // Set the default service version to 2011-08-18 (or a higher version like 2012-03-01)
                    //serviceProperties.DefaultServiceVersion = "2011-08-18";
                    try
                    {
                        TextBoxLogWriteLine("Setting storage version to '{0}'...",
                            form.RequestedStorageVersion ?? StorageSettings.noversion
                            );
                        serviceProperties.DefaultServiceVersion = form.RequestedStorageVersion;

                        // Save the updated service properties
                        await blobClient.SetServicePropertiesAsync(serviceProperties);
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

        private async void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            await DoCreateAssetReportEmailAsync();
        }

        private async void copyToClipboardToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            await DoDisplayAssetReportAsync();
        }

        private void visibleAssetsInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DoDeleteAssets(dataGridViewAssetsV.assets.ToList());
        }

        private void deleteVisibleAssetsInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DoDeleteAssets(dataGridViewAssetsV.assets.ToList());
        }

        private async void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoMenuDeleteSelectedAssetsAsync();
        }

        private void deleteAllAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DoDeleteAllAssets();
        }

        private async void visibleJobsInGridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoDeleteJobsAsync(dataGridViewJobsV.ReturnSelectedJobs());
        }

        private async void allJobsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoDeleteAllJobsAsync();
        }

        private async void selectedJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDeleteJobsAsync(dataGridViewJobsV.ReturnSelectedJobs());
        }

        private async void dataGridViewStorage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string storageId = dataGridViewStorage.Rows[e.RowIndex].Cells[dataGridViewStorage.Columns["Id"].Index].Value.ToString();
                await DoStorageVersionAsync(storageId);
            }
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


        private async Task DoCheckIntegrityLiveArchiveAsync()
        {
            List<Asset> assets = await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync();

            string question = (assets.Count == 1) ? string.Format("Check the integrity of '{0}' ?", assets[0].Name) : string.Format("Check the integrity of these {0} archives ?", assets.Count);
            if (System.Windows.Forms.MessageBox.Show(question, "Integrity check", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                bool usercanceled = false;
                //var storagekeys = BuildStorageKeyDictionary(assets, null, ref usercanceled, _context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey, null);

                if (!usercanceled)
                {
                    //assets.ForEach(asset => CheckListArchiveBlobs(storagekeys, asset, AssetInfo.GetManifestSegmentsList(asset)));
                }
            }
        }

        private async void toolStripMenuItem37_Click_1(object sender, EventArgs e)
        {
            await DoMenuDownloadToLocalAsync();
        }

        private async void toolStripMenuItem38_Click(object sender, EventArgs e)
        {
            await DoMenuDownloadToLocalAsync();
        }


        private async void editAlternateIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoMenuEditAssetAltIdAsync();
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


        private async void toolStripMenuItem38_Click_2(object sender, EventArgs e)
        {
            await DoDisplayOutputURLAssetOrProgramToWindowAsync();
        }


        private async void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            await DoCheckIntegrityLiveArchiveAsync();
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
        }

        private async void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            await DoCopyAssetToAnotherAMSAccountAsync();
        }

        private async Task DoCopyAssetToAnotherAMSAccountAsync()
        {

            var selectedAssets = await ReturnSelectedAssetsV3Async();
            CopyAsset copyAssetForm = new CopyAsset(selectedAssets.Count, CopyAssetBoxMode.CopyAsset, _amsClient.credentialsEntry.AccountName);

            if (copyAssetForm.ShowDialog() == DialogResult.OK)
            {
                if (!copyAssetForm.SingleDestinationAsset) // standard mode: 1:1 asset copy
                {
                    foreach (var asset in selectedAssets)
                    {
                        string assetName = copyAssetForm.CopyAssetName.Replace(Constants.NameconvAsset, asset.Name);

                        TextBoxLogWriteLine($"Creating empty asset '{assetName}' in '{copyAssetForm.DestinationStorageAccount}' account...");

                        var response = DoGridTransferAddItem($"Copy asset '{assetName}' to account '{copyAssetForm.DestinationStorageAccount}'", TransferType.ExportToOtherAMSAccount, false);
                        // Start a worker thread that does asset copy.
                        Task.Factory.StartNew(() =>
                        ProcessExportAssetToAnotherAMSAccount(_amsClient, copyAssetForm.DestinationStorageAccount, new List<Asset>() { asset }, assetName, response, copyAssetForm.DestinationAmsClient, copyAssetForm.DeleteSourceAsset), response.token);
                    }
                }
                else // merge all assets into a single asset
                {

                    var response = DoGridTransferAddItem($"Copy several assets to account '{copyAssetForm.DestinationStorageAccount}'", TransferType.ExportToOtherAMSAccount, false);
                    // Start a worker thread that does asset copy.
                    Task.Factory.StartNew(() =>
                    ProcessExportAssetToAnotherAMSAccount(_amsClient, copyAssetForm.DestinationStorageAccount, selectedAssets, copyAssetForm.CopyAssetName.Replace(Constants.NameconvAsset, selectedAssets.FirstOrDefault().Name), response, copyAssetForm.DestinationAmsClient, copyAssetForm.DeleteSourceAsset), response.token);

                }
                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
            }
        }

        private async Task ProcessExportAssetToAnotherAMSAccount(AMSClientV3 SourceAmsClient, string DestinationStorageAccount, List<Asset> SourceAssets, string TargetAssetName, TransferEntryResponse transferResponse, AMSClientV3 DestinationAmsClient, bool DeleteSourceAssets = false)
        {

            // If upload in the queue, let's wait our turn
            await DoGridTransferWaitIfNeededAsync(transferResponse.Id);
            if (transferResponse.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(transferResponse.Id);
                return;
            }

            bool ErrorCopyAsset = false;
            bool Cancelled = false;


            // target asset creation
            var assetParameters = new Asset
            {
                StorageAccountName = DestinationStorageAccount,
                Description = SourceAssets.First().Description,
                AlternateId = SourceAssets.First().AlternateId
            };
            try
            {
                TextBoxLogWriteLine($"Creating empty asset '{TargetAssetName}' in '{DestinationStorageAccount}' account...");
                await DestinationAmsClient.AMSclient.Assets.CreateOrUpdateAsync(DestinationAmsClient.credentialsEntry.ResourceGroup, DestinationAmsClient.credentialsEntry.AccountName, TargetAssetName, assetParameters);
                TextBoxLogWriteLine($"Asset '{TargetAssetName}' created.");
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error: Could not create new asset in destination account.", true);
                TextBoxLogWriteLine(ex);
                TextBoxLogWriteLine("Trying to continue if the goal is to copy blobs to an existing asset.", true);
            }

            // destination container
            ListContainerSasInput output = new ListContainerSasInput()
            {
                Permissions = AssetContainerPermission.ReadWrite,
                ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
            };
            AssetContainerSas response = null;
            try
            {
                response = await DestinationAmsClient.AMSclient.Assets.ListContainerSasAsync(DestinationAmsClient.credentialsEntry.ResourceGroup, DestinationAmsClient.credentialsEntry.AccountName, TargetAssetName, output.Permissions, output.ExpiryTime);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine(ex);
                return;
            }

            string destSasUrl = response.AssetContainerSasUrls.First();

            Uri destSasUri = new Uri(destSasUrl);
            var destContainer = new CloudBlobContainer(destSasUri);


            foreach (Asset asset in SourceAssets) // there are several assets only if user wants to do a copy with merge
            {
                if (transferResponse.token.IsCancellationRequested) break;

                // COPY OF BLOBS

                // Listing of source blobs
                ListContainerSasInput input = new ListContainerSasInput()
                {
                    Permissions = AssetContainerPermission.Read,
                    ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                };
                await SourceAmsClient.RefreshTokenIfNeededAsync();

                try
                {
                    response = await SourceAmsClient.AMSclient.Assets.ListContainerSasAsync(SourceAmsClient.credentialsEntry.ResourceGroup, SourceAmsClient.credentialsEntry.AccountName, asset.Name, input.Permissions, input.ExpiryTime);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine(ex);
                    return;
                }

                string uploadSasUrl = response.AssetContainerSasUrls.First();

                Uri sasUri = new Uri(uploadSasUrl);
                var container = new CloudBlobContainer(sasUri);

                long Length = 0;


                // let's list all blobs (at root) and calculate the size
                BlobContinuationToken continuationToken = null;
                var sourceBlobs = new List<IListBlobItem>();
                do
                {
                    BlobResultSegment segment = await container.ListBlobsSegmentedAsync(null, false, BlobListingDetails.Metadata, null, continuationToken, null, null);
                    sourceBlobs.AddRange(segment.Results);

                    // let's calculate the size of all blobs of the page/asset
                    foreach (IListBlobItem sourceBlob in segment.Results.Where(b => b.GetType() == typeof(CloudBlockBlob)))
                    {
                        Length += (sourceBlob as CloudBlockBlob).Properties.Length;
                    }

                    continuationToken = segment.ContinuationToken;
                }
                while (continuationToken != null);


                var listDirectories = sourceBlobs.Where(blob => blob.GetType() == typeof(CloudBlobDirectory)).Select(blob => (CloudBlobDirectory)blob);
                var listBlockBlobs = sourceBlobs.Where(blob => blob.GetType() == typeof(CloudBlockBlob)).Select(blob => (CloudBlockBlob)blob);


                long BytesCopied = 0;
                double percentComplete = 0;

                foreach (var sourceCBB in listBlockBlobs)
                {

                    if (sourceCBB.Properties.Length > 0)
                    {
                        try
                        {
                            TextBoxLogWriteLine($"Copying blob '{sourceCBB.Name}'...");
                            CloudBlockBlob destinationBlob = destContainer.GetBlockBlobReference(sourceCBB.Name);
                            string stringOperation = await destinationBlob.StartCopyAsync(sourceCBB);

                            CloudBlockBlob blob = (CloudBlockBlob)await destContainer.GetBlobReferenceFromServerAsync(sourceCBB.Name);

                            while (blob.CopyState.Status == CopyStatus.Pending)
                            {
                                await Task.Delay(TimeSpan.FromSeconds(1d));
                                await blob.FetchAttributesAsync();

                                if (transferResponse.token.IsCancellationRequested && !Cancelled)
                                {
                                    await destinationBlob.AbortCopyAsync(stringOperation);
                                    Cancelled = true;
                                }
                                blob.FetchAttributes();
                                percentComplete = 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                DoGridTransferUpdateProgressText(string.Format("Blob '{0}'", sourceCBB.Name), (int)percentComplete, transferResponse.Id);
                            }
                            await destinationBlob.FetchAttributesAsync();
                            TextBoxLogWriteLine($"Blob '{sourceCBB.Name}' copied.");
                            BytesCopied += sourceCBB.Properties.Length;

                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine($"Error while copying '{sourceCBB.Name}' blob to destination.", true);
                            TextBoxLogWriteLine(ex);
                            ErrorCopyAsset = true;
                            break;
                        }
                    }
                }

                // lets copy directory and blobs if any
                int indexdir = 0;
                foreach (var blobdir in listDirectories)
                {
                    try
                    {

                        // let's enumerate all blobs (in the directory) and calculate the size
                        continuationToken = null;
                        var sourceBlobsLive = new List<IListBlobItem>();
                        do
                        {
                            BlobResultSegment segment = await blobdir.ListBlobsSegmentedAsync(false, BlobListingDetails.Metadata, null, continuationToken, null, null);
                            sourceBlobsLive.AddRange(segment.Results);
                            continuationToken = segment.ContinuationToken;
                        }
                        while (continuationToken != null);

                        var listBlockBlobsLive = sourceBlobsLive.Where(blob => blob.GetType() == typeof(CloudBlockBlob)).Select(blob => (CloudBlockBlob)blob);

                        TextBoxLogWriteLine($"Copying {listBlockBlobsLive.Count()} blobs of directory '{blobdir.Prefix}'...");

                        //let's process the blobs per packet of 50 to be quicker
                        int packet = 50;
                        int indexstartpacket = 0;
                        do
                        {
                            var listBlockBlobsLivePacket = listBlockBlobsLive.Skip(indexstartpacket).Take(packet);
                            List<CloudBlockBlob> blobsCurrentCopy = new List<CloudBlockBlob>();

                            // for each pcket of blobs, let's start the copy
                            foreach (var srcBlob in listBlockBlobsLivePacket)
                            {
                                CloudBlockBlob destinationBlob = destContainer.GetBlockBlobReference(srcBlob.Name);
                                string stringOperation = await destinationBlob.StartCopyAsync(srcBlob);

                                blobsCurrentCopy.Add((CloudBlockBlob)await destContainer.GetBlobReferenceFromServerAsync(srcBlob.Name));
                            }

                            while (blobsCurrentCopy.Any(b => b.CopyState.Status == CopyStatus.Pending))
                            {
                                await Task.Delay(TimeSpan.FromSeconds(2d));

                                // let's refresh the blobs which are in status pending only
                                var tempBlobList = blobsCurrentCopy.Where(b => b.CopyState.Status != CopyStatus.Pending).ToList();
                                foreach (var b in blobsCurrentCopy.Where(b => b.CopyState.Status == CopyStatus.Pending).ToList())
                                {
                                    await b.FetchAttributesAsync();
                                    tempBlobList.Add(b);
                                }
                                blobsCurrentCopy = tempBlobList;

                                var nbCompleted = blobsCurrentCopy.Where(b => b.CopyState.Status != CopyStatus.Pending).Count();
                                percentComplete = 100d * (indexdir + Convert.ToDouble(indexstartpacket + nbCompleted) / Convert.ToDouble(listBlockBlobsLive.Count())) / Convert.ToDouble(listDirectories.Count());
                                DoGridTransferUpdateProgressText(string.Format("fragblobs directory '{0}' ({1}/{2})", blobdir.Prefix, indexstartpacket + nbCompleted, listBlockBlobsLive.Count()), (int)percentComplete, transferResponse.Id);
                            }
                            indexstartpacket += listBlockBlobsLivePacket.Count();
                            TextBoxLogWriteLine($"{indexstartpacket} blobs copied...");

                        }
                        while (indexstartpacket < listBlockBlobsLive.Count() - 1);

                        indexdir++;
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine($"Error while copying blobs of '{blobdir.Prefix}' directory to destination.", true);
                        TextBoxLogWriteLine(ex);
                        ErrorCopyAsset = true;
                        break;
                    }
                }


                // asset deletion if requested
                if (DeleteSourceAssets)
                {
                    try
                    {
                        TextBoxLogWriteLine($"Deleting asset '{asset.Name}'...");
                        await SourceAmsClient.AMSclient.Assets.DeleteAsync(SourceAmsClient.credentialsEntry.ResourceGroup, SourceAmsClient.credentialsEntry.AccountName, asset.Name);
                        TextBoxLogWriteLine($"Asset '{asset.Name}' deleted.");
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine($"Error when deleting asset '{asset.Name}'.", true);
                        TextBoxLogWriteLine(ex);
                    }
                }
            }


            if (!ErrorCopyAsset && !transferResponse.token.IsCancellationRequested)
            {
                TextBoxLogWriteLine($"Asset '{TargetAssetName}' ready.");

                //if (DeleteSourceAssets) SourceAssets.ForEach(a => a.Delete());
                //TextBoxLogWriteLine("Asset copy completed. The new asset in '{0}' has the Id :", AMSLogin.ReturnAccountName(DestinationCredentialsEntry));
                //TextBoxLogWriteLine(TargetAsset.Id);
                DoGridTransferDeclareCompleted(transferResponse.Id, destSasUrl);
            }
            else if (transferResponse.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(transferResponse.Id);
            }

            DoRefreshGridAssetV(false);
        }



        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void CancelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCancelTransfer();
        }


        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoClearTransferts();
        }

        private void DoClearTransferts()
        {
            DoGridTransferClearCompletedTransfers();
        }

        private void ClearCompletedTransfersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoGridTransferClearCompletedTransfers();
        }


        private async void FilesToSelectedAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoMenuUploadFileToAsset_Step1Async();
        }

        private void TrackBarConcurrentTransfers_Scroll(object sender, EventArgs e)
        {
            UpdateLabelConcurrentTransfers();
        }

        private void UpdateLabelConcurrentTransfers()
        {
            labelConcurrentTransfers.Text = string.Format(Constants.strTransfers, trackBarConcurrentTransfers.Value == Constants.MaxTransfersAsUnlimited ? "Unlimited" : "Limited to " + trackBarConcurrentTransfers.Value.ToString(), trackBarConcurrentTransfers.Value > 1 ? "s" : string.Empty);
            Properties.Settings.Default.ConcurrentTransfers = trackBarConcurrentTransfers.Value;
            Program.SaveAndProtectUserConfig();
        }

        private void AnalyzeAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void ToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            DoMenuImportFromHttp();
        }


        private async void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamingEndpoint SE = (await ReturnSelectedStreamingEndpointsAsync()).FirstOrDefault();
            if (SE != null)
            {
                //var form = new DisplayTelemetry(this, SE, _context, _credentials);
                //form.Show();
            }
        }

        private async void FromAzureStoragecontainerSASUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoMenuImportFromAzureStorageSASContainerAsync();
        }

        private async void FromAzureStorageSASContainerPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoMenuImportFromAzureStorageSASContainerAsync();
        }

        private async Task DoMenuImportFromAzureStorageSASContainerAsync()
        {
            ImportHttp form = new ImportHttp(_amsClient, true);

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TransferEntryResponse response = DoGridTransferAddItem(string.Format("Import from SAS Container Path '{0}'", form.GetURL.LocalPath), TransferType.ImportFromHttp, false);
                    // Start a worker thread that does uploading.
                    // ProcessHttpSourceV3
                    //Task<Task> myTask = Task.Factory.StartNew(() => ProcessHttpSASV3Async(form.GetURL, response.Id, response.token, form.StorageSelected, form.assetCreationSetting), response.token);

                    DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
                    await ProcessHttpSASV3Async(form.GetURL, response.Id, response.token, form.StorageSelected, form.assetCreationSetting);
                    DoRefreshGridAssetV(false);

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }

            }
        }

        private void THEOPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerTHEOplayerPartnership);
        }

        private void AzureMediaServicesReleaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkMoreInfoAMSReleaseNotes);
        }

        private async void SelectedJobsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            await DoCancelJobsAsync();
        }

        private async void AllJobsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            await DoCancelAllJobsAsync();
        }

        private void LinkLabelMoreInfoMediaUnits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void TextBoxAssetSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonAssetSearch_Click(this, new EventArgs());
            }
        }

        private void TextBoxJobSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonJobSearch_Click(this, new EventArgs());
            }
        }

        private void TextBoxSearchNameLiveEvent_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetFilterLiveEvent_Click(this, new EventArgs());
            }
        }

        private void ToolStripMenuItem31_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkReportBugAMSE);
        }

        private async void DataGridViewTransformsV_SelectionChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("transform selection changed : begin");
            List<Transform> SelectedTransforms = await dataGridViewTransformsV.ReturnSelectedTransformsAsync();
            if (SelectedTransforms.Count == 1)
            {
                dataGridViewJobsV.TransformSourceNames = SelectedTransforms.Select(c => c.Name).ToList();

                await Task.Run(() =>
                {
                    Debug.WriteLine("transform selection changed : before refresh");
                    DoRefreshGridJobV(false);
                });
            }
        }

        private void DataGridViewTransformsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        /// <summary>
        /// Create a Video Analyzer transform
        /// </summary>
        /// <returns>The name of the transform</returns>
        public async Task<string> CreateVideoAnalyzerTransformAsync()
        {
            PresetVideoAnalyzer form = new PresetVideoAnalyzer();

            if (form.ShowDialog() == DialogResult.OK)
            {
                TransformOutput[] outputs;

                outputs = new TransformOutput[]
                                                 {
                                                                new TransformOutput( new VideoAnalyzerPreset( ){ AudioLanguage=form.Language, InsightsToExtract= form.InsightsMode  }),
                                                 };

                try
                {
                    await _amsClient.RefreshTokenIfNeededAsync();

                    // Create the Transform with the output defined above
                    Transform transform = await _amsClient.AMSclient.Transforms.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform '{0}' created.", transform.Name); // Warning

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", true); // Warning
                    TextBoxLogWriteLine(ex);

                }

                DoRefreshGridTransformV(false);
                return form.TransformName;
            }
            return null;
        }

        /// <summary>
        /// Creates a Face Detector transform.
        /// </summary>
        /// <returns>The name of the transform.</returns>
        public async Task<string> CreateFaceDetectorTransformAsync()
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
                    await _amsClient.RefreshTokenIfNeededAsync();

                    // Create the Transform with the output defined above
                    Transform transform = await _amsClient.AMSclient.Transforms.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform '{0}' created.", transform.Name); // Warning

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", true); // Warning
                    TextBoxLogWriteLine(ex);
                }

                DoRefreshGridTransformV(false);
                return form.TransformName;
            }
            return null;
        }

        /// <summary>
        /// Create a MES Transform
        /// </summary>
        /// <returns>The name of the transform</returns>
        public async Task<string> CreateStandardEncoderTransformAsync()
        {
            PresetStandardEncoder form = new PresetStandardEncoder();

            if (form.ShowDialog() == DialogResult.OK)
            {
                await _amsClient.RefreshTokenIfNeededAsync();

                TransformOutput[] outputs;

                if (!form.UseCustomCopyPreset)
                {
                    outputs = new TransformOutput[]
                                                     {
                                                                new TransformOutput( new BuiltInStandardEncoderPreset( ){ PresetName= form.BuiltInPreset }),
                                                     };

                }
                else
                {
                    outputs = new TransformOutput[]
                                                    {
                                                                new TransformOutput(form.CustomCopyPreset),
                                                    };

                }

                try
                {
                    // Create the Transform with the output defined above
                    Transform transform = await _amsClient.AMSclient.Transforms.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform '{0}' created.", transform.Name); // Warning

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", true); // Warning
                    TextBoxLogWriteLine(ex);
                }

                DoRefreshGridTransformV(false);
                return form.TransformName;
            }
            return null;
        }

        public async Task<Transform> CreateAndGetCopyCodecTransformIfNeededAsync()
        {
            Transform myTransform = null;
            await _amsClient.RefreshTokenIfNeededAsync();

            bool found = true;
            try
            {
                myTransform = await GetTransformAsync(PresetStandardEncoder.CopyVideoAudioTransformName);
            }
            catch
            {
                found = false;
            }

            if (!found | myTransform == null)
            {
                TransformOutput[] outputs;
                PresetStandardEncoder form = new PresetStandardEncoder();

                outputs = new TransformOutput[]
                                                {
                                                                new TransformOutput(form.CustomCopyPreset),
                                                };

                try
                {
                    // Create the Transform with the output defined above
                    myTransform = await _amsClient.AMSclient.Transforms.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, PresetStandardEncoder.CopyVideoAudioTransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform '{0}' created.", myTransform.Name); // Warning
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", true); // Warning
                    TextBoxLogWriteLine(ex);
                }

                DoRefreshGridTransformV(false);
            }

            return myTransform;
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

        public async Task CreateAndSubmitJobsAsync(List<Transform> sel, List<Asset> assets, ClipTime start = null, ClipTime end = null, string jobName = null, Asset outputAsset = null, string assetNameSyntax = null)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            foreach (Asset asset in assets)
            {
                foreach (Transform transform in sel)
                {
                    string jobNameToUse = jobName;
                    string uniqueness = Program.GetUniqueness();
                    if (jobNameToUse == null)
                    {
                        jobNameToUse = $"job-{transform.Name}-{uniqueness}";
                    }
                    else if (assets.Count > 1) // job name defined but we need to add a uniqueness as there are several assets, so several jobs to submit
                    {
                        jobNameToUse += uniqueness;
                    }
                    Asset OutputAssetNow = outputAsset;
                    string OutputAssetNameNow = OutputAssetNow?.Name;

                    List<JobOutput> jobOutputs = new List<JobOutput>();

                    if (OutputAssetNow == null)
                    {
                        foreach (var outputTrans in transform.Outputs)
                        {

                            // output asset name management
                            if (assetNameSyntax != null)
                            {
                                OutputAssetNameNow = assetNameSyntax
                                    .Replace(Constants.NameconvInputasset, asset.Name)
                                    .Replace(Constants.NameconvTransform, transform.Name)
                                    .Replace(Constants.NameconvShortUniqueness, uniqueness);

                                // example of syntax by default:  Constants.NameconvInputasset + "-" + Constants.NameconvTransform + "-" + Constants.NameconvShortUniqueness;
                            }
                            else
                            {
                                OutputAssetNameNow = $"{asset.Name}-{transform.Name}-{uniqueness}" + ((transform.Outputs.Count > 1) ? "-" + transform.Outputs.IndexOf(outputTrans) : null);

                            }
                            // if several outputs, we need to add an index
                            OutputAssetNameNow += ((transform.Outputs.Count > 1) ? "-" + transform.Outputs.IndexOf(outputTrans) : null);

                            {
                                try
                                {
                                    OutputAssetNow = await _amsClient.AMSclient.Assets.CreateOrUpdateAsync(
                                                                                _amsClient.credentialsEntry.ResourceGroup,
                                                                                _amsClient.credentialsEntry.AccountName,
                                                                                OutputAssetNameNow,
                                                                                new Asset()
                                                                                );

                                    jobOutputs.Add(new JobOutputAsset(OutputAssetNameNow));

                                }
                                catch (Exception ex)
                                {
                                    TextBoxLogWriteLine("Error when creating output asset.", true); // Warning
                                    TextBoxLogWriteLine(ex);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        transform.Outputs.ToList().ForEach(t => jobOutputs.Add(new JobOutputAsset(OutputAssetNameNow)));
                    }

                    JobInputAsset jobInput = new JobInputAsset(asset.Name, start: start, end: end);
                    try
                    {
                        Job job = await
                                                     _amsClient.AMSclient.Jobs.CreateAsync(
                                                                    _amsClient.credentialsEntry.ResourceGroup,
                                                                    _amsClient.credentialsEntry.AccountName,
                                                                    transform.Name,
                                                                    jobNameToUse,
                                                                    new Job
                                                                    {
                                                                        Input = jobInput,
                                                                        Outputs = jobOutputs,
                                                                    });


                        TextBoxLogWriteLine("Job '{0}' created.", job.Name); // Warning

                        dataGridViewJobsV.DoJobProgress(new JobExtension() { Job = job, TransformName = transform.Name });
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error when creating output asset or submitting the job.", true); // Warning
                        TextBoxLogWriteLine(ex);
                    }
                }
            }
            DoRefreshGridJobV(false);
        }


        // Job creation when source is http
        private async Task CreateAndSubmitJobsAsync(List<Transform> sel, string url, ClipTime start = null, ClipTime end = null, Asset outputAsset = null, string assetNameSyntax = null)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            foreach (Transform transform in sel)
            {
                string uniqueness = Program.GetUniqueness();
                string jobName = $"job-{transform.Name}-{uniqueness}";

                Asset OutputAssetNow = outputAsset;
                string OutputAssetNameNow = OutputAssetNow?.Name;

                List<JobOutput> jobOutputs = new List<JobOutput>();

                if (OutputAssetNow == null)
                {
                    foreach (var outputTrans in transform.Outputs)
                    {
                        // output asset name management
                        if (assetNameSyntax != null)
                        {
                            OutputAssetNameNow = assetNameSyntax
                                .Replace(Constants.NameconvTransform, transform.Name)
                                .Replace(Constants.NameconvShortUniqueness, uniqueness);

                            // example of syntax by default:  Constants.NameconvInputasset + "-" + Constants.NameconvTransform + "-" + Constants.NameconvShortGuid;
                        }
                        else
                        {
                            OutputAssetNameNow = $"httpsource-{transform.Name}-{uniqueness}" + ((transform.Outputs.Count > 1) ? "-" + transform.Outputs.IndexOf(outputTrans) : null);

                        }
                        // if several outputs, we need to add an index
                        OutputAssetNameNow += ((transform.Outputs.Count > 1) ? "-" + transform.Outputs.IndexOf(outputTrans) : null);


                        try
                        {


                            OutputAssetNow = await _amsClient.AMSclient.Assets.CreateOrUpdateAsync(
                                                                        _amsClient.credentialsEntry.ResourceGroup,
                                                                        _amsClient.credentialsEntry.AccountName,
                                                                        OutputAssetNameNow,
                                                                        new Asset()
                                                                        );

                            jobOutputs.Add(new JobOutputAsset(OutputAssetNameNow));

                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine("Error when creating output asset.", true); // Warning
                            TextBoxLogWriteLine(ex);
                            break;
                        }
                    }
                }
                else
                {
                    transform.Outputs.ToList().ForEach(t => jobOutputs.Add(new JobOutputAsset(OutputAssetNameNow)));
                }

                JobInputHttp jobInput = new JobInputHttp(files: new[] { url }, start: start, end: end);

                try
                {
                    Job job = await _amsClient.AMSclient.Jobs.CreateAsync(
                                                                _amsClient.credentialsEntry.ResourceGroup,
                                                                _amsClient.credentialsEntry.AccountName,
                                                                transform.Name,
                                                                jobName,
                                                                new Job
                                                                {
                                                                    Input = jobInput,
                                                                    Outputs = jobOutputs,
                                                                });
                    TextBoxLogWriteLine("Job '{0}' created.", job.Name); // Warning

                    dataGridViewJobsV.DoJobProgress(new JobExtension() { Job = job, TransformName = transform.Name });
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating output asset or submitting the job.", true); // Warning
                    TextBoxLogWriteLine(ex);
                }
            }

            DoRefreshGridJobV(false);
        }


        private async void deleteTransformsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoDeleteTransformsAsync(await ReturnSelectedTransformsAsync());
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


        private async void videoAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await CreateVideoAnalyzerTransformAsync();
        }

        private async void mediaEncoderStandardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await CreateStandardEncoderTransformAsync();
        }

        private async void createJobUsingAnHttpSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await CreateJobFromTransformUsingHttpSourceAsync();
        }

        private async Task CreateJobFromTransformUsingHttpSourceAsync()
        {
            List<Transform> sel = await ReturnSelectedTransformsAsync();

            //CheckAssetSizeRegardingMediaUnit(SelectedAssets);
            JobSubmitFromTransform form = new JobSubmitFromTransform(_amsClient, this, null, sel)
            {
                //ProcessingPromptText = (SelectedAssets.Count > 1) ? string.Format("{0} assets have been selected. 1 job will be submitted.", SelectedAssets.Count) : string.Format("Asset '{0}' will be encoded.", SelectedAssets.FirstOrDefault().Name),
                Text = "Template based processing"
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                await CreateAndSubmitJobsAsync(new List<Transform>() { form.SelectedTransform }, form.GetURL.OriginalString, form.StartClipTime, form.EndClipTime, form.ExistingOutputAsset);

                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs, form.SelectedTransform);
            }
        }

        private async void fromHttpsSourceWithSelectedTransformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await CreateJobFromTransformUsingHttpSourceAsync();
        }

        private async void selectATransformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoSelectTransformAndSubmitJobAsync();
        }

        private async void storageSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoStorageVersionAsync();
        }

        private async void dataGridViewAssetsV_Scroll(object sender, ScrollEventArgs e)
        {
            await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();

        }

        private async void dataGridViewAssetsV_SizeChanged(object sender, EventArgs e)
        {
            await dataGridViewAssetsV.ReLaunchAnalyzeOfAssetsAsync();

        }

        private async void createASASUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateSASUrl(await ReturnSelectedAssetsFromLiveOutputsOrAssetsAsync());

        }

        private async void faceDetectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await CreateFaceDetectorTransformAsync();
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
                    await _mediaRUContext.SetMediaRUAsync(_amsClient, trackBarEncodingRU.Value, int.Parse(((Item)comboBoxEncodingRU.SelectedItem).Value));
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

        private void ListAuthorizedOperationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListAuthorizedOperations();
        }

        private void ListAuthorizedOperations()
        {
            var sb = new StringBuilder();
            sb.AppendLine("All API operations :");
            sb.AppendLine("See https://docs.microsoft.com/en-us/azure/media-services/latest/rbac-overview for more info");
            sb.AppendLine("============================================================================================");
            TextBoxLogWriteLine("Listing operations....");

            foreach (Microsoft.Azure.Management.Media.Models.Operation a in _amsClient.AMSclient.Operations.List())
            {
                sb.AppendLine($"{a.Name} - {a.Display.Operation} - {a.Display.Description}");
            }
            TextBoxLogWriteLine("Listing operations completed.");

            var form = new EditorXMLJSON("API operations (RBAC)", sb.ToString(), false, false, false);
            form.ShowDialog();
        }

        private async void ContextMenuItemLiveEventCopyIngestURLToClipboard_Click(object sender, EventArgs e)
        {
            await DoCopyLiveEventInputURLsToClipboardAsync();
        }

        private void Mainform_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            // for controls which are not using the default font
            DpiUtils.UpdatedSizeFontAfterDPIChange(new List<Control> { labelAMSBig, menuStripMain, contextMenuStripTransfers, contextMenuStripAssets, contextMenuStripJobs, contextMenuStripLiveEvents, contextMenuStripLiveOutputs, contextMenuStripStreaminEndpoints, contextMenuStripLog, contextMenuStripTransforms, contextMenuStripStorage, contextMenuStripFilters, statusStrip1 }, e, this);

            // to scale the bitmap in the buttons
            HighDpiHelper.AdjustControlImagesAfterDpiChange(panelButtons, e);

            this.Refresh();
        }

        private void FeedbackOnAzureMediaServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkFeedbackAMS);
        }

        private async void WithAdvancedTestPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType.AdvancedTestPlayer);
        }

        private async void WithAdvancedTestPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            await DoPlaySelectedAssetsOrProgramsWithPlayerAsync(PlayerType.AdvancedTestPlayer);
        }

        private void AdvancedTestPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.AdvancedTestPlayer);
        }

        private async void NewAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoNewAssetAsync();
        }

        private async Task DoNewAssetAsync()
        {
            NewAsset myForm = new NewAsset(_amsClient) { AssetName = "asset-" + Constants.NameconvShortUniqueness };
            if (myForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TextBoxLogWriteLine("Creating asset '{0}'...", myForm.AssetName);
                    Asset assetParam = new Asset() { StorageAccountName = myForm.StorageSelected, Container = myForm.AssetContainer, AlternateId = myForm.AssetAltId, Description = myForm.AssetDescription };
                    await _amsClient.AMSclient.Assets.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, myForm.AssetName.Replace(Constants.NameconvShortUniqueness, Program.GetUniqueness()), assetParam);
                    TextBoxLogWriteLine("Asset '{0}' created.", myForm.AssetName);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating asset.", true);
                    TextBoxLogWriteLine(e);
                }
                DoRefreshGridAssetV(false);
            }
        }

        private async void NewEmptyAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoNewAssetAsync();
        }

        private async void toolStripMenuItemCKDelete_Click(object sender, EventArgs e)
        {
            await DoDeleteCKPolAsync();
        }

        private async Task DoDeleteCKPolAsync()
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            var policies = await ReturnSelectedCKPoliciessAsync();
            Task[] deleteTasks = policies.Select(ck => _amsClient.AMSclient.ContentKeyPolicies.DeleteAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, ck.Name)).ToArray();

            try
            {
                TextBoxLogWriteLine("Deleting {0} content key policies(s)...", deleteTasks.Count());
                await Task.WhenAll(deleteTasks);
                TextBoxLogWriteLine("Content key policy(s) deleted.");
            }

            catch (Exception e)
            {
                TextBoxLogWriteLine("Error when deleting content key policy(s)", true);
                TextBoxLogWriteLine(e);
            }
            await DoRefreshGridCKPoliciesVAsync(false);
        }

        private async void toolStripMenuItemCKRefresh_Click(object sender, EventArgs e)
        {
            await DoRefreshGridCKPoliciesVAsync(false);
        }

        private async void toolStripMenuItemCKCreate_Click(object sender, EventArgs e)
        {
            await DoCreateContentKeyPolicyAsync();
        }

        private Task DoCreateContentKeyPolicyAsync()
        {
            /*

            // DRM
            ContentKeyPolicy keyPolicy = null;
            List<form_DRM_Config_TokenClaims> formPlayreadyTokenClaims = new List<form_DRM_Config_TokenClaims>();
            List<form_DRM_Config_TokenClaims> formWidevineTokenClaims = new List<form_DRM_Config_TokenClaims>();
            List<form_DRM_Config_TokenClaims> formFairPlayTokenClaims = new List<form_DRM_Config_TokenClaims>();
            List<form_DRM_Config_TokenClaims> formClearKeyTokenClaims = new List<form_DRM_Config_TokenClaims>();
            List<ContentKeyPolicyOption> options = new List<ContentKeyPolicyOption>();

            // let's preserve location of windows
            int left = formLocator.Left;
            int top = formLocator.Top;

            if (formLocator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey || formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming)
            {
                string tokenSymKey = Properties.Settings.Default.DynEncTokenSymKeyv3;
                if (string.IsNullOrWhiteSpace(tokenSymKey))
                {
                    tokenSymKey = null;
                }

                if (formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming)
                {
                    DRM_CENCCBSCDelivery formCencDelivery = new DRM_CENCCBSCDelivery(
                                                            true,
                                                            true,
                                                            formLocator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming
                                                            )
                    { Left = left, Top = top };
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

                        formPlayreadyTokenClaims.Add(new form_DRM_Config_TokenClaims(step++, i + 1, "PlayReady", tokenSymKey, false)
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

                        formWidevineTokenClaims.Add(new form_DRM_Config_TokenClaims(step++, i + 1, "Widevine", tokenSymKey, false) { Left = left, Top = top });
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

                    // for each FairPlay option
                    List<DRM_FairPlayLicense> formFairPlay = new List<DRM_FairPlayLicense>();

                    for (int i = 0; i < formCencDelivery.GetNumberOfAuthorizationPolicyOptionsFairPlay; i++)
                    {
                        bool laststep = (i == formCencDelivery.GetNumberOfAuthorizationPolicyOptionsFairPlay - 1);

                        formFairPlayTokenClaims.Add(new form_DRM_Config_TokenClaims(step++, i + 1, "FairPlay", tokenSymKey, false) { Left = left, Top = top });
                        if (formFairPlayTokenClaims[i].ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        tokenSymKey = formFairPlayTokenClaims[i].textBoxSymKey.Text; // let's reuse the same key if the user wants
                        SavePositionOfForm(formFairPlayTokenClaims[i], out left, out top);

                        formFairPlay.Add(new DRM_FairPlayLicense(step++, i + 1, laststep) { Left = left, Top = top });
                        if (formFairPlay[i].ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        SavePositionOfForm(formFairPlay[i], out left, out top);

                        options.Add(
                                    new ContentKeyPolicyOption()
                                    {
                                        Configuration = new ContentKeyPolicyFairPlayConfiguration()
                                        {
                                            RentalAndLeaseKeyType = formFairPlay[i].FairPlayRentalAndLeaseKeyType,
                                            RentalDuration = formFairPlay[i].RentalDuration,
                                            Ask = formCencDelivery.FairPlayASK,
                                            FairPlayPfxPassword = formCencDelivery.FairPlayCertificate.Password,
                                            FairPlayPfx = Convert.ToBase64String(formCencDelivery.FairPlayCertificate.Certificate.Export(X509ContentType.Pfx, formCencDelivery.FairPlayCertificate.Password)),
                                            OfflineRentalConfiguration = formFairPlay[i].FairPlayOfflineRentalConfig
                                        },
                                        Restriction = formFairPlayTokenClaims[i].GetContentKeyPolicyRestriction,
                                        Name = formFairPlay[i].FairPlayePolicyName
                                    });
                    }

                }
                else if (formLocator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey)
                {
                    formClearKeyTokenClaims.Add(new form_DRM_Config_TokenClaims(1, 1, "Clear Key", tokenSymKey, true) { Left = left, Top = top });
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
                    keyPolicy = await _amsClient.AMSclient.ContentKeyPolicies.CreateOrUpdateAsync(_amsClient.credentialsEntry.ResourceGroup,
                _amsClient.credentialsEntry.AccountName,
                "keypolicy-" + Program.GetUniqueness(),
                options);
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when creating the content key policy.", true);
                    TextBoxLogWriteLine(e);
                }
            } */
            return Task.Delay(100);
        }

        private async void generateClientManifestsismcWhenNeededToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManifestGeneration.ClientManifestUtils.MyDelegate writeLog = new ManifestGeneration.ClientManifestUtils.MyDelegate(TextBoxLogWriteLine);
            await ManifestGeneration.ClientManifestUtils.DoGenerateClientManifestForAllAssetsAsync(_amsClient, writeLog);
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
