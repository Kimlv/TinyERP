namespace App.Service.Security.User
{
    using App.Common.Mapping;
    using System;
    public class CreateUserResponse: IMappedFrom<App.Entity.Security.User>
    {
        public Guid Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}