namespace App.Service.Security.User
{
    using App.Common.Mapping;
    using System;
    using App.Common.Data;

    public class UserQuickProfile : BaseEntity, IMappedFrom<App.Entity.Security.User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime LastLoggedInDate { get; set; }
        public string LanguageCode { get; set; }
        public UserQuickProfile(Entity.Security.User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.LastLoggedInDate = user.LastLoggedInDate;
        }
    }
}
