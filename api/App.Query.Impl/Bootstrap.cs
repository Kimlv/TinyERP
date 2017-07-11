namespace App.Query.Impl
{
    using App.Common.DI;
    using App.Common.Tasks;
    public class Bootstrap : BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All){}
        public override void Execute(IBaseContainer context)
        {
            context.RegisterTransient<App.Query.Order.IOrderQuery, App.Query.Impl.Order.OrderQuery>();
        }
    }
}
