namespace App.Api.Feature.Share.Tasks.Common
{
    using App.Common.Tasks;
    using System.Web.Routing;
    using App.Common;

    public class RegisterWebApiRouteTask : BaseTask<TaskArgument<RouteCollection>>, IRouteConfiguredTask
    {
        public RegisterWebApiRouteTask() : base(ApplicationType.MVC | ApplicationType.WebApi)
        {
        }

        public override void Execute(TaskArgument<RouteCollection> arg)
        {
            if (!this.IsValid(arg.Type)) { return; }
        }
    }
}
