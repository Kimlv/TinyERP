namespace App.Common.Application
{
    public class ExternalWebApiApplication : WebApiApplication
    {
        public ExternalWebApiApplication(System.Web.HttpApplication application) : base(application, ApplicationType.ExternalWebApi) { }
    }
}
