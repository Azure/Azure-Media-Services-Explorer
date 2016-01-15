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
using System.Security.Cryptography;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections.Specialized;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Timers;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Microsoft.WindowsAzure.MediaServices.Client.Widevine;
using Newtonsoft.Json;
using Microsoft.WindowsAPICodePack.Dialogs;

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
        private ILocator PlayBackLocator = null;

        //Watch folder vars
        private Dictionary<string, DateTime> seen = new Dictionary<string, DateTime>();
        private TimeSpan seenInterval = new TimeSpan();
        WatchFolderSettings MyWatchFolderSettings = new WatchFolderSettings();

        private bool AMEPremiumWorkflowPresent = true;
        private bool AMEStandardPresent = true;
        private bool AMFaceDetectorPresent = true;
        private bool AMRedactorPresent = true;
        private bool AMMotionDetectorPresent = true;
        private bool AMStabilizerPresent = true;

        private System.Timers.Timer TimerAutoRefresh;
        bool DisplaySplashDuringLoading;
        private bool MediaRUFeatureOn = true; // On some test account, there is no Encoding RU so let's switch to OFF the feature in that case

        private bool backupCheckboxAnychannel = false;
        private bool CheckboxAnychannelChangedByCode = false;

        private bool largeAccount = false; // if nb assets > trigger
        private int triggerForLargeAccountNbAssets = 10000; // account with more than 10000 assets is considered as large account. Some queries will be disabled
        private int triggerForLargeAccountNbJobs = 5000; // account with more than 10000 assets is considered as large account. Some queries will be disabled
        private const int maxNbAssets = 1000000;
        private const int maxNbJobs = 50000;

        public Mainform()
        {
            InitializeComponent();

            // for player control embedded in UI
            Program.SetWebBrowserFeatures();

            this.Icon = Bitmaps.Azure_Explorer_ico;

            // USER SETTINSG CHECKS & UPDATES
            // upgrade settings from previous version
            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
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
            if ((Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder)))
            {
                Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEStdFiles;
            }

            // Default Slate Image
            if ((Properties.Settings.Default.DefaultSlateCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.DefaultSlateCurrentFolder)))
            {
                Properties.Settings.Default.DefaultSlateCurrentFolder = Application.StartupPath + Constants.PathDefaultSlateJPG;
            }

            Program.SaveAndProtectUserConfig(); // to save settings 

            _HelpFiles = Application.StartupPath + Constants.PathHelpFiles;

            AMSLogin form = new AMSLogin();

            if (form.ShowDialog() == DialogResult.Cancel)
            {
                Environment.Exit(0);
            }

            _credentials = form.LoginCredentials;

            DisplaySplashDuringLoading = true;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Splash(_credentials.AccountName))
                {
                    splashForm.Show();
                    while (DisplaySplashDuringLoading)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });

            // Get the service context.
            _context = Program.ConnectAndGetNewContext(_credentials, true);

            // mainform title
            toolStripStatusLabelConnection.Text = String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version) + " - Connected to " + _context.Credentials.ClientId;

            // notification title
            notifyIcon1.Text = string.Format(notifyIcon1.Text, _context.Credentials.ClientId);

            // name of the ams acount in the title of the form - useful when several instances to navigate with icons
            this.Text = string.Format(this.Text, _context.Credentials.ClientId);

            // Let's check storage credentials
            if (string.IsNullOrEmpty(_credentials.StorageKey))
            {
                havestoragecredentials = false;
            }
            else
            {
                bool ret = Program.ConnectToStorage(_context, _credentials);
            }

            if ((GetLatestMediaProcessorByName(Constants.ZeniumEncoder) == null) && (GetLatestMediaProcessorByName(Constants.AzureMediaEncoderPremiumWorkflow) == null))
            {
                AMEPremiumWorkflowPresent = false;
                encodeAssetWithPremiumWorkflowToolStripMenuItem.Enabled = false;  //menu
                ContextMenuItemPremiumWorkflow.Enabled = false; // mouse context menu
            }

            if (GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard) == null)
            {
                AMEStandardPresent = false;
                encodeAssetsWithAMEStandardToolStripMenuItem.Visible = false;
                encodeAssetWithAMEStandardToolStripMenuItem.Visible = false;
                toolStripSeparator35.Visible = false;
                toolStripSeparator32.Visible = false;

                // subclipping
                subclipLiveStreamsarchivesToolStripMenuItem.Visible = false;
                subclipProgramsToolStripMenuItem.Visible = false;
                subclipToolStripMenuItem.Visible = false;
            }

            if (GetLatestMediaProcessorByName(Constants.AzureMediaFaceDetector) == null)
            {
                AMFaceDetectorPresent = false;
                ProcessFaceDetectortoolStripMenuItem.Visible = false;
                toolStripMenuItemFaceDetector.Visible = false;
            }

            if (GetLatestMediaProcessorByName(Constants.AzureMediaMotionDetector) == null)
            {
                AMMotionDetectorPresent = false;
                ProcessMotionDetectortoolStripMenuItem.Visible = false;
                toolStripMenuItemMotionDetector.Visible = false;
            }

            if (GetLatestMediaProcessorByName(Constants.AzureMediaRedactor) == null)
            {
                AMRedactorPresent = false;
                ProcessRedactortoolStripMenuItem.Visible = false;
                toolStripMenuItemRedactor.Visible = false;
            }

            if (GetLatestMediaProcessorByName(Constants.AzureMediaStabilizer) == null)
            {
                AMStabilizerPresent = false;
                ProcessStabilizertoolStripMenuItem.Visible = false;
                toolStripMenuItemStabilizer.Visible = false;
            }

            // Timer Auto Refresh
            TimerAutoRefresh = new System.Timers.Timer(Properties.Settings.Default.AutoRefreshTime * 1000);
            TimerAutoRefresh.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Let's check if there is one streaming unit running
            if (_context.StreamingEndpoints.AsEnumerable().Where(o => o.State == StreamingEndpointState.Running).ToList().Count == 0)
                TextBoxLogWriteLine("There is no streaming endpoint running in this account.", true); // Warning

            // Let's check if there is one streaming scale units
            if (_context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count == 0)
                TextBoxLogWriteLine("There is no reserved unit streaming endpoint in this account. Dynamic packaging and live streaming output will not work.", true); // Warning

            // Let's check if there is enough streaming scale units for the channels
            double nbchannels = (double)_context.Channels.Count();
            double nbse = (double)_context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count;
            if (nbse > 0 && nbchannels > 0 && (nbchannels / nbse) > 5)
                TextBoxLogWriteLine("There are {0} channels and {1} streaming endpoint(s). Recommandation is to provision at least 1 streaming endpoint per group of 5 channels.", nbchannels, nbse, true); // Warning

            // let's check the encoding reserved unit and type
            try
            {
                if ((_context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits == 0) && (_context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType != ReservedUnitType.Basic))
                    TextBoxLogWriteLine("There is no Media Reserved Unit (encoding will use a shared pool) but unit type is not set to S1 (Basic).", true); // Warning
            }
            catch // can occur on test account
            {
                MediaRUFeatureOn = false;
                TextBoxLogWriteLine("There is an error when accessing to the Media Reserved Units API. Some controls are disabled in the processors tab.", true); // Warning
            }

            // nb assets limits
            int nbassets = _context.Assets.Count();
            largeAccount = nbassets > triggerForLargeAccountNbAssets;
            if (largeAccount)
            {
                TextBoxLogWriteLine("This account contains a lot of assets. Some queries are disabled."); // Warning
            }
            if (nbassets > (0.75 * maxNbAssets))
            {
                TextBoxLogWriteLine("This account contains {0} assets. Warning, the limit is {1}.", nbassets, maxNbAssets, true); // Warning
            }
            // nb jobs limits
            int nbjobs = _context.Jobs.Count();
            /*
            if (nbjobs > triggerForLargeAccountNbJobs)
            {
                TextBoxLogWriteLine("This account contains a lot of jobs. Sorting is disabled."); // Warning
            }
            */
            if (nbjobs > (0.75 * maxNbJobs))
            {
                TextBoxLogWriteLine("This account contains {0} jobs. Warning, the limit is {1}.", nbjobs, maxNbJobs, true); // Warning
            }

            ApplySettingsOptions(true);
        }



        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            DoRefresh();
        }

        public void Notify(string title, string text, bool Error = false)
        {
            notifyIcon1.ShowBalloonTip(3000, title, text, Error ? ToolTipIcon.Error : ToolTipIcon.Info);
        }


        private void ProcessImportFromHttp(Uri ObjectUrl, string assetname, string fileName, int index)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            bool Error = false;
            string ErrorMessage = string.Empty;

            TextBoxLogWriteLine("Starting the Http import process.");

            CloudBlockBlob blockBlob;
            IAssetFile assetFile;
            IAsset asset;
            ILocator destinationLocator = null;
            IAccessPolicy writePolicy = null;

            try
            {
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), _credentials.ReturnStorageSuffix(), true);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a new asset.
                asset = _context.Assets.Create(assetname, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
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

                blockBlob.StartCopy(ObjectUrl);

                DateTime startTime = DateTime.UtcNow;

                bool continueLoop = true;

                while (continueLoop)
                {
                    IEnumerable<IListBlobItem> blobsList = mediaBlobContainer.ListBlobs(null, true, BlobListingDetails.Copy);
                    foreach (var blob in blobsList)
                    {
                        var tempBlockBlob = (CloudBlockBlob)blob;
                        var destBlob = blob as CloudBlockBlob;

                        if (tempBlockBlob.Name == fileName)
                        {
                            tempBlockBlob.FetchAttributes();
                            var copyStatus = tempBlockBlob.CopyState;
                            if (copyStatus != null)
                            {
                                double percentComplete = (long)100 * (long)copyStatus.BytesCopied / (long)copyStatus.TotalBytes;

                                DoGridTransferUpdateProgress(percentComplete, index);

                                if (copyStatus.Status != CopyStatus.Pending)
                                {
                                    continueLoop = false;
                                    if (copyStatus.Status == CopyStatus.Failed)
                                    {
                                        Error = true;
                                        ErrorMessage = copyStatus.StatusDescription;
                                    }
                                }
                            }
                        }
                    }

                    System.Threading.Thread.Sleep(1000);
                }
                DateTime endTime = DateTime.UtcNow;
                TimeSpan diffTime = endTime - startTime;

                if (!Error)
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

                    DoGridTransferDeclareCompleted(index, asset.Id);
                    DoRefreshGridAssetV(false);
                }
                else // Error!
                {
                    DoGridTransferDeclareError(index, "Error during import. " + ErrorMessage);
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
                DoGridTransferDeclareError(index, ex);

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


        private async Task ProcessUploadFromFolder(object folderPath, int index, string storageaccount = null)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);
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
                asset = _context.Assets.CreateFromFolder(
                                                               folderPath as string,
                                                               storageaccount,
                                                               Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                                                               (af, p) =>
                                                               {
                                                                   progress[af.Name] = p.Progress;
                                                                   DoGridTransferUpdateProgress(progress.ToList().Average(l => l.Value), index);
                                                               }
                                                               );
                //SetISMFileAsPrimary(asset); // no need as primary seems to be set by .CreateFromFolder
            }
            catch (Exception e)
            {
                Error = true;
                DoGridTransferDeclareError(index, e);
                TextBoxLogWriteLine("Error when uploading from {0}", folderPath, true);
            }
            if (!Error)
            {
                DoGridTransferDeclareCompleted(index, asset.Id);
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


        static IChannel GetChannel(string channelId)
        {
            IChannel channel;

            try
            {
                // Use a LINQ Select query to get an asset.
                var channelInstance =
                    from a in _context.Channels
                    where a.Id == channelId
                    select a;
                // Reference the asset as an IAsset.
                channel = channelInstance.FirstOrDefault();
            }
            catch
            {

                channel = null;
            }
            return channel;
        }

        static IChannel GetChannelFromName(string name)
        {
            IChannel channel;

            try
            {
                // Use a LINQ Select query to get an asset.
                var channelInstance =
                    from a in _context.Channels
                    where a.Name == name
                    select a;
                // Reference the asset as an IAsset.
                channel = channelInstance.FirstOrDefault();
            }
            catch
            {
                channel = null;
            }
            return channel;
        }

        static IProgram GetProgram(string programId)
        {
            IProgram program;

            try
            {
                // Use a LINQ Select query to get an asset.
                var programInstance =
                    from a in _context.Programs
                    where a.Id == programId
                    select a;
                // Reference the asset as an IAsset.
                program = programInstance.FirstOrDefault();
            }
            catch
            {
                program = null;
            }
            return program;
        }

        static IStreamingEndpoint GetStreamingEndpoint(string originId)
        {
            IStreamingEndpoint origin;

            try
            {
                // Use a LINQ Select query to get an asset.
                var originInstance =
                    from a in _context.StreamingEndpoints
                    where a.Id == originId
                    select a;
                // Reference the asset as an IAsset.
                origin = originInstance.FirstOrDefault();
            }
            catch
            {
                origin = null;
            }
            return origin;
        }


        static void DeleteAsset(IAsset asset)
        {
            // delete the asset
            asset.Delete();
        }

        public void DeleteLocatorsForAsset(IAsset asset, bool onlyStreamingLocators)
        {
            if (asset != null)
            {
                string assetId = asset.Id;
                var locators = from a in _context.Locators
                               where a.AssetId == assetId
                               select a;

                if (onlyStreamingLocators)
                {
                    locators = locators.Where(l => l.Type == LocatorType.OnDemandOrigin);
                }

                foreach (var locator in locators)
                {
                    TextBoxLogWriteLine("Deleting locator {0} for asset {1}", locator.Path, assetId);
                    try
                    {
                        locator.Delete();
                    }
                    catch
                    {

                    }
                }
            }
        }

        void DeleteAccessPolicy(string existingPolicyId)
        {
            // To delete a specific access policy, get a reference to the policy.  
            // based on the policy Id passed to the method.
            var policyInstance =
                 from p in _context.AccessPolicies
                 where p.Id == existingPolicyId
                 select p;
            IAccessPolicy policy = policyInstance.FirstOrDefault();

            TextBoxLogWriteLine("Deleting policy {0}", existingPolicyId);
            policy.Delete();
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
                    || asset.AssetFiles.FirstOrDefault().Name.EndsWith(".kayak", StringComparison.OrdinalIgnoreCase)
                    || asset.AssetFiles.FirstOrDefault().Name.EndsWith(".graph", StringComparison.OrdinalIgnoreCase)
                    || asset.AssetFiles.FirstOrDefault().Name.EndsWith(".blueprint", StringComparison.OrdinalIgnoreCase)
                    || asset.AssetFiles.FirstOrDefault().Name.EndsWith(".zenium", StringComparison.OrdinalIgnoreCase)
                    || asset.AssetFiles.FirstOrDefault().Name.EndsWith(".xenio", StringComparison.OrdinalIgnoreCase));
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

        private void DoRefresh()
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridJobV(false);
            DoRefreshGridAssetV(false);
            DoRefreshGridChannelV(false);
            DoRefreshGridStreamingEndpointV(false);
            DoRefreshGridProcessorV(false);
            DoRefreshGridStorageV(false);
            DoRefreshGridFiltersV(false);
            DoRefreshGridIngestManifestV(false);
        }

        public void DoRefreshGridAssetV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewAssetsV.Init(_context);
                for (int i = 1; i <= dataGridViewAssetsV.PageCount; i++) comboBoxPageAssets.Items.Add(i);
                comboBoxPageAssets.SelectedIndex = 0;
                Debug.WriteLine("DoRefreshGridAssetforsttime");
            }

            Debug.WriteLine("DoRefreshGridAssetNotforsttime");
            int ComboBackupindex = 0;
            int DGpagecount = 0;

            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.AssetsPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid));
            comboBoxPageAssets.Invoke(new Action(() => ComboBackupindex = comboBoxPageAssets.SelectedIndex));
            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.RefreshAssets(_context, ComboBackupindex + 1)));
            comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.Items.Clear()));
            dataGridViewAssetsV.Invoke(new Action(() => DGpagecount = dataGridViewAssetsV.PageCount));

            for (int i = 0; i < DGpagecount; i++)
            {
                comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.Items.Add(i + 1)));
            }
            comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.SelectedIndex = dataGridViewAssetsV.CurrentPage - 1));

            tabPageAssets.Invoke(new Action(() => tabPageAssets.Text = string.Format(Constants.TabAssets + " ({0}/{1})", dataGridViewAssetsV.DisplayedCount, _context.Assets.Count())));
        }

        private void DoRefreshGridJobV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewJobsV.Init(_credentials, _context);
                for (int i = 1; i <= dataGridViewJobsV.PageCount; i++) comboBoxPageJobs.Items.Add(i);
                comboBoxPageJobs.SelectedIndex = 0;
            }

            Debug.WriteLine("DoRefreshGridJobVNotforsttime");
            int backupindex = 0;
            int pagecount = 0;
            dataGridViewJobsV.Invoke(new Action(() => dataGridViewJobsV.JobssPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid));

            comboBoxPageJobs.Invoke(new Action(() => backupindex = comboBoxPageJobs.SelectedIndex));
            dataGridViewJobsV.Invoke(new Action(() => dataGridViewJobsV.Refreshjobs(_context, backupindex + 1)));
            comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Clear()));
            dataGridViewJobsV.Invoke(new Action(() => pagecount = dataGridViewJobsV.PageCount));

            // add pages
            for (int i = 0; i < pagecount; i++)
            {
                comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Add(i + 1)));
            }
            comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.SelectedIndex = dataGridViewJobsV.CurrentPage - 1));
            //uodate tab nimber of jobs
            tabPageJobs.Invoke(new Action(() => tabPageJobs.Text = string.Format(Constants.TabJobs + " ({0}/{1})", dataGridViewJobsV.DisplayedCount, _context.Jobs.Count())));
        }

        public void DoRefreshGridIngestManifestV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewIngestManifestsV.Init(_credentials, _context);
                Debug.WriteLine("DoRefreshGridUngestManifestforsttime");
            }
            Debug.WriteLine("DoRefreshGridUngestManifestNotforsttime");
            dataGridViewIngestManifestsV.Invoke(new Action(() => dataGridViewIngestManifestsV.RefreshIngestManifests(_context)));
        }

        private void fromASingleFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuUploadFromSingleFile_Step1();
        }

        private void DoMenuUploadFromSingleFile_Step1()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DoMenuUploadFromSingleFile_Step2(openFileDialog.FileNames);
            }
        }

        private void DoMenuUploadFromSingleFile_Step2(string[] FileNames)
        {
            if (FileNames.Count() > 1)
            {
                if (System.Windows.Forms.MessageBox.Show("You selected multiple files. Each file will be uploaded as individual asset. If you want to create asset(s) that contain(s) several files, copy the files to folder(s) and upload or drag&drop the folder(s).", "Upload as invividual assets?", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

            // Each file goes in a individual asset
            foreach (String file in FileNames)
            {
                try
                {
                    int index = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, Properties.Settings.Default.useTransferQueue);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessUploadFileAndMore(file, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
        }




        private async Task ProcessUploadFileAndMore(object name, int index, WatchFolderSettings watchfoldersettings = null, string storageaccount = null)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            if (storageaccount == null) storageaccount = _context.DefaultStorageAccount.Name; // no storage account or null, then let's take the default one

            TextBoxLogWriteLine("Starting upload of file '{0}'", name);
            bool Error = false;
            IAsset asset = null;
            try
            {
                asset = _context.Assets.CreateFromFile(
                                                      name as string,
                                                      storageaccount,
                                                      Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                                                      (af, p) =>
                                                      {
                                                          DoGridTransferUpdateProgress(p.Progress, index);
                                                      }
                                                      );
                AssetInfo.SetFileAsPrimary(asset, Path.GetFileName(name as string));
            }
            catch (Exception e)
            {
                Error = true;
                DoGridTransferDeclareError(index, e);
                TextBoxLogWriteLine("Error when uploading '{0}'", name, true);
                TextBoxLogWriteLine(e);
                if (watchfoldersettings != null && watchfoldersettings.SendEmailToRecipient != null)
                {
                    Program.CreateAndSendOutlookMail(watchfoldersettings.SendEmailToRecipient, "Explorer Watchfolder: upload error " + name, e.Message);
                }

            }
            if (!Error)
            {
                DoGridTransferDeclareCompleted(index, asset.Id);
                if (watchfoldersettings != null && watchfoldersettings.DeleteFile) //user checked the box "delete the file"
                {
                    try
                    {
                        File.Delete(name as string);
                        TextBoxLogWriteLine("File '{0}' deleted.", name);
                    }
                    catch (Exception e)
                    {
                        TextBoxLogWriteLine("Error when deleting '{0}'", name, true);
                        if (watchfoldersettings.SendEmailToRecipient != null) Program.CreateAndSendOutlookMail(watchfoldersettings.SendEmailToRecipient, "Explorer Watchfolder: Error when deleting " + asset.Name, e.Message);

                    }
                }

                if (watchfoldersettings != null && watchfoldersettings.JobTemplate != null) // option with watchfolder to run a job based on a job template
                {
                    string jobname = string.Format("Processing of {0} with template {1}", asset.Name, watchfoldersettings.JobTemplate.Name);
                    List<IAsset> assetlist = new List<IAsset>() { asset };
                    // if user wants to insert a workflow or other asstes as asset #0
                    if (watchfoldersettings.TypeInputExtraInput != TypeInputExtraInput.None)
                    {
                        if (watchfoldersettings.ExtraInputAssets != null) assetlist.InsertRange(0, watchfoldersettings.ExtraInputAssets);
                    }

                    TextBoxLogWriteLine(string.Format("Submitting job '{0}'", jobname));

                    // Submit the job
                    IJob job = _context.Jobs.Create(jobname, watchfoldersettings.JobTemplate, assetlist, Properties.Settings.Default.DefaultJobPriority);

                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There has been a problem when submitting the job '{0}'", job.Name, true);
                        TextBoxLogWriteLine(e);
                        if (watchfoldersettings.SendEmailToRecipient != null)
                        {
                            Program.CreateAndSendOutlookMail(watchfoldersettings.SendEmailToRecipient, "Explorer Watchfolder: Error when submitting job for asset " + asset.Name, e.Message);
                        }
                        return;
                    }

                    DoRefreshGridJobV(false);

                    IJob myjob = GetJob(job.Id);
                    while (myjob.State == JobState.Processing || myjob.State == JobState.Queued || myjob.State == JobState.Scheduled)
                    {
                        System.Threading.Thread.Sleep(1000);
                        myjob = GetJob(job.Id);
                    }
                    if (myjob.State == JobState.Finished)
                    {
                        // job template does not rename the output assets. As a fix, we do this:
                        int taskind = 1;
                        foreach (var task in myjob.Tasks)
                        {
                            int outputind = 1;
                            foreach (var outputasset in task.OutputAssets)
                            {
                                IAsset oasset = AssetInfo.GetAsset(outputasset.Id, _context);
                                try
                                {
                                    oasset.Name = string.Format("{0} processed with {1}", asset.Name, watchfoldersettings.JobTemplate.Name);
                                    if (myjob.Tasks.Count > 1)
                                    {
                                        oasset.Name += string.Format(" - task {0}", taskind);
                                    }
                                    if (task.OutputAssets.Count > 1)
                                    {
                                        oasset.Name += string.Format(" - output asset {0}", outputind);
                                    }
                                    oasset.Update();
                                    TextBoxLogWriteLine("Output asset {0} renamed.", oasset.Name);
                                }
                                catch (Exception e)
                                {
                                    TextBoxLogWriteLine("Error when renaming an output asset", true);
                                    TextBoxLogWriteLine(e);
                                }

                                outputind++;
                            }
                            taskind++;
                        }


                        if (watchfoldersettings.PublishOutputAssets) //user wants to publish the output asset when it has been processed by the job 
                        {
                            IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + myjob.Name, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew), AccessPermissions.Read);
                            foreach (var oasset in myjob.OutputMediaAssets)
                            {
                                ILocator MyLocator = _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, oasset, policy, null);
                                if (watchfoldersettings.SendEmailToRecipient != null)
                                {
                                    IStreamingEndpoint SelectedSE = AssetInfo.GetBestStreamingEndpoint(_context);
                                    StringBuilder sb = new StringBuilder();
                                    Uri SmoothUri = MyLocator.GetSmoothStreamingUri();
                                    if (SmoothUri != null)
                                    {
                                        string playbackurl = AssetInfo.DoPlayBackWithStreamingEndpoint(PlayerType.AzureMediaPlayer, SmoothUri.AbsoluteUri, _context, this, oasset, launchbrowser: false, UISelectSEFiltersAndProtocols: false);
                                        sb.AppendLine("Link to playback the asset:");
                                        sb.AppendLine(playbackurl);
                                        sb.AppendLine();
                                    }
                                    sb.Append(AssetInfo.GetStat(oasset, SelectedSE));
                                    Program.CreateAndSendOutlookMail(watchfoldersettings.SendEmailToRecipient, "Explorer Watchfolder: Output asset published for asset " + asset.Name, sb.ToString());
                                }
                            }
                        }
                        else // no publication
                        {
                            foreach (var oasset in myjob.OutputMediaAssets)
                            {
                                if (watchfoldersettings.SendEmailToRecipient != null)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(AssetInfo.GetStat(oasset));

                                    Program.CreateAndSendOutlookMail(watchfoldersettings.SendEmailToRecipient, "Explorer Watchfolder: asset uploaded and processed " + asset.Name, sb.ToString());
                                }
                            }

                        }
                    }
                    else  // not completed successfuly
                    {
                        if (watchfoldersettings.SendEmailToRecipient != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append((new JobInfo(job).GetStats()));
                            Program.CreateAndSendOutlookMail(watchfoldersettings.SendEmailToRecipient, "Explorer Watchfolder: job " + job.State.ToString() + " for asset " + asset.Name, sb.ToString());
                        }
                    }
                }
                else // user selected no processing. Upload successfull
                {
                    if (watchfoldersettings != null && watchfoldersettings.SendEmailToRecipient != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(AssetInfo.GetStat(asset));
                        Program.CreateAndSendOutlookMail(watchfoldersettings.SendEmailToRecipient, "Explorer Watchfolder: upload successful " + asset.Name, sb.ToString());
                    }
                }
            }
            DoRefreshGridAssetV(false);
        }



        private void ProcessDownloadAsset(List<IAsset> SelectedAssets, string folder, int index, DownloadToFolderOption downloadOption, bool openFileExplorer)
        {
            // If download in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            bool multipleassets = SelectedAssets.Count > 1;
            bool Error = false;

            string labeldb = (multipleassets) ?
                string.Format("Starting download of files of {1} assets to {1}", SelectedAssets.Count, folder as string) :
                string.Format("Starting download of '{0}' to {1}", SelectedAssets.FirstOrDefault().Name, folder as string);

            TextBoxLogWriteLine(labeldb);
            foreach (IAsset mediaAsset in SelectedAssets)
            {
                string foldera = folder;
                bool ErrorCurrentAssetFolderCreation = false;
                bool ErrorCurrentAsset = false;
                if (downloadOption == DownloadToFolderOption.SubfolderAssetId)
                {
                    foldera += "\\" + mediaAsset.Id.Substring(12);
                }
                else if (downloadOption == DownloadToFolderOption.SubfolderAssetName)
                {
                    foldera += "\\" + mediaAsset.Name;
                }
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
                        mediaAsset.DownloadToFolder(foldera,
                                                                                         (af, p) =>
                                                                                         {
                                                                                             progress[af.Name] = p.Progress;
                                                                                             DoGridTransferUpdateProgress(progress.ToList().Average(l => l.Value), index);
                                                                                         }
                                                                                        );
                    }
                    catch (Exception e)
                    {
                        ErrorCurrentAsset = true;
                        Error = true;
                        TextBoxLogWriteLine(string.Format("Download of asset '{0}' failed.", mediaAsset.Name), true);
                        TextBoxLogWriteLine(e);
                        DoGridTransferDeclareError(index, e);
                    }
                    if (!ErrorCurrentAsset)
                    {
                        if (openFileExplorer) Process.Start(foldera);
                    }
                }

            }
            if (!Error)
            {
                DoGridTransferDeclareCompleted(index, folder.ToString());
            }
        }

        public void DoDownloadFileFromAsset(IAsset asset, IAssetFile File, object folder, int index)
        {
            // If download is in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

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

            Task.Factory.StartNew(async () =>
            {
                bool Error = false;
                try
                {
                    await File.DownloadAsync(Path.Combine(folder as string, File.Name), blobTransferClient, sasLocator, CancellationToken.None);
                    sasLocator.Delete();
                }
                catch (Exception e)
                {
                    Error = true;
                    TextBoxLogWriteLine(string.Format("Download of file '{0}' failed !", File.Name), true);
                    TextBoxLogWriteLine(e);
                    DoGridTransferDeclareError(index, e);
                }
                if (!Error)
                {
                    DoGridTransferDeclareCompleted(index, folder.ToString());
                }
            });
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
                    _backuprootfolderupload = SelectedPath;
                    int index = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(SelectedPath)), TransferType.UploadFromFolder, Properties.Settings.Default.useTransferQueue);

                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessUploadFromFolder(SelectedPath, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
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
            string valuekey = "";

            if (!havestoragecredentials)
            { // No blob credentials. Let's ask the user

                if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
                {
                    _credentials.StorageKey = valuekey;
                    havestoragecredentials = true;
                }
            }

            if (havestoragecredentials) // if we have the storage credentials
            {
                ImportHttp form = new ImportHttp();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    int index = DoGridTransferAddItem(string.Format("Import from Http of '{0}'", form.GetAssetFileName), TransferType.ImportFromHttp, Properties.Settings.Default.useTransferQueue);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessImportFromHttp(form.GetURL, form.GetAssetName, form.GetAssetFileName, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
                }
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
                    LocWarning = _context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count > 0 ? string.Empty : "Dynamic packaging will not work as there is no scale unit streaming endpoint in this account."
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
                        dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets.ToList());
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
            DialogResult? dialogResult = null;
            if (asset != null)
            {
                // Refresh the asset.
                _context = Program.ConnectAndGetNewContext(_credentials);
                asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();
                if (asset != null)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        AssetInformation form = new AssetInformation(this, _context)
                        {
                            myAsset = asset,
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

        public DialogResult? DisplayJobSource(IAsset asset)
        {
            DialogResult? dialogResult = null;
            if (asset != null)
            {
                var job = _context.Jobs.AsEnumerable().Where(j => j.OutputMediaAssets.Select(o => o.Id).ToList().Contains(asset.Id)).FirstOrDefault();

                if (job != null)
                {
                    DisplayInfo(job);
                }

                else
                {
                    MessageBox.Show("Source job was not found.");
                }
            }
            return dialogResult;
        }


        public static DialogResult CopyAssetToAzure(ref bool UseDefaultStorage, ref string containername, ref string otherstoragename, ref string otherstoragekey, ref List<IAssetFile> SelectedFiles, ref bool CreateNewContainer, IAsset sourceAsset)
        {
            ExportAssetToAzureStorage form = new ExportAssetToAzureStorage(_context, _credentials.StorageKey, sourceAsset, _credentials.ReturnStorageSuffix())
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


        public DialogResult? DisplayInfo(IJob job)
        {
            DialogResult? dialogResult = null;
            if (job != null)
            {
                // Refresh the context and job.
                _context = Program.ConnectAndGetNewContext(_credentials);
                job = _context.Jobs.Where(j => j.Id == job.Id).FirstOrDefault();
                if (job != null)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        JobInformation form = new JobInformation(_context)
                        {
                            MyJob = job
                        };
                        dialogResult = form.ShowDialog(this);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Arrow;
                    }
                }
            }
            return dialogResult;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)  // RENAME ASSET
        {
            DoMenuRenameAsset();
        }


        private void DoMenuRenameAsset()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count > 0)
            {
                IAsset AssetTORename = SelectedAssets.FirstOrDefault();

                if (AssetTORename != null)
                {
                    string value = AssetTORename.Name;

                    if (Program.InputBox("Asset rename", "Enter the new name:", ref value) == DialogResult.OK)
                    {
                        try
                        {
                            AssetTORename.Name = value;
                            AssetTORename.Update();
                        }
                        catch
                        {

                            TextBoxLogWriteLine("There is a problem when renaming the asset.", true);
                            return;
                        }
                        TextBoxLogWriteLine("Renamed asset '{0}'.", AssetTORename.Id);
                        dataGridViewAssetsV.PurgeCacheAsset(AssetTORename);
                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                    }
                }
            }
        }


        private void DoMenuDownloadToLocal()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count == 0) return;
            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
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
                        else if (form.FolderOption == DownloadToFolderOption.SubfolderAssetId)
                        {
                            path = Path.Combine(path, asset.Id);
                        }
                        listfiles.AddRange(asset.AssetFiles.ToList().Where(f => File.Exists(path + @"\\" + f.Name)).Select(f => path + @"\\" + f.Name).ToList());
                    }
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

                    string label = string.Format("Download of asset '{0}'", mediaAsset.Name);
                    if (SelectedAssets.Count > 1) label = string.Format("Download of {0} assets", SelectedAssets.Count);

                    int index = DoGridTransferAddItem(label, TransferType.DownloadToLocal, Properties.Settings.Default.useTransferQueue);
                    // Start a worker thread that does downloading.
                    Task.Factory.StartNew(() => ProcessDownloadAsset(SelectedAssets, form.FolderPath, index, form.FolderOption, form.OpenFolderAfterDownload));
                    DotabControlMainSwitch(Constants.TabTransfers);
                }
            }
        }


        private void cancelJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCancelJobs();
        }


        private void DoCancelJobs()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();

            if (SelectedJobs.Count > 0)
            {
                string question = "Cancel these " + SelectedJobs.Count + " jobs ?";
                if (SelectedJobs.Count == 1) question = "Cancel " + SelectedJobs[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) cancelation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IJob JobToCancel in SelectedJobs)
                    {
                        if (JobToCancel != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Canceling job '{0}'...", JobToCancel.Name);

                            try
                            {
                                JobToCancel.Cancel();
                                TextBoxLogWriteLine("Job '{0}' canceled.", JobToCancel.Name);

                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("Error when canceling job '{0}'.", JobToCancel.Name, true);
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



        private void DoCreateLocator(List<IAsset> SelectedAssets)
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
                    LocWarning = _context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count > 0 ? string.Empty : "Dynamic packaging will not work as there is no scale unit streaming endpoint in this account."
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // The permissions for the locator's access policy.
                    AccessPermissions accessPolicyPermissions = AccessPermissions.Read;

                    // The duration for the locator's access policy.
                    TimeSpan accessPolicyDuration = form.LocatorEndDate.Subtract(DateTime.UtcNow);
                    if (form.LocatorStartDate != null)
                    {
                        accessPolicyDuration = form.LocatorEndDate.Subtract((DateTime)form.LocatorStartDate);
                    }

                    sbuilder.Clear();

                    try
                    {
                        Task.Factory.StartNew(() => ProcessCreateLocator(form.LocatorType, SelectedAssets, accessPolicyPermissions, accessPolicyDuration, form.LocatorStartDate, form.ForceLocatorGuid));
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


        private void ProcessCreateLocator(LocatorType locatorType, List<IAsset> assets, AccessPermissions accessPolicyPermissions, TimeSpan accessPolicyDuration, Nullable<DateTime> startTime, string ForceLocatorGUID)
        {
            IAccessPolicy policy;
            try
            {
                policy = _context.AccessPolicies.Create("AP AMSE", accessPolicyDuration, accessPolicyPermissions);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error. Could not create access policy.", true);
                TextBoxLogWriteLine(ex);
                return;
            }

            foreach (var AssetToP in assets)
            {
                ILocator locator = null;

                try
                {
                    if (locatorType == LocatorType.Sas || string.IsNullOrEmpty(ForceLocatorGUID)) // It's a SAS loctor or user does not want to force the GUID if this is a Streaming locator
                    {
                        locator = _context.Locators.CreateLocator(locatorType, AssetToP, policy, startTime);
                    }
                    else // Streaming locator and user wants to force the GUID
                    {
                        locator = _context.Locators.CreateLocator(ForceLocatorGUID, LocatorType.OnDemandOrigin, AssetToP, policy, startTime);
                    }
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error. Could not create a locator for '{0}' (is the asset encrypted, or locators quota has been reached ?)", AssetToP.Name, true);
                    TextBoxLogWriteLine(ex);
                    return;
                }
                if (locator == null) return;

                // let's choose a SE that running and with higher number of RU
                IStreamingEndpoint SESelected = AssetInfo.GetBestStreamingEndpoint(_context);
                bool SESelectedHasRU = SESelected.ScaleUnits > 0;

                if (!SESelectedHasRU && AssetToP.AssetType != AssetType.SmoothStreaming)
                {
                    TextBoxLogWriteLine("There is no running streaming endpoint with a scale unit.", true);
                }

                StringBuilder sbuilderThisAsset = new StringBuilder();
                sbuilderThisAsset.AppendLine("Asset:");
                sbuilderThisAsset.AppendLine(AssetToP.Name);
                sbuilderThisAsset.AppendLine("Locator ID:");
                sbuilderThisAsset.AppendLine(locator.Id);
                sbuilderThisAsset.AppendLine("Locator Path (best streaming endpoint selected)");
                sbuilderThisAsset.AppendLine(AssetInfo.RW(locator.Path, SESelected));
                sbuilderThisAsset.AppendLine("");

                if (locatorType == LocatorType.OnDemandOrigin)
                {
                    // Get the MPEG-DASH URL of the asset for adaptive streaming.
                    Uri mpegDashUri = AssetInfo.RW(locator.GetMpegDashUri(), SESelected);

                    // Get the HLS URL of the asset for adaptive streaming.
                    Uri HLSUri = AssetInfo.RW(locator.GetHlsUri(), SESelected);
                    Uri HLSUriv3 = AssetInfo.RW(locator.GetHlsv3Uri(), SESelected);

                    // Get the Smooth URL of the asset for adaptive streaming.
                    Uri SmoothUri = AssetInfo.RW(locator.GetSmoothStreamingUri(), SESelected);

                    if (AssetToP.AssetType == AssetType.MediaServicesHLS)
                    // It is a static HLS asset, so let's propose only the standard HLS V3 locator
                    {
                        sbuilderThisAsset.AppendLine(AssetInfo._hls_v3 + " : ");
                        sbuilderThisAsset.AppendLine(AddBracket(HLSUriv3.AbsoluteUri));
                    }
                    else // It's not Static HLS
                    {
                        if (!SESelectedHasRU && AssetToP.AssetType == AssetType.SmoothStreaming)
                        // it's smooth streaming with no dynamic packaging
                        {
                            sbuilderThisAsset.AppendLine(AssetInfo._smooth + " : ");
                            sbuilderThisAsset.AppendLine(AddBracket(SmoothUri.AbsoluteUri));
                        }
                        else if (SESelectedHasRU && (AssetToP.AssetType == AssetType.SmoothStreaming || AssetToP.AssetType == AssetType.MultiBitrateMP4))
                        // Smooth or multi MP4, SE RU so dynamic packaging is possible
                        {
                            if (locator.GetSmoothStreamingUri() != null)
                            {
                                sbuilderThisAsset.AppendLine(AssetInfo._smooth + " : ");
                                sbuilderThisAsset.AppendLine(AddBracket(SmoothUri.AbsoluteUri));
                                sbuilderThisAsset.AppendLine(AssetInfo._smooth_legacy + " : ");
                                sbuilderThisAsset.AppendLine(AddBracket(AssetInfo.GetSmoothLegacy(SmoothUri.AbsoluteUri)));
                            }
                            if (locator.GetMpegDashUri() != null)
                            {
                                sbuilderThisAsset.AppendLine(AssetInfo._dash + " : ");
                                sbuilderThisAsset.AppendLine(AddBracket(mpegDashUri.AbsoluteUri));
                            }
                            if (locator.GetHlsUri() != null)
                            {
                                sbuilderThisAsset.AppendLine(AssetInfo._hls_v4 + " : ");
                                sbuilderThisAsset.AppendLine(AddBracket(HLSUri.AbsoluteUri));
                                sbuilderThisAsset.AppendLine(AssetInfo._hls_v3 + " : ");
                                sbuilderThisAsset.AppendLine(AddBracket(AssetInfo.RW(locator.GetHlsv3Uri(), SESelected).AbsoluteUri));
                            }
                        }
                    }
                }
                else //SAS
                {
                    IEnumerable<IAssetFile> AssetFiles = AssetToP.AssetFiles.ToList();

                    // Generate the Progressive Download URLs for each file. 
                    List<Uri> ProgressiveDownloadUris =
                        AssetFiles.Select(af => af.GetSasUri()).ToList();


                    TextBoxLogWriteLine("You can progressively download the following files :");
                    ProgressiveDownloadUris.ForEach(uri =>
                    {
                        sbuilderThisAsset.AppendLine(AddBracket(uri.AbsoluteUri));
                    }
                                        );
                }
                //log window
                TextBoxLogWriteLine(sbuilderThisAsset.ToString());

                if (sbuilderThisAsset != null)
                {
                    sbuilder.Append(sbuilderThisAsset); // we add this builder to the general builder
                                                        // COPY to clipboard. We need to create a STA thread for it
                    System.Threading.Thread MyThread = new Thread(new ParameterizedThreadStart(DoCopyClipboard));
                    MyThread.SetApartmentState(ApartmentState.STA);
                    MyThread.IsBackground = true;
                    MyThread.Start(sbuilder.ToString());
                }
            }
            dataGridViewAssetsV.PurgeCacheAssets(assets);
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


        private void DoDeleteAllLocatorsOnAssets(List<IAsset> SelectedAssets, bool onlyStreamingLocators = false)
        {
            if (SelectedAssets.Count > 0)
            {
                string question = "Delete all locators of these " + SelectedAssets.Count + " assets ?";
                if (SelectedAssets.Count == 1) question = "Delete all the locators of " + SelectedAssets[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Locators deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IAsset AssetToProcess in SelectedAssets)
                    {
                        if (AssetToProcess != null)
                        {
                            //delete locators
                            TextBoxLogWriteLine("Deleting locators of asset '{0}'", AssetToProcess.Name);
                            try
                            {
                                DeleteLocatorsForAsset(AssetToProcess, onlyStreamingLocators);
                                TextBoxLogWriteLine("Deletion done.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting locators of the asset {0}.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(ex);
                            }
                            dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                        }
                    }
                }
            }
        }

        private List<IAsset> ReturnSelectedAssetsFromProgramsOrAssets()
        {
            if (tabControlMain.SelectedTab.Text.StartsWith(Constants.TabAssets)) // we are in the asset tab
            {
                return ReturnSelectedAssets();
            }
            else if (tabControlMain.SelectedTab.Text.StartsWith(Constants.TabLive)) // we are in the live tab
            {
                return ReturnSelectedPrograms().Select(p => p.Asset).ToList();
            }
            else
            {
                return null;
            }
        }


        private List<IAsset> ReturnSelectedAssets()
        {
            List<IAsset> SelectedAssets = new List<IAsset>();
            try
            {
                foreach (DataGridViewRow Row in dataGridViewAssetsV.SelectedRows)
                {
                    var asset = _context.Assets.Where(j => j.Id == Row.Cells[dataGridViewAssetsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault();
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

        private List<IIngestManifest> ReturnSelectedIngestManifests()
        {
            List<IIngestManifest> SelectedIngestManifests = new List<IIngestManifest>();
            foreach (DataGridViewRow Row in dataGridViewIngestManifestsV.SelectedRows)
                SelectedIngestManifests.Add(_context.IngestManifests.Where(j => j.Id == Row.Cells["Id"].Value.ToString()).FirstOrDefault());
            SelectedIngestManifests.Reverse();
            return SelectedIngestManifests;
        }

        private IStorageAccount ReturnSelectedStorage()
        {

            IStorageAccount SelectedStorage = null;
            if (dataGridViewStorage.SelectedRows.Count == 1)
            {
                var row = dataGridViewStorage.SelectedRows[0];
                var index = dataGridViewStorage.Columns["StrictName"].Index;
                var storagename = row.Cells[index].Value.ToString();
                SelectedStorage = _context.StorageAccounts.Where(s => s.Name == storagename).FirstOrDefault();
            }

            return SelectedStorage;
        }

        private List<IStreamingFilter> ReturnSelectedFilters()
        {

            List<IStreamingFilter> SelectedFilters = new List<IStreamingFilter>();
            foreach (DataGridViewRow Row in dataGridViewFilters.SelectedRows)
            {
                string filtername = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                IStreamingFilter myfilter = _context.Filters.Where(f => f.Name == filtername).FirstOrDefault();
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
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            DoDeleteAssets(SelectedAssets);
        }

        private void DoDeleteAssets(List<IAsset> SelectedAssets)
        {
            if (SelectedAssets.Count > 0)
            {
                string question = (SelectedAssets.Count == 1) ? "Delete " + SelectedAssets[0].Name + " ?" : "Delete these " + SelectedAssets.Count + " assets ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Asset deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Task.Run(async () =>
                    {
                        bool Error = false;
                        try
                        {
                            Task[] deleteTasks = SelectedAssets.Select(a => a.DeleteAsync()).ToArray();
                            TextBoxLogWriteLine("Deleting asset(s)");
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
                Task.Run(async () =>
                {
                    bool Error = false;
                    int skipSize = 0;
                    int batchSize = 1000;
                    int currentSkipSize = 0;

                    // let's build the list of tasks
                    TextBoxLogWriteLine("Listing all the assets...");
                    List<Task> deleteTasks = new List<Task>();
                    while (true)
                    {
                        // Enumerate through all assets (1000 at a time)
                        var listassets = _context.Assets.Skip(skipSize).Take(batchSize).ToList();
                        currentSkipSize += listassets.Count;
                        deleteTasks.AddRange(listassets.Select(a => a.DeleteAsync()).ToArray());

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

                    TextBoxLogWriteLine(string.Format("Deleting {0} asset(s)", deleteTasks.Count));
                    try
                    {
                        Task.WaitAll(deleteTasks.ToArray());
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


        private void allAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAllAssets();
        }


        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedAssets().FirstOrDefault());
        }


        private void displayJobInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedJobs().FirstOrDefault());
        }


        private void DoMenuImportFromAzureStorage()
        {
            string valuekey = "";
            string targetAssetID = "";

            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count > 0) targetAssetID = SelectedAssets.FirstOrDefault().Id;

            if (!havestoragecredentials)
            { // No blob credentials. Let's ask the user
                if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
                {
                    _credentials.StorageKey = valuekey;
                    havestoragecredentials = true;
                }
            }
            if (havestoragecredentials) // if we have the storage credentials
            {
                ImportFromAzureStorage form = new ImportFromAzureStorage(_context, _credentials.StorageKey, _credentials.ReturnStorageSuffix())
                {
                    ImportLabelDefaultStorageName = _context.DefaultStorageAccount.Name,
                    ImportNewAssetName = "NewAsset Blob_" + Guid.NewGuid(),
                    ImportCreateNewAsset = true
                };

                if (!string.IsNullOrEmpty(targetAssetID))
                {
                    if (SelectedAssets.FirstOrDefault().Options == AssetCreationOptions.None && SelectedAssets.FirstOrDefault().StorageAccountName == _context.DefaultStorageAccount.Name) // Ok, the selected asset is not encrypyted and is in the default storage account
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
                    int index = DoGridTransferAddItem("Import from Azure Storage " + (form.ImportCreateNewAsset ? "to a new asset" : "to an existing asset"), TransferType.ImportFromAzureStorage, Properties.Settings.Default.useTransferQueue);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessImportFromAzureStorage(form.ImportUseDefaultStorage, form.SelectedBlobContainer, form.ImporOtherStorageName, form.ImportOtherStorageKey, form.SelectedBlobs, form.ImportCreateNewAsset, form.ImportNewAssetName, targetAssetID, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
        }


        private void ProcessImportFromAzureStorage(bool UseDefaultStorage, string containername, string otherstoragename, string otherstoragekey, List<IListBlobItem> SelectedBlobs, bool CreateNewAsset, string newassetname, string targetAssetID, int index)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            IAsset asset;
            if (CreateNewAsset)
            {
                // Create a new asset.
                asset = _context.Assets.Create(newassetname, AssetCreationOptions.None);
            }
            else //copy files in an existing asset
            {
                asset = AssetInfo.GetAsset(targetAssetID, _context);

            }
            if (UseDefaultStorage) // The default storage is used
            {
                CloudStorageAccount storageAccount;
                storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), _credentials.ReturnStorageSuffix(), true);


                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                var mediaBlobContainer = cloudBlobClient.GetContainerReference(containername);

                TextBoxLogWriteLine("Starting the blob copy process.");

                IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);

                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                Uri uploadUri = new Uri(destinationLocator.Path);
                string assetTargetContainerName = uploadUri.Segments[1];
                CloudBlobContainer assetTargetContainer = cloudBlobClient.GetContainerReference(assetTargetContainerName);

                CloudBlockBlob sourceCloudBlob, destinationBlob;
                long Length = 0;
                long BytesCopied = 0;
                string fileName;
                double percentComplete;
                bool Error = false;

                //calculate size
                foreach (var sourceBlob1 in SelectedBlobs)
                {
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob1.Uri.AbsoluteUri));

                    sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    Length += sourceCloudBlob.Properties.Length;
                }

                // do the copy
                int nbblob = 0;
                foreach (var sourceBlob in SelectedBlobs)
                {
                    nbblob++;
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                    sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    if (sourceCloudBlob.Properties.Length > 0)
                    {
                        try
                        {
                            IAssetFile assetFile = asset.AssetFiles.Create(fileName);
                            destinationBlob = assetTargetContainer.GetBlockBlobReference(fileName);

                            destinationBlob.DeleteIfExists();
                            destinationBlob.StartCopy(sourceCloudBlob);

                            CloudBlockBlob blob;
                            blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(fileName);

                            while (blob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                blob.FetchAttributes();
                                percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                DoGridTransferUpdateProgress(percentComplete, index);
                            }

                            if (blob.CopyState.Status == CopyStatus.Failed)
                            {
                                DoGridTransferDeclareError(index, blob.CopyState.StatusDescription);
                                Error = true;
                                break;
                            }

                            destinationBlob.FetchAttributes();
                            assetFile.ContentFileSize = sourceCloudBlob.Properties.Length;
                            assetFile.Update();

                            if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                            {
                                DoGridTransferDeclareError(index, "Error during blob copy.");
                                Error = true;
                                break;
                            }


                        }
                        catch (Exception ex)
                        {
                            TextBoxLogWriteLine("Failed to copy file '{0}'!.", fileName, true);
                            DoGridTransferDeclareError(index, ex);
                            Error = true;
                        }

                        BytesCopied += sourceCloudBlob.Properties.Length;
                        percentComplete = (long)100 * (long)BytesCopied / (long)Length;
                        if (!Error) DoGridTransferUpdateProgress(percentComplete, index);
                    }
                }

                asset.Update();

                destinationLocator.Delete();
                writePolicy.Delete();

                // Refresh the asset.
                asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();
                AssetInfo.SetISMFileAsPrimary(asset);
                if (!Error)
                {
                    DoGridTransferDeclareCompleted(index, asset.Id);
                }
                DoRefreshGridAssetV(false);

            }
            else // Use another storage account
            {
                // Create Media Services context.

                var externalStorageAccount = new CloudStorageAccount(new StorageCredentials(otherstoragename, otherstoragekey), _credentials.ReturnStorageSuffix(), true);
                var externalCloudBlobClient = externalStorageAccount.CreateCloudBlobClient();
                var externalMediaBlobContainer = externalCloudBlobClient.GetContainerReference(containername);

                TextBoxLogWriteLine("Starting the Azure Storage copy process.");

                externalMediaBlobContainer.CreateIfNotExists();

                // Get the SAS token to use for all blobs if dealing with multiple accounts
                string blobToken = externalMediaBlobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    // Specify the expiration time for the signature.
                    SharedAccessExpiryTime = DateTime.Now.AddDays(1),
                    // Specify the permissions granted by the signature.
                    Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read
                });

                IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy",
                  TimeSpan.FromDays(1), AccessPermissions.Write);
                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, asset, writePolicy);

                var destinationStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), _credentials.ReturnStorageSuffix(), true);
                var destBlobStorage = destinationStorageAccount.CreateCloudBlobClient();

                // Get the asset container URI and Blob copy from mediaContainer to assetContainer.
                string destinationContainerName = (new Uri(destinationLocator.Path)).Segments[1];

                CloudBlobContainer assetContainer =
                    destBlobStorage.GetContainerReference(destinationContainerName);

                CloudBlockBlob sourceCloudBlob, destinationBlob;
                long Length = 0;
                long BytesCopied = 0;
                string fileName;
                double percentComplete;

                //calculate size
                foreach (var sourceBlob in SelectedBlobs)
                {
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                    sourceCloudBlob = externalMediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    Length += sourceCloudBlob.Properties.Length;
                }

                // do the copy
                int nbblob = 0;
                bool Error = false;
                foreach (var sourceBlob in SelectedBlobs)
                {
                    nbblob++;
                    fileName = HttpUtility.UrlDecode(Path.GetFileName(sourceBlob.Uri.AbsoluteUri));

                    sourceCloudBlob = externalMediaBlobContainer.GetBlockBlobReference(fileName);
                    sourceCloudBlob.FetchAttributes();

                    if (sourceCloudBlob.Properties.Length > 0)
                    {
                        try
                        {
                            IAssetFile assetFile = asset.AssetFiles.Create(fileName);
                            destinationBlob = assetContainer.GetBlockBlobReference(fileName);

                            destinationBlob.DeleteIfExists();
                            destinationBlob.StartCopy(new Uri(sourceBlob.Uri.AbsoluteUri + blobToken));

                            while (destinationBlob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                destinationBlob.FetchAttributes();
                                percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + destinationBlob.CopyState.BytesCopied) / (long)Length;
                                DoGridTransferUpdateProgress(percentComplete, index);
                            }

                            if (destinationBlob.CopyState.Status == CopyStatus.Failed)
                            {
                                DoGridTransferDeclareError(index, destinationBlob.CopyState.StatusDescription);
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
                            DoGridTransferDeclareError(index, ex);
                            Error = true;
                            break;

                        }
                        BytesCopied += sourceCloudBlob.Properties.Length;
                        percentComplete = 100d * BytesCopied / Length;
                        if (!Error) DoGridTransferUpdateProgress(percentComplete, index);

                    }
                }

                asset.Update();
                destinationLocator.Delete();
                writePolicy.Delete();

                // Refresh the asset.
                asset = _context.Assets.Where(a => a.Id == asset.Id).FirstOrDefault();
                AssetInfo.SetISMFileAsPrimary(asset);

                if (!Error)
                {
                    DoGridTransferDeclareCompleted(index, asset.Id);
                }
                DoRefreshGridAssetV(false);

            }
        }


        private void ProcessExportAssetToAzureStorage(bool UseDefaultStorage, string containername, string otherstoragename, string otherstoragekey, List<IAssetFile> SelectedFiles, bool CreateNewContainer, int index)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            bool Error = false;
            if (UseDefaultStorage) // The default storage is used
            {
                TextBoxLogWriteLine("Starting the Azure export process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), _credentials.ReturnStorageSuffix(), true);
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
                        DoGridTransferDeclareError(index, string.Format("Failed to create container '{0}'. {1}", TargetContainer.Name, ex.Message));
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
                        nbblob++;
                        sourceCloudBlob = assetSourceContainer.GetBlockBlobReference(file.Name);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {
                            DoGridTransferUpdateProgress(100d * nbblob / SelectedFiles.Count, index);
                            try
                            {
                                destinationBlob = TargetContainer.GetBlockBlobReference(file.Name);
                                destinationBlob.DeleteIfExists();
                                destinationBlob.StartCopy(sourceCloudBlob);

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    blob.FetchAttributes();
                                    percentComplete = (long)100 * (long)(BytesCopied + blob.CopyState.BytesCopied) / (long)Length;
                                    DoGridTransferUpdateProgress((int)percentComplete, index);

                                }

                                if (blob.CopyState.Status == CopyStatus.Failed)
                                {
                                    DoGridTransferDeclareError(index, blob.CopyState.StatusDescription);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    DoGridTransferDeclareError(index, "Error during blob copy.");
                                    Error = true;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                DoGridTransferDeclareError(index, ex);
                                Error = true;
                            }
                            BytesCopied += sourceCloudBlob.Properties.Length;
                            percentComplete = (long)100 * (long)BytesCopied / (long)Length;
                            if (!Error) DoGridTransferUpdateProgress((int)percentComplete, index);

                        }
                    }

                    sourcelocator.Delete();


                    if (!Error)
                    {
                        DoGridTransferDeclareCompleted(index, TargetContainer.Uri.AbsoluteUri);
                    }
                    DoRefreshGridAssetV(false);
                }
            }
            else // Another storage is used
            {
                TextBoxLogWriteLine("Starting the blob copy process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount SourceStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), _credentials.ReturnStorageSuffix(), true);
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
                        DoGridTransferDeclareError(index, e);
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
                        nbblob++;
                        sourceCloudBlob = assetSourceContainer.GetBlockBlobReference(file.Name);
                        sourceCloudBlob.FetchAttributes();

                        if (sourceCloudBlob.Properties.Length > 0)
                        {
                            DoGridTransferUpdateProgress(100d * nbblob / SelectedFiles.Count, index);
                            try
                            {
                                destinationBlob = TargetContainer.GetBlockBlobReference(file.Name);
                                destinationBlob.DeleteIfExists();
                                destinationBlob.StartCopy(new Uri(sourceCloudBlob.Uri.AbsoluteUri + blobToken));

                                while (destinationBlob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    destinationBlob.FetchAttributes();
                                    percentComplete = 100d * (long)(BytesCopied + destinationBlob.CopyState.BytesCopied) / Length;
                                    DoGridTransferUpdateProgress(percentComplete, index);
                                }

                                if (destinationBlob.CopyState.Status == CopyStatus.Failed)
                                {
                                    DoGridTransferDeclareError(index, destinationBlob.CopyState.StatusDescription);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    DoGridTransferDeclareError(index, string.Format("Failed to copy file '{0}'", file.Name));
                                    Error = true;
                                    break;
                                }
                            }
                            catch (Exception e)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                DoGridTransferDeclareError(index, e);
                                Error = true;
                            }

                            BytesCopied += sourceCloudBlob.Properties.Length;
                            percentComplete = 100d * BytesCopied / Length;
                            if (!Error) DoGridTransferUpdateProgress(percentComplete, index);
                        }
                    }
                    sourcelocator.Delete();


                    if (!Error)
                    {
                        DoGridTransferDeclareCompleted(index, TargetContainer.Uri.AbsoluteUri);
                    }
                    DoRefreshGridAssetV(false);
                }
            }
        }

        private async void ProcessExportAssetToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, Dictionary<string, string> storagekeys, List<IAsset> SourceAssets, string TargetAssetName, int index, bool DeleteSourceAssets = false, bool CopyDynEnc = false, bool ReWriteLAURL = false, bool CloneAssetFilters = false)
        {
            // If upload in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            CloudMediaContext DestinationContext = Program.ConnectAndGetNewContext(DestinationCredentialsEntry);
            IAsset TargetAsset = DestinationContext.Assets.Create(TargetAssetName, DestinationStorageAccount, AssetCreationOptions.None);

            // let's backup the primary file from the first asset to set it to the copied/merged asset
            var ismAssetFile = SourceAssets.FirstOrDefault().AssetFiles.ToList().Where(f => f.IsPrimary).ToArray();

            bool ErrorCopyAsset = false;

            TextBoxLogWriteLine("Starting the asset copy process.");

            // let's get cloudblobcontainer for target
            CloudStorageAccount DestinationCloudStorageAccount =
                (DestinationStorageAccount == null) ?
                new CloudStorageAccount(new StorageCredentials(DestinationContext.DefaultStorageAccount.Name, storagekeys[DestinationContext.DefaultStorageAccount.Name]), DestinationCredentialsEntry.ReturnStorageSuffix(), true) :
                new CloudStorageAccount(new StorageCredentials(DestinationStorageAccount, storagekeys[DestinationStorageAccount]), DestinationCredentialsEntry.ReturnStorageSuffix(), true);

            var DestinationCloudBlobClient = DestinationCloudStorageAccount.CreateCloudBlobClient();
            IAccessPolicy writePolicy = DestinationContext.AccessPolicies.Create("writepolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
            ILocator DestinationLocator = DestinationContext.Locators.CreateLocator(LocatorType.Sas, TargetAsset, writePolicy);

            // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
            Uri targetUri = new Uri(DestinationLocator.Path);
            CloudBlobContainer DestinationCloudBlobContainer = DestinationCloudBlobClient.GetContainerReference(targetUri.Segments[1]);

            foreach (IAsset SourceAsset in SourceAssets) // there are several assets only if user wants to do a copy with merge
            {
                if (storagekeys.ContainsKey(SourceAsset.StorageAccountName))
                {
                    // let's get cloudblobcontainer for source
                    CloudStorageAccount SourceCloudStorageAccount = new CloudStorageAccount(new StorageCredentials(SourceAsset.StorageAccountName, storagekeys[SourceAsset.StorageAccountName]), _credentials.ReturnStorageSuffix(), true);
                    var SourceCloudBlobClient = SourceCloudStorageAccount.CreateCloudBlobClient();
                    IAccessPolicy readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                    ILocator SourceLocator = _context.Locators.CreateLocator(LocatorType.Sas, SourceAsset, readpolicy);

                    // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                    Uri sourceUri = new Uri(SourceLocator.Path);
                    CloudBlobContainer SourceCloudBlobContainer = SourceCloudBlobClient.GetContainerReference(sourceUri.Segments[1]);

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


                    // foreach (IAssetFile file in SourceAsset.AssetFiles) 
                    // For LIve archive, the folder for chiks are returnbed as files. So we detect this case and don't try to copy the folders as asset files
                    IEnumerable<IAssetFile> assetFilesToCopy = SourceAsset.AssetFiles.ToList();
                    if (
                        assetFilesToCopy.Where(af => af.Name.Contains(".")).Count() == 2
                        && assetFilesToCopy.Where(af => af.Name.ToUpper().EndsWith(".ISMC")).Count() == 1
                        && assetFilesToCopy.Where(af => af.Name.ToUpper().EndsWith(".ISM")).Count() == 1
                        ) // only 2 files with extensions, and these files are ISMC and ISM
                    {
                        assetFilesToCopy = SourceAsset.AssetFiles.ToList().Where(af => !af.Name.StartsWith("audio_") && !af.Name.StartsWith("video_") && !af.Name.StartsWith("scte35_"));
                        var assetFilesLiveFolders = SourceAsset.AssetFiles.ToList().Where(af => af.Name.StartsWith("audio_") || af.Name.StartsWith("video_") || af.Name.StartsWith("scte35_"));

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
                                        //destinationCloudBlockBlob.StartCopyFromBlob(file.GetSasUri());
                                        destinationCloudBlockBlob.StartCopy(file.GetSasUri());

                                        CloudBlockBlob blob;
                                        blob = (CloudBlockBlob)DestinationCloudBlobContainer.GetBlobReferenceFromServer(file.Name);

                                        while (blob.CopyState.Status == CopyStatus.Pending)
                                        {
                                            Task.Delay(TimeSpan.FromSeconds(0.5d)).Wait();
                                            blob.FetchAttributes();
                                            percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SourceAsset.AssetFiles.Count())) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                            DoGridTransferUpdateProgressText(string.Format("File '{0}'", file.Name), (int)percentComplete, index);
                                        }

                                        if (blob.CopyState.Status == CopyStatus.Failed)
                                        {
                                            DoGridTransferDeclareError(index, blob.CopyState.StatusDescription);
                                            ErrorCopyAssetFile = true;
                                            ErrorCopyAsset = true;
                                            break;
                                        }

                                        destinationCloudBlockBlob.FetchAttributes();
                                        destinationAssetFile.ContentFileSize = sourceCloudBlockBlob.Properties.Length;
                                        destinationAssetFile.Update();

                                        if (sourceCloudBlockBlob.Properties.Length != destinationCloudBlockBlob.Properties.Length)
                                        {
                                            DoGridTransferDeclareError(index, "Error during blob copy.");
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
                                DoGridTransferDeclareError(index, ex);
                                ErrorCopyAsset = true;
                                ErrorCopyAssetFile = true;
                            }
                            if (!ErrorCopyAssetFile) TextBoxLogWriteLine("File '{0}' copied.", file.Name);
                        }

                    }

                    if (!ErrorCopyAsset) // let's do the copy of additional fragblob if there are
                    {
                        List<CloudBlobDirectory> ListDirectories = new List<CloudBlobDirectory>();
                        // do the copy
                        nbblob = 0;
                        DoGridTransferUpdateProgressText(string.Format("fragblobs", SourceAsset.Name, DestinationCredentialsEntry.AccountName), 0, index);
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
                                            mylistresults.Add(targetBlob.StartCopyAsync(new Uri(blockblob.Uri.AbsoluteUri + SourceLocator.ContentAccessComponent)));
                                        }
                                    }
                                }
                                // let's launch the copy of fragblobs
                                double ind = 0;
                                foreach (var dir in ListDirectories)
                                {
                                    TextBoxLogWriteLine("Copying fragblobs directory '{0}'....", dir.Prefix);

                                    mylistresults.AddRange(AssetInfo.CopyBlobDirectory(dir, DestinationCloudBlobContainer, SourceLocator.ContentAccessComponent));//blobToken));
                                    if (mylistresults.Count > 0)
                                    {
                                        while (!mylistresults.All(r => r.IsCompleted))
                                        {
                                            Task.Delay(TimeSpan.FromSeconds(3d)).Wait();
                                            percentComplete = 100d * (ind + Convert.ToDouble(mylistresults.Where(c => c.IsCompleted).Count()) / Convert.ToDouble(mylistresults.Count)) / Convert.ToDouble(ListDirectories.Count);
                                            DoGridTransferUpdateProgressText(string.Format("fragblobs directory '{0}' ({1}/{2})", dir.Prefix, mylistresults.Where(r => r.IsCompleted).Count(), mylistresults.Count), (int)percentComplete, index);
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
                            DoGridTransferDeclareError(index, ex);
                            ErrorCopyAsset = true;
                        }
                    }

                    SourceLocator.Delete();
                    readpolicy.Delete();
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
            if (CopyDynEnc && !ErrorCopyAsset)
            {
                TextBoxLogWriteLine("Dynamic encryption settings copy...");
                try
                {
                    await DynamicEncryption.CopyDynamicEncryption(SourceAssets.FirstOrDefault(), TargetAsset, ReWriteLAURL);
                    TextBoxLogWriteLine("Dynamic encryption settings copied.");

                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when copying Dynamic encryption", true);
                    TextBoxLogWriteLine(ex);
                }
            }

            // Copy filters
            if (CloneAssetFilters && !ErrorCopyAsset && SourceAssets.FirstOrDefault().AssetFilters.Count() > 0)
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

            DestinationLocator.Delete();
            writePolicy.Delete();

            if (!ErrorCopyAsset)
            {
                if (DeleteSourceAssets) SourceAssets.ForEach(a => a.Delete());
                TextBoxLogWriteLine("Asset copy completed. The new asset in '{0}' has the Id :", DestinationCredentialsEntry.AccountName);
                TextBoxLogWriteLine(TargetAsset.Id);
                DoGridTransferDeclareCompleted(index, DestinationCloudBlobContainer.Uri.AbsoluteUri);
            }
            DoRefreshGridAssetV(false);
        }


        private void CheckListArchiveBlobs(Dictionary<string, string> storagekeys, IAsset SourceAsset, AssetInfo.ManifestSegmentsResponse manifestdata)
        {
            if (storagekeys.ContainsKey(SourceAsset.StorageAccountName))
            {
                TextBoxLogWriteLine("Starting the integrity check for asset {0}.", SourceAsset.Name);
                bool Error = false;

                TextBoxLogWriteLine("Checking video track segments in manifest...");
                int index = 0;
                foreach (var seg in manifestdata.videoSegments)
                {
                    if (seg.timestamp_mismatch)
                    {
                        TextBoxLogWriteLine("There is an overlap or gap issue in video track. Timestamp {0} calculation mismatch in manifest, index {1}", seg.timestamp, index, true);
                        Error = true;
                    }
                    index++;
                }

                TextBoxLogWriteLine("Checking audio track segments in manifest...");
                index = 0;
                int a_index = 0;
                foreach (var audiotrack in manifestdata.audioSegments)
                {
                    foreach (var seg in audiotrack)
                    {
                        if (seg.timestamp_mismatch)
                        {
                            TextBoxLogWriteLine("There is an overlap or gap issue in audio track {0}. Timestamp {1} calculation mismatch in manifest, index {2}", a_index, seg.timestamp, index, true);
                            Error = true;
                        }
                        index++;
                    }
                    a_index++;
                }


                TextBoxLogWriteLine("Checking blobs in storage...");


                // let's get cloudblobcontainer for source
                CloudStorageAccount SourceCloudStorageAccount = new CloudStorageAccount(new StorageCredentials(SourceAsset.StorageAccountName, storagekeys[SourceAsset.StorageAccountName]), _credentials.ReturnStorageSuffix(), true);
                var SourceCloudBlobClient = SourceCloudStorageAccount.CreateCloudBlobClient();
                IAccessPolicy readpolicy = _context.AccessPolicies.Create("readpolicy", TimeSpan.FromDays(1), AccessPermissions.Read);
                ILocator SourceLocator = _context.Locators.CreateLocator(LocatorType.Sas, SourceAsset, readpolicy);

                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                Uri sourceUri = new Uri(SourceLocator.Path);
                CloudBlobContainer SourceCloudBlobContainer = SourceCloudBlobClient.GetContainerReference(sourceUri.Segments[1]);

                var assetFilesLiveFolders = SourceAsset.AssetFiles.ToList().Where(af => af.Name.StartsWith("audio_") || af.Name.StartsWith("video_") || af.Name.StartsWith("scte35_"));

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
                    var audiodir = ListDirectories.Where(d => d.Prefix.StartsWith("audio_"));
                    var videodir = ListDirectories.Where(d => d.Prefix.StartsWith("video_")).Select(d => int.Parse(d.Prefix.Substring(6, d.Prefix.Length - 7)));
                    if (videodir.Count() != manifestdata.videoBitrates.Count)
                    {
                        TextBoxLogWriteLine("There are {0} video tracks in the manifest but {1} video directories in storage", manifestdata.videoBitrates.Count(), videodir.Count(), true);
                        Error = true;
                    }

                    if (audiodir.Count() != manifestdata.audioBitrates.GetLength(0))
                    {
                        TextBoxLogWriteLine("There are {0} audio tracks in the manifest but {1} audio directories in storage", manifestdata.audioBitrates.GetLength(0), audiodir.Count(), true);
                        Error = true;
                    }

                    var except = videodir.Except(manifestdata.videoBitrates);
                    if (except.Count() > 0)
                    {
                        TextBoxLogWriteLine("Some video directories in storage are not referenced as bitrate in the manifest. Bitrates : {0}", string.Join(",", except), true);
                        Error = true;
                    }
                    var exceptb = manifestdata.videoBitrates.Except(videodir);
                    if (exceptb.Count() > 0)
                    {
                        TextBoxLogWriteLine("Some bitrates in manifest cannot be found in storage as video directories. Bitrates : {0}", string.Join(",", exceptb), true);
                        Error = true;
                    }


                    // let's check the fragblobs
                    foreach (var dir in ListDirectories)
                    {

                        if (dir.Prefix.StartsWith("audio_") || dir.Prefix.StartsWith("video_"))
                        {
                            TextBoxLogWriteLine("Checking fragblobs in directory '{0}'....", dir.Prefix);

                            var srcBlobList = dir.ListBlobs(useFlatBlobListing: true, blobListingDetails: BlobListingDetails.None).ToList();
                            var listblobtimestamps = srcBlobList.Where(b => System.IO.Path.GetFileName(b.Uri.LocalPath) != "header").Select(b => ulong.Parse(System.IO.Path.GetFileName(b.Uri.LocalPath))).ToList().OrderBy(t => t).ToList();

                            List<AssetInfo.ManifestSegmentData> manifestdatacurrenttrack;

                            if (dir.Prefix.StartsWith("video_"))
                            {
                                manifestdatacurrenttrack = manifestdata.videoSegments;
                            }
                            else // audio
                            {
                                if (dir.Prefix.StartsWith("audio__"))  // let's get the index of audio track if it exists in directory name
                                                                       // there is an index "audio__0_64000" for example
                                {
                                    var split = dir.Prefix.Split('_');
                                    manifestdatacurrenttrack = manifestdata.audioSegments[int.Parse(split[2])].ToList();
                                }
                                else
                                {
                                    manifestdatacurrenttrack = manifestdata.audioSegments[0].ToList();
                                }
                            }

                            var timestampsinmanifest = manifestdatacurrenttrack.Select(a => a.timestamp).ToList();
                            var except2 = listblobtimestamps.Except(timestampsinmanifest);
                            if (except2.Count() > 0)
                            {
                                TextBoxLogWriteLine("Some segments in directory {0} are not in the manifest. Segments with timestamp: {1}", dir.Prefix, string.Join(",", except2), true);
                                Error = true;
                            }

                            var except3 = timestampsinmanifest.Except(listblobtimestamps);
                            if (except3.Count() > 0)
                            {
                                TextBoxLogWriteLine("Some segments in manifest are not in directory {0}. Segments with timestamp: {1}", dir.Prefix, string.Join(",", except3), true);
                                Error = true;
                            }

                            if (listblobtimestamps.Count < manifestdatacurrenttrack.Count) // mising blob in storage (header file)
                            {
                                TextBoxLogWriteLine("There are {0} segments in the manifest but only {1} segments in directory '{2}'", manifestdatacurrenttrack.Count, listblobtimestamps.Count, dir.Prefix, true);
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
                                        TextBoxLogWriteLine("There is an issue. Timestamp {0} in blob is different from timestamp {1} (defined) in manifest, in directory '{2}', index {3}", timestampinblob, seg.timestamp, dir.Prefix, index, true);
                                        Error = true;
                                        break;
                                    }
                                    else if (timestampinblob != seg.timestamp && seg.calculated)
                                    {
                                        TextBoxLogWriteLine("There is an issue. Timestamp {0} in blob is different from timestamp {1} (calculated) in manifest, in directory '{2}', index {3}", timestampinblob, seg.timestamp, dir.Prefix, index, true);
                                        Error = true;
                                        break;
                                    }
                                    index++;
                                }
                            }
                        }
                    }
                }
                SourceLocator.Delete();
                readpolicy.Delete();

                if (Error)
                {
                    TextBoxLogWriteLine("End of integrity check for asset {0}. Error(s) detected.", SourceAsset.Name);
                }
                else
                {
                    TextBoxLogWriteLine("End of integrity check for asset {0}. No error detected.", SourceAsset.Name);
                }
            }
            else
            {
                TextBoxLogWriteLine("Error storage key not found for asset '{0}'.", SourceAsset.Name, true);
            }
        }


        private async void ProcessCloneProgramToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, IProgram sourceProgram, bool CopyDynEnc, bool RewriteLAURL, bool CloneLocators, bool CloneAssetFilters)
        {
            TextBoxLogWriteLine("Starting the program cloning process.");
            CloudMediaContext DestinationContext = Program.ConnectAndGetNewContext(DestinationCredentialsEntry);

            // let's check that target channel exists
            IChannel clonedchannel = DestinationContext.Channels.Where(c => c.Name == sourceProgram.Channel.Name).FirstOrDefault();
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
            IAsset clonedAsset = DestinationContext.Assets.Create(sourceProgram.Asset.Name, DestinationStorageAccount, AssetCreationOptions.None);
            TextBoxLogWriteLine(string.Format("Cloned asset {0} created.", sourceProgram.Asset.Name));

            if (CopyDynEnc)
            {
                TextBoxLogWriteLine("Dynamic encryption settings copy...");
                try
                {
                    await DynamicEncryption.CopyDynamicEncryption(sourceProgram.Asset, clonedAsset, RewriteLAURL);
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
                        DestinationContext.Locators.CreateLocator(streamlocator.Id, LocatorType.OnDemandOrigin, clonedAsset, policy, streamlocator.StartTime);
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

            var STask = ProgramExecuteAsync(
              () =>
                  clonedchannel.Programs.CreateAsync(options),
                 sourceProgram.Name,
                 "created");
            await STask;

            TextBoxLogWriteLine(string.Format("Cloned program {0} created.", sourceProgram.Name));

        }


        private async void ProcessCloneChannelToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, IChannel sourceChannel)
        {
            TextBoxLogWriteLine("Starting the channel cloning process...");
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
            DoDeleteJobs(ReturnSelectedJobs());
        }

        private void DoDeleteJobs(List<IJob> SelectedJobs)
        {
            if (SelectedJobs.Count > 0)
            {
                string question = (SelectedJobs.Count == 1) ? "Delete " + SelectedJobs[0].Name + " ?" : "Delete these " + SelectedJobs.Count + " jobs ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Task.Run(async () =>
                    {
                        bool Error = false;
                        Task[] deleteTasks = SelectedJobs.ToList().Select(j => j.DeleteAsync()).ToArray();
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
            if (System.Windows.Forms.MessageBox.Show("Are you sure that you want to delete ALL the jobs ?", "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Task.Run(async () =>
                {
                    bool Error = false;
                    int skipSize = 0;
                    int batchSize = 1000;
                    int currentSkipSize = 0;


                    // let's build the tasks list
                    TextBoxLogWriteLine("Listing the jobs...");
                    List<Task> deleteTasks = new List<Task>();

                    while (true)
                    {
                        // Enumerate through all jobs (1000 at a time)
                        var listjobs = _context.Jobs.Skip(skipSize).Take(batchSize).ToList();
                        currentSkipSize += listjobs.Count;
                        deleteTasks.AddRange(listjobs.Select(a => a.DeleteAsync()).ToArray());

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

        private void silverlightMonitoringPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.LinkSMFHealth);
        }

        private void dASHIFHTML5ReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerDASHIFList);
        }

        private void iVXHLSPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.Player3IVXHLS);
        }



        private void encodeAssetWithDigitalRapidsKayakCloudEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithPremiumWorkflow();
        }

        private void DoMenuEncodeWithPremiumWorkflow()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            foreach (IAsset asset in SelectedAssets) // check that there is no blueprint in the selected assets
            {
                if (IsAWorkflow(asset))
                {
                    MessageBox.Show("One of the selected asset(s) is a workflow. Please select only video assets.");
                    return;
                }
            }

            List<IMediaProcessor> Encoders;
            Encoders = GetMediaProcessorsByName(Constants.AzureMediaEncoderPremiumWorkflow);
            Encoders.AddRange(GetMediaProcessorsByName(Constants.ZeniumEncoder));

            string taskname = "Premium Workflow Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvWorkflow;
            this.Cursor = Cursors.WaitCursor;
            EncodingPremium form = new EncodingPremium(_context)
            {
                EncodingPromptText = (SelectedAssets.Count > 1) ? "Input assets : " + SelectedAssets.Count + " assets have been selected." : "Input asset : '" + SelectedAssets.FirstOrDefault().Name + "'",
                EncodingProcessorsList = Encoders,
                EncodingJobName = "Premium Workflow Encoding of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + " - Premium Workflow encoded",
                EncodingNumberOfInputAssets = SelectedAssets.Count,
                EncodingPremiumWorkflowPresetXMLFiles = Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder,

            };
            form.JobOptions = new JobOptionsVar()
            {
                Priority = Properties.Settings.Default.DefaultJobPriority,
                StorageSelected = string.Empty,
                TasksOptionsSetting = TaskOptions.None, // we want to force this as encryption is not supported for empty string
                TasksOptionsSettingReadOnly = true,
                OutputAssetsCreationOptions = Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None
            };

            DialogResult dialogResult = form.ShowDialog();

            this.Cursor = Cursors.Arrow;

            if (dialogResult == DialogResult.OK)
            {
                // multiple jobs: one job for each input asset
                foreach (IAsset asset in SelectedAssets)
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name);

                    IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);
                    foreach (IAsset graphAsset in form.SelectedPremiumWorkflows) // for each workflow selected, we create a task
                    {
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);

                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   form.EncodingProcessorSelected,
                                   form.XMLData,
                                   form.JobOptions.TasksOptionsSetting
                                   );
                        // Specify the graph asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.Add(graphAsset);
                        task.InputAssets.Add(asset); // we add one asset
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);

                        task.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.StorageSelected, form.JobOptions.OutputAssetsCreationOptions);
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
                    dataGridViewJobsV.DoJobProgress(job);
                }
                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
            }
        }


        private void DoMenuEncodeWithAMESystemPreset()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            List<IMediaProcessor> Encoders;

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }
            DisplayDeprecatedMessageAME();

            CheckQuicktimeAndDisplayMessage(SelectedAssets);

            Encoders = GetMediaProcessorsByName(Constants.AzureMediaEncoder);

            EncodingAMEPreset form = new EncodingAMEPreset(_context)
            {
                EncodingOutputAssetName = Constants.NameconvInputasset + " - Azure Media encoded",
                Text = "Azure Media Encoding",
                EncodingLabel1 = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected. " + SelectedAssets.Count + " jobs will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingJobName = "Azure Media Encoding of " + Constants.NameconvInputasset,
                EncodingProcessorsList = Encoders,
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                string taskname = "Azure Media Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvAMEpreset;
                LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(
                    form.EncodingProcessorSelected,
                    SelectedAssets,
                    form.EncodingJobName,
                    form.JobOptions.Priority,
                    taskname,
                    form.EncodingOutputAssetName,
                    form.EncodingSelectedPreset,
                    form.JobOptions.OutputAssetsCreationOptions,
                    form.JobOptions.TasksOptionsSetting,
                    form.JobOptions.StorageSelected);
            }
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
        ///        "http://playready.directtaps.net/pr/svc/rightsmanager.asmx"
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



        /// <summary>
        /// Converts Smooth Stream to HLS.
        /// </summary>
        /// <param name="job">The job to which to add the new task.</param>
        /// <param name="asset">The Smooth Stream asset.</param>
        /// <param name="encrypt">
        /// If you want to encrypt the HLS to HLS with AES - 128, set the encrypt to true.
        /// The smoothStreamAsset parameter must contain a clear Smooth Stream.
        /// 
        /// If you want to encrypt the HLS to HLS with PlayReady, set the encrypt to false.
        /// The smoothStreamAsset parameter must contain Smooth Stream encrypted with PlayReady.
        /// </param>
        /// <returns>The asset that was packaged to HLS.</returns>


        private void DoMenuPackageSmoothToHLSStatic()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (mediaAsset == null) return;

            DisplayDeprecatedMessageStaticPackagers();

            if (!SelectedAssets.All(a => a.AssetType == AssetType.SmoothStreaming))
            {
                MessageBox.Show("Asset(s) should be in Smooth Streaming format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string jobname = "Smooth to " + Constants.NameconvFormathls + " packaging of " + Constants.NameconvInputasset;
            string taskname = "Smooth to " + Constants.NameconvFormathls + " packaging of " + Constants.NameconvInputasset;

            // Get the SDK extension method to  get a reference to the Azure Media Packager.
            IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                   MediaProcessorNames.WindowsAzureMediaPackager);

            HLSAESStatic form = new HLSAESStatic()
            {
                HLSEncrypt = false,
                HLSMaxBitrate = "6600000",
                HLSServiceSegment = "10",
                HLSKey = string.Empty,
                HLSKeyURL = string.Empty,
                HLSProcessorLabel = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                HLSLabel = (SelectedAssets.Count > 1) ? "Batch mode: " + SelectedAssets.Count + " assets have been selected." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be packaged to HLS as a new asset",
                HLSOutputAssetName = Constants.NameconvInputasset + "-Packaged to " + Constants.NameconvFormathls
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read and update the configuration XML.
                //
                string configHLS = LoadAndUpdateHLSConfiguration(Path.Combine(_configurationXMLFiles, @"MediaPackager_SmoothToHLS.xml"),
                form.HLSEncrypt, form.HLSKey, form.HLSKeyURL, form.HLSMaxBitrate, form.HLSServiceSegment);
                jobname = jobname.Replace(Constants.NameconvFormathls, form.HLSEncrypt ? "HLS/AES" : "HLS");
                taskname = taskname.Replace(Constants.NameconvFormathls, form.HLSEncrypt ? "HLS/AES" : "HLS");
                string outputassetname = form.HLSOutputAssetName.Replace(Constants.NameconvFormathls, form.HLSEncrypt ? "HLS/AES" : "HLS");
                LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(processor,
                    SelectedAssets,
                    jobname, Properties.Settings.Default.DefaultJobPriority,
                    taskname, outputassetname,
                    new List<string> { configHLS },
                    Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                    Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
            }
        }


        private void DoMenuMP4ToSmoothStatic()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0 || SelectedAssets.FirstOrDefault() == null)
            {
                MessageBox.Show("No asset was selected, or asset is null.");
            }
            else
            {
                DisplayDeprecatedMessageStaticPackagers();

                if (!SelectedAssets.All(a => a.AssetType == AssetType.MultiBitrateMP4 || a.AssetType == AssetType.MP4))
                {
                    MessageBox.Show("Asset(s) should be a multi bitrate or single MP4 file(s).", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                string labeldb = (SelectedAssets.Count > 1) ?
                    "Package these " + SelectedAssets.Count + " assets to Smooth Streaming ?" :
                    "Package '" + SelectedAssets.FirstOrDefault().Name + "' to Smooth Streaming ?";

                string jobname = "MP4 to Smooth Packaging of " + Constants.NameconvInputasset;
                string taskname = "MP4 to Smooth Packaging of " + Constants.NameconvInputasset;
                string outputassetname = Constants.NameconvInputasset + " - Packaged to Smooth";

                if (System.Windows.Forms.MessageBox.Show(labeldb, "Multi MP4 to Smooth", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                    IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                        MediaProcessorNames.WindowsAzureMediaPackager);

                    // Windows Azure Media Packager does not accept string presets, so load xml configuration
                    string smoothConfig = File.ReadAllText(Path.Combine(
                                _configurationXMLFiles,
                                "MediaPackager_MP4toSmooth.xml"));

                    LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(processor,
                        SelectedAssets,
                        jobname,
                        Properties.Settings.Default.DefaultJobPriority,
                        taskname,
                        outputassetname,
                        new List<string> { smoothConfig },
                        Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                        Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None
                        );
                }
            }
        }

        private void DoMenuVideoAnalytics(string processorStr, Image processorImage)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0 || SelectedAssets.FirstOrDefault() == null)
            {
                MessageBox.Show("No asset was selected, or asset is null.");
            }
            else
            {
                CheckSingleFileMP4MOVWMVExtension(SelectedAssets);

                // Get the SDK extension method to  get a reference to the processor.
                IMediaProcessor processor = GetLatestMediaProcessorByName(processorStr);

                var form = new VideoAnalyticsGeneric(_context, processor, processorImage, true)
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
                        new List<string> { @"{'Version':'1.0'}" },
                        form.JobOptions.OutputAssetsCreationOptions,
                        form.JobOptions.TasksOptionsSetting,
                        form.JobOptions.StorageSelected);
                }
            }
        }


        private void DoMenuProtectWithPlayReadyStatic()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }
            if (SelectedAssets.FirstOrDefault() == null) return;

            DisplayDeprecatedMessageStaticPackagers();

            if (!SelectedAssets.All(a => a.AssetType == AssetType.SmoothStreaming))
            {
                MessageBox.Show("Asset(s) should be in Smooth Streaming format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string jobname = "PlayReady Encryption of " + Constants.NameconvInputasset;
            string taskname = "PlayReady Encryption of " + Constants.NameconvInputasset;

            // Get the SDK extension method to  get a reference to the Windows Azure Media Encryptor.
            IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                MediaProcessorNames.WindowsAzureMediaEncryptor);

            PlayReadyStaticEnc form = new PlayReadyStaticEnc(_context)
            {
                PlayReadyProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                PlayReadyKeyId = Guid.NewGuid(),
                PlayReadyKeySeed = string.Empty,
                PlayReadyLAurl = string.Empty,
                PlayReadyUseSencBox = true,
                PlayReadyAdjustSubSamples = true,
                PlayReadyContentKey = string.Empty,
                PlayReadyServiceId = string.Empty,
                PlayReadyCustomAttributes = string.Empty,
                PlayReadyOutputAssetName = Constants.NameconvInputasset + " - PlayReady protected",
                PlayReadyAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected as an input. " + SelectedAssets.Count + " jobs will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encrypted with PlayReady."
            };
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string keyDeliveryServiceUri = form.PlayReadyLAurl;

                // Read and update the configuration XML.
                //
                string configPlayReady = LoadAndUpdatePlayReadyConfiguration(
                Path.Combine(_configurationXMLFiles, @"MediaEncryptor_PlayReadyProtection.xml"),
                form.PlayReadyKeySeed,
                keyDeliveryServiceUri,
                form.PlayReadyKeyId,
                form.PlayReadyContentKey,
                form.PlayReadyUseSencBox,
                form.PlayReadyAdjustSubSamples,
                form.PlayReadyServiceId,
                form.PlayReadyCustomAttributes);

                LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(processor,
                    SelectedAssets,
                    jobname,
                    Properties.Settings.Default.DefaultJobPriority,
                    taskname,
                    form.PlayReadyOutputAssetName,
                    new List<string> { configPlayReady },
                    AssetCreationOptions.CommonEncryptionProtected,
                    Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
            }

        }


        public void LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, int jobpriority, string taskname, string outputassetname, List<string> configuration, AssetCreationOptions myAssetCreationOptions, TaskOptions myTaskOptions, string storageaccountname = "")
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
                            myTask.OutputAssets.AddNew(outputassetnameloc, asset.StorageAccountName, myAssetCreationOptions); // let's use the same storage account than the input asset
                        }
                        else
                        {
                            myTask.OutputAssets.AddNew(outputassetnameloc, storageaccountname, myAssetCreationOptions);
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
                        Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(myJob));
                    }
                    TextBoxLogWriteLine("");
                }

                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);

            }

                );
        }


        public void LaunchJobs_OneJobPerInputAssetWithSpecificConfig(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, int jobpriority, string taskname, string outputassetname, List<string> configuration, AssetCreationOptions myAssetCreationOptions, TaskOptions myTaskOptions, string storageaccountname = "")
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
                        myTask.OutputAssets.AddNew(outputassetnameloc, asset.StorageAccountName, myAssetCreationOptions); // let's use the same storage account than the input asset
                    }
                    else
                    {
                        myTask.OutputAssets.AddNew(outputassetnameloc, storageaccountname, myAssetCreationOptions);
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
                        Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(myJob));
                    }
                    TextBoxLogWriteLine();
                }

                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);

            }

                );
        }


        private void DoMenuValidateMultiMP4Static()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            IAsset mediaAsset = SelectedAssets.FirstOrDefault();
            if (SelectedAssets.FirstOrDefault() == null) return;

            if (!SelectedAssets.All(a => a.AssetType == AssetType.MultiBitrateMP4))
            {
                MessageBox.Show("Asset(s) should be in multi bitrate MP4 format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            DisplayDeprecatedMessageStaticPackagers();

            string labeldb = "Validate '" + mediaAsset.Name + "'  ?";

            if (SelectedAssets.Count > 1)
            {
                labeldb = "Launch the validation of these " + SelectedAssets.Count + " assets ?";
            }
            string jobname = "Validate Multi MP4 of " + Constants.NameconvInputasset;
            string taskname = "Validate Multi MP4 of " + Constants.NameconvInputasset;
            string outputassetname = Constants.NameconvInputasset + " - Multi MP4 validated";


            if (System.Windows.Forms.MessageBox.Show(labeldb, "Multi MP4 Validation", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                // Read the task configuration data into a string. 
                string configMp4Validation = File.ReadAllText(Path.Combine(
                        _configurationXMLFiles,
                        "MediaPackager_ValidateTask.xml"));

                // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                    MediaProcessorNames.WindowsAzureMediaPackager);

                LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(processor,
                    SelectedAssets,
                    jobname,
                    Properties.Settings.Default.DefaultJobPriority,
                    taskname,
                    outputassetname,
                    new List<string> { configMp4Validation },
                    Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                    Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
            }
        }

        private void DisplayDeprecatedMessageStaticPackagers()
        {
            MessageBox.Show("Windows Azure Media Packager and Windows Azure Media Encryptor will reach end of life on March 1, 2016. At that time, these components will no longer be available.  The format conversion and encryption capabilities will be available through dynamic packaging and dynamic encryption.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DisplayDeprecatedMessageAME()
        {
            MessageBox.Show("Instead of using Azure Media Encoder, you should encode with \"Media Encoder Standard.\"\nIt provides better quality and performance, and it supports more input formats.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DoMenuIndexAssets()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            var proposedfiles = CheckSingleFileIndexerSupportedExtensions(SelectedAssets);

            // Get the SDK extension method to  get a reference to the Azure Media Indexer.
            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.AzureMediaIndexer);

            Indexer form = new Indexer(_context, processor.Version)
            {
                IndexerJobName = "Media Indexing of " + Constants.NameconvInputasset,
                IndexerOutputAssetName = Constants.NameconvInputasset + " - Indexed",

                IndexerInputAssetName = (SelectedAssets.Count > 1) ?
                SelectedAssets.Count + " assets have been selected for media indexing."
                :
                "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be indexed.",
            };

            string taskname = "Media Indexing of " + Constants.NameconvInputasset;

            if (form.ShowDialog() == DialogResult.OK)
            {
                var ListConfig = new List<string>();
                foreach (var asset in SelectedAssets)
                {
                    ListConfig.Add(
                                    Indexer.LoadAndUpdateIndexerConfiguration(
                                                                                Path.Combine(_configurationXMLFiles, @"MediaIndexer.xml"),
                                                                                form.IndexerTitle,
                                                                                form.IndexerDescription,
                                                                                form.IndexerLanguage,
                                                                                form.IndexerGenerationOptions,
                                                                                proposedfiles.ContainsKey(asset.Id) ? proposedfiles[asset.Id] : null
                                                                                )
                                                                                );

                }
                LaunchJobs_OneJobPerInputAssetWithSpecificConfig(
                            processor,
                            SelectedAssets,
                            form.IndexerJobName,
                            form.JobOptions.Priority,
                            taskname,
                            form.IndexerOutputAssetName,
                            ListConfig,
                            form.JobOptions.OutputAssetsCreationOptions,
                            form.JobOptions.TasksOptionsSetting,
                            form.JobOptions.StorageSelected
                                );

            }
        }

        private void DoMenuHyperlapseAssets()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0 || SelectedAssets.FirstOrDefault() == null)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            CheckSingleFileMP4MOVWMVExtension(SelectedAssets);

            // Get the SDK extension method to  get a reference to the Azure Media Indexer.
            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.AzureMediaHyperlapse);

            Hyperlapse form = new Hyperlapse(_context, processor.Version)
            {
                HyperlapseJobName = "Hyperlapse processing of " + Constants.NameconvInputasset,
                HyperlapseOutputAssetName = Constants.NameconvInputasset + " - Hyperlapsed",
                HyperlapseInputAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected for Hyperlapse processing." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be processed by Hyperlapse.",
            };

            string taskname = "Hyperlapse processing of " + Constants.NameconvInputasset;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string configHyperlapse = Hyperlapse.LoadAndUpdateHyperlapseConfiguration(
                Path.Combine(_configurationXMLFiles, @"Hyperlapse.xml"),
                form.HyperlapseStartFrame,
                form.HyperlapseNumFrames,
                form.HyperlapseSpeed
                );

                LaunchJobs_OneJobPerInputAsset_OneTaskPerfConfig(processor, SelectedAssets, form.HyperlapseJobName, form.JobOptions.Priority, taskname, form.HyperlapseOutputAssetName, new List<string> { configHyperlapse }, form.JobOptions.OutputAssetsCreationOptions, form.JobOptions.TasksOptionsSetting, form.JobOptions.StorageSelected);
            }
        }

        private static void CheckSingleFileMP4MOVWMVExtension(List<IAsset> SelectedAssets)
        {
            var mediaFileExtensions = new[] { ".MOV", ".WMV", ".MP4" };

            if (
                SelectedAssets.Any(a => a.AssetFiles.Count() != 1)
                ||
                SelectedAssets.Any(a => a.AssetFiles.Count() == 1 && (!mediaFileExtensions.Contains(Path.GetExtension(a.AssetFiles.FirstOrDefault().Name).ToUpperInvariant())))
                )
            {
                MessageBox.Show("Source asset must be a single MP4, MOV or WMV file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static Dictionary<string, string> CheckSingleFileIndexerSupportedExtensions(List<IAsset> SelectedAssets)
        {
            var mediaFileExtensions = new[] { ".MP4", ".WMV", ".MP3", ".M4A", ".WMA", ".AAC", ".WAV" };

            var IndexAnotherFile = new Dictionary<string, string>();

            if (SelectedAssets.Any(a => a.AssetFiles.Count() == 1 && !mediaFileExtensions.Contains(Path.GetExtension(a.AssetFiles.FirstOrDefault().Name).ToUpperInvariant())))
            {
                MessageBox.Show("If the input asset contains only one file, it must be a MP4, WMV, MP3, M4A, WMA, AAC or WAV file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        if (SelectedAssets.Count < 10)
                        {
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
                MessageBox.Show(string.Format("Asset '{0}' is a multi file asset and the primary file is not a MP4, WMV, MP3, M4A, WMA, AAC or WAV file.\nIndexing will probably fail.", assetnamepb), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MultiFileAssetPb > 1)
            {
                MessageBox.Show(string.Format("There are '{0}' assets which are multi files assets and the primary file is not a MP4, WMV, MP3, M4A, WMA, AAC or WAV file.\nIndexing will probably fail.", MultiFileAssetPb), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            MessageBox.Show("Please create a streaming locator in the Publish menu." + Constants.endline + Constants.endline + "Check that you have, at least, one Streaming endpoint scale Unit" + Constants.endline + "The asset should be:" + Constants.endline + "- a Smooth Streaming asset (Clear or PlayReady protected)," + Constants.endline + "- or a Clear Multi MP4 asset.", "Dynamic Packaging");
        }



        private void Mainform_Load(object sender, EventArgs e)
        {
            Hide();

            linkLabelFeedbackAMS.Links.Add(new LinkLabel.Link(0, linkLabelFeedbackAMS.Text.Length, Constants.LinkFeedbackAMS));

            comboBoxOrderAssets.Enabled = comboBoxStateAssets.Enabled = !largeAccount;
            //comboBoxOrderJobs.Enabled = _context.Jobs.Count() < triggerForLargeAccountNbJobs;


            toolStripStatusLabelWatchFolder.Visible = false;
            UpdateLabelStorageEncryption();

            comboBoxSearchAssetOption.Items.Add(new Item("Search in asset name :", SearchIn.AssetName.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Search for asset Id :", SearchIn.AssetId.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Search in asset alt Id :", SearchIn.AssetAltId.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Search in file name :", SearchIn.AssetFileName.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Search for file Id :", SearchIn.AssetFileId.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Search for locator Id :", SearchIn.LocatorId.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Search for program Id :", SearchIn.ProgramId.ToString()));
            comboBoxSearchAssetOption.Items.Add(new Item("Search in program name :", SearchIn.ProgramName.ToString()));
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

            comboBoxStateJobs.Items.AddRange(
            typeof(JobState)
            .GetFields()
            .Select(i => i.Name as string)
            .ToArray()
            );
            comboBoxStateJobs.Items[0] = "All";
            comboBoxStateJobs.SelectedIndex = 0;

            comboBoxStateAssets.Items.AddRange(
          typeof(StatusAssets)
          .GetFields()
          .Select(i => i.GetValue(null) as string)
          .ToArray()
          );
            comboBoxStateAssets.SelectedIndex = 0;

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


            comboBoxOrderProgram.Items.AddRange(
          typeof(OrderPrograms)
          .GetFields()
          .Select(i => i.GetValue(null) as string)
          .ToArray()
          );
            comboBoxOrderProgram.SelectedIndex = 0;


            comboBoxOrderChannel.Items.AddRange(
        typeof(OrderChannels)
        .GetFields()
        .Select(i => i.GetValue(null) as string)
        .ToArray()
        );
            comboBoxOrderChannel.SelectedIndex = 0;


            AddButtonsToSearchTextBox();

            // List of state and numbers of jobs per state

            DoRefreshGridJobV(true);
            DoGridTransferInit();
            DoRefreshGridIngestManifestV(true);
            DoRefreshGridAssetV(true);
            DoRefreshGridChannelV(true);
            DoRefreshGridProgramV(true);
            DoRefreshGridStreamingEndpointV(true);
            DoRefreshGridProcessorV(true);
            DoRefreshGridStorageV(true);
            DoRefreshGridFiltersV(true);

            // let's monitor channels or programs which are in "intermediate" state
            RestoreChannelsAndProgramsStatusMonitoring();

            dateTimePickerStartDate.Value = DateTime.Now.AddDays(-7d);
            dateTimePickerEndDate.Value = DateTime.Now;

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

        private void UpdateLabelStorageEncryption()
        {
            toolStripStatusLabelSE.Visible = Properties.Settings.Default.useStorageEncryption;
        }

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
                    c = _context.Jobs.Where(j => j.State == (JobState)Enum.Parse(typeof(JobState), filter)).Count();
                }
                if (c > 0) comboBoxStateJobs.Items[i] = string.Format("{0}  ({1})", filter, c);
                else comboBoxStateJobs.Items[i] = filter;
            }
        }

        private void createALocatorForTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();
            DoCreateLocator(SelectedAssets);
        }

        private void deleteAllLocatorsOfTheAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }

        private void DoMenuEncodeWithAMEAdvanced()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            List<IMediaProcessor> Encoders;

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            DisplayDeprecatedMessageAME();

            CheckQuicktimeAndDisplayMessage(SelectedAssets);

            string taskname = "Azure Media Encoding (adv) of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
            Encoders = GetMediaProcessorsByName(Constants.AzureMediaEncoder);

            EncodingAMEAdv form = new EncodingAMEAdv(_context)
            {
                EncodingLabel = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected. One job will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingProcessorsList = Encoders,
                EncodingJobName = "Azure Media Encoding (adv) of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + " - Azure Media encoded",
                EncodingAMEPresetXMLFiles = Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder,
                SelectedAssets = SelectedAssets
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read and update the configuration XML.
                //
                Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = form.EncodingAMEPresetXMLFiles;
                Program.SaveAndProtectUserConfig();

                string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);
                string tasknameloc = taskname.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvEncodername, form.EncodingProcessorSelected.Name + " v" + form.EncodingProcessorSelected.Version);
                ITask AMETask = job.Tasks.AddNew(
                    tasknameloc,
                  form.EncodingProcessorSelected,// processor,
                   form.EncodingConfiguration,
                   form.JobOptions.TasksOptionsSetting);

                AMETask.InputAssets.AddRange(form.SelectedAssets);

                // Add an output asset to contain the results of the job.  
                string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                AMETask.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.StorageSelected, form.JobOptions.OutputAssetsCreationOptions);

                // if UserControl wants also aboutToolStripMenuItem thumbnails task
                if (form.EncodingGenerateThumbnails)
                {
                    ITask AMETaskThumbnails = job.Tasks.AddNew(
                                       tasknameloc,
                                     form.EncodingProcessorSelected,// processor,
                                      "Thumbnails",
                                      form.JobOptions.TasksOptionsSetting
                                      );

                    AMETaskThumbnails.InputAssets.AddRange(form.SelectedAssets);

                    // Add an output asset to contain the results of the job.  
                    string outputassetnamelocthumbnails = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name) + " (Thumbnails)";
                    AMETaskThumbnails.OutputAssets.AddNew(outputassetnamelocthumbnails, form.JobOptions.StorageSelected, form.JobOptions.OutputAssetsCreationOptions);
                }
                // Submit the job and wait until it is completed. 
                try
                {
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    MessageBox.Show("There has been a problem when submitting the job " + jobnameloc + Constants.endline + Program.GetErrorMessage(e));
                    TextBoxLogWriteLine("There has been a problem when submitting the job {0}", jobnameloc, true);
                    TextBoxLogWriteLine(e);
                    return;
                }
                TextBoxLogWriteLine("Job '{0}' submitted", jobnameloc);
                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
                Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(job));
            }
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

                            string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);
                            task.OutputAssets.AddNew(outputassetnameloc, usertask.TaskOptions.StorageSelected, usertask.TaskOptions.OutputAssetsCreationOptions);
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
                        dataGridViewJobsV.DoJobProgress(job);
                    }
                }
                else if (form.EncodingCreationMode == TaskJobCreationMode.SingleJobForAllInputAssets) // Create one job for all inp
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
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, usertask.TaskOptions.StorageSelected, usertask.TaskOptions.OutputAssetsCreationOptions);
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
                    dataGridViewJobsV.DoJobProgress(job);

                }
                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
            }
        }

        private void butNextPageAsset_Click(object sender, EventArgs e)
        {
            if (comboBoxPageAssets.SelectedIndex < (comboBoxPageAssets.Items.Count - 1))
            {
                comboBoxPageAssets.SelectedIndex++;
                butPrevPageAsset.Enabled = true;
            }
            else butNextPageAsset.Enabled = false;

        }

        private void butPrevPageAsset_Click(object sender, EventArgs e)
        {
            if (comboBoxPageAssets.SelectedIndex > 0)
            {
                comboBoxPageAssets.SelectedIndex--;
                butNextPageAsset.Enabled = true;
            }
            else butPrevPageAsset.Enabled = false;
        }

        private void butNextPageJob_Click(object sender, EventArgs e)
        {
            if (comboBoxPageJobs.SelectedIndex < (comboBoxPageJobs.Items.Count - 1))
            {
                comboBoxPageJobs.SelectedIndex++;
                butPrevPageJob.Enabled = true;
            }
            else butNextPageJob.Enabled = false;
        }

        private void butPrevPageJob_Click(object sender, EventArgs e)
        {
            if (comboBoxPageJobs.SelectedIndex > 0)
            {
                comboBoxPageJobs.SelectedIndex--;
                butNextPageJob.Enabled = true;
            }
            else butPrevPageJob.Enabled = false;
        }

        private void encodeAssetWithAzureMediaEncoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMESystemPreset();
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
        }

        private void comboBoxPageJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedindex = ((ComboBox)sender).SelectedIndex;
            dataGridViewJobsV.DisplayPage(selectedindex + 1);

            butPrevPageJob.Enabled = (selectedindex == 0) ? false : true;
            butNextPageJob.Enabled = (selectedindex == (dataGridViewJobsV.PageCount - 1)) ? false : true;
        }

        private void dataGridViewAssetsV_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IAsset asset = AssetInfo.GetAsset(dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV.Columns["Id"].Index].Value.ToString(), _context);
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

        private void comboBoxPageAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedindex = ((ComboBox)sender).SelectedIndex;
            dataGridViewAssetsV.DisplayPage(selectedindex + 1);
            butPrevPageAsset.Enabled = (selectedindex == 0) ? false : true;
            butNextPageAsset.Enabled = (selectedindex == (dataGridViewAssetsV.PageCount - 1)) ? false : true;
        }

        private void dataGridViewJobsV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < dataGridViewJobsV.Columns["Progress"].Index)
            {
                var celljobstatevalue = dataGridViewJobsV.Rows[e.RowIndex].Cells[dataGridViewJobsV.Columns["State"].Index].Value;

                if (celljobstatevalue != null)
                {
                    JobState JS = (JobState)celljobstatevalue;
                    Color mycolor;

                    switch (JS)
                    {
                        case JobState.Error:
                            mycolor = Color.Red;
                            break;
                        case JobState.Canceled:
                            mycolor = Color.Blue;
                            break;
                        case JobState.Canceling:
                            mycolor = Color.Blue;
                            break;
                        case JobState.Processing:
                            mycolor = Color.DarkGreen;
                            break;
                        case JobState.Queued:
                            mycolor = Color.Green;
                            break;
                        default:
                            mycolor = Color.Black;
                            break;
                    }
                    e.CellStyle.ForeColor = mycolor;
                }
            }
        }

        private void dataGridViewJobsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IJob job = GetJob(dataGridViewJobsV.Rows[e.RowIndex].Cells[dataGridViewJobsV.Columns["Id"].Index].Value.ToString());
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

            //Debug.Print("cellformatting" + e.RowIndex + " " + e.ColumnIndex);

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
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._statEnc].Index)  // Mouseover for icons
            {

                var cell3 = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._statEncMouseOver].Value != null)
                    cell3.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._statEncMouseOver].Value.ToString();
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
            DisplayInfo(ReturnSelectedAssets().FirstOrDefault());
        }

        private void contextMenuStripAssets_Opening(object sender, CancelEventArgs e)
        {
            var assets = ReturnSelectedAssets();
            bool singleitem = (assets.Count == 1);
            var firstAsset = assets.FirstOrDefault();

            ContextMenuItemAssetDisplayInfo.Enabled =
            ContextMenuItemAssetRename.Enabled =
            contextMenuExportFilesToStorage.Enabled =
            createAnAssetFilterToolStripMenuItem.Enabled =
            displayParentJobToolStripMenuItem1.Enabled = singleitem;

            if (singleitem && firstAsset != null && firstAsset.AssetFiles.Count() == 1)
            {
                var assetfile = firstAsset.AssetFiles.FirstOrDefault();
                if (assetfile != null && assetfile.Name.EndsWith(".ism") && assetfile.ContentFileSize == 0)
                {
                    // live archive
                    contextMenuExportFilesToStorage.Enabled = false;
                    contextMenuExportDownloadToLocal.Enabled = false;
                }
            }
        }


        private void toolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            DoMenuRenameAsset();
        }


        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DoMenuDeleteSelectedAssets();
        }


        private void toolStripMenuAsset_DropDownOpening(object sender, EventArgs e)
        {
            var assets = ReturnSelectedAssets();
            bool singleitem = (assets.Count == 1);
            informationToolStripMenuItem.Enabled =
            renameToolStripMenuItem.Enabled =
            toAzureStorageToolStripMenuItem.Enabled =
            displayParentJobToolStripMenuItem.Enabled = singleitem;

            if (singleitem && (assets.FirstOrDefault().AssetFiles.Count() == 1))
            {
                var assetfile = assets.FirstOrDefault().AssetFiles.FirstOrDefault();
                if (assetfile.Name.EndsWith(".ism") && assetfile.ContentFileSize == 0)
                {
                    // live archive
                    toAzureStorageToolStripMenuItem.Enabled = false;
                    downloadToLocalToolStripMenuItem1.Enabled = false;
                }
            }
        }

        private void toolStripMenuJobDisplayInfo_Click(object sender, EventArgs e)
        {
            DisplayInfo(ReturnSelectedJobs().FirstOrDefault());
        }

        private void toolStripMenuJobsCancel_Click(object sender, EventArgs e)
        {
            DoCancelJobs();
        }

        private void contextMenuStripJobs_Opening(object sender, CancelEventArgs e)
        {
            bool singleitem = (ReturnSelectedJobs().Count == 1);
            ContextMenuItemJobDisplayInfo.Enabled = singleitem;
        }

        private void toolStripMenuItemJobsDelete_Click(object sender, EventArgs e)
        {
            DoDeleteSelectedJobs();
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


        private void createReportEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateJobReportEmail();
        }


        private void DoCreateJobReportEmail()
        {
            JobInfo JR = new JobInfo(ReturnSelectedJobs());
            JR.CreateOutlookMail();
        }

        private void DoCopyJobReportToClipboard()
        {
            JobInfo JR = new JobInfo(ReturnSelectedJobs());
            JR.CopyStatsToClipBoard();
        }

        private void createOutlookReportEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateJobReportEmail();
        }


        private void DoMenuDisplayAssetInfoFromLocatorID()
        {
            string locatorID = string.Empty;
            string clipbs = Clipboard.GetText();
            if (clipbs != null && clipbs.StartsWith(Constants.LocatorIdPrefix))
            {
                locatorID = clipbs;
            }

            if (Program.InputBox("Locator ID/GUID", "Please enter the known Locator Id or GUID :", ref locatorID) == DialogResult.OK)
            {
                if (!locatorID.StartsWith(Constants.LocatorIdPrefix))
                {
                    locatorID = Constants.LocatorIdPrefix + locatorID;
                }
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

        private void displayInformationForAKnownJobIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayJobInfoFromKnownID();
        }

        private void DoMenuDisplayJobInfoFromKnownID()
        {
            string JobId = "";
            string clipbs = Clipboard.GetText();
            if (clipbs != null) if (clipbs.StartsWith(Constants.JobIdPrefix)) JobId = clipbs;

            if (Program.InputBox("Job ID", "Please enter the known Job Id :", ref JobId) == DialogResult.OK)
            {
                IJob KnownJob = GetJob(JobId);
                if (KnownJob == null)
                {
                    MessageBox.Show("This job has not been found.");
                }
                else if (DisplayInfo(KnownJob) == DialogResult.OK)
                {
                }
            }
        }

        private void DoMenuDisplayAssetInfoFromKnownID()
        {
            string AssetId = string.Empty;
            string clipbs = Clipboard.GetText();
            if (clipbs != null && clipbs.StartsWith(Constants.AssetIdPrefix))
            {
                AssetId = clipbs;
            }

            if (Program.InputBox("Asset ID", "Please enter the known Asset Id :", ref AssetId) == DialogResult.OK)
            {
                IAsset KnownAsset = AssetInfo.GetAsset(AssetId, _context);
                if (KnownAsset == null)
                {
                    MessageBox.Show("This asset has not been found.");
                }
                else
                {
                    DisplayInfo(KnownAsset);
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

                        default:
                            mycolor = Color.Black;
                            break;

                    }
                    for (int i = 0; i < dataGridViewTransfer.Columns.Count; i++) dataGridViewTransfer.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;

                }
            }
        }


        private void toolStripComboBoxNbItemsPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox CB = (ToolStripComboBox)sender;
            string sel = CB.SelectedItem.ToString();
            sel = sel.Substring(0, sel.IndexOf(" "));
            int nbitem = Convert.ToInt16(sel);

            dataGridViewAssetsV.AssetsPerPage = nbitem;
            dataGridViewJobsV.JobssPerPage = nbitem;
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

        private void comboBoxStateAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewAssetsV.Initialized)
            {
                Debug.WriteLine("comboBoxStateAssets_SelectedIndexChanged");

                string filter = ((ComboBox)sender).SelectedItem.ToString();

                dataGridViewAssetsV.StateFilter = filter;
                DoRefreshGridAssetV(false);
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
                            IAsset asset = AssetInfo.GetAsset(location, _context);
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

            if (dataGridViewTransfer.SelectedRows.Count > 0)
            {
                if ((TransferState)dataGridViewTransfer.SelectedRows[0].Cells[dataGridViewTransfer.Columns["State"].Index].Value == TransferState.Finished)
                {
                    toolStripMenuItemOpenDest.Enabled = true;
                }
            }
            else toolStripMenuItemOpenDest.Enabled = false;
        }

        private void priorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoChangeJobPriority();
        }

        private void DoChangeJobPriority()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();

            if (SelectedJobs.Count > 0)
            {
                Priority form = new Priority()
                {
                    JobPriority = (SelectedJobs.Count == 1) ? SelectedJobs[0].Priority : Properties.Settings.Default.DefaultJobPriority // if only one job so we pass the current priority to dialog box
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    foreach (IJob JobToProcess in SelectedJobs)

                        if (JobToProcess != null)
                        {
                            //delete
                            TextBoxLogWriteLine(string.Format("Changing priority to {0} for job '{1}'.", form.JobPriority, JobToProcess.Name));
                            try
                            {
                                JobToProcess.Priority = form.JobPriority;
                                JobToProcess.Update();
                            }

                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when changing priority for {0}.", JobToProcess.Name, true);
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
                    LabelMain = "Last Modified Time Range of Assets"
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


        private void encodeAssetsWithAzureMediaEncoderToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMEAdvanced();
        }

        private void toolStripMenuItemIndex_Click(object sender, EventArgs e)
        {
            DoMenuIndexAssets();
        }

        private void indexAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuIndexAssets();
        }



        private void withFlashOSMFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAzurePage);
        }



        private void withSilverlightMMPPFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightMonitoring);
        }



        private void withFlashOSMFToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void playbackToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

        }

        private bool IsThereALocatorValid(IAsset asset, ref ILocator locator, LocatorType mylocatortype = LocatorType.OnDemandOrigin)
        {
            bool valid = false;
            if (asset != null && asset.Locators.Count > 0)
            {
                ILocator LocatorQuery = asset.Locators.Where(l => (l.Type == mylocatortype) && ((l.StartTime < DateTime.UtcNow) || (l.StartTime == null)) && (l.ExpirationDateTime > DateTime.UtcNow)).FirstOrDefault();
                if (LocatorQuery != null)
                {
                    //OK we can play the content
                    locator = LocatorQuery;
                    valid = true;
                }

            }
            return valid;
        }

        private void withMPEGDASHIFRefPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.DASHIFRefPlayer);
        }


        private void withFlashOSMFAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAzurePage);
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightMonitoring);
        }

        private void withMPEGDASHIFReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.DASHIFRefPlayer);
        }

        private void withMPEGDASHAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.DASHAzurePage);
        }

        private void createOutlookReportEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }

        private void DoCreateAssetReportEmail()
        {
            AssetInfo AR = new AssetInfo(ReturnSelectedAssets());
            AR.CreateOutlookMail();
        }

        private void DoCopyAssetReportToClipboard()
        {
            AssetInfo AR = new AssetInfo(ReturnSelectedAssets());
            AR.CopyStatsToClipBoard();
        }

        private void createOutlookReportEmailToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }

        private void processAssetsadvancedModeWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuProcessGeneric();
        }

        private void processAssetsWithAProcessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuProcessGeneric();
        }

        private void openOutputAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(false);
        }

        private void DoOpenJobAsset(bool inputasset) // if false, then display first outputasset
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();
            if (SelectedJobs.Count != 0)
            {
                // Refresh the job.
                IJob JobToDisplayP2 = _context.Jobs.Where(j => j.Id == SelectedJobs.FirstOrDefault().Id).FirstOrDefault();
                if (JobToDisplayP2 != null)
                {
                    ReadOnlyCollection<IAsset> assetcol = inputasset ? JobToDisplayP2.InputMediaAssets : JobToDisplayP2.OutputMediaAssets;
                    if (assetcol.Count > 0)
                    {
                        if (assetcol.Count > 1) MessageBox.Show("There are " + assetcol.Count + " assets. Displaying only the first one.");
                        IAsset asset = assetcol.FirstOrDefault();
                        if (asset != null)
                        {
                            DisplayInfo(asset);
                        }
                    }
                }
            }
        }

        private void outputAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(false);
        }

        private void inputAssetInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoOpenJobAsset(true);
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

            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count == 1)
            {
                if (!havestoragecredentials)
                { // No blob credentials. Let's ask the user
                    if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
                    {
                        _credentials.StorageKey = valuekey;
                        havestoragecredentials = true;
                    }
                }
                if (havestoragecredentials) // if we have the storage credentials
                {
                    if (SelectedAssets.FirstOrDefault().Options == AssetCreationOptions.None && SelectedAssets.FirstOrDefault().StorageAccountName == _context.DefaultStorageAccount.Name) // Ok, the selected asset is not encrypyted
                    {
                        if (CopyAssetToAzure(ref UseDefaultStorage, ref containername, ref otherstoragename, ref otherstoragekey, ref SelectedFiles, ref CreateNewContainer, SelectedAssets.FirstOrDefault()) == DialogResult.OK)
                        {
                            int index = DoGridTransferAddItem("Export to Azure Storage " + (CreateNewContainer ? "to a new container" : "to an existing container"), TransferType.ExportToAzureStorage, Properties.Settings.Default.useTransferQueue);
                            // Start a worker thread that does copy.
                            Task.Factory.StartNew(() => ProcessExportAssetToAzureStorage(UseDefaultStorage, containername, otherstoragename, otherstoragekey, SelectedFiles, CreateNewContainer, index));
                            DotabControlMainSwitch(Constants.TabTransfers);
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

        private void downloadToLocalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuDownloadToLocal();
        }

        private void copyAssetFilesToAzureStorageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoExportAssetToAzureStorage();
        }

        private void setupAWatchFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoWatchFolder();
        }

        private void DoWatchFolder()
        {
            WatchFolder form = new WatchFolder(_context, ReturnSelectedAssets(), MyWatchFolderSettings)
            {
                WatchUseQueue = Properties.Settings.Default.useTransferQueue,
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                MyWatchFolderSettings = form.WatchFolderGetSettings;
                Properties.Settings.Default.useTransferQueue = form.WatchUseQueue;
                Program.SaveAndProtectUserConfig();


                if (!MyWatchFolderSettings.IsOn) // user want to stop the watch folder (if if exists)
                {
                    if (MyWatchFolderSettings.Watcher != null)
                    {
                        MyWatchFolderSettings.Watcher.EnableRaisingEvents = false;
                        MyWatchFolderSettings.Watcher = null;
                    }
                    toolStripStatusLabelWatchFolder.Visible = false;

                }
                else // User wants to active the watch folder
                {
                    if (MyWatchFolderSettings.Watcher == null)
                    {
                        // Create a new FileSystemWatcher and set its properties.
                        MyWatchFolderSettings.Watcher = new FileSystemWatcher();


                        /* For later development : use notification queue
                        //create notifcation queue
                        //create the cloud storage account from name and private key


                        // TO DO: check that storage key exist
                        CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), _credentials.ReturnStorageSuffix(), true);


                        //create the cloud queue client from the storage connection string
                        CloudQueueClient cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();

                        //get a cloud queue reference
                        CloudQueue notificationsQueue = cloudQueueClient.GetQueueReference(Constants.AzureNotificationNameWatchFolder);

                        //create the queue if it does not exist
                        notificationsQueue.CreateIfNotExists();
                        

                        //create a notification endpoint and store it the glbal variable
                        MyWatchFolderSettings.NotificationEndPoint =
                            _context.NotificationEndPoints
                                .Create("notificationendpoint", NotificationEndPointType.AzureQueue, Constants.AzureNotificationNameWatchFolder);
                         */

                    }

                    MyWatchFolderSettings.Watcher.Path = MyWatchFolderSettings.FolderPath;
                    /* Watch for changes in LastAccess and LastWrite times, and
                       the renaming of files or directories. */
                    MyWatchFolderSettings.Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                       | NotifyFilters.FileName; //| NotifyFilters.DirectoryName;
                    // Only watch text files.
                    MyWatchFolderSettings.Watcher.Filter = "*.*";
                    MyWatchFolderSettings.Watcher.IncludeSubdirectories = false;

                    // Begin watching.
                    MyWatchFolderSettings.Watcher.EnableRaisingEvents = true;
                    toolStripStatusLabelWatchFolder.Visible = true;

                    MyWatchFolderSettings.Watcher.Created += (s, e) =>
                    {
                        if (!this.seen.ContainsKey(e.FullPath)
                            || (DateTime.Now - this.seen[e.FullPath]) > this.seenInterval)
                        {
                            this.seen[e.FullPath] = DateTime.Now;
                            ThreadPool.QueueUserWorkItem(
                                this.WaitForCreatingProcessToCloseFileThenDoStuff, e.FullPath);
                        }
                    };
                }
            }
        }



        private void WaitForCreatingProcessToCloseFileThenDoStuff(object threadContext)
        {
            // Make sure the just-found file is done being
            // written by repeatedly attempting to open it
            // for exclusive access.
            var path = (string)threadContext;


            //detect whether its a directory or file

            DateTime started = DateTime.Now;
            DateTime lastLengthChange = DateTime.Now;
            long lastLength = 0;
            var noGrowthLimit = new TimeSpan(0, 5, 0);
            var notFoundLimit = new TimeSpan(0, 0, 1);

            for (int tries = 0; ; ++tries)
            {
                try
                {
                    // Do Stuff
                    Debug.WriteLine(path);

                    try
                    {
                        int index = DoGridTransferAddItem(string.Format("Watch folder: upload of file '{0}'", Path.GetFileName(path)), TransferType.UploadFromFile, Properties.Settings.Default.useTransferQueue);
                        // Start a worker thread that does uploading.
                        Task.Factory.StartNew(() => ProcessUploadFileAndMore(path, index, MyWatchFolderSettings));

                    }
                    catch (Exception e)
                    {
                        TextBoxLogWriteLine("Error: Could not read file from disk. Original error : ", true);
                        TextBoxLogWriteLine(e);
                    }
                    break;
                }
                catch (FileNotFoundException)
                {
                    // Sometimes the file appears before it is there.
                    if (DateTime.Now - started > notFoundLimit)
                    {
                        // Should be there by now
                        break;
                    }
                }
                catch (IOException ex)
                {
                    // mask in severity, customer, and code
                    var hr = (int)(ex.HResult & 0xA000FFFF);
                    if (hr != 0x80000020 && hr != 0x80000021)
                    {
                        // not a share violation or a lock violation
                        throw;
                    }
                }

                try
                {
                    var fi = new FileInfo(path);
                    if (fi.Length > lastLength)
                    {
                        lastLength = fi.Length;
                        lastLengthChange = DateTime.Now;
                    }
                }
                catch
                {
                }

                // still locked
                if (DateTime.Now - lastLengthChange > noGrowthLimit)
                {
                    // 5 minutes, still locked, no growth.
                    break;
                }

                Thread.Sleep(111);
            }
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
            Process.Start(_HelpFiles + "MediaServices.chm");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox myabout = new AboutBox();
            myabout.Show();
        }



        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            TabControl tabcontrol = (TabControl)sender;

            // let's enable or disable all items from menu and context menu
            EnableChildItems(ref publishToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets) || (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive))));
            EnableChildItems(ref processToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets)));
            EnableChildItems(ref assetToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets)));
            EnableChildItems(ref contextMenuStripAssets, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabAssets)));

            EnableChildItems(ref filterToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabFilters)));
            EnableChildItems(ref contextMenuStripFilters, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabFilters)));

            EnableChildItems(ref encodingToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabJobs)));
            EnableChildItems(ref contextMenuStripJobs, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabJobs)));

            EnableChildItems(ref transferToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabTransfers)));
            EnableChildItems(ref contextMenuStripTransfers, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabTransfers)));

            EnableChildItems(ref originToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabOrigins)));
            EnableChildItems(ref contextMenuStripStreaminEndpoints, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabOrigins)));

            EnableChildItems(ref liveChannelToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));
            EnableChildItems(ref contextMenuStripChannels, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));
            EnableChildItems(ref contextMenuStripPrograms, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));

            // let's disable Premium Workflow if not present
            if (!AMEPremiumWorkflowPresent)
            {
                encodeAssetWithPremiumWorkflowToolStripMenuItem.Enabled = false;  //menu
                ContextMenuItemPremiumWorkflow.Enabled = false; // mouse context menu
            }

            // let's disable FaceDetector if not present
            if (!AMFaceDetectorPresent)
            {
                ProcessFaceDetectortoolStripMenuItem.Enabled = false;
                toolStripMenuItemFaceDetector.Enabled = false;
            }

            // let's disable Motion Detector if not present
            if (!AMMotionDetectorPresent)
            {
                ProcessMotionDetectortoolStripMenuItem.Enabled = false;
                toolStripMenuItemMotionDetector.Enabled = false;
            }

            // let's disable Redactor if not present
            if (!AMRedactorPresent)
            {
                ProcessRedactortoolStripMenuItem.Enabled = false;
                toolStripMenuItemRedactor.Enabled = false;
            }

            // let's disable Stabilizer if not present
            if (!AMStabilizerPresent)
            {
                ProcessStabilizertoolStripMenuItem.Enabled = false;
                toolStripMenuItemStabilizer.Enabled = false;
            }

            // let's disable AME Std if not present
            if (!AMEStandardPresent)
            {
                encodeAssetsWithAMEStandardToolStripMenuItem.Visible = false;
                encodeAssetWithAMEStandardToolStripMenuItem.Visible = false;
                toolStripSeparator35.Visible = false;
                toolStripSeparator32.Visible = false;

                // subclipping
                subclipLiveStreamsarchivesToolStripMenuItem.Visible = false;
                subclipProgramsToolStripMenuItem.Visible = false;
                subclipToolStripMenuItem.Visible = false;
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


        private void buttonbuildchart_Click(object sender, EventArgs e)
        {
            chart.Series.Clear();

            var seriesJobTotal = new Series()
            {
                Name = "Jobs total",
                XValueType = ChartValueType.DateTime,
                ChartType = SeriesChartType.Line,
                MarkerStyle = MarkerStyle.Square,
                Color = Color.Black,
                IsValueShownAsLabel = false
            };
            chart.Series.Add(seriesJobTotal);

            var seriesError = new Series()
            {
                Name = "Jobs in error",
                XValueType = ChartValueType.DateTime,
                ChartType = SeriesChartType.Line,
                MarkerStyle = MarkerStyle.Square,
                Color = Color.Red,
                IsValueShownAsLabel = false
            };
            chart.Series.Add(seriesError);

            var seriesCancel = new Series()
            {
                Name = "Jobs cancelled",
                XValueType = ChartValueType.DateTime,
                ChartType = SeriesChartType.Line,
                MarkerStyle = MarkerStyle.Square,
                Color = Color.Blue,
                IsValueShownAsLabel = false
            };
            chart.Series.Add(seriesCancel);

            var seriesSucess = new Series()
            {
                Name = "Jobs succeeded",
                XValueType = ChartValueType.DateTime,
                ChartType = SeriesChartType.Line,
                MarkerStyle = MarkerStyle.Square,
                Color = Color.Green,
                IsValueShownAsLabel = false
            };
            chart.Series.Add(seriesSucess);

            IEnumerable<IJob> jobs = _context.Jobs.Where(j => j.LastModified >= dateTimePickerStartDate.Value && j.LastModified <= dateTimePickerEndDate.Value);

            var querytotal = jobs.GroupBy(j => ((DateTime)j.Created).Date).Select(j => new { number = j.Count(), date = (DateTime)j.Key }).ToList();
            var queryerror = jobs.Where(j => j.State == JobState.Error).GroupBy(j => ((DateTime)j.Created).Date).Select(j => new { number = j.Count(), date = (DateTime)j.Key }).ToList();
            var querycancel = jobs.Where(j => j.State == JobState.Canceled).GroupBy(j => ((DateTime)j.Created).Date).Select(j => new { number = j.Count(), date = (DateTime)j.Key }).ToList();
            var querysuccess = jobs.Where(j => j.State == JobState.Finished).GroupBy(j => ((DateTime)j.Created).Date).Select(j => new { number = j.Count(), date = (DateTime)j.Key }).ToList();

            DateTime day = dateTimePickerStartDate.Value.Date;

            int val;
            while (day <= dateTimePickerEndDate.Value.Date)
            {
                if (querytotal.Where(d => d.date == day).FirstOrDefault() == null) val = 0; else val = querytotal.Where(d => d.date == day).FirstOrDefault().number;
                DrawPoint(seriesJobTotal, "(total)", day, val);

                if (queryerror.Where(d => d.date == day).FirstOrDefault() == null) val = 0; else val = queryerror.Where(d => d.date == day).FirstOrDefault().number;
                DrawPoint(seriesError, "in error", day, val);

                if (querycancel.Where(d => d.date == day).FirstOrDefault() == null) val = 0; else val = querycancel.Where(d => d.date == day).FirstOrDefault().number;
                DrawPoint(seriesCancel, "canceled", day, val);

                if (querysuccess.Where(d => d.date == day).FirstOrDefault() == null) val = 0; else val = querysuccess.Where(d => d.date == day).FirstOrDefault().number;
                DrawPoint(seriesSucess, "finished", day, val);

                day = day.AddDays(1);
            }

            // draw!
            chart.Invalidate();
        }

        private static void DrawPoint(Series seriestoprocess, string word, DateTime day, int val)
        {
            int pos = seriestoprocess.Points.AddXY(day, val);
            if (val != 0)
            {
                seriestoprocess.Points[pos].IsValueShownAsLabel = true;
                seriestoprocess.Points[pos].LabelToolTip = string.Format("{0} jobs {1} on {2}", val, word, day.ToShortDateString());
            }
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
                UpdateLabelStorageEncryption();
            }
        }

        private void ApplySettingsOptions(bool init = false)
        {
            if (!init)
            {
                dataGridViewAssetsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
                dataGridViewAssetsV.Columns["Storage"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
                dataGridViewJobsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayJobIDinGrid;
                dataGridViewChannelsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveChannelIDinGrid;
                dataGridViewProgramsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayLiveProgramIDinGrid;
                dataGridViewStreamingEndpointsV.Columns["Id"].Visible = Properties.Settings.Default.DisplayOriginIDinGrid;
            }

            dataGridViewAssetsV.AssetsPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid;
            dataGridViewJobsV.JobssPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid;

            TimerAutoRefresh.Interval = Properties.Settings.Default.AutoRefreshTime * 1000;
            TimerAutoRefresh.Enabled = Properties.Settings.Default.AutoRefresh;
            withCustomPlayerToolStripMenuItem.Visible = Properties.Settings.Default.CustomPlayerEnabled;
            withCustomPlayerToolStripMenuItem1.Visible = Properties.Settings.Default.CustomPlayerEnabled;
            withCustomPlayerToolStripMenuItem2.Visible = Properties.Settings.Default.CustomPlayerEnabled;
        }


        private void DoRefreshGridChannelV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewChannelsV.Init(_credentials, _context);
            }

            dataGridViewChannelsV.Invoke(new Action(() => dataGridViewChannelsV.RefreshChannels(_context, 1)));
            var count = _context.Channels.Count();
            tabPageLive.Invoke(new Action(() => tabPageLive.Text = string.Format(Constants.TabLive + " ({0}/{1})", dataGridViewChannelsV.DisplayedCount, count)));
            labelChannels.Invoke(new Action(() => labelChannels.Text = string.Format(Constants.LabelChannel + " ({0}/{1})", dataGridViewChannelsV.DisplayedCount, count)));
        }

        private void DoRefreshGridProgramV(bool firstime)
        {

            if (firstime)
            {
                Debug.WriteLine("DoRefreshGridProgramVforsttime");
                dataGridViewProgramsV.Init(_credentials, _context);
            }
            else
            {
                Debug.WriteLine("DoRefreshGridProgramVNotforsttime");
            }

            int backupindex = 0;
            dataGridViewProgramsV.Invoke(new Action(() => dataGridViewProgramsV.RefreshPrograms(_context, backupindex + 1)));
            labelPrograms.Invoke(new Action(() => labelPrograms.Text = string.Format(Constants.LabelProgram + " ({0}/{1})", dataGridViewProgramsV.DisplayedCount, _context.Programs.Count())));

        }

        private void DoRefreshGridStreamingEndpointV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewStreamingEndpointsV.Init(_credentials, _context);

            }
            Debug.WriteLine("DoRefreshGridOriginsVNotforsttime");
            dataGridViewStreamingEndpointsV.Invoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoints(_context)));

            tabPageAssets.Invoke(new Action(() => tabPageOrigins.Text = string.Format(Constants.TabOrigins + " ({0})", dataGridViewStreamingEndpointsV.DisplayedCount)));
        }

        private void DoRefreshGridProcessorV(bool firstime)
        {
            if (firstime)
            {
                // Processors tab
                dataGridViewProcessors.ColumnCount = 5;
                dataGridViewProcessors.Columns[0].HeaderText = "Vendor";
                dataGridViewProcessors.Columns[0].Width = 82;
                dataGridViewProcessors.Columns[1].HeaderText = "Name";
                dataGridViewProcessors.Columns[1].Width = 222;
                dataGridViewProcessors.Columns[2].HeaderText = "Version";
                dataGridViewProcessors.Columns[2].Width = 65;
                dataGridViewProcessors.Columns[3].HeaderText = "Id";
                dataGridViewProcessors.Columns[3].Width = 230;
                dataGridViewProcessors.Columns[4].HeaderText = "Description";
                dataGridViewProcessors.Columns[4].Width = 390;
            }
            dataGridViewProcessors.Rows.Clear();
            List<IMediaProcessor> Procs = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList();
            foreach (IMediaProcessor proc in Procs)
            {
                dataGridViewProcessors.Rows.Add(proc.Vendor, proc.Name, proc.Version, proc.Id, proc.Description);
            }
            tabPageProcessors.Text = string.Format(Constants.TabProcessors + " ({0})", Procs.Count());

            // Media Reserved Unit(s)
            if (MediaRUFeatureOn)
            {
                comboBoxEncodingRU.Items.Clear();

                foreach (var unit in Enum.GetValues(typeof(ReservedUnitType)))
                {
                    comboBoxEncodingRU.Items.Add(new Item(Program.ReturnMediaReservedUnitName((ReservedUnitType)unit), unit.ToString()));
                }

                // let's set the selected item
                string currentype = _context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType.ToString();
                foreach (var it in comboBoxEncodingRU.Items)
                {
                    if (((Item)it).Value == currentype)
                    {
                        comboBoxEncodingRU.SelectedItem = it;
                    }
                }

                trackBarEncodingRU.Maximum = _context.EncodingReservedUnits.FirstOrDefault().MaxReservableUnits;
                trackBarEncodingRU.Value = _context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits;
                UpdateLabelProcessorUnits();

                if (_context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits == 0)
                {
                    toolStripStatusLabelEncRU.Text = string.Format("No Media Reserved Unit");
                }
                else
                {
                    string s = _context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits > 1 ? "s" : "";
                    toolStripStatusLabelEncRU.Text = string.Format("{0} {1} Media Reserved Unit{2}", _context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits, Program.ReturnMediaReservedUnitName(_context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType), s);
                }
            }

            else
            {
                comboBoxEncodingRU.Enabled = trackBarEncodingRU.Enabled = buttonUpdateEncodingRU.Enabled = false;
                toolStripStatusLabelEncRU.Text = string.Format("No encoding on this account");
            }
        }

        private void DoRefreshGridStorageV(bool firstime)
        {
            const long OneTBInByte = 1099511627776;
            const long TotalStorageInBytes = OneTBInByte * 500;

            if (firstime)
            {
                // Storage tab
                dataGridViewStorage.ColumnCount = 3;

                DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
                {
                    Name = "% used",
                    DataPropertyName = "% used",
                    HeaderText = "% used"
                };
                dataGridViewStorage.Columns.Add(col);

                dataGridViewStorage.Columns[0].HeaderText = "Name";
                dataGridViewStorage.Columns[0].Width = 280;
                dataGridViewStorage.Columns[1].HeaderText = "Used space";
                dataGridViewStorage.Columns[1].Width = 100;
                dataGridViewStorage.Columns[2].Name = "StrictName";
                dataGridViewStorage.Columns[2].Visible = false;
                dataGridViewStorage.Columns[3].Width = 600;
            }
            dataGridViewStorage.Rows.Clear();
            List<IStorageAccount> Storages = _context.StorageAccounts.ToList().OrderByDescending(p => p.IsDefault).ThenBy(p => p.Name).ToList();
            foreach (IStorageAccount storage in Storages)
            {
                bool displaycapacity = false;
                double? capacityPercentageFullTmp = null;
                if (storage.BytesUsed != null)
                {
                    displaycapacity = true;
                    capacityPercentageFullTmp = (double)((100 * (double)storage.BytesUsed) / (double)TotalStorageInBytes);
                }

                int rowi = dataGridViewStorage.Rows.Add(storage.Name + (string)((storage.IsDefault) ? " (default)" : string.Empty), displaycapacity ? AssetInfo.FormatByteSize(storage.BytesUsed) : "(are the metrics enabled ?)", storage.Name, displaycapacity ? capacityPercentageFullTmp : null);
                if (storage.IsDefault)
                {
                    dataGridViewStorage.Rows[rowi].Cells[0].Style.ForeColor = Color.Blue;
                    dataGridViewStorage.Rows[rowi].Cells[0].ToolTipText = "Default storage account";
                }
                if (!displaycapacity)
                {
                    dataGridViewStorage.Rows[rowi].Cells[1].ToolTipText = "Storage Account Metrics are not enabled or no data is available";
                }
            }
            tabPageStorage.Text = string.Format(Constants.TabStorage + " ({0})", Storages.Count());
        }


        public void DoRefreshGridFiltersV(bool firstime)
        {
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
            //List<Filter> filters = _contextdynmanifest.ListGlobalFilters();

            foreach (var filter in _context.Filters)
            {
                string s = null;
                string e = null;
                string d = null;
                string l = null;

                if (filter.PresentationTimeRange != null)
                {
                    ulong? start = filter.PresentationTimeRange.StartTimestamp;
                    ulong? end = filter.PresentationTimeRange.EndTimestamp;
                    TimeSpan? dvr = filter.PresentationTimeRange.PresentationWindowDuration;
                    TimeSpan? live = filter.PresentationTimeRange.LiveBackoffDuration;

                    if (filter.PresentationTimeRange.Timescale != null)
                    {
                        double dscale = (double)filter.PresentationTimeRange.Timescale / (double)TimeSpan.TicksPerSecond;
                        if (start != null)
                        {
                            start = (ulong)((double)start / dscale);
                        }
                        if (end != null)
                        {
                            end = (ulong)((double)end / dscale);
                        }
                    }

                    //double scale = Convert.ToDouble(filter.PresentationTimeRange.Timescale) / 10000000;

                    s = (start != null) ? TimeSpan.FromTicks((long)start).ToString(@"d\.hh\:mm\:ss") : "min";
                    e = (end != null) ? TimeSpan.FromTicks((long)end).ToString(@"d\.hh\:mm\:ss") : "max";

                    d = (dvr != null) ? ((TimeSpan)dvr).ToString(@"d\.hh\:mm\:ss") : "max";
                    l = (live != null) ? ((TimeSpan)live).ToString(@"d\.hh\:mm\:ss") : "min";
                }
                int rowi = dataGridViewFilters.Rows.Add(filter.Name, filter.Tracks.Count, s, e, d, l);
            }
            tabPageFilters.Text = string.Format(Constants.TabFilters + " ({0})", _context.Filters.Count());
        }


        private List<IChannel> ReturnSelectedChannels()
        {
            List<IChannel> SelectedChannels = new List<IChannel>();
            foreach (DataGridViewRow Row in dataGridViewChannelsV.SelectedRows)
            {
                // sometimes, the channel can be null (if just deleted)
                var channel = _context.Channels.Where(j => j.Id == Row.Cells[dataGridViewChannelsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault();
                if (channel != null)
                {
                    SelectedChannels.Add(channel);
                }
            }
            SelectedChannels.Reverse();
            return SelectedChannels;
        }
        private List<IStreamingEndpoint> ReturnSelectedStreamingEndpoints()
        {
            List<IStreamingEndpoint> SelectedOrigins = new List<IStreamingEndpoint>();
            foreach (DataGridViewRow Row in dataGridViewStreamingEndpointsV.SelectedRows)
            {
                var se = _context.StreamingEndpoints.Where(j => j.Id == Row.Cells[dataGridViewStreamingEndpointsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault();
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
            foreach (DataGridViewRow Row in dataGridViewProgramsV.SelectedRows)
            {
                var program = _context.Programs.Where(j => j.Id == Row.Cells[dataGridViewProgramsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault();
                if (program != null)
                {
                    SelectedPrograms.Add(program);
                }
            }
            SelectedPrograms.Reverse();
            return SelectedPrograms;
        }

        private async void DoStopChannels()
        {
            List<IChannel> channels = ReturnSelectedChannels();
            List<string> ListChannelIDs = channels.Select(c => c.Id).ToList();
            var programquery = _context.Programs.AsEnumerable().Where(p => ListChannelIDs.Contains(p.ChannelId) && p.State != ProgramState.Stopped);
            var programqueryrunning = programquery.Where(p => p.State == ProgramState.Running);

            if (programquery.Where(p => p.State == ProgramState.Starting || p.State == ProgramState.Stopping).Count() > 0) // programs are starting or stopping
            {
                MessageBox.Show("Some programs are starting or stopping. Channel(s) cannot be stopped now.", "Channel(s) stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TextBoxLogWriteLine("Some programs are starting or stopping. Channel(s) cannot be stopped now.", true);
            }
            else
            {
                if (programqueryrunning.Count() > 0) // some programs are running
                {
                    if (MessageBox.Show("One or several programs are running. Do you want to stop the program(s) and then the channel(s) ?", "Channel stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Task.Run(async () =>
                         {
                             // let's stop the running programs
                             var tasks = programqueryrunning.Select(p => StopProgramASync(p)).ToArray();
                             await Task.WhenAll(tasks);

                             // let's stop the channels now that running programs are stopped
                             var tasksstop = channels.Select(c => StopChannelAsync(c)).ToArray();
                             await Task.WhenAll(tasksstop);
                         }
                         );
                    }
                }
                else
                {
                    string question = (channels.Count == 1) ? string.Format("Stop channel '{0}' ?", channels[0].Name) : string.Format("Stop these {0} channels ?", channels.Count);

                    if (System.Windows.Forms.MessageBox.Show(question, "Channel stop", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // let's stop the channels now that running programs are stopped
                        Task.Run(async () =>
                         {
                             // let's stop the channels now
                             var tasksstop = channels.Select(c => StopChannelAsync(c)).ToArray();
                             await Task.WhenAll(tasksstop);
                         }
                    );
                    }
                }
            }
        }

        private void DoStartChannels()
        {
            // let's stop the channels now that running programs are stopped
            Task.Run(async () =>
            {
                // let's stop the channels now
                var tasksstop = ReturnSelectedChannels().Select(c => StartChannelAsync(c)).ToArray();
                await Task.WhenAll(tasksstop);
            }
            );
        }



        // CHANNEL ASYNC OPERATIONS

        private async Task<IOperation> StartChannelAsync(IChannel myC)
        {
            TextBoxLogWriteLine("Channel '{0}' : starting...", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendStartOperationAsync, myC, "started", _context, this, dataGridViewChannelsV);
        }

        private async Task<IOperation> StopChannelAsync(IChannel myC)
        {
            TextBoxLogWriteLine("Channel '{0}' : stopping...", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendStopOperationAsync, myC, "stopped", _context, this, dataGridViewChannelsV);
        }

        private async Task<IOperation> ResetChannelAsync(IChannel myC)
        {
            TextBoxLogWriteLine("Channel '{0}' : reseting...", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendResetOperationAsync, myC, "reset", _context, this, dataGridViewChannelsV);
        }

        private async Task<IOperation> DeleteChannelAsync(IChannel myC)
        {
            TextBoxLogWriteLine("Channel '{0}' : deleting...", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendDeleteOperationAsync, myC, "deleted", _context, this, dataGridViewChannelsV);
        }


        // PROGRAM ASYNC OPERATIONS

        private async Task<IOperation> StopProgramASync(IProgram myP)
        {
            TextBoxLogWriteLine("Program '{0}' : stopping...", myP.Name);
            return await Task.Run(() => ProgramExecuteOperationAsync(myP.SendStopOperationAsync, myP, "stopped"));
        }

        private async Task<IOperation> StartProgramASync(IProgram myP)
        {
            TextBoxLogWriteLine("Program '{0}' : starting...", myP.Name);
            return await Task.Run(() => ProgramExecuteOperationAsync(myP.SendStartOperationAsync, myP, "started"));
        }


        internal async Task ProgramExecuteAsync(Func<Task> fCall, string strObjectName, string strStatusSuccess)
        // for program update and deletion
        {
            try
            {
                await fCall();
                TextBoxLogWriteLine("Program '{0}' : {1}.", strObjectName, strStatusSuccess);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Program '{0}' : Error! {1}", strObjectName, Program.GetErrorMessage(ex), true);
            }
        }



        // STREAMING ENDPOINT ASYNC OPERATIONS

        private async Task<IOperation> StartStreamingEndpoint(IStreamingEndpoint myO)
        {
            TextBoxLogWriteLine("Streaming endpoint '{0}' : starting...", myO.Name);
            return await Task.Run(() => StreamingEndpointExecuteOperationAsync(myO.SendStartOperationAsync, myO, "started"));
        }
        private async Task<IOperation> StopStreamingEndpointAsync(IStreamingEndpoint mySE)
        {
            TextBoxLogWriteLine("Streaming endpoint '{0}' : stopping...", mySE.Name);
            return await Task.Run(() => StreamingEndpointExecuteOperationAsync(mySE.SendStopOperationAsync, mySE, "stopped"));
        }

        private async Task<IOperation> DeleteStreamingEndpointAsync(IStreamingEndpoint myO)
        {
            TextBoxLogWriteLine("Streaming endpoint '{0}' : deleting...", myO.Name);
            return await Task.Run(() => StreamingEndpointExecuteOperationAsync(myO.SendDeleteOperationAsync, myO, "deleted"));
        }


        // used to monitor program and channels in starting or stopping mode with AMS Explorer is launched
        private async void RestoreChannelsAndProgramsStatusMonitoring()
        {
            var ProgramsToMonitor = _context.Programs.ToList().Where(p => p.State == ProgramState.Starting || p.State == ProgramState.Stopping).ToList();
            foreach (var c in ProgramsToMonitor)
                Task.Run(() => MonitorProgram(c));


            var ChannelsToMonitor = _context.Channels.ToList().Where(c => c.State == ChannelState.Starting).ToList();
            foreach (var c in ChannelsToMonitor)
                Task.Run(() => MonitorChannel(c));
        }

        private void MonitorProgram(IProgram program)
        {
            if (program.State == ProgramState.Starting || program.State == ProgramState.Stopping)
            {
                ProgramState state = program.State;
                while (program.State == state)
                {
                    System.Threading.Thread.Sleep(1000);
                    program = _context.Programs.Where(p => p.Id == program.Id).FirstOrDefault();
                }
                dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.RefreshProgram(program)), null);
            }
        }

        private void MonitorChannel(IChannel channel)
        {
            if (channel.State == ChannelState.Deleting || channel.State == ChannelState.Starting || channel.State == ChannelState.Stopping)
            {
                ChannelState state = channel.State;
                while (channel.State == state)
                {
                    System.Threading.Thread.Sleep(1000);
                    channel = _context.Channels.Where(c => c.Id == channel.Id).FirstOrDefault();
                }
                dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channel)), null);
            }
        }


        private async Task<IOperation> ScaleStreamingEndpoint(IStreamingEndpoint myO, int unit)
        {
            IOperation operation = null;
            if (myO != null)
            {
                try
                {
                    TextBoxLogWriteLine("Streaming endpoint '{0}' : scaling to {1} unit(s)...", myO.Name, unit.ToString());
                    operation = await myO.SendScaleOperationAsync(unit);
                    while (operation.State == OperationState.InProgress)
                    {
                        //refresh the operation
                        operation = _context.Operations.GetOperation(operation.Id);
                        System.Threading.Thread.Sleep(1000);
                    }
                    if (operation.State == OperationState.Succeeded)
                    {
                        TextBoxLogWriteLine("Streaming endpoint '{0}': scaled.", myO.Name);
                    }
                    else
                    {
                        TextBoxLogWriteLine("Streaming endpoint '{0}' : did NOT scale. (Error {1})", myO.Name, operation.ErrorCode, true);
                        TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                    }
                    dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myO)), null);
                }

                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Streaming endpoint '{0}' : Error when scaling. {1}", myO.Name, Program.GetErrorMessage(ex), true);
                }
            }
            return operation;
        }




        //used for program update and delete as there is not operation mode for these actions
        internal async Task ProgramExecuteAsync(Func<Task> fCall, IProgram program, string strStatusSuccess) //used for all except creation 
        {
            try
            {
                var STask = fCall();
                var state = program.State;
                while (!STask.IsCompleted)
                {
                    // refersh the program
                    IProgram programR = _context.Programs.Where(p => p.Id == program.Id).FirstOrDefault();
                    if (programR != null && state != programR.State)
                    {
                        state = programR.State;
                        dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.RefreshProgram(programR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                await STask;
                TextBoxLogWriteLine("Program '{0}' : {1}.", program.Name, strStatusSuccess);
                dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.RefreshProgram(program)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Program '{0}' : Error {1}", program.Name, Program.GetErrorMessage(ex), true);
            }
        }


        internal async Task<IOperation> ProgramExecuteOperationAsync(Func<Task<IOperation>> fCall, IProgram program, string strStatusSuccess) //used for all except creation 
        {
            IOperation operation = null;

            try
            {
                var state = program.State;
                var STask = fCall();
                operation = await STask;

                while (operation.State == OperationState.InProgress)
                {
                    //refresh the operation
                    operation = _context.Operations.GetOperation(operation.Id);
                    // refresh the program
                    IProgram programR = _context.Programs.Where(p => p.Id == program.Id).FirstOrDefault();
                    if (programR != null && state != programR.State)
                    {
                        state = programR.State;
                        dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.RefreshProgram(programR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                if (operation.State == OperationState.Succeeded)
                {
                    TextBoxLogWriteLine("Program '{0}' : {1}.", program.Name, strStatusSuccess);
                }
                else
                {
                    TextBoxLogWriteLine("Program '{0}' : NOT {1}. (Error {2})", program.Name, strStatusSuccess, operation.ErrorCode, true);
                    TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
                dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.RefreshProgram(program)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Program '{0}' : Error {1}", program.Name, Program.GetErrorMessage(ex), true);
            }
            return operation;
        }





        internal async Task<IOperation> StreamingEndpointExecuteOperationAsync(Func<Task<IOperation>> fCall, IStreamingEndpoint myO, string strStatusSuccess)
        //used for all except creation 
        {
            IOperation operation = null;

            try
            {
                var state = myO.State;
                var STask = fCall();
                operation = await STask;

                while (operation.State == OperationState.InProgress)
                {
                    //refresh the operation
                    operation = _context.Operations.GetOperation(operation.Id);
                    // refresh the streaming endpoint
                    IStreamingEndpoint myOR = _context.StreamingEndpoints.Where(se => se.Id == myO.Id).FirstOrDefault();
                    if (myOR != null && state != myOR.State)
                    {
                        state = myOR.State;
                        dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myOR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                if (operation.State == OperationState.Succeeded)
                {
                    TextBoxLogWriteLine("Streaming endpoint '{0}' : {1}.", myO.Name, strStatusSuccess);
                    IStreamingEndpoint myse = _context.StreamingEndpoints.Where(se => se.Id == myO.Id).FirstOrDefault();
                    // we display a notification is taskbar for channel started or reset
                    if (myse != null && strStatusSuccess == "started")
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            this.Notify("Streaming endpoint " + strStatusSuccess, string.Format("{0}", myse.Name), false);
                        }));
                    }
                }
                else
                {
                    TextBoxLogWriteLine("Streaming endpoint '{0}': NOT {1}. (Error {2})", myO.Name, strStatusSuccess, operation.ErrorCode, true);
                    TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
                dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myO)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Streaming endpoint '{0}' : Error {1}", myO.Name, Program.GetErrorMessage(ex), true);
            }
            return operation;
        }


        internal async Task<IOperation> IObjectExecuteOperationAsync(Func<Task<IOperation>> fCall, string objectname, string objectlogname, string strStatusSuccess, CloudMediaContext context) // used for creation 
                                                                                                                                                                                                // used for Streaming Endpoint and Channel creation
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
                    TextBoxLogWriteLine("{0} '{1}': {2}.", objectlogname, objectname, strStatusSuccess);
                }
                else
                {
                    TextBoxLogWriteLine("{0} '{1}': NOT {2}. (Error {3})", objectlogname, objectname, strStatusSuccess, operation.ErrorCode, true);
                    TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("{0} '{1}': Error {2}", objectlogname, objectname, Program.GetErrorMessage(ex), true);
            }
            return operation;
        }




        private async void DoDeleteChannels()
        {
            List<IChannel> SelectedChannels = ReturnSelectedChannels();
            string hannelstr = SelectedChannels.Count > 1 ? "hannels" : "hannel";
            if (SelectedChannels.Count > 0)
            {
                List<string> ChannelSourceIDs = SelectedChannels.Select(c => c.Id).ToList();
                List<IProgram> Programs = _context.Programs.AsEnumerable().Where(p => ChannelSourceIDs.Contains(p.ChannelId)).ToList();

                if (Programs.Count == 0) // No program associated to the channel(s) to be deleted
                {
                    string question = (SelectedChannels.Count == 1) ? "Delete channel " + SelectedChannels[0].Name + " ?" : "Delete these " + SelectedChannels.Count + " channels ?";

                    if (System.Windows.Forms.MessageBox.Show(question, "C" + hannelstr + " deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Task.Run(async () =>
                            {
                                // Stop the channels which run
                                var channelsrunning = SelectedChannels.Where(p => p.State == ChannelState.Running);
                                if (channelsrunning.Count() > 0)
                                {
                                    try
                                    {
                                        var taskcstop = channelsrunning.Select(c => StopChannelAsync(c)).ToArray();
                                        await Task.WhenAll(taskcstop);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when stopping a channel", true);
                                        TextBoxLogWriteLine(ex);
                                    }
                                }

                                // delete the channels
                                var taskcdel = SelectedChannels.Select(c => DeleteChannelAsync(c)).ToArray();
                                try
                                {
                                    await Task.WhenAll(taskcdel);
                                }

                                catch (Exception ex)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when deleting a channel", true);
                                    TextBoxLogWriteLine(ex);
                                }
                                DoRefreshGridChannelV(false);
                            }
                           );
                    }
                }
                else // There are programs associated to the channel(s) to be deleted. We need to delete the programs
                {
                    string question = (Programs.Count == 1) ? string.Format("There is one program associated to the c{0}.\nDelete the c{0} and program '{1}' ?", hannelstr, Programs[0].Name)
                                                            : string.Format("There are {0} programs associated to the c{1}.\nDelete the c{1} and these programs ?", Programs.Count, hannelstr);

                    DeleteProgramChannel form = new DeleteProgramChannel(question, "Delete C" + hannelstr);
                    if (form.ShowDialog() == DialogResult.OK)
                    {


                        Task.Run(async () =>
                        {
                            var assets = Programs.Select(p => p.Asset).ToArray();

                            // Stop the programs which run
                            var programsrunning = Programs.Where(p => p.State == ProgramState.Running);
                            if (programsrunning.Count() > 0)
                            {
                                var taskpstop = programsrunning.Select(p => StopProgramASync(p)).ToArray();
                                await Task.WhenAll(taskpstop);
                            }

                            // delete programs
                            Programs.ToList().ForEach(p => TextBoxLogWriteLine("Program '{0}': deleting...", p.Name));
                            var tasks = Programs.Select(p => ProgramExecuteAsync(p.DeleteAsync, p, "deleted")).ToArray();
                            bool Error = false;
                            try
                            {
                                await Task.WhenAll(tasks);
                            }
                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting a progam", true);
                                TextBoxLogWriteLine(ex);
                                Error = true;
                            }
                            DoRefreshGridProgramV(false);


                            if (form.DeleteAsset && Error == false)
                            {
                                assets.ToList().ForEach(a => TextBoxLogWriteLine("Asset '{0}': deleting...", a.Name));
                                var tasksassets = assets.Select(a => a.DeleteAsync()).ToArray();
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

                            // Stop the channels which run
                            var channelsrunning = SelectedChannels.Where(p => p.State == ChannelState.Running);
                            if (channelsrunning.Count() > 0)
                            {
                                try
                                {
                                    channelsrunning.ToList().ForEach(c => TextBoxLogWriteLine("Stopping channel '{0}'...", c.Name));
                                    var taskcstop = channelsrunning.Select(c => c.StopAsync()).ToArray();
                                    await Task.WhenAll(taskcstop);
                                }
                                catch (Exception ex)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when stopping a channel", true);
                                    TextBoxLogWriteLine(ex);
                                }
                            }

                            // delete the channels
                            var taskcdel = SelectedChannels.Select(c => DeleteChannelAsync(c)).ToArray();
                            try
                            {

                                await Task.WhenAll(taskcdel);
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting a channel", true);
                                TextBoxLogWriteLine(ex);
                            }
                            DoRefreshGridChannelV(false);

                        }
                         );

                    }
                }
            }
        }




        private void dataGridViewLiveV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cellchannelstatevalue = dataGridViewChannelsV.Rows[e.RowIndex].Cells[dataGridViewChannelsV.Columns["State"].Index].Value;

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

        private void startChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStartChannels();
        }

        private void stopChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStopChannels();
        }

        private void resetChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoResetChannels();
        }

        private async void DoResetChannels()
        {
            List<IChannel> channels = ReturnSelectedChannels();
            List<string> ListChannelIDs = channels.Select(c => c.Id).ToList();
            var programquery = _context.Programs.AsEnumerable().Where(p => ListChannelIDs.Contains(p.ChannelId) && p.State != ProgramState.Stopped);
            var programqueryrunning = programquery.Where(p => p.State == ProgramState.Running);

            if (programquery.Where(p => p.State == ProgramState.Starting || p.State == ProgramState.Stopping).Count() > 0) // programs are starting or stopping
                MessageBox.Show("Some programs are starting or stopping. Channel(s) cannot be reset now.", "Channel(s) stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (programqueryrunning.Count() > 0) // some programs are running
                {
                    if (MessageBox.Show("One or several programs are running which prevents the channel(s) reset. Do you want to stop the program(s) and then reset the channel(s) ?", "Channel reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        Task.Run(async () =>
                        {
                            programqueryrunning.ToList().ForEach(p => TextBoxLogWriteLine("Stopping program '{0}'...", p.Name));
                            var tasks = programqueryrunning.Select(p => ProgramExecuteOperationAsync(p.SendStopOperationAsync, p, "stopped")).ToArray();
                            await Task.WhenAll(tasks);

                            // let's reset the channels now that running programs are stopped
                            var tasksreset = channels.Select(c => ResetChannelAsync(c)).ToArray();
                            await Task.WhenAll(tasksreset);
                        }
                        );
                    }
                }
                else
                {
                    string question = (channels.Count == 1) ? string.Format("Reset channel '{0}' ?", channels[0].Name) : string.Format("Reset these {0} channels ?", channels.Count);

                    if (System.Windows.Forms.MessageBox.Show(question, "Channel reset", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        // let's reset the channels
                        Task.Run(async () =>
                       {
                           // let's reset the channels now that running programs are stopped
                           var tasksreset = channels.Select(c => ResetChannelAsync(c)).ToArray();
                           await Task.WhenAll(tasksreset);
                       });
                    }
                }
            }
        }

        private void deleteChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteChannels();
        }

        private void createChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateChannel();
        }

        private async void DoCreateChannel()
        {
            CreateLiveChannel form = new CreateLiveChannel(_context)
            {
                KeyframeInterval = Properties.Settings.Default.LiveKeyFrameInterval,
                HLSFragmentPerSegment = Properties.Settings.Default.LiveHLSFragmentsPerSegment,
                StartChannelNow = true
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                TextBoxLogWriteLine("Channel '{0}' : creating...", form.ChannelName);

                bool Error = false;
                ChannelCreationOptions options = new ChannelCreationOptions();
                try
                {
                    options = new ChannelCreationOptions()
                    {
                        Name = form.ChannelName,
                        Description = form.ChannelDescription,
                        EncodingType = form.EncodingType,
                        Input = new ChannelInput()
                        {
                            StreamingProtocol = form.Protocol,
                            AccessControl = new ChannelAccessControl()
                            {
                                IPAllowList = form.inputIPAllow
                            },
                            KeyFrameInterval = form.KeyframeInterval
                        },
                        Output = new ChannelOutput() { Hls = new ChannelOutputHls() { FragmentsPerSegment = form.HLSFragmentPerSegment } },
                        Preview = new ChannelPreview()
                        {
                            AccessControl = new ChannelAccessControl()
                            {
                                IPAllowList = form.previewIPAllow
                            },
                        }
                    };

                    if (form.EncodingType != ChannelEncodingType.None)
                    {
                        options.Encoding = form.EncodingOptions;
                        options.Slate = form.Slate;
                    }
                }

                catch (Exception ex)
                {
                    Error = true;
                    TextBoxLogWriteLine("Error with channel settings.", true);
                    TextBoxLogWriteLine(ex);
                }

                if (!Error)
                {
                    await Task.Run(() => IObjectExecuteOperationAsync(
                     () =>
                         _context.Channels.SendCreateOperationAsync(
                         options),
                         form.ChannelName,
                         "Channel",
                         "created",
                         _context));

                    DoRefreshGridChannelV(false);
                    IChannel channel = GetChannelFromName(form.ChannelName);
                    if (channel != null)
                    {
                        if (form.StartChannelNow)
                        {
                            Task.Run(async () =>
                            {
                                // let's start the channel now
                                await StartChannelAsync(GetChannelFromName(form.ChannelName));
                            }
                );
                        }
                    }
                }
            }
        }


        private void channToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayChannelInfo();
        }

        private void DoDisplayChannelInfo()
        {
            DoDisplayChannelInfo(ReturnSelectedChannels().FirstOrDefault());
        }

        private void DoDisplayChannelAdSlateControl()
        {
            ReturnSelectedChannels().ForEach(c => DoDisplayChannelAdSlateControl(c));

        }

        private void DoDisplayChannelAdSlateControl(IChannel channel)
        {
            if (channel != null && channel.Encoding != null)
            {
                ChannelAdSlateControl form = new ChannelAdSlateControl(this)
                {
                    MyChannel = channel,
                    MyContext = _context
                };

                form.Show();

            }
        }

        private async void DoDisplayChannelInfo(IChannel channel)
        {
            if (channel != null)
            {
                ChannelInformation form = new ChannelInformation(this)
                {
                    MyChannel = channel,
                    MyContext = _context
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    TextBoxLogWriteLine("Channel '{0}' : updating...", channel.Name);

                    channel.Description = form.GetChannelDescription;
                    channel.Input.KeyFrameInterval = form.KeyframeInterval;

                    if (channel.EncodingType != ChannelEncodingType.None && channel.Encoding != null && channel.State == ChannelState.Stopped)
                    {
                        channel.Encoding.SystemPreset = form.SystemPreset; // we update the system preset
                    }

                    // HLS Fragment per segment
                    if (form.HLSFragPerSegment != null)
                    {
                        if (channel.Output == null)
                        {
                            channel.Output = new ChannelOutput() { Hls = new ChannelOutputHls() { FragmentsPerSegment = form.HLSFragPerSegment } };
                        }
                        else if (channel.Output.Hls == null)
                        {
                            channel.Output.Hls = new ChannelOutputHls() { FragmentsPerSegment = form.HLSFragPerSegment };
                        }
                        else
                        {
                            channel.Output.Hls.FragmentsPerSegment = form.HLSFragPerSegment;
                        }
                    }
                    else // form.HLSFragPerSegment is null
                    {
                        if (channel.Output != null && channel.Output.Hls != null && channel.Output.Hls.FragmentsPerSegment != null)
                        {
                            channel.Output.Hls.FragmentsPerSegment = null;
                        }
                    }

                    // Input allow list
                    if (form.GetInputIPAllowList != null)
                    {
                        if (channel.Input.AccessControl == null)
                        {
                            channel.Input.AccessControl = new ChannelAccessControl();
                        }
                        channel.Input.AccessControl.IPAllowList = form.GetInputIPAllowList;
                    }
                    else
                    {
                        if (channel.Input.AccessControl != null)
                        {
                            channel.Input.AccessControl.IPAllowList = null;
                        }
                    }


                    // Preview allow list
                    if (form.GetPreviewAllowList != null)
                    {
                        if (channel.Preview.AccessControl == null)
                        {
                            channel.Preview.AccessControl = new ChannelAccessControl();
                        }
                        channel.Preview.AccessControl.IPAllowList = form.GetPreviewAllowList;

                    }
                    else
                    {
                        if (channel.Preview.AccessControl != null)
                        {
                            channel.Preview.AccessControl.IPAllowList = null;
                        }
                    }


                    // Client Access Policy
                    if (form.GetChannelClientPolicy != null)
                    {
                        if (channel.CrossSiteAccessPolicies == null)
                        {
                            channel.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
                        }
                        channel.CrossSiteAccessPolicies.ClientAccessPolicy = form.GetChannelClientPolicy;

                    }
                    else
                    {
                        if (channel.CrossSiteAccessPolicies != null)
                        {
                            channel.CrossSiteAccessPolicies.ClientAccessPolicy = null;
                        }
                    }


                    // Cross domain  Policy
                    if (form.GetChannelCrossdomaintPolicy != null)
                    {
                        if (channel.CrossSiteAccessPolicies == null)
                        {
                            channel.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
                        }
                        channel.CrossSiteAccessPolicies.CrossDomainPolicy = form.GetChannelCrossdomaintPolicy;

                    }
                    else
                    {
                        if (channel.CrossSiteAccessPolicies != null)
                        {
                            channel.CrossSiteAccessPolicies.CrossDomainPolicy = null;
                        }
                    }
                    await Task.Run(() => ChannelInfo.ChannelExecuteOperationAsync(channel.SendUpdateOperationAsync, channel, "updated", _context, this, dataGridViewChannelsV));
                }
            }
        }

        private void dataGridViewLiveV_SelectionChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("channel selection changed : begin");
            List<IChannel> SelectedChannels = ReturnSelectedChannels();
            if (SelectedChannels.Count > 0)
            {

                dataGridViewProgramsV.ChannelSourceIDs = SelectedChannels.Select(c => c.Id).ToList();

                Task.Run(() =>
                {
                    Debug.WriteLine("channel selection changed : before refresh");
                    DoRefreshGridProgramV(false);
                });
            }
        }


        private async void DoDeletePrograms()
        {

            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.FirstOrDefault() != null)
            {
                if (SelectedPrograms.Count > 0)
                {
                    string question = (SelectedPrograms.Count == 1) ? "Delete program " + SelectedPrograms[0].Name + " ?" : "Delete these " + SelectedPrograms.Count + " programs ?";

                    DeleteProgramChannel form = new DeleteProgramChannel(question, "Delete Program(s)");

                    if (form.ShowDialog() == DialogResult.OK)
                    {

                        var assets = SelectedPrograms.Select(p => p.Asset).ToArray();

                        // Stop the programs which run
                        var programsrunning = SelectedPrograms.Where(p => p.State == ProgramState.Running);
                        if (programsrunning.Count() > 0)
                        {
                            var taskpstop = programsrunning.Select(p => StopProgramASync(p)).ToArray();
                            await Task.WhenAll(taskpstop);
                        }

                        // delete programs
                        SelectedPrograms.ToList().ForEach(p => TextBoxLogWriteLine("Deleting program '{0}'...", p.Name));
                        var tasks = SelectedPrograms.Select(p => ProgramExecuteAsync(p.DeleteAsync, p, "deleted")).ToArray();
                        bool Error = false;
                        try
                        {
                            await Task.WhenAll(tasks);
                        }
                        catch (Exception ex)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when deleting a progam", true);
                            TextBoxLogWriteLine(ex);
                            Error = true;
                        }
                        DoRefreshGridProgramV(false);


                        if (form.DeleteAsset && Error == false)
                        {
                            assets.ToList().ForEach(a => TextBoxLogWriteLine("Deleting asset '{0}'", a.Name));
                            var tasksassets = assets.Select(a => a.DeleteAsync()).ToArray();
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
                }
            }
        }


        private void DoStartPrograms()
        {
            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();

            if (SelectedPrograms.Count > 0)
            {
                Task.Run(async () =>
                {
                    // let's start the programs now
                    var tasks = SelectedPrograms.Select(p => StartProgramASync(p)).ToArray();
                    await Task.WhenAll(tasks);
                }
                        );
            }

        }


        private void DoStopPrograms()
        {
            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.Count > 0)
            {
                string question = (SelectedPrograms.Count == 1) ? string.Format("Stop program '{0}' ?", SelectedPrograms[0].Name) : string.Format("Stop these {0} programs ?", SelectedPrograms.Count);

                if (System.Windows.Forms.MessageBox.Show(question, "Program stop", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Task.Run(async () =>
                {
                    // let's stop the programs now
                    var tasksstop = SelectedPrograms.Select(p => StopProgramASync(p)).ToArray();
                    await Task.WhenAll(tasksstop);
                }
      );
                }
            }
        }


        private IAsset CreateLiveAssetWithOptionalpecifiedLocatorID(string assetName, string storageaccount, bool createlocator, bool setupdynamicencryption, string LocatorID = null)
        {
            bool oktocontinue = true;
            bool Error = false;

            IAsset newAsset = _context.Assets.Create(assetName, storageaccount, AssetCreationOptions.None);

            if (setupdynamicencryption) // user want to enable Dynamic Encryption on the program's asset. We must do it before locator creation
            {
                // if user want to create a locator, force locator and setup dyn encryption, that means he wants to replicate the program from another dc, and so must provide the content key to have a mirror stream 
                oktocontinue = SetupDynamicEncryption(new List<IAsset> { newAsset }, createlocator && (LocatorID != null));
            }

            if (oktocontinue)
            {
                try
                {
                    if (createlocator)
                    {
                        TextBoxLogWriteLine("Creating locator for asset '{0}'", newAsset.Name);
                        IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + assetName, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew), AccessPermissions.Read);
                        if (LocatorID != null)
                        {
                            _context.Locators.CreateLocator(LocatorID, LocatorType.OnDemandOrigin, newAsset, policy, null);
                        }
                        else
                        {
                            _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, newAsset, policy, null);
                        }
                        if (AssetInfo.GetBestStreamingEndpoint(_context).ScaleUnits == 0)
                        {
                            TextBoxLogWriteLine("There is no running streaming endpoint with a scale unit. The locator on the program will not work.", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when creating the locator for the asset '{0}'.", newAsset.Name, true);
                    TextBoxLogWriteLine(ex);
                    Error = true;
                }
            }
            else
            {
                newAsset.Delete();
                Error = true;
                TextBoxLogWriteLine("User canceled.");
            }
            return Error ? null : newAsset;
        }




        private async void DoCreateProgram()
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();
            if (channel != null)
            {
                CreateProgram form = new CreateProgram(_context)
                {
                    ChannelName = channel.Name,
                    archiveWindowLength = new TimeSpan(4, 0, 0),
                    CreateLocator = true,
                    EnableDynEnc = false,
                    StartProgram = false,
                    ProposeStartProgram = (channel.State == ChannelState.Running),
                    AssetName = Constants.NameconvChannel + "-" + Constants.NameconvProgram,
                    ProposeScaleUnit = _context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count == 0
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
        }


        private void createProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateProgram();
        }

        private void startProgramsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartPrograms();
        }

        private void stopProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStopPrograms();
        }

        private void deleteProgramsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeletePrograms();
        }

        private void dataGridViewProgramV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cellprogramstatevalue = dataGridViewProgramsV.Rows[e.RowIndex].Cells[dataGridViewProgramsV.Columns["State"].Index].Value;

            if (cellprogramstatevalue != null)
            {
                ProgramState PS = (ProgramState)cellprogramstatevalue;
                Color mycolor;

                switch (PS)
                {
                    case ProgramState.Stopping:
                        mycolor = Color.OrangeRed;
                        break;
                    case ProgramState.Starting:
                        mycolor = Color.DarkCyan;
                        break;
                    case ProgramState.Stopped:
                        mycolor = Color.Blue;
                        break;
                    case ProgramState.Running:
                        mycolor = Color.Green;
                        break;

                    default:
                        mycolor = Color.Black;
                        break;
                }
                e.CellStyle.ForeColor = mycolor;
            }
        }

        private void displayProgramInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayProgramInfo();

        }

        private void DoDisplayProgramInfo()
        {
            DoDisplayProgramInfo(ReturnSelectedPrograms().FirstOrDefault());
        }

        private async void DoDisplayProgramInfo(IProgram program)
        {
            if (program != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    ProgramInformation form = new ProgramInformation(this, _context)
                    {
                        MyProgram = program,
                        MyStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints // we pass this information if user open asset info from the program info dialog box
                    };

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        program.ArchiveWindowLength = form.archiveWindowLength;
                        program.Description = form.ProgramDescription;
                        await Task.Run(() => ProgramExecuteAsync(program.UpdateAsync, program, "updated"));
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void generateThumbnailsForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoGenerateThumbnails();
        }

        private void DoGenerateThumbnails()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            string taskname = "Media Encoder Standard Thumbnails generation from " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;

            var processor = GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);

            EncodingAMEStandard form = new EncodingAMEStandard(_context, SelectedAssets.Count, processor.Version, ThumbnailsModeOnly: true)
            {
                EncodingLabel = (SelectedAssets.Count > 1) ?
                string.Format("{0} asset{1} selected. You are going to submit {0} job{1} with 1 task.", SelectedAssets.Count, Program.ReturnS(SelectedAssets.Count), SelectedAssets.Count)
                :
                "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded (1 job with 1 task).",

                EncodingJobName = "Thumbnails generation (MES) of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + " - Media Standard encoded",
                EncodingAMEStdPresetJSONFilesUserFolder = Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder,
                EncodingAMEStdPresetJSONFilesFolder = Application.StartupPath + Constants.PathAMEStdFiles,
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
                    AMEStandardTask.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.OutputAssetsCreationOptions);

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
                    Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(job));
                }
                DotabControlMainSwitch(Constants.TabJobs);
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

        private void displayOriginInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayStreamingEndpointInfo();
        }


        private void DoDisplayStreamingEndpointInfo()
        {
            DoDisplayStreamingEndpointInfo(ReturnSelectedStreamingEndpoints().FirstOrDefault());
        }
        private async void DoDisplayStreamingEndpointInfo(IStreamingEndpoint streamingendpoint)
        {

            StreamingEndpointInformation form = new StreamingEndpointInformation()
            {
                MyStreamingEndpoint = streamingendpoint,
                MyContext = _context
            };


            if (form.ShowDialog() == DialogResult.OK)
            {


                streamingendpoint.CustomHostNames = form.GetStreamingCustomHostnames;

                if (form.GetStreamingAllowList != null)
                {
                    if (streamingendpoint.AccessControl == null)
                    {
                        streamingendpoint.AccessControl = new StreamingEndpointAccessControl();
                    }
                    streamingendpoint.AccessControl.IPAllowList = form.GetStreamingAllowList;
                }
                else
                {
                    if (streamingendpoint.AccessControl != null)
                    {
                        streamingendpoint.AccessControl.IPAllowList = null;
                    }
                }

                if (form.GetStreamingAkamaiList != null)
                {
                    if (streamingendpoint.AccessControl == null)
                    {
                        streamingendpoint.AccessControl = new StreamingEndpointAccessControl();
                    }
                    streamingendpoint.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList = form.GetStreamingAkamaiList;

                }
                else
                {
                    if (streamingendpoint.AccessControl != null)
                    {
                        streamingendpoint.AccessControl.AkamaiSignatureHeaderAuthenticationKeyList = null;
                    }
                }

                if (form.MaxCacheAge != null)
                {
                    if (streamingendpoint.CacheControl == null)
                    {
                        streamingendpoint.CacheControl = new StreamingEndpointCacheControl();
                    }
                    streamingendpoint.CacheControl.MaxAge = form.MaxCacheAge;
                }
                else
                {
                    if (streamingendpoint.CacheControl != null)
                    {
                        streamingendpoint.CacheControl.MaxAge = null;
                    }
                }

                // Client Access Policy
                if (form.GetOriginClientPolicy != null)
                {
                    if (streamingendpoint.CrossSiteAccessPolicies == null)
                    {
                        streamingendpoint.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
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


                // Cross domain  Policy
                if (form.GetOriginCrossdomaintPolicy != null)
                {
                    if (streamingendpoint.CrossSiteAccessPolicies == null)
                    {
                        streamingendpoint.CrossSiteAccessPolicies = new CrossSiteAccessPolicies();
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

                streamingendpoint.Description = form.GetOriginDescription;

                // Let's take actions now

                if (streamingendpoint.ScaleUnits != form.GetScaleUnits)
                {
                    Task.Run(async () =>
                   {
                       await StreamingEndpointExecuteOperationAsync(streamingendpoint.SendUpdateOperationAsync, streamingendpoint, "updated");
                       await ScaleStreamingEndpoint(streamingendpoint, form.GetScaleUnits);
                   });
                }
                else // no scaling
                {
                    Task.Run(async () =>
                   {
                       await StreamingEndpointExecuteOperationAsync(streamingendpoint.SendUpdateOperationAsync, streamingendpoint, "updated");
                   });

                }

            }
        }

        private void startOriginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStartStreamingEndpoints();
        }

        private void DoStartStreamingEndpoints()
        {
            foreach (IStreamingEndpoint myO in ReturnSelectedStreamingEndpoints())
            {
                StartStreamingEndpoint(myO);
            }
        }

        private void stopOriginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStopStreamingEndpoints();
        }

        private void DoStopStreamingEndpoints()
        {
            foreach (IStreamingEndpoint myO in ReturnSelectedStreamingEndpoints())
            {
                StopStreamingEndpointAsync(myO);
            }
        }

        private void deleteOriginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteStreamingEndpoints();
        }

        private void DoDeleteStreamingEndpoints()
        {
            List<IStreamingEndpoint> SelectedOrigins = ReturnSelectedStreamingEndpoints();
            if (SelectedOrigins.Count > 0)
            {
                string question = (SelectedOrigins.Count == 1) ? "Delete streaming endpoint " + SelectedOrigins[0].Name + " ?" : "Delete these " + SelectedOrigins.Count + " streaming endpoints ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Streaming endpoint(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Task.Run(async () =>
                    {
                        // let's stop the channels now
                        var tasks = ReturnSelectedStreamingEndpoints().Select(se => DeleteStreamingEndpointAsync(se)).ToArray();
                        await Task.WhenAll(tasks);
                        DoRefreshGridStreamingEndpointV(false);
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
            CreateStreamingEndpoint form = new CreateStreamingEndpoint() { scaleUnits = 1 };

            if (form.ShowDialog() == DialogResult.OK)
            {
                TextBoxLogWriteLine("Creating streaming endpoint {0}...", form.StreamingEndpointName);

                var options = new StreamingEndpointCreationOptions()
                {
                    Name = form.StreamingEndpointName,
                    ScaleUnits = form.scaleUnits,
                    Description = form.StreamingEndpointDescription,
                    CdnEnabled = form.EnableAzureCDN
                };

                await Task.Run(() => IObjectExecuteOperationAsync(
                       () =>
                           _context.StreamingEndpoints.SendCreateOperationAsync(options),
                           form.StreamingEndpointName,
                           "Streaming Endpoint",
                           "created",
                           _context));

                DoRefreshGridStreamingEndpointV(false);
            }
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            var bytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }


        private void displayChannelInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayChannelInfo();
        }

        private void displayProgramInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDisplayProgramInfo();
        }

        private void dataGridViewLiveV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IChannel channel = GetChannel(dataGridViewChannelsV.Rows[e.RowIndex].Cells[dataGridViewChannelsV.Columns["Id"].Index].Value.ToString());
                if (channel != null)
                {
                    DoDisplayChannelInfo(channel);
                }
            }
        }

        private void dataGridViewProgramV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IProgram program = GetProgram(dataGridViewProgramsV.Rows[e.RowIndex].Cells[dataGridViewProgramsV.Columns["Id"].Index].Value.ToString());
                if (program != null)
                {
                    DoDisplayProgramInfo(program);
                }
            }
        }

        private void startChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStartChannels();
        }

        private void stopChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopChannels();
        }

        private void resetChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoResetChannels();
        }

        private void deleteChannelsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteChannels();
        }

        private void startProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStartPrograms();
        }

        private void stopProgramsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoStopPrograms();
        }

        private void deleteProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeletePrograms();
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
                IStreamingEndpoint se = GetStreamingEndpoint(dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[dataGridViewStreamingEndpointsV.Columns["Id"].Index].Value.ToString());
                if (se != null)
                {
                    DoDisplayStreamingEndpointInfo(se);
                }
            }
        }

        private void withFlashOSMFAzurePlayerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAzurePage);
        }

        /*
        private void DoPlayBackProgram(PlayerType ptype, bool tokenplayer = false)
        {
            foreach (var program in ReturnSelectedPrograms())
            {
                if (program != null && program.Asset != null)
                {
                    bool UserCancelled = false;
                    ProgramInfo PI = new ProgramInfo(program, _context);
                    IEnumerable<Uri> ValidURIs = PI.GetValidURIs();

                    if (ValidURIs.FirstOrDefault() == null) // no locator
                    {
                        if (MessageBox.Show(string.Format("There is no valid streaming locator for program '{0}'. Do you want to create one ?", program.Name), "Streaming locator", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            TextBoxLogWriteLine("Creating locator for program '{0}'", program.Name);
                            IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + program.Name, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew), AccessPermissions.Read);
                            ILocator MyLocator = _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, program.Asset, policy, null);
                            ValidURIs = PI.GetValidURIs();
                        }
                        else
                        {
                            UserCancelled = true;
                        }
                    }

                    if (!UserCancelled)
                    {
                        if (ValidURIs.FirstOrDefault() != null)
                        {
                            string myUri = ValidURIs.FirstOrDefault().ToString();
                            if (ptype == PlayerType.DASHLiveAzure)
                            {
                                myUri = program.Asset.GetMpegDashUri().ToString();
                            }
                            AssetInfo.DoPlayBackWithBestStreamingEndpoint(typeplayer: ptype, Urlstr: myUri, context: _context, myasset: program.Asset);
                        }
                        else
                        {
                            TextBoxLogWriteLine("No valid URL exists for this program. Check the streaming endpoints.", true);
                        }
                    }
                }
            }
        }
         */

        private void DoPlaybackChannelPreview(PlayerType ptype)
        {
            foreach (var channel in ReturnSelectedChannels())
            {
                if (channel != null)
                {
                    if (channel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri != null)
                    {
                        AssetInfo.DoPlayBackWithStreamingEndpoint(typeplayer: ptype, Urlstr: channel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri, DoNotRewriteURL: true, context: _context, formatamp: AzureMediaPlayerFormats.Smooth, UISelectSEFiltersAndProtocols: false, mainForm: this);
                    }
                }
            }
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightMonitoring);
        }


        private void copyIngestURLToClipboard_Click(object sender, EventArgs e)
        {
            string ingest = ReturnSelectedChannels().FirstOrDefault().Input.Endpoints.FirstOrDefault().Url.AbsoluteUri;
            System.Windows.Forms.Clipboard.SetText(ingest);
            TextBoxLogWriteLine("The following ingest URL has been copied to the clipboard :");
            TextBoxLogWriteLine("<" + ingest + ">");

        }

        private void copyPreviewURLToClipboard_Click(object sender, EventArgs e)
        {
            string preview = ReturnSelectedChannels().FirstOrDefault().Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri;
            EditorXMLJSON DisplayForm = new EditorXMLJSON("Preview URL", preview, false, false, false);
            DisplayForm.Display();
        }

        private void generateThumbnailsForTheAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoGenerateThumbnails();
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightMonitoring);
        }

        private void withFlashOSMFAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAzurePage);
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
                BatchUploadFrame2 form2 = new BatchUploadFrame2(form.BatchFolder, form.BatchProcessFiles, form.BatchProcessSubFolders, _context) { Left = form.Left, Top = form.Top };
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    DotabControlMainSwitch(Constants.TabTransfers);

                    Task.Run(async () =>
                    {
                        List<Task> MyTasks = new List<Task>();
                        foreach (string folder in form2.BatchSelectedFolders)
                        {
                            int index = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(folder)), TransferType.UploadFromFolder, Properties.Settings.Default.useTransferQueue);
                            //Task.Factory.StartNew(() => ProcessUploadFromFolder(folder, index, form2.StorageSelected));
                            MyTasks.Add(Task.Factory.StartNew(() => ProcessUploadFromFolder(folder, index, form2.StorageSelected)));
                        }

                        foreach (string file in form2.BatchSelectedFiles)
                        {
                            int index = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, Properties.Settings.Default.useTransferQueue);
                            //Task.Factory.StartNew(() => ProcessUploadFileAndMore(file, index, null, form2.StorageSelected));
                            MyTasks.Add(Task.Factory.StartNew(() => ProcessUploadFileAndMore(file, index, null, form2.StorageSelected)));
                        }
                        await Task.WhenAll(MyTasks);

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
            DoCreateProgram();
        }

        private void createChannelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateChannel();
        }

        private void PlaybackPreviewWithFlashOSMFAzurePlayer_Click(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.FlashAzurePage);
        }

        private void PlaybackPreviewWithSilverlightMonitoringPlayer_Click(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.SilverlightMonitoring);
        }

        private void comboBoxTimeProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewProgramsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewProgramsV.TimeFilter == FilterTime.TimeRange)
            {
                var form = new TimeRangeSelection()
                {
                    TimeRange = dataGridViewProgramsV.TimeFilterTimeRange,
                    LabelMain = "Last Modified Time Range of Programs"
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewProgramsV.TimeFilterTimeRange = form.TimeRange;
                }
                else
                {
                    // user cancelled timerange box TODO
                }
            }

            if (dataGridViewProgramsV.Initialized)
            {
                DoRefreshGridProgramV(false);
            }
        }

        private void buttonSetFilterProgram_Click(object sender, EventArgs e)
        {
            DoProgramSearch();
        }

        private void DoProgramSearch()
        {
            if (dataGridViewProgramsV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchProgramOption.SelectedItem as Item).Value);
                dataGridViewProgramsV.SearchInName = new SearchObject { Text = textBoxSearchNameProgram.Text, SearchType = stype };
                DoRefreshGridProgramV(false);
            }
        }

        private void comboBoxStatusProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewProgramsV.Initialized)
            {
                dataGridViewProgramsV.FilterState = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridProgramV(false);
            }
        }

        private void comboBoxOrderProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewProgramsV.Initialized)
            {
                dataGridViewProgramsV.OrderItemsInGrid = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridProgramV(false);
            }
        }

        private void createStreamingEndpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateStreamingEndpoint();
        }


        private void setupDynamicEncryptionForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSetupDynEnc();
        }

        private void DoSetupDynEnc()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();
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
                    DoDeleteAllLocatorsOnAssets(publishedAssets, true);
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
                        //  !NeedToDisplayPlayReadyLicense,
                        // ((form1.GetAssetDeliveryProtocol & AssetDeliveryProtocol.Dash) == AssetDeliveryProtocol.Dash) && (form1.GetDeliveryPolicyType == AssetDeliveryPolicyType.DynamicCommonEncryption))
                        { Left = form1.Left, Top = form1.Top };
                        if (form2_CENC.ShowDialog() == DialogResult.OK)
                        {
                            var form3_CENC = new AddDynamicEncryptionFrame3_CENCDelivery(_context, form1.PlayReadyPackaging, form1.WidevinePackaging);
                            if (form3_CENC.ShowDialog() == DialogResult.OK)
                            {
                                bool NeedToDisplayPlayReadyLicense = form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady > 0;
                                bool NeedToDisplayWidevineLicense = form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine > 0;

                                List<AddDynamicEncryptionFrame4> form4list = new List<AddDynamicEncryptionFrame4>();
                                List<AddDynamicEncryptionFrame5_PlayReadyLicense> form5list = new List<AddDynamicEncryptionFrame5_PlayReadyLicense>();
                                List<AddDynamicEncryptionFrame6_WidevineLicense> form6list = new List<AddDynamicEncryptionFrame6_WidevineLicense>();

                                bool usercancelledform4or5 = false;
                                bool usercancelledform4or6 = false;

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
                                        //AddDynamicEncryptionFrame6_WidevineLicense form6_WidevineLicense = new AddDynamicEncryptionFrame6_WidevineLicense(step, i + 1, i == (form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady - 1)) { Left = form3_CENC.Left, Top = form3_CENC.Top };
                                        step++;
                                        if (NeedToDisplayPlayReadyLicense) // it's a PlayReady license and user wants to deliver the license from Azure Media Services
                                        {
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
                                if (!usercancelledform4or5 && !usercancelledform4or6)
                                {
                                    DoDynamicEncryptionAndKeyDeliveryWithCENC(SelectedAssets, form1, form2_CENC, form3_CENC, form4list, form5list, form6list, true);
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
                            if (form3_AES.ShowDialog() == DialogResult.OK)
                            {
                                List<AddDynamicEncryptionFrame4> form4list = new List<AddDynamicEncryptionFrame4>();
                                bool usercancelledform4 = false;
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

                                if (!usercancelledform4)
                                {
                                    DoDynamicEncryptionWithAES(SelectedAssets, form1, form2_AES, form3_AES, form4list, true);
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



        private void DoDynamicEncryptionAndKeyDeliveryWithCENC(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, AddDynamicEncryptionFrame2_CENCKeyConfig form2_CENC, AddDynamicEncryptionFrame3_CENCDelivery form3_CENC, List<AddDynamicEncryptionFrame4> form4list, List<AddDynamicEncryptionFrame5_PlayReadyLicense> form5PlayReadyLicenseList, List<AddDynamicEncryptionFrame6_WidevineLicense> form6WidevineLicenseList, bool DisplayUI)
        {
            bool ErrorCreationKey = false;
            bool reusekey = false;
            bool firstkeycreation = true;
            IContentKey formerkey = null;

            if (!form2_CENC.ContentKeyRandomGeneration && (form2_CENC.KeyId != null))  // user want to manually enter the cryptography data and key if providedd 
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

                        if ((form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady + form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine) > 0 || form2_CENC.ContentKeyRandomGeneration)

                        // Azure will deliver the PR or WV license or user wants to auto generate the key, so we can create a key with a random content key
                        {
                            try
                            {
                                contentKey = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context);
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
                                        contentKey = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context, keyid, bytecontentkey);
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
                                else // no seed given, so content key has been setup
                                {
                                    try
                                    {
                                        contentKey = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context, (Guid)form2_CENC.KeyId, Convert.FromBase64String(form2_CENC.CENCContentKey));
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
                    else if (false)//form1.PlayReadyPackaging form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady == 0 || form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine == 0)
                                   // TO DO ? : if user wants to deliver license from external servers
                                   // user wants to deliver license with an external PlayReady and/or Widevine server but the key exists already !
                    {
                        TextBoxLogWriteLine("Warning for asset '{0}'. A CENC key already exists. You need to make sure that your external PlayReady or Widevine server can deliver the license for this asset.", AssetToProcess.Name, true);
                    }
                    else // let's use existing content key
                    {
                        contentKey = contentkeys.FirstOrDefault();
                        TextBoxLogWriteLine("Existing key '{0}' will be used for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                    }
                    if ((form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady + form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine) > 0) // PlayReady/Widevine license and delivery from Azure Media Services
                    {
                        // let's create the Authorization Policy
                        IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                                       ContentKeyAuthorizationPolicies.
                                       CreateAsync("Authorization Policy").Result;

                        // Associate the content key authorization policy with the content key.
                        contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                        contentKey = contentKey.UpdateAsync().Result;

                        foreach (var form4 in form4list)
                        { // for each option

                            string PlayReadyLicenseDeliveryConfig = null;
                            string WidevineLicenseDeliveryConfig = null;
                            bool ItIsAPlayReadyOption = form4list.IndexOf(form4) < form3_CENC.GetNumberOfAuthorizationPolicyOptionsPlayReady;

                            if (ItIsAPlayReadyOption)
                            { // user wants to define a PlayReady license for this option
                              // let's build the PlayReady license template

                                ErrorCreationKey = false;
                                try
                                {
                                    PlayReadyLicenseDeliveryConfig = form5PlayReadyLicenseList[form4list.IndexOf(form4)].GetLicenseTemplate;
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
                                                policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption(contentKey, ContentKeyDeliveryType.PlayReadyLicense, PlayReadyLicenseDeliveryConfig, _context);
                                                TextBoxLogWriteLine("Created PlayReady Open authorization policy for the asset '{0}' ", AssetToProcess.Name);
                                                contentKeyAuthorizationPolicy.Options.Add(policyOption);
                                            }
                                            else // widevine
                                            {
                                                policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption(contentKey, ContentKeyDeliveryType.Widevine, WidevineLicenseDeliveryConfig, _context);
                                                TextBoxLogWriteLine("Created Widevine Open authorization policy for the asset '{0}' ", AssetToProcess.Name);
                                                contentKeyAuthorizationPolicy.Options.Add(policyOption);
                                            }
                                            break;

                                        case ContentKeyRestrictionType.TokenRestricted:
                                            TokenVerificationKey mytokenverifkey = null;
                                            string OpenIdDoc = null;
                                            switch (form4.GetDetailedTokenType)
                                            {
                                                case ExplorerTokenType.SWT:
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
                                                policyOption = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyCENC(ContentKeyDeliveryType.PlayReadyLicense, contentKey, form4.GetAudience, form4.GetIssuer, form4.GetTokenRequiredClaims, form4.AddContentKeyIdentifierClaim, form4.GetTokenType, form4.GetDetailedTokenType, mytokenverifkey, _context, PlayReadyLicenseDeliveryConfig, OpenIdDoc);
                                                TextBoxLogWriteLine("Created Token PlayReady authorization policy for the asset '{0}'.", AssetToProcess.Name);
                                            }
                                            else //widevine
                                            {
                                                policyOption = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyCENC(ContentKeyDeliveryType.Widevine, contentKey, form4.GetAudience, form4.GetIssuer, form4.GetTokenRequiredClaims, form4.AddContentKeyIdentifierClaim, form4.GetTokenType, form4.GetDetailedTokenType, mytokenverifkey, _context, WidevineLicenseDeliveryConfig, OpenIdDoc);
                                                TextBoxLogWriteLine("Created Token Widevine authorization policy for the asset '{0}'", AssetToProcess.Name);
                                            }
                                            contentKeyAuthorizationPolicy.Options.Add(policyOption);


                                            if (form4.GetDetailedTokenType != ExplorerTokenType.JWTOpenID) // not possible to create a test token if OpenId is used
                                            {
                                                // let display a test token
                                                X509SigningCredentials signingcred = null;
                                                if (form4.GetDetailedTokenType == ExplorerTokenType.JWTX509)
                                                {
                                                    signingcred = new X509SigningCredentials(form4.GetX509Certificate);
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


                    // Let's create the Asset Delivery Policy now
                    if (form1.GetDeliveryPolicyType != AssetDeliveryPolicyType.None && form1.EnableDynEnc)
                    {
                        IAssetDeliveryPolicy DelPol = null;

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
                                widevineAcquisitionUrl: form3_CENC.GetNumberOfAuthorizationPolicyOptionsWidevine > 0 ? null : form3_CENC.WidevineLAurl
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

        private void DoDynamicEncryptionWithAES(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, AddDynamicEncryptionFrame2_AESKeyConfig form2, AddDynamicEncryptionFrame3_AESDelivery form3_AES, List<AddDynamicEncryptionFrame4> form4list, bool DisplayUI)
        {
            bool ErrorCreationKey = false;
            string aeskey = string.Empty;
            bool firstkeycreation = true;
            Uri aeslaurl = null;
            IContentKey formerkey = null;
            bool reusekey = false;

            if (!form2.ContentKeyRandomGeneration)
            {
                aeskey = form2.AESContentKey;
                aeslaurl = form3_AES.AESLaUrl;
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


                    if (form3_AES.GetNumberOfAuthorizationPolicyOptions > 0) // AES Key and delivery from Azure Media Services
                    {

                        // let's create the Authorization Policy
                        IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
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

                                        policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption(contentKey, ContentKeyDeliveryType.BaselineHttp, null, _context);
                                        TextBoxLogWriteLine("Created Open authorization policy for the asset {0} ", contentKey.Id, AssetToProcess.Name);
                                        contentKeyAuthorizationPolicy.Options.Add(policyOption);
                                        break;

                                    case ContentKeyRestrictionType.TokenRestricted:
                                        TokenVerificationKey mytokenverifkey = null;
                                        string OpenIdDoc = null;
                                        switch (form3.GetDetailedTokenType)
                                        {
                                            case ExplorerTokenType.SWT:
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
                                            X509SigningCredentials signingcred = null;
                                            if (form3.GetDetailedTokenType == ExplorerTokenType.JWTX509)
                                            {
                                                signingcred = new X509SigningCredentials(form3.GetX509Certificate);
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

                    // Let's create the Asset Delivery Policy now
                    IAssetDeliveryPolicy DelPol = null;
                    string name = string.Format("AssetDeliveryPolicy {0} ({1})", form1.GetContentKeyType.ToString(), form1.GetAssetDeliveryProtocol.ToString());

                    try
                    {
                        DelPol = DynamicEncryption.CreateAssetDeliveryPolicyAES(AssetToProcess, contentKey, form1.GetAssetDeliveryProtocol, name, _context, aeslaurl);
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

        private void mergeSelectedAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyAssetToAnotherAMSAccount();
        }

        private void mergeAssetsToANewAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyAssetToAnotherAMSAccount();
        }

        private void removeDynamicEncryptionForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRemoveDynEnc();
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
                labelAssetName += Constants.endline + "Do you want to also DELETE the policies ?";
                DialogResult myDialogResult = MessageBox.Show(labelAssetName, "Dynamic encryption", MessageBoxButtons.YesNoCancel);

                if (myDialogResult != DialogResult.Cancel)
                {
                    bool Error = false;
                    string keydeliveryconfig = string.Empty;

                    foreach (IAsset AssetToProcess in SelectedAssets)
                    {

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
                                }
                            }

                            if (Error) break;
                            TextBoxLogWriteLine("Removed{0} asset delivery policies, key authorization policies and locator(s) for asset {1}.", (myDialogResult == DialogResult.Yes) ? " and deleted" : string.Empty, AssetToProcess.Name);

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

            List<IAsset> SelectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();

            if (SelectedAssets.Count > 0)
            {
                labelAssetName = string.Format("CENC and Enveloppe keys will be removed for asset '{0}'.", SelectedAssets.FirstOrDefault().Name);
                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = string.Format("CENC and Enveloppe keys will removed for these {0} selected assets.", SelectedAssets.Count.ToString());
                }
                labelAssetName += Constants.endline + "Do you want to also DELETE the keys ?";

                DialogResult myDialogResult = MessageBox.Show(labelAssetName, "Dynamic encryption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (myDialogResult != DialogResult.Cancel)
                {
                    bool Error = false;

                    foreach (IAsset AssetToProcess in SelectedAssets)
                    {

                        if (AssetToProcess != null)
                        {
                            List<string> KeysListIDs = new List<string>();
                            try
                            {
                                IList<IContentKey> CENCAESkeys = AssetToProcess.ContentKeys.Where(k => k.ContentKeyType == ContentKeyType.CommonEncryption || k.ContentKeyType == ContentKeyType.EnvelopeEncryption).ToList();
                                KeysListIDs = CENCAESkeys.Select(k => k.Id).ToList(); // create a list of IDs



                                // deleting authorization policies & options
                                foreach (var key in CENCAESkeys)
                                {
                                    AssetToProcess.ContentKeys.Remove(key);
                                }
                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when removing the keys for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }

                            if (myDialogResult == DialogResult.Yes) // Let's delete the keys
                            {

                                try
                                {
                                    // deleting keys
                                    var deleteTasks = _context.ContentKeys.ToList().Where(k => KeysListIDs.Contains(k.Id)).ToList().Select(k => k.DeleteAsync()).ToArray();
                                    Task.WaitAll(deleteTasks);
                                }
                                catch (Exception e)
                                {
                                    // Add useful information to the exception
                                    TextBoxLogWriteLine("There is a problem when deleting the keys for '{0}'.", AssetToProcess.Name, true);
                                    TextBoxLogWriteLine(e);
                                }
                            }


                            if (Error) break;
                            TextBoxLogWriteLine("Removed{0} CENC and Enveloppe keys for asset {1}.", (myDialogResult == DialogResult.Yes) ? " and deleted" : string.Empty, AssetToProcess.Name);

                            dataGridViewAssetsV.PurgeCacheAssets(SelectedAssets);
                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                        }
                    }
                }
            }
        }

        private void encodeAssetsWithAzureMediaEncodersystemPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMESystemPreset();
        }

        private void encodeAssetsWithAzureMediaEncoderadvancedModeWithCustomPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMEAdvanced();
        }


        private void encodeAssetsWithPremiumWorkflowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithPremiumWorkflow();
        }

        private void createALocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            DoCreateLocator(SelectedAssets);
        }

        private void deleteAllLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
        }

        private void validateTheMultiMP4AssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuValidateMultiMP4Static();
        }

        private void packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoMenuMP4ToSmoothStatic();
        }

        private void packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoMenuPackageSmoothToHLSStatic();
        }

        private void encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoMenuProtectWithPlayReadyStatic();
        }

        private void DoCopyOutputURLAssetOrProgramToClipboard()
        {
            IAsset asset = ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault();
            if (asset != null)
            {
                AssetInfo AI = new AssetInfo(asset);
                IEnumerable<Uri> ValidURIs = AI.GetValidURIs();
                if (ValidURIs != null && ValidURIs.FirstOrDefault() != null)
                {
                    string url = ValidURIs.FirstOrDefault().AbsoluteUri;

                    if (_context.StreamingEndpoints.Count() > 1 || (_context.StreamingEndpoints.FirstOrDefault() != null && _context.StreamingEndpoints.FirstOrDefault().CustomHostNames.Count > 0) || _context.Filters.Count() > 0 || (asset.AssetFilters.Count() > 0))
                    {
                        var form = new ChooseStreamingEndpoint(_context, asset, url);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            url = AssetInfo.RW(new Uri(url), form.SelectStreamingEndpoint, form.SelectedFilters, form.ReturnHttps, form.ReturnSelectCustomHostName, form.ReturnStreamingProtocol, form.ReturnHLSAudioTrackName, form.ReturnHLSNoAudioOnlyMode).ToString();
                        }
                        else
                        {
                            return;
                        }
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

        private void removeDynamicEncryptionForTheAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoRemoveDynEnc();
        }

        private void setupDynamicEncryptionForTheAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoSetupDynEnc();
        }

        private void withCustomPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.CustomPlayer);
        }



        private void withCustomPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.CustomPlayer);
        }


        private void DoMenuCreateLocatorOnPrograms()
        {
            List<IAsset> SelectedAssets = ReturnSelectedPrograms().Select(p => p.Asset).ToList();
            DoCreateLocator(SelectedAssets);
            DoRefreshGridProgramV(false);
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
            List<IAsset> SelectedAssets = ReturnSelectedPrograms().Select(p => p.Asset).ToList();
            DoDeleteAllLocatorsOnAssets(SelectedAssets);
            DoRefreshGridProgramV(false);
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

        private void withDASHLiveAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.DASHLiveAzure);
        }

        private void withCustomPlayerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.CustomPlayer);
        }

        private void displayRelatedAssetInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoOfProgram();
        }


        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridAssetV(false);
        }

        private void refreshToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridJobV(false);
        }

        private void refreshToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridChannelV(false);
        }

        private void refreshToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridProgramV(false);
        }

        private void refreshToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridStreamingEndpointV(false);
        }

        private void refreshToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
            DoRefreshGridProcessorV(false);
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

        private async void extendExistingStreamingLocatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await DoRefreshStreamingLocators();
        }

        private void recreateProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoResetPrograms();
        }

        private async void DoResetPrograms()
        {
            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.FirstOrDefault() != null)
            {
                if (SelectedPrograms.Count > 0)
                {
                    string question = (SelectedPrograms.Count == 1) ? string.Format("Reset program '{0}' ?", SelectedPrograms[0].Name) : string.Format("Reset these {0} programs ?", SelectedPrograms.Count);
                    question += Constants.endline + Constants.endline + "This will delete the program, the related asset and locator and will re-create them with the same ISM file name, locator ID, keys, delivery policies and filters.";
                    question += Constants.endline + Constants.endline + "WARNING: As the new asset will use the same locator ID, this can create issues. Notes:";
                    question += Constants.endline + "- Encoder must use incremental timestamps";
                    question += Constants.endline + "- Locator GUID are cached by the streaming endpoints for 5 minutes";
                    question += Constants.endline + "- The old live archive manifest could have been be cached";

                    if (MessageBox.Show(question, "Program(s) reset", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        foreach (IProgram myP in SelectedPrograms)
                        {
                            if (myP.State != ProgramState.Stopped) // program is not stopped
                            {
                                TextBoxLogWriteLine("Program '{0}' must be stopped in order to reset it. Operation aborted.", myP.Name, true);
                            }
                            else // program is stopped
                            {
                                IAsset asset = myP.Asset;
                                IAsset newAsset = null;

                                string assetName = asset.Name; // backup the asset name
                                string assetstorageaccount = asset.StorageAccountName; // backup the storage account name
                                string programName = myP.Name; // backup program name
                                string programDesc = myP.Description; // backup program description
                                TimeSpan programArchiveWindowLength = myP.ArchiveWindowLength;
                                var locator = asset.Locators.Where(l => l.Type == LocatorType.OnDemandOrigin).ToArray();
                                IChannel myChannel = myP.Channel;
                                var ismAssetFiles = asset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase)).ToArray();


                                if (locator.Count() != 1) // no single locator, we do nothing
                                {
                                    TextBoxLogWriteLine("Asset of program '{0}' does not have a single locator. Operation aborted.", myP.Name, true);
                                }
                                else if (ismAssetFiles.Count() != 1)
                                {
                                    TextBoxLogWriteLine("Asset of program '{0}' does not have a ISM file. Operation aborted.", myP.Name, true);
                                }
                                else
                                {
                                    string ismName = Path.GetFileNameWithoutExtension(ismAssetFiles.FirstOrDefault().Name);
                                    string locatorID = locator.FirstOrDefault().Id;
                                    DateTime locatorExpDateTime = locator.FirstOrDefault().ExpirationDateTime;

                                    try
                                    {
                                        TextBoxLogWriteLine("Deleting program '{0}'", myP.Name);
                                        myP.Delete();
                                    }
                                    catch (Exception ex)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when deleting the program {0}.", myP.Name, true);
                                        TextBoxLogWriteLine(ex);
                                    }

                                    try
                                    {
                                        newAsset = _context.Assets.Create(assetName, assetstorageaccount, AssetCreationOptions.None);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when creating the new asset {0}.", assetName, true);
                                        TextBoxLogWriteLine(ex);
                                    }



                                    if (asset != null)
                                    {
                                        //let's copy the keys
                                        foreach (var key in asset.ContentKeys)
                                        {
                                            newAsset.ContentKeys.Add(key);
                                        }
                                        //let's copy the policies
                                        foreach (var delpol in asset.DeliveryPolicies)
                                        {
                                            newAsset.DeliveryPolicies.Add(delpol);
                                        }
                                        //let's copy the filters
                                        foreach (var filter in asset.AssetFilters)
                                        {
                                            try
                                            {
                                                newAsset.AssetFilters.Create(filter.Name, filter.PresentationTimeRange, filter.Tracks);
                                            }
                                            catch (Exception ex)
                                            {
                                                TextBoxLogWriteLine("There is a problem when copying filter {0} the new asset {1}.", filter.Name, assetName, true);
                                                TextBoxLogWriteLine(ex);
                                            }
                                        }

                                        //delete
                                        try
                                        {
                                            TextBoxLogWriteLine("Deleting asset '{0}'", asset.Name);
                                            DeleteAsset(asset);
                                            if (AssetInfo.GetAsset(asset.Id, _context) == null) TextBoxLogWriteLine("Deletion done.");
                                        }
                                        catch (Exception ex)
                                        {
                                            // Add useful information to the exception
                                            TextBoxLogWriteLine("There is a problem when deleting the asset {0}.", asset.Name, true);
                                            TextBoxLogWriteLine(ex);
                                        }
                                    }

                                    // let's use the same expiration date than previous locator
                                    IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + assetName, locatorExpDateTime.Subtract(DateTime.UtcNow), AccessPermissions.Read);

                                    try
                                    {
                                        TextBoxLogWriteLine("Creating locator for asset '{0}'", asset.Name);
                                        ILocator locat = _context.Locators.CreateLocator(locatorID, LocatorType.OnDemandOrigin, newAsset, policy, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Add useful information to the exception
                                        TextBoxLogWriteLine("There is a problem when creating the locator for the asset '{0}'.", asset.Name, true);
                                        TextBoxLogWriteLine(ex);
                                    }

                                    ProgramCreationOptions options = new ProgramCreationOptions()
                                    {
                                        AssetId = newAsset.Id,
                                        Name = programName,
                                        Description = programDesc,
                                        ArchiveWindowLength = programArchiveWindowLength,
                                        ManifestName = ismName,
                                    };
                                    await Task.Run(() => ProgramExecuteAsync(() => myChannel.Programs.CreateAsync(options), programName, "created"));
                                }
                            }
                        }
                        DoRefreshGridProgramV(false);
                        DoRefreshGridAssetV(false);
                    }
                }
            }
        }

        private void recreateProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoResetPrograms();
        }

        private void attachAnotherStoragheAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoAttachAnotherStorageAccount();
        }

        private void DoAttachAnotherStorageAccount()
        {
            AttachStorage form = new AttachStorage(_credentials);

            if (form.ShowDialog() == DialogResult.OK)
            {
                ManagementRESTAPIHelper helper = new ManagementRESTAPIHelper(form.GetAzureServiceManagementURL, form.GetCertBody, form.GetAzureSubscriptionID);

                // Initialize the AccountInfo class.
                MediaServicesAccount accountInfo = new MediaServicesAccount()
                {
                    AccountName = _context.Credentials.ClientId,
                    StorageAccountName = _context.DefaultStorageAccount.Name
                };

                AttachStorageAccountRequest storageAccountToAttach = new AttachStorageAccountRequest()
                {
                    StorageAccountName = form.GetStorageName,
                    StorageAccountKey = form.GetStorageKey,
                    BlobStorageEndpointUri = form.GetStorageEndpointURL
                };

                // Call AttachStorageAccountToMediaServiceAccount to 
                // attach an existing storage account to the Media Services account.
                try
                {
                    helper.AttachStorageAccountToMediaServiceAccount(accountInfo, storageAccountToAttach);
                    TextBoxLogWriteLine("Storage account '{0}' attached to '{1}' account.", form.GetStorageName, _context.Credentials.ClientId);
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when attaching the storage account.", true);
                    TextBoxLogWriteLine(ex);
                    TextBoxLogWriteLine(helper.stringBuilderLog.ToString());
                }
            }
        }


        private void displayErrorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoDisplayJobError();
        }

        private void DoDisplayJobError()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();
            if (SelectedJobs.Count == 1)
            {
                IJob JobToDisplayP = SelectedJobs.FirstOrDefault();

                // Refresh the job.
                _context = Program.ConnectAndGetNewContext(_credentials);
                IJob JobToDisplayP2 = _context.Jobs.Where(j => j.Id == JobToDisplayP.Id).FirstOrDefault();

                if (JobToDisplayP2 != null)
                {
                    if (JobToDisplayP2.State == JobState.Error)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var task in JobToDisplayP2.Tasks)
                        {
                            foreach (var details in task.ErrorDetails)
                            {
                                sb.AppendLine(details.Message);
                            }
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

        private void silverlightSmoothStreamingPlayReadyTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerSLToken);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void azureManagementPortalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //  string PortalUrl = (_credentials.UseOtherAPI == true.ToString() && _credentials.OtherAzureEndpoint.Equals(CredentialsEntry.OtherChinaAzureEndpoint)) ?
            //     CredentialsEntry.ChinaManagementPortal : CredentialsEntry.GlobalManagementPortal;
            string PortalUrl;
            if (_credentials.UseOtherAPI == true.ToString())
            {
                PortalUrl = _credentials.OtherManagementPortal;
            }
            else
            {
                PortalUrl = CredentialsEntry.GlobalManagementPortal;
            }

            if (!string.IsNullOrEmpty(PortalUrl)) Process.Start(PortalUrl);
        }

        private void dASHLivePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerDASHAzure);
        }

        private void packageTheSmoothStreamingAssetsToHLSV3staticToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuPackageSmoothToHLSStatic();
        }

        private void encryptTheSmoothStreamingAssetsWithPlayReadystaticToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuProtectWithPlayReadyStatic();
        }

        private void packageTheMultiMP4AssetsToSmoothStreamingstaticToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuMP4ToSmoothStatic();
        }

        private void validateTheMultiMP4AssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuValidateMultiMP4Static();
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

                string tasknameloc = taskname.Replace(Constants.NameconvInputasset, inputasssetname).Replace(Constants.NameconvProcessorname, form.SingleEncodingProcessorSelected.Name);

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
                task.OutputAssets.AddNew(outputassetnameloc, form.SingleTaskOptions.StorageSelected, form.SingleTaskOptions.OutputAssetsCreationOptions);

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
                dataGridViewJobsV.DoJobProgress(job);

                DoRefreshGridJobV(false);
            }
        }

        private void saveAsTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSaveJobAsTemplate();
        }

        private void DoSaveJobAsTemplate()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();
            string jobtemplatename = string.Empty;
            if (SelectedJobs.Count == 1)
            {
                if (Program.InputBox("Save as job template", "Job template name:", ref jobtemplatename) == DialogResult.OK)
                {
                    IJobTemplate jtemplate = SelectedJobs.FirstOrDefault().SaveAsTemplate(jobtemplatename);
                }
            }
            else
            {
                MessageBox.Show("Please select one job", "Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void submitFromTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSubmitFromTemplate();
        }

        private void DoSubmitFromTemplate()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            ProcessFromJobTemplate form = new ProcessFromJobTemplate(_context, SelectedAssets.Count)
            {
                ProcessingPromptText = (SelectedAssets.Count > 1) ? string.Format("{0} assets have been selected. 1 job will be submitted.", SelectedAssets.Count) : string.Format("Asset '{0}' will be encoded.", SelectedAssets.FirstOrDefault().Name),
                Text = "Template based processing",
                ProcessingJobName = string.Format("Processing of {0} with template {1}", Constants.NameconvInputasset, Constants.NameconvTemplate),
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                string jobname = form.ProcessingJobName.Replace(Constants.NameconvTemplate, form.SelectedJobTemplate.Name);
                string assetname = (SelectedAssets.Count == 1) ? SelectedAssets.FirstOrDefault().Name : "Multiple assets";
                jobname = jobname.Replace(Constants.NameconvInputasset, assetname);

                // Submit the job
                try
                {
                    IJob job = _context.Jobs.Create(jobname, form.SelectedJobTemplate, SelectedAssets, form.JobOptions.Priority);
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobname) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error); TextBoxLogWriteLine("There has been a problem when submitting the job {0}", jobname, true);
                    TextBoxLogWriteLine(e);
                    return;
                }
                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
            }
        }

        private void processAssetsWithAJobTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSubmitFromTemplate();
        }

        private void resubmitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoJobResubmit();
        }

        private void saveAsTemplateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoSaveJobAsTemplate();
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
                    DoMenuUploadFromSingleFile_Step2(files.ToArray()); // let's upload the objects as files, each file as an individual asset
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

        private void withFlashAESTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAESToken);

        }

        private void withSilverlightPlayReadyTokenPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightPlayReadyToken);

        }

        private void withFlashTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAESToken);
        }

        private void withSilverlightPlayReadyTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightPlayReadyToken);
        }

        private void flashSmoothStreamingAESTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerAESToken);
        }

        private void withFlashAESTokenPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAESToken);
        }

        private void withSilverlightPlayReadyTokenPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightPlayReadyToken);
        }

        private void buttonUpdateMediaRU_Click(object sender, EventArgs e)
        {
            DoUpdateMediaRU();
        }

        private async void DoUpdateMediaRU()
        {
            bool oktocontinue = true;

            if (trackBarEncodingRU.Value == 0 && (((Item)comboBoxEncodingRU.SelectedItem).Value != Enum.GetName(typeof(ReservedUnitType), ReservedUnitType.Basic)))
            // user selected 0 with a non S1 hardware...
            {
                if (MessageBox.Show("You selected 0 unit but the encoding type is not S1 (Basic). Are you sure you want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    oktocontinue = false;
                }
            }

            if (oktocontinue)
            {
                TextBoxLogWriteLine(string.Format("Updating to {0} {1} Media Reserved Unit{2}...", (int)trackBarEncodingRU.Value, ((Item)comboBoxEncodingRU.SelectedItem).Name, (int)trackBarEncodingRU.Value > 1 ? "s" : string.Empty));

                IEncodingReservedUnit EncResUnit = _context.EncodingReservedUnits.FirstOrDefault();
                EncResUnit.CurrentReservedUnits = (int)trackBarEncodingRU.Value;
                EncResUnit.ReservedUnitType = (ReservedUnitType)(Enum.Parse(typeof(ReservedUnitType), ((Item)comboBoxEncodingRU.SelectedItem).Value));

                trackBarEncodingRU.Enabled = false;
                comboBoxEncodingRU.Enabled = false;
                buttonUpdateEncodingRU.Enabled = false;

                await Task.Run(() =>
                {
                    try
                    {
                        EncResUnit.Update();
                        TextBoxLogWriteLine("Media Reserved Unit(s) updated.");
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error when updating Media Reserved Unit(s).");
                        TextBoxLogWriteLine(ex);
                    }
                }

                    );
                DoRefreshGridProcessorV(false);
                trackBarEncodingRU.Enabled = true;
                comboBoxEncodingRU.Enabled = true;
                buttonUpdateEncodingRU.Enabled = true;
            }
        }


        private void RUEncodingUpdateControls()
        {
            // If RU is set to 0, let's switch to S1 (Basic)
            if (trackBarEncodingRU.Value == 0)
            {
                foreach (var it in comboBoxEncodingRU.Items)
                {
                    if (((Item)it).Value == Enum.GetName(typeof(ReservedUnitType), ReservedUnitType.Basic))
                    {
                        comboBoxEncodingRU.SelectedItem = it;
                    }
                }
                //  comboBoxEncodingRU.SelectedItem = Enum.GetName(typeof(ReservedUnitType), ReservedUnitType.Basic);
            }
        }

        private void trackBarEncodingRU_ValueChanged(object sender, EventArgs e)
        {
            RUEncodingUpdateControls();
        }

        private void trackBarEncodingRU_Scroll(object sender, EventArgs e)
        {
            UpdateLabelProcessorUnits();
        }

        private void UpdateLabelProcessorUnits()
        {
            labelnbunits.Text = string.Format(Constants.strUnits, trackBarEncodingRU.Value, trackBarEncodingRU.Value > 1 ? "s" : string.Empty);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _context = Program.ConnectAndGetNewContext(_credentials);
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

        private void withAzureMediaPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer);
        }

        public void DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType playertype, List<IAsset> listassets, string filter = null)
        {
            foreach (var myAsset in listassets)
            {
                bool Error = false;
                if (!IsThereALocatorValid(myAsset, ref PlayBackLocator, LocatorType.OnDemandOrigin)) // No streaming locator valid
                {

                    if (MessageBox.Show(string.Format("There is no valid streaming locator for asset '{0}'.\nDo you want to create one ?", myAsset.Name), "Streaming locator", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        TextBoxLogWriteLine("Creating locator for asset '{0}'", myAsset.Name);
                        try
                        {
                            IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + myAsset.Name, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDaysNew), AccessPermissions.Read);
                            ILocator MyLocator = _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, myAsset, policy, null);
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

                if (!Error && IsThereALocatorValid(myAsset, ref PlayBackLocator, LocatorType.OnDemandOrigin)) // There is a streaming locator valid
                {
                    Uri MyUri = PlayBackLocator.GetSmoothStreamingUri();

                    if (MyUri != null)
                    {
                        AssetInfo.DoPlayBackWithStreamingEndpoint(playertype, MyUri.AbsoluteUri, _context, this, myAsset, false, filter);
                    }
                    else
                    {
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
                    }
                }
            }
        }

        private void DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType playertype)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(playertype, ReturnSelectedAssetsFromProgramsOrAssets());
        }

        private void withAzureMediaPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer);
        }

        private void withAzureMediaPlayerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.AzureMediaPlayer);
        }

        private void withAzureMediaPlayerToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.AzureMediaPlayer);
        }

        private void hTML5CaptionMakerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.DemoCaptionMaker);
        }

        private void removeKeysForTheAssetsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoRemoveKeys();
        }

        private void getATestTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoGetTestToken();
        }

        private void DoGetTestToken()
        {

            bool Error = true;
            IAsset MyAsset = ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault();
            if (MyAsset != null)
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

            CopyAsset form = new CopyAsset(_context, SelectedAssets.Count, CopyAssetBoxMode.CopyAsset)
            {
                CopyAssetName = string.Format("Copy of {0}", Constants.NameconvAsset),
                EnableSingleDestinationAsset = SelectedAssets.Count > 1
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                bool usercanceled = false;
                var storagekeys = BuildStorageKeyDictionary(SelectedAssets, form.DestinationLoginCredentials, ref usercanceled, _context.DefaultStorageAccount.Name, _credentials.StorageKey, form.DestinationStorageAccount);
                if (!usercanceled)
                {
                    if (!form.SingleDestinationAsset) // standard mode: 1:1 asset copy
                    {
                        foreach (IAsset asset in SelectedAssets)
                        {
                            int index = DoGridTransferAddItem(string.Format("Copy asset '{0}' to account '{1}'", asset.Name, form.DestinationLoginCredentials.AccountName), TransferType.ExportToOtherAMSAccount, Properties.Settings.Default.useTransferQueue);
                            // Start a worker thread that does asset copy.
                            Task.Factory.StartNew(() => ProcessExportAssetToAnotherAMSAccount(form.DestinationLoginCredentials, form.DestinationStorageAccount, storagekeys, new List<IAsset>() { asset }, form.CopyAssetName.Replace(Constants.NameconvAsset, asset.Name), index, form.DeleteSourceAsset, form.CopyDynEnc, form.RewriteLAURL, form.CloneAssetFilters));
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
                            int index = DoGridTransferAddItem(string.Format("Copy several assets to account '{0}'", form.DestinationLoginCredentials.AccountName), TransferType.ExportToOtherAMSAccount, Properties.Settings.Default.useTransferQueue);
                            // Start a worker thread that does asset copy.
                            Task.Factory.StartNew(() => ProcessExportAssetToAnotherAMSAccount(form.DestinationLoginCredentials, form.DestinationStorageAccount, storagekeys, SelectedAssets, form.CopyAssetName.Replace(Constants.NameconvAsset, SelectedAssets.FirstOrDefault().Name), index, form.DeleteSourceAsset));
                        }
                    }
                    DotabControlMainSwitch(Constants.TabTransfers);
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
                    if (Program.InputBox("Source Storage Account Key Needed", string.Format("Please enter the Storage Account Access Key for '{0}' : ", asset.StorageAccountName), ref valuekey) == DialogResult.OK)
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
                if (Program.InputBox("Destination Storage Account Key Needed", string.Format("Please enter the Storage Account Access Key for '{0}' : ", DestinationOtherStorage), ref valuekey) == DialogResult.OK)
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
                    if (string.IsNullOrEmpty(DestinationCredentials.StorageKey)) // but key is not provided
                    {

                        string valuekey = "";
                        if (Program.InputBox("Destination Storage Account Key Needed", string.Format("Please enter the Storage Account Access Key of the destination storage account ('{0}') : ", newcontext.DefaultStorageAccount.Name), ref valuekey) == DialogResult.OK)
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
                            storagekeys.Add(newcontext.DefaultStorageAccount.Name, DestinationCredentials.StorageKey);
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
            IStreamingEndpoint streamingendpoint = ReturnSelectedStreamingEndpoints().FirstOrDefault();

            if (streamingendpoint.State == StreamingEndpointState.Stopped)
            {
                if (streamingendpoint.ScaleUnits > 0)
                {
                    if (MessageBox.Show(string.Format("Are you sure you want to {0} CDN on Streaming Endpoint '{1}' ?", enable ? "enable" : "disable", streamingendpoint.Name), "Azure CDN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Task.Run(async () =>
                        {
                            streamingendpoint.CdnEnabled = enable;
                            await StreamingEndpointExecuteOperationAsync(streamingendpoint.SendUpdateOperationAsync, streamingendpoint, "updated");
                            DoRefreshGridStreamingEndpointV(false);
                        });
                    }
                }
                else if (enable) // 0 scale unit and user wants to enable cdn
                {
                    if (MessageBox.Show(string.Format("The Streaming Endpoint '{0}' does not have a reserved unit. Explorer will add a unit and enable CDN. Do you want to continue ?", streamingendpoint.Name), "Unit and Azure CDN", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Task.Run(async () =>
                        {
                            TextBoxLogWriteLine("Adding a streaming unit to enable Azure CDN...");
                            await ScaleStreamingEndpoint(streamingendpoint, 1);
                            streamingendpoint.CdnEnabled = enable;
                            await StreamingEndpointExecuteOperationAsync(streamingendpoint.SendUpdateOperationAsync, streamingendpoint, "updated");
                            DoRefreshGridStreamingEndpointV(false);
                        });
                    }
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
        }

        private void enableAzureCDNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeAzureCDN(true);
        }

        private void disableAzureCDNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeAzureCDN(false);
        }

        private void originToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            // enable Azure CDN operation if one se selected and in stopped state
            ManageMenuOptionsAzureCDN(disableAzureCDNToolStripMenuItem1, enableAzureCDNToolStripMenuItem1);
        }

        private void ManageMenuOptionsAzureCDN(ToolStripMenuItem disableAzureCDNToolStripMenuItem1, ToolStripMenuItem enableAzureCDNToolStripMenuItem1)
        {
            // enable Azure CDN operation if one se selected and in stopped state
            List<IStreamingEndpoint> streamingendpoints = ReturnSelectedStreamingEndpoints();

            if (streamingendpoints.Count == 1)
            {
                bool sestopped = (streamingendpoints.FirstOrDefault().State == StreamingEndpointState.Stopped);
                bool cdnenabled = streamingendpoints.FirstOrDefault().CdnEnabled;

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

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            DoMenuDownloadToLocal();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorage();

        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            DoMenuUploadFromSingleFile_Step1();
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
            DoWatchFolder();
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

        private void inputSSLURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, true);

        }

        private void inputURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, false);
        }

        private void DoCopyChannelInputURLToClipboard(bool secondary = false, bool https = false)
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();
            string absuri;
            bool Error = false;

            if (secondary && channel.Input.Endpoints.Count > 1) // secondary
            {
                absuri = channel.Input.Endpoints[1].Url.AbsoluteUri;
            }
            else // primary
            {
                absuri = channel.Input.Endpoints.FirstOrDefault().Url.AbsoluteUri;
            }

            if (https)
            {
                if (channel.Input.StreamingProtocol == StreamingProtocol.FragmentedMP4)
                {
                    absuri.Replace("http://", "https://");
                }
                else
                {
                    MessageBox.Show("SSL is only possible for Smooth Streaming input.", "SSL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Error = true;
                }
            }

            if (!Error) //System.Windows.Forms.Clipboard.SetText(absuri);
            {
                string label = string.Format("Input URL ({0})", secondary ? "secondary" : "primary");
                EditorXMLJSON DisplayForm = new EditorXMLJSON(label, absuri, false, false, false);
                DisplayForm.Display();
            }
        }

        private void primaryInputURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, false);

        }

        private void secondaryInputURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(true, false);

        }

        private void ContextMenuItemChannelCopyIngestURLToClipboard_DropDownOpening(object sender, EventArgs e)
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();

            inputURLToolStripMenuItem.Visible = (channel.Input.Endpoints.Count == 1);
            inputSSLURLToolStripMenuItem.Visible = (channel.Input.StreamingProtocol == StreamingProtocol.FragmentedMP4);
            primaryInputURLToolStripMenuItem.Visible = (channel.Input.Endpoints.Count > 1);
            secondaryInputURLToolStripMenuItem.Visible = (channel.Input.Endpoints.Count > 1);

        }

        private void copyInputURLToClipboardToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();

            inputURLToolStripMenuItem1.Visible = (channel.Input.Endpoints.Count == 1);
            inputSSLURLToolStripMenuItem1.Visible = (channel.Input.StreamingProtocol == StreamingProtocol.FragmentedMP4);
            primaryInputURLToolStripMenuItem1.Visible = (channel.Input.Endpoints.Count > 1);
            secondaryInputURLToolStripMenuItem1.Visible = (channel.Input.Endpoints.Count > 1);

        }

        private void inputURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, false);
        }

        private void inputSSLURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, true);

        }

        private void primaryInputURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, false);

        }

        private void secondaryInputURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(true, false);

        }
        private void adAndSlateControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayChannelAdSlateControl();
        }

        private void channelsAdAndSlateControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDisplayChannelAdSlateControl();
        }

        private void contextMenuStripChannels_Opening(object sender, CancelEventArgs e)
        {
            var channels = ReturnSelectedChannels();
            bool single = channels.Count == 1;
            bool oneOrMore = channels.Count > 0;

            // channel info if only one channel
            ContextMenuItemChannelDisplayInfomation.Enabled = single;

            // slate control if at least one channel with live transcoding
            ContextMenuItemChannelAdAndSlateControl.Enabled = channels.Any(c => c.Encoding != null);

            // copy input url if only one channel
            ContextMenuItemChannelCopyIngestURLToClipboard.Enabled = single;

            // on premises encoder if only one channel
            ContextMenuItemChannelRunOnPremisesLiveEncoder.Enabled = single;

            // copy preview url if only one channel
            ContextMenuItemChannelCopyPreviewURLToClipboard.Enabled = single;

            // start, stop, reset, delete, clone channel
            ContextMenuItemChannelStart.Enabled = oneOrMore;
            ContextMenuItemChannelStop.Enabled = oneOrMore;
            ContextMenuItemChannelReset.Enabled = oneOrMore;
            cloneChannelsToolStripMenuItem.Enabled = oneOrMore;
            ContextMenuItemChannelDelete.Enabled = oneOrMore;

            // playback preview
            playbackTheProgramToolStripMenuItem.Enabled = oneOrMore;
        }

        private void liveChannelToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var channels = ReturnSelectedChannels();
            bool single = channels.Count == 1;
            bool oneOrMore = channels.Count > 0;

            // channel info if only one channel
            channInfoToolStripMenuItem.Enabled = single;

            // slate control if at least one channel with live transcoding
            channelsAdAndSlateControlToolStripMenuItem.Enabled = channels.Any(c => c.Encoding != null);

            // copy input url if only one channel
            copyInputURLToClipboardToolStripMenuItem.Enabled = single;

            // on premises encoder if only one channel
            runAnOnpremisesLiveEncoderToolStripMenuItem.Enabled = single;

            // copy preview url if only one channel
            copyPreviewURLToClipboardToolStripMenuItem.Enabled = single;

            // start, stop, reset, delete, clone channel
            startChannelsToolStripMenuItem.Enabled = oneOrMore;
            stopChannelsToolStripMenuItem.Enabled = oneOrMore;
            resetChannelsToolStripMenuItem.Enabled = oneOrMore;
            deleteChannelsToolStripMenuItem.Enabled = oneOrMore;
            toolStripMenuItemCloneChannel.Enabled = oneOrMore;

            // playback preview
            playbackThePreviewToolStripMenuItem.Enabled = oneOrMore;

            ////////////

            var programs = ReturnSelectedPrograms();
            single = programs.Count == 1;
            oneOrMore = programs.Count > 0;

            // program info if only one program
            displayProgramInformationToolStripMenuItem.Enabled = single;

            // asset info if only one program
            ProgramDisplayRelatedAssetInformationToolStripMenuItem.Enabled = single;

            // reset program
            recreateProgramsToolStripMenuItem.Enabled = oneOrMore;

            // start, stop, delete program
            startProgramsToolStripMenuItem1.Enabled = oneOrMore;
            stopProgramsToolStripMenuItem.Enabled = oneOrMore;
            deleteProgramsToolStripMenuItem1.Enabled = oneOrMore;

            // clone program
            toolStripMenuItemCloneProgram.Enabled = oneOrMore;

            // sublcip program
            subclipLiveStreamsarchivesToolStripMenuItem1.Enabled = oneOrMore;

        }

        private void contextMenuStripPrograms_Opening(object sender, CancelEventArgs e)
        {
            var programs = ReturnSelectedPrograms();
            bool single = programs.Count == 1;
            bool oneOrMore = programs.Count > 0;

            // program info if only one program
            ContextMenuItemProgramDisplayInformation.Enabled = single;

            // asset info if only one program
            ContextMenuItemProgramDisplayRelatedAssetInformation.Enabled = single;

            // copy program url if only one program
            ContextMenuItemProgramCopyTheOutputURLToClipboard.Enabled = single;

            // reset program
            recreateProgramToolStripMenuItem.Enabled = oneOrMore;

            // start, stop, delete program
            ContextMenuItemProgramStart.Enabled = oneOrMore;
            ContextMenuItemProgramStop.Enabled = oneOrMore;
            ContextMenuItemProgramDelete.Enabled = oneOrMore;

            // clone program
            cloneToolStripMenuItem.Enabled = oneOrMore;

            // subclip program
            subclipProgramsToolStripMenuItem.Enabled = oneOrMore;

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

        private void azureMediaServicesSamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.AMSSamples);
        }

        private void processAssetsWithHyperlapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuHyperlapseAssets();
        }

        private void processAssetsWithHyperlapseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuHyperlapseAssets();
        }

        private void buttonSetFilterChannel_Click(object sender, EventArgs e)
        {
            DoChannelSearch();
        }

        private void DoChannelSearch()
        {
            if (dataGridViewChannelsV.Initialized)
            {
                SearchIn stype = (SearchIn)Enum.Parse(typeof(SearchIn), (comboBoxSearchChannelOption.SelectedItem as Item).Value);
                dataGridViewChannelsV.SearchInName = new SearchObject { Text = textBoxSearchNameChannel.Text, SearchType = stype };
                DoRefreshGridChannelV(false);
            }
        }

        private void comboBoxFilterTimeChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewChannelsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewChannelsV.TimeFilter == FilterTime.TimeRange)
            {
                var form = new TimeRangeSelection()
                {
                    TimeRange = dataGridViewChannelsV.TimeFilterTimeRange,
                    LabelMain = "Last Modified Time Range of Channels"
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewChannelsV.TimeFilterTimeRange = form.TimeRange;
                }
                else
                {
                    // user cancelled timerange box TODO
                }
            }

            if (dataGridViewChannelsV.Initialized)
            {
                DoRefreshGridChannelV(false);
            }
        }

        private void comboBoxStatusChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewChannelsV.Initialized)
            {
                dataGridViewChannelsV.FilterState = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridChannelV(false);
            }
        }

        private void comboBoxOrderChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridViewChannelsV.Initialized)
            {
                dataGridViewChannelsV.OrderItemsInGrid = ((ComboBox)sender).SelectedItem.ToString();
                DoRefreshGridChannelV(false);
            }
        }


        private void contextMenuStripStorage_Opening(object sender, CancelEventArgs e)
        {

        }

        private void findTheAssetFromTheLocatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoFromLocatorID();
        }

        private void findTheAssetFromTheLocatorIdGUIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoFromLocatorID();
        }

        private void displayParentJobToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DisplayJobSource(ReturnSelectedAssets().FirstOrDefault());
        }

        private void displayParentJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayJobSource(ReturnSelectedAssets().FirstOrDefault());
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
            DynManifestFilter form = new DynManifestFilter(_context);

            if (form.ShowDialog() == DialogResult.OK)
            {
                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;
                    _context.Filters.Create(filterinfo.Name, filterinfo.Presentationtimerange, filterinfo.Trackconditions);
                    TextBoxLogWriteLine("Global filter '{0}' created.", filterinfo.Name);
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
            try
            {
                ReturnSelectedFilters().ForEach(f => f.Delete());
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
            IStreamingFilter filter = ReturnSelectedFilters().FirstOrDefault();
            DynManifestFilter form = new DynManifestFilter(_context, filter);

            if (form.ShowDialog() == DialogResult.OK)
            {
                FilterCreationInfo filterinfotoupdate = null;
                try
                {
                    filterinfotoupdate = form.GetFilterInfo;
                    filter.PresentationTimeRange = filterinfotoupdate.Presentationtimerange;
                    filter.Tracks = filterinfotoupdate.Trackconditions;
                    filter.Update();
                    TextBoxLogWriteLine("Global filter '{0}' updated.", filter.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", filter.Name, true);
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

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMEStandard();
        }

        private void DoMenuEncodeWithAMEStandard()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;
            }

            if (SelectedAssets.FirstOrDefault() == null) return;

            string taskname = "Media Encoder Standard processing of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;

            var processor = GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);

            EncodingAMEStandard form = new EncodingAMEStandard(_context, SelectedAssets.Count, processor.Version)
            {
                EncodingLabel = (SelectedAssets.Count > 1) ?
                string.Format("{0} asset{1} selected. You are going to submit {0} job{1} with 1 task.", SelectedAssets.Count, Program.ReturnS(SelectedAssets.Count), SelectedAssets.Count)
                :
                "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded (1 job with 1 task).",

                EncodingJobName = "Media Encoder Standard processing of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + " - Media Standard encoded",
                EncodingAMEStdPresetJSONFilesUserFolder = Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder,
                EncodingAMEStdPresetJSONFilesFolder = Application.StartupPath + Constants.PathAMEStdFiles,
                SelectedAssets = SelectedAssets
            };



            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (IAsset asset in form.SelectedAssets)
                {
                    bool Error = false;
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
                    AMEStandardTask.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.OutputAssetsCreationOptions);

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
                        Error = true;
                    }
                    if (!Error) Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(job));
                }
                DotabControlMainSwitch(Constants.TabJobs);
                DoRefreshGridJobV(false);
            }
        }

        private void encodeAssetsWithAMEStandardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMEStandard();
        }


        private void contextMenuStripFilters_Opening(object sender, CancelEventArgs e)
        {
            var filters = ReturnSelectedFilters();
            bool singleitem = (filters.Count == 1);
            filterInfoupdateToolStripMenuItem.Enabled = singleitem;
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            DoCreateFilter();
        }

        private void toolStripMenuItem22_Click_1(object sender, EventArgs e)
        {
            DoUpdateFilter();
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            DoDeleteFilter();
        }


        private void filterToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var filters = ReturnSelectedFilters();
            bool singleitem = (filters.Count == 1);
            toolStripMenuItemFilterInfo.Enabled = singleitem;
        }


        private void toolStripMenuItem22_Click_2(object sender, EventArgs e)
        {
            DoCopyOutputURLAssetOrProgramToClipboard();
        }

        private void toolStripMenuItem25_Click_1(object sender, EventArgs e)
        {
            DoCopyOutputURLAssetOrProgramToClipboard();
        }

        private void publishToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var assets = ReturnSelectedAssetsFromProgramsOrAssets();

            // get test token, create asset filter, or copy publish URL only if one asset
            getATestTokenToolStripMenuItem.Enabled =
            createAnAssetFilterToolStripMenuItem1.Enabled =
            toolStripMenuItemPublishCopyPubURLToClipb.Enabled =
            assets.Count == 1;
        }

        private void dataGridViewTransfer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }



        private void withAzureMediaPlayerToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void withAzureMediaPlayerToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {
        }


        private void DoCreateAssetFilter()
        {
            IAsset selasset = ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault();

            DynManifestFilter form = new DynManifestFilter(_context, null, selasset);

            if (form.ShowDialog() == DialogResult.OK)
            {
                FilterCreationInfo filterinfo = null;
                try
                {
                    filterinfo = form.GetFilterInfo;
                    selasset.AssetFilters.Create(filterinfo.Name, filterinfo.Presentationtimerange, filterinfo.Trackconditions);
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

        private void duplicateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDuplicateFilter();
        }

        private void DoDuplicateFilter()
        {
            var filters = ReturnSelectedFilters();
            if (filters.Count == 1)
            {
                IStreamingFilter sourcefilter = filters.FirstOrDefault();

                string newfiltername = sourcefilter.Name + "Copy";
                if (Program.InputBox("New name", "Enter the name of the new duplicate filter:", ref newfiltername) == DialogResult.OK)
                {
                    try
                    {
                        _context.Filters.Create(newfiltername, sourcefilter.PresentationTimeRange, sourcefilter.Tracks);
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

        private void createAnAssetFilterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCreateAssetFilter();
        }

        private void withAzureMediaPlayerToolStripMenuItem2_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemProgramAssetFilterInfo_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            DoCreateFilter();
        }

        private void azureMediaPlayerDiagnosticsCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Constants.PlayerAMPDiagnostics);
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
            var SelectedPrograms = ReturnSelectedPrograms();

            CopyAsset form = new CopyAsset(_context, 1, CopyAssetBoxMode.CloneProgram);

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!form.SingleDestinationAsset) // standard mode: 1:1 asset copy
                {
                    foreach (IProgram program in SelectedPrograms)
                    {
                        // Start a worker thread that does asset copy.
                        Task.Factory.StartNew(() => ProcessCloneProgramToAnotherAMSAccount(form.DestinationLoginCredentials, form.DestinationStorageAccount, program, form.CopyDynEnc, form.RewriteLAURL, form.CloneLocators, form.CloneAssetFilters));
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
            CopyAsset form = new CopyAsset(_context, 1, CopyAssetBoxMode.CloneChannel);

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

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            DoClonePrograms();
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            DoCloneChannels();
        }


        private void dataGridViewV_VisibleChanged(object sender, EventArgs e)
        {
            Program.dataGridViewV_Resize(sender);
        }

        private void subclipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSubClip();
        }

        private void DoSubClip()
        {
            var selectedAssets = ReturnSelectedAssetsFromProgramsOrAssets();

            if (selectedAssets.Count > 0)
            {
                if (!selectedAssets.All(a => AssetInfo.GetAssetType(a).StartsWith(AssetInfo.Type_LiveArchive)))
                {
                    MessageBox.Show("Asset(s) should be a Live stream or archive." + Constants.endline + "Subclipping other types of assets is unpredictable.", "Format issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                Subclipping form = new Subclipping(_context, selectedAssets, this)
                {
                    EncodingJobName = "Subclipping of " + Constants.NameconvInputasset,
                    EncodingOutputAssetName = Constants.NameconvInputasset + " - Subclipped"
                };

                form.ShowDialog();
            }
        }

        private void subclipLiveStreamsarchivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSubClip();
        }

        private void subclipProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSubClip();
        }


        private void DoExportMetadata()
        {
            ExportToExcel form = new ExportToExcel(_context, ReturnSelectedAssets(), dataGridViewAssetsV.assets);
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

        private void subclipLiveStreamsarchivesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoSubClip();
        }

        private void storageVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoStorageVersion();
        }

        private void DoStorageVersion(string storageName = null)
        {
            string valuekey = "";
            bool StorageKeyKnown = false;
            if (storageName == null)
            {
                storageName = ReturnSelectedStorage().Name;
            }

            if (storageName == _context.DefaultStorageAccount.Name && havestoragecredentials)
            {
                valuekey = _credentials.StorageKey;
                StorageKeyKnown = true;
            }

            if ((!havestoragecredentials && storageName == _context.DefaultStorageAccount.Name) || (storageName != _context.DefaultStorageAccount.Name))
            { // No blob credentials. Let's ask the user
                StorageKeyKnown = false;
                if (Program.InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + storageName + ":", ref valuekey) == DialogResult.OK)
                {
                    StorageKeyKnown = true;
                    if (storageName == _context.DefaultStorageAccount.Name)
                    {
                        _credentials.StorageKey = valuekey;
                        havestoragecredentials = true;
                    }
                }
            }
            if (StorageKeyKnown) // if we have the storage credentials
            {
                bool Error = false;
                ServiceProperties serviceProperties = null;
                CloudBlobClient blobClient = null;
                try
                {
                    var storageAccount = new CloudStorageAccount(new StorageCredentials(storageName, valuekey), _credentials.ReturnStorageSuffix(), true);
                    blobClient = storageAccount.CreateCloudBlobClient();

                    // Get the current service properties
                    serviceProperties = blobClient.GetServiceProperties();
                }
                catch (Exception ex)
                {
                    Error = true;
                    MessageBox.Show("Error when accessing the storage account.\nIs the key correct ?\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextBoxLogWriteLine("Error when accessing the storage account.\nIs the key correct ?", true);
                    TextBoxLogWriteLine(ex);
                }

                if (!Error)
                {
                    var form = new StorageSettings(storageName, serviceProperties);

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
            DoCopyJobReportToClipboard();
        }

        private void toolStripMenuItem28_Click_1(object sender, EventArgs e)
        {
            DoCreateJobReportEmail();
        }

        private void copyToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyJobReportToClipboard();
        }

        private void copyToClipboardToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoCopyAssetReportToClipboard();
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            DoCreateAssetReportEmail();
        }

        private void copyToClipboardToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoCopyAssetReportToClipboard();
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

        private void visibleJobsInGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteJobs(dataGridViewJobsV.jobs.ToList());
        }

        private void visibleJobsInGridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteJobs(dataGridViewJobsV.jobs.ToList());
        }

        private void allJobsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoDeleteAllJobs();
        }

        private void selectedJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteJobs(ReturnSelectedJobs());
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            Process.Start(_HelpFiles + "MediaServices_ClientSDK.chm");
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            Process.Start(_HelpFiles + "MediaServices_ClientSDK_Ext_API.chm");
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            Process.Start(_HelpFiles + "MediaServices_Operations_RESTAPI.chm");
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            Process.Start(_HelpFiles + "MediaServices_RESTAPI.chm");
        }

        private void dataGridViewStorage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string storagename = dataGridViewStorage.Rows[e.RowIndex].Cells[dataGridViewStorage.Columns["StrictName"].Index].Value.ToString();
                DoStorageVersion(storagename);
            }
        }

        private void checkBoxAnyChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridViewProgramsV.Initialized && !CheckboxAnychannelChangedByCode)
            {

                dataGridViewProgramsV.AnyChannel = ((CheckBox)sender).Checked;
                Task.Run(() =>
                {
                    DoRefreshGridProgramV(false);
                });
            }
            CheckboxAnychannelChangedByCode = false;
        }

        private void textBoxSearchNameProgram_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSearchNameProgram.Text))
            {
                CheckboxAnychannelChangedByCode = true;
                checkBoxAnyChannel.Checked = backupCheckboxAnychannel;
                checkBoxAnyChannel.Enabled = true;
            }
            else if (checkBoxAnyChannel.Enabled) // not empty and checkbox is still enabled
            {
                CheckboxAnychannelChangedByCode = true;
                backupCheckboxAnychannel = checkBoxAnyChannel.Checked;
                checkBoxAnyChannel.Checked = true;
                checkBoxAnyChannel.Enabled = false;
            }
        }

        private void withAnExternalAsperaSignantAzCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateNewBulkUpload();
        }



        private void dataGridViewIngestManifestsV_Resize(object sender, EventArgs e)
        {
            Program.dataGridViewV_Resize(sender);
        }

        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DeleteIngestManifests();
        }

        private void DeleteIngestManifests()
        {
            var ims = ReturnSelectedIngestManifests();
            if (ims.Count > 0)
            {
                string question = (ims.Count == 1) ? "Delete bulk container " + ims[0].Name + " ?" : "Delete these " + ims.Count + " bulk containers ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Bulk container deletion", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    bool Error = false;
                    try
                    {
                        Task[] deleteTasks = ims.Select(a => a.DeleteAsync()).ToArray();
                        TextBoxLogWriteLine("Deleting bulk container(s)");
                        this.Cursor = Cursors.WaitCursor;
                        Task.WaitAll(deleteTasks);
                    }
                    catch (Exception ex)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There is a problem when deleting the bulk container(s)", true);
                        TextBoxLogWriteLine(ex);
                        Error = true;
                    }
                    if (!Error) TextBoxLogWriteLine("Bulk container(s) deleted.");
                    this.Cursor = Cursors.Default;
                    DoRefreshGridIngestManifestV(false);
                }
            }
        }

        private void defineAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateNewBulkUpload();
        }

        private void DoCreateNewBulkUpload()
        {
            BulkUpload form = new BulkUpload(_context);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.AssetFiles.Count() > 0)
                {
                    // Encryption of files
                    if (form.EncryptAssetFiles)
                    {
                        if (!Directory.Exists(form.EncryptToFolder))
                        {
                            if (MessageBox.Show(string.Format("Folder '{0}' does not exist." + Constants.endline + "Do you want to create it ?", form.EncryptToFolder), "Folder does not exist", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                try
                                {
                                    Directory.CreateDirectory(form.EncryptToFolder);
                                }
                                catch
                                {
                                    MessageBox.Show(string.Format("Error when creating folder '{0}'.", form.EncryptToFolder), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    TextBoxLogWriteLine("Bulk: Error when creating folder '{0}'.", form.EncryptToFolder, true);
                                }
                            }
                            else
                            {
                                TextBoxLogWriteLine("Bulk: User cancelled the folder creation.", true);
                                return;
                            }
                        }
                    }

                    Task.Run(async () =>
                    {
                        try
                        {
                            DoProcessCreateBulkIngestAndEncryptFiles(form.IngestName, form.IngestStorageSelected, form.AssetFiles, form.AssetStorageSelected, form.EncryptAssetFiles, form.EncryptToFolder, form.GenerateAzCopy, form.GenerateSigniant, form.SigniantServersSelected, form.SigniantAPIKey, form.GenerateAspera);
                        }
                        catch (Exception ex)
                        {
                            int i;
                        }
                    }
           );


                }
                else
                {
                    MessageBox.Show("There is no asset file name(s)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextBoxLogWriteLine("There is no asset file name(s).", true);
                }
            }
        }

        private void DoProcessCreateBulkIngestAndEncryptFiles(string IngestName, string IngestStorage, List<BulkUpload.BulkAsset> assetFiles, string assetStorage, bool encryptFiles, string encryptToFolder, bool GenerateAzCopy, bool GenerateSigniant, List<string> SigniantServers, string SigniantAPIKey, bool GenerateAspera)
        {
            TextBoxLogWriteLine("Creating bulk ingest '{0}'...", IngestName);
            IIngestManifest manifest = _context.IngestManifests.Create(IngestName, IngestStorage);

            // Create the assets that will be associated with this bulk ingest manifest
            foreach (var asset in assetFiles)
            {
                try
                {
                    IAsset destAsset = _context.Assets.Create(asset.AssetName, assetStorage, encryptFiles ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
                    IIngestManifestAsset bulkAsset = manifest.IngestManifestAssets.Create(destAsset, asset.AssetFiles);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Bulk: Error when declaring asset '{0}'.", asset.AssetName, true);
                    TextBoxLogWriteLine(ex);
                    return;
                }
            }
            TextBoxLogWriteLine("Bulk: {0} asset(s) / {1} file(s) declared for bulk ingest container '{2}'.", assetFiles.Count, manifest.Statistics.PendingFilesCount, manifest.Name);


            // Encryption of files
            bool Error = false;
            if (encryptFiles)
            {

                TextBoxLogWriteLine("Encryption of asset files for bulk upload...");
                try
                {
                    manifest.EncryptFilesAsync(encryptToFolder, CancellationToken.None).Wait();
                    TextBoxLogWriteLine("Encryption of asset files done to folder {0}.", encryptToFolder);
                    Process.Start(encryptToFolder);
                }
                catch
                {
                    TextBoxLogWriteLine("Error when encrypting files to folder '{0}'.", encryptToFolder, true);
                    Error = true;
                }
            }

            if (!Error)
            {
                TextBoxLogWriteLine("You can upload the file(s) to {0}", manifest.BlobStorageUriForUpload);
                if (encryptFiles)
                {
                    TextBoxLogWriteLine("Encrypted files are in {0}", encryptToFolder);
                }
                if (GenerateAspera)
                {
                    string commandline = GenerateAsperaUrl(manifest);
                    var form = new EditorXMLJSON("Aspera Ingest URL", commandline, false, false, false);
                    form.Display();
                }

                if (GenerateSigniant)
                {
                    string commandline = GenerateSigniantCommandLine(manifest, assetFiles, encryptFiles, encryptToFolder, SigniantServers, SigniantAPIKey);
                    var form = new EditorXMLJSON("Signiant Command Line", commandline, false, false, false);
                    form.Display();
                }

                if (GenerateAzCopy)
                {
                    string commandline = GenerateAzCopyCommandLine(manifest, assetFiles, encryptFiles, encryptToFolder);
                    var form = new EditorXMLJSON("AzCopy Command Line", commandline, false, false, false);
                    form.Display();
                }
            }
            DoRefreshGridIngestManifestV(false);
        }

        private void copyIngestURLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyIngestURL();
        }

        private void DoCopyIngestURL()
        {
            var im = ReturnSelectedIngestManifests().FirstOrDefault();
            if (im != null)
            {
                string myurl = im.BlobStorageUriForUpload;
                var form = new EditorXMLJSON("Bulk Ingest URL", myurl, false, false, false);
                form.Display();
            }
        }

        private static string GenerateAsperaUrl(IIngestManifest im)
        {
            string storKey = "InsertStorageKey";
            if (im.StorageAccountName == _context.DefaultStorageAccount.Name && !string.IsNullOrEmpty(_credentials.StorageKey))
            {
                storKey = _credentials.StorageKey;
            }
            return "azu://" + im.StorageAccountName + ":" + storKey + "@" + im.BlobStorageUriForUpload.Substring(im.BlobStorageUriForUpload.IndexOf(".") + 1);
        }

        private static string GenerateSigniantCommandLine(IIngestManifest im, List<BulkUpload.BulkAsset> assetFiles, bool fileencrypted, string encryptedfilefolder, List<string> signantservers, string APIKey)
        {
            string storKey = "InsertStorageKey";
            if (im.StorageAccountName == _context.DefaultStorageAccount.Name && !string.IsNullOrEmpty(_credentials.StorageKey))
            {
                storKey = _credentials.StorageKey;
            }
            string server = signantservers[0];
            if (signantservers.Count == 2)
            {
                server = server + " --server " + signantservers[1];  // secondary server
            }
            var command = string.Format(@"sigcli --apikey {0} --direction upload --server {1} --account-name {2} --access-key {3} --container {4}",
                             APIKey,
                             server,
                             im.StorageAccountName,
                             storKey,
                             (new Uri(im.BlobStorageUriForUpload)).PathAndQuery.Substring(1));

            if (!fileencrypted)
            {
                foreach (var asset in assetFiles)
                {
                    foreach (var file in asset.AssetFiles)
                    {
                        command = command + string.Format(@" ""{0}""", file);
                    }
                }
            }
            else
            {
                foreach (var asset in im.IngestManifestAssets)
                {
                    foreach (var file in asset.IngestManifestFiles)
                    {
                        command = command + string.Format(@" ""{0}""", Path.Combine(encryptedfilefolder, file.Name));
                    }
                }
            }

            return command;
        }

        private static string GenerateAzCopyCommandLine(IIngestManifest im, List<BulkUpload.BulkAsset> assetFiles, bool fileencrypted, string encryptedfilefolder)
        {
            StringBuilder command = new StringBuilder();
            string storKey = "InsertStorageKey";
            if (im.StorageAccountName == _context.DefaultStorageAccount.Name && !string.IsNullOrEmpty(_credentials.StorageKey))
            {
                storKey = _credentials.StorageKey;
            }


            if (!fileencrypted)
            {
                foreach (var asset in assetFiles)
                {
                    foreach (var file in asset.AssetFiles)
                    {
                        command.AppendLine(
                          string.Format(@"AzCopy /Source:""{0}"" /Dest:{1} /DestKey:{2} /Pattern:""{3}""",
                          Path.GetDirectoryName(file),
                          im.BlobStorageUriForUpload,
                          storKey,
                          Path.GetFileName(file)
                          )
                          );
                    }
                }
            }
            else
            {
                foreach (var asset in im.IngestManifestAssets)
                {
                    foreach (var file in asset.IngestManifestFiles)
                    {
                        command.AppendLine(
                            string.Format(@"AzCopy /Source:""{0}"" /Dest:{1} /DestKey:{2} /Pattern:""{3}""",
                            encryptedfilefolder,
                            im.BlobStorageUriForUpload,
                            storKey,
                            im.StorageAccountName,
                            file.Name
                            )
                            );
                    }
                }
            }

            return command.ToString();
        }

        private void createTestAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateTestsAssets();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoBulkContainerInfo(ReturnSelectedIngestManifests().FirstOrDefault());
        }

        private void DoBulkContainerInfo(IIngestManifest manifest)
        {
            if (manifest != null)
            {
                var form = new BulkContainerInfo(this, _context, manifest);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    manifest.Name = form.IngestManifestName;
                    Task.Run(async () =>
                    {
                        await manifest.UpdateAsync();
                        DoRefreshGridIngestManifestV(false);
                    }
           );
                }
            }
        }


        private void contextMenuStripIngestManifests_Opening(object sender, CancelEventArgs e)
        {
            var manifests = ReturnSelectedIngestManifests();
            bool singleitem = (manifests.Count == 1);

            infoToolStripMenuItem.Enabled =
                copyIngestURLToClipboardToolStripMenuItem.Enabled =
                singleitem;

            deleteToolStripMenuItem3.Enabled = manifests.Count > 0;
        }

        private void dataGridViewIngestManifestsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var manifestId = dataGridViewIngestManifestsV.Rows[e.RowIndex].Cells[dataGridViewIngestManifestsV.Columns["Id"].Index].Value.ToString();
                DoBulkContainerInfo(_context.IngestManifests.Where(m => m.Id == manifestId).FirstOrDefault());
            }
        }

        private void toolStripMenuItem33Refresh_Click(object sender, EventArgs e)
        {
            DoRefreshGridIngestManifestV(false);
        }


        private void linkLabelFeedbackAMS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e)
        {
            DoMenuHyperlapseAssets();
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            DoMenuIndexAssets();
        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e)
        {
            DoMenuHyperlapseAssets();
        }

        private void toolStripMenuItemFaceDetector_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaFaceDetector, Bitmaps.face_detector);
        }

        private void toolStripMenuItemRedactor_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaRedactor, Bitmaps.media_redactor);
        }

        private void toolStripMenuItemMotionDetector_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaMotionDetector, Bitmaps.motion_detector);
        }

        private void toolStripMenuItemStabilizer_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaStabilizer, Bitmaps.media_stabilizer);
        }

        private void ProcessFaceDetectortoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaFaceDetector, Bitmaps.face_detector);
        }

        private void ProcessRedactortoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaRedactor, Bitmaps.media_redactor);
        }

        private void ProcessMotionDetectortoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaMotionDetector, Bitmaps.motion_detector);
        }

        private void ProcessStabilizertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuVideoAnalytics(Constants.AzureMediaStabilizer, Bitmaps.media_stabilizer);
        }

        private void transferToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var bulks = ReturnSelectedIngestManifests();
            bool single = bulks.Count == 1;
            bool oneOrMore = bulks.Count > 0;

            toolStripMenuItem36BulkIngestInfo.Enabled =
               toolStripMenuItem38CopyBulkURL.Enabled =
               single;

            toolStripMenuItem37DelBulk.Enabled = oneOrMore;
        }

        private void toolStripMenuItem33_Click_1(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMESystemPreset();

        }

        private void toolStripMenuItem36_Click_1(object sender, EventArgs e)
        {
            DoMenuEncodeWithAMEAdvanced();

        }

        private void toolStripMenuItemProgramAssetFilterInfo_Click(object sender, EventArgs e)
        {

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

        private void checkIntegrityOfLiveArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCheckIntegrityLiveArchive();
        }

        private void DoCheckIntegrityLiveArchive()
        {
            var assets = ReturnSelectedAssetsFromProgramsOrAssets();

            bool usercanceled = false;
            var storagekeys = BuildStorageKeyDictionary(assets, null, ref usercanceled, _context.DefaultStorageAccount.Name, _credentials.StorageKey, null);

            Task.Run(async () =>
            {
                var segments = AssetInfo.GetManifestSegmentsList(assets.FirstOrDefault());
                CheckListArchiveBlobs(storagekeys, assets.FirstOrDefault(), segments);
            });
        }

        private void checkIntegrityOfLiveArchiveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCheckIntegrityLiveArchive();
        }
    }
}



namespace AMSExplorer
{
    /// <summary>
    /// AataGridViewColumn implementation that provides a column that
    /// will display a progress bar.
    /// </summary>
    public class DataGridViewProgressBarColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewProgressBarColumn()
        {
            // Set the cell template
            CellTemplate = new DataGridViewProgressBarCell();

            // Set the default style padding
            Padding pad = new Padding(
              DataGridViewProgressBarCell.STANDARD_HORIZONTAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_VERTICAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_HORIZONTAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_VERTICAL_MARGIN);
            DefaultCellStyle.Padding = pad;

            // Set the default format
            DefaultCellStyle.Format = "## \\%";
        }
    }

    /// <summary>
    /// A DataGridViewCell class that is used to display a progress bar
    /// within a grid cell.
    /// </summary>
    public class DataGridViewProgressBarCell : DataGridViewTextBoxCell
    {
        /// <summary>
        /// Standard value to use for horizontal margins
        /// </summary>
        internal const int STANDARD_HORIZONTAL_MARGIN = 4;

        /// <summary>
        /// Standard value to use for vertical margins
        /// </summary>
        internal const int STANDARD_VERTICAL_MARGIN = 4;

        /// <summary>
        /// Constructor sets the expected type to int
        /// </summary>
        public DataGridViewProgressBarCell()
        {
            this.ValueType = typeof(int);
        }

        /// <summary>
        /// Paints the content of the cell
        /// </summary>
        protected override void Paint(Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds,
          int rowIndex, DataGridViewElementStates cellState,
          object value, object formattedValue,
          string errorText,
          DataGridViewCellStyle cellStyle,
          DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
        {
            int leftMargin = STANDARD_HORIZONTAL_MARGIN;
            int rightMargin = STANDARD_HORIZONTAL_MARGIN;
            int topMargin = STANDARD_VERTICAL_MARGIN;
            int bottomMargin = STANDARD_VERTICAL_MARGIN;
            int imgHeight = 1;
            int imgWidth = 1;
            int progressWidth = 1;
            PointF fontPlacement = new PointF(0, 0);

            int progressVal;
            if (value != null)
                //progressVal = (int)value;
                progressVal = (int)((double)value);

            else
                progressVal = 0;

            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            // Get margins from the style
            if (null != cellStyle)
            {
                leftMargin = cellStyle.Padding.Left;
                rightMargin = cellStyle.Padding.Right;
                topMargin = cellStyle.Padding.Top;
                bottomMargin = cellStyle.Padding.Bottom;
            }

            // Calculate the sizes
            imgHeight = cellBounds.Bottom - cellBounds.Top - (topMargin + bottomMargin);
            imgWidth = cellBounds.Right - cellBounds.Left - (leftMargin + rightMargin);
            if (imgWidth <= 0)
            {
                imgWidth = 1;
            }
            if (imgHeight <= 0)
            {
                imgHeight = 1;
            }

            // Calculate the progress
            progressWidth = (imgWidth * (progressVal) / 100);
            if (progressWidth <= 0)
            {
                if (progressVal > 0)
                {
                    progressWidth = 1;
                }
                else
                {
                    progressWidth = 0;
                }
            }

            // Calculate the font
            if (null != formattedValue)
            {
                SizeF availArea = new SizeF(imgWidth, imgHeight);
                SizeF fontSize = g.MeasureString(formattedValue.ToString(), cellStyle.Font, availArea);

                #region [Font Placement Calc]

                if (null == cellStyle)
                {
                    fontPlacement.Y = cellBounds.Y + topMargin + (((float)imgHeight - fontSize.Height) / 2);
                    fontPlacement.X = cellBounds.X + leftMargin + (((float)imgWidth - fontSize.Width) / 2);
                }
                else
                {
                    // Set the Y vertical position
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.BottomRight:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin + imgHeight - fontSize.Height;
                                break;
                            }
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.TopLeft:
                        case DataGridViewContentAlignment.TopRight:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin - fontSize.Height;
                                break;
                            }
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.NotSet:
                        default:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin + (((float)imgHeight - fontSize.Height) / 2);
                                break;
                            }
                    }
                    // Set the X horizontal position
                    switch (cellStyle.Alignment)
                    {

                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.TopLeft:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin;
                                break;
                            }
                        case DataGridViewContentAlignment.BottomRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.TopRight:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin + imgWidth - fontSize.Width;
                                break;
                            }
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.NotSet:
                        default:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin + (((float)imgWidth - fontSize.Width) / 2);
                                break;
                            }
                    }
                }
                #endregion [Font Placement Calc]
            }

            if (progressVal <= 100) // because when job is done or in error, we set progress > 100 % to avoid displaying the progress bar
            {
                // Draw the background
                System.Drawing.Rectangle backRectangle = new System.Drawing.Rectangle(cellBounds.X + leftMargin, cellBounds.Y + topMargin, imgWidth, imgHeight);
                using (SolidBrush backgroundBrush = new SolidBrush(Color.FromKnownColor(KnownColor.LightGray)))
                {
                    g.FillRectangle(backgroundBrush, backRectangle);
                }

                // Draw the progress bar
                if (progressWidth > 0)
                {
                    System.Drawing.Rectangle progressRectangle = new System.Drawing.Rectangle(cellBounds.X + leftMargin, cellBounds.Y + topMargin, progressWidth, imgHeight);
                    using (LinearGradientBrush progressGradientBrush = new LinearGradientBrush(progressRectangle, Color.LightGreen, Color.MediumSeaGreen, LinearGradientMode.Vertical))
                    {
                        progressGradientBrush.SetBlendTriangularShape((float).5);
                        g.FillRectangle(progressGradientBrush, progressRectangle);
                    }
                }

                // Draw the text
                if (null != formattedValue && null != cellStyle)
                {
                    using (SolidBrush fontBrush = new SolidBrush(cellStyle.ForeColor))
                    {
                        g.DrawString(formattedValue.ToString(), cellStyle.Font, fontBrush, fontPlacement);
                    }
                }
            }

        }
    }

}



namespace AMSExplorer
{
    public static class OrderAssets
    {
        public const string LastModifiedDescending = "Last modified >";
        public const string LastModifiedAscending = "Last modified <";
        public const string NameDescending = "Name >";
        public const string NameAscending = "Name <";
        public const string SizeDescending = "Size >";
        public const string SizeAscending = "Size <";
        public const string LocatorExpirationAscending = "Publication exp >";
        public const string LocatorExpirationDescending = "Publication exp <";

    }

    public static class OrderJobs
    {
        public const string LastModifiedDescending = "Last modified >";
        public const string LastModifiedAscending = "Last modified <";
        public const string StartTimeDescending = "Start Time >";
        public const string StartTimeAscending = "Start Time <";
        public const string EndTimeDescending = "End Time >";
        public const string EndTimeAscending = "End Time <";
        public const string ProcessTimeDescending = "Duration >";
        public const string ProcessTimeAscending = "Duration <";
        public const string NameDescending = "Name >";
        public const string NameAscending = "Name <";
        public const string StateDescending = "State >";
        public const string StateAscending = "State <";
    }

    public static class OrderStreamingEndpoints
    {
        public const string LastModified = "Last modified";
        public const string Name = "Name";
        public const string State = "State";
        public const string ScaleUnits = "Scale units";
    }


    public static class StatusAssets
    {
        public const string All = "All";
        public const string Published = "Published";
        public const string PublishedExpired = "Published but expired";
        public const string DynEnc = "With Dyn Enc";
        public const string Empty = "Empty";
    }

    public static class FilterTime
    {
        public const string First50Items = "First 50 items";
        public const string First1000Items = "First 1000 items";
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
        Processing = 1,
        Finished = 2,
        Error = 3,
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


    public class DataGridViewAssets : DataGridView
    {
        public string _statEnc = "StaticEncryption";
        public string _publication = "Publication";
        public string _dynEnc = "DynamicEncryption";
        public string _filter = "Filters";
        public string _statEncMouseOver = "StaticEncryptionMouseOver";
        public string _publicationMouseOver = "PublicationMouseOver";
        public string _dynEncMouseOver = "DynamicEncryptionMouseOver";
        public string _filterMouseOver = "FiltersMouseOver";

        public string _locatorexpirationdate = "LocatorExpirationDate";
        public string _locatorexpirationdatewarning = "LocatorExpirationDateWarning";

        static BindingList<AssetEntry> _MyObservAsset;
        public IEnumerable<IAsset> assets;
        static Dictionary<string, AssetEntry> cacheAssetentries = new Dictionary<string, AssetEntry>();

        static private int _assetsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static private bool _neveranalyzed = true;
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.AssetName, Text = "" };
        static private string _statefilter = "";
        static private string _timefilter = FilterTime.First50Items;
        static private TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        static string _orderassets = OrderAssets.LastModifiedDescending;
        static BackgroundWorker WorkerAnalyzeAssets;
        static CloudMediaContext _context;
        static Bitmap cancelimage = Bitmaps.cancel;
        static Bitmap envelopeencryptedimage = Bitmaps.envelope_encryption;
        static Bitmap storageencryptedimage = Bitmaps.storage_encryption;
        static Bitmap storagedecryptedimage = Bitmaps.storage_decryption;
        static Bitmap commonencryptedimage = Bitmaps.DRM_protection;
        static Bitmap unsupportedencryptedimage = Bitmaps.help;
        static Bitmap SASlocatorimage = Bitmaps.SAS_locator;
        static Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        static Bitmap AssetFilterImage = Bitmaps.filter;
        static Bitmap AssetFiltersImage = Bitmaps.filters;
        static Bitmap Redstreamimage = Program.MakeRed(Streaminglocatorimage);
        static Bitmap Reddownloadimage = Program.MakeRed(SASlocatorimage);
        static Bitmap Bluestreamimage = Program.MakeBlue(Streaminglocatorimage);
        static Bitmap Bluedownloadimage = Program.MakeBlue(SASlocatorimage);

        public int AssetsPerPage
        {
            get
            {
                return _assetsperpage;
            }
            set
            {
                _assetsperpage = value;
            }
        }
        public int PageCount
        {
            get
            {
                return _pagecount;
            }
        }
        public int CurrentPage
        {
            get
            {
                return _currentpage;
            }
        }
        public string OrderAssetsInGrid
        {
            get
            {
                return _orderassets;
            }
            set
            {
                _orderassets = value;
            }
        }
        public bool Initialized
        {
            get
            {
                return _initialized;
            }
        }
        public SearchObject SearchInName
        {
            get
            {
                return _searchinname;
            }
            set
            {
                _searchinname = value;
            }
        }


        public string StateFilter
        {
            get
            {
                return _statefilter;
            }
            set
            {
                _statefilter = value;
            }
        }
        public string TimeFilter
        {
            get
            {
                return _timefilter;
            }
            set
            {
                _timefilter = value;
            }
        }
        public TimeRangeValue TimeFilterTimeRange
        {
            get
            {
                return _timefilterTimeRange;
            }
            set
            {
                _timefilterTimeRange = value;
            }
        }
        public int DisplayedCount
        {
            get
            {
                return _MyObservAsset.Count();
            }
        }


        public void Init(CloudMediaContext context)
        {
            Debug.WriteLine("AssetsInit");

            IEnumerable<AssetEntry> assetquery;
            _context = context;

            assetquery = from a in context.Assets.Take(0)
                         orderby a.LastModified descending
                         select new AssetEntry
                         {
                             Name = a.Name,
                             Id = a.Id,
                             LastModified = ((DateTime)a.LastModified).ToLocalTime().ToString("G"),
                             Storage = a.StorageAccountName
                         };

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle()
            {
                NullValue = null,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            DataGridViewImageColumn imageCol = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _publication,
                DataPropertyName = _publication,
            };
            this.Columns.Add(imageCol);

            DataGridViewImageColumn imageCol2 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _statEnc,
                DataPropertyName = _statEnc,
            };
            this.Columns.Add(imageCol2);

            DataGridViewImageColumn imageCol3 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _dynEnc,
                DataPropertyName = _dynEnc,
            };
            this.Columns.Add(imageCol3);

            DataGridViewImageColumn imageCol4 = new DataGridViewImageColumn()
            {
                DefaultCellStyle = cellstyle,
                Name = _filter,
                DataPropertyName = _filter,
            };
            this.Columns.Add(imageCol4);

            BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(assetquery.Take(0).ToList()); // just to create columns
            this.DataSource = MyObservAssethisPage;

            int lastColumn_sIndex = this.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).DisplayIndex;
            this.Columns[_statEncMouseOver].Visible = false;
            this.Columns[_dynEncMouseOver].Visible = false;
            this.Columns[_publicationMouseOver].Visible = false;
            this.Columns[_filterMouseOver].Visible = false;

            this.Columns[_locatorexpirationdatewarning].Visible = false; // used to store warning and put color in red

            this.Columns["Type"].HeaderText = "Type (streams nb)";
            this.Columns["LastModified"].HeaderText = "Last modified";
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
            this.Columns["Storage"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
            this.Columns["SizeLong"].Visible = false;
            this.Columns[_filter].DisplayIndex = lastColumn_sIndex;
            this.Columns[_filter].DefaultCellStyle.NullValue = null;
            this.Columns[_publication].DisplayIndex = lastColumn_sIndex - 1;
            this.Columns[_publication].DefaultCellStyle.NullValue = null;
            this.Columns[_dynEnc].DisplayIndex = lastColumn_sIndex - 2;
            this.Columns[_dynEnc].DefaultCellStyle.NullValue = null;
            this.Columns[_statEnc].DisplayIndex = lastColumn_sIndex - 3;
            this.Columns[_statEnc].DefaultCellStyle.NullValue = null;

            this.Columns[_statEnc].HeaderText = "Static Encryption";
            this.Columns[_dynEnc].HeaderText = "Dynamic Encryption";

            this.Columns["Type"].Width = 140;
            this.Columns["Size"].Width = 80;
            this.Columns[_statEnc].Width = 80;
            this.Columns[_dynEnc].Width = 80;
            this.Columns[_publication].Width = 90;
            this.Columns[_filter].Width = 50;
            this.Columns[_locatorexpirationdate].HeaderText = "Publication Expiration";
            this.Columns[_locatorexpirationdate].DisplayIndex = this.Columns.Count - 1;
            this.Columns[_locatorexpirationdate].Width = 130;
            this.Columns["LastModified"].Width = 140;
            this.Columns["Id"].Width = 300;
            this.Columns["Storage"].Width = 140;

            WorkerAnalyzeAssets = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true
            };
            WorkerAnalyzeAssets.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerAnalyzeAssets_DoWork);

            _initialized = true;
        }

        private void WorkerAnalyzeAssets_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("WorkerAnalyzeAssets_DoWork");
            BackgroundWorker worker = sender as BackgroundWorker;
            IAsset asset = null;

            PublishStatus SASLoc;
            PublishStatus OrigLoc;

            var listae = _MyObservAsset.OrderBy(a => cacheAssetentries.ContainsKey(a.Id)).ToList(); // as priority, assets not yet analyzed

            foreach (AssetEntry AE in listae)
            {
                try
                {
                    asset = _context.Assets.Where(a => a.Id == AE.Id).FirstOrDefault();
                    if (asset != null)
                    {
                        AssetInfo myAssetInfo = new AssetInfo(asset);
                        AE.Name = asset.Name;
                        AE.LastModified = asset.LastModified.ToLocalTime().ToString("G");
                        SASLoc = myAssetInfo.GetPublishedStatus(LocatorType.Sas);
                        OrigLoc = myAssetInfo.GetPublishedStatus(LocatorType.OnDemandOrigin);

                        AssetBitmapAndText assetBitmapAndText = ReturnStaticProtectedBitmap(asset);
                        AE.StaticEncryption = assetBitmapAndText.bitmap;
                        AE.StaticEncryptionMouseOver = assetBitmapAndText.MouseOverDesc;

                        assetBitmapAndText = BuildBitmapPublication(asset);
                        AE.Publication = assetBitmapAndText.bitmap;
                        AE.PublicationMouseOver = assetBitmapAndText.MouseOverDesc;

                        AE.Type = AssetInfo.GetAssetType(asset);
                        AE.SizeLong = myAssetInfo.GetSize();
                        AE.Size = AssetInfo.FormatByteSize(AE.SizeLong);

                        assetBitmapAndText = BuildBitmapDynEncryption(asset);
                        AE.DynamicEncryption = assetBitmapAndText.bitmap;
                        AE.DynamicEncryptionMouseOver = assetBitmapAndText.MouseOverDesc;

                        DateTime? LocDate = asset.Locators.Any() ? (DateTime?)asset.Locators.Min(l => l.ExpirationDateTime).ToLocalTime() : null;
                        AE.LocatorExpirationDate = LocDate.HasValue ? ((DateTime)LocDate).ToLocalTime().ToString() : null;
                        AE.LocatorExpirationDateWarning = LocDate.HasValue ? (LocDate < DateTime.Now.ToLocalTime()) : false;

                        assetBitmapAndText = BuildBitmapAssetFilters(asset);
                        AE.Filters = assetBitmapAndText.bitmap;
                        AE.FiltersMouseOver = assetBitmapAndText.MouseOverDesc;

                        cacheAssetentries[asset.Id] = AE; // let's put it in cache (or update the cache)
                    }
                }
                catch // in some case, we have a timeout on Assets.Where...
                {

                }
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }

            }
            this.BeginInvoke(new Action(() => this.Refresh()), null);
        }

        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if (_neveranalyzed) // first display of the page, let's analyzed the assets
            {
                _neveranalyzed = false;
                AnalyzeItemsInBackground();

            }
            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(_MyObservAsset.Skip(_assetsperpage * (page - 1)).Take(_assetsperpage).ToList());

                this.DataSource = MyObservAssethisPage;
            }
        }

        public void PurgeCacheAssets(List<IAsset> assets)
        {
            assets.ToList().ForEach(a => cacheAssetentries.Remove(a.Id));
        }

        public void PurgeCacheAsset(IAsset asset)
        {
            cacheAssetentries.Remove(asset.Id);
        }

        public void RefreshAssets(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;
            Debug.WriteLine("RefreshAssets Start");

            if (WorkerAnalyzeAssets.IsBusy)
            {
                // cancel the analyze.
                WorkerAnalyzeAssets.CancelAsync();
            }
            this.FindForm().Cursor = Cursors.WaitCursor;


            // DAYS
            bool filterStartDate = false;
            bool filterEndDate = false;

            DateTime dateTimeStart = DateTime.UtcNow;
            DateTime dateTimeRangeEnd = DateTime.UtcNow.AddDays(1);

            int days = FilterTime.ReturnNumberOfDays(_timefilter);
            if (days > 0)
            {
                filterStartDate = true;
                dateTimeStart = (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)));
            }
            else if (days == -1) // TimeRange
            {
                filterStartDate = true;
                filterEndDate = true;
                dateTimeStart = _timefilterTimeRange.StartDate;
                if (_timefilterTimeRange.EndDate != null) // there is an end time
                {
                    dateTimeRangeEnd = (DateTime)_timefilterTimeRange.EndDate;
                }
            }

            IQueryable<IAsset> assetsServerQuery = null;// = context.Assets.AsQueryable(); ;
            bool SwitchedToLocalQuery = false;

            ///////////////////////
            // SEARCH
            ///////////////////////
            if (_searchinname != null && !string.IsNullOrEmpty(_searchinname.Text))
            {
                bool Error = false;
                int skipSize = 0;
                int batchSize = 1000;
                int currentSkipSize = 0;
                string strsearch = _searchinname.Text.ToLower();

                switch (_searchinname.SearchType)
                {
                    // Search on Asset name
                    case SearchIn.AssetName:

                        assetsServerQuery = context.Assets.Where(a =>
                                (a.Name.Contains(_searchinname.Text))
                                &&
                                (!filterStartDate || a.LastModified > dateTimeStart)
                                &&
                                (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                );
                        /*
                        if (assetsServerQuery.Count() > 1000) // we need to paginate
                        {

                        IList<IAsset> newAssetList = new List<IAsset>();

                        while (true)
                        {
                            // Enumerate through all assets (1000 at a time)
                                var assetsq = assetsServerQuery
                                .Skip(skipSize).Take(batchSize).ToList();

                            currentSkipSize += assetsq.Count;

                            foreach (var a in assetsq)
                            {
                                newAssetList.Add(a);
                            }

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

                        SwitchedToLocalQuery = true;
                        assets = newAssetList;
                        }
                        */
                        break;

                    // Search on Asset aternate id
                    case SearchIn.AssetAltId:
                        assetsServerQuery = context.Assets.Where(a =>
                                  (a.AlternateId.Contains(_searchinname.Text))
                                  &&
                                  (!filterStartDate || a.LastModified > dateTimeStart)
                                  &&
                                  (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                  );
                        break;

                    // Search on Asset ID
                    case SearchIn.AssetId:
                        string assetguid = _searchinname.Text;
                        if (assetguid.StartsWith(Constants.AssetIdPrefix))
                        {
                            assetguid = assetguid.Substring(Constants.AssetIdPrefix.Length);
                        }
                        try
                        {
                            var g = new Guid(assetguid);
                        }
                        catch
                        {
                            Error = true;
                            MessageBox.Show("Error with asset Id. Is it a valid GUID or asset Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (!Error)
                        {
                            assetsServerQuery = context.Assets.Where(a =>
                                                             (a.Id == Constants.AssetIdPrefix + assetguid)
                                                             &&
                                                             (!filterStartDate || a.LastModified > dateTimeStart)
                                                             &&
                                                             (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                                             );
                        }
                        break;


                    // Search on Asset file name
                    case SearchIn.AssetFileName:
                        IList<string> assetFileListID = new List<string>();

                        while (true)
                        {
                            // Enumerate through all asset files (1000 at a time)
                            var filesq = context.Files
                                .Skip(skipSize).Take(batchSize).ToList();

                            currentSkipSize += filesq.Count;
                            var filesq2 = filesq.Where(f => f.Name.ToLower().Contains(strsearch)).Select(f => f.ParentAssetId);

                            foreach (var a in filesq2)
                            {
                                assetFileListID.Add(a);
                            }

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

                        var assetlist = new List<IAsset>();
                        foreach (var a in assetFileListID.Distinct())
                        {
                            assetlist.Add(AssetInfo.GetAsset(a, context));
                        }

                        SwitchedToLocalQuery = true;
                        assets = assetlist.Where(a =>
                                (!filterStartDate || a.LastModified > dateTimeStart)
                                &&
                                (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                );


                        break;

                    // Search on Asset file ID
                    case SearchIn.AssetFileId:
                        string fileguid = _searchinname.Text;
                        if (fileguid.StartsWith(Constants.AssetFileIdPrefix))
                        {
                            fileguid = fileguid.Substring(Constants.AssetFileIdPrefix.Length);
                        }
                        try
                        {
                            var g = new Guid(fileguid);
                        }
                        catch
                        {
                            Error = true;
                            MessageBox.Show("Error with file asset Id. Is it a valid GUID or asset Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (!Error)
                        {
                            var myfile = context.Files.Where(f => f.Id == Constants.AssetFileIdPrefix + fileguid).FirstOrDefault();
                            if (myfile != null)
                            {
                                assetsServerQuery = context.Assets.Where(a =>
                                                                   (!filterStartDate || a.LastModified > dateTimeStart)
                                                                   &&
                                                                   (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                                                   &&
                                                                   myfile.Asset.Id == a.Id
                                                                   );
                            }
                            else
                            {
                                MessageBox.Show("No file was found with this Id.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;

                    // Search on Locator id / guid
                    case SearchIn.LocatorId:
                        string locatorguid = _searchinname.Text;
                        if (locatorguid.StartsWith(Constants.LocatorIdPrefix))
                        {
                            locatorguid = locatorguid.Substring(Constants.LocatorIdPrefix.Length);
                        }
                        try
                        {
                            var g = new Guid(locatorguid);
                        }
                        catch
                        {
                            Error = true;
                            MessageBox.Show("Error with locator Id. Is it a valid GUID or locator Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (!Error)
                        {
                            var myloc = context.Locators.Where(l => l.Id == Constants.LocatorIdPrefix + locatorguid).FirstOrDefault();
                            if (myloc != null)
                            {
                                assetsServerQuery = context.Assets.Where(a =>
                                                                    (!filterStartDate || a.LastModified > dateTimeStart)
                                                                    &&
                                                                    (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                                                    &&
                                                                    a.Id == myloc.AssetId
                                                                    );
                            }
                            else
                            {
                                MessageBox.Show("No locator was found using this locator Id.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;

                    // Search on Program id / guid
                    case SearchIn.ProgramId:
                        string programguid = _searchinname.Text;
                        if (programguid.StartsWith(Constants.ProgramIdPrefix))
                        {
                            programguid = programguid.Substring(Constants.ProgramIdPrefix.Length);
                        }
                        try
                        {
                            var g = new Guid(programguid);
                        }
                        catch
                        {
                            Error = true;
                            MessageBox.Show("Error with program Id. Is it a valid GUID or program Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (!Error)
                        {
                            var queryprog = context.Programs.Where(p => p.Id == Constants.ProgramIdPrefix + programguid).FirstOrDefault();
                            if (queryprog != null)
                            {
                                assetsServerQuery = context.Assets.Where(a =>
                                                                   (!filterStartDate || a.LastModified > dateTimeStart)
                                                                   &&
                                                                   (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                                                   &&
                                                                   queryprog.AssetId == a.Id
                                                                   );
                            }
                            else
                            {
                                MessageBox.Show("No program was found with this Id.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;

                    // Search on Program name
                    case SearchIn.ProgramName:

                        IList<string> assetFileListIDP = new List<string>();

                        while (true)
                        {
                            // Enumerate through all programs (1000 at a time)
                            var programsq = context.Programs
                                .Skip(skipSize).Take(batchSize).ToList();

                            currentSkipSize += programsq.Count;
                            var programsq2 = programsq.Where(p => p.Name.ToLower().Contains(strsearch)).Select(p => p.AssetId);

                            foreach (var a in programsq2)
                            {
                                assetFileListIDP.Add(a);
                            }

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

                        var assetlist2 = new List<IAsset>();
                        foreach (var a in assetFileListIDP)
                        {
                            assetlist2.Add(AssetInfo.GetAsset(a, context));
                        }

                        SwitchedToLocalQuery = true;
                        assets = assetlist2.Where(a =>
                                (!filterStartDate || a.LastModified > dateTimeStart)
                                &&
                                (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                );

                        break;


                    default:
                        break;
                }
            }
            else // NO SEARCH
            {
                assetsServerQuery = context.Assets.Where(a =>
                                (!filterStartDate || a.LastModified > dateTimeStart)
                                &&
                                (!filterEndDate || a.LastModified < dateTimeRangeEnd)
                                );
            }

            if (!SwitchedToLocalQuery && assetsServerQuery == null) // teh current query did not find asset (locator id search for example)
            {
                assets = _context.Assets.AsEnumerable().Take(0);
                SwitchedToLocalQuery = true;
            }


            // SHORTCUT (needed for account with large number fo assets)
            if (!SwitchedToLocalQuery && (_statefilter == StatusAssets.All || _statefilter == "") && _orderassets == OrderAssets.LastModifiedDescending)
            {
                if (_timefilter == FilterTime.First50Items)
                {
                    assets = assetsServerQuery.Take(50);
                }
                else if (_timefilter == FilterTime.First1000Items)
                {
                    assets = assetsServerQuery.Take(1000);
                }
                else
                {
                    assets = assetsServerQuery;
                }
            }
            else // general case

            {

                ///////////////////////
                // STATE FILTER
                ///////////////////////
                // we need to do paging
                // not excuted for large account as state filter control is disabled

                IList<IAsset> aggregateListAssets = new List<IAsset>();
                int skipSize2 = 0;
                int batchSize2 = 1000;
                int currentSkipSize2 = 0;

                while (true)
                {
                    // Enumerate through all assets (1000 at a time)
                    IEnumerable<IAsset> listPagedAssets;
                    IList<IAsset> fassets = new List<IAsset>();

                    if (SwitchedToLocalQuery)
                    {
                        listPagedAssets = assets.Skip(skipSize2).Take(batchSize2).ToList();
                    }
                    else
                    {
                        listPagedAssets = assetsServerQuery.Skip(skipSize2).Take(batchSize2).ToList();
                    }
                    currentSkipSize2 += listPagedAssets.Count();

                    switch (_statefilter)
                    {
                        case StatusAssets.Published:
                        case StatusAssets.PublishedExpired:

                            bool bexpired = _statefilter == StatusAssets.PublishedExpired;
                            IList<IAsset> newListAssets1 = new List<IAsset>();

                            int skipSizeLoc = 0;
                            int batchSizeLoc = 1000;
                            int currentSkipSizeLoc = 0;

                            while (true)
                            {
                                // Enumerate through all locators (1000 at a time)
                                var listlocators = context.Locators.Where(l => !bexpired || l.ExpirationDateTime < DateTime.UtcNow).Skip(skipSizeLoc).Take(batchSizeLoc).ToList().Select(l => l.AssetId).ToList();
                                currentSkipSizeLoc += listlocators.Count;

                                var assetexpired = listPagedAssets.Where(a => listlocators.Contains(a.Id));

                                foreach (var a in assetexpired)
                                {
                                    newListAssets1.Add(a);
                                }

                                if (currentSkipSizeLoc == batchSizeLoc)
                                {
                                    skipSizeLoc += batchSizeLoc;
                                    currentSkipSizeLoc = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            foreach (var a in newListAssets1)
                            {
                                fassets.Add(a);
                            }
                            break;


                        case StatusAssets.DynEnc:
                            var assetwithDelPol = listPagedAssets.Where(a => a.DeliveryPolicies.Any());
                            foreach (var a in assetwithDelPol)
                            {
                                fassets.Add(a);
                            }
                            break;

                        case StatusAssets.Empty:

                            IList<IAsset> newListAssets2 = listPagedAssets.ToList();

                            int skipSizeEmpty = 0;
                            int batchSizeEmpty = 1000;
                            int currentSkipSizeEmpty = 0;

                            while (true)
                            {
                                // Enumerate through all files (1000 at a time)
                                var listfiles = context.Files.Where(f => f.ContentFileSize > 0).Skip(skipSizeEmpty).Take(batchSizeEmpty).ToList().Select(f => f.ParentAssetId).ToList();
                                currentSkipSizeEmpty += listfiles.Count;

                                var assetsnotempty = listPagedAssets.Where(a => listfiles.Contains(a.Id)).ToList(); ;

                                foreach (var a in assetsnotempty)
                                {
                                    newListAssets2.Remove(a); // if file with size >0, then we remove it parenrt id from the lis
                                }

                                if (currentSkipSizeEmpty == batchSizeEmpty)
                                {
                                    skipSizeEmpty += batchSizeEmpty;
                                    currentSkipSizeEmpty = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            foreach (var a in newListAssets2)
                            {
                                fassets.Add(a);
                            }
                            break;


                        case StatusAssets.All: // we need this to parse all assets
                        default:
                            foreach (var a in listPagedAssets)
                            {
                                fassets.Add(a);
                            }
                            break;
                            /*

                            // below is REMOVED  as queries are executed by the back-end in order to process all assets and be quick. Th equery below needs to be
                            // executed locally and would be slow. Could be reintroduce if customer demand.

                        case StatusAssets.NotPublished:
                            fassets = listassets.Where(a => a.Locators.Count == 0);
                            break;
                        case StatusAssets.Storage:
                            fassets = listassets.Where(a => a.Options == AssetCreationOptions.StorageEncrypted);
                            break;
                        case StatusAssets.CENC:
                            fassets = listassets.Where(a => a.Options == AssetCreationOptions.CommonEncryptionProtected);
                            break;
                        case StatusAssets.Envelope:
                            fassets = listassets.Where(a => a.Options == AssetCreationOptions.EnvelopeEncryptionProtected);
                            break;
                        case StatusAssets.NotEncrypted:
                            fassets = listassets.Where(a => a.Options == AssetCreationOptions.None);
                            break;
                        case StatusAssets.DynEnc:
                            fassets = listassets.Where(a => a.DeliveryPolicies.Any());
                            break;
                        case StatusAssets.Streamable:
                            fassets = listassets.Where(a => a.IsStreamable);
                            break;
                        case StatusAssets.SupportDynEnc:
                            fassets = listassets.Where(a => a.SupportsDynamicEncryption);
                            break;
                        case StatusAssets.Empty:
                            fassets = listassets.Where(a => a.AssetFiles.Count() == 0);
                            break;
                        case StatusAssets.DefaultStorage:
                            fassets = listassets.Where(a => a.StorageAccountName == _context.DefaultStorageAccount.Name);
                            break;
                        case StatusAssets.NotDefaultStorage:
                            fassets = listassets.Where(a => a.StorageAccountName != _context.DefaultStorageAccount.Name);
                            break;
                            */
                    }

                    foreach (var a in fassets)
                    {
                        aggregateListAssets.Add(a);
                    }

                    if (currentSkipSize2 == batchSize2)
                    {
                        skipSize2 += batchSize2;
                        currentSkipSize2 = 0;
                    }
                    else
                    {
                        break;
                    }
                }
                SwitchedToLocalQuery = true;
                assets = aggregateListAssets;

                ///////////////////////
                // SORTING
                ///////////////////////
                // let's sort the aggregate results
                var size = new Func<IAsset, long>(AssetInfo.GetSize);

                // client side only ! (a take is done otherwise)

                switch (_orderassets)
                {
                    case OrderAssets.LastModifiedDescending:
                        assets = from a in assets orderby a.LastModified descending select a;
                        break;

                    case OrderAssets.LastModifiedAscending:
                        assets = from a in assets orderby a.LastModified ascending select a;
                        break;

                    case OrderAssets.NameAscending:
                        assets = from a in assets orderby a.Name ascending select a;
                        break;

                    case OrderAssets.NameDescending:
                        assets = from a in assets orderby a.Name descending select a;
                        break;

                    case OrderAssets.SizeDescending:
                        assets = from a in assets orderby size(a) descending select a;
                        break;

                    case OrderAssets.SizeAscending:
                        assets = from a in assets orderby size(a) ascending select a;
                        break;

                    case OrderAssets.LocatorExpirationAscending:
                        assets = from a in assets where a.Locators.Any() orderby a.Locators.Min(l => l.ExpirationDateTime) ascending select a;
                        break;

                    case OrderAssets.LocatorExpirationDescending:
                        assets = from a in assets where a.Locators.Any() orderby a.Locators.Min(l => l.ExpirationDateTime) descending select a;
                        break;

                    default:
                        assets = from a in assets orderby a.LastModified descending select a;
                        break;
                }

                if (_timefilter == FilterTime.First50Items)
                {
                    assets = assets.Take(50);
                }
                else // if (_timefilter == FilterTime.First1000Items)
                {
                    assets = assets.Take(1000);
                }

            }// end of general case

            _context = context;
            _pagecount = (int)Math.Ceiling(((double)assets.Count()) / ((double)_assetsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            try
            {
                IEnumerable<AssetEntry> assetquery = assets.AsEnumerable().Select(a =>
               // let's return the data cached in memory of it exists and last modified time is the same
               (cacheAssetentries.ContainsKey(a.Id)
               && cacheAssetentries[a.Id].LastModified != null
               && (cacheAssetentries[a.Id].LastModified == a.LastModified.ToLocalTime().ToString("G")) ?
               cacheAssetentries[a.Id] :
                             new AssetEntry
                             {
                                 Name = a.Name,
                                 Id = a.Id,
                                 Type = null,
                                 LastModified = a.LastModified.ToLocalTime().ToString("G"),
                                 Storage = a.StorageAccountName
                             }
                              ));

                _MyObservAsset = new BindingList<AssetEntry>(assetquery.ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + Constants.endline + Program.GetErrorMessage(e));
                Environment.Exit(0);
            }

            BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(_MyObservAsset.Skip(_assetsperpage * (_currentpage - 1)).Take(_assetsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = MyObservAssethisPage));
            _refreshedatleastonetime = true;

            Debug.WriteLine("RefreshAssets End");
            AnalyzeItemsInBackground();

            this.FindForm().Cursor = Cursors.Default;
        }


        public void AnalyzeItemsInBackground()
        {
            Task.Run(() =>
       {
           WorkerAnalyzeAssets.CancelAsync();
           // let's wait a little for the previous worker to cancel if needed
           System.Threading.Thread.Sleep(2000);

           if (WorkerAnalyzeAssets.IsBusy != true)
           {
               // Start the asynchronous operation.
               try
               {
                   WorkerAnalyzeAssets.RunWorkerAsync();
               }
               catch { }
           }
       });

        }



        public static AssetBitmapAndText BuildBitmapPublication(IAsset asset)
        {
            if (asset == null) return null;

            Bitmap returnedImage = null;
            string returnedText = null;

            foreach (var locator in asset.Locators)
            {
                Bitmap newbitmap = null;
                string newtext = null;
                PublishStatus Status = AssetInfo.GetPublishedStatusForLocator(locator);

                switch (locator.Type)
                {

                    case (LocatorType.OnDemandOrigin):
                        switch (Status)
                        {
                            case PublishStatus.PublishedActive:
                                newbitmap = Streaminglocatorimage;
                                newtext = "Active Streaming locator";
                                break;

                            case PublishStatus.PublishedExpired:
                                newbitmap = Redstreamimage;
                                newtext = "Expired Streaming locator";
                                break;

                            case PublishStatus.PublishedFuture:
                                newbitmap = Bluestreamimage;
                                newtext = "Future Streaming locator";
                                break;

                            case PublishStatus.NotPublished:
                            default:
                                break;
                        }
                        break;

                    case (LocatorType.Sas):
                        switch (Status)
                        {
                            case PublishStatus.PublishedActive:
                                newbitmap = SASlocatorimage;
                                newtext = "Active SAS locator";
                                break;

                            case PublishStatus.PublishedExpired:
                                newbitmap = Reddownloadimage;
                                newtext = "Expired SAS locator";
                                break;

                            case PublishStatus.PublishedFuture:
                                newbitmap = Bluedownloadimage;
                                newtext = "Future SAS locator";
                                break;

                            case PublishStatus.NotPublished:
                            default:
                                break;
                        }
                        break;


                    default:
                        break;
                }

                returnedImage = AddBitmap(returnedImage, newbitmap);
                returnedText += !string.IsNullOrEmpty(newtext) ? newtext + Constants.endline : string.Empty;
            }

            return new AssetBitmapAndText()
            {
                bitmap = returnedImage,
                MouseOverDesc = returnedText ?? "Not published"
            };
        }

        private static Bitmap AddBitmap(Bitmap bitmap1, Bitmap bitmap2)
        {
            Bitmap returnedbitmap;
            if (bitmap1 != null)
            {
                returnedbitmap = new Bitmap((bitmap1.Width + bitmap2.Width), bitmap1.Height);
                using (Graphics graphicsObject = Graphics.FromImage(returnedbitmap))
                {
                    graphicsObject.DrawImage(bitmap1, new Point(0, 0));
                    graphicsObject.DrawImage(bitmap2, new Point(bitmap1.Width, 0));
                }

            }
            else
            {
                returnedbitmap = bitmap2;
            }

            return returnedbitmap;
        }




        private AssetBitmapAndText ReturnStaticProtectedBitmap(IAsset asset)
        {
            AssetBitmapAndText ABT = new AssetBitmapAndText();

            switch (asset.Options)
            {
                case AssetCreationOptions.StorageEncrypted:
                    ABT.bitmap = storageencryptedimage;
                    ABT.MouseOverDesc = "Storage encrypted";
                    break;

                case AssetCreationOptions.CommonEncryptionProtected:
                    ABT.bitmap = commonencryptedimage;
                    ABT.MouseOverDesc = "CENC encrypted";
                    break;

                case AssetCreationOptions.EnvelopeEncryptionProtected:
                    ABT.bitmap = envelopeencryptedimage;
                    ABT.MouseOverDesc = "Envelope encrypted";
                    break;

                default:
                    break;
            }
            return ABT;
        }


        private static AssetBitmapAndText BuildBitmapAssetFilters(IAsset asset)
        {
            var filcount = asset.AssetFilters.Count();

            if (filcount == 0)
            {
                return new AssetBitmapAndText();
            }
            else if (filcount == 1)
            {
                return new AssetBitmapAndText()
                {
                    bitmap = AssetFilterImage,
                    MouseOverDesc = "1 filter"
                }; ;
            }
            else // >1
            {
                return new AssetBitmapAndText()
                {
                    bitmap = AssetFiltersImage,
                    MouseOverDesc = string.Format("{0} filters", filcount)
                };
            }
        }


        private static AssetBitmapAndText BuildBitmapDynEncryption(IAsset asset)
        {
            AssetBitmapAndText ABT = new AssetBitmapAndText();
            AssetEncryptionState assetEncryptionState = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming | AssetDeliveryProtocol.HLS | AssetDeliveryProtocol.Dash);

            switch (assetEncryptionState)
            {
                case AssetEncryptionState.DynamicCommonEncryption:
                    ABT.bitmap = commonencryptedimage;
                    ABT.MouseOverDesc = "Dynamic Common Encryption (CENC)";
                    break;

                case AssetEncryptionState.DynamicEnvelopeEncryption:
                    ABT.bitmap = envelopeencryptedimage;
                    ABT.MouseOverDesc = "Dynamic Envelope Encryption (AES)";
                    break;

                case AssetEncryptionState.NoDynamicEncryption:
                    ABT.bitmap = storagedecryptedimage;
                    ABT.MouseOverDesc = "No Dynamic Encryption";
                    break;

                case AssetEncryptionState.NoSinglePolicyApplies:
                    AssetEncryptionState assetEncryptionStateHLS = asset.GetEncryptionState(AssetDeliveryProtocol.HLS);
                    AssetEncryptionState assetEncryptionStateSmooth = asset.GetEncryptionState(AssetDeliveryProtocol.SmoothStreaming);
                    AssetEncryptionState assetEncryptionStateDash = asset.GetEncryptionState(AssetDeliveryProtocol.Dash);
                    bool CENCEnable = (assetEncryptionStateHLS == AssetEncryptionState.DynamicCommonEncryption || assetEncryptionStateSmooth == AssetEncryptionState.DynamicCommonEncryption || assetEncryptionStateDash == AssetEncryptionState.DynamicCommonEncryption);
                    bool EnvelopeEnable = (assetEncryptionStateHLS == AssetEncryptionState.DynamicEnvelopeEncryption || assetEncryptionStateSmooth == AssetEncryptionState.DynamicEnvelopeEncryption || assetEncryptionStateDash == AssetEncryptionState.DynamicEnvelopeEncryption);
                    if (CENCEnable && EnvelopeEnable)
                    {
                        ABT.bitmap = new Bitmap((envelopeencryptedimage.Width + commonencryptedimage.Width), envelopeencryptedimage.Height);
                        using (Graphics graphicsObject = Graphics.FromImage(ABT.bitmap))
                        {
                            graphicsObject.DrawImage(envelopeencryptedimage, new Point(0, 0));
                            graphicsObject.DrawImage(commonencryptedimage, new Point(envelopeencryptedimage.Width, 0));
                        }
                    }
                    else
                    {
                        ABT.bitmap = CENCEnable ? commonencryptedimage : envelopeencryptedimage;
                    }
                    ABT.MouseOverDesc = "Multiple policies";
                    break;

                default:
                    break;
            }
            return ABT;
        }
    }



    public class DataGridViewJobs : DataGridView
    {

        public int JobssPerPage
        {
            get
            {
                return _jobsperpage;
            }
            set
            {
                _jobsperpage = value;
            }
        }
        public int PageCount
        {
            get
            {
                return _pagecount;
            }

        }
        public int CurrentPage
        {
            get
            {
                return _currentpage;
            }

        }
        public string OrderJobsInGrid
        {
            get
            {
                return _orderjobs;
            }
            set
            {
                _orderjobs = value;
            }

        }
        public string FilterJobsState
        {
            get
            {
                return _filterjobsstate;
            }
            set
            {
                _filterjobsstate = value;
            }

        }
        public SearchObject SearchInName
        {
            get
            {
                return _searchinname;
            }
            set
            {
                _searchinname = value;
            }

        }
        public bool Initialized
        {
            get
            {
                return _initialized;
            }

        }
        public string TimeFilter
        {
            get
            {
                return _timefilter;
            }
            set
            {
                _timefilter = value;
            }
        }

        public TimeRangeValue TimeFilterTimeRange
        {
            get
            {
                return _timefilterTimeRange;
            }
            set
            {
                _timefilterTimeRange = value;
            }
        }
        public int DisplayedCount
        {
            get
            {
                return _MyObservJob.Count();
            }

        }

        static BindingList<JobEntry> _MyObservJob;
        static BindingList<JobEntry> _MyObservAssethisPage;
        public IEnumerable<IJob> jobs;
        //static IEnumerable<IJob> jobs;
        // static List<string> _MyListJobsMonitored = new List<string>(); // List of jobds monitored. It contains the jobs ids. Used when a new job is discovered (created by another program) to activate the display of job progress
        static Dictionary<string, CancellationTokenSource> _MyListJobsMonitored = new Dictionary<string, CancellationTokenSource>(); // List of jobds monitored. It contains the jobs ids. Used when a new job is discovered (created by another program) to activate the display of job progress

        static private int _jobsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderjobs = OrderJobs.LastModifiedDescending;
        static string _filterjobsstate = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private SearchObject _searchinname = new SearchObject { SearchType = SearchIn.JobName, Text = "" };
        static private string _timefilter = FilterTime.LastWeek;
        static private TimeRangeValue _timefilterTimeRange = new TimeRangeValue(DateTime.Now.ToLocalTime().AddDays(-7).Date, null);
        private const int DefaultJobRefreshIntervalInMilliseconds = 2500;
        static int JobRefreshIntervalInMilliseconds = DefaultJobRefreshIntervalInMilliseconds;

        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<JobEntry> jobquery;
            _credentials = credentials;

            _context = context;// Program.ConnectAndGetNewContext(_credentials);
            jobquery = from j in _context.Jobs.Take(0)
                       orderby j.LastModified descending
                       select new JobEntry
                       {
                           Name = j.Name,
                           Id = j.Id,
                           Tasks = j.Tasks.Count,
                           Priority = j.Priority,
                           State = j.State,
                           StartTime = j.StartTime.HasValue ? ((DateTime)j.StartTime).ToLocalTime().ToString("G") : null,
                           EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString("G") : null,
                           Duration = (j.StartTime.HasValue && j.EndTime.HasValue) ? ((DateTime)j.EndTime).Subtract((DateTime)j.StartTime).ToString(@"d\.hh\:mm\:ss") : string.Empty,
                           Progress = (j.State == JobState.Scheduled || j.State == JobState.Processing || j.State == JobState.Queued) ? j.GetOverallProgress() : 101d
                       };

            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
            {
                Name = "Progress",
                DataPropertyName = "Progress",
                HeaderText = "Progress"
            };

            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();

            this.Columns.Add(col);
            BindingList<JobEntry> MyObservJobInPage = new BindingList<JobEntry>(jobquery.Take(0).ToList());
            this.DataSource = MyObservJobInPage;
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayJobIDinGrid;
            this.Columns["Progress"].DisplayIndex = 5;
            this.Columns["Progress"].Width = 150;
            this.Columns["Tasks"].Width = 50;
            this.Columns["Priority"].Width = 50;
            this.Columns["State"].Width = 80;
            this.Columns["StartTime"].Width = 150;
            this.Columns["EndTime"].Width = 150;
            this.Columns["Duration"].Width = 90;

            _initialized = true;
        }



        public void DisplayPage(int page)
        {
            if (!_initialized) return;
            if (!_refreshedatleastonetime) return;

            if ((page <= _pagecount) && (page > 0))
            {
                _currentpage = page;
                this.DataSource = new BindingList<JobEntry>(_MyObservJob.Skip(_jobsperpage * (page - 1)).Take(_jobsperpage).ToList());
            }
        }


        public void Refreshjobs(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized || context == null) return;

            Debug.WriteLine("Refresh Jobs Start");

            this.FindForm().Cursor = Cursors.WaitCursor;
            _context = context;

            IEnumerable<JobEntry> jobquery;

            // DAYS
            bool filterStartDate = false;
            bool filterEndDate = false;

            DateTime dateTimeStart = DateTime.UtcNow;
            DateTime dateTimeRangeEnd = DateTime.UtcNow.AddDays(1);

            int days = FilterTime.ReturnNumberOfDays(_timefilter);

            if (days > 0)
            {
                filterStartDate = true;
                dateTimeStart = (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)));
            }
            else if (days == -1) // TimeRange
            {
                filterStartDate = true;
                filterEndDate = true;
                dateTimeStart = _timefilterTimeRange.StartDate;
                if (_timefilterTimeRange.EndDate != null) // there is an end time
                {
                    dateTimeRangeEnd = (DateTime)_timefilterTimeRange.EndDate;
                }
            }

            IQueryable<IJob> jobsServerQuery = null;// = context.Jobs.AsQueryable();

            // STATE
            bool filterstate = _filterjobsstate != "All";
            JobState jobstate = JobState.Finished;
            if (filterstate)
            {
                jobstate = (JobState)Enum.Parse(typeof(JobState), _filterjobsstate);
            }

            // search
            if (_searchinname != null && !string.IsNullOrEmpty(_searchinname.Text))
            {
                bool Error = false;

                switch (_searchinname.SearchType)
                {
                    case SearchIn.JobName:
                        jobsServerQuery = context.Jobs.Where(j =>
                                             (j.Name.Contains(_searchinname.Text))
                                             &&
                                             (!filterStartDate || j.LastModified > dateTimeStart)
                                              &&
                                             (!filterEndDate || j.LastModified < dateTimeRangeEnd)
                                             &&
                                             (!filterstate || j.State == jobstate)
                                             );

                        break;

                    case SearchIn.JobId:
                        string jobguid = _searchinname.Text;
                        if (jobguid.StartsWith(Constants.JobIdPrefix))
                        {
                            jobguid = jobguid.Substring(Constants.JobIdPrefix.Length);
                        }
                        try
                        {
                            var g = new Guid(jobguid);
                        }
                        catch
                        {
                            Error = true;
                            MessageBox.Show("Error with job Id. Is it a valid GUID or asset Id ?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (!Error)
                        {
                            jobsServerQuery = context.Jobs.Where(j =>
                                                    (j.Id == Constants.JobIdPrefix + jobguid)
                                                    &&
                                                    (!filterStartDate || j.LastModified > dateTimeStart)
                                                     &&
                                                    (!filterEndDate || j.LastModified < dateTimeRangeEnd)
                                                    &&
                                                    (!filterstate || j.State == jobstate)
                                                    );
                        }
                        break;


                    default:
                        break;
                }
            }
            else
            {
                jobsServerQuery = context.Jobs.Where(j =>
                                 (!filterStartDate || j.LastModified > dateTimeStart)
                                 &&
                                 (!filterEndDate || j.LastModified < dateTimeRangeEnd)
                                 &&
                                 (!filterstate || j.State == jobstate)
                                );
            }


            // SHORTCUT (needed for account with large number of jobs)
            if (_orderjobs == OrderJobs.LastModifiedDescending && (_timefilter == FilterTime.First50Items || _timefilter == FilterTime.First1000Items))
            {
                if (_timefilter == FilterTime.First50Items)
                {
                    jobs = jobsServerQuery.Take(50);
                }
                else if (_timefilter == FilterTime.First1000Items)
                {
                    jobs = jobsServerQuery.Take(1000);
                }
            }
            else // general case

            {
                // let's get all the results locally

                IList<IJob> aggregateListJobs = new List<IJob>();
                int skipSize = 0;
                int batchSize = 1000;
                int currentSkipSize = 0;
                while (true)
                {
                    // Enumerate through all jobs (1000 at a time)
                    var jobsq = jobsServerQuery
                        .Skip(skipSize).Take(batchSize).ToList();

                    currentSkipSize += jobsq.Count;

                    foreach (var j in jobsq)
                    {
                        aggregateListJobs.Add(j);
                    }

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
                jobs = aggregateListJobs;



                switch (_orderjobs)
                {
                    case OrderJobs.LastModifiedDescending:
                        jobs = from j in jobs orderby j.LastModified descending select j;
                        break;

                    case OrderJobs.LastModifiedAscending:
                        jobs = from j in jobs orderby j.LastModified ascending select j;
                        break;

                    case OrderJobs.NameDescending:
                        jobs = from j in jobs orderby j.Name descending select j;
                        break;

                    case OrderJobs.NameAscending:
                        jobs = from j in jobs orderby j.Name ascending select j;
                        break;

                    case OrderJobs.EndTimeDescending:
                        jobs = from j in jobs orderby j.EndTime descending select j;
                        break;

                    case OrderJobs.EndTimeAscending:
                        jobs = from j in jobs orderby j.EndTime ascending select j;
                        break;

                    case OrderJobs.ProcessTimeDescending:
                        jobs = from j in jobs orderby j.RunningDuration descending select j;
                        break;

                    case OrderJobs.ProcessTimeAscending:
                        jobs = from j in jobs orderby j.RunningDuration ascending select j;
                        break;

                    case OrderJobs.StartTimeDescending:
                        jobs = from j in jobs orderby j.StartTime descending select j;
                        break;

                    case OrderJobs.StartTimeAscending:
                        jobs = from j in jobs orderby j.StartTime ascending select j;
                        break;

                    case OrderJobs.StateDescending:
                        jobs = from j in jobs orderby j.State descending select j;
                        break;

                    case OrderJobs.StateAscending:
                        jobs = from j in jobs orderby j.State ascending select j;
                        break;

                    default:
                        jobs = from j in jobs orderby j.LastModified descending select j;
                        break;
                }





                if (_timefilter == FilterTime.First50Items)
                {
                    jobs = jobs.Take(50);
                }
                else if (_timefilter == FilterTime.First1000Items)
                {
                    jobs = jobs.Take(1000);
                }


            } // end of general case

            _context = context;
            _pagecount = (int)Math.Ceiling(((double)jobs.Count()) / ((double)_jobsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            try
            {
                jobquery = from j in jobs
                           select new JobEntry
                           {
                               Name = j.Name,
                               Id = j.Id,
                               Tasks = j.Tasks.Count,
                               Priority = j.Priority,
                               State = j.State,
                               StartTime = j.StartTime.HasValue ? ((DateTime)j.StartTime).ToLocalTime().ToString("G") : null,
                               EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString("G") : null,
                               Duration = (j.StartTime.HasValue && j.EndTime.HasValue) ? ((DateTime)j.EndTime).Subtract((DateTime)j.StartTime).ToString(@"d\.hh\:mm\:ss") : string.Empty,
                               Progress = (j.State == JobState.Scheduled || j.State == JobState.Processing || j.State == JobState.Queued) ? j.GetOverallProgress() : 101d
                           };
                _MyObservJob = new BindingList<JobEntry>(jobquery.ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + Constants.endline + Program.GetErrorMessage(e));
                Environment.Exit(0);
            }

            _MyObservAssethisPage = new BindingList<JobEntry>(_MyObservJob.Skip(_jobsperpage * (_currentpage - 1)).Take(_jobsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservAssethisPage));
            _refreshedatleastonetime = true;

            RestoreJobProgress(); // display job progress of new visible jobs

            Debug.WriteLine("Refresh Jobs End");

            this.FindForm().Cursor = Cursors.Default;
        }



        // Used to restore job progress. 2 cases: when app is launched or when a job has been created by an external program
        public void RestoreJobProgress()  // when app is launched for example, we want to restore job progress updates
        {
            Task.Run(() =>
           {
               //IEnumerable<IJob> ActiveJobs = _context.Jobs.Where(j => (j.State == JobState.Queued) || (j.State == JobState.Scheduled) || (j.State == JobState.Processing));
               IEnumerable<IJob> ActiveAndVisibleJobs = jobs.Where(j => (j.State == JobState.Queued) || (j.State == JobState.Scheduled) || (j.State == JobState.Processing));

               // let's cancel monitor task of non visible jobs
               foreach (var jobmonitored in _MyListJobsMonitored)
               {
                   if (ActiveAndVisibleJobs.Where(j => j.Id == jobmonitored.Key).FirstOrDefault() == null)
                   {
                       jobmonitored.Value.Cancel();
                       _MyListJobsMonitored.Remove(jobmonitored.Key);
                   }
               }

               // let's adjust the JobRefreshIntervalInMilliseconds based on the number of jobs to monitor
               // 2500 ms if 5 jobs or less, 500ms*nbjobs otherwise
               JobRefreshIntervalInMilliseconds = Math.Max(DefaultJobRefreshIntervalInMilliseconds, Convert.ToInt32(DefaultJobRefreshIntervalInMilliseconds * ActiveAndVisibleJobs.Count() / 5d));

               // let's monitor job that are not yet monitored
               foreach (IJob job in ActiveAndVisibleJobs)
               {
                   if (!_MyListJobsMonitored.ContainsKey(job.Id))
                   {
                       this.DoJobProgress(job); // token will be added to dictionnary in this function
                   }
               }
           });
        }



        public void DoJobProgress(IJob job)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            _MyListJobsMonitored.Add(job.Id, tokenSource); // to track the task and be able to cancel it later

            Task.Run(() =>
            {
                try
                {
                    job = job.StartExecutionProgressTask(JobRefreshIntervalInMilliseconds,
                       j =>
                       {
                           // Was cancellation already requested? 
                           if (token.IsCancellationRequested == true)
                           {
                               return;
                           }

                           // refesh context and job
                           _context = Program.ConnectAndGetNewContext(_credentials); // needed to get overallprogress
                                                                                     /// NET TO RESTORE CONTEXT
                           IJob JobRefreshed = GetJob(j.Id);
                           int index = -1;

                           foreach (JobEntry je in _MyObservJob) // let's search for index
                           {
                               if (je.Id == JobRefreshed.Id)
                               {
                                   index = _MyObservJob.IndexOf(je);
                                   break;
                               }
                           }


                           if (index >= 0) // we found it
                           { // we update the observation collection

                               if (JobRefreshed.State == JobState.Scheduled || JobRefreshed.State == JobState.Processing || JobRefreshed.State == JobState.Queued || JobRefreshed.State == JobState.Canceling) // in progress
                               {
                                   double progress = JobRefreshed.GetOverallProgress();
                                   _MyObservJob[index].Progress = progress;
                                   _MyObservJob[index].Priority = JobRefreshed.Priority;
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? ((DateTime)JobRefreshed.StartTime).ToLocalTime().ToString("G") : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString("G") : null;

                                   _MyObservJob[index].State = JobRefreshed.State;
                                   Debug.WriteLine(index.ToString() + JobRefreshed.State);

                                   StringBuilder sb = new StringBuilder(); // display percentage for each task for mouse hover (tooltiptext)
                                   foreach (ITask task in JobRefreshed.Tasks) sb.AppendLine(string.Format("{0} % ({1})", Convert.ToInt32(task.Progress).ToString(), task.Name));

                                   // let's calculate the estipated time
                                   string ETAstr = "", Durationstr = "";
                                   if (progress > 3)
                                   {
                                       DateTime startlocaltime = ((DateTime)JobRefreshed.StartTime).ToLocalTime();
                                       TimeSpan interval = (TimeSpan)(DateTime.Now - startlocaltime);
                                       DateTime ETA = DateTime.Now.AddSeconds((100d / progress - 1d) * interval.TotalSeconds);
                                       TimeSpan estimatedduration = (TimeSpan)(ETA - startlocaltime);

                                       ETAstr = "Estimated: " + ETA.ToString("G");
                                       Durationstr = "Estimated: " + estimatedduration.ToString(@"d\.hh\:mm\:ss");
                                       _MyObservJob[index].EndTime = ETA.ToString(@"G") + " ?";
                                       _MyObservJob[index].Duration = JobRefreshed.EndTime.HasValue ?
                                                    ((TimeSpan)((DateTime)JobRefreshed.EndTime - (DateTime)JobRefreshed.StartTime)).ToString(@"d\.hh\:mm\:ss")
                                                    : estimatedduration.ToString(@"d\.hh\:mm\:ss") + " ?";
                                   }

                                   int indexdisplayed = -1;
                                   foreach (JobEntry je in _MyObservAssethisPage) // let's search for index in the page
                                   {
                                       if (je.Id == JobRefreshed.Id)
                                       {
                                           indexdisplayed = _MyObservAssethisPage.IndexOf(je);
                                           try
                                           {
                                               this.BeginInvoke(new Action(() =>
                                               {
                                                   this.Rows[indexdisplayed].Cells[this.Columns["Progress"].Index].ToolTipText = sb.ToString(); // mouse hover info
                                                   if (progress != 0)
                                                   {
                                                       this.Rows[indexdisplayed].Cells[this.Columns["EndTime"].Index].ToolTipText = ETAstr;// mouse hover info
                                                       this.Rows[indexdisplayed].Cells[this.Columns["Duration"].Index].ToolTipText = Durationstr;// mouse hover info
                                                   }
                                                   this.Refresh();
                                               }));
                                           }
                                           catch
                                           {

                                           }

                                           break;
                                       }
                                   }
                               }
                               else // no progress anymore (cancelled, finished or failed)
                               {
                                   double progress = JobRefreshed.GetOverallProgress();
                                   _MyObservJob[index].Duration = JobRefreshed.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - JobRefreshed.StartTime)).ToString(@"d\.hh\:mm\:ss") : null;
                                   _MyObservJob[index].Progress = 101d;// progress;  we don't want the progress bar to be displayed
                                   _MyObservJob[index].Priority = JobRefreshed.Priority;
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? ((DateTime)JobRefreshed.StartTime).ToLocalTime().ToString("G") : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString("G") : null;
                                   _MyObservJob[index].State = JobRefreshed.State;

                                   if (_MyListJobsMonitored.ContainsKey(JobRefreshed.Id)) // we want to display only one time
                                   {
                                       _MyListJobsMonitored.Remove(JobRefreshed.Id); // let's remove from the list of monitored jobs
                                       Mainform myform = (Mainform)this.FindForm();
                                       string status = Enum.GetName(typeof(JobState), JobRefreshed.State).ToLower();

                                       myform.BeginInvoke(new Action(() =>
                                       {
                                           myform.Notify(string.Format("Job {0}", status), string.Format("Job {0}", _MyObservJob[index].Name), JobRefreshed.State == JobState.Error);
                                           myform.TextBoxLogWriteLine(string.Format("Job '{0}': {1}.", _MyObservJob[index].Name, status), JobRefreshed.State == JobState.Error);
                                           if (JobRefreshed.State == JobState.Error)
                                           {
                                               foreach (var task in JobRefreshed.Tasks)
                                               {
                                                   foreach (var error in task.ErrorDetails)
                                                   {
                                                       myform.TextBoxLogWriteLine(string.Format("Task '{0}', Error : {1}", task.Name, error.Code + " : " + error.Message), true);
                                                   }
                                               }
                                           }
                                           myform.DoRefreshGridAssetV(false);
                                       }));

                                       this.BeginInvoke(new Action(() =>
                                       {
                                           this.Refresh();
                                       }));

                                   }
                               }
                           }
                       },
                       token).Result;

                }
                catch (Exception e)
                {
                    //MessageBox.Show(Program.GetErrorMessage(e), "Job Monitoring Error");
                }
            }, token);

        }


        static IJob GetJob(string jobId)
        {
            // Use a Linq select query to get an updated 
            // reference by Id. 
            var jobInstance =
                from j in _context.Jobs
                where j.Id == jobId
                select j;
            // Return the job reference as an Ijob. 
            IJob job = jobInstance.FirstOrDefault();

            return job;
        }
    }
}
