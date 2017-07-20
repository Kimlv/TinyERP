namespace App.Common.Configurations
{
    using CommandHandler;
    using EventHandler;
    using System.Configuration;

    public class Configuration : System.Configuration.ConfigurationSection
    {
        private static Configuration current;
        public static Configuration Current
        {
            get
            {
                if (current == null)
                {
                    current = (Configuration)System.Configuration.ConfigurationManager.GetSection("appconfiguration");
                }

                return current;
            }
        }

        [ConfigurationProperty("settings")]
        public App.Common.Configurations.SettingElement Setting
        {
            get
            {
                return (App.Common.Configurations.SettingElement)this["settings"];
            }
        }
        [ConfigurationProperty("messageBus")]
        public MessageBusElement MessageBus
        {
            get
            {
                return (MessageBusElement)this["messageBus"];
            }
        }

        [ConfigurationProperty("authentication")]
        public AuthenticationElement Authentication
        {
            get
            {
                return (AuthenticationElement)this["authentication"];
            }
        }

        [ConfigurationProperty("paging")]
        public PagingElement Paging
        {
            get
            {
                return (PagingElement)this["paging"];
            }
        }

        [ConfigurationProperty("repository")]
        public RepositoryElement Repository
        {
            get
            {
                return (RepositoryElement)this["repository"];
            }
        }

        [ConfigurationProperty("databases", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(DatabasesElement),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public DatabasesElement Databases
        {
            get
            {
                return (DatabasesElement)this["databases"];
            }
        }

        [ConfigurationProperty("localization")]
        public LocalizationElement Localization
        {
            get
            {
                return (LocalizationElement)this["localization"];
            }
        }

        [ConfigurationProperty("uitest")]
        public UITestElement UITest
        {
            get
            {
                return (UITestElement)this["uitest"];
            }
        }

        [ConfigurationProperty("mail")]
        public MailElement Mail
        {
            get
            {
                return (MailElement)this["mail"];
            }
        }

        [ConfigurationProperty("folder")]
        public FolderElement Folder
        {
            get
            {
                return (FolderElement)this["folder"];
            }
        }

        [ConfigurationProperty("integration-test")]
        public IntegrationTestElement IntegrationTest
        {
            get
            {
                return (IntegrationTestElement)this["integration-test"];
            }
        }


        [ConfigurationProperty("commandHandlerSettings", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(CommandHandlerSettingsElement),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public CommandHandlerSettingsElement CommandHandlerSettings
        {
            get
            {
                return (CommandHandlerSettingsElement)this["commandHandlerSettings"];
            }
        }

        [ConfigurationProperty("eventHandlers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(EventHandlersElement),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public EventHandlersElement EventHandlers
        {
            get
            {
                return (EventHandlersElement)this["eventHandlers"];
            }
        }

        [ConfigurationProperty("aggregates", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(AggregatesElement),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public AggregatesElement Aggregates
        {
            get
            {
                return (AggregatesElement)this["aggregates"];
            }
        }
    }
}
