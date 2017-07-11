namespace App.Api.Features.Common
{
    using App.Common.DI;
    public class Bootstrap: App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.WebApi) { }
        public override void Execute(IBaseContainer context) {
            context.RegisterSingleton<App.Common.Data.IDbContextResolver, DbContextResolver>();
        }
    }
}