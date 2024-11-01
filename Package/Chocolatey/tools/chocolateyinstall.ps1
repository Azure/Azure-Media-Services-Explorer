﻿
$ErrorActionPreference = 'Stop';
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/Azure/Azure-Media-Services-Explorer/releases/download/v5.8.3.0/AMSExplorerSetup_v5.8.3.msi'

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  fileType      = 'MSI'
  url           = $url

  softwareName  = 'Azure Media Services Explorer'

  checksum      = '9C0C5AE2D7C2F070C4DAE65259BD1624C16EFD934BE51293B6B50EE220F29EE5'
  checksumType  = 'sha256'
  checksum64    = ''
  checksumType64= 'sha256'

  silentArgs    = "/quiet"
  validExitCodes= @(0, 3010, 1641)
}

Install-ChocolateyPackage @packageArgs










    








