using AlmacenEconomia.Domain.Events.Admin.AddPermissions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Admin;

public class AddPermissionAdminEntityEventHandler : INotificationHandler<AddPermissionsAdminEntityEvent>
{
    private readonly ILogger<AddPermissionAdminEntityEventHandler> log;
    public AddPermissionAdminEntityEventHandler(ILogger<AddPermissionAdminEntityEventHandler> logger)
    {
        log = logger;
    }
    public Task Handle(AddPermissionsAdminEntityEvent notification, CancellationToken cancellationToken)
    {
        log.LogInformation("Se le han agregado permisos al admin con id: "+notification.AdminId+" con email: "+notification.AdminEmail+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}