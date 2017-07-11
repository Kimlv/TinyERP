namespace App.Repository.Impl.Security
{
    using App.Common.Data;
    using App.Common.Data.MSSQL;
    using App.Entity.Security;
    using App.Repository.Secutiry;

    internal class UserGroupRepository : BaseContentRepository<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository() : base(new App.Context.AppDbContext(App.Common.IOMode.Read))
        {
        }

        public UserGroupRepository(IUnitOfWork uow) : base(uow.Context as IMSSQLDbContext)
        {
        }
    }
}
