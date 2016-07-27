Version 3.42.0.0 brings the following features and improvements :

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