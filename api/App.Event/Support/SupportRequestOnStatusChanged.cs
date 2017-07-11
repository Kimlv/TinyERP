using App.Common;
using App.Common.Event;
using System;

namespace App.Event.Support
{
    public class SupportRequestOnStatusChanged : BaseEvent
    {
        
        public SupportRequestOnStatusChanged(Guid itemId, ItemStatus status) 
        {
            this.SupportRequestId = itemId;
            this.Status = status;
        }

        public Guid SupportRequestId { get; set; }
        public ItemStatus Status { get; set; }
    }
}
