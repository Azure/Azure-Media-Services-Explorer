
//----------------------------------------------------------------------- 
// <copyright file="Program.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MediaServices.Client;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Drawing;
using System.Diagnostics;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Xml.Linq;
using System.Runtime.ExceptionServices;
using System.Collections;
using System.Reflection;


namespace AMSExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Mainform());
        }

        public static CloudMediaContext ConnectAndGetNewContext(CredentialsEntry credentials)
        {
            CloudMediaContext myContext = null;
            if (credentials.UsePartnerAPI == true.ToString())
            {
                // Get the service context for partner context.
                try
                {
                    Uri partnerAPIServer = new Uri(CredentialsEntry.PartnerAPIServer);
                    myContext = new CloudMediaContext(partnerAPIServer, credentials.AccountName, credentials.AccountKey, CredentialsEntry.PartnerScope, CredentialsEntry.PartnerACSBaseAddress);
                }
                catch (Exception e)
                {
                    MessageBox.Show("There is a credentials problem when connecting to Azure Media Services (custom API)." + Constants.endline + "Application will close. " + Constants.endline + e.Message);
                    Environment.Exit(0);
                }
            }
            else if (credentials.UseOtherAPI == true.ToString())
            {
                try
                {
                    Uri otherAPIServer = new Uri(credentials.OtherAPIServer);
                    myContext = new CloudMediaContext(otherAPIServer, credentials.AccountName, credentials.AccountKey, credentials.OtherScope, credentials.OtherACSBaseAddress);
                }
                catch (Exception e)
                {
                    MessageBox.Show("There is a credentials problem when connecting to Azure Media Services (Partner API)." + Constants.endline + "Application will close." + Constants.endline + e.Message);
                    Environment.Exit(0);
                }
            }
            else
            {
                // Get the service context.
                try
                {
                    myContext = new CloudMediaContext(credentials.AccountName, credentials.AccountKey);
                }
                catch (Exception e)
                {
                    MessageBox.Show("There is a credentials problem when connecting to Azure Media Services." + Constants.endline + "Application will close." + Constants.endline + e.Message);
                    Environment.Exit(0);
                }
            }
            try { myContext.Credentials.RefreshToken(); } // to force connection to WAMS
            catch (Exception e)
            {
                // Add useful information to the exception
                MessageBox.Show("There is a credentials problem when connecting to Azure Media Services." + Constants.endline + "Application will close." + Constants.endline + e.Message);
                Environment.Exit(0);
            }
            return myContext;
        }

        public static string GetAPIServer(CredentialsEntry credentials)
        {
            if (credentials.UsePartnerAPI == true.ToString())
            {
                return CredentialsEntry.PartnerAPIServer;
            }
            else if (credentials.UseOtherAPI == true.ToString())
            {
                return credentials.OtherAPIServer;
            }
            else
            {
                return Constants.ProdAPIServer;
            }
        }

        public static string GetACSBaseAddress(CredentialsEntry credentials)
        {
            if (credentials.UsePartnerAPI == true.ToString())
            {
                return CredentialsEntry.PartnerACSBaseAddress;
            }
            else if (credentials.UseOtherAPI == true.ToString())
            {
                return credentials.OtherACSBaseAddress;
            }
            else
            {
                return Constants.ProdACSBaseAddress;
            }
        }


        public static bool ConnectToStorage(CloudMediaContext context, CredentialsEntry credentials)
        {
            try
            {
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(context.DefaultStorageAccount.Name, credentials.StorageKey), credentials.ReturnStorageSuffix(), true);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("There is a problem when connecting to the Azure storage account.\r\nIs the storage key correct ?\r\n{0}", e.Message), "Storage Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static string GetErrorMessage(Exception e)
        {
            string s = "";

            while (e != null)
            {
                s = e.Message;
                e = e.InnerException;
            }
            return ParseXml(s);
        }

        public static string ParseXml(string strXml)
        {
            try
            {
                var message = XDocument
                    .Parse(strXml)
                    .Descendants()
                    .Where(d => d.Name.LocalName == "message")
                    .Select(d => d.Value)
                    .SingleOrDefault();

                return message;
            }
            catch
            {
                return strXml;
            }
        }

        public static string MessageNewVersion = string.Empty;

        public static void CheckAMSEVersion()
        {
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += DownloadVersionRequestCompleted;
            webClient.DownloadStringAsync(new Uri(Constants.GitHubAMSEVersion));
        }

        public static void DownloadVersionRequestCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    var xmlversion = XDocument.Parse(e.Result);
                    Version versionAMSEGitHub = new Version(xmlversion.Descendants("Versions").Descendants("Production").Attributes("Version").FirstOrDefault().Value.ToString());
                    Version versionAMSELocal = Assembly.GetExecutingAssembly().GetName().Version;
                    if (versionAMSEGitHub > versionAMSELocal)
                    {
                        MessageNewVersion = string.Format("A new version ({0}) is available on GitHub: {1}", versionAMSEGitHub, Constants.GitHubAMSEReleases);
                        if (MessageBox.Show(string.Format("A new version of Azure Media Services Explorer ({0}) is available." + Constants.endline + "Would you like to download it ?", versionAMSEGitHub), "Update available", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        { // user selected yes
                            System.Diagnostics.Process.Start(Constants.GitHubAMSELink);
                            Environment.Exit(0);
                        }
                    }
                }
                catch
                {

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

        public static void SaveAndProtectUserConfig()
        {
            try
            {
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {

            }

            /*
            try
            {
                string assemblyname = Assembly.GetExecutingAssembly().GetName().Name;
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                ConfigurationSection connStrings = config.GetSection("userSettings/" + assemblyname + ".Properties.Settings");

                if (connStrings != null)
                {
                    if (!connStrings.SectionInformation.IsProtected)
                    {
                        if (!connStrings.ElementInformation.IsLocked)
                        {
                            connStrings.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                            connStrings.SectionInformation.ForceSave = true;
                            config.Save(ConfigurationSaveMode.Full);
                        }
                    }
                }
            }


            catch (Exception e)
            {
                MessageBox.Show("Step2 " + e.Message);
            }
             * */


            // let's decrypt as encryption create issues with some users
            try
            {
                string assemblyname = Assembly.GetExecutingAssembly().GetName().Name;
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                ConfigurationSection connStrings = config.GetSection("userSettings/" + assemblyname + ".Properties.Settings");

                if (connStrings != null)
                {
                    if (connStrings.SectionInformation.IsProtected)
                    {
                        if (!connStrings.ElementInformation.IsLocked)
                        {
                            connStrings.SectionInformation.UnprotectSection();
                            connStrings.SectionInformation.ForceSave = true;
                            config.Save(ConfigurationSaveMode.Full);
                        }
                    }
                }
            }


            catch (Exception e)
            {
                MessageBox.Show("Error " + e.Message);
            }

        }

    }

    public class Constants
    {
        public const string GitHubAMSEVersion = "https://raw.githubusercontent.com/Azure/Azure-Media-Services-Explorer/master/version.xml";
        public const string GitHubAMSEReleases = "https://github.com/Azure/Azure-Media-Services-Explorer/releases";
        public const string GitHubAMSELink = "http://aka.ms/amse";

        public const string WindowsAzureMediaEncoder = "Windows Azure Media Encoder";
        public const string AzureMediaEncoder = "Azure Media Encoder";
        public const string AzureMediaEncoderPremiumWorkflow = "Media Encoder Premium Workflow";
        public const string ZeniumEncoder = "Digital Rapids - Kayak Cloud Engine";
        public const string AzureMediaIndexer = "Azure Media Indexer";
        public const string NameconvInputasset = "{Input Asset Name}";
        public const string NameconvUploadasset = "{File Name}";
        public const string NameconvWorkflow = "{Workflow}";
        public const string NameconvTemplate = "{Template}";
        public const string NameconvAMEpreset = "{Preset}";
        public const string NameconvFormathls = "{Format}";
        public const string NameconvEncodername = "{Encoder}";
        public const string NameconvProcessorname = "{Processor}";
        public const string NameconvChannel = "{Channel}";
        public const string NameconvProgram = "{Program}";
        public const string NameconvProtocols = "{Protocols}";
        public const string NameconvContentKeyType = "{Content key type}";
        public const string NameconvManifestURL = "{manifest url}";

        public const string endline = "\r\n";

        public const string TabAssets = "Assets"; // name of the Assets tab
        public const string TabTransfers = "Transfers"; // name of the Transfers tab
        public const string TabJobs = "Jobs"; // name of the Jobs tab
        public const string TabLive = "Live"; // name of the Live tab
        public const string TabProcessors = "Processors"; // name of the Processors tab
        public const string TabOrigins = "Streaming endpoints"; // name of the Origins tab
        public const string TabStorage = "Storage"; // name of the Origins tab
        public const string TabLog = "Log"; // name of the Jobs tab

        public const string PathPremiumWorkflowFiles = @"\PremiumWorflowSamples\";
        public const string PathAMEFiles = @"\AMEPresetFiles\";
        public const string PathConfigFiles = @"\configurations\";
        public const string PathHelpFiles = @"\HelpFiles\";

        public const string PathLicense = @"\license\Azure Media Services Explorer.rtf";

        public const string AMSPlayer = @"http://amsplayer.azurewebsites.net/?player=flash&format=smooth&url=";

        public const string LocatorIdPrefix = "nb:lid:UUID:";
        public const string AssetIdPrefix = "nb:cid:UUID:";
        public const string ContentKeyIdPrefix = "nb:kid:UUID:";
        public const string JobIdPrefix = "nb:jid:UUID:";

        public const string ProdAPIServer = "https://media.windows.net";
        public const string ProdACSBaseAddress = "https://wamsprodglobal001acs.accesscontrol.windows.net";

        public const string Bearer = "Bearer ";
        public const string strUnits = "{0} unit{1}";
    }




    public class JobInfo
    {
        private List<IJob> SelectedJobs;

        public JobInfo(IJob job)
        {
            SelectedJobs = new List<IJob>();
            SelectedJobs.Add(job);

        }
        public JobInfo(List<IJob> MySelectedJobs)
        {
            SelectedJobs = MySelectedJobs;
        }

        public void CreateOutlookMail()
        {
            StringBuilder SB = GetStats();

            // Let's create the email with Outlook
            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
            if (SelectedJobs.Count == 1)
            {
                string title = (SelectedJobs.FirstOrDefault().State == JobState.Error) ? "ERROR Report: Job '{0}'" : "Report: Job '{0}'";

                mailItem.Subject = string.Format(title, SelectedJobs.FirstOrDefault().Name);

            }
            else
            {
                mailItem.Subject = string.Format("Report: {0} jobs, {1} Error(s)", SelectedJobs.Count(), SelectedJobs.Where(j => j.State == JobState.Error).Count());

            }

            mailItem.HTMLBody = "<FONT Face=\"Courier New\">";
            mailItem.HTMLBody += SB.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />").ToString();
            mailItem.Display(false);
        }

        public void CopyStatsToClipBoard()
        {
            StringBuilder SB = GetStats();
            Clipboard.SetText((string)SB.ToString());
        }

        public static long GetInputFilesSize(ITask task) // return -1 if one asset has been deleted
        {
            // Loop through the HistoricalEvents associated with the Task to find Tasks that have Finished
            // Task.State only has the Conpleted State and based on that it is not possible to know whether the task had an error or did it finish successfully
            long lSize = 0;
            bool sizecanbecalculated = false;
            if (task.State == JobState.Finished)
            {
                lSize = 0;
                sizecanbecalculated = true;
                foreach (IAsset asset in task.InputAssets)
                {
                    if (asset.State == AssetState.Deleted)
                    {
                        sizecanbecalculated = false;
                    }
                    else
                    {
                        foreach (IAssetFile fileItem in asset.AssetFiles)
                        {
                            lSize += fileItem.ContentFileSize;
                        }
                    }
                }
            }
            if (sizecanbecalculated)
            {
                return lSize;
            }
            else
            {
                return -1;
            }
        }

        public static long GetOutputFilesSize(ITask task) // return -1 if one asset has been deleted
        {
            // Loop through the HistoricalEvents associated with the Task to find Tasks that have Finished
            // Task.State only has the Conpleted State and based on that it is not possible to know whether the task had an error or did it finish successfully
            long lSize = 0;
            bool sizecanbecalculated = false;
            if (task.State == JobState.Finished)
            {
                lSize = 0;
                sizecanbecalculated = true;
                foreach (IAsset asset in task.OutputAssets)
                {
                    if (asset.State == AssetState.Deleted)
                    {
                        sizecanbecalculated = false;
                    }
                    else
                    {
                        foreach (IAssetFile fileItem in asset.AssetFiles)
                        {
                            lSize += fileItem.ContentFileSize;
                        }
                    }
                }
            }
            if (sizecanbecalculated)
            {
                return lSize;
            }
            else
            {
                return -1;
            }
        }

        public static TaskSizeAndPrice CalculateJobSizeAndPrice(IJob job)
        {
            long totalinputsize = 0;
            long totalouputsize = 0;
            double totalprice = 0;
            bool inputsizecanbecalculated = true;
            bool outputsizecanbecalculated = true;
            bool pricecanbecalculated = true;

            foreach (ITask task in job.Tasks)
            {
                TaskSizeAndPrice taskinfo = CalculateTaskSizeAndPrice(task, (CloudMediaContext)job.GetMediaContext());
                if (taskinfo.InputSize != -1)
                {
                    totalinputsize += taskinfo.InputSize;
                }
                else inputsizecanbecalculated = false;

                if (taskinfo.OutputSize != -1)
                {
                    totalouputsize += taskinfo.OutputSize;
                }
                else outputsizecanbecalculated = false;

                if (taskinfo.Price != -1)
                {
                    totalprice += taskinfo.Price;
                }
                else pricecanbecalculated = false;
            }

            return new TaskSizeAndPrice
            {
                InputSize = inputsizecanbecalculated ? totalinputsize : -1,
                OutputSize = outputsizecanbecalculated ? totalouputsize : -1,
                Price = pricecanbecalculated ? totalprice : -1
            };
        }

        public static TaskSizeAndPrice CalculateTaskSizeAndPrice(ITask task, CloudMediaContext context)
        {
            IMediaProcessor processor = JobInfo.GetMediaProcessorFromId(task.MediaProcessorId, context);
            StringBuilder sb = new StringBuilder();

            long lSizeinput = -1;
            long lSizeoutput = -1;
            double pricetask = -1;

            if (task.State == JobState.Finished)
            {
                lSizeinput = JobInfo.GetInputFilesSize(task);
                lSizeoutput = JobInfo.GetOutputFilesSize(task);

                if (lSizeinput != -1 && lSizeoutput != -1)
                {
                    double lsizeinputprocessed = (double)lSizeinput / (1024 * 1024 * 1024);
                    double lsizeoutputprocessed = (double)lSizeoutput / (1024 * 1024 * 1024);

                    if (processor != null)
                    {

                        switch (processor.Name)
                        {
                            case (Constants.AzureMediaEncoder):
                                // AME Encoding task
                                pricetask = lsizeoutputprocessed * (double)Properties.Settings.Default.AMEPrice;
                                break;
                            case (Constants.WindowsAzureMediaEncoder):
                                // WAME Encoding task
                                pricetask = (lsizeinputprocessed + lsizeoutputprocessed) * (double)Properties.Settings.Default.LegacyEncodingPrice;
                                break;
                            case (MediaProcessorNames.StorageDecryption):
                            case (MediaProcessorNames.WindowsAzureMediaEncryptor):
                            case (MediaProcessorNames.WindowsAzureMediaPackager):
                                // No cost
                                pricetask = 0;
                                break;
                            case (Constants.AzureMediaIndexer):
                                // Indexing task
                                // TO DO: GET DURATION OF CONTENT
                                //pricetask = lsizeprocessed * (double)Properties.Settings.Default.LegacyEncodingPrice;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return new TaskSizeAndPrice()
            {
                InputSize = lSizeinput,
                OutputSize = lSizeoutput,
                Price = pricetask
            };

        }

        private static long ListFilesInAsset(IAsset asset, ref StringBuilder builder)
        {
            // Display the files associated with each asset. 

            long assetSize = 0;
            foreach (IAssetFile fileItem in asset.AssetFiles)
            {
                if (fileItem.IsPrimary) builder.AppendLine("Primary");
                builder.AppendLine("Name: " + fileItem.Name);
                builder.AppendLine("Size: " + fileItem.ContentFileSize + " Bytes");
                assetSize += fileItem.ContentFileSize;
                builder.AppendLine("==============");
            }
            return assetSize;
        }

        public static IMediaProcessor GetMediaProcessorFromId(string processorID, CloudMediaContext _context)
        {
            bool Error = false;
            IMediaProcessor processor = null;
            try
            {
                var processorquery =
                  from p in _context.MediaProcessors
                  orderby p.Version descending
                  where p.Id == processorID
                  select p;
                // Reference the asset as an IAsset.
                processor = processorquery.FirstOrDefault(); //lastordefault returns an error so that's why we use first with sorting descending
            }
            catch
            {
                Error = true;
            }
            return Error ? null : processor;
        }

        private StringBuilder GetStats()
        {
            StringBuilder sb = new StringBuilder();
            const string section = "==============================================================================";
            if (SelectedJobs.Count > 0)
            {
                // Job Stats

                foreach (IJob theJob in SelectedJobs)
                {
                    sb.AppendLine(section);
                    sb.AppendLine(" START OF JOB REPORT");
                    sb.AppendLine(section);
                    sb.AppendLine("");
                    sb.AppendLine("Job Name            : " + theJob.Name);
                    sb.AppendLine("Job ID              : " + theJob.Id);
                    sb.AppendLine("Job State           : " + theJob.State);
                    sb.AppendLine("Job Priority        : " + theJob.Priority);
                    sb.AppendLine("Job Template Id     : " + theJob.TemplateId);
                    sb.AppendLine("Job Created (UTC)   : " + theJob.Created.ToLongDateString() + " " + theJob.Created.ToLongTimeString());
                    if (theJob.StartTime != null)
                        sb.AppendLine("Job StartTime (UTC) : " + theJob.StartTime.Value.ToLongDateString() + " " + theJob.StartTime.Value.ToLongTimeString());
                    if (theJob.EndTime != null)
                        sb.AppendLine("Job EndTime (UTC)   : " + theJob.EndTime.Value.ToLongDateString() + " " + theJob.EndTime.Value.ToLongTimeString());
                    TimeSpan ts = theJob.RunningDuration;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    sb.AppendLine("Job CPU runtime     : " + elapsedTime);

                    if ((theJob.StartTime != null) && (theJob.EndTime != null))
                    {
                        ts = ((DateTime)theJob.EndTime).Subtract((DateTime)theJob.StartTime);
                        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        sb.AppendLine("Job Duration        : " + elapsedTime);
                    }
                    sb.AppendLine("Number of tasks     : " + theJob.Tasks.Count);
                    sb.AppendLine("Media Account       : " + theJob.GetMediaContext().Credentials.ClientId);
                    sb.AppendLine("");

                    foreach (ITask task in theJob.Tasks)
                    {
                        sb.AppendLine(section);
                        sb.AppendLine("");
                        sb.AppendLine("Task Name           : " + task.Name);
                        sb.AppendLine("Task ID             : " + task.Id);
                        sb.AppendLine("Task Priority       : " + task.Priority);
                        sb.AppendLine("Media Processor     : " + task.MediaProcessorId);
                        IMediaProcessor processor = JobInfo.GetMediaProcessorFromId(task.MediaProcessorId, (CloudMediaContext)theJob.GetMediaContext());
                        if (processor != null)
                            sb.AppendLine("Media Processor Name: " + processor.Name);

                        if (task.StartTime != null) // If not in queued state
                            sb.AppendLine("Task StartTime (UTC): " + task.StartTime.Value.ToLongDateString() + " " + task.StartTime.Value.ToLongTimeString());
                        if (task.EndTime != null) // If not completed yet
                            sb.AppendLine("Task EndTime (UTC)  : " + task.EndTime.Value.ToLongDateString() + " " + task.EndTime.Value.ToLongTimeString());
                        ts = task.RunningDuration;
                        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        sb.AppendLine("Task CPU runtime    : " + elapsedTime);

                        if ((task.StartTime != null) && (task.EndTime != null))
                        {
                            ts = ((DateTime)task.EndTime).Subtract((DateTime)task.StartTime);
                            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            sb.AppendLine("Task Duration       : " + elapsedTime);
                        }

                        sb.AppendLine("Task PerfMessage    : " + task.PerfMessage);
                        sb.AppendLine("Task Progress       : " + task.Progress);
                        // Task historical event?
                        foreach (TaskHistoricalEvent thEvent in task.HistoricalEvents)
                        {
                            sb.AppendLine(thEvent.TimeStamp.ToLongTimeString() + " :-  " + thEvent.Message);
                        }

                        sb.AppendLine("");
                        if (task.State == JobState.Error)
                        {
                            foreach (var errordetail in task.ErrorDetails)
                            {
                                sb.AppendLine("Error Message : " + errordetail.Message);
                                sb.AppendLine("Error Code    : " + errordetail.Code);
                            }
                        }

                        if (task.State == JobState.Finished)
                        {
                            TaskSizeAndPrice MyTaskSizePrice = CalculateTaskSizeAndPrice(task, (CloudMediaContext)theJob.GetMediaContext());

                            sb.AppendLine("Input Assets:");
                            foreach (IAsset asset in task.InputAssets)
                            {
                                if (asset.State == AssetState.Deleted)
                                {
                                    sb.AppendLine("Asset Deleted");
                                }
                            }
                            sb.AppendLine("");
                            sb.AppendLine("Output Assets :");
                            foreach (IAsset asset in task.OutputAssets)
                                ListFilesInAsset(asset, ref sb);

                            if (MyTaskSizePrice.InputSize != -1 && MyTaskSizePrice.OutputSize != -1)
                            {

                                if (theJob.Tasks.Count > 1) // only display for the task if there are several tasks
                                {
                                    sb.AppendLine("Input size processed by the task  : " + AssetInfo.FormatByteSize(MyTaskSizePrice.InputSize));
                                    sb.AppendLine("Output size processed by the task : " + AssetInfo.FormatByteSize(MyTaskSizePrice.OutputSize));
                                    sb.AppendLine("Total size processed by the task  : " + AssetInfo.FormatByteSize(MyTaskSizePrice.InputSize + MyTaskSizePrice.OutputSize));
                                }

                                if (MyTaskSizePrice.Price >= 0)
                                {
                                    if (theJob.Tasks.Count > 1) sb.AppendLine(string.Format("Estimated cost of the task        : {0} {1:0.00}", Properties.Settings.Default.Currency, MyTaskSizePrice.Price));
                                }
                            }
                            else
                            {
                                sb.AppendLine("Gigabytes processed by the task : cannot be calculated, asset deleted?");
                            }
                        }

                        sb.AppendLine("");
                        sb.AppendLine(section);
                    }
                    sb.AppendLine("");

                    TaskSizeAndPrice MyJobSizePrice = CalculateJobSizeAndPrice(theJob);

                    if (MyJobSizePrice.InputSize != -1 && MyJobSizePrice.OutputSize != -1)
                    {
                        sb.AppendLine("Input size processed by the job  : " + AssetInfo.FormatByteSize(MyJobSizePrice.InputSize));
                        sb.AppendLine("Output size processed by the job : " + AssetInfo.FormatByteSize(MyJobSizePrice.OutputSize));
                        sb.AppendLine("Total size processed by the job  : " + AssetInfo.FormatByteSize(MyJobSizePrice.InputSize + MyJobSizePrice.OutputSize));
                    }
                    if (MyJobSizePrice.Price != -1)
                    {
                        sb.AppendLine(string.Format("Estimated cost of the job        : {0} {1:0.00}", Properties.Settings.Default.Currency, MyJobSizePrice.Price));
                    }
                    sb.AppendLine("");
                    sb.AppendLine(section);
                    sb.AppendLine(" END OF JOB REPORT");
                    sb.AppendLine(section);
                    sb.AppendLine("");
                }
            }
            return sb;
        }
    }

    public class AssetBitmapAndText
    {
        public Bitmap bitmap;
        public string MouseOverDesc;
    }


    public class AssetInfo
    {
        private List<IAsset> SelectedAssets;
        public const string Type_Workflow = "Workflow";
        public const string Type_LiveArchive = "Live archive";
        public const string Type_Thumbnails = "Thumbnails";
        public const string Type_Empty = "(empty)";
        public const string _prog_down_https_SAS = "Progressive Download URIs (SAS)";
        public const string _prog_down_http_streaming = "Progressive Download URIs (SE)";
        public const string _hls_v4 = "HLS v4  URI";
        public const string _hls_v3 = "HLS v3  URI";
        public const string _dash = "MPEG-DASH URI";
        public const string _smooth = "Smooth Streaming URI";
        public const string _smooth_legacy = "Smooth Streaming (legacy) URI";
        public const string _hls = "HLS URI";

        private const string format_smooth_legacy = "fmp4-v20";
        private const string format_hls_v4 = "m3u8-aapl";
        private const string format_hls_v3 = "m3u8-aapl-v3";
        private const string format_dash = "mpd-time-csf";
        private const string format_url = "(format={0})";

        public AssetInfo(List<IAsset> MySelectedAssets)
        {
            SelectedAssets = MySelectedAssets;
        }
        public AssetInfo(IAsset asset)
        {
            SelectedAssets = new List<IAsset>();
            SelectedAssets.Add(asset);
        }


        public static string GetSmoothLegacy(string smooth_uri)
        {
            return string.Format("{0}(format={1})", smooth_uri, format_smooth_legacy);
        }

        public static string GetHLSv3(string hls_uri)
        {
            return hls_uri.Replace("(format=" + format_hls_v4, "(format=" + format_hls_v3);
        }

        // return the URL with hostname from streaming endpoint
        public static Uri rw(Uri url, IStreamingEndpoint se, bool https = false)
        {
            if (url != null)
            {
                string hostname = se.HostName;
                UriBuilder urib = new UriBuilder()
                {
                    Host = hostname,
                    Scheme = https ? "https://" : "http://",
                    Path = url.AbsolutePath,
                };
                return urib.Uri;
            }
            else return null;
        }

        public static string rw(string path, IStreamingEndpoint se, bool https = false)
        {
            return rw(new Uri(path), se, https).ToString();
        }


        public long GetSize()
        {
            return GetSize(0);
        }

        public long GetSize(int index)
        {
            if (index >= SelectedAssets.Count) return -1;

            long size = 0;
            foreach (IAssetFile objFile in SelectedAssets[index].AssetFiles)
            { size += objFile.ContentFileSize; }
            return size;
        }

        public static long GetSize(IAsset asset)
        {

            long size = 0;
            foreach (IAssetFile objFile in asset.AssetFiles)
            { size += objFile.ContentFileSize; }
            return size;
        }



        public static string GetDynamicEncryptionType(IAsset asset)
        {
            if (asset.DeliveryPolicies.Count > 0)
            {
                string str = string.Empty;
                switch (asset.DeliveryPolicies.FirstOrDefault().AssetDeliveryPolicyType)
                {
                    case AssetDeliveryPolicyType.Blocked:
                        str = "Blocked";
                        break;
                    case AssetDeliveryPolicyType.DynamicCommonEncryption:
                        str = "CENC";
                        break;
                    case AssetDeliveryPolicyType.DynamicEnvelopeEncryption:
                        str = "AES";
                        break;
                    case AssetDeliveryPolicyType.NoDynamicEncryption:
                        str = "No";
                        break;
                    case AssetDeliveryPolicyType.None:
                    default:
                        str = string.Empty;
                        break;

                }
                return str;
            }
            else
            {
                return string.Empty;
            }
        }


        public PublishStatus GetPublishedStatus(LocatorType LocType)
        {
            PublishStatus LocPubStatus;

            // if there is one locato for this type
            if ((SelectedAssets.FirstOrDefault().Locators.Where(l => l.Type == LocType).Count() > 0))
            {
                if (!SelectedAssets.FirstOrDefault().Locators.Where(l => (l.Type == LocType)).All(l => (l.ExpirationDateTime < DateTime.UtcNow)))
                {// not all int the past
                    var query = SelectedAssets.FirstOrDefault().Locators.Where(l => ((l.Type == LocType) && (l.ExpirationDateTime > DateTime.UtcNow) && (l.StartTime != null)));
                    // if no locator are valid today but at least one will in the future
                    if (query.ToList().Count() > 0)
                    {
                        LocPubStatus = (query.All(l => (l.StartTime > DateTime.UtcNow))) ? PublishStatus.PublishedFuture : PublishStatus.PublishedActive;
                    }
                    else
                    {
                        LocPubStatus = PublishStatus.PublishedActive;
                    }
                }
                else      // if all locators are in the past
                {
                    LocPubStatus = PublishStatus.PublishedExpired;
                }
            }
            else
            {
                LocPubStatus = PublishStatus.NotPublished;
            }
            return LocPubStatus;
        }

        public static PublishStatus GetPublishedStatusForLocator(ILocator Locator)
        {
            PublishStatus LocPubStatus;
            if (!(Locator.ExpirationDateTime < DateTime.UtcNow))
            {// not in the past
                // if  locator is not valid today but will be in the future
                if (Locator.StartTime != null)
                {
                    LocPubStatus = (Locator.StartTime > DateTime.UtcNow) ? PublishStatus.PublishedFuture : PublishStatus.PublishedActive;
                }
                else
                {
                    LocPubStatus = PublishStatus.PublishedActive;
                }
            }
            else      // if locator is in the past
            {
                LocPubStatus = PublishStatus.PublishedExpired;
            }
            return LocPubStatus;
        }

        public static IAsset GetAsset(string assetId, CloudMediaContext _context)
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

        public static string GetAssetType(IAsset asset)
        {
            string type = asset.AssetType.ToString();
            int assetfilescount = asset.AssetFiles.Count();
            int number = assetfilescount;

            switch (asset.AssetType)
            {
                case AssetType.MediaServicesHLS:
                    type = "Media Services HLS";
                    break;

                case AssetType.MP4:
                    break;

                case AssetType.MultiBitrateMP4:
                    var mp4files = asset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)).ToArray();
                    number = mp4files.Count();
                    type = number == 1 ? "Single Bitrate MP4" : "Multi Bitrate MP4";
                    break;

                case AssetType.SmoothStreaming:
                    type = "Smooth Streaming";
                    var cfffiles = asset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".ismv", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".isma", StringComparison.OrdinalIgnoreCase)).ToArray();
                    number = cfffiles.Count();
                    break;

                case AssetType.Unknown:
                    string ext;
                    string pr = string.Empty;

                    if (assetfilescount == 0) return "(empty)";

                    if (assetfilescount == 1)
                    {
                        number = 1;
                        ext = Path.GetExtension(asset.AssetFiles.FirstOrDefault().Name.ToUpper());
                        if (!string.IsNullOrEmpty(ext)) ext = ext.Substring(1);
                        switch (ext)
                        {
                            case "KAYAK":
                            case "GRAPH":
                            case "XENIO":
                            case "ZENIUM":
                            case "WORKFLOW":
                            case "BLUEPRINT":
                                type = Type_Workflow;
                                break;

                            case "ISM":
                                /*
                                var program = asset.GetMediaContext().Programs.ToList().Where(p => p.AssetId == asset.Id).ToArray();
                                if (program.Count() == 1) // from a live program
                                {
                                */
                                return Type_LiveArchive;
                                /*
                                }
                                else
                                {
                                    type = ext;
                                }
                                */

                                break;

                            default:
                                type = ext;
                                break;
                        }
                    }
                    else
                    { // multi files in asset
                        var AssetFiles = asset.AssetFiles.ToList();
                        var JPGAssetFiles = AssetFiles.Where(f => f.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)).ToArray();

                        if ((JPGAssetFiles.Count() > 1) && (JPGAssetFiles.Count() == AssetFiles.Count))
                        {
                            type = Type_Thumbnails;
                            number = JPGAssetFiles.Count();
                        }
                    }
                    break;

                default:
                    break;
            }
            return string.Format("{0} ({1})", type, number);
        }


        public void CreateOutlookMail()
        {
            Exception exception = null;
            try
            {
                StringBuilder SB = GetStats();
                // Let's create the email with Outlook
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                if (SelectedAssets.Count == 1)
                {
                    string title = "Report: Asset '{0}'";
                    mailItem.Subject = string.Format(title, SelectedAssets.FirstOrDefault().Name);
                }
                else
                {
                    mailItem.Subject = string.Format("Report: {0} Assets", SelectedAssets.Count());
                }
                mailItem.HTMLBody = "<FONT Face=\"Courier New\">";
                mailItem.HTMLBody += SB.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />").ToString();
                mailItem.Display(false);
            }
            catch (System.Runtime.InteropServices.COMException ce)
            {
                // 0x80040154 Class not registered
                // This happen if outlook is not installed
                if (ce.HResult == unchecked((int)0x80040154))
                {
                    MessageBox.Show("Please install Office Outlook to use this functionality.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                exception = ce;
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception != null)
            {
                MessageBox.Show("Exception while trying to compose the email." + exception, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CopyStatsToClipBoard()
        {
            StringBuilder SB = GetStats();
            Clipboard.SetText((string)SB.ToString());
        }

        private static long ListFilesInAsset(IAsset asset, ref StringBuilder builder)
        {
            // Display the files associated with each asset. 

            long assetSize = 0;
            foreach (IAssetFile fileItem in asset.AssetFiles)
            {
                if (fileItem.IsPrimary) builder.AppendLine("Primary");
                builder.AppendLine("Name: " + fileItem.Name);
                builder.AppendLine("Size: " + fileItem.ContentFileSize + " Bytes");
                assetSize += fileItem.ContentFileSize;
                builder.AppendLine("==============");
            }
            return assetSize;
        }

        public static String FormatByteSize(long? byteCountl)
        {
            if (byteCountl.HasValue == true)
            {
                long byteCount = (long)byteCountl;
                string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
                if (byteCount == 0)
                    return "0 " + suf[0];
                long bytes = Math.Abs(byteCount);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
            }
            else return null;
        }



        public static AssetProtectionType GetAssetProtection(IAsset MyAsset, CloudMediaContext _context)
        {
            AssetProtectionType type = AssetProtectionType.None;
            IAssetDeliveryPolicy policy = MyAsset.DeliveryPolicies.FirstOrDefault();

            if (policy != null)
            {
                switch (policy.AssetDeliveryPolicyType)
                {
                    case AssetDeliveryPolicyType.DynamicEnvelopeEncryption:
                        type = AssetProtectionType.AES;
                        break;

                    case AssetDeliveryPolicyType.DynamicCommonEncryption:
                        type = AssetProtectionType.PlayReady;
                        break;

                    default:
                        break;
                }
            }
            else if (MyAsset.Options == AssetCreationOptions.CommonEncryptionProtected)
            {
                type = AssetProtectionType.PlayReady; // CENC Static protection
            }

            return type;
        }


        private StringBuilder GetStats()
        {
            StringBuilder sb = new StringBuilder();

            if (SelectedAssets.Count > 0)
            {
                // Asset Stats
                foreach (IAsset theAsset in SelectedAssets)
                {
                    string MyAssetType = AssetInfo.GetAssetType(theAsset);
                    bool bfileinasset = (theAsset.AssetFiles.Count() == 0) ? false : true;
                    long size = -1;
                    if (bfileinasset)
                    {
                        size = 0;
                        foreach (IAssetFile file in theAsset.AssetFiles)
                        {
                            size += file.ContentFileSize;
                        }
                    }
                    sb.AppendLine("Asset Name          : " + theAsset.Name);
                    sb.AppendLine("Asset Type          : " + theAsset.AssetType);
                    sb.AppendLine("Asset Id            : " + theAsset.Id);
                    sb.AppendLine("Alternate ID        : " + theAsset.AlternateId);
                    if (size != -1)
                        sb.AppendLine("Size                : " + FormatByteSize(size));
                    sb.AppendLine("State               : " + theAsset.State);
                    sb.AppendLine("Created (UTC)       : " + theAsset.Created.ToLongDateString() + " " + theAsset.Created.ToLongTimeString());
                    sb.AppendLine("Last Modified (UTC) : " + theAsset.LastModified.ToLongDateString() + " " + theAsset.LastModified.ToLongTimeString());
                    sb.AppendLine("Creations Options   : " + theAsset.Options);

                    if (theAsset.State != AssetState.Deleted)
                    {
                        sb.AppendLine("IsStreamable        : " + theAsset.IsStreamable);
                        sb.AppendLine("SupportsDynEnc      : " + theAsset.SupportsDynamicEncryption);
                        sb.AppendLine("Uri                 : " + theAsset.Uri.ToString());
                        sb.AppendLine("");
                        sb.AppendLine("Storage Name        : " + theAsset.StorageAccountName);
                        sb.AppendLine("Storage Bytes used  : " + FormatByteSize(theAsset.StorageAccount.BytesUsed));
                        sb.AppendLine("Storage IsDefault   : " + theAsset.StorageAccount.IsDefault);
                        sb.AppendLine("");

                        foreach (IAsset p_asset in theAsset.ParentAssets)
                        {
                            sb.AppendLine("Parent asset Name   : " + p_asset.Name);
                            sb.AppendLine("Parent asset Id     : " + p_asset.Id);
                        }
                        sb.AppendLine("");
                        foreach (IContentKey key in theAsset.ContentKeys)
                        {
                            sb.AppendLine("Content key         : " + key.Name);
                            sb.AppendLine("Content key Id      : " + key.Id);
                            sb.AppendLine("Content key Type    : " + key.ContentKeyType);
                        }
                        sb.AppendLine("");
                        foreach (var pol in theAsset.DeliveryPolicies)
                        {
                            sb.AppendLine("Deliv policy Name   : " + pol.Name);
                            sb.AppendLine("Deliv policy Id     : " + pol.Id);
                            sb.AppendLine("Deliv policy Type   : " + pol.AssetDeliveryPolicyType);
                            sb.AppendLine("Deliv pol Protocol  : " + pol.AssetDeliveryProtocol);
                        }
                        sb.AppendLine("");

                        foreach (IAssetFile fileItem in theAsset.AssetFiles)
                        {
                            if (fileItem.IsPrimary) sb.AppendLine("Primary");
                            sb.AppendLine("Name                 : " + fileItem.Name);
                            sb.AppendLine("Id                   : " + fileItem.Id);
                            sb.AppendLine("File size            : " + fileItem.ContentFileSize + " Bytes");
                            sb.AppendLine("Mime type            : " + fileItem.MimeType);
                            sb.AppendLine("Init vector          : " + fileItem.InitializationVector);
                            sb.AppendLine("Created              : " + fileItem.Created);
                            sb.AppendLine("Last modified        : " + fileItem.LastModified);
                            sb.AppendLine("Encrypted            : " + fileItem.IsEncrypted);
                            sb.AppendLine("EncryptionScheme     : " + fileItem.EncryptionScheme);
                            sb.AppendLine("EncryptionVersion    : " + fileItem.EncryptionVersion);
                            sb.AppendLine("Encryption key id    : " + fileItem.EncryptionKeyId);
                            sb.AppendLine("InitializationVector : " + fileItem.InitializationVector);
                            sb.AppendLine("ParentAssetId        : " + fileItem.ParentAssetId);
                            sb.AppendLine("==============");
                            sb.AppendLine("");
                        }

                        foreach (ILocator locator in theAsset.Locators)
                        {
                            sb.AppendLine("Locator Name      : " + locator.Name);
                            sb.AppendLine("Locator Type      : " + locator.Type.ToString());
                            sb.AppendLine("Locator Id        : " + locator.Id);
                            sb.AppendLine("Locator Path      : " + locator.Path);
                            if (locator.StartTime != null) sb.AppendLine("Start Time        : " + ((DateTime)locator.StartTime).ToLongDateString() + " " + ((DateTime)locator.StartTime).ToLongTimeString());
                            if (locator.ExpirationDateTime != null) sb.AppendLine("Expiration Time   : " + ((DateTime)locator.ExpirationDateTime).ToLongDateString() + " " + ((DateTime)locator.ExpirationDateTime).ToLongTimeString());
                            sb.AppendLine("");

                            if (locator.Type == LocatorType.OnDemandOrigin)
                            {
                                sb.AppendLine(_prog_down_http_streaming + " : ");
                                foreach (IAssetFile IAF in theAsset.AssetFiles) sb.AppendLine(locator.Path + IAF.Name);
                                sb.AppendLine("");

                                if (MyAssetType.StartsWith("HLS")) // It is a static HLS asset, so let's propose only the standard HLS V3 locator
                                {
                                    sb.AppendLine(AssetInfo._hls_v3 + " : ");
                                    sb.AppendLine(locator.GetHlsUri().ToString().Replace("format=m3u8-aapl", "format=m3u8-aapl-v3"));
                                    sb.AppendLine("");
                                }
                                else // It's not Static HLS
                                {
                                    if (locator.GetSmoothStreamingUri() != null)
                                    {
                                        sb.AppendLine(AssetInfo._smooth + " : ");
                                        sb.AppendLine(locator.GetSmoothStreamingUri().ToString());
                                        sb.AppendLine(AssetInfo._smooth_legacy + " : ");
                                        sb.AppendLine(AssetInfo.GetSmoothLegacy(locator.GetSmoothStreamingUri().ToString()));
                                    }

                                    if (locator.GetMpegDashUri() != null)
                                    {
                                        sb.AppendLine(AssetInfo._dash + " : ");
                                        sb.AppendLine(locator.GetMpegDashUri().ToString());
                                    }

                                    if (locator.GetHlsUri() != null)
                                    {
                                        sb.AppendLine(AssetInfo._hls_v4 + " : ");
                                        sb.AppendLine(locator.GetHlsUri().ToString());
                                        sb.AppendLine(AssetInfo._hls_v3 + " : ");
                                        sb.AppendLine(locator.GetHlsv3Uri().ToString());
                                        sb.AppendLine("");
                                    }
                                }
                            }
                            if (locator.Type == LocatorType.Sas)
                            {
                                List<Uri> ProgressiveDownloadUris;
                                IEnumerable<IAssetFile> MyAssetFiles;
                                sb.AppendLine(AssetInfo._prog_down_https_SAS + " : ");
                                MyAssetFiles = theAsset.AssetFiles.ToList();
                                // Generate the Progressive Download URLs for each file. 
                                ProgressiveDownloadUris = MyAssetFiles.Select(af => af.GetSasUri(locator)).ToList();
                                ProgressiveDownloadUris.ForEach(uri => sb.AppendLine(uri.ToString()));
                            }
                            sb.AppendLine("");
                            sb.AppendLine("==============================================================================");
                            sb.AppendLine("");
                        }
                    }
                    sb.AppendLine("");
                    sb.AppendLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    sb.AppendLine("");
                }
            }
            return sb;
        }





        public static void DoPlayBackWithBestStreamingEndpoint(PlayerType typeplayer, string Urlstr, CloudMediaContext context, IAsset myasset = null, bool DoNotRewriteURL = false, AssetProtectionType keytype = AssetProtectionType.None, AzureMediaPlayerFormats formatamp = AzureMediaPlayerFormats.Auto)
        {
            if (!string.IsNullOrEmpty(Urlstr))
            {

                IStreamingEndpoint choosenSE = GetBestStreamingEndpoint(context);
                if (!DoNotRewriteURL) Urlstr = rw(Urlstr.ToString(), choosenSE);

                //string token = null;
                DynamicEncryption.TokenResult tokenresult = new DynamicEncryption.TokenResult();

                if (myasset != null && DynamicEncryption.IsAssetHasAuthorizationPolicyWithToken(myasset, context))
                {
                    // user wants perhaps to play an asset with a token, so let's try to generate it
                    switch (typeplayer)
                    {
                        case PlayerType.SilverlightPlayReadyToken:
                            tokenresult = DynamicEncryption.GetTestToken(myasset, context, ContentKeyType.CommonEncryption);
                            if (!string.IsNullOrEmpty(tokenresult.TokenString))
                            {
                                tokenresult.TokenString = HttpUtility.UrlEncode(Constants.Bearer + tokenresult.TokenString);
                                keytype = AssetProtectionType.PlayReady;
                            }
                            break;

                        case PlayerType.FlashAESToken:
                            tokenresult = DynamicEncryption.GetTestToken(myasset, context, ContentKeyType.EnvelopeEncryption);
                            if (!string.IsNullOrEmpty(tokenresult.TokenString))
                            {
                                tokenresult.TokenString = HttpUtility.UrlEncode(Constants.Bearer + tokenresult.TokenString);
                                keytype = AssetProtectionType.AES;
                            }
                            break;

                        case PlayerType.AzureMediaPlayer:
                            keytype = AssetInfo.GetAssetProtection(myasset, context);
                            switch (keytype)
                            {
                                case AssetProtectionType.None:
                                    break;
                                case AssetProtectionType.AES:
                                case AssetProtectionType.PlayReady:
                                    tokenresult = DynamicEncryption.GetTestToken(myasset, context, displayUI: true);
                                    if (!string.IsNullOrEmpty(tokenresult.TokenString))
                                    {
                                        tokenresult.TokenString = HttpUtility.UrlEncode(Constants.Bearer + tokenresult.TokenString);
                                        // if the user selecteed an CENC key, let's assume that the content is protected with PlayReady, otherwise AES
                                        keytype = (tokenresult.ContentKeyType == ContentKeyType.CommonEncryption) ? AssetProtectionType.PlayReady : AssetProtectionType.AES;
                                    }
                                    break;
                            }
                            break;


                        default:
                            // no token enabled player
                            break;
                    }
                }


                // let's launch the player
                switch (typeplayer)
                {
                    case PlayerType.AzureMediaPlayer:
                        string playerurl = "http://aka.ms/azuremediaplayer?url={0}";
                        string protectionsyntax = "&protection={0}";
                        string tokensyntax = "&token={0}";
                        string formatsyntax = "&format={0}";

                        if (keytype != AssetProtectionType.None)
                        {
                            switch (keytype)
                            {
                                case AssetProtectionType.AES:
                                    playerurl += string.Format(protectionsyntax, "aes");
                                    break;

                                case AssetProtectionType.PlayReady:
                                    playerurl += string.Format(protectionsyntax, "playready");
                                    break;

                                default:
                                    break;
                            }
                            if (!string.IsNullOrEmpty(tokenresult.TokenString))
                            {
                                playerurl += string.Format(tokensyntax, tokenresult.TokenString);
                            }
                        }

                        if (formatamp != AzureMediaPlayerFormats.Auto)
                        {
                            switch (formatamp)
                            {
                                case AzureMediaPlayerFormats.Dash:
                                    playerurl += string.Format(formatsyntax, "dash");
                                    break;

                                case AzureMediaPlayerFormats.Smooth:
                                    playerurl += string.Format(formatsyntax, "smooth");
                                    break;

                                case AzureMediaPlayerFormats.HLS:
                                    playerurl += string.Format(formatsyntax, "hls");
                                    break;

                                case AzureMediaPlayerFormats.VideoMP4:
                                    playerurl += string.Format(formatsyntax, "video/mp4");
                                    break;

                                default: // auto or other
                                    break;
                            }
                            if (tokenresult != null)
                            {
                                playerurl += string.Format(tokensyntax, tokenresult);
                            }
                        }
                        else // format auto. If 0 Reserved Unit, and asset is smooth, let's force to smooth (player cannot get the dash stream for example)
                        {
                            if (choosenSE.ScaleUnits == 0 && myasset != null && myasset.AssetType == AssetType.SmoothStreaming)
                                playerurl += string.Format(formatsyntax, "smooth");
                        }
                        Process.Start(string.Format(playerurl, Urlstr));
                        break;

                    case PlayerType.SilverlightMonitoring:
                        Process.Start(@"http://smf.cloudapp.net/healthmonitor?Autoplay=true&url=" + Urlstr);
                        break;

                    case PlayerType.SilverlightPlayReadyToken:
                        Process.Start(string.Format(@"http://sltoken.azurewebsites.net/#/!?url={0}&token={1}", Urlstr, tokenresult));
                        break;

                    case PlayerType.DASHIFRefPlayer:
                        if (!Urlstr.EndsWith(string.Format(AssetInfo.format_url, AssetInfo.format_dash))) Urlstr += string.Format(AssetInfo.format_url, AssetInfo.format_dash); // if not DASH extension, let's add it
                        Process.Start(@"http://dashif.org/reference/players/javascript/1.2.0/index.html?url=" + Urlstr);
                        break;

                    case PlayerType.DASHAzurePage:
                        Process.Start(@"http://amsplayer.azurewebsites.net/player.html?player=silverlight&format=mpeg-dash&url=" + Urlstr);
                        break;

                    case PlayerType.DASHLiveAzure:
                        if (!Urlstr.EndsWith(string.Format(AssetInfo.format_url, AssetInfo.format_dash))) Urlstr += string.Format(AssetInfo.format_url, AssetInfo.format_dash); // if not DASH extension, let's add it
                        Process.Start(@"http://dashplayer.azurewebsites.net?url=" + Urlstr);
                        break;

                    case PlayerType.FlashAzurePage:
                        Process.Start(@"http://amsplayer.azurewebsites.net/player.html?player=flash&format=smooth&url=" + Urlstr);
                        break;

                    case PlayerType.FlashAESToken:
                        Process.Start(string.Format(@"http://aestoken.azurewebsites.net/#/!?url={0}&token={1}", Urlstr, tokenresult));
                        break;

                    case PlayerType.MP4AzurePage:
                        Process.Start(string.Format(@"http://amsplayer.azurewebsites.net/player.html?player=html5&format=mp4&url={0}&mp4url={0}", Urlstr));
                        break;

                    case PlayerType.CustomPlayer:
                        string myurl = Properties.Settings.Default.CustomPlayerUrl;
                        Process.Start(myurl.Replace(Constants.NameconvManifestURL, Urlstr));
                        break;
                }


            }



        }



        internal static IStreamingEndpoint GetBestStreamingEndpoint(CloudMediaContext _context)
        {
            // let's choose the default SE if it is running and use one RU minimum
            IStreamingEndpoint SESelected = _context.StreamingEndpoints.Where(se => se.Name == "default").FirstOrDefault(); //default
            if (SESelected == null || SESelected.ScaleUnits == 0 || SESelected.State != StreamingEndpointState.Running) //default is not there, or not running or has no scale unit
            {
                IStreamingEndpoint SESelected2 = _context.StreamingEndpoints.ToList().Where(se => se.State == StreamingEndpointState.Running).OrderBy(se => se.ScaleUnits).LastOrDefault();
                if (SESelected2 != null) SESelected = SESelected2;
            }
            return SESelected;
        }
    }

    public class TaskSizeAndPrice
    {
        public long InputSize { get; set; }
        public long OutputSize { get; set; }
        public double Price { get; set; }
    }

    public class JobEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Tasks { get; set; }
        public int Priority { get; set; }
        public JobState State { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public Double Progress { get; set; }
    }


    public class AssetEntry
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public Nullable<DateTime> LastModified { get; set; }
        public string Size { get; set; }
        public long SizeLong { get; set; }
        public string Storage { get; set; }
        public Bitmap StaticEncryption { get; set; }
        public string StaticEncryptionMouseOver { get; set; }
        public Bitmap DynamicEncryption { get; set; }
        public string DynamicEncryptionMouseOver { get; set; }
        public Bitmap Publication { get; set; }
        public string PublicationMouseOver { get; set; }
    }


    public class TransferEntry
    {
        public string Name { get; set; }
        public TransferType Type { get; set; }
        public TransferState State { get; set; }
        public double Progress { get; set; }
        public Nullable<DateTime> SubmitTime { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public string EndTime { get; set; }
        public string DestLocation { get; set; }
        public bool processedinqueue { get; set; }  // true if we want to process in the queue. Otherwise, we don't wait and we do paralell transfers
        public string ErrorDescription { get; set; }
    }

    public class CredentialsEntry
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string StorageKey { get; set; }
        public string Description { get; set; }
        public string UsePartnerAPI { get; set; }
        public string UseOtherAPI { get; set; }
        public string OtherAPIServer { get; set; }
        public string OtherScope { get; set; }
        public string OtherACSBaseAddress { get; set; }
        public string OtherAzureEndpoint { get; set; }

        public static readonly int StringsCount = 10; // number of strings
        public static readonly string PartnerAPIServer = "https://nimbuspartners.cloudapp.net/API/";
        public static readonly string PartnerScope = "urn:NimbusPartners";
        public static readonly string PartnerACSBaseAddress = "https://mediaservices.accesscontrol.windows.net";
        public static readonly string PartnerAzureEndpoint = "";

        public static readonly string OtherGlobalAPIServer = "https://media.windows.net/API/";
        public static readonly string OtherGlobalScope = "urn:WindowsAzureMediaServices";
        public static readonly string OtherGlobalACSBaseAddress = "https://wamsprodglobal001acs.accesscontrol.windows.net";
        public static readonly string OtherGlobalAzureEndpoint = "windows.net";

        public static readonly string OtherChinaAPIServer = "https://wamsbjbclus001rest-hs.chinacloudapp.cn/API/";
        public static readonly string OtherChinaScope = "urn:WindowsAzureMediaServices";
        public static readonly string OtherChinaACSBaseAddress = "https://wamsprodglobal001acs.accesscontrol.chinacloudapi.cn";
        public static readonly string OtherChinaAzureEndpoint = "chinacloudapi.cn";

        public static readonly string CoreServiceManagement = "https://management.core."; // with Azure endpoint, that gives "https://management.core.windows.net" for Azure Global and "https://management.core.chinacloudapi.cn" for China
        public static readonly string CoreAttachStorageURL = "https://{0}.blob.core."; // with Azure endpoint, that gives "https://{0}.blob.core.windows.net" for Azure Global and "https://{0}.blob.core.chinacloudapi.cn/" for China
        public static readonly string CoreStorage = "core."; // with Azure endpoint, that gives "core.windows.net" for Azure Global and "core.chinacloudapi.cn" for China

        public static readonly string GlobalAzureEndpoint = "windows.net";
        public static readonly string GlobalManagementPortal = "http://manage.windowsazure.com";
        public static readonly string ChinaManagementPortal = "http://manage.windowsazure.cn";

        public CredentialsEntry(string accountname, string accountkey, string storagekey, string description, string usepartnerapi, string useotherapi, string apiserver, string scope, string acsbaseaddress, string azureendpoint)
        {
            AccountName = accountname;
            AccountKey = accountkey;
            StorageKey = storagekey;
            Description = description;
            UsePartnerAPI = usepartnerapi;
            UseOtherAPI = useotherapi;
            OtherAPIServer = apiserver;
            OtherScope = scope;
            OtherACSBaseAddress = acsbaseaddress;
            OtherAzureEndpoint = azureendpoint;
        }

        public string[] ToArray()
        {
            string[] myList = new String[] { AccountName, AccountKey, StorageKey, Description, UsePartnerAPI, UseOtherAPI, OtherAPIServer, OtherScope, OtherACSBaseAddress, OtherAzureEndpoint };
            return myList;
        }

        // return the storage suffix for China, or null for Global Azure
        public string ReturnStorageSuffix()
        {
            if (UseOtherAPI == true.ToString())
                return CoreStorage + OtherAzureEndpoint;
            else
                return null;
        }
    }

    public enum PlayerType
    {
        AzureMediaPlayer = 0,
        FlashAzurePage = 1,
        SilverlightAzurePage = 2,
        SilverlightMonitoring = 3,
        SilverlightPlayReadyToken = 4,
        FlashAESToken = 5,
        DASHAzurePage = 6,
        DASHLiveAzure = 7,
        DASHIFRefPlayer = 8,
        MP4AzurePage = 9,
        CustomPlayer = 10
    }

    public enum TaskJobCreationMode
    {
        MultipleTasks_MultipleJobs = 0,
        MultipleTasks_SingleJob = 1,
        SingleTask_SingleJob = 2,
    }

    public enum PublishStatus
    {
        NotPublished = 0,
        PublishedActive = 1,
        PublishedFuture = 2,
        PublishedExpired = 3,
    }

    public enum AzureMediaPlayerFormats
    {
        Auto = 0,
        Smooth = 1,
        Dash = 2,
        HLS = 3,
        VideoMP4 = 4
    }


    public enum AssetProtectionType
    {
        None = 0,
        AES = 1,
        PlayReady = 2
    }

    class HostNameClass
    {
        public string HostName { get; set; }
    }

    public class Item
    {
        public string Name;
        public string Value;
        public Item(string name, string value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }




    public class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal;
            // Determine whether the type being compared is a date type.
            try
            {
                // Parse the two objects passed as a parameter as a DateTime.
                System.DateTime firstDate =
                        DateTime.Parse(((ListViewItem)x).SubItems[col].Text);
                System.DateTime secondDate =
                        DateTime.Parse(((ListViewItem)y).SubItems[col].Text);
                // Compare the two dates.
                returnVal = DateTime.Compare(firstDate, secondDate);
            }
            // If neither compared object has a valid date format, compare
            // as a string.
            catch
            {
                // Compare the two items as a string.
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                            ((ListViewItem)y).SubItems[col].Text);
            }
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }

        static public void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView ThisListView = (ListView)sender;
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != ((int)ThisListView.Tag))
            {
                // Set the sort column to the new column.
                ThisListView.Tag = e.Column;
                // Set the sort order to ascending by default.
                ThisListView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (ThisListView.Sorting == SortOrder.Ascending)
                    ThisListView.Sorting = SortOrder.Descending;
                else
                    ThisListView.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            ThisListView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            ThisListView.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              ThisListView.Sorting);
        }

    }


}
