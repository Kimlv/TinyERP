
using module ".\common\enum.psm1"
using module ".\common\logger\defaultLogger.psm1"
using module ".\common\helpers\pathHelper.psm1"
using module ".\common\actionResult.psm1"
using module ".\common\helpers\fileHelper.psm1"
using module ".\common\ftpClient.psm1"
using module ".\agents\buildAgent.psm1"
using module ".\config.psm1"

$Global:msbuildPath= [Configuration]::MSBuildPath
$root= Resolve-Path ../


$Global:logger = [DefaultLogger]::new()
$Global:FolderSeparator = [Configuration]::FolderSeparator
$Global:FtpFolderSeparator = [Configuration]::FtpFolderSeparator
$Global:FtpTimeout = [Configuration]::FtpTimeout

# path to solution folder
[string] $solutionDir=$root
[string] $outputDir = "$($solutionDir)\deployment"
# path to azure publish profile
[string] $azureSettingFile="d:\temp\tinyerp.PublishSettings"
# path to csproj file to build
[string] $fileToBuild="$($solutionDir)\api\Application.Api\App.Api.csproj"
# this is where compiled files were stored
[string] $projectOutputFolder="$($outputDir)\webapi"
# set TRUE to clear output folder in each build
[string] $cleanOutputFolderBeforeBuild=$TRUE

[BuildAgent] $buildAgent=[BuildAgent]::new($fileToBuild, $solutionDir, $projectOutputFolder, $cleanOutputFolderBeforeBuild, $azureSettingFile)
$buildAgent.Build()