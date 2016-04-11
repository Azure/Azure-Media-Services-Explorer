Version 3.39.0.0 brings the following features and improvements :

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

Version 3.38.0.0 brings the following features and improvements :

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

Version 3.37.0.0 brings the following features and improvements :

* A way to change the video index and audio indexes and languages for an **existing encoding live channel (RTP)**
* Update to **Media Encoder Standard** dialog box
  * Update to thumbnails (percentage mode)
  * Support for PreserveResolutionAfterRotation flag
* Bug fixes and improvements

Version 3.36.0.0 brings the following features and improvements :

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

Version 3.35.0.0 brings the following features and improvements :

* New **manifest generation** feature : if an asset contains one or several MP4 files without a manifest (.ISM), user can generate a manifest and review it. New button is located in asset info/files tab.
* **Bulk ingest** update :
  * Added support for **Signiant Flight**. New UI to specify the Signiant API Key and Server. The sigcli.exe command line is generated.
  * Added **AzCopy** command line generation 
* Encoding Reserved Units (Basic/Standard/Premium) renamed to Media Reserved Units (S1/S2/S3)
* Support for new **silent audio track generation** setting with Media Encoder Standard (MES)
* Added SAS locator information in the "Export to Excel" feature
* Live Encoding preset is displayed in the grid for live encoding channels
* Bug fixes and improvements

Version 3.34.0.0 brings the following features and improvements :

* **Google Widevine license delivery** update :  packaging is enabled by default, and the JSON license template is visible and editable
* **Optimizations for large number of jobs running**. Code has been optimized to monitor only visible jobs and increase the job polling time when needed. This should dramatically improve performances for accounts with a large number of running jobs
* **Thumbnails generation** is now available in Media Encoder Standard UI. The thumbnail generation option in the menu has been moved from AME to MES processor
* **Date/time range filtering**. A date/time range can be specified for the assets, jobs, channels and programs queries.
* It's now possible to **change the encoding preset on an existing live channel**
* **Asset filters copy option**. When a program is cloned or an asset is duplicated, there is an option to copy the asset filters. (time start and end values are reset when doing program cloning)
* Icons update
* Bug fixes

Version 3.33.0.0 brings the following features and improvements :

* **Google Widevine license delivery** support (public preview). See [this announcement](https://azure.microsoft.com/en-us/blog/announcing-google-widevine-license-delivery-services-public-preview-in-azure-media-services/). Feature will continue to improve in the coming releases
* **Dynamic encryption wizard update**. UI has been fully revisited.
* Content keys and token keys are displayed with a button, to increase security
* Possibility to select Microsoft Edge, Microsoft Internet Explorer or Google Chrome for asset playback
* More checks on asset sources for Indexer task
* Media Services SDK updated to 3.5.2
* Bug fixes

Version 3.32.0.0 brings the following features and improvements :

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

Version 3.31.0.0 brings the following features and improvements :

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

Version 3.30.0.1 brings the following features and improvements :

* **Support for Widevine Modular DRM header insertion** for DASH output with Common Encryption. There is now an option in the tool to add a Widevine header when adding a dynamic encryption policy. A license acquisition URL can be optionally specified (a third party service is needed to deliver the licenses, see this [blog post](https://azure.microsoft.com/en-us/blog/azure-media-services-adds-google-widevine-packaging-for-delivering-multi-drm-stream/)).
* Update to the way Azure Media Player is called for Widevine protected content (protection=widevine or protection=drm)
* New **asset file Edit feature** in asset info/asset files. For example, a manifest or a subtitle file can be directly edited on-line
* Added a button to pass **XML data to Premium Workflow Encoder processor** (to set transcodeSource or setRuntimeProperties data)
* Media Services SDK updated to 3.5.1
* Bug fixes
