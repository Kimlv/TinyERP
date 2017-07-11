namespace App.Common
{
    using App.Common.DI;
    using App.Common.Tasks;
    using Event;
    using MessageBus;
    using MessageBus.REST;

    public class Bootstrap : BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(ApplicationType.All){}

        public override void Execute(IBaseContainer context)
        {
            context.RegisterSingleton<App.Common.Logging.ILogger, App.Common.Logging.DefaultLogger>();
            context.RegisterSingleton<App.Common.Mail.IMailService, App.Common.Mail.MailService>();
            context.RegisterSingleton<App.Common.Event.IEventManager, App.Common.Event.BaseEventManager>();

            context.RegisterSingleton<IEventManagerStrategy, EventManagerStrategy>();

            context.RegisterSingleton<IMessageBusProvider, RESTMessageBusProvider>();
        }
    }
}