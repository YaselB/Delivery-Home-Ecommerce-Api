using AlmacenEconomia.Domain.Events.Admin.RevokePermissions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Events.Admin;

public class RevokePermissionsAdminEntityEventHandler : INotificationHandler<RevokePermissionsAdminEntityEvent>
{
    private readonly ILogger<RevokePermissionsAdminEntityEventHandler> logger;
    public RevokePermissionsAdminEntityEventHandler(ILogger<RevokePermissionsAdminEntityEventHandler> log){
        logger = log;
    }
    public Task Handle(RevokePermissionsAdminEntityEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Se le han revocado permisos al admin con id: "+notification.AdminId+" y email: "+notification.AdminEmail+" en la fecha: "+notification.CreatedAt+" y con idEvent: "+notification.id);
        return Task.CompletedTask;
    }
}