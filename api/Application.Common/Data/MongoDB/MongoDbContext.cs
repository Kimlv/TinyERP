namespace App.Common.Data.MongoDB
{
    using System.Linq;
    using System.Collections.Generic;
    using global::MongoDB.Kennedy;
    using global::MongoDB.Bson;
    using System;

    public class MongoDbContext : ConcurrentDataContext, IMongoDbContext
    {
        private IList<OnContextSaveChange> saveChangeEvents;
        public IOMode Mode { get; set; }
        public MongoDbContext(IConnectionString connection, IOMode mode = IOMode.Read)
            : base(connection.Database, connection.Server, connection.Port)
        {
            this.Mode = mode;
        }
        public MongoDbContext(IOMode mode = IOMode.Read, string connectionName = "") : this(new MongoConnectionString(connectionName), mode) { }
        public IDbSet<TEntity, TId> GetDbSet<TEntity, TId>() where TEntity : class, IBaseEntity<TId>
        {
            IQueryable<TEntity> collection = this.GetCollection<TEntity>();
            IDbSet<TEntity, TId> dbset = new MongoDbSet<TEntity, TId>(this, collection);
            return dbset;
        }

        public void Save<TEntity>(TEntity item) where TEntity : class
        {
            this.ThrowIfNotEditable();
            base.Save(item);
        }

        private void ThrowIfNotEditable()
        {
            if (this.IsEditable()) { return; }
            throw new System.InvalidOperationException("common.dbContext.contextWasNotDeclareAsEditable");
        }

        private bool IsEditable()
        {
            return this.Mode == IOMode.Write;
        }

        public void Delete<TEntity, TId>(TId id)
        {
            this.ThrowIfNotEditable();
            ObjectId itemId = new ObjectId(id.ToString());
            base.Delete<TEntity>(itemId);
        }

        public int SaveChanges()
        {
            return 0;
            //throw new NotImplementedException();
        }

        public void RegisterSaveChangeEvent(OnContextSaveChange ev)
        {
            this.saveChangeEvents.Add(ev);
        }

        public void OnSaveChanged()
        {
            foreach (OnContextSaveChange ev in this.saveChangeEvents)
            {
                ev(this);
            }
        }
    }
}
