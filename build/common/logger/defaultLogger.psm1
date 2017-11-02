class DefaultLogger{
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