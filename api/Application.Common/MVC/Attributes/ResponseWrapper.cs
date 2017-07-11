namespace App.Common.MVC.Attributes
{
    using DI;
    using Http;
    using Logging;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;
    using Validation;

    public class ResponseWrapper : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            IResponseData<object> response = new ResponseData<object>();
            HttpStatusCode httpStatus = HttpStatusCode.OK;
            if (actionExecutedContext.Exception != null && !(actionExecutedContext.Exception is ValidationException))
            {
                httpStatus = HttpStatusCode.InternalServerError;
                ILogger logger = IoC.Container.Resolve<ILogger>();
                logger.Error(actionExecutedContext.Exception);
            }

            if (actionExecutedContext.Exception != null && actionExecutedContext.Exception is ValidationException)
            {
                IValidationException exception = actionExecutedContext.Exception as IValidationException;
                response.SetErrors(exception.Errors);
                httpStatus = HttpStatusCode.BadRequest;
            }

            if (actionExecutedContext.Exception == null && actionExecutedContext.ActionContext.Response.StatusCode != HttpStatusCode.NoContent)
            {
                ObjectContent content = (ObjectContent)actionExecutedContext.ActionContext.Response.Content;
                response.SetData(content != null ? content.Value : null);
            }
            response.SetStatus(httpStatus);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(httpStatus, response);
        }
    }
}
