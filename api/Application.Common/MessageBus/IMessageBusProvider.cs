namespace App.Common.MessageBus
{
    public interface IMessageBusProvider
    {
        void Send(MessageBusEvent eventItem);
    }
}
