using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.HomeSale;

namespace AlmacenEconomia.Application.Interfaces.Repository.HomeSaleRepository;
public interface IHomeSaleRepository : IGenericRepository<HomeSaleEntity>
{
    public Task<int> DeleteOldestEntities(DateTime olderTime , CancellationToken cancellationToken);
}