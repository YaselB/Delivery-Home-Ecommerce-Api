using AlmacenEconomia.Application.Command.Generic.Delete;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Admin;
using AlmacenEconomia.Domain.Events.Admin.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Admin.Delete;

public class DeleteAdminEntityCommandHandler : DeleteGenericEntityCommandHandler<AdminEntity, DeleteAdminEntityCommand>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    public DeleteAdminEntityCommandHandler(IAdminRepository genericRepository , ILogger<AdminEntity> logger) : base(genericRepository)
    {
        adminRepository = genericRepository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(DeleteAdminEntityCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetById(request.Id , cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("El admin con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        var DeleteAdminDomainEvent = new DeleteAdminEntityEvent(admin.Id , admin.Email);
        admin.AddDomainEvent(DeleteAdminDomainEvent);
        await adminRepository.DeleteAsync(admin , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}