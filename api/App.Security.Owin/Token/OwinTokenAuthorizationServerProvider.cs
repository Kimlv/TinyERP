namespace App.Security.Owin.Token
{
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.OAuth;
    using App.Security.Command;
    using System.Security.Claims;
    using App.Common.Command;
    using App.Security.Aggregate;
    using App.Security.Command.UserNameAndPwd;
    using App.Common;

    internal class OwinTokenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //ntext.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { Configuration.Current.Authentication.AllowOrigins });
            string userName = context.UserName;
            string password = context.Password;

            AuthenticationResult authorise = this.Authorise(userName, password);
            if (authorise == null || !authorise.IsValid) { return; }
            ClaimsIdentity claimsIdentity = App.Common.Helpers.SecurityHelper.CreateClaimIdentity(authorise.FullName, authorise.Email, authorise.TokenExpiredAfter, authorise.Roles.ToList(), context.Options.AuthenticationType);
            context.Validated(claimsIdentity);

        }

        private AuthenticationResult Authorise(string userName, string password)
        {
            ICommandHandlerStrategy commandHandlerStrategy = CommandHandlerStrategyFactory.Create<User>();
            UserNameAndPwdAuthenticationRequest request = new UserNameAndPwdAuthenticationRequest(userName, password);
            commandHandlerStrategy.Execute(request);
            return request.Result;
        }
    }
}
