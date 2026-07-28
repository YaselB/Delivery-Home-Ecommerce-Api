using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminDebt.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.AdminDebt.GetAll;

public class GetAllAdminDebtQueryHandler : GetAllGenericEntityQueryHandler<AdminDebtEntity, GetAllAdminDebtQuery, AdminDebtDto>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly IMapper mapper;
    public GetAllAdminDebtQueryHandler(IAdminDebtRepository genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
        adminDebtRepository = genericRepository;
        this.mapper = mapper;
    }
    public override async Task<Result<IReadOnlyList<AdminDebtDto>>> Handle(GetAllAdminDebtQuery request, CancellationToken cancellationToken)
    {
        var debts = await adminDebtRepository.GetAll(cancellationToken);
        var backList = new List<AdminDebtDto>();
        foreach(var i in debts)
        {
            backList.Add(mapper.Map<AdminDebtDto>(i));
        }
        return Result<IReadOnlyList<AdminDebtDto>>.Success(backList);
    }
}