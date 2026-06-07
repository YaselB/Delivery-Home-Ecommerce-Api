using AlmacenEconomia.Application.Features.Customer.Dto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.Customer;

namespace AlmacenEconomia.Application.Query.Customer.GetAll;
public class GetAllCustomerEntityQuery : GetAllGenericEntityQuery<CustomerEntity ,CustomerResultDto>{}