namespace App.MessageBus
{
    using App.Common;
    public class WebApiApplication : App.ApiContainer.ApiApplication
    {
        public WebApiApplication() : base() { }
        protected override ApplicationType GetApplicationType()
        {
            return ApplicationType.MessageBus;
        }
    }
}
