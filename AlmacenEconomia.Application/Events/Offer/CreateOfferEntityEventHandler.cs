using AlmacenEconomia.Domain.Events.Offer.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Offer;

public class CreateOfferEntityEventHandler : INotificationHandler<CreateOfferEntityEvent>
{
    private readonly ILogger<CreateOfferEntityEventHandler> logger;
    public CreateOfferEntityEventHandler(ILogger<CreateOfferEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateOfferEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado una oferta con id: "+notification.OfferId+" y con nombre: "+notification.OfferName+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}