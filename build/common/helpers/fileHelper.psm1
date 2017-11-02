Class FileHelper{
	static CopyFolder([string] $fromFolder, [string] $toFolder){
		$Global:logger.Write("Copying from '$($fromFolder)' to '$($toFolder)'...")
		Copy-Item -Path $fromFolder -Recurse -Destination $toFolder -Container
		$Global:logger.Write("'$($fromFolder)' folder was copied to '$($toFolder)'")
	}
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