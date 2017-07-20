namespace App.Common.Application
{
    using Owin;
    public abstract class BaseWebApiApplication : System.Web.HttpApplication
    {
        private App.Common.Application.IApplication application;
        public BaseWebApiApplication()
        {
            this.application = App.Common.Application.ApplicationFactory.Create<System.Web.HttpApplication>(this.GetApplicationType(), this);
        }
        protected virtual App.Common.ApplicationType GetApplicationType()
        {
            throw new System.InvalidOperationException("Please specify type of application");
        }

        public override void Init()
        {
            base.Init();
            this.application.OnApplicationInitialized();
        }
        protected virtual void Application_Start()
        {
            this.application.OnApplicationStarted();
        }

        protected virtual void Application_End()
        {
            this.application.OnApplicationEnded();
        }
        public virtual void Configuration(IAppBuilder app)
        {
            this.Config<IAppBuilder>(app);
        }
        protected virtual void Config<IApp>(IApp app)
        {
            this.application.Config<IApp>(app);
        }
    }
}
