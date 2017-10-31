
# we need to install:
# PSFTP module

$global:msbuildPath="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
$msbuildPath="C:\Windows\Microsoft.NET\Framework64\v3.5\MSBuild.exe"
$root=Resolve-Path ../
$outputDir="$root\deployment"
$Global:logger = $null
$Global:FolderSeparator="\"
$Global:FtpFolderSeparator="/"
# Add-Type -TypeDefinition @"
# 	public enum ActionStatusType{
# 		None=0,
# 		Success=200,
# 		Fail=-1
# 	}
# "@
Enum AppStatusCode{
	Success
	Error
}
Enum ActionStatusType{
	None
	Success
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
		Write-Host "Starting building '$($this.FileToBuild)' ..."
		Invoke-Expression "$($global:msbuildPath) $($this.FileToBuild) /p:OutputPath=$($this.OutputFolder) /p:PublishProfile=FolderProfile"
		Write-Host "Building '$($this.FileToBuild)' was completed..."
	}
	UploadArtifactsToRemote(){
		# Config
		$Username = 'tinyerp\$tinyerp'
		$Password = "2rFnjrrgKFp6bDsz1xjE35M8RaN8btlpDAfg39Eugi3s7zmdl3qSf2gqxTj1"
		$LocalFile = "D:\project\tfs_tinyerp\deployment\webapi\webapi.zip"
		$RemoteFolder = "ftp://waws-prod-sn1-113.ftp.azurewebsites.windows.net/site/wwwroot/"
		$files = [FileHelper]::GetAllFiles($this.OutputFolder)
		[FTP] $ftp = [FTP]::new($this.OutputFolder, $RemoteFolder, $Username, $Password)

		ForEach($file in $files){
			$relativePath=$file.replace($this.OutputFolder + "\",'')
			$Global:logger.Write("Working with '$($relativePath)'...")
			if([FileHelper]::ContainFolder($relativePath) -and ($ftp.CreateFolder([System.IO.Path]::GetDirectoryName($relativePath)).Status -eq [ActionStatusType]::Fail)){
				$Global:logger.Error("Error while working on '$($relativePath)'. Please check in the console for more information.")
				Exit [AppStatusCode]::Error
				
			}

			$remoteFile = $RemoteFolder + $relativePath
			$Global:logger.Write("Start uploading '$($relativePath)'...")
			#Write-Host "Start uploading '$($relativePath)' to $($remoteFile) ..."
			$ftp.Upload($relativePath)
			#$ftp.Upload($file, $remoteFile, $Username, $Password)
			$Global:logger.Write("'$($relativePath)' was uploaded.")
			# Write-Host "'$($relativePath)' was uploaded."
		}
		# $relativePath=$localFile.replace($this.OutputFolder,'')
		# Write-Host "Start uploading '$($relativePath)' ..."
		# [FTPHelper]::Upload($LocalFile, $RemoteFile, $Username, $Password)
		# Write-Host "'$($relativePath)' was uploaded."
		# Create a FTPWebRequest 
		# $FTPRequest = [System.Net.FtpWebRequest]::Create($RemoteFile) 
		# $FTPRequest.Credentials = New-Object System.Net.NetworkCredential($Username,$Password) 
		# $FTPRequest.Method = [System.Net.WebRequestMethods+Ftp]::UploadFile 
		# $FTPRequest.UseBinary = $true 
		# $FTPRequest.KeepAlive = $false
		# #$FTPRequest.UsePassive = $true
		# # Send the ftp request
		# $FTPResponse = $FTPRequest.GetResponse() 
		# # Get a download stream from the server response 
		# $ResponseStream = $FTPResponse.GetResponseStream() 
		# # $files = [FileHelper]::GetAllFiles($this.OutputFolder)
		# # ForEach($file in $files){
		# # 	Write-Host "File: $($file)"
		# # }

		# Write-Host "Start uploading '$($LocalFile)' to '$($RemoteFile)' ..."

		# $content = [System.IO.File]::ReadAllBytes($LocalFile)
		# $FTPRequest.ContentLength= $content.Length
		# $writeStream = $FTPRequest.GetRequestStream()
		# $writeStream.Write($content, 0, $content.Length)
		# $writeStream.Close()
		# $writeStream.Dispose()
		#[System.IO.Stream] uploadStream = FTPHelper.GetUploadStream() 
		
		# $Files = Get-ChildItem -Path $this.OutputFolder -Recurse | Select-Obejct FullName

		# Create the target file on the local system and the download buffer 
		# $LocalFileFile = New-Object IO.FileStream ($LocalFile,[IO.FileMode]::Create) 
		# [byte[]]$ReadBuffer = New-Object byte[] 1024 
		# # Loop through the download 
		# Write-Host "Start uploading '$($LocalFile)' to '$($RemoteFile)' ..."
		# do { 
		# 	$ReadLength = $ResponseStream.Read($ReadBuffer,0,1024) 
		# 	$LocalFileFile.Write($ReadBuffer,0,$ReadLength) 
		# } 
		# while ($ReadLength -ne 0)
		#Write-Host "'$($LocalFile)' was uploaded to '$($RemoteFile)'."
	}
	build(){
		# Write-Host "Start building '$($this.FileToBuild)' output to '$($this.OutputFolder)', clearDest: $($this.ClearDest)"
		# if($this.IsValidRequest($this.FileToBuild) -ne  $TRUE){
		# 	Write-Output "'$($this.FileToBuild)' was not existed. Please specify project to build"
		# 	return
		# }
		#$this.OnBeforeBuild()
		#$this.Building()
		$this.UploadArtifactsToRemote()
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
		$FTPRequest = [System.Net.FtpWebRequest]::Create($remoteFile) 
		$FTPRequest.Credentials = New-Object System.Net.NetworkCredential($this.UserName, $this.Password) 
		$FTPRequest.Method = [System.Net.WebRequestMethods+Ftp]::UploadFile 
		$FTPRequest.UseBinary = $true 
		$FTPRequest.KeepAlive = $false
		$FTPRequest.UsePassive = $true
		$content = [System.IO.File]::ReadAllBytes($localFile)
		$FTPRequest.ContentLength =$content.Length
		$writeStream = $FTPRequest.GetRequestStream()
		$writeStream.Write($content, 0, $content.Length)
		$writeStream.Close()
		$writeStream.Dispose()
	}
	# Upload([string] $localFile, [string] $remoteFile, [string] $userName, [string] $pwd){
	# 	# need to handle response from server and error
	# 	$FTPRequest = [System.Net.FtpWebRequest]::Create($remoteFile) 
	# 	$FTPRequest.Credentials = New-Object System.Net.NetworkCredential($userName, $pwd) 
	# 	$FTPRequest.Method = [System.Net.WebRequestMethods+Ftp]::UploadFile 
	# 	$FTPRequest.UseBinary = $true 
	# 	$FTPRequest.KeepAlive = $false
	# 	$FTPRequest.UsePassive = $true
	# 	$content = [System.IO.File]::ReadAllBytes($localFile)
	# 	$FTPRequest.ContentLength =$content.Length
	# 	$writeStream = $FTPRequest.GetRequestStream()
	# 	$writeStream.Write($content, 0, $content.Length)
	# 	$writeStream.Close()
	# 	$writeStream.Dispose()
	# }

	[ActionResult] CreateFolder([string] $path){
		if([System.String]::IsNullOrWhiteSpace($path)){
			$Global:logger.Write("invalid '$($path)'")
			return [ActionResult]::new([ActionStatusType]::Fail)
		}
		[ActionResult] $result = New-Object ActionResult([ActionStatusType]::Success)
		[string[]] $paths = $path.Split($Global:FolderSeparator)
		[string] $createdFolder=""

		For([int] $index=0; $index -lt $paths.Length; $index++){
			$createdFolder = [PathHelper]::Combine($createdFolder, $paths[$index], $Global:FtpFolderSeparator)
			[ActionResult] $createFolderResult = $this.CreateFolderByPath($createdFolder)
			if($createFolderResult.Status -ne [ActionStatusType]::Success){
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
		Try{
			$Global:logger.Write("Creating new '$($path)' folder).")
			[System.Net.FtpWebRequest] $request = $this.CreateRequest($path, [System.Net.WebRequestMethods+Ftp]::MakeDirectory)
			[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $request.GetResponse()
			[System.IO.Stream] $stream = $response.GetResponseStream()
			$result.Status = [ActionStatusType]::Success
		}Catch{
			# Will handle response later
			#[System.Net.FtpWebResponse] $response = [System.Net.FtpWebResponse] $_.Response
			$Global:logger.Error($_.Exception.Message)
			$result.Status=[ActionStatusType]::Fail
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
function buildWebApi([string]$sln, [string]$output, [bool]$clearDest){
	[BuildAgent] $buildAgent=[BuildAgent]::new($sln, $output, $clearDest)
	$buildAgent.build()
}
class Logger{
	Write([string] $str){
		Write-Host $str
	}
	Error([string] $str){
		Write-Host $str -ForeGroundColor "Red"
	}

}
$webApiSln="D:\project\tfs_tinyerp\api\Application.sln"
$outputFolder="$outputDir\webapi"
$cleanFolderBeforeBuild=$TRUE
$global:logger=[Logger]::new()

buildWebApi $webApiSln $outputFolder $cleanFolderBeforeBuild