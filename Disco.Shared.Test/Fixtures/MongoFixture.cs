using System.Threading;
using System.Threading.Tasks;
using Disco.Service.Users.Integration.Helpers;
using Disco.Shared.Mongo;
using MongoDB.Driver;

namespace Disco.Shared.Test.Fixtures;

public class MongoFixture
{
    private readonly IMongoDatabase _db;
    private readonly IMongoClient _client;
    private readonly string _collectionName;
    private readonly string _dbName;

    public MongoFixture()
    {
        var options = OptionsHelper.GetOptions<MongoOptions>("mongo", "appsettings.tests.json");
        
        _collectionName = options.Collection!;
        _dbName = options.Database!;
        
        _client = new MongoClient(options.ConnectionString);
        _db = _client.GetDatabase(_dbName);
        
        DropCollection();
        CreateCollection();
    }


    public void CreateCollection()
    {
        _db.CreateCollection(_collectionName);
    }

    public void DropCollection()
        => _db.DropCollection(_collectionName);

    public Task DropCollectionAsync(CancellationToken ct = default)
        => _db.DropCollectionAsync(_collectionName, ct);

    public IMongoCollection<T> GetCollection<T>()
        => _db.GetCollection<T>(_collectionName);

    public void DropDatabase()
        => _client.DropDatabase(_dbName);

    public Task DropDatabaseAsync(CancellationToken ct = default)
        => _client.DropDatabaseAsync(_dbName, ct);
}