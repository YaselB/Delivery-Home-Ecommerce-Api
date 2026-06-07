using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Domain.Entity.Worker;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Worker.UpdatePassword;

public class UpdateWorkerPasswordCommandHandler : IRequestHandler<UpdateWorkerPasswordCommand, Result<Unit>>
{
    private readonly IWorkerRepository workerRepository;
    private readonly ILogger<WorkerEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    public UpdateWorkerPasswordCommandHandler(IWorkerRepository worker , ILogger<WorkerEntity> logger , IPasswordHashed password)
    {
        workerRepository = worker;
        this.logger = logger;
        passwordHashed = password;
    }
    public async Task<Result<Unit>> Handle(UpdateWorkerPasswordCommand request, CancellationToken cancellationToken)
    {
        var worker = await workerRepository.GetByEmail(request.Email ,cancellationToken);
        if(worker == null)
        {
            logger.LogWarning("El trabajador con email: "+request.Email+" no esta registrado");
            return Result<Unit>.Failure(new WorkerNotFoundError());
        }
        var newPassword = passwordHashed.GenerateHash(request.NewPassword);
        worker.Update(newPassword);
        await workerRepository.UpdateAsync(worker , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}