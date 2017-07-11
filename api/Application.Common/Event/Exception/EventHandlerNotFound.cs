namespace App.Common.Event.Exception
{
    using App.Common.Validation;
    public class EventHandlerNotFound<TEventType> : ValidationException where TEventType : IEvent
    {
        public EventHandlerNotFound():base(EventHandlerConst.EVENT_HANDLER_NOT_FOUND)
        {
        }
    }
}
