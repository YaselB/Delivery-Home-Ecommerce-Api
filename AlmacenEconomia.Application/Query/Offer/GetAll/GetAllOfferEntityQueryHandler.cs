using AlmacenEconomia.Application.Common.Result_Value;
using AlmacenEconomia.Application.Features.Offer.OfferResult;
using AlmacenEconomia.Application.Interfaces.Repository.Offer;
using AlmacenEconomia.Application.Query.Generic.GetAll;
using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Offer;
using AutoMapper;

namespace AlmacenEconomia.Application.Query.Offer.GetAll;

public class GetAllOfferEntityQueryHandler : GetAllGenericEntityQueryHandler<OfferEntity, GetAllOfferEntityQuery, OfferResultDto>
{
    private readonly IOfferRepository offerRepository;
    private readonly IMapper mapper;
    public GetAllOfferEntityQueryHandler(IOfferRepository genericRepository, IMapper mapper) : base(genericRepository, mapper)
    {
        offerRepository = genericRepository;
        this.mapper = mapper;
    }
    public override async Task<Result<IReadOnlyList<OfferResultDto>>> Handle(GetAllOfferEntityQuery request, CancellationToken cancellationToken)
    {
        var offers = await offerRepository.GetAll(cancellationToken);
        var offersBack = new List<OfferResultDto>();
        foreach(var i in offers)
        {
            offersBack.Add(mapper.Map<OfferResultDto>(i));
        }
        return Result<IReadOnlyList<OfferResultDto>>.Success(offersBack);
    }
}