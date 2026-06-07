using AlmacenEconomia.Domain.Events.Product.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Product;

public class CreateProductEntityEventHandler : INotificationHandler<CreateProductEntityEvent>
{
    private readonly ILogger<CreateProductEntityEventHandler> logger;
    public CreateProductEntityEventHandler(ILogger<CreateProductEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateProductEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un nuevo producto con id: "+notification.ProductId+" y con nombre: "+notification.ProductName+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}