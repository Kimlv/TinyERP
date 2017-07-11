namespace App.Command.Order
{
    using App.Common.Command;
    public interface IOrderCommandHandler: 
        IBaseCommandHandler<CreateOrderRequest>,
        IBaseCommandHandler<AddOrderLineRequest>,
        IBaseCommandHandler<ActivateOrder>
    {
    }
}
