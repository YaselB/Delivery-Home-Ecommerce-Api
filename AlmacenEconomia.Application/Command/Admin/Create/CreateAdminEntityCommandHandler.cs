using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Domain.Entity.Admin;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Admin.Create;

public class CreateAdminEntityCommandHandler : CreateGenericEntityCommandHandler<AdminEntity, CreateAdminEntityCommand>
{
    private readonly IAdminRepository adminRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<CreateAdminEntityCommandHandler> logger;
    private readonly IPasswordHashed password;
    private readonly IWorkerRepository workerRepository;
    public CreateAdminEntityCommandHandler(IAdminRepository repository, IMapper mapper , ILogger<CreateAdminEntityCommandHandler> logger , IPasswordHashed passwordHashed , ICustomerRepository customerRepository , IWorkerRepository worker) : base(repository, mapper)
    {
        adminRepository = repository;
        this.logger = logger;
        password = passwordHashed;
        this.customerRepository = customerRepository;
        workerRepository = worker;
    }
    public override async Task<Result<Unit>> Handle(CreateAdminEntityCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByEmail(request.Email , cancellationToken);
        if(admin != null)
        {
            logger.LogWarning("El admin con username: "+request.Email+" ya esta registrado");
            return Result<Unit>.Failure(new AdminRegisteredError());
        }
        var customer = await customerRepository.GetByEmail(request.Email, cancellationToken);
        if(customer != null)
        {
            logger.LogWarning("Ese correo: "+request.Email+" ya esta registrado por un cliente. ");
            return Result<Unit>.Failure(new EmailRegisteredByCustomer());
        }
        var worker = await workerRepository.GetByEmail(request.Email , cancellationToken);
        if(worker != null)
        {
            logger.LogWarning("Se ha intentado crear un admin con el correo de un trabajador: "+request.Email);
            return Result<Unit>.Failure(new WorkerRegisteredError());
        }
        var passwordHash = password.GenerateHash(request.Password);
        var newAdmin = AdminEntity.Create(request.Email , passwordHash);
        await adminRepository.AddAsync(newAdmin);
        return Result<Unit>.Success(Unit.Value);
    }
}