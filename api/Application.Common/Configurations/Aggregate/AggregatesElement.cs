namespace App.Common.Configurations.EventHandler
{
    using System.Configuration;
    public class AggregatesElement : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AggregateOption();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AggregateOption)element).Name;
        }

        public AggregateOption this[int index]
        {
            get
            {
                return (AggregateOption)this.BaseGet(index);
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

        public new AggregateOption this[string name]
        {
            get
            {
                return (AggregateOption)this.BaseGet(name);
            }
        }

        public int IndexOf(AggregateOption item)
        {
            return this.BaseIndexOf(item);
        }

        public void Add(AggregateOption option)
        {
            this.BaseAdd(option);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            this.BaseAdd(element, false);
        }

        public void Remove(AggregateOption item)
        {
            if (this.BaseIndexOf(item) >= 0)
            {
                this.BaseRemove(item.Name);
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

        public System.Collections.Generic.IList<AggregateOption> ToList()
        {
            System.Collections.Generic.IList<AggregateOption> items = new System.Collections.Generic.List<AggregateOption>();
            object[] keys = this.BaseGetAllKeys();
            foreach (object key in keys)
            {
                var item = (AggregateOption)this.BaseGet(key);
                items.Add(item);
            }

            return items;
        }
    }
}
