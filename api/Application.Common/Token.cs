namespace App.Common
{
    using System;

    public class AuthenticationToken
    {
        public string Value { get; set; }
        public DateTime ExpiredAfter { get; set; }
        public AuthenticationToken(Guid value, DateTime expiredAfter)
        {
            this.Value = value.ToString();
            this.ExpiredAfter = expiredAfter;
        }

        public AuthenticationToken(string value)
        {
            this.Value = value;
            this.ExpiredAfter = DateTime.Now;
        }
    }
}