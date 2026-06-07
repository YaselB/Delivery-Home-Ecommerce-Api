using AlmacenEconomia.Domain.Events.Product.UpdateProductQuantityEvent;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Product;

public class UpdateProductQuantityEventHandler : INotificationHandler<UpdateProductQuantityEvent>
{
    private readonly ILogger<UpdateProductQuantityEventHandler> logger;
    public UpdateProductQuantityEventHandler(ILogger<UpdateProductQuantityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateProductQuantityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el producto: "+notification.ProductName+" con la nueva cantidad: "+notification.NewQuantity+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}