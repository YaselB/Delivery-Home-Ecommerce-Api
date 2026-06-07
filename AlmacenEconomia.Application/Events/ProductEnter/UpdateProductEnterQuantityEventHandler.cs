using AlmacenEconomia.Domain.Events.ProductEnter.UpdateQuantity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.ProductEnter;

public class UpdateProductEnterQuantityEventHandler : INotificationHandler<UpdateProductEnterQuantityEvent>
{
    private readonly ILogger<UpdateProductEnterQuantityEventHandler> logger;
    public UpdateProductEnterQuantityEventHandler(ILogger<UpdateProductEnterQuantityEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(UpdateProductEnterQuantityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la cantidad de la entrada con id: "+notification.ProductEnterId+" con la nueva cantidad: "+notification.ProductEnterQuantity+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}