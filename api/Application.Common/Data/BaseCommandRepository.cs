namespace App.Common.Data
{
    using System;
    public class BaseCommandRepository<TEntity> : BaseRepository<TEntity, Guid> where TEntity : class, IBaseEntity<Guid>
    {
        public BaseCommandRepository(IDbContext context) : base(context)
        {
        }
    }
}
