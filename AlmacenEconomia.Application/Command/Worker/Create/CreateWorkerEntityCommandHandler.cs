using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Common.WorkersType;
using AlmacenEconomia.Domain.Entity.Worker;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Worker.Create;

public class CreateWorkerEntityCommandHandler : CreateGenericEntityCommandHandler<WorkerEntity, CreateWorkerEntityCommand>
{
    private readonly IWorkerRepository workerRepository;
    private readonly IAdminRepository adminRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<WorkerEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    public CreateWorkerEntityCommandHandler(IWorkerRepository repository, IMapper mapper , IAdminRepository adminRepository , ICustomerRepository customer ,ILogger<WorkerEntity> logger , IPasswordHashed password) : base(repository, mapper)
    {
        workerRepository = repository;
        this.adminRepository = adminRepository;
        customerRepository = customer;
        this.logger = logger;
        passwordHashed = password;
    }
    public override async Task<Result<Unit>> Handle(CreateWorkerEntityCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByEmail(request.Email , cancellationToken);
        if(admin != null)
        {
            logger.LogWarning("Se ha intentado crear un trabajador con el correo de un admin: "+request.Email);
            return Result<Unit>.Failure(new AdminRegisteredError());
        }
        var customer = await customerRepository.GetByEmail(request.Email , cancellationToken);
        if(customer != null)
        {
            logger.LogWarning("Se ha intentado crear un trabajador con el correo de un cliente: "+request.Email);
            return Result<Unit>.Failure(new CustomerRegisteredError());
        }
        var worker = await workerRepository.GetByEmail(request.Email , cancellationToken);
        if(worker != null)
        {
            logger.LogWarning("Se ha intentado registrar un trabajador con el correo de otro trabajador: "+request.Email);
            return Result<Unit>.Failure(new WorkerRegisteredError());
        }
        var list = WorkersType.AllWorkers.ToList();
        if (!list.Contains(request.job))
        {
            logger.LogWarning("Se ha intentado crear un trabajador con un puesto no registrado: "+request.job);
            return Result<Unit>.Failure(new JobNotFoundError());
        }
        var hash = passwordHashed.GenerateHash(request.Password);
        var newWorker = WorkerEntity.Create(request.Email, hash , request.job);
        await workerRepository.AddAsync(newWorker , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}