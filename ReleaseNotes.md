Version 3.47.0.0 (January 10, 2017) brings the following features and improvements :

* Support for the new auto scaling Standard Streaming Endpoint and new enhanced CDN configuration. Details are available in this [blog post](https://azure.microsoft.com/en-us/blog/standardstreamingendpoint/)
   * Provides a way to migrate a Classic Streaming Endpoint to Standard Streaming Endpoint.
   * Use the new names : Standard, Classic and Premium. 
   * Support for the new CDN enhanced configuration : CDN type (Verizon Standard, Verizon Premium, Akamai Standard) and profile.
* Support for pre-fragmented output when using Azure Media encoders. 
   * The output from encoding can be fragmented for better streaming performance.
   * Can be configured in the task/job option.
* Azure Media Analytics update. Support for private preview of Video Annotation.
* Update related to the [pricing model for encoding](https://azure.microsoft.com/en-us/blog/encoding-with-media-services-everything-you-need-to-know-about-new-pricing-model/)
   * Media Encoder Standard dialog box : the output minute multiplier is calculated and displayed, and the price per input minute is estimated.
* Source code has been rearchitectured for multilanguage support (some strings have been extracted to resources). This work is not complete yet.
* Media Services SDK updated to version 3.8.0.5.
* Bug fixes.