namespace App.MessageBus.CommandHandler.BusEvent
{
    using App.Common.Command;
    using Common.Validation.Attribute;

    public class CreateBusEventRequest: BaseCommand
    {
        [Required("messageBus.createBusEvent.validation.keyIsRequire")]
        public string Key { get; set; }
        [Required("messageBus.createBusEvent.validation.contentIsRequire")]
        public string Content { get; set; }
    }
}