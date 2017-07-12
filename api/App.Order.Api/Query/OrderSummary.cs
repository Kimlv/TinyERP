namespace App.Query.Order
{
    using App.Common.Data.MongoDB;
    using App.Common.Mapping;
    public class OrderSummary: BaseMongoDbEntity, IMappedFrom<App.Order.Query.Entity.Order>
    {
    }
}
