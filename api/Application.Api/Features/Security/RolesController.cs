namespace App.Api.Features.Security
{
    using App.Common.DI;
    using App.Common.MVC;
    using App.Common.MVC.Attributes;
    using App.Service.Security;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [ResponseWrapper()]
        public IList<RoleListItemSummary> GetRoles()
        {
            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            IList<RoleListItemSummary> roles = roleService.GetRoles();
            return roles;
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseWrapper()]
        public GetRoleResponse GetRole(Guid id)
        {
            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            GetRoleResponse role = roleService.Get(id);
            return role;
        }

        [HttpPost]
        [Route("")]
        [ResponseWrapper()]
        public CreateRoleResponse CreateRole(CreateRoleRequest request)
        {
            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            CreateRoleResponse role = roleService.Create(request);
            return role;
        }

        [HttpPut]
        [Route("{id}")]
        [ResponseWrapper()]
        public void UpdateRole(Guid id, UpdateRoleRequest request)
        {
            request.Id = id;
            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            roleService.Update(request);
        }

        [HttpDelete]
        [Route("{id}")]
        [ResponseWrapper()]
        public void DeleteRole(Guid id)
        {
            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            roleService.Delete(id);
        }
    }
}
