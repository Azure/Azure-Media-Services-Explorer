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

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.Storage;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Win32;
using Microsoft.Azure.Storage.Blob;
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
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage;

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
                if (e.GetType() == typeof(ApiErrorException))
                {
                    s = ((ApiErrorException)e).Body?.Error?.Message;
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

        public static async Task<ManifestGenerated> LoadAndUpdateManifestTemplateAsync(CloudBlobContainer container)
        {
            // Let's list the blobs
            BlobContinuationToken continuationToken = null;
            List<IListBlobItem> allBlobs = new List<IListBlobItem>();
            do
            {
                BlobResultSegment segment = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.Metadata, null, continuationToken, null, null);
                allBlobs.AddRange(segment.Results);
                continuationToken = segment.ContinuationToken;
            }
            while (continuationToken != null);

            IEnumerable<CloudBlockBlob> blobs = allBlobs.Where(c => c.GetType() == typeof(CloudBlockBlob)).Select(c => c as CloudBlockBlob);

            CloudBlockBlob[] mp4AssetFiles = blobs.Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)).ToArray();
            CloudBlockBlob[] m4aAssetFiles = blobs.Where(f => f.Name.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase)).ToArray();
            CloudBlockBlob[] mediaAssetFiles = blobs.Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith(".m4a", StringComparison.OrdinalIgnoreCase)).ToArray();

            if (mediaAssetFiles.Count() != 0)
            {
                // Prepare the manifest
                string mp4fileuniqueaudio = null;
                XDocument doc = XDocument.Load(Path.Combine(Application.StartupPath + Constants.PathManifestFile, @"Manifest.ism"));

                XNamespace ns = "http://www.w3.org/2001/SMIL20/Language";

                XElement bodyxml = doc.Element(ns + "smil");
                XElement body2 = bodyxml.Element(ns + "body");

                XElement switchxml = body2.Element(ns + "switch");

                // audio tracks (m4a)
                foreach (CloudBlockBlob file in m4aAssetFiles)
                {
                    switchxml.Add(new XElement(ns + "audio", new XAttribute("src", file.Name), new XAttribute("title", Path.GetFileNameWithoutExtension(file.Name))));
                }

                if (m4aAssetFiles.Count() == 0)
                {
                    // audio track(s)
                    IEnumerable<CloudBlockBlob> mp4AudioAssetFilesName = mp4AssetFiles.Where(f =>
                                                               (f.Name.ToLower().Contains("audio") && !f.Name.ToLower().Contains("video"))
                                                               ||
                                                               (f.Name.ToLower().Contains("aac") && !f.Name.ToLower().Contains("h264"))
                                                               );

                    IOrderedEnumerable<CloudBlockBlob> mp4AudioAssetFilesSize = mp4AssetFiles.OrderBy(f => f.Properties.Length);

                    string mp4fileaudio = (mp4AudioAssetFilesName.Count() == 1) ? mp4AudioAssetFilesName.FirstOrDefault().Name : mp4AudioAssetFilesSize.FirstOrDefault().Name; // if there is one file with audio or AAC in the name then let's use it for the audio track
                    switchxml.Add(new XElement(ns + "audio", new XAttribute("src", mp4fileaudio), new XAttribute("title", "audioname")));

                    if (mp4AudioAssetFilesName.Count() == 1 && mediaAssetFiles.Count() > 1) //looks like there is one audio file and dome other video files
                    {
                        mp4fileuniqueaudio = mp4fileaudio;
                    }
                }

                // video tracks
                foreach (CloudBlockBlob file in mp4AssetFiles)
                {
                    if (file.Name != mp4fileuniqueaudio) // we don't put the unique audio file as a video track
                    {
                        switchxml.Add(new XElement(ns + "video", new XAttribute("src", file.Name)));
                    }
                }

                // manifest filename
                string name = CommonPrefix(mediaAssetFiles.Select(f => Path.GetFileNameWithoutExtension(f.Name)).ToArray());
                if (string.IsNullOrEmpty(name))
                {
                    name = "manifest";
                }
                else if (name.EndsWith("_") && name.Length > 1) // i string ends with "_", let's remove it
                {
                    name = name.Substring(0, name.Length - 1);
                }
                name += ".ism";

                return new ManifestGenerated() { Content = doc.Declaration.ToString() + Environment.NewLine + doc.ToString(), FileName = name };
            }
            else
            {
                return new ManifestGenerated() { Content = null, FileName = string.Empty }; // no mp4 in asset
            }
        }

        public class ManifestGenerated
        {
            public string FileName;
            public string Content;
        }

        private static string CommonPrefix(string[] ss)
        {
            if (ss.Length == 0)
            {
                return string.Empty;
            }

            if (ss.Length == 1)
            {
                return ss[0];
            }

            int prefixLength = 0;

            foreach (char c in ss[0])
            {
                foreach (string s in ss)
                {
                    if (s.Length <= prefixLength || s[prefixLength] != c)
                    {
                        return ss[0].Substring(0, prefixLength);
                    }
                }
                prefixLength++;
            }

            return ss[0]; // all strings identical
        }

        public static string ReturnS(int number)
        {
            return number > 1 ? "s" : string.Empty;
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
            int browserVersion = 0;
            using (RegistryKey ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                object version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                    {
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                    }
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            if (browserVersion < 7)
            {
                throw new ApplicationException("Unsupported version of Microsoft Internet Explorer!");
            }

            uint mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. 

            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. 
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. 
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                    
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.
                    break;
            }

            return mode;
        }
    }

    public class Constants
    {
        public const string GitHubAMSEVersionPrimaryV3 = "https://amsexplorer.azureedge.net/release/versionv3.json";
        public const string GitHubAMSEVersionSecondaryV3 = "https://raw.githubusercontent.com/Azure/Azure-Media-Services-Explorer/master/versionv3.json";

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
        public const string PlayerDASHIFToLaunch = @"http://reference.dashif.org/dash.js/v3.0.0/samples/dash-if-reference-player/index.html?url={0}";

        public const string PlayerMP4AzurePage = @"https://ampdemo.azureedge.net/azuremediaplayer.html?player=html5&format=mp4&url={0}&mp4url={0}";
        public const string AdvancedTestPlayer = @"https://openidconnectweb.azurewebsites.net/AMTestPlayer?url={0}";

        public const string PlayerInfoHTML5Video = @"http://www.w3schools.com/html/html5_video.asp";
        public const string PlayerJWPlayerPartnership = @"https://www.jwplayer.com/";
        public const string PlayerTHEOplayerPartnership = @"https://www.theoplayer.com/partners/azure";

        public const string DemoCaptionMaker = @"https://testdrive-archive.azurewebsites.net/Graphics/CaptionMaker/Default.html";

        public const string LinkFeedbackAMS = "http://aka.ms/amsvoice";
        public const string LinkInfoMediaUnit = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-scale-media-processing-overview";

        public const string TemporaryWidevineLicenseServer = "https://thiswillbereplacedbytheAMSwidevineurl/?KID=00000000-0000-0000-0000-000000000000";

        public static readonly string[] BrowserEdge = { "Microsoft Edge", "microsoft-edge:" };
        public static readonly string[] BrowserIE = { "Internet Explorer", "iexplore.exe" };
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

        public const string LinkMoreInfoSE = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-streaming-endpoints-overview";
        public const string LinkMoreInfoAzCopy = "https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azcopy";

        public const string LinkMoreInfoVideoAnalyzer = "https://docs.microsoft.com/en-us/azure/media-services/latest/analyzing-video-audio-files-concept";
        public const string LinkMoreInfoMediaEncoderBuiltIn = "https://docs.microsoft.com/en-us/azure/media-services/latest/encoding-concept";

        public const string LinkHowIMoreInfoDynamicManifest = "https://docs.microsoft.com/en-us/azure/media-services/latest/filters-dynamic-manifest-overview";
        public const string LinkHowIMoreInfoSubclipping = "http://azure.microsoft.com/blog/2015/04/14/dynamic-manifests-and-rendered-sub-clips/";
        public const string LinkMoreInfoSubClipAMSE = "https://azure.microsoft.com/en-us/blog/sub-clipping-and-live-archive-extraction-with-media-encoder-standard/";
        public const string LinkMoreInfoLiveEncoding = "https://docs.microsoft.com/en-us/azure/media-services/latest/live-streaming-overview#live-encoding";
        public const string LinkMoreInfoLiveStreaming = "https://docs.microsoft.com/en-us/azure/media-services/latest/live-streaming-overview";
        public const string LinkMoreInfoPricing = "https://azure.microsoft.com/en-us/pricing/details/media-services/";
        public const string LinkMoreInfoStorageVersioning = "https://msdn.microsoft.com/en-us/library/azure/dd894041.aspx";
        public const string LinkMoreInfoStorageAnalytics = "https://msdn.microsoft.com/library/azure/hh343258.aspx";
        public const string LinkMoreInfoFairPlay = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-protect-hls-with-fairplay";
        public const string LinkMoreInfoTelemetry = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-telemetry-overview";

        public const string LinkPlayReadyTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/latest/playready-license-template-overview";
        public const string LinkPlayReadyCompliance = "http://www.microsoft.com/playready/licensing/compliance/";
        public const string LinkWidevineTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/latest/widevine-license-template-overview";

        public const string LinkAMSCreateAccount = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-portal-create-account";
        public const string LinkAMSAADAut = "https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-portal-get-started-with-aad";
        public const string LinkAMSAzCli = "https://docs.microsoft.com/en-us/azure/media-services/latest/access-api-cli-how-to";

        public const string LinkAMSE = "http://aka.ms/amse";
        public const string LinkMailtoAMSE = "mailto:amse@microsoft.com?subject=Azure Media Services Explorer - Question/Comment";
        public const string LinkReportBugAMSE = @"https://github.com/Azure/Azure-Media-Services-Explorer/issues";
        public const string LinkAMSEReleaseNotes = @"https://rawgit.com/Azure/Azure-Media-Services-Explorer/master/AllReleaseNotes.html";

        public const long maxSlateJPGFileSize = 3 * 1000 * 1000; // Max 3 MB
        public const int maxSlateJPGHorizontalResolution = 1920;
        public const int maxSlateJPGVerticalResolution = 1080;
        public const double SlateJPGAspectRatio = 16d / 9d;
        public const string SlateJPGExtension = ".jpg";

        public const string stringNull = "(null)"; // To display null is textbox

        public const int MaxTransfersAsUnlimited = 5;
        public const string strTransfers = "{0} concurrent transfer{1}";

        public const string LinkMoreInfoLiveTranscript = "https://docs.microsoft.com/en-us/azure/media-services/latest/live-transcription";
        public const string LinkMoreInfoLiveTranscriptRegions = "https://docs.microsoft.com/en-us/azure/media-services/latest/azure-clouds-regions#feature-availability-in-preview";
    }


    public class AssetBitmapAndText
    {
        public Bitmap bitmap;
        public string MouseOverDesc;
        public IList<AssetStreamingLocator> Locators;
    }


    public class AssetInfo
    {
        private readonly List<Asset> SelectedAssetsV3;
        private readonly AMSClientV3 _amsClient;
        public const string Type_Empty = "(empty)";
        public const string Type_Workflow = "Workflow";
        public const string Type_Single = "Single Bitrate MP4";
        public const string Type_Multi = "Multi Bitrate MP4";
        public const string Type_Smooth = "Smooth Streaming";
        public const string Type_LiveArchive = "Live Archive";
        public const string Type_Fragmented = "Pre-fragmented";
        public const string Type_AMSHLS = "Media Services HLS";
        public const string Type_Thumbnails = "Thumbnails";
        public const string Type_Unknown = "Unknown";
        public const string _prog_down_https_SAS = "Progressive Download URLs (SAS)";
        public const string _prog_down_http_streaming = "Progressive Download URLs (SE)";
        public const string _hls_cmaf = "HLS CMAF URL";
        public const string _hls_v4 = "HLS v4 URL";
        public const string _hls_v3 = "HLS v3 URL";
        public const string _dash_csf = "MPEG-DASH CSF URL";
        public const string _dash_cmaf = "MPEG-DASH CMAF URL";
        public const string _smooth = "Smooth Streaming URL";
        public const string _smooth_legacy = "Smooth Streaming (legacy) URL";
        public const string _hls = "HLS URL";

        public const string format_smooth_legacy = "fmp4-v20";
        public const string format_hls_v4 = "m3u8-aapl";
        public const string format_hls_v3 = "m3u8-aapl-v3";
        public const string format_hls_cmaf = "m3u8-cmaf";
        public const string format_dash_csf = "mpd-time-csf";
        public const string format_dash_cmaf = "mpd-time-cmaf";

        private const string format_url = "format={0}";
        private const string filter_url = "filter={0}";
        private const string audioTrack_url = "audioTrack={0}";

        private const string ManifestFileExtension = ".ism";


        public AssetInfo(Asset myAsset, AMSClientV3 amsClient)
        {
            SelectedAssetsV3 = new List<Asset>() { myAsset };
            _amsClient = amsClient;
        }

        public AssetInfo(List<Asset> mySelectedAssets, AMSClientV3 amsClient)
        {
            SelectedAssetsV3 = mySelectedAssets;
            _amsClient = amsClient;
        }
        public AssetInfo(Asset asset)
        {
            SelectedAssetsV3 = new List<Asset>() { asset };
        }


        public static async Task<StreamingLocator> CreateTemporaryOnDemandLocatorAsync(Asset asset, AMSClientV3 _amsClientV3)
        {
            StreamingLocator tempLocator = null;
            await _amsClientV3.RefreshTokenIfNeededAsync();

            try
            {
                string streamingLocatorName = "templocator-" + Program.GetUniqueness();

                tempLocator = new StreamingLocator(
                    assetName: asset.Name,
                    streamingPolicyName: PredefinedStreamingPolicy.ClearStreamingOnly,
                    streamingLocatorId: null,
                    endTime: DateTime.UtcNow.AddHours(1)
                    );


                tempLocator = await _amsClientV3.AMSclient.StreamingLocators.CreateAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, streamingLocatorName, tempLocator);
            }

            catch
            {
                throw;
            }

            return tempLocator;
        }

        public static async Task DeleteStreamingLocatorAsync(AMSClientV3 _amsClientV3, string streamingLocatorName)
        {
            await _amsClientV3.RefreshTokenIfNeededAsync();

            try
            {
                await _amsClientV3.AMSclient.StreamingLocators.DeleteAsync(_amsClientV3.credentialsEntry.ResourceGroup, _amsClientV3.credentialsEntry.AccountName, streamingLocatorName);
            }
            catch
            {
                throw;
            }
        }

        public async static Task<Uri> GetValidOnDemandURIAsync(Asset asset, AMSClientV3 _amsClient, string useThisLocatorName = null)
        {
            await _amsClient.RefreshTokenIfNeededAsync();

            IList<AssetStreamingLocator> locators = (await _amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name)).StreamingLocators;

            var ses = await _amsClient.AMSclient.StreamingEndpoints.ListAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName);

            StreamingEndpoint runningSes = ses.Where(s => s.ResourceState == StreamingEndpointResourceState.Running).FirstOrDefault();
            if (runningSes == null)
            {
                runningSes = ses.FirstOrDefault();
            }

            if (locators.Count > 0 && runningSes != null)
            {
                string locatorName = useThisLocatorName ?? locators.First().Name;
                IList<StreamingPath> streamingPaths = (await _amsClient.AMSclient.StreamingLocators.ListPathsAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, locatorName)).StreamingPaths;
                UriBuilder uribuilder = new UriBuilder
                {
                    Host = runningSes.HostName,
                    Path = streamingPaths.Where(p => p.StreamingProtocol == StreamingPolicyStreamingProtocol.SmoothStreaming).FirstOrDefault().Paths.FirstOrDefault()
                };
                return uribuilder.Uri;
            }
            else
            {
                return null;
            }
        }

        public static async Task<AssetStreamingLocator> IsThereALocatorValidAsync(Asset asset, AMSClientV3 amsClient)
        {
            if (asset == null) return null;

            await amsClient.RefreshTokenIfNeededAsync();
            IList<AssetStreamingLocator> locators = (await amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, asset.Name))
                                                    .StreamingLocators;

            if (locators.Count > 0)
            {
                AssetStreamingLocator LocatorQuery = locators.Where(l => ((l.StartTime < DateTime.UtcNow) || (l.StartTime == null)) && (l.EndTime > DateTime.UtcNow)).FirstOrDefault();
                if (LocatorQuery != null)
                {
                    return LocatorQuery;
                }
            }
            return null;
        }

        public static string GetSmoothLegacy(string smooth_uri)
        {
            return string.Format("{0}(format={1})", smooth_uri, format_smooth_legacy);
        }
        public static string AddFilterToUrlString(string urlstr, string filter)
        {
            // add a filter
            if (filter != null)
            {
                return AddParameterToUrlString(urlstr, string.Format(AssetInfo.filter_url, filter));
            }
            else
            {
                return urlstr;
            }
        }

        public static string AddAudioTrackToUrlString(string urlstr, string trackname)
        {
            // add a track name
            if (trackname != null)
            {
                return AddParameterToUrlString(urlstr, string.Format(AssetInfo.audioTrack_url, trackname));
            }
            else
            {
                return urlstr;
            }
        }

        public static string AddHLSNoAudioOnlyModeToUrlString(string urlstr)
        {
            return AddParameterToUrlString(urlstr, "audio-only=false");
        }

        public static string AddProtocolFormatInUrlString(string urlstr, AMSOutputProtocols protocol = AMSOutputProtocols.NotSpecified)
        {
            switch (protocol)
            {
                case AMSOutputProtocols.DashCsf:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_dash_csf)) + Constants.mpd;

                case AMSOutputProtocols.DashCmaf:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_dash_cmaf)) + Constants.mpd;

                case AMSOutputProtocols.HLSv3:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_hls_v3)) + Constants.m3u8;

                case AMSOutputProtocols.HLSv4:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_hls_v4)) + Constants.m3u8;

                case AMSOutputProtocols.HLSCmaf:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_hls_cmaf)) + Constants.m3u8;

                case AMSOutputProtocols.SmoothLegacy:
                    return AddParameterToUrlString(urlstr, string.Format(AssetInfo.format_url, AssetInfo.format_smooth_legacy));

                case AMSOutputProtocols.NotSpecified:
                default:
                    return urlstr;
            }
        }

        public static string AddParameterToUrlString(string urlstr, string parameter)
        {
            // add a parameter (like "format=mpd-time-csf" or "filter=myfilter" or "audioTrack=name to urlstr

            const string querystr = "/manifest(";

            // let's remove temporary the extension
            string streamExtension = string.Empty;
            if (urlstr.EndsWith(Constants.mpd))
            {
                streamExtension = Constants.mpd;
                urlstr = urlstr.Substring(0, urlstr.Length - Constants.mpd.Length);
            }
            else if (urlstr.EndsWith(Constants.m3u8))
            {
                streamExtension = Constants.m3u8;
                urlstr = urlstr.Substring(0, urlstr.Length - Constants.m3u8.Length);
            }

            if (urlstr.Contains(querystr)) // there is already a parameter
            {
                int pos = urlstr.IndexOf(querystr, 0);
                urlstr = urlstr.Substring(0, pos + 10) + parameter + "," + urlstr.Substring(pos + 10);
            }
            else
            {
                urlstr += string.Format("({0})", parameter);
            }

            return urlstr + streamExtension; // we restore the extension
        }

        public static Uri RW(string path, StreamingEndpoint se, string filters = null, bool https = false, string customHostName = null, AMSOutputProtocols protocol = AMSOutputProtocols.NotSpecified, string audiotrackname = null, bool HLSNoAudioOnly = false)
        {
            return RW(new Uri("https://" + se.HostName + path), se, filters, https, customHostName, protocol, audiotrackname, HLSNoAudioOnly);
        }

        // return the URL with hostname from streaming endpoint
        public static Uri RW(Uri url, StreamingEndpoint se = null, string filters = null, bool https = false, string customHostName = null, AMSOutputProtocols protocol = AMSOutputProtocols.NotSpecified, string audiotrackname = null, bool HLSNoAudioOnly = false)
        {
            if (url != null)
            {
                string path = AddFilterToUrlString(url.AbsolutePath, filters);
                path = AddProtocolFormatInUrlString(path, protocol);

                if (protocol == AMSOutputProtocols.HLSv3)
                {
                    path = AddAudioTrackToUrlString(path, audiotrackname);
                    if (HLSNoAudioOnly)
                    {
                        path = AddHLSNoAudioOnlyModeToUrlString(path);
                    }
                }

                string hostname = null;
                if (customHostName != null)
                {
                    hostname = customHostName;
                }
                else if (se != null)
                {
                    hostname = se.HostName;
                }

                UriBuilder urib = new UriBuilder()
                {
                    Host = hostname ?? url.Host,
                    Scheme = https ? "https://" : "http://",
                    Path = path,
                };
                return urib.Uri;
            }
            else
            {
                return null;
            }
        }


        public static string RW(string path, StreamingEndpoint se, string filter = null, bool https = false, string customhostname = null, AMSOutputProtocols protocol = AMSOutputProtocols.NotSpecified, string audiotrackname = null)
        {
            return RW(new Uri(path), se, filter, https, customhostname, protocol, audiotrackname).AbsoluteUri;
        }



        public static PublishStatus GetPublishedStatusForLocator(AssetStreamingLocator Locator)
        {
            PublishStatus LocPubStatus;
            if (!(Locator.EndTime < DateTime.UtcNow))
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

        public static PublishStatus GetPublishedStatusForLocator(StreamingLocator Locator)
        {
            PublishStatus LocPubStatus;
            if (!(Locator.EndTime < DateTime.UtcNow))
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

        public static TimeSpan ReturnTimeSpanOnGOP(ManifestTimingData data, TimeSpan ts)
        {
            TimeSpan response = ts;
            ulong timestamp = (ulong)(ts.TotalSeconds * data.TimeScale);

            int i = 0;
            foreach (ulong t in data.TimestampList)
            {
                if (t < timestamp && i < (data.TimestampList.Count - 1) && timestamp < data.TimestampList[i + 1])
                {
                    response = TimeSpan.FromSeconds(t / (double)data.TimeScale);
                    break;
                }
                i++;
            }
            return response;
        }


        public async static Task<XDocument> TryToGetClientManifestContentAsABlobAsync(Asset asset, AMSClientV3 _amsClient)
        {
            // get the manifest
            ListContainerSasInput input = new ListContainerSasInput()
            {
                Permissions = AssetContainerPermission.Read,
                ExpiryTime = DateTime.Now.AddMinutes(5).ToUniversalTime()
            };
            await _amsClient.RefreshTokenIfNeededAsync();

            AssetContainerSas responseSas = await _amsClient.AMSclient.Assets.ListContainerSasAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, asset.Name, input.Permissions, input.ExpiryTime);

            string uploadSasUrl = responseSas.AssetContainerSasUrls.First();

            Uri sasUri = new Uri(uploadSasUrl);
            var container = new CloudBlobContainer(sasUri);


            BlobContinuationToken continuationToken = null;
            var blobs = new List<CloudBlockBlob>();

            do
            {
                BlobResultSegment segment = await container.ListBlobsSegmentedAsync(null, false, BlobListingDetails.Metadata, null, continuationToken, null, null);
                blobs.AddRange(segment.Results.Where(blob => blob.GetType() == typeof(CloudBlockBlob)).Select(b => b as CloudBlockBlob));

                continuationToken = segment.ContinuationToken;
            }
            while (continuationToken != null);
            var ismc = blobs.Where(b => b.Name.EndsWith(".ismc", StringComparison.OrdinalIgnoreCase));

            if (ismc.Count() == 0)
            {
                throw new Exception("No ISMC file in asset.");
            }

            var content = await ismc.First().DownloadTextAsync();

            return XDocument.Parse(content);
        }

        public async static Task<XDocument> TryToGetClientManifestContentUsingStreamingLocatorAsync(Asset asset, AMSClientV3 _amsClient, string preferredLocatorName = null)
        {
            Uri myuri = await GetValidOnDemandURIAsync(asset, _amsClient, preferredLocatorName);

            if (myuri == null)
            {
                myuri = await GetValidOnDemandURIAsync(asset, _amsClient);
            }
            if (myuri != null)
            {
                return XDocument.Load(myuri.ToString());
            }
            else
            {
                throw new Exception("Streaming locator is null");
            }
        }

        /// <summary>
        /// Parse the manifest data.
        /// It is recommended to call TryToGetClientManifestContentAsABlobAsync and TryToGetClientManifestContentUsingStreamingLocatorAsync to get the content
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns></returns>
        public static ManifestTimingData GetManifestTimingData(XDocument manifest)
        {

            if (manifest == null) throw new ArgumentNullException();

            ManifestTimingData response = new ManifestTimingData() { IsLive = false, Error = false, TimestampOffset = 0, TimestampList = new List<ulong>(), DiscontinuityDetected = false };

            try
            {
                XElement smoothmedia = manifest.Element("SmoothStreamingMedia");
                IEnumerable<XElement> videotrack = smoothmedia.Elements("StreamIndex").Where(a => a.Attribute("Type").Value == "video");

                // TIMESCALE
                string timescaleFromManifest = smoothmedia.Attribute("TimeScale").Value;
                if (videotrack.FirstOrDefault().Attribute("TimeScale") != null) // there is timescale value in the video track. Let's take this one.
                {
                    timescaleFromManifest = videotrack.FirstOrDefault().Attribute("TimeScale").Value;
                }
                long timescale = long.Parse(timescaleFromManifest);
                response.TimeScale = timescale;

                // DURATION
                string durationFromManifest = smoothmedia.Attribute("Duration").Value;
                ulong? overallDuration = null;
                if (durationFromManifest != null) // there is a duration value. Let's take this one.
                {
                    overallDuration = (ulong?)ulong.Parse(durationFromManifest);
                }

                // Timestamp offset
                if (videotrack.FirstOrDefault().Element("c").Attribute("t") != null)
                {
                    response.TimestampOffset = ulong.Parse(videotrack.FirstOrDefault().Element("c").Attribute("t").Value);
                }
                else
                {
                    response.TimestampOffset = 0; // no timestamp, so it should be 0
                }

                ulong totalDuration = 0;
                ulong durationPreviousChunk = 0;
                ulong durationChunk;
                int repeatChunk;
                foreach (XElement chunk in videotrack.Elements("c"))
                {
                    durationChunk = chunk.Attribute("d") != null ? ulong.Parse(chunk.Attribute("d").Value) : 0;
                    repeatChunk = chunk.Attribute("r") != null ? int.Parse(chunk.Attribute("r").Value) : 1;

                    if (chunk.Attribute("t") != null)
                    {
                        ulong tvalue = ulong.Parse(chunk.Attribute("t").Value);
                        response.TimestampList.Add(tvalue);
                        if (tvalue != response.TimestampOffset)
                        {
                            totalDuration = tvalue - response.TimestampOffset; // Discountinuity ? We calculate the duration from the offset
                            response.DiscontinuityDetected = true; // let's flag it
                        }
                    }
                    else
                    {
                        response.TimestampList.Add(response.TimestampList[response.TimestampList.Count() - 1] + durationPreviousChunk);
                    }

                    totalDuration += durationChunk * (ulong)repeatChunk;

                    for (int i = 1; i < repeatChunk; i++)
                    {
                        response.TimestampList.Add(response.TimestampList[response.TimestampList.Count() - 1] + durationChunk);
                    }

                    durationPreviousChunk = durationChunk;
                }
                response.TimestampEndLastChunk = response.TimestampList[response.TimestampList.Count() - 1] + durationPreviousChunk;

                if (smoothmedia.Attribute("IsLive") != null && smoothmedia.Attribute("IsLive").Value == "TRUE")
                { // Live asset.... No duration to read or it is always zero (but we can read scaling and compute duration)
                    response.IsLive = true;
                    response.AssetDuration = TimeSpan.FromSeconds(totalDuration / ((double)timescale));
                }
                else
                {
                    if (overallDuration != null & overallDuration > 0) // let's trust the duration property in the manifest
                    {
                        response.AssetDuration = TimeSpan.FromSeconds((ulong)overallDuration / ((double)timescale));

                    }
                    else // no trust
                    {
                        response.AssetDuration = TimeSpan.FromSeconds(totalDuration / ((double)timescale));
                    }
                }
            }
            catch
            {
                response.Error = true;
            }
            return response;
        }


        public class ManifestSegmentData
        {
            public ulong timestamp;
            public bool calculated; // it means the timesatmp has been calculated from previous
            public bool timestamp_mismatch; // if there is a mismatch
        }

        public class ManifestSegmentsResponse
        {
            public List<ManifestSegmentData> videoSegments;
            public List<int> videoBitrates;
            public string videoName;
            public ManifestSegmentData[][] audioSegments;
            public int[][] audioBitrates;
            public string[] audioName;

            public ManifestSegmentsResponse()
            {
                videoSegments = new List<ManifestSegmentData>();
                videoBitrates = new List<int>();
            }
        }

        /*
        static public ManifestSegmentsResponse GetManifestSegmentsList(IAsset asset)
        // Parse the manifest and get data from it
        {
            ManifestSegmentsResponse response = new ManifestSegmentsResponse();

            try
            {
                ILocator mytemplocator = null;
                Uri myuri = AssetInfo.GetValidOnDemandURI(asset);
                if (myuri == null)
                {
                    mytemplocator = AssetInfo.CreatedTemporaryOnDemandLocator(asset);
                    myuri = AssetInfo.GetValidOnDemandURI(asset);
                }
                if (myuri != null)
                {
                    XDocument manifest = XDocument.Load(myuri.ToString());
                    var smoothmedia = manifest.Element("SmoothStreamingMedia");

                    ulong d = 0, r;
                    bool calc = true;
                    bool mismatch = false;
                    bool firstchunk = true;
                    ulong timeStamp = 0;

                    // video track
                    var videotrack = smoothmedia.Elements("StreamIndex").Where(a => a.Attribute("Type").Value == "video").FirstOrDefault();
                    response.videoBitrates = videotrack.Elements("QualityLevel").Select(e => int.Parse(e.Attribute("Bitrate").Value)).ToList();
                    response.videoName = videotrack.Attribute("Name").Value;

                    foreach (var chunk in videotrack.Elements("c"))
                    {
                        mismatch = false;
                        if (chunk.Attribute("t") != null)
                        {
                            var readtimeStamp = ulong.Parse(chunk.Attribute("t").Value);
                            mismatch = (!firstchunk && readtimeStamp != timeStamp);
                            timeStamp = readtimeStamp;
                            calc = false;
                            firstchunk = false;
                        }
                        else
                        {
                            calc = true;
                        }

                        d = chunk.Attribute("d") != null ? ulong.Parse(chunk.Attribute("d").Value) : 0;
                        r = chunk.Attribute("r") != null ? ulong.Parse(chunk.Attribute("r").Value) : 1;
                        for (ulong i = 0; i < r; i++)
                        {
                            response.videoSegments.Add(new ManifestSegmentData()
                            {
                                timestamp = timeStamp,
                                timestamp_mismatch = (i == 0) ? mismatch : false,
                                calculated = (i == 0) ? calc : true
                            });
                            timeStamp += d;
                        }
                    }

                    // audio track
                    var audiotracks = smoothmedia.Elements("StreamIndex").Where(a => a.Attribute("Type").Value == "audio");
                    response.audioBitrates = new int[audiotracks.Count()][];
                    response.audioSegments = new ManifestSegmentData[audiotracks.Count()][];
                    response.audioName = new string[audiotracks.Count()];


                    int a_index = 0;
                    foreach (var audiotrack in audiotracks)
                    {
                        response.audioBitrates[a_index] = audiotrack.Elements("QualityLevel").Select(e => int.Parse(e.Attribute("Bitrate").Value)).ToArray();
                        response.audioName[a_index] = audiotrack.Attribute("Name").Value;

                        var audiotracksegmentdata = new List<ManifestSegmentData>();

                        timeStamp = 0;
                        d = 0;
                        firstchunk = true;
                        foreach (var chunk in audiotrack.Elements("c"))
                        {
                            mismatch = false;
                            if (chunk.Attribute("t") != null)
                            {
                                var readtimeStamp = ulong.Parse(chunk.Attribute("t").Value);
                                mismatch = (!firstchunk && readtimeStamp != timeStamp);
                                timeStamp = readtimeStamp;
                                calc = false;
                                firstchunk = false;
                            }
                            else
                            {
                                calc = true;
                            }

                            d = chunk.Attribute("d") != null ? ulong.Parse(chunk.Attribute("d").Value) : 0;
                            r = chunk.Attribute("r") != null ? ulong.Parse(chunk.Attribute("r").Value) : 1;
                            for (ulong i = 0; i < r; i++)
                            {
                                audiotracksegmentdata.Add(new ManifestSegmentData()
                                {
                                    timestamp = timeStamp,
                                    timestamp_mismatch = (i == 0) ? mismatch : false,
                                    calculated = (i == 0) ? calc : true
                                });
                                timeStamp += d;
                            }

                        }
                        response.audioSegments[a_index] = audiotracksegmentdata.ToArray();
                        a_index++;
                    }
                }
                else
                {
                    // Error
                }
                if (mytemplocator != null) mytemplocator.Delete();
            }
            catch
            {
                // Error
            }
            return response;
        }
        */

        public static long ReturnTimestampInTicks(ulong timestamp, ulong? timescale)
        {
            double timescale2 = timescale ?? TimeSpan.TicksPerSecond;
            return (long)(timestamp * (double)TimeSpan.TicksPerSecond / timescale2);
        }


        public static async Task<AssetInfoData> GetAssetTypeAsync(string assetName, AMSClientV3 _amsClient)
        {
            ListContainerSasInput input = new ListContainerSasInput()
            {
                Permissions = AssetContainerPermission.ReadWriteDelete,
                ExpiryTime = DateTime.Now.AddHours(2).ToUniversalTime()
            };
            await _amsClient.RefreshTokenIfNeededAsync();

            string type = string.Empty;
            long size = 0;

            AssetContainerSas response = null;
            try
            {
                response = await _amsClient.AMSclient.Assets.ListContainerSasAsync(_amsClient.credentialsEntry.ResourceGroup, _amsClient.credentialsEntry.AccountName, assetName, input.Permissions, input.ExpiryTime);
            }
            catch
            {
                return null;
            }

            string uploadSasUrl = response.AssetContainerSasUrls.First();

            Uri sasUri = new Uri(uploadSasUrl);
            CloudBlobContainer container = new CloudBlobContainer(sasUri);

            BlobContinuationToken continuationToken1 = null;
            List<IListBlobItem> rootBlobs = new List<IListBlobItem>();
            do
            {
                var segment = await container.ListBlobsSegmentedAsync(null, false, BlobListingDetails.Metadata, null, continuationToken1, null, null);
                continuationToken1 = segment.ContinuationToken;
                rootBlobs = segment.Results.ToList();
            }
            while (continuationToken1 != null);

            List<CloudBlockBlob> blocsc = rootBlobs.Where(b => b.GetType() == typeof(CloudBlockBlob)).Select(b => (CloudBlockBlob)b).ToList();
            List<CloudBlobDirectory> blocsdir = rootBlobs.Where(b => b.GetType() == typeof(CloudBlobDirectory)).Select(b => (CloudBlobDirectory)b).ToList();

            int number = blocsc.Count;

            CloudBlockBlob[] ismfiles = blocsc.Where(f => f.Name.EndsWith(".ism", StringComparison.OrdinalIgnoreCase)).ToArray();
            CloudBlockBlob[] ismcfiles = blocsc.Where(f => f.Name.EndsWith(".ismc", StringComparison.OrdinalIgnoreCase)).ToArray();

            // size calculation
            blocsc.ForEach(b => size += b.Properties.Length);

            // fragments in subfolders (live archive)
            foreach (CloudBlobDirectory dir in blocsdir)
            {
                CloudBlobDirectory dirRef = container.GetDirectoryReference(dir.Prefix);

                BlobContinuationToken continuationToken = null;
                List<CloudBlockBlob> subBlobs = new List<CloudBlockBlob>();
                do
                {
                    var segment = await dirRef.ListBlobsSegmentedAsync(true, BlobListingDetails.Metadata, null, continuationToken, null, null);
                    continuationToken = segment.ContinuationToken;
                    subBlobs = segment.Results.Where(b => b.GetType() == typeof(CloudBlockBlob)).Select(b => (CloudBlockBlob)b).ToList();
                    subBlobs.ForEach(b => size += b.Properties.Length);
                }
                while (continuationToken != null);
            }

            CloudBlockBlob[] mp4files = blocsc.Where(f => f.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)).ToArray();

            if (mp4files.Count() > 0 && ismcfiles.Count() == 1 && ismfiles.Count() == 1)  // Multi bitrate MP4
            {
                number = mp4files.Count();
                type = number == 1 ? Type_Single : Type_Multi;
            }

            else if (blocsc.Count == 0)
            {
                return new AssetInfoData() { Size = size, Type = Type_Empty };
            }
            else if (ismcfiles.Count() == 1 && ismfiles.Count() == 1 && blocsdir.Count > 0)
            {
                type = Type_LiveArchive;
                number = blocsdir.Count;
            }

            else if (blocsc.Count == 1)
            {
                number = 1;
                string ext = Path.GetExtension(blocsc.FirstOrDefault().Name.ToUpper());
                if (!string.IsNullOrEmpty(ext))
                {
                    ext = ext.Substring(1);
                }

                switch (ext)
                {
                    case "WORKFLOW":
                        type = Type_Workflow;
                        break;

                    default:
                        type = ext;
                        break;
                }
            }

            else
            {
                type = Type_Unknown;
            }

            return new AssetInfoData()
            {
                Size = size,
                Type = string.Format("{0} ({1})", type, number),
                Blobs = rootBlobs
            };
        }


        public async Task CopyStatsToClipBoardAsync()
        {
            StringBuilder SB = await GetStatsAsync();
            Clipboard.SetText(SB.ToString());
        }


        public static string FormatByteSize(long? byteCountl)
        {
            if (byteCountl.HasValue == true)
            {
                long byteCount = (long)byteCountl;
                string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
                if (byteCount == 0)
                {
                    return "0 " + suf[0];
                }

                long bytes = Math.Abs(byteCount);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1000)));
                double num = Math.Round(bytes / Math.Pow(1000, place), 1);
                return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
            }
            else
            {
                return null;
            }
        }

        public static long? Inverse_FormatByteSize(string mystring)
        {
            List<UnitSize> sizes = new List<UnitSize> {
                  new UnitSize() { Unitn = "B", Mult = 1 },
                  new UnitSize(){ Unitn = "KB", Mult = 1000 },
                  new UnitSize(){ Unitn = "MB", Mult = (long)1000*1000 },
                  new UnitSize(){ Unitn = "GB", Mult = (long)1000*1000*1000 },
                  new UnitSize(){ Unitn = "TB", Mult = (long)1000*1000*1000*1000 },
                  new UnitSize(){ Unitn = "PB", Mult = (long)1000*1000*1000*1000*1000 },
                  new UnitSize(){ Unitn = "EB", Mult = (long)1000*1000*1000*1000*1000*1000 }
                  };

            if (sizes.Any(s => mystring.EndsWith(" " + s.Unitn)))
            {
                string val = mystring.Substring(0, mystring.Length - 2).Trim();
                try
                {
                    double valdouble = double.Parse(val);
                    string myunit = mystring.Substring(mystring.Length - 2, 2).Trim();
                    long mymult = sizes.Where(s => s.Unitn == myunit).FirstOrDefault().Mult;
                    return (long)(valdouble * mymult);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public class UnitSize
        {
            public string Unitn { get; set; }
            public long Mult { get; set; }
        }


        public static AssetProtectionType GetAssetProtection(AssetStreamingLocator locator)
        {
            AssetProtectionType type = AssetProtectionType.None;

            if (locator != null)
            {
                if (locator.StreamingPolicyName == PredefinedStreamingPolicy.ClearKey.ToString())
                {
                    type = AssetProtectionType.AES;
                }
                else if (locator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmCencStreaming.ToString())
                {
                    type = AssetProtectionType.PlayReadyAndWidevine;
                }
                else if (locator.StreamingPolicyName == PredefinedStreamingPolicy.MultiDrmStreaming.ToString())
                {
                    type = AssetProtectionType.PlayReadyAndWidevineAndFairplay;
                }
            }

            return type;
        }


        public async Task<StringBuilder> GetStatsAsync()
        {
            StringBuilder sb = new StringBuilder();

            if (SelectedAssetsV3.Count > 0)
            {
                // Asset Stats
                foreach (Asset theAsset in SelectedAssetsV3)
                {
                    sb.Append(await GetStatAsync(theAsset, _amsClient));
                }
            }
            return sb;
        }
        /*
        public static StringBuilder GetStat(IAsset MyAsset, StreamingEndpoint SelectedSE = null)
        {
            StringBuilder sb = new StringBuilder();
            string MyAssetType = AssetInfo.GetAssetType(MyAsset);
            bool bfileinasset = (MyAsset.AssetFiles.Count() == 0) ? false : true;
            long size = -1;
            if (bfileinasset)
            {
                size = 0;
                foreach (IAssetFile file in MyAsset.AssetFiles)
                {
                    size += file.ContentFileSize;
                }
            }
            sb.AppendLine("Asset Name          : " + MyAsset.Name);
            sb.AppendLine("Asset Type          : " + MyAsset.AssetType);
            sb.AppendLine("Asset Id            : " + MyAsset.Id);
            sb.AppendLine("Alternate ID        : " + MyAsset.AlternateId);
            if (size != -1)
                sb.AppendLine("Size                : " + FormatByteSize(size));
            sb.AppendLine("State               : " + MyAsset.State);
            sb.AppendLine("Created (UTC)       : " + MyAsset.Created.ToLongDateString() + " " + MyAsset.Created.ToLongTimeString());
            sb.AppendLine("Last Modified (UTC) : " + MyAsset.LastModified.ToLongDateString() + " " + MyAsset.LastModified.ToLongTimeString());
            sb.AppendLine("Creations Options   : " + MyAsset.Options);

            if (MyAsset.State != AssetState.Deleted)
            {
                sb.AppendLine("IsStreamable        : " + MyAsset.IsStreamable);
                sb.AppendLine("SupportsDynEnc      : " + MyAsset.SupportsDynamicEncryption);
                sb.AppendLine("Uri                 : " + MyAsset.Uri.AbsoluteUri);
                sb.AppendLine("");
                sb.AppendLine("Storage Name        : " + MyAsset.StorageAccountName);
                sb.AppendLine("Storage Bytes used  : " + FormatByteSize(MyAsset.StorageAccount.BytesUsed));
                sb.AppendLine("Storage IsDefault   : " + MyAsset.StorageAccount.IsDefault);
                sb.AppendLine("");

                foreach (IAsset p_asset in MyAsset.ParentAssets)
                {
                    sb.AppendLine("Parent asset Name   : " + p_asset.Name);
                    sb.AppendLine("Parent asset Id     : " + p_asset.Id);
                }
                sb.AppendLine("");
                foreach (IContentKey key in MyAsset.ContentKeys)
                {
                    sb.AppendLine("Content key         : " + key.Name);
                    sb.AppendLine("Content key Id      : " + key.Id);
                    sb.AppendLine("Content key Type    : " + key.ContentKeyType);
                }
                sb.AppendLine("");
                foreach (var pol in MyAsset.DeliveryPolicies)
                {
                    sb.AppendLine("Deliv policy Name   : " + pol.Name);
                    sb.AppendLine("Deliv policy Id     : " + pol.Id);
                    sb.AppendLine("Deliv policy Type   : " + pol.AssetDeliveryPolicyType);
                    sb.AppendLine("Deliv pol Protocol  : " + pol.AssetDeliveryProtocol);
                }
                sb.AppendLine("");

                foreach (IAssetFile fileItem in MyAsset.AssetFiles)
                {
                    if (fileItem.IsPrimary)
                    {
                        sb.AppendLine("   ------------(-P-R-I-M-A-R-Y-)------------------");
                    }
                    else
                    {
                        sb.AppendLine("   -----------------------------------------------");
                    }
                    sb.AppendLine("   Name                 : " + fileItem.Name);
                    sb.AppendLine("   Id                   : " + fileItem.Id);
                    sb.AppendLine("   File size            : " + fileItem.ContentFileSize + " Bytes");
                    sb.AppendLine("   Mime type            : " + fileItem.MimeType);
                    sb.AppendLine("   Init vector          : " + fileItem.InitializationVector);
                    sb.AppendLine("   Created (UTC)        : " + fileItem.Created.ToString("G"));
                    sb.AppendLine("   Last modified (UTC)  : " + fileItem.LastModified.ToString("G"));
                    sb.AppendLine("   Encrypted            : " + fileItem.IsEncrypted);
                    sb.AppendLine("   EncryptionScheme     : " + fileItem.EncryptionScheme);
                    sb.AppendLine("   EncryptionVersion    : " + fileItem.EncryptionVersion);
                    sb.AppendLine("   Encryption key id    : " + fileItem.EncryptionKeyId);
                    sb.AppendLine("   InitializationVector : " + fileItem.InitializationVector);
                    sb.AppendLine("   ParentAssetId        : " + fileItem.ParentAssetId);
                    sb.AppendLine("");
                }
                sb.Append(GetDescriptionLocators(MyAsset, SelectedSE));
            }
            sb.AppendLine("");
            sb.AppendLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            sb.AppendLine("");

            return sb;
        }
        */
        public static async Task<StringBuilder> GetStatAsync(Asset MyAsset, AMSClientV3 _amsClient)
        {
            ListRepData infoStr = new ListRepData();

            AssetInfoData MyAssetTypeInfo = await AssetInfo.GetAssetTypeAsync(MyAsset.Name, _amsClient);
            if (MyAssetTypeInfo == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Error accessing asset type info");
                return sb;
            }

            bool bfileinasset = (MyAssetTypeInfo.Blobs.Count() == 0) ? false : true;
            long size = -1;
            if (bfileinasset)
            {
                size = 0;
                foreach (IListBlobItem blob in MyAssetTypeInfo.Blobs.Where(b => b.GetType() == typeof(CloudBlockBlob)))
                {
                    size += (blob as CloudBlockBlob).Properties.Length;
                }
            }

            infoStr.Add("Asset Name", MyAsset.Name);
            infoStr.Add("Asset Description", MyAsset.Description);

            infoStr.Add("Asset Type", MyAssetTypeInfo.Type);
            infoStr.Add("Id", MyAsset.Id);
            infoStr.Add("Asset Id", MyAsset.AssetId.ToString());
            infoStr.Add("Alternate ID", MyAsset.AlternateId);
            if (size != -1)
            {
                infoStr.Add("Size", FormatByteSize(size));
            }

            infoStr.Add("Container", MyAsset.Container);
            infoStr.Add("Created (UTC)", MyAsset.Created.ToLongDateString() + " " + MyAsset.Created.ToLongTimeString());
            infoStr.Add("Last Modified (UTC)", MyAsset.LastModified.ToLongDateString() + " " + MyAsset.LastModified.ToLongTimeString());
            infoStr.Add("Storage account", MyAsset.StorageAccountName);
            infoStr.Add("Storage Encryption", MyAsset.StorageEncryptionFormat);

            infoStr.Add(string.Empty);

            foreach (IListBlobItem blob in MyAssetTypeInfo.Blobs)
            {
                infoStr.Add("   -----------------------------------------------");

                if (blob.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blobc = blob as CloudBlockBlob;
                    infoStr.Add("   Block Blob Name", blobc.Name);
                    infoStr.Add("   Type", blobc.BlobType.ToString());
                    infoStr.Add("   Blob length", blobc.Properties.Length + " Bytes");
                    infoStr.Add("   Content type", blobc.Properties.ContentType);
                    infoStr.Add("   Created (UTC)", blobc.Properties.Created?.ToString("G"));
                    infoStr.Add("   Last modified (UTC)", blobc.Properties.LastModified?.ToString("G"));
                    infoStr.Add("   Server Encrypted", blobc.Properties.IsServerEncrypted.ToString());
                    infoStr.Add("   Content MD5", blobc.Properties.ContentMD5);
                    infoStr.Add(string.Empty);

                }
                else if (blob.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory blobd = blob as CloudBlobDirectory;
                    infoStr.Add("   Blob Directory Name", blobd.Prefix);
                    infoStr.Add("   Type", "BlobDirectory");
                    infoStr.Add("   Blob Director length", GetSizeBlobDirectory(blobd) + " Bytes");
                    infoStr.Add(string.Empty);
                }
            }
            infoStr.Add(await GetDescriptionLocatorsAsync(MyAsset, _amsClient));

            infoStr.Add(string.Empty);
            infoStr.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            infoStr.Add(string.Empty);

            return infoStr.ReturnStringBuilder();
        }

        public static long GetSizeBlobDirectory(CloudBlobDirectory blobd)
        {
            long sizeDir = 0;
            List<CloudBlockBlob> subBlobs = blobd.ListBlobs(blobListingDetails: BlobListingDetails.Metadata).Where(b => b.GetType() == typeof(CloudBlockBlob)).Select(b => (CloudBlockBlob)b).ToList();
            subBlobs.ForEach(b => sizeDir += b.Properties.Length);

            return sizeDir;
        }


        public static async Task<ListRepData> GetDescriptionLocatorsAsync(Asset MyAsset, AMSClientV3 amsClient)
        {
            await amsClient.RefreshTokenIfNeededAsync();

            IList<AssetStreamingLocator> locators = (await amsClient.AMSclient.Assets.ListStreamingLocatorsAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, MyAsset.Name))
                                                    .StreamingLocators;

            ListRepData infoStr = new ListRepData();

            if (locators.Count == 0)
            {
                infoStr.Add("No streaming locator created for this asset.", null);
            }

            foreach (AssetStreamingLocator locatorbase in locators)
            {
                StreamingLocator locator = await amsClient.AMSclient.StreamingLocators.GetAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, locatorbase.Name);


                infoStr.Add("Locator Name", locator.Name);
                infoStr.Add("Locator Id", locator.StreamingLocatorId.ToString());
                infoStr.Add("Start Time", locator.StartTime?.ToLongDateString());
                infoStr.Add("End Time", locator.EndTime?.ToLongDateString());
                infoStr.Add("Streaming Policy Name", locator.StreamingPolicyName);
                infoStr.Add("Default Content Key Policy Name", locator.DefaultContentKeyPolicyName);
                infoStr.Add("Associated filters", string.Join(", ", locator.Filters.ToArray()));

                IList<StreamingPath> streamingPaths = (await amsClient.AMSclient.StreamingLocators.ListPathsAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, locator.Name)).StreamingPaths;
                IList<string> downloadPaths = (await amsClient.AMSclient.StreamingLocators.ListPathsAsync(amsClient.credentialsEntry.ResourceGroup, amsClient.credentialsEntry.AccountName, locator.Name)).DownloadPaths;

                foreach (StreamingPath path in streamingPaths)
                {
                    foreach (string p in path.Paths)
                    {
                        infoStr.Add(path.StreamingProtocol.ToString() + " " + path.EncryptionScheme, p);
                    }
                }

                foreach (string path in downloadPaths)
                {
                    infoStr.Add("Download", path);
                }

                infoStr.Add("==============================================================================");
                infoStr.Add(string.Empty);

            }
            return infoStr;
        }


        public static async Task<string> DoPlayBackWithStreamingEndpointAsync(PlayerType typeplayer, string path, AMSClientV3 client, Mainform mainForm,
            Asset myasset = null, bool DoNotRewriteURL = false, string filter = null, AssetProtectionType keytype = AssetProtectionType.None,
            AzureMediaPlayerFormats formatamp = AzureMediaPlayerFormats.Auto,
            AzureMediaPlayerTechnologies technology = AzureMediaPlayerTechnologies.Auto, bool launchbrowser = true, bool UISelectSEFiltersAndProtocols = true, string selectedBrowser = "",
            AssetStreamingLocator locator = null, string subtitleLanguageCode = null)
        {
            string FullPlayBackLink = null;

            if (!string.IsNullOrEmpty(path))
            {
                StreamingEndpoint choosenSE = await AssetInfo.GetBestStreamingEndpointAsync(client);

                if (choosenSE == null)
                {
                    return null;
                }

                // Let's ask for SE if several SEs or Custom Host Names or Filters
                if (!DoNotRewriteURL)
                {
                    ChooseStreamingEndpoint form = new ChooseStreamingEndpoint(client, myasset, path, filter, typeplayer, true);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        path = AssetInfo.RW(form.UpdatedPath, form.SelectStreamingEndpoint, form.SelectedFilters, form.ReturnHttps, form.ReturnSelectCustomHostName, form.ReturnStreamingProtocol, form.ReturnHLSAudioTrackName, form.ReturnHLSNoAudioOnlyMode).ToString();
                        choosenSE = form.SelectStreamingEndpoint;
                        selectedBrowser = form.ReturnSelectedBrowser;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }

                if (myasset != null)
                {
                    keytype = AssetInfo.GetAssetProtection(locator); // let's save the protection scheme (use by azure player): AES, PlayReady, Widevine or PlayReadyAndWidevine V3 migration
                }
            }

            // let's launch the player
            switch (typeplayer)
            {
                case PlayerType.AzureMediaPlayer:
                case PlayerType.AzureMediaPlayerFrame:
                case PlayerType.AzureMediaPlayerClear:

                    string playerurl = string.Empty;

                    if (keytype != AssetProtectionType.None)
                    {
                        bool insertoken = false;// !string.IsNullOrEmpty(tokenresult.TokenString);

                        if (insertoken)  // token. Let's analyse the token to find the drm technology used
                        {/*
                                switch (tokenresult.ContentKeyDeliveryType)
                                {
                                    case ContentKeyDeliveryType.BaselineHttp:
                                        playerurl += string.Format(Constants.AMPAes, true.ToString());
                                        playerurl += string.Format(Constants.AMPAesToken, tokenresult.TokenString);
                                        break;

                                    case ContentKeyDeliveryType.PlayReadyLicense:
                                        playerurl += string.Format(Constants.AMPPlayReady, true.ToString());
                                        playerurl += string.Format(Constants.AMPPlayReadyToken, tokenresult.TokenString);
                                        break;

                                    case ContentKeyDeliveryType.Widevine:
                                        playerurl += string.Format(Constants.AMPWidevine, true.ToString());
                                        playerurl += string.Format(Constants.AMPWidevineToken, tokenresult.TokenString);
                                        break;
                                        
                                    default:
                                        break;
                                }
                            */
                        }
                        else // No token. Open mode. Let's look to the key to know the drm technology
                        {
                            switch (keytype)
                            {
                                case AssetProtectionType.AES:
                                    playerurl += string.Format(Constants.AMPAes, true.ToString());
                                    break;

                                case AssetProtectionType.PlayReady:
                                    playerurl += string.Format(Constants.AMPPlayReady, true.ToString());
                                    break;

                                case AssetProtectionType.Widevine:
                                    playerurl += string.Format(Constants.AMPWidevine, true.ToString());
                                    break;

                                case AssetProtectionType.PlayReadyAndWidevine:
                                case AssetProtectionType.PlayReadyAndWidevineAndFairplay:
                                    playerurl += string.Format(Constants.AMPPlayReady, true.ToString());
                                    playerurl += string.Format(Constants.AMPWidevine, true.ToString());
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    if (formatamp != AzureMediaPlayerFormats.Auto)
                    {
                        switch (formatamp)
                        {
                            case AzureMediaPlayerFormats.Dash:
                                playerurl += string.Format(Constants.AMPformatsyntax, "dash");
                                break;

                            case AzureMediaPlayerFormats.Smooth:
                                playerurl += string.Format(Constants.AMPformatsyntax, "smooth");
                                break;

                            case AzureMediaPlayerFormats.HLS:
                                playerurl += string.Format(Constants.AMPformatsyntax, "hls");
                                break;

                            case AzureMediaPlayerFormats.VideoMP4:
                                playerurl += string.Format(Constants.AMPformatsyntax, "video/mp4");
                                break;

                            default: // auto or other
                                break;
                        }
                        if (false)//tokenresult.TokenString != null)
                        {
                            //playerurl += string.Format(Constants.AMPtokensyntax, tokenresult);
                        }
                    }

                    if (technology != AzureMediaPlayerTechnologies.Auto)
                    {
                        switch (technology)
                        {
                            case AzureMediaPlayerTechnologies.Flash:
                                playerurl += string.Format(Constants.AMPtechsyntax, "flash");
                                break;

                            case AzureMediaPlayerTechnologies.JavaScript:
                                playerurl += string.Format(Constants.AMPtechsyntax, "js");
                                break;

                            case AzureMediaPlayerTechnologies.NativeHTML5:
                                playerurl += string.Format(Constants.AMPtechsyntax, "html5");
                                break;

                            case AzureMediaPlayerTechnologies.Silverlight:
                                playerurl += string.Format(Constants.AMPtechsyntax, "silverlight");
                                break;

                            default: // auto or other
                                break;
                        }
                    }

                    /* V3 migration
                    if (myasset != null) // wtt subtitles files
                    {
                        var subtitles = myasset.AssetFiles.ToList().Where(f => f.Name.ToLower().EndsWith(".vtt")).ToList();
                        if (subtitles.Count > 0)
                        {
                            var urlasset = new Uri(Urlstr);
                            string baseurlwith = urlasset.GetLeftPart(UriPartial.Authority) + urlasset.Segments[0] + urlasset.Segments[1];
                            var listsub = new List<string>();
                            foreach (var s in subtitles)
                            {
                                listsub.Add(Path.GetFileNameWithoutExtension(s.Name) + ",und," + HttpUtility.UrlEncode(baseurlwith + s.Name));
                            }
                            playerurl += string.Format(Constants.AMPSubtitles, string.Join(";", listsub));
                        }
                    }
                    */

                    string playerurlbase = string.Empty;
                    if (typeplayer == PlayerType.AzureMediaPlayer)
                    {
                        playerurlbase = Constants.PlayerAMPToLaunch;
                    }
                    else if (typeplayer == PlayerType.AzureMediaPlayerFrame)
                    {
                        playerurlbase = Constants.PlayerAMPIFrameToLaunch;
                    }
                    else if (typeplayer == PlayerType.AzureMediaPlayerClear)
                    {
                        playerurlbase = Constants.PlayerAMPToLaunch.Replace("https://", "http://");
                    }

                    if (subtitleLanguageCode != null) // let's add the subtitle syntax to AMP
                    {
                        try
                        {
                            var culture = CultureInfo.GetCultureInfo(subtitleLanguageCode);
                            string trackName = WebUtility.HtmlEncode(culture.DisplayName);
                            playerurl += $"&imsc1Captions={trackName},{subtitleLanguageCode}";
                        }
                        catch
                        {

                        }
                    }

                    FullPlayBackLink = string.Format(playerurlbase, HttpUtility.UrlEncode(path)) + playerurl;
                    break;

                case PlayerType.DASHIFRefPlayer:
                    if (!path.Contains(string.Format(AssetInfo.format_url, AssetInfo.format_dash_csf)) && !path.Contains(string.Format(AssetInfo.format_url, AssetInfo.format_dash_cmaf)))
                    {
                        path = AssetInfo.AddParameterToUrlString(path, string.Format(AssetInfo.format_url, AssetInfo.format_dash_csf));
                    }
                    FullPlayBackLink = string.Format(Constants.PlayerDASHIFToLaunch, path);
                    break;

                case PlayerType.MP4AzurePage:
                    FullPlayBackLink = string.Format(Constants.PlayerMP4AzurePage, HttpUtility.UrlEncode(path));
                    break;


                case PlayerType.AdvancedTestPlayer:
                    string playerurlAd = string.Empty;
                    if (subtitleLanguageCode != null) // let's add the subtitle syntax to AMP
                    {
                        try
                        {
                            var culture = CultureInfo.GetCultureInfo(subtitleLanguageCode);
                            string trackName = WebUtility.HtmlEncode(culture.DisplayName);
                            playerurlAd += $"&imsc1CaptionsSettings={trackName},{subtitleLanguageCode}";
                        }
                        catch
                        {

                        }
                    }
                    FullPlayBackLink = string.Format(Constants.AdvancedTestPlayer, HttpUtility.UrlEncode(path)) + playerurlAd;
                    break;

                case PlayerType.CustomPlayer:
                    string myurl = Properties.Settings.Default.CustomPlayerUrl;
                    FullPlayBackLink = myurl.Replace(Constants.NameconvManifestURL, HttpUtility.UrlEncode(path)).Replace(Constants.NameconvToken, string.Empty /*tokenresult.TokenString*/);
                    break;
            }

            if (FullPlayBackLink != null && launchbrowser)
            {
                try
                {
                    if (string.IsNullOrEmpty(selectedBrowser))
                    {
                        Process.Start(FullPlayBackLink);
                    }
                    else
                    {
                        if (selectedBrowser.Contains("edge"))
                        {
                            Process.Start(selectedBrowser + FullPlayBackLink);
                        }
                        else
                        {
                            Process.Start(selectedBrowser, FullPlayBackLink);
                        }
                    }
                }
                catch
                {
                    mainForm.TextBoxLogWriteLine("Error when launching the browser.", true);
                }
            }


            return FullPlayBackLink;
        }


        internal static async Task<StreamingEndpoint> GetBestStreamingEndpointAsync(AMSClientV3 client)
        {
            await client.RefreshTokenIfNeededAsync();
            IEnumerable<StreamingEndpoint> SEList = (await client.AMSclient.StreamingEndpoints.ListAsync(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName)).AsEnumerable();
            StreamingEndpoint SESelected = SEList.Where(se => se.ResourceState == StreamingEndpointResourceState.Running).OrderBy(se => se.CdnEnabled).OrderBy(se => se.ScaleUnits).LastOrDefault();
            if (SESelected == null)
            {
                SESelected = await client.AMSclient.StreamingEndpoints.GetAsync(client.credentialsEntry.ResourceGroup, client.credentialsEntry.AccountName, "default");
            }

            if (SESelected == null)
            {
                SESelected = SEList.FirstOrDefault();
            }

            return SESelected;
        }

        // copy a directory of the same container to another container
        public static List<Task> CopyBlobDirectory(CloudBlobDirectory srcDirectory, CloudBlobContainer destContainer, string sourceblobToken, CancellationToken token)
        {

            List<Task> mylistresults = new List<Task>();

            List<IListBlobItem> srcBlobList = srcDirectory.ListBlobs(
                useFlatBlobListing: true,
                blobListingDetails: BlobListingDetails.None).ToList();

            foreach (IListBlobItem src in srcBlobList)
            {
                ICloudBlob srcBlob = src as ICloudBlob;

                // Create appropriate destination blob type to match the source blob
                CloudBlob destBlob;
                if (srcBlob.Properties.BlobType == BlobType.BlockBlob)
                {
                    destBlob = destContainer.GetBlockBlobReference(srcBlob.Name);
                }
                else
                {
                    destBlob = destContainer.GetPageBlobReference(srcBlob.Name);
                }

                // copy using src blob as SAS
                mylistresults.Add(destBlob.StartCopyAsync(new Uri(srcBlob.Uri.AbsoluteUri + sourceblobToken), token));
            }

            return mylistresults;
        }


        public static string GetXMLSerializedTimeSpan(TimeSpan timespan)
        // return TimeSpan as a XML string: P28DT15H50M58.348S
        {
            DataContractSerializer serialize = new DataContractSerializer(typeof(TimeSpan));
            XNamespace ns = "http://schemas.microsoft.com/2003/10/Serialization/";

            using (MemoryStream ms = new MemoryStream())
            {
                serialize.WriteObject(ms, timespan);
                string xmlstart = Encoding.Default.GetString(ms.ToArray());
                // serialization is : <duration xmlns="http://schemas.microsoft.com/2003/10/Serialization/">P28DT15H50M58.348S</duration>
                return XDocument.Parse(xmlstart).Element(ns + "duration").Value.ToString();
            }
        }

        private static readonly List<string> InvalidFileNamePrefixList = new List<string>
                {
                    "CON",
                    "PRN",
                    "AUX",
                    "NUL",
                    "COM1",
                    "COM2",
                    "COM3",
                    "COM4",
                    "COM5",
                    "COM6",
                    "COM7",
                    "COM8",
                    "COM9",
                    "LPT1",
                    "LPT2",
                    "LPT3",
                    "LPT4",
                    "LPT5",
                    "LPT6",
                    "LPT7",
                    "LPT8",
                    "LPT9"
                };

        private static readonly char[] NtfsInvalidChars = System.IO.Path.GetInvalidFileNameChars();


        public static bool BlobNameForAMSIsOk(string filename)
        {
            // check if the blob name is compatible with AMS
            // Validates if the blob name conforms to the following requirements
            // blob name must be a valid blob name.
            // blob name must be a valid NTFS file name.
            // blob should not contain the following characters: [ ] { } + % and #
            // blob should not contain only space(s)
            // blob should not start with certain prefixes restricted by NTFS such as CON1, PRN ... 
            // A blob constructed using the above mentioned criteria shall be encoded, streamed and played back successfully.

            if (string.IsNullOrWhiteSpace(filename))
            {
                return false;
            }

            // let's make sure we exract the file name (without the path)
            filename = Path.GetFileName(filename);

            // white space
            if (string.IsNullOrWhiteSpace(filename))
            {
                return false;
            }

            if (filename.Length > 255)
            {
                return false;
            }

            Regex reg = new Regex(@"[+%#\[\]]", RegexOptions.Compiled);
            if (filename.IndexOfAny(NtfsInvalidChars) > 0 || reg.IsMatch(filename))
            {
                return false;
            }

            //// Invalid NTFS Filename prefix checks
            if (InvalidFileNamePrefixList.Any(x => filename.StartsWith(x + ".", StringComparison.OrdinalIgnoreCase)) ||
                InvalidFileNamePrefixList.Any(x => filename.Equals(x, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            // blob name requirements
            try
            {
                NameValidator.ValidateBlobName(filename);
            }
            catch
            {
                return false;
            }

            return true;
        }

        // Return the list of problematic filenames
        public static List<string> ReturnFilenamesWithProblem(List<string> filenames)
        {
            List<string> listreturn = new List<string>();
            foreach (string f in filenames)
            {
                if (!BlobNameForAMSIsOk(f))
                {
                    listreturn.Add(Path.GetFileName(f));
                }
            }
            return listreturn;
        }

        public static string FileNameProblemMessage(List<string> listpb)

        {
            if (listpb.Count == 1)
            {
                return "This file name is not compatible with Media Services :\n\n" + listpb.FirstOrDefault() + "\n\nFile name is restricted to blob name requirements and NTFS requirements" + "\n\nOperation aborted.";
            }
            else
            {
                return "These file names are not compatible with Media Services :\n\n" + string.Join("\n", listpb) + "\n\nFile name is restricted to blob name requirements and NTFS requirements" + "\n\nOperation aborted.";
            }
        }
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

    public class AssetEntryV3 : INotifyPropertyChanged
    {

        private readonly SynchronizationContext syncContext;

        public AssetEntryV3(SynchronizationContext mysyncContext)
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

        public string _StorageAccountName;
        public string StorageAccountName
        {
            get => _StorageAccountName;
            set
            {
                if (value != _StorageAccountName)
                {
                    _StorageAccountName = value;
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

        public Guid AssetId { get; set; }

        public string _AlternateId;
        public string AlternateId
        {
            get => _AlternateId;
            set
            {
                if (value != _AlternateId)
                {
                    _AlternateId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string _Type;
        public string Type
        {
            get => _Type;
            set
            {
                if (value != _Type)
                {
                    _Type = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string _Size;
        public string Size
        {
            get => _Size;
            set
            {
                if (value != _Size)
                {
                    _Size = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private long _SizeLong;
        public long SizeLong
        {
            get => _SizeLong;
            set
            {
                if (value != _SizeLong)
                {
                    _SizeLong = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Bitmap _DynamicEncryption;
        public Bitmap DynamicEncryption
        {
            get => _DynamicEncryption;
            set
            {
                if (value != _DynamicEncryption)
                {
                    _DynamicEncryption = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _DynamicEncryptionMouseOver;
        public string DynamicEncryptionMouseOver
        {
            get => _DynamicEncryptionMouseOver;
            set
            {
                if (value != _DynamicEncryptionMouseOver)
                {
                    _DynamicEncryptionMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Bitmap _Publication = null;

        public Bitmap Publication
        {
            get => _Publication;
            set
            {
                if (value != _Publication)
                {
                    _Publication = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int? _Filters = null;
        public int? Filters
        {
            get => _Filters;
            set
            {
                if (value != _Filters)
                {
                    _Filters = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _FiltersMouseOver;
        public string FiltersMouseOver
        {
            get => _FiltersMouseOver;
            set
            {
                if (value != _FiltersMouseOver)
                {
                    _FiltersMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _PublicationMouseOver;
        public string PublicationMouseOver
        {
            get => _PublicationMouseOver;
            set
            {
                if (value != _PublicationMouseOver)
                {
                    _PublicationMouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _LocatorExpirationDate;
        public string LocatorExpirationDate
        {
            get => _LocatorExpirationDate;
            set
            {
                if (value != _LocatorExpirationDate)
                {
                    _LocatorExpirationDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _LocatorExpirationDateWarning;
        public bool LocatorExpirationDateWarning
        {
            get => _LocatorExpirationDateWarning;
            set
            {
                if (value != _LocatorExpirationDateWarning)
                {
                    _LocatorExpirationDateWarning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _AssetWarning;
        public bool AssetWarning
        {
            get => _AssetWarning;
            set
            {
                if (value != _AssetWarning)
                {
                    _AssetWarning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _Created;
        public string Created
        {
            get => _Created;
            set
            {
                if (value != _Created)
                {
                    _Created = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string p = "")
        {
            if (PropertyChanged != null)
            {
                try
                {
                    var handler = PropertyChanged;

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
                    var handler = PropertyChanged;

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
        public AuthenticationResult accessToken, accessTokenForRestV2;
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
            if (accessToken != null && accessToken.ExpiresOn.ToUniversalTime() < DateTimeOffset.UtcNow.AddMinutes(-3))
            {
                ConnectAndGetNewClientV3Async().GetAwaiter().GetResult();
                //Task.Run(async () => await ConnectAndGetNewClientV3Async());
            }
        }

        public async Task RefreshTokenIfNeededAsync()
        {
            // if end user use interactive authentication, then the token is not renewed automatically after it expired (one hour)
            // with Service Principal authentication, the token is renewed automatically apparently ! (result of tests)
            if (accessToken != null && accessToken.ExpiresOn.ToUniversalTime() < DateTimeOffset.UtcNow.AddMinutes(-3))
            {
                await ConnectAndGetNewClientV3Async();
            }
        }

        public async Task<AzureMediaServicesClient> ConnectAndGetNewClientV3Async()
        {
            if (!credentialsEntry.UseSPAuth)
            {
                // we specify the tenant id if there
                AuthenticationContext authContext = new AuthenticationContext(authority: environment.AADSettings.AuthenticationEndpoint + string.Format("{0}", credentialsEntry.AadTenantId ?? "common"), validateAuthority: true);

                bool adalTokenInteract = false;
                try
                {
                    accessToken = await authContext.AcquireTokenSilentAsync(
                                                               resource: environment.AADSettings.TokenAudience.ToString(),
                                                               clientId: environment.ClientApplicationId
                                                                 );

                }
                catch (AdalException adalException)
                {
                    if (adalException.ErrorCode == AdalError.FailedToAcquireTokenSilently
                        || adalException.ErrorCode == AdalError.InteractionRequired)
                    {
                        adalTokenInteract = true;
                    }
                }

                if (adalTokenInteract)
                {
                    try
                    {
                        accessToken = await authContext.AcquireTokenAsync(
                                        resource: environment.AADSettings.TokenAudience.ToString(),
                                        clientId: environment.ClientApplicationId,
                                        redirectUri: new Uri("urn:ietf:wg:oauth:2.0:oob"),
                                        parameters: new PlatformParameters(credentialsEntry.PromptUser)
                                        );


                        accessTokenForRestV2 = await authContext.AcquireTokenAsync(
                                                                resource: environment.MediaServicesV2Resource,
                                                                clientId: environment.ClientApplicationId,
                                                                redirectUri: new Uri("urn:ietf:wg:oauth:2.0:oob"),
                                                                parameters: new PlatformParameters(credentialsEntry.PromptUser)
                                                                );
                    }

                    catch (AdalException adalException)
                    {
                        Debug.Print("Adal token interact exception !" + adalException.Message);
                    }
                }

                credentials = new TokenCredentials(accessToken.AccessToken, "Bearer");

                // Getting Media Services accounts...
                AMSclient = new AzureMediaServicesClient(environment.ArmEndpoint, credentials)
                {
                    SubscriptionId = _azureSubscriptionId
                };
            }

            else // Service Principal
            {
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
            }

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AMSclient.SetUserAgent("AMSE", version);
            return AMSclient;
        }


        public async Task<string> GetStorageKeyAsync(string storageId)
        {
            string valuekey = null;
            bool classic = false;
            string version = "2016-01-01";
            string token = accessToken?.AccessToken;

            // if SP
            if (accessToken == null && credentialsEntry.UseSPAuth)
            {
                // let's get the current token in Service Principal mode
                TokenCacheItem accessTokenCache = TokenCache.DefaultShared.ReadItems()
                        .Where(t => t.ClientId == credentialsEntry.ADSPClientId)
                        .OrderByDescending(t => t.ExpiresOn)
                        .First();
                token = accessTokenCache?.AccessToken;
            }

            if (token == null)
            {
                return null;
            }

            if (storageId.Contains("/providers/Microsoft.ClassicStorage/storageAccounts"))
            {
                version = "2015-06-01";
                classic = true;
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(environment.ArmEndpoint + storageId.Substring(1) + "/listKeys?api-version=" + version, credentialsEntry.AzureSubscriptionId, GetStorageResourceName(storageId), GetStorageName(storageId)));

            request.Method = "POST";
            request.Headers["Authorization"] = "Bearer " + token;
            request.ContentType = "application/json";
            request.ContentLength = 0;

            try
            {
                //Get the response
                HttpWebResponse httpResponse = (HttpWebResponse)(await request.GetResponseAsync());

                using (System.IO.StreamReader r = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    string jsonResponse = await r.ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject(jsonResponse);
                    valuekey = classic ? data.primaryKey : data.keys[0].value;
                }
            }
            catch // not authorization
            {
                return null;
            }

            return valuekey;
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
        public SubscriptionMediaService MediaService;

        public string ADSPClientId;

        //  A contract is used to ignore this property when exporting the entry
        public string EncryptedADSPClientSecret;

        public string AadTenantId;
        public AzureEnvironment Environment;
        public PromptBehavior PromptUser;
        public bool ManualConfig = false;
        public bool UseSPAuth = false;
        public string Description;

        public CredentialsEntryV3(SubscriptionMediaService mediaService, AzureEnvironment environment, PromptBehavior promptUser, bool useSPAuth = false, string tenantId = null, bool manualConfig = false)
        {
            MediaService = mediaService;
            Environment = environment;
            UseSPAuth = useSPAuth;
            PromptUser = promptUser;
            ManualConfig = manualConfig;
            AadTenantId = tenantId;
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
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
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


    public static class DpiUtils
    {
        private enum MonitorOptions : uint
        {
            DefaultToNull = 0,
            DefaultToPrimary = 1,
            DefaultToNearest = 2
        }

        private enum DpiType
        {
            Effective = 0,
            Angular = 1,
            Raw = 2,
        }

        private struct SuggestedBoundsRect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private const int WM_DPICHANGED = 0x02E0;

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromPoint(Point pt, MonitorOptions dwFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("Shcore.dll")]
        private static extern IntPtr GetDpiForMonitor([In] IntPtr hmonitor, [In] DpiType dpiType, [Out] out uint dpiX, [Out] out uint dpiY);

        private static int GetPerMonitorDpiForControl(Control c)
        {
            var dpi = 96;

            try
            {
                var p = c.PointToScreen(new Point(c.Bounds.X + c.Bounds.Width / 2, c.Bounds.Y + c.Bounds.Height / 2));
                var monitorHandle = MonitorFromPoint(p, MonitorOptions.DefaultToNearest);
                uint dpiX, dpiY;
                GetDpiForMonitor(monitorHandle, DpiType.Effective, out dpiX, out dpiY);
                dpi = (int)dpiX;
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Exception when getting screen DPI: {e}");
            }

            return dpi;
        }

        public static void InitPerMonitorDpi(Form form)
        {
            var reportedDpi = form.DeviceDpi;
            var trueDpi = GetPerMonitorDpiForControl(form);

            if (reportedDpi == trueDpi)
                return;

            var wParam = (trueDpi << 16) | (trueDpi & 0xffff);
            var dpiRatio = trueDpi / (double)reportedDpi;
            var suggestedBounds = new SuggestedBoundsRect
            {
                Left = form.Location.X,
                Top = form.Location.Y,
                Right = form.Location.X + (int)(form.Width * dpiRatio),
                Bottom = form.Location.Y + (int)(form.Height * dpiRatio)
            };

            try
            {
                var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(suggestedBounds));
                Marshal.StructureToPtr(suggestedBounds, ptr, false);
                SendMessage(form.Handle, WM_DPICHANGED, wParam, ptr);
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"Exception when initialising per-monitor DPI: {e}");
            }
        }

        public static void UpdatedSizeFontAfterDPIChange(Control control, DpiChangedEventArgs e)
        {
            UpdatedSizeFontAfterDPIChange(new List<Control> { control }, e, null);
        }

        public static void UpdatedSizeFontAfterDPIChange(List<Control> controls, DpiChangedEventArgs e, Form currentForm)
        {
            return; // test as we moved to .Net framework v8

            if (currentForm != null) currentForm.SuspendLayout();
            Debug.Print($"Old DPI: {e.DeviceDpiOld}, new DPI {e.DeviceDpiNew}");
            float factor = (float)e.DeviceDpiNew / (float)e.DeviceDpiOld;
            foreach (var c in controls)
            {
                c.Font = new Font(c.Font.Name, c.Font.Size * factor);
                if (c.GetType() == typeof(MenuStrip) || c.GetType() == typeof(ContextMenuStrip))// if menu  control
                {
                    var sizevar = Convert.ToInt32(16f * (float)e.DeviceDpiNew / 96f);
                    (c as ToolStrip).ImageScalingSize = new Size(sizevar, sizevar);
                }
            }
            if (currentForm != null) currentForm.ResumeLayout();
        }

        public static void UpdatedSizeFontAfterDPIChangeV8(List<Control> controls, DpiChangedEventArgs e, Form currentForm)
        {
            if (currentForm != null) currentForm.SuspendLayout();
            Debug.Print($"Old DPI: {e.DeviceDpiOld}, new DPI {e.DeviceDpiNew}");
            float factor = (float)e.DeviceDpiNew / (float)e.DeviceDpiOld;
            foreach (var c in controls)
            {
                c.Font = new Font(c.Font.Name, c.Font.Size * factor);
                if (c.GetType() == typeof(MenuStrip) || c.GetType() == typeof(ContextMenuStrip))// if menu  control
                {
                    var sizevar = Convert.ToInt32(16f * (float)e.DeviceDpiNew / 96f);
                    (c as ToolStrip).ImageScalingSize = new Size(sizevar, sizevar);
                }
            }
            if (currentForm != null) currentForm.ResumeLayout();
        }
    }


    public static class HighDpiHelper
    {
        public static void AdjustControlImagesDpiScale(Control container)
        {
            //var dpiScale = GetDpiScale(container).Value;
            var dpiScale = GetDpiScale(container);
            if (CloseToOne(dpiScale))
                return;

            AdjustControlImagesDpiScale(container.Controls, dpiScale);
        }

        public static void AdjustControlImagesAfterDpiChange(Control container, DpiChangedEventArgs e)
        {
            float dpiScale = (float)e.DeviceDpiNew / (float)e.DeviceDpiOld;
            AdjustControlImagesDpiScale(container.Controls, dpiScale);
        }

        private static void AdjustButtonImageDpiScale(ButtonBase button, float dpiScale)
        {
            var image = button.Image;
            if (image == null)
                return;

            button.Image = ScaleImage(image, dpiScale);
        }

        private static void AdjustControlImagesDpiScale(Control.ControlCollection controls, float dpiScale)
        {
            foreach (Control control in controls)
            {
                if (control is ButtonBase button)
                {
                    AdjustButtonImageDpiScale(button, dpiScale);
                }
                else
                {
                    if (control is PictureBox pictureBox)
                    {
                        AdjustPictureBoxDpiScale(pictureBox, dpiScale);
                    }
                }
                AdjustControlImagesDpiScale(control.Controls, dpiScale);
            }
        }

        private static void AdjustPictureBoxDpiScale(PictureBox pictureBox, float dpiScale)
        {
            var image = pictureBox.Image;
            if (image == null)
                return;

            if (pictureBox.SizeMode == PictureBoxSizeMode.CenterImage)
                pictureBox.Image = ScaleImage(pictureBox.Image, dpiScale);
        }

        private static bool CloseToOne(float dpiScale)
        {
            return Math.Abs(dpiScale - 1) < 0.001;
        }

        /*
        public static Lazy<float> GetDpiScale(Control control)
        {
            return new Lazy<float>(() =>
            {
                using (var graphics = control.CreateGraphics())
                    return graphics.DpiX / 96.0f;
            });
        }
        */

        public static float GetDpiScale(Control control)
        {
            return control.DeviceDpi / 96f;


        }


        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, float dpiScale)
        {
            if (image == null) return null;

            var newSize = ScaleSize(image.Size, dpiScale);
            var newBitmap = new Bitmap(newSize.Width, newSize.Height);

            Bitmap tempbitmap = (Bitmap)image; // to avoid multi thread using the same bitmap and create an exception

            using (var g = Graphics.FromImage(newBitmap))
            {
                // According to this blog post http://blogs.msdn.com/b/visualstudio/archive/2014/03/19/improving-high-dpi-support-for-visual-studio-2013.aspx
                // NearestNeighbor is more adapted for 200% and 200%+ DPI

                var interpolationMode = InterpolationMode.HighQualityBicubic;
                // if (dpiScale >= 2.0f)
                //     interpolationMode = InterpolationMode.NearestNeighbor;

                g.InterpolationMode = interpolationMode;
                g.DrawImage(tempbitmap, new System.Drawing.Rectangle(new Point(), newSize));
            }

            return newBitmap;
        }

        private static Size ScaleSize(Size size, float scale)
        {
            return new Size((int)(size.Width * scale), (int)(size.Height * scale));
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
