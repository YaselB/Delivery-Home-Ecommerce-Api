using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Offer;

namespace AlmacenEconomia.Application.Interfaces.Repository.Offer;
public interface IOfferRepository : IGenericRepository<OfferEntity>
{
    public Task<OfferEntity?> GetByName(string name , CancellationToken cancellationToken);
}