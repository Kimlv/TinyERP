namespace App.Common.Configurations.EventHandler
{
    using System.Configuration;
    public class EventHandlerOption : ConfigurationElement
    {
        [ConfigurationProperty("eventName", IsKey = true, IsRequired = true)]
        public string EventName
        {
            get
            {
                return (string)this["eventName"];
            }
        }
        [ConfigurationProperty("type")]
        public EventHandlerStategyType Type
        {
            get
            {
                return (EventHandlerStategyType)this["type"];
            }
        }
    }
}
