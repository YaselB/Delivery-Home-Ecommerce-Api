using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Admin.Create;

public class CreateAdminEntityEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string AdminId {get ;}
    public string adminEmail {get ;}
    public CreateAdminEntityEvent(string adminId , string email)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        AdminId = adminId;
        adminEmail = email;
    }
}