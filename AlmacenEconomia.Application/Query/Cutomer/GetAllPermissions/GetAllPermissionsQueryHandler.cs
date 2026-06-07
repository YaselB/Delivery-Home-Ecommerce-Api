using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Common.Permission;
using MediatR;

namespace AlmacenEconomia.Application.Query.Customer.GetAllPermissions;

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Result<List<string>>>
{
    public Task<Result<List<string>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = Permissions.AllCustomerPermissions.ToList();
        return Task.FromResult(Result<List<string>>.Success(permissions));   
    }
}