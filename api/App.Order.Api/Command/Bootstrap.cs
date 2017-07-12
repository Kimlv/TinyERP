namespace App.Order.Command
{
    using App.Common.DI;
    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All)
        {
        }

        public override void Execute(IBaseContainer context)
        {
            if (!this.IsValid(this.ApplicationType)) { return; }
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<CreateOrderRequest>, OrderCommandHandler>("CreateOrderRequest");
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<AddOrderLineRequest>, OrderCommandHandler>("AddOrderLineRequest");
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<ActivateOrder>, OrderCommandHandler>("ActivateOrder");
        }
    }
}
