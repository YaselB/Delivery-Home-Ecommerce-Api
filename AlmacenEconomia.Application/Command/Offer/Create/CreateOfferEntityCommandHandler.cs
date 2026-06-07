using AlmacenEconomia.Application.Command.Generic.Create;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Domain.Entity.OfferDetails;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Offer.Create;

public class CreateOfferEntityCommandHandler : CreateGenericEntityCommandHandler<OfferEntity, CreateOfferEntityCommand>
{
    private readonly IOfferRepository offerRepository;
    private readonly IProductRepository productRepository;
    private readonly ILogger<OfferEntity> logger;
    public CreateOfferEntityCommandHandler(IOfferRepository repository, IMapper mapper , ILogger<OfferEntity> logger , IProductRepository product) : base(repository, mapper)
    {
        offerRepository = repository;
        productRepository = product;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(CreateOfferEntityCommand request, CancellationToken cancellationToken)
    {
        var offer = await offerRepository.GetByName(request.Name , cancellationToken);
        if(offer != null)
        {
            logger.LogWarning("Existe una oferta registrada con ese nombre: "+request.Name);
            return Result<Unit>.Failure(new OfferRegisteredError());
        }
        var productsIds = request.OfferDetails.Select(p => p.ProductId).Distinct().ToList();
        var validProducts = await productRepository.ContainsId(productsIds ,cancellationToken);
        if(validProducts != productsIds.Count)
        {
            logger.LogWarning("Algunos productos no estan regitrados");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        var newOffer = OfferEntity.Create(request.Name , request.Price);
        var ProductList = request.OfferDetails.Select(o => OfferDetailsEntity.Create(newOffer.Id ,o.ProductId , o.Quantity)).ToList();
        newOffer.OffersDetails.AddRange(ProductList);
        await offerRepository.AddAsync(newOffer);
        return Result<Unit>.Success(Unit.Value);
    }
}