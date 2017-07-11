namespace App.MessageBus.Repository
{
    public interface IBusEventRepository: Common.Data.IBaseCommandRepository<Aggregate.BusEventAggregate>
    {
    }
}
