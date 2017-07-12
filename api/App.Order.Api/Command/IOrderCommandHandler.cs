namespace App.Order.Command
{
    using App.Common.Command;
    public interface IOrderCommandHandler: 
        IBaseCommandHandler<CreateOrderRequest>,
        IBaseCommandHandler<AddOrderLineRequest>,
        IBaseCommandHandler<ActivateOrder>
    {
    }
}
