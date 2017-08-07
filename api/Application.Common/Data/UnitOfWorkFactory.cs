namespace App.Common.Data
{
    using App.Common.Configurations;
    public sealed class UnitOfWorkFactory
    {
        public static IUnitOfWork Create(string connectionName)
        {
            RepositoryType repoType = UnitOfWorkFactory.GetRepoType(connectionName);
            return new UnitOfWork(repoType, connectionName);
        }

        private static RepositoryType GetRepoType(string connectionName)
        {
            ConnectionStringElement configElement = Configuration.Current.Databases[connectionName];
            return configElement != null ? configElement.DbType : Configuration.Current.Repository.DefaultRepoType;
        }
    }
}
