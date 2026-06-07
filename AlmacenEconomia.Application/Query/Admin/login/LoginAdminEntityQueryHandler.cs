using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Jwt;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Domain.Entity.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Admin.Login;

public class LoginAdminEntityQueryHandler : IRequestHandler<LoginAdminEntityQuery, Result<string?>>
{
    private readonly IAdminRepository adminRepository;
    private readonly IPasswordHashed password;
    private readonly ILogger<AdminEntity> logger;
    private readonly IJwtGenerator jwtGenerator;
    public LoginAdminEntityQueryHandler(IAdminRepository repository , IPasswordHashed passwordHashed ,ILogger<AdminEntity> logger , IJwtGenerator jwtGenerator)
    {
        adminRepository = repository;
        password = passwordHashed;
        this.logger = logger;
        this.jwtGenerator = jwtGenerator;
    }
    public async Task<Result<string?>> Handle(LoginAdminEntityQuery request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByEmail(request.Email ,cancellationToken);
        if(admin == null)
        {
            logger.LogWarning("El admin con username: "+request.Email+" no esta registrado");
            return Result<string?>.Failure(new AdminNotFoundError());
        }
        if(!password.VerifiPassword(request.Password , admin.Password))
        {
            logger.LogWarning("El admin con username: "+admin.Email+" intento acceder con una contraseña falsa");
            return Result<string?>.Failure(new AdminPasswordNotMatchError());
        }
        var token = jwtGenerator.GenerateAdminToken(admin);
        return Result<string?>.Success(token);
    }
}