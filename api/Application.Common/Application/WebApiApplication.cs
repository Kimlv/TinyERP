namespace App.Common.Application
{
    using App.Common.Helpers;
    using App.Common.Tasks;
    using System.Web.Http;
    using System.Web.Routing;

    public class WebApiApplication : BaseApplication<System.Web.HttpApplication>
    {
        public WebApiApplication(System.Web.HttpApplication context)
            : this(context, ApplicationType.WebApi) { }

        protected WebApiApplication(System.Web.HttpApplication context, ApplicationType type)
            : base(context, type)
        {
            this.Context.BeginRequest += this.OnBeginRequest;
            this.Context.EndRequest += this.OnEndRequest;
            this.Context.Error += this.OnError;
        }

        public override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            this.OnRouteConfigured();
        }

        private void OnRouteConfigured()
        {
            TaskArgument<RouteCollection> taskArg = new TaskArgument<RouteCollection>(RouteTable.Routes, this.Type);
            AssemblyHelper.ExecuteTasks<IRouteConfiguredTask, TaskArgument<RouteCollection>>(taskArg, runTasksInOrder: true);
        }

        private void OnBeginRequest(object sender, System.EventArgs e)
        {
            this.OnApplicationRequestStarted();
        }

        private void OnEndRequest(object sender, System.EventArgs e)
        {
            this.OnApplicationRequestEnded();
        }

        private void OnError(object sender, System.EventArgs e)
        {
            this.OnUnHandledError();
        }
    }
}