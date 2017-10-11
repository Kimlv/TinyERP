namespace App.Api.Features.Common
{
    using System.Web.Http;
    [RoutePrefix("api/hello")]
    public class HelloController : ApiController
    {
        [Route("")]
        public string SayHello() {
            return "Hello";
        }
    }
}
