Azure-Media-Services-Explorer
=============================

Azure Media Explorer is a Winforms/C# application for Windows that does upload, download, encode, package, and stream assets with Azure Media Services.

See a full description on http://azure.microsoft.com/blog/2014/10/08/managing-media-workflows-with-the-new-azure-media-services-explorer-tool

The latest binary (with an installer) is available in the release section: https://github.com/Azure/Azure-Media-Services-Explorer/releases and also on Chocolatey: https://chocolatey.org/packages/amsexplorer

This solution has been developped with Visual Studio 2015. It contains two projects: "AMSExplorer", the main application, and "SetupAMSExplorer", an InstallShield project that creates a Setup executable (EXE/MSI).

In order to compile this setup project, you must install InstallShield Limited Edition for VisualStudio. To do this:
- Launch Visual Studio 2015
- Select New project
- Select Installed / Other Project Types / InstallShield Limited Edition Project
- This should trigger the process to install and activate InstallShield.

Contact: amse@microsoft.com

![Screen capture](https://cloud.githubusercontent.com/assets/8104205/10526542/d696c884-738b-11e5-80d1-669eb488172f.png)
