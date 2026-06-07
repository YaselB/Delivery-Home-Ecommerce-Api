using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Interfaces.Repository.Worker;
public interface IWorkerRepository : IGenericRepository<WorkerEntity>
{
    public Task<WorkerEntity?> GetByEmail(string email , CancellationToken cancellationToken);
}