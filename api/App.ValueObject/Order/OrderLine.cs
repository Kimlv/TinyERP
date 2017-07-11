namespace App.ValueObject.Order
{
    using App.Common.Data;
    using System;

    public class OrderLine : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderLine(){}
        public OrderLine(Guid productId, string productName,double quantity, decimal price)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Quantity = quantity;
            this.Price = price;
        }
    }
}
