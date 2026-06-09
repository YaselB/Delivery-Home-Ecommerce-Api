using AlmacenEconomia.Domain.Events.HomeSale.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.HomeSale;

public class CreateHomeSaleEventHandler : INotificationHandler<CreateHomeSaleEvent>
{
    private readonly ILogger<CreateHomeSaleEventHandler> logger;
    public CreateHomeSaleEventHandler(ILogger<CreateHomeSaleEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateHomeSaleEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado una salida para la casa con id: "+notification.HomeSaleId+" con un gasto de :"+notification.Total+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}