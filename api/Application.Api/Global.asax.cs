[assembly: Microsoft.Owin.OwinStartup(typeof(App.Api.WebApiApplication))]
namespace App.Api
{
    using App.Common;
    using App.Common.Application;

    public class WebApiApplication : BaseWebApiApplication
    {
        public WebApiApplication() : base() { }
        protected override ApplicationType GetApplicationType()
        {
            return ApplicationType.WebApi;
        }
    }
}
