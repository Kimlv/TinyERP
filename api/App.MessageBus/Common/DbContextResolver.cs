namespace App.MessageBus
{
    using System;
    using App.Common;
    using App.Common.Data;
    using App.MessageBus.Context.BusEvent;
    using App.Common.Data.Context;

    public class DbContextResolver : BaseDbContextResolver
    {
        public DbContextResolver() : base(ConnectionStrings.DefaultMessageBus)
        {
        }
        protected override IDbContext CreateDefaultDbContext(DbContextOption option)
        {
            string connectionStringName = string.IsNullOrWhiteSpace(option.ConnectionStringName) ? this.DefaultConnectionString : option.ConnectionStringName;
            return new MessageBusDbContext(option.IOMode, connectionName: connectionStringName);
        }
        //public override IDbContext Resolve(DbContextOption option)
        //{
        //    switch (option.RepositoryType)
        //    {
        //        case RepositoryType.MSSQL:
        //            string connectionStringName = string.IsNullOrWhiteSpace(option.ConnectionStringName) ? this.DefaultConnectionString : option.ConnectionStringName;
        //            return new MessageBusDbContext(option.IOMode, connectionName: connectionStringName);
        //        default:
        //            throw new InvalidOperationException("common.errors.unsupportedTyeOdDbContext");
        //    }
        //}
    }
}