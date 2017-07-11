namespace App.Common.Tasks.Error
{
    using System;
    using App.Common.Logging;
    using App.Common.Extensions;

    public class UnHandledError : BaseTask<TaskArgument<System.Web.HttpApplication>>, IUnHandledErrorTask<TaskArgument<System.Web.HttpApplication>>
    {
        private ILogger logger;

        public UnHandledError() : base(ApplicationType.MVC | ApplicationType.WebApi | ApplicationType.MessageBus)
        {
            this.logger = new DefaultLogger();
        }

        public override void Execute(TaskArgument<System.Web.HttpApplication> arg)
        {
            if (!this.IsValid(arg.Type) || arg.Data.Context.Error == null) { return; }
            this.logger.Error(arg.Data.Context.Error.ToString());
        }
    }
}