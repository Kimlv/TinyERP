namespace App.Security.Command.Impl
{
    using System;
    using App.Common.Command;
    using App.Common.Data;
    using App.Security.Aggregate;
    using App.Security.Repository;
    using App.Common.DI;
    using App.Common.Helpers;
    using App.Common.Validation;

    internal class SecurityCommandHandler : BaseCommandHandler, ISecurityCommandHandler
    {
        public void Handle(CreateUserCommand command)
        {
            this.ValidateCreateUserRequest(command);
            using (IUnitOfWork uow = this.CreateUnitOfWork<User>())
            {
                IUserRepository repository = IoC.Container.Resolve<IUserRepository>(uow);
                User user = new User();
                user.FirstName = command.FirstName;
                user.LastName = command.LastName;
                user.Email = command.Email;
                user.UserName = command.UserName;
                user.Pwd = EncodeHelper.EncodePassword(command.Pwd);
                foreach (Role role in command.Roles)
                {
                    user.AddRole(role);
                }
                repository.Add(user);
                uow.Commit();

                command.Result = user.Id;
                user.PublishEvents();
            }
        }

        private void ValidateCreateUserRequest(CreateUserCommand command)
        {
            IValidationException validationException = ValidationHelper.Validate(command);
            IUserRepository repository = IoC.Container.Resolve<IUserRepository>();

            User user = repository.GetUserByUserName(command.UserName);
            if (user != null)
            {
                validationException.Add(new ValidationError("security.users.createUser.userNameExisted"));
            }

            user = repository.GetUserByEmail(command.Email);
            if (user != null)
            {
                validationException.Add(new ValidationError("security.users.createUser.emailExisted"));
            }
            validationException.ThrowIfError();
        }
    }
}
