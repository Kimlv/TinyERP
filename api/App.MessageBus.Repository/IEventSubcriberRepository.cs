namespace App.MessageBus.Repository
{
    using System.Collections.Generic;
    using App.Common.Data;
    using App.MessageBus.Aggregate;

    public interface IEventSubcriberRepository : IBaseCommandRepository<App.MessageBus.Aggregate.EventSubcriber>
    {
        IList<EventSubcriber> GetAllActive(string key);
        EventSubcriber GetItem(string key, string uri);
    }
}
