namespace App.Common.Configurations
{
    using System.Configuration;

    public class LocalizationElement : ConfigurationElement
    {
        [ConfigurationProperty("defaultLanguageCode")]
        public string DefaultLanguageCode
        {
            get { return (string)this["defaultLanguageCode"]; }
        }
    }
}
