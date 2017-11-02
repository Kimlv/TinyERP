
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
$outputDir = "$root\deployment"

$Global:logger = [DefaultLogger]::new()
$Global:FolderSeparator = [Configuration]::FolderSeparator
$Global:FtpFolderSeparator = [Configuration]::FtpFolderSeparator
$Global:FtpTimeout = [Configuration]::FtpTimeout

$fileToBuild="D:\project\tfs_tinyerp\api\Application.Api\App.Api.csproj"
$solutionDir="D:\project\tfs_tinyerp\api\"
$buildOutputFolder="$outputDir\webapi"
$cleanOutputFolderBeforeBuild=$TRUE

[BuildAgent] $buildAgent=[BuildAgent]::new($fileToBuild, $solutionDir, $buildOutputFolder, $cleanOutputFolderBeforeBuild)
$buildAgent.Build()

# class ActionResult{
# 	[ActionStatusType] $Status=[ActionStatusType]::None
# 	[string]$Message = ""
# 	ActionResult([ActionStatusType] $status){
# 		$this.Status = $status
# 	}
# }

# Class BuildAgent{
# 	[string]$FileToBuild
# 	[string]$OutputFolder
# 	[bool]$ClearDest
	
# 	BuildAgent([string]$fileToBuild, [string]$output, [bool]$clearDest){
# 		$this.FileToBuild=$fileToBuild
# 		$this.OutputFolder=$output
# 		$this.ClearDest=$clearDest
# 	}
# 	[bool] IsValidRequest([string] $filePath){
# 		return [FileHelper]::Exist($filePath)
# 	}
# 	OnBeforeBuild(){
# 		if (($this.ClearDest -eq $TRUE) -and [FileHelper]::ExistFolder($this.OutputFolder)){
# 			Write-Host "Deleting '$($this.OutputFolder)' ..."
# 			Remove-Item $this.OutputFolder -force -recurse
# 			Write-Host "'$($this.OutputFolder)' folder was deleted"
# 		}
# 		if([FileHelper]::ExistFolder($this.OutputFolder) -ne $TRUE){
# 			Write-Host "Creating '$($this.OutputFolder)' folder ..."
# 			New-Item -ItemType Directory -Force -Path $this.OutputFolder
# 			Write-Host "'$($this.OutputFolder)' folder was created ..."
# 		}
# 	}
# 	Building(){
# 		$this.BuildProject()

# 		[string] $projectDir=[System.IO.Path]::GetDirectoryName($this.FileToBuild)
# 		[string] $copyFrom = [System.String]::Format("{0}\obj\Release\Package\PackageTmp\*", $projectDir)
# 		[FileHelper]::CopyFolder($copyFrom, $this.OutputFolder)
# 	#	$this.UploadToRemoteHost()
# 	}
# 	BuildProject(){
# 		$GLobal:logger.Write("Starting building '$($this.FileToBuild)' ...")
# 		#$arguments = @()
# 		#$arguments+=('$this.FileToBuild', "/p:DeployOnBuild=true",  "/p:OutputPath=$($this.OutputFolder)", "/p:PublishProfile=FolderProfile")
# 		#Invoke-Expression "& '$Global:msbuildPath' $arguments"
# 		$cmd = "$($Global:msbuildPath) $($this.FileToBuild) /p:SolutionDir=D:\project\tfs_tinyerp\api /p:DeployOnBuild=true /t:Package /p:PublishProfile=FolderProfile /p:Configuration=Release /p:CreatePackageOnPublish=true "
# 		$Global:logger.Write("Running build command '$($cmd)'")
# 		# $cmd="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe D:\project\tfs_tinyerp\api\Application.Api\App.Api.csproj /p:SolutionDir=D:\project\tfs_tinyerp\api /p:DeployOnBuild=true /p:OutputDir=BuildAgent.OutputFolder  /p:OutputPath=D:\project\tfs_tinyerp\deployment\webapi /p:PublishProfile=FolderProfile /p:Configuration=Release"
# 		Invoke-Expression $cmd
# 		$GLobal:logger.Write("Building '$($this.FileToBuild)' was completed...")
# 	}
# 	UploadArtifactsToRemote(){
# 		# Config
# 		$UserName = 'tinyerp\$tinyerp'
# 		$Password = "2rFnjrrgKFp6bDsz1xjE35M8RaN8btlpDAfg39Eugi3s7zmdl3qSf2gqxTj1"
# 		#$LocalFile = "D:\project\tfs_tinyerp\deployment\webapi\New_folder\ThoughtWorks.Selenium.Core.dll"
# 		$RemoteFolder = "ftp://waws-prod-sn1-113.ftp.azurewebsites.windows.net/site/wwwroot"
		
# 		[FTP] $ftp = [FTP]::new($this.OutputFolder, $RemoteFolder, $UserName, $Password)
# 		$files = [FileHelper]::GetAllFiles($this.OutputFolder)
# 		ForEach($file in $files){
# 			$relativePath=$file.replace($this.OutputFolder + "\",'')
# 			if([FileHelper]::ContainFolder($relativePath) -and ($ftp.CreateFolder([System.IO.Path]::GetDirectoryName($relativePath)).Status -eq [ActionStatusType]::Fail)){
# 				$Global:logger.Error("Error while working on '$($relativePath)'. Please check in the console for more information.")
# 				Exit [AppStatusCode]::Error
# 			}

# 			#$remoteFile = $RemoteFolder + $relativePath
# 			$Global:logger.Write("Start uploading '$($relativePath)'...")
# 			$ftp.Upload($relativePath)
# 			$Global:logger.Write("'$($relativePath)' was uploaded.")
# 		}
# 	}
# 	Build(){
# 		$Global:logger.Write("Start building '$($this.FileToBuild)' output to '$($this.OutputFolder)', clearDest: $($this.ClearDest)")
# 		if($this.IsValidRequest($this.FileToBuild) -ne  $TRUE){
# 			$Global:logger.Write( "'$($this.FileToBuild)' was not existed. Please specify project to build")
# 			return
# 		}
# 		$Global:logger.Write("Preparing ...", "cyan")
# 		$this.OnBeforeBuild()
# 		$Global:logger.Write("Preparing was completed", "cyan")
# 		$Global:logger.Write("Building ...", "cyan")
# 		$this.Building()
# 		$Global:logger.Write("Building was completed ...", "cyan")
# 		$Global:logger.Write("Upload to remote host ...", "cyan")
# 		$this.UploadArtifactsToRemote()
# 		$Global:logger.Write("Upload was completed...", "cyan")
# 		$Global:logger.Write("Building '$($this.FileToBuild)' was completed.")
# 	}
# }
# class FTP{
# 	[string] $UserName;
# 	[string] $Password;
# 	[string] $BasePath;
# 	[string] $LocalWorkingFolder
# 	FTP([string] $localFolder, [string] $basePath, [string] $userName, [string] $password){
# 		$this.LocalWorkingFolder = $localFolder
# 		$this.UserName = $userName
# 		$this.Password = $password;
# 		$this.BasePath = $basePath
# 	}
# 	Upload([string] $relativePath){
# 		$Global:logger.Write("Uploading '$($relativePath)' file...")
# 		[string] $localFile = [PathHelper]::Combine($this.LocalWorkingFolder, $relativePath)
# 		[string] $remoteFilePath = [PathHelper]::ToFtpPath($relativePath) # [PathHelper]::Combine($this.BasePath, [PathHelper]::ToFtpPath($relativePath), $Global:FtpFolderSeparator)
# 		# need to handle response from server and error
# 		[System.Net.FtpWebRequest] $FTPRequest = $this.CreateRequest($remoteFilePath, [System.Net.WebRequestMethods+Ftp]::UploadFile)
# 		$content = [System.IO.File]::ReadAllBytes($localFile)
# 		$FTPRequest.ContentLength =$content.Length
# 		$writeStream = $FTPRequest.GetRequestStream()
# 		$writeStream.Write($content, 0, $content.Length)
# 		$writeStream.Close()
# 		$writeStream.Dispose()
# 		$Global:logger.Write("'$($relativePath)' file was uploaded")
# 	}
# 	[ActionResult] CreateFolder([string] $path){
# 		if([System.String]::IsNullOrWhiteSpace($path)){
# 			$Global:logger.Write("invalid '$($path)'")
# 			return [ActionResult]::new([ActionStatusType]::Fail)
# 		}
# 		$Global:logger.Write("Creating folder '$($path)'")
# 		[ActionResult] $result =  [ActionResult]::new([ActionStatusType]::Success)
# 		[string[]] $paths = $path.Split($Global:FolderSeparator)
# 		[string] $createdFolder=""

# 		For([int] $index=0; $index -lt $paths.Length; $index++){
# 			if([System.String]::IsNullOrWhiteSpace($createdFolder)){
# 				$createdFolder = $paths[$index]
# 			}else{
# 				$createdFolder = [PathHelper]::Combine($createdFolder, $paths[$index], $Global:FtpFolderSeparator)
# 			}
			
# 			[ActionResult] $createFolderResult = $this.CreateFolderByPath($createdFolder)
# 			if($createFolderResult.Status -eq [ActionStatusType]::Fail){
# 				$result.Status	 = $createFolderResult.Status
# 				$result.Message = $createFolderResult.Message
# 				Break
# 			}
# 		}
# 		$Global:logger.Write("'$($path)' was completed.")
# 		return $result
# 	}
# 	[ActionResult] CreateFolderByPath([string]$path){
# 		[ActionResult] $result = [ActionResult]::new([ActionStatusType]::Success)
# 		if($this.Exists($path).Status -eq [ActionStatusType]::Exists){
# 			$Global:logger.Write("'$($path)' was already existed.")
# 			$result.Status=[ActionStatusType]::Exists
# 			return $result 
# 		}
# 		Try{
# 			$Global:logger.Write("Creating new '$($path)' folder).")
# 			[System.Net.FtpWebRequest] $request = $this.CreateRequest($path, [System.Net.WebRequestMethods+Ftp]::MakeDirectory)
# 			[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $request.GetResponse()
# 			$response.Close()
# 			# [System.IO.Stream] $stream = $response.GetResponseStream()
# 			$result.Status = [ActionStatusType]::Success
# 		}Catch{
# 			# Will handle response later
# 			[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $_.Response
# 			$Global:logger.Error("Status code: $($response.StatusCode), Error message: $($_.Exception.Message)")
# 			$result.Status=[ActionStatusType]::Fail
# 			$result.Message = $_.Exception.Message
			
# 		}
# 		return $result
# 	}
# 	[ActionResult] Exists([string] $path){
# 		[ActionResult] $result = [ActionResult]::new([ActionStatusType]::Success)
# 		Try{
# 			$Global:logger.Write("Check if '$($path)' folder) was existed.")
# 			[System.Net.FtpWebRequest] $request = $this.CreateRequest($path, [System.Net.WebRequestMethods+Ftp]::ListDirectory)
# 			[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $request.GetResponse()
# 			$response.Close()
# 			$result.Status = [ActionStatusType]::Exists
# 		}Catch{
# 			$result.Status = [ActionStatusType]::Fail
# 			$result.Message = $_.Exception.Message
# 		}
# 		return $result
# 	}
# 	[System.Net.FtpWebRequest] CreateRequest([string] $path, [string] $action){

# 		[string] $remoteFile =[PathHelper]::Combine($this.BasePath, $path, $Global:FtpFolderSeparator)
# 		$Global:logger.Write("Creating request to '$($remoteFile)'")
# 		[System.Net.FtpWebRequest]$FTPRequest = [System.Net.FtpWebRequest]::Create($remoteFile) 
# 		$FTPRequest.Credentials = New-Object System.Net.NetworkCredential($this.UserName, $this.Password) 
# 		$FTPRequest.Method = $action 
# 		$FTPRequest.UseBinary = $true 
# 		$FTPRequest.KeepAlive = $false
# 		$FTPRequest.UsePassive = $true
# 		#$FTPRequest.Timeout = $Global:FtpTimeout
# 		return $FTPRequest
# 	}
# }

# Class FileHelper{
# 	static CopyFolder([string] $fromFolder, [string] $toFolder){
# 		$Global:logger.Write("Copying from '$($fromFolder)' to '$($toFolder)'...")
# 		Copy-Item -Path $fromFolder -Recurse -Destination $toFolder -Container
# 		$Global:logger.Write("'$($fromFolder)' folder was copied to '$($toFolder)'")
# 	}
# 	[String[]] static GetAllFiles($path){
# 		return (Get-ChildItem -Path $path -Recurse | Where {!$_.PSIsContainer}).FullName
# 	}
# 	[bool]static Exist([string]$filePath){
# 		return Test-Path -Path $filePath
# 	}
# 	[bool]static ExistFolder($folder){
# 		return Test-Path -Path $folder
# 	}
# 	[bool] static ContainFolder([string] $path){
# 		return ![System.String]::IsNullOrWhiteSpace([System.IO.Path]::GetDirectoryName($path))
# 	}
# }

# class Logger{
# 	Write([string] $str){
# 		$this.Write($str, "white")
# 	}
# 	Write([string] $str, [string]$color){
# 		Write-Host $str -ForegroundColor $color
# 	}
# 	Error([string] $str){
# 		$this.Write($str, "red")
# 	}

# }

