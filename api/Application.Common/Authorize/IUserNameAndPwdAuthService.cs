namespace App.Common.Authorize
{
    public interface IUserNameAndPwdAuthService
    {
        bool IsAuthorized(string userName, string pwd);
    }
}
