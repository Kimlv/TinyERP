namespace App.MessageBus.Aggregate
{
    using App.Common;
    using App.Common.Data;
    public class EventSubcriber: BaseEntity
    {
        public string Key { get; set; }
        public string Uri { get; set; }
        public BusEventSubcriberStatus Status { get; set; }
        public string ModuleName { get; set; }
    }
}
