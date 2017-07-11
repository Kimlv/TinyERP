namespace App.Service.Security.UserGroup
{
    using App.Common.Data;
    using App.Common.Mapping;
    using System;
    using System.Collections.Generic;

    public class UpdateUserGroupRequest : BaseContent, IMappedFrom<App.Entity.Security.UserGroup>
    {
        public IList<Guid> PermissionIds { get; set; }
        public UpdateUserGroupRequest() : base()
        {
            this.PermissionIds = new List<Guid>();
        }
    }
}
