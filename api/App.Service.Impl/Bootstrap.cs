namespace App.Service.Impl
{
    using App.Common.DI;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All)
        {
        }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterSingleton<App.Service.Security.User.IUserService, App.Service.Impl.Security.UserService>();
            context.RegisterSingleton<App.Service.Common.ILanguageService, App.Service.Impl.Common.LanguageService>();
            context.RegisterSingleton<App.Service.Security.IRoleService, App.Service.Impl.Security.RoleService>();
            context.RegisterSingleton<App.Service.Security.Permission.IPermissionService, App.Service.Impl.Security.PermissionService>();
            context.RegisterSingleton<App.Service.Security.IUserGroupService, App.Service.Impl.Security.UserGroupService>();
            context.RegisterSingleton<App.Service.Common.File.IFileService, App.Service.Impl.Common.FileService>();
            context.RegisterSingleton<App.Service.Setting.IContentTypeService, App.Service.Impl.Setting.ContentTypeService>();
            context.RegisterSingleton<App.Service.Support.IRequestService, App.Service.Impl.Support.RequestService>();
            context.RegisterSingleton<App.Service.Inventory.ICategoryService, App.Service.Impl.Inventory.CategoryService>();
            context.RegisterSingleton<App.Service.Inventory.IUnitOfMeasurementService, App.Service.Impl.Inventory.UnitOfMeasurementService>();
        }
    }
}
