namespace App.Common.Event
{
    public interface IEventManager
    {
        void Publish<TEventType>(TEventType eventType) where TEventType : IEvent;
    }
}