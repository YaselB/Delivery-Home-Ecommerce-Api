using AlmacenEconomia.Application.Command.Generic.Delete;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Customer;
using AlmacenEconomia.Domain.Events.Customer.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Customer.Delete;

public class DeleteCustomerEntityCommandHandler : DeleteGenericEntityCommandHandler<CustomerEntity, DeleteCustomerEntityCommand>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<CustomerEntity> logger;
    public DeleteCustomerEntityCommandHandler(ICustomerRepository genericRepository, ILogger<CustomerEntity> logger) : base(genericRepository)
    {
        customerRepository = genericRepository;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(DeleteCustomerEntityCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.Id , cancellationToken);
        if(customer == null)
        {
            logger.LogWarning("El cliente con id: "+request.Id+" no esta registrado");
            return Result<Unit>.Failure(new CustomerEntityNotFoundError());
        }
        var DeleteCustomerDomainEvent = new DeleteCustomerEntityEvent(customer.Id , customer.Email);
        customer.AddDomainEvent(DeleteCustomerDomainEvent);
        await customerRepository.DeleteAsync(customer, cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}