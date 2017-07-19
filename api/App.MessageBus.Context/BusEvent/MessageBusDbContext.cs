namespace App.MessageBus.Context.BusEvent
{
    using System.Data.Entity;
    public class MessageBusDbContext : App.Common.Data.MSSQL.MSSQLDbContext, Common.Data.IDbContext<Aggregate.BusEventAggregate>
    {
        public IDbSet<App.MessageBus.Aggregate.BusEventAggregate> BusEvents { get; set; }
        public IDbSet<App.MessageBus.Aggregate.EventSubcriber> EventSubcribers { get; set; }
        public MessageBusDbContext() : this(Common.IOMode.Read)
        {

        }
        public MessageBusDbContext(Common.IOMode mode, string connectionName = "DefaultMessageBus") : base(new Common.Data.MSSQL.MSSQLConnectionString(connectionName), mode)
        {
            Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MessageBusDbContext>());
        }

    }
}
