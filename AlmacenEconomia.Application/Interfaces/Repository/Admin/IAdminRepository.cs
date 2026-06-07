using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Admin;

namespace AlmacenEconomia.Application.Interfaces.Repository.Admin;
public interface IAdminRepository : IGenericRepository<AdminEntity>
{
    public Task<AdminEntity?> GetByEmail(string email , CancellationToken cancellationToken);
}