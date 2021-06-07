
$ErrorActionPreference = 'Stop';
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://amsexplorer.azureedge.net/release/AMSExplorerSetup_v5.4.2.1.msi'

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  fileType      = 'MSI'
  url           = $url

  softwareName  = 'Azure Media Services Explorer'

  checksum      = '55C8C3998F3C5BAA6629027694B1EE66F56BF777BE14B18C2D31D1CE484EF2A3'
  checksumType  = 'sha256'
  checksum64    = ''
  checksumType64= 'sha256'

  silentArgs    = "/quiet"
  validExitCodes= @(0, 3010, 1641)
}

Install-ChocolateyPackage @packageArgs










    








