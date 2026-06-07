using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Domain.Entity.Customer;
using AlmacenEconomia.Domain.ValueObject;
using AlmacenEconomia.Infrastructure.Db;
using AlmacenEconomia.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
{
    private readonly EconomiaDbContext context;
    public CustomerRepository(EconomiaDbContext dbContext) : base(dbContext)
    {
        context = dbContext;
    }

    public async Task<CustomerEntity?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await context.Customers.FirstOrDefaultAsync(c => c.Email == email);
    }
}