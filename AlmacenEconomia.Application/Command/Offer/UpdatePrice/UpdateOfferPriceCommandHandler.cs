using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Domain.Entity.Offer;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Offer.UpdatePrice;

public class UpdateOfferPriceCommandHandler : UpdateGenericEntityCommandHandler<OfferEntity, UpdateOfferPriceCommand>
{
    private readonly IOfferRepository offerRepository;
    private readonly ILogger<OfferEntity> logger;
    public UpdateOfferPriceCommandHandler(IOfferRepository generic, IMapper mapper, ILogger<OfferEntity> logger) : base(generic, mapper)
    {
        offerRepository = generic;
        this.logger = logger;
    }
    public override async Task<Result<Unit>> Handle(UpdateOfferPriceCommand request, CancellationToken cancellationToken)
    {
        var offer = await offerRepository.GetById(request.Id, cancellationToken);
        if (offer == null)
        {
            logger.LogInformation("La oferta con id: " + request.Id + " no esta registrada");
            return Result<Unit>.Failure(new OfferNotFoundError());
        }
        offer.UpdatePrice(request.Price);
        await offerRepository.UpdateAsync(offer , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}