namespace App.Common.Configurations
{
    using System.Configuration;

    public class PagingElement : ConfigurationElement
    {
        [ConfigurationProperty("pageSize")]
        public int PageSize
        {
            get { return (int)this["pageSize"]; }
        }
    }
}
