namespace App.Common.Helpers
{
    using System;

    public class TokenHelper
    {
        public static AuthenticationToken CreateNewAuthenticationToken(string token = "")
        {
            return new AuthenticationToken(String.IsNullOrWhiteSpace(token) ? Guid.NewGuid() : new Guid(token), DateTimeHelper.GetAuthenticationTokenExpiredUtcDateTime());
        }
    }
}