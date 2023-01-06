using System;
using System.Threading.Tasks;
using Disco.Service.Barcodes.Infrastructure.Mongo.Documents;
using Disco.Shared.Test.Fixtures;

namespace Disco.Service.Barcodes.Integration.Fixtures;

public class BarCodeFixture
{
    private readonly MongoFixture _db;

    public BarCodeFixture(MongoFixture db)
    {
        _db = db;
    }

    public Task AddBarcodeToDatabase(BarcodeDocument bd)
        => AddBarcodeToDatabase(bd.Id, bd.UserId, bd.Code);
    
    public Task AddBarcodeToDatabase(Guid? id = null, Guid? userId = null, long? code = null)
    {
        if (id is null)
        {
            id = Guid.NewGuid();
        }

        if (userId is null)
        {
            userId = Guid.NewGuid();
        }

        if (code is null)
        {
            code = GenerateNewCode();
        }
        
        return _db.GetCollection<BarcodeDocument>().InsertOneAsync(new BarcodeDocument
        {
            Id = id.Value,
            Code = code.Value,
            UserId = userId.Value
            
        });
    }
    public static long GenerateNewCode()
    {
        var code = string.Join("", Guid.NewGuid().ToByteArray()).AsSpan(0,18);
        return long.Parse(code);
    }
}