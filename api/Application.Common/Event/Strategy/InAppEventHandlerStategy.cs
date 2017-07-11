namespace App.Common.Event.Strategy
{
    using DI;
    using Exception;
    using Helpers;
    using System;

    public class InAppEventHandlerStategy : IEventHandlerStrategy
    {
        public void Publish<TEventType>(TEventType ev) where TEventType : IEvent
        {
            try
            {
                object handler = IoC.Container.Resolve(ev.HandlerType);
                if (handler == null)
                {
                    throw new EventHandlerNotFound<TEventType>();
                }
                ObjectHelper.Invoke(handler, "Execute", ev);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("common.event.handlerTypeForEventIsRequired", ex);
            }
        }
    }
}