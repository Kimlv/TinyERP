namespace App.Service.Setting
{
    using System;

    public interface IContentTypeService
    {
        System.Collections.Generic.IList<ContentTypeListItem> GetContentTypes();
        void CreateIfNotExist(System.Collections.Generic.IList<CreateContentTypeRequest> request);
        void Delete(Guid id);
        GetContentTypeResponse Get(Guid id);
        void Create(CreateContentTypeRequest request);
        void Update(UpdateContentTypeRequest request);
    }
}
