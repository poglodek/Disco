using Disco.Shared.Mongo.Exceptions;
using Disco.Shared.Mongo.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Disco.Shared.Mongo;

public static class MongoExtensions
{

    public static IServiceCollection AddMongo<TEntity,TKey>(this IServiceCollection service, IConfiguration configuration) where TEntity : IIdentifiable<TKey>
    {
        var mongoOption = configuration?.GetSection("mongo").Get<MongoOptions>();
        
        ValidateOptions(mongoOption);
        
        service.AddSingleton(_=> mongoOption);
        service.AddSingleton<IMongoClient>(_ => new MongoClient(mongoOption.ConnectionString));
        service.AddSingleton<IMongoDatabase>(sp => sp.GetService<IMongoClient>()!.GetDatabase(mongoOption.Database));

        service.AddTransient<IMongoRepository<TEntity,TKey>>(sp =>
        {
            var db = sp.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<TEntity,TKey>(db, mongoOption.Collection);
        });

        return service;
    }
    
    private static void ValidateOptions(MongoOptions mongoOption)
    {
        if (mongoOption is null)
            throw new MongoInvalidConfiguration("mongo configuration is null");
        
        if(mongoOption.ConnectionString is null || mongoOption.ConnectionString == string.Empty)
            throw new MongoInvalidConfiguration("mongo connection is null");
        
        if(mongoOption.Database is null || mongoOption.Database == string.Empty)
            throw new MongoInvalidConfiguration("database is null");
        
        if(mongoOption.Collection is null || mongoOption.Collection == string.Empty)
            throw new MongoInvalidConfiguration("collection is null");
    }
}