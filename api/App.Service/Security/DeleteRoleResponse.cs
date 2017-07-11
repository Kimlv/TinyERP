namespace App.Service.Security
{
    using App.Common.Data;
    using App.Common.Mapping;
    using App.Entity.Security;

    public class DeleteRoleResponse : BaseContent, IMappedFrom<Role>
    {
    }
}
