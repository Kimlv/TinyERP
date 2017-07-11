namespace App.Common.Data.MongoDB
{
    using global::MongoDB.Bson;
    public interface IMongoDbContext : IDbContext
    {
        //IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IBaseEntity<System.Guid>;
        void Save<TEntity>(TEntity item) where TEntity : class;
        void Delete<TEntity, TId>(TId id);
        //IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IBaseEntity<System.Guid>;
    }
}
