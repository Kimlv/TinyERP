namespace App.Common.Tasks.EventSubcriber
{
    public class RegisterRemoteEventSubcriberTask : BaseTask<TaskArgument<System.Web.HttpApplication>>, IApplicationStartedTask<TaskArgument<System.Web.HttpApplication>>
    {
        public RegisterRemoteEventSubcriberTask() : base(ApplicationType.All)
        {
        }

        public override void Execute(TaskArgument<System.Web.HttpApplication> arg)
        {
            if (!this.IsValid(arg.Type)) { return; }
        }
    }
}
