namespace App.Api.Features.Support
{
    using App.Common.DI;
    using App.Common.Http;
    using App.Common.Validation;
    using App.Service.Support;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    [RoutePrefix("api/support/requests")]
    public class RequestsController : ApiController
    {
        [HttpPost]
        [Route("{itemId}/markAsResolved")]
        public IResponseData<string> MarkAsResolved(Guid itemId)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                IRequestService service = IoC.Container.Resolve<IRequestService>();
                service.MarkAsResolved(itemId);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(HttpStatusCode.PreconditionFailed);
            }

            return response;
        }

        [HttpPost]
        [Route("{itemId}/cancel")]
        public IResponseData<string> CancelRequest(Guid itemId)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                IRequestService service = IoC.Container.Resolve<IRequestService>();
                service.Cancel(itemId);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(HttpStatusCode.PreconditionFailed);
            }

            return response;
        }

        [HttpGet]
        [Route("{itemId}")]
        public IResponseData<GetRequestResponse> GetRequest(Guid itemId)
        {
            IResponseData<GetRequestResponse> response = new ResponseData<GetRequestResponse>();
            try
            {
                IRequestService service = IoC.Container.Resolve<IRequestService>();
                GetRequestResponse item = service.GetRequest(itemId);
                response.SetData(item);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(HttpStatusCode.PreconditionFailed);
            }

            return response;
        }

        [HttpGet]
        [Route("")]
        public IResponseData<IList<SupportRequestListItem>> GetRequests()
        {
            IResponseData<IList<SupportRequestListItem>> response = new ResponseData<IList<SupportRequestListItem>>();
            try
            {
                IRequestService service = IoC.Container.Resolve<IRequestService>();
                IList<SupportRequestListItem> items = service.GetRequests();
                response.SetData(items);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(HttpStatusCode.PreconditionFailed);
            }

            return response;
        }

        [HttpPost]
        [Route("")]
        public IResponseData<string> CreateRequest(CreateRequest request)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                IRequestService service = IoC.Container.Resolve<IRequestService>();
                service.CreateRequest(request);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(HttpStatusCode.PreconditionFailed);
            }

            return response;
        }
    }
}
