namespace App.Repository.Impl.Inventory
{
    using App.Common.Data;
    using App.Repository.Inventory;
    using App.Common.Data.MSSQL;
    using App.Context;
    using App.Entity.Inventory;

    public class CategoryRepository : BaseContentRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork uow) : base(uow.Context as IMSSQLDbContext)
        {
        }

        public CategoryRepository() : base(new AppDbContext(App.Common.IOMode.Read))
        {
        }
    }
}
