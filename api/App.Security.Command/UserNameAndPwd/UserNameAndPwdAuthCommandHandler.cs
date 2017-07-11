namespace App.Security.Command.UserNameAndPwd
{
    using App.Common.Command;
    public interface IUserNameAndPwdAuthCommandHandler :
        IBaseCommandHandler<UserNameAndPwdAuthenticationRequest>
    {
    }
}
