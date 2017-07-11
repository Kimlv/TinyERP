using System;

namespace App.Common.Data
{
    public class DbContextOption
    {
        public RepositoryType RepositoryType { get; protected set; }
        public IOMode IOMode { get; protected set; }
        public IDbContext DbContext { get; protected set; }
        public string ConnectionStringName { get; protected set; }
        public Type DbContextType { get; protected set; }
        public DbContextOption(IOMode mode, RepositoryType type, string connectionStringName = "", IDbContext context = null, Type dbContextType = null)
        {
            this.ConnectionStringName = connectionStringName;
            this.DbContext = context;
            this.IOMode = mode;
            this.RepositoryType = type;
            this.DbContextType = dbContextType;
        }
    }
}
