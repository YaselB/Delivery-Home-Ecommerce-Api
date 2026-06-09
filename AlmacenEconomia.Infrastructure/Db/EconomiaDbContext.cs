using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Domain.Entity.Code;
using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Domain.Entity.ComboDetails;
using AlmacenEconomia.Domain.Entity.Customer;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AlmacenEconomia.Domain.Entity.HomeSaleDetails;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Domain.Entity.OfferDetails;
using AlmacenEconomia.Domain.Entity.Product;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AlmacenEconomia.Domain.Entity.Worker;
using AlmacenEconomia.Domain.Interfaces.DomainEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Db;
public class EconomiaDbContext : DbContext
{
    private readonly IMediator mediator;
    public EconomiaDbContext(DbContextOptions<EconomiaDbContext> options , IMediator mediator) : base(options)
    {
        this.mediator = mediator;
    }
    public DbSet<AdminEntity> Admins {get ; set ;}
    public DbSet<CustomerEntity> Customers {get ; set ;}
    public DbSet<CodeEntity> Codes {get ; set ;}
    public DbSet<WorkerEntity> Workers {get ; set ;}
    public DbSet<ProductEntity> Products {get ; set ;}
    public DbSet<ComboDetailsEntity> ComboDetails {get ; set ;}
    public DbSet<ComboEntity> Combo {get ; set ;}
    public DbSet<OfferEntity> Offers {get ; set ;}
    public DbSet<OfferDetailsEntity> OfferDetails {get ; set ;}
    public DbSet<ProductEnterEntity> ProductEnters {get ; set; }
    public DbSet<HomeSaleEntity> HomeSales {get ; set ;}
    public DbSet<HomeSaleDetailsEntity> HomeSaleDetails {get ; set ;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ComboDetailsEntity>(entity =>
        {
            entity.HasOne(c => c.Combo).WithMany(c => c.ComboDetails).HasForeignKey(c => c.ComboId);
            entity.HasOne(c => c.Product).WithMany(c => c.ComboDetailsEntities).HasForeignKey(c => c.ProductId);
        });
        modelBuilder.Entity<OfferDetailsEntity>(entity =>
        {
            entity.HasOne(o => o.OfferEntity).WithMany(o => o.OffersDetails).HasForeignKey(o => o.OfferId);
            entity.HasOne(o => o.ProductEntity).WithMany(o => o.OfferDetailsEntities).HasForeignKey(o => o.ProductId);
        });
        modelBuilder.Entity<ProductEnterEntity>(entity =>
        {
           entity.HasOne(p => p.ProductEntity).WithMany(p => p.ProductEnterEntities).HasForeignKey(p => p.ProductId); 
        });
        modelBuilder.Entity<HomeSaleDetailsEntity>(entity =>
        {
            entity.HasOne(h => h.HomeSaleEntity).WithMany(h => h.HomeSaleDetailsEntities).HasForeignKey(h => h.HomeSaleId);
            entity.HasOne(p => p.ProductEntity).WithMany(h => h.HomeSaleDetailsEntities).HasForeignKey(p => p.ProductId);
        });
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEventsEntities = ChangeTracker.Entries<IHasDomainEvents>().Select(e => e.Entity).Where(e => e.DomainEvents.Any()).ToList();
        var domainEvents = domainEventsEntities.SelectMany(e => e.DomainEvents).ToList();
        var result = await base.SaveChangesAsync(cancellationToken);
        foreach( var i in domainEvents)
        {
           await mediator.Publish(i , cancellationToken); 
        }
        foreach(var i in domainEventsEntities)
        {
            i.ClearDomainEvent();
        }
        return result;
    }
}