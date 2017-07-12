namespace App.Order.Query
{
    using App.Common.DI;
    using App.Common.Tasks;
    public class Bootstrap : BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All){}
        public override void Execute(IBaseContainer context)
        {
            context.RegisterTransient<App.Order.Query.IOrderQuery, App.Order.Query.OrderQuery>();
        }
    }
}
