using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.Admin;

public class AdminRepository : GenericRepository<AdminEntity>, IAdminRepository
{
    private readonly EconomiaDbContext context;
    public AdminRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }

    public async Task<AdminEntity?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var admin = await context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        if(admin == null)
        {
            return null;
        }
        return admin;
    }
}