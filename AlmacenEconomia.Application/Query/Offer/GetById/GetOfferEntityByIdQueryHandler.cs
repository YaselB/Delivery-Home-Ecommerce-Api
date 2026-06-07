using AlmacenEconomia.Application.Common.Error;
using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Offer.OfferResult;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Application.Query.Generic.GetById;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Offer;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmacenEconomia.Application.Query.Offer.GetById;

public class GetOfferEntityByIdQueryHandler : GetGenericEntityByIdQueryHandler<OfferEntity, GetOfferEntityByIdQuery, OfferResultDto>
{
    private readonly IOfferRepository offerRepository;
    private readonly ILogger<OfferEntity> logger;
    private readonly IMapper mapper;
    public GetOfferEntityByIdQueryHandler(IOfferRepository genericRepository, IMapper mapper , ILogger<OfferEntity> logger) : base(genericRepository, mapper)
    {
        offerRepository = genericRepository;
        this.logger = logger;
        this.mapper = mapper;
    }
    public override async Task<Result<OfferResultDto?>> Handle(GetOfferEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var offer = await offerRepository.GetById(request.Id , cancellationToken);
        if(offer == null)
        {
            logger.LogWarning("La oferta con id: "+request.Id+" no esta registrada");
            return Result<OfferResultDto?>.Failure(new OfferNotFoundError());
        }
        var offerBack = mapper.Map<OfferResultDto>(offer);
        return Result<OfferResultDto?>.Success(offerBack);
    }
}