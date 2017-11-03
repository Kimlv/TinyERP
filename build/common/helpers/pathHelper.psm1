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