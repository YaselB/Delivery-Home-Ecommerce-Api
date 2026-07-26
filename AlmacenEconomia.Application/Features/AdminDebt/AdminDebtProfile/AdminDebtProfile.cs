using AlmacenEconomia.Application.Features.AdminDebt.Dto;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;

namespace AlmacenEconomia.Application.Features.AdminDebt.AdminDebtProfile;
public class AdminDebtProfile : Profile
{
    public AdminDebtProfile()
    {
        CreateMap<AdminDebtEntity , AdminDebtDto>()
        .ForMember(dest => dest.Email , opt => opt.MapFrom(src => src.Admin != null ? src.Admin.Email : string.Empty))
        .ReverseMap();
    }
}