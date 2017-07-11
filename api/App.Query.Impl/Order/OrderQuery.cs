namespace App.Query.Impl.Order
{
    using App.Common.Data;
    using App.Query.Order;
    using App.Query.Entity.Order;
    using System.Collections.Generic;
    using Common.Mapping;
    using Common;
    using System;
    using System.Linq;
    internal class OrderQuery : BaseQueryRepository<Order>, IOrderQuery
    {
        public OrderQuery(IUnitOfWork uow) : base(new DbContextOption(IOMode.Write, uow.RepositoryType, context: uow.Context)) { }
        public OrderQuery() : base(RepositoryType.MongoDb) { }

        public Order GetByOrderId(Guid orderId)
        {
            return this.DbSet.AsQueryable().FirstOrDefault(item => item.OrderId == orderId);
        }

        public TEntity GetOrder<TEntity>(string id) where TEntity : IMappedFrom<Order>
        {
            return this.GetById<TEntity>(id);
        }
        public IList<TEntity> GetOrders<TEntity>() where TEntity : IMappedFrom<Order>
        {
            return this.GetItems<TEntity>();
        }
    }
}
