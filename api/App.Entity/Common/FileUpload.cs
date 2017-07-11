namespace App.Entity.Common
{
    using App.Common.Data;

    public class FileUpload : BaseEntity
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public FileUpload()
        {
        }

        public FileUpload(string fileName, string contentType, long fileSize, byte[] content)
        {
            this.FileName = fileName;
            this.ContentType = contentType;
            this.Size = fileSize;
            this.Content = content;
        }
    }
}
