namespace App.Entity.Setting
{
    using App.Common.Data;

    public class ContentType : BaseContent
    {
        public ContentType() : base()
        {
        }

        public ContentType(string name, string key, string description) : base(name, key, description)
        {
        }
    }
}
