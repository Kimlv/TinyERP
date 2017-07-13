namespace App.MessageBus.Service.EventSubcriber
{
    using App.Common.Validation.Attribute;
    public class RegisterEventSubcriber
    {
        [Required("messageBus.eventSubcriber.keyIsRequired")]
        public string Key { get; set; }
        [Required("messageBus.eventSubcriber.uriIsRequired")]
        public string Uri { get; set; }
        [Required("messageBus.eventSubcriber.moduleNameIsRequired")]
        public string ModuleName { get; set; }
    }
}