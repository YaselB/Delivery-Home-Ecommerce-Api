using AlmacenEconomia.Domain.Events.ProductEnter.UpdateCode;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.ProductEnter;

public class UpdateProductEnterCodeEventHandler : INotificationHandler<UpdateProductEnterCode>
{
    private readonly ILogger<UpdateProductEnterCodeEventHandler> logger;
    public UpdateProductEnterCodeEventHandler(ILogger<UpdateProductEnterCodeEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(UpdateProductEnterCode notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado el codigo de la entrada con id: "+notification.ProductEnterId+" con el nuevo codigo: "+notification.ProductEnterCode+"en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}