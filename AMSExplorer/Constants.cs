//----------------------------------------------------------------------------------------------
//    Copyright 2022 Microsoft Corporation
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

namespace AMSExplorer
{
    public static class Constants
    {
        public static string webViewCachePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AMSE";

        public const string Webview2Installer = "https://go.microsoft.com/fwlink/p/?LinkId=2124703";
        public const string Webview2RegPath64 = "SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}";
        public const string Webview2RegPath32 = "SOFTWARE\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}";
        public const string Webview2MinVersion = "100.0.1185.50";

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

        public const string PathPremiumWorkflowFiles = @"PremiumWorkflowSamples\";
        public const string PathMESFiles = @"MESPresetFiles\";
        public const string PathConfigFiles = @"configurations\";
        public const string PathManifestFile = @"manifest\";
        public const string PathHelpFiles = @"HelpFiles\";
        public const string PathDefaultSlateJPG = @"SlateJPG\";

        public const string PathLicense = @"license\Azure Media Services Explorer.rtf";

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
        public const string LinkQAAMS = @"https://docs.microsoft.com/en-us/answers/topics/azure-media-services.html";
        public const string LinkBlogAMS = @"https://azure.microsoft.com/en-us/blog/topics/media-services/";

        public const string LinkMoreInfoSE = "https://docs.microsoft.com/en-us/azure/media-services/latest/stream-streaming-endpoint-concept";
        public const string LinkMoreInfoAzCopy = "https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azcopy-v10";

        public const string LinkMoreInfoVideoAnalyzer = "https://docs.microsoft.com/en-us/azure/media-services/latest/analyzing-video-audio-files-concept";
        public const string LinkMoreInfoMediaEncoderBuiltIn = "https://docs.microsoft.com/en-us/azure/media-services/latest/encoding-concept";
        public const string LinkMoreInfoMediaEncoderThumbnail = "https://docs.microsoft.com/en-us/azure/media-services/latest/transform-generate-thumbnails-dotnet-how-to";

        public const string LinkHowIMoreInfoDynamicManifest = "https://docs.microsoft.com/en-us/azure/media-services/latest/filters-dynamic-manifest-concept";
        public const string LinkHowIMoreInfoSubclipping = "https://azure.microsoft.com/en-us/blog/dynamic-manifests-and-rendered-sub-clips/";
        public const string LinkMoreInfoSubClipAMSE = "https://azure.microsoft.com/en-us/blog/sub-clipping-and-live-archive-extraction-with-media-encoder-standard/";
        public const string LinkMoreInfoLiveEventTypes = "https://docs.microsoft.com/en-us/azure/media-services/latest/live-event-types-comparison-reference";
        public const string LinkMoreInfoPricing = "https://azure.microsoft.com/en-us/pricing/details/media-services/";
        public const string LinkMoreInfoStorageVersioning = "https://docs.microsoft.com/en-us/rest/api/storageservices/Versioning-for-the-Azure-Storage-Services";
        public const string LinkMoreInfoStorageAnalytics = "https://docs.microsoft.com/en-us/azure/storage/blobs/monitor-blob-storage";
        public const string LinkMoreInfoFairPlay = "https://docs.microsoft.com/en-us/azure/media-services/latest/drm-fairplay-license-overview";

        public const string LinkPlayReadyTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/latest/drm-playready-license-template-concept";
        public const string LinkPlayReadyCompliance = "http://www.microsoft.com/playready/licensing/compliance/";
        public const string LinkWidevineTemplateInfo = "https://docs.microsoft.com/en-us/azure/media-services/latest/drm-widevine-license-template-concept";

        public const string LinkAMSCreateAccount = "https://docs.microsoft.com/en-us/azure/media-services/latest/account-create-how-to";
        public const string LinkAMSAzCli = "https://docs.microsoft.com/en-us/azure/media-services/latest/access-api-howto";
        public const string LinkAMSAvailabilityZones = "https://docs.microsoft.com/en-us/azure/media-services/latest/concept-availability-zones";
        public const string LinkAMSManagedIdentities = "https://docs.microsoft.com/en-us/azure/media-services/latest/concept-managed-identities";
        public const string LinkAMSCustomerManagedKeys = "https://docs.microsoft.com/en-us/azure/media-services/latest/concept-use-customer-managed-keys-byok";

        public const string LinkAMSE = "http://aka.ms/amse";
        public const string LinkMailtoAMSE = "mailto:amse@microsoft.com?subject=Azure Media Services Explorer - Question/Comment";
        public const string LinkReportBugAMSE = @"https://github.com/Azure/Azure-Media-Services-Explorer/issues";
        public const string LinkAMSEReleaseNotes = @"https://github.com/Azure/Azure-Media-Services-Explorer/blob/main/AllReleaseNotes.md";

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

}
