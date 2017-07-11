namespace App.Api.Features.Setting
{
    using App.Common.DI;
    using App.Common.MVC;
    using App.Common.MVC.Attributes;
    using App.Service.Setting;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("api/contenttypes")]
    public class ContentTypesController : BaseApiController
    {
        [Route("{id}")]
        [HttpPut]
        [ResponseWrapper()]
        public void UpdateContentType(UpdateContentTypeRequest request)
        {
            IContentTypeService service = IoC.Container.Resolve<IContentTypeService>();
            service.Update(request);
        }

        [Route("")]
        [HttpPost]
        [ResponseWrapper()]
        public void CreateContentType(CreateContentTypeRequest request)
        {
            IContentTypeService service = IoC.Container.Resolve<IContentTypeService>();
            service.Create(request);
        }

        [Route("{id}")]
        [HttpGet]
        [ResponseWrapper()]
        public GetContentTypeResponse GetContentType(Guid id)
        {
            IContentTypeService service = IoC.Container.Resolve<IContentTypeService>();
            return service.Get(id);
        }

        [Route("{id}")]
        [HttpDelete]
        [ResponseWrapper()]
        public void DeleteContentType(Guid id)
        {
            IContentTypeService service = IoC.Container.Resolve<IContentTypeService>();
            service.Delete(id);
        }

        [Route("")]
        [HttpGet]
        [ResponseWrapper()]
        public IList<ContentTypeListItem> GetContentTypes()
        {
            IContentTypeService service = IoC.Container.Resolve<IContentTypeService>();
            return service.GetContentTypes();
        }
    }
}