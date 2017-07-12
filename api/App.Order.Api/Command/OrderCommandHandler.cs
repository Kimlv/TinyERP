namespace App.Order.Command
{
    using Common.Aggregate;
    using Common.Data;
    using Common.DI;
    using Common.Command;
    using App.Order.Aggregate;
    using App.Order.Repository;

    internal class OrderCommandHandler : BaseCommandHandler, IOrderCommandHandler
    {
        public void Handle(ActivateOrder command)
        {
            using (IUnitOfWork uow = this.CreateUnitOfWork<OrderAggregate>())
            {
                IOrderRepository repository = IoC.Container.Resolve<IOrderRepository>(uow);
                OrderAggregate order = repository.GetById(command.OrderId.ToString(), "OrderLines");
                order.Activate();
                repository.Update(order);
                uow.Commit();
                order.PublishEvents();
            }
        }

        public void Handle(AddOrderLineRequest command)
        {
            using (IUnitOfWork uow = this.CreateUnitOfWork<OrderAggregate>())
            {
                IOrderRepository repository = IoC.Container.Resolve<IOrderRepository>(uow);
                OrderAggregate order = repository.GetById(command.OrderId.ToString(), "OrderLines");
                order.AddOrderLineItem(command.ProductId, command.ProductName, command.Quantity, command.Price);
                repository.Update(order);
                uow.Commit();
                order.PublishEvents();
            }
        }

        public void Handle(CreateOrderRequest command)
        {

            OrderAggregate order = AggregateFactory.Create<OrderAggregate>();
            order.AddCustomerDetail(command.CustomerDetail);
            order.AddOrderLineItems(command.OrderLines);
            using (IUnitOfWork uow = this.CreateUnitOfWork<OrderAggregate>())
            {
                IOrderRepository repository = IoC.Container.Resolve<IOrderRepository>(uow);
                repository.Add(order);
                uow.Commit();
                order.AddEvent(new App.Order.Event.OnOrderCreated(order.Id));
            }
            order.PublishEvents();
        }
    }
}
