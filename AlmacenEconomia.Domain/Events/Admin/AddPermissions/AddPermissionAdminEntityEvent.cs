using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Admin.AddPermissions;

public class AddPermissionsAdminEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string AdminId {get ;}
    public string AdminEmail {get ;}
    public AddPermissionsAdminEntityEvent(string adminId , string adminEmail)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        AdminId = adminId;
        AdminEmail = adminEmail;
    }
}