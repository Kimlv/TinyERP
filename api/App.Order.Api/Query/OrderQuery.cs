namespace App.Order.Query
{
    using App.Common.Data;
    using System.Collections.Generic;
    using Common.Mapping;
    using Common;
    using System;
    using System.Linq;
    internal class OrderQuery : BaseQueryRepository<App.Order.Query.Entity.Order>, IOrderQuery
    {
        public OrderQuery(IUnitOfWork uow) : base(new DbContextOption(IOMode.Write, uow.RepositoryType, context: uow.Context)) { }
        public OrderQuery() : base(RepositoryType.MongoDb) { }

        public App.Order.Query.Entity.Order GetByOrderId(Guid orderId)
        {
            return this.DbSet.AsQueryable().FirstOrDefault(item => item.OrderId == orderId);
        }

        public TEntity GetOrder<TEntity>(string id) where TEntity : IMappedFrom<App.Order.Query.Entity.Order>
        {
            return this.GetById<TEntity>(id);
        }
        public IList<TEntity> GetOrders<TEntity>() where TEntity : IMappedFrom<App.Order.Query.Entity.Order>
        {
            return this.GetItems<TEntity>();
        }
    }
}
