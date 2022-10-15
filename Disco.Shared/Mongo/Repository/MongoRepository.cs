using System.Linq.Expressions;
using MongoDB.Driver;


namespace Disco.Shared.Mongo.Repository;

internal sealed class MongoRepository<TEntity, TKey> : IMongoRepository<TEntity,TKey> where TEntity : IIdentifiable<TKey>
{
    public IMongoCollection<TEntity> Collection { get; }

    public MongoRepository(IMongoDatabase db, string? collection)
    {
        Collection = db.GetCollection<TEntity>(collection);
    }

    public Task<TEntity> GetAsync(IIdentifiable<TKey> id)
        => GetAsync(x=>x.Id.Equals(id));
    
    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        => Collection.Find(predicate).FirstOrDefaultAsync();
    

    public async Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        => await (await Collection.FindAsync(predicate)).ToListAsync();
    
    public Task AddAsync(TEntity entity)
        => Collection.InsertOneAsync(entity);

    public Task UpdateAsync(TEntity entity)
        => UpdateAsync(entity, x => x.Id.Equals(entity.Id));

    public Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        => Collection.ReplaceOneAsync(predicate,entity);

    public Task RemoveAsync(IIdentifiable<TKey> id)
        => RemoveAsync(x => x.Id.Equals(id));

    public Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
        => Collection.DeleteOneAsync(predicate);

    public Task<bool> ExistsAsync(IIdentifiable<TKey> id)
        => ExistsAsync(x => x.Id.Equals(id));

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => await (await Collection.FindAsync(predicate)).AnyAsync();
}