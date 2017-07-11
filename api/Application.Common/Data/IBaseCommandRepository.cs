namespace App.Common.Data
{
    using System;
    public interface IBaseCommandRepository<TEntity>: IBaseRepository<TEntity, Guid>
    {
        
    }
}
