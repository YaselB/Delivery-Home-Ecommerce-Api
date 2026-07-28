using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.AdminDebt;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminDebt;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.AdminDebt.Create;

public class CreateAdminDebtCommandHandler : CreateGenericEntityCommandHandler<AdminDebtEntity, CreateAdminDebtCommand>
{
    private readonly IAdminRepository adminRepository;
    private readonly IAdminDebtRepository adminDebtRepository;
    private readonly ILogger<AdminDebtEntity> logger;
    public CreateAdminDebtCommandHandler(IAdminDebtRepository repository, IMapper mapper , IAdminRepository admin , ILogger<AdminDebtEntity> logger) : base(repository, mapper)
    {
        adminRepository = admin;
        adminDebtRepository = repository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(CreateAdminDebtCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetById(request.AdminId , cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("El admin con id: "+request.AdminId+" no esta registrado");
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        var adminDebt = AdminDebtEntity.Create(request.Amount , request.AdminId);
        await adminDebtRepository.AddAsync(adminDebt , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}