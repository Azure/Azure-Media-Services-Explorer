# AMSE release notes history

## Version 5.8.2.0 (June 13th, 2024) brings the following features and improvements

* Support for AMS account expiration date display. User can request a one month extension
* Updates to MK.IO (SDK, endpoint, name, logo...)
* Nugget packages and documentation update
* Note : the MSI is not signed as the process for the signature of binaries has changed at Microsoft

## Version 5.8.1.0 (December 8th, 2023) brings the following features and improvements

* Bug fixes
* Nugget packages and documentation update

## Version 5.8.0.0 (November 23rd, 2023) brings the following features and improvements

* Support for MediaKind MK.IO
  * UI to connect to MK.IO instance
  * Provide Storage access to MK.IO (using add/remove commands)
  * Content Key Policies migration to MK.IO (create/delete commands)
  * Assets migration to MK.IO (create/delete commands) with migration of locators
  * Display MK.IO info and streaming URLs in asset information
* Nugget packages and documentation update

## Version 5.7.2.0 (July 25th, 2023) brings the following features and improvements

* Displays the AMS Retirement notice and a link to the retirement guide
* Nugget packages and documentation update

## Version 5.7.1.0 (April 4th, 2023) brings the following features and improvements

* Adds Rewind window setting for live output
* Fixes a few bugs
* Nugget packages and documentation update

## Version 5.7.0.0 (March 14th, 2023) brings the following features and improvements

* Code refactoring - now uses the new [Azure.ResourceManager.Media](https://github.com/Azure/azure-sdk-for-net/blob/Azure.ResourceManager.Media_1.1.0/sdk/mediaservices/Azure.ResourceManager.Media/README.md) nugget package (AMS SDK)
* Authentication now uses [Web Account Manager (WAM)](https://learn.microsoft.com/azure/active-directory/develop/scenario-desktop-acquire-token-wam)
* Added Saas presets for Media Encoder Standard
* Support for audio track creation and edit, support for text track edit
* Displays live event quota
* Nugget packages and documentation update

## Version 5.6.1.0 (January 4th, 2023) brings the following features and improvements

* Application has been upgraded to .NET 7.0
* Code refactoring to use new Azure.ResourceManager nugget
* Bug fixes, REST version update
* Nugget packages and documentation update

## Version 5.5.2.0 (September 7th, 2022) brings the following features and improvements

* Support for the [new API](https://docs.microsoft.com/en-us/rest/api/media/tracks) for asset tracks management in the asset information UI
  * List the tracks of the asset
  * Create a text track from a .vtt blob
  * Text track player visibility management and deletion
* Live transcription is GA, support for more languages
* Using default SystemAware mode for High DPI, as there are issues with PerMonitor and PerMonitorV2 modes
* Bug fixes
* Nugget packages and documentation update

## Version 5.5.0.0 (June 03, 2022) brings the following features and improvements

* Support for low latency v2 parameter (LL-HLS) for live events
* It is now possible to select an existing content key policy when creating a protected locator (AES/DRM)
* Moved project to .Net 6
* Bug fixes
* Nugget packages and documentation update

## Version 5.4.7.0 (January 28, 2022) brings the following features and improvements

* Better management of settings for stopped and running live events
* Media Services account creation display only locations where Media Services is present
* Bug fixes
* Nugget packages and documentation update

## Version 5.4.6.0 (October 19, 2021) brings the following features and improvements

* Support for the new [basic pass-through live event](https://docs.microsoft.com/en-us/azure/media-services/latest/live-event-types-comparison-reference#types-comparison)
* Support for [constrained Content Aware Encoding presets](https://docs.microsoft.com/en-us/azure/media-services/latest/encode-content-aware-concept#configure-output-settings) when creating a transform. It enables the user to constrain the encoding with the CAE presets, like the maximum resolution or the minimum or maximum bitrate.
* Microsoft.Azure.Management.Media SDK updated to v5.0.0
* Nugget packages and documentation update

## Version 5.4.4.0 (July 12, 2021) brings the following features and improvements

* It's now possible to create an AMS Account (using the account pickup button) in interactive authentication mode
  * if location supports Availability Zones then more redundancy options are presented for the storage account creation
* Program Output URLs are now displayed even if live event is not started or no live data reached the asset yet
* Storage attachment UI update that lists the existing storage accounts in the same location
* By default, when uploading a file, the file name is stored in alternate Id field. As this field is indexed, it makes the assets search easier
* New "Open in portal" option in Account Management menu
* Nugget packages and documentation update
* Code optimization, bugs and crashes fixes

## Version 5.4.3.0 (June 23, 2021) brings the following features and improvements

* Support for archiving all bitrates in subclipping UI, using the new CopyAllBitrateNonInterleaved preset. This preset is also exposed in Media Encoder Standard UI
* Support for [IP addresses allow list](https://docs.microsoft.com/en-us/azure/media-services/latest/drm-content-protection-key-delivery-ip-allow) for AES & DRM key delivery
* Upload and download transfers can now be resumed
* Job and Transform reports
* Fixes an issue when downloading a live archive, and when copying asset between two storage accounts
* Azure Media Services SDK update to version 4.0.0
* Nugget packages and documentation update
* Code optimization, bugs and crashes fixes

## Version 5.4.2.1 (June 4, 2021) brings the following features and improvements

* Project has been migrated to [Window Forms .Net v5](https://devblogs.microsoft.com/dotnet/whats-new-in-windows-forms-runtime-in-net-5-0/)
  * Code is now based on .Net v5. Improvements should be a better management of high DPI display and better performances. AMSE needs the [.NET **Desktop** Runtime](https://dotnet.microsoft.com/download/dotnet/5.0) 5.0.6 or later
  * Authentication has been moved from ADAL to MSAL (as the ADAL library has been deprecated and does not work with .Net 5).
* Other platform changes
  * Setup project is now using the [Microsoft Visual Studio Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects). InstallShield has been dropped. 64 bits version of AMSE tool is now shipped
  * Export to Excel feature has been redeveloped using the [Open XML SDK](https://github.com/) for no more dependancy to Excel software to generate the xlsx file
  * Webview moved to [Webview2 runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/#download-section). Component installation is proposed when the app is launched
* Widevine DRM schema update for correct serialization
* Support for Multiple Input File stitching
* Added [HEVC encoding preset](https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes#hevc-encoding-support-in-standard-encoder) for Media Encoder Standard UI
* New [language support for AudioAnalyzer preset](https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes#new-language-support-added-to-the-audioanalyzer-preset)
* [Japanese translation fixes](https://github.com/Azure/Azure-Media-Services-Explorer/pull/142) from @m-otoguro
* Telemetry added to the tool through the use of Application Insights to report exceptions and anonymized usage statistics
* Media unit management feature using REST v2 has been removed
* Nugget packages and documentation update
* Code refactoring
* Bugs fixes

## Version 5.3.0.1 (January 11, 2021) brings the following features and improvements

* Live event
  * Adds support for new custom static hostname prefix
  * Adds support for live encoding key frame interval
* Thumbnails and sprite thumbnails can be generated by AMSE as a custom Standard Encoder preset
* Adds support for new [**Basic Audio Analysis**](https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes#basic-audio-analysis)
* Removed 'v3' from AMSE application name
* Microsoft.Azure.Management.Media package updated to v3.0.3
  * Live transcript setup now uses the SDK. Feature is better managed when editing live events
* Nugget packages and documentation update
* Bugs fixes

## Version 5.2.1.0 (October 26, 2020) brings the following features and improvements:

* Blob upload, copy and download processes now use the [Azure Data Movement Library](https://github.com/Azure/azure-storage-net-data-movement) for better performance
  * Related settings can be set in a new tab in Options/Options : number of parallel operations, MD5 check on/off and blob block size
  * Progress bar is correctly updated for upload and download
* New feature added : export assets information to Excel or CSV
* Nugget packages and documentation update
* No more exe for the installer, but a signed MSI file

## Version 5.2.0.8 (September 18, 2020) brings the following features and improvements

* Bug fix : storage account selection when creating a new asset now works properly
* Bug fix : live event status is refreshed when using REST
* Setup and AMSExplorer.exe files are now code signed to remove the strong security warnings when downloading the tool from GitHub
* Nugget packages and documentation update

## Version 5.2.0.7 (September 8, 2020) brings the following features and improvements

* A **custom encoding JSON preset** can now be pasted when creating a new transform, or when adding an output to an existing transform (code uses the REST API)
  * It is also possible to see the JSON preset of an existing transform output
* Fixes an issue regarding the duration calculation of a live/archive asset. [Issue #138](https://github.com/Azure/Azure-Media-Services-Explorer/issues/138)
* Nugget packages and documentation update, code cleanup

## Version 5.2.0.5 (July 17, 2020) brings the following features and improvements:

* An output can be added to an existing Transform
* Client manifest (.imsc) can be generated and stored as a blob
  * code change to reference the client manifest in server manifest
  * in asset tools : option to list all published assets and add client manifest when needed
* Nugget packages and documentation update, code cleanup
* Bug fixes

## Version 5.2.0.4 (June 29, 2020) brings the following features and improvements

* Display the content key policies in a new tab
* Client manifest (.imsc) can be generated and stored as a blob
* Support for the API access JSON data generated by the Azure portal
* Bug fixes (subscriptions browsing, crash when doing a drag and drop of file or folder in the asset grid)

## Version 5.2.0.2 (June 13, 2020) brings the following features and improvements

* Application moved to .Net Framework v4.8. You may loose the list of AMS accounts in the login window after the upgrade. To avoid this, export your entries from v5.0.x.x and reimport them in v5.2.0.x.
* Update to [Live Transcription](https://docs.microsoft.com/en-us/azure/media-services/latest/live-transcription) with support of [additional regions](https://docs.microsoft.com/en-us/azure/media-services/latest/azure-clouds-regions#feature-availability-in-preview) and additional languages (more details later this month)
* Generation of test token now uses the selected DRM key
* New icon for AMSE v5
* Content key policy : add support for OpenID Connect
* Removed the deprecated storage analytics (not in the new Storage SDK)
* Bug fixes (FairPlay rental duration, Live archive duration, ...)
* Code optimizations, SDKs update

## Version 5.0.16.0 (March 16, 2020) brings the following features and improvements

* Support for [Live Transcriptions](https://docs.microsoft.com/en-us/azure/media-services/latest/live-transcription) (which is in preview in West Us 2 region)
* Visual Studio Solution : [migrated from packages.config to PackageReference](https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference)

## Version 5.0.15.0 (January 10, 2020) brings the following features and improvements

* Updated with the new v2.0.4 Azure Media SDK
  * Support for Job start and end times display. Duration of jobs are also displayed.
  * Support for the new FairPlay dual expiry settings (rentals)
  * New ContentAwareEncoding MES preset (GA)
* Asset copy feature has been introduced. It can be used to copy one or several assets to the same or a different AMS account. It supports VOD and live archive assets. The destination storage account attached to the account can also be selected.
* Optimization of REST calls for the background assets listing
* When submitting a job, the output asset name can now be customized
* A short GUID of 10 characters is now used as uniqueness
* Displays an example of Az Cli syntax at start
* Bug fixes

## Version 5.0.14.0 (December 6, 2019) brings the following features and improvements

* It's now possible to select an existing asset as an output of a new job. It allows to output several jobs to the same asset.
* Bug fixes

## Version 5.0.13.0 (November 26, 2019) brings the following features and improvements

* AMSE uses a new way to generate asset name for output asset. Output asset name is now based on source asset name + Transform name + a short random GUID. This should make the search for assets easier : for example, using the search option "Asset name (starts with)" and specifying the source asset name should now return related output assets too.
* Nugget packages update
* Bug fixes

## Version 5.0.12.0 (October 14, 2019) brings the following features and improvements

* New empty asset creation feature
* Now it is possible to configure some advanced options for new assets, like the container name
* Portions of the code moved to Async for better UI responsiveness (work on going)
* Bug fixes

## Version 5.0.11.0 (September 20, 2019) brings the following features and improvements

* Multi DRM (with Apple FairPlay) support when creating a locator
* Better High DPI support (for Windows 10), several UI updates
* Portions of the code moved to Async for better UI responsiveness (work on going)
* Bug fixes

## Version 5.0.10.0 (September 9, 2019) brings the following features and improvements

* Subclipping (preview) - for live and vod, with codec copy or transcoding
* Better High DPI support (for Windows 10), several UI updates
* Bug fixes

## Version 5.0.9.0 (August 23, 2019) brings the following features and improvements

* Adds an option to create an Audio and Video streams copy preset for Media Encoder Standard
* Fixes an issue with REST v2 call (deadlock)
* All nuget packages were updated to latest version

## Version 5.0.7.0 (July 30, 2019) brings the following features and improvements

* Add an option in 'Transforms & Jobs' tab to scale media reserved units using old AMS v2 REST API
* There is now a way to generate a DRM token from Asset Information / Content Protection tab
* Add .mpd and .m3u8 extensions to DASH and HLS Urls
* UI update to make it more different from v4
* Bug fixes

## Version 5.0.6.0 (July 17, 2019) brings the following features and improvements

* Custom block size to leverage [Azure Storage high-throughput](https://azure.microsoft.com/en-us/blog/high-throughput-with-azure-blob-storage/) for video file upload
* Support for DRM token replay prevention (preview)
* Job creation UI update
* Application upgraded to .Net Framework 4.7
* AMSE string in REST request user-agent
* Bug fixes

## Version 5.0.5.0 (July 4, 2019) brings the following features and improvements

* Support for time trimming when submitting a job from a transform (for MES presets)
* Redesign of the job submit UI, transform info UI, job info UI, Transforms/Jobs panel UI
* Fixes an issue when filtering assets based on assetid
* Adds support for filtering assets using a beginwith method on asset name
* It's now possible to select the locator in the UI when playing back an asset
* Displays the streaming URLs when a live output has been created and no data has yet arrived in it
* Bug fixes

## Version 5.0.4.1 (June 12, 2019) brings the following features and improvements

* Support for [Premium Live encoder](https://azure.microsoft.com/en-us/updates/premium-live-encoder-now-supports-streaming-at-1080p-and-30-frames-per-second/) when creating a live event. This encoder supports up to 1080p and 30 frames per second
  * Live event creation UI updated with default preset info
* Redesign of locator creation UI to support [associated filters](https://docs.microsoft.com/bs-latn-ba/azure/media-services/latest/filters-concept#associating-filters-with-streaming-locator)
* Number of filters and publication expiration now displayed in asset list
* Media SDK update to latest version
* Bug fixes

## Version 5.0.3.0 (June 3, 2019) brings the following features and improvements

* Support for face detector preset
* Major update on content protection for AMS v3 :
  * More info for content protection in asset info (streaming policy, content keys, content key policy)
  * When creating a locator: support for all predefined streaming policies (except FairPlay), with setup of options for PlayReady and Widevine for CENC policy
* Bug fixes : interactive token renewal, filters are back in streaming endpoint selection, download only locator display fix
* Solve an issue with High DPI screen and Live tab

## Version 5.0.2.0 (May 13, 2019) brings the following features and improvements

* Code update to support High DPI screen
* Support for SAS Urls creation, and asset import from SAS Container path
* Update to latest ADAL SDK and other librairies
* Bug fixes

## Version 5.0.1.0 (April 19, 2019) brings the following features and improvements

* Support for [experimental preset for content-aware encoding](https://docs.microsoft.com/en-us/azure/media-services/latest/cae-experimental)
* Content keys are now displayed in asset info UI
* Some deadlocks were removed, better management of authentication token renewal
* AMS v2 SDK has been removed from code and binary, AMS v3 and Storage SDKs updated to latest version
* Bug fixes

## Version 5.0.0.21 Preview (March 27, 2019) brings the following features and improvements

* Critical bug fixes and performance optimizations

## Version 5.0.0.17 Preview (March 18, 2019) brings the following features and improvements

* Support for multi tenant browsing when adding an account
* Support for Live Output outputSnapTime
* Started implementation of content protection setup when publishing an asset - clear key mode
* Athentication token is now refreshed when expired - this was causing crashes
* Better error management for some live entities operations
* Bug fixes

## Version 5.0.0.16 Preview (February 4, 2019) brings the following features and improvements

* Support for Azure national clouds authentication
* Corrected file and asset size calculation (now 1 GB = 1000 MB)
* Major Bug fixes

## Version 5.0.0.12 Preview (January 11, 2019) brings the following features and improvements

* Displays capacity used by storage account, and support for storage settings
* Major Bug fixes

## Version 5.0.0.11 Preview (January 8, 2019) brings the following features and improvements

* First public preview of AMSE for AMS v3
