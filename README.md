---
page_type: sample
languages:
- csharp
products:
- azure
- azure-media-services
---

# Azure Media Services Explorer (for AMS v3)

Azure Media Services Explorer (AMSE) is a .Net 5 (C#) application for Windows that does upload, download, encode and stream VOD and live content with [Azure Media Services v3](https://azure.microsoft.com/en-us/services/media-services/).

See a full description [here](http://azure.microsoft.com/blog/2014/10/08/managing-media-workflows-with-the-new-azure-media-services-explorer-tool).

**The latest binary for Windows (with a EXE installer) is available in the [releases section](https://github.com/Azure/Azure-Media-Services-Explorer/releases)**. Please note that AMSE v5 is for AMS v3 API (main branch), and AMSE v4 is for AMS v2 (AMSv2 branch).

This solution has been developped with [Visual Studio 2019 16.10.0 Preview 2.1](https://visualstudio.microsoft.com/vs/preview/). It contains two projects: "AMSExplorer", the main application, and "Setup", a project that creates a Setup executable (EXE/MSI). Setup project is using the [Microsoft Visual Studio Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects).
This solution requires [.NET 5 Desktop Runtime to run or .NET 5 SDK to compile](https://dotnet.microsoft.com/download/dotnet/5.0).

Contact: amse@microsoft.com

Open a bug [here](https://github.com/Azure/Azure-Media-Services-Explorer/issues/new).

![Screen capture](https://user-images.githubusercontent.com/8104205/116678834-17935c80-a9aa-11eb-9419-6c79de82b8ca.png)

## Contributing

This project welcomes contributions and suggestions. Please see our [contributing guide](CONTRIBUTING.md).

This project has adopted the [Microsoft Open Source Code of Conduct](CODE_OF_CONDUCT.md).
