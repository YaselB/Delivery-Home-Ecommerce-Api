using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Combo.ResultDto;
using AlmacenEconomia.Application.Interfaces.Repository.Combo;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Combo;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Combo.GetById;

public class GetComboEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<ComboEntity, GetComboEntityByIdQuery, ComboResultDto>
{
    private readonly IComboRepository comboRepository;
    private readonly ILogger<ComboEntity> logger;
    private readonly IMapper mapper;
    public GetComboEntityByIdQueryHandler(IComboRepository genericRepository, IMapper mapper , ILogger<ComboEntity> logger) : base(genericRepository, mapper)
    {
        comboRepository = genericRepository;
        this.logger = logger;
        this.mapper = mapper;
    }
    public override async Task<Result<ComboResultDto?>> Handle(GetComboEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var combo = await comboRepository.GetById(request.Id , cancellationToken);
        if(combo == null)
        {
            logger.LogWarning("El combo con id: "+request.Id+" no esta registrado");
            return Result<ComboResultDto?>.Failure(new ComboNotFoundError());
        }
        var comboBack = mapper.Map<ComboResultDto>(combo);
        return Result<ComboResultDto?>.Success(comboBack);
    }
}