namespace App.Repository.Impl.Order
{
    using App.Common;
    using App.Common.Data;
    using App.Common.Data.MSSQL;
    using Context;
    using Repository.Order;

    public class OrderRepository : BaseCommandRepository<App.Aggregate.Order.OrderAggregate>, IOrderRepository
    {
        public OrderRepository() : base(new AppDbContext(IOMode.Read)) { }
        public OrderRepository(IUnitOfWork uow) : base(uow.Context) { }
    }
}
