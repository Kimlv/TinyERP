namespace App.Service.Security.User
{
    using App.Common.Validation.Attribute;
    public class CreateUserRequest
    {
        public string Email { get; set; }
        [Required("security.addOrUpdateUser.validation.firstNameIsRequire")]
        public string FirstName { get; set; }
        [Required("security.addOrUpdateUser.validation.lastNameIsRequire")]
        public string LastName { get; set; }
    }
}