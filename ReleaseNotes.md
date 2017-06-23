Version 4.0.0.0 (June 22, 2017) brings the following features and improvements :

* Support for Azure Active Directory (AAD) authentication for Media Services
  *  Two modes are supported: Interactive authentication (recommended), and Service Principal
  *  Support for Azure Global, Azure in China, Azure in Germany, US Government and custom settings
  *  See the [blog announcement](https://azure.microsoft.com/en-us/blog/azure-media-service-aad-auth-and-acs-deprecation/) for more information
* Adds support for British English and Mexican Spanish in Indexer v2 
* More reliable upload and download. The following settings are exposed: parallel transfer thread count and number of concurrent transfers (in Options). Default is one transfer process in the transfer tab.
* Media Services SDK updated to 4.0.0.4, with ADAL support. Storage SDK updated to 8.1.4.0.
* Fixes several bugs