namespace App.Service.Support
{
    using App.Common;
    using App.Common.Data;
    using App.Common.Mapping;

    public class SupportRequestListItem : BaseEntity, IMappedFrom<App.Entity.Support.Request>
    {
        public string Subject { get; set; }
        public ItemStatus Status { get; set; }
        public string Email { get; set; }
    }
}