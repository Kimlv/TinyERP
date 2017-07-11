namespace App.Api.Features.Common
{
    using App.Common.Data;
    using Context;
    using App.Common.Data.Context;

    public class DbContextResolver : BaseDbContextResolver
    {
        protected override IDbContext CreateDefaultDbContext(DbContextOption option)
        {
            return new AppDbContext(option.IOMode, connectionName: option.ConnectionStringName);
        }
        //public IDbContext Resolve(DbContextOption option)
        //{
        //    switch (option.RepositoryType)
        //    {
        //        case RepositoryType.MSSQL:
        //            return new AppDbContext(option.IOMode, connectionName: option.ConnectionStringName);
        //        case RepositoryType.MongoDb:
        //            return new App.Common.Data.MongoDB.MongoDbContext(option.IOMode, connectionName: option.ConnectionStringName);
        //        default:
        //            throw new InvalidOperationException("common.errors.unsupportedTyeOdDbContext");
        //    }
        //}
    }
}