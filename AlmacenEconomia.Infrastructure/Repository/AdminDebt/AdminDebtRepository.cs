using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.AdminDebt;

public class AdminDebtRepository : GenericRepository<AdminDebtEntity>, IAdminDebtRepository
{
    private readonly EconomiaDbContext context;
    public AdminDebtRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }
    public override async Task<IReadOnlyList<AdminDebtEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.AdminDebts.Include(a => a.Admin).ToListAsync(cancellationToken);
    }

    public async Task<List<AdminDebtEntity>> GetAllPendigs(CancellationToken cancellationToken)
    {
        return await context.AdminDebts.Where(a => a.Paid == false).ToListAsync();
    }

    public override async Task<AdminDebtEntity?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await context.AdminDebts.Include(a => a.Admin).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<List<AdminDebtEntity>> GetDebtByIds(List<string> ids, CancellationToken cancellationToken)
    {
        return await context.AdminDebts.Where(a => ids.Contains(a.Id)).ToListAsync();
    }

    public async Task<List<AdminDebtEntity>> GetAllEnded(CancellationToken cancellationToken)
    {
        return await context.AdminDebts.Where(a => a.CreatedAt < DateTime.UtcNow.AddMonths(-3)).ToListAsync();
    }

    public async Task RemoveRange(List<AdminDebtEntity> adminDebtEntities, CancellationToken cancellationToken)
    {
        context.AdminDebts.RemoveRange(adminDebtEntities);
        await context.SaveChangesAsync();   
    }

    public async Task<IReadOnlyList<AdminDebtEntity>> GetByAdminId(string id, CancellationToken cancellationToken)
    {
        return await context.AdminDebts.Include(a => a.Admin).Where(a => a.AdminId == id).ToListAsync();
    }
}