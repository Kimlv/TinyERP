namespace App.Common.Event.Strategy
{
    using DI;
    using Helpers;
    using MessageBus;
    using System;

    public class ExternalEventHandlerStategy : IEventHandlerStrategy
    {
        public void Publish<TEventType>(TEventType ev) where TEventType : IEvent
        {
            try
            {
                string jsonContent = JsonHelper.ToJson(ev);
                MessageBusEvent eventItem = new MessageBusEvent(ObjectHelper.GetClassName(ev), jsonContent);
                IMessageBusProvider mbProvider = IoC.Container.Resolve<IMessageBusProvider>();
                mbProvider.Send(eventItem);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("common.event.handlerTypeForEventIsRequired", ex);
            }
        }
    }
}