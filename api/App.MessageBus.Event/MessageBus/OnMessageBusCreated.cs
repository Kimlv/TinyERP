namespace App.MessageBus.Event.MessageBus
{
    public class OnMessageBusCreated : App.Common.Event.BaseEvent
    {
        public string Key { get; protected set; }
        public string Content { get; protected set; }
        public OnMessageBusCreated(string key, string content)
        {
            this.Key = key;
            this.Content = content;
        }
    }
}
