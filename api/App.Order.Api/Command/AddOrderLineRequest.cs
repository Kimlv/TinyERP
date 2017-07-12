namespace App.Order.Command
{
    using App.Common.Command;
    using System;

    public class AddOrderLineRequest : BaseCommand
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
