namespace App.Common.Event
{
    using App.Common.Configurations;
    using Configurations.EventHandler;
    using Strategy;

    public class EventManagerStrategy : IEventManagerStrategy
    {
        public void Publish<TEventType>(TEventType ev) where TEventType : IEvent
        {
            IEventHandlerStrategy strategy = this.GetStrategyHandler<TEventType>(ev);
            strategy.Publish<TEventType>(ev);
        }

        private IEventHandlerStrategy GetStrategyHandler<TEventType>(TEventType ev) where TEventType : IEvent
        {
            string className = ev.GetType().FullName;
            EventHandlerOption option = Configuration.Current.EventHandlers[className];
            EventHandlerStategyType type = option == null ? EventHandlerStategyType.InApp : option.Type;
            switch (type)
            {
                case EventHandlerStategyType.External:
                    return new ExternalEventHandlerStategy();
                case EventHandlerStategyType.InApp:
                default:
                    return new InAppEventHandlerStategy();
            }
        }
    }
}
