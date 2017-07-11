namespace App.Api.Features.Security
{
    using App.Common.DI;
    using App.Common.Http;
    using App.Common.MVC;
    using App.Common.MVC.Attributes;
    using App.Common.Validation;
    using App.Service.Security.User;
    using Repository.Security;
    using Service.Security.User;
    using System;
    using System.Web.Http;

    [RoutePrefix("api/users")]
    public class UsersController : BaseApiController
    {
        [HttpPost()]
        [Route("")]
        [ResponseWrapper()]
        public CreateUserResponse CreateUser([FromBody]CreateUserRequest request)
        {
                IUserService userService = IoC.Container.Resolve<IUserService>();
                return userService.CreateUser(request);
        }

        [HttpGet()]
        [Route("{userId}")]
        [ResponseWrapper()]
        public UserSummary GetUser([FromUri]Guid userId)
        {
            IUserRepository userRepo = IoC.Container.Resolve<IUserRepository>();
            return userRepo.GetById<UserSummary>(userId.ToString());
        }

        [HttpPost]
        [Route("signin")]
        public IResponseData<UserSignInResponse> SignIn([FromBody]UserSignInRequest request)
        {
            IResponseData<UserSignInResponse> response = new ResponseData<UserSignInResponse>();
            try
            {
                IUserService userService = IoC.Container.Resolve<IUserService>();
                UserSignInResponse signInResponse = userService.SignIn(request);
                response.SetData(signInResponse);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
            }

            return response;
        }

        [HttpPost]
        [Route("{token}/signout")]
        public IResponseData<string> SignOut([FromUri]string token)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                IUserService userService = IoC.Container.Resolve<IUserService>();
                userService.SignOut(token);
            }
            catch (ValidationException ex)
            {
                response.SetErrors(ex.Errors);
                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
            }

            return response;
        }
    }
}
