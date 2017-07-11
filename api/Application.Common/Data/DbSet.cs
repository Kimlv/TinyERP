namespace App.Common.Data
{
    public class DbSet<TEntity, TId> : IDbSet<TEntity, TId>
    {
        public IDbContext Context { get; protected set; }
        public DbSet(IDbContext context)
        {
            context.RegisterSaveChangeEvent(this.OnContextSaveChange);
        }

        public DbSet()
        {
        }

        public virtual TEntity Get(string id, string includes = "")
        {
            throw new System.NotImplementedException();
        }

        public virtual void Add(TEntity item)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Delete(TId id)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update(TEntity item)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnContextSaveChange(IDbContext context)
        {
            throw new System.NotImplementedException();
        }

        public virtual System.Linq.IQueryable<TEntity> AsQueryable(string include = "")
        {
            throw new System.NotImplementedException();
        }
    }
}