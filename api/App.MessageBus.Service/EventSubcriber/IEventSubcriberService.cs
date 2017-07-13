namespace App.MessageBus.Service.EventSubcriber
{
    public interface IEventSubcriberService
    {
        void Register(RegisterEventSubcriber request);
    }
}
