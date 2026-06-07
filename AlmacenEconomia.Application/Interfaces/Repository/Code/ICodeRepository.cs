using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Code;

namespace AlmacenEconomia.Application.Interfaces.Repository.Code;
public interface ICodeRepository : IGenericRepository<CodeEntity>
{
    public Task<CodeEntity?> GetCodeByEmail(string Email , CancellationToken cancellationToken);
}