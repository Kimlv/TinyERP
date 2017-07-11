namespace App.Service.Impl.Security
{
    using System;
    using App.Service.Security.User;
    using App.Common;
    using App.Common.Helpers;
    using App.Common.Data;
    using App.Common.Validation;
    using App.Entity.Security;
    using App.Repository.Security;
    using App.Common.DI;
    using System.Collections.Generic;

    internal class UserService : IUserService
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void CreateIfNotExist(IList<User> users)
        {
            if (users == null) { return; }

            using (IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                IUserRepository userRepository = IoC.Container.Resolve<IUserRepository>(uow);
                foreach (User user in users)
                {
                    User existUser = userRepository.GetByEmail(user.Email);
                    if (existUser != null) { continue; }

                    userRepository.Add(user);
                }

                uow.Commit();
            }
        }

        public UserSignInResponse SignIn(UserSignInRequest request)
        {
            AuthenticationToken token;
            User user;
            this.ValidateUserLoginRequest(request);
            using (IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                IUserRepository userRepository = IoC.Container.Resolve<IUserRepository>(uow);
                token = new App.Common.AuthenticationToken(Guid.NewGuid(), DateTimeHelper.GetAuthenticationTokenExpiredUtcDateTime());
                user = userRepository.GetByEmail(request.Email);
                user.Token = token.Value;
                user.TokenExpiredAfter = token.ExpiredAfter;
                userRepository.Update(user);
                uow.Commit();
            }

            UserQuickProfile profile = new UserQuickProfile(user);
            return new UserSignInResponse(token, profile);
        }

        private void ValidateUserLoginRequest(UserSignInRequest request)
        {
            ValidationException exception = new ValidationException();
            if (request == null)
            {
                exception.Add(new ValidationError("common.invalidRequest"));
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                exception.Add(new ValidationError("registration.signin.validation.emailRequired"));
            }

            if (string.IsNullOrWhiteSpace(request.Pwd))
            {
                exception.Add(new ValidationError("registration.signin.validation.pwdRequired"));
            }

            IUserRepository userRepository = IoC.Container.Resolve<IUserRepository>();
            User userProfile = userRepository.GetByEmail(request.Email);

            if (userProfile == null || EncodeHelper.EncodePassword(request.Pwd) != userProfile.Password)
            {
                exception.Add(new ValidationError("registration.signin.validation.invalidEmailOrPwd"));
            }

            exception.ThrowIfError();
        }

        public void SignOut(string token)
        {
            using (IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                IUserRepository userRepository = IoC.Container.Resolve<IUserRepository>(uow);
                User user = userRepository.GetByToken(token);
                if (user == null) { return; }

                user.Token = string.Empty;
                user.TokenExpiredAfter = DateTime.UtcNow;
                userRepository.Update(user);
                uow.Commit();
            }
        }

        public bool IsValidToken(string authenticationToken)
        {
            IUserRepository userRepository = IoC.Container.Resolve<IUserRepository>();
            User user = userRepository.GetByToken(authenticationToken);
            return user != null && !DateTimeHelper.IsExpired(user.TokenExpiredAfter);
        }

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            this.ValidateCreateUserRequest(request);
            using (IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                User user = new User(request.Email, string.Empty, request.FirstName, request.LastName);

                IUserRepository userRepository = IoC.Container.Resolve<IUserRepository>(uow);
                userRepository.Add(user);
                uow.Commit();

                return ObjectHelper.Convert<CreateUserResponse>(user);
            }
        }

        private void ValidateCreateUserRequest(CreateUserRequest request)
        {
            IValidationException validationException = ValidationHelper.Validate(request);
            if (!EmailHelper.IsValid(request.Email))
            {
                validationException.Add(new ValidationError("security.addOrUpdateUser.validation.invalidEmail"));
            }
            validationException.ThrowIfError();
        }
    }
}
