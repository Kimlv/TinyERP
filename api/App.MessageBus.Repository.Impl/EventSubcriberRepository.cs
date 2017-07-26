namespace App.MessageBus.Repository.Impl
{
    using System.Collections.Generic;
    using App.Common.Data;
    using App.MessageBus.Aggregate;
    using System.Linq;
    using App.Common;
    using System;

    internal class EventSubcriberRepository : BaseCommandRepository<EventSubcriber>, IEventSubcriberRepository
    {
        public EventSubcriberRepository() : base(new Context.BusEvent.MessageBusDbContext(Common.IOMode.Read)) { }
        public EventSubcriberRepository(Common.Data.IUnitOfWork uow) : base(uow.Context) { }

        public IList<EventSubcriber> GetAllActive(string key)
        {
            return this.DbSet.AsQueryable().Where(item => item.Key == key && item.Status == BusEventSubcriberStatus.Active).ToList();
        }

        public EventSubcriber GetItem(string key, string uri)
        {
            return this.DbSet.AsQueryable().FirstOrDefault(item => item.Key == key && item.Uri == uri);
        }
    }
}
