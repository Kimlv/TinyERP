namespace App.Order.Event
{
    using System;
    using App.Common.Event;
    public class OnCustomerDetailChanged : BaseEvent
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public OnCustomerDetailChanged(Guid orderId, string customerName) : base()
        {
            this.OrderId = orderId;
            this.CustomerName = customerName;
        }
    }
}
