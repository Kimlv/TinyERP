namespace App.Common.Ftp
{
    public class FTPCreateFolderRequest : FTPRequest
    {
        public string SubDir { get; set; }
        public FTPCreateFolderRequest(string subDir, FTPRequest request) : base(request)
        {
            this.SubDir = subDir;
        }
    }
}