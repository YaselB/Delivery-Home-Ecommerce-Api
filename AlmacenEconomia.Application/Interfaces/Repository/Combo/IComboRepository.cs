using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Combo;

namespace AlmacenEconomia.Application.Interfaces.Repository.Combo;
public interface IComboRepository : IGenericRepository<ComboEntity>
{
    public Task<ComboEntity?> GetByName(string name , CancellationToken cancellationToken);
}