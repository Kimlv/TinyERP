namespace App.Common.Authorize
{
    using App.Common.Validation;

    public class UnAuthorizedException : AuthenticationException
    {
        public UnAuthorizedException(AuthenticationType authType, string key)
            : base(authType, key)
        {
        }
    }
}
