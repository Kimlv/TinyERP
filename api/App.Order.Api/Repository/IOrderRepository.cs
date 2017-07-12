namespace App.Order.Repository
{
    using App.Common.Data;
    public interface IOrderRepository : IBaseCommandRepository<App.Order.Aggregate.OrderAggregate>{}
}
