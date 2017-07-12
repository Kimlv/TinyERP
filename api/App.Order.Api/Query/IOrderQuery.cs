namespace App.Order.Query
{
    using Common.Data;
    using Common.Mapping;
    using System.Collections.Generic;
    using System;

    public interface IOrderQuery: IBaseQueryRepository<App.Order.Query.Entity.Order>
    {
        IList<TEntity> GetOrders<TEntity>() where TEntity : IMappedFrom<App.Order.Query.Entity.Order>;
        TEntity GetOrder<TEntity>(string id) where TEntity : IMappedFrom<App.Order.Query.Entity.Order>;
        App.Order.Query.Entity.Order GetByOrderId(Guid orderId);
    }
}
