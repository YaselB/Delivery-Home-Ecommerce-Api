using AlmacenEconomia.Application.Command.Generic.Update;
using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Offer;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Command.Offer.UpdateName;

public class UpdateOfferNameCommandHandler : UpdateGenericEntityCommandHandler<OfferEntity, UpdateOfferNameCommand>
{
    private readonly IOfferRepository offerRepository;
    private readonly ILogger<OfferEntity> logger;
    public UpdateOfferNameCommandHandler(IOfferRepository generic, IMapper mapper , ILogger<OfferEntity> logger) : base(generic, mapper)
    {
        offerRepository = generic;
        this.logger = logger ;
    }
    public override async Task<Result<Unit>> Handle(UpdateOfferNameCommand request, CancellationToken cancellationToken)
    {
        var offer = await offerRepository.GetById(request.Id , cancellationToken);
        if(offer == null)
        {
            logger.LogWarning("La oferta con ese id: "+request.Id+" no esta registrada");
            return Result<Unit>.Failure(new OfferNotFoundError());
        }
        var nameRegistered = await offerRepository.GetByName(request.Name , cancellationToken);
        if(nameRegistered != null)
        {
            logger.LogWarning("El nombre: "+request.Name+" ya ha sido usado");
            return Result<Unit>.Failure(new OfferRegisteredError());
        }
        offer.UpdateName(request.Name);
        await offerRepository.UpdateAsync(offer , cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}