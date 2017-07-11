namespace App.Entity.Security
{
    using App.Common.Data;
    using System.Collections.Generic;

    public class Permission : BaseContent
    {
        public IList<Role> Roles { get; set; }
        public IList<UserGroup> UserGroups { get; set; }
        public Permission() : base()
        {
        }

        public Permission(string name, string key, string desc) : base(name, key, desc)
        {
        }
    }

    public enum PermissionField {
        Name,
        Key,
        Description
    }
}
