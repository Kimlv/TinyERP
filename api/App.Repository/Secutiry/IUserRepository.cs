namespace App.Repository.Security
{
    using App.Common.Data;
    using App.Entity.Security;

    public interface IUserRepository : IBaseCommandRepository<User>
    {
        User GetByEmail(string email);
        User GetByToken(string token);
    }
}
