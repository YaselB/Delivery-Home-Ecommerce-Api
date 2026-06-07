using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using AlmacenEconomia.Domain.Entity.Customer;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Customer.Create;

public class CreateCustomerEntityCommandHandler : CreateGenericEntityCommandHandler<CustomerEntity, CreateCustomerEntityCommand>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IAdminRepository adminRepository;
    private readonly ILogger<CustomerEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    private readonly IWorkerRepository workerRepository;
    public CreateCustomerEntityCommandHandler(ICustomerRepository repository, IMapper mapper , IAdminRepository adminRepository , ILogger<CustomerEntity> logger , IPasswordHashed password, IWorkerRepository worker) : base(repository, mapper)
    {
        customerRepository = repository;
        this.adminRepository = adminRepository;
        this.logger = logger;
        passwordHashed = password;
        workerRepository = worker;
    }
    public override async Task<Result<Unit>> Handle(CreateCustomerEntityCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByEmail(request.Email , cancellationToken);
        if(customer != null)
        {
            logger.LogWarning("Se ha intentado registrar un cliente con correo: "+request.Email+" pero ya hay un cliente registrado con ese correo");
            return Result<Unit>.Failure(new CustomerRegisteredError());
        }
        var admin = await adminRepository.GetByEmail(request.Email , cancellationToken);
        if(admin != null)
        {
            logger.LogWarning("Se ha intentado crear un cliente con correo: "+request.Email+" y ya ese correo ha sido registrado por un admin");
            return Result<Unit>.Failure(new EmailRegisteredByAdminError());
        }
        var worker = await workerRepository.GetByEmail(request.Email , cancellationToken);
        if(worker != null)
        {
            logger.LogWarning("Se ha intentado crear un cliente con el correo de un trabajador: "+request.Email);
            return Result<Unit>.Failure(new WorkerRegisteredError());
        }
        var passwordhash = passwordHashed.GenerateHash(request.Password);
        var newCustomer = CustomerEntity.Create(request.Email , passwordhash);
        await customerRepository.AddAsync(newCustomer ,cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}