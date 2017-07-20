namespace App.Common.Data
{
    using System;
    public abstract class BaseDbContextResolver : IDbContextResolver
    {
        protected string DefaultConnectionString { get; private set; }
        public BaseDbContextResolver(string connectionName = "")
        {
            this.DefaultConnectionString = connectionName;
        }
        public virtual IDbContext Resolve(DbContextOption option)
        {
            throw new NotImplementedException();
        }
    }
}
