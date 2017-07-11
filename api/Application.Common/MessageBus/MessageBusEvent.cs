namespace App.Common.MessageBus
{
    public class MessageBusEvent
    {
        public string Key { get; protected set; }
        public string Content { get; protected set; }
        /// <summary>
        /// Please do not use this constructor
        /// </summary>
        public MessageBusEvent()
        {

        }
        public MessageBusEvent(string key, string content)
        {
            this.Key = key;
            this.Content = content;
        }
    }
}
