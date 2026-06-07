using AlmacenEconomia.Domain.Events.ProductEnter.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.ProductEnter;

public class CreateProductEnterEntityEventHandler : INotificationHandler<CreateProductEnterEntityEvent>
{
    private readonly ILogger<CreateProductEnterEntityEventHandler> logger;
    public CreateProductEnterEntityEventHandler(ILogger<CreateProductEnterEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateProductEnterEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado una entrada de producto con codigo: "+notification.ProductEnterCode+" y con id: "+notification.id+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}