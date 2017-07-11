namespace App.Common.Data
{
    using global::MongoDB.Bson;
    public abstract class BaseQueryRepository<TEntity> : BaseRepository<TEntity, ObjectId>, IBaseQueryRepository<TEntity> 
        where TEntity : class, IBaseEntity<ObjectId>
    {
        public BaseQueryRepository(DbContextOption option) : base(option)
        {
            if (option.DbContext == null) {
                throw new System.InvalidOperationException("common.context.contextCanNotBeNull");
            }
            if (option.IOMode == IOMode.Read)
            {
                throw new System.InvalidOperationException("common.context.invalidContructorForRead");
            }
            this.DbSet = option.DbContext.GetDbSet<TEntity, ObjectId>();
        }

        public BaseQueryRepository(RepositoryType type) : base(new DbContextOption(IOMode.Read,type)){}
    }
}
