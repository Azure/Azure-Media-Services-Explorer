//----------------------------------------------------------------------------------------------
//    Copyright 2021 Microsoft Corporation
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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Identity.Client;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AMSExplorer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        private const string languageparam = "/language:";

        [STAThread]
        private static void Main(string[] args)
        {

            /*
            
            .net v5 :
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            */


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0 && args.Any(a => a.StartsWith(languageparam)))
            {
                string language = args.Where(a => a.StartsWith(languageparam)).FirstOrDefault().Substring(languageparam.Length);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(language, false);
            }
            Application.Run(new Mainform(args));
        }

        public static void DataGridViewV_Resize(object sender)
        {
            return; // let's disable this code for now
            // let's resize the column name to fill the space
            DataGridView grid = (DataGridView)sender;
            int indexname = -1;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i].HeaderText == "Name")
                {
                    indexname = i;
                    break;
                }
            }

            if (indexname != -1)
            {
                grid.Columns[indexname].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                int colw = Math.Max(grid.Columns[indexname].Width, 100);
                grid.Columns[indexname].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grid.Columns[indexname].Width = colw;
            }
        }


        public static string GetErrorMessage(Exception e)
        {
            string s = string.Empty;

            while (e != null)
            {
                if (e is ApiErrorException eApi)
                {
                    s = eApi.Body?.Error?.Message;
                }
                else
                {
                    s = e.Message;
                }

                e = e.InnerException;
            }
            return s;// ParseXml(s);
        }


        // Detect if this JSON or XML data or other and store in private var
        public static TypeConfig AnalyseConfigurationString(string config)
        {
            config = config.Trim();
            if (string.IsNullOrEmpty(config))
            {
                return TypeConfig.Empty;
            }
            if (config.StartsWith("<")) // XML data
            {
                return TypeConfig.XML;
            }
            else if (config.StartsWith("[") || config.StartsWith("{")) // JSON
            {
                return TypeConfig.JSON;
            }
            else // something else
            {
                return TypeConfig.Other;
            }
        }

        public enum TypeConfig
        {
            JSON = 0,
            XML,
            Empty,
            Other
        }

        public static string AnalyzeTextAndReportSyntaxError(string myText)
        {
            string strReturn = string.Empty;
            TypeConfig type = Program.AnalyseConfigurationString(myText);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax
                try
                {
                    JObject jo = JObject.Parse(myText);
                }
                catch (Exception ex)
                {
                    strReturn = string.Format("JSON Syntax error: {0}", ex.Message);
                }
            }
            else if (type == TypeConfig.XML) // XML 
            {
                try
                {
                    XElement xml = XElement.Load(new StringReader(myText));
                }
                catch (Exception ex)
                {
                    strReturn = string.Format("XML Syntax error: {0}", ex.Message);
                }
            }

            return strReturn;
        }

        public static string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.Declaration + Environment.NewLine + doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static string AnalyzeAndIndentXMLJSON(string myText)
        {
            TypeConfig type = Program.AnalyseConfigurationString(myText);
            if (type == TypeConfig.JSON)
            {
                // Let's check JSON syntax
                try
                {
                    dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(myText);
                    myText = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
                }
                catch
                {
                }
            }
            else if (type == TypeConfig.XML) // XML 
            {
                try
                {
                    myText = FormatXml(myText);
                }
                catch
                {
                }
            }
            return myText;
        }


        public static string MessageNewVersion = string.Empty;

#pragma warning disable 1998
        public static async Task CheckAMSEVersionAsync()
#pragma warning restore 1998
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += (sender, e) => DownloadVersionRequestCompletedV3(true, sender, e);
            webClient.DownloadStringAsync(new Uri(Constants.GitHubAMSEVersionPrimaryV3));
        }

        public static void DownloadVersionRequestCompletedV3(bool firsttry, object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    dynamic data = JsonConvert.DeserializeObject(e.Result);
                    Version versionAMSEGitHub = new Version((string)data.Version);
                    Uri RelNotesUrl = new Uri((string)data.ReleaseNotesUrl);
                    Uri AllRelNotesUrl = new Uri((string)data.AllReleaseNotesUrl);
                    Uri BinaryUrl = new Uri((string)data.BinaryUrl);

                    Version versionAMSELocal = Assembly.GetExecutingAssembly().GetName().Version;
                    if (versionAMSEGitHub > versionAMSELocal)
                    {
                        MessageNewVersion = string.Format("A new version ({0}) is available on GitHub: {1}", versionAMSEGitHub, Constants.GitHubAMSEReleases);
                        SoftwareUpdate form = new SoftwareUpdate(RelNotesUrl, versionAMSEGitHub, BinaryUrl);
                        form.ShowDialog();
                    }
                }
                catch
                {

                }
            }
            else if (firsttry)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += (sender2, e2) => DownloadVersionRequestCompletedV3(false, sender2, e2);
                webClient.DownloadStringAsync(new Uri(Constants.GitHubAMSEVersionSecondaryV3));
            }
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


        public static DialogResult InputBox(string title, string promptText, ref string value, bool passwordWildcard = false)
        {
            InputBox inputForm = new InputBox(title, promptText, value, passwordWildcard);

            inputForm.ShowDialog();
            value = inputForm.InputValue;

            return inputForm.DialogResult;
        }


        public static void SaveAndProtectUserConfig()
        {
            try
            {
                Properties.Settings.Default.Save();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Generate a short uniqueness of 10 characters
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueness()
        {
            return Guid.NewGuid().ToString().Substring(0, 11).Replace("-", "");
        }

        public class LiveOutputExt
        {
            public LiveOutput LiveOutputItem { get; set; }
            public string LiveEventName { get; set; }
        }

        // set WebBrowser features, more info: http://stackoverflow.com/a/18333982/1768303
        public static void SetWebBrowserFeatures()
        {
            // don't change the registry if running in-proc inside Visual Studio
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
            {
                return;
            }

            string appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            string featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";

            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, GetBrowserEmulationMode(), RegistryValueKind.DWord);

            // enable the features which are "On" for the full Internet Explorer browser

            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_AJAX_CONNECTIONEVENTS",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_GPU_RENDERING",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_WEBOC_DOCUMENT_ZOOM",
                appName, 1, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_NINPUT_LEGACYMODE",
                appName, 0, RegistryValueKind.DWord);

        }

        private static uint GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                    break;
                default:
                    // use IE11 mode by default
                    break;
            }

            return mode;
        }
    }

    public class Constants
    {
        public const string GitHubAMSEVersionPrimaryV3 = "https://amsexplorer.azureedge.net/release/versionv3.json";
        public const string GitHubAMSEVersionSecondaryV3 = "https://raw.githubusercontent.com/Azure/Azure-Media-Services-Explorer/main/versionv3.json";

        public const string GitHubAMSEReleases = "https://github.com/Azure/Azure-Media-Services-Explorer/releases";
        public const string GitHubAMSELink = "http://aka.ms/amse";

        public const string NameconvInputasset = "{Input Asset Name}";
        public const string NameconvUploadasset = "{File Name}";
        public const string NameconvWorkflow = "{Workflow}";
        public const string NameconvTemplate = "{Template}";
        public const string NameconvFormathls = "{Format}";
        public const string NameconvEncodername = "{Encoder}";
        public const string NameconvLiveEvent = "{LiveEvent}";
        public const string NameconvLiveOutput = "{LiveOutput}";
        public const string NameconvProtocols = "{Protocols}";
        public const string NameconvContentKeyType = "{Content key type}";
        public const string NameconvManifestURL = "{manifest url}";
        public const string NameconvToken = "{token}";
        public const string NameconvAsset = "{Asset Name}";
        public const string NameconvJob = "{Job Name}";
        public const string NameconvTransform = "{Transform Name}";
        public const string NameconvShortUniqueness = "{Uniqueness}";
        public const string NameconvFileName = "{File Name}";
        public const string NameconvUrl = "{Url}";

        public const string endline = "\r\n";

        public const string PathPremiumWorkflowFiles = @"\PremiumWorkflowSamples\";
        public const string PathMESFiles = @"\MESPresetFiles\";
        public const string PathConfigFiles = @"\configurations\";
        public const string PathManifestFile = @"\manifest\";
        public const string PathHelpFiles = @"\HelpFiles\";
        public const string PathDefaultSlateJPG = @"\SlateJPG\";

        public const string PathLicense = @"\license\Azure Media Services Explorer.rtf";

        public const string PlayerAMPinOptions = @"https://ampdemo.azureedge.net/azuremediaplayer.html?player=flash&format=smooth&url={0}";
        public const string PlayerAMP = @"https://aka.ms/azuremediaplayer";
        public const string PlayerAMPToLaunch = @"https://aka.ms/azuremediaplayer?url={0}";

        public const string PlayerAMPIFrameToLaunch = @"https://ampdemo.azureedge.net/azuremediaplayer_embed.html?autoplay=true&url={0}";
        public const string AMPprotectionsyntax = "&protection={0}";
        public const string AMPtokensyntax = "&token={0}";
        public const string AMPformatsyntax = "&format={0}";
        public const string AMPtechsyntax = "&tech={0}";
        public const string AMPPlayReady = "&playready={0}";
        public const string AMPPlayReadyToken = "&playreadytoken={0}";
        public const string AMPWidevine = "&widevine={0}";
        public const string AMPWidevineToken = "&widevinetoken={0}";
        public const string AMPAes = "&aes={0}";
        public const string AMPAesToken = "&aestoken={0}";
        public const string AMPSubtitles = "&subtitles={0}";

        public const string PlayerDASHIFList = @"http://reference.dashif.org/dash.js/";
        public const string PlayerDASHIFToLaunch = @"http://reference.dashif.org/dash.js/latest/samples/dash-if-reference-player/index.html?url={0}";

        public const string PlayerMP4AzurePage = @"https://ampdemo.azureedge.net/azuremediaplayer.html?player=html5&format=mp4&url={0}&mp4url={0}";
        public const string AdvancedTestPlayerRoot = @"https://openidconnectweb.azurewebsites.net/AMTestPlayer";
        public const string AdvancedTestPlayer = AdvancedTestPlayerRoot + @"?url={0}";

        public const string PlayerInfoHTML5Video = @"http://www.w3schools.com/html/html5_video.asp";
        public const string PlayerJWPlayerPartnership = @"https://www.jwplayer.com/";
        public const string PlayerTHEOplayerPartnership = @"https://www.theoplayer.com/partners/azure";

        public const string DemoCaptionMaker = @"https://testdrive-archive.azurewebsites.net/Graphics/CaptionMaker/Default.html";

        public const string LinkFeedbackAMS = "http://aka.ms/amsvoice";
        public const string LinkInfoMediaUnit = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-scale-media-processing-overview";

        public const string TemporaryWidevineLicenseServer = "https://thiswillbereplacedbytheAMSwidevineurl/?KID=00000000-0000-0000-0000-000000000000";

        public static readonly string[] BrowserEdge = { "Microsoft Edge", "microsoft-edge:" };
        public static readonly string[] BrowserChrome = { "Google Chrome", "chrome.exe" };
        internal static string mpd = ".mpd";
        internal static string m3u8 = ".m3u8";
        public const string AssetIdPrefix = "nb:cid:UUID:";

        public const string Bearer = "Bearer ";
        public const string strUnits = "{0} unit{1}";

        public const string LinkAzureUpdates = @"https://azure.microsoft.com/en-us/updates/?product=cdn,media-services";
        public const string LinkMoreInfoAMSReleaseNotes = @"https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes";
        public const string LinkMoreInfoDocAMS = @"https://docs.microsoft.com/en-us/azure/media-services/";
        public const string LinkForumAMS = @"https://social.msdn.microsoft.com/Forums/azure/en-US/home?forum=MediaServices";
        public const string LinkBlogAMS = @"https://azure.microsoft.com/en-us/blog/topics/media-services/";

        public const string LinkMoreInfoSE = "https://docs.microsoft.com/en-us/azure/media-services/latest/stream-streaming-endpoint-concept";
        public const string LinkMoreInfoAzCopy = "https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azcopy-v10";

        public const string LinkMoreInfoVideoAnalyzer = "https://docs.microsoft.com/en-us/azure/media-services/latest/analyzing-video-audio-files-concept";
        public const string LinkMoreInfoMediaEncoderBuiltIn = "https://docs.microsoft.com/en-us/azure/media-services/latest/encoding-concept";
        public const string LinkMoreInfoMediaEncoderThumbnail = "https://docs.microsoft.com/en-us/azure/media-services/latest/transform-generate-thumbnails-dotnet-how-to";

        public const string LinkHowIMoreInfoDynamicManifest = "https://docs.microsoft.com/en-us/azure/media-services/latest/filters-dynamic-manifest-concept";
        public const string LinkHowIMoreInfoSubclipping = "https://azure.microsoft.com/en-us/blog/dynamic-manifests-and-rendered-sub-clips/";
        public const string LinkMoreInfoSubClipAMSE = "https://azure.microsoft.com/en-us/blog/sub-clipping-and-live-archive-extraction-with-media-encoder-standard/";
        public const string LinkMoreInfoLiveEncoding = "https://docs.microsoft.com/en-us/azure/media-services/latest/stream-live-streaming-concept#live-encoding";
        public const string LinkMoreInfoLiveStreaming = "https://docs.microsoft.com/en-us/azure/media-services/latest/stream-live-streaming-concept";
        public const string LinkMoreInfoPricing = "https://azure.microsoft.com/en-us/pricing/details/media-services/";
        public const string LinkMoreInfoStorageVersioning = "https://docs.microsoft.com/en-us/rest/api/storageservices/Versioning-for-the-Azure-Storage-Services";
        public const string LinkMoreInfoStorageAnalytics = "https://docs.microsoft.com/en-us/azure/storage/blobs/monitor-blob-storage";
        public const string LinkMoreInfoFairPlay = "https://docs.microsoft.com/en-us/azure/media-services/latest/drm-fairplay-license-overview";
        public const string LinkMoreInfoTelemetry = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-telemetry-overview";

        public const string LinkPlayReadyTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/latest/drm-playready-license-template-concept";
        public const string LinkPlayReadyCompliance = "http://www.microsoft.com/playready/licensing/compliance/";
        public const string LinkWidevineTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/latest/drm-widevine-license-template-concept";

        public const string LinkAMSCreateAccount = "https://docs.microsoft.com/en-us/azure/media-services/latest/account-create-how-to";
        public const string LinkAMSAADAut = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-portal-get-started-with-aad";
        public const string LinkAMSAzCli = "https://docs.microsoft.com/en-us/azure/media-services/latest/access-api-howto";

        public const string LinkAMSE = "http://aka.ms/amse";
        public const string LinkMailtoAMSE = "mailto:amse@microsoft.com?subject=Azure Media Services Explorer - Question/Comment";
        public const string LinkReportBugAMSE = @"https://github.com/Azure/Azure-Media-Services-Explorer/issues";
        public const string LinkAMSEReleaseNotes = @"https://rawgit.com/Azure/Azure-Media-Services-Explorer/main/AllReleaseNotes.html";

        public const long maxSlateJPGFileSize = 3 * 1000 * 1000; // Max 3 MB
        public const int maxSlateJPGHorizontalResolution = 1920;
        public const int maxSlateJPGVerticalResolution = 1080;
        public const double SlateJPGAspectRatio = 16d / 9d;
        public const string SlateJPGExtension = ".jpg";

        public const string stringNull = "(null)"; // To display null is textbox

        public const int MaxTransfersAsUnlimited = 5;
        public const string strTransfers = "{0} concurrent transfer{1}";

        public const string LinkMoreInfoLiveTranscript = "https://docs.microsoft.com/en-us/azure/media-services/latest/live-event-live-transcription-how-to";
        public const string LinkMoreInfoLiveTranscriptRegions = "https://docs.microsoft.com/en-us/azure/media-services/latest/azure-clouds-regions#feature-availability-in-preview";
    }


    public class JobEntryV3
    {
        public string Name { get; set; }
        public string TransformName { get; set; }
        public string Description { get; set; }
        public int Outputs { get; set; }
        public Priority? Priority { get; set; }
        public Microsoft.Azure.Management.Media.Models.JobState State { get; set; }
        public string StartTime { get; set; }
        public string LastModified { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public double Progress { get; set; }
    }

    public class JobExtension
    {
        public Job Job { get; set; }
        public string TransformName { get; set; }
    }


    public class TransformEntryV3 : INotifyPropertyChanged
    {
        private readonly SynchronizationContext syncContext;

        public TransformEntryV3(SynchronizationContext mysyncContext)
        {
            syncContext = mysyncContext;
        }

        public string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string _Description;
        public string Description
        {
            get => _Description;
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int _Outputs;
        public int Outputs
        {
            get => _Outputs;
            set
            {
                if (value != _Outputs)
                {
                    _Outputs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int _Jobs;
        public int Jobs
        {
            get => _Jobs;
            set
            {
                if (value != _Jobs)
                {
                    _Jobs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _LastModified;
        public string LastModified
        {
            get => _LastModified;
            set
            {
                if (value != _LastModified)
                {
                    _LastModified = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /*
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string p = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
        */

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string p = "")
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChangedEventHandler handler = PropertyChanged;

                    if (syncContext != null)
                        syncContext.Post(_ => handler(this, new PropertyChangedEventArgs(p)), null);
                    else
                        handler(this, new PropertyChangedEventArgs(p));

                }
                catch
                {

                }
            }
        }
    }


    public class ExplorerOpenIDSample
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }


    public class ListCredentialsRPv3
    {
        public decimal Version = 3;
        public List<CredentialsEntryV3> MediaServicesAccounts = new List<CredentialsEntryV3>();
    }

    public class LiveOutputUtil
    {
        public static string ReturnLiveEventFromOutput(LiveOutput liveoutput)

        {
            string[] idParts = liveoutput.Id.Split('/');
            return idParts[10];
        }
    }
    public class AssetInfoData
    {
        public long Size;
        public string Type;
        public IEnumerable<IListBlobItem> Blobs;
    }

    public class JsonFromAzureCliOrPortal
    {
        public string AadClientId { get; set; }
        public Uri AadEndpoint { get; set; }
        public string AadSecret { get; set; }
        public string AadTenantId { get; set; }
        public string AccountName { get; set; }
        public Uri ArmAadAudience { get; set; }
        public Uri ArmEndpoint { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string ResourceGroup { get; set; }
        public string SubscriptionId { get; set; }
    }

    public class AMSClientV3
    {
        public AzureMediaServicesClient AMSclient;
        public AuthenticationResult authResult, authResultForRestV2;
        public CredentialsEntryV3 credentialsEntry;
        public TokenCredentials credentials;
        public AzureEnvironment environment;
        private readonly string _azureSubscriptionId;

        public AMSClientV3(AzureEnvironment myEnvironment, string azureSubscriptionId, CredentialsEntryV3 myCredentialsEntry)
        {
            environment = myEnvironment;
            _azureSubscriptionId = azureSubscriptionId;
            credentialsEntry = myCredentialsEntry;
        }

        public void RefreshTokenIfNeeded()
        {
            // if end user use interactive authentication, then the token is not renewed automatically after it expired (one hour)
            // with Service Principal authentication, the token is renewed automatically apparently ! (result of tests)
            if (authResult != null && authResult.ExpiresOn.ToUniversalTime() < DateTimeOffset.UtcNow.AddMinutes(-3))
            {
                ConnectAndGetNewClientV3Async().GetAwaiter().GetResult();
            }
        }

        public async Task RefreshTokenIfNeededAsync()
        {
            // if end user use interactive authentication, then the token is not renewed automatically after it expired (one hour)
            // with Service Principal authentication, the token is renewed automatically apparently ! (result of tests)
            if (authResult != null && authResult.ExpiresOn.ToUniversalTime() < DateTimeOffset.UtcNow.AddMinutes(-3))
            {
                await ConnectAndGetNewClientV3Async();
            }
        }

        public async Task<AzureMediaServicesClient> ConnectAndGetNewClientV3Async()
        {
            if (!credentialsEntry.UseSPAuth)
            {
                // we specify the tenant id if there
                //AuthenticationContext authContext = new AuthenticationContext(authority: environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "common"), validateAuthority: true);

                var scopes = new[] { environment.AADSettings.TokenAudience.ToString() + "/user_impersonation" };

                IPublicClientApplication app = PublicClientApplicationBuilder.Create(environment.ClientApplicationId)
                    //.WithAuthority(AzureCloudInstance.AzurePublic, credentialsEntry.AadTenantId)
                    .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "common"))
                    .WithRedirectUri("http://localhost")
                    .Build();


                var accounts = await app.GetAccountsAsync();

                try
                {
                    authResult = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync().ConfigureAwait(false); ;
                }
                catch (MsalUiRequiredException ex)
                {
                    try
                    {
                        authResult = await app.AcquireTokenInteractive(scopes).WithPrompt(credentialsEntry.PromptUser ? Prompt.ForceLogin : Prompt.NoPrompt).ExecuteAsync().ConfigureAwait(false);
                    }
                    catch (MsalException maslException)
                    {
                        Debug.Print("MSAL interactive authentication exception !" + maslException.Message);
                    }
                }
                catch (MsalException maslException)
                {
                    Debug.Print("MSAL silent authentication exception !" + maslException.Message);
                }


                try
                {
                    var scopes2 = new[] { environment.MediaServicesV2Resource + "/user_impersonation" };

                    authResultForRestV2 = await app.AcquireTokenSilent(scopes2, authResult.Account).ExecuteAsync().ConfigureAwait(false);
                }
                catch
                {

                }
            }

            else // Service Principal
            {
                AmsLoginServicePrincipal form = new AmsLoginServicePrincipal
                {
                    ClientId = credentialsEntry.ADSPClientId,
                    ClientSecret = credentialsEntry.ClearADSPClientSecret
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    credentialsEntry.ADSPClientId = form.ClientId;
                    credentialsEntry.ClearADSPClientSecret = form.ClientSecret;

                    var scopes3 = new[] { environment.AADSettings.TokenAudience.ToString() + "/.default" };


                    var app3 = ConfidentialClientApplicationBuilder.Create(credentialsEntry.ADSPClientId)
                        .WithClientSecret(credentialsEntry.ClearADSPClientSecret)
                         .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "common"), true)
                        .Build();

                    // IByRefreshToken appRt = app3 as IByRefreshToken;

                    authResult = await app3.AcquireTokenForClient(scopes3)
                                                             .ExecuteAsync()
                                                             .ConfigureAwait(false);

                }
                else
                {
                    return null;
                }



                //var accounts = await app3.GetAccountsAsync();


                /*
                      IPublicClientApplication app = PublicClientApplicationBuilder.Create(environment.ClientApplicationId)
                    .WithAuthority(environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "common"))
                    .WithRedirectUri("http://localhost")
                    .Build();
                */



                // let comment for now
                /*
                // other code for service principal
                AmsLoginServicePrincipal form = new AmsLoginServicePrincipal
                {
                    ClientId = credentialsEntry.ADSPClientId,
                    ClientSecret = credentialsEntry.ClearADSPClientSecret
                };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    credentialsEntry.ADSPClientId = form.ClientId;
                    credentialsEntry.ClearADSPClientSecret = form.ClientSecret;

                    ClientCredential clientCredential = new ClientCredential(credentialsEntry.ADSPClientId, credentialsEntry.ClearADSPClientSecret);

                    ActiveDirectoryServiceSettings set = new ActiveDirectoryServiceSettings()
                    {
                        AuthenticationEndpoint = credentialsEntry.Environment.AADSettings.AuthenticationEndpoint,
                        TokenAudience = credentialsEntry.Environment.AADSettings.TokenAudience,
                        ValidateAuthority = true
                    };
                    ServiceClientCredentials cred = await ApplicationTokenProvider.LoginSilentAsync(credentialsEntry.AadTenantId, clientCredential, set);

                    // Getting Media Services accounts...
                    AMSclient = new AzureMediaServicesClient(environment.ArmEndpoint, cred)
                    {
                        SubscriptionId = _azureSubscriptionId
                    };
                }
                else
                {
                    return null;
                }
                */
            }

            credentials = new TokenCredentials(authResult.AccessToken, "Bearer");

            // Getting Media Services account...
            AMSclient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials)
            {
                SubscriptionId = _azureSubscriptionId
            };

            // let's get info on mediaService, specifically to get the location (region)
            credentialsEntry.MediaService = await AMSclient.Mediaservices.GetAsync(credentialsEntry.ResourceGroup, credentialsEntry.AccountName);

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AMSclient.SetUserAgent("AMSE", version);
            return AMSclient;
        }



        public static string GetStorageName(string storageId)
        {
            return storageId.Split('/').Last();
        }

        public static string GetStorageResourceName(string storageId)
        {
            string[] split = storageId.Split('/');
            return storageId.Split('/')[split.Count() - 5];
        }
    }

    public class CredentialsEntryV3 : IEquatable<CredentialsEntryV3>
    {
        public MediaService MediaService;

        public string ADSPClientId;

        //  A contract is used to ignore this property when exporting the entry
        public string EncryptedADSPClientSecret;

        public string AadTenantId;
        public AzureEnvironment Environment;
        public bool PromptUser;
        public bool ManualConfig = false;
        public bool UseSPAuth = false;
        public string Description;

        public CredentialsEntryV3(MediaService mediaService, AzureEnvironment environment, bool promptUser, bool useSPAuth = false, string tenantId = null, bool manualConfig = false, string adSPClientId = null, string clearADSPClientSecret = null)
        {
            MediaService = mediaService;
            Environment = environment;
            UseSPAuth = useSPAuth;
            PromptUser = promptUser;
            ManualConfig = manualConfig;
            AadTenantId = tenantId;
            if (useSPAuth)
            {
                ADSPClientId = adSPClientId;
                ClearADSPClientSecret = clearADSPClientSecret;
            }
        }

        public string AccountName => MediaService.Name;

        public string ResourceGroup
        {
            get
            {
                string[] idParts = MediaService.Id.Split('/');
                return idParts[4];
            }
        }

        public string AzureSubscriptionId
        {
            get
            {
                string[] idParts = MediaService.Id.Split('/');
                return idParts[2];
            }
        }

        //  A contract is used to ignore this property when saveing settings to disk
        public string ClearADSPClientSecret
        {
            get => EncryptedADSPClientSecret != null ? DecryptSecret(EncryptedADSPClientSecret) : null;
            set => EncryptedADSPClientSecret = (value != null) ? EncryptSecret(value) : null;
        }

        public bool Equals(CredentialsEntryV3 other)
        {
            return false;
            /* To implement
                (this.AccountKey ?? "") == (other.AccountKey ?? "")
                && (this.AccountName ?? "") == (other.AccountName ?? "")
                && (this.ADRestAPIEndpoint ?? "") == (other.ADRestAPIEndpoint ?? "")
                && (this.ADTenantDomain ?? "") == (other.ADTenantDomain ?? "")
                && this.UseAADInteract == other.UseAADInteract
                && this.UseAADServicePrincipal == other.UseAADServicePrincipal
                && (this.ADDeploymentName ?? "") == (other.ADDeploymentName ?? "")
                && (this.ADCustomSettings) == (other.ADCustomSettings)
                && this.UseOtherAPI == other.UseOtherAPI
                && this.UsePartnerAPI == other.UsePartnerAPI
                && (this.Description ?? "") == (other.Description ?? "")
                && (this.OtherACSBaseAddress ?? "") == (other.OtherACSBaseAddress ?? "")
                && (this.OtherAPIServer ?? "") == (other.OtherAPIServer ?? "")
                && (this.OtherAzureEndpoint ?? "") == (other.OtherAzureEndpoint ?? "")
                && (this.OtherManagementPortal ?? "") == (other.OtherManagementPortal ?? "")
                && (this.OtherScope ?? "") == (other.OtherScope ?? "")
                && (this.DefaultStorageKey ?? "") == (other.DefaultStorageKey ?? "")
                 ;
                 */
        }

        private string EncryptSecret(string clientSecretClear)
        {
            // Create the original data to be encrypted
            byte[] toEncrypt = UnicodeEncoding.ASCII.GetBytes(clientSecretClear);

            byte[] encryptedSecret = Protect(toEncrypt);

            return Convert.ToBase64String(encryptedSecret);
        }

        private string DecryptSecret(string clientSecretEncrypted)
        {
            // Create the original data to be encrypted (The data length should be a multiple of 16).
            byte[] toDecrypt = Convert.FromBase64String(clientSecretEncrypted);


            // Decrypt the data and store in a byte array.
            byte[] originalData = Unprotect(toDecrypt);
            return UnicodeEncoding.ASCII.GetString(originalData);

        }

        private static readonly byte[] s_aditionalEntropy = { 9, 1, 4, 5, 5 };

        public static byte[] Protect(byte[] data)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
                //  only by the same current user.
                return ProtectedData.Protect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            try
            {
                //Decrypt the data using DataProtectionScope.CurrentUser.
                return ProtectedData.Unprotect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }

    public class RepData
    {
        public string label;
        public string value;
    }

    public class ListRepData
    {
        public List<RepData> data;
        public ListRepData()
        {
            data = new List<RepData>();
        }
        public void Add(string label, string value)
        {
            data.Add(new RepData() { label = label, value = value ?? string.Empty });
        }

        public void Add(ListRepData listRepData)
        {
            data.AddRange(listRepData.data);
        }

        public void Add(string label)
        {
            data.Add(new RepData() { label = label, value = null });
        }

        public StringBuilder ReturnStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            // calculate padding max
            int maxLenghtStr = data.Where(d => d.value != null).Select(d => d.label.Length).Max();

            // build StringBuilder
            data.Select(d => d.value != null ? d.label + new string(' ', maxLenghtStr - d.label.Length) + ": " + d.value : d.label).ToList().ForEach(s => sb.AppendLine(s));

            return sb;
        }
    }

    public enum AzureEnvType
    {
        Azure = 0,
        DevTest,
        AzureChina,
        AzureUSGovernment,
        AzureGermany,
        Custom
    }

    public class AzureEnvironment
    {
        public string DisplayName { get; set; }
        //public string Authority { get; set; }
        public Uri ArmEndpoint { get; set; }
        public string ClientApplicationId { get; set; }
        public string MediaServicesV2Resource { get; set; }
        public ActiveDirectoryServiceSettings AADSettings { get; set; }


        public AzureEnvironment(AzureEnvType type)
        {
            switch (type)
            {
                case AzureEnvType.DevTest:
                    DisplayName = "Azure Dev/Test";
                    ArmEndpoint = new Uri("https://api-dogfood.resources.windows-int.net/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = new ActiveDirectoryServiceSettings() { TokenAudience = new Uri("https://management.core.windows.net/"), ValidateAuthority = true, AuthenticationEndpoint = new Uri("https://login.windows-ppe.net/") };
                    MediaServicesV2Resource = "https://rest.media.azure-test.net";
                    break;

                case AzureEnvType.Azure:
                    DisplayName = "Azure";
                    ArmEndpoint = new Uri("https://management.azure.com/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.Azure;
                    MediaServicesV2Resource = "https://rest.media.azure.net";
                    break;

                case AzureEnvType.AzureChina:
                    DisplayName = "Azure China";
                    ArmEndpoint = new Uri("https://management.chinacloudapi.cn/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.AzureChina;
                    MediaServicesV2Resource = "https://rest.media.chinacloudapi.cn";
                    break;

                case AzureEnvType.AzureUSGovernment:
                    DisplayName = "Azure US Government";
                    ArmEndpoint = new Uri("https://management.usgovcloudapi.net/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.AzureUSGovernment;
                    MediaServicesV2Resource = "https://rest.media.usgovcloudapi.net";
                    break;

                case AzureEnvType.AzureGermany:
                    DisplayName = "Azure Germany";
                    ArmEndpoint = new Uri("https://management.cloudapi.de/");
                    ClientApplicationId = "04b07795-8ddb-461a-bbee-02f9e1bf7b46";
                    AADSettings = ActiveDirectoryServiceSettings.AzureGermany;
                    MediaServicesV2Resource = "https://rest.media.cloudapi.de";
                    break;

                case AzureEnvType.Custom:
                    DisplayName = "Custom";
                    ArmEndpoint = null;
                    ClientApplicationId = string.Empty;
                    AADSettings = new ActiveDirectoryServiceSettings();
                    MediaServicesV2Resource = null;
                    break;
            }
        }

        public string ReturnStorageSuffix()
        {
            return "core." + ReturnHostNameTwoSegmentsRight(AADSettings.TokenAudience.ToString()); // "core.cloudapi.de"
        }

        private string ReturnHostNameTwoSegmentsRight(string myUrl)
        {
            string[] hosts = (new Uri(myUrl)).Host.Split('.');
            int i = hosts.Count();
            return hosts[i - 2] + "." + hosts[i - 1];
        }
    }

    public enum PlayerType
    {
        AzureMediaPlayer = 0,
        AzureMediaPlayerFrame,
        AzureMediaPlayerClear,
        DASHIFRefPlayer,
        MP4AzurePage,
        AdvancedTestPlayer,
        CustomPlayer
    }


    public enum PublishStatus
    {
        NotPublished = 0,
        PublishedActive,
        PublishedFuture,
        PublishedExpired
    }

    public enum AzureMediaPlayerFormats
    {
        Auto = 0,
        Smooth = 1,
        Dash = 2,
        HLS = 3,
        VideoMP4 = 4
    }

    public enum AMSOutputProtocols
    {
        NotSpecified = 0,
        Smooth,
        SmoothLegacy,
        DashCsf,
        HLSv3,
        HLSv4,
        HLSCmaf,
        DashCmaf
    }

    public enum AzureMediaPlayerTechnologies
    {
        Auto = 0,
        JavaScript,
        Flash,
        Silverlight,
        NativeHTML5
    }


    public enum AssetProtectionType
    {
        None = 0,
        AES,
        PlayReady,
        Widevine,
        PlayReadyAndWidevine,
        PlayReadyAndWidevineAndFairplay

    }


    public class ManifestTimingData
    {
        public TimeSpan AssetDuration { get; set; }
        public ulong TimestampOffset { get; set; }
        public long? TimeScale { get; set; }
        public bool IsLive { get; set; }
        public bool Error { get; set; }
        public List<ulong> TimestampList { get; set; }
        public ulong TimestampEndLastChunk { get; set; }
        public bool DiscontinuityDetected { get; set; }
    }


    public class FilterCreationInfo
    {
        public string Name { get; set; }  // contains the full configuration for subclipping
        public Microsoft.Azure.Management.Media.Models.FirstQuality Firstquality { get; set; }
        public Microsoft.Azure.Management.Media.Models.PresentationTimeRange Presentationtimerange { get; set; }
        public IList<FilterTrackSelection> Tracks { get; set; }
    }
    public class SubClipConfiguration
    {
        public bool Reencode { get; set; }
        public bool Trimming { get; set; }
        public bool CreateAssetFilter { get; set; }
        public TimeSpan AbsoluteStartTime { get; set; }
        public TimeSpan AbsoluteEndTime { get; set; }
    }

    internal class HostNameClass
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


    public enum SearchIn
    {
        AssetNameEquals = 0,
        AssetNameStartsWith,
        AssetNameGreaterThan,
        AssetNameLessThan,
        AssetId,
        AssetAltId,
        AssetFileName,
        AssetFileId,
        LocatorId,
        JobName,
        JobId,
        TaskName,
        TaskId,
        TaskProcessorId,
        LiveEventName,
        LiveEventId,
        LiveOutputName,
        LiveOutputId
    }

    public enum DownloadToFolderOption
    {
        DoNotCreateSubfolder = 0,
        SubfolderAssetName
    }

    public class SearchObject
    {
        public string Text { get; set; }
        public SearchIn SearchType { get; set; }

    }

    public class LocatorAndUrls
    {
        public List<StreamingPath> Paths { get; set; }
        public string LocatorName { get; set; }
        public Guid? LocatorId { get; set; }
        public string AssetName { get; set; }
    }


    public sealed class FilterPropertyFourCCValue
    {
        public static readonly string mp4a = "mp4a";
        public static readonly string avc1 = "avc1";
        public static readonly string ec3 = "ec-3";
        public static readonly string hev1 = "hev1";
        public static readonly string hvc1 = "hvc1";
    }

    public sealed class FilterPropertyTypeValue
    {
        public static readonly string Audio = "audio";
        public static readonly string Video = "video";
        public static readonly string Text = "text";
    }


    public class ExFilterTrack
    {
        public List<ExCondition> Conditions { get; set; }
    }

    public class ExCondition
    {
        public string Property { get; set; }
        public string Oper { get; set; }
        public string Value { get; set; }
    }


    public class ListViewItemComparer : IComparer
    {
        private readonly int col;
        private readonly SortOrder order;
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
            int returnVal = 0;
            bool Error = false;

            string sx = ((ListViewItem)x).SubItems[col].Text;
            string sy = ((ListViewItem)y).SubItems[col].Text;
            // Determine whether the type being compared is a date type.

            // Inverse_FormatByteSize
            // let's compare if the field is a size format (512 B or 45 KB...)

            // Parse the two objects passed as a parameter as a DateTime.
            try
            {
                long? firstSize = AssetInfo.Inverse_FormatByteSize(sx);
                long? secondSize = AssetInfo.Inverse_FormatByteSize(sy);
                if (firstSize != null && secondSize != null)
                {
                    long firstSizel = (long)firstSize;
                    long secondSizel = (long)secondSize;
                    if (firstSizel < secondSizel)
                    {
                        returnVal = -1;
                    }
                    else if (firstSizel > secondSizel)
                    {
                        returnVal = 1;
                    }
                    else
                    {
                        returnVal = 0;
                    }
                }
                else
                {
                    Error = true;
                }
            }
            catch
            {
                Error = true;
            }

            if (Error)
            {
                /*
                try
                {
                    // Parse the two objects passed as a parameter as a DateTime.
                    System.DateTime firstDate =
                            DateTime.Parse(sx); 
                    System.DateTime secondDate =
                            DateTime.Parse(sy);
                    // Compare the two dates.
                    returnVal = DateTime.Compare(firstDate, secondDate);
                }
                // If neither compared object has a valid date format, compare
                // as a string.
                catch
                {
                    // Compare the two items as a string.
                    returnVal = String.Compare(sx, sy);
                }
                */

                // Parse the two objects passed as a parameter as a DateTime.
                if (DateTime.TryParse(sx, out DateTime firstDate) && DateTime.TryParse(sy, out DateTime secondDate))
                {
                    returnVal = DateTime.Compare(firstDate, secondDate);
                }
                else
                {
                    returnVal = string.Compare(sx, sy);
                }
            }

            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
            {
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            }

            return returnVal;
        }

        public static void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
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
                {
                    ThisListView.Sorting = SortOrder.Descending;
                }
                else
                {
                    ThisListView.Sorting = SortOrder.Ascending;
                }
            }

            // Call the sort method to manually sort.
            ThisListView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            ThisListView.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              ThisListView.Sorting);
        }
    }

    public class ListViewItemComparerQuickNoDate : IComparer
    {
        private readonly int col;
        private readonly SortOrder order;
        public ListViewItemComparerQuickNoDate()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparerQuickNoDate(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = 0;
            bool Error = false;

            string sx = ((ListViewItem)x).SubItems[col].Text;
            string sy = ((ListViewItem)y).SubItems[col].Text;


            // Inverse_FormatByteSize
            // let's compare if the field is a size format (512 B or 45 KB...)

            // Parse the two objects passed as a parameter as a DateTime.
            try
            {
                long? firstSize = AssetInfo.Inverse_FormatByteSize(sx);
                long? secondSize = AssetInfo.Inverse_FormatByteSize(sy);
                if (firstSize != null && secondSize != null)
                {
                    long firstSizel = (long)firstSize;
                    long secondSizel = (long)secondSize;
                    if (firstSizel < secondSizel)
                    {
                        returnVal = -1;
                    }
                    else if (firstSizel > secondSizel)
                    {
                        returnVal = 1;
                    }
                    else
                    {
                        returnVal = 0;
                    }
                }
                else
                {
                    Error = true;
                }

            }
            catch
            {
                Error = true;
            }

            if (Error)
            {
                // Compare the two items as a string.
                returnVal = string.Compare(sx, sy);
            }


            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
            {
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            }

            return returnVal;
        }

        public static void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
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
                {
                    ThisListView.Sorting = SortOrder.Descending;
                }
                else
                {
                    ThisListView.Sorting = SortOrder.Ascending;
                }
            }

            // Call the sort method to manually sort.
            ThisListView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            ThisListView.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              ThisListView.Sorting);
        }
    }


    public class PropertyRenameAndIgnoreSerializerContractResolver : DefaultContractResolver
    {
        private readonly Dictionary<Type, HashSet<string>> _ignores;
        private readonly Dictionary<Type, Dictionary<string, string>> _renames;

        public PropertyRenameAndIgnoreSerializerContractResolver()
        {
            _ignores = new Dictionary<Type, HashSet<string>>();
            _renames = new Dictionary<Type, Dictionary<string, string>>();
        }

        public void IgnoreProperty(Type type, params string[] jsonPropertyNames)
        {
            if (!_ignores.ContainsKey(type))
            {
                _ignores[type] = new HashSet<string>();
            }

            foreach (string prop in jsonPropertyNames)
            {
                _ignores[type].Add(prop);
            }
        }

        public void RenameProperty(Type type, string propertyName, string newJsonPropertyName)
        {
            if (!_renames.ContainsKey(type))
            {
                _renames[type] = new Dictionary<string, string>();
            }

            _renames[type][propertyName] = newJsonPropertyName;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (IsIgnored(property.DeclaringType, property.PropertyName))
            {
                property.ShouldSerialize = i => false;
                property.Ignored = true;
            }

            if (IsRenamed(property.DeclaringType, property.PropertyName, out string newJsonPropertyName))
            {
                property.PropertyName = newJsonPropertyName;
            }

            return property;
        }

        private bool IsIgnored(Type type, string jsonPropertyName)
        {
            if (!_ignores.ContainsKey(type))
            {
                return false;
            }

            return _ignores[type].Contains(jsonPropertyName);
        }

        private bool IsRenamed(Type type, string jsonPropertyName, out string newJsonPropertyName)
        {

            if (!_renames.TryGetValue(type, out Dictionary<string, string> renames) || !renames.TryGetValue(jsonPropertyName, out newJsonPropertyName))
            {
                newJsonPropertyName = null;
                return false;
            }

            return true;
        }
    }



    public static class Extensions
    {
        public static void Invoke<TControlType>(this TControlType control, Action<TControlType> del)
            where TControlType : Control
        {
            if (control.InvokeRequired)
                control.Invoke(new Action(() => del(control)));
            else
                del(control);
        }
    }

}
