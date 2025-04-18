
namespace Persistence.Repositories;

public class UnitOfWork(StoreDbContext context) : IUnitOfWork
{
    private readonly Dictionary<string, object> _repositories = [];
    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        var typeName = typeof(TEntity).Name;
        if(_repositories.ContainsKey(typeName))
            return (IGenericRepository<TEntity, TKey>)_repositories[typeName];
        var repo=new GenericRepository<TEntity, TKey>(context);
        _repositories[typeName] = repo;
        return repo; 
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
