namespace App.Common.Tasks.Request
{
    using App.Common.Logging;
    using System.Web;

    public class RequestStarted : BaseTask<TaskArgument<HttpApplication>>, IApplicationRequestStartedTask<TaskArgument<HttpApplication>>
    {
        public RequestStarted() : base(ApplicationType.MVC | ApplicationType.WebApi | ApplicationType.MessageBus)
        {
        }

        public override void Execute(TaskArgument<HttpApplication> arg)
        {
            if (!this.IsValid(arg.Type)) { return; }
            new DefaultLogger().Info("New request started: {0}", arg.Data.Context.Request.RawUrl);
        }
    }
}