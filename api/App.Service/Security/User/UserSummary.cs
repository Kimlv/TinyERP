namespace App.Service.Security.User
{
    using App.Common.Data;
    using App.Common.Mapping;
    public class UserSummary: BaseEntity, IMappedFrom<App.Entity.Security.User>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
