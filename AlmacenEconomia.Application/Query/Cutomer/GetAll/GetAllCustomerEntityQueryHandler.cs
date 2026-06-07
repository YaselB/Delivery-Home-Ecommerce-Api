using AlmacenEconomia.Application.Features.Customer.Dto;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Customer;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.Customer.GetAll;

public class GetAllCustomerEntityQueryHandler : GetAllGenericEntityQueryHandler<CustomerEntity, GetAllCustomerEntityQuery, CustomerResultDto>
{
    public GetAllCustomerEntityQueryHandler(IGenericRepository<CustomerEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
    }
}