namespace App.Common.MVC.Resolver
{
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    public class HttpControllerTypeResolver : DefaultHttpControllerTypeResolver
    {
        public HttpControllerTypeResolver() : base(IsValidController) { }
        protected static bool IsValidController(Type typeOfController)
        {
            if (typeOfController == null) throw new ArgumentNullException("typeOfController");
            return typeOfController.IsClass &&
                typeOfController.IsVisible &&
                !typeOfController.IsAbstract &&
                typeof(BaseApiController).IsAssignableFrom(typeOfController) &&
                typeof(IHttpController).IsAssignableFrom(typeOfController);
        }
    }
}
