namespace App.Common.MVC.Attributes
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using App.Common.Authorize;
    using App.Common.DI;

    public class AuthorizedRequestAttribute : AuthorizeAttribute
    {
        public AuthenticationType Type { get; set; }
        public string TokenParam { get; set; }
        private readonly IAuthorization authorization;
        public AuthorizedRequestAttribute(AuthenticationType type)
        {
            this.Type = type;
            this.authorization = this.GetAuthorization();
            this.TokenParam = "authtoken";
        }

        public AuthorizedRequestAttribute(AuthenticationType type, string token)
            : this(type)
        {
            this.TokenParam = token;
        }

        public override bool AllowMultiple
        {
            get { return true; }
        }

        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            return this.authorization.IsAuthorized(httpContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            if (actionContext.Request != null &&
                actionContext.Request.Headers.GetValues(this.TokenParam).Count() > 0)
            {
                string authenticationToken = actionContext.Request.Headers.GetValues(this.TokenParam).FirstOrDefault();
                return this.authorization.IsAuthorized(authenticationToken);
            }

            return false;
        }

        private IAuthorization GetAuthorization()
        {
            switch (this.Type)
            {
                case AuthenticationType.User:
                    return IoC.Container.Resolve<IUserLoginAuthorization>();
                default:
                    throw new Exception("Unsupported authorization:" + this.Type.ToString());
            }
        }
    }
}