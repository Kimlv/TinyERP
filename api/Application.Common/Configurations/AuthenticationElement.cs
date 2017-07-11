namespace App.Common.Configurations
{
    using System.Configuration;

    public class AuthenticationElement : ConfigurationElement
    {
        [ConfigurationProperty("tokenExpiredAfterInMinute")]
        public double TokenExpiredAfterInMinute
        {
            get { return (double)this["tokenExpiredAfterInMinute"]; }
        }

        [ConfigurationProperty("authType")]
        public AuthType AuthType
        {
            get
            {
                return (AuthType)this["authType"];
            }
        }

        [ConfigurationProperty("path")]
        public string Path {
            get {
                return (string)this["path"];
            }
        }

        [ConfigurationProperty("allowOrigins")]
        public string AllowOrigins {
            get {
                return (string)this["allowOrigins"];
            }
        }
    }
}
