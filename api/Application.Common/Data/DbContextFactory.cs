namespace App.Common.Data
{
    using App.Common.DI;
    using Helpers;

    public class DbContextFactory
    {
        public static IDbContext Create(DbContextOption option)
        {

            //if (option.DbContextType != null)
            //{
            //    return (IDbContext)ObjectHelper.CreateInstance(option.DbContextType, new object[] { option.IOMode, option.ConnectionStringName });
            //}
            IDbContextResolver resolver = IoC.Container.Resolve<IDbContextResolver>();
            return resolver.Resolve(option);
        }
    }
}
