using module "..\common\enum.psm1"
using module "..\common\logger\defaultLogger.psm1"
using module "..\common\helpers\pathHelper.psm1"
using module "..\common\actionResult.psm1"
using module "..\common\helpers\fileHelper.psm1"
using module "..\common\ftpClient.psm1"
using module "..\common\ftpInformation.psm1"
Class BuildAgent{
	[string]$FileToBuild
	[string]$OutputFolder
	[bool]$ClearDest
	[string] $SolutionDir
	[string] $AzurePublishSettingFile

	BuildAgent([string]$fileToBuild, [string] $solutionDir, [string]$output, [bool]$clearDest, [string] $azureSettingFile){
		$this.FileToBuild = $fileToBuild
		$this.OutputFolder = $output
		$this.ClearDest = $clearDest
		$this.SolutionDir = $solutionDir
		$this.AzurePublishSettingFile = $azureSettingFile
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
	Building(){
		$this.BuildProject()

		[string] $projectDir=[System.IO.Path]::GetDirectoryName($this.FileToBuild)
		[string] $copyFrom = [System.String]::Format("{0}\obj\Release\Package\PackageTmp\*", $projectDir)
		[FileHelper]::CopyFolder($copyFrom, $this.OutputFolder)
	}
	BuildProject(){
		$GLobal:logger.Write("Starting building '$($this.FileToBuild)' ...")
		$cmd = "$($Global:msbuildPath) $($this.FileToBuild) /p:SolutionDir=$($this.SolutionDir) /p:DeployOnBuild=true /t:Package /p:PublishProfile=FolderProfile /p:Configuration=Release /p:CreatePackageOnPublish=true "
		$Global:logger.Write("Running build command '$($cmd)'")
		Invoke-Expression $cmd
		$GLobal:logger.Write("Building '$($this.FileToBuild)' was completed...")
	}
	UploadArtifactsToRemote(){
		# Config
		[FTPInformation] $ftpInfo = [FTPInformation]::Load($this.AzurePublishSettingFile)
		# $UserName = 'tinyerp\$tinyerp'
		# $Password = "2rFnjrrgKFp6bDsz1xjE35M8RaN8btlpDAfg39Eugi3s7zmdl3qSf2gqxTj1"
		# $RemoteFolder = "ftp://waws-prod-sn1-113.ftp.azurewebsites.windows.net/site/wwwroot"
		
		[FTP] $ftp = [FTP]::new($this.OutputFolder, $ftpInfo.Path, $ftpInfo.UserName, $ftpInfo.Password)
		$files = [FileHelper]::GetAllFiles($this.OutputFolder)
		ForEach($file in $files){
			$relativePath=$file.replace($this.OutputFolder + "\",'')
			if([FileHelper]::ContainFolder($relativePath) -and ($ftp.CreateFolder([System.IO.Path]::GetDirectoryName($relativePath)).Status -eq [ActionStatusType]::Fail)){
				$Global:logger.Error("Error while working on '$($relativePath)'. Please check in the console for more information.")
				Exit [AppStatusCode]::Error
			}
			$Global:logger.Write("Start uploading '$($relativePath)'...")
			$ftp.Upload($relativePath)
			$Global:logger.Write("'$($relativePath)' was uploaded.")
		}
	}
	Build(){
		$Global:logger.Write("Start building '$($this.FileToBuild)' output to '$($this.OutputFolder)', clearDest: $($this.ClearDest)")
		if($this.IsValidRequest($this.FileToBuild) -ne  $TRUE){
			$Global:logger.Write( "'$($this.FileToBuild)' was not existed. Please specify project to build")
			return
		}
		$Global:logger.Write("Preparing ...", "cyan")
		$this.OnBeforeBuild()
		$Global:logger.Write("Preparing was completed", "cyan")
		$Global:logger.Write("Building ...", "cyan")
		$this.Building()
		$Global:logger.Write("Building was completed ...", "cyan")
		$Global:logger.Write("Upload to remote host ...", "cyan")
		$this.UploadArtifactsToRemote()
		$Global:logger.Write("Upload was completed...", "cyan")
		$Global:logger.Write("Building '$($this.FileToBuild)' was completed.")
	}
}