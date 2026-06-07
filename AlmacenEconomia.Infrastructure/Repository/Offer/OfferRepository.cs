using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.Offer;

public class OfferRepository : GenericRepository<OfferEntity>, IOfferRepository
{
    private readonly EconomiaDbContext context;
    public OfferRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }

    public async Task<OfferEntity?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Offers.FirstOrDefaultAsync(o => o.Name == name);
    }
    public override async Task<IReadOnlyList<OfferEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Offers.Include(o => o.OffersDetails).ThenInclude(o => o.ProductEntity).ToListAsync();
    }
    public override async Task<OfferEntity?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await context.Offers.Include(o => o.OffersDetails).ThenInclude(o => o.ProductEntity).FirstOrDefaultAsync(o => o.Id == id);
    }
}