namespace App.Repository.Order
{
    using App.Common.Data;
    public interface IOrderRepository : IBaseCommandRepository<App.Aggregate.Order.OrderAggregate>{}
}
