namespace App.Security.EventHandler
{
    using App.Common.Event;
    using App.Security.Event.User;

    public interface ISecurityEventHandler : 
        IEventHandler<App.Security.Event.OnUserCreated>,
        IEventHandler<OnUserRoleAdded>
    {
    }
}
