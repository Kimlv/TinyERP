namespace App.Common.Helpers
{
    using System.IO;

    public class IOHelper
    {
        public static void CreateFolderIfNotExist(string fullFolderPath)
        {
            if (Directory.Exists(fullFolderPath))
            {
                return;
            }

            Directory.CreateDirectory(fullFolderPath);
        }

        public static MemoryStream CopyFileToMemoryStream(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);
            return ms;
        }
    }
}