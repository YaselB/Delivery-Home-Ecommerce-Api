using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Domain.Entity.Product;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.Product;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
    private readonly EconomiaDbContext context;
    public ProductRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }

    public async Task<int> ContainsId(List<string> ids, CancellationToken cancellationToken)
    {
        return await context.Products.Where(p => ids.Contains(p.Id)).CountAsync();
    }

    public async Task<ProductEntity?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Name == name);
    }
}