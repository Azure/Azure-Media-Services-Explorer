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
        public static MediaServiceContextForDynManifest _contextdynmanifest = null;
        public static string Salt;
        private string _backuprootfolderupload = "";
        private StringBuilder sbuilder = new StringBuilder(); // used for locator copy to clipboard
        private ILocator PlayBackLocator = null;

        //Watch folder vars
        private Dictionary<string, DateTime> seen = new Dictionary<string, DateTime>();
        private TimeSpan seenInterval = new TimeSpan();
        WatchFolderSettings MyWatchFolderSettings = new WatchFolderSettings();

        private bool AMEPremiumWorkflowPresent = true;
        private bool HyperlapsePresent = true;
        private bool AMEStandardPresent = true;


        private System.Timers.Timer TimerAutoRefresh;
        bool DisplaySplashDuringLoading;
        private bool EncodingRUFeatureOn = true; // On some test account, there is no Encoding RU so let's switch to OFF the feature in that case

        public Mainform()
        {
            InitializeComponent();
            this.Icon = Bitmaps.Azure_Explorer_ico;

            // USER SETTINSG CHECKS & UPDATES
            if (Properties.Settings.Default.CallUpgrade) // upgrade settings from previous version
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
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
            _context = Program.ConnectAndGetNewContext(_credentials);

            // Dynamic filter
            _contextdynmanifest = new MediaServiceContextForDynManifest(_credentials);
            _contextdynmanifest.AccessToken = _context.Credentials.AccessToken;
            _contextdynmanifest.CheckForRedirection();

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


            if (GetLatestMediaProcessorByName(Constants.AzureMediaHyperlapse) == null)
            {
                HyperlapsePresent = false;
                processAssetsWithHyperlapseToolStripMenuItem.Enabled = false;
                processAssetsWithHyperlapseToolStripMenuItem1.Enabled = false;
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
                    TextBoxLogWriteLine("There is no reserved encoding unit (encoding will use a shared pool) but unit type is not set to BASIC.", true); // Warning
            }
            catch // can occur on test account
            {
                EncodingRUFeatureOn = false;
                TextBoxLogWriteLine("There is an error when accessing to the Encoding Reserved Units API. Some controls are disabled in the processors tab.", true); // Warning
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

                blockBlob.StartCopyFromBlob(ObjectUrl, null, null, null);

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
                    TextBoxLogWriteLine("You are ready to use asset '{0}'", asset.Name);
                }
                else // Error!
                {
                    TextBoxLogWriteLine("Error during file import.", true);
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



        private void ProcessUploadFromFolder(object folderPath, int index, string storageaccount = null)
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

            string[] progressafname = new string[filePaths.Count()];
            double[] progressafint = new double[filePaths.Count()];

            int indexa = 0;
            IAsset asset = null;

            try
            {
                asset = _context.Assets.CreateFromFolder(
                                                               folderPath as string,
                                                               storageaccount,
                                                               Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None,
                                                               (af, p) =>
                                                               {
                                                                   int indexc = Array.IndexOf(progressafname, af.Name);
                                                                   if (indexc == -1)
                                                                   {
                                                                       progressafname[indexa] = af.Name;
                                                                       progressafint[indexa] = (int)p.Progress;
                                                                       indexa++;

                                                                   }
                                                                   else
                                                                   {
                                                                       progressafint[indexc] = (int)p.Progress;
                                                                   }

                                                                   DoGridTransferUpdateProgress(progressafint.Average(), index);
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
                TextBoxLogWriteLine(string.Format("Uploading of the file(s) in {0} done.", folderPath));
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

        public void DeleteLocatorsForAsset(IAsset asset)
        {
            if (asset != null)
            {
                string assetId = asset.Id;
                var locators = from a in _context.Locators
                               where a.AssetId == assetId
                               select a;

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

        public void TextBoxLogWriteLine(string text, bool Error = false)
        {
            text += Environment.NewLine;
            string date = string.Format("[{0}] ", String.Format("{0:G}", DateTime.Now));

            if (richTextBoxLog.InvokeRequired)
            {
                richTextBoxLog.BeginInvoke(new Action(() =>
                {
                    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                    richTextBoxLog.SelectionLength = 0;

                    richTextBoxLog.SelectionColor = Color.Gray;
                    richTextBoxLog.AppendText(date);

                    richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                    richTextBoxLog.SelectionLength = 0;

                    richTextBoxLog.SelectionColor = Error ? Color.Red : Color.Black;
                    richTextBoxLog.AppendText(text);
                    richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor;
                }));
            }
            else
            {
                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                richTextBoxLog.SelectionLength = 0;

                richTextBoxLog.SelectionColor = Color.Gray;
                richTextBoxLog.AppendText(date);

                richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
                richTextBoxLog.SelectionLength = 0;

                richTextBoxLog.SelectionColor = Error ? Color.Red : Color.Black;
                richTextBoxLog.AppendText(text);
                richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor;
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
        }

        public void DoRefreshGridAssetV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewAssetsV.Init(_context, _contextdynmanifest);
                for (int i = 1; i <= dataGridViewAssetsV.PageCount; i++) comboBoxPageAssets.Items.Add(i);
                comboBoxPageAssets.SelectedIndex = 0;
                Debug.WriteLine("DoRefreshGridAssetforsttime");
            }

            Debug.WriteLine("DoRefreshGridAssetNotforsttime");
            int backupindex = 0;
            int pagecount = 0;

            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.AssetsPerPage = Properties.Settings.Default.NbItemsDisplayedInGrid));
            comboBoxPageAssets.Invoke(new Action(() => backupindex = comboBoxPageAssets.SelectedIndex));
            dataGridViewAssetsV.Invoke(new Action(() => dataGridViewAssetsV.RefreshAssets(_context, backupindex + 1)));
            comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.Items.Clear()));
            dataGridViewAssetsV.Invoke(new Action(() => pagecount = dataGridViewAssetsV.PageCount));

            for (int i = 1; i <= pagecount; i++) comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.Items.Add(i)));
            comboBoxPageAssets.Invoke(new Action(() => comboBoxPageAssets.SelectedIndex = dataGridViewAssetsV.CurrentPage - 1));

            tabPageAssets.Invoke(new Action(() => tabPageAssets.Text = string.Format(Constants.TabAssets + " ({0})", dataGridViewAssetsV.DisplayedCount)));
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
            for (int i = 1; i <= pagecount; i++) comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Add(i)));
            comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.SelectedIndex = dataGridViewJobsV.CurrentPage - 1));
            //uodate tab nimber of jobs
            tabPageJobs.Invoke(new Action(() => tabPageJobs.Text = string.Format(Constants.TabJobs + " ({0})", dataGridViewJobsV.DisplayedCount)));

            // job progress restore
            dataGridViewJobsV.RestoreJobProgress();
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
                    DoRefreshGridAssetV(false);
                }
                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error: Could not read file from disk.", true);
                    TextBoxLogWriteLine(ex);
                }
            }
        }




        private void ProcessUploadFileAndMore(object name, int index, WatchFolderSettings watchfoldersettings = null, string storageaccount = null)
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
                TextBoxLogWriteLine(string.Format("Uploading of {0} done.", name));
                DoGridTransferDeclareCompleted(index, asset.Id);
                if (watchfoldersettings != null && watchfoldersettings.DeleteFile) //use checked the box "delete the file"
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
                    //job.JobNotificationSubscriptions.AddNew(NotificationJobState.FinalStatesOnly, watchfoldersettings.NotificationEndPoint);

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
                        // job template does not rename the outout assets. As a fix, we do this:
                        int taskind = 1;
                        foreach (var task in myjob.Tasks)
                        {
                            int outputind = 1;
                            foreach (var outputasset in task.OutputAssets)
                            {
                                IAsset oasset = AssetInfo.GetAsset(outputasset.Id, _context);
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
                                outputind++;
                            }
                            taskind++;
                        }


                        if (watchfoldersettings.PublishOutputAssets) //user wants to publish the output asset when it has been processed by the job 
                        {
                            IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + myjob.Name, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDays), AccessPermissions.Read);
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
                                        string playbackurl = AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.AzureMediaPlayer, SmoothUri.AbsoluteUri, _context, oasset, launchbrowser: false);
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



        private void ProcessDownloadAsset(List<IAsset> SelectedAssets, object folder, int index)
        {
            // If download in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            bool multipleassets = SelectedAssets.Count > 1;
            bool Error = false;

            string labeldb = "Starting download of " + SelectedAssets.FirstOrDefault().Name + " to " + folder as string + Constants.endline;
            if (multipleassets)
            {
                labeldb = "Starting download of files of " + SelectedAssets.Count + " assets to " + folder as string + Constants.endline;
            }
            TextBoxLogWriteLine(labeldb);
            foreach (IAsset mediaAsset in SelectedAssets)
            {
                string foldera = folder.ToString();
                if (multipleassets)
                {
                    foldera += "\\" + mediaAsset.Id.Substring(12);
                    Directory.CreateDirectory(foldera);
                }
                try
                {
                    mediaAsset.DownloadToFolder(foldera,
                                                                                     (af, p) =>
                                                                                     {
                                                                                         DoGridTransferUpdateProgress(p.Progress, index);
                                                                                     }
                                                                                    );
                }
                catch (Exception e)
                {
                    Error = true;
                    TextBoxLogWriteLine(string.Format("Download of asset '{0}' failed.", mediaAsset.Name), true);
                    TextBoxLogWriteLine(e);
                    DoGridTransferDeclareError(index, e);
                }

            }
            if (!Error)
            {
                TextBoxLogWriteLine("Download finished.");
                DoGridTransferDeclareCompleted(index, folder.ToString());

            }
        }

        public void DoDownloadFileFromAsset(IAsset asset, IAssetFile File, object folder, int index)
        {
            // If download is in the queue, let's wait our turn
            DoGridTransferWaitIfNeeded(index);

            string labeldb = "Starting download of " + File.Name + " of asset " + asset.Name + " to " + folder as string + Constants.endline;
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
                    TextBoxLogWriteLine(string.Format("Download of file '{0}' is finished.", File.Name));
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
            FolderBrowserDialog openFolderDialog1 = new FolderBrowserDialog();

            if (!string.IsNullOrEmpty(_backuprootfolderupload)) openFolderDialog1.SelectedPath = _backuprootfolderupload;

            if (openFolderDialog1.ShowDialog() == DialogResult.OK)
            {
                DoMenuUploadFromFolder_Step2(openFolderDialog1.SelectedPath);
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
                    DoRefreshGridAssetV(false);
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
                                   LocStartDate = DateTime.Now.ToLocalTime(),
                                   LocEndDate = DateTime.Now.ToLocalTime().AddDays(Properties.Settings.Default.DefaultLocatorDurationDays),
                                   LocAssetName = labelAssetName,
                                   LocHasStartDate = false,
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
                                .Where(locator => locator.Type == form.LocType)
                                .Select(locator => UpdateLocatorExpirationDate(locator, form.LocEndDate));

                            await Task.WhenAll(tasks);
                        }
                    }
                    finally
                    {

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
                        AssetInformation form = new AssetInformation(this, _context, _contextdynmanifest)
                        {
                            myAsset = asset,
                            myStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints // we want to keep the same sorting
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
                        DoRefreshGridAssetV(false);
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
            if (folderBrowserDialogDownload.ShowDialog() == DialogResult.OK)
            {
                string label = string.Format("Download of asset '{0}'", mediaAsset.Name);
                if (SelectedAssets.Count > 1) label = string.Format("Download of {0} assets", SelectedAssets.Count);

                int index = DoGridTransferAddItem(label, TransferType.DownloadToLocal, Properties.Settings.Default.useTransferQueue);
                // Start a worker thread that does downloading.
                Task.Factory.StartNew(() => ProcessDownloadAsset(SelectedAssets, folderBrowserDialogDownload.SelectedPath, index));
                DotabControlMainSwitch(Constants.TabTransfers);
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
                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) cancelation", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
                    LocStartDate = DateTime.Now.ToLocalTime(),
                    LocEndDate = DateTime.Now.ToLocalTime().AddDays(Properties.Settings.Default.DefaultLocatorDurationDays),
                    LocAssetName = labelAssetName,
                    LocHasStartDate = false,
                    LocWarning = _context.StreamingEndpoints.Where(o => o.ScaleUnits > 0).ToList().Count > 0 ? string.Empty : "Dynamic packaging will not work as there is no scale unit streaming endpoint in this account."
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // The permissions for the locator's access policy.
                    AccessPermissions accessPolicyPermissions = AccessPermissions.Read;

                    // The duration for the locator's access policy.
                    TimeSpan accessPolicyDuration = form.LocEndDate.Subtract(DateTime.Now.ToLocalTime());

                    sbuilder.Clear();

                    foreach (IAsset AssetTOProcess in SelectedAssets)

                        if (AssetTOProcess != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Creating locator for asset '{0}'... ", AssetTOProcess.Name);
                            try
                            {

                                Task.Factory.StartNew(() => ProcessCreateLocator(form.LocType, AssetTOProcess, accessPolicyPermissions, accessPolicyDuration, form.LocStartDate, form.ForceLocatorGuid));
                            }

                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when creating the locator on asset '{0}'.", AssetTOProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }
                        }
                }
            }
        }


        private void ProcessCreateLocator(LocatorType locatorType, IAsset AssetToP, AccessPermissions accessPolicyPermissions, TimeSpan accessPolicyDuration, Nullable<DateTime> startTime, string ForceLocatorGUID)
        {
            ILocator locator = null;
            try
            {
                IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + AssetToP, accessPolicyDuration, accessPolicyPermissions);
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
            sbuilderThisAsset.AppendLine("");
            sbuilderThisAsset.AppendLine("Asset:");
            sbuilderThisAsset.AppendLine(AssetToP.Name);
            sbuilderThisAsset.AppendLine("Asset ID:");
            sbuilderThisAsset.AppendLine(AssetToP.Id);
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


        private void DoDeleteAllLocatorsOnAssets(List<IAsset> SelectedAssets)
        {
            if (SelectedAssets.Count > 0)
            {
                string question = "Delete all locators of these " + SelectedAssets.Count + " assets ?";
                if (SelectedAssets.Count == 1) question = "Delete all the locators of " + SelectedAssets[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Locators deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IAsset AssetToProcess in SelectedAssets)
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
            foreach (DataGridViewRow Row in dataGridViewAssetsV.SelectedRows)
            {
                SelectedAssets.Add(_context.Assets.Where(j => j.Id == Row.Cells[dataGridViewAssetsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
            }
            SelectedAssets.Reverse();
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

        private IStorageAccount ReturnSelectedStorage()
        {

            IStorageAccount SelectedStorage = null;
            if (dataGridViewStorage.SelectedRows.Count == 1)
            {
                var row = dataGridViewStorage.SelectedRows[0];
                SelectedStorage = _context.StorageAccounts.Where(s => s.Name == row.Cells[dataGridViewStorage.Columns["StrictName"].Index].Value.ToString()).FirstOrDefault();
            }

            return SelectedStorage;
        }

        private List<Filter> ReturnSelectedFilters()
        {

            List<Filter> SelectedFilters = new List<Filter>();
            foreach (DataGridViewRow Row in dataGridViewFilters.SelectedRows)
            {
                string filtername = Row.Cells[dataGridViewFilters.Columns["Name"].Index].Value.ToString();
                Filter myfilter = _contextdynmanifest.GetGlobalFilter(filtername);
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
                if (System.Windows.Forms.MessageBox.Show(question, "Asset deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    bool Error = false;
                    try
                    {
                        Task[] deleteTasks = SelectedAssets.ToList().Select(a => a.DeleteAsync()).ToArray();
                        TextBoxLogWriteLine("Deleting asset(s)");
                        this.Cursor = Cursors.WaitCursor;
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
                    this.Cursor = Cursors.Default;
                    DoRefreshGridAssetV(false);
                }
            }
        }


        private void allAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAssets(_context.Assets.ToList());
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
                            destinationBlob.StartCopyFromBlob(sourceCloudBlob);

                            CloudBlockBlob blob;
                            blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(fileName);

                            while (blob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(fileName);
                                percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                DoGridTransferUpdateProgress(percentComplete, index);

                            }

                            if (blob.CopyState.Status == CopyStatus.Failed)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'.", fileName, true);
                                TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                DoGridTransferDeclareError(index, blob.CopyState.StatusDescription);
                                Error = true;
                                break;
                            }

                            destinationBlob.FetchAttributes();
                            assetFile.ContentFileSize = sourceCloudBlob.Properties.Length;
                            assetFile.Update();

                            if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'", fileName, true);
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
                    TextBoxLogWriteLine("Azure Storage copy completed.");
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
                            destinationBlob.StartCopyFromBlob(new Uri(sourceBlob.Uri.AbsoluteUri + blobToken));


                            CloudBlockBlob blob;
                            blob = (CloudBlockBlob)assetContainer.GetBlobReferenceFromServer(fileName);

                            while (blob.CopyState.Status == CopyStatus.Pending)
                            {
                                Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                blob = (CloudBlockBlob)assetContainer.GetBlobReferenceFromServer(fileName);
                                percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SelectedBlobs.Count)) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / (long)Length;
                                DoGridTransferUpdateProgress(percentComplete, index);
                            }

                            if (blob.CopyState.Status == CopyStatus.Failed)
                            {
                                TextBoxLogWriteLine("Failed to copy file '{0}'.", fileName, true);
                                TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                DoGridTransferDeclareError(index, blob.CopyState.StatusDescription);
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
                    TextBoxLogWriteLine("Azure Storage copy completed.");
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
                        TextBoxLogWriteLine("Failed to create container '{0}'", TargetContainer.Name, true);
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
                                destinationBlob.StartCopyFromBlob(sourceCloudBlob);

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);
                                    percentComplete = (long)100 * (long)(BytesCopied + blob.CopyState.BytesCopied) / (long)Length;
                                    DoGridTransferUpdateProgress((int)percentComplete, index);

                                }

                                if (blob.CopyState.Status == CopyStatus.Failed)
                                {
                                    TextBoxLogWriteLine("Failed to copy '{0}'", file.Name, true);
                                    TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                    DoGridTransferDeclareError(index, blob.CopyState.StatusDescription);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
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
                        TextBoxLogWriteLine("Blob copy completed.");
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
                                destinationBlob.StartCopyFromBlob(new Uri(sourceCloudBlob.Uri.AbsoluteUri + blobToken));

                                CloudBlockBlob blob;
                                blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);

                                while (blob.CopyState.Status == CopyStatus.Pending)
                                {
                                    Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    blob = (CloudBlockBlob)TargetContainer.GetBlobReferenceFromServer(file.Name);
                                    percentComplete = 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                    DoGridTransferUpdateProgress(percentComplete, index);

                                }

                                if (blob.CopyState.Status == CopyStatus.Failed)
                                {
                                    TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
                                    TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
                                    DoGridTransferDeclareError(index, blob.CopyState.StatusDescription);
                                    Error = true;
                                    break;
                                }

                                destinationBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length != destinationBlob.Properties.Length)
                                {
                                    TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
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
                        TextBoxLogWriteLine("Blob copy completed.");
                        DoGridTransferDeclareCompleted(index, TargetContainer.Uri.AbsoluteUri);
                    }
                    DoRefreshGridAssetV(false);
                }
            }
        }

        private async void ProcessExportAssetToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, Dictionary<string, string> storagekeys, List<IAsset> SourceAssets, string TargetAssetName, int index, bool DeleteSourceAssets = false, bool CopyDynEnc = false, bool ReWriteLAURL = false)
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
                        assetFilesToCopy = SourceAsset.AssetFiles.ToList().Where(af => !af.Name.StartsWith("audio_") && !af.Name.StartsWith("video_"));
                        var assetFilesLiveFolders = SourceAsset.AssetFiles.ToList().Where(af => af.Name.StartsWith("audio_") || af.Name.StartsWith("video_"));

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

                                        destinationCloudBlockBlob.DeleteIfExists();
                                        destinationCloudBlockBlob.StartCopyFromBlob(file.GetSasUri());

                                        CloudBlockBlob blob;
                                        blob = (CloudBlockBlob)DestinationCloudBlobContainer.GetBlobReferenceFromServer(file.Name);

                                        while (blob.CopyState.Status == CopyStatus.Pending)
                                        {
                                            Task.Delay(TimeSpan.FromSeconds(0.5d)).Wait();
                                            blob = (CloudBlockBlob)DestinationCloudBlobContainer.GetBlobReferenceFromServer(file.Name);
                                            percentComplete = (Convert.ToDouble(nbblob) / Convert.ToDouble(SourceAsset.AssetFiles.Count())) * 100d * (long)(BytesCopied + blob.CopyState.BytesCopied) / Length;
                                            DoGridTransferUpdateProgressText(string.Format("File '{0}'", file.Name), (int)percentComplete, index);
                                        }

                                        if (blob.CopyState.Status == CopyStatus.Failed)
                                        {
                                            TextBoxLogWriteLine("Failed to copy '{0}'", file.Name, true);
                                            TextBoxLogWriteLine("({0})", blob.CopyState.StatusDescription, true);
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
                                            TextBoxLogWriteLine("Failed to copy file '{0}'", file.Name, true);
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
                                List<ICancellableAsyncResult> mylistresults = new List<ICancellableAsyncResult>();

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
                                            mylistresults.Add(targetBlob.BeginStartCopyFromBlob(new Uri(blockblob.Uri.AbsoluteUri + SourceLocator.ContentAccessComponent), null, null));
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


        private async void ProcessCloneProgramToAnotherAMSAccount(CredentialsEntry DestinationCredentialsEntry, string DestinationStorageAccount, IProgram sourceProgram, bool CopyDynEnc, bool RewriteLAURL, bool CloneLocators)
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
            DoDeleteJobs(_context.Jobs.ToList());

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
                if (System.Windows.Forms.MessageBox.Show(question, "Job deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    bool Error = false;
                    Task[] deleteTasks = SelectedJobs.ToList().Select(j => j.DeleteAsync()).ToArray();
                    TextBoxLogWriteLine("Deleting job(s)");
                    this.Cursor = Cursors.WaitCursor;
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
                    this.Cursor = Cursors.Default;
                    DoRefreshGridJobV(false);
                }
            }
        }


        private void silverlightMonitoringPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://smf.cloudapp.net/healthmonitor");
        }

        private void dASHIFHTML5ReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://dashif.org/reference/players/javascript/");
        }

        private void iVXHLSPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://apps.microsoft.com/windows/en-us/app/3ivx-hls-player/f79ce7d0-2993-4658-bc4e-83dc182a0614");
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

            string taskname = "Premium Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvWorkflow;
            this.Cursor = Cursors.WaitCursor;
            EncodingPremium form = new EncodingPremium(_context)
            {
                EncodingPromptText = (SelectedAssets.Count > 1) ? "Input assets : " + SelectedAssets.Count + " assets have been selected." : "Input asset : '" + SelectedAssets.FirstOrDefault().Name + "'",
                EncodingProcessorsList = Encoders,
                EncodingJobName = "Premium Encoding of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + "-Premium encoded with " + Constants.NameconvWorkflow,
                EncodingMultipleJobs = true,
                EncodingNumberInputAssets = SelectedAssets.Count,
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
                if (!form.EncodingMultipleJobs) // ONE job with all input assets
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                    IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);
                    foreach (IAsset graphAsset in form.SelectedPremiumWorkflows) // for each blueprint selected, we create a task
                    {
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);
                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   form.EncodingProcessorSelected,
                                   "",
                                   form.JobOptions.TasksOptionsSetting
                                   );
                        // Specify the workflow asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.Add(graphAsset);
                        task.InputAssets.AddRange(SelectedAssets); // we add all assets
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);
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
                        MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobnameloc) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error); TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }
                    dataGridViewJobsV.DoJobProgress(job);

                }
                else // multiple jobs: one job for each input asset
                {
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
                                       "",
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

            Encoders = GetMediaProcessorsByName(Constants.AzureMediaEncoder);
            Encoders.AddRange(GetMediaProcessorsByName(Constants.WindowsAzureMediaEncoder));

            EncodingAMEPreset form = new EncodingAMEPreset(_context)
            {
                EncodingOutputAssetName = Constants.NameconvInputasset + "-AME encoded with " + Constants.NameconvAMEpreset,
                Text = "Azure Media Encoding",
                EncodingLabel1 = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected. " + SelectedAssets.Count + " jobs will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingJobName = "AME Encoding of " + Constants.NameconvInputasset,
                EncodingProcessorsList = Encoders,
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                string taskname = "AME Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvAMEpreset;
                LaunchJobs(
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


        private void Mainform_Shown(object sender, EventArgs e)
        {
            // display the update message if a new version is available
            if (!string.IsNullOrEmpty(Program.MessageNewVersion)) TextBoxLogWriteLine(Program.MessageNewVersion);
        }





        private void oSMFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1) Set the src to MPEG-DASH or Smooth Streaming source" + Constants.endline + "2) Select 'Microsoft Adaptive Streaming Plugin'" + Constants.endline + "3) Click 'Preview and Update'");
            System.Diagnostics.Process ieProcess = System.Diagnostics.Process.Start("iexplore", @"http://wamsclient.cloudapp.net/release/setup.html");
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

        public static string LoadAndUpdateThumbnailsConfiguration(string xmlFileName, string ThumbnailsSize, string ThumbnailsType, string ThumbnailsFileName, string ThumbnailsTimeValue, string ThumbnailsTimeStep, string ThumbnailsTimeStop)
        {
            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var ThumbnailEl = doc.Element("Thumbnail");
            var TimeEl = ThumbnailEl.Element("Time");

            ThumbnailEl.Attribute("Size").SetValue(ThumbnailsSize);
            ThumbnailEl.Attribute("Type").SetValue(ThumbnailsType);
            ThumbnailEl.Attribute("Filename").SetValue(ThumbnailsFileName);

            TimeEl.Attribute("Value").SetValue(ThumbnailsTimeValue);
            TimeEl.Attribute("Step").SetValue(ThumbnailsTimeStep);
            if (ThumbnailsTimeStop != string.Empty)
                TimeEl.Add(new XAttribute("Stop", ThumbnailsTimeStop));

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

            DisplayDeprecatedMessage();

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
                LaunchJobs(processor,
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
                DisplayDeprecatedMessage();

                if (!SelectedAssets.All(a => a.AssetType == AssetType.MultiBitrateMP4 || a.AssetType == AssetType.MP4))
                {
                    MessageBox.Show("Asset(s) should be a multi bitrate or single MP4 file(s).", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                string labeldb = (SelectedAssets.Count > 1) ?
                    "Package these " + SelectedAssets.Count + " assets to Smooth Streaming ?" :
                    "Package '" + SelectedAssets.FirstOrDefault().Name + "' to Smooth Streaming ?";

                string jobname = "MP4 to Smooth Packaging of " + Constants.NameconvInputasset;
                string taskname = "MP4 to Smooth Packaging of " + Constants.NameconvInputasset;
                string outputassetname = Constants.NameconvInputasset + "-Packaged to Smooth";

                if (System.Windows.Forms.MessageBox.Show(labeldb, "Multi MP4 to Smooth", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {

                    // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                    IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                        MediaProcessorNames.WindowsAzureMediaPackager);

                    // Windows Azure Media Packager does not accept string presets, so load xml configuration
                    string smoothConfig = File.ReadAllText(Path.Combine(
                                _configurationXMLFiles,
                                "MediaPackager_MP4toSmooth.xml"));

                    LaunchJobs(processor,
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


        private void DoMenuProtectWithPlayReadyStatic()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count == 0)
            {
                MessageBox.Show("No asset was selected");
                return;

            }
            if (SelectedAssets.FirstOrDefault() == null) return;

            DisplayDeprecatedMessage();

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
                PlayReadyOutputAssetName = Constants.NameconvInputasset + "-PlayReady protected",
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

                LaunchJobs(processor,
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


        public void LaunchJobs(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, int jobpriority, string taskname, string outputassetname, List<string> configuration, AssetCreationOptions myAssetCreationOptions, TaskOptions myTaskOptions, string storageaccountname = "")
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
                try
                {
                    myJob.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    if (selectedassets.Count < 5)  // only if 4 or less jobs submitted
                    {
                        MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobnameloc) + Constants.endline + Constants.endline + Program.GetErrorMessage(e), "Job Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Add useful information to the exception
                    TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                    TextBoxLogWriteLine(e);
                    return;
                }
                TextBoxLogWriteLine("Job '{0}' submitted.", jobnameloc);
                Task.Factory.StartNew(() => dataGridViewJobsV.DoJobProgress(myJob));
            }
            DotabControlMainSwitch(Constants.TabJobs);
            DoRefreshGridJobV(false);
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
            DisplayDeprecatedMessage();

            string labeldb = "Validate '" + mediaAsset.Name + "'  ?";

            if (SelectedAssets.Count > 1)
            {
                labeldb = "Launch the validation of these " + SelectedAssets.Count + " assets ?";
            }
            string jobname = "Validate Multi MP4 of " + Constants.NameconvInputasset;
            string taskname = "Validate Multi MP4 of " + Constants.NameconvInputasset;
            string outputassetname = Constants.NameconvInputasset + "-Multi MP4 validated";


            if (System.Windows.Forms.MessageBox.Show(labeldb, "Multi MP4 Validation", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Read the task configuration data into a string. 
                string configMp4Validation = File.ReadAllText(Path.Combine(
                        _configurationXMLFiles,
                        "MediaPackager_ValidateTask.xml"));

                // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                    MediaProcessorNames.WindowsAzureMediaPackager);

                LaunchJobs(processor,
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

        private void DisplayDeprecatedMessage()
        {
            MessageBox.Show("On November 1, 2015, Windows Azure Media Packager and Windows Azure Media Encryptor will reach end of life. At that time, these components—used to convert file packaging formats and apply encryption—become fully deactivated and no longer available. The conversion and encryption capabilities will be transitioned to the dynamic packaging component of Azure Media Services.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // Get the SDK extension method to  get a reference to the Azure Media Indexer.
            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.AzureMediaIndexer);

            Indexer form = new Indexer(_context)
            {
                IndexerJobName = "Indexing of " + Constants.NameconvInputasset,
                IndexerOutputAssetName = Constants.NameconvInputasset + "-Indexed",
                IndexerProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                IndexerInputAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected for media indexing." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be indexed.",
            };

            string taskname = "Indexing of " + Constants.NameconvInputasset;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string configIndexer = Indexer.LoadAndUpdateIndexerConfiguration(
                Path.Combine(_configurationXMLFiles, @"MediaIndexer.xml"),
                form.IndexerTitle,
                form.IndexerDescription,
                form.IndexerLanguage,
               form.IndexerGenerationOptions
                );

                LaunchJobs(processor,
                    SelectedAssets,
                    form.IndexerJobName,
                    form.JobOptions.Priority,
                    taskname,
                    form.IndexerOutputAssetName,
                    new List<string> { configIndexer },
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

            if (SelectedAssets.Any(a => a.AssetFiles.Count() != 1)
                ||
                SelectedAssets.Any(a => a.AssetFiles.Count() == 1 && (!a.AssetFiles.FirstOrDefault().Name.EndsWith(".wmv", StringComparison.OrdinalIgnoreCase) && (!a.AssetFiles.FirstOrDefault().Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)))
                ))
            {
                MessageBox.Show("Source asset must be a single MP4 or WMV file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Get the SDK extension method to  get a reference to the Azure Media Indexer.
            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.AzureMediaHyperlapse);

            Hyperlapse form = new Hyperlapse(_context)
            {
                HyperlapseJobName = "Hyperlapse processing of " + Constants.NameconvInputasset,
                HyperlapseOutputAssetName = Constants.NameconvInputasset + "-Hyperlapsed",
                HyperlapseProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
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

                LaunchJobs(processor, SelectedAssets, form.HyperlapseJobName, form.JobOptions.Priority, taskname, form.HyperlapseOutputAssetName, new List<string> { configHyperlapse }, form.JobOptions.OutputAssetsCreationOptions, form.JobOptions.TasksOptionsSetting, form.JobOptions.StorageSelected);
            }
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

            string outputassetname = Constants.NameconvInputasset + "-Storage decrypted";
            string jobname = "Storage Decryption of " + Constants.NameconvInputasset;
            string taskname = "Storage Decryption of " + Constants.NameconvInputasset;

            if (System.Windows.Forms.MessageBox.Show(labeldb, "Asset Decryption", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Get the SDK extension method to  get a reference to the Windows Azure Media Packager.
                IMediaProcessor processor = _context.MediaProcessors.GetLatestMediaProcessorByName(
                    MediaProcessorNames.StorageDecryption);

                LaunchJobs(processor,
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
            Process.Start(@"http://aka.ms/azuremediaplayer");
        }

        private void hTML5VideoElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.w3schools.com/html/html5_video.asp");
        }

        private void dynamicPackagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please create a streaming locator in the Publish menu." + Constants.endline + Constants.endline + "Check that you have, at least, one Streaming endpoint scale Unit" + Constants.endline + "The asset should be:" + Constants.endline + "- a Smooth Streaming asset (Clear or PlayReady protected)," + Constants.endline + "- or a Clear Multi MP4 asset.", "Dynamic Packaging");
        }



        private void Mainform_Load(object sender, EventArgs e)
        {
            Hide();

            toolStripStatusLabelWatchFolder.Visible = false;
            UpdateLabelStorageEncryption();

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

            comboBoxOrderStreamingEndpoints.Items.AddRange(
          typeof(OrderStreamingEndpoints)
          .GetFields()
          .Select(i => i.GetValue(null) as string)
          .ToArray()
          );
            comboBoxOrderStreamingEndpoints.SelectedIndex = 0;

            // List of state and numbers of jobs per state

            DoRefreshGridJobV(true);
            DoGridTransferInit();
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

            string taskname = "AME (adv) Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
            Encoders = GetMediaProcessorsByName(Constants.AzureMediaEncoder);
            Encoders.AddRange(GetMediaProcessorsByName(Constants.WindowsAzureMediaEncoder));

            EncodingAMEAdv form = new EncodingAMEAdv(_context)
            {
                EncodingLabel = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected. One job will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingProcessorsList = Encoders,
                EncodingJobName = "AME (adv) Encoding of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + "-AME (adv) encoded",
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
                EncodingOutputAssetName = Constants.NameconvInputasset + "-" + Constants.NameconvProcessorname + " processed",
                SelectedAssets = SelectedAssets,
                EncodingCreationMode = TaskJobCreationMode.SingleJobForAllInputAssets
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read and update the configuration XML.
                //

                var gentasks = form.GetGenericTasks;


                if (form.EncodingCreationMode == TaskJobCreationMode.OneJobPerInputAsset) // a job for each input asset
                {
                    foreach (IAsset asset in SelectedAssets)
                    {
                        string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, gentasks.Count > 1 ? "multi processors" : gentasks.FirstOrDefault().Processor.Name); ;
                        IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);

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
                                 form.JobOptions.TasksOptionsSetting
                                 );

                            string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);
                            task.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.StorageSelected, form.JobOptions.OutputAssetsCreationOptions);
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
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, gentasks.Count > 1 ? "multi processors" : gentasks.FirstOrDefault().Processor.Name); ;

                    IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);

                    foreach (var usertask in gentasks)
                    // let's create all tasks and output assets
                    {
                        string assetname = string.Empty;
                        switch (usertask.InputAssetType)
                        {
                            case TypeInputAssetGeneric.InputJobAssets:
                                assetname = "multiple assets";
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
                           form.JobOptions.TasksOptionsSetting
                           );

                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, assetname).Replace(Constants.NameconvProcessorname, usertask.Processor.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.StorageSelected, form.JobOptions.OutputAssetsCreationOptions);
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
                if (System.Windows.Forms.MessageBox.Show("One or several transfers are in the queue or in progress and will be interrupted." + Constants.endline + "Are you sure that you want to quit the application?", "Caution: transfer(s) in progress", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
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

            Debug.Print("cellformatting" + e.RowIndex + " " + e.ColumnIndex);

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

            ContextMenuItemAssetDisplayInfo.Enabled =
            ContextMenuItemAssetRename.Enabled =
            contextMenuExportFilesToStorage.Enabled =
            createAnAssetFilterToolStripMenuItem.Enabled =
            displayParentJobToolStripMenuItem1.Enabled = singleitem;
            assetFilterInfoupdateToolStripMenuItem.Enabled = singleitem;

            if (singleitem && (assets.FirstOrDefault().AssetFiles.Count() == 1))
            {
                var assetfile = assets.FirstOrDefault().AssetFiles.FirstOrDefault();
                if (assetfile.Name.EndsWith(".ism") && assetfile.ContentFileSize == 0)
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

        private void toolStripMenuItemDownloadToLocal_Click(object sender, EventArgs e)
        {
            DoMenuDownloadToLocal();
        }

        private void toolStripMenuItemUploadFileFromAzure_Click(object sender, EventArgs e)
        {
            DoMenuImportFromAzureStorage();
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
            if (dataGridViewJobsV.Initialized)
            {
                Debug.WriteLine("buttonJobSearch_Click");
                string search = textBoxJobSearch.Text;
                dataGridViewJobsV.SearchInName = search;
                DoRefreshGridJobV(false);
            }
        }

        private void buttonAssetSearch_Click(object sender, EventArgs e)
        {
            if (dataGridViewAssetsV.Initialized)
            {
                string search = textBoxAssetSearch.Text;
                dataGridViewAssetsV.SearchInName = search;
                DoRefreshGridAssetV(false);
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
            if (dataGridViewAssetsV.Initialized)
            {
                DoRefreshGridAssetV(false);
            }
        }

        private void comboBoxFilterJobsTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewJobsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();
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
            // }
        }


        private void azureMediaServicesDocumentationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"https://azure.microsoft.com/en-us/documentation/services/media-services/");
        }

        private void azureMediaServicesForumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"https://social.msdn.microsoft.com/Forums/azure/en-US/home?forum=MediaServices");
        }

        private void azureMediaHelpFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(_HelpFiles + "/AzureMedia.chm");
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

            // let's disable Hyperlapse if not present
            if (!HyperlapsePresent)
            {
                processAssetsWithHyperlapseToolStripMenuItem.Enabled = false;  //menu
                processAssetsWithHyperlapseToolStripMenuItem1.Enabled = false; // mouse context menu
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
            tabPageLive.Invoke(new Action(() => tabPageLive.Text = string.Format(Constants.TabLive + " ({0})", dataGridViewChannelsV.DisplayedCount)));
        }

        private void DoRefreshGridProgramV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewProgramsV.Init(_credentials, _context);
            }

            Debug.WriteLine("DoRefreshGridProgramVNotforsttime");
            int backupindex = 0;
            dataGridViewProgramsV.Invoke(new Action(() => dataGridViewProgramsV.RefreshPrograms(_context, backupindex + 1)));
        }

        private void DoRefreshGridStreamingEndpointV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewStreamingEndpointsV.Init(_credentials, _context);

            }
            Debug.WriteLine("DoRefreshGridOriginsVNotforsttime");
            dataGridViewStreamingEndpointsV.Invoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoints(_context, 1)));

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

            // Encoding Reserved Unit(s)
            if (EncodingRUFeatureOn)
            {
                comboBoxEncodingRU.Items.Clear();
                comboBoxEncodingRU.Items.AddRange(Enum.GetNames(typeof(ReservedUnitType)).ToArray()); // encoding ru hardware type
                comboBoxEncodingRU.SelectedItem = Enum.GetName(typeof(ReservedUnitType), _context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType);
                trackBarEncodingRU.Maximum = _context.EncodingReservedUnits.FirstOrDefault().MaxReservableUnits;
                trackBarEncodingRU.Value = _context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits;
                UpdateLabelProcessorUnits();
            }
            else
            {
                comboBoxEncodingRU.Enabled = trackBarEncodingRU.Enabled = buttonUpdateEncodingRU.Enabled = false;
            }
        }

        private void DoRefreshGridStorageV(bool firstime)
        {
            const long OneTBInByte = 1099511627776;
            const long TotalStorageInBytes = OneTBInByte * 500;

            if (firstime)
            {
                // Storage tab
                dataGridViewStorage.ColumnCount = 2;

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
                dataGridViewStorage.Columns[2].Width = 600;

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

                int rowi = dataGridViewStorage.Rows.Add(storage.Name + (string)((storage.IsDefault) ? " (default)" : string.Empty), displaycapacity ? AssetInfo.FormatByteSize(storage.BytesUsed) : "?", displaycapacity ? capacityPercentageFullTmp : null);
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
            List<Filter> filters = _contextdynmanifest.ListGlobalFilters();

            foreach (var filter in filters)
            {
                string s = null;
                string e = null;
                string d = null;
                string l = null;

                if (filter.PresentationTimeRange != null)
                {
                    long start = Convert.ToInt64(filter.PresentationTimeRange.StartTimestamp);
                    long end = Convert.ToInt64(filter.PresentationTimeRange.EndTimestamp);
                    long dvr = Convert.ToInt64(filter.PresentationTimeRange.PresentationWindowDuration);
                    long live = Convert.ToInt64(filter.PresentationTimeRange.LiveBackoffDuration);

                    double scale = Convert.ToDouble(filter.PresentationTimeRange.Timescale) / 10000000;
                    e = (end == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(end / scale)).ToString(@"d\.hh\:mm\:ss");
                    s = (start == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(start / scale)).ToString(@"d\.hh\:mm\:ss");
                    d = (dvr == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(dvr / scale)).ToString(@"d\.hh\:mm\:ss");
                    l = (live == long.MaxValue) ? "max" : TimeSpan.FromTicks((long)(live / scale)).ToString(@"d\.hh\:mm\:ss");
                }
                int rowi = dataGridViewFilters.Rows.Add(filter.Name, filter.Tracks.Count, s, e, d, l);
            }
            tabPageFilters.Text = string.Format(Constants.TabFilters + " ({0})", filters.Count());
        }


        private List<IChannel> ReturnSelectedChannels()
        {
            List<IChannel> SelectedChannels = new List<IChannel>();
            foreach (DataGridViewRow Row in dataGridViewChannelsV.SelectedRows)
            {
                SelectedChannels.Add(_context.Channels.Where(j => j.Id == Row.Cells[dataGridViewChannelsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
            }
            SelectedChannels.Reverse();
            return SelectedChannels;
        }
        private List<IStreamingEndpoint> ReturnSelectedStreamingEndpoints()
        {
            List<IStreamingEndpoint> SelectedOrigins = new List<IStreamingEndpoint>();
            foreach (DataGridViewRow Row in dataGridViewStreamingEndpointsV.SelectedRows)
            {
                SelectedOrigins.Add(_context.StreamingEndpoints.Where(j => j.Id == Row.Cells[dataGridViewStreamingEndpointsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
            }
            SelectedOrigins.Reverse();
            return SelectedOrigins;
        }

        private List<IProgram> ReturnSelectedPrograms()
        {
            bool Error = false;
            List<IProgram> SelectedPrograms = new List<IProgram>();
            foreach (DataGridViewRow Row in dataGridViewProgramsV.SelectedRows)
            {
                try
                {
                    SelectedPrograms.Add(_context.Programs.Where(j => j.Id == Row.Cells[dataGridViewProgramsV.Columns["Id"].Index].Value.ToString()).FirstOrDefault());
                }
                catch
                {
                    Error = true;
                    break;

                }
            }
            if (!Error)
            {
                SelectedPrograms.Reverse();
                return SelectedPrograms;
            }
            else
            {
                return null;
            }

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
                    if (MessageBox.Show("One or several programs are running. Do you want to stop the program(s) ?", "Channel stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            TextBoxLogWriteLine("Starting channel '{0}' ", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendStartOperationAsync, myC, "started", _context, this, dataGridViewChannelsV);
        }

        private async Task<IOperation> StopChannelAsync(IChannel myC)
        {
            TextBoxLogWriteLine("Stopping channel '{0}'", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendStopOperationAsync, myC, "stopped", _context, this, dataGridViewChannelsV);
        }

        private async Task<IOperation> ResetChannelAsync(IChannel myC)
        {
            TextBoxLogWriteLine("Reseting channel '{0}'", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendResetOperationAsync, myC, "reset", _context, this, dataGridViewChannelsV);
        }

        private async Task<IOperation> DeleteChannelAsync(IChannel myC)
        {
            TextBoxLogWriteLine("Deleting channel '{0}'", myC.Name);
            return await ChannelInfo.ChannelExecuteOperationAsync(myC.SendDeleteOperationAsync, myC, "deleted", _context, this, dataGridViewChannelsV);
        }


        // PROGRAM ASYNC OPERATIONS

        private async Task<IOperation> StopProgramASync(IProgram myP)
        {
            TextBoxLogWriteLine("Stopping program '{0}'...", myP.Name);
            return await Task.Run(() => ProgramExecuteOperationAsync(myP.SendStopOperationAsync, myP, "stopped"));
        }

        private async Task<IOperation> StartProgramASync(IProgram myP)
        {
            TextBoxLogWriteLine("Starting program '{0}'...", myP.Name);
            return await Task.Run(() => ProgramExecuteOperationAsync(myP.SendStartOperationAsync, myP, "started"));
        }


        internal async Task ProgramExecuteAsync(Func<Task> fCall, string strObjectName, string strStatusSuccess)
        // for program update and deletion
        {
            try
            {
                await fCall();
                TextBoxLogWriteLine("Program '{0}' {1}.", strObjectName, strStatusSuccess);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with program '{0}' : {1}", strObjectName, Program.GetErrorMessage(ex), true);
            }
        }



        // STREAMING ENDPOINT ASYNC OPERATIONS

        private async Task<IOperation> StartStreamingEndpoint(IStreamingEndpoint myO)
        {
            TextBoxLogWriteLine("Starting streaming endpoint '{0}'...", myO.Name);
            return await Task.Run(() => StreamingEndpointExecuteOperationAsync(myO.SendStartOperationAsync, myO, "started"));
        }
        private async Task<IOperation> StopStreamingEndpointAsync(IStreamingEndpoint mySE)
        {
            TextBoxLogWriteLine("Stopping streaming endpoint '{0}'...", mySE.Name);
            return await Task.Run(() => StreamingEndpointExecuteOperationAsync(mySE.SendStopOperationAsync, mySE, "stopped"));
        }

        private async Task<IOperation> DeleteStreamingEndpointAsync(IStreamingEndpoint myO)
        {
            TextBoxLogWriteLine("Deleting streaming endpoint '{0}'.", myO.Name);
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
                    TextBoxLogWriteLine("Scaling streaming endpoint '{0}' to {1} unit(s)...", myO.Name, unit.ToString());
                    //await Task.Run(() => myO.ScaleAsync(unit));
                    operation = await myO.SendScaleOperationAsync(unit);
                    while (operation.State == OperationState.InProgress)
                    {
                        //refresh the operation
                        operation = _context.Operations.GetOperation(operation.Id);
                        System.Threading.Thread.Sleep(1000);
                    }
                    if (operation.State == OperationState.Succeeded)
                    {
                        TextBoxLogWriteLine("Streaming endpoint '{0}' scaled.", myO.Name);
                    }
                    else
                    {
                        TextBoxLogWriteLine("Streaming endpoint '{0}' did NOT scaled. (Error {1})", myO.Name, operation.ErrorCode, true);
                        TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                    }
                    dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myO)), null);
                }

                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when scaling streaming endpoint '{0}' : {1}", myO.Name, Program.GetErrorMessage(ex), true);
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
                TextBoxLogWriteLine("Program '{0}' {1}.", program.Name, strStatusSuccess);
                dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.RefreshProgram(program)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with program '{0}' : {1}", program.Name, Program.GetErrorMessage(ex), true);
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
                    TextBoxLogWriteLine("Program '{0}' {1}.", program.Name, strStatusSuccess);
                }
                else
                {
                    TextBoxLogWriteLine("Program '{0}' NOT {1}. (Error {2})", program.Name, strStatusSuccess, operation.ErrorCode, true);
                    TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
                dataGridViewProgramsV.BeginInvoke(new Action(() => dataGridViewProgramsV.RefreshProgram(program)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with program '{0}' : {1}", program.Name, Program.GetErrorMessage(ex), true);
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
                    TextBoxLogWriteLine("Streaming endpoint '{0}' {1}.", myO.Name, strStatusSuccess);
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
                    TextBoxLogWriteLine("Streaming endpoint '{0}' NOT {1}. (Error {2})", myO.Name, strStatusSuccess, operation.ErrorCode, true);
                    TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
                dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myO)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with streaming endpoint '{0}' : {1}", myO.Name, Program.GetErrorMessage(ex), true);
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
                    TextBoxLogWriteLine("{0} '{1}' {2}.", objectlogname, objectname, strStatusSuccess);
                }
                else
                {
                    TextBoxLogWriteLine("{0} '{1}' NOT {2}. (Error {3})", objectlogname, objectname, strStatusSuccess, operation.ErrorCode, true);
                    TextBoxLogWriteLine("Error message : {0}", operation.ErrorMessage, true);
                }
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with {0} '{1}' : {2}", objectlogname, objectname, Program.GetErrorMessage(ex), true);
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

                    if (System.Windows.Forms.MessageBox.Show(question, "C" + hannelstr + " deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
                            Programs.ToList().ForEach(p => TextBoxLogWriteLine("Deleting program '{0}'...", p.Name));
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
                            SelectedChannels.ToList().ForEach(c => TextBoxLogWriteLine("Deleting channel '{0}'", c.Name));
                            var taskcdel = SelectedChannels.Select(c => c.DeleteAsync()).ToArray();
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
                    if (MessageBox.Show("One or several programs are running which prevents the channel(s) reset. Do you want to stop the program(s) ?", "Channel reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                TextBoxLogWriteLine("Creating Channel '{0}'...", form.ChannelName);

                var options = new ChannelCreationOptions()
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
                    channel.Description = form.GetChannelDescription;
                    channel.Input.KeyFrameInterval = form.KeyframeInterval;

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
            List<IChannel> SelectedChannels = ReturnSelectedChannels();
            if (SelectedChannels.Count > 0)
            {
                try // sometimes, the channel can be null (if just deleted)
                {
                    dataGridViewProgramsV.ChannelSourceIDs = SelectedChannels.Select(c => c.Id).ToList();
                }
                catch
                {

                }
                DoRefreshGridProgramV(false);
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
                Task.Run(async () =>
                {
                    // let's stop the programs now
                    var tasksstop = SelectedPrograms.Select(p => StopProgramASync(p)).ToArray();
                    await Task.WhenAll(tasksstop);
                }
      );
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
                        IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + assetName, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDays), AccessPermissions.Read);
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
                            ManifestName = form.IsReplica ? form.ReplicaManifestName : null // if replica is selected, then we force the manifest name
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
                            //StartProgam(_context.Programs.Where(p => p.Name == form.ProgramName && p.ChannelId == channel.Id).FirstOrDefault());
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
                    ProgramInformation form = new ProgramInformation(this, _context, _contextdynmanifest)
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

            string taskname = "Thumbnails generation of " + Constants.NameconvInputasset;
            IMediaProcessor processor = GetLatestMediaProcessorByName(Constants.AzureMediaEncoder);

            Thumbnails form = new Thumbnails(_context)
            {
                ThumbnailsFileName = "{OriginalFilename}_{ThumbnailIndex}.{DefaultExtension}",
                ThumbnailsInputAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected for thumbnails generation." : "Generate thumbnails for '" + SelectedAssets.FirstOrDefault().Name + "'  ?",
                ThumbnailsOutputAssetName = Constants.NameconvInputasset + "-Thumbnails",
                ThumbnailsProcessorName = "Processor: " + processor.Vendor + " / " + processor.Name + " v" + processor.Version,
                ThumbnailsJobName = "Thumbnails generation of " + Constants.NameconvInputasset,
                ThumbnailsTimeValue = "0:0:0",
                ThumbnailsTimeStep = "0:0:5",
                ThumbnailsTimeStop = string.Empty,
                ThumbnailsSize = "300,*",
                ThumbnailsType = "Jpeg"
            };


            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string configThumbnails = LoadAndUpdateThumbnailsConfiguration(
                Path.Combine(_configurationXMLFiles, @"Thumbnails.xml"),
                form.ThumbnailsSize,
                form.ThumbnailsType,
                form.ThumbnailsFileName,
                form.ThumbnailsTimeValue,
                form.ThumbnailsTimeStep,
                form.ThumbnailsTimeStop
                );

                LaunchJobs(processor,
                    SelectedAssets,
                    form.ThumbnailsJobName,
                    form.JobOptions.Priority,
                    taskname,
                    form.ThumbnailsOutputAssetName,
                    new List<string> { configThumbnails },
                    form.JobOptions.OutputAssetsCreationOptions,
                    form.JobOptions.TasksOptionsSetting,
                    form.JobOptions.StorageSelected);
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
                MyOrigin = streamingendpoint,
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
                if (System.Windows.Forms.MessageBox.Show(question, "Streaming endpoint(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
                            IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + program.Name, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDays), AccessPermissions.Read);
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
                        AssetInfo.DoPlayBackWithBestStreamingEndpoint(typeplayer: ptype, Urlstr: channel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri, DoNotRewriteURL: true, context: _context, formatamp: AzureMediaPlayerFormats.Smooth);
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
            System.Windows.Forms.Clipboard.SetText(ReturnSelectedChannels().FirstOrDefault().Input.Endpoints.FirstOrDefault().Url.AbsoluteUri);
        }

        private void copyPreviewURLToClipboard_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(ReturnSelectedChannels().FirstOrDefault().Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri);
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

        private void DoBatchUpload()
        {
            BatchUploadFrame1 form = new BatchUploadFrame1();
            if (form.ShowDialog() == DialogResult.OK)
            {
                BatchUploadFrame2 form2 = new BatchUploadFrame2(form.BatchFolder, form.BatchProcessFiles, form.BatchProcessSubFolders, _context) { Left = form.Left, Top = form.Top };
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    int index;
                    foreach (string folder in form2.BatchSelectedFolders)
                    {
                        index = DoGridTransferAddItem(string.Format("Upload of folder '{0}'", Path.GetFileName(folder)), TransferType.UploadFromFolder, Properties.Settings.Default.useTransferQueue);
                        Task.Factory.StartNew(() => ProcessUploadFromFolder(folder, index, form2.StorageSelected));
                    }
                    foreach (string file in form2.BatchSelectedFiles)
                    {
                        index = DoGridTransferAddItem("Upload of file '" + Path.GetFileName(file) + "'", TransferType.UploadFromFile, Properties.Settings.Default.useTransferQueue);
                        Task.Factory.StartNew(() => ProcessUploadFileAndMore(file, index, null, form2.StorageSelected));
                    }
                    DotabControlMainSwitch(Constants.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
        }

        private void azureMediaBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://azure.microsoft.com/blog/topics/media-services/");
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
            if (dataGridViewProgramsV.Initialized)
            {
                DoRefreshGridProgramV(false);
            }
        }

        private void buttonSetFilterProgram_Click(object sender, EventArgs e)
        {
            if (dataGridViewProgramsV.Initialized)
            {
                dataGridViewProgramsV.SearchInName = textBoxSearchNameProgram.Text;
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
            string labelAssetName;
            bool oktoproceed = false;
            if (SelectedAssets.Count > 0)
            {
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
                            bool NeedToDisplayPlayReadyLicense = form1.GetNumberOfAuthorizationPolicyOptions > 0;
                            AddDynamicEncryptionFrame2_PlayReadyKeyConfig form2_PlayReady = new AddDynamicEncryptionFrame2_PlayReadyKeyConfig(
                                SelectedAssets.Count > 1, form1.GetNumberOfAuthorizationPolicyOptions > 0, forceusertoprovidekey || (form1.GetNumberOfAuthorizationPolicyOptions == 0), !NeedToDisplayPlayReadyLicense) { Left = form1.Left, Top = form1.Top };
                            if (form2_PlayReady.ShowDialog() == DialogResult.OK)
                            {
                                List<AddDynamicEncryptionFrame3> form3list = new List<AddDynamicEncryptionFrame3>();
                                List<AddDynamicEncryptionFrame4_PlayReadyLicense> form4list = new List<AddDynamicEncryptionFrame4_PlayReadyLicense>();
                                bool usercancelledform3or4 = false;
                                int step = 3;
                                string tokensymmetrickey = null;
                                for (int i = 0; i < form1.GetNumberOfAuthorizationPolicyOptions; i++)
                                {
                                    AddDynamicEncryptionFrame3 form3 = new AddDynamicEncryptionFrame3(_context, step, i + 1, tokensymmetrickey, !NeedToDisplayPlayReadyLicense) { Left = form2_PlayReady.Left, Top = form2_PlayReady.Top };
                                    if (form3.ShowDialog() == DialogResult.OK)
                                    {
                                        step++;
                                        form3list.Add(form3);
                                        tokensymmetrickey = form3.SymmetricKey;
                                        AddDynamicEncryptionFrame4_PlayReadyLicense form4_PlayReadyLicense = new AddDynamicEncryptionFrame4_PlayReadyLicense(step, i + 1, i == (form1.GetNumberOfAuthorizationPolicyOptions - 1)) { Left = form3.Left, Top = form3.Top };
                                        if (NeedToDisplayPlayReadyLicense) // it's a PlayReady license and user wants to deliver the license from Azure Media Services
                                        {
                                            step++;
                                            if (form4_PlayReadyLicense.ShowDialog() == DialogResult.OK) // let's display the dialog box to configure the playready license
                                            {
                                                form4list.Add(form4_PlayReadyLicense);
                                            }
                                            else
                                            {
                                                usercancelledform3or4 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        usercancelledform3or4 = true;
                                    }
                                }
                                if (!usercancelledform3or4)
                                {
                                    DoDynamicEncryptionAndKeyDeliveryWithPlayReady(SelectedAssets, form1, form2_PlayReady, form3list, form4list, true);
                                    oktoproceed = true;
                                    dataGridViewAssetsV.AnalyzeItemsInBackground();
                                }
                            }
                            break;

                        ///////////////////////////////////////////// AES Dynamic Encryption
                        case AssetDeliveryPolicyType.DynamicEnvelopeEncryption:
                            AddDynamicEncryptionFrame2_AESKeyConfig form2_AES = new AddDynamicEncryptionFrame2_AESKeyConfig(forceusertoprovidekey) { Left = form1.Left, Top = form1.Top };
                            if (form2_AES.ShowDialog() == DialogResult.OK)
                            {
                                List<AddDynamicEncryptionFrame3> form3list = new List<AddDynamicEncryptionFrame3>();
                                bool usercancelledform3 = false;
                                string tokensymmetrickey = null;
                                for (int i = 0; i < form1.GetNumberOfAuthorizationPolicyOptions; i++)
                                {
                                    AddDynamicEncryptionFrame3 form3 = new AddDynamicEncryptionFrame3(_context, i + 3, i + 1, tokensymmetrickey, true) { Left = form2_AES.Left, Top = form2_AES.Top };
                                    if (form3.ShowDialog() == DialogResult.OK)
                                    {
                                        form3list.Add(form3);
                                        tokensymmetrickey = form3.SymmetricKey;
                                    }
                                    else
                                    {
                                        usercancelledform3 = true;
                                    }
                                }

                                if (!usercancelledform3)
                                {
                                    DoDynamicEncryptionWithAES(SelectedAssets, form1, form2_AES, form3list);
                                    oktoproceed = true;
                                    dataGridViewAssetsV.AnalyzeItemsInBackground();
                                }
                            }
                            break;

                        ///////////////////////////////////////////// Decrypt storage protected content
                        case AssetDeliveryPolicyType.NoDynamicEncryption:
                            AddDynDecryption(SelectedAssets, form1, _context);
                            oktoproceed = true;
                            dataGridViewAssetsV.AnalyzeItemsInBackground();
                            break;

                        default:
                            break;

                    }

                }
            }


            return oktoproceed;
        }



        private void DoDynamicEncryptionAndKeyDeliveryWithPlayReady(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, AddDynamicEncryptionFrame2_PlayReadyKeyConfig form2_PlayReady, List<AddDynamicEncryptionFrame3> form3list, List<AddDynamicEncryptionFrame4_PlayReadyLicense> form4PlayReadyLicenseList, bool DisplayUI)
        {
            bool ErrorCreationKey = false;
            bool reusekey = false;
            bool firstkeycreation = true;
            IContentKey formerkey = null;

            if (!form2_PlayReady.ContentKeyRandomGeneration)  // user want to manually enter the key and did not provide a seed
            {
                // if the key already exists in the account (same key id), let's 
                formerkey = SelectedAssets.FirstOrDefault().GetMediaContext().ContentKeys.Where(c => c.Id == Constants.ContentKeyIdPrefix + form2_PlayReady.PlayReadyKeyId.ToString()).FirstOrDefault();
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

                        if (form1.GetNumberOfAuthorizationPolicyOptions > 0 && (form2_PlayReady.ContentKeyRandomGeneration)) // Azure will deliver the license and user want to auto generate the key, so we can create a key with a random content key
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
                        else // user wants to deliver with an external PlayReady server or want to provide the key, so let's create the key based on what the user input
                        {
                            // if the key does not exist in the account (same key id), let's 
                            if (firstkeycreation && !reusekey)
                            {
                                if (!string.IsNullOrEmpty(form2_PlayReady.PlayReadyKeySeed)) // seed has been given
                                {
                                    Guid keyid = (form2_PlayReady.PlayReadyKeyId == null) ? Guid.NewGuid() : (Guid)form2_PlayReady.PlayReadyKeyId;
                                    byte[] bytecontentkey = CommonEncryption.GeneratePlayReadyContentKey(Convert.FromBase64String(form2_PlayReady.PlayReadyKeySeed), keyid);
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
                                        contentKey = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context, (Guid)form2_PlayReady.PlayReadyKeyId, Convert.FromBase64String(form2_PlayReady.PlayReadyContentKey));
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
                    else if (form1.GetNumberOfAuthorizationPolicyOptions == 0)  // user wants to deliver with an external PlayReady server but the key exists already !
                    {
                        TextBoxLogWriteLine("Warning for asset '{0}'. A CENC key already exists. You need to make sure that your external PlayReady server can deliver the license for this asset.", AssetToProcess.Name, true);
                    }
                    else // let's use existing content key
                    {
                        contentKey = contentkeys.FirstOrDefault();
                        TextBoxLogWriteLine("Existing key '{0}' will be used for asset '{1}'.", contentKey.Id, AssetToProcess.Name);
                    }
                    if (form1.GetNumberOfAuthorizationPolicyOptions > 0) // PlayReady license and delivery from Azure Media Services
                    {
                        // let's create the Authorization Policy
                        IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                                       ContentKeyAuthorizationPolicies.
                                       CreateAsync("My Authorization Policy").Result;

                        // Associate the content key authorization policy with the content key.
                        contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                        contentKey = contentKey.UpdateAsync().Result;

                        foreach (var form3 in form3list)
                        {
                            // let's build the PlayReady license template
                            string PlayReadyLicenseDeliveryConfig = null;
                            ErrorCreationKey = false;
                            try
                            {
                                PlayReadyLicenseDeliveryConfig = form4PlayReadyLicenseList[form3list.IndexOf(form3)].GetLicenseTemplate;
                                // PlayReadyLicenseDeliveryConfig = DynamicEncryption.ConfigurePlayReadyLicenseTemplate(form4PlayReadyLicenseList[form3list.IndexOf(form3)].GetLicenseTemplate);
                            }
                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when configuring the PlayReady license template.", true);
                                TextBoxLogWriteLine(e);
                                ErrorCreationKey = true;
                            }
                            if (!ErrorCreationKey)
                            {
                                IContentKeyAuthorizationPolicyOption policyOption = null;
                                try
                                {
                                    switch (form3.GetKeyRestrictionType)
                                    {
                                        case ContentKeyRestrictionType.Open:
                                            policyOption = DynamicEncryption.AddOpenAuthorizationPolicyOption(contentKey, ContentKeyDeliveryType.PlayReadyLicense, PlayReadyLicenseDeliveryConfig, _context);
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

                                            policyOption = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyPlayReady(contentKey, form3.GetAudience, form3.GetIssuer, form3.GetTokenRequiredClaims, form3.AddContentKeyIdentifierClaim, form3.GetTokenType, form3.GetDetailedTokenType, mytokenverifkey, _context, PlayReadyLicenseDeliveryConfig, OpenIdDoc);
                                            TextBoxLogWriteLine("Created Token CENC authorization policy for the asset {0} ", contentKey.Id, AssetToProcess.Name);
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
                                                TextBoxLogWriteLine("The authorization test token for option #{0} ({1} with Bearer) is:\n{2}", form3list.IndexOf(form3), form3.GetTokenType.ToString(), Constants.Bearer + testToken.TokenString);
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
                    if (form1.GetDeliveryPolicyType != AssetDeliveryPolicyType.None)
                    {
                        IAssetDeliveryPolicy DelPol = null;
                        string name = string.Format("AssetDeliveryPolicy {0} ({1})", form1.GetContentKeyType.ToString(), form1.GetAssetDeliveryProtocol.ToString());
                        ErrorCreationKey = false;
                        try
                        {
                            if (form1.GetNumberOfAuthorizationPolicyOptions > 0) // Licenses delivered by Azure Media Services
                            {
                                DelPol = DynamicEncryption.CreateAssetDeliveryPolicyCENC(AssetToProcess, contentKey, form1.GetAssetDeliveryProtocol, name, _context, null, false, form2_PlayReady.PlayReadyCustomAttributes);
                            }
                            else // Licenses NOT delivered by Azure Media Services but by a third party server
                            {
                                DelPol = DynamicEncryption.CreateAssetDeliveryPolicyCENC(AssetToProcess, contentKey, form1.GetAssetDeliveryProtocol, name, _context, new Uri(form2_PlayReady.PlayReadyLAurl), form2_PlayReady.PlayReadyLAurlEncodeForSL, form2_PlayReady.PlayReadyCustomAttributes);
                            }

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

        private void DoDynamicEncryptionWithAES(List<IAsset> SelectedAssets, AddDynamicEncryptionFrame1 form1, AddDynamicEncryptionFrame2_AESKeyConfig form2, List<AddDynamicEncryptionFrame3> form3list)
        {
            bool Error = false;
            string aeskey = string.Empty;
            if (!form2.ContentKeyRandomGeneration)
            {
                aeskey = form2.AESContentKey;
            }

            foreach (IAsset AssetToProcess in SelectedAssets)
            {

                if (AssetToProcess != null)
                {
                    IContentKey contentKey = null;

                    var contentkeys = AssetToProcess.ContentKeys.Where(c => c.ContentKeyType == form1.GetContentKeyType);
                    if (contentkeys.Count() == 0) // no content key existing so we need to create one
                    {
                        Error = false;
                        try
                        {
                            if ((!form2.ContentKeyRandomGeneration) && !string.IsNullOrEmpty(aeskey)) // user has to provide the key
                            {
                                contentKey = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess, Convert.FromBase64String(aeskey));
                            }
                            else
                            {
                                contentKey = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess);
                            }
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There is a problem when creating the content key for '{0}'.", AssetToProcess.Name, true);
                            TextBoxLogWriteLine(e);
                            Error = true;
                        }
                        if (!Error)
                        {
                            TextBoxLogWriteLine("Created key {0} for the asset {1} ", contentKey.Id, AssetToProcess.Name);
                        }

                    }

                    else // let's use existing content key
                    {
                        contentKey = contentkeys.FirstOrDefault();
                        TextBoxLogWriteLine("Existing key {0} will be used for asset {1}.", contentKey.Id, AssetToProcess.Name);
                    }


                    // let's create the Authorization Policy
                    IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                                   ContentKeyAuthorizationPolicies.
                                   CreateAsync("My Authorization Policy").Result;

                    // Associate the content key authorization policy with the content key.
                    contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
                    contentKey = contentKey.UpdateAsync().Result;


                    foreach (var form3 in form3list)
                    {
                        IContentKeyAuthorizationPolicyOption policyOption = null;
                        Error = false;
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
                                        TextBoxLogWriteLine("The authorization test token for option #{0} ({1} with Bearer) is:\n{2}", form3list.IndexOf(form3), form3.GetTokenType.ToString(), Constants.Bearer + testToken.TokenString);
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
                            Error = true;
                        }

                    }
                    contentKeyAuthorizationPolicy.Update();

                    // Let's create the Asset Delivery Policy now
                    IAssetDeliveryPolicy DelPol = null;
                    string name = string.Format("AssetDeliveryPolicy {0} ({1})", form1.GetContentKeyType.ToString(), form1.GetAssetDeliveryProtocol.ToString());

                    try
                    {
                        DelPol = DynamicEncryption.CreateAssetDeliveryPolicyAES(AssetToProcess, contentKey, form1.GetAssetDeliveryProtocol, name, _context);
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
                labelAssetName = string.Format("Dynamic encryption policies and key authorization policies will be removed for asset '{0}'.", SelectedAssets.FirstOrDefault().Name);
                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = string.Format("Dynamic encryption policies and key authorization policies will removed for these {0} selected assets.", SelectedAssets.Count.ToString());
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
                if (ValidURIs.FirstOrDefault() != null)
                {
                    string url = ValidURIs.FirstOrDefault().AbsoluteUri;
                    /*
                    if (selectedGlobalFilter != null)
                    {
                        url = AssetInfo.AddFilterToUrlString(url, selectedGlobalFilter);
                    }
                    */
                    System.Windows.Forms.Clipboard.SetText(url);
                }
            }
        }


        private void jwPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.jwplayer.com/partners/azure/");
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

        private void withMPEGDASHAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.DASHAzurePage);
        }

        private void withDASHLiveAzurePlayerToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.DASHLiveAzure);
        }

        private void comboBoxOrderStreamingEndpoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewStreamingEndpointsV.OrderStreamingEndpointsInGrid = ((ComboBox)sender).SelectedItem.ToString();

            if (dataGridViewStreamingEndpointsV.Initialized)
            {
                DoRefreshGridStreamingEndpointV(false);
            }
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
                    question += Constants.endline + Constants.endline + "This will delete the program, the related asset and locator and will re-create them with the same ISM file name, locator ID, keys and delivery policies.";
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
            Process.Start(@"http://sltoken.azurewebsites.net");
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
            Process.Start(@"http://dashplayer.azurewebsites.net");
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
                JobOptions = new JobOptionsVar
                {
                    Priority = myJob.Priority,
                    OutputAssetsCreationOptions = myJob.OutputMediaAssets.FirstOrDefault().Options,
                    StorageSelected = myJob.OutputMediaAssets.FirstOrDefault().StorageAccountName,
                    TasksOptionsSetting = myJob.Tasks.FirstOrDefault().Options
                },
                EncodingCreationMode = TaskJobCreationMode.SingleJobForAllInputAssets
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;
                IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);

                string tasknameloc = taskname.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                ITask task = job.Tasks.AddNew(
                            tasknameloc,
                           form.EncodingProcessorSelected,
                           form.EncodingConfiguration,
                           form.JobOptions.TasksOptionsSetting
                           );
                // Specify the graph asset to be encoded, followed by the input video asset to be used
                task.InputAssets.AddRange(myJob.InputMediaAssets.ToList());
                string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                task.OutputAssets.AddNew(outputassetnameloc, form.JobOptions.StorageSelected, form.JobOptions.OutputAssetsCreationOptions);

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
            Process.Start(@"http://aestoken.azurewebsites.net");
        }

        private void withFlashAESTokenPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.FlashAESToken);
        }

        private void withSilverlightPlayReadyTokenPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.SilverlightPlayReadyToken);
        }

        private void buttonUpdateEncodingRU_Click(object sender, EventArgs e)
        {
            DoUpdateEncodingRU();
        }

        private async void DoUpdateEncodingRU()
        {
            bool oktocontinue = true;

            if (trackBarEncodingRU.Value == 0 && ((string)comboBoxEncodingRU.SelectedItem != Enum.GetName(typeof(ReservedUnitType), ReservedUnitType.Basic)))
            // user selected 0 with a non BASIC hardware...
            {
                if (MessageBox.Show("You selected 0 unit but the encoding type is not Basic. Are you sure you want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    oktocontinue = false;
                }
            }

            if (oktocontinue)
            {
                TextBoxLogWriteLine(string.Format("Updating to {0} {1} reserved unit{2}...", (int)trackBarEncodingRU.Value, (string)comboBoxEncodingRU.SelectedItem, (int)trackBarEncodingRU.Value > 1 ? "s" : string.Empty));

                IEncodingReservedUnit EncResUnit = _context.EncodingReservedUnits.FirstOrDefault();
                EncResUnit.CurrentReservedUnits = (int)trackBarEncodingRU.Value;
                EncResUnit.ReservedUnitType = (ReservedUnitType)(Enum.Parse(typeof(ReservedUnitType), (string)comboBoxEncodingRU.SelectedItem));

                trackBarEncodingRU.Enabled = false;
                comboBoxEncodingRU.Enabled = false;
                buttonUpdateEncodingRU.Enabled = false;

                await Task.Run(() =>
                {
                    try
                    {
                        EncResUnit.Update();
                        TextBoxLogWriteLine("Encoding reserved unit(s) updated.");
                    }
                    catch (Exception ex)
                    {
                        TextBoxLogWriteLine("Error when updating encoding reserved unit(s).");
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
            // If RU is set to 0, let's switch to basic
            if (trackBarEncodingRU.Value == 0)
            {
                comboBoxEncodingRU.SelectedItem = Enum.GetName(typeof(ReservedUnitType), ReservedUnitType.Basic);
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
            Debug.Print("rowpostpaint" + e.RowIndex);
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

        public void DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType playertype, List<IAsset> listassets, string selectedGlobalFilter = null)
        {
            foreach (var myAsset in listassets)
            {
                if (!IsThereALocatorValid(myAsset, ref PlayBackLocator, LocatorType.OnDemandOrigin)) // No streaming locator valid
                {
                    if (MessageBox.Show(string.Format("There is no valid streaming locator for asset '{0}'.\nDo you want to create one ?", myAsset.Name), "Streaming locator", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        TextBoxLogWriteLine("Creating locator for asset '{0}'", myAsset.Name);
                        IAccessPolicy policy = _context.AccessPolicies.Create("AP:" + myAsset.Name, TimeSpan.FromDays(Properties.Settings.Default.DefaultLocatorDurationDays), AccessPermissions.Read);
                        ILocator MyLocator = _context.Locators.CreateLocator(LocatorType.OnDemandOrigin, myAsset, policy, null);
                    }
                }

                if (IsThereALocatorValid(myAsset, ref PlayBackLocator, LocatorType.OnDemandOrigin)) // There is a streaming locator valid
                {
                    Uri MyUri;
                    if (playertype == PlayerType.DASHIFRefPlayer || playertype == PlayerType.DASHLiveAzure)
                    {
                        MyUri = PlayBackLocator.GetMpegDashUri();
                    }
                    else
                    {
                        MyUri = PlayBackLocator.GetSmoothStreamingUri();
                    }

                    if (MyUri != null)
                    {
                        AssetInfo.DoPlayBackWithBestStreamingEndpoint(playertype, MyUri.AbsoluteUri, _context, myAsset, false, selectedGlobalFilter);
                    }
                    else
                    {
                        // there is a streaming locator but the asset cannot be played back with adaptive streaming. It could be a single file in the asset.
                        // if this is a single MP4 file, we can play it with the streaming locator but as progressive download
                        if (myAsset.AssetFiles.Count() == 1 && myAsset.AssetFiles.FirstOrDefault().Name.ToLower().EndsWith(".mp4") && (playertype == PlayerType.AzureMediaPlayer))
                        {
                            MessageBox.Show(string.Format("The asset '{0}' in a single MP4 file and cannot be played with adaptive streaming as there is no manifest file.\nThe MP4 file will be played through progressive download.", myAsset.Name), "Single MP4 file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.AzureMediaPlayer, PlayBackLocator.Path + myAsset.AssetFiles.FirstOrDefault().Name, _context, myAsset, formatamp: AzureMediaPlayerFormats.VideoMP4);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The asset '{0}' does not seem to be playable with adaptive streaming.", myAsset.Name), "Adaptive streaming", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }

        private void DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType playertype, string selectedGlobalFilter = null)
        {
            DoPlaySelectedAssetsOrProgramsWithPlayer(playertype, ReturnSelectedAssetsFromProgramsOrAssets(), selectedGlobalFilter);
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
            Process.Start(@"http://ie.microsoft.com/testdrive/graphics/captionmaker");
        }

        private void removeKeysForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRemoveKeys();
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
                    System.Windows.Forms.Clipboard.SetText(Constants.Bearer + testToken.TokenString);
                    MessageBox.Show(string.Format("The test token below has been be copied to the log window and clipboard.\n\n{0}", Constants.Bearer + testToken.TokenString), "Test token copied");
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
                            Task.Factory.StartNew(() => ProcessExportAssetToAnotherAMSAccount(form.DestinationLoginCredentials, form.DestinationStorageAccount, storagekeys, new List<IAsset>() { asset }, form.CopyAssetName.Replace(Constants.NameconvAsset, asset.Name), index, form.DeleteSourceAsset, form.CopyDynEnc, form.RewriteLAURL));
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

        private void toAnotherAzureMediaServicesAccountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyAssetToAnotherAMSAccount();
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
                    Task.Run(async () =>
                    {
                        streamingendpoint.CdnEnabled = enable;
                        await StreamingEndpointExecuteOperationAsync(streamingendpoint.SendUpdateOperationAsync, streamingendpoint, "updated");
                    });
                }
                else if (enable) // 0 scale unit and user wants to enable cdn
                {
                    Task.Run(async () =>
                    {
                        TextBoxLogWriteLine("Adding a streaming unit to enable Azure CDN...");
                        await ScaleStreamingEndpoint(streamingendpoint, 1);
                        streamingendpoint.CdnEnabled = enable;
                        await StreamingEndpointExecuteOperationAsync(streamingendpoint.SendUpdateOperationAsync, streamingendpoint, "updated");
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
            bool sestopped = (streamingendpoints.Count == 1 && streamingendpoints.FirstOrDefault().State == StreamingEndpointState.Stopped);
            bool sesingle = (streamingendpoints.Count == 1);

            disableAzureCDNToolStripMenuItem1.Enabled = sestopped && streamingendpoints.FirstOrDefault().CdnEnabled;
            enableAzureCDNToolStripMenuItem1.Enabled = sestopped && !streamingendpoints.FirstOrDefault().CdnEnabled;
            enableAzureCDNToolStripMenuItem1.Visible = sesingle && !streamingendpoints.FirstOrDefault().CdnEnabled;
            disableAzureCDNToolStripMenuItem1.Visible = sesingle && streamingendpoints.FirstOrDefault().CdnEnabled;
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
            DoCopyChannelInputURLToClipboard(false, true, true);

        }

        private void inputURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, false, true);
        }

        private void DoCopyChannelInputURLToClipboard(bool secondary = false, bool https = false, bool UI = true)
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
                    if (UI) MessageBox.Show("SSL is only possible for Smooth Streaming input.", "SSL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Error = true;
                }
            }

            if (!Error) System.Windows.Forms.Clipboard.SetText(absuri);


        }

        private void primaryInputURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, false, true);

        }

        private void secondaryInputURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(true, false, true);

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
            DoCopyChannelInputURLToClipboard(false, false, true);
        }

        private void inputSSLURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, true, true);

        }

        private void primaryInputURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(false, false, true);

        }

        private void secondaryInputURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoCopyChannelInputURLToClipboard(true, false, true);

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

            // channel info if only one channel
            ContextMenuItemChannelDisplayInfomation.Enabled = channels.Count == 1;

            // slate control if at least one channel with live transcoding
            ContextMenuItemChannelAdAndSlateControl.Enabled = channels.Any(c => c.Encoding != null);

            // copy input url if only one channel
            ContextMenuItemChannelCopyIngestURLToClipboard.Enabled = channels.Count == 1;

            // on premises encoder if only one channel
            ContextMenuItemChannelRunOnPremisesLiveEncoder.Enabled = channels.Count == 1;

            // copy preview url if only one channel
            ContextMenuItemChannelCopyPreviewURLToClipboard.Enabled = channels.Count == 1;
        }

        private void liveChannelToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            var channels = ReturnSelectedChannels();
            bool single = channels.Count == 1;

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


            ////////////

            var programs = ReturnSelectedPrograms();
            single = programs.Count == 1;

            // program info if only one program
            displayProgramInformationToolStripMenuItem.Enabled = single;

            // asset info if only one program
            ProgramDisplayRelatedAssetInformationToolStripMenuItem.Enabled = single;

        }

        private void contextMenuStripPrograms_Opening(object sender, CancelEventArgs e)
        {
            var programs = ReturnSelectedPrograms();
            bool single = programs.Count == 1;

            // program info if only one program
            ContextMenuItemProgramDisplayInformation.Enabled = single;

            // asset info if only one program
            ContextMenuItemProgramDisplayRelatedAssetInformation.Enabled = single;

            // copy program url if only one program
            ContextMenuItemProgramCopyTheOutputURLToClipboard.Enabled = single;

            // edit asset filter if only one program
            toolStripMenuItemProgramAssetFilterInfo.Enabled = single;

        }

        private void ContextMenuItemProgramCopyTheOutputURLToClipboard_Click(object sender, EventArgs e)
        {
            DoCopyOutputURLAssetOrProgramToClipboard();
        }

        private void azureMediaServicesSamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://github.com/AzureMediaServicesSamples");

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
            if (dataGridViewChannelsV.Initialized)
            {
                dataGridViewChannelsV.SearchInName = textBoxSearchNameChannel.Text;
                DoRefreshGridChannelV(false);
            }
        }

        private void comboBoxFilterTimeChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewChannelsV.TimeFilter = ((ComboBox)sender).SelectedItem.ToString();
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
            DynManifestFilter form = new DynManifestFilter(_contextdynmanifest, _context);

            if (form.ShowDialog() == DialogResult.OK)
            {

                var myfilter = form.GetFilter;
                try
                {
                    myfilter.Create();
                    TextBoxLogWriteLine("Global filter '{0}' created.", myfilter.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", myfilter.Name, true);
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
            Filter filter = ReturnSelectedFilters().FirstOrDefault();
            DynManifestFilter form = new DynManifestFilter(_contextdynmanifest, _context, filter);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Filter filtertoupdate = form.GetFilter;
                try
                {
                    filtertoupdate.Delete();
                    filtertoupdate.Create();
                    TextBoxLogWriteLine("Global filter '{0}' recreated.", filtertoupdate.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", filtertoupdate.Name, true);
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

            string taskname = "AME Standard encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;

            List<IMediaProcessor> Procs = GetMediaProcessorsByName(Constants.AzureMediaEncoderStandard);

            EncodingAMEStandard form = new EncodingAMEStandard(_context)
            {
                EncodingLabel = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected. " + SelectedAssets.Count + " jobs will be submitted." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be encoded.",
                EncodingProcessorsList = Procs,
                EncodingJobName = "Media Encoder Standard encoding of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + " - Media Encoder Standard encoded",
                EncodingAMEStdPresetXMLFilesUserFolder = Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder,
                EncodingAMEStdPresetXMLFilesFolder = Application.StartupPath + Constants.PathAMEStdFiles,
                SelectedAssets = SelectedAssets
            };



            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (IAsset asset in form.SelectedAssets)
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name);
                    IJob job = _context.Jobs.Create(jobnameloc, form.JobOptions.Priority);
                    string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvEncodername, form.EncodingProcessorSelected.Name + " v" + form.EncodingProcessorSelected.Version);
                    ITask AMEStandardTask = job.Tasks.AddNew(
                        tasknameloc,
                      form.EncodingProcessorSelected,// processor,
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
            toolStripMenuItemAssetInfo36.Enabled =
            assets.Count == 1;
        }

        private void dataGridViewTransfer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        public void DoLaunchAzureMediaPlayerWithFilter(object sender, System.EventArgs e)
        {
            // we launch AMP with a filter
            DoPlaySelectedAssetsOrProgramsWithPlayer(PlayerType.AzureMediaPlayer, sender.ToString());
        }

        public void DoDisplayFilter(object sender, System.EventArgs e)
        {
            IAsset myasset = ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault();
            var myassetfilters = _contextdynmanifest.ListAssetFilters(myasset);
            var myassetfilter = myassetfilters.Where(f => f.Name == sender.ToString()).FirstOrDefault();
            if (myassetfilter != null)
            {
                DynManifestFilter form = new DynManifestFilter(_contextdynmanifest, _context, (Filter)myassetfilter, myasset);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    AssetFilter filtertoupdate = (AssetFilter)form.GetFilter;
                    try
                    {
                        filtertoupdate.Delete();
                        filtertoupdate.Create();
                        TextBoxLogWriteLine("Asset filter '{0}' has been updated.", filtertoupdate.Name);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when updating asset filter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextBoxLogWriteLine("Error when updating asset filter '{0}'.", filtertoupdate.Name, true);
                        TextBoxLogWriteLine(ex);
                    }
                }
            }
        }


        private void AddFilterToAMPMenu(ToolStripMenuItem mytoolstripmenuitem, List<IAsset> ListAssets)
        {
            mytoolstripmenuitem.DropDownItems.Clear();
            ToolStripMenuItem SSMenuGF = new ToolStripMenuItem("with a global filter");
            mytoolstripmenuitem.DropDownItems.Add(SSMenuGF);
            ToolStripMenuItem SSMenuAF = new ToolStripMenuItem("with an asset filter");
            mytoolstripmenuitem.DropDownItems.Add(SSMenuAF);

            var Filters = _contextdynmanifest.ListGlobalFilters();
            if (Filters.Count > 0)
            {
                foreach (var filter in Filters)
                {
                    ToolStripMenuItem SSMenu = new ToolStripMenuItem(filter.Name, null, DoLaunchAzureMediaPlayerWithFilter);
                    SSMenuGF.DropDownItems.Add(SSMenu);
                }
            }
            if (ListAssets.Count == 1)
            {
                var AssetFilters = _contextdynmanifest.ListAssetFilters(ListAssets.FirstOrDefault());
                if (AssetFilters.Count > 0)
                {
                    foreach (var filter in AssetFilters)
                    {
                        ToolStripMenuItem SSMenu = new ToolStripMenuItem(filter.Name, null, DoLaunchAzureMediaPlayerWithFilter);
                        SSMenuAF.DropDownItems.Add(SSMenu);
                    }
                }
            }

        }


        private void AddAssetFilterInfoToMenu(ToolStripMenuItem mytoolstripmenuitem, IAsset asset)
        {
            mytoolstripmenuitem.DropDownItems.Clear();

            var Filters = _contextdynmanifest.ListAssetFilters(asset);
            if (Filters.Count > 0)
            {
                foreach (var filter in Filters)
                {
                    ToolStripMenuItem SSMenu = new ToolStripMenuItem(filter.Name, null, DoDisplayFilter);
                    mytoolstripmenuitem.DropDownItems.Add(SSMenu);
                }
            }
        }


        private void withAzureMediaPlayerToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            AddFilterToAMPMenu((ToolStripMenuItem)sender, ReturnSelectedAssets());
        }

        private void withAzureMediaPlayerToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {
            AddFilterToAMPMenu((ToolStripMenuItem)sender, ReturnSelectedAssetsFromProgramsOrAssets());
        }

        private void createAssetFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCreateAssetFilter();
        }

        private void DoCreateAssetFilter()
        {
            IAsset selasset = ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault();

            DynManifestFilter form = new DynManifestFilter(_contextdynmanifest, _context, null, selasset);

            if (form.ShowDialog() == DialogResult.OK)
            {
                AssetFilter myassetfilter = new AssetFilter(selasset);

                Filter filter = form.GetFilter;
                myassetfilter.Name = filter.Name;
                myassetfilter.PresentationTimeRange = filter.PresentationTimeRange;
                myassetfilter.Tracks = filter.Tracks;
                myassetfilter._context = filter._context;
                try
                {
                    myassetfilter.Create();
                    TextBoxLogWriteLine("Asset filter '{0}' created.", myassetfilter.Name);
                }
                catch (Exception e)
                {
                    TextBoxLogWriteLine("Error when creating filter '{0}'.", myassetfilter.Name, true);
                    TextBoxLogWriteLine(e);
                }
                DoRefreshGridFiltersV(false);
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
                Filter sourcefilter = filters.FirstOrDefault();

                string newfiltername = sourcefilter.Name + "Copy";
                if (Program.InputBox("New name", "Enter the name of the new duplicate filter:", ref newfiltername) == DialogResult.OK)
                {
                    Filter copyfilter = new Filter();
                    copyfilter.Name = newfiltername;
                    copyfilter.PresentationTimeRange = sourcefilter.PresentationTimeRange;
                    copyfilter.Tracks = sourcefilter.Tracks;
                    copyfilter._context = sourcefilter._context;
                    try
                    {
                        copyfilter.Create();
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
            AddFilterToAMPMenu((ToolStripMenuItem)sender, ReturnSelectedAssetsFromProgramsOrAssets());

        }

        private void assetFilterInfoupdateToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            AddAssetFilterInfoToMenu((ToolStripMenuItem)sender, ReturnSelectedAssets().FirstOrDefault());
        }

        private void toolStripMenuItemAssetInfo36_DropDownOpening(object sender, EventArgs e)
        {
            AddAssetFilterInfoToMenu((ToolStripMenuItem)sender, ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault());
        }

        private void toolStripMenuItemProgramAssetFilterInfo_DropDownOpening(object sender, EventArgs e)
        {
            AddAssetFilterInfoToMenu((ToolStripMenuItem)sender, ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault());
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            DoCreateFilter();
        }

        private void dataGridViewFilters_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // on line on two is blue
            Debug.Print("rowpostpaint" + e.RowIndex);
            if (e.RowIndex % 2 == 0)
            {
                foreach (DataGridViewCell c in ((DataGridView)sender).Rows[e.RowIndex].Cells) c.Style.BackColor = Color.AliceBlue;
            }

        }

        private void azureMediaPlayerDiagnosticsCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aka.ms/ampdiagnostics");
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
                        Task.Factory.StartNew(() => ProcessCloneProgramToAnotherAMSAccount(form.DestinationLoginCredentials, form.DestinationStorageAccount, program, form.CopyDynEnc, form.RewriteLAURL, form.CloneLocators));
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
                        //int index = DoGridTransferAddItem(string.Format("Copy asset '{0}' to account '{1}'", asset.Name, form.DestinationLoginCredentials.AccountName), TransferType.ExportToOtherAMSAccount, Properties.Settings.Default.useTransferQueue);
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
                Subclipping form = new Subclipping(_context, _contextdynmanifest, selectedAssets, this)
                {
                    EncodingJobName = "Subclipping of " + Constants.NameconvInputasset,
                    EncodingOutputAssetName = Constants.NameconvInputasset + " - Subclipped"
                };

                form.ShowDialog();

                //if (form.Show() == DialogResult.OK)
                //{
                //    var subclipConfig = form.GetSubclippingConfiguration();

                //    if (subclipConfig.Reencode) // reencode the clip
                //    {
                //        List<IMediaProcessor> Procs = GetMediaProcessorsByName(Constants.AzureMediaEncoderStandard);
                //        EncodingAMEStandard form2 = new EncodingAMEStandard(_context, subclipConfig)
                //        {
                //            EncodingLabel = (selectedAssets.Count > 1) ? selectedAssets.Count + " assets have been selected. " + selectedAssets.Count + " jobs will be submitted." : "Asset '" + selectedAssets.FirstOrDefault().Name + "' will be encoded.",
                //            EncodingProcessorsList = Procs,
                //            EncodingJobName = "Subclipping with reencoding of " + Constants.NameconvInputasset,
                //            EncodingOutputAssetName = Constants.NameconvInputasset + "- Subclipped with reencoding",
                //            EncodingAMEStdPresetXMLFilesUserFolder = Properties.Settings.Default.AMEStandardPresetXMLFilesCurrentFolder,
                //            EncodingAMEStdPresetXMLFilesFolder = Application.StartupPath + Constants.PathAMEStdFiles,
                //            SelectedAssets = selectedAssets
                //        };

                //        if (form2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //        {
                //            string taskname = "Subclipping with reencoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
                //            LaunchJobs(
                //               form2.EncodingProcessorSelected,
                //               selectedAssets,
                //               form2.EncodingJobName,
                //               form2.JobOptions.Priority,
                //               taskname,
                //               form2.EncodingOutputAssetName,
                //               new List<string>() { form2.EncodingConfiguration },
                //               form2.JobOptions.OutputAssetsCreationOptions,
                //               form2.JobOptions.TasksOptionsSetting,
                //               form2.JobOptions.StorageSelected);
                //        }
                //    }
                //    else if (subclipConfig.CreateAssetFilter) // create a asset filter
                //    {
                //        IAsset selasset = selectedAssets.FirstOrDefault();
                //        DynManifestFilter formAF = new DynManifestFilter(_contextdynmanifest, _context, null, selasset, subclipConfig);

                //        if (formAF.ShowDialog() == DialogResult.OK)
                //        {
                //            AssetFilter myassetfilter = new AssetFilter(selasset);

                //            Filter filter = formAF.GetFilter;
                //            myassetfilter.Name = filter.Name;
                //            myassetfilter.PresentationTimeRange = filter.PresentationTimeRange;
                //            myassetfilter.Tracks = filter.Tracks;
                //            myassetfilter._context = filter._context;
                //            try
                //            {
                //                myassetfilter.Create();
                //                TextBoxLogWriteLine("Asset filter '{0}' created.", myassetfilter.Name);
                //            }
                //            catch (Exception e)
                //            {
                //                TextBoxLogWriteLine("Error when creating filter '{0}'.", myassetfilter.Name, true);
                //                TextBoxLogWriteLine(e);
                //            }
                //            DoRefreshGridFiltersV(false);
                //        }

                //    }
                //    else // no reencode or asset filter but stream copy
                //    {
                //        string taskname = "Subclipping of " + Constants.NameconvInputasset + " with " + Constants.NameconvEncodername;
                //        IMediaProcessor Proc = GetLatestMediaProcessorByName(Constants.AzureMediaEncoderStandard);

                //        LaunchJobs(
                //            Proc,
                //            selectedAssets,
                //            form.EncodingJobName,
                //            form.JobOptions.Priority,
                //            taskname,
                //            form.EncodingOutputAssetName,
                //            new List<string>() { form.GetSubclippingConfiguration().Configuration },
                //            form.JobOptions.OutputAssetsCreationOptions,
                //            form.JobOptions.TasksOptionsSetting,
                //            form.JobOptions.StorageSelected);
                //    }
                
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
    }
}



namespace AMSExplorer
{
    /// <summary>
    /// A DataGridViewColumn implementation that provides a column that
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
        public const string NotPublished = "Not published";
        public const string Streamable = "Streamable";
        public const string CENC = "CENC static";
        public const string Envelope = "Envelope static";
        public const string Storage = "Storage encrypted";
        public const string SupportDynEnc = "Support dyn. encryption";
        public const string DynEnc = "Dynamic encrypted";
        public const string NotEncrypted = "Not encrypted";
        public const string Empty = "Empty";
        public const string DefaultStorage = "Default storage";
        public const string NotDefaultStorage = "Not default storage";
    }

    public static class FilterTime
    {
        public const string First50Items = "First 50 items";
        public const string LastDay = "Last 24 hours";
        public const string LastWeek = "Last week";
        public const string LastMonth = "Last month";
        public const string LastYear = "Last year";
        public const string All = "All";

        public static int ReturnNumberOfDays(string timeFilter)
        {
            int days = -1;
            if (timeFilter != string.Empty && timeFilter != null && timeFilter != FilterTime.All)
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

                    default:
                        break;
                }
            }
            return days;
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
        UploadFromFolder = 1,
        ImportFromAzureStorage = 2,
        ImportFromHttp = 3,
        ExportToOtherAMSAccount = 4,
        ExportToAzureStorage = 5,
        DownloadToLocal = 6
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
        static private int _assetsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static private bool _neveranalyzed = true;
        static private string _searchinname = "";
        static private string _statefilter = "";
        static private string _timefilter = FilterTime.First50Items;

        static string _orderassets = OrderAssets.LastModifiedDescending;
        static BackgroundWorker WorkerAnalyzeAssets;
        static CloudMediaContext _context;
        static MediaServiceContextForDynManifest _contextDynManifest;
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
        static Bitmap Redstreamimage = MakeRed(Streaminglocatorimage);
        static Bitmap Reddownloadimage = MakeRed(SASlocatorimage);
        static Bitmap Bluestreamimage = MakeBlue(Streaminglocatorimage);
        static Bitmap Bluedownloadimage = MakeBlue(SASlocatorimage);

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
        public string SearchInName
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
        public int DisplayedCount
        {
            get
            {
                return _MyObservAsset.Count();
            }
        }


        public void Init(CloudMediaContext context, MediaServiceContextForDynManifest contextDynManifest)
        {
            Debug.WriteLine("AssetsInit");

            IEnumerable<AssetEntry> assetquery;
            _context = context;
            _contextDynManifest = contextDynManifest;

            assetquery = from a in context.Assets orderby a.LastModified descending select new AssetEntry { Name = a.Name, Id = a.Id, LastModified = ((DateTime)a.LastModified).ToLocalTime(), Storage = a.StorageAccountName };

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
            IAsset asset;

            PublishStatus SASLoc;
            PublishStatus OrigLoc;
            int i = 0;

            foreach (AssetEntry AE in _MyObservAsset)
            {
                asset = null;
                try
                {
                    asset = _context.Assets.Where(a => a.Id == AE.Id).FirstOrDefault();
                    if (asset != null)
                    {
                        AssetInfo myAssetInfo = new AssetInfo(asset);

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
                        AE.LocatorExpirationDate = LocDate;
                        AE.LocatorExpirationDateWarning = (LocDate < DateTime.Now);
                        assetBitmapAndText = BuildBitmapAssetFilters(asset);
                        AE.Filters = assetBitmapAndText.bitmap;
                        AE.FiltersMouseOver = assetBitmapAndText.MouseOverDesc;
                        i++;
                        if (i % 5 == 0)
                        {
                            this.BeginInvoke(new Action(() => this.Refresh()), null);
                        }
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




        public void RefreshAssets(CloudMediaContext context, int pagetodisplay) // all assets are refreshed
        {
            if (!_initialized) return;
            Debug.WriteLine("RefreshAssets");

            if (WorkerAnalyzeAssets.IsBusy)
            {
                // cancel the analyze.
                WorkerAnalyzeAssets.CancelAsync();
            }
            this.FindForm().Cursor = Cursors.WaitCursor;

            IEnumerable<IAsset> assets;
            IEnumerable<AssetEntry> assetquery;

            int days = FilterTime.ReturnNumberOfDays(_timefilter);
            assets = (days == -1) ? context.Assets : context.Assets.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)))));

            // search
            if (!string.IsNullOrEmpty(_searchinname))
            {
                //string searchlower = _searchinname.ToLower();
                //assets = assets.Where(a => (a.Name.ToLower().Contains(searchlower) || a.Id.ToLower().Contains(searchlower)));
                assets = assets.Where(a =>
                    (a.Name.IndexOf(_searchinname, StringComparison.OrdinalIgnoreCase) != -1)  // for no case sensitive
                    ||
                    (a.Id.IndexOf(_searchinname, StringComparison.OrdinalIgnoreCase) != -1)
                    // ||
                    //(a.AssetFiles.ToList().Any(f => f.Name.IndexOf(_searchinname, StringComparison.OrdinalIgnoreCase) != -1))
                );


            }

            if ((!string.IsNullOrEmpty(_statefilter)) && _statefilter != StatusAssets.All)
            {
                switch (_statefilter)
                {
                    case StatusAssets.Published:
                        assets = assets.Where(a => a.Locators.Any());
                        break;
                    case StatusAssets.PublishedExpired:
                        assets = assets.Where(a => a.Locators.Any(l => l.ExpirationDateTime < DateTime.UtcNow));
                        break;
                    case StatusAssets.NotPublished:
                        assets = assets.Where(a => a.Locators.Count == 0);
                        break;
                    case StatusAssets.Storage:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.StorageEncrypted);
                        break;
                    case StatusAssets.CENC:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.CommonEncryptionProtected);
                        break;
                    case StatusAssets.Envelope:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.EnvelopeEncryptionProtected);
                        break;
                    case StatusAssets.NotEncrypted:
                        assets = assets.Where(a => a.Options == AssetCreationOptions.None);
                        break;
                    case StatusAssets.DynEnc:
                        assets = assets.Where(a => a.DeliveryPolicies.Any());
                        break;
                    case StatusAssets.Streamable:
                        assets = assets.Where(a => a.IsStreamable);
                        break;
                    case StatusAssets.SupportDynEnc:
                        assets = assets.Where(a => a.SupportsDynamicEncryption);
                        break;
                    case StatusAssets.Empty:
                        assets = assets.Where(a => a.AssetFiles.Count() == 0);
                        break;
                    case StatusAssets.DefaultStorage:
                        assets = assets.Where(a => a.StorageAccountName == _context.DefaultStorageAccount.Name);
                        break;
                    case StatusAssets.NotDefaultStorage:
                        assets = assets.Where(a => a.StorageAccountName != _context.DefaultStorageAccount.Name);
                        break;
                    default:
                        break;
                }
            }

            var size = new Func<IAsset, long>(AssetInfo.GetSize);

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


            if ((!string.IsNullOrEmpty(_timefilter)) && _timefilter == FilterTime.First50Items)
            {
                assets = assets.Take(50);
            }

            _context = context;
            _pagecount = (int)Math.Ceiling(((double)assets.Count()) / ((double)_assetsperpage));
            if (_pagecount == 0) _pagecount = 1; // no asset but one page

            if (pagetodisplay < 1) pagetodisplay = 1;
            if (pagetodisplay > _pagecount) pagetodisplay = _pagecount;
            _currentpage = pagetodisplay;

            try
            {
                assetquery = from a in assets
                             select new AssetEntry { Name = a.Name, Id = a.Id, Type = null, LastModified = ((DateTime)a.LastModified).ToLocalTime(), Storage = a.StorageAccountName };
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

        public static Bitmap MakeRed(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(originalColor.A, 255, originalColor.G, originalColor.B));
                }
            }

            return newBitmap;
        }

        public static Bitmap MakeBlue(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, Color.FromArgb(originalColor.A, originalColor.R, originalColor.G, 255));
                }
            }

            return newBitmap;
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

                                break;
                        }
                        break;


                    default:
                        break;
                }

                returnedImage = AddBitmap(returnedImage, newbitmap);
                returnedText += !string.IsNullOrEmpty(newtext) ? newtext + Constants.endline : string.Empty;

            }


            AssetBitmapAndText ABT = new AssetBitmapAndText()
            {
                bitmap = returnedImage,
                MouseOverDesc = returnedText ?? "Not published"

            };

            return ABT;
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
            AssetBitmapAndText ABT = new AssetBitmapAndText();
            var filters = _contextDynManifest.ListAssetFilters(asset);

            if (filters.Count > 1)
            {
                ABT.bitmap = AssetFiltersImage;
                ABT.MouseOverDesc = string.Format("{0} filters", filters.Count);
            }
            else if (filters.Count == 1)
            {
                ABT.bitmap = AssetFilterImage;
                ABT.MouseOverDesc = string.Format("1 filter");
            }
            return ABT;
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
        public string SearchInName
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
        public int DisplayedCount
        {
            get
            {
                return _MyObservJob.Count();
            }

        }
        public IEnumerable<IJob> DisplayedJobs
        {
            get
            {
                return jobs;
            }

        }


        static BindingList<JobEntry> _MyObservJob;
        static BindingList<JobEntry> _MyObservAssethisPage;

        static IEnumerable<IJob> jobs;
        static List<string> _MyListJobsMonitored = new List<string>(); // List of jobds monitored. It contains the jobs ids. Used when a new job is discovered (created by another program) to activate the display of job progress

        static private int _jobsperpage = 50; //nb of items per page
        static private int _pagecount = 1;
        static private int _currentpage = 1;
        static private bool _initialized = false;
        static private bool _refreshedatleastonetime = false;
        static string _orderjobs = OrderJobs.LastModifiedDescending;
        static string _filterjobsstate = "All";
        static CloudMediaContext _context;
        static private CredentialsEntry _credentials;
        static private string _searchinname = "";
        static private string _timefilter = FilterTime.LastWeek;

        public void Init(CredentialsEntry credentials, CloudMediaContext context)
        {
            IEnumerable<JobEntry> jobquery;
            _credentials = credentials;

            _context = context;// Program.ConnectAndGetNewContext(_credentials);
            jobquery = from j in _context.Jobs
                       orderby j.LastModified descending
                       select new JobEntry
                           {
                               Name = j.Name,
                               Id = j.Id,
                               Tasks = j.Tasks.Count,
                               Priority = j.Priority,
                               State = j.State,
                               StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                               EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
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

            this.FindForm().Cursor = Cursors.WaitCursor;
            _context = context;

            IEnumerable<JobEntry> jobquery;

            int days = FilterTime.ReturnNumberOfDays(_timefilter);
            jobs = (days == -1) ? context.Jobs : context.Jobs.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)))));

            if (_filterjobsstate != "All")
            {
                jobs = jobs.Where(j => j.State == (JobState)Enum.Parse(typeof(JobState), _filterjobsstate));
            }

            if (!string.IsNullOrEmpty(_searchinname))
            {
                string searchlower = _searchinname.ToLower();
                jobs = jobs.Where(j => (j.Name.ToLower().Contains(searchlower) || j.Id.ToLower().Contains(searchlower)));
            }

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

            if ((!string.IsNullOrEmpty(_timefilter)) && _timefilter == FilterTime.First50Items)
            {
                jobs = jobs.Take(50);
            }

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
                               StartTime = j.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)j.StartTime).ToLocalTime() : null,
                               EndTime = j.EndTime.HasValue ? ((DateTime)j.EndTime).ToLocalTime().ToString() : null,
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
            this.FindForm().Cursor = Cursors.Default;
        }

        // Used to restore job progress. 2 cases: when app is launched or when a job has been created by an external program
        public void RestoreJobProgress()  // when app is launched for example, we want to restore job progress updates
        {
            Task.Run(() =>
           {
               IEnumerable<IJob> ActiveJobs = _context.Jobs.Where(j => (j.State == JobState.Queued) || (j.State == JobState.Scheduled) || (j.State == JobState.Processing));

               foreach (IJob job in ActiveJobs)
               {
                   if (!_MyListJobsMonitored.Contains(job.Id))
                   {
                       _MyListJobsMonitored.Add(job.Id);
                       this.DoJobProgress(job);
                   }

               }
           });
        }



        public void DoJobProgress(IJob job)
        {
            Task.Run(() =>
            {
                try
                {
                    job = job.StartExecutionProgressTask(
                       j =>
                       {
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
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)JobRefreshed.StartTime).ToLocalTime() : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString() : null;

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

                                       ETAstr = "Estimated: " + ETA.ToString();
                                       Durationstr = "Estimated: " + estimatedduration.ToString(@"d\.hh\:mm\:ss");
                                       _MyObservJob[index].EndTime = ETA.ToString(@"g") + " ?";
                                       _MyObservJob[index].Duration = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString() : estimatedduration.ToString(@"d\.hh\:mm\:ss") + " ?";
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
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)JobRefreshed.StartTime).ToLocalTime() : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString() : null;
                                   _MyObservJob[index].State = JobRefreshed.State;

                                   if (_MyListJobsMonitored.Contains(JobRefreshed.Id)) // we want to display only one time
                                   {
                                       _MyListJobsMonitored.Remove(JobRefreshed.Id); // let's remove from the list of monitored jobs
                                       Mainform myform = (Mainform)this.FindForm();
                                       string status = Enum.GetName(typeof(JobState), JobRefreshed.State).ToLower();

                                       myform.BeginInvoke(new Action(() =>
                                       {
                                           myform.Notify(string.Format("Job {0}", status), string.Format("Job {0}", _MyObservJob[index].Name), JobRefreshed.State == JobState.Error);
                                       }));
                                       Debug.WriteLine("Job", string.Format("Job {0} {1}", _MyObservJob[index].Name, _MyObservJob[index].State));

                                       this.BeginInvoke(new Action(() =>
                                       {
                                           this.Refresh();
                                       }));

                                       myform.BeginInvoke(new Action(() =>
                                       {
                                           myform.DoRefreshGridAssetV(false);
                                       }));
                                   }
                               }
                           }
                       },
                       CancellationToken.None).Result;

                }
                catch (Exception e)
                {
                    MessageBox.Show(Program.GetErrorMessage(e), "Job Monitoring Error");
                }
            });
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