namespace App.Common.Tasks.Request
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Routing;
    public class ConfigCustomControllerResolver : BaseTask<TaskArgument<RouteCollection>>, IRouteConfiguredTask
    {
        public ConfigCustomControllerResolver() : base(ApplicationType.MVC | ApplicationType.WebApi | ApplicationType.MessageBus | ApplicationType.ExternalWebApi)
        {
            this.Order = (int)TaskPriority.Medium;
        }
        public override void Execute(TaskArgument<RouteCollection> arg)
        {
            if (!this.IsValid(arg.Type)) { return; }
            GlobalConfiguration.Configure((HttpConfiguration config) =>
            {
                config.Services.Replace(typeof(IHttpControllerTypeResolver), new App.Common.MVC.Resolver.HttpControllerTypeResolver());
                config.MapHttpAttributeRoutes();
            });
        }
    }
}
