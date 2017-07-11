namespace App.Security.Command.UserNameAndPwd
{
    using App.Common.Command;
    public class UserNameAndPwdAuthenticationRequest : BaseCommandWithResult<AuthenticationResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserNameAndPwdAuthenticationRequest(string userName, string password) : base()
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
