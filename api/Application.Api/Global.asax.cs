[assembly: Microsoft.Owin.OwinStartup(typeof(App.Api.WebApiApplication))]
namespace App.Api
{
    using App.Common;
    using Owin;

    public class WebApiApplication : App.ApiContainer.ApiApplication
    {
        public WebApiApplication() : base() { }
        protected override ApplicationType GetApplicationType()
        {
            return ApplicationType.WebApi;
        }

        public void Configuration(IAppBuilder app)
        {
            this.Config<IAppBuilder>(app);
        }
    }
}
