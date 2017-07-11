namespace App.Service.Security.Permission
{
    using App.Common.Validation.Attribute;

    public class CreatePermissionRequest
    {
        [Required("security.addOrUpdatePermission.validation.nameIsRequire")]
        public string Name { get; set; }

        [Required("security.addOrUpdatePermission.validation.keyIsRequire")]
        public string Key { get; set; }

        public string Description { get; set; }

        public CreatePermissionRequest(string name, string key, string desc)
        {
            this.Name = name;
            this.Key = key;
            this.Description = desc;
        }

        public CreatePermissionRequest()
        {
        }
    }
}