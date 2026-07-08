using AlmacenEconomia.Domain.Entity.AdminSaleDetails;
using AlmacenEconomia.Domain.Entity.ComboDetails;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Domain.Entity.HomeSaleDetails;
using AlmacenEconomia.Domain.Entity.OfferDetails;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AlmacenEconomia.Domain.Events.Product.Create;
using AlmacenEconomia.Domain.Events.Product.UpdatePrice;
using AlmacenEconomia.Domain.Events.Product.UpdateProductQuantityEvent;
using AlmacenEconomia.Domain.Events.Product.UpdateUnity;
using AlmacenEconomia.Domain.Events.Product.UpdateUrl;

namespace AlmacenEconomia.Domain.Entity.Product;
public class ProductEntity : GenericEntity<ProductEntity>
{
    public string Name {get ; set ;} = string.Empty;
    public double Quantity {get ; set ;}
    public string Section {get ; set ;} = string.Empty;
    public string Url {get ; set ;} = string.Empty;
    public double Price {get ; set ;}
    public string Unity { get ; set ;} = string.Empty;
    public ICollection<ComboDetailsEntity>? ComboDetailsEntities {get ; set ;}
    public ICollection<OfferDetailsEntity>? OfferDetailsEntities {get ; set ;}
    public ICollection<ProductEnterEntity>? ProductEnterEntities {get ; set ;}
    public ICollection<HomeSaleDetailsEntity>? HomeSaleDetailsEntities {get ; set ;}
    public ICollection<AdminSaleDetailsEntity>? AdminSaleDetailsEntities {get ; set ;}
    public static ProductEntity Create(string name , string section , string url , double price , string unity)
    {
        var product = new ProductEntity
        {
            Name = name,
            Quantity = 0,
            Section = section,
            Url = url,
            Price = price,
            Unity = unity
        };
        var createProductDomainEvent = new CreateProductEntityEvent(product.Id , product.Name);
        product.AddDomainEvent(createProductDomainEvent);
        return product;
    }
    public void UpdateQuantity(double Quantity)
    {
        this.Quantity = Quantity;
        UpdatedAt = DateTime.UtcNow;
        var updateQuantityDomainEvent = new UpdateProductQuantityEvent(Name, Quantity);
        AddDomainEvent(updateQuantityDomainEvent);
    }
    public void UpdateUrl(string url)
    {
        Url = url;
        UpdatedAt = DateTime.UtcNow;
        var updateUrlDomainEvent = new UpdateProductUrlEvent(Name , url);
        AddDomainEvent(updateUrlDomainEvent);
    }
    public void UpdatePrice( double price)
    {
        Price = price;
        UpdatedAt = DateTime.UtcNow;
        var updatePriceDomainEvent = new UpdateProductPriceEvent(Name ,Price);
        AddDomainEvent(updatePriceDomainEvent);
    }
    public void UpdateUnity(string unity , double newQuantity)
    {
        Unity = unity;
        Quantity = newQuantity;
        UpdatedAt = DateTime.UtcNow;
        var productUnityDomainEvent = new UpdateProductUnityEvent(Name , unity);
        AddDomainEvent(productUnityDomainEvent);
    }
}