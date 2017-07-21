namespace App.MessageBus.EventHandler.Impl
{
    using App.Common.DI;
    using App.Common.Logging;
    using System;
    using System.Collections.Generic;
    using App.Common.Connector;
    using App.MessageBus.Aggregate;
    using App.MessageBus.Repository;

    internal class BusEventHandler : IBusEventHandler
    {
        public void Execute(App.MessageBus.Event.MessageBus.OnMessageBusCreated ev)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            IEventSubcriberRepository subcriberRepo = IoC.Container.Resolve<IEventSubcriberRepository>();

            IList<EventSubcriber> subcribers = subcriberRepo.GetAllActive(ev.Key);
            IConnector connector = ConnectorFactory.Create(Common.ConnectorType.REST);
            foreach (EventSubcriber subcriber in subcribers)
            {
                try
                {
                    connector.Post<bool>(subcriber.Uri, ev.Content);
                    logger.Info("'{0}' was sent to '{1}' at '{2}' with parameters '{3}'", ev.Key, subcriber.Uri, DateTime.UtcNow, ev.Content);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
        }
    }
}
