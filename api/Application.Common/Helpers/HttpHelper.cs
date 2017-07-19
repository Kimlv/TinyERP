namespace App.Common.Helpers
{
    using System;
    using System.Web;
    using App.Common.Tasks;

    public class HttpHelper
    {
        public static void AddCookie(string key, string value)
        {
            var cookieItem = new HttpCookie(key, value);
            HttpContext.Current.Response.Cookies.Add(cookieItem);
        }

        public static string GetCookie(string key)
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(key);
            return cookie != null ? cookie.Value : string.Empty;
        }

        public static void ClearCookie(string key)
        {
            HttpContext.Current.Request.Cookies.Remove(key);
        }

        public static void Redirect(string url, bool endResponse)
        {
            HttpContext.Current.Response.Redirect(url, endResponse);
        }

        internal static string GetRootUri(HttpApplication app)
        {
            return app.Request.Url.Host;
        }

        public static string Resolve(string url)
        {
            return HttpContext.Current == null ? System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, url) : HttpContext.Current.Server.MapPath(url);
        }
    }
}