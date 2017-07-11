namespace App.Common.Configurations.CommandHandler
{
    using Helpers;
    using System.Configuration;
    public class CommandHandlerOption : ConfigurationElement
    {
        [ConfigurationProperty("aggregate", IsKey = true, IsRequired = true)]
        public string Aggregate
        {
            get
            {
                return (string)this["aggregate"];
            }
        }
        [ConfigurationProperty("type")]
        public CommandHandlerStategyType Type
        {
            get
            {
                return (CommandHandlerStategyType)this["type"];
            }
        }
    }
}
