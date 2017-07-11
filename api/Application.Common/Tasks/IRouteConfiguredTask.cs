namespace App.Common.Tasks
{
    using System.Web.Routing;

    public interface IRouteConfiguredTask : IBaseTask<TaskArgument<RouteCollection>>
    {
    }
}