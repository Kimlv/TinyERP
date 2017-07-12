namespace App.Order.Event
{
    using App.Common.DI;
    using App.Common.Tasks;
    using Common.Event;

    public class Bootstrap : BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All)
        {
        }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterTransient<IEventHandler<OnCustomerDetailChanged>, App.Order.Event.OrderEventHandler>("OnCustomerDetailChanged");
            context.RegisterTransient<IEventHandler<OnOrderLineItemAdded>, OrderEventHandler>("OnOrderLineItemAdded");
            context.RegisterTransient<IEventHandler<OnOrderCreated>, OrderEventHandler>("OnOrderCreated");
            context.RegisterTransient<IEventHandler<OnOrderActivated>, OrderEventHandler>("OnOrderActivated");
        }
    }
}