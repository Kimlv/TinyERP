namespace App.Order.Event
{
    using App.Common.Event;
    using System;

    public class OnOrderLineItemAdded : BaseEvent
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OnOrderLineItemAdded(Guid orderId, Guid productId, string productName,int quantity, decimal price) : base()
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.ProductName = productName;
            this.Quantity = quantity;
            this.Price = price;
        }
    }
}
