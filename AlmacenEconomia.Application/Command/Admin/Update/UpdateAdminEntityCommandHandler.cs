using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Domain.Entity.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Admin.Update;

public class UpdateAdminEntityCommandHandler : IRequestHandler<UpdateAdminEntityCommand, Result<Unit>>
{
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<AdminEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    public UpdateAdminEntityCommandHandler(IAdminRepository admin , ILogger<AdminEntity> logger , IPasswordHashed password)
    {
        adminRepository = admin;
        this.logger = logger;
        passwordHashed = password;
    }
    public async Task<Result<Unit>> Handle(UpdateAdminEntityCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByEmail(request.Email , cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("El admin con email :"+request.Email+" no esta registrado");
            return Result<Unit>.Failure(new AdminNotFoundError());
        }
        var passwordHash = passwordHashed.GenerateHash(request.NewPassword);
        admin.Update(passwordHash);
        await adminRepository.UpdateAsync(admin , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}