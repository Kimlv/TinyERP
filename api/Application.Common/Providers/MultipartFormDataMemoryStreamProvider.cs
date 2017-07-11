namespace App.Common.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class MultipartFormDataMemoryStreamProvider : MultipartMemoryStreamProvider
    {
        public class FileInfo
        {
            public byte[] Content { get; set; }
            public string ContentType { get; internal set; }
            public string FileName { get; set; }
            public long FileSize { get; internal set; }
            public FileInfo()
            {
                this.ContentType = FileContentType.Png;
                this.FileSize = 0;
            }
        }

        private readonly Collection<bool> isFormData = new Collection<bool>();
        private readonly NameValueCollection formData = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
        private readonly List<FileInfo> fileData = new List<FileInfo>();
        public NameValueCollection FormData
        {
            get { return this.formData; }
        }

        public List<FileInfo> FileData
        {
            get { return this.fileData; }
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }

            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
            if (contentDisposition == null)
            {
                throw new InvalidOperationException("Did not find required 'Content-Disposition' header field in MIME multipart body part.");
            }

            this.isFormData.Add(string.IsNullOrEmpty(contentDisposition.FileName));
            return base.GetStream(parent, headers);
        }

        public override async Task ExecutePostProcessingAsync()
        {
            for (int index = 0; index < this.Contents.Count; ++index)
            {
                HttpContent formContent = this.Contents[index];
                if (this.isFormData[index])
                {
                    // Field
                    string formFieldName = MultipartFormDataMemoryStreamProvider.UnquoteToken(formContent.Headers.ContentDisposition.Name) ?? string.Empty;
                    string formFieldValue = await formContent.ReadAsStringAsync();
                    this.FormData.Add(formFieldName, formFieldValue);
                }
                else
                {
                    // File
                    FileInfo fileInfo = new FileInfo();
                    fileInfo.FileName = MultipartFormDataMemoryStreamProvider.UnquoteToken(formContent.Headers.ContentDisposition.FileName);
                    fileInfo.Content = await formContent.ReadAsByteArrayAsync();
                    fileInfo.FileSize = (long)formContent.Headers.ContentLength;
                    fileInfo.ContentType = formContent.Headers.ContentType.MediaType;
                    this.FileData.Add(fileInfo);
                }
            }
        }

        private static string UnquoteToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token) == true) { return token; }

            if ((token.StartsWith("\"", StringComparison.Ordinal) == true)
                && (token.EndsWith("\"", StringComparison.Ordinal) == true)
                && (token.Length > 1))
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }
    }
}