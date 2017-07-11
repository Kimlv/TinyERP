namespace App.Event.Order
{
    using System;
    using App.Common.Event;
    public class OnOrderActivated : BaseEvent
    {
        public Guid OrderId { get; set; }
        public OnOrderActivated(Guid id)
        {
            this.OrderId = id;
        }
    }
}
