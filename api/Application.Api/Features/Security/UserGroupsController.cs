namespace App.Api.Features.Security
{
    using App.Common.DI;
    using App.Common.MVC;
    using App.Common.MVC.Attributes;
    using App.Service.Security;
    using App.Service.Security.UserGroup;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [RoutePrefix("api/usergroups")]
    public class UserGroupsController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [ResponseWrapper()]
        public IList<UserGroupListItemSummary> GetUserGroups()
        {
            IUserGroupService userGroupService = IoC.Container.Resolve<IUserGroupService>();
            IList<UserGroupListItemSummary> userGroups = userGroupService.GetUserGroups();
            return userGroups;
        }

        [HttpGet]
        [Route("{id}")]
        [ResponseWrapper()]
        public GetUserGroupResponse GetRole(Guid id)
        {
            IUserGroupService service = IoC.Container.Resolve<IUserGroupService>();
            return service.Get(id);
        }

        [HttpPost]
        [Route("")]
        [ResponseWrapper()]
        public CreateUserGroupResponse CreateUserGroup(CreateUserGroupRequest request)
        {
            IUserGroupService userGroupService = IoC.Container.Resolve<IUserGroupService>();
            return userGroupService.Create(request);
        }

        [HttpDelete]
        [Route("{id}")]
        [ResponseWrapper()]
        public void DeleteRole(Guid id)
        {
            IUserGroupService service = IoC.Container.Resolve<IUserGroupService>();
            service.Delete(id);
        }

        [HttpPut]
        [Route("{id}")]
        [ResponseWrapper()]
        public void UpdateUserGroup(Guid id, UpdateUserGroupRequest request)
        {
            request.Id = id;
            IUserGroupService roleService = IoC.Container.Resolve<IUserGroupService>();
            roleService.Update(request);
        }
    }
}
