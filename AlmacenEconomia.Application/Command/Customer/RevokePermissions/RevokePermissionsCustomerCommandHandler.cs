using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Customer;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Customer.RevokePermissions;

public class RevokePermissionsCustomerCommandHandler : UpdateGenericEntityCommandHandler<CustomerEntity, RevokePermissionsCustomerCommand>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<CustomerEntity> logger;
    public RevokePermissionsCustomerCommandHandler(ICustomerRepository generic, IMapper mapper , ILogger<CustomerEntity> logger) : base(generic, mapper)
    {
        customerRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(RevokePermissionsCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.Id , cancellationToken);
        if(customer == null)
        {
            logger.LogWarning("El cliente con id: "+request.Id+" no esta registrado para usar el RevokePermissions");
            return Result<Unit>.Failure(new CustomerEntityNotFoundError());
        }
        customer.RevokePermission(request.Permissions);
        await customerRepository.UpdateAsync(customer , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}