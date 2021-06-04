# Version 5.4.1.1 (June 4, 2021) brings the following features and improvements

* Project has been migrated to [Window Forms .Net v5](https://devblogs.microsoft.com/dotnet/whats-new-in-windows-forms-runtime-in-net-5-0/)
  * Code is now based on .Net v5. Improvements should be a better management of high DPI display and better performances. AMSE needs the [.NET **Desktop** Runtime](https://dotnet.microsoft.com/download/dotnet/5.0) 5.0.6 or later
  * Authentication has been moved from ADAL to MSAL (as the ADAL library has been deprecated and does not work with .Net 5).
* Other platform changes
  * Setup project is now using the [Microsoft Visual Studio Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects). InstallShield has been dropped. 64 bits version of AMSE tool is now shipped
  * Export to Excel feature has been redeveloped using the [Open XML SDK](https://github.com/) for no more dependancy to Excel software to generate the xlsx file
  * Webview moved to [Webview2 runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/#download-section). Component installation is proposed when the app is launched
* Widevine DRM schema update for correct serialization
* Support for Multiple Input File stitching
* Added [HEVC encoding preset](https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes#hevc-encoding-support-in-standard-encoder) for Media Encoder Standard UI
* New [language support for AudioAnalyzer preset](https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes#new-language-support-added-to-the-audioanalyzer-preset)
* [Japanese translation fixes](https://github.com/Azure/Azure-Media-Services-Explorer/pull/142) from @m-otoguro
* Telemetry added to the tool through the use of Application Insights to report exceptions and anonymized usage statistics
* Media unit management feature using REST v2 has been removed
* Nugget packages and documentation update
* Code refactoring
* Bugs fixes

---
**Note on the update and the media services account list**

As the .Net runtime changed, you will loose the list of your favorites media services accounts in the connection window. To avoid this, export the list using the "Export" button before the upgrade, and reimport the list after the upgrade.

---
