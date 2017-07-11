namespace App.ApiContainer
{
    public class ApiApplication : System.Web.HttpApplication
    {
        private App.Common.Application.IApplication application;
        public ApiApplication()
        {
            this.application = App.Common.Application.ApplicationFactory.Create<System.Web.HttpApplication>(this.GetApplicationType(), this);
        }
        protected virtual App.Common.ApplicationType GetApplicationType()
        {
            throw new System.InvalidOperationException("Please specify type of application");
        }

        protected void Application_Start()
        {
            this.application.OnApplicationStarted();
        }

        protected void Application_End()
        {
            this.application.OnApplicationEnded();
        }

        protected void Config<IApp>(IApp app) {
            this.application.Config<IApp>(app);
        }
    }
}
