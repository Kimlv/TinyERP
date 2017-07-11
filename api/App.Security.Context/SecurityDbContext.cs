namespace App.Security.Context
{
    using App.Common.Data;
    using App.Security.Aggregate;
    using Common;
    using System.Data.Entity;

    public class SecurityDbContext : App.Common.Data.MSSQL.MSSQLDbContext, IDbContext<User>
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public SecurityDbContext(IOMode mode = IOMode.Read, string connectionName = "SecurityConnection") : base(new App.Common.Data.MSSQL.MSSQLConnectionString(connectionName), mode)
        {
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<SecurityDbContext>());
        }
    }
}
