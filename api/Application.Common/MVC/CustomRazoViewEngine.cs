namespace App.Common.MVC
{
    using System.Web.Mvc;

    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine() : base()
        {
            this.AreaViewLocationFormats = new string[] {
                            "~/Features/{2}/{1}/{0}.cshtml",
                            "~/Features/{2}/Shared/Views/{0}/view.cshtml",
                            "~/Areas/{2}/Views/{1}/%1/{0}.cshtml",
                             "~/Areas/{2}/Views/{1}/%1/{0}.vbhtml",
                             "~/Areas/{2}/Views/Shared/%1/{0}.cshtml",
                             "~/Areas/{2}/Views/Shared/%1/{0}.vbhtml" };
            this.AreaMasterLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/%1/{0}.cshtml",
                               "~/Areas/{2}/Views/{1}/%1/{0}.vbhtml",
                               "~/Areas/{2}/Views/Shared/%1/{0}.cshtml",
                               "~/Areas/{2}/Views/Shared/%1/{0}.vbhtml" };
            this.AreaPartialViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/%1/{0}.cshtml",
                                "~/Areas/{2}/Views/{1}/%1/{0}.vbhtml",
                                "~/Areas/{2}/Views/Shared/%1/{0}.cshtml",
                                "~/Areas/{2}/Views/Shared/%1/{0}.vbhtml" };
            this.ViewLocationFormats = new string[] { "~/Views/{1}/%1/{0}.cshtml",
                         "~/Views/{1}/%1/{0}.vbhtml",
                         "~/Views/Shared/%1/{0}.cshtml",
                         "~/Views/Shared/%1/{0}.vbhtml" };
            this.MasterLocationFormats = new string[] { "~/Views/{1}/%1/{0}.cshtml",
                           "~/Views/{1}/%1/{0}.vbhtml",
                           "~/Views/Shared/%1/{0}.cshtml",
                           "~/Views/Shared/%1/{0}.vbhtml" };
            this.PartialViewLocationFormats = new string[] { "~/Views/{1}/%1/{0}.cshtml",
                            "~/Views/{1}/%1/{0}.vbhtml",
                            "~/Views/Shared/%1/{0}.cshtml",
                            "~/Views/Shared/%1/{0}.vbhtml" };
            this.FileExtensions = new string[] { "cshtml", "vbhtml" };
            this.FileExtensions = new string[] { "cshtml", "vbhtml" };
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return base.CreateView(controllerContext, viewPath.Replace("%1", "Pages"), masterPath.Replace("%1", "Pages"));
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return base.CreatePartialView(controllerContext, partialPath.Replace("%1", "PartialPages"));
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return base.FileExists(controllerContext, virtualPath.Replace("%1", "Pages")) ||
                base.FileExists(controllerContext, virtualPath.Replace("%1", "PartialPages"));
        }
    }
}