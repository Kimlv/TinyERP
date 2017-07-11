namespace App.Security.Owin.Tasks
{
    using App.Common.Command;
    using App.Common.DI;
    using App.Common.Logging;
    using App.Common.Tasks;
    using App.Security.Aggregate;
    using App.Security.Command;
    using App.Common;
    using App.Common.Helpers;

    public class CreateTestUserTask : BaseTask<TaskArgument<System.Web.HttpApplication>>, IApplicationReadyTask<TaskArgument<System.Web.HttpApplication>>
    {
        public CreateTestUserTask() : base(App.Common.ApplicationType.All) { }

        public override void Execute(TaskArgument<System.Web.HttpApplication> context)
        {
            ICommandHandlerStrategy commandHandlerStrategy = CommandHandlerStrategyFactory.Create<User>();
            CreateUserCommand request = this.CreateUserCommand();
            commandHandlerStrategy.Execute(request);

            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("New user created with Id:{0}", request.Result);
        }

        private CreateUserCommand CreateUserCommand()
        {
            CreateUserCommand command = new Command.CreateUserCommand();
            command.FirstName = "Tu";
            command.LastName = "Tran";
            command.Email = "contact@tranthanhtu.vn";
            command.UserName = "techcoaching";
            command.Pwd = "123";
            command.Roles.Add(new Role("Security Administrator", SecurityRoleType.Administrator, "just for testing", DomainType.Security));
            command.Roles.Add(new Role("Security User", SecurityRoleType.User, "just for testing", DomainType.Security));
            command.Roles.Add(new Role("Security Guest", SecurityRoleType.Guest, "just for testing", DomainType.Security));
            return command;
        }
    }
}
