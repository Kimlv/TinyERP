namespace App.Order.Event
{
    using Common.DI;
    using ValueObject.Order;
    using Common.Data;
    using Common;
    using App.Order.Query;

    internal class OrderEventHandler : IOrderEventHandler
    {
        public void Execute(OnOrderActivated ev)
        {
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MongoDb))
            {
                IOrderQuery query = IoC.Container.Resolve<IOrderQuery>(uow);
                App.Order.Query.Entity.Order order = query.GetByOrderId(ev.OrderId);
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
                query.Add(new App.Order.Query.Entity.Order(ev.OrderId));
                uow.Commit();
            }
        }

        public void Execute(OnOrderLineItemAdded ev)
        {
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MongoDb))
            {
                IOrderQuery query = IoC.Container.Resolve<IOrderQuery>(uow);
                App.Order.Query.Entity.Order order = query.GetByOrderId(ev.OrderId);
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
                App.Order.Query.Entity.Order order = query.GetByOrderId(ev.OrderId);
                order.Name = ev.CustomerName;
                query.Update(order);
                uow.Commit();
            }
        }
    }
}
