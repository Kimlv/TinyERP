namespace App.MessageBus.Service
{
    public interface IMessageBusEventService
    {
        App.Common.MessageBus.CreateMessageBusEventResponse Create(App.Common.MessageBus.MessageBusEvent ev);
    }
}
