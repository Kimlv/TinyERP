namespace App.Common.Data.MongoDB
{
    using global::MongoDB.Bson;
    using System.Linq;
    public class MongoDbSet<TEntity, TId> : DbSet<TEntity, TId> where TEntity : class, IBaseEntity<TId>
    {
        public IQueryable<TEntity> Collection { get; protected set; }
        public new IMongoDbContext Context { get; protected set; }

        public MongoDbSet(IMongoDbContext mongoDbContext, IQueryable<TEntity> collection)
        {
            this.Context = mongoDbContext;
            this.Collection = collection;
            //this.Context.RegisterSaveChangeEvent(this.OnContextSaveChange);
        }

        public override void Add(TEntity item)
        {
            this.Context.Save(item);
        }
        public override void Delete(TId id)
        {
            //ObjectId itemId = new ObjectId(id);
            this.Context.Delete<TEntity, TId>(id);
        }

        public override TEntity Get(string id, string includes = "")
        {
            return this.Collection.Where(item => item.Id.ToString() == id).FirstOrDefault();
        }

        public override void Update(TEntity item)
        {
            this.Context.Save(item);
        }
        public override void OnContextSaveChange(IDbContext context)
        {
        }
        public override IQueryable<TEntity> AsQueryable(string include = "")
        {
            return this.Collection;
        }
    }
}
