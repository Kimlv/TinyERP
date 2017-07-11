namespace App.Common.Aggregate
{
    using App.Common.Data;
    using DI;
    using Event;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseAggregateRoot : BaseEntity, IBaseAggregateRoot
    {
        protected IList<BaseEvent> Events { get; set; }
        public BaseAggregateRoot()
        {
            this.Events = new List<BaseEvent>();
        }
        public void PublishEvents()
        {
            if (this.Events == null || this.Events.Count == 0) { return; }
            IEventManagerStrategy eventStrategyManager = IoC.Container.Resolve<IEventManagerStrategy>();
            foreach (BaseEvent ev in this.Events.OrderByDescending(item => item.Priority))
            {
                eventStrategyManager.Publish(ev);
            }
        }
        public void AddEvent(BaseEvent ev)
        {
            this.Events.Add(ev);
        }
        public virtual void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
