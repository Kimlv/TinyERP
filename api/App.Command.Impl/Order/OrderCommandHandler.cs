namespace App.Command.Impl.Order
{
    using App.Command.Order;
    using Aggregate.Order;
    using Common.Aggregate;
    using Common.Data;
    using Repository.Order;
    using Common.DI;
    using Event.Order;
    using Common.Command;

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
                order.AddEvent(new OnOrderCreated(order.Id));
            }
            order.PublishEvents();
        }
    }
}
