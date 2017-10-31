
# we need to install:
# PSFTP module

$Global:msbuildPath="C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"
$root=Resolve-Path ../
$outputDir="$root\deployment"
$Global:logger = $null
$Global:FolderSeparator="\"
$Global:FtpFolderSeparator="/"
$Global:FtpTimeout = 1000
Enum AppStatusCode{
	Success
	Error
}
Enum ActionStatusType{
	None
	Success
	Exists
	Fail
}

class ActionResult{
	[ActionStatusType] $Status=[ActionStatusType]::None
	[string]$Message = ""
	ActionResult([ActionStatusType] $status){
		$this.Status = $status
	}
}

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
	Building(){
		$this.BuildProject()
	#	$this.UploadToRemoteHost()
	}
	BuildProject(){
		$GLobal:logger.Write("Starting building '$($this.FileToBuild)' ...")
		#$arguments = @()
		#$arguments+=('$this.FileToBuild', "/p:DeployOnBuild=true",  "/p:OutputPath=$($this.OutputFolder)", "/p:PublishProfile=FolderProfile")
		#Invoke-Expression "& '$Global:msbuildPath' $arguments"
		Invoke-Expression "& '$($Global:msbuildPath)' $this.FileToBuild /p:DeployOnBuild=true  /p:OutputPath=$($this.OutputFolder) /p:PublishProfile=FolderProfile "
		$GLobal:logger.Write("Building '$($this.FileToBuild)' was completed...")
	}
	UploadArtifactsToRemote(){
		# Config
		$UserName = 'tinyerp\$tinyerp'
		$Password = "2rFnjrrgKFp6bDsz1xjE35M8RaN8btlpDAfg39Eugi3s7zmdl3qSf2gqxTj1"
		#$LocalFile = "D:\project\tfs_tinyerp\deployment\webapi\New_folder\ThoughtWorks.Selenium.Core.dll"
		$RemoteFolder = "ftp://waws-prod-sn1-113.ftp.azurewebsites.windows.net/site/wwwroot"
		
		[FTP] $ftp = [FTP]::new($this.OutputFolder, $RemoteFolder, $UserName, $Password)
		$files = [FileHelper]::GetAllFiles($this.OutputFolder)
		ForEach($file in $files){
			$relativePath=$file.replace($this.OutputFolder + "\",'')
			if([FileHelper]::ContainFolder($relativePath) -and ($ftp.CreateFolder([System.IO.Path]::GetDirectoryName($relativePath)).Status -eq [ActionStatusType]::Fail)){
				$Global:logger.Error("Error while working on '$($relativePath)'. Please check in the console for more information.")
				Exit [AppStatusCode]::Error
			}

			#$remoteFile = $RemoteFolder + $relativePath
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
class FTP{
	[string] $UserName;
	[string] $Password;
	[string] $BasePath;
	[string] $LocalWorkingFolder
	FTP([string] $localFolder, [string] $basePath, [string] $userName, [string] $password){
		$this.LocalWorkingFolder = $localFolder
		$this.UserName = $userName
		$this.Password = $password;
		$this.BasePath = $basePath
	}
	Upload([string] $relativePath){
		[string] $localFile = [PathHelper]::Combine($this.LocalWorkingFolder, $relativePath)
		[string] $remoteFile = [PathHelper]::Combine($this.BasePath, [PathHelper]::ToFtpPath($relativePath), $Global:FtpFolderSeparator)
		# need to handle response from server and error
		[System.Net.FtpWebRequest] $FTPRequest = $this.CreateRequest($remoteFile, [System.Net.WebRequestMethods+Ftp]::UploadFile)
		# $FTPRequest = [System.Net.FtpWebRequest]::Create($remoteFile) 
		# $FTPRequest.Credentials = New-Object System.Net.NetworkCredential($this.UserName, $this.Password) 
		# $FTPRequest.Method = [System.Net.WebRequestMethods+Ftp]::UploadFile 
		# $FTPRequest.UseBinary = $true 
		# $FTPRequest.KeepAlive = $false
		# $FTPRequest.UsePassive = $true
		$content = [System.IO.File]::ReadAllBytes($localFile)
		$FTPRequest.ContentLength =$content.Length
		$writeStream = $FTPRequest.GetRequestStream()
		$writeStream.Write($content, 0, $content.Length)
		$writeStream.Close()
		$writeStream.Dispose()
	}
	[ActionResult] CreateFolder([string] $path){
		if([System.String]::IsNullOrWhiteSpace($path)){
			$Global:logger.Write("invalid '$($path)'")
			return [ActionResult]::new([ActionStatusType]::Fail)
		}
		[ActionResult] $result = New-Object ActionResult([ActionStatusType]::Success)
		[string[]] $paths = $path.Split($Global:FolderSeparator)
		[string] $createdFolder=""

		For([int] $index=0; $index -lt $paths.Length; $index++){
			if([System.String]::IsNullOrWhiteSpace($createdFolder)){
				$createdFolder = $paths[$index]
			}else{
				$createdFolder = [PathHelper]::Combine($createdFolder, $paths[$index], $Global:FtpFolderSeparator)
			}
			
			[ActionResult] $createFolderResult = $this.CreateFolderByPath($createdFolder)
			if($createFolderResult.Status -eq [ActionStatusType]::Fail){
				$result.Status	 = $createFolderResult.Status
				$result.Message = $createFolderResult.Message
				Break
			}
		}
		$Global:logger.Write("'$($path)' was completed.")
		return $result
	}
	[ActionResult] CreateFolderByPath([string]$path){
		[ActionResult] $result = [ActionResult]::new([ActionStatusType]::Success)
		if($this.Exists($path).Status -eq [ActionStatusType]::Exists){
			$Global:logger.Write("'$($path)' was already existed.")
			$result.Status=[ActionStatusType]::Exists
			return $result 
		}
		Try{
			$Global:logger.Write("Creating new '$($path)' folder).")
			[System.Net.FtpWebRequest] $request = $this.CreateRequest($path, [System.Net.WebRequestMethods+Ftp]::MakeDirectory)
			[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $request.GetResponse()
			$response.Close()
			# [System.IO.Stream] $stream = $response.GetResponseStream()
			$result.Status = [ActionStatusType]::Success
		}Catch{
			# Will handle response later
			[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $_.Response
			$Global:logger.Error("Status code: $($response.StatusCode), Error message: $($_.Exception.Message)")
			$result.Status=[ActionStatusType]::Fail
			$result.Message = $_.Exception.Message
			
		}
		return $result
	}
	[ActionResult] Exists([string] $path){
		[ActionResult] $result = [ActionResult]::new([ActionStatusType]::Success)
		Try{
			$Global:logger.Write("Check if '$($path)' folder) was existed.")
			[System.Net.FtpWebRequest] $request = $this.CreateRequest($path, [System.Net.WebRequestMethods+Ftp]::ListDirectory)
			[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $request.GetResponse()
			$response.Close()
			$result.Status = [ActionStatusType]::Exists
		}Catch{
			$result.Status = [ActionStatusType]::Fail
			$result.Message = $_.Exception.Message
		}
		return $result
	}
	[System.Net.FtpWebRequest] CreateRequest([string] $path, [string] $action){

		[string] $remoteFile =[PathHelper]::Combine($this.BasePath, $path, $Global:FtpFolderSeparator)
		$Global:logger.Write("Creating request to '$($remoteFile)'")
		[System.Net.FtpWebRequest]$FTPRequest = [System.Net.FtpWebRequest]::Create($remoteFile) 
		$FTPRequest.Credentials = New-Object System.Net.NetworkCredential($this.UserName, $this.Password) 
		$FTPRequest.Method = $action 
		$FTPRequest.UseBinary = $true 
		$FTPRequest.KeepAlive = $false
		$FTPRequest.UsePassive = $true
		#$FTPRequest.Timeout = $Global:FtpTimeout
		return $FTPRequest
	}
}
class PathHelper{
	
	[string] static Combine(
		[string]$first, 
		[string] $second, 
		[string]$seperator){
		return [System.String]::Format("{0}{1}{2}", $first, $seperator, $second)
	}
	[string] static Combine(
		[string]$first, 
		[string] $second){
		return [PathHelper]::Combine($first, $second, $Global:FolderSeparator)
	}
	[string] static ToFtpPath([string] $path){
		if([System.String]::IsNullOrWhiteSpace($path)) {return $path}
		return $path.Replace($Global:FolderSeparator, $Global:FtpFolderSeparator)
	}
}
Class FileHelper{
	[String[]] static GetAllFiles($path){
		return (Get-ChildItem -Path $path -Recurse | Where {!$_.PSIsContainer}).FullName
	}
	[bool]static Exist([string]$filePath){
		return Test-Path -Path $filePath
	}
	[bool]static ExistFolder($folder){
		return Test-Path -Path $folder
	}
	[bool] static ContainFolder([string] $path){
		return ![System.String]::IsNullOrWhiteSpace([System.IO.Path]::GetDirectoryName($path))
	}
}
function BuildWebApi([string]$sln, [string]$output, [bool]$clearDest){
	[BuildAgent] $buildAgent=[BuildAgent]::new($sln, $output, $clearDest)
	$buildAgent.Build()
}
class Logger{
	Write([string] $str){
		$this.Write($str, "white")
	}
	Write([string] $str, [string]$color){
		Write-Host $str -ForegroundColor $color
	}
	Error([string] $str){
		$this.Write($str, "red")
	}

}
$webApiSln="D:\project\tfs_tinyerp\api\Application.sln"
$outputFolder="$outputDir\webapi"
$cleanFolderBeforeBuild=$TRUE
$global:logger=[Logger]::new()

BuildWebApi $webApiSln $outputFolder $cleanFolderBeforeBuild