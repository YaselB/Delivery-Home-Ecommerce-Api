using AlmacenEconomia.Domain.Events.Offer.UpdatePrice;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Offer;

public class UpdateOfferPriceEventHandler : INotificationHandler<UpdateOfferPriceEvent>
{
    private readonly ILogger<UpdateOfferPriceEventHandler> logger;
    public UpdateOfferPriceEventHandler(ILogger<UpdateOfferPriceEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateOfferPriceEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la oferta con id: "+notification.OfferId+" con el nuevo precio: "+notification.Price+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}
