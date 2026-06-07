using AlmacenEconomia.Application.Interfaces.Repository.Code;
using AlmacenEconomia.Domain.Entity.Code;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.Code;

public class CodeRepository : GenericRepository<CodeEntity>, ICodeRepository
{
    private readonly EconomiaDbContext context;
    public CodeRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }

    public async Task<CodeEntity?> GetCodeByEmail(string Email, CancellationToken cancellationToken)
    {
        var code = await context.Codes.FirstOrDefaultAsync(c => c.Email == Email);
        return code;
    }
}