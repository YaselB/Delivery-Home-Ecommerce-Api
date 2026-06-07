using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Product;

namespace AlmacenEconomia.Application.Interfaces.Repository.Product;
public interface IProductRepository : IGenericRepository<ProductEntity>
{
    public Task<ProductEntity?> GetByName(string name , CancellationToken cancellationToken);
    public Task<int> ContainsId(List<string> ids , CancellationToken cancellationToken);
}