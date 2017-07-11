namespace App.Entity.Support
{
    using App.Common;
    using App.Common.Data;

    public class Request : BaseEntity
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public ItemStatus Status { get; set; }
        public Request()
        {
        }

        public Request(string subject, string description, string email)
        {
            this.Subject = subject;
            this.Description = description;
            this.Email = email;
            this.Status = ItemStatus.New;
        }
    }
}
