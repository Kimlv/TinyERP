namespace App.Common.Ftp
{
    using System.IO;

    public class FTPHelper
    {
        public static void UploadFolder(FTPFolderUploadRequest request)
        {
            string[] files = Directory.GetFiles(request.LocalFolder, "*", SearchOption.AllDirectories);
            FTPClient ftp = new FTPClient(request);
            foreach (string fileName in files)
            {
                ftp.UploadFile(fileName);
            }
        }
    }
}