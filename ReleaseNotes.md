Version 5.0.15.0 (January 10, 2020) brings the following features and improvements:

* Updated with the new v2.0.4 Azure Media SDK
    * Support for Job start and end times display. Duration of jobs are also displayed.
    * Support for the new FairPlay dual expiry settings (rentals)
    * New ContentAwareEncoding MES preset (GA)
* Asset copy feature has been introduced. It can be used to copy one or several assets to the same or a different AMS account. It supports VOD and live archive assets. The destination storage account attached to the account can also be selected.
* Optimization of REST calls for the background assets listing
* When submitting a job, the output asset name can now be customized
* A short GUID of 10 characters is now used as uniqueness
* Displays an example of Az Cli syntax at start
* Bug fixes