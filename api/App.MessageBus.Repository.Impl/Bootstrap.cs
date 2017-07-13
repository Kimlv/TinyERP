namespace App.MessageBus.Repository.Impl
{
    using App.Common.DI;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.MessageBus) { }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterTransient<IBusEventRepository, BusEventRepository>();
            context.RegisterTransient<IEventSubcriberRepository, EventSubcriberRepository>();
        }
    }
}