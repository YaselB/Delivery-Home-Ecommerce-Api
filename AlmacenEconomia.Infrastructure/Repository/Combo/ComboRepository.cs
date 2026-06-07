using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Domain.Entity.Combo;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.Combo;

public class ComboRepository : GenericRepository<ComboEntity>, IComboRepository
{
    private readonly EconomiaDbContext context;
    public ComboRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }
    public override async Task<ComboEntity?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await context.Combo.Include(c => c.ComboDetails).ThenInclude(c => c.Product).FirstOrDefaultAsync(c => c.Id == id); 
    }

    public async Task<ComboEntity?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Combo.FirstOrDefaultAsync(c => c.Name == name);
    }
    public override async Task<IReadOnlyList<ComboEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Combo.Include(c => c.ComboDetails).ThenInclude(c => c.Product).ToListAsync();
    }
}