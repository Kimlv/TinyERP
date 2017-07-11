namespace App.Api.Features.Order
{
    using System.Web.Http;
    using App.Common.MVC;
    using App.Common.MVC.Attributes;
    using Query.Order;
    using App.Common.DI;
    using System.Collections.Generic;

    [RoutePrefix("api/orders")]
    public class OrderQueryHandler : BaseApiController
    {
        [HttpGet()]
        [Route("")]
        [ResponseWrapper()]
        public IList<OrderSummaryItem> GetOrders()
        {
            IOrderQuery query = IoC.Container.Resolve<IOrderQuery>();
            return query.GetOrders<OrderSummaryItem>();
        }

    }
}
