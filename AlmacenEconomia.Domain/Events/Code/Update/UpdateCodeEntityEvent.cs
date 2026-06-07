using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Code.Update;

public class UpdateCodeEntityEvent : IDomainEvent, INotification
{
    public string id {get ; }

    public DateTime CreatedAt { get ;}
    public string Code { get ;}
    public string Email {get ;}
    public DateTime ExpirationUtc {get ;}
    public UpdateCodeEntityEvent(string code , string email , DateTime Expires)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        Code = code;
        Email = email;
        ExpirationUtc = Expires;
    } 
}