using AlmacenEconomia.Domain.Events.Offer.UpdateName;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Offer;

public class UpdateOfferNameEventHandler : INotificationHandler<UpdateOfferNameEvent>
{
    private readonly ILogger<UpdateOfferNameEventHandler> logger;
    public UpdateOfferNameEventHandler(ILogger<UpdateOfferNameEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateOfferNameEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la oferta con id:"+notification.OfferId+" y con nuevo nombre: "+notification.OfferName+" en la fecha: "+notification.CreatedAt+" y on idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}