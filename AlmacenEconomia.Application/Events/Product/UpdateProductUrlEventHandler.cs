using AlmacenEconomia.Domain.Events.Product.UpdateUrl;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Product;

public class UpdateProductUrlEventHandler : INotificationHandler<UpdateProductUrlEvent>
{
    private readonly ILogger<UpdateProductUrlEventHandler> logger;
    public UpdateProductUrlEventHandler(ILogger<UpdateProductUrlEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateProductUrlEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el producto: "+notification.ProductName+" con la nueva url: "+notification.NewUrl+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}