namespace App.Common.Configurations.EventHandler
{
    using System.Configuration;
    public class EventHandlersElement : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EventHandlerOption();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EventHandlerOption)element).EventName;
        }

        public EventHandlerOption this[int index]
        {
            get
            {
                return (EventHandlerOption)this.BaseGet(index);
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

        public new EventHandlerOption this[string name]
        {
            get
            {
                return (EventHandlerOption)this.BaseGet(name);
            }
        }

        public int IndexOf(EventHandlerOption item)
        {
            return this.BaseIndexOf(item);
        }

        public void Add(EventHandlerOption option)
        {
            this.BaseAdd(option);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            this.BaseAdd(element, false);
        }

        public void Remove(EventHandlerOption item)
        {
            if (this.BaseIndexOf(item) >= 0)
            {
                this.BaseRemove(item.EventName);
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

        public System.Collections.Generic.IList<EventHandlerOption> ToList()
        {
            System.Collections.Generic.IList<EventHandlerOption> items = new System.Collections.Generic.List<EventHandlerOption>();
            object[] keys = this.BaseGetAllKeys();
            foreach (object key in keys)
            {
                var item = (EventHandlerOption)this.BaseGet(key);
                items.Add(item);
            }

            return items;
        }
    }
}
