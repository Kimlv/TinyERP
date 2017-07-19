namespace App.MessageBus.Service.EventSubcriber
{
    using App.Common.Event;
    public interface IEventSubcriberService
    {
        void Register(RegisterEventSubcriber request);
    }
}
