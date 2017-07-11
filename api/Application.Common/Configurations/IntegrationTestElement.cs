namespace App.Common.Configurations
{
    using System.Configuration;

    public class IntegrationTestElement : ConfigurationElement
    {
        [ConfigurationProperty("baseUrl")]
        public string BaseUrl
        {
            get { return (string)this["baseUrl"]; }
        }
    }
}