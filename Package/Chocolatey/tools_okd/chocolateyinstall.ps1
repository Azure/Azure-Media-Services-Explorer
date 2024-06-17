
$ErrorActionPreference = 'Stop';
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/Azure/Azure-Media-Services-Explorer/releases/download/v5.8.3.0/AMSExplorerSetup_v5.8.3.msi'

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  fileType      = 'MSI'
  url           = $url

  softwareName  = 'Azure Media Services Explorer'

  checksum      = '0B87C516F61DCD0A46B815F8CA00EF6DB8C25115BD5D096B7E180C9D482EC4FE'
  checksumType  = 'sha256'
  checksum64    = ''
  checksumType64= 'sha256'

  silentArgs    = "/quiet"
  validExitCodes= @(0, 3010, 1641)
}

Install-ChocolateyPackage @packageArgs










    








