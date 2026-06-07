using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Application.Interfaces.Repository.Product;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Domain.Entity.OfferDetails;
using AlmacenEconomia.Domain.Events.Offer.UpdateProductList;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Offer.UpdateProductList;

public class UpdateProductListCommandHandler : UpdateGenericEntityCommandHandler<OfferEntity, UpdateProductListCommand>
{
    private readonly IProductRepository productRepository;
    private readonly IOfferRepository offerRepository;
    private readonly ILogger<OfferEntity> logger;
    public UpdateProductListCommandHandler(IOfferRepository generic, IMapper mapper , IProductRepository product , ILogger<OfferEntity> logger) : base(generic, mapper)
    {
        productRepository = product;
        offerRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateProductListCommand request, CancellationToken cancellationToken)
    {
        var offer = await offerRepository.GetById(request.Id ,cancellationToken);
        if(offer == null)
        {
            logger.LogWarning("La oferta con id: "+request.Id+" no esta registrada");
            return Result<Unit>.Failure(new OfferNotFoundError());
        }
        var productIds = request.CreateOfferDetails.Select(p => new {ProductId = p.ProductId , Quantity = p.Quantity}).Distinct().ToList();
        var ValidQuantity = await productRepository.ContainsId(productIds.Select(p => p.ProductId).ToList() , cancellationToken);
        if(ValidQuantity != productIds.Count())
        {
            logger.LogWarning("Algunos productos no estan registrados");
            return Result<Unit>.Failure(new ProductsNotRegisteredError());
        }
        foreach(var detalles in offer.OffersDetails.ToList())
        {
            var product = request.CreateOfferDetails.FirstOrDefault(p => p.ProductId == detalles.ProductId);
            if(product != null)
            {
                detalles.Quantity = product.Quantity;
            }
            else
            {
                offer.OffersDetails.Remove(detalles);
            }
        }
        var IdsExists = offer.OffersDetails.Select(o => o.ProductId).ToList();
        var addDetails = productIds.Where(c => !IdsExists.Contains(c.ProductId)).Select(p => OfferDetailsEntity.Create(offer.Id , p.ProductId , p.Quantity));
        offer.OffersDetails.AddRange(addDetails);
        var updateListDomainEvent = new UpdateProductListEvent(offer.Id , offer.Name);
        offer.AddDomainEvent(updateListDomainEvent);
        await offerRepository.UpdateAsync(offer ,cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}