using AlmacenEconomia.Domain.Events.ProductEnter.UpdatePriceCup;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.ProductEnter;

public class UpdateEnterPriceCupEventHandler : INotificationHandler<UpdateEnterPriceCupEvent>
{
    private readonly ILogger<UpdateEnterPriceCupEventHandler> logger;
    public UpdateEnterPriceCupEventHandler(ILogger<UpdateEnterPriceCupEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateEnterPriceCupEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el precio en cup: "+notification.NewPriceCup+" y tambien el precion en usd: "+notification.NewPriceUsd+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}