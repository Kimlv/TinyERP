namespace App.Query.Order
{
    using Common.Data;
    using Common.Mapping;
    using System.Collections.Generic;
    using Entity.Order;
    using System;

    public interface IOrderQuery: IBaseQueryRepository<App.Query.Entity.Order.Order>
    {
        IList<TEntity> GetOrders<TEntity>() where TEntity : IMappedFrom<App.Query.Entity.Order.Order>;
        TEntity GetOrder<TEntity>(string id) where TEntity : IMappedFrom<App.Query.Entity.Order.Order>;
        Order GetByOrderId(Guid orderId);
    }
}
