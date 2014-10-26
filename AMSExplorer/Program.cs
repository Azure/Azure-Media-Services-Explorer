
//----------------------------------------------------------------------- 
// <copyright file="Program.cs" company="Microsoft">Copyright (c) Microsoft Corporation. All rights reserved.</copyright> 
// <license>
// Azure Media Services Explorer Ver. 3.0
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
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(context.DefaultStorageAccount.Name, credentials.StorageKey), true);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                return true;
            }
            catch
            {
                MessageBox.Show(string.Format("There is a problem when connecting to the Azure storage account {0}.\r\nIs the storage key correct ?", context.DefaultStorageAccount.Name), "Storage Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }

    public class Constants
    {
        public const string GitHubAMSEVersion = "https://raw.githubusercontent.com/Azure/Azure-Media-Services-Explorer/master/version.xml";
        public const string GitHubAMSEReleases = "https://github.com/Azure/Azure-Media-Services-Explorer/releases";

        public const string ZeniumConfig = "";
        public const string WindowsAzureMediaEncoder = "Windows Azure Media Encoder";
        public const string AzureMediaEncoder = "Azure Media Encoder";
        public const string ZeniumEncoder = "Digital Rapids - Kayak Cloud Engine";
        public const string AzureMediaIndexer = "Azure Media Indexer";

        public const string NameconvInputasset = "{Input Asset Name}";
        public const string NameconvUploadasset = "{File Name}";
        public const string NameconvBlueprint = "{Blueprint}";
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
        public const string TabLog = "Log"; // name of the Jobs tab

        public const string PathAMEFiles = @"\WAMEPresetFiles\";
        public const string PathConfigFiles = @"\configurations\";
        public const string PathHelpFiles = @"\HelpFiles\";
        public const string PathLicense = @"\license\Azure Media Services Explorer.rtf";

        public const string AMSPlayer = @"http://amsplayer.azurewebsites.net/?player=flash&format=smooth&url=";

        public const string LocatorIdPrefix = "nb:lid:UUID:";

        public const string ProdAPIServer = "https://media.windows.net";
        public const string ProdACSBaseAddress = "https://wamsprodglobal001acs.accesscontrol.windows.net";
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

        private StringBuilder GetStats()
        {
            StringBuilder sb = new StringBuilder();
            if (SelectedJobs.Count > 0)
            {
                // Job Stats

                foreach (IJob theJob in SelectedJobs)
                {
                    sb.AppendLine("Job Name        : " + theJob.Name);
                    sb.AppendLine("Job ID          : " + theJob.Id);
                    sb.AppendLine("Job State       : " + theJob.State);
                    sb.AppendLine("Job Priority    : " + theJob.Priority);
                    sb.AppendLine("Job Created     : " + theJob.Created.ToLongDateString() + " " + theJob.Created.ToLongTimeString());
                    if (theJob.StartTime != null)
                        sb.AppendLine("Job StartTime   : " + theJob.StartTime.Value.ToLongDateString() + " " + theJob.StartTime.Value.ToLongTimeString());
                    if (theJob.EndTime != null)
                        sb.AppendLine("Job EndTime     : " + theJob.EndTime.Value.ToLongDateString() + " " + theJob.EndTime.Value.ToLongTimeString());
                    TimeSpan ts = theJob.RunningDuration;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    sb.AppendLine("Job CPU runtime : " + elapsedTime);

                    if ((theJob.StartTime != null) && (theJob.EndTime != null))
                    {
                        ts = ((DateTime)theJob.EndTime).Subtract((DateTime)theJob.StartTime);
                        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}.{4:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        sb.AppendLine("Job Duration    : " + elapsedTime);
                    }

                    sb.AppendLine("");
                    sb.AppendLine("Media Account : " + theJob.GetMediaContext().Credentials.ClientId);
                    sb.AppendLine("");


                    foreach (ITask task in theJob.Tasks)
                    {
                        sb.AppendLine("Task Name           : " + task.Name);
                        sb.AppendLine("Task ID             : " + task.Id);
                        sb.AppendLine("Task Priority       : " + task.Priority);
                        sb.AppendLine("Task MP             : " + task.MediaProcessorId);
                        if (task.StartTime != null) // If not in queued state
                            sb.AppendLine("Task StartTime      : " + task.StartTime.Value.ToLongDateString() + " " + task.StartTime.Value.ToLongTimeString());
                        if (task.EndTime != null) // If not completed yet
                            sb.AppendLine("Task EndTime        : " + task.EndTime.Value.ToLongDateString() + " " + task.EndTime.Value.ToLongTimeString());
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
                            //long lSize = 0;
                            long lSizeinput = 0;
                            long lSizeoutput = 0;
                            bool sizecanbecalculated = true;
                            sb.AppendLine("Input Assets:");
                            foreach (IAsset asset in task.InputAssets)
                            {
                                if (asset.State == AssetState.Deleted)
                                {
                                    sb.AppendLine("Asset Deleted");
                                    sizecanbecalculated = false;
                                }

                                lSizeinput += ListFilesInAsset(asset, ref sb);
                            }
                            sb.AppendLine("");
                            sb.AppendLine("Output Assets:");
                            foreach (IAsset asset in task.OutputAssets)
                                lSizeoutput += ListFilesInAsset(asset, ref sb);

                            double lsizeinputprocessed = (double)lSizeinput / (1024 * 1024 * 1024);
                            double lsizeoutputprocessed = (double)lSizeoutput / (1024 * 1024 * 1024);
                            double lsizeprocessed = (double)(lSizeinput + lSizeoutput) / (1024 * 1024 * 1024);
                            if (sizecanbecalculated)
                            {
                                sb.AppendLine("Input gigabytes processed by the task  : " + lsizeinputprocessed.ToString());
                                sb.AppendLine("Output gigabytes processed by the task : " + lsizeoutputprocessed.ToString());
                                sb.AppendLine("Total gigabytes processed by the task  : " + lsizeprocessed.ToString());

                            }
                            else sb.AppendLine("Gigabytes processed by the task : cannot be calculated, asset deleted?");

                        }
                        sb.AppendLine("");
                        sb.AppendLine("==============================================================================");
                        sb.AppendLine("");
                    }
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
        public const string Type_Blueprint = "Blueprint";
        public const string Type_Empty = "(empty)";
        public const string _prog_down_https = "Progressive Download URIs (https)";
        public const string _prog_down_http = "Progressive Download URIs (http)";
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
                    var cfffiles = asset.AssetFiles.ToList().Where(f => f.Name.EndsWith(".ismv", StringComparison.OrdinalIgnoreCase) | f.Name.EndsWith(".isma", StringComparison.OrdinalIgnoreCase)).ToArray();
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
                            case "XENIO":
                                type = Type_Blueprint;
                                break;

                            case "ISM":
                                var program = asset.GetMediaContext().Programs.ToList().Where(p => p.AssetId == asset.Id).ToArray();
                                if (program.Count() == 1) // from a live program
                                {
                                    return "Live archive";
                                }
                                else
                                {
                                    type = ext;
                                }

                                break;

                            default:
                                type = ext;
                                break;
                        }
                    }
                    else
                    { // multi files in asset
                        var AssetFiles = asset.AssetFiles.ToList();
                        var JPGAssetFiles = AssetFiles.Where(f => f.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) | f.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) | f.Name.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) | f.Name.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)).ToArray();

                        if ((JPGAssetFiles.Count() > 1) && (JPGAssetFiles.Count() == AssetFiles.Count))
                        {
                            type = "Thumbnails";
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
                    sb.AppendLine("Asset Name        : " + theAsset.Name);
                    sb.AppendLine("Asset Type        : " + theAsset.AssetType);
                    sb.AppendLine("Asset Id          : " + theAsset.Id);
                    sb.AppendLine("Alternate ID      : " + theAsset.AlternateId);
                    if (size != -1) sb.AppendLine("Size              : " + FormatByteSize(size));
                    sb.AppendLine("State             : " + theAsset.State);
                    sb.AppendLine("Created           : " + theAsset.Created.ToLongDateString() + " " + theAsset.Created.ToLongTimeString());
                    sb.AppendLine("Last Modified     : " + theAsset.LastModified.ToLongDateString() + " " + theAsset.LastModified.ToLongTimeString());
                    sb.AppendLine("Creations Options : " + theAsset.Options);

                    if (theAsset.State != AssetState.Deleted)
                    {
                        sb.AppendLine("IsStreamable      : " + theAsset.IsStreamable);
                        sb.AppendLine("SupportsDynEnc    : " + theAsset.SupportsDynamicEncryption);
                        sb.AppendLine("Uri               : " + theAsset.Uri.ToString());
                        sb.AppendLine("");
                        sb.AppendLine("Storage Name      : " + theAsset.StorageAccountName);
                        sb.AppendLine("Storage Bytes used: " + FormatByteSize(theAsset.StorageAccount.BytesUsed));
                        sb.AppendLine("Storage IsDefault : " + theAsset.StorageAccount.IsDefault);
                        sb.AppendLine("");

                        foreach (IAsset p_asset in theAsset.ParentAssets)
                        {
                            sb.AppendLine("Parent asset Name : " + p_asset.Name);
                            sb.AppendLine("Parent asset Id   : " + p_asset.Id);
                        }
                        sb.AppendLine("");
                        foreach (IContentKey key in theAsset.ContentKeys)
                        {
                            sb.AppendLine("Content key       : " + key.Name);
                            sb.AppendLine("Content key Id    : " + key.Id);
                            sb.AppendLine("Content key Type  : " + key.ContentKeyType);
                        }
                        sb.AppendLine("");
                        foreach (var pol in theAsset.DeliveryPolicies)
                        {
                            sb.AppendLine("Deliv policy Name : " + pol.Name);
                            sb.AppendLine("Deliv policy Id   : " + pol.Id);
                            sb.AppendLine("Deliv policy Type : " + pol.AssetDeliveryPolicyType);
                            sb.AppendLine("Deliv pol Protocol: " + pol.AssetDeliveryProtocol);
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
                                sb.AppendLine(_prog_down_http + " : ");
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
                                sb.AppendLine(AssetInfo._prog_down_https + " : ");
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

        public static void DoPlayBack(PlayerType typeplayer, Uri Url)
        {
            DoPlayBack(typeplayer, Url.ToString());
        }

        public static void DoPlayBack(PlayerType typeplayer, string Url)
        {
            switch (typeplayer)
            {
                case PlayerType.SilverlightMonitoring:
                    Process.Start(@"http://smf.cloudapp.net/healthmonitor?Autoplay=true&url=" + Url);
                    break;

                case PlayerType.DASHIFRefPlayer:
                    if (!Url.EndsWith(string.Format(AssetInfo.format_url, AssetInfo.format_dash))) Url += string.Format(AssetInfo.format_url, AssetInfo.format_dash); // if not DASH extension, let's add it
                    Process.Start(@"http://dashif.org/reference/players/javascript/1.2.0/index.html?url=" + Url);
                    break;

                case PlayerType.DASHAzurePage:
                    if (!Url.EndsWith(string.Format(AssetInfo.format_url, AssetInfo.format_dash))) Url += string.Format(AssetInfo.format_url, AssetInfo.format_dash); // if not DASH extension, let's add it
                    Process.Start(@"http://amsplayer.azurewebsites.net/player.html?player=silverlight&format=mpeg-dash&url=" + Url);
                    break;

                case PlayerType.DASHLiveAzure:
                    if (!Url.EndsWith(string.Format(AssetInfo.format_url, AssetInfo.format_dash))) Url += string.Format(AssetInfo.format_url, AssetInfo.format_dash); // if not DASH extension, let's add it
                    Process.Start(@"http://dashplayer.azurewebsites.net?url=" + Url);
                    break;

                case PlayerType.FlashAzurePage:
                    Process.Start(@"http://amsplayer.azurewebsites.net/player.html?player=flash&format=smooth&url=" + Url);
                    break;

                case PlayerType.MP4AzurePage:
                    Process.Start(string.Format(@"http://amsplayer.azurewebsites.net/player.html?player=html5&format=mp4&url={0}&mp4url={0}", Url));
                    break;

                case PlayerType.CustomPlayer:
                    string myurl = Properties.Settings.Default.CustomPlayerUrl;
                    Process.Start(myurl.Replace(Constants.NameconvManifestURL, Url.ToString()));
                    break;
            }
        }
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
        public string Reserved { get; set; }

        public static readonly int StringsCount = 10; // number of strings
        public static readonly string PartnerAPIServer = "https://nimbuspartners.cloudapp.net/API/";
        public static readonly string PartnerScope = "urn:NimbusPartners";
        public static readonly string PartnerACSBaseAddress = "https://mediaservices.accesscontrol.windows.net";

        public CredentialsEntry(string accountname, string accountkey, string storagekey, string description, string usepartnerapi, string useotherapi, string apiserver, string scope, string acsbaseaddress, string reserved)
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
            Reserved = reserved;
        }

        public string[] ToArray()
        {
            string[] myList = new String[] { AccountName, AccountKey, StorageKey, Description, UsePartnerAPI, UseOtherAPI, OtherAPIServer, OtherScope, OtherACSBaseAddress, Reserved };
            return myList;
        }
    }

    public enum PlayerType
    {
        FlashAzurePage = 0,
        SilverlightAzurePage = 1,
        SilverlightMonitoring = 2,
        DASHAzurePage = 3,
        DASHLiveAzure = 4,
        DASHIFRefPlayer = 5,
        MP4AzurePage = 6,
        CustomPlayer = 7
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
}
