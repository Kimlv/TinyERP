namespace App.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    public class UriHelper
    {
        public static IList<string> GetRelativeUries(IList<Type> handlers)
        {
            IList<string> uris = new List<string>();
            foreach (Type handler in handlers)
            {
                string baseUri = UriHelper.GetBaseUri(handler);
                IList<string> uriesOfHandler = UriHelper.GetUris(baseUri, handler);
                uris = uris.Concat(uriesOfHandler).ToList();
            }
            return uris;
        }

        public static string GetBaseUri(Type handler)
        {
            RoutePrefixAttribute routePrefix = (RoutePrefixAttribute)handler.GetCustomAttributes(typeof(RoutePrefixAttribute), true).FirstOrDefault();
            if (routePrefix == null)
            {
                throw new InvalidOperationException("Handler should have RoutePrefix attribute in their class declaration");
            }
            return routePrefix.Prefix;
        }

        private static IList<string> GetUris(string baseUri, Type handler)
        {
            IList<RouteAttribute> routes = handler.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Select(method => (RouteAttribute)method.GetCustomAttributes(typeof(RouteAttribute), true).FirstOrDefault())
                .ToList();
            IList<string> uriesOfHandler = new List<string>();
            foreach (RouteAttribute attr in routes)
            {
                uriesOfHandler.Add(String.Format("{0}/{1}", baseUri, attr.Template));
            }
            return uriesOfHandler;
        }
    }
}
