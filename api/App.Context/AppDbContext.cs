namespace App.Context
{
    using App.Entity.Security;
    using System.Data.Entity;
    using App.Common;
    using App.Entity.ProductManagement;
    using App.Entity.Common;
    using App.Entity.Store;
    using App.Entity.Setting;
    using App.Entity.Support;
    using App.Entity.Inventory;

    public class AppDbContext : App.Common.Data.MSSQL.MSSQLDbContext
    {
        /*Order*/
        //public System.Data.Entity.DbSet<OrderAggregate> OrderAggregates { get; set; }
        //public System.Data.Entity.DbSet<OrderCustomerDetail> CustomerDetails { get; set; }
        //public System.Data.Entity.DbSet<OrderLine> OrderLines { get; set; }

        public System.Data.Entity.DbSet<FileUpload> FileUploads { get; set; }
        public System.Data.Entity.DbSet<Product> Products { get; set; }
        public System.Data.Entity.DbSet<ProductCategory> ProductCategories { get; set; }
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<ContentType> ContentTypes { get; set; }
        public System.Data.Entity.DbSet<Permission> Permissions { get; set; }
        public System.Data.Entity.DbSet<Role> Roles { get; set; }
        public System.Data.Entity.DbSet<UserGroup> UserGroups { get; set; }
        public System.Data.Entity.DbSet<StoreAccount> Accounts { get; set; }
        public System.Data.Entity.DbSet<Store> Stores { get; set; }
        public System.Data.Entity.DbSet<Order> Orders { get; set; }
        public System.Data.Entity.DbSet<OrderContact> OrderContacts { get; set; }
        public System.Data.Entity.DbSet<OrderItem> OrderItems { get; set; }
        public System.Data.Entity.DbSet<Parameter> Parameters { get; set; }
        public System.Data.Entity.DbSet<Request> Requests { get; set; }
        public System.Data.Entity.DbSet<Category> Categories { get; set; }
        public System.Data.Entity.DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public AppDbContext(IOMode mode = IOMode.Read, string connectionName = "") : base(new App.Common.Data.MSSQL.MSSQLConnectionString(connectionName), mode)
        {
            Database.SetInitializer<AppDbContext>(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasMany(order => order.Items);
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Permission>().ToTable("Permissions");

            modelBuilder.Entity<Role>()
                .HasMany(role => role.Permissions)
                .WithMany(per => per.Roles)
                .Map(m =>
                {
                    m.MapLeftKey("RoleId");
                    m.MapRightKey("PermissionId");
                    m.ToTable("PermissionInRoles");
                });

            modelBuilder.Entity<UserGroup>()
                .HasMany(ug => ug.Permissions)
                .WithMany(per => per.UserGroups)
                .Map(m =>
                {
                    m.MapLeftKey("UserGroupId");
                    m.MapRightKey("PermissionId");
                    m.ToTable("PermissionInUserGroups");
                });
        }
    }
}
