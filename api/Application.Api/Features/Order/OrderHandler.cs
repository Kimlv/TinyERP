namespace App.Api.Features.Order
{
    using Command.Order;
    using App.Common.Command;
    using App.Common.MVC.Attributes;
    using System.Web.Http;
    using Aggregate.Order;
    using System;
    using System.Collections.Generic;
    using Query.Order;
    using App.Common.DI;

    /// <summary>
    /// See https://github.com/techcoaching/TinyERP/issues/77 to understand why this handles both query and command action
    /// </summary>
    [RoutePrefix("api/orders")]
    public class OrderHandler : CommandHandlerController<OrderAggregate>
    {
        [HttpGet()]
        [Route("")]
        [ResponseWrapper()]
        public IList<OrderSummaryItem> GetOrders()
        {
            IOrderQuery query = IoC.Container.Resolve<IOrderQuery>();
            return query.GetOrders<OrderSummaryItem>();
        }


        [HttpGet()]
        [Route("{orderId}")]
        [ResponseWrapper()]
        public OrderSummary GetOrder(string id)
        {
            IOrderQuery query = IoC.Container.Resolve<IOrderQuery>();
            return query.GetOrder<OrderSummary>(id);
        }

        [Route("")]
        [HttpPost()]
        [ResponseWrapper()]
        public void CreateOrder(CreateOrderRequest request)
        {
            this.Execute(request);
        }

        [Route("{orderId}/orderLines")]
        [HttpPost()]
        [ResponseWrapper()]
        public void AddOrderLine(Guid orderId, AddOrderLineRequest request)
        {
            request.OrderId = orderId;
            this.Execute(request);
        }

        [Route("{orderId}/activate")]
        [HttpPost()]
        [ResponseWrapper()]
        public void ActivateOrder(Guid orderId)
        {
            ActivateOrder request = new ActivateOrder(orderId);
            this.Execute(request);
        }
    }
}
