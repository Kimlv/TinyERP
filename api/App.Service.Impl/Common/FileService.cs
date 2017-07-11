namespace App.Service.Impl.Common
{
    using System;
    using System.Collections.Generic;
    using App.Common.Providers;
    using App.Service.Common.File;
    using App.Common.Data;
    using App.Common;
    using App.Common.Helpers;
    using App.Common.DI;
    using App.Repository.Common;
    using App.Entity.Common;
    using System.Drawing;

    internal class FileService : IFileService
    {
        public FileUpload Get(Guid id)
        {
            IFileRepository repo = IoC.Container.Resolve<IFileRepository>();
            return repo.GetById(id.ToString());
        }

        public string GetPhotoAsBase64(Guid id)
        {
            IFileRepository repo = IoC.Container.Resolve<IFileRepository>();
            App.Entity.Common.FileUpload file = repo.GetById(id.ToString());
            return ImageHelper.ToBase64(file.FileName, file.Content);
        }

        public Bitmap GetThumbnail(Guid id, ThumbnailSize size)
        {
            IFileRepository repo = IoC.Container.Resolve<IFileRepository>();
            App.Entity.Common.FileUpload file = repo.GetById(id.ToString());
            return ImageHelper.GetThumbnail(file.FileName, file.Content, size);
        }

        public IList<FileUploadResponse> UploadFiles(List<MultipartFormDataMemoryStreamProvider.FileInfo> files)
        {
            this.ValidateUploadRequest(files);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                IFileRepository repo = IoC.Container.Resolve<IFileRepository>(uow);
                IList<FileUploadResponse> filesUploaded = new List<FileUploadResponse>();
                foreach (App.Common.Providers.MultipartFormDataMemoryStreamProvider.FileInfo file in files)
                {
                    App.Entity.Common.FileUpload fileCreated = new Entity.Common.FileUpload(file.FileName, file.ContentType, file.FileSize, file.Content);
                    repo.Add(fileCreated);
                    filesUploaded.Add(ObjectHelper.Convert<FileUploadResponse>(fileCreated));
                }

                uow.Commit();
                return filesUploaded;
            }
        }

        private void ValidateUploadRequest(object files)
        {
        }
    }
}
