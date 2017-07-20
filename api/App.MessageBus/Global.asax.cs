namespace App.MessageBus
{
    using App.Common;
    using App.Common.Application;

    public class WebApiApplication : BaseWebApiApplication
    {
        public WebApiApplication() : base() { }
        protected override ApplicationType GetApplicationType()
        {
            return ApplicationType.MessageBus;
        }
    }
}
