namespace App.EventHandler.Order
{
    using App.Common.Event;
    using App.Event.Order;
    public interface IOrderEventHandler: 
        IEventHandler<OnCustomerDetailChanged>,
        IEventHandler<OnOrderLineItemAdded>,
        IEventHandler<OnOrderCreated>,
        IEventHandler<OnOrderActivated>
    {
    }
}
