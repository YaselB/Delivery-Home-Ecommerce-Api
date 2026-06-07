using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;

namespace AlmacenEconomia.Domain.Events.Combo.UpdateName;

public class UpdateComboNameEvent : IDomainEvent, INotification
{
    public string id {get;}

    public DateTime CreatedAt {get;}
    public string ComboId {get ;}
    public string Name {get ;}
    public UpdateComboNameEvent(string comboId , string name)
    {
        id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        ComboId = comboId;
        Name = name;
    }
}