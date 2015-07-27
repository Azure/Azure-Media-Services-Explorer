$ErrorActionPreference = 'Stop';
$packageName = 'amsexplorer'
$toolsDir = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url = 'https://amsexplorer.blob.core.windows.net/release/AMSExplorerSetup_v3.27.0.0.exe'

$packageArgs = @{
  packageName   = $packageName
  unzipLocation = $toolsDir
  fileType      = 'EXE'
  url           = $url
  validExitCodes= @(0, 3010, 1641)
  silentArgs    = "/v/qn"
  registryUninstallerKey = '{4118FAF1-F307-42DF-9DF3-707E94331841}'
}

Install-ChocolateyPackage @packageArgs