namespace App.MessageBus
{
    using System;
    using App.Common;
    using App.Common.Data;
    using App.MessageBus.Context.BusEvent;

    public class DbContextResolver : IDbContextResolver
    {
        public IDbContext Resolve(DbContextOption option)
        {
            switch (option.RepositoryType)
            {
                case RepositoryType.MSSQL:
                    return new MessageBusDbContext(option.IOMode, connectionName: option.ConnectionStringName);
                default:
                    throw new InvalidOperationException("common.errors.unsupportedTyeOdDbContext");
            }
        }
    }
}