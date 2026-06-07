using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Domain.Entity.Customer;
using AlmacenEconomia.Domain.Entity.Worker;

namespace AlmacenEconomia.Application.Interfaces.Jwt;
public interface IJwtGenerator
{
    public string GenerateAdminToken(AdminEntity admin);
    public string GenerateCustomerToken(CustomerEntity customer);
    public string GenerateWorkerToken(WorkerEntity worker);
}