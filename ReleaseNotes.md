Version 3.47.0.0 (January 11, 2017) brings the following features and improvements :

* Support for the new auto scaling Standard Streaming Endpoint and new enhanced CDN configuration. See the [announcement here](https://azure.microsoft.com/en-us/blog/standardstreamingendpoint/)
   * Provides an option to migrate a streaming endpoint from Classic to Standard.
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