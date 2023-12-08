---
page_type: sample
languages:
- csharp
products:
- azure
- azure-media-services
---

# Azure Media Services Explorer (for AMS v3)

Azure Media Services Explorer (AMSE) is a .NET 7.0 (C#) application for Windows that does upload, download, encode and stream VOD and live content with [Azure Media Services v3](https://azure.microsoft.com/services/media-services/).

Important : Azure Media Services will be [retired on June 30th, 2024](https://learn.microsoft.com/azure/media-services/latest/azure-media-services-retirement).

This tool can help you migrate to MediaKind [MK/IO](https://io.mediakind.com).

## Installing the tool with Winget

Run the following commands in a command prompt to install the prerequisites and AMSE :

```console
winget install Microsoft.DotNet.DesktopRuntime.7
winget install Microsoft.EdgeWebView2Runtime
winget install Microsoft.AzureMediaServicesExplorer
```

## Manual installation

**The latest binary for Windows (with a MSI installer) is available in the [releases section](https://github.com/Azure/Azure-Media-Services-Explorer/releases)**.

This application requires the installation of

- [.NET **Desktop** Runtime 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Microsoft Edge Webview2 runtime](https://developer.microsoft.com/microsoft-edge/webview2/)

## Prerequisites to compile the solution from source

This solution has been developed using [Visual Studio 2022](https://visualstudio.microsoft.com/vs/). It contains two projects: "AMSExplorer", the main application, and "Setup", a project that creates a Setup executable (EXE/MSI).

You need to install the [Microsoft Visual Studio Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2022InstallerProjects) in order to open and build the Setup project in Visual Studio.

This solution requires [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) to compile.

## MediaKind MK/IO

This version of the Azure Media Services Explorer supports creating assets in MediaKind MK/IO from existing AMS assets.

You must have an active MediaKind MK/IO subscription and access to the MediaKind I/O portal:
[https://io.mediakind.com](https://io.mediakind.com)

To connect with MK I/O you will need:

- Subscription Name
- MK/IO Token

The subscription name is also called the resource name and is available in the portal.

An MK/IO token can be obtained by following the directions contained in this [article](https://support.mediakind.com/portal/en/kb/articles/how-to-use-mkio-apis-step-by-step).

AMSE relies on the MK/IO SDK available [here](https://www.nuget.org/packages/MK.IO).

## Notes

You can force the English or Japanese language by using /language:en-US or /language:ja-JA as a parameter of the AMSExplorer.exe executable.

AMSE uses Application Insights for Telemetry. This feature can be turned off in the Options of AMSE.

## Contacts

Contact: <amse@microsoft.com>

Open a bug [here](https://github.com/Azure/Azure-Media-Services-Explorer/issues/new).

![Screen capture](https://user-images.githubusercontent.com/8104205/116678834-17935c80-a9aa-11eb-9419-6c79de82b8ca.png)

## Contributing

This project welcomes contributions and suggestions. Please see our [contributing guide](CONTRIBUTING.md).

This project has adopted the [Microsoft Open Source Code of Conduct](CODE_OF_CONDUCT.md).

## Data Collection

The software may collect information about you and your use of the software and send it to Microsoft. Microsoft may use this information to provide services and improve our products and services. You may turn off the telemetry as described in the repository. There are also some features in the software that may enable you and Microsoft to collect data from users of your applications. If you use these features, you must comply with applicable law, including providing appropriate notices to users of your applications together with a copy of Microsoft's privacy statement. Our privacy statement is located at <https://go.microsoft.com/fwlink/?LinkID=824704>. You can learn more about data collection and use in the help documentation and our privacy statement. Your use of the software operates as your consent to these practices.

## Security

Please read our [security policy](SECURITY.md) to learn how to report security issues.
