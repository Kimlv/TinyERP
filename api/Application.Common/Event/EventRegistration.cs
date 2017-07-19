namespace App.Common.Event
{
    public class EventRegistration
    {
        public string Uri { get; set; }
        public string EventClassName { get; set; }
        public string ModuleName { get; set; }
        public EventRegistration(string eventClassName, string uri, string moduleName = "")
        {
            this.EventClassName = eventClassName;
            this.Uri = uri;
            this.ModuleName = moduleName;
        }
    }
}
