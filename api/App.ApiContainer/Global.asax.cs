namespace App.ApiContainer
{
    using App.Common;
    public class WebApiApplication : App.Common.Application.BaseWebApiApplication
    {
        public WebApiApplication() : base() { }
        protected override ApplicationType GetApplicationType()
        {
            return ApplicationType.WebApi;
        }
    }
}
