using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Domain.Entity.Product;
using AlmacenEconomia.Domain.Events.Product.UpdateUnity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlamcenEconomia.Application.Events.Product;
public class UpdateProductUnityEventHandler : INotificationHandler<UpdateProductUnityEvent>
{
    private readonly ILogger<ProductEntity> logger;
    public UpdateProductUnityEventHandler(ILogger<ProductEntity> logger)
    {
        this.logger = logger;
    }

    public Task Handle(UpdateProductUnityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha actualizado la unidad de medida del producto: "+notification.ProductName+" ,la nueva unidad es: "+notification.NewUnity+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}