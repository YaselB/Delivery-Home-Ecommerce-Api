using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Combo.Delete;

public class DeleteComboEntityEvent : IDomainEvent, INotification
{
    public string id {get ;}

    public DateTime CreatedAt {get ;}
    public string ComboId {get ;}
    public string ComboName {get ;}
    public DeleteComboEntityEvent(string comboId , string comboName)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ComboId = comboId;
        ComboName = comboName;
    }
}