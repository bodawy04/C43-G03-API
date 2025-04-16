using Domain.Models;

namespace Domain.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    IGenericRepository<TEntity, TKey> GetRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;
}
