namespace App.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    public class SecurityHelper
    {
        public static ClaimsIdentity CreateClaimIdentity(string fullName, string email, DateTime tokenExpiredAfter, IList<string> roles, string authType = "custom")
        {
            IList<Claim> claimCollection = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Expired, tokenExpiredAfter.ToString()),
                };
            foreach (string roleName in roles)
            {
                claimCollection.Add(new Claim(ClaimTypes.Role, roleName));
            }

            return new ClaimsIdentity(claimCollection, authType);
        }
    }
}
