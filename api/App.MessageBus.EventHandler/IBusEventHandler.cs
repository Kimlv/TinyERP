namespace App.MessageBus.EventHandler
{
    using App.Common.Event;
    using App.MessageBus.Event.MessageBus;

    public interface IBusEventHandler : IEventHandler<OnMessageBusCreated>
    {
    }
}
