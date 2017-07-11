namespace App.Service.Security.Permission
{
    using System;
    using System.Collections.Generic;

    public interface IPermissionService
    {
        IList<PermissionAsKeyNamePair> GetPermissions();
        void CreateIfNotExist(IList<CreatePermissionRequest> pers);
        CreatePermissionResponse Create(CreatePermissionRequest permission);
        void Delete(Guid id);
        GetPermissionResponse GetPermission(Guid id);
        void Update(UpdatePermissionRequest request);
    }
}
