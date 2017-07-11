namespace App.Service.Security
{
    using App.Common.Data;
    using App.Common.Mapping;
    using App.Entity.Security;
    using System;
    using System.Collections.Generic;

    public class CreateUserGroupRequest : BaseContent, IMappedFrom<App.Entity.Security.UserGroup>
    {
        public IList<Guid> PermissionIds { get; set; }
        public CreateUserGroupRequest() : base()
        {
            this.PermissionIds = new List<Guid>();
        }
    }
}
