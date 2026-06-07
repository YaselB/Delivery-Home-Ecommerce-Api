using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Admin;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Admin.RevokePermission;

public class RevokePermissionsAdminEntityEventHandler : UpdateGenericEntityCommandHandler<AdminEntity, RevokePermissionsAdminEntityCommand>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    public RevokePermissionsAdminEntityEventHandler(IAdminRepository generic, IMapper mapper , ILogger<AdminEntity> logger) : base(generic, mapper)
    {
       adminRepository = generic;
       this.logger = logger; 
    }
    public override async Task<Result<Unit>> Handle(RevokePermissionsAdminEntityCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetById(request.Id , cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("El admin con id: "+request.Permissions+" no esta registrado");
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        admin.RevokePermission(request.Permissions);
        await adminRepository.UpdateAsync(admin , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}