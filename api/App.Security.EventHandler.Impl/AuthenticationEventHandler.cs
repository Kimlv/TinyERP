namespace App.Security.EventHandler.Impl
{
    using App.Common.DI;
    using App.Common.Logging;
    using System;
    internal class AuthenticationEventHandler : IAuthenticationEventHandler
    {
        public void Execute(Security.Event.Authentication.OnAuthenticationFailed context)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("Authentication fail for \'{0}\' with \'{1}\' as password at {2}", context.UserName, context.Password, context.At);
        }

        public void Execute(Security.Event.Authentication.OnAuthenticationSuccess context)
        {
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("Authentication success for \'{0} {1} ({2})\' at \'{3}\', token:\'{4}\'", context.FirstName, context.LastName, context.Email, context.At, context.Token);
        }
    }
}
