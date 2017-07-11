namespace App.Common.Event
{
    public interface IEventHandlerStrategy
    {
        void Publish<TEventType>(TEventType eventType) where TEventType : IEvent;
    }
}
