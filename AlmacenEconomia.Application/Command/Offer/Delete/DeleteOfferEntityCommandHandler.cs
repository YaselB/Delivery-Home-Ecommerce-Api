using AlmacenEconomia.Application.Command.Generic.Delete;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Offer;
using AlmacenEconomia.Domain.Events.Offer.Delete;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Offer.Delete;

public class DeleteOfferEntityCommandHandler : DeleteGenericEntityCommandHandler<OfferEntity, DeleteOfferEntityCommand>
{
    private readonly ILogger<OfferEntity> logger;
    private readonly IOfferRepository offerRepository;
    public DeleteOfferEntityCommandHandler(IOfferRepository genericRepository , ILogger<OfferEntity> logger) : base(genericRepository)
    {
        this.logger = logger;
        offerRepository = genericRepository;
    }
    public override async Task<Result<Unit>> Handle(DeleteOfferEntityCommand request, CancellationToken cancellationToken)
    {
        var offer = await offerRepository.GetById(request.Id , cancellationToken);
        if(offer == null)
        {
            logger.LogWarning("La oferta con id: "+request.Id+" no esta registrada");
            return Result<Unit>.Failure(new OfferNotFoundError());
        }
        var deleteOfferDomainEvent = new DeleteOfferEntityEvent(offer.Id , offer.Name);
        offer.AddDomainEvent(deleteOfferDomainEvent);
        await offerRepository.DeleteAsync(offer , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}