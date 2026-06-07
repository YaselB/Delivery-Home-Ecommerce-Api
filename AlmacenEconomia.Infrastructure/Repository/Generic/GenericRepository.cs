using AlmacenEconomia.Application.Repository.Generic;
using AlmacenEconomia.Domain.Entity.Generic;
using AlmacenEconomia.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace AlmacenEconomia.Infrastructure.Repository.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : GenericEntity<T>
{
    private readonly EconomiaDbContext context;
    private readonly DbSet<T> dbSet;
    public GenericRepository(EconomiaDbContext dbContext)
    {
        context = dbContext;
        dbSet = context.Set<T>();
    }
    public virtual async Task<T?> AddAsync(T genericEntity, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(genericEntity , cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return genericEntity;
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbSet.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetAll(CancellationToken cancellationToken = default)
    {
        return await dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await dbSet.FindAsync( new object [] {id} ,cancellationToken);
    }

    public virtual async Task UpdateAsync(T genericEntity, CancellationToken cancellationToken = default)
    {
        dbSet.Update(genericEntity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
