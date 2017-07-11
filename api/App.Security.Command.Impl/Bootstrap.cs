namespace App.Security.Command.Impl
{
    using App.Common.DI;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All) { }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<App.Security.Command.UserNameAndPwd.UserNameAndPwdAuthenticationRequest>, App.Security.Command.Impl.UserNameAndPwdAuthCommandHandler>("UserNameAndPwdAuthenticationRequest");
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<App.Security.Command.CreateUserCommand>, App.Security.Command.Impl.SecurityCommandHandler>("CreateUserCommand");
        }
    }
}