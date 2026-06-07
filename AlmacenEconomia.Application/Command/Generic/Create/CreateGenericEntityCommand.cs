using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Entity.Generic;
using MediatR;

namespace AlmacenEconomia.Application.Command.Generic.Create;
public class CreateGenericEntityCommand<T> : IRequest<Result<Unit>> where T : GenericEntity<T>{}