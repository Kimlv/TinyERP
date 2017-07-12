namespace App.Order.Event
{
    using App.Common.Event;
    public interface IOrderEventHandler: 
        IEventHandler<OnCustomerDetailChanged>,
        IEventHandler<OnOrderLineItemAdded>,
        IEventHandler<OnOrderCreated>,
        IEventHandler<OnOrderActivated>
    {
    }
}
