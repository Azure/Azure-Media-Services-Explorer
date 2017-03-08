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