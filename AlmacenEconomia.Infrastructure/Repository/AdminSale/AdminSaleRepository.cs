using AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
using AlmacenEconomia.Domain.Entity.AdminSale;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.AdminSale;

public class AdminSaleRepository : GenericRepository<AdminSaleEntity>, IAdminSaleRepository
{
    private readonly EconomiaDbContext context;
    public AdminSaleRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }
    public override async Task<IReadOnlyList<AdminSaleEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.AdminSales.Include(a => a.AdminEntity).Include(a => a.AdminSaleDetailsEntities).ThenInclude(a => a.ProductEntity).ToListAsync();
    }

    public async Task<List<AdminSaleEntity>> GetAllEnded(CancellationToken cancellationToken)
    {
        return await context.AdminSales.Where(p => p.CreatedAt < DateTime.UtcNow.AddMonths(-3)).ToListAsync();   
    }

    public override async Task<AdminSaleEntity?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await context.AdminSales.Include(a => a.AdminEntity).Include(a => a.AdminSaleDetailsEntities).ThenInclude(a => a.ProductEntity).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IReadOnlyList<AdminSaleEntity>> GetByProductId(string ProductId, CancellationToken cancellationToken)
    {
        return await context.AdminSales.Include(a => a.AdminEntity).Include(a => a.AdminSaleDetailsEntities).ThenInclude(a => a.ProductEntity).ToListAsync();
    }

    public async Task RemoveRange(List<AdminSaleEntity> list, CancellationToken cancellationToken)
    {
        context.AdminSales.RemoveRange(list);
        await context.SaveChangesAsync();
    }
}