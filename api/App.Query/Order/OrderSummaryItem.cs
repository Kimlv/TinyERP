namespace App.Query.Order
{
    using App.Common.Mapping;
    using Common.Data.MongoDB;
    using System;
    public class OrderSummaryItem :
        BaseMongoDbEntity,
        IMappedFrom<App.Query.Entity.Order.Order>
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
