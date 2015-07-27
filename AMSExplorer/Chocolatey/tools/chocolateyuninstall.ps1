$ErrorActionPreference = 'Stop';

$packageName = 'amsexplorer'
$registryUninstallerKeyName = '{4118FAF1-F307-42DF-9DF3-707E94331841}'
$msiProductCodeGuid = '{4118FAF1-F307-42DF-9DF3-707E94331841}'
$shouldUninstall = $true

$local_key     = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\$registryUninstallerKeyName"
$local_key6432   = "HKCU:\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\$registryUninstallerKeyName" 
$machine_key   = "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$registryUninstallerKeyName"
$machine_key6432 = "HKLM:\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\$registryUninstallerKeyName"

$file = @($local_key, $local_key6432, $machine_key, $machine_key6432) `
    | ?{ Test-Path $_ } `
    | Get-ItemProperty `
    | Select-Object -ExpandProperty UninstallString

if ($file -eq $null -or $file -eq '') {
    Write-Host "$packageName has already been uninstalled by other means."
    $shouldUninstall = $false
}

$installerType = 'MSI' 
$silentArgs = "$msiProductCodeGuid /qn /norestart"
$validExitCodes = @(0, 3010, 1605, 1614, 1641)
$file = ''

if ($shouldUninstall) {
 Uninstall-ChocolateyPackage -PackageName $packageName -FileType $installerType -SilentArgs $silentArgs -validExitCodes $validExitCodes -File $file
}