Version 4.3.15.0 (November 16, 2020) brings the following features and improvements :

* Makes the program operations sequential
* Removed unavailable processors
* Updated documentation
* MSI installation file is now signed


Version 4.3.14.0 (June 2, 2020) brings the following features and improvements :

* Fixes FairPlay rental duration unit


Version 4.3.13.0 (February 27, 2020) brings the following features and improvements :

* Updated deprecation notices for some processors
* Updated offline documentation


Version 4.3.12.0 (January 9, 2020) brings the following features and improvements :

* Updated parameters for Media Indexer v1
* Updated offline documentation


Version 4.3.11.0 (December 6, 2019) brings the following features and improvements :

* Updated retirement notice for some processors with a link
* Updated offline documentation


Version 4.3.10.0 (October 14, 2019) brings the following features and improvements :

* Retirement notice for some processors
* Updated offline documentation


Version 4.3.9.0 (September 9, 2019) brings the following features and improvements :

* Retirement notice for some processors
* Updated offline documentation


Version 4.3.8.0 (August 23, 2019) brings the following features and improvements :

* Updated the query to list JPG assets for Slate insertion with live encoding channels
* Updated offline documentation


Version 4.3.7.0 (July 22, 2019) brings the following features and improvements :

* Updated the query to get workflow assets for Premium Encoder UI
* Updated offline documentation


Version 4.3.5.0 (February 25, 2019) brings the following features and improvements :

* Corrected file and asset size calculation (now 1 GB = 1000 MB)
* Bug fixes
* Updated offline documentation


Version 4.3.4.0 (December 20, 2018) brings the following features and improvements :

* Multiple transfers bug fix
* Updated offline documentation
* [Fix](https://github.com/Azure/Azure-Media-Services-Explorer/pull/104) to "264 Multiple Bitrate 16x9 for iOS" preset
* Azure Media Hyperlapse was removed from the UI [as announced here](https://azure.microsoft.com/en-us/updates/retirement-of-media-hyperlapse-in-preview-on-march-29-2019/)


Version 4.3.3.0 (October 20, 2018) brings the following features and improvements :

* Bug fixes
* Update to AMS 4.2.0 SDKs (for v2 API)


Version 4.3.2.1 (July 30, 2018) brings the following features and improvements :

* Fixes an issue with multiple transfers


Version 4.3.2.0 (June 14, 2018) brings the following features and improvements :

* Updated Japanese translation
* Visual Studio 2017 /Installshield 2018 project


Version 4.3.1.0 (May 25, 2018) brings the following features and improvements :

* Links and doc updated
* Media Services v2 label
* Warning when deleting a policy. Default mode is removal, not deletion
* Bug fixes


Version 4.3.0.0 (May 15, 2018) brings the following features and improvements :

* The **storage account attachment** feature use the latest management API
  * It is now possible to detach a secondary storage account (if no asset is stored in it)
  * Updated UI
* Update to **Content Moderation** preset (uses now version 2)
* Remove support for RTP/MPEG-TS live ingest with encoding channels (see the [announcement](https://social.msdn.microsoft.com/Forums/azure/en-US/f521e036-1165-4783-898d-781248b910de/critical-announcement-action-required-migrate-from-rtpmpeg2-input-in-azure-media-services-live?forum=MediaServices))
* Fixes a bug related to visual overlay with Media Encoder Standard
* Other bug fixes


Version 4.2.0.1 (February 9, 2018) brings the following features and improvements :

* Adds support for offline Fairplay license configuration
* New tab refresh button
* Update to asset copy:
  * Support for AAD authentication
  * Option to copy alternate Id
* Adds Enhanced Server Manifest option in MES 
* Removes the support for ACS when connecting to AMS accounts


Version 4.1.2.0 (November 23, 2017) brings the following features and improvements :

* Minor improvements and several bug fixes (stitching, bad characters in filter tracks..)


Version 4.1.1.0 (November 2, 2017) brings the following features and improvements :

* Support for lowest bitrate subclipping
* Update to Redaction media processor (now GA). New blur modes (high, med, low, black, box)
* Update to Media Encoder Standard UI (some checkboxes have 3 states)
* Warning for accounts which use ACS authentication
* Uses the local character separator for CSV export
* Fixes an issue with the player in subclipping, and another issue with asset report


Version 4.0.1.0 (July 28, 2017) brings the following feature and improvement :

* Supports a new preset for Media Encoder Standard : "Content Adaptive Multiple Bitrate MP4"
  *  This is an automatic preset which muxes the audio stream in each MP4 file 
  *  Designed for Streaming and Progressive Download
  *  This is now the default preset
  * More info [here](https://docs.microsoft.com/en-us/azure/media-services/media-services-autogen-bitrate-ladder-with-mes)
* Fixes a bug that was preventing AMSE to connect to other environment than Azure Global


Version 4.0.0.0 (June 22, 2017) brings the following features and improvements :

* Support for Azure Active Directory (AAD) authentication for Media Services
  *  Two modes are supported: Interactive authentication (recommended), and Service Principal
  *  Support for Azure Global, Azure in China, Azure in Germany, US Government and custom settings
  *  See the [blog announcement](https://azure.microsoft.com/en-us/blog/azure-media-service-aad-auth-and-acs-deprecation/) for more information
* Adds support for British English and Mexican Spanish in Indexer v2 
* More reliable upload and download. The following settings are exposed: parallel transfer thread count and number of concurrent transfers (in Options). Default is one transfer process in the transfer tab.
* Media Services SDK updated to 4.0.0.4, with ADAL support. Storage SDK updated to 8.1.4.0.
* Fixes several bugs


Version 3.49.0.0 (June 5, 2017) brings the following features and improvements :

* Support for the Russian language in Indexer v2 preview
* Provides a way to hide the taskbar notifications 
* Default to https for Azure Media Player and streaming URLs
* Force the use of Internet Explorer for previewing a live channel (because Flash or Silverlight is required for smooth streaming)
* Displays the URL generation option dialog box in all cases
* Fixes several bugs and crashes


Version 3.48.0.0 (March 8, 2017) brings the following features and improvements :

* The default preset for Media Encoder Standard (MES) is the new automatic **"Adaptive Streaming"** preset.
  * It autogenerates a bitrate ladder (bitrate-resolution pairs) based on the input resolution and bitrate. The auto-generated preset will never exceed the input resolution and bitrate. For example, if the input is 720p at 3Mbps, output will remain 720p at best, and will start at rates lower than 3Mbps.
  * See the [documentation](https://docs.microsoft.com/en-us/azure/media-services/media-services-autogen-bitrate-ladder-with-mes)
* Japanese UI
  * This version includes Japanese UI resources. AMSE will be displayed in Japanese if the selected language of Windows is Japanese
  * Language can be forced by launching the command line "AMSExplorer.exe JA"
  * Thanks to [shigeyf](https://github.com/shigeyf) for the translation
* Asset files can be sorted by clicking the column
* New option to export asset(s) information to a CSV file (in addition to Excel)
* OCR : fix a bug with language preset for Chinese or Serbian languages
* Better support for inserting black video with MES
* Code change to better support pre-fragmented assets
* Removed some analytics pre-check as MES encoded assets are supported as input for most analytics
* Added code to check asset size regarding the media reserved unit type. This is documented in the [quotas and limitations article](https://docs.microsoft.com/en-us/azure/media-services/media-services-quotas-and-limitations)
* Other bug fixes


Version 3.47.2.0 (January 16, 2017) brings the following features and improvements :

* Update for Content Moderator (private preview) which uses a new configuration JSON.
* Fixes an issue with output minute multiplier calculation.
* In some cases, a warning message was incorrectly displayed when creating a locator.


Version 3.47.0.0 (January 11, 2017) brings the following features and improvements :

* Support for the new auto scaling Standard Streaming Endpoint and new enhanced CDN configuration. See the [announcement here](https://azure.microsoft.com/en-us/blog/standardstreamingendpoint/)
   * Provides an optin to migrate a streaming endpoint from Classic to Standard.
   * Use the new names : Standard, Classic and Premium. 
   * Support for the new CDN enhanced configuration : CDN type (Verizon Standard, Verizon Premium, Akamai Standard) and profile.
* Support for pre-fragmented output when using Azure Media encoders. 
   * The output from encoding can be fragmented for better streaming performance.
   * Can be configured in the task/job option.
* Azure Media Analytics update. Support for private preview of Video Annotation. Update to Media Indexer v2 for better language name description.
* Update related to the new [pricing model for encoding](https://azure.microsoft.com/en-us/blog/encoding-with-media-services-everything-you-need-to-know-about-new-pricing-model/)
   * Media Encoder Standard dialog box : the output minute multiplier is calculated and displayed, and the price per input minute is estimated.
* Source code and UI has been rearchitectured for multilanguage support (some strings have been extracted to resources ; this work is not complete yet).
* Media Services SDK updated to version 3.8.0.5.


Version 3.46.1.0 (December 2, 2016) brings the following features and improvements :

* Bug fixes


Version 3.46.0.0 (November 21, 2016) brings the following features and improvements :

* Support for new **“Blob storage accounts”** including **hot and cool** tiers
  * Only for additional (attached) storage accounts
  * Primary storage account still needs to be regular storage account and only LRS/GRS/GRS-RA is supported
  * Uploading to new blob storage accounts is supported
* New dialog box when a single file or multiple files are uploaded
  * User can select the destination storage account, storage encryption or not, single asset option 
* Update to **Azure Media Telemetry**
* Support for JSON semaphore file in Watchfolder
* Added support for Japanese in Media Indexer v2
* Documentation links update
* Updated the Media Services SDK to version 3.8.0.3
* Bug fixes


Version 3.45.0.0 (November 14, 2016) brings the following features and improvements :

* Update for Media Encoder Standard (MES)
  * Update to audio only presets
  * Multi asset input support (for stitching)
  * Video rotation mode
* Minor update to **Azure Media Analytics**
* Dynamic Encryption
  * It's now possible to select an existing delivering policy and key authorization policy in Dynamic Encryption Wizard and Asset info, as it is recommended to reuse existing policies.
* File name check when uploading a file
* Support for calling an API in Watchfolder (to call an Azure logical app, for example)
* Bug fixes, links update


Version 3.44.1.0 (September 9, 2016) brings the following features and improvements :

* New **Import from SAS Container Path** option
  * AMS Explorer now generates SAS locators that include the list permission
  * A SAS Container Path can be used to import an asset to the current AMS account. This is useful to share multiple files asset or a live archive with another user using only one URL.
* Additional **advanced features** with Media Encoder Standard (MES)
  * Insert black video, non interleaved mode for audio streams
* Update to **Azure Media Analytics** (Redaction, Face detection)
* Bug fixes


Version 3.43.0.0 (August 26, 2016) brings the following features and improvements :

* Support for **Azure Media Telemetry** (Private Preview - activated on selected accounts)
  * initial configuration, update and deletion (management menu)
  * loading and displaying telemetry data for Streaming Endpoints and Channels
  * Media Services Account ID field added to the logon window
* **Video Cropping** with Media Encoder Standard (MES)
* **Visual Region Editor**
  * This editor displays the asset thumbnails and provides a way to define the cropping region for MES, and detection regions for Media Analytics OCR and Motion Detection
  * Supports rectangles and polygones when available
  * Uses the output of an "asset analysis job" which generates 100% resolution thumbnails. Settings can be changed in options.
* New **First Quality Bitrate for HLS** setting in filters
* New tab in job details that displays the input and output assets
* New **single asset output** option in advanced processing / process asset(s) with multiple processors
* Locators can be cloned (same ID) when copying an asset between two accounts in two regions
* Credentials can be exported and imported using **JSON** files
* Fixes an issue with displayed type of multi MP4 files asset
* FairPlay support is GA. Specific icon for FairPlay protected assets
* Updated the Media Services SDK to version 3.7.0.1
* Bug fixes and improvements 


Version 3.42.0.0 (July 5, 2016) brings the following features and improvements :

* Detects **WebVTT subtitles** in the asset and pass them automatically to **Azure Media Player**
* Support for **semaphore files in the watch folder** mechanism for multi files asset upload.
  * The semaphore is a XML file, compatible with the ones created by Rozhet. A sample file is provided.
  * When a semaphore file is dropped to the watch folder, AMSE reads and uploads the listed files as a single asset.
* Media Analytics
  * Update to **Motion Detection** (Media Analytics) to support new settings and features.
  * A future version of AMSE will provide a visual regions editor to define regions for OCR and Motion Detection
  * Update to other media analytics processors to display the JSON generated configuration
* In the tranfer tab, it's possible to manage dynamically the number of **concurrent transfers** : 1/2/3/4/unlimited
* New option to upload file(s) to asset(s) from the asset menus
* Adds a resize anchor to several Windows
* Adds support for manifest generation with m4a files
* Bug fixes and improvements


Version 3.41.0.0 (June 8, 2016) brings the following features and improvements :

* Support for **Apple FairPlay** content protection
  * See the [announcement](https://azure.microsoft.com/en-us/blog/azure-media-services-expands-multi-drm-offering/)
  * Note : you can run the Dynamic Encryption wizard several times if you want to setup multiple content protection schemes. For example : PlayReady/Widevine for DASH, PlayReady for Smooth and FairPlay for HLS. It's not possible to setup two different encryption schemes for the same protocol.
  * Note : FairPlay uses a key which is different from the CENC key. So it's not possible to have the same key id for the CENC and FairPlay keys.
* **Azure Media Video OCR** private preview. Supported languages includes Arabic, Chinese Simplified, Chinese Traditional, Czech, Danish, Dutch, English, Finnish, French, German, Greek, Hungarian, Italian, Japanese, Korean, Norwegian, Polish, Portuguese, Romanian, Russian, Serbian Cyrillic, Serbian Latin, Slovak, Spanish, Swedish, and Turkish.
   * See the [blog post](https://azure.microsoft.com/en-us/blog/ocr-on-azure-media-analytics/)
* **Azure Media Content Moderator** private preview
   * See the [blog post](https://azure.microsoft.com/en-us/blog/content-moderator-azure-media-analytics/)
* Other Media Analytics update
   * Minor UI update
   * New option to copy the subtitles generated by Indexer to the source asset (copy is done by AMSE after the completion of the job)
   * If the source asset contains a primary file which is not an audio/video file, AMSE proposes to change the primary file to an audio/video file in order to take it as a source by the processor.
* Support for custom AES key delivery URL when key is delivered by AMS
   * used by some customers in order to cache the key with a CDN
* Adds support for Ignore708Captions setting for live channels
* Critical performance improvement in the live tab when listing programs related to channel
* New features for transfers : transfers can be cancelled, and completed transfers can be cleared
* Fixed some issues with live archive copy. Better displays and manages the fragmented flag, and displays some warning when doing live archive copy
* Reorganization of the asset menu
* Updated the Media Services SDK to version 3.6.0
* Bug fixes and improvements


Version 3.40.0.0 (April 14, 2016) brings the following features and improvements :

* **Media Analytics Update**
  * See the [Media Analytics announcement](https://azure.microsoft.com/en-us/blog/introducing-azure-media-analytics/) for NAB2016 
  * **Azure Media Indexer 2 Preview**. Supported languages includes: English, Spanish, French, German, Italian, Chinese, Portuguese and Arabic
  * Minor update to Emotion Detection UI
  * Available also as public preview : face detection, video thumbnails, motion detection
* Support of **Editing Decision List (EDL)** in live stream/archive subclipping UI and Media Encoder Standard UI
  * this allows some basic editing of the source asset when doing encoding or live extraction
* Other bug fixes and improvements


Version 3.39.0.0 (April 11, 2016) brings the following features and improvements :

* **Multiple selection to update settings in batch mode**
  * Live channels, programs and streaming endpoints can be updated using multiple selection
* New setting to display **related/none/all programs** in the live tab
  * Useful to increase UI performance when working on channels 
* Added **Progressive Download protocol** for Dynamic Storage Decryption
* Azure Storage Import: new option to create one asset for each file selected
* Additional workflows for Premium Encoder (from tutorial)
* Update to manifest generation
* Fixes an issue with Media Encoder Standard overlay
* Fixes an issue with job and asset display when a parent asset has been deleted
* Other bug fixes and improvements


Version 3.38.0.0 (March 25, 2016) brings the following features and improvements :

* New **Overlay feature** now available for **Media Encoder Standard**
  * Single asset mode (image must be in the source asset)
* Update to media analytics (private preview)
* **Content protection minor updates**
  * Use of BaseLicenseAcquisitionUrl for Widevine and AES. The key ID not stored in the policy.
  * UI update to specify the authorization policy option name
  * Button to remove/delete an authorization policy option
* Option to display and edit the AlternateId property for assets
* Media Services SDK updated to 3.5.3
* Bug fixes and improvements


Version 3.37.0.0 (February 24, 2016) brings the following features and improvements :

* A way to change the video index and audio indexes and languages for an **existing encoding live channel (RTP)**
* Update to **Media Encoder Standard** dialog box
  * Update to thumbnails (percentage mode)
  * Support for PreserveResolutionAfterRotation flag
* Bug fixes and improvements


Version 3.36.0.0 (February 3, 2016) brings the following features and improvements :

* New **Live Archive Integrity check**. This feature checks the timestamps in the manifest and the segments in the blob storage for a live archive. It will detect timestamps in overlap or gap, and missing segments.
* Support for **disable auto de-interlacing** setting with Media Encoder Standard (MES)
* New feature to check and fix manifests which have a **wrong systemBitrate attribute**
* Channels, Programs and Streaming Endpoints can be sorted by clicking a column
* Update to support latest Azure Media Player update (PlayReady and Widevine tokens)
* A way to signal that the asset to be uploaded is already CENC/PlayReady encrypted (in Batch and Bulk upload modes)
* Fixes an issue with text format when copying a preset from Chrome to Explorer
* Fixes an issue as InstallShield was not shipping the right version of some libraries
* Better dialog box for selecting a folder
* Bug fixes and improvements


Version 3.35.0.0 (January 6, 2016) brings the following features and improvements :

* New **manifest generation** feature : if an asset contains one or several MP4 files without a manifest (.ISM), user can generate a manifest and review it. New button is located in asset info/files tab.
* **Bulk ingest** update :
  * Added support for **Signiant Flight**. New UI to specify the Signiant API Key and Server. The sigcli.exe command line is generated.
  * Added **AzCopy** command line generation 
* Encoding Reserved Units (Basic/Standard/Premium) renamed to Media Reserved Units (S1/S2/S3)
* Support for new **silent audio track generation** setting with Media Encoder Standard (MES)
* Added SAS locator information in the "Export to Excel" feature
* Live Encoding preset is displayed in the grid for live encoding channels
* Bug fixes and improvements


Version 3.34.0.0 (December 10, 2016) brings the following features and improvements :

* **Google Widevine license delivery** update :  packaging is enabled by default, and the JSON license template is visible and editable
* **Optimizations for large number of jobs running**. Code has been optimized to monitor only visible jobs and increase the job polling time when needed. This should dramatically improve performances for accounts with a large number of running jobs
* **Thumbnails generation** is now available in Media Encoder Standard UI. The thumbnail generation option in the menu has been moved from AME to MES processor
* **Date/time range filtering**. A date/time range can be specified for the assets, jobs, channels and programs queries.
* It's now possible to **change the encoding preset on an existing live channel**
* **Asset filters copy option**. When a program is cloned or an asset is duplicated, there is an option to copy the asset filters. (time start and end values are reset when doing program cloning)
* Icons update
* Bug fixes


Version 3.33.0.0 (November 19, 2015) brings the following features and improvements :

* **Google Widevine license delivery** support (public preview). See [this announcement](https://azure.microsoft.com/en-us/blog/announcing-google-widevine-license-delivery-services-public-preview-in-azure-media-services/). Feature will continue to improve in the coming releases
* **Dynamic encryption wizard update**. UI has been fully revisited.
* Content keys and token keys are displayed with a button, to increase security
* Possibility to select Microsoft Edge, Microsoft Internet Explorer or Google Chrome for asset playback
* More checks on asset sources for Indexer task
* Media Services SDK updated to 3.5.2
* Bug fixes


Version 3.32.0.0 (November 9, 2015) brings the following features and improvements :

* **New bulk ingest manifest feature** (in transfers tab). Useful if you want to upload assets with an another client application (like Aspera or Signiant). Upload is decoupled from Azure Media Services asset creation. Any application that copies files to Azure Storage can be used.
  * a new UI to select files or folder to upload, and to organize the destinations assets
  * support for local storage encryption before the upload
  * tested with Cloudberry Explorer
* Support for **new Media Processors** (private preview)
* Adds two **audio only profiles** for Media Encoder Standard (MES)
* Support for **Hyperlapse with a speed of 1** (stabilization)
* **Optimization for large number of assets or jobs**
  * This version has been tested on a media services account with 200 000 assets
  * Some client side queries are disabled with an account with more than 10 000 assets or 5 000 jobs
  * Better performances for jobs and assets delete processes
* More checks (channel and program names, channel properties, Streaming endpoints creation), file overwrite before download
* Bug fixes (channel name sorting, PlayReady relative date,  etc)


Version 3.31.0.0 (October 14, 2015) brings the following features and improvements :

* **New asset download to local** dialog box. The tool can create folders based on the asset name or asset Id and open them when download is completed.
* **Better performance to display assets details** in the grid (cache in memory)
* **Optimization for large number of assets or jobs**.  Search in assets/jobs/channels/programs are now done on all items, not the last 1000 items (queries have been rewritten to be executed by the back-end when possible). This version has been tested on a media services account with 11000 assets. New option to display programs for all channels.
* When playing-back a content, user can select **multiple filters and the no audio-only mode for HLS**, and see the preview of the URL. See the recent [blog post](https://azure.microsoft.com/en-us/blog/azure-media-services-release-dynamic-manifest-composition-remove-hls-audio-only-track-and-hls-i-frame-track-support/) regarding these new features.
* **New software update UI**. Explorer can auto update when a new release is published. Released notes are displayed.
* **Azure Storage settings** dialog box (to activate metrics, and change the default storage in order to allow byte range access for progressive download)
* Option to **Delete all visible assets, or to delete all visible jobs**
* More tasks details in job reports.
* JSON and XML syntax check in Premium Workflow, MES and in multi processors dialog box
* Task option in multiple processors, option to process all visible assets
* Price is displayed for MES jobs
* Live archiving uses now JSON presets
* New updated Media Services documentation (5 CHM files), link to released notes
* Requires .NET 4.6
* Bug fixes


Version 3.30.0.1 (September 18, 2015) brings the following features and improvements :

* **Support for Widevine Modular DRM header insertion** for DASH output with Common Encryption. There is now an option in the tool to add a Widevine header when adding a dynamic encryption policy. A license acquisition URL can be optionally specified (a third party service is needed to deliver the licenses, see this [blog post](https://azure.microsoft.com/en-us/blog/azure-media-services-adds-google-widevine-packaging-for-delivering-multi-drm-stream/)).
* Update to the way Azure Media Player is called for Widevine protected content (protection=widevine or protection=drm)
* New **asset file Edit feature** in asset info/asset files. For example, a manifest or a subtitle file can be directly edited on-line
* Added a button to pass **XML data to Premium Workflow Encoder processor** (to set transcodeSource or setRuntimeProperties data)
* Media Services SDK updated to 3.5.1
* Bug fixes


Version 3.29.0.0 (September 10, 2015) brings the following features and improvements :

* **Sub-clipping and live archive extraction is now available** in the tool
* **New dialog box** to select the streaming endpoint, filter and format before playing a content or coping the streaming URL to clipboard
* Update to **Live Encoding** (custom preset)
* Update to the **Media Encoder Standard** interface (new option to trim the source, update to the JSON settings for sub-clipping)
* Various improvements (note that default locator duration is now 10 years)
* Several critical bug fixes


Version 3.28.0.0 (September 2, 2015) brings the following features and improvements :

* Code updated to use **Media Services SDK v3.4.0.0** and **Visual Studio 2015**
* The new installer checks the requirement for .NET 4.5.2
* Moved all API calls from REST to .NET SDK for asset and global filters
* **New options to search** in Id or name of assets, asset files, locators, jobs, channels and programs
* Option to specify the manifest name for live programs
* **Dynamic encryption updates**: an external AES key server can be configured, and it's possible to use the same key (key id) for several assets (AES or PlayReady)
* Added support for **Premium Live Encoding** (in private preview)
* Fixes to subclipping (currently in private preview)
* UI updates and bug fixes


Version 3.27.0.0 (July 17, 2015) brings the following features and improvements :

* **Asset information export to Excel**. You can now export assets data to Excel (asset name, ID, streaming URL, expiration) then use Excel to analyse the data (with a pivot table). Please give us some feedback!
* Update to the logging window (timestamps and more details)
* Switched to JSON presets for **Media Encoder Standard** [which is now GA](http://azure.microsoft.com/blog/2015/07/16/announcing-the-general-availability-of-media-encoder-standard/).
* Subclipping support (currently in private preview)
* UI updates
* Several bug fixes