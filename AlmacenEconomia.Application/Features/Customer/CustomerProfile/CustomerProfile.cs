using AlmacenEconomia.Application.Features.Customer.Dto;
using AlmacenEconomia.Domain.Entity.Customer;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.Customer.CustomerProfile;
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerResultDto , CustomerEntity>()
        .ReverseMap();
    }
}