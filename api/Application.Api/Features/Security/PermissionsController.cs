namespace App.Api.Features.Security
{
    using App.Common.MVC;
    using App.Common.DI;
    using App.Common.MVC.Attributes;
    using App.Service.Security;
    using App.Service.Security.Permission;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using App.Common;

    [RoutePrefix("api/permissions")]
    public class PermissionsController : BaseApiController
    {
        //[Authorize(Roles = SecurityRoleType.Administrator)]
        [HttpGet]
        [Route("")]
        [ResponseWrapper()]
        public IList<PermissionAsKeyNamePair> GetPermissions()
        {
            IPermissionService permissionService = IoC.Container.Resolve<IPermissionService>();
            IList<PermissionAsKeyNamePair> pers = permissionService.GetPermissions();
            return pers;
        }
        //[Authorize()]
        [HttpGet]
        [Route("{id}")]
        [ResponseWrapper()]
        public GetPermissionResponse GetPermission([FromUri]Guid id)
        {
            IPermissionService permissionService = IoC.Container.Resolve<IPermissionService>();
            GetPermissionResponse per = permissionService.GetPermission(id);
            return per;
        }

        [HttpPost]
        [Route("")]
        [ResponseWrapper()]
        public CreatePermissionResponse CreatePermission(CreatePermissionRequest request)
        {
            IPermissionService permissionService = IoC.Container.Resolve<IPermissionService>();
            CreatePermissionResponse per = permissionService.Create(request);
            return per;
        }

        [HttpPut]
        [Route("{id}")]
        [ResponseWrapper()]
        public void UpdatePermission([FromUri]Guid id, UpdatePermissionRequest request)
        {
            request.Id = id;
            IPermissionService permissionService = IoC.Container.Resolve<IPermissionService>();
            permissionService.Update(request);
        }

        [HttpDelete]
        [Route("{id}")]
        [ResponseWrapper()]
        public void DeletePermission([FromUri]Guid id)
        {
            IPermissionService permissionService = IoC.Container.Resolve<IPermissionService>();
            permissionService.Delete(id);
        }
    }
}