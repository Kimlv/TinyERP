namespace App.Common.Aggregate
{
    using App.Common.Data;
    using Event;
    using System;

    public interface IBaseAggregateRoot : IBaseEntity<Guid>
    {
        void AddEvent(BaseEvent ev);
        void PublishEvents();
    }
}
