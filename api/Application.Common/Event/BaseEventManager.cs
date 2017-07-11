namespace App.Common.Event
{
    public class BaseEventManager : IEventManager
    {
        public void Publish<TEventType>(TEventType eventType) where TEventType : IEvent
        {
            App.Common.Helpers.AssemblyHelper.ExecuteTasks<IEventHandler<TEventType>, TEventType>(eventType);
        }
    }
}