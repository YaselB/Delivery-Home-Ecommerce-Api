using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Customer;
using AlmacenEconomia.Domain.ValueObject;

namespace AlmacenEconomia.Application.Interfaces.Repository.Customer;
public interface ICustomerRepository : IGenericRepository<CustomerEntity>
{
    Task<CustomerEntity?> GetByEmail(string email, CancellationToken cancellationToken);
}