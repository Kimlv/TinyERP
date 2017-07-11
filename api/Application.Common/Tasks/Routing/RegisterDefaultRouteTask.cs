namespace App.Common.Tasks.Routing
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RegisterDefaultRouteTask : BaseTask<TaskArgument<RouteCollection>>, IRouteConfiguredTask
    {
        public RegisterDefaultRouteTask() : base(ApplicationType.MVC | ApplicationType.WebApi | ApplicationType.ExternalWebApi | ApplicationType.MessageBus)
        {
        }

        public override void Execute(TaskArgument<RouteCollection> arg)
        {
            if (!this.IsValid(arg.Type)) { return; }

            HttpConfiguration config = GlobalConfiguration.Configuration;
            //config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "users", id = RouteParameter.Optional });

            RouteCollection routes = arg.Data;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            
        }
    }
}
