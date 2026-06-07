using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Query.Worker.GetAllPermissions;
public class GetAllPermissionsQuery : IRequest<Result<List<string>>>{}