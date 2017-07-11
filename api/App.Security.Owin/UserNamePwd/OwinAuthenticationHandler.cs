namespace App.Security.Owin.UserNamePwd
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Infrastructure;
    using System.Security.Claims;
    using Microsoft.Owin;
    using Common;
    using System.Linq;
    using Common.Command;
    using Aggregate;
    using Command;
    using App.Security.Command.UserNameAndPwd;

    internal class OwinAuthenticationHandler : AuthenticationHandler<UserNamePwd.UserNamePwdAuthOptions>
    {
        protected async override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationResult authorise = this.Authorise(this.Request.Headers);
            if (authorise == null || !authorise.IsValid)
            {
                return null;
            }
            AuthenticationProperties authProperties = new AuthenticationProperties();
            authProperties.IssuedUtc = DateTime.UtcNow;
            authProperties.ExpiresUtc = authorise.TokenExpiredAfter;
            authProperties.AllowRefresh = true;
            authProperties.IsPersistent = true;
           ClaimsIdentity claimsIdentity = App.Common.Helpers.SecurityHelper.CreateClaimIdentity(authorise.FullName, authorise.Email, authorise.TokenExpiredAfter, authorise.Roles.ToList());
            AuthenticationTicket ticket = new AuthenticationTicket(claimsIdentity, authProperties);
            return ticket;
        }

        private AuthenticationResult Authorise(IHeaderDictionary headers)
        {
            string[] acceptLanguageValues;
            bool acceptLanguageHeaderPresent = headers.TryGetValue(Constants.AUTHENTICATION_TOKEN, out acceptLanguageValues);
            if (!acceptLanguageHeaderPresent)
            {
                return null;
            }
            string[] elementsInHeader = acceptLanguageValues.ToList()[0].Split(new string[] { Constants.AUTHENTICATION_TOKEN_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);
            string userName = elementsInHeader[0];
            string pwd = elementsInHeader[1];

            ICommandHandlerStrategy commandHandlerStrategy = CommandHandlerStrategyFactory.Create<User>();
            UserNameAndPwdAuthenticationRequest request = new UserNameAndPwdAuthenticationRequest(userName, pwd);
            commandHandlerStrategy.Execute(request);
            return request.Result;
        }
    }
}
