namespace App.Aggregate.Order
{
    using App.Common.Aggregate;
    using System.Collections.Generic;
    using Event.Order;
    using ValueObject.Order;
    using System;

    public class OrderAggregate : BaseAggregateRoot
    {
        public bool IsActivated { get; set; }
        public OrderCustomerDetail CustomerDetail { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
        public OrderAggregate()
        {
            this.OrderLines = new List<OrderLine>();
        }

        public void AddOrderLineItems(IList<App.Command.Order.OrderLine> orderLines)
        {
            foreach (App.Command.Order.OrderLine item in orderLines)
            {
                this.AddOrderLineItem(item.ProductId, item.ProductName, item.Quantity, item.Price);
            }
        }
        public void AddOrderLineItem(Guid productId, string productName, int quantity, decimal price)
        {
            OrderLine orderLine = new OrderLine(productId, productName, quantity, price);
            this.OrderLines.Add(orderLine);
            this.AddEvent(new OnOrderLineItemAdded(this.Id, productId, productName, quantity, price));
        }

        public void Activate()
        {
            this.IsActivated = true;
            this.AddEvent(new OnOrderActivated(this.Id));
        }

        public void AddCustomerDetail(App.Command.Order.CustomerDetail customerDetail)
        {
            this.CustomerDetail = new OrderCustomerDetail(customerDetail.Name);
            this.AddEvent(new OnCustomerDetailChanged(this.Id, customerDetail.Name));
        }

        
    }
}
