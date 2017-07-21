namespace App.Common.Helpers
{
    using App.Common.Event;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public class EventHelper
    {
        public static IList<EventRegistration> GetSubcriberRequests(IList<Type> handlers)
        {
            IList<EventRegistration> requests = new List<EventRegistration>();
            foreach (Type handler in handlers)
            {
                string handlerPrefix = UriHelper.GetBaseUri(handler);
                IList<EventRegistration> requestsOfHandler = EventHelper.GetSubcriberRequests(handlerPrefix, handler);
                requests = requests.Concat(requestsOfHandler).ToList();
            }
            return requests;
        }

        private static IList<EventRegistration> GetSubcriberRequests(string baseUri, Type handler)
        {
            IList<EventRegistration> registrations = handler.GetMethods()
                .Where(method => method.IsDefined(typeof(RouteAttribute), true))
                .Select(method => new EventRegistration(
                    method.GetParameters().FirstOrDefault().ParameterType.FullName,
                    ((RouteAttribute)method.GetCustomAttributes(typeof(RouteAttribute), true).FirstOrDefault()).Template

                ))
                .ToList();
            registrations = registrations.Select(item => new EventRegistration(
                item.EventClassName,
                String.Format("{0}/{1}", baseUri, item.Uri)
                ))
                .ToList();
            return registrations;
        }
    }
}
