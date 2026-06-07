using AlmacenEconomia.Domain.Entity.ComboDetails;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Events.Combo.Create;
using AlmacenEconomia.Domain.Events.Combo.UpdateName;
using AlmacenEconomia.Domain.Events.Combo.UpdatePrice;

namespace AlmacenEconomia.Domain.Entity.Combo;
public class ComboEntity : GenericEntity<ComboEntity>
{
    public string Name {get ; set ;} = string.Empty;
    public double Price {get ; set ;}
    public List<ComboDetailsEntity> ComboDetails{get ; set ;} = new List<ComboDetailsEntity>();
    public static ComboEntity Create(string name , double price)
    {
       var combo = new ComboEntity
       {
           Name = name,
           Price = price
       };
       var CreateComboDomainEvent = new CreateComboEntityEvent(combo.Id , combo.Name);
       combo.AddDomainEvent(CreateComboDomainEvent);
       return combo;
    }
    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
        var updateNameDomainEvent = new UpdateComboNameEvent(Id, Name);
        AddDomainEvent(updateNameDomainEvent);
    }
    public void UpdatePrice(double price)
    {
        Price = price;
        UpdatedAt = DateTime.UtcNow;
        var updatePriceDomainEvent = new UpdateComboPriceEvent(Name , Price);
        AddDomainEvent(updatePriceDomainEvent);
    }
}