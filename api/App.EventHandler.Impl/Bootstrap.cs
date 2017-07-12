namespace App.EventHandler.Impl
{
    using App.Common.DI;
    using App.Common.Tasks;
    public class Bootstrap : BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All)
        {
        }

        public override void Execute(IBaseContainer context)
        {
            //context.RegisterTransient<IEventHandler<OnCustomerDetailChanged>, App.EventHandler.Impl.Order.OrderEventHandler>("OnCustomerDetailChanged");
            //context.RegisterTransient<IEventHandler<OnOrderLineItemAdded>, App.EventHandler.Impl.Order.OrderEventHandler>("OnOrderLineItemAdded");
            //context.RegisterTransient<IEventHandler<OnOrderCreated>, App.EventHandler.Impl.Order.OrderEventHandler>("OnOrderCreated");
            //context.RegisterTransient<IEventHandler<OnOrderActivated>, App.EventHandler.Impl.Order.OrderEventHandler>("OnOrderActivated");
        }
    }
}