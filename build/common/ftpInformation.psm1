Class FTPInformation{
    [string] $Path
    [string] $UserName
    [string] $Password
    FTPInformation([string]$path, [string]$userName, [string] $password){
        $this.Path = $path
        $this.UserName = $userName
        $this.Password = $password
    }
    [FTPInformation] static Load([string] $settingFilePath){
        [System.Xml.XmlDocument] $xmlDocument = New-Object System.Xml.XmlDocument
        $xmlDocument.Load($settingFilePath)
        [System.Xml.XmlNode] $ftpSetting = $xmlDocument.SelectSingleNode('//publishProfile[@publishMethod="FTP"]')
        return New-Object FTPInformation($ftpSetting.publishUrl, $ftpSetting.userName, $ftpSetting.userPWD)
    }
}