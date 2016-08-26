Version 3.43.0.0 (August 26, 2016) brings the following features and improvements :

* Support for **Azure Media Telemetry** (Private Preview - activated on selected accounts)
  * initial configuration, update and deletion (management menu)
  * loading and displaying telemetry data for Streaming Endpoints and Channels
  * Media Services Account ID field added to the logon Windows
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