//----------------------------------------------------------------------- 
// <copyright file="Mainform.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.1
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//  
// http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// </license> 

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


namespace AMSExplorer
{

    public partial class Mainform : Form
    {
        // XML Congiguration files path.
        public static string _configurationXMLFiles;
        private static string _HelpFiles;
        public static CredentialsEntry _credentials;
        public static bool havestoragecredentials = true;
        // Field for service context.
        public static CloudMediaContext _context = null;
        public static string Salt;
        private string _backuprootfolderupload = "";
        private StringBuilder sbuilder = new StringBuilder(); // used for locator copy to clipboard
        private BindingList<TransferEntry> _MyListTransfer; // list of upload/download
        private List<int> _MyListTransferQueue; // List of transfers in the queue. It contains the index in the order of schedule
        private ILocator PlayBackLocator = null;
        //Watch folder vars
        private Dictionary<string, DateTime> seen = new Dictionary<string, DateTime>();
        private TimeSpan seenInterval = new TimeSpan();
        private string WatchFolderFolderPath = string.Empty;
        private bool WatchFolderIsOn = false;
        private bool WatchFolderDeleteFile = false;
        private IJobTemplate WatchFolderJobTemplate = null;
        FileSystemWatcher WatchFolderWatcher;
        private bool AMEZeniumPresent = true;

        private System.Timers.Timer TimerAutoRefresh;

        bool DisplaySplashDuringLoading;

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

            if ((Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder)))
            {
                Properties.Settings.Default.WAMEPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathAMEFiles;
            }

            if ((Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder == string.Empty) || (!Directory.Exists(Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder)))
            {
                Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder = Application.StartupPath + Constants.PathPremiumWorkflowFiles;
            }
            Program.SaveAndProtectUserConfig(); // to save settings and to make sure they are encrypted if the user just upgraded from an earlier version



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
            toolStripStatusLabelConnection.Text = String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version) + " - Connected to " + _context.Credentials.ClientId;

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
                AMEZeniumPresent = false;
                encodeAssetWithZeniumToolStripMenuItem.Enabled = false;  //menu
                encodeAssetWithZeniumToolStripMenuItem.Visible = false;
                ContextMenuItemZenium.Enabled = false; // mouse context menu
                ContextMenuItemZenium.Visible = false;
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
            if ((_context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits == 0) && (_context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType != ReservedUnitType.Basic))
                TextBoxLogWriteLine("There is no reserved encoding unit (encoding will use a shared pool) but unit type is not set to BASIC.", true); // Warning

            ApplySettingsOptions(true);
        }



        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            DoRefresh();
        }



        private void ProcessImportFromHttp(Uri ObjectUrl, string assetname, string fileName, int index)
        {
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
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
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



        //delete all assets except those specified or published or Zenium Blueprint
        void DeleteAllAssets(string[] exceptionAssetNames)
        {
            List<string> objList_string = new List<string>();
            foreach (string assetName in exceptionAssetNames)
            {
                objList_string.Add(assetName);
            }
            foreach (IAsset objIAsset in _context.Assets)
            {
                if (!objList_string.Contains(objIAsset.Name))
                {
                    try
                    {
                        DeleteLocatorsForAsset(objIAsset);
                        objIAsset.Delete();
                    }
                    catch (Exception e)
                    {
                        TextBoxLogWriteLine("Error when deleting asset '{0}'.", objIAsset.Name);
                        TextBoxLogWriteLine(e);
                    }

                }
            }
            TextBoxLogWriteLine("DeleteAllAssets() completed.");
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
                SetISMFileAsPrimary(asset);
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


        private static IMediaProcessor GetLatestMediaProcessorByName(string mediaProcessorName)
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

        private static List<IMediaProcessor> GetMediaProcessorsByName(string mediaProcessorName)
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

        /*
        static IAsset GetAsset(string assetId)
        {
            IAsset asset;

            try
            {
                // Use a LINQ Select query to get an asset.
                var assetInstance =
                    from a in _context.Assets
                    where a.Id == assetId
                    select a;
                // Reference the asset as an IAsset.
                asset = assetInstance.FirstOrDefault();
            }
            catch
            {

                asset = null;
            }

            return asset;
        }
         * */

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


        static void SetISMFileAsPrimary(IAsset asset)
        {
            var ismAssetFiles = asset.AssetFiles.ToList().
                Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase)).ToArray();

            if (ismAssetFiles.Count() == 0)
                return;

            ismAssetFiles.First().IsPrimary = true;
            ismAssetFiles.First().Update();
        }

        static void SetISMFileAsPrimary(IAsset asset, string assetfilename)
        {
            var ismAssetFiles = asset.AssetFiles.ToList().
                Where(f => f.Name.Equals(assetfilename, StringComparison.OrdinalIgnoreCase)).ToArray();

            if (ismAssetFiles.Count() != 1)
                return;

            ismAssetFiles.First().IsPrimary = true;
            ismAssetFiles.First().Update();
        }


        static void TextBoxAddLine(TextBox TB, string ST)
        {
            TB.Text += ST + "\r\n";
            TB.Refresh();
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
            TextBoxLogWriteLine(e.Message);
            if (e.InnerException != null)
            {
                TextBoxLogWriteLine(Program.GetErrorMessage(e), true);
            }
        }

        public void TextBoxLogWriteLine(string text, bool Error = false)
        {

            text += Environment.NewLine;

            if (richTextBoxLog.InvokeRequired)
            {
                richTextBoxLog.BeginInvoke(new Action(() =>
                {
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
        }

        private void DoRefreshGridAssetV(bool firstime)
        {
            if (firstime)
            {

                dataGridViewAssetsV.Init(_context);
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
                dataGridViewJobsV.Init(_credentials);
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
                    Task.Factory.StartNew(() => ProcessUploadFile(file, index, false, null));
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




        private void ProcessUploadFile(object name, int index, bool bdeletefile, IJobTemplate jobtemplatetorun, string storageaccount = null)
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
            }
            catch (Exception e)
            {
                Error = true;
                DoGridTransferDeclareError(index, e);
                TextBoxLogWriteLine("Error when uploading '{0}'", name, true);
            }
            if (!Error)
            {
                TextBoxLogWriteLine(string.Format("Uploading of {0} done.", name));
                DoGridTransferDeclareCompleted(index, asset.Id);
                if (bdeletefile) //use checked the box "delete the file"
                {
                    try
                    {
                        File.Delete(name as string);
                        TextBoxLogWriteLine("File '{0}' deleted.", name);
                    }
                    catch
                    {
                        TextBoxLogWriteLine("Error when deleting '{0}'", name, true);
                    }
                }

                if (jobtemplatetorun != null) // option with watchfolder to run a job based on a job template
                {
                    string jobname = string.Format("Processing of {0} with template {1}", asset.Name, jobtemplatetorun.Name);
                    List<IAsset> assetlist = new List<IAsset>() { asset };

                    TextBoxLogWriteLine(string.Format("Submitting job '{0}'", jobname));
                    // Submit the job
                    IJob job = _context.Jobs.Create(jobname, jobtemplatetorun, assetlist, Properties.Settings.Default.DefaultJobPriority);

                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There has been a problem when submitting the job '{0}'", job.Name, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }

                    DoRefreshGridJobV(false);
                }
            }
            DoRefreshGridAssetV(false);
        }




        private void DoGridTransferUpdateText(string progresstext, int index)
        {
            _MyListTransfer[index].Name = progresstext;
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }
        private void DoGridTransferUpdateProgress(double progress, int index)
        {
            _MyListTransfer[index].Progress = progress;
            if (progress > 3 && _MyListTransfer[index].StartTime != null)
            {
                TimeSpan interval = (TimeSpan)(DateTime.UtcNow - ((DateTime)_MyListTransfer[index].StartTime).ToUniversalTime());
                DateTime ETA = DateTime.UtcNow.AddSeconds((100d / progress - 1d) * interval.TotalSeconds);
                _MyListTransfer[index].EndTime = ETA.ToLocalTime().ToString() + " ?";
            }

            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private void DoGridTransferDeclareCompleted(int index, string DestLocation)  // Process is completed
        {
            _MyListTransfer[index].Progress = 100;
            _MyListTransfer[index].State = TransferState.Finished;
            _MyListTransfer[index].EndTime = DateTime.Now.ToString();
            _MyListTransfer[index].DestLocation = DestLocation;
            if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }
        private void DoGridTransferDeclareError(int index, Exception e)  // Process is completed
        {
            string message = e.Message;
            if (e.InnerException != null)
            {
                message = message + Constants.endline + Program.GetErrorMessage(e);
            }
            DoGridTransferDeclareError(index, message);
        }

        private void DoGridTransferDeclareError(int index, string ErrorDesc = "")  // Process is completed
        {
            _MyListTransfer[index].Progress = 100;
            _MyListTransfer[index].EndTime = DateTime.Now.ToString();
            _MyListTransfer[index].State = TransferState.Error;
            _MyListTransfer[index].ErrorDescription = ErrorDesc;
            if (DoGridTransferIsQueueRequested(index)) _MyListTransferQueue.Remove(index);
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private void DoGridTransferDeclareTransferStarted(int index)  // Process is started
        {
            _MyListTransfer[index].Progress = 0;
            _MyListTransfer[index].State = TransferState.Processing;
            _MyListTransfer[index].StartTime = DateTime.Now;
            dataGridViewTransfer.BeginInvoke(new Action(() => dataGridViewTransfer.Refresh()), null);
        }

        private bool DoGridTransferQueueOurTurn(int index)  // Return true if this is out turn
        {
            return (_MyListTransferQueue.Count > 0) ? (_MyListTransferQueue[0] == index) : true;
        }

        private bool DoGridTransferIsQueueRequested(int index)  // Return true trasfer is managed in the queue
        {
            return (_MyListTransfer[index].processedinqueue);
        }

        private void DoGridTransferWaitIfNeeded(int index)
        {
            // If upload in the queue, let's wait our turn
            if (DoGridTransferIsQueueRequested(index))
            {
                while (!DoGridTransferQueueOurTurn(index))
                {
                    Thread.Sleep(500);
                }

                DoGridTransferDeclareTransferStarted(index);
            }
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

            MessageBox.Show("Download process has been initiated. See the Transfers tab to check the progress.");
        }



        private void fromMultipleFilesToolStripMenuItem_Click(object sender, EventArgs e) // upload from multiple files
        {
            DoMenuUploadFromFolder_Step1();
        }

        private void DoMenuUploadFromFolder_Step1()
        {
            FolderBrowserDialog openFolderDialog1 = new FolderBrowserDialog();

            string name;
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
                MessageBox.Show("Error: Could not read file from disk. Original error: " + Constants.endline + ex.Message);
                TextBoxLogWriteLine("Error: Could not read file or folder '{0}' from disk.", SelectedPath, true);
                TextBoxLogWriteLine(ex);
            }
        }


        private void DoMenuImportFromHttp()
        {
            string valuekey = "";

            if (!havestoragecredentials)
            { // No blob credentials. Let's ask the user

                if (InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
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
                    int index = DoGridTransferAddItem(string.Format("Import from Http of '{0}'", form.GetAssetFileName), TransferType.ImportFromHttp, false);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessImportFromHttp(form.GetURL, form.GetAssetName, form.GetAssetFileName, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
        }



        private async void DoMergeAssetsToNewAsset()
        {
            IList<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count > 0)
            {
                if (SelectedAssets.Any(a => a.Options != AssetCreationOptions.None))
                {
                    MessageBox.Show("Assets cannot be merged as at least one asset is encrypted.", "Asset encrypted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string newassetname = string.Empty;
                    if (InputBox("Assets merging", "Enter the new asset name:", ref newassetname) == DialogResult.OK)
                    {

                        if (!havestoragecredentials)
                        { // No blob credentials.
                            MessageBox.Show("Please specifiy the account storage key in the login window to access this feature.");
                        }
                        else
                        {
                            await Task.Factory.StartNew(() => ProcessMergeAssetsInNewAsset(SelectedAssets, newassetname));
                            // Refresh the assets.
                            DoRefreshGridAssetV(false);
                        }
                    }
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


        private void ProcessMergeAssetsInNewAsset(IList<IAsset> MyAssets, string newassetname)
        {
            bool Error = false;
            try
            {
                TextBoxLogWriteLine("Merging assets to new asset '{0}'...", newassetname);
                IAsset NewAsset = _context.Assets.Create(newassetname, AssetCreationOptions.None); // No encryption as we do storage copy
                CloudStorageAccount storageAccount;
                storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, Mainform._credentials.StorageKey), true);
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                IAccessPolicy writePolicy = _context.AccessPolicies.Create("writePolicy", TimeSpan.FromDays(1), AccessPermissions.Write);
                IAccessPolicy readPolicy = _context.AccessPolicies.Create("readPolicy", TimeSpan.FromDays(1), AccessPermissions.Read);

                ILocator destinationLocator = _context.Locators.CreateLocator(LocatorType.Sas, NewAsset, writePolicy);
                Uri uploadUri = new Uri(destinationLocator.Path);

                // let's backup the primary file from the first asset to set it to the merged asset
                var ismAssetFile = MyAssets.FirstOrDefault().AssetFiles.ToList().Where(f => f.IsPrimary).ToArray();

                foreach (IAsset MyAsset in MyAssets)
                {
                    if (MyAsset.StorageAccountName == _context.DefaultStorageAccount.Name) // asset is in default storage
                    {
                        ILocator sourceLocator = _context.Locators.CreateLocator(LocatorType.Sas, MyAsset, readPolicy);
                        Uri SourceUri = new Uri(sourceLocator.Path);
                        foreach (IAssetFile MyAssetFile in MyAsset.AssetFiles)
                        {
                            TextBoxLogWriteLine("   Copying file '{0}' from asset '{1}'...", MyAssetFile.Name, MyAsset.Name);
                            if (MyAssetFile.IsEncrypted)
                            {
                                TextBoxLogWriteLine("   Cannot copy file '{0}' because it is encrypted.", MyAssetFile.Name, true);
                            }
                            else
                            {
                                IAssetFile AssetFileTarget = NewAsset.AssetFiles.Where(f => f.Name == MyAssetFile.Name).FirstOrDefault();
                                if (AssetFileTarget == null)
                                {
                                    AssetFileTarget = NewAsset.AssetFiles.Create(MyAssetFile.Name); // does not exist so we create it
                                }
                                else
                                {
                                    int i = 0;
                                    while (NewAsset.AssetFiles.Where(f => f.Name == Path.GetFileNameWithoutExtension(MyAssetFile.Name) + "#" + i.ToString() + Path.GetExtension(MyAssetFile.Name)).FirstOrDefault() != null)
                                    {
                                        i++;
                                    }
                                    AssetFileTarget = NewAsset.AssetFiles.Create(Path.GetFileNameWithoutExtension(MyAssetFile.Name) + "#" + i.ToString() + Path.GetExtension(MyAssetFile.Name));// exist so we add a number
                                }

                                // Get the asset container URI and copy blobs from mediaContainer to assetContainer.
                                string sourceTargetContainerName = SourceUri.Segments[1];
                                string assetTargetContainerName = uploadUri.Segments[1];
                                CloudBlobContainer mediaBlobContainer = cloudBlobClient.GetContainerReference(sourceTargetContainerName);
                                CloudBlobContainer assetTargetContainer = cloudBlobClient.GetContainerReference(assetTargetContainerName);
                                CloudBlockBlob sourceCloudBlob, destinationBlob;
                                sourceCloudBlob = mediaBlobContainer.GetBlockBlobReference(MyAssetFile.Name);
                                sourceCloudBlob.FetchAttributes();

                                if (sourceCloudBlob.Properties.Length > 0)
                                {

                                    destinationBlob = assetTargetContainer.GetBlockBlobReference(AssetFileTarget.Name);

                                    destinationBlob.DeleteIfExists();
                                    destinationBlob.StartCopyFromBlob(sourceCloudBlob);

                                    CloudBlockBlob blob;
                                    blob = (CloudBlockBlob)assetTargetContainer.GetBlobReferenceFromServer(AssetFileTarget.Name);

                                    while (blob.CopyState.Status == CopyStatus.Pending)
                                    {
                                        Task.Delay(TimeSpan.FromSeconds(1d)).Wait();
                                    }
                                    destinationBlob.FetchAttributes();
                                    AssetFileTarget.ContentFileSize = sourceCloudBlob.Properties.Length;
                                    AssetFileTarget.Update();
                                    TextBoxLogWriteLine("     File '{0}' copied as '{1}'...", MyAssetFile.Name, AssetFileTarget.Name);
                                    //MyAsset.Update();
                                }
                            }
                        }
                        sourceLocator.Delete();
                    }
                    else // asset in not in the default storage
                    {
                        TextBoxLogWriteLine("Asset '{0}' has been ignored as this asset is not in the default storage account.", MyAsset.Name, true);
                    }
                }
                destinationLocator.Delete();
                readPolicy.Delete();
                writePolicy.Delete();
                if (ismAssetFile.Count() > 0)
                {
                    SetISMFileAsPrimary(NewAsset, ismAssetFile.FirstOrDefault().Name);
                }
                else
                {
                    SetISMFileAsPrimary(NewAsset);
                }

            }
            catch
            {
                MessageBox.Show("Error when merging the assets.");
                TextBoxLogWriteLine("Error when merging the assets.", true);
                Error = true;
            }
            if (!Error) TextBoxLogWriteLine("Assets merged to new asset '{0}'.", newassetname);
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

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Button buttonOk = new Button()
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                };

            Button buttonCancel = new Button()
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                };


            Form form = new Form()
            {
                ClientSize = new Size(396, 107),
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                AcceptButton = buttonOk,
                CancelButton = buttonCancel,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            Label label = new Label()
            {
                AutoSize = true,
                Text = promptText
            };
            TextBox textBox = new TextBox()
            {
                Text = value
            };

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }



        public DialogResult DisplayInfo(IAsset asset)
        {
            AssetInformation form = new AssetInformation(this)
                {
                    MyAsset = asset,
                    MyContext = _context,
                    MyStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints // we want to keep the same sorting

                };

            DialogResult dialogResult = form.ShowDialog(this);
            return dialogResult;
        }


        public static DialogResult CopyAssetToAzure(ref bool UseDefaultStorage, ref string containername, ref string otherstoragename, ref string otherstoragekey, ref List<IAssetFile> SelectedFiles, ref bool CreateNewContainer, IAsset sourceAsset)
        {
            ExportAssetToAzureStorage form = new ExportAssetToAzureStorage(_context, _credentials.StorageKey, sourceAsset)
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


        public static DialogResult DisplayInfo(IJob job)
        {
            CloudMediaContext context = Program.ConnectAndGetNewContext(_credentials);
            JobInformation form = new JobInformation(context);
            // we get a new context to have the latest job and task information (otherwise, task is not dynamically updated)
            form.MyJob = context.Jobs.Where(j => j.Id == job.Id).FirstOrDefault();
            DialogResult dialogResult = form.ShowDialog();
            return dialogResult;
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

                    if (InputBox("Asset rename", "Enter the new name:", ref value) == DialogResult.OK)
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
            sbuilderThisAsset.AppendLine(AssetInfo.rw(locator.Path, SESelected));
            sbuilderThisAsset.AppendLine("");

            if (locatorType == LocatorType.OnDemandOrigin)
            {

                // Get the MPEG-DASH URL of the asset for adaptive streaming.
                Uri mpegDashUri = AssetInfo.rw(locator.GetMpegDashUri(), SESelected);

                // Get the HLS URL of the asset for adaptive streaming.
                Uri HLSUri = AssetInfo.rw(locator.GetHlsUri(), SESelected);

                // Get the Smooth URL of the asset for adaptive streaming.
                Uri SmoothUri = AssetInfo.rw(locator.GetSmoothStreamingUri(), SESelected);

                if (AssetToP.AssetType == AssetType.MediaServicesHLS)
                // It is a static HLS asset, so let's propose only the standard HLS V3 locator
                {
                    sbuilderThisAsset.AppendLine(AssetInfo._hls_v3 + " : ");
                    sbuilderThisAsset.AppendLine(AddBracket(AssetInfo.GetHLSv3(HLSUri.ToString())));
                }
                else // It's not Static HLS
                {
                    if (!SESelectedHasRU && AssetToP.AssetType == AssetType.SmoothStreaming)
                    // it's smooth streaming with no dynamic packaging
                    {
                        sbuilderThisAsset.AppendLine(AssetInfo._smooth + " : ");
                        sbuilderThisAsset.AppendLine(AddBracket(SmoothUri.ToString()));

                    }
                    else if (SESelectedHasRU && (AssetToP.AssetType == AssetType.SmoothStreaming || AssetToP.AssetType == AssetType.MultiBitrateMP4))
                    // Smooth or multi MP4, SE RU so dynamic packaging is possible
                    {
                        if (locator.GetSmoothStreamingUri() != null)
                        {
                            sbuilderThisAsset.AppendLine(AssetInfo._smooth + " : ");
                            sbuilderThisAsset.AppendLine(AddBracket(SmoothUri.ToString()));
                            sbuilderThisAsset.AppendLine(AssetInfo._smooth_legacy + " : ");
                            sbuilderThisAsset.AppendLine(AddBracket(AssetInfo.GetSmoothLegacy(SmoothUri.ToString())));


                        }
                        if (locator.GetMpegDashUri() != null)
                        {
                            sbuilderThisAsset.AppendLine(AssetInfo._dash + " : ");
                            sbuilderThisAsset.AppendLine(AddBracket(mpegDashUri.ToString()));
                        }
                        if (locator.GetHlsUri() != null)
                        {
                            sbuilderThisAsset.AppendLine(AssetInfo._hls_v4 + " : ");
                            sbuilderThisAsset.AppendLine(AddBracket(HLSUri.ToString()));
                            sbuilderThisAsset.AppendLine(AssetInfo._hls_v3 + " : ");
                            sbuilderThisAsset.AppendLine(AddBracket(AssetInfo.rw(locator.GetHlsv3Uri(), SESelected).ToString()));
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
                                    sbuilderThisAsset.AppendLine(AddBracket(uri.ToString()));

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
                    Task[] deleteTasks = SelectedAssets.ToList().Select(a => a.DeleteAsync()).ToArray();
                    TextBoxLogWriteLine("Deleting asset(s)");
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
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
        /*
          private void DoMenuDeleteSelectedAssets()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();

            if (SelectedAssets.Count > 0)
            {
                string question = (dataGridViewAssetsV.SelectedRows.Count == 1) ? "Delete " + SelectedAssets[0].Name + " ?" : "Delete these " + SelectedAssets.Count + " assets ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Asset deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IAsset AssetTODelete in SelectedAssets)

                        if (AssetTODelete != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Deleting asset '{0}'", AssetTODelete.Name);
                            try
                            {
                                DeleteAsset(AssetTODelete);
                                if (AssetInfo.GetAsset(AssetTODelete.Id, _context) == null) TextBoxLogWriteLine("Deletion done.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting the asset {0}.", AssetTODelete.Name, true);
                                TextBoxLogWriteLine(ex);
                            }
                        }
                    DoRefreshGridAssetV(false);
                }
            }
        } 
         */

        private void allAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoDeleteAssets(_context.Assets.ToList());
        }


        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfo();
        }

        private void DoMenuDisplayAssetInfo()
        {
            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count != 1) return;
            IAsset AssetToDisplayP = SelectedAssets.FirstOrDefault();
            if (AssetToDisplayP == null) return;

            // Refresh the asset.
            AssetToDisplayP = _context.Assets.Where(a => a.Id == AssetToDisplayP.Id).FirstOrDefault();

            if (DisplayInfo(AssetToDisplayP) == DialogResult.OK)
            {
            }
        }


        private void displayJobInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayJobInfo();
        }


        private void DoMenuDisplayJobInfo()
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
                    if (DisplayInfo(JobToDisplayP2) == DialogResult.OK)
                    {
                    }
                }
            }
        }


        public int DoGridTransferAddItem(string text, TransferType TType, bool PutInTheQueue)
        {
            TransferEntry myTE = new TransferEntry()
                {
                    Name = text,
                    SubmitTime = DateTime.Now,
                    Type = TType
                };

            dataGridViewTransfer.Invoke(new Action(() =>
            {
                _MyListTransfer.Add(myTE);

            }
                ));
            int indexloc = _MyListTransfer.IndexOf(myTE);

            if (PutInTheQueue)
            {
                _MyListTransferQueue.Add(indexloc);
                myTE.processedinqueue = true;
                myTE.State = TransferState.Queued;

            }
            else
            {
                myTE.processedinqueue = false;
                myTE.State = TransferState.Processing;
                myTE.StartTime = DateTime.Now;
            }

            // refresh number in tab
            tabPageTransfers.Invoke(new Action(() => tabPageTransfers.Text = string.Format(Constants.TabTransfers + " ({0})", _MyListTransfer.Count())));
            return indexloc;
        }



        private void DoMenuImportFromAzureStorage()
        {
            string valuekey = "";
            string targetAssetID = "";

            List<IAsset> SelectedAssets = ReturnSelectedAssets();
            if (SelectedAssets.Count > 0) targetAssetID = SelectedAssets.FirstOrDefault().Id;

            if (!havestoragecredentials)
            { // No blob credentials. Let's ask the user
                if (InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
                {
                    _credentials.StorageKey = valuekey;
                    havestoragecredentials = true;
                }
            }
            if (havestoragecredentials) // if we have the storage credentials
            {
                ImportFromAzureStorage form = new ImportFromAzureStorage(_context, _credentials.StorageKey)
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
                    int index = DoGridTransferAddItem("Import from Azure Storage " + (form.ImportCreateNewAsset ? "to a new asset" : "to an existing asset"), TransferType.ImportFromAzureStorage, false);
                    // Start a worker thread that does uploading.
                    Task.Factory.StartNew(() => ProcessImportFromAzureStorage(form.ImportUseDefaultStorage, form.SelectedBlobContainer, form.ImporOtherStorageName, form.ImportOtherStorageKey, form.SelectedBlobs, form.ImportCreateNewAsset, form.ImportNewAssetName, targetAssetID, index));
                    DotabControlMainSwitch(Constants.TabTransfers);
                    DoRefreshGridAssetV(false);
                }
            }
        }


        private void ProcessImportFromAzureStorage(bool UseDefaultStorage, string containername, string otherstoragename, string otherstoragekey, List<IListBlobItem> SelectedBlobs, bool CreateNewAsset, string newassetname, string targetAssetID, int index)
        {
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
                storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);


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
                SetISMFileAsPrimary(asset);
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

                var externalStorageAccount = new CloudStorageAccount(new StorageCredentials(otherstoragename, otherstoragekey), true);
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

                var destinationStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
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
                SetISMFileAsPrimary(asset);

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

            bool Error = false;
            if (UseDefaultStorage) // The default storage is used
            {
                TextBoxLogWriteLine("Starting the Azure export process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
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
                        DoGridTransferDeclareCompleted(index, TargetContainer.Uri.ToString());
                    }
                    DoRefreshGridAssetV(false);
                }
            }
            else // Another storage is used
            {
                TextBoxLogWriteLine("Starting the blob copy process.");

                // let's get cloudblobcontainer for source
                CloudStorageAccount SourceStorageAccount = new CloudStorageAccount(new StorageCredentials(_context.DefaultStorageAccount.Name, _credentials.StorageKey), true);
                CloudStorageAccount TargetStorageAccount = new CloudStorageAccount(new StorageCredentials(otherstoragename, otherstoragekey), true);

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
                        DoGridTransferDeclareCompleted(index, TargetContainer.Uri.ToString());
                    }
                    DoRefreshGridAssetV(false);
                }
            }
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

        /*
         *   private void DoDeleteSelectedJobs()
        {
            List<IJob> SelectedJobs = ReturnSelectedJobs();

            if (SelectedJobs.Count > 0)
            {
                string question = "Delete these " + SelectedJobs.Count + " jobs ?";
                if (SelectedJobs.Count == 1) question = "Delete " + SelectedJobs[0].Name + " ?";
                if (System.Windows.Forms.MessageBox.Show(question, "Job(s) deletion", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IJob JobToDelete in SelectedJobs)

                        if (JobToDelete != null)
                        {
                            //delete
                            TextBoxLogWriteLine("Deleting job '{0}'.", JobToDelete.Name);

                            try
                            {
                                JobToDelete.Delete();
                                TextBoxLogWriteLine("Job deleted.");
                            }

                            catch (Exception ex)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting the job '{0}'", JobToDelete.Name, true);
                                TextBoxLogWriteLine(ex);
                            }
                        }
                    DoRefreshGridJobV(false);
                }
            }
        }
         */


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


        private void azureManagementPortalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://manage.windowsazure.com");
        }

        private void encodeAssetWithDigitalRapidsKayakCloudEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithZenium();
        }

        private void DoMenuEncodeWithZenium()
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

            string taskname = "Zenium Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvWorkflow;

            EncodingZenium form = new EncodingZenium(_context)
            {
                EncodingPromptText = (SelectedAssets.Count > 1) ? "Input assets : " + SelectedAssets.Count + " assets have been selected." : "Input asset : '" + SelectedAssets.FirstOrDefault().Name + "'",
                EncodingProcessorsList = Encoders,
                EncodingJobName = "Zenium Encoding of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + "-Zenium encoded with " + Constants.NameconvWorkflow,
                EncodingPriority = Properties.Settings.Default.DefaultJobPriority,
                EncodingMultipleJobs = true,
                EncodingNumberInputAssets = SelectedAssets.Count,
                EncodingPremiumWorkflowPresetXMLFiles = Properties.Settings.Default.PremiumWorkflowPresetXMLFilesCurrentFolder
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (!form.EncodingMultipleJobs) // ONE job with all input assets
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                    IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);
                    foreach (IAsset graphAsset in form.SelectedZeniumWorkflows) // for each blueprint selected, we create a task
                    {
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);
                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   form.EncodingProcessorSelected,
                                   "",
                                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                        // Specify the workflow asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.Add(graphAsset);
                        task.InputAssets.AddRange(SelectedAssets); // we add all assets
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
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
                        TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
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

                        IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);
                        foreach (IAsset graphAsset in form.SelectedZeniumWorkflows) // for each workflow selected, we create a task
                        {
                            string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);

                            ITask task = job.Tasks.AddNew(
                                        tasknameloc,
                                       form.EncodingProcessorSelected,
                                       "",
                                       Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                            // Specify the graph asset to be encoded, followed by the input video asset to be used
                            task.InputAssets.Add(graphAsset);
                            task.InputAssets.Add(asset); // we add one asset
                            string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvWorkflow, graphAsset.Name);

                            task.OutputAssets.AddNew(outputassetnameloc, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
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
                EncodingLabel2 = "Select a encoding profile:",
                EncodingProcessorsList = Encoders,
                EncodingJobPriority = Properties.Settings.Default.DefaultJobPriority
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                string taskname = "AME Encoding of " + Constants.NameconvInputasset + " with " + Constants.NameconvAMEpreset;
                LaunchJobs(form.EncodingProcessorSelected, SelectedAssets, form.EncodingJobName, form.EncodingJobPriority, taskname, form.EncodingOutputAssetName, form.EncodingSelectedPreset, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None, form.StorageSelected);
            }
        }


        private void Mainform_Shown(object sender, EventArgs e)
        {

            // display the update message if a new version is available
            if (!string.IsNullOrEmpty(Program.MessageNewVersion)) TextBoxLogWriteLine(Program.MessageNewVersion);
        }



        private void DoGridTransferInit()
        {
            const string labelProgress = "Progress";

            _MyListTransfer = new BindingList<TransferEntry>();
            _MyListTransferQueue = new List<int>();


            DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn();
            DataGridViewCellStyle cellstyle = new DataGridViewCellStyle();
            col.Name = labelProgress;
            col.DataPropertyName = labelProgress;
            dataGridViewTransfer.Invoke(new Action(() =>
            {
                dataGridViewTransfer.Columns.Add(col);
            }
            ));

            dataGridViewTransfer.Invoke(new Action(() =>
            {
                dataGridViewTransfer.DataSource = _MyListTransfer;
            }
          ));

            dataGridViewTransfer.Invoke(new Action(() =>
                {
                    dataGridViewTransfer.Columns[labelProgress].DisplayIndex = 3;
                    dataGridViewTransfer.Columns[labelProgress].HeaderText = labelProgress;
                    dataGridViewTransfer.Columns["processedinqueue"].Visible = false;
                    dataGridViewTransfer.Columns["ErrorDescription"].Visible = false;
                }
          ));
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

        public static string LoadAndUpdateIndexerConfiguration(string xmlFileName, string AssetTitle, string AssetDescription)
        {
            // Prepare the encryption task template
            XDocument doc = XDocument.Load(xmlFileName);

            var inputxml = doc.Element("configuration").Element("input");

            if (!string.IsNullOrEmpty(AssetTitle)) inputxml.Add(new XElement("metadata", new XAttribute("key", "title"), new XAttribute("value", AssetTitle)));
            if (!string.IsNullOrEmpty(AssetDescription)) inputxml.Add(new XElement("metadata", new XAttribute("key", "description"), new XAttribute("value", AssetDescription)));

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


        private void packageSmoothStreamingTOHLSstaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuPackageSmoothToHLSStatic();

        }

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
                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, new List<string> { configHLS }, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }
        }

        private void packageMultiMP4AssetToSmoothStreamingstaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuMP4ToSmoothStatic();
        }

        private void DoMenuMP4ToSmoothStatic()
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

            if (!SelectedAssets.All(a => a.AssetType == AssetType.MultiBitrateMP4))
            {
                MessageBox.Show("Asset(s) should be in multi bitrate MP4 format.", "Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            string labeldb = "Package '" + mediaAsset.Name + "' to Smooth ?";

            if (SelectedAssets.Count > 1)
            {
                labeldb = "Package these " + SelectedAssets.Count + " assets to Smooth Streaming?";
            }

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

                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, new List<string> { smoothConfig }, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
            }
        }

        private void encryptWithPlayReadystaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuProtectWithPlayReadyStatic();
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

            PlayReadyStaticEnc form = new PlayReadyStaticEnc()
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
                bool Error = false;
                IContentKey contentKey;
                string keyDeliveryServiceUri = form.PlayReadyLAurl;
                if (form.PlayReadyConfigureLicenseDelivery)
                {
                    PlayReadyLicense formPlayReady = new PlayReadyLicense();
                    if (formPlayReady.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            contentKey = DynamicEncryption.ConfigureKeyDeliveryServiceForPlayReady(
                                _context,
                                form.PlayReadyKeyId,
                                Convert.FromBase64String(form.PlayReadyContentKey),
                                formPlayReady.GetLicenseTemplate,
                                form.GetPlayReadyKeyRestrictionType,
                                formPlayReady.PlayReadyPolicyName
                                );

                            keyDeliveryServiceUri = contentKey.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense).ToString();
                            TextBoxLogWriteLine("PlayReady license delivery configured. License Acquisition URL is {0}.", keyDeliveryServiceUri);
                        }

                        catch (Exception e)
                        {
                            TextBoxLogWriteLine("Error when configuring PlayReady license delivery.", true);
                            TextBoxLogWriteLine(e);
                            Error = true;
                        }
                    }
                    else
                    {
                        return;
                    }



                }
                if (!Error)
                {
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

                    LaunchJobs(processor, SelectedAssets, jobname, taskname, form.PlayReadyOutputAssetName, new List<string> { configPlayReady }, AssetCreationOptions.CommonEncryptionProtected);
                }

            }

        }
        private void LaunchJobs(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, string taskname, string outputassetname, List<string> configuration, AssetCreationOptions creationoptions, string storageaccountname = "")
        {
            LaunchJobs(processor, selectedassets, jobname, Properties.Settings.Default.DefaultJobPriority, taskname, outputassetname, configuration, creationoptions, storageaccountname);
        }

        private void LaunchJobs(IMediaProcessor processor, List<IAsset> selectedassets, string jobname, int jobpriority, string taskname, string outputassetname, List<string> configuration, AssetCreationOptions creationoptions, string storageaccountname = "")
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
                          Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);

                    myTask.InputAssets.Add(asset);

                    // Add an output asset to contain the results of the task
                    string outputassetnameloc = outputassetname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvAMEpreset, config);
                    if (storageaccountname == "")
                    {
                        myTask.OutputAssets.AddNew(outputassetnameloc, asset.StorageAccountName, creationoptions); // let's use the same storage account than the input asset

                    }
                    else
                    {
                        myTask.OutputAssets.AddNew(outputassetnameloc, storageaccountname, creationoptions);
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

        private void validateMultiMP4AssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuValidateMultiMP4Static();
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

                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, new List<string> { configMp4Validation }, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
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
                IndexerJobPriority = Properties.Settings.Default.DefaultJobPriority,
                IndexerInputAssetName = (SelectedAssets.Count > 1) ? SelectedAssets.Count + " assets have been selected for media indexing." : "Asset '" + SelectedAssets.FirstOrDefault().Name + "' will be indexed.",
            };

            string taskname = "Indexing of " + Constants.NameconvInputasset;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string configIndexer = string.Empty;

                if (!string.IsNullOrEmpty(form.IndexerTitle) || !string.IsNullOrEmpty(form.IndexerDescription))
                {
                    configIndexer = LoadAndUpdateIndexerConfiguration(
               Path.Combine(_configurationXMLFiles, @"MediaIndexer.xml"),
               form.IndexerTitle,
               form.IndexerDescription
               );
                }

                LaunchJobs(processor, SelectedAssets, form.IndexerJobName, form.IndexerJobPriority, taskname, form.IndexerOutputAssetName, new List<string> { configIndexer }, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None, form.StorageSelected);
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

                LaunchJobs(processor, SelectedAssets, jobname, taskname, outputassetname, new List<string> { "" }, AssetCreationOptions.None);
            }
        }

        private void storageDecryptTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDecryptAsset();
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
            MessageBox.Show("Please create an streaming locator in the Publish menu." + Constants.endline + Constants.endline + "Check that you have, at least, one Streaming endpoint scale Unit" + Constants.endline + "The asset should be:" + Constants.endline + "- a Smooth Streaming asset (Clear or PlayReady protected)," + Constants.endline + "- or a Clear Multi MP4 asset.", "Dynamic Packaging");
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
            comboBoxFilterTimeProgram.SelectedIndex = 0; // last 50 items

            comboBoxStatusProgram.Items.AddRange(
            typeof(ProgramState)
            .GetFields()
            .Select(i => i.Name as string)
            .ToArray()
            );
            comboBoxStatusProgram.Items[0] = "All";
            comboBoxStatusProgram.SelectedIndex = 0;


            comboBoxOrderProgram.Items.AddRange(
          typeof(OrderPrograms)
          .GetFields()
          .Select(i => i.GetValue(null) as string)
          .ToArray()
          );
            comboBoxOrderProgram.SelectedIndex = 0;

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
                EncodingPriority = Properties.Settings.Default.DefaultJobPriority,
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
                IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);
                string tasknameloc = taskname.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name).Replace(Constants.NameconvEncodername, form.EncodingProcessorSelected.Name + " v" + form.EncodingProcessorSelected.Version);
                ITask AMETask = job.Tasks.AddNew(
                    tasknameloc,
                  form.EncodingProcessorSelected,// processor,
                   form.EncodingConfiguration,
                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);

                AMETask.InputAssets.AddRange(form.SelectedAssets);

                // Add an output asset to contain the results of the job.  
                string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name);
                AMETask.OutputAssets.AddNew(outputassetnameloc, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);

                // if UserControl wants also aboutToolStripMenuItem thumbnails task
                if (form.EncodingGenerateThumbnails)
                {
                    ITask AMETaskThumbnails = job.Tasks.AddNew(
                                       tasknameloc,
                                     form.EncodingProcessorSelected,// processor,
                                      "Thumbnails",
                                      Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);

                    AMETaskThumbnails.InputAssets.AddRange(form.SelectedAssets);

                    // Add an output asset to contain the results of the job.  
                    string outputassetnamelocthumbnails = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, SelectedAssets[0].Name) + " (Thumbnails)";
                    AMETaskThumbnails.OutputAssets.AddNew(outputassetnamelocthumbnails, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
                }
                // Submit the job and wait until it is completed. 
                try
                {
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    MessageBox.Show("There has been a problem when submitting the job " + jobnameloc + Constants.endline + e.Message);
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

            GenericProcessor form = new GenericProcessor(_context)
            {
                EncodingProcessorsList = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList(),
                EncodingJobName = Constants.NameconvProcessorname + " processing of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = Constants.NameconvInputasset + "-" + Constants.NameconvProcessorname + " processed",
                EncodingPriority = Properties.Settings.Default.DefaultJobPriority,
                SelectedAssets = SelectedAssets,
                EncodingCreationMode = TaskJobCreationMode.MultipleTasks_MultipleJobs,
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Read and update the configuration XML.
                //
                if (form.EncodingCreationMode == TaskJobCreationMode.MultipleTasks_MultipleJobs) // One task per input asset, each task in one job
                {
                    foreach (IAsset asset in SelectedAssets)
                    {
                        string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;

                        IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);

                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   form.EncodingProcessorSelected,
                                   form.EncodingConfiguration,
                                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                        // Specify the graph asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.AddRange(SelectedAssets);
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);

                        TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                        // Submit the job and wait until it is completed. 
                        try
                        {
                            job.Submit();
                        }
                        catch (Exception e)
                        {
                            // Add useful information to the exception
                            TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                            TextBoxLogWriteLine(e);
                            return;
                        }
                        dataGridViewJobsV.DoJobProgress(job);
                    }
                }
                else if (form.EncodingCreationMode == TaskJobCreationMode.MultipleTasks_SingleJob)  /////////////   Several tasks but all in one job
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;
                    IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);

                    foreach (IAsset asset in SelectedAssets)
                    {
                        string tasknameloc = taskname.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                        ITask task = job.Tasks.AddNew(
                                    tasknameloc,
                                   form.EncodingProcessorSelected,
                                   form.EncodingConfiguration,
                                   Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                        // Specify the graph asset to be encoded, followed by the input video asset to be used
                        task.InputAssets.Add(asset);
                        string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, asset.Name).Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                        task.OutputAssets.AddNew(outputassetnameloc, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);
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
                        MessageBox.Show("There has been a problem when submitting the job " + jobnameloc);
                        TextBoxLogWriteLine("There has been a problem when submitting the job {0}.", jobnameloc, true);
                        TextBoxLogWriteLine(e);
                        return;
                    }
                    dataGridViewJobsV.DoJobProgress(job);
                }
                else if (form.EncodingCreationMode == TaskJobCreationMode.SingleTask_SingleJob) // Create one single task in one job
                {
                    string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;
                    IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);

                    string tasknameloc = taskname.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                    ITask task = job.Tasks.AddNew(
                                tasknameloc,
                               form.EncodingProcessorSelected,
                               form.EncodingConfiguration,
                               Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                    // Specify the graph asset to be encoded, followed by the input video asset to be used
                    task.InputAssets.AddRange(SelectedAssets);
                    string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                    task.OutputAssets.AddNew(outputassetnameloc, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);

                    TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                    // Submit the job and wait until it is completed. 
                    try
                    {
                        job.Submit();
                    }
                    catch (Exception e)
                    {
                        // Add useful information to the exception
                        TextBoxLogWriteLine("There has been a problem when submitting the job '{0}'", jobnameloc, true);
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

                if (asset == null) return;

                if (DisplayInfo(asset) == DialogResult.OK)
                {
                }
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
            if (e.ColumnIndex == dataGridViewJobsV.Columns["State"].Index) // state column
            {
                if (dataGridViewJobsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    JobState JS = (JobState)dataGridViewJobsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
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
                            mycolor = Color.LightBlue;
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
                    for (int i = 0; i < dataGridViewJobsV.Columns["Progress"].Index; i++) dataGridViewJobsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;
                }
            }
        }

        private void dataGridViewJobsV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                IJob job = GetJob(dataGridViewJobsV.Rows[e.RowIndex].Cells[dataGridViewJobsV.Columns["Id"].Index].Value.ToString());

                if (job == null) return;

                if (DisplayInfo(job) == DialogResult.OK)
                {
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

            if (e.ColumnIndex == dataGridViewAssetsV.Columns["Type"].Index) // state column
            {
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string TypeStr = (string)dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (TypeStr.Equals(AssetInfo.Type_Empty)) foreach (DataGridViewCell c in dataGridViewAssetsV.Rows[e.RowIndex].Cells) c.Style.ForeColor = Color.Red;
                    else if (TypeStr.Contains(AssetInfo.Type_Workflow)) foreach (DataGridViewCell c in dataGridViewAssetsV.Rows[e.RowIndex].Cells) c.Style.ForeColor = Color.Blue;
                }
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns["Size"].Index) // size column
            {
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    string TypeStr = (string)dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (TypeStr.Equals("0 B")) foreach (DataGridViewCell c in dataGridViewAssetsV.Rows[e.RowIndex].Cells) c.Style.ForeColor = Color.Red;
                }
            }

            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._statEnc].Index)  // Mouseover for icons
            {

                var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._statEncMouseOver].Value != null)
                    cell.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._statEncMouseOver].Value.ToString();
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._dynEnc].Index)// Mouseover for icons
            {
                var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value != null)
                    cell.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._dynEncMouseOver].Value.ToString();
            }
            else if (e.ColumnIndex == dataGridViewAssetsV.Columns[dataGridViewAssetsV._publication].Index)// Mouseover for icons
            {
                var cell = dataGridViewAssetsV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value != null)
                    cell.ToolTipText = dataGridViewAssetsV.Rows[e.RowIndex].Cells[dataGridViewAssetsV._publicationMouseOver].Value.ToString();
            }
        }

        private void toolStripMenuItemDisplayInfo_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfo();
        }

        private void contextMenuStripAssets_Opening(object sender, CancelEventArgs e)
        {
            bool singleitem = (ReturnSelectedAssets().Count == 1);
            ContextMenuItemAssetDisplayInfo.Enabled = singleitem;
            ContextMenuItemAssetRename.Enabled = singleitem;
            ContextMenuItemAssetExportAssetFilesToAzureStorage.Enabled = singleitem;
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
            bool singleitem = (ReturnSelectedAssets().Count == 1);
            informationToolStripMenuItem.Enabled = singleitem;
            renameToolStripMenuItem.Enabled = singleitem;
            toAzureStorageToolStripMenuItem.Enabled = singleitem;

        }

        private void toolStripMenuJobDisplayInfo_Click(object sender, EventArgs e)
        {
            DoMenuDisplayJobInfo();
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

        private void displayInformationForAKnownJobIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayJobInfoFromKnownID();
        }

        private static void DoMenuDisplayJobInfoFromKnownID()
        {

            string JobId = "";
            string clipbs = Clipboard.GetText();
            if (clipbs != null) if (clipbs.StartsWith("nb:jid:UUID:")) JobId = clipbs;


            if (InputBox("Job ID", "Please enter the known Job Id :", ref JobId) == DialogResult.OK)
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

            string AssetId = "";
            string clipbs = Clipboard.GetText();
            if (clipbs != null) if (clipbs.StartsWith("nb:cid:UUID:")) AssetId = clipbs;

            if (InputBox("Asset ID", "Please enter the known Asset Id :", ref AssetId) == DialogResult.OK)
            {
                IAsset KnownAsset = AssetInfo.GetAsset(AssetId, _context);
                if (KnownAsset == null)
                {
                    MessageBox.Show("This asset has not been found.");
                }

                else if (DisplayInfo(KnownAsset) == DialogResult.OK)
                {
                }
            }

        }

        private void displayInformationForAKnownAssetIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoFromKnownID();
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

        private void toolStripComboBoxNbItemsPage_Click(object sender, EventArgs e)
        {

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
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.FlashAzurePage, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }



        private void withSilverlightMMPPFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.SilverlightMonitoring, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }



        private void withFlashOSMFToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void playbackToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            bool CanBePlay = IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator);
            withFlashOSMFToolStripMenuItem.Enabled = CanBePlay;
            withSilverlightMMPPFToolStripMenuItem.Enabled = CanBePlay;
            withMPEGDASHAzurePlayerToolStripMenuItem.Enabled = CanBePlay;
            withMPEGDASHIFRefPlayerToolStripMenuItem.Enabled = CanBePlay;
            withCustomPlayerToolStripMenuItem.Enabled = CanBePlay;
        }

        private bool IsAssetCanBePlayed(IAsset asset, ref ILocator locator)
        {
            if (asset != null)
            {
                if (asset.Locators.Count > 0)
                {
                    ILocator LocatorsOrigin = asset.Locators.Where(l => (l.Type == LocatorType.OnDemandOrigin) && ((l.StartTime < DateTime.UtcNow) || (l.StartTime == null)) && (l.ExpirationDateTime > DateTime.UtcNow)).FirstOrDefault();
                    if (LocatorsOrigin != null)
                    {
                        //OK we can play the content
                        locator = LocatorsOrigin;
                        return true;
                    }
                }
            }
            return false;
        }

        private void withMPEGDASHIFRefPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.DASHIFRefPlayer, PlayBackLocator.GetMpegDashUri(), _context);
        }



        private void withFlashOSMFAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.FlashAzurePage, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.SilverlightMonitoring, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }

        private void withMPEGDASHIFReferencePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.DASHIFRefPlayer, PlayBackLocator.GetMpegDashUri(), _context);
        }

        private void withMPEGDASHAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.DASHAzurePage, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }

        private void playbackTheAssetToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            bool CanBePlay = IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator);
            ContextMenuItemPlaybackWithMPEGDASHAzure.Enabled = CanBePlay;
            ContextMenuItemPlaybackWithMPEGDASHIFReference.Enabled = CanBePlay;
            ContextMenuItemPlaybackWithSilverlightMonitoring.Enabled = CanBePlay;
            ContextMenuItemPlaybackWithFlashOSMFAzure.Enabled = CanBePlay;
            withFlashTokenPlayerToolStripMenuItem.Enabled = CanBePlay;
            withSilverlightPlayReadyTokenPlayerToolStripMenuItem.Enabled = CanBePlay;
            withCustomPlayerToolStripMenuItem1.Enabled = CanBePlay;
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
                    if (InputBox("Storage Account Key Needed", "Please enter the Storage Account Access Key for " + _context.DefaultStorageAccount.Name + ":", ref valuekey) == DialogResult.OK)
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
                            int index = DoGridTransferAddItem("Export to Azure Storage " + (CreateNewContainer ? "to a new container" : "to an existing container"), TransferType.ExportToAzureStorage, false);
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
            WatchFolder form = new WatchFolder(_context)
            {
                WatchDeleteFile = WatchFolderDeleteFile,
                WatchFolderPath = WatchFolderFolderPath,
                WatchOn = WatchFolderIsOn,
                WatchUseQueue = Properties.Settings.Default.useTransferQueue
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                WatchFolderFolderPath = form.WatchFolderPath;
                WatchFolderIsOn = form.WatchOn;
                Properties.Settings.Default.useTransferQueue = form.WatchUseQueue;
                Program.SaveAndProtectUserConfig();
                WatchFolderDeleteFile = form.WatchDeleteFile;
                WatchFolderJobTemplate = form.WatchRunJobTemplate ? form.WatchSelectedJobTemplate : null; // let's save the job template to the main variable

                if (!WatchFolderIsOn) // user want to stop the watch folder (if if exists)
                {
                    if (WatchFolderWatcher != null)
                    {
                        WatchFolderWatcher.EnableRaisingEvents = false;
                        WatchFolderWatcher = null;
                    }
                    toolStripStatusLabelWatchFolder.Visible = false;

                }
                else // User wants to active the watch folder
                {
                    if (WatchFolderWatcher == null)
                    {
                        // Create a new FileSystemWatcher and set its properties.
                        WatchFolderWatcher = new FileSystemWatcher();
                    }

                    WatchFolderWatcher.Path = WatchFolderFolderPath;
                    /* Watch for changes in LastAccess and LastWrite times, and
                       the renaming of files or directories. */
                    WatchFolderWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                       | NotifyFilters.FileName; //| NotifyFilters.DirectoryName;
                    // Only watch text files.
                    WatchFolderWatcher.Filter = "*.*";
                    WatchFolderWatcher.IncludeSubdirectories = false;

                    // Begin watching.
                    WatchFolderWatcher.EnableRaisingEvents = true;
                    toolStripStatusLabelWatchFolder.Visible = true;

                    WatchFolderWatcher.Created += (s, e) =>
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
                        Task.Factory.StartNew(() => ProcessUploadFile(path, index, WatchFolderDeleteFile, WatchFolderJobTemplate));

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
                catch (Exception ex)
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




        private void azureMediaServicesMSDNToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"http://aka.ms/wamsmsdn");
        }

        private void azureMediaServicesForumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start(@"http://aka.ms/wamshelp");
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

            EnableChildItems(ref encodingToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabJobs)));
            EnableChildItems(ref contextMenuStripJobs, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabJobs)));

            EnableChildItems(ref transferToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabTransfers)));
            EnableChildItems(ref contextMenuStripTransfers, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabTransfers)));

            EnableChildItems(ref originToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabOrigins)));
            EnableChildItems(ref contextMenuStripStreaminEndpoints, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabOrigins)));

            EnableChildItems(ref liveChannelToolStripMenuItem, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));
            EnableChildItems(ref contextMenuStripChannels, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));
            EnableChildItems(ref contextMenuStripPrograms, (tabcontrol.SelectedTab.Text.StartsWith(Constants.TabLive)));

            // let's disable Zenium if not present
            if (!AMEZeniumPresent)
            {
                encodeAssetWithZeniumToolStripMenuItem.Enabled = false;  //menu
                ContextMenuItemZenium.Enabled = false; // mouse context menu
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
                dataGridViewChannelsV.Init(_credentials);
            }

            Debug.WriteLine("DoRefreshGridLiveVNotforsttime");
            int backupindex = 0;
            int pagecount = 0;
            //comboBoxPageJobs.Invoke(new Action(() => backupindex = comboBoxPageJobs.SelectedIndex));
            dataGridViewChannelsV.Invoke(new Action(() => dataGridViewChannelsV.RefreshChannels(_context, backupindex + 1)));
            //comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Clear()));
            //dataGridViewJobsV.Invoke(new Action(() => pagecount = dataGridViewJobsV.PageCount));

            // add pages
            //for (int i = 1; i <= pagecount; i++) comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.Items.Add(i)));
            //comboBoxPageJobs.Invoke(new Action(() => comboBoxPageJobs.SelectedIndex = dataGridViewJobsV.CurrentPage - 1));
            //uodate tab nimber of jobs

            tabPageLive.Invoke(new Action(() => tabPageLive.Text = string.Format(Constants.TabLive + " ({0})", dataGridViewChannelsV.DisplayedCount)));
        }

        private void DoRefreshGridProgramV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewProgramsV.Init(_credentials);
            }

            Debug.WriteLine("DoRefreshGridProgramVNotforsttime");
            int backupindex = 0;
            dataGridViewProgramsV.Invoke(new Action(() => dataGridViewProgramsV.RefreshPrograms(_context, backupindex + 1)));
        }

        private void DoRefreshGridStreamingEndpointV(bool firstime)
        {
            if (firstime)
            {
                dataGridViewStreamingEndpointsV.Init(_credentials);

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
                dataGridViewProcessors.Columns[1].HeaderText = "Name";
                dataGridViewProcessors.Columns[2].HeaderText = "Version";
                dataGridViewProcessors.Columns[3].HeaderText = "Id";
                dataGridViewProcessors.Columns[4].HeaderText = "Description";
                dataGridViewProcessors.Columns[0].Width = 110;
                dataGridViewProcessors.Columns[2].Width = 70;
            }
            dataGridViewProcessors.Rows.Clear();
            List<IMediaProcessor> Procs = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList();
            foreach (IMediaProcessor proc in Procs)
            {
                dataGridViewProcessors.Rows.Add(proc.Vendor, proc.Name, proc.Version, proc.Id, proc.Description);
            }
            tabPageProcessors.Text = string.Format(Constants.TabProcessors + " ({0})", Procs.Count());

            // Encoding Reserved Unit(s)
            comboBoxEncodingRU.Items.Clear();
            comboBoxEncodingRU.Items.AddRange(Enum.GetNames(typeof(ReservedUnitType)).ToArray()); // encoding ru hardware type
            comboBoxEncodingRU.SelectedItem = Enum.GetName(typeof(ReservedUnitType), _context.EncodingReservedUnits.FirstOrDefault().ReservedUnitType);
            trackBarEncodingRU.Maximum = _context.EncodingReservedUnits.FirstOrDefault().MaxReservableUnits;
            trackBarEncodingRU.Value = _context.EncodingReservedUnits.FirstOrDefault().CurrentReservedUnits;
            UpdateLabelProcessorUnits();
        }

        private void DoRefreshGridStorageV(bool firstime)
        {
            const long OneTBInByte = 1099511627776;
            const long TotalStorageInBytes = OneTBInByte * 200;

            if (firstime)
            {
                // Storage tab
                dataGridViewStorage.ColumnCount = 2;
                dataGridViewStorage.Columns[0].HeaderText = "Name";
                dataGridViewStorage.Columns[1].HeaderText = "Used space";

                DataGridViewProgressBarColumn col = new DataGridViewProgressBarColumn()
                {
                    Name = "% used",
                    DataPropertyName = "% used",
                    HeaderText = "% used"
                };
                dataGridViewStorage.Columns.Add(col);
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
                MessageBox.Show("Some programs are starting or stopping. Channel(s) cannot be stopped now.", "Channel(s) stop", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (programqueryrunning.Count() > 0) // some programs are running
                {
                    if (MessageBox.Show("One or several programs are running. Do you want to stop the program(s) ?", "Channel stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var yourForeachTask = Task.Run(() =>
                        {
                            Parallel.ForEach(programqueryrunning, myP =>
                            {
                                TextBoxLogWriteLine("Stopping program '{0}'...", myP.Name);
                                ProgramExecuteAsync(myP.StopAsync, myP, "stopped");
                            });
                        });
                        await yourForeachTask;
                    }
                }

                // let's stop the channels now that running programs are stopped
                foreach (IChannel myC in channels)
                {
                    StopChannel(myC);
                }
            }
        }

        private void DoStartChannels()
        {
            foreach (IChannel myC in ReturnSelectedChannels())
            {
                StartChannel(myC);
            }
        }


        private async void StartChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Starting channel '{0}' ", myC.Name);
                await Task.Run(() => ChannelExecuteAsync(myC.StartAsync, myC, "started"));
            }
        }

        private async void StopChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Stopping channel '{0}'", myC.Name);
                await Task.Run(() => ChannelExecuteAsync(myC.StopAsync, myC, "stopped"));
            }
        }

        private async void ResetChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Reseting channel '{0}'", myC.Name);
                await Task.Run(() => ChannelExecuteAsync(myC.ResetAsync, myC, "reset"));
            }
        }

        private async void DeleteChannel(IChannel myC)
        {
            if (myC != null)
            {
                TextBoxLogWriteLine("Deleting channel '{0}'...", myC.Name);
                await Task.Run(() => ChannelExecuteAsync(myC.DeleteAsync, myC, "deleted"));
                DoRefreshGridChannelV(false);
            }
        }

        private async void DeleteProgram(IProgram myP)
        {
            if (myP != null)
            {
                TextBoxLogWriteLine("Deleting program '{0}'...", myP.Name);
                await Task.Run(() => ProgramExecuteAsync(myP.DeleteAsync, myP, "deleted"));
                DoRefreshGridProgramV(false);
            }
        }

        private async void StartProgam(IProgram myP)
        {
            if (myP != null)
            {
                TextBoxLogWriteLine("Starting program '{0}'...", myP.Name);
                await Task.Run(() => ProgramExecuteAsync(myP.StartAsync, myP, "started"));
            }
        }

        private async void StopProgram(IProgram myP)
        {
            if (myP != null)
            {
                TextBoxLogWriteLine("Stopping program '{0}'...", myP.Name);
                await Task.Run(() => ProgramExecuteAsync(myP.StopAsync, myP, "stopped"));
            }
        }

        private async void StartStreamingEndpoint(IStreamingEndpoint myO)
        {
            if (myO != null)
            {
                TextBoxLogWriteLine("Starting streaming endpoint '{0}'...", myO.Name);
                await Task.Run(() => StreamingEndpointExecuteAsync(myO.StartAsync, myO, "started"));
            }
        }
        private async void StopStreamingEndpoint(IStreamingEndpoint myO)
        {
            if (myO != null)
            {
                TextBoxLogWriteLine("Stopping streaming endpoint '{0}'...", myO.Name);
                await Task.Run(() => StreamingEndpointExecuteAsync(myO.StopAsync, myO, "stopped"));
            }
        }

        private async void DeleteStreamingEndpoint(IStreamingEndpoint myO)
        {
            if (myO != null)
            {
                TextBoxLogWriteLine("Deleting streaming endpoint '{0}'.", myO.Name);
                await Task.Run(() => StreamingEndpointExecuteAsync(myO.DeleteAsync, myO, "deleted"));
                DoRefreshGridStreamingEndpointV(false);
            }
        }


        private async void ScaleStreamingEndpoint(IStreamingEndpoint myO, int unit)
        {
            if (myO != null)
            {
                try
                {
                    TextBoxLogWriteLine("Scaling streaming endpoint '{0}' to {1} unit(s)...", myO.Name, unit.ToString());
                    await Task.Run(() => myO.ScaleAsync(unit));
                    TextBoxLogWriteLine("Streaming endpoint '{0}' scaled.", myO.Name);
                    dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myO)), null);
                }

                catch (Exception ex)
                {
                    TextBoxLogWriteLine("Error when scaling streaming endpoint '{0}' : {1}", myO.Name, Program.GetErrorMessage(ex), true);
                }
            }
        }

        internal async Task ChannelExecuteAsync(Func<Task> fCall, IChannel channel, string strStatusSuccess) //used for all except creation 
        {
            try
            {
                var STask = fCall();
                var state = channel.State;
                while (!STask.IsCompleted)
                {
                    // refresh the channel
                    IChannel channelR = _context.Channels.Where(c => c.Id == channel.Id).FirstOrDefault();
                    if (channelR != null && state != channelR.State)
                    {
                        state = channelR.State;
                        dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channelR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                await STask;
                TextBoxLogWriteLine("Channel '{0}' {1}.", channel.Name, strStatusSuccess);
                dataGridViewChannelsV.BeginInvoke(new Action(() => dataGridViewChannelsV.RefreshChannel(channel)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with channel '{0}' : {1}", channel.Name, Program.GetErrorMessage(ex), true);
            }
        }

        internal async Task ChannelExecuteAsync(Func<Task> fCall, string strObjectName, string strStatusSuccess)
        {
            try
            {
                await fCall();
                TextBoxLogWriteLine("Channel '{0}' {1}.", strObjectName, strStatusSuccess);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with channel '{0}' : {1}", strObjectName, Program.GetErrorMessage(ex), true);
            }
        }

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

        internal async Task ProgramExecuteAsync(Func<Task> fCall, string strObjectName, string strStatusSuccess)
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


        internal async Task StreamingEndpointExecuteAsync(Func<Task> fCall, IStreamingEndpoint myO, string strStatusSuccess) //used for all except creation 
        {
            try
            {
                var state = myO.State;
                var STask = fCall();
                while (!STask.IsCompleted)
                {
                    // refresh the streaming endpoint
                    IStreamingEndpoint myOR = _context.StreamingEndpoints.Where(se => se.Id == myO.Id).FirstOrDefault();
                    if (myOR != null && state != myOR.State)
                    {
                        state = myOR.State;
                        dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myOR)), null);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                await STask;
                TextBoxLogWriteLine("Streaming endpoint '{0}' {1}.", myO.Name, strStatusSuccess);
                dataGridViewStreamingEndpointsV.BeginInvoke(new Action(() => dataGridViewStreamingEndpointsV.RefreshStreamingEndpoint(myO)), null);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with streaming endpoint '{0}' : {1}", myO.Name, Program.GetErrorMessage(ex), true);
            }
        }

        internal async Task StreamingEndpointExecuteAsync(Func<Task> fCall, string sename, string strStatusSuccess) // used for creation 
        {
            try
            {
                await fCall();
                TextBoxLogWriteLine("Streaming endpoint '{0}' {1}.", sename, strStatusSuccess);
            }
            catch (Exception ex)
            {
                TextBoxLogWriteLine("Error with streaming endpoint '{0}' : {1}", sename, Program.GetErrorMessage(ex), true);
            }
        }



        private void DoDeleteChannels()
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
                        foreach (IChannel myC in ReturnSelectedChannels())
                        {
                            DeleteChannel(myC);
                        }
                    }
                }
                else // There are programs associated to the channel(s) to be deleted. We need to delete the programs
                {
                    string question = (Programs.Count == 1) ? string.Format("There is one program associated to the c{0}.\nDelete the c{0} and program '{1}' ?", hannelstr, Programs[0].Name)
                                                            : string.Format("There are {0} programs associated to the c{1}.\nDelete the c{1} and these programs ?", Programs.Count, hannelstr);

                    DeleteProgramChannel form = new DeleteProgramChannel(question, "Delete C" + hannelstr);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        foreach (IProgram myP in Programs)
                        {
                            IAsset asset = myP.Asset;
                            DeleteProgram(myP);
                            if (form.DeleteAsset)
                            {
                                if (myP.Asset != null)
                                {
                                    //delete
                                    TextBoxLogWriteLine("Deleting asset '{0}'", asset.Name);
                                    try
                                    {
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
                            }
                        }
                        foreach (IChannel myC in ReturnSelectedChannels())
                        {
                            DeleteChannel(myC);
                        }
                    }
                }
            }
        }




        private void dataGridViewLiveV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewChannelsV.Columns["State"].Index) // state column
            {
                if (dataGridViewChannelsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ChannelState CS = (ChannelState)dataGridViewChannelsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (CS)
                    {
                        case ChannelState.Deleting:
                            mycolor = Color.Red;
                            break;
                        case ChannelState.Stopping:
                            mycolor = Color.Blue;
                            break;
                        case ChannelState.Starting:
                            mycolor = Color.LightBlue;
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
                    for (int i = 0; i < dataGridViewChannelsV.Columns.Count; i++) dataGridViewChannelsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;
                }
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
                        var yourForeachTask = Task.Run(() =>
                        {
                            Parallel.ForEach(programqueryrunning, myP =>
                            {
                                TextBoxLogWriteLine("Stopping program '{0}'...", myP.Name);
                                ProgramExecuteAsync(myP.StopAsync, myP, "stopped");
                            });
                        });
                        await yourForeachTask;
                    }
                }

                // let's stop the channels now that running programs are stopped
                foreach (IChannel myC in channels)
                {
                    ResetChannel(myC);
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
            CreateLiveChannel form = new CreateLiveChannel()
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
                    Input = new ChannelInput()
                    {
                        StreamingProtocol = form.Protocol,
                        AccessControl = new ChannelAccessControl()
                        {
                            IPAllowList = form.inputIPAllow
                        },
                        KeyFrameInterval = form.KeyframeInterval
                    },
                    Output = new ChannelOutput() { Hls = new ChannelOutputHls() { FragmentsPerSegment = form.HLSFragmentPerSegment } }
                };

                await Task.Run(() => ChannelExecuteAsync(
                     () =>
                         _context.Channels.CreateAsync(
                         options
                              ),
                         form.ChannelName,
                         "created"));

                DoRefreshGridChannelV(false);
                IChannel channel = GetChannelFromName(form.ChannelName);
                if (channel != null)
                {
                    if (form.StartChannelNow)
                    {
                        StartChannel(GetChannelFromName(form.ChannelName));
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

        private async void DoDisplayChannelInfo(IChannel channel)
        {

            ChannelInformation form = new ChannelInformation()
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
                else
                {
                    if (channel.Output.Hls.FragmentsPerSegment != null)
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
                await Task.Run(() => ChannelExecuteAsync(channel.UpdateAsync, channel, "updated"));
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


        private void DoDeletePrograms()
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
                        foreach (IProgram myP in SelectedPrograms)
                        {
                            IAsset asset = myP.Asset;
                            DeleteProgram(myP);
                            if (form.DeleteAsset)
                            {
                                if (myP.Asset != null)
                                {
                                    //delete
                                    TextBoxLogWriteLine("Deleting asset '{0}'", asset.Name);
                                    try
                                    {
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
                            }
                        }
                    }
                }
            }
        }


        private void DoStartPrograms()
        {
            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.FirstOrDefault() != null)
            {
                if (SelectedPrograms.Count > 0)
                {
                    foreach (IProgram myP in SelectedPrograms)
                    {
                        StartProgam(myP);
                    }
                }
            }
        }


        private void DoStopPrograms()
        {
            List<IProgram> SelectedPrograms = ReturnSelectedPrograms();
            if (SelectedPrograms.FirstOrDefault() != null)
            {
                if (SelectedPrograms.Count > 0)
                {
                    foreach (IProgram myP in SelectedPrograms)
                    {
                        StopProgram(myP);
                    }
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
                // if user want to create a locator, force locator and setyp dyn encryption, that means he wants to replicate the program from another dc, and so must provide the content key to have a mirror stream 
                oktocontinue = SetupDynamicEncryption(new List<IAsset> { newAsset }, true, createlocator && (LocatorID != null));
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
                        ScaleStreamingEndpoint(_context.StreamingEndpoints.FirstOrDefault(), 1);
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
                                   channel.Programs.CreateAsync(options)
                                   , form.ProgramName,
                                   "created");
                        await STask;

                        DoRefreshGridProgramV(false);

                        if (form.StartProgram)
                        {

                            StartProgam(_context.Programs.Where(p => p.Name == form.ProgramName && p.ChannelId == channel.Id).FirstOrDefault());
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
            if (e.ColumnIndex == dataGridViewProgramsV.Columns["State"].Index) // state column
            {
                if (dataGridViewProgramsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ProgramState CS = (ProgramState)dataGridViewProgramsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (CS)
                    {
                        case ProgramState.Stopping:
                            mycolor = Color.Blue;
                            break;
                        case ProgramState.Starting:
                            mycolor = Color.LightBlue;
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
                    for (int i = 0; i < dataGridViewProgramsV.Columns.Count; i++) dataGridViewProgramsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;
                }
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
                ProgramInformation form = new ProgramInformation(this)
                {
                    MyProgram = program,
                    MyContext = _context,
                    MyStreamingEndpoints = dataGridViewStreamingEndpointsV.DisplayedStreamingEndpoints // we pass this information if user open asset info from the program info dialog box
                };


                if (form.ShowDialog() == DialogResult.OK)
                {
                    program.ArchiveWindowLength = form.archiveWindowLength;
                    program.Description = form.ProgramDescription;
                    await Task.Run(() => ProgramExecuteAsync(program.UpdateAsync, program, "updated"));
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
                ThumbnailsJobPriority = Properties.Settings.Default.DefaultJobPriority,
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

                LaunchJobs(processor, SelectedAssets, form.ThumbnailsJobName, form.ThumbnailsJobPriority, taskname, form.ThumbnailsOutputAssetName, new List<string> { configThumbnails }, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None, form.StorageSelected);
            }
        }

        private void dataGridViewOriginsV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewStreamingEndpointsV.Columns["State"].Index) // state column
            {
                if (dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    StreamingEndpointState OS = (StreamingEndpointState)dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Color mycolor;

                    switch (OS)
                    {
                        case StreamingEndpointState.Deleting:
                            mycolor = Color.Red;
                            break;
                        case StreamingEndpointState.Stopping:
                            mycolor = Color.Red;
                            break;
                        case StreamingEndpointState.Starting:
                            mycolor = Color.Blue;
                            break;
                        case StreamingEndpointState.Stopped:
                            mycolor = Color.Red;
                            break;
                        case StreamingEndpointState.Running:
                            mycolor = Color.Black;
                            break;
                        default:
                            mycolor = Color.Black;
                            break;

                    }
                    for (int i = 0; i < dataGridViewStreamingEndpointsV.Columns.Count; i++) dataGridViewStreamingEndpointsV.Rows[e.RowIndex].Cells[i].Style.ForeColor = mycolor;

                }
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
                if (streamingendpoint.ScaleUnits != form.GetScaleUnits)
                {
                    ScaleStreamingEndpoint(streamingendpoint, form.GetScaleUnits);
                }

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
                await Task.Run(() => StreamingEndpointExecuteAsync(streamingendpoint.UpdateAsync, streamingendpoint, "updated"));
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
                StopStreamingEndpoint(myO);
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
                    foreach (IStreamingEndpoint myO in ReturnSelectedStreamingEndpoints())
                    {
                        DeleteStreamingEndpoint(myO);
                    }
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
                    Description = form.StreamingEndpointDescription
                };

                await Task.Run(() => StreamingEndpointExecuteAsync(
                       () =>
                           _context.StreamingEndpoints.CreateAsync(options)
                           , form.StreamingEndpointName,
                           "created"));

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
            DoPlaybackProgram(PlayerType.FlashAzurePage);
        }

        private void DoPlaybackProgram(PlayerType ptype, bool tokenplayer = false)
        {
            IProgram program = ReturnSelectedPrograms().FirstOrDefault();
            if (program != null && program.Asset != null)
            {
                ProgramInfo PI = new ProgramInfo(program, _context);
                IEnumerable<Uri> ValidURIs = PI.GetValidURIs();
                if (ValidURIs.FirstOrDefault() != null)
                {
                    AssetInfo.DoPlayBack(ptype, ValidURIs.FirstOrDefault().ToString(), _context, program.Asset);
                }
                else
                {
                    TextBoxLogWriteLine("No valid URL exists for this program. Check the streaming endpoints.", true);
                }

            }
        }

        private void DoPlaybackChannelPreview(PlayerType ptype)
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();
            if (channel != null)
            {
                if (channel.Preview.Endpoints.FirstOrDefault().Url.AbsoluteUri != null)
                {
                    AssetInfo.DoPlayBack(ptype, channel.Preview.Endpoints.FirstOrDefault().Url.ToString(), null, AssetProtectionType.None, true);
                }
            }
        }

        private void withSilverlightMontoringPlayerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DoPlaybackProgram(PlayerType.SilverlightMonitoring);
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
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.SilverlightMonitoring, PlayBackLocator.GetSmoothStreamingUri());

        }

        private void withFlashOSMFAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBack(PlayerType.FlashAzurePage, PlayBackLocator.GetSmoothStreamingUri());

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
                BatchUploadFrame2 form2 = new BatchUploadFrame2(form.BatchFolder, form.BatchProcessFiles, form.BatchProcessSubFolders, _context);
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
                        Task.Factory.StartNew(() => ProcessUploadFile(file, index, false, null, form2.StorageSelected));
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
            SetupDynamicEncryption(SelectedAssets, false, false);
        }

        private bool SetupDynamicEncryption(List<IAsset> SelectedAssets, bool IsLiveAsset, bool forceusertoprovidekey)
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
                AddDynamicEncryption form = new AddDynamicEncryption(_context, IsLiveAsset, forceusertoprovidekey);

                if (form.ShowDialog() == DialogResult.OK)
                {

                    bool UserCancelledPlayReadyForm = false;
                    PlayReadyLicense formPlayReadyLicense = new PlayReadyLicense();
                    PlayReadyExternalServer formPlayReadyExternalServer = new PlayReadyExternalServer(SelectedAssets.Count > 1, /*forceusertoprovidekey ||*/ form.GetKeyRestrictionType != null);
                    string aeskey = string.Empty;

                    // TO DO: ASK for content key if the user has to provid it for CENC
                    if (form.GetContentKeyType == ContentKeyType.CommonEncryption) // it's PlayReady dyn encryption
                    {
                        if (form.GetKeyRestrictionType != null) // PlayReady license and delivery from Azure Media Services
                        {
                            if (forceusertoprovidekey || !form.ContentKeyRandomGeneration) // user has to provide the playready key
                            {
                                if (formPlayReadyExternalServer.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                                {
                                    UserCancelledPlayReadyForm = true;
                                }
                            }
                            if (!UserCancelledPlayReadyForm)
                            {
                                if (formPlayReadyLicense.ShowDialog() != System.Windows.Forms.DialogResult.OK) //form to set the license
                                {
                                    UserCancelledPlayReadyForm = true;
                                }
                            }
                        }
                        else // PlayReady license but delivery from an external PlayReady server
                        {
                            if (formPlayReadyExternalServer.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            {
                                UserCancelledPlayReadyForm = true;
                            }
                        }
                    }
                    else if (form.GetContentKeyType == ContentKeyType.EnvelopeEncryption && (forceusertoprovidekey || !form.ContentKeyRandomGeneration)) // Envelope and user want to provide the key
                    {
                        if (InputBox("AES Key", "Please enter the AES clear key :", ref aeskey) == DialogResult.Cancel)
                        {
                            aeskey = string.Empty;
                        }
                    }


                    if (!UserCancelledPlayReadyForm)
                    {
                        oktoproceed = true;
                        bool Error = false;
                        string keydeliveryconfig = null;
                        foreach (IAsset AssetToProcess in SelectedAssets)
                            if (AssetToProcess != null)
                            {
                                if (form.GetDeliveryPolicyType != AssetDeliveryPolicyType.NoDynamicEncryption)  // Dynamic encryption
                                {
                                    IContentKey contentKey = null;

                                    var contentkeys = AssetToProcess.ContentKeys.Where(c => c.ContentKeyType == form.GetContentKeyType);
                                    if (contentkeys.Count() == 0) // no content key existing so we need to create one
                                    {
                                        try
                                        {
                                            if (form.GetContentKeyType == ContentKeyType.EnvelopeEncryption) // Envelope
                                            {
                                                if ((forceusertoprovidekey || !form.ContentKeyRandomGeneration) && !string.IsNullOrEmpty(aeskey)) // user has to provide the key
                                                {
                                                    contentKey = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess, Convert.FromBase64String(aeskey));
                                                }
                                                else
                                                {
                                                    contentKey = DynamicEncryption.CreateEnvelopeTypeContentKey(AssetToProcess);
                                                }
                                            }
                                            else // CENC
                                            {
                                                if (form.GetKeyRestrictionType != null && (!forceusertoprovidekey && form.ContentKeyRandomGeneration)) // Azure will deliver the license and user want to auto generate the key, so we can create a key with a random content key
                                                {
                                                    contentKey = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context);
                                                }
                                                else // user wants to deliver with an external PlayReady server or want to provide the key, so let's create the key based on what the user input
                                                {
                                                    if (!string.IsNullOrEmpty(formPlayReadyExternalServer.PlayReadyKeySeed)) // seed has been given
                                                    {
                                                        Guid keyid = (formPlayReadyExternalServer.PlayReadyKeyId == null) ? Guid.NewGuid() : (Guid)formPlayReadyExternalServer.PlayReadyKeyId;
                                                        byte[] bytecontentkey = DynamicEncryption.GeneratePlayReadyContentKey(Convert.FromBase64String(formPlayReadyExternalServer.PlayReadyKeySeed), keyid);
                                                        contentKey = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context, keyid, bytecontentkey);
                                                    }
                                                    else // no seed given, so content key has been setup
                                                    {
                                                        contentKey = DynamicEncryption.CreateCommonTypeContentKey(AssetToProcess, _context, (Guid)formPlayReadyExternalServer.PlayReadyKeyId, Convert.FromBase64String(formPlayReadyExternalServer.PlayReadyContentKey));
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            // Add useful information to the exception
                                            TextBoxLogWriteLine("There is a problem when creating the content key for '{0}'.", AssetToProcess.Name, true);
                                            TextBoxLogWriteLine(e);
                                            Error = true;
                                        }
                                        if (Error) break;
                                        TextBoxLogWriteLine("Created key {0} for the asset {1} ", contentKey.Id, AssetToProcess.Name);
                                    }
                                    else if (form.GetKeyRestrictionType == null)  // user wants to deliver with an external PlayReady server but the key exists already !
                                    {
                                        TextBoxLogWriteLine("Warning for asset '{0}'. A CENC key already exists. You need to make sure that your external PlayReady server can deliver the license for this asset.", AssetToProcess.Name, true);
                                    }

                                    else // let's use existing content key
                                    {
                                        contentKey = contentkeys.FirstOrDefault();
                                        TextBoxLogWriteLine("Existing key {0} will be used for asset {1}.", contentKey.Id, AssetToProcess.Name);
                                    }


                                    // if CENC, let's build the PlayReady license template
                                    if (form.GetContentKeyType == ContentKeyType.CommonEncryption)
                                    {
                                        if (form.GetKeyRestrictionType != null) // PlayReady license and delivery from Azure Media Services
                                        {
                                            try
                                            {
                                                keydeliveryconfig = DynamicEncryption.ConfigurePlayReadyLicenseTemplate(formPlayReadyLicense.GetLicenseTemplate);
                                            }
                                            catch (Exception e)
                                            {
                                                // Add useful information to the exception
                                                TextBoxLogWriteLine("There is a problem when configuring the PlayReady license template.", true);
                                                TextBoxLogWriteLine(e);
                                                Error = true;
                                            }

                                        }
                                        else // PlayReady license but delivery from an external PlayReady server
                                        {

                                        }

                                    }

                                    string tokenTemplateString = null;
                                    try
                                    {
                                        switch (form.GetKeyRestrictionType)
                                        {
                                            case ContentKeyRestrictionType.Open:

                                                IContentKeyAuthorizationPolicy pol = DynamicEncryption.AddOpenAuthorizationPolicy(contentKey, (form.GetContentKeyType == ContentKeyType.EnvelopeEncryption) ? ContentKeyDeliveryType.BaselineHttp : ContentKeyDeliveryType.PlayReadyLicense, keydeliveryconfig, _context);
                                                TextBoxLogWriteLine("Created Open authorization policy for the asset {0} ", contentKey.Id, AssetToProcess.Name);
                                                break;

                                            case ContentKeyRestrictionType.TokenRestricted:


                                                if (form.GetDeliveryPolicyType == AssetDeliveryPolicyType.DynamicCommonEncryption) // CENC
                                                {
                                                    tokenTemplateString = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyPlayReady(contentKey, form.GetAudienceUri, form.GetIssuerUri, _context, keydeliveryconfig);
                                                    TextBoxLogWriteLine("Created Token CENC authorization policy for the asset {0} ", contentKey.Id, AssetToProcess.Name);
                                                }
                                                else  // Envelope encryption 
                                                {
                                                    tokenTemplateString = DynamicEncryption.AddTokenRestrictedAuthorizationPolicyAES(contentKey, form.GetAudienceUri, form.GetIssuerUri, _context);
                                                    TextBoxLogWriteLine("Created Token AES authorization policy for the asset {0} ", contentKey.Id, AssetToProcess.Name);
                                                }

                                                break;

                                            case null:
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
                                    if (Error) break;


                                    // Let's create the Asset Delivery Policy now
                                    IAssetDeliveryPolicy DelPol = null;
                                    string name = string.Format("AssetDeliveryPolicy {0} ({1})", form.GetContentKeyType.ToString(), form.GetAssetDeliveryProtocol.ToString());
                                    try
                                    {
                                        if (form.GetDeliveryPolicyType == AssetDeliveryPolicyType.DynamicCommonEncryption) // CENC
                                        {
                                            if (form.GetKeyRestrictionType != null) // Licenses delivered by Azure Media Services
                                            {
                                                DelPol = DynamicEncryption.CreateAssetDeliveryPolicyCENC(AssetToProcess, contentKey, form.GetAssetDeliveryProtocol, name, _context);
                                            }
                                            else // Licenses NOT delivered by Azure Media Services
                                            {
                                                DelPol = DynamicEncryption.CreateAssetDeliveryPolicyCENC(AssetToProcess, contentKey, form.GetAssetDeliveryProtocol, name, _context, new Uri(formPlayReadyExternalServer.PlayReadyLAurl));
                                            }

                                        }
                                        else  // Envelope encryption or no encryption
                                        {
                                            DelPol = DynamicEncryption.CreateAssetDeliveryPolicyAES(AssetToProcess, contentKey, form.GetAssetDeliveryProtocol, name, _context);
                                        }

                                        TextBoxLogWriteLine("Created asset delivery policy {0} for asset {1}.", DelPol.AssetDeliveryPolicyType, AssetToProcess.Name);
                                    }
                                    catch (Exception e)
                                    {
                                        TextBoxLogWriteLine("There is a problem when creating the delivery policy for '{0}'.", AssetToProcess.Name, true);
                                        TextBoxLogWriteLine(e);
                                        Error = true;
                                    }

                                    if (Error) break;

                                    if (!String.IsNullOrEmpty(tokenTemplateString))
                                    {
                                        string testToken = AssetInfo.GetTestToken(AssetToProcess, form.GetContentKeyType, _context);
                                        TextBoxLogWriteLine("The authorization test token (without Bearer) is:\n{0}", testToken);
                                        TextBoxLogWriteLine("The authorization test token (with Bearer) is:\n{0}", Constants.Bearer + testToken);
                                    }
                                }
                                else // No Dynamic encryption
                                {
                                    IAssetDeliveryPolicy DelPol = null;

                                    var DelPols = _context.AssetDeliveryPolicies
                                       .Where(p => (p.AssetDeliveryProtocol == form.GetAssetDeliveryProtocol) && (p.AssetDeliveryPolicyType == AssetDeliveryPolicyType.NoDynamicEncryption));
                                    if (DelPols.Count() == 0) // no delivery policy found or user want to force creation
                                    {
                                        try
                                        {
                                            DelPol = DynamicEncryption.CreateAssetDeliveryPolicyNoDynEnc(AssetToProcess, form.GetAssetDeliveryProtocol, _context);
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

                                    if (Error) break;
                                }

                            }

                        dataGridViewAssetsV.AnalyzeItemsInBackground();
                    }
                }
            }
            return oktoproceed;
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
                System.Windows.Forms.Clipboard.SetText(richTextBoxLog.SelectedText);
            }
            else
            {
                System.Windows.Forms.Clipboard.SetText(richTextBoxLog.Text);
            }

        }

        private void mergeSelectedAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMergeAssetsToNewAsset();
        }

        private void mergeAssetsToANewAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMergeAssetsToNewAsset();
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
                labelAssetName = string.Format("Dynamic encryption policies will be removed for asset '{0}'.", SelectedAssets.FirstOrDefault().Name);
                if (SelectedAssets.Count > 1)
                {
                    labelAssetName = string.Format("Dynamic encryption policies will removed for these {0} selected assets.", SelectedAssets.Count.ToString());
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
                            try
                            {
                                //Removing all locators associated with asset
                                var tasks = _context.Locators.Where(c => c.AssetId == AssetToProcess.Id && c.Type == LocatorType.OnDemandOrigin)
                                        .ToList()
                                        .Select(locator => locator.DeleteAsync())
                                        .ToArray();
                                Task.WaitAll(tasks);

                                //Removing all delivery policies associated with asset
                                List<IAssetDeliveryPolicy> items = AssetToProcess.DeliveryPolicies.ToList(); // let's do a copy of the list in order to do a removal
                                foreach (var item in items)
                                {
                                    AssetToProcess.DeliveryPolicies.Remove(item);
                                }


                                if (myDialogResult == DialogResult.Yes) // Let's delete the policies
                                {
                                    Task<IMediaDataServiceResponse>[] deleteTasks = _context.ContentKeyAuthorizationPolicies.Where(c => c.Name == AssetToProcess.Id).ToList().Select(policy => policy.DeleteAsync()).ToArray();
                                    Task.WaitAll(deleteTasks);

                                    deleteTasks = _context.ContentKeyAuthorizationPolicyOptions.Where(c => c.Name == AssetToProcess.Id).ToList().Select(policyOption => policyOption.DeleteAsync()).ToArray();
                                    Task.WaitAll(deleteTasks);


                                    /* // Code removed as it will delete also storage encryption key !
                                    //removing all content keys associated with assets
                                    for (int j = 0; j < AssetToProcess.ContentKeys.Count; j++)
                                    {
                                        AssetToProcess.ContentKeys.RemoveAt(0);
                                    }
                                     */
                                }
                            }

                            catch (Exception e)
                            {
                                // Add useful information to the exception
                                TextBoxLogWriteLine("There is a problem when deleting the delivery policy or locator for '{0}'.", AssetToProcess.Name, true);
                                TextBoxLogWriteLine(e);
                            }

                            if (Error) break;
                            TextBoxLogWriteLine("Removed{0} asset delivery policies and locator(s) for asset {1}.", (myDialogResult == DialogResult.Yes) ? " and deleted" : string.Empty, AssetToProcess.Name);

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


        private void encodeAssetsWithZeniumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoMenuEncodeWithZenium();
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

        private void copyTheOutputURLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopyOutputURLToClipboard();

        }

        private void DoCopyOutputURLToClipboard()
        {
            IProgram program = ReturnSelectedPrograms().FirstOrDefault();
            if (program != null)
            {
                ProgramInfo PI = new ProgramInfo(program, _context);
                IEnumerable<Uri> ValidURIs = PI.GetValidURIs();
                if (ValidURIs.FirstOrDefault() != null)
                {
                    System.Windows.Forms.Clipboard.SetText(ValidURIs.FirstOrDefault().ToString());
                }
            }
        }

        private void withDASHLiveAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoPlaybackProgram(PlayerType.DASHLiveAzure);
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
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }

        private void withCustomPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri(), _context);
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
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }

        private void withDASHLiveAzurePlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.DASHLiveAzure, PlayBackLocator.GetMpegDashUri(), _context);

        }

        private void withCustomPlayerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedPrograms().FirstOrDefault().Asset, ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.CustomPlayer, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }

        private void displayRelatedAssetInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoMenuDisplayAssetInfoOfProgram();
        }

        private void withMPEGDASHAzurePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.DASHAzurePage, PlayBackLocator.GetSmoothStreamingUri(), _context);
        }

        private void withDASHLiveAzurePlayerToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.DASHLiveAzure, PlayBackLocator.GetMpegDashUri(), _context);
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
            DoRefreshGridAssetV(false);
        }

        private void refreshToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoRefreshGridJobV(false);
        }

        private void refreshToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoRefreshGridChannelV(false);
        }

        private void refreshToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DoRefreshGridProgramV(false);
        }

        private void refreshToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DoRefreshGridStreamingEndpointV(false);
        }

        private void refreshToolStripMenuItem6_Click(object sender, EventArgs e)
        {
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
            AttachStorage form = new AttachStorage();

            if (form.ShowDialog() == DialogResult.OK)
            {

                ManagementRESTAPIHelper helper = new ManagementRESTAPIHelper("https://management.core.windows.net", form.GetCertThumbprint, form.GetAzureSubscriptionID);

                // Initialize the AccountInfo class.
                MediaServicesAccount accountInfo = new MediaServicesAccount();
                accountInfo.AccountName = _context.Credentials.ClientId;

                //accountInfo.Region = "";
                accountInfo.StorageAccountName = _context.DefaultStorageAccount.Name;
                //accountInfo.StorageAccountKey = _credentials.StorageKey;
                //accountInfo.BlobStorageEndpointUri = _context.DefaultStorageAccount.;


                AttachStorageAccountRequest storageAccountToAttach = new AttachStorageAccountRequest();
                storageAccountToAttach.StorageAccountName = form.GetStorageName;
                storageAccountToAttach.StorageAccountKey = form.GetStorageKey;
                storageAccountToAttach.BlobStorageEndpointUri = form.GetStorageEndpoint;

                // Call CreateMediaServiceAccountUsingXmlContentType to create a new \
                // Media Services account. 
                //helper.CreateMediaServiceAccountUsingXmlContentType(accountInfo);

                // Call AttachStorageAccountToMediaServiceAccount to 
                // attach an existing storage account to the Media Services account.
                try
                {
                    helper.AttachStorageAccountToMediaServiceAccount(accountInfo,
                                                   storageAccountToAttach);
                    TextBoxLogWriteLine("Storage account '{0}' attached to '{1}' account.", form.GetStorageName, _context.Credentials.ClientId);
                }
                catch (Exception ex)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There is a problem when attaching the storage account.", true);
                    TextBoxLogWriteLine(ex);

                }


                // Call the following methods to get details about 
                // Media Services account.
                //helper.GetAccountDetails(accountInfo);
                //helper.ListAvailableRegions(accountInfo);
                //helper.ListSubscriptionAccounts(accountInfo);
                //helper.ListSubscriptionAccounts(accountInfo);

            }
        }


        private void addADynamicEncryptionPolicyForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSetupDynEnc();
        }

        private void removeAllDynamicEncryptionPoliciesForTheAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRemoveDynEnc();
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
            Process.Start(@"https://manage.windowsazure.com");
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
            if (SelectedJobs.FirstOrDefault().Tasks.Count != 1)
            {
                MessageBox.Show("This feature works only with a job that contains a single task");
                return;
            }

            string taskname = myJob.Tasks.FirstOrDefault().Name; //Constants.NameconvProcessorname + " processing of " + Constants.NameconvInputasset;

            GenericProcessor form = new GenericProcessor(_context, myJob)
            {
                Text = "Job re-submission",
                EncodingProcessorsList = _context.MediaProcessors.ToList().OrderBy(p => p.Vendor).ThenBy(p => p.Name).ThenBy(p => new Version(p.Version)).ToList(),
                EncodingJobName = string.Format("{0} (resubmitted on {1})", myJob.Name, DateTime.Now.ToString()), // Constants.NameconvProcessorname + " processing of " + Constants.NameconvInputasset,
                EncodingOutputAssetName = string.Format("{0} (resubmitted on {1})", myJob.OutputMediaAssets.FirstOrDefault().Name, DateTime.Now.ToString()), // Constants.NameconvInputasset + "-" + Constants.NameconvProcessorname + " processed",
                EncodingPriority = myJob.Priority,
                SelectedAssets = myJob.InputMediaAssets.ToList(),
                EncodingCreationMode = TaskJobCreationMode.SingleTask_SingleJob,
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string jobnameloc = form.EncodingJobName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name); ;
                IJob job = _context.Jobs.Create(jobnameloc, form.EncodingPriority);

                string tasknameloc = taskname.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);

                ITask task = job.Tasks.AddNew(
                            tasknameloc,
                           form.EncodingProcessorSelected,
                           form.EncodingConfiguration,
                           Properties.Settings.Default.useProtectedConfiguration ? TaskOptions.ProtectedConfiguration : TaskOptions.None);
                // Specify the graph asset to be encoded, followed by the input video asset to be used
                task.InputAssets.AddRange(myJob.InputMediaAssets.ToList());
                string outputassetnameloc = form.EncodingOutputAssetName.Replace(Constants.NameconvInputasset, "multiple assets").Replace(Constants.NameconvProcessorname, form.EncodingProcessorSelected.Name);
                task.OutputAssets.AddNew(outputassetnameloc, form.StorageSelected, Properties.Settings.Default.useStorageEncryption ? AssetCreationOptions.StorageEncrypted : AssetCreationOptions.None);

                TextBoxLogWriteLine("Submitting encoding job '{0}'", jobnameloc);
                // Submit the job and wait until it is completed. 
                try
                {
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    TextBoxLogWriteLine("There has been a problem when submitting the job '{0}'", jobnameloc, true);
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
                if (InputBox("Save as job template", "Job template name:", ref jobtemplatename) == DialogResult.OK)
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
                ProcessingPriority = Properties.Settings.Default.DefaultJobPriority
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                string jobname = form.ProcessingJobName.Replace(Constants.NameconvTemplate, form.SelectedJobTemplate.Name);
                string assetname = (SelectedAssets.Count == 1) ? SelectedAssets.FirstOrDefault().Name : "Multiple assets";
                jobname = jobname.Replace(Constants.NameconvInputasset, assetname);

                // Submit the job
                try
                {
                    IJob job = _context.Jobs.Create(jobname, form.SelectedJobTemplate, SelectedAssets, form.ProcessingPriority);
                    job.Submit();
                }
                catch (Exception e)
                {
                    // Add useful information to the exception
                    MessageBox.Show(string.Format("There has been a problem when submitting the job '{0}'", jobname) + Constants.endline + e.Message);
                    TextBoxLogWriteLine("There has been a problem when submitting the job {0}", jobname, true);
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

        private void copyInputURLSSLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopySSLIngestURLToClipboard();
        }

        private void DoCopySSLIngestURLToClipboard()
        {
            IChannel channel = ReturnSelectedChannels().FirstOrDefault();
            if (channel.Input.StreamingProtocol == StreamingProtocol.FragmentedMP4)
            {
                System.Windows.Forms.Clipboard.SetText(ReturnSelectedChannels().FirstOrDefault().Input.Endpoints.FirstOrDefault().Url.AbsoluteUri.ToString().Replace("http://", "https://"));
            }
            else
            {
                MessageBox.Show("SSL is not yet possible for live RTMP input.", "SSL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyInputSSLURLToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoCopySSLIngestURLToClipboard();
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
            DoPlaybackProgram(PlayerType.FlashAESToken);

        }

        private void withSilverlightPlayReadyTokenPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DoPlaybackProgram(PlayerType.SilverlightPlayReadyToken);

        }

        private void withFlashTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.FlashAESToken, PlayBackLocator.GetSmoothStreamingUri(), _context, ReturnSelectedAssets().FirstOrDefault());

        }

        private void withSilverlightPlayReadyTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.SilverlightPlayReadyToken, PlayBackLocator.GetSmoothStreamingUri(), _context, ReturnSelectedAssets().FirstOrDefault());
        }

        private void flashSmoothStreamingAESTokenPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://aestoken.azurewebsites.net");
        }

        private void withFlashAESTokenPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.FlashAESToken, PlayBackLocator.GetSmoothStreamingUri(), _context, ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault());
        }

        private void withSilverlightPlayReadyTokenPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.SilverlightPlayReadyToken, PlayBackLocator.GetSmoothStreamingUri(), _context, ReturnSelectedAssetsFromProgramsOrAssets().FirstOrDefault());
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
            DoRefreshGridStorageV(false);
        }

        private void attachAnotherStorageAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoAttachAnotherStorageAccount();
        }

        private void dataGridViewV_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            if (e.RowIndex % 2 == 0)
            {
                foreach (DataGridViewCell c in ((DataGridView)sender).Rows[e.RowIndex].Cells) c.Style.BackColor = Color.AliceBlue;
            }
        }

        private void dataGridViewAssetsV_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }

        private void withAzureMediaPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.AzureMediaPlayer, PlayBackLocator.GetSmoothStreamingUri(), _context, ReturnSelectedAssets().FirstOrDefault());

        }

        private void withAzureMediaPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsAssetCanBePlayed(ReturnSelectedAssets().FirstOrDefault(), ref PlayBackLocator))
                AssetInfo.DoPlayBackWithBestStreamingEndpoint(PlayerType.AzureMediaPlayer, PlayBackLocator.GetSmoothStreamingUri(), _context, ReturnSelectedAssets().FirstOrDefault());

        }

        private void withAzureMediaPlayerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DoPlaybackProgram(PlayerType.AzureMediaPlayer);
        }

        private void withAzureMediaPlayerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.AzureMediaPlayer);
        }

        private void withAzureMediaPlayerToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DoPlaybackChannelPreview(PlayerType.AzureMediaPlayer);
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
        ExportToAzureStorage = 4,
        DownloadToLocal = 5
    }


    public class DataGridViewAssets : DataGridView
    {
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
        public string _statEnc = "StaticEncryption";
        public string _publication = "Publication";
        public string _dynEnc = "DynamicEncryption";
        public string _statEncMouseOver = "StaticEncryptionMouseOver";
        public string _publicationMouseOver = "PublicationMouseOver";
        public string _dynEncMouseOver = "DynamicEncryptionMouseOver";

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
        static Bitmap cancelimage = Bitmaps.cancel;
        static Bitmap envelopeencryptedimage = Bitmaps.envelope_encryption;
        static Bitmap storageencryptedimage = Bitmaps.storage_encryption;
        static Bitmap storagedecryptedimage = Bitmaps.storage_decryption;
        static Bitmap commonencryptedimage = Bitmaps.DRM_protection;
        static Bitmap unsupportedencryptedimage = Bitmaps.help;
        static Bitmap SASlocatorimage = Bitmaps.SAS_locator;
        static Bitmap Streaminglocatorimage = Bitmaps.streaming_locator;
        static Bitmap Redstreamimage = MakeRed(Streaminglocatorimage);
        static Bitmap Reddownloadimage = MakeRed(SASlocatorimage);
        static Bitmap Bluestreamimage = MakeBlue(Streaminglocatorimage);
        static Bitmap Bluedownloadimage = MakeBlue(SASlocatorimage);

        public void Init(CloudMediaContext context)
        {
            Debug.WriteLine("AssetsInit");

            IEnumerable<AssetEntry> assetquery;
            _context = context;

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

            BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(assetquery.Take(0).ToList()); // just to create columns
            this.DataSource = MyObservAssethisPage;

            int lastColumn_sIndex = this.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).DisplayIndex;
            this.Columns[_statEncMouseOver].Visible = false;
            this.Columns[_dynEncMouseOver].Visible = false;
            this.Columns[_publicationMouseOver].Visible = false;

            this.Columns["Type"].HeaderText = "Type (streams nb)";
            this.Columns["LastModified"].HeaderText = "Last modified";
            this.Columns["Id"].Visible = Properties.Settings.Default.DisplayAssetIDinGrid;
            this.Columns["Storage"].Visible = Properties.Settings.Default.DisplayAssetStorageinGrid;
            this.Columns["SizeLong"].Visible = false;
            this.Columns[_publication].DisplayIndex = lastColumn_sIndex;
            this.Columns[_publication].DefaultCellStyle.NullValue = null;
            this.Columns[_dynEnc].DisplayIndex = lastColumn_sIndex - 1;
            this.Columns[_dynEnc].DefaultCellStyle.NullValue = null;
            this.Columns[_statEnc].DisplayIndex = lastColumn_sIndex - 2;
            this.Columns[_statEnc].DefaultCellStyle.NullValue = null;

            this.Columns[_statEnc].HeaderText = "Static Encryption";
            this.Columns[_dynEnc].HeaderText = "Dynamic Encryption";
            this.Columns["Size"].Width = 70;
            this.Columns[_statEnc].Width = 70;
            this.Columns[_dynEnc].Width = 70;
            this.Columns[_publication].Width = 70;

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
                        AssetInfo AR = new AssetInfo(asset);

                        SASLoc = AR.GetPublishedStatus(LocatorType.Sas);
                        OrigLoc = AR.GetPublishedStatus(LocatorType.OnDemandOrigin);
                        AssetBitmapAndText ABR = ReturnStaticProtectedBitmap(asset);
                        AE.StaticEncryption = ABR.bitmap;
                        AE.StaticEncryptionMouseOver = ABR.MouseOverDesc;
                        ABR = BuildBitmapPublication(SASLoc, OrigLoc);
                        AE.Publication = ABR.bitmap;
                        AE.PublicationMouseOver = ABR.MouseOverDesc;
                        AE.Type = AssetInfo.GetAssetType(asset);
                        AE.SizeLong = AR.GetSize();
                        AE.Size = AssetInfo.FormatByteSize(AE.SizeLong);
                        ABR = BuildBitmapDynEncryption(asset);
                        AE.DynamicEncryption = ABR.bitmap;
                        AE.DynamicEncryptionMouseOver = ABR.MouseOverDesc;
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


            //  Task.Run(() =>
            // {
            IEnumerable<IAsset> assets;
            IEnumerable<AssetEntry> assetquery;

            int days = -1;
            if (!string.IsNullOrEmpty(_timefilter))
            {
                switch (_timefilter)
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
            assets = (days == -1) ? context.Assets : context.Assets.Where(a => (a.LastModified > (DateTime.UtcNow.Add(-TimeSpan.FromDays(days)))));


            if (!string.IsNullOrEmpty(_searchinname))
            {
                string searchlower = _searchinname.ToLower();
                assets = assets.Where(a => (a.Name.ToLower().Contains(searchlower) || a.Id.ToLower().Contains(searchlower)));
            }

            if ((!string.IsNullOrEmpty(_statefilter)) && _statefilter != StatusAssets.All)
            {
                switch (_statefilter)
                {
                    case StatusAssets.Published:
                        assets = assets.Where(a => a.Locators.Count > 0);
                        break;
                    case StatusAssets.PublishedExpired:
                        assets = assets.Where(a => a.Locators.Count > 0).Where(a => a.Locators.All(l => l.ExpirationDateTime < DateTime.UtcNow));
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
                        assets = assets.Where(a => a.DeliveryPolicies.Count > 0);
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
                assetquery = from a in assets select new AssetEntry { Name = a.Name, Id = a.Id, Type = null, LastModified = ((DateTime)a.LastModified).ToLocalTime(), Storage = a.StorageAccountName };
                _MyObservAsset = new BindingList<AssetEntry>(assetquery.ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + Constants.endline + e.Message);
                Environment.Exit(0);
            }

            BindingList<AssetEntry> MyObservAssethisPage = new BindingList<AssetEntry>(_MyObservAsset.Skip(_assetsperpage * (_currentpage - 1)).Take(_assetsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = MyObservAssethisPage));
            _refreshedatleastonetime = true;

            AnalyzeItemsInBackground();

            //  });
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



        private static AssetBitmapAndText BuildBitmapPublication(PublishStatus SASPub, PublishStatus OriginPub)
        {
            // optmized for speed
            AssetBitmapAndText ABT = new AssetBitmapAndText();

            if ((SASPub == PublishStatus.NotPublished) && (OriginPub == PublishStatus.NotPublished)) // IF NOT PUBLISHED
            {
                ABT.MouseOverDesc = "Not published";
                return ABT;
            }


            Bitmap MyPublishedImage;
            Bitmap streami = null;
            Bitmap downloadi = null;
            string streams = string.Empty;
            string downloads = string.Empty;

            switch (SASPub)
            {
                case PublishStatus.PublishedActive:
                    downloadi = SASlocatorimage;
                    downloads = "Active SAS locator";
                    break;

                case PublishStatus.PublishedExpired:
                    downloadi = Reddownloadimage;
                    downloads = "Expired SAS locator";
                    break;

                case PublishStatus.PublishedFuture:
                    downloadi = Bluedownloadimage;
                    downloads = "Future SAS locator";
                    break;

                case PublishStatus.NotPublished:
                    downloadi = null;
                    break;

            }

            switch (OriginPub)
            {
                case PublishStatus.PublishedActive:
                    streami = Streaminglocatorimage;
                    streams = "Active Streaming locator";
                    break;

                case PublishStatus.PublishedExpired:
                    streami = Redstreamimage;
                    streams = "Expired Streaming locator";
                    break;

                case PublishStatus.PublishedFuture:
                    streami = Bluestreamimage;
                    streams = "Future Streaming locator";
                    break;

                case PublishStatus.NotPublished:
                    streami = null;

                    break;
            }

            // IF BOTH PUBLISHED
            if ((SASPub != PublishStatus.NotPublished) && (OriginPub != PublishStatus.NotPublished)) // SAS and Origin
            {
                MyPublishedImage = new Bitmap((downloadi.Width + streami.Width), streami.Height);
                using (Graphics graphicsObject = Graphics.FromImage(MyPublishedImage))
                {
                    graphicsObject.DrawImage(downloadi, new Point(0, 0));
                    graphicsObject.DrawImage(streami, new Point(downloadi.Width, 0));
                }
            }
            else //only one published
            {
                MyPublishedImage = (SASPub != PublishStatus.NotPublished) ? downloadi : streami;
            }
            ABT.bitmap = MyPublishedImage;
            ABT.MouseOverDesc = downloads + (string.IsNullOrEmpty(downloads) ? string.Empty : Constants.endline) + streams;
            return ABT;
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

        public void Init(CredentialsEntry credentials)
        {
            IEnumerable<JobEntry> jobquery;
            _credentials = credentials;

            _context = Program.ConnectAndGetNewContext(_credentials);
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
                               Progress = j.GetOverallProgress()
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
            this.Columns["Tasks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Tasks"].Width = 50;
            this.Columns["Priority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Columns["Priority"].Width = 50;

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
            if (!_initialized) return;

            this.FindForm().Cursor = Cursors.WaitCursor;
            _context = context;

            IEnumerable<JobEntry> jobquery;

            int days = -1;
            if (!string.IsNullOrEmpty(_timefilter))
            {
                switch (_timefilter)
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
                               Progress = j.GetOverallProgress()
                           };
                _MyObservJob = new BindingList<JobEntry>(jobquery.ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show("There is a problem when connecting to Azure Media Services. Application will close. " + Constants.endline + e.Message);
                Environment.Exit(0);
            }

            _MyObservAssethisPage = new BindingList<JobEntry>(_MyObservJob.Skip(_jobsperpage * (_currentpage - 1)).Take(_jobsperpage).ToList());
            this.BeginInvoke(new Action(() => this.DataSource = _MyObservAssethisPage));
            _refreshedatleastonetime = true;
            this.FindForm().Cursor = Cursors.Default;
        }

        // Used to restore job progress. 2 cases: when app is launched or when a job has been created by an external program
        public void RestoreJobProgress()  // when app is launched, we want to restore job progress updates
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

                               if ((JobRefreshed.State == JobState.Processing || JobRefreshed.State == JobState.Queued)) // in progress
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
                               else // no progress anymore (finished or failed)
                               {
                                   double progress = JobRefreshed.GetOverallProgress();
                                   _MyObservJob[index].Duration = JobRefreshed.StartTime.HasValue ? ((TimeSpan)(DateTime.UtcNow - JobRefreshed.StartTime)).ToString(@"d\.hh\:mm\:ss") : null;
                                   _MyObservJob[index].Progress = progress;
                                   _MyObservJob[index].Priority = JobRefreshed.Priority;
                                   _MyObservJob[index].StartTime = JobRefreshed.StartTime.HasValue ? (Nullable<DateTime>)((DateTime)JobRefreshed.StartTime).ToLocalTime() : null;
                                   _MyObservJob[index].EndTime = JobRefreshed.EndTime.HasValue ? ((DateTime)JobRefreshed.EndTime).ToLocalTime().ToString() : null;
                                   _MyObservJob[index].State = JobRefreshed.State;
                                   _MyListJobsMonitored.Remove(JobRefreshed.Id); // let's removed from the list of monitored jobs
                                   this.BeginInvoke(new Action(() =>
                                   {
                                       this.Refresh();
                                   }));
                               }
                           }
                       },
                       CancellationToken.None).Result;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Job Monitoring Error");
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