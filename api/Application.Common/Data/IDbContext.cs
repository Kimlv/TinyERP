namespace App.Common.Data
{
    public interface IDbContext
    {
        IDbSet<TEntity, TId> GetDbSet<TEntity, TId>() where TEntity : class, IBaseEntity<TId>;
        int SaveChanges();
        void RegisterSaveChangeEvent(OnContextSaveChange ev);
        void OnSaveChanged();
    }
    public interface IDbContext<TEntity> : IDbContext { }
}