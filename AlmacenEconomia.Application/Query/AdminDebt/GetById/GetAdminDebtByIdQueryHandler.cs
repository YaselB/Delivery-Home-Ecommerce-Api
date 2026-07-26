using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.AdminDebt.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.AdminDebt.GetById;

public class GetAdminDebtByIdQueryHandler : GetGenericEntityByIdQueryHandler<AdminDebtEntity, GetAdminDebtByIdQuery, AdminDebtDto>
{
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly ILogger<AdminDebtEntity> logger;
    private readonly IMapper mapper;
    public GetAdminDebtByIdQueryHandler(IAdminDebtRepository genericRepository, IMapper mapper , ILogger<AdminDebtEntity> logger) : base(genericRepository, mapper)
    {
        adminDebtRepository = genericRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    public override async Task<Result<AdminDebtDto?>> Handle(GetAdminDebtByIdQuery request, CancellationToken cancellationToken)
    {
        var debt = await adminDebtRepository.GetById(request.Id , cancellationToken);
        if(debt == null)
        {
            logger.LogWarning("El prestamo con id: "+request.Id+" para un admin ,no esta registrado");
            return Result<AdminDebtDto?>.Failure(new AdminDebtNotFoundError());
        }
        return Result<AdminDebtDto?>.Success(mapper.Map<AdminDebtDto>(debt));
    }
}