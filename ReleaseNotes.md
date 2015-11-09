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