namespace App.Common.Configurations.CommandHandler
{
    using System.Configuration;
    public class CommandHandlerSettingsElement : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CommandHandlerOption();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CommandHandlerOption)element).Aggregate;
        }

        public CommandHandlerOption this[int index]
        {
            get
            {
                return (CommandHandlerOption)this.BaseGet(index);
            }

            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        public new CommandHandlerOption this[string name]
        {
            get
            {
                return (CommandHandlerOption)this.BaseGet(name);
            }
        }

        public int IndexOf(CommandHandlerOption item)
        {
            return this.BaseIndexOf(item);
        }

        public void Add(CommandHandlerOption url)
        {
            this.BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            this.BaseAdd(element, false);
        }

        public void Remove(CommandHandlerOption item)
        {
            if (this.BaseIndexOf(item) >= 0)
            {
                this.BaseRemove(item.Aggregate);
            }
        }

        public void RemoveAt(int index)
        {
            this.BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            this.BaseRemove(name);
        }

        public void Clear()
        {
            this.BaseClear();
        }

        public System.Collections.Generic.IList<CommandHandlerOption> ToList()
        {
            System.Collections.Generic.IList<CommandHandlerOption> items = new System.Collections.Generic.List<CommandHandlerOption>();
            object[] keys = this.BaseGetAllKeys();
            foreach (object key in keys)
            {
                var item = (CommandHandlerOption)this.BaseGet(key);
                items.Add(item);
            }

            return items;
        }
    }
}
