using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Jwt;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Domain.Entity.Worker;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Worker.Login;

public class LoginWorkerEntityQueryHandler : IRequestHandler<LoginWorkerEntityQuery, Result<string?>>
{
    private readonly IWorkerRepository workerRepository;
    private readonly ILogger<WorkerEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    private readonly IJwtGenerator jwtGenerator;
    public LoginWorkerEntityQueryHandler(IWorkerRepository workerRepository , ILogger<WorkerEntity> logger , IPasswordHashed passwordHashed ,IJwtGenerator jwtGenerator)
    {
        this.workerRepository = workerRepository;
        this.logger = logger;
        this.passwordHashed = passwordHashed;
        this.jwtGenerator = jwtGenerator;
    }
    public async Task<Result<string?>> Handle(LoginWorkerEntityQuery request, CancellationToken cancellationToken)
    {
        var worker = await workerRepository.GetByEmail(request.Email , cancellationToken);
        if(worker == null)
        {
            logger.LogWarning("EL trabajador con email: "+request.Email+" no esta registrado");
            return Result<string?>.Failure(new WorkerNotFoundError());
        }
        if(!passwordHashed.VerifiPassword(request.Password , worker.Password))
        {
           logger.LogWarning("Las contraseñas no coinciden");
           return Result<string?>.Failure(new AdminPasswordNotMatchError()); 
        }
        var token = jwtGenerator.GenerateWorkerToken(worker);
        return Result<string?>.Success(token);
    }
}