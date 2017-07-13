namespace App.Common.MessageBus.REST
{
    using Configurations;
    using Connector;
    using Http;
    internal class RESTMessageBusProvider : IMessageBusProvider
    {
        private IConnector connector;
        public RESTMessageBusProvider()
        {
            this.connector = ConnectorFactory.Create(ConnectorType.REST);
        }
        public void Send(MessageBusEvent eventItem)
        {
            string url =string.Format("{0}/{1}",Configuration.Current.MessageBus.BaseUri, "/api/busevents");
            /// Will handle response later
            IResponseData<CreateMessageBusEventResponse> response = this.connector.Post<MessageBusEvent, CreateMessageBusEventResponse>(url, eventItem);
        }
    }
}
