namespace App.Security.Event.Authentication
{
    using App.Common.Event;
    using System;

    public class OnAuthenticationSuccess : BaseEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiredAfter { get; set; }
        public DateTime At { get; set; }
        public OnAuthenticationSuccess(string firstName, string lastName, string email, string token, DateTime expiredAfter, DateTime at) : base()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Token = token;
            this.TokenExpiredAfter = expiredAfter;
            this.At = at;
        }
    }
}
