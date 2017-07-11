namespace App.Common.Data
{
    using App.Common.Data.MSSQL;
    using App.Common.Mapping;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class, IBaseEntity<TId>
    {
        public IDbSet<TEntity, TId> DbSet { get; protected set; }
        public BaseRepository(IDbContext context)
        {
            this.DbSet = context.GetDbSet<TEntity, TId>();
        }
        public BaseRepository(DbContextOption option) : this(DbContextFactory.Create(option)) { }

        public virtual TEntity GetById(string id, string includes = "")
        {
            return this.DbSet.Get(id, includes);
        }

        public virtual TEntity GetById(string id)
        {
            return this.GetById(id, string.Empty);
        }

        public virtual TResult GetById<TResult>(string id) where TResult : IMappedFrom<TEntity>
        {
            TEntity entity = this.GetById(id);
            return AutoMapper.Mapper.Map<TResult>(entity);
        }

        public virtual IList<TResult> GetItems<TResult>(string include = "") where TResult : IMappedFrom<TEntity>
        {
            return this.DbSet.AsQueryable(include).ProjectTo<TResult>().ToList();
        }

        public virtual void Add(TEntity item)
        {
            this.DbSet.Add(item);
        }

        public virtual void Delete(TId id)
        {
            this.DbSet.Delete(id);
        }

        public virtual void Update(TEntity item)
        {
            this.DbSet.Update(item);
        }

        public virtual Paging.IPagingData<TResult> GetAll<TResult, SearchRequestType>(Paging.IPagingRequest<SearchRequestType> request) where TResult : Mapping.IMappedFrom<TEntity>
        {
            throw new System.NotImplementedException();
        }

        public virtual Paging.IPagingData<TEntity> GetAll<SearchRequestType>(Paging.IPagingRequest<SearchRequestType> request)
        {
            throw new System.NotImplementedException();
        }
    }
}