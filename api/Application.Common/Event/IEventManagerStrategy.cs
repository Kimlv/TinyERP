namespace App.Common.Event
{
    public interface IEventManagerStrategy
    {
        void Publish<TEventType>(TEventType eventType) where TEventType : IEvent;
    }
}
