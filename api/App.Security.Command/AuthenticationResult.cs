namespace App.Security.Command
{
    using App.Common.Mapping;
    using App.Security.Aggregate;
    using System;
    public class AuthenticationResult : IMappedFrom<App.Security.Aggregate.User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return String.Format("{0} {1}", this.FirstName, this.LastName); } }
        public string Email { get; set; }
        public bool IsValid { get; set; }
        public string LoginToken { get; set; }
        public DateTime TokenExpiredAfter { get; set; }
        public Roles Roles { get; set; }
        public AuthenticationResult(bool isValid = false)
        {
            this.IsValid = isValid;
            this.LoginToken = string.Empty;
            this.TokenExpiredAfter = DateTime.UtcNow;
        }
    }
}
