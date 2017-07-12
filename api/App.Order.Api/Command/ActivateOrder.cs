namespace App.Order.Command
{
    using App.Common.Command;
    using System;

    public class ActivateOrder: BaseCommand
    {
        public Guid OrderId { get; set; }

        public ActivateOrder(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}
