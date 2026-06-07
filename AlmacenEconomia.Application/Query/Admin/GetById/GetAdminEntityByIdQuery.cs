using AlmacenEconomia.Application.Features.Admin.Dto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Admin;

namespace AlmacenEconomia.Application.Query.Admin.GetById;
public class GetAdminEntityByIdQuery : GetGenericEntityByIdQuery<AdminEntity, AdminResultDto>
{
    
}