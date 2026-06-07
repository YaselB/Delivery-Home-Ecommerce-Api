using AlmacenEconomia.Domain.Events.Offer.UpdateProductList;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Offer;

public class UpdateProductListEventHandler : INotificationHandler<UpdateProductListEvent>
{
    private readonly ILogger<UpdateProductListEventHandler> logger;
    public UpdateProductListEventHandler(ILogger<UpdateProductListEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateProductListEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la lista de productos de la oferta: "+notification.OfferId+" con el nombre: "+notification.OfferName+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}