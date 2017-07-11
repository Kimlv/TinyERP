namespace App.Security.Owin.UserNamePwd
{
    using System;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Infrastructure;
    internal class UserNamePwdAuthMiddleware : AuthenticationMiddleware<UserNamePwdAuthOptions>
    {
        public UserNamePwdAuthMiddleware(OwinMiddleware next, UserNamePwdAuthOptions options) : base(next, options){}
        protected override AuthenticationHandler<UserNamePwdAuthOptions> CreateHandler()
        {
            return new OwinAuthenticationHandler();
        }
    }
}
