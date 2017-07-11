namespace App.EventHandler.Support
{
    using App.Common;
    using App.Common.Event;
    using System;

    public class SupportRequestOnStatusChanged : BaseEvent
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public ItemStatus CurrentStatus { get; set; }
        public ItemStatus NewStatus { get; set; }
        public string RequestorEmail { get; set; }
        public SupportRequestOnStatusChanged(Guid itemId, string subject, ItemStatus currentStatus, ItemStatus newStatus, string requestorEmail) : base()
        {
            this.Id = itemId;
            this.Subject = subject;
            this.CurrentStatus = currentStatus;
            this.NewStatus = newStatus;
            this.RequestorEmail = requestorEmail;
        }
    }
}
