using AlmacenEconomia.Domain.Interfaces.DomainEvent;

namespace AlmacenEconomia.Domain.Common.Interface.Generic;
public interface IGenericEntity<T> 
{
    public string Id {get ; set ;}
    public DateTime CreatedAt {get ; set ;}
    public DateTime UpdatedAt {get ; set ;}
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get ;}
    public void AddDomainEvent(IDomainEvent domainEvent);
    public void RemoveDomainEvent(IDomainEvent domainEvent);
    public void ClearDomainEvent();
}