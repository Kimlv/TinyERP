namespace App.Order.Repository
{
    using App.Common;
    using App.Common.Data;
    using Context;

    public class OrderRepository : BaseCommandRepository<App.Order.Aggregate.OrderAggregate>, IOrderRepository
    {
        public OrderRepository() : base(new OrderDbContext(IOMode.Read)) { }
        public OrderRepository(IUnitOfWork uow) : base(uow.Context) { }
    }
}
