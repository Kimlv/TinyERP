namespace App.Query.Entity.Order
{
    using Common.Data.MongoDB;
    using System;

    public class OrderLine : BaseMongoDbEntity
    {
        public Guid OrderId { get; set; }
        private decimal Price { get; set; }

        public OrderLine(Guid orderId, decimal price)
        {
            this.OrderId = orderId;
            this.Price = price;
        }
    }
}
