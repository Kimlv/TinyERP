namespace App.Common.Validation
{
    public class AuthenticationException : ValidationException
    {
        public AuthenticationType Type { get; set; }

        public AuthenticationException(AuthenticationType authType, string key) : base(key)
        {
            this.Type = authType;
        }
    }
}