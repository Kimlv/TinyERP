namespace App.Repository.Impl.Common
{
    using System;
    using System.Collections.Generic;
    using App.Common;
    using App.Common.Data;
    using App.Common.Data.MSSQL;
    using App.Context;
    using App.Entity.Common;
    using App.Repository.Common;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    internal class FileRepository : BaseCommandRepository<FileUpload>, IFileRepository
    {
        public FileRepository() : base(new AppDbContext(IOMode.Read))
        {
        }

        public FileRepository(IUnitOfWork uow) : base(uow.Context as IMSSQLDbContext)
        {
        }

        public IList<TEntity> GetByIds<TEntity>(IList<Guid> ids)
        {
            return this.DbSet.AsQueryable().Where(item => ids.Contains(item.Id)).ProjectTo<TEntity>().ToList();
        }
    }
}
