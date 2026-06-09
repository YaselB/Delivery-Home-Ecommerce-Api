using AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
using AlmacenEconomia.Domain.Entity.HomeSale;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.HomeSaleRepository;

public class HomeSaleRepository : GenericRepository<HomeSaleEntity>, IHomeSaleRepository
{
    private readonly EconomiaDbContext context ;
    public HomeSaleRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }

    public async Task<int> DeleteOldestEntities(DateTime olderTime, CancellationToken cancellationToken)
    {
        return await context.HomeSales.Where(p => p.CreatedAt < olderTime).ExecuteDeleteAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<HomeSaleEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.HomeSales.Include(h => h.HomeSaleDetailsEntities).ThenInclude(h => h.ProductEntity).ToListAsync();
    }
    public override async Task<HomeSaleEntity?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await context.HomeSales.Include(h => h.HomeSaleDetailsEntities).ThenInclude(h => h.ProductEntity).FirstOrDefaultAsync(h => h.Id == id);
    }
}