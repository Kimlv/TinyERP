namespace App.Repository.Impl.Inventory
{
    using App.Common.Data.MSSQL;
    using App.Context;
    using Repository.Inventory;
    using App.Common.Data;
    using Entity.Inventory;

    public class UnitOfMeasurementRepository : BaseContentRepository<UnitOfMeasurement>, IUnitOfMeasurementRepository
    {
        public UnitOfMeasurementRepository(IUnitOfWork uow) : base(uow.Context as IMSSQLDbContext)
        {
        }

        public UnitOfMeasurementRepository() : base(new AppDbContext(App.Common.IOMode.Read))
        {
        }
    }
}