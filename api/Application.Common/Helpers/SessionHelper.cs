namespace App.Common.Helpers
{
    using System.Web;

    public class SessionHelper
    {
        private const string CultureKey = "Culture";
        private const string RolesKey = "Roles";

        public static ExpectedType Get<ExpectedType>(string key)
        {
            if (HttpContext.Current.Session == null)
            {
                return default(ExpectedType);
            }

            object obj = HttpContext.Current.Session[key];
            if (obj == null)
            {
                return default(ExpectedType);
            }

            return (ExpectedType)obj;
        }
    }
}