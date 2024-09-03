using MassTransit;
using MassTransit.Mediator;
public class AddActivityMassTransithandle : RequestHandler<AddActivityMassTransitCommand, ConfirmActivityCommandOutput>
{
    private readonly OrderProvider provider;

    public OrderStatusRequestHandler(OrderProvider provider)
    {
        this.provider = provider;
    }

    protected override async Task<ValidationErrors> Validate(GetOrderStatus request, CancellationToken token)
    {
        if (request.OrderId <= 0)
        {
            return new ValidationErrors(new[] {$"Order ID {request.OrderId} is invalid."});
        }

        return new ValidationErrors();
    }

    protected override async Task<OrderStatusResult> Handle(GetOrderStatus request, CancellationToken cancellationToken)
    {
        var order = await provider.GetByIdAsync(request.OrderId);

        return new OrderStatusResult(order.Id, order.State);
    }
}
