using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Jwt;
using AlmacenEconomia.Application.Interfaces.Repository.Admin;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Interfaces.Repository.Worker;
using MediatR;

namespace AlmacenEconomia.Application.Query.Auth.LoginAll;

public class LoginAllQueryHandler : IRequestHandler<LoginAllQuery, Result<string?>>
{
    private readonly IAdminRepository adminRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly IWorkerRepository workerRepository;
    private readonly IJwtGenerator jwtGenerator;
    public LoginAllQueryHandler(IAdminRepository admin , ICustomerRepository customer ,IWorkerRepository worker , IJwtGenerator jwt)
    {
        adminRepository = admin;
        customerRepository = customer;
        workerRepository = worker;
        jwtGenerator = jwt;
    }
    public async Task<Result<string?>> Handle(LoginAllQuery request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByEmail(request.Email , cancellationToken);
        if(admin != null)
        {
            var token = jwtGenerator.GenerateAdminToken(admin);
            return Result<string?>.Success(token);
        }
        var customer = await customerRepository.GetByEmail(request.Email , cancellationToken);
        if(customer != null)
        {
            var token = jwtGenerator.GenerateCustomerToken(customer);
            return Result<string?>.Success(token);
        }
        var worker = await workerRepository.GetByEmail(request.Email ,cancellationToken);
        if(worker != null)
        {
            var token = jwtGenerator.GenerateWorkerToken(worker);
            return Result<string?>.Success(token);
        }
        return Result<string?>.Success(null);
    }
}