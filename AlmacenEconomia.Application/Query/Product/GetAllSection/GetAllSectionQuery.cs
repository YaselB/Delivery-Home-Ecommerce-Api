using AlmacenEconomia.Application.Common.Result_Value;
using MediatR;

namespace AlmacenEconomia.Application.Query.Product.GetAllSection;
public class GetAllSectionQuery : IRequest<Result<List<string>>>{}