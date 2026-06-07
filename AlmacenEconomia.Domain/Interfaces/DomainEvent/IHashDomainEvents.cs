namespace AlmacenEconomia.Domain.Interfaces.DomainEvent;
public interface IHasDomainEvents
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents {get;}
    public void AddDomainEvent(IDomainEvent domainEvent);
    public void ClearDomainEvent();

}