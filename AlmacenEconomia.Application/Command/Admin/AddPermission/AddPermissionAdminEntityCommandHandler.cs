using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Admin;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Admin.AddPermission;

public class AddPermissionAdminEntityCommandHandler : UpdateGenericEntityCommandHandler<AdminEntity, AddPermissionAdminEntity>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    public AddPermissionAdminEntityCommandHandler(IAdminRepository generic, IMapper mapper , ILogger<AdminEntity> log) : base(generic, mapper)
    {
        adminRepository = generic;
        logger = log;
    }
    public override async Task<Result<Unit>> Handle(AddPermissionAdminEntity request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetById(request.Id , cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("El admin con id: "+request.Id+" no se encuentra");
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        var invalidPermissions = request.Permissions.Where(p => !Permissions.AllAdminPermissions.Contains(p));
        if (invalidPermissions.Any())
        {
            logger.LogWarning($"Permisos inválidos: {string.Join(", ", invalidPermissions)}");
            return Result<Unit>.Failure(new PermissionsNotFoundError());
        }
        admin.AddPermission(request.Permissions);
        await adminRepository.UpdateAsync(admin , cancellationToken);
        return Result<Unit>.Success(Unit.Value);    
    }
}