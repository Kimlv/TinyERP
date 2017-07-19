namespace App.Common.Configurations
{
    using System.Configuration;
    public class MessageBusElement : ConfigurationElement
    {
        [ConfigurationProperty("baseUri")]
        public string BaseUri
        {
            get
            {
                return (string)this["baseUri"];
            }
        }

        public string RegisterEventSubciberUri
        {
            get
            {
                return string.Format("{0}/api/eventsubcribers", this.BaseUri);
            }
        }
    }
}
