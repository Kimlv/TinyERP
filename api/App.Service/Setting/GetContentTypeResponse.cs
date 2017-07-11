namespace App.Service.Setting
{
    using App.Common.Data;
    using App.Common.Mapping;
    using System.Collections.Generic;

    public class GetContentTypeResponse : BaseContent, IMappedFrom<App.Entity.Setting.ContentType>
    {
        public IList<App.Entity.Common.Parameter> Parameters { get; set; }
        public GetContentTypeResponse() : base()
        {
            this.Parameters = new List<App.Entity.Common.Parameter>();
        }
    }
}
