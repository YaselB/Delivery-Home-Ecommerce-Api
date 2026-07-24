using AlmacenEconomia.Domain.Events.Product.UpdateSection;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Product;

public class UpdateSectionEventHandler : INotificationHandler<UpdateSectionEvent>
{
    private readonly ILogger<UpdateSectionEventHandler> logger;
    public UpdateSectionEventHandler(ILogger<UpdateSectionEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateSectionEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se actualizo un producto con id: "+notification.ProductId+" con la nueva seccion: "+notification.Section+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}