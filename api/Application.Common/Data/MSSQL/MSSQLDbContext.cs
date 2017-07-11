namespace App.Common.Data.MSSQL
{
    using System.Collections.Generic;

    public abstract class MSSQLDbContext : System.Data.Entity.DbContext, IMSSQLDbContext
    {
        private IList<OnContextSaveChange> saveChangeEvents;
        private System.Data.Entity.DbContext context;
        protected IOMode Mode { get; private set; }
        public MSSQLDbContext(IConnectionString connection, IOMode mode = IOMode.Read) : base(connection.ToString())
        {
            this.Mode = mode;
            this.saveChangeEvents = new List<OnContextSaveChange>();
            this.context = this;
        }

        public void RegisterSaveChangeEvent(OnContextSaveChange ev)
        {
            this.saveChangeEvents.Add(ev);
        }

        public virtual void OnSaveChanged()
        {
            foreach (OnContextSaveChange ev in this.saveChangeEvents)
            {
                ev(this);
            }
        }
        public IDbSet<TEntity, TId> GetDbSet<TEntity, TId>() where TEntity : class, IBaseEntity<TId>
        {
            IDbSet<TEntity, TId> dbset = new App.Common.Data.MSSQL.MSSQLDbSet<TEntity, TId>(this, this.context, this.Mode);
            return dbset;
        }
    }
}