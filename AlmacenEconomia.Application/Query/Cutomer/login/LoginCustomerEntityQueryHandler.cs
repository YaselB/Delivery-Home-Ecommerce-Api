using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Jwt;
using AlmacenEconomia.Application.Interfaces.Password;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Domain.Entity.Customer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Customer.Login;

public class LoginCustomerEntityQueryHandler : IRequestHandler<LoginCustomerEntityQuery, Result<string?>>{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<CustomerEntity> logger;
    private readonly IPasswordHashed passwordHashed;
    private readonly IJwtGenerator jwtGenerator;
    public LoginCustomerEntityQueryHandler(ICustomerRepository customer , ILogger<CustomerEntity> logger , IPasswordHashed password , IJwtGenerator jwt)
    {
        customerRepository = customer;
        this.logger = logger;
        passwordHashed = password;
        jwtGenerator = jwt;
    }

    public async Task<Result<string?>> Handle(LoginCustomerEntityQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByEmail(request.Email , cancellationToken);
        if(customer == null)
        {
            logger.LogWarning("El cliente con email: "+request.Email+" no esta registrado");
            return Result<string?>.Failure(new CustomerEntityNotFoundError());
        }
        if(!passwordHashed.VerifiPassword(request.Password , customer.Password))
        {
            logger.LogWarning("Las contraseñas no coinciden ");
            return Result<string?>.Failure(new AdminPasswordNotMatchError());
        }
        var token = jwtGenerator.GenerateCustomerToken(customer);
        return Result<string?>.Success(token);
    }
}