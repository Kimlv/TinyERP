namespace App.Security.EventHandler.Impl
{
    using App.Security.Event;
    using App.Common.Helpers;
    using App.Common.Logging;
    using App.Common.DI;
    using App.Security.Event.User;
    using System;

    internal class SecurityEventHandler : ISecurityEventHandler
    {
        public void Execute(OnUserCreated context)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("OnUserCreated:{0}", ObjectHelper.ToJson(context));
        }

        public void Execute(OnUserRoleAdded context)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("OnUserRoleAdded:{0}", ObjectHelper.ToJson(context));
        }
    }
}
