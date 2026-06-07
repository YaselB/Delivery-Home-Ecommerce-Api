using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Command.Code.CreateOrUpdateCommand;
public class CreateOrUpdateCommand : IRequest<Result<Unit>>
{
    public string Email { get ; set ; } = string.Empty;
}