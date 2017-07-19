namespace App.Common.Event
{
    using App.Common.Validation.Attribute;
    public class RegisterEventSubcriber
    {
        [Required("messageBus.eventSubcriber.keyIsRequired")]
        public string Key { get; set; }
        [Required("messageBus.eventSubcriber.uriIsRequired")]
        public string Uri { get; set; }
        public string ModuleName { get; set; }
        public RegisterEventSubcriber(string key, string module, string uri)
        {
            this.Key = key;
            this.ModuleName = module;
            this.Uri = uri;
        }
    }
}