using AlmacenEconomia.Application.Features.Admin.Dto;
using AlmacenEconomia.Domain.Entity.Admin;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.Admin.AdminProfile;
public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<AdminEntity , AdminResultDto>()
        .ReverseMap();
    }
}