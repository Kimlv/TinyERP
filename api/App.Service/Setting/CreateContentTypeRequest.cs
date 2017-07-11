namespace App.Service.Setting
{
    using System.Collections.Generic;

    public class CreateContentTypeRequest
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public IList<App.Entity.Common.Parameter> Parameters { get; set; }

        public CreateContentTypeRequest(string name, string key, string description)
        {
            this.Name = name;
            this.Key = key;
            this.Description = description;
        }

        public CreateContentTypeRequest()
        {
            this.Parameters = new List<App.Entity.Common.Parameter>();
        }
    }
}
