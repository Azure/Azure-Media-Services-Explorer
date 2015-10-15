$ErrorActionPreference = 'Stop';
$packageName = 'amsexplorer'
$toolsDir = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url = 'https://amsexplorer.blob.core.windows.net/release/AMSExplorerSetup_v3.28.0.0.exe'

$packageArgs = @{
  packageName   = $packageName
  unzipLocation = $toolsDir
  fileType      = 'EXE'
  url           = $url
  validExitCodes= @(0, 3010, 1641)
  silentArgs    = "/v/qn"
  registryUninstallerKey = '{153DE731-881C-48CD-9A31-D52962B1F267}'
}

Install-ChocolateyPackage @packageArgs