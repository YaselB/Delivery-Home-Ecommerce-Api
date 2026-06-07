using System.ComponentModel.DataAnnotations;
using AlmacenEconomia.Domain.Common.Interface.Generic;
using AlmacenEconomia.Domain.Interfaces.DomainEvent;

namespace AlmacenEconomia.Domain.Entity.Generic;

public class GenericEntity<T> : IGenericEntity<T>, IHasDomainEvents
{
    [Key]
    public string Id{get ; set ;} = Guid.NewGuid().ToString();
    public DateTime CreatedAt{get ; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt{get ; set;} = DateTime.UtcNow;
    private readonly List<IDomainEvent> domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvent()
    {
        domainEvents.Clear();
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Remove(domainEvent);
    }
      [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ICollection<IDomainEvent> _domainEvents
        {
            get => domainEvents;
            set { domainEvents.Clear(); if (value != null) foreach (var e in value) domainEvents.Add(e); }
        }
}