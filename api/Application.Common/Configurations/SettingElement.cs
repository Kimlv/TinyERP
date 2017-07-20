namespace App.Common.Configurations
{
    using System.Configuration;
    public class SettingElement : ConfigurationElement
    {
        [ConfigurationProperty("baseUri")]
        public string BaseUri
        {
            get
            {
                return (string)this["baseUri"];
            }
        }
    }
}
