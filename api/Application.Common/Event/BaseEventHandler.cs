namespace App.Common.Event
{
    using App.Common.Tasks;

    public class BaseEventHandler<TEntity> : BaseTask<TEntity>, IEventHandler<TEntity> where TEntity : IEvent
    {
        public BaseEventHandler() : base(ApplicationType.All)
        {
        }

        public override bool IsValid(ApplicationType type)
        {
            return true;
        }
    }
}