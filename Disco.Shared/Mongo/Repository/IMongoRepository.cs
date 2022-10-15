using System.Linq.Expressions;
using MongoDB.Driver;

namespace Disco.Shared.Mongo.Repository;

public interface IMongoRepository<TEntity,TKey> where TEntity : IIdentifiable<TKey>
{
    public IMongoCollection<TEntity> Collection { get; }
    Task<TEntity> GetAsync(IIdentifiable<TKey> id);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity,Expression<Func<TEntity, bool>> predicate);
    Task RemoveAsync(IIdentifiable<TKey> id);
    Task RemoveAsync(Expression<Func<TEntity, bool>> predicate);
    
    Task<bool> ExistsAsync(IIdentifiable<TKey> id);
    
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
