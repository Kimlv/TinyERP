namespace App.Repository.Secutiry
{
    using App.Common.Data;
    using App.Common.Mapping;
    using App.Entity.Security;
    using System.Collections.Generic;

    public interface IPermissionRepository : IBaseContentRepository<Permission>
    {
        System.Collections.Generic.IList<TResult> GetPermissions<TResult>() where TResult : IMappedFrom<Permission>;
        System.Collections.Generic.IList<Permission> GetPermissions(IList<System.Guid> ids);
        IList<Permission> GetByRoleId(string roleId);
    }
}
