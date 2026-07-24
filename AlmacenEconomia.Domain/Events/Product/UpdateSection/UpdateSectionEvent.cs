using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Product.UpdateSection;

public class UpdateSectionEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string ProductId {get ;}
    public string Section {get ;}
    public UpdateSectionEvent(string productId , string section)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ProductId = productId;
        Section = section;
    }
}