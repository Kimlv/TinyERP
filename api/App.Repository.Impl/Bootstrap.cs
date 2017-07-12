namespace App.Repository.Impl
{
    using App.Common.DI;
    using App.Repository.Impl.Security;
    using App.Repository.Security;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All) { }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterTransient<IUserRepository, UserRepository>();
            context.RegisterTransient<Repository.Common.ILanguageRepository, App.Repository.Impl.Common.LanguageRepository>();
            context.RegisterTransient<Repository.Secutiry.IRoleRepository, App.Repository.Impl.Security.RoleRepository>();
            context.RegisterTransient<Repository.Secutiry.IPermissionRepository, App.Repository.Impl.Security.PermissionRepository>();
            context.RegisterTransient<Repository.Secutiry.IUserGroupRepository, App.Repository.Impl.Security.UserGroupRepository>();
            context.RegisterTransient<Repository.Common.IFileRepository, App.Repository.Impl.Common.FileRepository>();
            context.RegisterTransient<App.Repository.Setting.IContentTypeRepository, App.Repository.Impl.Setting.ContentTypeRepository>();
            context.RegisterTransient<App.Repository.Common.IParameterRepository, App.Repository.Impl.Common.ParameterRepository>();
            context.RegisterTransient<App.Repository.Support.IRequestRepository, App.Repository.Impl.Support.RequestRepository>();
            context.RegisterTransient<App.Repository.Inventory.ICategoryRepository, App.Repository.Impl.Inventory.CategoryRepository>();
            context.RegisterTransient<App.Repository.Inventory.IUnitOfMeasurementRepository, App.Repository.Impl.Inventory.UnitOfMeasurementRepository>();

            /*Order */
            //context.RegisterTransient<App.Repository.Order.IOrderRepository, App.Repository.Impl.Order.OrderRepository>();
        }
    }
}