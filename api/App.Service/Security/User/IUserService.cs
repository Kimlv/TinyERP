namespace App.Service.Security.User
{
    using System.Collections.Generic;

    public interface IUserService
    {
        UserSignInResponse SignIn(UserSignInRequest request);
        void CreateIfNotExist(IList<Entity.Security.User> users);
        void SignOut(string token);
        bool IsValidToken(string authenticationToken);
        CreateUserResponse CreateUser(CreateUserRequest request);
    }
}
