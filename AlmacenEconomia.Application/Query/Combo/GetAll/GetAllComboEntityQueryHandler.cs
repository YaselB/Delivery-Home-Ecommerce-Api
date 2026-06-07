using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Combo.ResultDto;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Domain.Entity.Combo;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.Combo.GetAll;

public class GetAllComboEntityQueryHandler : GetAllGenericEntityQueryHandler<ComboEntity, GetAllComboEntityQuery, ComboResultDto>
{
    private readonly IComboRepository comboRepository;
    private readonly IMapper mapper;
    public GetAllComboEntityQueryHandler(IComboRepository genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
        comboRepository = genericRepository;
        this.mapper = mapper;
    }
    public override async Task<Result<IReadOnlyList<ComboResultDto>>> Handle(GetAllComboEntityQuery request, CancellationToken cancellationToken)
    {
        var combos = await comboRepository.GetAll(cancellationToken);
        var listBack = new List<ComboResultDto>();
        foreach( var i in combos)
        {
            var comboBack = mapper.Map<ComboResultDto>(i);
            listBack.Add(comboBack);
        }
        return Result<IReadOnlyList<ComboResultDto>>.Success(listBack);
    }
}