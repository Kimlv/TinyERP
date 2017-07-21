namespace App.MessageBus.Aggregate
{
    using App.Common;
    using App.Common.Data;
    public class EventSubcriber : BaseEntity
    {
        public string Key { get; set; }
        public string Uri { get; set; }
        public BusEventSubcriberStatus Status { get; set; }
        public string ModuleName { get; set; }
        /// <summary>
        /// For EF only
        /// </summary>
        public EventSubcriber(){}
        public EventSubcriber(string key, string uri, string moduleName)
        {
            this.Key = key;
            this.Uri = uri;
            this.ModuleName = moduleName;
            this.Status = BusEventSubcriberStatus.Active;
        }
    }
}
