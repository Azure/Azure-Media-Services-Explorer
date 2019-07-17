Version 5.0.6.0 (July 17, 2019) brings the following features and improvements :

* Custom block size to leverage [Azure Storage high-throughput](https://azure.microsoft.com/en-us/blog/high-throughput-with-azure-blob-storage/) for video file upload
* Support for DRM token replay prevention (preview)
* Job creation UI update
* Application upgraded to .Net Framework 4.7
* AMSE string in REST request user-agent
* Bug fixes


Version 5.0.5.0 (July 4, 2019) brings the following features and improvements :

* Support for time trimming when submitting a job from a transform (for MES presets)
* Redesign of the job submit UI, transform info UI, job info UI, Transforms/Jobs panel UI
* Fixes an issue when filtering assets based on assetid
* Adds support for filtering assets using a beginwith method on asset name
* It's now possible to select the locator in the UI when playing back an asset
* Displays the streaming URLs when a live output has been created and no data has yet arrived in it
* Bug fixes


Version 5.0.4.1 (June 12, 2019) brings the following features and improvements :

* Support for [Premium Live encoder](https://azure.microsoft.com/en-us/updates/premium-live-encoder-now-supports-streaming-at-1080p-and-30-frames-per-second/) when creating a live event. This encoder supports up to 1080p and 30 frames per second
  * Live event creation UI updated with default preset info
* Redesign of locator creation UI to support [associated filters](https://docs.microsoft.com/bs-latn-ba/azure/media-services/latest/filters-concept#associating-filters-with-streaming-locator)
* Number of filters and publication expiration now displayed in asset list
* Media SDK update to latest version
* Bug fixes


Version 5.0.3.0 (June 3, 2019) brings the following features and improvements :

* Support for face detector preset
* Major update on content protection for AMS v3 :
  * More info for content protection in asset info (streaming policy, content keys, content key policy)
  * When creating a locator: support for all predefined streaming policies (except FairPlay), with setup of options for PlayReady and Widevine for CENC policy
* Bug fixes : interactive token renewal, filters are back in streaming endpoint selection, download only locator display fix
* Solve an issue with High DPI screen and Live tab


Version 5.0.2.0 (May 13, 2019) brings the following features and improvements :

* Code update to support High DPI screen
* Support for SAS Urls creation, and asset import from SAS Container path
* Update to latest ADAL SDK and other librairies
* Bug fixes


Version 5.0.1.0 (April 19, 2019) brings the following features and improvements :

* Support for [experimental preset for content-aware encoding](https://docs.microsoft.com/en-us/azure/media-services/latest/cae-experimental)
* Content keys are now displayed in asset info UI
* Some deadlocks were removed, better management of authentication token renewal
* AMS v2 SDK has been removed from code and binary, AMS v3 and Storage SDKs updated to latest version
* Bug fixes


Version 5.0.0.21 Preview (March 27, 2019) brings the following features and improvements :

* Critical bug fixes and performance optimizations


Version 5.0.0.17 Preview (March 18, 2019) brings the following features and improvements :

* Support for multi tenant browsing when adding an account
* Support for Live Output outputSnapTime
* Started implementation of content protection setup when publishing an asset - clear key mode
* Athentication token is now refreshed when expired - this was causing crashes
* Better error management for some live entities operations
* Bug fixes


Version 5.0.0.16 Preview (February 4, 2019) brings the following features and improvements :

* Support for Azure national clouds authentication
* Corrected file and asset size calculation (now 1 GB = 1000 MB)
* Major Bug fixes


Version 5.0.0.12 Preview (January 11, 2019) brings the following features and improvements :

* Displays capacity used by storage account, and support for storage settings
* Major Bug fixes


Version 5.0.0.11 Preview (January 8, 2019) brings the following features and improvements :

* First public preview of AMSE for AMS v3