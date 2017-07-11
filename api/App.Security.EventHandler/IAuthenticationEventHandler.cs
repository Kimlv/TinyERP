namespace App.Security.EventHandler
{
    using App.Common.Event;
    using App.Security.Event.Authentication;
    public interface IAuthenticationEventHandler :
        IEventHandler<OnAuthenticationSuccess>,
        IEventHandler<OnAuthenticationFailed>
    {
    }
}
