namespace App.Common.DI.Castle
{
    using System.Web.Mvc;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;
    using App.Common.Helpers;
    using App.Common.Tasks;

    public class MvcRegisterCastleContainerTask : BaseTask<TaskArgument<System.Web.HttpApplication>>, IApplicationStartedTask<TaskArgument<System.Web.HttpApplication>>
    {
        public MvcRegisterCastleContainerTask() : base(ApplicationType.MVC | ApplicationType.WebApi | ApplicationType.MessageBus)
        {
        }

        public override void Execute(TaskArgument<System.Web.HttpApplication> taskArg)
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            this.RegisterInjector(windsorContainer);
            this.RegisterControllerFactory(windsorContainer);

            IBaseContainer baseContainer = new CastleContainer(windsorContainer);
            IoC.CreateInstance(baseContainer);

            AssemblyHelper.ExecuteTasks<IBootstrapper, IBaseContainer>(IoC.Container);
        }

        private void RegisterInjector(IWindsorContainer container)
        {
            container.Register(
                Component.For<IWindsorContainer>()
                .Instance(container));
        }

        private void RegisterControllerFactory(IWindsorContainer container)
        {
            var controllerFactory = new ControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}