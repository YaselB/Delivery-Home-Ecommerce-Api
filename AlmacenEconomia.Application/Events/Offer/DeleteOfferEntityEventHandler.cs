using AlmacenEconomia.Domain.Events.Offer.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Offer;

public class DeleteOfferEntityEventHandler : INotificationHandler<DeleteOfferEntityEvent>
{
    private readonly ILogger<DeleteOfferEntityEventHandler> logger;
    public DeleteOfferEntityEventHandler(ILogger<DeleteOfferEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(DeleteOfferEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha eliminado la oferta: "+notification.OfferName+" con id: "+notification.OfferId+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}