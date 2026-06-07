using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Admin.Update;

public class UpdateAdminEntityEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string AdminId{get;}
    public string Email {get;}
    public UpdateAdminEntityEvent(string adminId , string email)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        AdminId = adminId;
        Email = email;
    }
}