namespace App.Api.Features.Inventory
{
    using System.Collections.Generic;
    using System.Web.Http;
    using App.Common.Http;
    using App.Common.Validation;
    using App.Common.DI;
    using App.Service.Inventory;
    using System;

    [RoutePrefix("api/inventory/categories")]
    public class CategoriesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IResponseData<IList<CategoryListItem>> GetCategories()
        {
            IResponseData<IList<CategoryListItem>> response = new ResponseData<IList<CategoryListItem>>();
            try
            {
                ICategoryService categoryService = IoC.Container.Resolve<ICategoryService>();
                IList<CategoryListItem> categories = categoryService.GetCategories();
                response.SetStatus(System.Net.HttpStatusCode.OK);
                response.SetData(categories);
            }
            catch (ValidationException exception)
            {
                response.SetErrors(exception.Errors);
                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
            }

            return response;
        }

        [Route("{id}")]
        [HttpGet]
        public IResponseData<GetCategoryResponse> GetCategory([FromUri]Guid id)
        {
            IResponseData<GetCategoryResponse> response = new ResponseData<GetCategoryResponse>();
            try
            {
                ICategoryService categoryService = IoC.Container.Resolve<ICategoryService>();
                GetCategoryResponse category = categoryService.GetCategory(id);
                response.SetData(category);
            }
            catch (ValidationException exception)
            {
                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
                response.SetErrors(exception.Errors);
            }

            return response;
        }

        [Route("")]
        [HttpPost]
        public IResponseData<string> CreateCategory([FromBody]CreateCategoryRequest request)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                ICategoryService categorytService = IoC.Container.Resolve<ICategoryService>();
                categorytService.Create(request);
            }
            catch (ValidationException exception)
            {
                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
                response.SetErrors(exception.Errors);
            }

            return response;
        }

        [Route("{id}")]
        [HttpPut]
        public IResponseData<string> UpdateCategory([FromUri]Guid id, [FromBody]UpdateCategoryRequest request)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                request.Id = id;
                ICategoryService categoryService = IoC.Container.Resolve<ICategoryService>();
                categoryService.Update(request);
            }
            catch (ValidationException exception)
            {
                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
                response.SetErrors(exception.Errors);
            }

            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public IResponseData<string> DeleteCategory([FromUri] Guid id)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                ICategoryService categoryService = IoC.Container.Resolve<ICategoryService>();
                categoryService.Delete(id);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
            }

            return response;
        }
    }
}