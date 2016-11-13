Azure-Media-Services-Explorer
=============================
Azure Media Services Explorer 多言語化 (日本語化) プロジェクト
日本語ローカライズ版の Azure Media Services Explorer ツールを試験的にリリースします。
2016年11月現在、一部のUIのみ日本語化を行っています。
日本語化の作業のご協力、あるいは、日本語化メッセージ内容のフィードバックを頂ける方は、下記の連絡先までご連絡ください。

連絡先: shigeyf@microsoft.com


=============================
[Original release notes]

Azure Media Services Explorer (AMSE) is a Winforms/C# application for Windows that does upload, download, encode and stream VOD and live content with [Azure Media Services](https://azure.microsoft.com/en-us/services/media-services/).

See a full description on http://azure.microsoft.com/blog/2014/10/08/managing-media-workflows-with-the-new-azure-media-services-explorer-tool

**The latest binary for Windows (with a EXE installer) is available in the [releases section](https://github.com/Azure/Azure-Media-Services-Explorer/releases)**.

There are some nice [Azure Media Services Step-by-Step tutorials](https://docs.com/fukushima-shigeyuki/3439/english-azure-media-services-step-by-step-series) using Azure Media Services Explorer.

This solution has been developped with Visual Studio 2015. It contains two projects: "AMSExplorer", the main application, and "SetupAMSExplorer", an InstallShield project that creates a Setup executable (EXE/MSI).

In order to compile this setup project, you must install InstallShield Limited Edition for VisualStudio. To do this:
- Launch Visual Studio 2015
- Select New project
- Select Installed / Other Project Types / InstallShield Limited Edition Project
- This should trigger the process to install and activate InstallShield.

Contact: amse@microsoft.com

Open a bug [here](https://github.com/Azure/Azure-Media-Services-Explorer/issues/new).

![Screen capture](https://cloud.githubusercontent.com/assets/8104205/18006831/e6808a8c-6ba1-11e6-80e9-7d8e4a8b1a08.png)

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.