# Version 5.4.0.1 Preview (April 30, 2021) brings the following features and improvements

* Project has been migrated to [Window Forms .Net v5](https://devblogs.microsoft.com/dotnet/whats-new-in-windows-forms-runtime-in-net-5-0/)
  * Code is now based on .Net v5. As this is a huge change and Visual Studio Preview was used, this AMSE version is in preview. Some features may be broken. Improvements should be a better management of high DPI display and better performances.
  * Solution has been developed with [Visual Studio 2019 16.10.0 Preview 2.1](https://visualstudio.microsoft.com/vs/preview/) 
  * Setup project is now using the [Microsoft Visual Studio Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects). InstallShield has been dropped. 64 bits version of AMSE tool is now shipped.
  * Authentication has been moved to MSAL from ADAL (as this library has been deprecated and does not work with .Net 5). Authentication is done launching the default browser (this may change in the future)
* Support for Multiple Input File stitching
* Added [HEVC encoding preset](https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes#hevc-encoding-support-in-standard-encoder) for Media Encoder Standard UI
* New [language support for AudioAnalyzer preset](https://docs.microsoft.com/en-us/azure/media-services/latest/release-notes#new-language-support-added-to-the-audioanalyzer-preset)
* [Japanese translation fixes](https://github.com/Azure/Azure-Media-Services-Explorer/pull/142) from @m-otoguro 
* Nugget packages and documentation update
* Bugs fixes
