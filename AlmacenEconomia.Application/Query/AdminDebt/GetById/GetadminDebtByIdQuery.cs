using AlmacenEconomia.Application.Features.AdminDebt.Dto;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.AdminDebt;

namespace AlmacenEconomia.Application.Query.AdminDebt.GetById;
public class GetAdminDebtByIdQuery : GetGenericEntityByIdQuery<AdminDebtEntity , AdminDebtDto>
{
    
}