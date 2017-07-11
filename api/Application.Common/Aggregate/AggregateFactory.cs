namespace App.Common.Aggregate
{
    using App.Common.Data;
    using App.Common.Helpers;
    public class AggregateFactory
    {
        public static TEntity Create<TEntity>() where TEntity : BaseEntity
        {
            return ObjectHelper.CreateInstance<TEntity>();
        }
    }
}
