namespace App.Common.Data
{
    public interface IDbContextResolver
    {
        IDbContext Resolve(DbContextOption option);
    }
}
