namespace App.Customer.Api
{
    using App.Common.DI;
    using App.Common.Event;
    using App.Common.Helpers;
    using App.Common.Logging;
    using App.Order.Event;
    using System.Web.Http;

    [RoutePrefix("api/customers")]
    public class CustomerHandler : RemoteEventSubcriberHandler
    {
        [Route("onOrderCreated")]
        [HttpPost]
        public bool OnOrderCreated(OnOrderCreated ev)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("new order created, detail:{0}", JsonHelper.ToJson(ev));
            return true;
        }
        [Route("onCustomerDetailChanged")]
        [HttpPost]
        public bool OnCustomerDetailChanged(OnCustomerDetailChanged ev)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("OnCustomerDetailChanged, detail:{0}", JsonHelper.ToJson(ev));
            return true;
        }

        [Route("onOrderLineItemAdded")]
        [HttpPost]
        public bool OnOrderLineItemAdded(OnOrderLineItemAdded ev)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("OnOrderLineItemAdded, detail:{0}", JsonHelper.ToJson(ev));
            return true;
        }

    }
}
