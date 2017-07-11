namespace App.Service.Security.Permission
{
    using App.Common.Validation.Attribute;
    using System;

    public class UpdatePermissionRequest
    {
        public UpdatePermissionRequest(Guid id, string name, string key, string desc)
        {
            this.Id = id;
            this.Name = name;
            this.Key = key;
            this.Description = desc;
        }

        [Required("common.validation.invalidRequest")]
        public Guid Id { get; set; }
        [Required("security.addPermission.validation.nameIsRequire")]
        public string Name { get; set; }
        [Required("security.addPermission.validation.keyIsRequire")]
        public string Key { get; set; }
        public string Description { get; set; }
    }
}
