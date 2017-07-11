namespace App.Security.Repository.Impl
{
    using App.Common.DI;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All) { }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterTransient<App.Security.Repository.IUserRepository, App.Security.Repository.Impl.UserRepository>();
        }
    }
}