namespace App.MessageBus.EventHandler.Impl
{
    using App.Common.DI;
    using App.Common.Event;
    using App.MessageBus.Event.MessageBus;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.MessageBus) { }

        public override void Execute(IBaseContainer context)
        {
            //context.RegisterTransient<IBusEventHandler, BusEventHandler>();
            context.RegisterTransient<IEventHandler<OnMessageBusCreated>, BusEventHandler>("App.MessageBus.Event.MessageBus.OnMessageBusCreated");
        }
    }
}