using AlmacenEconomia.Domain.Entity.Product;
using AlmacenEconomia.Domain.Events.Product.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Product;

public class DeleteProductEntityEventHandler : INotificationHandler<DeleteProductEntityEvent>
{
    private readonly ILogger<ProductEntity> logger;
    public DeleteProductEntityEventHandler(ILogger<ProductEntity> logger)
    {
        this.logger = logger;
    }
    public Task Handle(DeleteProductEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Ha sido eliminado el producto: " + notification.ProductName + " y con id: " + notification.ProductId + " en la fecha: " + notification.CreatedAt + " y con idEvent: " + notification.id);
        return Task.CompletedTask;
    }
}