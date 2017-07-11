namespace App.Common.Configurations
{
    using System.Configuration;
    public class RepositoryElement : ConfigurationElement
    {
        [ConfigurationProperty("defaultRepoType")]
        public RepositoryType DefaultRepoType
        {
            get
            {
                return (RepositoryType)this["defaultRepoType"];
            }
        }

        [ConfigurationProperty("defaultConnectionStringName")]
        public string DefaultConnectionStringName
        {
            get
            {
                return (string)this["defaultConnectionStringName"];
            }
        }
    }
}
