namespace App.Common.Data.Context
{
    using App.Common.Helpers;
    using System;
    public abstract class BaseDbContextResolver : IDbContextResolver
    {
        protected string DefaultConnectionString { get; private set; }
        public BaseDbContextResolver(string connectionName = "")
        {
            this.DefaultConnectionString = connectionName;
        }
        public IDbContext Resolve(DbContextOption option)
        {
            if (option.DbContextType != null)
            {
                return (IDbContext)ObjectHelper.CreateInstance(option.DbContextType, new object[] { option.IOMode, option.ConnectionStringName });
            }

            switch (option.RepositoryType)
            {
                case RepositoryType.MSSQL:
                    return this.CreateDefaultDbContext(option);
                case RepositoryType.MongoDb:
                    return new App.Common.Data.MongoDB.MongoDbContext(option.IOMode, connectionName: option.ConnectionStringName);
                default:
                    throw new InvalidOperationException("common.errors.unsupportedTyeOdDbContext");
            }
        }

        protected virtual IDbContext CreateDefaultDbContext(DbContextOption option)
        {
            throw new NotImplementedException();
        }
    }
}
