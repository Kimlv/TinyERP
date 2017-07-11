namespace App.Security.Owin.UserNamePwd
{
    using Common;
    using Microsoft.Owin.Security;
    internal class UserNamePwdAuthOptions : AuthenticationOptions
    {
        public UserNamePwdAuthOptions() : base(Constants.AUTHENTICATION_TOKEN) { }
    }
}
