using AlmacenEconomia.Domain.Events.Product.UpdatePrice;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Product;

public class UpdateProductPriceEventHandler : INotificationHandler<UpdateProductPriceEvent>
{
    private readonly ILogger<UpdateProductPriceEventHandler> logger;
    public UpdateProductPriceEventHandler(ILogger<UpdateProductPriceEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateProductPriceEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el producto con nombre: "+notification.ProductName+" y el nuevo precio: "+notification.NewPrice+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}