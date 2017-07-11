namespace App.MessageBus.CommandHandler.Impl
{
    using App.Common.DI;
    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.MessageBus)
        {
        }

        public override void Execute(IBaseContainer context)
        {
            if (!this.IsValid(this.ApplicationType)) { return; }
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<App.MessageBus.CommandHandler.BusEvent.CreateBusEventRequest>, App.MessageBus.CommandHandler.Impl.BusEvent.BusEventCommandHandler>("MessageBus_CreateBusEventRequest");
        }
    }
}
