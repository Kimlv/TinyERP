namespace App.EventHandler.Support
{
    using App.Common.Event;

    public interface ISupportRequestEventHandler : IEventHandler<SupportRequestOnStatusChanged>
    {
    }
}
