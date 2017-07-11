namespace App.Service.Security.Permission
{
    using App.Common.Data;
    using App.Common.Mapping;

    public class GetPermissionResponse : BaseContent, IMappedFrom<App.Entity.Security.Permission>
    {
        public GetPermissionResponse() : base()
        {
        }
    }
}
