namespace App.Common.Event
{
    using App.Common.Configurations;
    using Configurations.EventHandler;
    using Strategy;
    using System.Collections.Generic;

    public class EventManagerStrategy : IEventManagerStrategy
    {
        public void Publish<TEventType>(TEventType ev) where TEventType : IEvent
        {
            IList<IEventHandlerStrategy> strategies = this.GetStrategiesHandler<TEventType>(ev);
            foreach (IEventHandlerStrategy strategy in strategies)
            {
                strategy.Publish<TEventType>(ev);
            }
        }

        private IList<IEventHandlerStrategy> GetStrategiesHandler<TEventType>(TEventType ev) where TEventType : IEvent
        {
            IList<IEventHandlerStrategy> strategies = new List<IEventHandlerStrategy>();
            strategies.Add(new InAppEventHandlerStategy());

            string className = ev.GetType().FullName;
            EventHandlerOption option = Configuration.Current.EventHandlers[className];
            if (option != null && option.Type == EventHandlerStategyType.External)
            {
                strategies.Add(new ExternalEventHandlerStategy());
            }
            return strategies;
        }
    }
}
