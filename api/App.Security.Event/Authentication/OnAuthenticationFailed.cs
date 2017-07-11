namespace App.Security.Event.Authentication
{
    using App.Common.Event;
    using System;

    public class OnAuthenticationFailed : BaseEvent
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime At { get; set; }
        public OnAuthenticationFailed(string userName, string pwd, DateTime at) : base()
        {
            this.UserName = userName;
            this.Password = pwd;
            this.At = at;
        }
    }
}
