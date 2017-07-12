namespace App.Order.Event
{
    using App.Common.Event;
    using Common;
    using System;
    public class OnOrderCreated : BaseEvent
    {
        public Guid OrderId { get; set; }
        public OnOrderCreated(Guid orderId):base(EventPriority.High)
        {
            this.OrderId = orderId;
        }
    }
}
