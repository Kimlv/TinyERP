namespace App.Customer.Api
{
    using App.Common.DI;
    using App.Common.Event;
    using App.Common.Logging;
    using App.Order.Event;
    using System.Web.Http;

    [RoutePrefix("api/customers")]
    public class CustomerHandler : RemoteEventSubcriberHandler
    {
        [Route("onOrderCreated")]
        [HttpPost]
        public void OnOrderCreated(OnOrderCreated ev)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("new order created, id:{0}", ev.OrderId);
        }

    }
}
