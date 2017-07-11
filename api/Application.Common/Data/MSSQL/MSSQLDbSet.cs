namespace App.Common.Data.MSSQL
{
    using System;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public class MSSQLDbSet<TEntity, TId> : DbSet<TEntity, TId> where TEntity : class, IBaseEntity<TId>
    {
        public System.Data.Entity.DbSet<TEntity> DbSet { get; protected set; }
        public new App.Common.Data.MSSQL.IMSSQLDbContext Context { get; protected set; }
        public IOMode Mode { get; set; }
        private System.Data.Entity.DbContext EFContext { get; set; }
        public MSSQLDbSet(App.Common.Data.MSSQL.IMSSQLDbContext mssqlDbContext, System.Data.Entity.DbContext efcontext, IOMode mode = IOMode.Read)
        {
            this.Context = mssqlDbContext;
            this.EFContext = efcontext;
            this.Mode = mode;
            this.DbSet = this.EFContext.Set<TEntity>();
            this.Context.RegisterSaveChangeEvent(this.OnContextSaveChange);
        }

        protected DbQuery<TEntity> GetDbSet(string includes = "")
        {
            DbQuery<TEntity> query = this.Mode == IOMode.Read ? this.DbSet.AsNoTracking() : this.DbSet;
            if (!string.IsNullOrWhiteSpace(includes))
            {
                string[] includesItems = includes.Split(',');
                foreach (string item in includesItems)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        public override void Add(TEntity item)
        {
            this.DbSet.Add(item);
        }

        public override void Delete(TId id)
        {
            TEntity entity = this.Get(id.ToString());
            this.DbSet.Remove(entity);
        }

        public override TEntity Get(string id, string includes = "")
        {
            //TId itemId = new Guid(id);
            DbQuery<TEntity> query = this.GetDbSet();
            if (!string.IsNullOrWhiteSpace(includes))
            {
                string[] includesItems = includes.Split(',');
                foreach (string item in includesItems)
                {
                    query = query.Include(item);
                }
            }

            return query.FirstOrDefault(item => item.Id.ToString() == id);
        }

        public override void Update(TEntity item)
        {
            System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> dbentityEntry = this.EFContext.Entry(item);
            if (dbentityEntry.State == System.Data.Entity.EntityState.Detached)
            {
                var keys = this.GetEntityKey<TEntity>(this.EFContext, item);
                TEntity attachedItem = this.DbSet.Find(keys);
                if (attachedItem == null)
                {
                    this.DbSet.Attach(item);
                    dbentityEntry.State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    this.EFContext.Entry(attachedItem).CurrentValues.SetValues(item);
                }
            }
        }

        public override void OnContextSaveChange(IDbContext context)
        {
        }

        private object[] GetEntityKey<T>(System.Data.Entity.DbContext context, T entity) where T : class, IBaseEntity<TId>
        {
            var oc = ((IObjectContextAdapter)context).ObjectContext;
            var keys = oc.MetadataWorkspace.GetEntityContainer(oc.DefaultContainerName, System.Data.Entity.Core.Metadata.Edm.DataSpace.CSpace)
                                             .BaseEntitySets
                                             .First(meta => meta.ElementType.Name == typeof(T).Name)
                                             .ElementType
                                             .KeyMembers
                                             .Select(k => k.Name)
                                             .ToList();
            var propertys = entity.GetType().GetProperties().Where(prop => keys.Contains(prop.Name));
            return propertys.ToList().Select(property => property.GetValue(entity, null)).ToArray();
        }

        public override IQueryable<TEntity> AsQueryable(string include = "")
        {
            return this.GetDbSet(include);
        }
    }
}