Version 5.2.1.0 (October 26, 2020) brings the following features and improvements:

* Blob upload, copy and download processes now use the [Azure Data Movement Library](https://github.com/Azure/azure-storage-net-data-movement) for better performance
  * Related settings can be set in a new tab in Options/Options : number of parallel operations, MD5 check on/off and blob block size
  * Progress bar is correctly updated for upload and download
* New feature added : export assets information to Excel or CSV
* Nugget packages and documentation update
* No more exe for the installer, but a signed MSI file
