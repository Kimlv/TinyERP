using App.Common.DI;

namespace App.Command.Impl
{
    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All)
        {
        }

        public override void Execute(IBaseContainer context)
        {
            if (!this.IsValid(this.ApplicationType)) { return; }
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<App.Command.Order.CreateOrderRequest>, App.Command.Impl.Order.OrderCommandHandler>("CreateOrderRequest");
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<App.Command.Order.AddOrderLineRequest>, App.Command.Impl.Order.OrderCommandHandler>("AddOrderLineRequest");
            context.RegisterSingleton<App.Common.Command.IBaseCommandHandler<App.Command.Order.ActivateOrder>, App.Command.Impl.Order.OrderCommandHandler>("ActivateOrder");
        }
    }
}
