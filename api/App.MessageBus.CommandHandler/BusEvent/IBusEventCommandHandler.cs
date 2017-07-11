namespace App.MessageBus.CommandHandler.BusEvent
{
    using App.Common.Command;
    public interface IBusEventCommandHandler: 
        IBaseCommandHandler<CreateBusEventRequest>
    {
    }
}
