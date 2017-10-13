namespace App.Api.Features.Common
{
    using App.Common.MVC;
    using App.Common.MVC.Attributes;
    using System.Web.Http;
    [RoutePrefix("api/utilities")]
    public class UtilitiessController : BaseApiController
    {
        [HttpGet]
        [Route("sayHello")]
        [ResponseWrapper()]
        public string SayHello() {
            return "Hello";
        }
    }
}
