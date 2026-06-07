using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Admin.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Admin;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Admin.GetById;

public class GetAdminEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<AdminEntity, GetAdminEntityByIdQuery, AdminResultDto>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    private readonly IMapper mapper;
    public GetAdminEntityByIdQueryHandler(IAdminRepository genericRepository, IMapper mapper , ILogger<AdminEntity> logger) : base(genericRepository, mapper)
    {
        adminRepository = genericRepository;
        this.logger = logger;
        this.mapper = mapper;
    }
    public override async Task<Result<AdminResultDto?>> Handle(GetAdminEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetById(request.Id ,cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("El admin con id: "+request.Id+" no esta registrado");
            return Result<AdminResultDto?>.Failure(new AdminNotFoundError());
        }
        var adminBack = mapper.Map<AdminResultDto>(admin);
        return Result<AdminResultDto?>.Success(adminBack);
    }
}