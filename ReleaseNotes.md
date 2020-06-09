Version 5.2.0.0 (June 8, 2020) brings the following features and improvements:

* Application moved to .Net Framework v4.8. You may loose the list of AMS accounts in the login window after the upgrade. To avoid this, export your entries from v5.0.x.x and reimport them in v5.2.0.0.
* Update to [Live Transcription](https://docs.microsoft.com/en-us/azure/media-services/latest/live-transcription) with support of [additional regions](https://docs.microsoft.com/en-us/azure/media-services/latest/azure-clouds-regions#feature-availability-in-preview) and additional languages (more details later this month)
* Generation of test token now uses the selected DRM key
* New icon for AMSE v5
* Removed the deprecated storage analytics (not in the new Storage SDK)
* Bug fixes (FairPlay rental duration, Live archive duration, ...)
* Code optimizations, SDKs update