namespace App.MessageBus.Service.Impl
{
    using App.Common.DI;
    using App.MessageBus.Service.EventSubcriber;

    public class Bootstrap : App.Common.Tasks.BaseTask<IBaseContainer>, IBootstrapper
    {
        public Bootstrap() : base(App.Common.ApplicationType.All) { }

        public override void Execute(IBaseContainer context)
        {
            context.RegisterSingleton<IEventSubcriberService, EventSubcriberService>();
        }
    }
}