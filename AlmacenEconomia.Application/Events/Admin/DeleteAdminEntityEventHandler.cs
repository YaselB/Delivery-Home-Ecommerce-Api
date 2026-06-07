using AlmacenEconomia.Domain.Events.Admin.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Admin;

public class DeleteAdminEntityEventHandler : INotificationHandler<DeleteAdminEntityEvent>
{
    private readonly ILogger<DeleteAdminEntityEventHandler> logger;
    public DeleteAdminEntityEventHandler(ILogger<DeleteAdminEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(DeleteAdminEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("El admin con id: "+notification.AdminId+" y userName: "+notification.Email+" ha sido eliminado satisfactoriamente en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}