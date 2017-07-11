namespace App.Security.Command.Impl
{
    using System;
    using App.Common.Command;
    using App.Security.Command.UserNameAndPwd;
    using Common.Data;
    using Aggregate;
    using Repository;
    using Common.DI;
    using Common.Helpers;
    using Common.Validation;
    using Event.Authentication;

    internal class UserNameAndPwdAuthCommandHandler : BaseCommandHandler, IUserNameAndPwdAuthCommandHandler
    {
        public void Handle(UserNameAndPwdAuthenticationRequest command)
        {
            this.ValidateUserNameAndPwdAuthenticationRequest(command);
            using (IUnitOfWork uow = this.CreateUnitOfWork<User>())
            {
                IUserRepository repository = IoC.Container.Resolve<IUserRepository>(uow);
                User user = repository.GetActiveUser(command.UserName, EncodeHelper.EncodePassword(command.Password));
                if (user == null)
                {
                    command.Result = new AuthenticationResult(false);
                    this.Publish(new OnAuthenticationFailed(command.UserName, command.Password, DateTime.UtcNow));
                    return;
                }
                user.GenerateLoginToken();
                repository.Update(user);
                uow.Commit();

                command.Result = ObjectHelper.Convert<AuthenticationResult>(user);
                command.Result.IsValid = true;

                this.Publish(new OnAuthenticationSuccess(user.FirstName, user.LastName, user.Email, user.LoginToken, user.TokenExpiredAfter, DateTime.UtcNow));
            }
        }

        private void ValidateUserNameAndPwdAuthenticationRequest(UserNameAndPwdAuthenticationRequest command)
        {
            IValidationException validationException = ValidationHelper.Validate(command);
            validationException.ThrowIfError();
        }
    }
}
