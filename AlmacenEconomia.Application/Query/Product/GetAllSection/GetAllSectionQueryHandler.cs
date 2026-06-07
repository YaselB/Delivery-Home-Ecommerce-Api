using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Domain.Common.ProductSections;
using MediatR;

namespace AlmacenEconomia.Application.Query.Product.GetAllSection;

public class GetAllSectionQueryHandler : IRequestHandler<GetAllSectionQuery, Result<List<string>>>
{
    public Task<Result<List<string>>> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
    {
        var sections = ProductSections.AllSections.ToList();
        return Task.FromResult(Result<List<string>>.Success(sections));
    }
}