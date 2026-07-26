using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.AdminSale;

namespace AlmacenEconomia.Application.Interfaces.Repository.AdminSale;
public interface IAdminSaleRepository : IGenericRepository<AdminSaleEntity>
{
   public Task<List<AdminSaleEntity>> GetAllEnded ( CancellationToken cancellationToken);
   public Task<List<AdminSaleEntity>> GetListEntities(List<string> ids , CancellationToken cancellationToken);
   public Task RemoveRange (List<AdminSaleEntity> list , CancellationToken cancellationToken); 
   public Task<IReadOnlyList<AdminSaleEntity>> GetByProductId(string ProductId , CancellationToken cancellationToken);
   public Task<double> GetDebt(string AdminId , CancellationToken cancellationToken);
}