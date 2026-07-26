using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminDebt;

namespace AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
public interface IAdminDebtRepository : IGenericRepository<AdminDebtEntity>
{
    public Task<List<AdminDebtEntity>> GetDebtByIds(List<string> ids, CancellationToken cancellationToken);
    public Task<List<AdminDebtEntity>> GetAllPendigs(CancellationToken cancellationToken);
    public Task<List<AdminDebtEntity>> GetAllEnded(CancellationToken cancellationToken);
    public Task RemoveRange(List<AdminDebtEntity> adminDebtEntities , CancellationToken cancellationToken);
    public Task<IReadOnlyList<AdminDebtEntity>> GetByAdminId(string id , CancellationToken cancellationToken);
}