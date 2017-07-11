namespace App.Common.Data
{
    using global::MongoDB.Bson;
    public interface IBaseQueryRepository<TEntity> : IBaseRepository<TEntity, ObjectId> where TEntity : class, IBaseEntity<ObjectId>
    {
    }
}
