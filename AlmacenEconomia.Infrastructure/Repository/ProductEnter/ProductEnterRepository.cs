using AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
using AlmacenEconomia.Domain.Entity.ProductEnter;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.ProductEnter;

public class ProductEnterRepository : GenericRepository<ProductEnterEntity>, IProductEnterRepository
{
    private readonly EconomiaDbContext context;
    public ProductEnterRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }
    public override async Task<ProductEnterEntity?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await context.ProductEnters.Include(p => p.ProductEntity).FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<IReadOnlyList<ProductEnterEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.ProductEnters.Include(p => p.ProductEntity).ToListAsync();
    }

    public async Task<int> DeleteOldEntriesAsync(DateTime olderTime, CancellationToken cancellationToken)
    {
        return await context.ProductEnters.Where(p => p.CreatedAt <olderTime).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductEnterEntity>> GetByProductId(string productId, CancellationToken cancellationToken)
    {
        return await context.ProductEnters.Where(p => p.ProductId == productId).ToListAsync();
    }

    public async Task<ProductEnterEntity?> GetByCode(string code, string productId, CancellationToken cancellationToken)
    {
        return await context.ProductEnters.FirstOrDefaultAsync(p => p.Code == code && p.ProductId == productId);
    }

    public async Task<List<ProductEnterEntity>> GetByIdsProducts(List<string> productsId, CancellationToken cancellationToken)
    {
        return await context.ProductEnters.Where(p => productsId.Contains(p.ProductId)).Include(p => p.ProductEntity).ToListAsync();
    }
}