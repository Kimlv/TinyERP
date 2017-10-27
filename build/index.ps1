$msbuildPath="C:\Windows\Microsoft.NET\Framework64\v3.5\MSBuild.exe"
$root=Resolve-Path ../
$outputDir="$root\deployment"
Class BuildAgent{
	[string]$FileToBuild
	[string]$OutputFolder
	[bool]$ClearDest
	
	BuildAgent([string]$fileToBuild, [string]$output, [bool]$clearDest){
		$this.FileToBuild=$fileToBuild
		$this.OutputFolder=$output
		$this.ClearDest=$clearDest
	}
	[bool] IsValidRequest([string] $filePath){
		return [FileHelper]::Exist($filePath)
	}
	OnBeforeBuild(){
		if (($this.ClearDest -eq $TRUE) -and [FileHelper]::ExistFolder($this.OutputFolder)){
			Write-Host "Deleting '$($this.OutputFolder)' ..."
			Remove-Item $this.OutputFolder -force -recurse
			Write-Host "'$($this.OutputFolder)' folder was deleted"
		}
		if([FileHelper]::ExistFolder($this.OutputFolder) -ne $TRUE){
			Write-Host "Creating '$($this.OutputFolder)' folder ..."
			New-Item -ItemType Directory -Force -Path $this.OutputFolder
			Write-Host "'$($this.OutputFolder)' folder was created ..."
		}
	}
	build(){
		Write-Host "Start building '$($this.FileToBuild)' output to '$($this.OutputFolder)', clearDest: $($this.ClearDest)"
		if($this.IsValidRequest($this.FileToBuild) -ne  $TRUE){
			Write-Output "'$($this.FileToBuild)' was not existed. Please specify project to build"
			return
		}
		$this.OnBeforeBuild()
		Write-Host "Building '$($this.FileToBuild)' was completed."
	}
}

Class FileHelper{
	[bool]static Exist([string]$filePath){
		return Test-Path -Path $filePath
	}
	[bool]static ExistFolder($folder){
		return Test-Path -Path $folder
	}
}
function buildWebApi([string]$sln, [string]$output, [bool]$clearDest){
	[BuildAgent] $buildAgent=[BuildAgent]::new($sln, $output, $clearDest)
	$buildAgent.build();
}

$webApiSln="D:\project\tfs_tinyerp\api\Application.sln"
$outputFolder="$outputDir\webapi"
$cleanFolderBeforeBuild=$FALSE

buildWebApi $webApiSln $outputFolder $cleanFolderBeforeBuild