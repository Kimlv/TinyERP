namespace App.Common.Configurations.EventHandler
{
    using System.Configuration;
    public class AggregateOption : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }
        [ConfigurationProperty("repoType")]
        public RepositoryType RepoType
        {
            get
            {
                return (RepositoryType)this["repoType"];
            }
        }
        [ConfigurationProperty("connectionStringName")]
        public string ConnectionStringName
        {
            get
            {
                return (string)this["connectionStringName"];
            }
        }
    }
}
