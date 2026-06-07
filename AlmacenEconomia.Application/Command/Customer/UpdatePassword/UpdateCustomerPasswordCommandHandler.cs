using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Domain.Entity.Customer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Customer.UpdatePassword;

public class UpdateCustomerPasswordCommandHandler : IRequestHandler<UpdateCustomerPasswordCommand, Result<Unit>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<CustomerEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    public UpdateCustomerPasswordCommandHandler(ICustomerRepository customerRepository , ILogger<CustomerEntity> logger , IPasswordHashed passwordHashed)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
        this.passwordHashed = passwordHashed;
    }
    public async Task<Result<Unit>> Handle(UpdateCustomerPasswordCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByEmail(request.Email ,cancellationToken);
        if(customer == null)
        {
            logger.LogWarning("Se ha intentado cambiar la contraseña del email: "+request.Email+" pero no esta registrado");
            return Result<Unit>.Failure(new CustomerEntityNotFoundError());
        }
        var passwordhash = passwordHashed.GenerateHash(request.NewPassword);
        customer.UpdatePassword(passwordhash);
        await customerRepository.UpdateAsync(customer);
        return Result<Unit>.Success(Unit.Value);
    }
}