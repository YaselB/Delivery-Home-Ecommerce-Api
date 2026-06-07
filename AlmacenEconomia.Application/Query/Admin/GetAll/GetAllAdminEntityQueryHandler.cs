using AlmacenEconomia.Application.Features.Admin.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.Admin;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.Admin.GetAll;

public class GetAllAdminEntityQueryHandler : GetAllGenericEntityQueryHandler<AdminEntity, GetAllAdminEntityQuery, AdminResultDto>
{
    public GetAllAdminEntityQueryHandler(IAdminRepository genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
    }
}