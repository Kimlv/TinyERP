namespace App.Order.Command
{
    using App.Common.Command;
    using System.Collections.Generic;
    public class CreateOrderRequest : BaseCommand
    {
        public CustomerDetail CustomerDetail { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
    }
}
