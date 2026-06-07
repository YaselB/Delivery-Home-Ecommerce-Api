using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Product.Dto;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Product;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Product.GetById;

public class GetProductEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<ProductEntity, GetProductEntityByIdQuery, ProductResultDto>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<ProductEntity> logger;
    private readonly IMapper mapper;
    public GetProductEntityByIdQueryHandler(IProductRepository genericRepository, IMapper mapper , ILogger<ProductEntity> logger) : base(genericRepository, mapper)
    {
        productRepository = genericRepository;
        this.logger = logger ;
        this.mapper = mapper;
    }
    public override async Task<Result<ProductResultDto?>> Handle(GetProductEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(request.Id, cancellationToken);
        if(product == null){
            logger.LogWarning("El producto con id: "+request.Id+" no se encuentra");
            return Result<ProductResultDto?>.Failure(new ProductNotFoundError());
        }
        var productBack = mapper.Map<ProductResultDto>(product);
        return Result<ProductResultDto?>.Success(productBack);
    }
}