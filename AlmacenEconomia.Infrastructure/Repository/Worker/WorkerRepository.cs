using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Domain.Entity.Worker;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.Worker;

public class WorkerRepository : GenericRepository<WorkerEntity>, IWorkerRepository
{
    private readonly EconomiaDbContext context;
    public WorkerRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }
    public async Task<WorkerEntity?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await context.Workers.FirstOrDefaultAsync(w => w.Email == email); 
    }
}