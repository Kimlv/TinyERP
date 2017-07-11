namespace App.Common.Event
{
    using App.Common.Tasks;

    public interface IEventHandler<TEventType> : IExecutable<TEventType> where TEventType : IEvent
    {
    }
}