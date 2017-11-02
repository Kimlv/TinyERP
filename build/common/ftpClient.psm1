using module ".\enum.psm1"
using module ".\actionResult.psm1"
using module ".\helpers\pathHelper.psm1"

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
		$Global:logger.Write("Uploading '$($relativePath)' file...")
		[string] $localFile = [PathHelper]::Combine($this.LocalWorkingFolder, $relativePath)
		[string] $remoteFilePath = [PathHelper]::ToFtpPath($relativePath) # [PathHelper]::Combine($this.BasePath, [PathHelper]::ToFtpPath($relativePath), $Global:FtpFolderSeparator)
		# need to handle response from server and error
		[System.Net.FtpWebRequest] $FTPRequest = $this.CreateRequest($remoteFilePath, [System.Net.WebRequestMethods+Ftp]::UploadFile)
		$content = [System.IO.File]::ReadAllBytes($localFile)
		$FTPRequest.ContentLength =$content.Length
		$writeStream = $FTPRequest.GetRequestStream()
		$writeStream.Write($content, 0, $content.Length)
		$writeStream.Close()
		$writeStream.Dispose()
		$Global:logger.Write("'$($relativePath)' file was uploaded")
	}
	[ActionResult] CreateFolder([string] $path){
		if([System.String]::IsNullOrWhiteSpace($path)){
			$Global:logger.Write("invalid '$($path)'")
			return [ActionResult]::new([ActionStatusType]::Fail)
		}
		$Global:logger.Write("Creating folder '$($path)'")
		[ActionResult] $result =  [ActionResult]::new([ActionStatusType]::Success)
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