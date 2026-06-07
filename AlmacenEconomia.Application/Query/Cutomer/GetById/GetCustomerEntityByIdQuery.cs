using AlmacenEconomia.Application.Features.Customer.Dto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Customer;

namespace AlmacenEconomia.Application.Query.Customer.GetById;
public class GetCustomerEntityByIdQuery : GetGenericEntityByIdQuery<CustomerEntity , CustomerResultDto>
{
    
}