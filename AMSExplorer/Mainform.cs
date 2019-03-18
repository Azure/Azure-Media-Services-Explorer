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
using Microsoft.Rest.Azure;
using Microsoft.Rest.Azure.OData;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace AMSExplorer
{
    public partial class Mainform : Form
    {
        // XML Configuration files path.
        public static string _configurationXMLFiles;
        private static string _HelpFiles;
        public static CredentialsEntry _credentials;
        public static bool havestoragecredentials = true;

        // Field for service context.
        public static CloudMediaContext _context = null;
        public static string Salt;
        private string _backuprootfolderupload = "";
        private string _backuprootfolderdownload = "";
        private StringBuilder sbuilder = new StringBuilder(); // used for locator copy to clipboard
        private AssetStreamingLocator PlayBackLocator = null;

        //Watch folder vars
        private Dictionary<string, DateTime> seen = new Dictionary<string, DateTime>();
        WatchFolderSettings MyWatchFolderSettings = new WatchFolderSettings();

        private System.Timers.Timer TimerAutoRefresh;
        bool DisplaySplashDuringLoading;

        private enumDisplayProgram backupCheckboxAnychannel = enumDisplayProgram.Selected;
        private bool CheckboxAnychannelChangedByCode = false;

        private bool largeAccount = false; // if nb assets > trigger
        private int triggerForLargeAccountNbAssets = 10000; // account with more than 10000 assets is considered as large account. Some queries will be disabled
        private const int maxNbAssets = 1000000;
        private const int maxNbJobs = 50000;
        private bool enableTelemetry = true;

        private static readonly long OneGB = 1000L * 1000L * 1000L;
        private static readonly int S1AssetSizeLimit = 325; // GBytes
        private static readonly int S2AssetSizeLimit = 640; // GBytes
        private static readonly int S3AssetSizeLimit = 260; // GBytes
        public string _accountname;
        private static AMSClientV3 _amsClientV3;

        const string resetcredentials = "/resetcredentials";

        public Mainform(string[] args)
        {
            InitializeComponent();

            // for player control embedded in UI
            Program.SetWebBrowserFeatures();

            this.Icon = Bitmaps.Azure_Explorer_ico;

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
                Properties.Settings.Default.LoginListRPv3JSON = "";
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

            // AME Standard preset folder
            if ((Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder)))
            {
                Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEFiles;
            }

            // AME Premium Workflow preset folder
            if ((Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder)))
            {
                Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathPremiumWorkflowFiles;
            }

            // AME Standard preset folder
            if ((Properties.Settings.Default.MESPresetFilesCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.MESPresetFilesCurrentFolder)))
            {
                Properties.Settings.Default.MESPresetFilesCurrentFolder = Application.StartupPath + Constants.PathMESFiles;
            }

            // Default Slate Image
            if ((Properties.Settings.Default.DefaultSlateCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.DefaultSlateCurrentFolder)))
            {
                Properties.Settings.Default.DefaultSlateCurrentFolder = Application.StartupPath + Constants.PathDefaultSlateJPG;
            }

            Program.SaveAndProtectUserConfig(); // to save settings 

            _HelpFiles = Application.StartupPath + Constants.PathHelpFiles;

            AMSLogin formLogin = new AMSLogin();

            if (formLogin.ShowDialog() == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }

            // Get the service context.
            _context = null;// ormLogin.context;// Program.ConnectAndGetNewContext(_credentials, true);
            _amsClientV3 = formLogin.AMSClient;

            _accountname = _amsClientV3.credentialsEntry.AccountName;

            DisplaySplashDuringLoading = true;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Splash(_accountname))
                {
                    splashForm.Show();
                    while (DisplaySplashDuringLoading)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });

            // mainform title
            toolStripStatusLabelConnection.Text = String.Format("Version {0} for Media Services v3", Assembly.GetExecutingAssembly().GetName().Version) + " - Connected to " + _accountname;

            // notification title
            notifyIcon1.Text = string.Format(notifyIcon1.Text, _accountname);

            // name of the ams acount in the title of the form - useful when several instances to navigate with icons
            this.Text = string.Format(this.Text, _accountname);

            // Timer Auto Refresh
            TimerAutoRefresh = new System.Timers.Timer(Properties.Settings.Default.AutoRefreshTime * 1000);
            TimerAutoRefresh.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Let's check if there is one streaming unit running
            try
            {
                var se = _amsClientV3.AMSclient.StreamingEndpoints.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);

                if (se.AsEnumerable().Where(o => o.ResourceState == StreamingEndpointResourceState.Running).ToList().Count == 0)
                    TextBoxLogWriteLine("There is no streaming endpoint running in this account.", true); // Warning

                // Let's check if there is dynamic packaging for the channels
                double nbchannels = (double)_amsClientV3.AMSclient.LiveEvents.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).Count();
                double nbse = (double)se.Count();
                if (nbse > 0 && nbchannels > 0 && (nbchannels / nbse) > 5)
                    TextBoxLogWriteLine("There are {0} channels and {1} streaming endpoint(s). Recommandation is to provision at least 1 streaming endpoint per group of 5 channels.", nbchannels, nbse, true); // Warning

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


        private async void ProcessImportFromHttp(Uri ObjectUrl, string assetname, string fileName, Guid guidTransfer, CancellationToken token, string targetStorage, string targetStorageKey)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(guidTransfer);
            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }

            bool Error = false;
            bool Canceled = false;
            string ErrorMessage = string.Empty;

            TextBoxLogWriteLine("Starting the Http import process.");

            CloudBlockBlob blockBlob;
            IAssetFile assetFile;
            IAsset asset;
            ILocator destinationLocator = null;
            IAccessPolicy writePolicy = null;

            try
            {
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(targetStorage, targetStorageKey), _credentials.ReturnStorageSuffix(), true);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a new asset.
                asset = _context.Assets.Create(assetname, targetStorage, AssetCreationOptions.None);
                writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(2), AccessPermissions.Write);
                assetFile = asset.AssetFiles.Create(fileName);
                destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);
                Uri uploadUri = new Uri(destinationLocator.Path);
                string assetContainerName = uploadUri.Segments[1];

                CloudBlobContainer mediaBlobContainer = cloudBlobClient.GetContainerReference(assetContainerName);
                TextBoxLogWriteLine("Creating the blob container.");

                mediaBlobContainer.CreateIfNotExists();

                blockBlob = mediaBlobContainer.GetBlockBlobReference(fileName);
                TextBoxLogWriteLine("Created a reference for block blob in Azure....");

                string stringOperation = await blockBlob.StartCopyAsync(ObjectUrl, token);
                bool Cancelled = false;

                DateTime startTime = DateTime.UtcNow;

                bool continueLoop = true;

                while (continueLoop)// && !token.IsCancellationRequested)
                {
                    if (token.IsCancellationRequested && !Cancelled)
                    {
                        await blockBlob.AbortCopyAsync(stringOperation);
                        Cancelled = true;
                    }

                    blockBlob.FetchAttributes();
                    var copyStatus = blockBlob.CopyState;
                    if (copyStatus != null)
                    {
                        double percentComplete = (long)100 * (long)copyStatus.BytesCopied / (long)copyStatus.TotalBytes;

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
                DateTime endTime = DateTime.UtcNow;
                TimeSpan diffTime = endTime - startTime;

                if (!Error && !Canceled)
                {
                    TextBoxLogWriteLine("time transfer: {0}", diffTime.Duration().ToString());
                    TextBoxLogWriteLine("Creating Azure Media Services asset...");
                    blockBlob.FetchAttributes();
                    assetFile.ContentFileSize = blockBlob.Properties.Length;
                    assetFile.Update();
                    destinationLocator.Delete();
                    writePolicy.Delete();
                    // Refresh the asset.
                    asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();

                    // make the file primary
                    AssetInfo.SetFileAsPrimary(asset, assetFile.Name);

                    DoGridTransferDeclareCompleted(guidTransfer, asset.Id);
                    DoRefreshGridAssetV(false);
                }
                else if (Canceled)
                {
                    try
                    {
                        destinationLocator.Delete();
                        writePolicy.Delete();
                    }
                    catch { }

                    DoGridTransferDeclareCancelled(guidTransfer);
                    DoRefreshGridAssetV(false);
                }
                else // Error!
                {
                    DoGridTransferDeclareError(guidTransfer, "Error during import. " + ErrorMessage);
                    try
                    {
                        destinationLocator.Delete();
                        writePolicy.Delete();
                    }
                    catch { }
                }
            }

            catch (Exception ex)
            {
                Error = true;
                TextBoxLogWriteLine("Error during file import.", true);
                TextBoxLogWriteLine(ex);
                DoGridTransferDeclareError(guidTransfer, ex);

                if (destinationLocator != null)
                {
                    try
                    {
                        destinationLocator.Delete();
                    }
                    catch
                    {

                    }
                }
                if (writePolicy != null)
                {
                    try
                    {
                        writePolicy.Delete();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private async void ProcessImportFromStorageContainerSASUrl(Uri ObjectUrl, string assetname, TransferEntryResponse response, string destStorage, string destStorageKey)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(response.Id);
            if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
                return;
            }

            bool Error = false;
            bool Canceled = false;
            string ErrorMessage = string.Empty;

            TextBoxLogWriteLine("Starting the Http import process.");

            CloudBlockBlob blockBlob;
            IAssetFile assetFile;
            IAsset asset;
            ILocator destinationLocator = null;
            IAccessPolicy writePolicy = null;

            try
            {

                // Create a new blob.

                CloudBlobContainer Container = new CloudBlobContainer(ObjectUrl);
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(destStorage, destStorageKey), _credentials.ReturnStorageSuffix(), true);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a new asset.
                TextBoxLogWriteLine("Creating Azure Media Services asset...");

                asset = _context.Assets.Create(assetname, destStorage, AssetCreationOptions.None);
                writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(2), AccessPermissions.Write);
                destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);

                Uri uploadUri = new Uri(destinationLocator.Path);
                string assetContainerName = uploadUri.Segments[1];

                CloudBlobContainer mediaBlobContainer = cloudBlobClient.GetContainerReference(assetContainerName);

                TextBoxLogWriteLine("Creating the blob container.");

                mediaBlobContainer.CreateIfNotExists();

                long Length = 0;
                foreach (var blob in Container.ListBlobs())
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        var blobblock = (CloudBlockBlob)blob;
                        Length += blobblock.Properties.Length;
                    }
                }

                var blobsblock = Container.ListBlobs().Where(b => b.GetType() == typeof(CloudBlockBlob));
                int nbtotalblobblock = blobsblock.Count();
                int nbblob = 0;
                long BytesCopied = 0;
                foreach (var blob in blobsblock)
                {
                    nbblob++;
                    string fileName = Path.GetFileName(blob.Uri.ToString());
                    if (fileName != "_azuremediaservices.config")
                    {
                        assetFile = asset.AssetFiles.Create(fileName);
                    }
                    else
                    {
                        assetFile = null;
                    }

                    blockBlob = mediaBlobContainer.GetBlockBlobReference(fileName);
                    TextBoxLogWriteLine("Copying file '{0}'....", fileName);

                    var urib = new UriBuilder(ObjectUrl);
                    urib.Path = urib.Path + "/" + Path.GetFileName(blob.Uri.ToString());

                    string stringOperation = await blockBlob.StartCopyAsync(urib.Uri, response.token);
                    bool Cancelled = false;

                    DateTime startTime = DateTime.UtcNow;

                    bool continueLoop = true;

                    while (continueLoop)
                    {
                        if (response.token.IsCancellationRequested && !Cancelled)
                        {
                            await blockBlob.AbortCopyAsync(stringOperation);
                            Cancelled = true;
                        }

                        blockBlob.FetchAttributes();
                        var copyStatus = blockBlob.CopyState;
                        if (copyStatus != null)
                        {
                            double percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(nbtotalblobblock)) * 100d * (long)(BytesCopied + copyStatus.BytesCopied) / Length;

                            DoGridTransferUpdateProgress(percentComplete, response.Id);

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
                    if (assetFile != null)
                    {
                        assetFile.ContentFileSize = blockBlob.Properties.Length;
                        assetFile.Update();
                    }

                    DateTime endTime = DateTime.UtcNow;
                    TimeSpan diffTime = endTime - startTime;

                    BytesCopied += blockBlob.Properties.Length;
                }


                List<CloudBlobDirectory> ListDirectories = new List<CloudBlobDirectory>();
                List<Task> mylistresults = new List<Task>();

                var blobsdir = Container.ListBlobs().Where(b => b.GetType() == typeof(CloudBlobDirectory));
                int nbtotalblobdir = blobsdir.Count();
                int nbblobdir = 0;
                foreach (var blob in blobsdir)
                {
                    nbblobdir++;
                    string fileName = blob.Uri.Segments[2];
                    assetFile = asset.AssetFiles.Create(fileName.Substring(0, fileName.Length - 1));  // to remove / at the end

                    CloudBlobDirectory blobdir = (CloudBlobDirectory)blob;
                    ListDirectories.Add(blobdir);
                    TextBoxLogWriteLine("Fragblobs detected (live archive) '{0}'.", blobdir.Prefix);

                    var srcBlobList = blobdir.ListBlobs(
                           useFlatBlobListing: true,
                           blobListingDetails: BlobListingDetails.None).ToList();

                    var subblocks = srcBlobList.Where(s => s.GetType() == typeof(CloudBlockBlob));
                    long size = 0;
                    if (subblocks.Count() > 0) size = subblocks.Sum(s => ((CloudBlockBlob)s).Properties.Length);
                    assetFile.ContentFileSize = size;
                    assetFile.Update();
                }


                // let's launch the copy of fragblobs
                double ind = 0;
                foreach (var dir in ListDirectories)
                {
                    TextBoxLogWriteLine("Copying fragblobs directory '{0}'....", dir.Prefix);

                    mylistresults.AddRange(AssetInfo.CopyBlobDirectory(dir, mediaBlobContainer, ObjectUrl.Query, response.token));

                    if (mylistresults.Count > 0)
                    {
                        while (!mylistresults.All(r => r.IsCompleted))
                        {
                            Task.Delay(TimeSpan.FromSeconds(3d)).Wait();
                            double percentComplete = 100d * (ind + Convert.ToDouble(mylistresults.Where(c => c.IsCompleted).Count()) / Convert.ToDouble(mylistresults.Count)) / Convert.ToDouble(ListDirectories.Count);
                            DoGridTransferUpdateProgressText(string.Format("fragblobs directory '{0}' ({1}/{2})", dir.Prefix, mylistresults.Where(r => r.IsCompleted).Count(), mylistresults.Count), (int)percentComplete, response.Id);
                        }
                    }
                    ind++;
                    mylistresults.Clear();
                }

                if (!Error && !Canceled)
                {

                    destinationLocator.Delete();
                    writePolicy.Delete();
                    // Refresh the asset.
                    asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();

                    // make one of the file primary
                    AssetInfo.SetAFileAsPrimary(asset);

                    DoGridTransferDeclareCompleted(response.Id, asset.Id);
                    DoRefreshGridAssetV(false);
                }
                else if (Canceled)
                {
                    try
                    {
                        destinationLocator.Delete();
                        writePolicy.Delete();
                    }
                    catch { }

                    DoGridTransferDeclareCancelled(response.Id);
                    DoRefreshGridAssetV(false);
                }
                else // Error!
                {
                    DoGridTransferDeclareError(response.Id, "Error during import. " + ErrorMessage);
                    try
                    {
                        destinationLocator.Delete();
                        writePolicy.Delete();
                    }
                    catch { }
                }
            }

            catch (Exception ex)
            {
                Error = true;
                TextBoxLogWriteLine("Error during file import.", true);
                TextBoxLogWriteLine(ex);
                DoGridTransferDeclareError(response.Id, ex);

                if (destinationLocator != null)
                {
                    try
                    {
                        destinationLocator.Delete();
                    }
                    catch
                    {

                    }
                }
                if (writePolicy != null)
                {
                    try
                    {
                        writePolicy.Delete();
                    }
                    catch
                    {

                    }
                }
            }
        }


        private async Task ProcessUploadFromFolder(object folderPath, Guid guidTransfer, AssetCreationOptions assetcreationoption, CancellationToken token, string storageaccount = null)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(guidTransfer);
            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }

            if (storageaccount == null) storageaccount = _context.DefaultStorageAccount.Name; // no storage account or null, then let's take the default one

            var filePaths = Directory.EnumerateFiles(folderPath as string);

            TextBoxLogWriteLine("There are {0} files in {1}", filePaths.Count().ToString(), (folderPath as string));
            if (!filePaths.Any())
            {
                throw new FileNotFoundException(String.Format("No files in directory, check folderPath: {0}", folderPath));
            }
            bool Error = false;

            IAsset asset = null;

            var progress = new Dictionary<string, double>(); // used to store progress of all files
            filePaths.ToList().ForEach(f => progress[Path.GetFileName(f)] = 0d);

            try
            {
                var tasset = _context.Assets.CreateFromFolderAsync(
                                                               folderPath as string,
                                                               storageaccount,
                                                               assetcreationoption,
                                                               (af, p) =>
                                                               {
                                                                   progress[af.Name] = p.Progress;
                                                                   DoGridTransferUpdateProgress(progress.ToList().Average(l => l.Value), guidTransfer);
                                                               }, token
                                                               );
                asset = await tasset;
                //SetISMFileAsPrimary(asset); // no need as primary seems to be set by .CreateFromFolder
            }
            catch (Exception e)
            {
                Error = true;
                DoGridTransferDeclareError(guidTransfer, e);
                TextBoxLogWriteLine("Error when uploading from {0}", folderPath, true);
            }
            if (!Error)
            {
                if (!token.IsCancellationRequested)
                {
                    DoGridTransferDeclareCompleted(guidTransfer, asset.Id);
                }
                else
                {
                    DoGridTransferDeclareCancelled(guidTransfer);
                }
            }
            DoRefreshGridAssetV(false);
        }


        public static IMediaProcessor GetLatestMediaProcessorByName(string mediaProcessorName)
        {
            // The possible strings that can be passed into the 
            // method for the mediaProcessor parameter:
            //   Windows Azure Media Encoder
            //   Windows Azure Media Packager
            //   Windows Azure Media Encryptor
            //   Storage Decryption

            var processor = _context.MediaProcessors.Where(p => p.Name == mediaProcessorName).
                ToList().OrderBy(p => new Version(p.Version)).LastOrDefault();

            return processor;
        }

        public static List<IMediaProcessor> GetMediaProcessorsByName(string mediaProcessorName)
        {
            var processors = _context.MediaProcessors.Where(p => p.Name == mediaProcessorName).
                ToList().OrderBy(p => new Version(p.Version)).Reverse();

            return processors.ToList();
        }

        static Asset GetAsset(string assetName)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            return _amsClientV3.AMSclient.Assets.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, assetName);
        }


        static IJob GetJob(string jobId)
        {
            // Use a Linq select query to get an updated 
            // reference by Id. 
            IJob job;
            try
            {
                var jobInstance =
                    from j in _context.Jobs
                    where j.Id == jobId
                    select j;
                // Return the job reference as an Ijob. 
                job = jobInstance.FirstOrDefault();
            }
            catch
            {
                job = null;
            }
            return job;
        }

        static Job GetJob(string transformName, string jobName)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            return _amsClientV3.AMSclient.Jobs.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transformName, jobName);
        }

        static Transform GetTransform(string transformName)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            return _amsClientV3.AMSclient.Transforms.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transformName);
        }

        static LiveEvent GetLiveEvent(string liveEventName)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            return _amsClientV3.AMSclient.LiveEvents.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, liveEventName);
        }

        static LiveOutput GetLiveOutput(string liveEventName, string liveOutputName)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            return _amsClientV3.AMSclient.LiveOutputs.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, liveEventName, liveOutputName);
        }


        static StreamingEndpoint GetStreamingEndpoint(string seName)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            return _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, seName);
        }


        static void DeleteAsset(IAsset asset)
        {
            // delete the asset
            asset.Delete();
        }

        public void DeleteLocatorsForAsset(Asset asset)
        {
            if (asset != null)
            {
                _amsClientV3.RefreshTokenIfNeeded();
                var locators = _amsClientV3.AMSclient.Assets.ListStreamingLocators(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, asset.Name).StreamingLocators;

                foreach (var locator in locators)
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
                var eApi = (ApiErrorException)e;
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
            string date = string.Format("[{0}] ", String.Format("{0:G}", DateTime.Now));

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


        static private bool IsAWorkflow(IAsset asset)
        {
            if (asset.AssetFiles.Count() == 1)
            {
                return (asset.AssetFiles.FirstOrDefault().Name.EndsWith(".workflow", StringComparison.OrdinalIgnoreCase)
            );
            }
            else
            {
                return false;
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

            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.RefreshAssets(GetTextBoxAssetsPageNumber())));


            //tabPageAssets.Invoke(new Action(() => tabPageAssets.Text = string.Format(AMSExplorer.Properties.Resources.TabAssets + " ({0}/{1})", dataGridViewAssetsV.DisplayedCount, 10 /*_context.Assets.Count()*/)));
        }

        public void DoPurgeAssetInfoFromCache(Asset asset)
        {
            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.PurgeCacheAsset(asset)));
        }

        public void DoPurgeAssetInfoFromCache(IAsset asset)
        {
            // dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.PurgeCacheAsset(asset)));
        }

        private void DoRefreshGridTransformV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewTransformsV.Init(_amsClientV3.AMSclient, _amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);
            }

            Debug.WriteLine("DoRefreshGridTransformVNotforsttime");

            dataGridViewTransformsV.Invoke(new Action(() => dataGridViewTransformsV.RefreshTransforms()));

            //uodate tab nimber of jobs
            //    tabPageJobs.Invoke(new Action(() => tabPageJobs.Text = string.Format(AMSExplorer.Properties.Resources.TabJobs + " ({0}/{1})", dataGridViewJobsV.DisplayedCount, _context.Jobs.Count())));
        }


        private void DoRefreshGridJobV(bool firstime)
        {
            if (!dataGridViewJobsV._initialized)
                if (firstime)
                {
                    SetTextBoxJobsPageNumber(1);
                    dataGridViewJobsV.Init(_amsClientV3.AMSclient, _amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);
                }

            Debug.WriteLine("DoRefreshGridJobVNotforsttime");

            dataGridViewJobsV.Invoke(new Action(() => dataGridViewJobsV.Refreshjobs(GetTextBoxJobsPageNumber())));
        }


        private void fromASingleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuUploadFromSingleFiles_Step1();
        }

        private void DoMenuUploadFromSingleFiles_Step1()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoMenuUploadFromSingleFileS_Step2(openFileDialog.FileNames);
            }
        }

        private void DoMenuUploadFromSingleFileS_Step2(string[] FileNames)
        {
            var listpb = AssetInfo.ReturnFilenamesWithProblem(FileNames.ToList());
            if (listpb.Count > 0)
            {
                MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new UploadOptions(_amsClientV3, FileNames.Count() > 1);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            if (FileNames.Count() > 1 && form.SingleAsset) // all files in one asset
            {
                try
                {
                    var response = DoGridTransferAddItem(string.Format("Upload of {0} files into a single asset", FileNames.Count()), TransferType.UploadFromFile, true);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(FileNames.ToList(), response.Id, response.token, storageaccount: form.StorageSelected), response.token);
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
                foreach (String file in FileNames)
                {
                    try
                    {
                        i++;
                        var response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, true);
                        // Start a worker thread that does uploading.
                        Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(new List<string>() { file }, response.Id, response.token, form.StorageSelected), response.token);

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


        private async Task ProcessUploadFileAndMoreV3(List<string> filenames, Guid guidTransfer, CancellationToken token, string storageaccount = null, string destAssetName = null)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(guidTransfer);
            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }
            _amsClientV3.RefreshTokenIfNeeded();
            var storAccounts = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).StorageAccounts;

            if (storageaccount == null)
            {
                storageaccount = AMSClientV3.GetStorageName(storAccounts.Where(s => s.Type == StorageAccountType.Primary).First().Id);
                // no storage account or null, then let's take the default one
            }

            bool Error = false;
            Asset asset = null;

            var listpb = AssetInfo.ReturnFilenamesWithProblem(filenames);
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

                    var response = _amsClientV3.AMSclient.Assets.ListContainerSasAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime).Result;

                    string uploadSasUrl = response.AssetContainerSasUrls.First();

                    var sasUri = new Uri(uploadSasUrl);
                    CloudBlobContainer container = new CloudBlobContainer(sasUri);

                    foreach (var file in filenames)
                    {
                        if (token.IsCancellationRequested) return;

                        string filename = Path.GetFileName(file);

                        var blob = container.GetBlockBlobReference(filename);
                        if (filename.ToLower().EndsWith(".mp4")) blob.Properties.ContentType = "video/mp4";
                        //Console.WriteLine("Uploading File to container: {0}", sasUri);

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
            _amsClientV3.RefreshTokenIfNeeded();
            var storAccounts = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).StorageAccounts;

            if (storageaccount == null)
            {
                storageaccount = AMSClientV3.GetStorageName(storAccounts.Where(s => s.Type == StorageAccountType.Primary).First().Id);
                // no storage account or null, then let's take the default one
            }

            bool Error = false;
            Asset asset = null;

            try
            {

                if (destAssetName == null) destAssetName = "uploaded-" + Guid.NewGuid().ToString().Substring(0, 13);
                asset = await _amsClientV3.AMSclient.Assets.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, new Asset() { StorageAccountName = storageaccount, Description = destAssetDescription }, token);


                ListContainerSasInput input = new ListContainerSasInput()
                {
                    Permissions = AssetContainerPermission.ReadWrite,
                    ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
                };

                var response = _amsClientV3.AMSclient.Assets.ListContainerSasAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, destAssetName, input.Permissions, input.ExpiryTime).Result;

                string uploadSasUrl = response.AssetContainerSasUrls.First();

                var sasUri = new Uri(uploadSasUrl);
                CloudBlobContainer container = new CloudBlobContainer(sasUri);

                if (token.IsCancellationRequested) return;

                string filename = Path.GetFileName(source.LocalPath);

                var blob = container.GetBlockBlobReference(filename);
                if (filename.ToLower().EndsWith(".mp4")) blob.Properties.ContentType = "video/mp4";

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
                    var copyStatus = blob.CopyState;
                    if (copyStatus != null)
                    {
                        double percentComplete = (long)100 * (long)copyStatus.BytesCopied / (long)copyStatus.TotalBytes;

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


        private void MyUploadFileRohzetModeProgressChanged(object sender, Microsoft.WindowsAzure.MediaServices.Client.UploadProgressChangedEventArgs e, Guid guidTransfer, int indexfile, int nbfiles)
        {
            double progress = 100 * (double)indexfile / (double)nbfiles + e.Progress / (double)nbfiles;
            DoGridTransferUpdateProgress(progress, guidTransfer);
        }

        private void MyUploadFileProgressChanged(Guid guidTransfer, int indexfile, int nbfiles)
        {
            double progress = 100 * (double)indexfile / (double)nbfiles;
            DoGridTransferUpdateProgress(progress, guidTransfer);
        }

        private void DoMenuUploadFileToAsset_Step1()
        {
            var assets = ReturnSelectedAssetsV3();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoMenuUploadFileToAsset_Step2(openFileDialog.FileNames, assets);
            }
        }

        private void DoMenuUploadFileToAsset_Step2(string[] FileNames, List<Asset> assets)
        {
            var listpb = AssetInfo.ReturnFilenamesWithProblem(FileNames.ToList());
            if (listpb.Count > 0)
            {
                MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
            int i = 0;
            foreach (var asset in assets)
            {
                try
                {
                    i++;
                    var response = DoGridTransferAddItem(string.Format("Upload of {0} file{1} to asset '{2}'", FileNames.Count(), FileNames.Count() > 1 ? "s" : "", asset.Name), TransferType.UploadFromFile, true);
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

        private async Task ProcessUploadFilesToAsset(string[] FileNames, IAsset asset, Guid guidTransfer, CancellationToken token)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(guidTransfer);
            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }

            bool Error = false;

            foreach (var myfile in FileNames)
            {
                if (token.IsCancellationRequested)
                {
                    TextBoxLogWriteLine("Upload cancelled by the user.", true);
                    break;
                }
                TextBoxLogWriteLine("Starting upload of file '{0}' to asset '{1}'", myfile, asset.Name);

                try
                {
                    IAssetFile UploadedAssetFile = await asset.AssetFiles.CreateAsync(Path.GetFileName(myfile), token);

                    UploadedAssetFile.UploadProgressChanged += (af, p) =>
                     {
                         DoGridTransferUpdateProgress(p.Progress, guidTransfer);
                     };

                    UploadedAssetFile.Upload(myfile);

                }
                catch (Exception e)
                {
                    Error = true;
                    DoGridTransferDeclareError(guidTransfer, e);
                    TextBoxLogWriteLine("Error when uploading '{0}'", myfile, true);
                    TextBoxLogWriteLine(e);
                }
            }
            if (!Error && !token.IsCancellationRequested)
            {
                DoGridTransferDeclareCompleted(guidTransfer, asset.Id);
            }

            DoRefreshGridAssetV(false);
        }


        private async void ProcessDownloadAsset(List<IAsset> SelectedAssets, string folder, Guid guidTransfer, DownloadToFolderOption downloadOption, bool openFileExplorer, CancellationToken token)
        {
            // If download in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(guidTransfer);
            if (token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(guidTransfer);
                return;
            }

            bool multipleassets = SelectedAssets.Count > 1;
            bool Error = false;

            string labeldb = (multipleassets) ?
                string.Format("Starting download of files of {1} assets to {1}", SelectedAssets.Count, folder as string) :
                string.Format("Starting download of '{0}' to {1}", SelectedAssets.FirstOrDefault().Name, folder as string);

            TextBoxLogWriteLine(labeldb);
            foreach (IAsset mediaAsset in SelectedAssets)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                string foldera = folder;
                bool ErrorCurrentAssetFolderCreation = false;
                bool ErrorCurrentAsset = false;
                /*
                if (downloadOption == DownloadToFolderOption.SubfolderAssetId)
                {
                    foldera += "\\" + mediaAsset.Id.Substring(12);
                }
                else if (downloadOption == DownloadToFolderOption.SubfolderAssetName)
                {
                    foldera += "\\" + mediaAsset.Name;
                }
                */
                if (!File.Exists(foldera))
                {
                    try
                    {
                        Directory.CreateDirectory(foldera);
                    }
                    catch
                    {
                        TextBoxLogWriteLine("Error when creating folder '{0}'.", foldera, true);
                        ErrorCurrentAssetFolderCreation = true;
                    }
                }
                if (!ErrorCurrentAssetFolderCreation)
                {
                    var progress = new Dictionary<string, double>(); // used to store progress of all files
                    mediaAsset.AssetFiles.ToList().ForEach(f => progress[f.Name] = 0d);
                    try
                    {
                        await mediaAsset.DownloadToFolderAsync(foldera,
                                                                                         (af, p) =>
                                                                                         {
                                                                                             progress[af.Name] = p.Progress;
                                                                                             DoGridTransferUpdateProgress(progress.ToList().Average(l => l.Value), guidTransfer);
                                                                                         },
                                                                                         token
                                                                                        );
                    }
                    catch (Exception e)
                    {
                        ErrorCurrentAsset = true;
                        Error = true;
                        TextBoxLogWriteLine(string.Format("Download of asset '{0}' failed.", mediaAsset.Name), true);
                        TextBoxLogWriteLine(e);
                        DoGridTransferDeclareError(guidTransfer, e);
                    }


                    if (!ErrorCurrentAsset)
                    {
                        if (openFileExplorer) Process.Start(foldera);
                    }
                }

            }
            if (!Error)
            {
                if (!token.IsCancellationRequested)
                {
                    DoGridTransferDeclareCompleted(guidTransfer, folder.ToString());
                }
                else
                {
                    DoGridTransferDeclareCancelled(guidTransfer);
                }
            }
        }

        public void DoDownloadFileFromAsset(IAsset asset, IAssetFile File, object folder, TransferEntryResponse response)
        {
            // If download is in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(response.Id);
            if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
                return;
            }

            string labeldb = string.Format("Starting download of '{0}' of asset '{1}' to {2}", File.Name, asset.Name, folder as string);
            ILocator sasLocator = null;
            var locatorTask = Task.Factory.StartNew(() =>
            {
                sasLocator = _context.Locators.Create(LocatorType.Sas, asset, AccessPermissions.Read, TimeSpan.FromHours(24));
            });
            locatorTask.Wait();

            TextBoxLogWriteLine(labeldb);

            BlobTransferClient blobTransferClient = new BlobTransferClient
            {
                NumberOfConcurrentTransfers = _context.NumberOfConcurrentTransfers,
                ParallelTransferThreadCount = _context.ParallelTransferThreadCount
            };

            var myTask = Task.Factory.StartNew(async () =>
            {
                bool Error = false;
                try
                {
                    await File.DownloadAsync(Path.Combine(folder as string, File.Name), blobTransferClient, sasLocator, response.token);
                    sasLocator.Delete();
                }
                catch (Exception e)
                {
                    Error = true;
                    TextBoxLogWriteLine(string.Format("Download of file '{0}' failed !", File.Name), true);
                    TextBoxLogWriteLine(e);
                    DoGridTransferDeclareError(response.Id, e);
                }
                if (!Error)
                {
                    if (!response.token.IsCancellationRequested)
                    {
                        DoGridTransferDeclareCompleted(response.Id, folder.ToString());
                    }
                    else
                    {
                        DoGridTransferDeclareCancelled(response.Id);
                    }
                }
            }, response.token);
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
            client.RefreshTokenIfNeeded();
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

            var myTask = Task.Factory.StartNew(async () =>
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
                    if (openFileExplorer) Process.Start(outputFolderName);
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

            if (!string.IsNullOrEmpty(_backuprootfolderupload)) openFolderDialog.DefaultDirectory = _backuprootfolderupload;

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

                    var listpb = AssetInfo.ReturnFilenamesWithProblem(Directory.GetFiles(SelectedPath).ToList());
                    if (listpb.Count > 0)
                    {
                        MessageBox.Show(AssetInfo.FileNameProblemMessage(listpb), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var form = new UploadOptions(_amsClientV3, true);
                    if (form.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }

                    _backuprootfolderupload = SelectedPath;

                    var filePaths = Directory.EnumerateFiles(SelectedPath as string);

                    TextBoxLogWriteLine("There are {0} files in {1}", filePaths.Count().ToString(), (SelectedPath as string));
                    if (!filePaths.Any())
                    {
                        throw new FileNotFoundException(String.Format("No files in directory, check folderPath: {0}", SelectedPath));
                    }


                    if (form.SingleAsset)
                    {
                        var response = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(SelectedPath)), TransferType.UploadFromFolder, true);

                        var myTask = Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                              filePaths.ToList(),
                              response.Id,
                              response.token,
                              storageaccount: form.StorageSelected
                              ), response.token);

                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {

                            int i = 0;
                            foreach (var f in filePaths.ToList())
                            {
                                try
                                {
                                    i++;
                                    var response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(f) + "'", TransferType.UploadFromFile, true);
                                    // Start a worker thread that does uploading.
                                    Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                                      new List<string>() { f },
                                      response.Id,
                                      response.token,
                                      storageaccount: form.StorageSelected
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
                    var response = DoGridTransferAddItem(string.Format("Import from Http of '{0}'", Path.GetFileName(form.GetURL.LocalPath)), TransferType.ImportFromHttp, false);
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

        private void DoMenuImportFromAzureStorageSASContainer()
        {
            ImportHttp form = null;// new ImportHttp(_context, true);

            if (form.ShowDialog() == DialogResult.OK)
            {
                string DestStorage = _context.DefaultStorageAccount.Name;
                string passwordDestStorage = _credentials.DefaultStorageKey;
                if (form.StorageSelected != _context.DefaultStorageAccount.Name)
                {
                    // Not the default storage, no blob credentials, or another storage. Let's ask the user
                    string valuekey2 = "";
                    if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + form.StorageSelected + ":", ref valuekey2, true) == DialogResult.OK)
                    {
                        DestStorage = form.StorageSelected;
                        passwordDestStorage = valuekey2;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (!havestoragecredentials)
                { // No blob credentials. Let's ask the user

                    string valuekey = "";
                    if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey, true) == DialogResult.OK)
                    {
                        _credentials.DefaultStorageKey = passwordDestStorage = valuekey;
                        havestoragecredentials = true;
                    }
                    else
                    {
                        return;
                    }
                }

                var response = DoGridTransferAddItem(string.Format("Import from SAS Container Path '{0}'", "" /*form.GetAssetFileName*/), TransferType.ImportFromHttp, false);
                // Start a worker thread that does uploading.
                var myTask = Task.Factory.StartNew(() => ProcessImportFromStorageContainerSASUrl(form.GetURL, form.GetAssetName, response, DestStorage, passwordDestStorage), response.token);
                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabTransfers);
            }
        }

        private async Task DoRefreshStreamingLocators()
        {
            IList<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count > 0)
            {
                string labelAssetName = "Streaming locators will be updated for Asset '" + SelectedAssets.FirstOrDefault().Name + "'.";

                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = "Streaming locators will be updated for the " + SelectedAssets.Count.ToString() + " selected assets.";
                }

                CreateLocator form = new CreateLocator(true)
                {
                    LocatorStartDate = DateTime.Now.ToLocalTime(),
                    LocatorEndDate = DateTime.Now.ToLocalTime().AddDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew),
                    LocAssetName = labelAssetName,
                    LocatorHasStartDate = false,
                    LocWarning = string.Empty
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        foreach (var asset in SelectedAssets)
                        {
                            var tasks = asset
                                .Locators
                                .Where(locator => locator.Type == form.LocatorType)
                                .Select(locator => UpdateLocatorExpirationDate(locator, form.LocatorEndDate));

                            await Task.WhenAll(tasks);
                        }
                    }
                    finally
                    {
                        //dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets.ToList());
                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                    }
                }
            }
        }

        private async Task UpdateLocatorExpirationDate(ILocator locator, DateTime expirationTime)
        {
            try
            {
                if (locator.ExpirationDateTime >= expirationTime)
                {
                    TextBoxLogWriteLine("Skipped streaming locator {1} on asset '{0}' because it already have an expiration time greater than the provided value.",
                        locator.Asset.Name, locator.Id);
                    return;
                }
                TextBoxLogWriteLine(
                        "Update asset '{0}' streaming locator {1} expiration date from {2} to {3} ...",
                        locator.Asset.Name, locator.Id, locator.ExpirationDateTime, expirationTime
                );
                await locator.UpdateAsync(expirationTime);
                TextBoxLogWriteLine("Update asset '{0}' streaming locator {1}...Done.", locator.Asset.Name, locator.Id);
            }
            catch (Exception e)
            {
                TextBoxLogWriteLine("Failed to update asset '{0}' streaming locator {1}.", locator.Asset.Name, locator.Id, true);
                TextBoxLogWriteLine(e);
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

        public DialogResult? DisplayInfo(IAsset asset)
        {
            return null;
        }
        public DialogResult? DisplayInfo(Asset asset)
        {
            DialogResult? dialogResult = null;
            if (asset != null)
            {
                // Refresh the asset.
                //_context = Program.ConnectAndGetNewContext(_credentials);
                _amsClientV3.RefreshTokenIfNeeded();
                asset = _amsClientV3.AMSclient.Assets.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, asset.Name);
                if (asset != null)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        AssetInformation form = new AssetInformation(this, _amsClientV3)
                        {
                            myAssetV3 = asset,

                            myStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints // we want to keep the same sorting
                        };

                        dialogResult = form.ShowDialog(this);

                    }
                    finally
                    {
                        this.Cursor = Cursors.Arrow;
                        dataGridViewAssetsV.PurgeCacheAsset(asset);
                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                    }
                }
            }
            return dialogResult;
        }

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


        public DialogResult? DisplayInfo(JobExtension job)
        {
            DialogResult? dialogResult = null;
            if (job != null)
            {


                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    JobInformation form = new JobInformation(this, _amsClientV3.AMSclient)
                    {
                        MyJob = job.Job
                        //  MyStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints, // we pass this information if user open asset info from the job info dialog box
                    };
                    dialogResult = form.ShowDialog(this);
                }
                finally
                {
                    this.Cursor = Cursors.Arrow;
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
                    this.Cursor = Cursors.WaitCursor;
                    TransformInformation form = new TransformInformation(this, _amsClientV3.AMSclient)
                    {
                        MyTransform = t
                        //  MyStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints, // we pass this information if user open asset info from the job info dialog box
                    };
                    dialogResult = form.ShowDialog(this);
                }
                finally
                {
                    this.Cursor = Cursors.Arrow;
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
            var SelectedAssets = ReturnSelectedAssetsV3();
            if (SelectedAssets.Count == 0) return;
            var mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null) return;

            var form = new DownloadToLocal(SelectedAssets, _backuprootfolderdownload);

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
                    var listfiles = new List<string>(); // let's see if some files exist in the destination
                    foreach (var asset in SelectedAssets)
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
                    foreach (var asset in SelectedAssets)
                    {
                        i++;
                        string label = string.Format("Download of asset '{0}'", asset.Name);
                        var response = DoGridTransferAddItem(label, TransferType.DownloadToLocal, true);
                        // if (SelectedAssets.Count > 1) label = string.Format("Download of {0} assets", SelectedAssets.Count);
                        var myTask = Task.Factory.StartNew(() =>

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
            var SelectedJobs = ReturnSelectedJobsV3();

            if (SelectedJobs.Count > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                string question = "Cancel these " + SelectedJobs.Count + " jobs ?";
                if (SelectedJobs.Count == 1) question = "Cancel " + SelectedJobs[0].Job.Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) cancelation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (var JobToCancel in SelectedJobs)
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



        private async void DoCreateLocator(List<Asset> SelectedAssets)
        {
            string labelAssetName;
            if (SelectedAssets.Count > 0)
            {
                labelAssetName = "A locator will be created for Asset '" + SelectedAssets.FirstOrDefault().Name + "'.";

                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = "A locator will be created for the " + SelectedAssets.Count.ToString() + " selected assets.";
                }

                CreateLocator form = new CreateLocator()
                {
                    LocatorStartDate = DateTime.UtcNow.AddMinutes(-5),
                    LocatorEndDate = DateTime.UtcNow.AddDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew),
                    LocAssetName = labelAssetName,
                    LocatorHasStartDate = false,
                    LocWarning = string.Empty
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    // The duration for the locator's access policy.
                    TimeSpan accessPolicyDuration = form.LocatorEndDate.Subtract(DateTime.UtcNow);
                    if (form.LocatorStartDate != null)
                    {
                        accessPolicyDuration = form.LocatorEndDate.Subtract((DateTime)form.LocatorStartDate);
                    }

                    // DRM
                    ContentKeyPolicy keyPolicy = null;
                    if (form.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey || form.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming || form.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming)
                    {

                        var formJwt = new DRM_Config_TokenClaims(1, 1, "PlayReady", null, true);

                        if (formJwt.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        else
                        {
                            keyPolicy = await _amsClientV3.AMSclient.ContentKeyPolicies.CreateOrUpdateAsync(
                               _amsClientV3.credentialsEntry.ResourceGroup,
                                _amsClientV3.credentialsEntry.AccountName,
                                "keypolicy-" + Guid.NewGuid().ToString().Substring(0, 13),
                                new List<ContentKeyPolicyOption> { formJwt.Option });

                        }
                    }

                    sbuilder.Clear();

                    try
                    {
                        Task.Factory.StartNew(() => ProcessCreateLocatorV3(form.StreamingPolicyName, SelectedAssets, form.LocatorStartDate, form.LocatorEndDate, form.ForceLocatorGuid, keyPolicy));
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


        private void ProcessCreateLocatorV3(string streamingPolicyName, List<Asset> assets, Nullable<DateTime> startTime, Nullable<DateTime> endTime, string ForceLocatorGUID, ContentKeyPolicy keyPolicy)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (var AssetToP in assets)
            {
                StreamingLocator locator = null;
                string keyPolicyName = keyPolicy != null ? keyPolicy.Name : null;

                try
                {
                    var uniqueness = Guid.NewGuid().ToString().Substring(0, 13);
                    var streamingLocatorName = "locator-" + uniqueness;

                    locator = new StreamingLocator(
                        assetName: AssetToP.Name,
                        streamingPolicyName: streamingPolicyName,
                        streamingLocatorId: string.IsNullOrEmpty(ForceLocatorGUID) ? (Guid?)null : Guid.Parse(ForceLocatorGUID),
                        startTime: startTime,
                        endTime: endTime,
                        defaultContentKeyPolicyName: keyPolicyName
                        );


                    locator = _amsClientV3.AMSclient.StreamingLocators.Create(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, streamingLocatorName, locator);

                    TextBoxLogWriteLine("Locator created : {0}", locator.Name);
                    var streamingPaths = _amsClientV3.AMSclient.StreamingLocators.ListPaths(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, locator.Name).StreamingPaths;
                }

                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error. Could not create a locator for '{0}' ", AssetToP.Name, true);
                    TextBoxLogWriteLine(ex);
                    return;
                }
            }

            dataGridViewAssetsV.PurgeCacheAssetsV3(assets);
            dataGridViewAssetsV.AnalyzeItemsInBackground();
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
                string question = "Delete all locators of these " + SelectedAssets.Count + " assets ?";
                if (SelectedAssets.Count == 1) question = "Delete all the locators of " + SelectedAssets[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Locators deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (var AssetToProcess in SelectedAssets)
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

        private List<IAsset> ReturnSelectedAssetsFromProgramsOrAssets()
        {
            return new List<IAsset>();
        }

        private List<IAsset> ReturnSelectedAssets()
        {
            return new List<IAsset>();
        }

        private List<Asset> ReturnSelectedAssetsV3()
        {
            List<Asset> SelectedAssets = new List<Asset>();
            _amsClientV3.RefreshTokenIfNeeded();

            try
            {
                foreach (DataGridViewRow Row in dataGridViewAssetsV.SelectedRows)
                {
                    var asset = _amsClientV3.AMSclient.Assets.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, Row.Cells[dataGridViewAssetsV.Columns["Name"].Index].Value.ToString());
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



        private List<IJob> ReturnSelectedJobs()
        {
            List<IJob> SelectedJobs = new List<IJob>();
            foreach (DataGridViewRow Row in dataGridViewJobsV.SelectedRows)
                SelectedJobs.Add(_context.Jobs.Where(j => j.Id == Row.Cells["Id"].Value.ToString()).FirstOrDefault());
            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        private List<JobExtension> ReturnSelectedJobsV3()
        {
            var SelectedJobs = new List<JobExtension>();
            foreach (DataGridViewRow Row in dataGridViewJobsV.SelectedRows)
                SelectedJobs.Add(new JobExtension()
                {
                    Job = GetJob(Row.Cells["TransformName"].Value.ToString(), Row.Cells["Name"].Value.ToString()),
                    TransformName = Row.Cells["TransformName"].Value.ToString()
                });
            SelectedJobs.Reverse();
            return SelectedJobs;
        }

        private List<Transform> ReturnSelectedTransforms()
        {
            var SelectedTransforms = new List<Transform>();
            _amsClientV3.RefreshTokenIfNeeded();

            var Transforms = _amsClientV3.AMSclient.Transforms.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);

            foreach (DataGridViewRow Row in dataGridViewTransformsV.SelectedRows)
            {
                string transformName = Row.Cells[dataGridViewTransformsV.Columns["Name"].Index].Value.ToString();
                var myTransform = Transforms.Where(f => f.Name == transformName).FirstOrDefault();
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

                var row = dataGridViewStorage.SelectedRows[0];
                var index = dataGridViewStorage.Columns["Id"].Index;
                var storagename = AMSClientV3.GetStorageName(row.Cells[index].Value.ToString());
                SelectedStorage = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).StorageAccounts.Where(s => AMSClientV3.GetStorageName(s.Id) == storagename).FirstOrDefault();
            }

            return SelectedStorage;
        }

        private List<AccountFilter> ReturnSelectedAccountFilters()
        {
            List<AccountFilter> SelectedFilters = new List<AccountFilter>();
            _amsClientV3.RefreshTokenIfNeeded();

            var aFilters = _amsClientV3.AMSclient.AccountFilters.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);
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
                var form = new DeleteKeyAndPolicy(SelectedAssets.Count);

                if (form.ShowDialog() == DialogResult.OK)
                    _amsClientV3.RefreshTokenIfNeeded();

                {
                    Task.Run(async () =>
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
                        if (!Error) TextBoxLogWriteLine("Asset(s) deleted.");
                        DoRefreshGridAssetV(false);
                    }
          );

                }
            }
        }

        private void DoDeleteAssets(List<IAsset> SelectedAssets)
        {

            if (SelectedAssets.Count > 0)
            {
                var form = new DeleteKeyAndPolicy(SelectedAssets.Count);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    Task.Run(async () =>
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
                        if (!Error) TextBoxLogWriteLine("Asset(s) deleted.");
                        DoRefreshGridAssetV(false);
                    }
          );

                }
            }
        }

        private void DoDeleteAllAssets()
        {

            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete ALL the assets ?", "Asset deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string valuekey = "";
                if (Program.InputBox("Please confirm", string.Format("To confirm the operation, please type the name of the media service account ({0})", _accountname), ref valuekey, false) == DialogResult.OK)
                {
                    if (valuekey != _accountname)
                    {
                        MessageBox.Show("Strings do not match. Operation is aborted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Task.Run(async () =>
                {
                    bool Error = false;
                    int skipSize = 0;
                    int batchSize = 1000;
                    int nbAssetInAccount = _context.Assets.Count() + 1;

                    int nbassetdeleted = 0;
                    int nbassetFailedDeleted = 0;
                    bool lastround = false;

                    // let's build the list of tasks
                    TextBoxLogWriteLine("Deleting all the assets...");
                    List<IAsset> deleteTasks = new List<IAsset>();

                    try
                    {
                        while (!lastround && nbAssetInAccount != _context.Assets.Count())
                        {
                            // Enumerate through all assets (1000 at a time)
                            var listassets = _context.Assets.Skip(skipSize).Take(batchSize).ToList();
                            lastround = (listassets.Count < batchSize); // last round if less than batchSize
                            nbAssetInAccount = _context.Assets.Count();

                            var tasks = listassets.Select(a => a.DeleteAsync()).ToArray();

                            while (!tasks.All(t => t.IsCompleted))
                            {
                                TextBoxLogWriteLine("{0} assets deleted...", nbassetdeleted + tasks.Where(t => t.IsCompleted).Count());
                                Task.Delay(TimeSpan.FromSeconds(5d)).Wait();
                            }
                            nbassetdeleted += tasks.Where(t => t.Status == TaskStatus.RanToCompletion).Count();
                            nbassetFailedDeleted += tasks.Where(t => t.IsFaulted).Count();
                            TextBoxLogWriteLine("{0} assets deleted...", nbassetdeleted);
                        }
                        if (nbassetFailedDeleted > 0)
                        {
                            TextBoxLogWriteLine("{0} asset deletions faulted.", nbassetFailedDeleted, true);
                        }
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
                        TextBoxLogWriteLine("Deletion completed.");
                    }

                    DoRefreshGridAssetV(false);
                }
           );


                }
            }
        }


        private void allAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAllAssets();
        }


        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedAssetsV3().FirstOrDefault());
        }


        private void displayJobInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedJobsV3().FirstOrDefault());
        }


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

        private async void ProcessExportAssetToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, Dictionary<string, string> storagekeys, List<IAsset> SourceAssets, string TargetAssetName, TransferEntryResponse response, CloudMediaContext DestinationContext, bool DeleteSourceAssets = false, bool CopyDynEnc = false, bool ReWriteLAURL = false, bool CloneAssetFilters = false, bool CloneStreamingLocators = false, bool UnpublishSourceAsset = false, bool CopyAltId = false)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(response.Id);
            if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
                return;
            }

            bool ErrorCopyAsset = false;
            //CloudMediaContext DestinationContext;
            IAsset TargetAsset;
            CloudStorageAccount DestinationCloudStorageAccount;
            CloudBlobClient DestinationCloudBlobClient;
            IAccessPolicy writePolicy;
            ILocator DestinationLocator;
            IAssetFile[] ismAssetFile;

            try
            {
                TargetAsset = DestinationContext.Assets.Create(TargetAssetName, DestinationStorageAccount, AssetCreationOptions.None);
                if (CopyAltId)
                {
                    TargetAsset.AlternateId = SourceAssets.FirstOrDefault().AlternateId;
                    TargetAsset.Update();
                }
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error", true);
                TextBoxLogWriteLine(ex);
                DoGridTransferDeclareError(response.Id, ex);
                return;
            }

            try
            {
                // let's backup the primary file from the first asset to set it to the copied/merged asset
                ismAssetFile = SourceAssets.FirstOrDefault().AssetFiles.ToList().Where(f => f.IsPrimary).ToArray();

                TextBoxLogWriteLine("Starting the asset copy process.");

                // let's get cloudblobcontainer for target
                DestinationCloudStorageAccount =
                   (DestinationStorageAccount == null) ?
                   new CloudStorageAccount(new StorageCredentials(DestinationContext.DefaultStorageAccount.Name, storagekeys[DestinationContext.DefaultStorageAccount.Name]), DestinationCredentialsEntry.ReturnStorageSuffix(), true) :
                   new CloudStorageAccount(new StorageCredentials(DestinationStorageAccount, storagekeys[DestinationStorageAccount]), DestinationCredentialsEntry.ReturnStorageSuffix(), true);

                DestinationCloudBlobClient = DestinationCloudStorageAccount.CreateCloudBlobClient();
                writePolicy = DestinationContext.AccessPolicies.Create("writepolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
                DestinationLocator = DestinationContext.Locators.CreateLocator(LocatorType.Sas, TargetAsset, writePolicy);
            }

            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error", true);
                TextBoxLogWriteLine(ex);
                DoGridTransferDeclareError(response.Id, ex);
                TargetAsset.Delete();
                return;
            }


            // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
            Uri targetUri = new Uri(DestinationLocator.Path);
            CloudBlobContainer DestinationCloudBlobContainer = DestinationCloudBlobClient.GetContainerReference(targetUri.Segments[1]);

            foreach (IAsset SourceAsset in SourceAssets) // there are several assets only if user wants to do a copy with merge
            {
                if (response.token.IsCancellationRequested) break;

                if (storagekeys.ContainsKey(SourceAsset.StorageAccountName))
                {
                    CloudStorageAccount SourceCloudStorageAccount;
                    CloudBlobClient SourceCloudBlobClient;
                    IAccessPolicy readpolicy;
                    ILocator SourceLocator;
                    CloudBlobContainer SourceCloudBlobContainer;

                    try
                    {
                        // let's get cloudblobcontainer for source
                        SourceCloudStorageAccount = new CloudStorageAccount(new StorageCredentials(SourceAsset.StorageAccountName, storagekeys[SourceAsset.StorageAccountName]), _credentials.ReturnStorageSuffix(), true);
                        SourceCloudBlobClient = SourceCloudStorageAccount.CreateCloudBlobClient();
                        //readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                        //SourceLocator = _context.Locators.CreateLocator(LocatorType.Sas, SourceAsset, readpolicy);

                        // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                        SourceCloudBlobContainer = SourceCloudBlobClient.GetContainerReference(SourceAsset.Uri.Segments[1]);

                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error", true);
                        TextBoxLogWriteLine(ex);
                        DoGridTransferDeclareError(response.Id, ex);
                        DestinationLocator.Delete();
                        writePolicy.Delete();
                        TargetAsset.Delete();
                        return;
                    }

                    var signature = SourceCloudBlobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy
                    {
                        Permissions = SharedAccessBlobPermissions.Read,
                        SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                        SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5)
                    });


                    ErrorCopyAsset = false;
                    CloudBlockBlob sourceCloudBlockBlob, destinationCloudBlockBlob;
                    long Length = 0;
                    long BytesCopied = 0;
                    double percentComplete = 0;

                    //calculate size
                    foreach (IAssetFile file in SourceAsset.AssetFiles)
                    {
                        Length += file.ContentFileSize;
                    }
                    if (Length == 0) Length = 1; // as this could happen with Live archive and create a divide error

                    // do the copy
                    int nbblob = 0;

                    // For Live archive, the folder for chunks are returned as files. So we detect this case and don't try to copy the folders as asset files
                    List<IAssetFile> assetFilesToCopy = SourceAsset.AssetFiles.ToList();
                    if (
                        assetFilesToCopy.Where(af => af.Name.Contains(".")).Count() == 2
                        && assetFilesToCopy.Where(af => af.Name.ToUpper().EndsWith(".ISMC")).Count() == 1
                        && assetFilesToCopy.Where(af => af.Name.ToUpper().EndsWith(".ISM")).Count() == 1
                        ) // only 2 files with extensions, and these files are ISMC and ISM
                    {

                        // let read the storage to make sure it's not a directory
                        var mediablobsFolders = SourceCloudBlobContainer.ListBlobs().ToList().Where(b => b.GetType() == typeof(CloudBlobDirectory)).Select(a => (a as CloudBlobDirectory).Prefix);

                        assetFilesToCopy = SourceAsset.AssetFiles.ToList().Where(af => af.AssetFileOptions == AssetFileOptions.None && !mediablobsFolders.Contains(af.Name + "/")).ToList();

                        var assetFilesLiveFolders = SourceAsset.AssetFiles.ToList().Where(af => af.AssetFileOptions == AssetFileOptions.Fragmented || mediablobsFolders.Contains(af.Name + "/")).ToList();

                        foreach (IAssetFile sourcefolder in assetFilesLiveFolders)
                        {
                            var folder = TargetAsset.AssetFiles.Create(sourcefolder.Name);
                            folder.ContentFileSize = sourcefolder.ContentFileSize;
                            folder.MimeType = sourcefolder.MimeType;
                            folder.Update();
                        }
                    }
                    foreach (IAssetFile file in assetFilesToCopy)
                    {
                        if (response.token.IsCancellationRequested) break;

                        if (file.IsEncrypted)
                        {
                            TextBoxLogWriteLine("   Cannot copy file '{0}' because it is encrypted.", file.Name, true);
                        }
                        else
                        {
                            bool ErrorCopyAssetFile = false;
                            nbblob++;

                            try
                            {

                                sourceCloudBlockBlob = SourceCloudBlobContainer.GetBlockBlobReference(file.Name);
                                // TO DO: chek if this is a folder or a file
                                sourceCloudBlockBlob.FetchAttributes();

                                if (sourceCloudBlockBlob.Properties.Length > 0)
                                {
                                    if (!TargetAsset.AssetFiles.ToList().Any(f => f.Name == file.Name))  // file does not exist in the target asset
                                    {
                                        IAssetFile destinationAssetFile = TargetAsset.AssetFiles.Create(file.Name);
                                        destinationCloudBlockBlob = DestinationCloudBlobContainer.GetBlockBlobReference(destinationAssetFile.Name);

                                        try
                                        {
                                            destinationCloudBlockBlob.DeleteIfExists();
                                        }
                                        catch
                                        {
                                            // exception if Blob does not exist, which is fine
                                        }

                                        string stringOperation = await destinationCloudBlockBlob.StartCopyAsync(new Uri(sourceCloudBlockBlob.Uri.AbsoluteUri + signature));

                                        bool Cancelled = false;

                                        CloudBlockBlob blob;
                                        blob = (CloudBlockBlob)DestinationCloudBlobContainer.GetBlobReferenceFromServer(file.Name);

                                        while (blob.CopyState.Status == CopyStatus.Pending)
                                        {
                                            Task.Delay(TimeSpan.FromSeconds(0.5d)).Wait();

                                            if (response.token.IsCancellationRequested && !Cancelled)
                                            {
                                                await destinationCloudBlockBlob.AbortCopyAsync(stringOperation);
                                                Cancelled = true;
                                            }
                                            blob.FetchAttributes();
                                            percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SourceAsset.AssetFiles.Count())) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                            DoGridTransferUpdateProgressText(string.Format("File '{0}'", file.Name), (int)percentComplete, response.Id);
                                        }

                                        if (blob.CopyState.Status == CopyStatus.Failed)
                                        {
                                            DoGridTransferDeclareError(response.Id, blob.CopyState.StatusDescription);
                                            ErrorCopyAssetFile = true;
                                            ErrorCopyAsset = true;
                                            break;
                                        }

                                        if (blob.CopyState.Status == CopyStatus.Aborted)
                                        {
                                            DoGridTransferDeclareCancelled(response.Id);
                                            ErrorCopyAssetFile = true;
                                            ErrorCopyAsset = true;
                                            break;
                                        }

                                        destinationCloudBlockBlob.FetchAttributes();
                                        destinationAssetFile.ContentFileSize = sourceCloudBlockBlob.Properties.Length;
                                        destinationAssetFile.Update();

                                        if (sourceCloudBlockBlob.Properties.Length != destinationCloudBlockBlob.Properties.Length)
                                        {
                                            DoGridTransferDeclareError(response.Id, "Error during blob copy.");
                                            ErrorCopyAssetFile = true;
                                            ErrorCopyAsset = true;
                                            break;
                                        }

                                        BytesCopied += sourceCloudBlockBlob.Properties.Length;
                                        percentComplete = (long)100 * (long)BytesCopied / (long)Length;
                                    }
                                    else // file already exists.Can occur with merge function
                                    {
                                        TextBoxLogWriteLine("Failed to copy file '{0} as file already exists in the destination asset.", file.Name, true);
                                        ErrorCopyAssetFile = true;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                DoGridTransferDeclareError(response.Id, ex);
                                ErrorCopyAsset = true;
                                ErrorCopyAssetFile = true;
                            }
                            if (!ErrorCopyAssetFile && !response.token.IsCancellationRequested) TextBoxLogWriteLine("File '{0}' copied.", file.Name);
                        }

                    }

                    if (!ErrorCopyAsset && !response.token.IsCancellationRequested) // let's do the copy of additional fragblob if there are
                    {
                        List<CloudBlobDirectory> ListDirectories = new List<CloudBlobDirectory>();
                        // do the copy
                        nbblob = 0;
                        DoGridTransferUpdateProgressText(string.Format("fragblobs", SourceAsset.Name, DestinationCredentialsEntry.ReturnAccountName()), 0, response.Id);
                        try
                        {
                            var mediablobs = SourceCloudBlobContainer.ListBlobs();
                            if (mediablobs.ToList().Any(b => b.GetType() == typeof(CloudBlobDirectory))) // there are fragblobs
                            {
                                List<Task> mylistresults = new List<Task>();

                                foreach (var blob in mediablobs)
                                {
                                    if (blob.GetType() == typeof(CloudBlobDirectory))
                                    {
                                        CloudBlobDirectory blobdir = (CloudBlobDirectory)blob;
                                        ListDirectories.Add(blobdir);
                                        TextBoxLogWriteLine("Fragblobs detected (live archive) '{0}'.", blobdir.Prefix);
                                    }
                                    else if (blob.GetType() == typeof(CloudBlockBlob))
                                    {
                                        // we must copy the file.ismc too
                                        var blockblob = (CloudBlockBlob)blob;
                                        if (blockblob.Name.EndsWith(".ismc") && !SourceAsset.AssetFiles.ToList().Any(f => f.Name == blockblob.Name)) // if there is a .ismc in the blov and not in the asset files, then we need to copy it
                                        {
                                            CloudBlockBlob targetBlob = DestinationCloudBlobContainer.GetBlockBlobReference(blockblob.Name);
                                            // copy using src blob as SAS
                                            //mylistresults.Add(targetBlob.StartCopyAsync(new Uri(blockblob.Uri.AbsoluteUri + SourceLocator.ContentAccessComponent), response.token));
                                            mylistresults.Add(targetBlob.StartCopyAsync(new Uri(blockblob.Uri.AbsoluteUri + signature), response.token));

                                            //string stringOperation = await destinationCloudBlockBlob.StartCopyAsync(new Uri(sourceCloudBlockBlob.Uri.AbsoluteUri + signature));



                                        }
                                    }
                                }
                                // let's launch the copy of fragblobs
                                double ind = 0;
                                foreach (var dir in ListDirectories)
                                {
                                    TextBoxLogWriteLine("Copying fragblobs directory '{0}'....", dir.Prefix);

                                    mylistresults.AddRange(AssetInfo.CopyBlobDirectory(dir, DestinationCloudBlobContainer, signature /*SourceLocator.ContentAccessComponent*/, response.token));

                                    if (mylistresults.Count > 0)
                                    {
                                        while (!mylistresults.All(r => r.IsCompleted))
                                        {
                                            Task.Delay(TimeSpan.FromSeconds(3d)).Wait();
                                            percentComplete = 100d * (ind + Convert.ToDouble(mylistresults.Where(c => c.IsCompleted).Count()) / Convert.ToDouble(mylistresults.Count)) / Convert.ToDouble(ListDirectories.Count);
                                            DoGridTransferUpdateProgressText(string.Format("fragblobs directory '{0}' ({1}/{2})", dir.Prefix, mylistresults.Where(r => r.IsCompleted).Count(), mylistresults.Count), (int)percentComplete, response.Id);
                                        }
                                    }
                                    ind++;
                                    mylistresults.Clear();
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine("Failed to copy live fragblobs", true);
                            TextBoxLogWriteLine(ex);
                            DoGridTransferDeclareError(response.Id, ex);
                            ErrorCopyAsset = true;
                        }
                    }

                    //SourceLocator.Delete();
                    //readpolicy.Delete();
                }
                else
                {
                    TextBoxLogWriteLine("Error storage key not found for asset '{0}'.", SourceAsset.Name, true);
                    ErrorCopyAsset = true;
                }
            }

            // let's set the primary file
            if (ismAssetFile.Count() > 0)
            {
                AssetInfo.SetFileAsPrimary(TargetAsset, ismAssetFile.FirstOrDefault().Name);
            }
            else
            {
                AssetInfo.SetISMFileAsPrimary(TargetAsset);
            }


            // Copy Dynamic Encryption
            if (CopyDynEnc && !ErrorCopyAsset && !response.token.IsCancellationRequested)
            {
                TextBoxLogWriteLine("Dynamic encryption settings copy...");
                try
                {
                    await DynamicEncryption.CopyDynamicEncryption(SourceAssets.FirstOrDefault(), TargetAsset, ReWriteLAURL, _accountname, DestinationCredentialsEntry.ReturnAccountName());
                    TextBoxLogWriteLine("Dynamic encryption settings copied.");

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when copying Dynamic encryption", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            // Copy filters
            if (CloneAssetFilters && !ErrorCopyAsset && SourceAssets.FirstOrDefault().AssetFilters.Count() > 0 && !response.token.IsCancellationRequested)
            {
                try
                {
                    TextBoxLogWriteLine("Copying filter(s) to cloned asset '{0}'", SourceAssets.FirstOrDefault().Name);

                    foreach (var filter in SourceAssets.FirstOrDefault().AssetFilters)
                    {
                        TargetAsset.AssetFilters.Create(filter.Name, filter.PresentationTimeRange, filter.Tracks);
                        TextBoxLogWriteLine(string.Format("Cloned filter {0} created.", filter.Name));
                    }
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when copying filter(s) to the asset '{0}'.", TargetAsset.Name, true);
                    TextBoxLogWriteLine(ex);
                }
            }

            // Clone streaming locators
            if (CloneStreamingLocators && !ErrorCopyAsset && SourceAssets.FirstOrDefault().Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).Count() > 0 && !response.token.IsCancellationRequested)
            {
                try
                {
                    TextBoxLogWriteLine("Cloning streaming locator(s) to cloned asset '{0}'", SourceAssets.FirstOrDefault().Name);

                    var sourceLocators = SourceAssets.FirstOrDefault().Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).Select(l => new { l.Id, l.Name, l.StartTime, l.ExpirationDateTime }).ToList();

                    if (UnpublishSourceAsset)
                    {
                        sourceLocators.ForEach(sl => _context.Locators.Where(l => l.Id == sl.Id).FirstOrDefault().Delete());
                        TextBoxLogWriteLine(string.Format("Source locator(s) for asset {0} deleted.", SourceAssets.FirstOrDefault().Name));
                        Thread.Sleep(1000); // to make sure tables are updated before new locators are created
                    }

                    foreach (var streamLocator in sourceLocators)
                    {
                        IAccessPolicy policy = DestinationContext.AccessPolicies.Create("AP:" + SourceAssets.FirstOrDefault().Name, (streamLocator.ExpirationDateTime - DateTime.UtcNow), AccessPermissions.Read);
                        var newLoc = DestinationContext.Locators.CreateLocator(streamLocator.Id, LocatorType.OnDemandOrigin, TargetAsset, policy, streamLocator.StartTime, streamLocator.Name);
                        TextBoxLogWriteLine(string.Format("Cloned locator {0} created.", newLoc.Id));
                    }
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when cloning locator(s) to the asset '{0}'.", TargetAsset.Name, true);
                    TextBoxLogWriteLine(ex);
                }
            }

            // end of copy
            DestinationLocator.Delete();
            writePolicy.Delete();

            if (!ErrorCopyAsset && !response.token.IsCancellationRequested)
            {
                if (DeleteSourceAssets) SourceAssets.ForEach(a => a.Delete());
                TextBoxLogWriteLine("Asset copy completed. The new asset in '{0}' has the Id :", DestinationCredentialsEntry.ReturnAccountName());
                TextBoxLogWriteLine(TargetAsset.Id);
                DoGridTransferDeclareCompleted(response.Id, DestinationCloudBlobContainer.Uri.AbsoluteUri);
            }
            else if (response.token.IsCancellationRequested)
            {
                DoGridTransferDeclareCancelled(response.Id);
            }

            DoRefreshGridAssetV(false);
        }


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

                    //var assetFilesLiveFolders = SourceAsset.AssetFiles.ToList().Where(af => af.Name.StartsWith("audio_") || af.Name.StartsWith("video_") || af.Name.StartsWith("scte35_"));

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
                                    /*
                                    if (dir.Prefix.Contains("__"))  // let's get the index of audio track if it exists in directory name
                                    {
                                        var split = dir.Prefix.Split('_');
                                        manifestdatacurrenttrack = manifestdata.audioSegments[int.Parse(split[2])].ToList();
                                    }
                                    else
                                    {
                                        manifestdatacurrenttrack = manifestdata.audioSegments[0].ToList();
                                    }
                                    */
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



        private async void ProcessCloneProgramToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, LiveOutput sourceProgram, bool CopyDynEnc, bool RewriteLAURL, bool CloneLocators, bool CloneAssetFilters, bool copyAltId)
        {
            return;
            /*
            TextBoxLogWriteLine("Starting the program cloning process.");

            if (DestinationCredentialsEntry.UseAADServicePrincipal)  // service principal mode
            {
                var spcrendentialsform = new AMSLoginServicePrincipal();
                if (spcrendentialsform.ShowDialog() == DialogResult.OK)
                {
                    DestinationCredentialsEntry.ADSPClientId = spcrendentialsform.ClientId;
                    DestinationCredentialsEntry.ADSPClientSecret = spcrendentialsform.ClientSecret;
                }
                else
                {
                    return;
                }
            }
            CloudMediaContext DestinationContext = Program.ConnectAndGetNewContext(DestinationCredentialsEntry);

            // let's check that target channel exists
            LiveEvent clonedchannel = DestinationContext.Channels.Where(c => c.Name == sourceProgram.Channel.Name).FirstOrDefault();
            if (clonedchannel == null)
            {
                TextBoxLogWriteLine(string.Format("Cloned channel '{0}' not found in destination account!", sourceProgram.Channel.Name), true);
                return;
            }

            // let's check that a program with same name does not already exist for the channel
            if (DestinationContext.Programs.Where(p => p.Name == sourceProgram.Name && p.ChannelId == clonedchannel.Id).FirstOrDefault() != null)
            {
                TextBoxLogWriteLine(string.Format("A program '{0}' has been already found in destination account for channel '{1}'. A new one cannot be created.", sourceProgram.Name, clonedchannel.Name), true);
                return;
            }

            // Cloned asset creation
            Asset clonedAsset = DestinationContext.Assets.Create(sourceProgram.Asset.Name, DestinationStorageAccount, AssetCreationOptions.None);
            TextBoxLogWriteLine(string.Format("Cloned asset {0} created.", sourceProgram.Asset.Name));

            if (copyAltId)
            {
                clonedAsset.AlternateId = _amsClientV3.AMSclient.Assets.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, sourceProgram.AssetName).AlternateId;
                clonedAsset.Update();
            }

            if (CopyDynEnc)
            {
                TextBoxLogWriteLine("Dynamic encryption settings copy...");
                try
                {
                    await DynamicEncryption.CopyDynamicEncryption(sourceProgram.Asset, clonedAsset, RewriteLAURL, _accountname, DestinationCredentialsEntry.ReturnAccountName());
                    TextBoxLogWriteLine("Dynamic encryption settings copied.");
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when copying Dynamic encryption", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            if (CloneLocators)
            {
                try
                {
                    TextBoxLogWriteLine("Creating locator for cloned asset '{0}'", sourceProgram.Asset.Name);

                    foreach (var streamlocator in sourceProgram.Asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin))
                    {
                        IAccessPolicy policy = DestinationContext.AccessPolicies.Create("AP:" + sourceProgram.Asset.Name, (streamlocator.ExpirationDateTime - DateTime.UtcNow), AccessPermissions.Read);
                        DestinationContext.Locators.CreateLocator(streamlocator.Id, LocatorType.OnDemandOrigin, clonedAsset, policy, streamlocator.StartTime, streamlocator.Name);
                        TextBoxLogWriteLine(string.Format("Cloned locator {0} created.", streamlocator.Id));
                    }
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when creating the locator for the asset '{0}'.", clonedAsset.Name, true);
                    TextBoxLogWriteLine(ex);
                }
            }

            if (CloneAssetFilters && sourceProgram.Asset.AssetFilters.Count() > 0)
            {
                try
                {
                    TextBoxLogWriteLine("Copying filter(s) to cloned asset '{0}'", sourceProgram.Asset.Name);

                    foreach (var filter in sourceProgram.Asset.AssetFilters)
                    {
                        // let's remove start and end time
                        var PTR = new PresentationTimeRange(filter.PresentationTimeRange.Timescale, null, null, filter.PresentationTimeRange.PresentationWindowDuration, filter.PresentationTimeRange.LiveBackoffDuration);
                        clonedAsset.AssetFilters.Create(filter.Name, PTR, filter.Tracks);
                        TextBoxLogWriteLine(string.Format("Cloned filter {0} created.", filter.Name));
                    }
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when copying filter(s) to the asset '{0}'.", clonedAsset.Name, true);
                    TextBoxLogWriteLine(ex);
                }
            }

            var options = new ProgramCreationOptions()
            {
                Name = sourceProgram.Name,
                Description = sourceProgram.Description,
                ArchiveWindowLength = sourceProgram.ArchiveWindowLength,
                AssetId = clonedAsset.Id,
                ManifestName = sourceProgram.ManifestName
            };

            var STask = LiveEventCreateAsync(_amsClientV3.AMSclient, _amsClientV3.credentialsEntry,  eOutputs.CreateAsync Create CreateLiveOutput LiveOutputCreate ProgramExecuteAsync(
              () =>
                  clonedchannel.Programs.CreateAsync(options),
                 sourceProgram.Name,
                 "created");
            await STask;

            TextBoxLogWriteLine(string.Format("Cloned program {0} created.", sourceProgram.Name));
            */
        }


        private async void ProcessCloneChannelToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, IChannel sourceChannel)
        {
            TextBoxLogWriteLine("Starting the channel cloning process...");

            if (DestinationCredentialsEntry.UseAADServicePrincipal)  // service principal mode
            {
                var spcrendentialsform = new AMSLoginServicePrincipal();
                if (spcrendentialsform.ShowDialog() == DialogResult.OK)
                {
                    DestinationCredentialsEntry.ADSPClientId = spcrendentialsform.ClientId;
                    DestinationCredentialsEntry.ADSPClientSecret = spcrendentialsform.ClientSecret;
                }
                else
                {
                    return;
                }
            }
            CloudMediaContext DestinationContext = Program.ConnectAndGetNewContext(DestinationCredentialsEntry);

            var options = new ChannelCreationOptions()
            {
                Name = sourceChannel.Name,
                Description = sourceChannel.Description,
                EncodingType = sourceChannel.EncodingType,
                Input = sourceChannel.Input,
                Output = sourceChannel.Output,
                Preview = sourceChannel.Preview
            };

            if (sourceChannel.EncodingType != ChannelEncodingType.None)
            {
                options.Encoding = sourceChannel.Encoding;
                options.Slate = sourceChannel.Slate;
            }

            await Task.Run(() => IObjectExecuteOperationAsync(
                 () =>
                     DestinationContext.Channels.SendCreateOperationAsync(
                     options),
                     sourceChannel.Name,
                     "Cloned channel",
                     "created",
                     DestinationContext));

        }

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

                    Task.Run(async () =>
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
                        if (!Error) TextBoxLogWriteLine("Job(s) deleted.");
                        DoRefreshGridJobV(false);
                    }
           );
                }
            }
        }


        private void DoDeleteAllJobs()
        {
            if (dataGridViewTransformsV.ReturnSelectedTransforms().Count > 1) return;

            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete ALL the jobs from the selected transform?", "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                Task.Run(async () =>
                {
                    bool Error = false;


                    // let's build the tasks list
                    TextBoxLogWriteLine("Listing the jobs...");
                    List<Task> deleteTasks = new List<Task>();

                    //   foreach (var transform in dataGridViewTransformsV.ReturnSelectedTransforms())
                    {
                        var transform = dataGridViewTransformsV.ReturnSelectedTransforms().First();
                        var listjobs = _amsClientV3.AMSclient.Jobs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transform.Name);
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

                    if (!Error) TextBoxLogWriteLine("Job(s) deleted.");
                    DoRefreshGridJobV(false);
                }
          );

            }
        }



        private void DoCancelAllJobs()
        {
            if (dataGridViewTransformsV.ReturnSelectedTransforms().Count > 1) return;

            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to cancel ALL the jobs from the selected transform ?", "Job cancelation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                Task.Run(async () =>
                {
                    bool Error = false;

                    // let's build the tasks list
                    TextBoxLogWriteLine("Listing the jobs...");
                    List<Task> deleteTasks = new List<Task>();

                    //  foreach (var transform in dataGridViewTransformsV.ReturnSelectedTransforms())
                    {
                        var transform = dataGridViewTransformsV.ReturnSelectedTransforms().First();
                        var listjobs = _amsClientV3.AMSclient.Jobs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, transform.Name);
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

                    if (!Error) TextBoxLogWriteLine("Job(s) canceled.");
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

                    Task.Run(async () =>
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
                        if (!Error) TextBoxLogWriteLine("Transform(s) deleted.");
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


        private static void CheckQuicktimeAndDisplayMessage(List<IAsset> SelectedAssets)
        {
            if (SelectedAssets.Any(a => AssetInfo.GetAssetType(a) == "MOV (1)"))
            {
                bool multi = SelectedAssets.Count > 1;
                MessageBox.Show(string.Format("Asset{0} seem{1} to be a Quicktime or ProRes file.", multi ? "s" : "", multi ? "" : "s") + Constants.endline + "You should use Media Encoder Standard instead.", "Format issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Mainform_Shown(object sender, EventArgs e)
        {
            // display the update message if a new version is available
            if (!string.IsNullOrEmpty(Program.MessageNewVersion)) TextBoxLogWriteLine(Program.MessageNewVersion);
        }


        private void oSMFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) Set the src to MPEG-DASH or Smooth Streaming source" + Constants.endline + "2) Select 'Microsoft Adaptive Streaming Plugin'" + Constants.endline + "3) Click 'Preview and Update'");
            System.Diagnostics.Process ieProcess = System.Diagnostics.Process.Start("iexplore", Constants.PlayerOSMFRCst);
        }


        /// <summary>
        /// Updates your configuration .xml file dynamically.
        /// </summary>
        /// <param name="licenseAcquisitionUrl">The URL of your 
        ///        license acquisition server. For example:
        ///        "http://playready-testserver.azurewebsites.net/rightsmanager.asmx"
        /// </param>
        public static string LoadAndUpdatePlayReadyConfiguration(string xmlFileName, string keyseed, string licenseAcquisitionUrlstr, Guid keyId, string contentkey, bool useSencBox, bool adjustSubSamples, string serviceid, string customattributes)
        {
            Uri keyDeliveryServiceUri = null;
            if (!string.IsNullOrEmpty(licenseAcquisitionUrlstr)) keyDeliveryServiceUri = new Uri(licenseAcquisitionUrlstr);

            XNamespace xmlns = "http://schemas.microsoft.com/iis/media/v4/TM/TaskDefinition#";

            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var keyseedEl = doc
                 .Descendants(xmlns + "property")
                 .Where(p => p.Attribute("name").Value == "keySeedValue")
                 .FirstOrDefault();
            var licenseAcquisitionUrlEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "licenseAcquisitionUrl")
                    .FirstOrDefault();
            var contentKeyEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "contentKey")
                    .FirstOrDefault();
            var keyIdEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "keyId")
                    .FirstOrDefault();
            var useSencBoxEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "useSencBox")
                   .FirstOrDefault();
            var adjustSubSamplesEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "adjustSubSamples")
                   .FirstOrDefault();

            var serviceIdEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "serviceId")
                   .FirstOrDefault();

            var customAttributesEl = doc
                   .Descendants(xmlns + "property")
                   .Where(p => p.Attribute("name").Value == "customAttributes")
                   .FirstOrDefault();


            // Update the "value" property.

            if (keyseed != null)
                keyseedEl.Attribute("value").SetValue(keyseed);

            if (licenseAcquisitionUrlstr != null && keyDeliveryServiceUri != null)
                licenseAcquisitionUrlEl.Attribute("value").SetValue(keyDeliveryServiceUri);

            if (contentkey != null)
                contentKeyEl.Attribute("value").SetValue(contentkey);

            if (keyId != null)
                keyIdEl.Attribute("value").SetValue(keyId);

            if (useSencBoxEl != null)
                useSencBoxEl.Attribute("value").SetValue(useSencBox.ToString());

            if (adjustSubSamplesEl != null)
                adjustSubSamplesEl.Attribute("value").SetValue(adjustSubSamples.ToString());

            if (serviceIdEl != null)
                serviceIdEl.Attribute("value").SetValue(serviceid.ToString());

            if (customAttributesEl != null)
                customAttributesEl.Attribute("value").SetValue(customattributes.ToString());

            return doc.ToString();
        }



        public static string LoadAndUpdateHLSConfiguration(string xmlFileName, bool encrypt, string key, string keyuri, string maxbitrate, string segment)
        {
            XNamespace xmlns = "http://schemas.microsoft.com/iis/media/v4/TM/TaskDefinition#";

            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var encryptEl = doc
                .Descendants(xmlns + "property")
                .Where(p => p.Attribute("name").Value == "encrypt")
                .FirstOrDefault();
            var keyEl = doc
                 .Descendants(xmlns + "property")
                 .Where(p => p.Attribute("name").Value == "key")
                 .FirstOrDefault();
            var keyuriEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "keyuri")
                    .FirstOrDefault();
            var maxbitrateEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "maxbitrate")
                    .FirstOrDefault();
            var segmentEl = doc
                    .Descendants(xmlns + "property")
                    .Where(p => p.Attribute("name").Value == "segment")
                    .FirstOrDefault();

            // Update the "value" property.
            if (maxbitrateEl != null)
                maxbitrateEl.Attribute("value").SetValue(maxbitrate);

            if (segmentEl != null)
                segmentEl.Attribute("value").SetValue(segment);

            if (encryptEl != null)
                encryptEl.Attribute("value").SetValue(encrypt.ToString());

            if (encrypt)
            {
                if (!string.IsNullOrEmpty(keyuri))
                {
                    Uri keyurluri = new Uri(keyuri);
                    if (keyuriEl != null)
                        keyuriEl.Attribute("value").SetValue(keyurluri);
                }

                if (keyEl != null)
                    keyEl.Attribute("value").SetValue(key);
            }
            return doc.ToString();
        }


        private void DoMenuVideoAnalytics(string processorStr, System.Drawing.Image processorImage, string urlMoreInfo, string preset = null, bool preview = true)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0 || SelectedAssets.FirstOrDefault() == null)
            {
                MessageBox.Show("No asset was selected, or asset is null.");
            }
            else
            {
                CheckAssetSizeRegardingMediaUnit(SelectedAssets);

                // not needed as ism as primary seems to work ok
                // CheckPrimaryFileExtension(SelectedAssets, new[] { ".MOV", ".WMV", ".MP4" });

                // Get the SDK extension method to  get a reference to the processor.
                IMediaProcessor processor = GetLatestMediaProcessorByName(processorStr);

                var form = new MediaAnalyticsGeneric(_context, processor, processorImage, preview, urlMoreInfo)
                {
                    MIJobName = processorStr + " processing of " + Constants.NameconvInputasset,
                    MIOutputAssetName = Constants.NameconvInputasset + " - processed with " + processorStr,
                    MIInputAssetName = (SelectedAssets.Count > 1) ?
                    string.Format("{0} assets have been selected for processing.", SelectedAssets.Count)
                    : string.Format("Asset '{0}' will be processed.", SelectedAssets.FirstOrDefault().Name)
                };

                string taskname = string.Format("{0} processing of {1} ", processorStr, Constants.NameconvInputasset);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(processor,
                        SelectedAssets,
                        form.MIJobName,
                        form.JobOptions.Priority,
                        taskname,
                        form.MIOutputAssetName,
                        preset == null ? new List<string> { @"{'Version':'1.0'}" } : new List<string> { preset },
                        form.JobOptions.OutputAssetsCreationOptions,
                        form.JobOptions.OutputAssetsFormatOption,
                        form.JobOptions.TasksOptionsSetting,
                        form.JobOptions.StorageSelected);
                }
            }
        }


        public void LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, int jobpriority, string taskname, string outputassetname, List<string> configuration, AssetCreationOptions myAssetCreationOptions, AssetFormatOption myAssetFormatOption, TaskOptions myTaskOptions, string storageaccountname = "")
        {
            // a job per asset, one task per config
            Task.Factory.StartNew(() =>
            {
                foreach (IAsset asset in selectedassets)
                {
                    string jobnameloc = jobname.Replace(Constants.NameconvInputasset, asset.Name);
                    IJob myJob = _context.Jobs.Create(jobnameloc, jobpriority);
                    foreach (string config in configuration)
                    {
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvAMEpreset, config);
                        ITask myTask = myJob.Tasks.AddNew(
                               tasknameloc,
                              processor,
                              config,
                              myTaskOptions);

                        myTask.InputAssets.Add(asset);

                        // Add an output asset to contain the results of the task
                        string outputassetnameloc = outputassetname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvAMEpreset, config);
                        if (storageaccountname == "")
                        {
                            myTask.OutputAssets.AddNew(outputassetnameloc, asset.StorageAccountName, myAssetCreationOptions, myAssetFormatOption); // let's use the same storage account than the input asset
                        }
                        else
                        {
                            myTask.OutputAssets.AddNew(outputassetnameloc, storageaccountname, myAssetCreationOptions, myAssetFormatOption);
                        }
                    }

                    // Submit the job and wait until it is completed. 
                    bool Error = false;
                    try
                    {
                        TextBoxLogWriteLine("Job '{0}' : submitting...", jobnameloc);
                        myJob.Submit();
                    }

                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("Job '{0}' : problem", jobnameloc, true);
                        TextBoxLogWriteLine(ex);
                        Error = true;
                    }
                    if (!Error)
                    {
                        TextBoxLogWriteLine("Job '{0}' : submitted.", jobnameloc);
                        Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(new JobExtension()));
                    }
                    TextBoxLogWriteLine("");
                }

                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs);
                DoRefreshGridJobV(false);

            }

                );
        }


        public void LaunchJobs_OneJobPerInputAssetWithSpecificConfig(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, int jobpriority, string taskname, string outputassetname, List<string> configuration, AssetCreationOptions myAssetCreationOptions, AssetFormatOption myAssetFormatOption, TaskOptions myTaskOptions, string storageaccountname = "", bool copySubtitlesToInput = false)
        {
            // a job per asset, one task per job, but each task has a specific config
            Task.Factory.StartNew(() =>
            {
                int index = -1;

                foreach (IAsset asset in selectedassets)
                {
                    index++;
                    string jobnameloc = jobname.Replace(Constants.NameconvInputasset, asset.Name);
                    IJob myJob = _context.Jobs.Create(jobnameloc, jobpriority);

                    string config = configuration[index];

                    string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvAMEpreset, config);
                    ITask myTask = myJob.Tasks.AddNew(
                           tasknameloc,
                          processor,
                          config,
                          myTaskOptions);

                    myTask.InputAssets.Add(asset);

                    // Add an output asset to contain the results of the task
                    string outputassetnameloc = outputassetname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvAMEpreset, config);
                    if (storageaccountname == "")
                    {
                        myTask.OutputAssets.AddNew(outputassetnameloc, asset.StorageAccountName, myAssetCreationOptions, myAssetFormatOption); // let's use the same storage account than the input asset
                    }
                    else
                    {
                        myTask.OutputAssets.AddNew(outputassetnameloc, storageaccountname, myAssetCreationOptions, myAssetFormatOption);
                    }

                    // Submit the job and wait until it is completed. 
                    bool Error = false;
                    try
                    {
                        TextBoxLogWriteLine("Job '{0}' : submitting...", jobnameloc);
                        myJob.Submit();
                    }

                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("Job '{0}' : problem", jobnameloc, true);
                        TextBoxLogWriteLine(ex);
                        Error = true;
                    }
                    if (!Error)
                    {
                        TextBoxLogWriteLine("Job '{0}' : submitted.", jobnameloc);
                        Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(new JobExtension()));
                    }
                    TextBoxLogWriteLine();
                }

                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs);
                DoRefreshGridJobV(false);
            }

                );
        }


        private void DisplayDeprecatedMessageAME()
        {
            MessageBox.Show("The end of life date for Azure Media Encoder is March 1, 2017.\n\nIt is now recommended to use Media Encoder Standard (MES).\nIt provides better quality and performance, and it supports more input formats.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


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



        private static Dictionary<string, string> CheckSingleFileIndexerV1SupportedExtensions(List<IAsset> SelectedAssets, string[] mediaFileExtensions)
        {
            var IndexAnotherFile = new Dictionary<string, string>();

            if (SelectedAssets.Any(a => a.AssetFiles.Count() == 1 && !mediaFileExtensions.Contains(Path.GetExtension(a.AssetFiles.FirstOrDefault().Name).ToUpperInvariant())))
            {
                MessageBox.Show("If the input asset contains only one file, it must be a " + string.Join(", ", mediaFileExtensions) + " file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            int MultiFileAssetPb = 0;
            string assetnamepb = string.Empty;

            foreach (var asset in SelectedAssets)
            {
                if (asset.AssetFiles.Count() > 1)
                {
                    var pf = asset.AssetFiles.Where(a => a.IsPrimary).FirstOrDefault();
                    if (pf != null && !mediaFileExtensions.Contains(Path.GetExtension(pf.Name).ToUpperInvariant()))
                    { // primary file is not ok to index
                        if (SelectedAssets.Count < 5)
                        {
                            /*
                            var supportedfile = asset.AssetFiles.AsEnumerable().Where(af =>
                                                    mediaFileExtensions.Contains(Path.GetExtension(af.Name).ToUpperInvariant()))
                                                    .ToList().OrderByDescending(af => af.ContentFileSize);
                            if (supportedfile.Count() > 0) // but there is one that can be indexed
                            {
                                var proposedfile = supportedfile.FirstOrDefault().Name;
                                if (MessageBox.Show(string.Format("The asset '{0}'\nis a multi file asset and the primary file '{1}'\nis not supported as an input.\n\nConfigure Indexer to index file '{2}' ?", asset.Name, pf.Name, proposedfile), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    IndexAnotherFile.Add(asset.Id, proposedfile);
                                }
                            }
                            */
                            var supportedfile = asset.AssetFiles.ToList().Where(f => mediaFileExtensions.Contains(Path.GetExtension(f.Name).ToUpperInvariant())).ToList();
                            if (supportedfile.Count() > 0) // but there is one that can be indexed
                            {
                                var form = new MediaAnalyticsPickVideoFileInAsset(asset, mediaFileExtensions, false);
                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    IndexAnotherFile.Add(asset.Id, form.SelectedAssetFile.Name);
                                }
                            }
                            else
                            {
                                MultiFileAssetPb++; // if too many assets, we do not ask the user but we will warm him
                                assetnamepb = asset.Name;
                            }
                        }
                        else
                        {
                            MultiFileAssetPb++; // if too many assets, we do not ask the user but we will warm him
                            assetnamepb = asset.Name;
                        }
                    }
                }
            }

            if (MultiFileAssetPb == 1)
            {
                MessageBox.Show(string.Format("Asset '{0}' is a multi file asset and the primary file is not a " + string.Join(", ", mediaFileExtensions) + " file.\nIndexing will probably fail.", assetnamepb), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MultiFileAssetPb > 1)
            {
                MessageBox.Show(string.Format("There are {0} assets which are multi files assets and the primary file is not a " + string.Join(", ", mediaFileExtensions) + " file.\nIndexing will probably fail.", MultiFileAssetPb), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return IndexAnotherFile;
        }

        private void decryptAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDecryptAsset();
        }


        private void DoMenuDecryptAsset()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;

            }
            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null) return;

            string labeldb = (SelectedAssets.Count > 1) ? "Decrypt these " + SelectedAssets.Count + " assets  ?" : "Decrypt '" + mediaAsset.Name + "'  ?";

            string outputassetname = Constants.NameconvInputasset + " - Storage decrypted";
            string jobname = "Storage Decryption of " + Constants.NameconvInputasset;
            string taskname = "Storage Decryption of " + Constants.NameconvInputasset;

            if (System.Windows.Forms.MessageBox.Show(labeldb, "Asset Decryption", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                    MediaProcessorNames.StorageDecryption);

                LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(processor,
                    SelectedAssets,
                    jobname,
                    Properties.Settings.Default.DefaultJobPriority,
                    taskname,
                    outputassetname,
                    new List<string> { "" },
                    AssetCreationOptions.None,
                    AssetFormatOption.None,
                    Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
            }
        }

        private void azureMediaServicesPlayerPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerAMP);
        }

        private void hTML5VideoElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerInfoHTML5Video);
        }

        private void dynamicPackagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please create a streaming locator in the Publish menu." + Constants.endline + Constants.endline + "Check that you have, at least, one Standard or Premium Streaming endpoint" + Constants.endline + "The asset should be:" + Constants.endline + "- a Smooth Streaming asset (Clear or PlayReady protected)," + Constants.endline + "- or a Clear Multi MP4 asset.", "Dynamic Packaging");
        }



        private void Mainform_Load(object sender, EventArgs e)
        {
            Hide();

            linkLabelFeedbackAMS.Links.Add(new LinkLabel.Link(0, linkLabelFeedbackAMS.Text.Length, Constants.LinkFeedbackAMS));
            linkLabelMoreInfoMediaUnits.Links.Add(new LinkLabel.Link(0, linkLabelMoreInfoMediaUnits.Text.Length, Constants.LinkInfoMediaUnit));

            //comboBoxOrderJobs.Enabled = _context.Jobs.Count() < triggerForLargeAccountNbJobs;

            toolStripStatusLabelWatchFolder.Visible = false;

            comboBoxSearchAssetOption.Items.Add(new Item("Asset name (equals) :", SearchIn.AssetNameEquals.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Asset name (greater than) :", SearchIn.AssetNameGreaterThan.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Asset name (less than) :", SearchIn.AssetNameLessThan.ToString()));

            comboBoxSearchAssetOption.Items.Add(new Item("Asset Id (equals) :", SearchIn.AssetId.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Asset alt Id (equals) :", SearchIn.AssetAltId.ToString()));
            comboBoxSearchAssetOption.SelectedIndex = 0;

            comboBoxSearchJobOption.Items.Add(new Item("Search in job name :", SearchIn.JobName.ToString()));
            comboBoxSearchJobOption.Items.Add(new Item("Search for job Id :", SearchIn.JobId.ToString()));
            comboBoxSearchJobOption.SelectedIndex = 0;

            comboBoxSearchChannelOption.Items.Add(new Item("Search in channel name :", SearchIn.ChannelName.ToString()));
            comboBoxSearchChannelOption.Items.Add(new Item("Search for channel Id :", SearchIn.ChannelId.ToString()));
            comboBoxSearchChannelOption.SelectedIndex = 0;

            comboBoxSearchProgramOption.Items.Add(new Item("Search in program name :", SearchIn.ProgramName.ToString()));
            comboBoxSearchProgramOption.Items.Add(new Item("Search for program Id :", SearchIn.ProgramId.ToString()));
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


            comboBoxFilterTimeChannel.Items.AddRange(
                typeof(FilterTime)
                .GetFields()
                .Select(i => i.GetValue(null) as string)
                .ToArray()
                );
            comboBoxFilterTimeChannel.SelectedIndex = 0;


            comboBoxStatusProgram.Items.AddRange(
                typeof(ProgramState)
                .GetFields()
                .Select(i => i.Name as string)
                .ToArray()
                );
            comboBoxStatusProgram.Items[0] = "All";
            comboBoxStatusProgram.SelectedIndex = 0;

            comboBoxStatusChannel.Items.AddRange(
              typeof(ChannelState)
              .GetFields()
              .Select(i => i.Name as string)
              .ToArray()
              );
            comboBoxStatusChannel.Items[0] = "All";
            comboBoxStatusChannel.SelectedIndex = 0;

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

            Show();
        }

        private void AddButtonsToSearchTextBox()
        {
            // let's add a button to asset textbox search
            var btna = new Button();
            btna.Size = new Size(18, textBoxAssetSearch.ClientSize.Height + 2);
            btna.Location = new Point(textBoxAssetSearch.ClientSize.Width - btna.Width, -1);
            btna.Anchor = AnchorStyles.Right;
            btna.Cursor = Cursors.Default;
            btna.Text = "X";
            btna.BackColor = SystemColors.Window;
            btna.Click += Btna_Click;
            textBoxAssetSearch.Controls.Add(btna);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(textBoxAssetSearch.Handle, 0xd3, (IntPtr)2, (IntPtr)(btna.Width << 16));

            // let's add a button to job textbox search
            var btnj = new Button();
            btnj.Size = new Size(18, textBoxJobSearch.ClientSize.Height + 2);
            btnj.Location = new Point(textBoxJobSearch.ClientSize.Width - btnj.Width, -1);
            btnj.Anchor = AnchorStyles.Right;
            btnj.Cursor = Cursors.Default;
            btnj.Text = "X";
            btnj.BackColor = SystemColors.Window;
            btnj.Click += Btnj_Click;
            textBoxJobSearch.Controls.Add(btnj);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(textBoxJobSearch.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnj.Width << 16));

            // let's add a button to channel textbox search
            var btnc = new Button();
            btnc.Size = new Size(18, textBoxSearchNameChannel.ClientSize.Height + 2);
            btnc.Location = new Point(textBoxSearchNameChannel.ClientSize.Width - btnc.Width, -1);
            btnc.Anchor = AnchorStyles.Right;
            btnc.Cursor = Cursors.Default;
            btnc.Text = "X";
            btnc.BackColor = SystemColors.Window;
            btnc.Click += Btnc_Click;
            textBoxSearchNameChannel.Controls.Add(btnc);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(textBoxSearchNameChannel.Handle, 0xd3, (IntPtr)2, (IntPtr)(btnc.Width << 16));

            // let's add a button to program textbox search
            var btnp = new Button();
            btnp.Size = new Size(18, textBoxSearchNameProgram.ClientSize.Height + 2);
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
            textBoxSearchNameChannel.Text = string.Empty;
            DoChannelSearch();
        }
        private void Btnp_Click(object sender, EventArgs e)
        {
            textBoxSearchNameProgram.Text = string.Empty;
            DoProgramSearch();
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void comboBoxStateJobsCountJobs() // To ad number of jobs in the combobox
        {
            int c = 0;
            string filter;
            const string p = "  (";

            for (int i = 0; i < comboBoxStateJobs.Items.Count; i++)
            {
                filter = comboBoxStateJobs.Items[i].ToString();
                if (filter.Contains(p)) filter = filter.Substring(0, filter.IndexOf(p));

                if (filter == "All")
                {
                    c = _context.Jobs.Count();
                }
                else
                {
                    c = _context.Jobs.Where(j => j.State == (Microsoft.WindowsAzure.MediaServices.Client.JobState)Enum.Parse(typeof(Microsoft.WindowsAzure.MediaServices.Client.JobState), filter)).Count();
                }
                if (c > 0) comboBoxStateJobs.Items[i] = string.Format("{0}  ({1})", filter, c);
                else comboBoxStateJobs.Items[i] = filter;
            }
        }

        private void createALocatorForTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateLocator(ReturnSelectedAssetsFromProgramsOrAssetsV3());
        }

        private void deleteAllLocatorsOfTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssetsV3();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }


        private void DoMenuProcessGeneric()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            CheckAssetSizeRegardingMediaUnit(SelectedAssets);

            string taskname = Constants.NameconvProcessorname + " processing of " + Constants.NameconvInputasset;

            MultipleProcessor form = new MultipleProcessor(_context)
            {
                EncodingProcessorsList = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList(),
                EncodingJobName = Constants.NameconvProcessorname + " processing of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + " - " + Constants.NameconvProcessorname + " processed",
                SelectedAssets = SelectedAssets,
                VisibleAssets = dataGridViewAssetsV.assets,
                EncodingCreationMode = TaskJobCreationMode.SingleJobForAllInputAssets
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var gentasks = form.GetGenericTasks;
                IAsset OutputAsset = null;

                if (form.EncodingCreationMode == TaskJobCreationMode.OneJobPerInputAsset || form.EncodingCreationMode == TaskJobCreationMode.OneJobPerVisibleAsset)
                // a job for each input asset
                {
                    if (form.EncodingCreationMode == TaskJobCreationMode.OneJobPerVisibleAsset)
                    {
                        SelectedAssets = dataGridViewAssetsV.assets.ToList();
                    }

                    foreach (IAsset asset in SelectedAssets)
                    {
                        string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, gentasks.Count > 1 ? "multi processors" : gentasks.FirstOrDefault().Processor.Name); ;
                        IJob job = _context.Jobs.Create(jobnameloc, form.JobPriority);

                        foreach (var usertask in gentasks)
                        // let's create all tasks and output assets
                        {
                            string assetname = string.Empty;
                            switch (usertask.InputAssetType)
                            {
                                case TypeInputAssetGeneric.InputJobAssets:
                                    assetname = asset.Name;
                                    break;
                                case TypeInputAssetGeneric.SpecificAssetID:
                                    assetname = AssetInfo.GetAsset(usertask.InputAsset, _context).Name;
                                    break;
                                case TypeInputAssetGeneric.TaskOutputAsset:
                                    assetname = "output of task#" + usertask.InputAsset;
                                    break;
                            }
                            string tasknameloc = taskname.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);
                            ITask task = job.Tasks.AddNew(
                                  tasknameloc,
                                 usertask.Processor,
                                 usertask.ProcessorConfiguration,
                                 usertask.TaskOptions.TasksOptionsSetting// form.JobOptions.TasksOptionsSetting
                                 );
                            task.Priority = usertask.TaskOptions.Priority;

                            if (form.SingleOutputAsset && OutputAsset != null)
                            {
                                task.OutputAssets.Add(OutputAsset);
                            }
                            else
                            {
                                string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);
                                OutputAsset = task.OutputAssets.AddNew(outputassetnameloc, usertask.TaskOptions.StorageSelected, usertask.TaskOptions.OutputAssetsCreationOptions, usertask.TaskOptions.OutputAssetsFormatOption);
                            }
                        }
                        // let(s branch the input assets
                        foreach (var usertask in gentasks)
                        {
                            switch (usertask.InputAssetType)
                            {
                                case TypeInputAssetGeneric.InputJobAssets:
                                    job.Tasks[gentasks.IndexOf(usertask)].InputAssets.Add(asset);
                                    break;
                                case TypeInputAssetGeneric.SpecificAssetID:
                                    job.Tasks[gentasks.IndexOf(usertask)].InputAssets.Add(AssetInfo.GetAsset(usertask.InputAsset, _context));
                                    break;
                                case TypeInputAssetGeneric.TaskOutputAsset:
                                    var oasset = job.Tasks[Convert.ToInt16(usertask.InputAsset)].OutputAssets;
                                    job.Tasks[gentasks.IndexOf(usertask)].InputAssets.AddRange(oasset);
                                    break;
                            }
                        }

                        TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                        // Submit the job and wait until it is completed. 
                        try
                        {
                            job.Submit();
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            if (SelectedAssets.Count < 5)
                            {
                                MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobnameloc) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                            TextBoxLogWriteLine(e);
                            return;
                        }
                        dataGridViewJobsV.DoJobProgress(new JobExtension());
                    }
                }
                else if (form.EncodingCreationMode == TaskJobCreationMode.SingleJobForAllInputAssets) // Create one job for all input
                {
                    string inputasssetname = SelectedAssets.Count == 1 ? SelectedAssets.FirstOrDefault().Name : "multiple assets";
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, inputasssetname).Replace(Constants.NameconvProcessorname, gentasks.Count > 1 ? "multi processors" : gentasks.FirstOrDefault().Processor.Name); ;

                    IJob job = _context.Jobs.Create(jobnameloc, form.JobPriority);

                    foreach (var usertask in gentasks)
                    // let's create all tasks and output assets
                    {
                        string assetname = string.Empty;
                        switch (usertask.InputAssetType)
                        {
                            case TypeInputAssetGeneric.InputJobAssets:
                                assetname = inputasssetname;
                                break;
                            case TypeInputAssetGeneric.SpecificAssetID:
                                assetname = AssetInfo.GetAsset(usertask.InputAsset, _context).Name;
                                break;
                            case TypeInputAssetGeneric.TaskOutputAsset:
                                assetname = "output of task#" + usertask.InputAsset;
                                break;
                        }
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);

                        ITask task = job.Tasks.AddNew(
                            tasknameloc,
                           usertask.Processor,
                           usertask.ProcessorConfiguration,
                           usertask.TaskOptions.TasksOptionsSetting
                           );

                        task.Priority = usertask.TaskOptions.Priority;

                        if (form.SingleOutputAsset && OutputAsset != null)
                        {
                            task.OutputAssets.Add(OutputAsset);
                        }
                        else
                        {
                            string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);
                            OutputAsset = task.OutputAssets.AddNew(outputassetnameloc, usertask.TaskOptions.StorageSelected, usertask.TaskOptions.OutputAssetsCreationOptions, usertask.TaskOptions.OutputAssetsFormatOption);
                        }
                    }
                    // let(s branch the input assets
                    foreach (var usertask in gentasks)
                    {
                        switch (usertask.InputAssetType)
                        {
                            case TypeInputAssetGeneric.InputJobAssets:
                                job.Tasks[gentasks.IndexOf(usertask)].InputAssets.AddRange(SelectedAssets);
                                break;
                            case TypeInputAssetGeneric.SpecificAssetID:
                                job.Tasks[gentasks.IndexOf(usertask)].InputAssets.Add(AssetInfo.GetAsset(usertask.InputAsset, _context));
                                break;
                            case TypeInputAssetGeneric.TaskOutputAsset:
                                var oasset = job.Tasks[Convert.ToInt16(usertask.InputAsset) - 1].OutputAssets;
                                job.Tasks[gentasks.IndexOf(usertask)].InputAssets.AddRange(oasset);
                                break;
                        }
                    }

                    TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                    // Submit the job and wait until it is completed. 
                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobnameloc) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }
                    dataGridViewJobsV.DoJobProgress(new JobExtension());

                }
                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs);
                DoRefreshGridJobV(false);
            }
        }

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
            dataGridViewAssetsV.RefreshAssets(GetTextBoxAssetsPageNumber() + 1);
            if (!dataGridViewAssetsV.CurrentPageIsMax)
            {
                SetTextBoxAssetsPageNumber(GetTextBoxAssetsPageNumber() + 1);
            }
        }

        private void butPrevPageAsset_Click(object sender, EventArgs e)
        {
            if (GetTextBoxAssetsPageNumber() > 1)
            {
                dataGridViewAssetsV.RefreshAssets(GetTextBoxAssetsPageNumber() - 1);

                SetTextBoxAssetsPageNumber(GetTextBoxAssetsPageNumber() - 1);
            }
        }

        private void butNextPageJob_Click(object sender, EventArgs e)
        {
            dataGridViewJobsV.Refreshjobs(GetTextBoxJobsPageNumber() + 1);
            if (!dataGridViewJobsV.CurrentPageIsMax)
            {
                SetTextBoxJobsPageNumber(GetTextBoxJobsPageNumber() + 1);
            }
        }

        private void butPrevPageJob_Click(object sender, EventArgs e)
        {
            if (GetTextBoxJobsPageNumber() > 1)
            {
                dataGridViewJobsV.Refreshjobs(GetTextBoxJobsPageNumber() - 1);

                SetTextBoxJobsPageNumber(GetTextBoxJobsPageNumber() - 1);
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
                notifyIcon1.Dispose();
            }
        }


        private async void dataGridViewAssetsV_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _amsClientV3.RefreshTokenIfNeeded();
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
                var celljobstatevalue = dataGridViewJobsV.Rows[e.RowIndex].Cells[dataGridViewJobsV.Columns["State"].Index].Value;

                if (celljobstatevalue != null)
                {
                    var JS = (Microsoft.Azure.Management.Media.Models.JobState)celljobstatevalue;
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
                var row = dataGridViewJobsV.Rows[e.RowIndex];
                var job = new JobExtension()
                {
                    Job = GetJob(row.Cells[dataGridViewJobsV.Columns["TransformName"].Index].Value.ToString(), row.Cells[dataGridViewJobsV.Columns["Name"].Index].Value.ToString()),
                    TransformName = row.Cells[dataGridViewJobsV.Columns["TransformName"].Index].Value.ToString()
                };

                if (job != null)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        if (DisplayInfo(job) == DialogResult.OK)
                        {
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Arrow;
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
            var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[indextype];  // Type cell
            if (cell.Value != null)
            {
                string TypeStr = (string)cell.Value;
                if (TypeStr.Contains(AssetInfo.Type_Workflow)) e.CellStyle.ForeColor = Color.Blue;
            }

            var cell1 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[indexassetwarning];  // warning
            if (cell1.Value != null)
            {
                bool warning = (bool)cell1.Value;
                if (warning) e.CellStyle.ForeColor = Color.Red;
            }



            if (e.ColumnIndex == indexlocalexp)  // locator expiration,
            {
                var value = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._locatorexpirationdatewarning].Value;
                if (value != null && (((bool)value) == true))
                    e.CellStyle.ForeColor = Color.Red;
            }
            else if (e.ColumnIndex == indexlocalexp)  // locator expiration,
            {
                var value = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._locatorexpirationdatewarning].Value;
                if (value != null && (((bool)value) == true))
                    e.CellStyle.ForeColor = Color.Red;
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._dynEnc].Index)// Mouseover for icons
            {
                var cell4 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value != null)
                    cell4.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value.ToString();
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._publication].Index)// Mouseover for icons
            {
                var cell5 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value != null)
                    cell5.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value.ToString();
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._filter].Index)// Mouseover for icon filter
            {
                var cell6 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._filterMouseOver].Value != null)
                    cell6.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._filterMouseOver].Value.ToString();
            }

        }

        private void toolStripMenuItemDisplayInfo_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedAssetsV3().FirstOrDefault());
        }

        private void contextMenuStripAssets_Opening(object sender, CancelEventArgs e)
        {
            var assets = ReturnSelectedAssetsV3();
            bool singleitem = (assets.Count == 1);
            var firstAsset = assets.FirstOrDefault();

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
                if (filter.Contains(p)) filter = filter.Substring(0, filter.IndexOf(p));
                dataGridViewJobsV.FilterJobsState = filter;
                DoRefreshGridJobV(false);
            }
        }

        private void DoCreateJobReportEmail()
        {
            JobInfo JR = new JobInfo(ReturnSelectedJobs(), _accountname);
            JR.CreateOutlookMail();
        }

        private void DoDisplayJobReport()
        {
            JobInfo JR = new JobInfo(ReturnSelectedJobs(), _accountname);
            StringBuilder SB = JR.GetStats();
            var tokenDisplayForm = new EditorXMLJSON("Job report", SB.ToString(), false, false, false);
            tokenDisplayForm.Display();
        }

        private void DoMenuDisplayAssetInfoFromLocatorID()
        {
            string locatorID = string.Empty;
            string clipbs = Clipboard.GetText();
            if (clipbs != null)
            {
                locatorID = clipbs;
            }

            if (Program.InputBox("Locator ID/GUID", "Please enter the known Locator Id or GUID :", ref locatorID) == DialogResult.OK)
            {
                ILocator knownLocator = _context.Locators.Where(l => l.Id == locatorID).FirstOrDefault();

                if (knownLocator == null)
                {
                    MessageBox.Show("This locator has not been found.");
                }
                else if (knownLocator.Asset != null)
                {
                    DisplayInfo(knownLocator.Asset);
                }
            }
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
                    for (int i = 0; i < dataGridViewTransfer.Columns.Count; i++) dataGridViewTransfer.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;

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

        private void DoOpenTransferDestLocation()
        {
            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                if ((TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value == TransferState.Finished)
                {
                    string location = dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["DestLocation"].Index].Value.ToString();

                    switch ((TransferType)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["Type"].Index].Value)
                    {
                        case TransferType.DownloadToLocal:
                            if (!string.IsNullOrEmpty(location) && location != null) Process.Start(location);
                            break;

                        case TransferType.ImportFromAzureStorage:
                        case TransferType.ImportFromHttp:
                        case TransferType.UploadFromFile:
                        case TransferType.UploadFromFolder:
                        case TransferType.UploadWithExternalTool:
                            _amsClientV3.RefreshTokenIfNeeded();
                            Asset asset = _amsClientV3.AMSclient.Assets.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, location);
                            if (asset != null) DisplayInfo(asset);
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
                var status = (TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value;

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
            var SelectedJobs = ReturnSelectedJobsV3();

            if (SelectedJobs.Count > 0)
            {
                PriorityForm form = new PriorityForm()
                {
                    JobPriority = (SelectedJobs.Count == 1) ? SelectedJobs[0].Job.Priority : Priority.Normal // if only one job so we pass the current priority to dialog box
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    foreach (var JobToProcess in SelectedJobs)

                        if (JobToProcess != null)
                        {
                            //delete
                            TextBoxLogWriteLine(string.Format("Changing priority to {0} for job '{1}'.", form.JobPriority, JobToProcess.Job.Name));
                            try
                            {
                                JobToProcess.Job.Priority = form.JobPriority;
                                _amsClientV3.RefreshTokenIfNeeded();
                                _amsClientV3.AMSclient.Jobs.Update(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, JobToProcess.TransformName, JobToProcess.Job.Name, JobToProcess.Job);
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

        private void changePriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoChangeJobPriority();
        }

        private void comboBoxFilterTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewAssetsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewAssetsV.TimeFilter == FilterTime.TimeRange)
            {
                var form = new TimeRangeSelection()
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
                var form = new TimeRangeSelection()
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
            var locators = client.AMSclient.Assets.ListStreamingLocators(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName, asset.Name).StreamingLocators;
            if (asset != null && locators.Count > 0)
            {
                var LocatorQuery = locators.Where(l => ((l.StartTime < DateTime.UtcNow) || (l.StartTime == null)) && (l.EndTime > DateTime.UtcNow)).FirstOrDefault();
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
            AR.CreateOutlookMail();
        }

        private void DoDisplayAssetReport()
        {
            AssetInfo AR = new AssetInfo(ReturnSelectedAssetsV3(), _amsClientV3);
            StringBuilder SB = AR.GetStats();
            var tokenDisplayForm = new EditorXMLJSON("Asset report", SB.ToString(), false, false, false);
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
            var SelectedJobs = ReturnSelectedJobsV3();
            if (SelectedJobs.Count != 0)
            {
                var jobToDisplay = SelectedJobs.FirstOrDefault();
                if (jobToDisplay != null)
                {
                    try
                    {
                        if (inputasset) // input
                        {
                            if (jobToDisplay.Job.Input.GetType() == typeof(JobInputAsset))
                            {
                                var jobinputasset = (JobInputAsset)jobToDisplay.Job.Input;
                                var asset = GetAsset(jobinputasset.AssetName);
                                if (asset != null)
                                    DisplayInfo(GetAsset(jobinputasset.AssetName));
                                else
                                    MessageBox.Show($"Input asset '{jobinputasset.AssetName}' not found.", "Asset error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }
                        else // output
                        {
                            if (jobToDisplay.Job.Outputs.FirstOrDefault() != null && (jobToDisplay.Job.Outputs.FirstOrDefault().GetType() == typeof(JobOutputAsset)))
                            {
                                var joboutputasset = (JobOutputAsset)jobToDisplay.Job.Outputs.FirstOrDefault();
                                var asset = GetAsset(joboutputasset.AssetName);
                                if (asset != null)
                                    DisplayInfo(GetAsset(joboutputasset.AssetName));
                                else
                                    MessageBox.Show($"Output asset '{joboutputasset.AssetName}' not found.", "Asset error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void DoCreateTestsAssets()
        {
            Task.Run(async () =>
            {
                int i = 168744;
                int c = 0;
                while (i < 200001)
                {
                    // String.Format("{0:00000}", 15);          // "00015"

                    _context.Assets.CreateAsync("Asset-" + string.Format("{0:000000}", i), AssetCreationOptions.None, CancellationToken.None);
                    i++;
                    c++;
                    if (c == 100)
                    {
                        Debug.WriteLine(i);
                        c = 0;
                    }
                }
            }
                        );
        }

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

        private void fromAzureStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorage();
        }

        private void fromASingleHTTPURLAmazonS3EtcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromHttp();
        }

        private void toAzureStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoExportAssetToAzureStorage();
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

            EnableChildItems(ref liveChannelToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(AMSExplorer.Properties.Resources.TabLive)));
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
                        foreach (ToolStripItem itemd in itemt.DropDownItems) itemd.Enabled = bflag;
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
                        foreach (ToolStripItem itemd in itemt.DropDownItems) itemd.Enabled = bflag;
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
                dataGridViewLiveEventsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
                dataGridViewLiveOutputV.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
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
                dataGridViewLiveEventsV.Init(_amsClientV3.AMSclient, _amsClientV3.credentialsEntry);
            }
            dataGridViewLiveEventsV.Invoke(new Action(() => dataGridViewLiveEventsV.RefreshChannels(1)));

            var count = _amsClientV3.AMSclient.LiveEvents.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName).Count();
            tabPageLive.Invoke(new Action(() => tabPageLive.Text = string.Format(AMSExplorer.Properties.Resources.TabLive + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, count)));
            labelChannels.Invoke(new Action(() => labelChannels.Text = string.Format(AMSExplorer.Properties.Resources.LabelChannel + " ({0}/{1})", dataGridViewLiveEventsV.DisplayedCount, count)));
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

            int backupindex = 0;
            dataGridViewLiveOutputV.Invoke(new Action(() => dataGridViewLiveOutputV.RefreshPrograms(backupindex + 1)));
            labelPrograms.Invoke(new Action(() => labelPrograms.Text = string.Format(AMSExplorer.Properties.Resources.LabelProgram + " ({0}/{1})", dataGridViewLiveOutputV.DisplayedCount, 0/*_context.Programs.Count()*/)));

        }

        private void DoRefreshGridStreamingEndpointV(bool firstime)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            if (firstime)
            {
                dataGridViewStreamingEndpointsV.Init(_amsClientV3.AMSclient, _amsClientV3.credentialsEntry);

            }
            Debug.WriteLine("DoRefreshGridOriginsVNotforsttime");
            dataGridViewStreamingEndpointsV.Invoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoints()));

            tabPageAssets.Invoke(new Action(() => tabPageOrigins.Text = string.Format(AMSExplorer.Properties.Resources.TabOrigins + " ({0})", dataGridViewStreamingEndpointsV.DisplayedCount)));
        }


        private void DoRefreshGridStorageV(bool firstime)
        {
            _amsClientV3.RefreshTokenIfNeeded();
            var amsaccount = _amsClientV3.AMSclient.Mediaservices.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);
            const long OneTBInByte = 1099511627776;
            const long TotalStorageInBytes = OneTBInByte * 500;

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

            foreach (var storage in amsaccount.StorageAccounts)
            {

                long? capacity = null;
                try
                {
                    capacity = _amsClientV3.GetStorageCapacity(storage.Id);
                }
                catch (Exception ex)
                {
                    //TextBoxLogWriteLine(ex);
                }

                /*
                double? capacityPercentageFullTmp = null;
                if (storage.BytesUsed != null)
                {
                    displaycapacity = true;
                    capacityPercentageFullTmp = (double)((100 * (double)storage.BytesUsed) / (double)TotalStorageInBytes);
                }
                */

                var name = AMSClientV3.GetStorageName(storage.Id);
                string append = "";
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

            var filters = _amsClientV3.AMSclient.AccountFilters.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName);
            foreach (var filter in filters)
            {
                string s = null;
                string e = null;
                string d = null;
                string l = null;

                if (filter.PresentationTimeRange != null)
                {
                    var start = filter.PresentationTimeRange.StartTimestamp;
                    var end = filter.PresentationTimeRange.EndTimestamp;
                    var dvr = filter.PresentationTimeRange.PresentationWindowDuration;
                    var backoff = filter.PresentationTimeRange.LiveBackoffDuration;

                    if (true)//filter.PresentationTimeRange.Timescale != null)
                    {
                        double dscale = (double)filter.PresentationTimeRange.Timescale / (double)TimeSpan.TicksPerSecond;
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
                    var nbtracks = filter.Tracks.Count;
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, filter.Tracks.Count, s, e, d, l);
                }
                catch
                {
                    int rowi = dataGridViewFilters.Rows.Add(filter.Name, "Error", s, e, d, l);
                }
            }

            tabPageFilters.Text = string.Format(AMSExplorer.Properties.Resources.TabFilters + " ({0})", filters.Count());
        }


        private List<IChannel> ReturnSelectedChannels()
        {
            List<IChannel> SelectedChannels = new List<IChannel>();
            foreach (DataGridViewRow Row in dataGridViewLiveEventsV.SelectedRows)
            {
                // sometimes, the channel can be null (if just deleted)
                var channel = _context.Channels.Where(j => j.Id == Row.Cells[dataGridViewLiveEventsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault();
                if (channel != null)
                {
                    SelectedChannels.Add(channel);
                }
            }
            SelectedChannels.Reverse();
            return SelectedChannels;
        }

        private List<LiveEvent> ReturnSelectedLiveEvents()
        {
            List<LiveEvent> SelectedLiveEvents = new List<LiveEvent>();
            foreach (DataGridViewRow Row in dataGridViewLiveEventsV.SelectedRows)
            {
                // sometimes, the channel can be null (if just deleted)
                var liveEvent = GetLiveEvent(Row.Cells[dataGridViewLiveEventsV.Columns["Name"].Index].Value.ToString());
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
                var se = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, seName);
                if (se != null)
                {
                    SelectedOrigins.Add(se);
                }
            }
            SelectedOrigins.Reverse();
            return SelectedOrigins;
        }

        private List<IProgram> ReturnSelectedPrograms()
        {
            List<IProgram> SelectedPrograms = new List<IProgram>();
            foreach (DataGridViewRow Row in dataGridViewLiveOutputV.SelectedRows)
            {
                var program = _context.Programs.Where(j => j.Id == Row.Cells[dataGridViewLiveOutputV.Columns["Id"].Index].Value.ToString()).FirstOrDefault();
                if (program != null)
                {
                    SelectedPrograms.Add(program);
                }
            }
            SelectedPrograms.Reverse();
            return SelectedPrograms;
        }

        private List<LiveOutput> ReturnSelectedLiveOutputs()
        {
            List<LiveOutput> SelectedLiveOutputs = new List<LiveOutput>();
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (DataGridViewRow Row in dataGridViewLiveOutputV.SelectedRows)
            {
                var liveOutput = _amsClientV3.AMSclient.LiveOutputs.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, Row.Cells[dataGridViewLiveOutputV.Columns["LiveEventName"].Index].Value.ToString(), Row.Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString());
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

            Task.Run(async () =>
            {
                DoStartLiveEventsEngine(ReturnSelectedLiveEvents());
            }
                    );
        }


        internal async Task<IOperation> IObjectExecuteOperationAsync(Func<Task<IOperation>> fCall, string objectname, string objectlogname, string strStatusSuccess, CloudMediaContext context) // used for creation 
        {
            IOperation operation = null;
            try
            {
                operation = await fCall();
                while (operation.State == OperationState.InProgress)
                {
                    //refresh the operation
                    operation = context.Operations.GetOperation(operation.Id);
                    System.Threading.Thread.Sleep(1000);
                }
                if (operation.State == OperationState.Succeeded)
                {
                    TextBoxLogWriteLine("{0} '{1}' : {2}.", objectlogname, objectname, strStatusSuccess);
                }
                else
                {
                    TextBoxLogWriteLine("{0} '{1}' : NOT {2}. (Error {3})", objectlogname, objectname, strStatusSuccess, operation.ErrorCode, true);
                    TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("{0} '{1}' : Error {2}", objectlogname, objectname, Program.GetErrorMessage(ex), true);
            }
            return operation;
        }


        private async void DoStopOrDeleteLiveEvents(bool deleteLiveEvents)
        {
            // delete also if delete = true
            var ListEvents = ReturnSelectedLiveEvents();
            List<LiveOutput> LOList = new List<LiveOutput>();
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (var le in ListEvents)
            {
                LOList.AddRange(_amsClientV3.AMSclient.LiveOutputs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, le.Name).ToList());
            }

            string channelstr = ListEvents.Count > 1 ? "live events" : "live event";

            if (ListEvents.Count > 0)
            {
                if (LOList.Count > 0) // There are live outputs associated to the live event(s) to be deleted
                {
                    string leaction = deleteLiveEvents ? "Delete" : "Stop";
                    string question = (LOList.Count == 1) ? string.Format("There is one live output associated to the {0}.\n{1} the {0} and delete live output '{2}' ?", channelstr, leaction, LOList[0].Name)
                                                        : string.Format("There are {0} live outputs associated to the {1}.\n{2} the c{1} and delete these live outputs ?", LOList.Count, channelstr, leaction);

                    DeleteLiveOutputEvent form = new DeleteLiveOutputEvent(question, "Delete");
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await Task.Factory.StartNew(() =>
                           DoDeleteLiveOutputsEngine(LOList, form.DeleteAsset)
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

                    if (System.Windows.Forms.MessageBox.Show(question, "C" + channelstr + " deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                }

                var myTask = Task.Factory.StartNew(() =>
                                    DoStopOrDeleteLiveEventsEngine(ListEvents, deleteLiveEvents)
                                     );

            }
        }


        private void dataGridViewLiveV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cellchannelstatevalue = dataGridViewLiveEventsV.Rows[e.RowIndex].Cells[dataGridViewLiveEventsV.Columns["State"].Index].Value;

            if (cellchannelstatevalue != null)
            {
                ChannelState CS = (ChannelState)cellchannelstatevalue;
                Color mycolor;

                switch (CS)
                {
                    case ChannelState.Deleting:
                        mycolor = Color.Red;
                        break;
                    case ChannelState.Stopping:
                        mycolor = Color.OrangeRed;
                        break;
                    case ChannelState.Starting:
                        mycolor = Color.DarkCyan;
                        break;
                    case ChannelState.Stopped:
                        mycolor = Color.Blue;
                        break;
                    case ChannelState.Running:
                        mycolor = Color.Green;
                        break;
                    default:
                        mycolor = Color.Black;
                        break;
                }
                e.CellStyle.ForeColor = mycolor;
            }
        }

        private async void DoResetLiveEvents()
        {
            var ListEvents = ReturnSelectedLiveEvents();
            List<Program.LiveOutputExt> LOList = new List<Program.LiveOutputExt>();
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (var le in ListEvents)
            {
                var plist = _amsClientV3.AMSclient.LiveOutputs.List(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, le.Name).ToList();
                plist.ForEach(p => LOList.Add(new Program.LiveOutputExt() { LiveOutputItem = p, LiveEventName = le.Name }));
            }

            var liveOutputRunningQuery = LOList.Where(p => p.LiveOutputItem.ResourceState == LiveOutputResourceState.Running);

            if (LOList.Where(p => p.LiveOutputItem.ResourceState == LiveOutputResourceState.Creating || p.LiveOutputItem.ResourceState == LiveOutputResourceState.Deleting).Count() > 0) // live outputs are in creation or deletion mode
                MessageBox.Show("Some live outputs are being created or deleted. Live event(s) cannot be reset now.", "Live event(s) stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                 DoDeleteLiveOutputs(liveOutputRunningQuery.Select(o => o.LiveOutputItem).ToList());

                                 // let's reset the live events now that running output are stopped
                                 ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Reseting live event '{0}'...", e.Name));
                                 var tasksreset = ListEvents.Select(c => _amsClientV3.AMSclient.LiveEvents.ResetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                                 await Task.WhenAll(tasksreset);
                                 ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Live event '{0}' reset.", e.Name));

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
                               // let's reset the channels now that live outputs are deleted
                               ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Reseting live event '{0}'...", e.Name));
                               var tasksreset = ListEvents.Select(c => _amsClientV3.AMSclient.LiveEvents.ResetAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                               await Task.WhenAll(tasksreset);
                               ListEvents.ToList().ForEach(e => TextBoxLogWriteLine("Live event '{0}' reset.", e.Name));
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


        private void createChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateLiveEvent();
        }

        private async void DoCreateLiveEvent()
        {
            LiveEventCreation form = new LiveEventCreation()
            {
                KeyframeInterval = Properties.Settings.Default.LiveKeyFrameInterval.ToString(),
                StartChannelNow = true
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                TextBoxLogWriteLine("Channel '{0}' : creating...", form.LiveEventName);

                bool Error = false;
                ChannelCreationOptions options = new ChannelCreationOptions();
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
                        KeyFrameIntervalDuration = form.KeyframeInterval,
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
                                               form.LowLatencyMode ?  StreamOptionsFlag.LowLatency: StreamOptionsFlag.Default
                                              }
                                                );

                }

                catch (Exception ex)
                {
                    Error = true;
                    TextBoxLogWriteLine("Error with channel settings.", true);
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
                                                                         autoStart: form.StartChannelNow ? true : false)
                                                                      );

                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error with channel creation.", true);
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


        private async void DoDisplayLiveEventInfo(List<LiveEvent> channels)
        {
            var firstchannel = channels.FirstOrDefault();
            bool multiselection = channels.Count > 1;

            if (firstchannel != null)
            {
                LiveEventInformation form = new LiveEventInformation(this, _amsClientV3)
                {
                    MyLiveEvent = firstchannel,
                    MultipleSelection = multiselection
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    var modifications = form.Modifications;
                    if (multiselection)
                    {
                        var formSettings = new SettingsSelection("channels", modifications);
                        if (formSettings.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        else
                        {
                            modifications = (ExplorerLiveEventModifications)formSettings.SettingsObject;
                        }
                    }

                    foreach (var channel in channels)
                    {
                        TextBoxLogWriteLine("Live event '{0}' : updating...", channel.Name);

                        if (modifications.Description) // let' update description if needed
                        {
                            channel.Description = form.GetLiveEventDescription;
                        }
                        if (modifications.KeyFrameInterval)
                        {
                            channel.Input.KeyFrameIntervalDuration = form.KeyframeInterval;
                        }

                        if (channel.Encoding.EncodingType == firstchannel.Encoding.EncodingType)
                        {
                            if (channel.Encoding.EncodingType != LiveEventEncodingType.None && channel.Encoding != null && channel.ResourceState == LiveEventResourceState.Stopped)
                            {
                                if (modifications.SystemPreset)
                                {
                                    channel.Encoding.PresetName = form.PresetName; // we update the system preset
                                }

                            }
                            else if (channel.Encoding.EncodingType != LiveEventEncodingType.None && channel.ResourceState != LiveEventResourceState.Stopped)
                            {
                                TextBoxLogWriteLine("Live event '{0}' : must be stoped to update the encoding settings", channel.Name);
                            }
                            else if (channel.Encoding.EncodingType != LiveEventEncodingType.None && channel.Encoding == null)
                            {
                                TextBoxLogWriteLine("Live event '{0}' : configured as encoding live event but settings are null", channel.Name, true);
                            }
                        }



                        if (modifications.InputIPAllowList)
                        {
                            // Input allow list
                            if (form.GetInputAllowList != null)
                            {
                                if (channel.Input.AccessControl == null)
                                {
                                    channel.Input.AccessControl = new LiveEventInputAccessControl();
                                }
                                channel.Input.AccessControl.Ip = form.GetInputAllowList;
                            }
                            else
                            {
                                if (channel.Input.AccessControl != null)
                                {
                                    channel.Input.AccessControl.Ip = null;
                                }
                            }
                        }


                        if (modifications.PreviewIPAllowList)
                        {
                            // Preview allow list
                            if (form.GetPreviewAllowList != null)
                            {
                                if (channel.Preview.AccessControl == null)
                                {
                                    channel.Preview.AccessControl = new LiveEventPreviewAccessControl();
                                }
                                channel.Preview.AccessControl.Ip = form.GetPreviewAllowList;
                            }
                            else
                            {
                                if (channel.Preview.AccessControl != null)
                                {
                                    channel.Preview.AccessControl.Ip = null;
                                }
                            }
                        }


                        if (modifications.ClientAccessPolicy)
                        {
                            // Client Access Policy
                            if (form.GetLiveEventClientPolicy != null)
                            {
                                if (channel.CrossSiteAccessPolicies == null)
                                {
                                    channel.CrossSiteAccessPolicies = new Microsoft.Azure.Management.Media.Models.CrossSiteAccessPolicies();
                                }
                                channel.CrossSiteAccessPolicies.ClientAccessPolicy = form.GetLiveEventClientPolicy;

                            }
                            else
                            {
                                if (channel.CrossSiteAccessPolicies != null)
                                {
                                    channel.CrossSiteAccessPolicies.ClientAccessPolicy = null;
                                }
                            }
                        }

                        if (modifications.CrossDomainPolicy)
                        {
                            // Cross domain  Policy
                            if (form.GetLiveEventCrossdomainPolicy != null)
                            {
                                if (channel.CrossSiteAccessPolicies == null)
                                {
                                    channel.CrossSiteAccessPolicies = new Microsoft.Azure.Management.Media.Models.CrossSiteAccessPolicies();
                                }
                                channel.CrossSiteAccessPolicies.CrossDomainPolicy = form.GetLiveEventCrossdomainPolicy;

                            }
                            else
                            {
                                if (channel.CrossSiteAccessPolicies != null)
                                {
                                    channel.CrossSiteAccessPolicies.CrossDomainPolicy = null;
                                }
                            }
                        }
                        _amsClientV3.RefreshTokenIfNeeded();

                        Task.Run(async () =>
                        {
                            await _amsClientV3.AMSclient.LiveEvents.UpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, channel.Name, channel);
                            dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshChannel(channel)), null);
                            TextBoxLogWriteLine("Live event '{0}' : updated.", channel.Name);
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
                Debug.WriteLine("channel selection changed : begin");
                List<LiveEvent> SelectedChannels = ReturnSelectedLiveEvents();
                if (SelectedChannels.Count > 0)
                {

                    dataGridViewLiveOutputV.LiveEventSourceNames = SelectedChannels.Select(c => c.Name).ToList();

                    Task.Run(() =>
                    {
                        Debug.WriteLine("channel selection changed : before refresh");
                        DoRefreshGridLiveOutputV(false);
                    });
                }
            }
        }

        private async void DoStopOrDeleteLiveEventsEngine(List<LiveEvent> ListEvents, bool deleteLiveEvents)
        {

            // Stop the channels which run
            var liveeventsrunning = ListEvents.Where(p => p.ResourceState == LiveEventResourceState.Running).ToList();
            var names = String.Join(", ", liveeventsrunning.Select(le => le.Name).ToArray());

            if (liveeventsrunning.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Stopping live event(s) : {0}...", names));
                    var states = liveeventsrunning.Select(p => p.ResourceState).ToList();
                    var taskcstop = liveeventsrunning.Select(c => _amsClientV3.AMSclient.LiveEvents.StopAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    int complete = 0;
                    while (!taskcstop.All(t => t.IsCompleted) && complete != liveeventsrunning.Count)
                    {
                        // refresh the channels

                        foreach (var loitem in liveeventsrunning)
                        {
                            var loitemR = GetLiveEvent(loitem.Name);
                            if (loitemR != null && states[liveeventsrunning.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[liveeventsrunning.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshChannel(loitemR)), null);
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

                // delete the channels
                try
                {
                    var names2 = String.Join(", ", ListEvents.Select(le => le.Name).ToArray());

                    TextBoxLogWriteLine(string.Format("Deleting live event(s) : {0}...", names2));
                    var states = ListEvents.Select(p => p.ResourceState).ToList();
                    var taskcdel = ListEvents.Select(c => _amsClientV3.AMSclient.LiveEvents.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    while (!taskcdel.All(t => t.IsCompleted))
                    {
                        // refresh the channels

                        foreach (var loitem in ListEvents)
                        {
                            var loitemR = GetLiveEvent(loitem.Name);
                            if (loitemR != null && states[ListEvents.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[ListEvents.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshChannel(loitemR)), null);
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


        private async void DoStartLiveEventsEngine(List<LiveEvent> ListEvents)
        {
            // Start the channels which are stopped
            var liveevntsstopped = ListEvents.Where(p => p.ResourceState == LiveEventResourceState.Stopped).ToList();
            var names = String.Join(", ", liveevntsstopped.Select(le => le.Name).ToArray());
            if (liveevntsstopped.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Starting live event(s) : {0}...", names));
                    var states = liveevntsstopped.Select(p => p.ResourceState).ToList();
                    var taskLEStart = liveevntsstopped.Select(c => _amsClientV3.AMSclient.LiveEvents.StartAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                    int complete = 0;

                    while (!taskLEStart.All(t => t.IsCompleted) && complete != liveevntsstopped.Count)
                    {
                        // refresh the channels

                        foreach (var loitem in liveevntsstopped)
                        {
                            var loitemR = GetLiveEvent(loitem.Name);
                            if (loitemR != null && states[liveevntsstopped.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[liveevntsstopped.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewLiveEventsV.RefreshChannel(loitemR)), null);
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


        private async void DoDeleteLiveOutputs(List<LiveOutput> ListOutputs = null)
        {
            // delete also if delete = true
            if (ListOutputs == null) ListOutputs = ReturnSelectedLiveOutputs();

            if (ListOutputs.Count > 0)
            {
                string question = (ListOutputs.Count == 1) ? string.Format("Delete the live output '{0}' ?", ListOutputs[0].Name)
                                                        : string.Format("Delete these {0} live outputs ?", ListOutputs.Count);

                DeleteLiveOutputEvent form = new DeleteLiveOutputEvent(question, "Delete");
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DoDeleteLiveOutputsEngine(ListOutputs, form.DeleteAsset);
                    /*
                                        await Task.Run(() =>
                                        {
                                            DoDeleteLiveOutputsEngine(ListOutputs, form.DeleteAsset);
                                        });
                                        */
                }
            }
        }


        private async void DoDeleteLiveOutputsEngine(List<LiveOutput> ListOutputs, bool DeleteAsset)
        {
            var assets = ListOutputs.Select(p => p.AssetName).ToArray();

            bool Error = false;
            _amsClientV3.RefreshTokenIfNeeded();

            try
            {   // delete programs
                ListOutputs.ToList().ForEach(p => TextBoxLogWriteLine("Live output '{0}' : deleting...", p.Name));
                var states = ListOutputs.Select(p => p.ResourceState).ToList();
                var tasks = ListOutputs.Select(p => _amsClientV3.AMSclient.LiveOutputs.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, LiveOutputUtil.ReturnLiveEventFromOutput(p), p.Name)).ToArray();

                while (!tasks.All(t => t.IsCompleted))
                {
                    // refresh the programs

                    foreach (var loitem in ListOutputs)
                    {
                        var loitemR = GetLiveOutput(LiveOutputUtil.ReturnLiveEventFromOutput(loitem), loitem.Name);
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
                var tasksassets = assets.Select(a => _amsClientV3.AMSclient.Assets.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, a)).ToArray();
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

        private async void DoStartStreamingEndpointEngine(List<StreamingEndpoint> ListStreamingEndpoints)
        {
            // Start the streaming endpoint which are stopped
            var streamingendpointsstopped = ListStreamingEndpoints.Where(p => p.ResourceState == StreamingEndpointResourceState.Stopped).ToList();
            var names = String.Join(", ", streamingendpointsstopped.Select(le => le.Name).ToArray());
            if (streamingendpointsstopped.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Starting streaming endpoint(s) : {0}...", names));
                    var states = streamingendpointsstopped.Select(p => p.ResourceState).ToList();
                    var taskSEStart = streamingendpointsstopped.Select(c => _amsClientV3.AMSclient.StreamingEndpoints.StartAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();
                    int complete = 0;

                    while (!taskSEStart.All(t => t.IsCompleted) && complete != streamingendpointsstopped.Count)
                    {
                        // refresh the channels

                        foreach (var loitem in streamingendpointsstopped)
                        {
                            var loitemR = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, loitem.Name);
                            if (loitemR != null && states[streamingendpointsstopped.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[streamingendpointsstopped.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(loitemR)), null);
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




        private async void DoUpdateAndScaleStreamingEndpointEngine(StreamingEndpoint se, int? units = null)
        {
            _amsClientV3.RefreshTokenIfNeeded();

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


        private async void DoStopOrDeleteStreamingEndpointsEngine(List<StreamingEndpoint> ListStreamingEndpoints, bool deleteStreamingEndpoints)
        {

            // Stop the streaming endpoints which run
            var sesrunning = ListStreamingEndpoints.Where(p => p.ResourceState == StreamingEndpointResourceState.Running).ToList();
            var names = String.Join(", ", sesrunning.Select(le => le.Name).ToArray());

            if (sesrunning.Count() > 0)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                try
                {
                    TextBoxLogWriteLine(string.Format("Stopping streaming endpoints(s) : {0}...", names));
                    var states = sesrunning.Select(p => p.ResourceState).ToList();
                    var taskSEstop = sesrunning.Select(c => _amsClientV3.AMSclient.StreamingEndpoints.StopAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    int complete = 0;
                    while (!taskSEstop.All(t => t.IsCompleted) && complete != sesrunning.Count)
                    {
                        // refresh the streaming endpoints

                        foreach (var loitem in sesrunning)
                        {
                            var loitemR = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, loitem.Name);
                            if (loitemR != null && states[sesrunning.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[sesrunning.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(loitemR)), null);
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
                    var names2 = String.Join(", ", ListStreamingEndpoints.Select(le => le.Name).ToArray());

                    TextBoxLogWriteLine(string.Format("Deleting streaming endpoints(s) : {0}...", names2));
                    var states = ListStreamingEndpoints.Select(p => p.ResourceState).ToList();
                    var taskSEdel = ListStreamingEndpoints.Select(c => _amsClientV3.AMSclient.StreamingEndpoints.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, c.Name)).ToArray();

                    while (!taskSEdel.All(t => t.IsCompleted))
                    {
                        // refresh the channels

                        foreach (var loitem in ListStreamingEndpoints)
                        {
                            var loitemR = _amsClientV3.AMSclient.StreamingEndpoints.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, loitem.Name);
                            if (loitemR != null && states[ListStreamingEndpoints.IndexOf(loitem)] != loitemR.ResourceState)
                            {
                                states[ListStreamingEndpoints.IndexOf(loitem)] = loitemR.ResourceState;
                                dataGridViewLiveEventsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(loitemR)), null);
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


        private async void DoCreateLiveOutput()
        {
            var liveEvent = ReturnSelectedLiveEvents().FirstOrDefault();
            if (liveEvent != null)
            {
                string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);

                LiveOutputCreation form = new LiveOutputCreation(_amsClientV3)
                {
                    ChannelName = liveEvent.Name,
                    archiveWindowLength = new TimeSpan(0, 5, 0),
                    CreateLocator = true,
                    EnableDynEnc = false,
                    AssetName = Constants.NameconvChannel + "-" + Constants.NameconvProgram,
                    ProgramName = "LiveOutput-" + uniqueness,
                    HLSFragmentPerSegment = Properties.Settings.Default.LiveHLSFragmentsPerSegment,
                    ManifestName = uniqueness
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    string assetname = form.AssetName.Replace(Constants.NameconvProgram, form.ProgramName).Replace(Constants.NameconvChannel, form.ChannelName);
                    var newAsset = new Asset() { StorageAccountName = form.StorageSelected };

                    Task.Run(async () =>
                    {
                        try
                        {
                            TextBoxLogWriteLine("Asset creation...");
                            Asset asset = await _amsClientV3.AMSclient.Assets.CreateOrUpdateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, assetname, newAsset);
                            TextBoxLogWriteLine("Asset created.");

                            TextBoxLogWriteLine("Live output creation...");

                            Hls hlsParam = null;
                            if (form.HLSFragmentPerSegment != null)
                            {
                                hlsParam = new Hls(fragmentsPerTsSegment: form.HLSFragmentPerSegment);
                            }

                            LiveOutput liveOutput = new LiveOutput(
                                asset.Name,
                                form.archiveWindowLength,
                                null,
                                form.ProgramName,
                                null,
                                form.ProgramDescription,
                                form.ManifestName ?? uniqueness,
                                hlsParam,
                                form.StartRecordTimestamp
                                );

                            var liveOutput2 = await _amsClientV3.AMSclient.LiveOutputs.CreateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, liveEvent.Name, form.ProgramName, liveOutput);
                            TextBoxLogWriteLine("Live output created.");

                            if (form.CreateLocator)
                            {
                                DoCreateLocator(new List<Asset> { asset });
                            };
                        }
                        catch (Exception ex)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when creating a live output", true);
                            TextBoxLogWriteLine(ex);
                        }
                        DoRefreshGridLiveOutputV(false);
                    }
                    );
                }
            }


            /*





                CreateProgram form = new CreateProgram(_context)
                {
                    ChannelName = channel.Name,
                    archiveWindowLength = new TimeSpan(4, 0, 0),
                    CreateLocator = true,
                    EnableDynEnc = false,
                    StartProgram = false,
                    ProposeStartProgram = (channel.ResourceState == LiveEventResourceState.Running),
                    AssetName = Constants.NameconvChannel + "-" + Constants.NameconvProgram,
                    ProposeScaleUnit = _context.StreamingEndpoints.AsEnumerable().All(o => StreamingEndpointInformation.ReturnTypeSE(o) == StreamingEndpointInformation.StreamEndpointType.Classic)
                };
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.ScaleUnit)
                    {
                        Task.Run(async () =>
                        {
                            await ScaleStreamingEndpoint(_context.StreamingEndpoints.FirstOrDefault(), 1);
                        });
                    }

                    TextBoxLogWriteLine("Creating Program '{0}'...", form.ProgramName);
                    string assetname = form.AssetName.Replace(Constants.NameconvProgram, form.ProgramName).Replace(Constants.NameconvChannel, form.ChannelName);
                    IAsset NewAsset;
                    if (form.IsReplica) // special case. We want to create a program with a specific manifest name, locator GUID and encryption key
                    {
                        NewAsset = CreateLiveAssetWithOptionalpecifiedLocatorID(assetname, form.StorageSelected, true, form.EnableDynEnc, form.ReplicaLocatorID);
                    }
                    else // normal case
                    {
                        NewAsset = CreateLiveAssetWithOptionalpecifiedLocatorID(assetname, form.StorageSelected, form.CreateLocator, form.EnableDynEnc);
                    }

                    if (NewAsset != null)
                    {
                        var options = new ProgramCreationOptions()
                        {
                            Name = form.ProgramName,
                            Description = form.ProgramDescription,
                            ArchiveWindowLength = form.archiveWindowLength,
                            AssetId = NewAsset.Id,
                            ManifestName = form.ForceManifestName // if replica is selected or force manifest name is pecified, then we force the manifest name
                        };

                        var STask = ProgramExecuteAsync(
                               () =>
                                   channel.Programs.CreateAsync(options),
                                  form.ProgramName,
                                  "created");
                        await STask;

                        DoRefreshGridProgramV(false);

                        if (form.StartProgram)
                        {
                            Task.Run(async () =>
                            {
                                // let's start the program now
                                IProgram program = _context.Programs.Where(p => p.Name == form.ProgramName && p.ChannelId == channel.Id).FirstOrDefault();
                                await StartProgramASync(program);
                            }
                            );
                        }
                    }
                    DoRefreshGridAssetV(false);
                }
            }
            else
            {
                MessageBox.Show("No channel has been selected.");
            }
            */
        }


        private void createProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateLiveOutput();
        }


        private void dataGridViewProgramV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cellprogramstatevalue = dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["State"].Index].Value;

            if (cellprogramstatevalue != null)
            {
                LiveOutputResourceState PS = (LiveOutputResourceState)cellprogramstatevalue;
                Color mycolor;

                switch (PS)
                {
                    case LiveOutputResourceState.Deleting:
                        mycolor = Color.OrangeRed;
                        break;
                    case LiveOutputResourceState.Creating:
                        mycolor = Color.DarkCyan;
                        break;
                    case LiveOutputResourceState.Running:
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

        private async void DoDisplayLiveOutputInfo(List<LiveOutput> liveoutputs)
        {
            bool multiselection = liveoutputs.Count > 1;
            if (liveoutputs.FirstOrDefault() != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
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
                    this.Cursor = Cursors.Arrow;
                }
            }
        }


        private void DoGenerateThumbnails()
        {
            List<IAsset> SelectedAssets = new List<IAsset>(); //ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            CheckAssetSizeRegardingMediaUnit(SelectedAssets);

            string taskname = "Media Encoder Standard Thumbnails generation from " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;

            var processor = GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);

            EncodingMES form = new EncodingMES(_context, new List<IAsset>(), processor.Version, ThumbnailsModeOnly: true, main: this)
            {
                EncodingLabel = (SelectedAssets.Count > 1) ?
                string.Format("{0} asset{1} selected. You are going to submit {0} job{1} with 1 task.", SelectedAssets.Count, Program.ReturnS(SelectedAssets.Count), SelectedAssets.Count)
                :
                "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded (1 job with 1 task).",

                EncodingJobName = "Thumbnails generation (MES) of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + " - Media Standard encoded",
                EncodingAMEStdPresetJSONFilesUserFolder = Properties.Settings.Default.MESPresetFilesCurrentFolder,
                EncodingAMEStdPresetJSONFilesFolder = Application.StartupPath + Constants.PathMESFiles,
                SelectedAssets = SelectedAssets
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (IAsset asset in form.SelectedAssets)
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name);
                    IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);
                    string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvEncodername, processor.Name + " v" + processor.Version);
                    ITask AMEStandardTask = job.Tasks.AddNew(
                        tasknameloc,
                        processor,
                       form.EncodingConfiguration,
                       form.JobOptions.TasksOptionsSetting
                      );

                    AMEStandardTask.InputAssets.Add(asset);

                    // Add an output asset to contain the results of the job. 
                    string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name);
                    AMEStandardTask.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.StorageSelected, form.JobOptions.OutputAssetsCreationOptions, form.JobOptions.OutputAssetsFormatOption);

                    // Submit the job  
                    TextBoxLogWriteLine("Submitting job '{0}'", jobnameloc);
                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        if (SelectedAssets.Count < 5)
                        {
                            MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobnameloc) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        TextBoxLogWriteLine("There has been a problem when submitting the job '{0}' ", jobnameloc, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }
                    Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(new JobExtension()));
                }
                DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs);
                DoRefreshGridJobV(false);
            }
        }

        private void dataGridViewOriginsV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            var cellSEstatevalue = dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[dataGridViewStreamingEndpointsV.Columns["State"].Index].Value;

            if (cellSEstatevalue != null)
            {
                StreamingEndpointState SES = (StreamingEndpointState)cellSEstatevalue;
                Color mycolor;

                switch (SES)
                {
                    case StreamingEndpointState.Deleting:
                        mycolor = Color.Red;
                        break;
                    case StreamingEndpointState.Stopping:
                        mycolor = Color.OrangeRed;
                        break;
                    case StreamingEndpointState.Starting:
                        mycolor = Color.DarkCyan;
                        break;
                    case StreamingEndpointState.Stopped:
                        mycolor = Color.Red;
                        break;
                    case StreamingEndpointState.Running:
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
        private async void DoDisplayStreamingEndpointInfo(List<StreamingEndpoint> streamingendpoints)
        {
            bool multiselection = streamingendpoints.Count > 1;

            StreamingEndpointInformation form = new StreamingEndpointInformation(streamingendpoints.FirstOrDefault())
            {
                MultipleSelection = multiselection
            };


            if (form.ShowDialog() == DialogResult.OK)
            {
                var modifications = form.Modifications;
                if (multiselection)
                {
                    var formSettings = new SettingsSelection("streaming endpoints", modifications);

                    if (formSettings.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        modifications = (ExplorerSEModifications)formSettings.SettingsObject;
                    }
                }

                foreach (var streamingendpoint in streamingendpoints)
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
                        string newmode = null;

                        if (form.GetScaleUnits == 0)
                        {
                            newmode = "Standard";
                        }
                        else if (streamingendpoint.ScaleUnits == 0 && form.GetScaleUnits > 0)
                        {
                            newmode = "Premium";
                        }

                        Task.Run(async () =>
                        {
                            DoUpdateAndScaleStreamingEndpointEngine(streamingendpoint, form.GetScaleUnits);
                        });


                    }
                    else // no scaling
                    {
                        Task.Run(async () =>
                        {
                            DoUpdateAndScaleStreamingEndpointEngine(streamingendpoint);
                        });
                    }
                }
            }
        }


        private void DoStartStreamingEndpoints()
        {
            Task.Run(async () =>
            {
                DoStartStreamingEndpointEngine(ReturnSelectedStreamingEndpoints());
            }
                   );
        }

        private void DoStopStreamingEndpoints()
        {
            Task.Run(async () =>
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
                    Task.Run(async () =>
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

        private async void DoCreateStreamingEndpoint()
        {
            var form = new CreateStreamingEndpoint();
            var cdnform = new StreamingEndpointCDNEnable();

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


                var newStreamingEndpoint = new StreamingEndpoint(name: form.StreamingEndpointName,
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
                        var secreated = _amsClientV3.AMSclient.StreamingEndpoints.Create(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, form.StreamingEndpointName, newStreamingEndpoint);
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


        private void displayChannelInfomationToolStripMenuItem_Click(object sender, EventArgs e)
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
                var channel = GetLiveEvent(dataGridViewLiveEventsV.Rows[e.RowIndex].Cells[dataGridViewLiveEventsV.Columns["Name"].Index].Value.ToString());
                if (channel != null)
                {
                    DoDisplayLiveEventInfo((new List<LiveEvent>() { channel }));
                }
            }
        }

        private void dataGridViewProgramV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _amsClientV3.RefreshTokenIfNeeded();

                var liveoutput = _amsClientV3.AMSclient.LiveOutputs.Get(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["LiveEventName"].Index].Value.ToString(), dataGridViewLiveOutputV.Rows[e.RowIndex].Cells[dataGridViewLiveOutputV.Columns["Name"].Index].Value.ToString());
                if (liveoutput != null)
                {
                    DoDisplayLiveOutputInfo(new List<LiveOutput>() { liveoutput });
                }
            }
        }

        private void startChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartLiveEvents();
        }

        private void stopChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopOrDeleteLiveEvents(false);
        }

        private void resetChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoResetLiveEvents();
        }

        private void deleteChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopOrDeleteLiveEvents(true);
        }


        private void deleteProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteLiveOutputs();
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
                StreamingEndpoint se = GetStreamingEndpoint(dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[dataGridViewStreamingEndpointsV.Columns["Name"].Index].Value.ToString());
                if (se != null)
                {
                    DoDisplayStreamingEndpointInfo(new List<StreamingEndpoint>() { se });
                }
            }
        }

        private void DoPlaybackChannelPreview(PlayerType ptype)
        {
            foreach (var liveEvent in ReturnSelectedLiveEvents())
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
            var channel = ReturnSelectedLiveEvents().FirstOrDefault();
            if (channel != null && channel.Preview != null)
            {
                if (channel.Preview.Endpoints.FirstOrDefault() != null && channel.Preview.Endpoints.FirstOrDefault().Url != null)
                {
                    string preview = channel.Preview.Endpoints.FirstOrDefault().Url;
                    EditorXMLJSON DisplayForm = new EditorXMLJSON("Preview URL", preview, false, false, false);
                    DisplayForm.Display();
                }
                else
                {
                    MessageBox.Show($"There is no active preview URL for live event '{channel.Name}'. Maybe no data has arrived so no manifest is available.", "No preview URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void batchUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoBatchUpload();
        }

        private async void DoBatchUpload()
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
                            var response = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(folder)), TransferType.UploadFromFolder, true);
                            //var myTask = Task.Factory.StartNew(() => ProcessUploadFromFolder(folder, response.Id, AssetCreationOptions.None, response.token, form2.StorageSelected), response.token);


                            var filePaths = Directory.EnumerateFiles(folder as string);

                            var myTask = Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                                      filePaths.ToList(),
                                      response.Id,
                                      response.token,
                                      storageaccount: form2.StorageSelected
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
                            var response = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, true);

                            var myTask = Task.Factory.StartNew(() => ProcessUploadFileAndMoreV3(
                                      new List<string>() { file },
                                      response.Id,
                                      response.token,
                                      storageaccount: form2.StorageSelected
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

        private void createChannelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateLiveEvent();
        }

        private void comboBoxTimeProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewLiveOutputV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewLiveOutputV.TimeFilter == FilterTime.TimeRange)
            {
                var form = new TimeRangeSelection()
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


        private void DoSetupDynEnc()
        {
            List<IAsset> SelectedAssets = new List<IAsset>(); //ReturnSelectedAssetsFromProgramsOrAssets();
            SetupDynamicEncryption(SelectedAssets, false);
        }

        private bool SetupDynamicEncryption(List<IAsset> SelectedAssets, bool forceusertoprovidekey)
        {
            if (SelectedAssets.Count == 0) return false;

            string labelAssetName;
            bool oktoproceed = false;

            // check if assets are published
            var publishedAssets = SelectedAssets.Where(a => a.Locators.Count > 0).ToList();
            if (publishedAssets.Count > 0)
            {
                if (MessageBox.Show("Some selected asset(s) are published.\nYou need to unpublish them before doing any dynamic encryption change.\n\nOk to unpublish (delete locators) ?", "Published assets", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //DoDeleteAllLocatorsOnAssets(publishedAssets, true);
                }
                else
                {
                    return false;
                }
            }

            labelAssetName = "Dynamic encryption will be applied for Asset '" + SelectedAssets.FirstOrDefault().Name + "'.";
            if (SelectedAssets.Count > 1)
            {
                labelAssetName = "Dynamic encryption will applied to the " + SelectedAssets.Count.ToString() + " selected assets.";
            }
            AddDynamicEncryptionFrame1 form1 = new AddDynamicEncryptionFrame1(_context);

            if (form1.ShowDialog() == DialogResult.OK)
            {

                switch (form1.GetDeliveryPolicyType)
                {
                    ///////////////////////////////////////////// CENC Dynamic Encryption
                    case AssetDeliveryPolicyType.DynamicCommonEncryption:
                    case AssetDeliveryPolicyType.None: // in that case, user want to configure license delivery on an asset already encrypted

                        AddDynamicEncryptionFrame2_CENCKeyConfig form2_CENC = new AddDynamicEncryptionFrame2_CENCKeyConfig(

                            forceusertoprovidekey)
                        { Left = form1.Left, Top = form1.Top };
                        if (form2_CENC.ShowDialog() == DialogResult.OK)
                        {
                            var form3_CENC = new AddDynamicEncryptionFrame3_CENCDelivery(_context, form1.PlayReadyPackaging, form1.WidevinePackaging);
                            AddDynamicEncryptionFrame3_ExistingPolicies form3_ExistingPolicies = new AddDynamicEncryptionFrame3_ExistingPolicies(_context, form1);

                            if ((form1.SelectExistingPolicies && form3_ExistingPolicies.ShowDialog() == DialogResult.OK) || (!form1.SelectExistingPolicies && form3_CENC.ShowDialog() == DialogResult.OK))
                            {

                                bool NeedToDisplayPlayReadyLicense = form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady > 0;
                                bool NeedToDisplayWidevineLicense = form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine > 0;

                                List<AddDynamicEncryptionFrame4> form4list = new List<AddDynamicEncryptionFrame4>();
                                List<AddDynamicEncryptionFrame5_PlayReadyLicense> form5list = new List<AddDynamicEncryptionFrame5_PlayReadyLicense>();
                                List<AddDynamicEncryptionFrame6_WidevineLicense> form6list = new List<AddDynamicEncryptionFrame6_WidevineLicense>();

                                bool usercancelledform4or5 = false;
                                bool usercancelledform4or6 = false;

                                if (!form1.SelectExistingPolicies) // user did not select an existing authorization policy
                                {
                                    int step = 3;
                                    string tokensymmetrickey = null;
                                    for (int i = 0; i < form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady; i++)
                                    {
                                        AddDynamicEncryptionFrame4 form4 = new AddDynamicEncryptionFrame4(_context, step, i + 1, "PlayReady", tokensymmetrickey, false) { Left = form2_CENC.Left, Top = form2_CENC.Top };

                                        if (form4.ShowDialog() == DialogResult.OK)
                                        {
                                            step++;
                                            form4list.Add(form4);
                                            tokensymmetrickey = form4.SymmetricKey;
                                            AddDynamicEncryptionFrame5_PlayReadyLicense form5_PlayReadyLicense = new AddDynamicEncryptionFrame5_PlayReadyLicense(step, i + 1, i == (form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady - 1)) { Left = form3_CENC.Left, Top = form3_CENC.Top };
                                            step++;
                                            if (NeedToDisplayPlayReadyLicense) // it's a PlayReady license and user wants to deliver the license from Azure Media Services
                                            {
                                                string tokentype = form4.GetKeyRestrictionType == ContentKeyRestrictionType.TokenRestricted ? " " + form4.GetDetailedTokenType.ToString() : "";
                                                form5_PlayReadyLicense.PlayReadOptionName = string.Format("{0}{1} PlayReady Option {2}", form4.GetKeyRestrictionType.ToString(), tokentype, i + 1);
                                                if (form5_PlayReadyLicense.ShowDialog() == DialogResult.OK) // let's display the dialog box to configure the playready license
                                                {
                                                    form5list.Add(form5_PlayReadyLicense);
                                                }
                                                else
                                                {
                                                    usercancelledform4or5 = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            usercancelledform4or5 = true;
                                        }
                                    }

                                    // widevine
                                    for (int i = 0; i < form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine; i++)
                                    {
                                        AddDynamicEncryptionFrame4 form4 = new AddDynamicEncryptionFrame4(_context, step, i + 1, "Widevine", tokensymmetrickey, false) { Left = form2_CENC.Left, Top = form2_CENC.Top };

                                        if (form4.ShowDialog() == DialogResult.OK)
                                        {
                                            step++;
                                            form4list.Add(form4);
                                            tokensymmetrickey = form4.SymmetricKey;
                                            AddDynamicEncryptionFrame6_WidevineLicense form6_WidevineLicense = new AddDynamicEncryptionFrame6_WidevineLicense(Constants.TemporaryWidevineLicenseServer, step, i + 1, i == (form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine - 1)) { Left = form3_CENC.Left, Top = form3_CENC.Top };
                                            string tokentype = form4.GetKeyRestrictionType == ContentKeyRestrictionType.TokenRestricted ? " " + form4.GetDetailedTokenType.ToString() : "";
                                            form6_WidevineLicense.WidevinePolicyName = string.Format("{0}{1} Widevine Option {2}", form4.GetKeyRestrictionType.ToString(), tokentype, i + 1);

                                            step++;

                                            if (form6_WidevineLicense.ShowDialog() == DialogResult.OK) // let's display the dialog box to configure the playready license
                                            {
                                                form6list.Add(form6_WidevineLicense);
                                            }
                                            else
                                            {
                                                usercancelledform4or6 = true;
                                            }
                                        }
                                        else
                                        {
                                            usercancelledform4or6 = true;
                                        }
                                    }
                                }

                                if (!usercancelledform4or5 && !usercancelledform4or6)
                                {
                                    DoDynamicEncryptionAndKeyDeliveryWithCENC(SelectedAssets, form1, form2_CENC, form3_ExistingPolicies, form3_CENC, form4list, form5list, form6list, true);
                                    oktoproceed = true;
                                    dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                                    dataGridViewAssetsV.AnalyzeItemsInBackground();
                                }
                            }
                        }
                        break;

                    ///////////////////////////////////////////// CENC CBCS (FairPlay) Dynamic Encryption
                    case AssetDeliveryPolicyType.DynamicCommonEncryptionCbcs:

                        var form2_CENC_Cbcs = new AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig()
                        { Left = form1.Left, Top = form1.Top };
                        if (form2_CENC_Cbcs.ShowDialog() == DialogResult.OK)
                        {
                            var form3_CENC = new AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery(_context);
                            AddDynamicEncryptionFrame3_ExistingPolicies form3_ExistingPolicies = new AddDynamicEncryptionFrame3_ExistingPolicies(_context, form1);

                            if ((form1.SelectExistingPolicies && form3_ExistingPolicies.ShowDialog() == DialogResult.OK) || (!form1.SelectExistingPolicies && form3_CENC.ShowDialog() == DialogResult.OK))
                            {
                                bool NeedToDisplayFairPlayLicense = form3_CENC.GetNumberOfAuthorizationPolicyOptionsFairPlay > 0;

                                List<AddDynamicEncryptionFrame4> form4list = new List<AddDynamicEncryptionFrame4>();
                                List<AddDynamicEncryptionFrame5_FairplayLicense> form5list = new List<AddDynamicEncryptionFrame5_FairplayLicense>();


                                bool usercancelledform4or5 = false;

                                if (!form1.SelectExistingPolicies) // user did not select an existing authorization policy
                                {
                                    int step = 3;
                                    string tokensymmetrickey = null;
                                    for (int i = 0; i < form3_CENC.GetNumberOfAuthorizationPolicyOptionsFairPlay; i++)
                                    {
                                        AddDynamicEncryptionFrame4 form4 = new AddDynamicEncryptionFrame4(_context, step, i + 1, "FairPlay", tokensymmetrickey, false) { Left = form2_CENC_Cbcs.Left, Top = form2_CENC_Cbcs.Top };

                                        if (form4.ShowDialog() == DialogResult.OK)
                                        {
                                            step++;
                                            form4list.Add(form4);
                                            tokensymmetrickey = form4.SymmetricKey;
                                            AddDynamicEncryptionFrame5_FairplayLicense form5_FairPlayLicense = new AddDynamicEncryptionFrame5_FairplayLicense(step, i + 1, i == (form3_CENC.GetNumberOfAuthorizationPolicyOptionsFairPlay - 1)) { Left = form3_CENC.Left, Top = form3_CENC.Top };
                                            string tokentype = form4.GetKeyRestrictionType == ContentKeyRestrictionType.TokenRestricted ? " " + form4.GetDetailedTokenType.ToString() : "";

                                            step++;

                                            if (form5_FairPlayLicense.ShowDialog() == DialogResult.OK) // let's display the dialog box to configure the playready license
                                            {
                                                form5list.Add(form5_FairPlayLicense);
                                            }
                                            else
                                            {
                                                usercancelledform4or5 = true;
                                            }
                                        }
                                        else
                                        {
                                            usercancelledform4or5 = true;
                                        }
                                    }
                                }

                                if (!usercancelledform4or5)
                                {
                                    DoDynamicEncryptionAndKeyDeliveryWithCENCCbcs(SelectedAssets, form1, form2_CENC_Cbcs, form3_ExistingPolicies, form3_CENC, form4list, form5list, true);
                                    oktoproceed = true;
                                    dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                                    dataGridViewAssetsV.AnalyzeItemsInBackground();
                                }
                            }
                        }
                        break;

                    ///////////////////////////////////////////// AES Dynamic Encryption
                    case AssetDeliveryPolicyType.DynamicEnvelopeEncryption:
                        AddDynamicEncryptionFrame2_AESKeyConfig form2_AES =
                            new AddDynamicEncryptionFrame2_AESKeyConfig(forceusertoprovidekey) { Left = form1.Left, Top = form1.Top };
                        if (form2_AES.ShowDialog() == DialogResult.OK)
                        {
                            var form3_AES = new AddDynamicEncryptionFrame3_AESDelivery(_context);
                            AddDynamicEncryptionFrame3_ExistingPolicies form3_ExistingPolicies = new AddDynamicEncryptionFrame3_ExistingPolicies(_context, form1);

                            if ((form1.SelectExistingPolicies && form3_ExistingPolicies.ShowDialog() == DialogResult.OK) || (!form1.SelectExistingPolicies && form3_AES.ShowDialog() == DialogResult.OK))
                            {
                                List<AddDynamicEncryptionFrame4> form4list = new List<AddDynamicEncryptionFrame4>();
                                bool usercancelledform4 = false;

                                if (!form1.SelectExistingPolicies) // user did not select an existing authorization policy
                                {
                                    string tokensymmetrickey = null;
                                    for (int i = 0; i < form3_AES.GetNumberOfAuthorizationPolicyOptions; i++)
                                    {
                                        AddDynamicEncryptionFrame4 form4 = new AddDynamicEncryptionFrame4(_context, i + 3, i + 1, "AES", tokensymmetrickey, true) { Left = form2_AES.Left, Top = form2_AES.Top };
                                        if (form4.ShowDialog() == DialogResult.OK)
                                        {
                                            form4list.Add(form4);
                                            tokensymmetrickey = form4.SymmetricKey;
                                        }
                                        else
                                        {
                                            usercancelledform4 = true;
                                        }
                                    }
                                }

                                if (!usercancelledform4)
                                {
                                    DoDynamicEncryptionWithAES(SelectedAssets, form1, form2_AES, form3_ExistingPolicies, form3_AES, form4list, true);
                                    oktoproceed = true;
                                    dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                                    dataGridViewAssetsV.AnalyzeItemsInBackground();
                                }
                            }
                        }
                        break;

                    ///////////////////////////////////////////// Decrypt storage protected content
                    case AssetDeliveryPolicyType.NoDynamicEncryption:
                        AddDynDecryption(SelectedAssets, form1, _context);
                        oktoproceed = true;
                        dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                        break;

                    default:
                        break;

                }
            }

            return oktoproceed;
        }



        private void DoDynamicEncryptionAndKeyDeliveryWithCENC(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, AddDynamicEncryptionFrame2_CENCKeyConfig form2_CENC, AddDynamicEncryptionFrame3_ExistingPolicies form3_ExistingPolicies, AddDynamicEncryptionFrame3_CENCDelivery form3_CENC, List<AddDynamicEncryptionFrame4> form4list, List<AddDynamicEncryptionFrame5_PlayReadyLicense> form5PlayReadyLicenseList, List<AddDynamicEncryptionFrame6_WidevineLicense> form6WidevineLicenseList, bool DisplayUI)
        {
            bool ErrorCreationKey = false;
            bool reusekey = false;
            bool firstkeycreation = true;
            IContentKey formerkey = null;
            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = form3_ExistingPolicies.UseExistingAuthorizationPolicy;
            IAssetDeliveryPolicy DelPol = form3_ExistingPolicies.UseExistingDeliveryPolicy;


            bool ManualForceKeyData = !form2_CENC.ContentKeyRandomGeneration && (form2_CENC.KeyId != null);  // user want to manually enter the cryptography data and key if provided

            if (ManualForceKeyData)  // user want to manually enter the cryptography data and key if provided
            {
                // if the key already exists in the account (same key id), let's 
                formerkey = SelectedAssets.FirstOrDefault().GetMediaContext().ContentKeys.Where(c => c.Id == Constants.ContentKeyIdPrefix + form2_CENC.KeyId.ToString()).FirstOrDefault();
                if (formerkey != null)
                {
                    if (DisplayUI && MessageBox.Show("A Content key with the same Key Id exists already in the account.\nDo you want to try to replace it?\n(If not, the existing key will be used)", "Content key Id", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // user wants to replace the key
                        try
                        {
                            formerkey.Delete();
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when deleting the content key {0}.", formerkey.Id, true);
                            TextBoxLogWriteLine(e);
                            TextBoxLogWriteLine("The former key will be reused.", true);
                            reusekey = true;
                        }
                    }
                    else
                    {
                        reusekey = true;
                    }
                }
            }


            foreach (IAsset AssetToProcess in SelectedAssets)
            {
                if (AssetToProcess != null)
                {
                    IContentKey contentKey = null;
                    var contentkeys = AssetToProcess.ContentKeys.Where(c => c.ContentKeyType == form1.GetContentKeyType);
                    // special case, no dynamic encryption, goal is to setup key auth policy. CENC key is selected

                    if (contentkeys.Count() == 0) // no content key existing so we need to create one
                    {
                        ErrorCreationKey = false;

                        //    if ((form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady + form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine) > 0 && form2_CENC.ContentKeyRandomGeneration)
                        //// Azure will deliver the PR or WV license and user wants to auto generate the key, so we can create a key with a random content key

                        if (!reusekey && ((form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady + form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine) > 0 && form2_CENC.ContentKeyRandomGeneration))

                        // Azure will deliver the PR or WV license or user wants to auto generate the key, so we can create a key with a random content key
                        // changed || form2_CENC.ContentKeyRandomGeneratio to && form2_CENC.ContentKeyRandomGeneratio
                        {
                            try
                            {
                                contentKey = DynamicEncryption.CreateCommonTypeContentKeyAndAttachAsset(AssetToProcess, _context);
                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when creating the content key for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                                ErrorCreationKey = true;
                            }
                            if (!ErrorCreationKey)
                            {
                                TextBoxLogWriteLine("Created key {0} for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                            }

                        }
                        else // user wants to deliver with an external PlayReady or Widevine server or want to provide the key, so let's create the key based on what the user input
                        {
                            // if the key does not exist in the account (same key id), let's create it
                            if ((firstkeycreation && !reusekey) || form2_CENC.KeyId == null) // if we need to generate a new key id for each asset
                            {
                                if (form2_CENC.KeySeed != null) // seed has been given
                                {
                                    Guid keyid = (form2_CENC.KeyId == null) ? Guid.NewGuid() : (Guid)form2_CENC.KeyId;
                                    byte[] bytecontentkey = CommonEncryption.GeneratePlayReadyContentKey(Convert.FromBase64String(form2_CENC.KeySeed), keyid);
                                    try
                                    {
                                        contentKey = DynamicEncryption.CreateCommonTypeContentKeyAndAttachAsset(AssetToProcess, _context, keyid, bytecontentkey);
                                    }
                                    catch (Exception e)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when creating the content key for '{0}'.", AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        ErrorCreationKey = true;
                                    }
                                    if (!ErrorCreationKey)
                                    {
                                        TextBoxLogWriteLine("Created key {0} for the asset {1} ", contentKey.Id, AssetToProcess.Name);
                                    }
                                }
                                else // no seed given, so content key has been setup or not (if external server)
                                {
                                    Guid keyid = (form2_CENC.KeyId == null) ? Guid.NewGuid() : (Guid)form2_CENC.KeyId;
                                    byte[] bytecontentkey = (string.IsNullOrWhiteSpace(form2_CENC.CENCContentKey)) ? DynamicEncryption.GetRandomBuffer(16) : Convert.FromBase64String(form2_CENC.CENCContentKey);

                                    try
                                    {
                                        contentKey = DynamicEncryption.CreateCommonTypeContentKeyAndAttachAsset(AssetToProcess, _context, keyid, bytecontentkey);
                                    }
                                    catch (Exception e)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when creating the content key for asset '{0}'.", AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        ErrorCreationKey = true;
                                    }
                                    if (!ErrorCreationKey)
                                    {
                                        TextBoxLogWriteLine("Created key {0} for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                                    }

                                }
                                formerkey = contentKey;
                                firstkeycreation = false;
                            }
                            else
                            {
                                contentKey = formerkey;
                                AssetToProcess.ContentKeys.Add(contentKey);
                                AssetToProcess.Update();
                                TextBoxLogWriteLine("Reusing key {0} for the asset {1} ", contentKey.Id, AssetToProcess.Name);
                            }
                        }
                    }

                    else // let's use existing content key
                    {
                        contentKey = contentkeys.FirstOrDefault();
                        TextBoxLogWriteLine("Existing key '{0}' will be used for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                    }
                    if (
                        (form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady + form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine) > 0 // PlayReady/Widevine license and delivery from Azure Media Services
                        &&
                        (!ManualForceKeyData || (ManualForceKeyData && contentKeyAuthorizationPolicy == null)) // If the user want to reuse the key, then no need to recreate the Aut Policy if already created
                        )
                    {

                        if (contentKeyAuthorizationPolicy != null) // authorization policy already created so we use it
                        {
                            try
                            {
                                // Associate the content key authorization policy with the content key.
                                contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                                contentKey = contentKey.UpdateAsync().Result;
                                TextBoxLogWriteLine("Attached authorization policy to key '{0}' for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("There is a proble when attaching authorization policy to key '{0}' for asset '{1}'.", contentKey.Id, AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                        else if (!form1.SelectExistingPolicies) // authorization policy to create (policy==null and user did not select the option to choose an existing policy)
                        {
                            // let's create the Authorization Policy
                            contentKeyAuthorizationPolicy = _context.
                                       ContentKeyAuthorizationPolicies.
                                       CreateAsync("Authorization Policy").Result;

                            // Associate the content key authorization policy with the content key.
                            contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                            contentKey = contentKey.UpdateAsync().Result;

                            foreach (var form4 in form4list)
                            { // for each option

                                string PlayReadyLicenseDeliveryConfig = null;
                                string PlayReadyLicenseOptionName = null;
                                string WidevineLicenseDeliveryConfig = null;
                                string WidevineLicenseOptionName = null;
                                bool ItIsAPlayReadyOption = form4list.IndexOf(form4) < form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady;

                                if (ItIsAPlayReadyOption)
                                { // user wants to define a PlayReady license for this option
                                  // let's build the PlayReady license template

                                    ErrorCreationKey = false;
                                    try
                                    {
                                        PlayReadyLicenseDeliveryConfig = form5PlayReadyLicenseList[form4list.IndexOf(form4)].GetLicenseTemplate;
                                        PlayReadyLicenseOptionName = form5PlayReadyLicenseList[form4list.IndexOf(form4)].PlayReadOptionName;
                                    }
                                    catch (Exception e)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when configuring the PlayReady license template.", true);
                                        TextBoxLogWriteLine(e);
                                        ErrorCreationKey = true;
                                    }

                                }
                                else
                                { // user wants to define a Widevine license for this option

                                    WidevineLicenseDeliveryConfig =
                                        form6WidevineLicenseList[form4list.IndexOf(form4) - form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady]
                                        .GetWidevineConfiguration(contentKey.GetKeyDeliveryUrl(ContentKeyDeliveryType.Widevine).ToString());
                                    WidevineLicenseOptionName =
                                        form6WidevineLicenseList[form4list.IndexOf(form4) - form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady]
                                        .WidevinePolicyName;
                                }

                                if (!ErrorCreationKey)
                                {
                                    IContentKeyAuthorizationPolicyOption policyOption = null;
                                    try
                                    {
                                        switch (form4.GetKeyRestrictionType)
                                        {
                                            case ContentKeyRestrictionType.Open:
                                                if (ItIsAPlayReadyOption)
                                                {
                                                    policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption(PlayReadyLicenseOptionName, contentKey, ContentKeyDeliveryType.PlayReadyLicense, PlayReadyLicenseDeliveryConfig, _context);
                                                    TextBoxLogWriteLine("Created PlayReady Open authorization policy for the asset '{0}' ", AssetToProcess.Name);
                                                    contentKeyAuthorizationPolicy.Options.Add(policyOption);
                                                }
                                                else // widevine
                                                {
                                                    policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption(WidevineLicenseOptionName, contentKey, ContentKeyDeliveryType.Widevine, WidevineLicenseDeliveryConfig, _context);
                                                    TextBoxLogWriteLine("Created Widevine Open authorization policy for the asset '{0}' ", AssetToProcess.Name);
                                                    contentKeyAuthorizationPolicy.Options.Add(policyOption);
                                                }
                                                break;

                                            case ContentKeyRestrictionType.TokenRestricted:
                                                TokenVerificationKey mytokenverifkey = null;
                                                string OpenIdDoc = null;
                                                switch (form4.GetDetailedTokenType)
                                                {
                                                    case ExplorerTokenType.SWTSym:
                                                    case ExplorerTokenType.JWTSym:
                                                        mytokenverifkey = new SymmetricVerificationKey(Convert.FromBase64String(form4.SymmetricKey));
                                                        break;

                                                    case ExplorerTokenType.JWTOpenID:
                                                        OpenIdDoc = form4.GetOpenIdDiscoveryDocument;
                                                        break;

                                                    case ExplorerTokenType.JWTX509:
                                                        mytokenverifkey = new X509CertTokenVerificationKey(form4.GetX509Certificate);
                                                        break;
                                                }

                                                if (ItIsAPlayReadyOption)
                                                {
                                                    policyOption = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyCENC(PlayReadyLicenseOptionName, ContentKeyDeliveryType.PlayReadyLicense, contentKey, form4.GetAudience, form4.GetIssuer, form4.GetTokenRequiredClaims, form4.AddContentKeyIdentifierClaim, form4.GetTokenType, form4.GetDetailedTokenType, mytokenverifkey, _context, PlayReadyLicenseDeliveryConfig, OpenIdDoc);
                                                    TextBoxLogWriteLine("Created Token PlayReady authorization policy for the asset '{0}'.", AssetToProcess.Name);
                                                }
                                                else //widevine
                                                {
                                                    policyOption = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyCENC(WidevineLicenseOptionName, ContentKeyDeliveryType.Widevine, contentKey, form4.GetAudience, form4.GetIssuer, form4.GetTokenRequiredClaims, form4.AddContentKeyIdentifierClaim, form4.GetTokenType, form4.GetDetailedTokenType, mytokenverifkey, _context, WidevineLicenseDeliveryConfig, OpenIdDoc);
                                                    TextBoxLogWriteLine("Created Token Widevine authorization policy for the asset '{0}'", AssetToProcess.Name);
                                                }
                                                contentKeyAuthorizationPolicy.Options.Add(policyOption);


                                                if (form4.GetDetailedTokenType != ExplorerTokenType.JWTOpenID) // not possible to create a test token if OpenId is used
                                                {
                                                    // let display a test token
                                                    Microsoft.IdentityModel.Tokens.X509SigningCredentials signingcred = null;
                                                    if (form4.GetDetailedTokenType == ExplorerTokenType.JWTX509)
                                                    {
                                                        signingcred = new Microsoft.IdentityModel.Tokens.X509SigningCredentials(form4.GetX509Certificate);
                                                    }

                                                    _context = Program.ConnectAndGetNewContext(_credentials); // otherwise cache issues with multiple options
                                                    DynamicEncryption.TokenResult testToken = DynamicEncryption.GetTestToken(AssetToProcess, _context, form1.GetContentKeyType, signingcred, policyOption.Id);
                                                    TextBoxLogWriteLine("The authorization test token for option #{0} ({1} with Bearer) is:\n{2}", form4list.IndexOf(form4), form4.GetTokenType.ToString(), Constants.Bearer + testToken.TokenString);
                                                    System.Windows.Forms.Clipboard.SetText(Constants.Bearer + testToken.TokenString);
                                                }
                                                break;

                                            default:
                                                break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when creating the authorization policy for '{0}'.", AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        ErrorCreationKey = true;
                                    }
                                }
                            }
                            contentKeyAuthorizationPolicy.Update();
                        }

                    }


                    // Let's create the Asset Delivery Policy now
                    if (form1.GetDeliveryPolicyType != AssetDeliveryPolicyType.None && form1.EnableDynEnc)
                    {
                        if (DelPol != null) // already created
                        {
                            try
                            {
                                AssetToProcess.DeliveryPolicies.Add(DelPol);
                                TextBoxLogWriteLine("Attached asset delivery policy '{0}' to asset '{1}'.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("There is a problem when attaching the delivery policy for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                        else if (!form1.SelectExistingPolicies) // delivery policy to create (policy==null and user did not select the option to choose an existing policy)
                        {
                            var assetDeliveryProtocol = form1.GetAssetDeliveryProtocol;
                            if (!form1.PlayReadyPackaging && form1.WidevinePackaging)
                            {
                                assetDeliveryProtocol = AssetDeliveryProtocol.Dash;  // only DASH
                            }

                            string name = string.Format("AssetDeliveryPolicy {0} ({1})", form1.GetContentKeyType.ToString(), assetDeliveryProtocol.ToString());
                            ErrorCreationKey = false;

                            try
                            {
                                DelPol = DynamicEncryption.CreateAssetDeliveryPolicyCENC(
                                    AssetToProcess,
                                    contentKey,
                                    form1,
                                    name,
                                    _context,
                                    playreadyAcquisitionUrl: form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady > 0 ? null : form3_CENC.PlayReadyLAurl,
                                    playreadyEncodeLAURLForSilverlight: form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady > 0 ? false : form3_CENC.PlayReadyLAurlEncodeForSL,
                                    widevineAcquisitionUrl: form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine > 0 ? null : form3_CENC.WidevineLAurl,
                                    widevineAcquisitionURLFinal: form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine > 0 ? false : form3_CENC.WidevineFinalLAurl
                                    );

                                TextBoxLogWriteLine("Created asset delivery policy '{0}' for asset '{1}'.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("There is a problem when creating the delivery policy for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                                ErrorCreationKey = true;
                            }
                        }

                    }
                }
            }
        }

        private void DoDynamicEncryptionAndKeyDeliveryWithCENCCbcs(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, AddDynamicEncryptionFrame2_CENC_Cbcs_KeyConfig form2_CENC_cbcs, AddDynamicEncryptionFrame3_ExistingPolicies form3_ExistingPolicies, AddDynamicEncryptionFrame3_CENC_Cbcs_Delivery form3_CENC, List<AddDynamicEncryptionFrame4> form4list, List<AddDynamicEncryptionFrame5_FairplayLicense> form5list, bool DisplayUI)
        {
            bool ErrorCreationKey = false;
            IContentKey formerkey = null;
            IContentKey contentKey = null;
            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = form3_ExistingPolicies.UseExistingAuthorizationPolicy;
            IAssetDeliveryPolicy DelPol = form3_ExistingPolicies.UseExistingDeliveryPolicy;

            // if the key already exists in the account (same key id), let's
            formerkey = SelectedAssets.FirstOrDefault().GetMediaContext().ContentKeys.Where(c => c.Id == Constants.ContentKeyIdPrefix + form2_CENC_cbcs.KeyId.ToString()).FirstOrDefault();
            if (formerkey != null)
            {
                bool sametype = formerkey.ContentKeyType == ContentKeyType.CommonEncryptionCbcs;
                string message;
                if (!sametype)
                {
                    message = "A Content key with the same Key Id exists already in the account but it is not a FairPlay cbcs key.\nDo you want to try to replace it?\n(If not, the existing key will be used which is not going to work)";
                    TextBoxLogWriteLine("A Content key with the same Key Id exists already in the account but it is not a FairPlay cbcs key.");
                }
                else
                {
                    message = "A FairPlay cbcs content key with the same Key Id exists already in the account.\nDo you want to try to replace it?\n(If not, the existing key will be used)";
                    TextBoxLogWriteLine("A FairPlay cbcs content key with the same Key Id exists already in the account.");
                }

                if (DisplayUI && MessageBox.Show(message, "Content key Id", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // user wants to replace the key
                    TextBoxLogWriteLine("User decided to replace it.");

                    try
                    {
                        //formerkey.Delete();
                        DynamicEncryption.DeleteKeyAuthorizationPolicyAndFairplayAsk(_context, formerkey);
                        TextBoxLogWriteLine("Key has been deleted.");
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when deleting the content key {0}.", formerkey.Id, true);
                        TextBoxLogWriteLine(e);
                        TextBoxLogWriteLine("The former key will be reused.", true);
                        contentKey = formerkey;
                    }
                }
                else
                {
                    contentKey = formerkey;
                }
            }


            if (contentKey == null) // let's create the key one time
            {
                try
                {
                    contentKey = DynamicEncryption.CreateCommonTypeContentKey(_context, form2_CENC_cbcs.KeyId, form2_CENC_cbcs.FairPlayContentKey, ContentKeyType.CommonEncryptionCbcs);
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when creating the FairPlay cbcs content key", true);
                    TextBoxLogWriteLine(e);
                    ErrorCreationKey = true;
                }
                if (!ErrorCreationKey)
                {
                    TextBoxLogWriteLine("Created FairPlay cbcs key {0}.", contentKey.Id);
                }
            }


            //  IAssetDeliveryPolicy DelPol = null; // if not null, it means it has been created so we reuse for multiple assets


            foreach (IAsset AssetToProcess in SelectedAssets)
            {
                bool fairplayPersistent = false;

                if (AssetToProcess != null)
                {
                    IContentKey currentAssetKey = null;

                    var contentkeys = AssetToProcess.ContentKeys.Where(c => c.ContentKeyType == form1.GetContentKeyType);
                    // special case, no dynamic encryption, goal is to setup key auth policy. CENC key is selected

                    if (contentkeys.Count() == 0) // no content key existing so we can attach the CBC key
                    {
                        TextBoxLogWriteLine("Attaching FairPlay to asset {0}.", AssetToProcess.Name);
                        try
                        {
                            // Associate the key with the asset.
                            AssetToProcess.ContentKeys.Add(contentKey);
                            AssetToProcess.Update();
                            TextBoxLogWriteLine("Key attached.");
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when attaching the content key for '{0}'.", AssetToProcess.Name, true);
                            TextBoxLogWriteLine(e);
                            ErrorCreationKey = true;
                        }
                        currentAssetKey = contentKey;
                    }

                    else // asset has already a FairPlay cbcs attached - let's use it
                    {
                        currentAssetKey = contentkeys.FirstOrDefault();
                        TextBoxLogWriteLine("Existing FairPlay cbcs key '{0}' will be used for asset '{1}'. It is recommended to delete the key before to create a new authorization policy", currentAssetKey.Id, AssetToProcess.Name, true);
                    }



                    if (
                        form3_CENC.GetNumberOfAuthorizationPolicyOptionsFairPlay > 0 // FairPlay license and delivery from Azure Media Services
                        &&
                        (currentAssetKey.AuthorizationPolicyId == null  /*contentKeyAuthorizationPolicy == null*/) // If the user want to reuse the key, then no need to recreate the Aut Policy if already created
                        )
                    {
                        if (contentKeyAuthorizationPolicy != null) // authorization policy already created so we use it
                        {
                            try
                            {
                                // Associate the content key authorization policy with the content key.
                                currentAssetKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                                currentAssetKey = currentAssetKey.UpdateAsync().Result;
                                TextBoxLogWriteLine("Attached authorization policy to key '{0}' for asset '{1}'.", currentAssetKey.Id, AssetToProcess.Name);
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("There is a proble when attaching authorization policy to key '{0}' for asset '{1}'.", currentAssetKey.Id, AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                        else if (!form1.SelectExistingPolicies) // authorization policy to create (policy==null and user did not select the option to choose an existing policy)
                        {
                            // let's create the Authorization Policy
                            contentKeyAuthorizationPolicy = _context.
                                           ContentKeyAuthorizationPolicies.
                                           CreateAsync("Authorization Policy").Result;

                            // Associate the content key authorization policy with the content key.
                            currentAssetKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                            currentAssetKey = currentAssetKey.UpdateAsync().Result;


                            foreach (var form4 in form4list)
                            { // for each option

                                IContentKeyAuthorizationPolicyOption policyOption = null;

                                // Fairplay persistent or not
                                var isPersistent = form5list[form4list.IndexOf(form4)].EnablePersistent;
                                if (isPersistent)
                                {
                                    fairplayPersistent = true;
                                }
                                var rentalDuration = form5list[form4list.IndexOf(form4)].RentalDuration;


                                string FairPlayLicenseDeliveryConfig = DynamicEncryption.ConfigureFairPlayPolicyOptions(_context, form3_CENC.FairPlayASK, form3_CENC.FairPlayIV, form3_CENC.FairPlayCertificate, isPersistent, rentalDuration);

                                try
                                {
                                    string tokentype = form4.GetKeyRestrictionType == ContentKeyRestrictionType.TokenRestricted ? " " + form4.GetDetailedTokenType.ToString() : "";
                                    string FairPlayPolicyName = string.Format("{0}{1} FairPlay Option {2}", form4.GetKeyRestrictionType.ToString(), tokentype, form4list.IndexOf(form4) + 1);

                                    switch (form4.GetKeyRestrictionType)
                                    {
                                        case ContentKeyRestrictionType.Open:

                                            policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption(FairPlayPolicyName, currentAssetKey, ContentKeyDeliveryType.FairPlay, FairPlayLicenseDeliveryConfig, _context);
                                            TextBoxLogWriteLine("Created FairPlay Open authorization policy for the key '{0}' ", currentAssetKey.Id);
                                            contentKeyAuthorizationPolicy.Options.Add(policyOption);

                                            break;

                                        case ContentKeyRestrictionType.TokenRestricted:
                                            TokenVerificationKey mytokenverifkey = null;
                                            string OpenIdDoc = null;
                                            switch (form4.GetDetailedTokenType)
                                            {
                                                case ExplorerTokenType.SWTSym:
                                                case ExplorerTokenType.JWTSym:
                                                    mytokenverifkey = new SymmetricVerificationKey(Convert.FromBase64String(form4.SymmetricKey));
                                                    break;

                                                case ExplorerTokenType.JWTOpenID:
                                                    OpenIdDoc = form4.GetOpenIdDiscoveryDocument;
                                                    break;

                                                case ExplorerTokenType.JWTX509:
                                                    mytokenverifkey = new X509CertTokenVerificationKey(form4.GetX509Certificate);
                                                    break;
                                            }

                                            policyOption = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyCENC(FairPlayPolicyName, ContentKeyDeliveryType.FairPlay, currentAssetKey, form4.GetAudience, form4.GetIssuer, form4.GetTokenRequiredClaims, form4.AddContentKeyIdentifierClaim, form4.GetTokenType, form4.GetDetailedTokenType, mytokenverifkey, _context, FairPlayLicenseDeliveryConfig, OpenIdDoc);
                                            TextBoxLogWriteLine("Created Token FairPlay authorization policy for the key '{0}'.", currentAssetKey.Id);

                                            contentKeyAuthorizationPolicy.Options.Add(policyOption);

                                            if (form4.GetDetailedTokenType != ExplorerTokenType.JWTOpenID) // not possible to create a test token if OpenId is used
                                            {
                                                // let display a test token
                                                Microsoft.IdentityModel.Tokens.X509SigningCredentials signingcred = null;
                                                if (form4.GetDetailedTokenType == ExplorerTokenType.JWTX509)
                                                {
                                                    signingcred = new Microsoft.IdentityModel.Tokens.X509SigningCredentials(form4.GetX509Certificate);
                                                }

                                                _context = Program.ConnectAndGetNewContext(_credentials); // otherwise cache issues with multiple options
                                                DynamicEncryption.TokenResult testToken = DynamicEncryption.GetTestToken(AssetToProcess, _context, form1.GetContentKeyType, signingcred, policyOption.Id);
                                                TextBoxLogWriteLine("The authorization test token for option #{0} ({1} with Bearer) is:\n{2}", form4list.IndexOf(form4), form4.GetTokenType.ToString(), Constants.Bearer + testToken.TokenString);
                                                System.Windows.Forms.Clipboard.SetText(Constants.Bearer + testToken.TokenString);
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when creating the authorization policy for key '{0}'.", currentAssetKey.Id, true);
                                    TextBoxLogWriteLine(e);
                                    ErrorCreationKey = true;
                                }

                            }
                            contentKeyAuthorizationPolicy.Update();
                        }
                    }


                    // Let's create the Asset Delivery Policy now
                    if (form1.EnableDynEnc)
                    {
                        if (DelPol != null) // already created
                        {
                            try
                            {
                                AssetToProcess.DeliveryPolicies.Add(DelPol);
                                TextBoxLogWriteLine("Attached asset delivery policy '{0}' to asset '{1}'.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("There is a problem when attaching the delivery policy for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                        else if (!form1.SelectExistingPolicies) // delivery policy to create (policy==null and user did not select the option to choose an existing policy)
                        {
                            var assetDeliveryProtocol = form1.GetAssetDeliveryProtocol;

                            string name = string.Format("AssetDeliveryPolicy {0} ({1})", form1.GetContentKeyType.ToString(), assetDeliveryProtocol.ToString());
                            ErrorCreationKey = false;

                            string myIV = null;
                            if (form3_CENC.GetNumberOfAuthorizationPolicyOptionsFairPlay == 0 && form3_CENC.FairPlayIV != null)
                            {
                                myIV = DynamicEncryption.ByteArrayToHexString(form3_CENC.FairPlayIV);
                            }

                            try
                            {
                                DelPol = DynamicEncryption.CreateAssetDeliveryPolicyCENC(
                                    AssetToProcess,
                                   currentAssetKey,
                                    form1,
                                    name,
                                    _context,
                                    fairplayAcquisitionUrl: form3_CENC.GetNumberOfAuthorizationPolicyOptionsFairPlay > 0 ? null : form3_CENC.FairPlayLAurl,
                                    fairplayAcquisitionURLFinal: form3_CENC.FairPlayFinalLAurl,
                                    iv_if_externalserver: myIV,
                                    UseSKDForAMSLAURL: form3_CENC.AMSLAURLSchemeSKD,
                                    FairplayAllowPersistentLicense: fairplayPersistent
                                       );

                                TextBoxLogWriteLine("Created asset delivery policy '{0}' for asset '{1}'.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("There is a problem when creating the delivery policy for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                                ErrorCreationKey = true;
                            }
                        }
                    }
                }
            }
        }


        private void DoDynamicEncryptionWithAES(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, AddDynamicEncryptionFrame2_AESKeyConfig form2, AddDynamicEncryptionFrame3_ExistingPolicies form3_ExistingPolicies, AddDynamicEncryptionFrame3_AESDelivery form3_AES, List<AddDynamicEncryptionFrame4> form4list, bool DisplayUI)
        {
            bool ErrorCreationKey = false;
            string aeskey = string.Empty;
            bool firstkeycreation = true;
            Uri aeslaurl = form3_AES.AESLaUrl;
            bool aesFinalUrl = form3_AES.AESFinalLAurl;
            IContentKey formerkey = null;
            bool reusekey = false;

            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = form3_ExistingPolicies.UseExistingAuthorizationPolicy;
            IAssetDeliveryPolicy DelPol = form3_ExistingPolicies.UseExistingDeliveryPolicy;

            bool ManualForceKeyData = !form2.ContentKeyRandomGeneration;  // user want to manually enter the cryptography data

            if (ManualForceKeyData)  // user want to manually enter the cryptography data and key if provided
            {
                aeskey = form2.AESContentKey;
                //aeslaurl = form3_AES.AESLaUrl;
            }


            if (!form2.ContentKeyRandomGeneration && (form2.AESKeyId != null))  // user want to manually enter the cryptography data and key if providedd 
            {
                // if the key already exists in the account (same key id), let's 
                formerkey = SelectedAssets.FirstOrDefault().GetMediaContext().ContentKeys.Where(c => c.Id == Constants.ContentKeyIdPrefix + form2.AESKeyId.ToString()).FirstOrDefault();
                if (formerkey != null)
                {
                    if (DisplayUI && MessageBox.Show("A Content key with the same Key Id exists already in the account.\nDo you want to try to replace it?\n(If not, the existing key will be used)", "Content key Id", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // user wants to replace the key
                        try
                        {
                            formerkey.Delete();
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when deleting the content key {0}.", formerkey.Id, true);
                            TextBoxLogWriteLine(e);
                            TextBoxLogWriteLine("The former key will be reused.", true);
                            reusekey = true;
                        }
                    }
                    else
                    {
                        reusekey = true;
                    }
                }
            }



            foreach (IAsset AssetToProcess in SelectedAssets)
            {

                if (AssetToProcess != null)
                {
                    IContentKey contentKey = null;

                    var contentkeys = AssetToProcess.ContentKeys.Where(c => c.ContentKeyType == form1.GetContentKeyType);

                    if (contentkeys.Count() == 0) // no content key existing so we need to create one
                    {
                        ErrorCreationKey = false;


                        if (form3_AES.GetNumberOfAuthorizationPolicyOptions > 0 && (form2.ContentKeyRandomGeneration))
                        // Azure will deliver the license and user want to auto generate the key, so we can create a key with a random content key
                        {
                            try
                            {
                                contentKey = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess);
                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when creating the content key for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                                ErrorCreationKey = true;
                            }
                            if (!ErrorCreationKey)
                            {
                                TextBoxLogWriteLine("Created key {0} for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                            }
                        }
                        else // user wants to deliver with an external key server or want to provide some cryptography, so let's create the key based on what the user input
                        {
                            if ((firstkeycreation && !reusekey) || form2.AESKeyId == null) // if we need to generate a new key id for each asset
                            {
                                try
                                {
                                    if ((!form2.ContentKeyRandomGeneration) && !string.IsNullOrEmpty(aeskey)) // user provides custom crypto (key, or key id)
                                    {
                                        contentKey = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess, Convert.FromBase64String(aeskey), form2.AESKeyId);
                                    }
                                    else // content key if random. Perhaps key id has been provided
                                    {
                                        contentKey = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess, form2.AESKeyId);
                                    }
                                }
                                catch (Exception e)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when creating the content key for asset '{0}'.", AssetToProcess.Name, true);
                                    TextBoxLogWriteLine(e);
                                    ErrorCreationKey = true;
                                }
                                if (!ErrorCreationKey)
                                {
                                    TextBoxLogWriteLine("Created key {0} for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                                }

                                formerkey = contentKey;
                                firstkeycreation = false;
                            }
                            else
                            {
                                contentKey = formerkey;
                                AssetToProcess.ContentKeys.Add(contentKey);
                                AssetToProcess.Update();
                                TextBoxLogWriteLine("Reusing key {0} for the asset {1} ", contentKey.Id, AssetToProcess.Name);
                            }
                        }

                    }
                    else if (form3_AES.GetNumberOfAuthorizationPolicyOptions == 0)  // user wants to deliver with an external key server but the key exists already !
                    {
                        TextBoxLogWriteLine("Warning for asset '{0}'. A AES key already exists. You need to make sure that your external key server can deliver the key for this asset.", AssetToProcess.Name, true);
                    }

                    else // let's use existing content key
                    {
                        contentKey = contentkeys.FirstOrDefault();
                        TextBoxLogWriteLine("Existing key {0} will be used for asset {1}.", contentKey.Id, AssetToProcess.Name);
                    }


                    if (
                        form3_AES.GetNumberOfAuthorizationPolicyOptions > 0
                        &&
                        (!ManualForceKeyData || (ManualForceKeyData && contentKeyAuthorizationPolicy == null)) // If the user want to reuse the key, then no need to recreate the Aut Policy if already created
                        ) // AES Key and delivery from Azure Media Services
                    {

                        if (contentKeyAuthorizationPolicy != null) // authorization policy already created so we use it
                        {
                            try
                            {
                                // Associate the content key authorization policy with the content key.
                                contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                                contentKey = contentKey.UpdateAsync().Result;
                                TextBoxLogWriteLine("Attached authorization policy to key '{0}' for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("There is a proble when attaching authorization policy to key '{0}' for asset '{1}'.", contentKey.Id, AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                        else if (!form1.SelectExistingPolicies) // authorization policy to create (policy==null and user did not select the option to choose an existing policy)
                        {
                            // let's create the Authorization Policy
                            contentKeyAuthorizationPolicy = _context.
                                       ContentKeyAuthorizationPolicies.
                                       CreateAsync("Authorization Policy").Result;

                            // Associate the content key authorization policy with the content key.
                            contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                            contentKey = contentKey.UpdateAsync().Result;


                            foreach (var form3 in form4list)
                            {
                                IContentKeyAuthorizationPolicyOption policyOption = null;
                                ErrorCreationKey = false;
                                try
                                {
                                    switch (form3.GetKeyRestrictionType)
                                    {
                                        case ContentKeyRestrictionType.Open:

                                            policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption("Open Mode AES", contentKey, ContentKeyDeliveryType.BaselineHttp, null, _context);
                                            TextBoxLogWriteLine("Created Open authorization policy for the asset {0} ", contentKey.Id, AssetToProcess.Name);
                                            contentKeyAuthorizationPolicy.Options.Add(policyOption);
                                            break;

                                        case ContentKeyRestrictionType.TokenRestricted:
                                            TokenVerificationKey mytokenverifkey = null;
                                            string OpenIdDoc = null;
                                            switch (form3.GetDetailedTokenType)
                                            {
                                                case ExplorerTokenType.SWTSym:
                                                case ExplorerTokenType.JWTSym:
                                                    mytokenverifkey = new SymmetricVerificationKey(Convert.FromBase64String(form3.SymmetricKey));
                                                    break;

                                                case ExplorerTokenType.JWTOpenID:
                                                    OpenIdDoc = form3.GetOpenIdDiscoveryDocument;
                                                    break;

                                                case ExplorerTokenType.JWTX509:
                                                    mytokenverifkey = new X509CertTokenVerificationKey(form3.GetX509Certificate);
                                                    break;
                                            }

                                            policyOption = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyAES(contentKey, form3.GetAudience, form3.GetIssuer, form3.GetTokenRequiredClaims, form3.AddContentKeyIdentifierClaim, form3.GetTokenType, form3.GetDetailedTokenType, mytokenverifkey, _context, OpenIdDoc);
                                            TextBoxLogWriteLine("Created Token AES authorization policy for the asset {0} ", contentKey.Id, AssetToProcess.Name);
                                            contentKeyAuthorizationPolicy.Options.Add(policyOption);

                                            if (form3.GetDetailedTokenType != ExplorerTokenType.JWTOpenID) // not possible to create a test token if OpenId is used
                                            {
                                                // let display a test token
                                                Microsoft.IdentityModel.Tokens.X509SigningCredentials signingcred = null;
                                                if (form3.GetDetailedTokenType == ExplorerTokenType.JWTX509)
                                                {
                                                    signingcred = new Microsoft.IdentityModel.Tokens.X509SigningCredentials(form3.GetX509Certificate);
                                                }

                                                _context = Program.ConnectAndGetNewContext(_credentials); // otherwise cache issues with multiple options
                                                DynamicEncryption.TokenResult testToken = DynamicEncryption.GetTestToken(AssetToProcess, _context, form1.GetContentKeyType, signingcred, policyOption.Id);
                                                TextBoxLogWriteLine("The authorization test token for option #{0} ({1} with Bearer) is:\n{2}", form4list.IndexOf(form3), form3.GetTokenType.ToString(), Constants.Bearer + testToken.TokenString);
                                                System.Windows.Forms.Clipboard.SetText(Constants.Bearer + testToken.TokenString);

                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when creating the authorization policy for '{0}'.", AssetToProcess.Name, true);
                                    TextBoxLogWriteLine(e);
                                    ErrorCreationKey = true;
                                }

                            }
                            contentKeyAuthorizationPolicy.Update();

                        }
                    }


                    //////////// Delivery Policy
                    if (DelPol != null) // already created
                    {
                        try
                        {
                            AssetToProcess.DeliveryPolicies.Add(DelPol);
                            TextBoxLogWriteLine("Attached asset delivery policy '{0}' to asset '{1}'.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                        }
                        catch (Exception e)
                        {
                            TextBoxLogWriteLine("There is a problem when attaching the delivery policy for '{0}'.", AssetToProcess.Name, true);
                            TextBoxLogWriteLine(e);
                        }
                    }
                    else if (!form1.SelectExistingPolicies) // delivery policy to create (policy==null and user did not select the option to choose an existing policy)
                    {
                        // Let's create the Asset Delivery Policy now
                        string name = string.Format("AssetDeliveryPolicy {0} ({1})", form1.GetContentKeyType.ToString(), form1.GetAssetDeliveryProtocol.ToString());

                        try
                        {
                            DelPol = DynamicEncryption.CreateAssetDeliveryPolicyAES(AssetToProcess, contentKey, form1.GetAssetDeliveryProtocol, name, _context, aeslaurl, aesFinalUrl);
                            TextBoxLogWriteLine("Created asset delivery policy {0} for asset {1}.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                        }
                        catch (Exception e)
                        {
                            TextBoxLogWriteLine("There is a problem when creating the delivery policy for '{0}'.", AssetToProcess.Name, true);
                            TextBoxLogWriteLine(e);
                        }
                    }
                }
            }
        }

        private bool AddDynDecryption(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, CloudMediaContext _context)
        {
            bool Error = false;
            IAssetDeliveryPolicy DelPol = null;

            foreach (IAsset AssetToProcess in SelectedAssets)
                if (AssetToProcess != null)
                {
                    var DelPols = _context.AssetDeliveryPolicies
                       .Where(p => (p.AssetDeliveryProtocol == form1.GetAssetDeliveryProtocol) && (p.AssetDeliveryPolicyType == AssetDeliveryPolicyType.NoDynamicEncryption));
                    if (DelPols.Count() == 0) // no delivery policy found or user want to force creation
                    {
                        try
                        {
                            DelPol = DynamicEncryption.CreateAssetDeliveryPolicyNoDynEnc(AssetToProcess, form1.GetAssetDeliveryProtocol, _context);
                            TextBoxLogWriteLine("Created asset delivery policy {0} for asset {1}.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when creating the delivery policy for '{0}'.", AssetToProcess.Name, true);
                            TextBoxLogWriteLine(e);
                            Error = true;
                        }
                    }
                    else // let's use an existing delivery policy for no dynamic encryption
                    {
                        try
                        {
                            AssetToProcess.DeliveryPolicies.Add(DelPols.FirstOrDefault());
                            TextBoxLogWriteLine("Binded existing asset delivery policy {0} for asset {1}.", DelPols.FirstOrDefault().Id, AssetToProcess.Name);
                        }

                        catch (Exception e)
                        {
                            TextBoxLogWriteLine("There is a problem when using the delivery policy {0} for '{1}'.", DelPols.FirstOrDefault().Id, AssetToProcess.Name, true);
                            TextBoxLogWriteLine(e);
                            Error = true;
                        }
                    }
                }

            return Error;

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

        private void DoRemoveDynEnc()
        {
            string labelAssetName;

            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();

            if (SelectedAssets.Count > 0)
            {
                labelAssetName = string.Format("Locators, dynamic encryption policies and key authorization policies will be removed for asset '{0}'.", SelectedAssets.FirstOrDefault().Name);
                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = string.Format("Locators, dynamic encryption policies and key authorization policies will removed for these {0} selected assets.", SelectedAssets.Count.ToString());
                }
                labelAssetName += Constants.endline + Constants.endline + "Do you want to also DELETE the policies ? Be careful, this can impact other assets if the policies are shared !";
                DialogResult myDialogResult = MessageBox.Show(labelAssetName, "Dynamic encryption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (myDialogResult != DialogResult.Cancel)
                {
                    string keydeliveryconfig = string.Empty;

                    foreach (IAsset AssetToProcess in SelectedAssets)
                    {
                        bool Error = false;

                        if (AssetToProcess != null)
                        {
                            List<string> AutPolListIDs = new List<string>();
                            try
                            {
                                //Removing all locators associated with asset
                                var tasks = _context.Locators.Where(c => c.AssetId == AssetToProcess.Id && c.Type == LocatorType.OnDemandOrigin)
                                        .ToList()
                                        .Select(locator => locator.DeleteAsync())
                                        .ToArray();
                                Task.WaitAll(tasks);

                                //Removing all delivery policies associated with asset
                                List<IAssetDeliveryPolicy> DelPolItems = AssetToProcess.DeliveryPolicies.ToList(); // let's do a copy of the list in order to do a removal
                                foreach (var item in DelPolItems)
                                {
                                    AssetToProcess.DeliveryPolicies.Remove(item);
                                }


                                //Removing all authorization policies associated with asset keys
                                AutPolListIDs = AssetToProcess.ContentKeys.Select(k => k.AuthorizationPolicyId).ToList();
                                AssetToProcess.ContentKeys.ToList().ForEach(k => k.AuthorizationPolicyId = null);
                                var tasks2 = AssetToProcess.ContentKeys
                                        .ToList()
                                        .Select(k => k.UpdateAsync())
                                        .ToArray();
                                Task.WaitAll(tasks2);
                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when removing the delivery policy or locator for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                                Error = true;
                            }

                            if (myDialogResult == DialogResult.Yes) // Let's delete the policies
                            {
                                try
                                {
                                    // deleting authorization policies & options
                                    var policies = _context.ContentKeyAuthorizationPolicies.ToList().Where(p => AutPolListIDs.Contains(p.Id)).ToList();
                                    foreach (var policy in policies)
                                    {
                                        var AutPolOptionListIDs = policy.Options.Select(o => o.Id).ToList(); // create a list of IDs
                                        policy.Delete();

                                        // deleting authorization policies options
                                        Task<IMediaDataServiceResponse>[] deleteTasks = _context.ContentKeyAuthorizationPolicyOptions.ToList().Where(p => AutPolOptionListIDs.Contains(p.Id)).ToList().Select(o => o.DeleteAsync()).ToArray();
                                        Task.WaitAll(deleteTasks);
                                    }
                                }
                                catch (Exception e)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when deleting the delivery policy or locator for '{0}'.", AssetToProcess.Name, true);
                                    TextBoxLogWriteLine(e);
                                    Error = true;
                                }
                            }

                            if (!Error)
                            {
                                TextBoxLogWriteLine("Removed{0} asset delivery policies, key authorization policies and locator(s) for asset {1}.", (myDialogResult == DialogResult.Yes) ? " and deleted" : string.Empty, AssetToProcess.Name);
                            }

                            dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                        }
                    }
                }
            }
        }


        private void DoRemoveKeys()
        {
            string labelAssetName;

            List<IAsset> SelectedAssets = new List<IAsset>(); //ReturnSelectedAssetsFromProgramsOrAssets();

            if (SelectedAssets.Count > 0)
            {
                labelAssetName = string.Format("CENC, FairPlay and Envelope keys will be removed for asset '{0}'.", SelectedAssets.FirstOrDefault().Name);
                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = string.Format("CENC, FairPlay and Envelope keys will removed for these {0} selected assets.", SelectedAssets.Count.ToString());
                }
                labelAssetName += Constants.endline + "Do you want to also DELETE the keys ?";

                DialogResult myDialogResult = MessageBox.Show(labelAssetName, "Dynamic encryption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (myDialogResult != DialogResult.Cancel)
                {

                    foreach (IAsset AssetToProcess in SelectedAssets)
                    {
                        bool Error = false;

                        if (AssetToProcess != null)
                        {
                            List<string> KeysListIDs = new List<string>();
                            try
                            {
                                IList<IContentKey> CENCAESkeys = AssetToProcess.ContentKeys.Where(k => k.ContentKeyType == ContentKeyType.CommonEncryption || k.ContentKeyType == ContentKeyType.CommonEncryptionCbcs || k.ContentKeyType == ContentKeyType.EnvelopeEncryption).ToList();
                                KeysListIDs = CENCAESkeys.Select(k => k.Id).ToList(); // create a list of IDs

                                // deleting authorization policies & options
                                foreach (var key in CENCAESkeys)
                                {
                                    DynamicEncryption.DeleteKeyAuthorizationPolicyAndFairplayAsk(_context, key);
                                    AssetToProcess.ContentKeys.Remove(key);
                                    //if (deleteKeys) AssetToProcess.ContentKeys.Remove(key);
                                }
                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when removing the keys for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                                Error = true;
                            }
                            if (!Error)
                            {
                                TextBoxLogWriteLine("Removed {0} key{1} for asset {0}.", KeysListIDs.Count, KeysListIDs.Count > 1 ? "s" : "", AssetToProcess.Name);
                            }

                            if (myDialogResult == DialogResult.Yes) // Let's delete the keys
                            {
                                Error = false;

                                // deleting keys
                                //var deleteTasks = _context.ContentKeys.ToList().Where(k => KeysListIDs.Contains(k.Id)).ToList().Select(k => k.DeleteAsync()).ToArray();
                                //Task.WaitAll(deleteTasks);
                                foreach (var key in _context.ContentKeys.ToList().Where(k => KeysListIDs.Contains(k.Id)).ToList())
                                {
                                    try
                                    {
                                        //DynamicEncryption.CleanupKey(_context, key);
                                        key.Delete();
                                    }
                                    catch (Exception e)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when deleting the key {0} which was attached to '{1}'.", key.Id, AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        Error = true;
                                    }
                                }

                                if (!Error)
                                {
                                    TextBoxLogWriteLine("Deleted {0} key{1} for asset {0}.", KeysListIDs.Count, KeysListIDs.Count > 1 ? "s" : "", AssetToProcess.Name);
                                }
                            }

                            dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                        }
                    }
                }
            }
        }

        private void createALocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateLocator(ReturnSelectedAssetsFromProgramsOrAssetsV3());
        }

        private void deleteAllLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var SelectedAssets = ReturnSelectedAssetsV3();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }


        private void DoCopyOutputURLAssetOrProgramToClipboard()
        {
            Asset asset = ReturnSelectedAssetsFromProgramsOrAssetsV3().FirstOrDefault();
            if (asset != null)
            {
                AssetInfo AI = new AssetInfo(asset);
                var ValidURI = AssetInfo.GetValidOnDemandURI(asset, _amsClientV3);
                if (ValidURI != null)
                {
                    string url = ValidURI.AbsoluteUri;
                    var form = new ChooseStreamingEndpoint(_amsClientV3, asset, url);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        url = AssetInfo.RW(new Uri(url), form.SelectStreamingEndpoint, form.SelectedFilters, form.ReturnHttps, form.ReturnSelectCustomHostName, form.ReturnStreamingProtocol, form.ReturnHLSAudioTrackName, form.ReturnHLSNoAudioOnlyMode).ToString();
                    }
                    else
                    {
                        return;
                    }

                    var tokenDisplayForm = new EditorXMLJSON("Output URL", url, false, false, false);
                    tokenDisplayForm.Display();
                }
                else
                {
                    MessageBox.Show(string.Format("No valid URL is available for asset '{0}'.", asset.Name), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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
            var SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssetsV3();
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
            var SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssetsV3();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
            DoRefreshGridLiveOutputV(false);
        }

        private void displayRelatedAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoOfProgram();
        }

        private void DoMenuDisplayAssetInfoOfProgram()
        {
            List<IAsset> SelectedAssets = ReturnSelectedPrograms().Select(p => p.Asset).ToList();
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

        private async void extendExistingLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoRefreshStreamingLocators();
        }


        private void attachAnotherStoragheAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoAttachAnotherStorageAccount();
        }

        private async void DoAttachAnotherStorageAccount()
        {
            AttachStorage form = new AttachStorage(_amsClientV3);

            if (form.ShowDialog() == DialogResult.OK)
            {

                // Update storage accounts
                try
                {
                    TextBoxLogWriteLine("Processing Attach/Detach Storage account(s)...");
                    form.UpdateStorageAccounts();
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
            var SelectedJobs = ReturnSelectedJobsV3();
            if (SelectedJobs.Count == 1)
            {
                var JobToDisplayP = SelectedJobs.FirstOrDefault();

                if (JobToDisplayP != null)
                {
                    // var jobqueue = _context.Jobs.Where(j => j.State == Microsoft.WindowsAzure.MediaServices.Client.JobState.Processing).Count();
                    var outputsError = JobToDisplayP.Job.Outputs.Where(o => o.State == Microsoft.Azure.Management.Media.Models.JobState.Error);
                    if (outputsError.Count() > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var output in outputsError)
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
            string PortalUrl;
            if (_credentials.UseOtherAPI)
            {
                PortalUrl = _credentials.OtherManagementPortal;
            }
            else if (_credentials.UseAADInteract || _credentials.UseAADServicePrincipal)
            {
                if (_credentials.ADCustomSettings != null)
                {
                    PortalUrl = _credentials.OtherManagementPortal;
                }
                else
                {
                    AADEndPointMapping entrymapping = CredentialsEntry.AADMappings.Where(m => m.Name == _credentials.ADDeploymentName).FirstOrDefault();
                    PortalUrl = entrymapping != null ? entrymapping.ManagementPortal : "";
                }
            }
            else
            {
                PortalUrl = CredentialsEntry.GlobalPortal;
            }

            if (!string.IsNullOrEmpty(PortalUrl)) Process.Start(PortalUrl);
        }

        private void resubmitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoJobResubmit();
        }

        private void DoJobResubmit()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();

            if (SelectedJobs.Count == 0)
            {
                MessageBox.Show("No job was selected");
                return;
            }
            else if (SelectedJobs.Count > 1)
            {
                MessageBox.Show("Please select one job only");
                return;
            }

            if (SelectedJobs.FirstOrDefault() == null)
            {
                MessageBox.Show("No job was selected");
                return;
            }
            IJob myJob = SelectedJobs.FirstOrDefault();

            if (myJob.Tasks.Count != 1)
            {
                MessageBox.Show("This feature works only with a job that contains a single task");
                return;
            }

            if (myJob.InputMediaAssets == null)
            {
                MessageBox.Show("No input assets !");
                return;
            }

            string taskname = myJob.Tasks.FirstOrDefault().Name;

            MultipleProcessor form = new MultipleProcessor(_context, myJob)
            {
                Text = "Job re-submission",
                EncodingProcessorsList = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList(),
                EncodingJobName = string.Format("{0} (resubmitted on {1})", myJob.Name, DateTime.Now.ToString()),
                EncodingOutputAssetName = string.Format("{0} (resubmitted on {1})", myJob.OutputMediaAssets.FirstOrDefault().Name, DateTime.Now.ToString()),
                SingleTaskOptions = new JobOptionsVar
                {
                    Priority = myJob.Tasks.FirstOrDefault().Priority,
                    OutputAssetsCreationOptions = myJob.Tasks.FirstOrDefault().OutputAssets.FirstOrDefault().Options,
                    StorageSelected = myJob.Tasks.FirstOrDefault().OutputAssets.FirstOrDefault().StorageAccountName,
                    TasksOptionsSetting = myJob.Tasks.FirstOrDefault().Options
                },
                EncodingCreationMode = TaskJobCreationMode.SingleJobForAllInputAssets,
                JobPriority = myJob.Priority
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string inputasssetname = SelectedJobs.FirstOrDefault().InputMediaAssets.Count == 1 ? SelectedJobs.FirstOrDefault().InputMediaAssets.FirstOrDefault().Name : "multiple assets";
                string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, inputasssetname).Replace(Constants.NameconvProcessorname, form.SingleEncodingProcessorSelected.Name); ;

                IJob job = _context.Jobs.Create(jobnameloc, form.JobPriority);

                string tasknameloc = taskname ?? Constants.stringNull;//.Replace(Constants.NameconvInputasset, inputasssetname).Replace(Constants.NameconvProcessorname, form.SingleEncodingProcessorSelected.Name);

                ITask task = job.Tasks.AddNew(
                            tasknameloc,
                           form.SingleEncodingProcessorSelected,
                           form.SingleEncodingConfiguration,
                           form.SingleTaskOptions.TasksOptionsSetting
                           );
                task.Priority = form.SingleTaskOptions.Priority;

                // Specify the graph asset to be encoded, followed by the input video asset to be used
                task.InputAssets.AddRange(myJob.InputMediaAssets.ToList());
                string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, inputasssetname).Replace(Constants.NameconvProcessorname, form.SingleEncodingProcessorSelected.Name);
                task.OutputAssets.AddNew(outputassetnameloc, form.SingleTaskOptions.StorageSelected, form.SingleTaskOptions.OutputAssetsCreationOptions, form.SingleTaskOptions.OutputAssetsFormatOption);

                TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                // Submit the job and wait until it is completed. 
                try
                {
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobnameloc) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error); TextBoxLogWriteLine("There has been a problem when submitting the job '{0}'", jobnameloc, true);
                    TextBoxLogWriteLine(e);
                    return;
                }
                dataGridViewJobsV.DoJobProgress(new JobExtension());

                DoRefreshGridJobV(false);
            }
        }


        private void DoSelectTransformAndSubmitJob()
        {
            var SelectedAssets = ReturnSelectedAssetsV3();

            //CheckAssetSizeRegardingMediaUnit(SelectedAssets);
            ProcessFromTransform form = new ProcessFromTransform(_amsClientV3, SelectedAssets.Count)
            {
                ProcessingPromptText = (SelectedAssets.Count > 1) ? string.Format("{0} assets have been selected. 1 job will be submitted.", SelectedAssets.Count) : string.Format("Asset '{0}' will be encoded.", SelectedAssets.FirstOrDefault().Name),
                Text = "Template based processing"
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                CreateAndSubmitJobs(new List<Transform>() { form.SelectedTransform }, SelectedAssets);

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

                foreach (var fold in folders)
                    DoMenuUploadFromFolder_Step2(fold); // it's a folder

                if (files.Count > 0)
                    DoMenuUploadFromSingleFileS_Step2(files.ToArray()); // let's upload the objects as files, each file as an individual asset
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
                foreach (DataGridViewCell c in ((DataGridView)sender).Rows[e.RowIndex].Cells) c.Style.BackColor = Color.AliceBlue;
            }
        }


        private void withAzureMediaPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer);
        }


        public void DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType playertype, List<Asset> listassets, string filter = null)
        {
            foreach (var myAsset in listassets)
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
                            //IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + myAsset.Name, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew), AccessPermissions.Read);
                            //ILocator MyLocator = _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, myAsset, policy, null);
                            string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);
                            //var policy = new StreamingPolicy(Guid.NewGuid().ToString(), "strpol" + uniqueness, null, DateTime.Now, null, null, null, null, null);
                            //var policy2 = _amsClientV3.AMSclient.StreamingPolicies.Create(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, policy.Name, policy);

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
                    var MyUri = _amsClientV3.AMSclient.StreamingLocators.ListPaths(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, PlayBackLocator.Name)
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
            DoPlaybackChannelPreview(PlayerType.AzureMediaPlayerClear);
        }

        private void hTML5CaptionMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.DemoCaptionMaker);
        }

        private void DoGetTestToken()
        {
            bool Error = true;
            IAsset MyAsset = ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault();
            if (MyAsset != null)
            {
                if (DynamicEncryption.IsAssetHasAuthorizationPolicyWithToken(MyAsset, _context)) // dynamic encryption with token
                {
                    DynamicEncryption.TokenResult testToken = DynamicEncryption.GetTestToken(MyAsset, _context, displayUI: true);

                    if (!string.IsNullOrEmpty(testToken.TokenString))
                    {
                        TextBoxLogWriteLine("The authorization test token (with Bearer) is :\n{0}", Constants.Bearer + testToken.TokenString);
                        var tokenDisplayForm = new EditorXMLJSON("Authorization test token", Constants.Bearer + testToken.TokenString, false, false);
                        tokenDisplayForm.Display();
                        Error = false;
                    }
                }
                else
                {
                    MessageBox.Show("There is no policy defined using the token mode", "No token", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Error = false;
                }
            }
            if (Error) MessageBox.Show("Error when generating the test token", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            DoMenuDecryptAsset();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DoSetupDynEnc();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            DoGetTestToken();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            DoRemoveDynEnc();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            DoRemoveKeys();
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            DoSetupDynEnc();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            DoRemoveDynEnc();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            DoRemoveKeys();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            DoGetTestToken();
        }

        private void toAnotherAzureMediaServicesAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyAssetToAnotherAMSAccount();
        }

        private async void DoCopyAssetToAnotherAMSAccount()
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
                /*
                
                if (newdestinationcredentials.UseAADServicePrincipal)
                {
                    var spcrendentialsform = new AMSLoginServicePrincipal();
                    if (spcrendentialsform.ShowDialog() == DialogResult.OK)
                    {
                        newdestinationcredentials.ADSPClientId = spcrendentialsform.ClientId;
                        newdestinationcredentials.ADSPClientSecret = spcrendentialsform.ClientSecret;
                    }
                    else
                    {
                        return;
                    }
                   
                }
                */

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

        private static Dictionary<string, string> BuildStorageKeyDictionary(List<IAsset> SelectedAssets, CredentialsEntry DestinationCredentials, ref bool usercanceled, string SourceDefaultStorageName = null, string SourceDefaultStorageKey = null, string DestinationOtherStorage = null)
        {
            Dictionary<string, string> storagekeys = new Dictionary<string, string>();
            bool canceled = false;

            if (!string.IsNullOrEmpty(SourceDefaultStorageName) && !string.IsNullOrEmpty(SourceDefaultStorageKey))
            {
                storagekeys.Add(SourceDefaultStorageName, SourceDefaultStorageKey);
            }

            foreach (IAsset asset in SelectedAssets)
            {

                if (!storagekeys.ContainsKey(asset.StorageAccountName))
                {
                    string valuekey = "";
                    if (Program.InputBox("Source Storage Account Key Needed", string.Format("Please enter the Storage Account Access Key for '{0}' : ", asset.StorageAccountName), ref valuekey, true) == DialogResult.OK)
                    {
                        storagekeys.Add(asset.StorageAccountName, valuekey);
                    }
                    else
                    {
                        canceled = true;
                    }
                }
            }


            // useful for destination media services account with non default storage selected
            if (DestinationOtherStorage != null)
            {
                string valuekey = "";
                if (Program.InputBox("Destination Storage Account Key Needed", string.Format("Please enter the Storage Account Access Key for '{0}' : ", DestinationOtherStorage), ref valuekey, true) == DialogResult.OK)
                {
                    storagekeys.Add(DestinationOtherStorage, valuekey);
                }
                else
                {
                    canceled = true;
                }
            }
            else // default destination storage is used
            {
                CloudMediaContext newcontext = null;
                bool ErrorConnect = false;
                try
                {
                    newcontext = Program.ConnectAndGetNewContext(DestinationCredentials);
                }
                catch
                {
                    ErrorConnect = true;
                }
                if (!ErrorConnect)
                {
                    if (string.IsNullOrEmpty(DestinationCredentials.DefaultStorageKey) && !storagekeys.ContainsKey(newcontext.DefaultStorageAccount.Name)) // but key is not provided
                    {

                        string valuekey = "";
                        if (Program.InputBox("Destination Storage Account Key Needed", string.Format("Please enter the Storage Account Access Key of the destination storage account ('{0}') : ", newcontext.DefaultStorageAccount.Name), ref valuekey, true) == DialogResult.OK)
                        {
                            storagekeys.Add(newcontext.DefaultStorageAccount.Name, valuekey);
                        }
                        else
                        {
                            canceled = true;
                        }

                    }
                    else // key is provided
                    {
                        if (!storagekeys.ContainsKey(newcontext.DefaultStorageAccount.Name))
                        {
                            storagekeys.Add(newcontext.DefaultStorageAccount.Name, DestinationCredentials.DefaultStorageKey);
                        }
                    }
                }
            }
            usercanceled = canceled;
            return storagekeys;
        }

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
                        DoUpdateAndScaleStreamingEndpointEngine(streamingendpoint);
                    });
                }
            }
            else // enable
            {
                var form = new StreamingEndpointCDNEnable();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Task.Run(async () =>
                    {
                        streamingendpoint.CdnEnabled = true;
                        streamingendpoint.CdnProvider = form.ProviderSelectedString;
                        streamingendpoint.CdnProfile = form.Profile;
                        DoUpdateAndScaleStreamingEndpointEngine(streamingendpoint);
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
                var se = streamingendpoints.FirstOrDefault();
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
            DoCopyAssetToAnotherAMSAccount();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            DoExportAssetToAzureStorage();

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorage();

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
            ChannelRunOnPremisesLiveEncoder();
        }

        private void ChannelRunOnPremisesLiveEncoder()
        {
            ChannelRunOnPremisesEncoder form = new ChannelRunOnPremisesEncoder(_context, ReturnSelectedChannels());
            form.ShowDialog();
        }

        private void runAnOnpremisesLiveEncoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChannelRunOnPremisesLiveEncoder();
        }


        private void DoCopyChannelInputURLToClipboard(object sender, EventArgs e)
        {
            int index = 0;
            if (sender.GetType() == typeof(ToolStrip))
            {
                var send = (ToolStrip)sender;
                index = Convert.ToInt32(send.Name.Last().ToString()) - 1;
            }
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                var send = (ToolStripMenuItem)sender;
                index = Convert.ToInt32(send.Name.Last().ToString()) - 1;
            }

            var channel = ReturnSelectedLiveEvents().FirstOrDefault();

            string absuri;
            if (index == 1 && channel.Input.Endpoints.Count == 1 && channel.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4) // Smooth https
            {
                absuri = channel.Input.Endpoints[0].Url.Replace("http://", "https://");
            }
            else
            {
                absuri = channel.Input.Endpoints[index].Url;
            }

            string label = string.Format("Input URL ({0})", index);
            EditorXMLJSON DisplayForm = new EditorXMLJSON(label, absuri, false, false, false);
            DisplayForm.Display();
        }


        private void ContextMenuItemChannelCopyIngestURLToClipboard_DropDownOpening(object sender, EventArgs e)
        {
            ContextMenuOpeningLiveEventCopyInputUrl();
        }

        private void ContextMenuOpeningLiveEventCopyInputUrl()
        {
            var channel = ReturnSelectedLiveEvents().FirstOrDefault();

            inputURLMToolStripMenuItem1.Visible = (channel.Input.Endpoints.Count > 0);
            inputURLMToolStripMenuItem2.Visible = (channel.Input.Endpoints.Count > 1) || (channel.Input.Endpoints.Count == 1 && channel.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4);
            inputURLMToolStripMenuItem3.Visible = (channel.Input.Endpoints.Count > 2);
            inputURLMToolStripMenuItem4.Visible = (channel.Input.Endpoints.Count > 3);

            inputURLMToolStripMenuItem1.Text = (channel.Input.Endpoints.Count > 0) ? string.Format((string)inputURLMToolStripMenuItem1.Tag, new Uri(channel.Input.Endpoints[0].Url).Scheme) : "";
            inputURLMToolStripMenuItem2.Text = (channel.Input.Endpoints.Count > 1) ? string.Format((string)inputURLMToolStripMenuItem2.Tag, new Uri(channel.Input.Endpoints[1].Url).Scheme) : "";
            inputURLMToolStripMenuItem3.Text = (channel.Input.Endpoints.Count > 2) ? string.Format((string)inputURLMToolStripMenuItem3.Tag, new Uri(channel.Input.Endpoints[2].Url).Scheme) : "";
            inputURLMToolStripMenuItem4.Text = (channel.Input.Endpoints.Count > 3) ? string.Format((string)inputURLMToolStripMenuItem4.Tag, new Uri(channel.Input.Endpoints[3].Url).Scheme) : "";

            if (channel.Input.Endpoints.Count == 1 && channel.Input.StreamingProtocol == LiveEventInputProtocol.FragmentedMP4) //Smooth https
            {
                inputURLMToolStripMenuItem2.Text = string.Format((string)inputURLMToolStripMenuItem2.Tag, new Uri(channel.Input.Endpoints[0].Url.Replace("http://", "https://")).Scheme);
            }
        }


        private void contextMenuStripChannels_Opening(object sender, CancelEventArgs e)
        {
            var channels = ReturnSelectedLiveEvents();
            bool single = channels.Count == 1;
            bool oneOrMore = channels.Count > 0;

            // channel info
            ContextMenuItemChannelDisplayInfomation.Enabled = oneOrMore;

            // copy input url if only one channel
            ContextMenuItemChannelCopyIngestURLToClipboard.Enabled = single;

            // on premises encoder if only one channel
            ContextMenuItemChannelRunOnPremisesLiveEncoder.Enabled = single;

            // copy preview url if only one channel and preview is available
            ContextMenuItemChannelCopyPreviewURLToClipboard.Enabled = single && channels.FirstOrDefault().Preview != null;

            // start, stop, reset, delete, clone channel
            ContextMenuItemChannelStart.Enabled = oneOrMore;
            ContextMenuItemChannelStop.Enabled = oneOrMore;
            ContextMenuItemChannelReset.Enabled = oneOrMore;
            cloneChannelsToolStripMenuItem.Enabled = false;// oneOrMore;
            ContextMenuItemChannelDelete.Enabled = oneOrMore;

            // playback preview
            playbackTheProgramToolStripMenuItem.Enabled = oneOrMore;

            // telemetry
            loadMetricsToolStripMenuItem.Enabled = false;// enableTelemetry;
        }

        private void liveChannelToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void contextMenuStripPrograms_Opening(object sender, CancelEventArgs e)
        {
            var liveOutputs = ReturnSelectedLiveOutputs();
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

            // clone live output
            cloneToolStripMenuItem.Enabled = oneOrMore;

            // secutiry
            securityToolStripMenuItem.Enabled = oneOrMore;

            // publish
            publishToolStripMenuItem2.Enabled = oneOrMore;

            // playback
            ContextMenuItemProgramPlayback.Enabled = oneOrMore;

        }

        private void ContextMenuItemProgramCopyTheOutputURLToClipboard_Click(object sender, EventArgs e)
        {
            DoCopyOutputURLAssetOrProgramToClipboard();
        }

        private void buttonSetFilterChannel_Click(object sender, EventArgs e)
        {
            DoChannelSearch();
        }

        private void DoChannelSearch()
        {
            if (dataGridViewLiveEventsV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchChannelOption.SelectedItem as Item).Value);
                dataGridViewLiveEventsV.SearchInName = new SearchObject { Text = textBoxSearchNameChannel.Text, SearchType = stype };
                DoRefreshGridLiveEventV(false);
            }
        }

        private void comboBoxFilterTimeChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewLiveEventsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewLiveEventsV.TimeFilter == FilterTime.TimeRange)
            {
                var form = new TimeRangeSelection()
                {
                    TimeRange = dataGridViewLiveEventsV.TimeFilterTimeRange,
                    LabelMain = "Last Modified Time Range of Channels"
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

        private void comboBoxStatusChannel_SelectedIndexChanged(object sender, EventArgs e)
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
            var filter = ReturnSelectedAccountFilters().FirstOrDefault();
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
            var filters = ReturnSelectedAccountFilters();
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
            var selasset = ReturnSelectedAssetsFromProgramsOrAssetsV3().FirstOrDefault();

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
            var filters = ReturnSelectedAccountFilters();
            if (filters.Count == 1)
            {
                var sourcefilter = filters.FirstOrDefault();

                string newfiltername = sourcefilter.Name + "Copy";
                if (Program.InputBox("New name", "Enter the name of the new duplicate filter:", ref newfiltername) == DialogResult.OK)
                {
                    _amsClientV3.RefreshTokenIfNeeded();

                    try
                    {
                        _amsClientV3.AMSclient.AccountFilters.CreateOrUpdate(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, newfiltername, sourcefilter);
                        //_context.Filters.Create(newfiltername, sourcefilter.PresentationTimeRange, sourcefilter.Tracks, sourcefilter.FirstQuality);
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


        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoClonePrograms();
        }

        private void DoClonePrograms()
        {
            var SelectedPrograms = ReturnSelectedLiveOutputs();

            CopyAsset form = new CopyAsset(_context, 1, CopyAssetBoxMode.CloneProgram, _accountname);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!form.SingleDestinationAsset) // standard mode: 1:1 asset copy
                {
                    foreach (var program in SelectedPrograms)
                    {
                        // Start a worker thread that does asset copy.
                        Task.Factory.StartNew(() => ProcessCloneProgramToAnotherAMSAccount(form.DestinationLoginCredentials, form.DestinationStorageAccount, program, form.CopyDynEnc, form.RewriteLAURL, form.CloneLocators, form.CloneAssetFilters, form.CopyAlternateId));
                    }
                }
            }
        }

        private void cloneChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCloneChannels();
        }

        private void DoCloneChannels()
        {
            var SelectedChannels = ReturnSelectedChannels();
            CopyAsset form = new CopyAsset(_context, 1, CopyAssetBoxMode.CloneChannel, _accountname);

            if (form.ShowDialog() == DialogResult.OK)
            {

                if (!form.SingleDestinationAsset) // standard mode: 1:1 asset copy
                {
                    foreach (IChannel channel in SelectedChannels)
                    {
                        // Start a worker thread that does asset copy.
                        Task.Factory.StartNew(() => ProcessCloneChannelToAnotherAMSAccount(form.DestinationLoginCredentials, form.DestinationStorageAccount, channel));
                    }
                }
            }
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
            ExportToExcel form = new ExportToExcel(_context, _accountname, ReturnSelectedAssets(), dataGridViewAssetsV.assets);
            if (form.ShowDialog() == DialogResult.OK)
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
            string valuekey = "";
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
                var storageAccount = new CloudStorageAccount(new StorageCredentials(AMSClientV3.GetStorageName(storageId), valuekey), _amsClientV3.environment.ReturnStorageSuffix(), true);
                blobClient = storageAccount.CreateCloudBlobClient();

                // Get the current service properties
                serviceProperties = blobClient.GetServiceProperties();
            }
            catch (Exception ex)
            {
                Error = true;
                MessageBox.Show(ex.Message, "Error accessing the storage account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextBoxLogWriteLine(ex);
            }

            if (!Error)
            {
                var form = new StorageSettings(AMSClientV3.GetStorageName(storageId), serviceProperties);

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
            explorerReleaseNotesToolStripMenuItem.Enabled = (Program.AllReleaseNotesUrl != null);
        }

        private void explorerReleaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.AllReleaseNotesUrl != null)
                Process.Start(Program.AllReleaseNotesUrl.ToString());
        }

        private void toolStripMenuItem27_Click_1(object sender, EventArgs e)
        {
            DoCreateJobReportEmail();
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
            DoDeleteAssets(dataGridViewAssetsV.assets.ToList());
        }

        private void deleteVisibleAssetsInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAssets(dataGridViewAssetsV.assets.ToList());
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDeleteSelectedAssets();
        }

        private void deleteAllAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAllAssets();
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
                CheckboxAnychannelChangedByCode = true;
                SetRadiobuttonDisplayProgram(backupCheckboxAnychannel);
                radioButtonChAll.Enabled = radioButtonChNone.Enabled = radioButtonChSelected.Enabled = true;
            }
            else if (radioButtonChAll.Checked) // not empty and checkbox is still enabled
            {
                CheckboxAnychannelChangedByCode = true;
                backupCheckboxAnychannel = ReturnDisplayProgram();
                SetRadiobuttonDisplayProgram(enumDisplayProgram.Any);
                radioButtonChAll.Enabled = radioButtonChNone.Enabled = radioButtonChSelected.Enabled = false;
            }
        }

        private void createTestAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateTestsAssets();
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
                var sortOrder = DG.SortOrder;
                var dataGridViewColumn = DG.Columns[DG.SortedColumn.Name];
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
            var assets = ReturnSelectedAssetsFromProgramsOrAssets();

            string question = (assets.Count == 1) ? string.Format("Check the integrity of '{0}' ?", assets[0].Name) : string.Format("Check the integrity of these {0} archives ?", assets.Count);
            if (System.Windows.Forms.MessageBox.Show(question, "Integrity check", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                bool usercanceled = false;
                var storagekeys = BuildStorageKeyDictionary(assets, null, ref usercanceled, _context.DefaultStorageAccount.Name, _credentials.DefaultStorageKey, null);

                if (!usercanceled)
                {
                    Task.Run(async () =>
                    {
                        assets.ForEach(asset => CheckListArchiveBlobs(storagekeys, asset, AssetInfo.GetManifestSegmentsList(asset)));
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

        private void ProcessUploadFileToAsset(string SafeFileName, string FileName, IAsset MyAsset)
        {
            IAssetFile UploadedAssetFile = MyAsset.AssetFiles.Create(SafeFileName);
            UploadedAssetFile.Upload(FileName as string);
        }

        private async void DoFixSystemBitrate()
        {
            var dialogResult = System.Windows.Forms.MessageBox.Show(
                "AMS Explorer will check all manifest files (.ism) modified after Jan 20, 2016.\n\nDo you want to fix the ones with a wrong (too low) systemBitrate attribute ?\n(Yes: fix issues, No: only list issues)",
                "Manifest check", System.Windows.Forms.MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (dialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                bool fixError = dialogResult == System.Windows.Forms.DialogResult.Yes;

                Task.Run(async () =>
                {
                    List<IAssetFile> manifestFiles = new List<IAssetFile>();

                    bool Error = false;
                    int skipSize = 0;
                    int batchSize = 1000;
                    int currentSkipSize = 0;

                    // let's build the list of tasks
                    TextBoxLogWriteLine("Listing all the manifest file since Jan 20, 2016...");
                    var manifestFilesQuery = _context.Files.Where(f => f.LastModified > new DateTime(2016, 01, 20));

                    while (true)
                    {
                        // Enumerate through all manifests (1000 at a time)
                        var listfiles = manifestFilesQuery.Skip(skipSize).Take(batchSize).ToList();
                        currentSkipSize += listfiles.Count;
                        manifestFiles.AddRange(listfiles.Where(f => (f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase))));

                        if (currentSkipSize == batchSize)
                        {
                            skipSize += batchSize;
                            currentSkipSize = 0;
                        }
                        else
                        {
                            break;
                        }
                    }

                    TextBoxLogWriteLine("Found {0} manifest files modified after Jan 20, 2016", manifestFiles.Count);
                    int numberOfProcessedFiles = 0;

                    try
                    {
                        foreach (var file in manifestFiles)
                        {

                            string tempPath = System.IO.Path.GetTempPath();
                            string filePath = Path.Combine(tempPath, file.Name);
                            var currentAsset = file.Asset;

                            TextBoxLogWriteLine("Reading file '{0}' of asset ({1})", file.Name, currentAsset.Id);


                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                            await Task.Factory.StartNew(() => file.Download(filePath));

                            StreamReader streamReader = new StreamReader(filePath);
                            Encoding fileEncoding = streamReader.CurrentEncoding;
                            string datastring = streamReader.ReadToEnd();
                            streamReader.Close();


                            // let's analyse the manifest
                            // Prepare the manifest
                            bool ManifestMustBeUpdated = false;

                            XDocument doc = XDocument.Parse(datastring);

                            XNamespace ns = "http://www.w3.org/2001/SMIL20/Language";

                            var bodyxml = doc.Element(ns + "smil");
                            var body2 = bodyxml.Element(ns + "body");

                            var switchxml = body2.Element(ns + "switch");

                            var video = switchxml.Elements(ns + "video");
                            var audio = switchxml.Elements(ns + "audio");

                            // video tracks
                            foreach (var vtrack in video)
                            {
                                var systemBitrate = vtrack.Attribute("systemBitrate");
                                if (systemBitrate != null && (int.Parse(systemBitrate.Value) < 99000))
                                {
                                    ManifestMustBeUpdated = true;
                                    if (fixError) systemBitrate.Remove();
                                }
                            }

                            // audio tracks
                            foreach (var vtrack in audio)
                            {
                                var systemBitrate = vtrack.Attribute("systemBitrate");
                                if (systemBitrate != null && (int.Parse(systemBitrate.Value) < 1000))
                                {
                                    ManifestMustBeUpdated = true;
                                    if (fixError) systemBitrate.Remove();
                                }
                            }

                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }

                            if (ManifestMustBeUpdated) // file must be modified 
                            {
                                TextBoxLogWriteLine("Manifest file '{0}' of asset ({1}) needs to be updated...", file.Name, currentAsset.Id, true);

                                if (fixError) // user wants to fix the issue
                                {

                                    // let's create new manifest in temp folder
                                    StreamWriter outfile = new StreamWriter(filePath, false, fileEncoding);
                                    outfile.Write(doc.Declaration.ToString() + Environment.NewLine + doc.ToString());
                                    outfile.Close();

                                    // let's deleyte file online
                                    string assetFileName = file.Name;
                                    bool assetFilePrimary = file.IsPrimary;
                                    file.Delete();

                                    await Task.Factory.StartNew(() => ProcessUploadFileToAsset(Path.GetFileName(filePath), filePath, currentAsset));

                                    if (File.Exists(filePath))
                                    {
                                        File.Delete(filePath);
                                    }

                                    if (assetFilePrimary)
                                    {
                                        AssetInfo.SetFileAsPrimary(currentAsset, assetFileName);
                                    }
                                    TextBoxLogWriteLine("Manifest file '{0}' of asset ({1}) has been updated.", file.Name, currentAsset.Id);
                                }
                                numberOfProcessedFiles++;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when processing the manifest file(s)", true);
                        TextBoxLogWriteLine(ex);
                        Error = true;
                    }

                    if (!Error)
                    {
                        if (fixError)
                        {
                            TextBoxLogWriteLine("{0} manifest file(s) processed.", numberOfProcessedFiles);
                        }
                        else
                        {
                            TextBoxLogWriteLine("{0} manifest file(s) need to be processed.", numberOfProcessedFiles);
                        }
                    }
                }
           );

            }
        }


        private void editAlternateIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEditAssetAltId();
        }


        private void DoAccessAssetFilesTest()
        {
            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to test the query of all asset files ?", "Files query", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string valueBatch = "1000";
                string valuePosition = "0";
                if (
                    Program.InputBox("Start", "Please enter the first position :", ref valuePosition) == DialogResult.OK
                    &&
                    Program.InputBox("Batch", "Please enter the file batch size (1: each file tested individually but slow, 1000 max) :", ref valueBatch) == DialogResult.OK
                    )
                {

                    Task.Run(async () =>
                {
                    int skipSize = Convert.ToInt32(valuePosition);
                    int batchSize = Convert.ToInt32(valueBatch);
                    if (batchSize > 1000)
                    {
                        batchSize = 1000;
                    }
                    int i = 0;

                    // let's build the list of tasks
                    TextBoxLogWriteLine("Listing the files... There are {0} files in the account.", _context.Files.Count());
                    while (true)
                    {
                        bool Error = false;
                        IQueryable<IAssetFile> listfile = new List<IAssetFile>().AsQueryable();
                        List<IAssetFile> listfilel = new List<IAssetFile>();
                        // Enumerate through all asset files (batchSize at a time)
                        try
                        {
                            listfile = _context.Files.Skip(skipSize).Take(batchSize);
                            listfilel = listfile.ToList();
                        }
                        catch (Exception ex)
                        {
                            if (batchSize > 1)
                            {
                                TextBoxLogWriteLine("Error accessing file(s). Position: between {0} and {1}", skipSize, skipSize + batchSize - 1, true);
                            }
                            else
                            {
                                TextBoxLogWriteLine("Error accessing file. Position: {0}", skipSize, true);
                            }
                            TextBoxLogWriteLine(ex);
                            Error = true;
                        }

                        if (!Error && listfilel.Count == 0)
                        {
                            break;
                        }

                        skipSize += batchSize;
                        i++;

                        if (i % 5 == 0)
                        {
                            TextBoxLogWriteLine("Files from {0} to {1} accessed", skipSize - (batchSize * 5), skipSize - 1);
                        }
                    }
                    TextBoxLogWriteLine("File listing completed.");
                }
           );

                }
            }
        }

        private void DoAccessAssetTest()
        {
            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to test the query of all assets  ?", "Assets query", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string valueBatch = "1000";
                string valuePosition = "0";
                if (
                    Program.InputBox("Start", "Please enter the first position :", ref valuePosition) == DialogResult.OK
                    &&
                    Program.InputBox("Batch", "Please enter the file batch size (1: each asset tested individually but slow, 1000 max) :", ref valueBatch) == DialogResult.OK
                    )
                {

                    Task.Run(async () =>
                    {
                        int skipSize = Convert.ToInt32(valuePosition);
                        int batchSize = Convert.ToInt32(valueBatch);
                        if (batchSize > 1000)
                        {
                            batchSize = 1000;
                        }
                        int i = 0;

                        // let's build the list of tasks
                        TextBoxLogWriteLine("Listing the assets... There are {0} assets in the account.", _context.Assets.Count());
                        while (true)
                        {
                            bool Error = false;
                            IQueryable<IAsset> assetfile = new List<IAsset>().AsQueryable();
                            List<IAsset> listassetl = new List<IAsset>();
                            // Enumerate through all asset files (batchSize at a time)
                            try
                            {
                                assetfile = _context.Assets.Skip(skipSize).Take(batchSize);
                                listassetl = assetfile.ToList();
                            }
                            catch (Exception ex)
                            {
                                if (batchSize > 1)
                                {
                                    TextBoxLogWriteLine("Error accessing asset(s). Position: between {0} and {1}", skipSize, skipSize + batchSize - 1, true);
                                }
                                else
                                {
                                    TextBoxLogWriteLine("Error accessing asset. Position: {0}", skipSize, true);
                                }

                                TextBoxLogWriteLine(ex);
                                Error = true;
                            }

                            if (!Error && listassetl.Count == 0)
                            {
                                break;
                            }

                            skipSize += batchSize;
                            i++;

                            if (i % 5 == 0)
                            {
                                TextBoxLogWriteLine("Assets from {0} to {1} accessed", skipSize - (batchSize * 5), skipSize - 1);
                            }
                        }
                        TextBoxLogWriteLine("Asset listing completed.");
                    }
           );

                }
            }
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
            if (dataGridViewLiveOutputV.Initialized && !CheckboxAnychannelChangedByCode)
            {
                dataGridViewLiveOutputV.DisplayChannel = ReturnDisplayProgram();

                Task.Run(() =>
                {
                    DoRefreshGridLiveOutputV(false);
                });
            }
            CheckboxAnychannelChangedByCode = false;
        }

        private void tabPageLive_Resize(object sender, EventArgs e)
        {
            panelChannels.Size = new Size(panelChannels.Size.Width, tabPageLive.Size.Height / 2);
        }

        private void toolStripMenuItem38_Click_2(object sender, EventArgs e)
        {
            DoCopyOutputURLAssetOrProgramToClipboard();

        }


        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            DoCheckIntegrityLiveArchive();
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
            DoFixSystemBitrate();
        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            DoCopyAssetToAnotherAMSAccount();
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
            DoAnalyzeAssets(ReturnSelectedAssets(), true);
        }

        public void DoAnalyzeAssets(List<IAsset> assets, bool displayMessage)
        {
            if (displayMessage && MessageBox.Show("A thumbnails creation job will be created for each asset.\nThumbnails are used to setup regions with Media OCR, Motion Detector and MES Video Cropping.\nMetadata file provides technical information on the source.\n\nDo you want to submit the job(s) ?", "Asset(s) Analysis", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            var processor = Mainform.GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);
            StreamReader r = new StreamReader(Path.Combine(Application.StartupPath + Constants.PathConfigFiles, "AssetAnalysis.json"));
            string json = r.ReadToEnd();
            json = json.Replace("{AssetAnalysisStart}", Properties.Settings.Default.AssetAnalysisStart.ToString()).Replace("{AssetAnalysisStep}", Properties.Settings.Default.AssetAnalysisStep.ToString());

            foreach (var asset in assets)
            {
                bool Error = false;

                string taskname = string.Format("Analysis of asset '{0}'", asset.Name);
                string jobname = string.Format("Analysis of asset '{0}'", asset.Name);
                string outputname = string.Format("{0} (metadata and thumbnail)", asset.Name);
                string jsonwithid = json.Replace("{Basename}", asset.Id.Substring(Constants.AssetIdPrefix.Length));

                /*
                LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(
                   processor,
                   assets,
                   jobname,
                   Properties.Settings.Default.DefaultJobPriority,
                   taskname,
                   outputname,
                   new List<string>() { jsonwithid },
                  AssetCreationOptions.None,
                  AssetFormatOption.None,
                  Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None,
                   _context.DefaultStorageAccount.Name);
*/

                IJob job = _context.Jobs.Create(jobname, Properties.Settings.Default.DefaultJobPriority);
                ITask AnalyzeTask = job.Tasks.AddNew(
                    taskname,
                    processor,
                    jsonwithid,
                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None
                  );
                AnalyzeTask.InputAssets.Add(asset);

                // Add an output asset to contain the results of the job.  
                AnalyzeTask.OutputAssets.AddNew(outputname, _context.DefaultStorageAccount.Name, AssetCreationOptions.None, AssetFormatOption.None);

                // Submit the job  
                TextBoxLogWriteLine("Submitting job '{0}'", jobname);
                try
                {
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    if (assets.Count < 5)
                    {
                        MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobname) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    TextBoxLogWriteLine("There has been a problem when submitting the job '{0}' ", jobname, true);
                    TextBoxLogWriteLine(e);
                    Error = true;
                }
                if (!Error) Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(new JobExtension()));
            }
            DotabControlMainSwitch(AMSExplorer.Properties.Resources.TabJobs);
            DoRefreshGridJobV(false);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            DoMenuImportFromHttp();
        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new DisplayTelemetry(this, ReturnSelectedStreamingEndpoints().FirstOrDefault(), _context, _credentials);
            form.Show();
        }

        private void loadMetricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new DisplayTelemetry(this, ReturnSelectedChannels().FirstOrDefault(), _context, _credentials);
            form.Show();
        }

        private void fromAzureStoragecontainerSASUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorageSASContainer();
        }

        private void fromAzureStorageSASContainerPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorageSASContainer();
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

        private void textBoxSearchNameChannel_KeyDown(object sender, KeyEventArgs e)
        {
            // user pressed enter. let's apply the filter
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetFilterChannel_Click(this, new EventArgs());
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
            var SelectedTransforms = dataGridViewTransformsV.ReturnSelectedTransforms();
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
                var row = dataGridViewTransformsV.Rows[e.RowIndex];
                var transform = GetTransform(row.Cells[dataGridViewTransformsV.Columns["Name"].Index].Value.ToString());
                if (transform != null)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        if (DisplayInfo(transform) == DialogResult.OK)
                        {
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Arrow;
                    }
                }
            }
        }


        private void CreateVideoAnalyzerTransform()
        {
            var form = new PresetVideoAnalyzer();

            if (form.ShowDialog() == DialogResult.OK)
            {
                _amsClientV3.RefreshTokenIfNeeded();

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
                    // Create the Transform with the output defined above
                    var transform = _amsClientV3.AMSclient.Transforms.CreateOrUpdate(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
                    TextBoxLogWriteLine("Transform {0} created.", transform.Name); // Warning

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when creating the transform.", ex); // Warning
                }

                DoRefreshGridTransformV(false);
            }
        }

        private void CreateStandardEncoderTransform()
        {
            var form = new PresetStandardEncoder();

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
                    var transform = _amsClientV3.AMSclient.Transforms.CreateOrUpdate(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, form.TransformName, outputs, form.Description);
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
            var sel = ReturnSelectedTransforms();
            if (sel.Count > 0)
            {
                toolStripMenuItemSelectedTransform.Text = "From selected asset(s) with selected transform : " + string.Join(", ", ReturnSelectedTransforms().Select(t => t.Name));
                fromHttpsSourceWithSelectedTransformToolStripMenuItem.Text = "From http(s) source with selected transform : " + string.Join(", ", ReturnSelectedTransforms().Select(t => t.Name));
                toolStripMenuItemSelectedTransform.Enabled = fromHttpsSourceWithSelectedTransformToolStripMenuItem.Enabled = true;
            }
            else
            {
                toolStripMenuItemSelectedTransform.Text = "From selected asset(s) with selected transform : (no selection)";
                fromHttpsSourceWithSelectedTransformToolStripMenuItem.Text = "From http(s) source with selected transform : (no selection)";
                toolStripMenuItemSelectedTransform.Enabled = fromHttpsSourceWithSelectedTransformToolStripMenuItem.Enabled = false;
            }
        }

        private void toolStripMenuItemSelectedTransform_Click(object sender, EventArgs e)
        {
            CreateAndSubmitJobs(ReturnSelectedTransforms(), ReturnSelectedAssetsV3());
        }

        private void CreateAndSubmitJobs(List<Transform> sel, List<Asset> assets)
        {
            _amsClientV3.RefreshTokenIfNeeded();

            foreach (var asset in assets)
            {
                foreach (var transform in sel)
                {
                    string uniqueness = Guid.NewGuid().ToString("N");
                    string jobName = $"job-{uniqueness}";
                    string outputAssetName = $"output-{uniqueness}";

                    JobInputAsset jobInput = new JobInputAsset(asset.Name);

                    try
                    {


                        var outputAsset = _amsClientV3.AMSclient.Assets.CreateOrUpdate(
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

        private void deleteTransformsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteTransforms(ReturnSelectedTransforms());
        }

        private void dataGridViewTransformsV_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // on line on two is blue
            if (e.RowIndex % 2 == 0)
            {
                foreach (DataGridViewCell c in ((DataGridView)sender).Rows[e.RowIndex].Cells) c.Style.BackColor = Color.AliceBlue;
            }
        }

        private void textBoxAssetsPageNumber_TextChanged(object sender, EventArgs e)
        {
            dataGridViewAssetsV.RefreshAssets(GetTextBoxAssetsPageNumber());
            butNextPageAsset.Enabled = !dataGridViewAssetsV.CurrentPageIsMax;
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
            var sel = ReturnSelectedTransforms();

            var form = new HttpSource();

            if (form.ShowDialog() == DialogResult.OK)
            {
                JobInputHttp jobInput = new JobInputHttp(files: new[] { form.GetURL.ToString() });

                foreach (var transform in sel)
                {
                    string uniqueness = Guid.NewGuid().ToString("N");
                    string jobName = $"job-{uniqueness}";
                    string outputAssetName = $"output-{uniqueness}";

                    try
                    {


                        var outputAsset = _amsClientV3.AMSclient.Assets.CreateOrUpdate(
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
