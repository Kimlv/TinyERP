namespace App.EventHandler.Impl.Order
{
    using Event.Order;
    using App.EventHandler.Order;
    using Common.DI;
    using Query.Order;
    using ValueObject.Order;
    using Common.Data;
    using Common;

    internal class OrderEventHandler : IOrderEventHandler
    {
        public void Execute(OnOrderActivated ev)
        {
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MongoDb))
            {
                IOrderQuery query = IoC.Container.Resolve<IOrderQuery>(uow);
                App.Query.Entity.Order.Order order = query.GetByOrderId(ev.OrderId);
                order.IsActivated = true;
                query.Update(order);
                uow.Commit();
            }
        }

        public void Execute(OnOrderCreated ev)
        {
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MongoDb))
            {
                IOrderQuery query = IoC.Container.Resolve<IOrderQuery>(uow);
                query.Add(new App.Query.Entity.Order.Order(ev.OrderId));
                uow.Commit();
            }
        }

        public void Execute(OnOrderLineItemAdded ev)
        {
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MongoDb))
            {
                IOrderQuery query = IoC.Container.Resolve<IOrderQuery>(uow);
                App.Query.Entity.Order.Order order = query.GetByOrderId(ev.OrderId);
                order.OrderLines.Add(new OrderLine(ev.ProductId, ev.ProductName, ev.Quantity, ev.Price));
                order.TotalItems += ev.Quantity;
                order.TotalPrice += ev.Price * (decimal)ev.Quantity;
                query.Update(order);
                uow.Commit();
            }
        }

        public void Execute(OnCustomerDetailChanged ev)
        {
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MongoDb))
            {
                IOrderQuery query = IoC.Container.Resolve<IOrderQuery>(uow);
                App.Query.Entity.Order.Order order = query.GetByOrderId(ev.OrderId);
                order.Name = ev.CustomerName;
                query.Update(order);
                uow.Commit();
            }
        }
    }
}
