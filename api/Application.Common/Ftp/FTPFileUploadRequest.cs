namespace App.Common.Ftp
{
    public class FTPFileUploadRequest : FTPRequest
    {
        public string FilePath { get; set; }
        public string BaseFolder { get; set; }
        public FTPFileUploadRequest(string filePath, string baseFolder, FTPRequest uploadRequest)
            : base(uploadRequest)
        {
            this.FilePath = filePath;
            this.BaseFolder = baseFolder;
        }
    }
}