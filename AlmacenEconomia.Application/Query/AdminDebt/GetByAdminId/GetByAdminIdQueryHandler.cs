using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminDebt.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.AdminDebt.GetByAdminId;

public class GetByAdminIdQueryHandler : IRequestHandler<GetByAdminIdQuery, Result<IReadOnlyList<AdminDebtDto>>>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly IMapper mapper;
    public GetByAdminIdQueryHandler(IAdminDebtRepository adminDebtRepository , IMapper mapper)
    {
        this.adminDebtRepository = adminDebtRepository;
        this.mapper = mapper;
    }
    public async Task<Result<IReadOnlyList<AdminDebtDto>>> Handle(GetByAdminIdQuery request, CancellationToken cancellationToken)
    {
        var debts = await adminDebtRepository.GetByAdminId(request.AdminId , cancellationToken);
        var backList = new List<AdminDebtDto>();
        foreach(var i in debts)
        {
            backList.Add(mapper.Map<AdminDebtDto>(i));
        }
        return Result<IReadOnlyList<AdminDebtDto>>.Success(backList);
    }
}