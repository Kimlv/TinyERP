namespace App.Service.Common.File
{
    using System;
    using System.Collections.Generic;
    using App.Common;
    using App.Common.Providers;
    using App.Entity.Common;

    public interface IFileService
    {
        IList<FileUploadResponse> UploadFiles(List<MultipartFormDataMemoryStreamProvider.FileInfo> fileData);
        string GetPhotoAsBase64(Guid id);
        FileUpload Get(Guid id);
        System.Drawing.Bitmap GetThumbnail(Guid id, ThumbnailSize medium);
    }
}
