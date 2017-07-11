﻿namespace App.Context
{
    using Aggregate.Order;
    using App.Common;
    using Common.Data;
    using System.Data.Entity;
    using ValueObject.Order;

    public class OrderDbContext: App.Common.Data.MSSQL.MSSQLDbContext, IDbContext<OrderAggregate>
    {
        public System.Data.Entity.DbSet<OrderAggregate> OrderAggregates { get; set; }
        public System.Data.Entity.DbSet<OrderCustomerDetail> CustomerDetails { get; set; }
        public System.Data.Entity.DbSet<OrderLine> OrderLines { get; set; }
        public OrderDbContext(IOMode mode = IOMode.Read, string connectionName = "") : base(new App.Common.Data.MSSQL.MSSQLConnectionString(connectionName), mode)
        {
            Database.SetInitializer<AppDbContext>(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }
    }
}
