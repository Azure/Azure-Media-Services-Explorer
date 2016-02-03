Version 3.36.0.0 brings the following features and improvements :

* New **Live Archive Integrity check**. This feature checks the timestamps in the manifest and the segments in the blob storage for a live archive. It will detect timestamps in overlap or gap, and missing segments.
* Support for **disable auto interlacing** setting with Media Encoder Standard (MES)
* New feature to check and fix manifests which have a **wrong systemBitrate attribute**
* Channels, Programs and Streaming Endpoints can be sorted by clicking a column
* Update to support latest Azure Media Player update (PlayReady and Widevine tokens)
* A way to signal that the asset to be uploaded is already CENC/PlayReady encrypted (in Batch and Bulk upload modes)
* Fixes an issue with text format when copying a preset from Chrome to Explorer
* Fixes an issue as InstallShield was not shipping the right version of some libraries
* Better dialog box for selecting a folder
* Bug fixes and improvements