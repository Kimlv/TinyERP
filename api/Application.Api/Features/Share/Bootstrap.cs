namespace App.Api.Features.Share
{
    using App.Common.DI;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All)
        {
        }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterSingleton<App.Common.Authorize.IUserLoginAuthorization, App.Api.Features.Share.Authorize.UserLoginAuthorization>();
        }
    }
}