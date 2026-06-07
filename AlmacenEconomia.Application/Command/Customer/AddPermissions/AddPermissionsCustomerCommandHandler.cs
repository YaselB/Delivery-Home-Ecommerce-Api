using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Common.Permission;
using AlmacenEconomia.Domain.Entity.Customer;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Customer.AddPermissions;

public class AddPermissionsCustomerCommandHandler : UpdateGenericEntityCommandHandler<CustomerEntity, AddPermissionsCustomerCommand>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<CustomerEntity> logger;
    public AddPermissionsCustomerCommandHandler(ICustomerRepository generic, IMapper mapper , ILogger<CustomerEntity> logger) : base(generic, mapper)
    {
        customerRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(AddPermissionsCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.Id , cancellationToken);
        if(customer == null)
        {
            logger.LogWarning("El cliente con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new CustomerEntityNotFoundError());
        }
        var invalidPermissions = request.Permissions.Where(p => !Permissions.AllCustomerPermissions.Contains(p));
        if (invalidPermissions.Any())
        {
            logger.LogWarning($"Permisos inválidos: {string.Join(", ", invalidPermissions)}");
            return Result<Unit>.Failure(new PermissionsNotFoundError());
        }
        customer.AddPermission(request.Permissions);
        await customerRepository.UpdateAsync(customer);
        return Result<Unit>.Success(Unit.Value);
    }
}