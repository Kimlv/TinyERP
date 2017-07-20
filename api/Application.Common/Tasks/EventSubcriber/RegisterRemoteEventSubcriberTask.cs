namespace App.Common.Tasks.EventSubcriber
{
    using App.Common.Configurations;
    using App.Common.Connector;
    using App.Common.DI;
    using App.Common.Event;
    using App.Common.Helpers;
    using App.Common.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class RegisterRemoteEventSubcriberTask : BaseTask<TaskArgument<System.Web.HttpApplication>>, IApplicationInitializedTask<TaskArgument<System.Web.HttpApplication>>
    {
        public RegisterRemoteEventSubcriberTask() : base(ApplicationType.WebApi | ApplicationType.ExternalWebApi)
        {
        }

        public override void Execute(TaskArgument<System.Web.HttpApplication> arg)
        {
            if (!this.IsValid(arg.Type)) { return; }
            ILogger logger = IoC.Container.Resolve<ILogger>();
            string baseUri = Configuration.Current.Setting.BaseUri ;
            IList<Type> handlers = AssemblyHelper.GetTypes<RemoteEventSubcriberHandler>().ToList();
            IList<EventRegistration> registrations = EventHelper.GetSubcriberRequests(handlers);
            IConnector restConnector = ConnectorFactory.Create(ConnectorType.REST);
            foreach (EventRegistration registration in registrations)
            {
                string fullUri = String.Format("{0}/{1}", baseUri, registration.Uri);
                RegisterEventSubcriber subcriberEvent = new RegisterEventSubcriber(registration.EventClassName, registration.ModuleName, fullUri);
                restConnector.Post<RegisterEventSubcriber, string>(Configurations.Configuration.Current.MessageBus.RegisterEventSubciberUri, subcriberEvent);
                logger.Info("Register uri \'{0}\' to \'{1}\'", subcriberEvent, Configurations.Configuration.Current.MessageBus.RegisterEventSubciberUri);
            }
        }
    }
}
