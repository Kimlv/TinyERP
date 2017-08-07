namespace App.Common.Data
{
    using System;

    public class UnitOfWork : IUnitOfWork
    {
        public RepositoryType RepositoryType { get; protected set; }
        public IDbContext Context { get; private set; }
        public UnitOfWork(RepositoryType repoType, string connectionString = "") : this(new DbContextOption(IOMode.Write, repoType, connectionString)) { }
        internal UnitOfWork(DbContextOption option) : this(DbContextFactory.Create(option))
        {
            this.RepositoryType = option.RepositoryType;
        }
        protected UnitOfWork(IDbContext context)
        {
            this.Context = context;
        }

        public void Commit()
        {
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}