Version 3.34.0.0 brings the following features and improvements :

* **Google Widevine license delivery** update :  packaging is enabled by default, and the JSON license template is visible and editable
* **Optimizations for large number of jobs running**. Code has been optimized to monitor only visible jobs and increase the job polling time when needed. This should dramatically improve performances for accounts with a large number of running jobs
* **Thumbnails generation** is now available in Media Encoder Standard UI. The thumbnail generation option in the menu has been moved from AME to MES processor
* **Date/time range filtering**. A date/time range can be specified for the assets, jobs, channels and programs queries.
* It's now possible to **change the encoding preset on an existing live channel**
* **Asset filters copy option**. When a program is cloned or an asset is duplicated, there is an option to copy the asset filters. (time start and end values are reset when doing program cloning)
* Icons update
* Bug fixes