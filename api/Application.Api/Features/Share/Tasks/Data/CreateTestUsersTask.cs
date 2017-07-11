namespace App.Api.Features.Share.Tasks.Data
{
    using App.Common.Tasks;
    using System.Collections.Generic;
    using System.Web;
    using App.Common;
    using App.Common.DI;
    using App.Service.Security.User;

    public class CreateUsersTask : BaseTask<TaskArgument<System.Web.HttpApplication>>, IApplicationReadyTask<TaskArgument<System.Web.HttpApplication>>
    {
        public CreateUsersTask() : base(ApplicationType.All)
        {
            this.Order = 2;
        }

        public override void Execute(TaskArgument<HttpApplication> context)
        {
            this.CreateLanguages();
            IList<Entity.Security.User> users = new List<Entity.Security.User>();
            users.Add(new Entity.Security.User("tu.tran@orientsoftware.net", "123456"));
            users.Add(new Entity.Security.User("tu.tran@yahoo.com", "123456", "TU", "Tran"));
            IUserService userService = IoC.Container.Resolve<IUserService>();
            userService.CreateIfNotExist(users);
        }

        private void CreateLanguages()
        {
            IList<Entity.Common.Language> languages = new List<Entity.Common.Language>();
            languages.Add(new Entity.Common.Language("Viet name", "vn", "VN-vn"));
            languages.Add(new Entity.Common.Language("English", "en", "EN-en"));
            Service.Common.ILanguageService languageService = IoC.Container.Resolve<Service.Common.ILanguageService>();
            languageService.Add(languages);
        }
    }
}