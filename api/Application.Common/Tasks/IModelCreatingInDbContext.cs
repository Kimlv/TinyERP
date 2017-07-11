namespace App.Common.Tasks
{
    using System.Data.Entity;

    public interface IModelCreatingInDbContext : IBaseTask<DbModelBuilder>
    {
    }
}