namespace App.Common.Tasks.CORS
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Routing;
    public class ConfigCORS : BaseTask<TaskArgument<RouteCollection>>, IRouteConfiguredTask
    {
        public ConfigCORS() : base(ApplicationType.MVC | ApplicationType.WebApi | ApplicationType.ExternalWebApi | ApplicationType.MessageBus)
        {
            this.Order = 100;
        }

        public override void Execute(TaskArgument<RouteCollection> arg)
        {
            //return;
            if (!this.IsValid(arg.Type)) { return; }
            HttpConfiguration config = GlobalConfiguration.Configuration;
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
        }
    }
}
