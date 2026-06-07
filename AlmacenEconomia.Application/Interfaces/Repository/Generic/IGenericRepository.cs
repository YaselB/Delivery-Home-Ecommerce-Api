using AlmacenEconomia.Domain.Entity.Generic;

namespace AlmacenEconomia.Application.Repository.Generic;
public interface IGenericRepository<T> where T : GenericEntity<T>
{
    public Task<T?> AddAsync(T genericEntity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(T genericEntity , CancellationToken cancellationToken = default);
    public Task DeleteAsync(T entity , CancellationToken cancellationToken = default);
    public Task<T?> GetById(string id , CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<T>> GetAll(CancellationToken cancellationToken = default);
}