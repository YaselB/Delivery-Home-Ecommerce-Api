using AlmacenEconomia.Domain.Events.Admin.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Admin;

public class CreateAdminEntityEventHandler : INotificationHandler<CreateAdminEntityEvent>
{
    private readonly ILogger<CreateAdminEntityEventHandler> logger;
    public CreateAdminEntityEventHandler(ILogger<CreateAdminEntityEventHandler> logger)
    {
        this.logger = logger;
    }
    public Task Handle(CreateAdminEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se ha creado un nuevo admin con id: "+notification.AdminId+" y username: "+notification.adminEmail+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}