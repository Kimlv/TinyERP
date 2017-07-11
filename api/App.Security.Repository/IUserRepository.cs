namespace App.Security.Repository
{
    using App.Common.Data;
    using App.Security.Aggregate;
    public interface IUserRepository : IBaseCommandRepository<User>
    {
        User GetActiveUser(string userName, string password);
        User GetUserByEmail(string email, bool isActiveRequired = false);
        User GetUserByUserName(string userName, bool isActiveRequired = false);
    }
}
