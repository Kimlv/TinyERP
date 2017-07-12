namespace App.Order.Repository
{
    using App.Common.DI;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All) { }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterTransient<App.Order.Repository.IOrderRepository, App.Order.Repository.OrderRepository>();
        }
    }
}