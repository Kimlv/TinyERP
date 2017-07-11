namespace App.Service.Security.User
{
    using App.Common;

    public class UserSignInResponse
    {
        public AuthenticationToken Token { get; set; }
        public UserQuickProfile Profile { get; set; }

        public UserSignInResponse(AuthenticationToken token, UserQuickProfile userProfile)
        {
            this.Token = token;
            this.Profile = userProfile;
        }
    }
}
