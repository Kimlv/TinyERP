namespace App.MessageBus.Repository.Impl
{
    internal class BusEventRepository: App.Common.Data.BaseCommandRepository<Aggregate.BusEventAggregate>, IBusEventRepository
    {
        public BusEventRepository() : base(new Context.BusEvent.MessageBusDbContext(Common.IOMode.Read)) { }
        public BusEventRepository(Common.Data.IUnitOfWork uow) : base(uow.Context) { }
    }
}
