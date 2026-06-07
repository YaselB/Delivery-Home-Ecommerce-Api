using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.ProductEnter;

namespace AlmacenEconomia.Application.Interfaces.Repository.ProductEnter;
public interface IProductEnterRepository : IGenericRepository<ProductEnterEntity>
{
    public Task<ProductEnterEntity?> GetByCode(string code ,string productId , CancellationToken cancellationToken);
    public Task<int> DeleteOldEntriesAsync(DateTime olderTime , CancellationToken cancellationToken);
    public Task<IReadOnlyList<ProductEnterEntity>> GetByProductId(string productId , CancellationToken cancellationToken);
}