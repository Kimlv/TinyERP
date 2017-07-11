namespace App.Api.Features.Share.Tasks.Security
{
    using App.Common.Tasks;
    using System.Collections.Generic;
    using System.Web;
    using App.Common;
    using App.Common.DI;
    using Service.Security.Permission;

    public class CreatePermissionTask : BaseTask<TaskArgument<System.Web.HttpApplication>>, IApplicationReadyTask<TaskArgument<System.Web.HttpApplication>>
    {
        public CreatePermissionTask() : base(ApplicationType.All)
        {
        }

        public override void Execute(TaskArgument<HttpApplication> context)
        {
            IPermissionService perService = IoC.Container.Resolve<IPermissionService>();
            IList<CreatePermissionRequest> pers = this.GetPermissions();
            perService.CreateIfNotExist(pers);
        }

        private IList<CreatePermissionRequest> GetPermissions()
        {
            return new List<CreatePermissionRequest>()
            {
                new CreatePermissionRequest("View User", "common.permissions.user.view", "common.permissions.user.viewDesc"),
                new CreatePermissionRequest("Edit User", "common.permissions.user.edit", "common.permissions.user.editDesc"),
                new CreatePermissionRequest("Add User", "common.permissions.user.add", "common.permissions.user.addDesc"),
            };
        }
    }
}