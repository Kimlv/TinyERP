namespace App.MessageBus.Aggregate
{
    using App.Common.Aggregate;
    using Common.MessageBus;

    public class BusEventAggregate : BaseAggregateRoot
    {
        public MessageBusEvent Content { get; set; }

        public void CreateEventContent(string key, string content)
        {
            this.Content = new MessageBusEvent(key, content);
            this.AddEvent(new Event.MessageBus.OnMessageBusCreated(key, content));
        }
    }
}
