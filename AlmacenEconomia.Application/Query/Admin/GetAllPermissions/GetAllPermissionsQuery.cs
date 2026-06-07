using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Common.Permission;
using MediatR;

namespace AlmacenEconomia.Application.Query.Admin.GetAllPermissions;
public class GetAllPermissionsQuery : IRequest<Result<List<string>>>
{
    
}