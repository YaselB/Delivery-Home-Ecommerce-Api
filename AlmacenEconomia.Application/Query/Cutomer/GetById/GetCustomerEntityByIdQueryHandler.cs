using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Customer.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.Customer;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Domain.Entity.Customer;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Customer.GetById;

public class GetCustomerEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<CustomerEntity, GetCustomerEntityByIdQuery, CustomerResultDto>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<CustomerEntity> logger;
    private readonly IMapper mapper;
    
    public GetCustomerEntityByIdQueryHandler(ICustomerRepository genericRepository, IMapper mapper , ILogger<CustomerEntity> logger) : base(genericRepository, mapper)
    {
        customerRepository = genericRepository;
        this.logger = logger;
        this.mapper = mapper;
    }
    public override async Task<Result<CustomerResultDto?>> Handle(GetCustomerEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.Id , cancellationToken);
        if(customer == null)
        {
            logger.LogWarning("El cliente con id: "+request.Id+" no esta registrado");
            return Result<CustomerResultDto?>.Failure(new CustomerEntityNotFoundError());
        }
        var customerBack = mapper.Map<CustomerResultDto>(customer);
        return Result<CustomerResultDto?>.Success(customerBack);
    }
}