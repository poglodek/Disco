using System;
using System.Threading.Tasks;
using Disco.Service.Discounts.Infrastructure.Mongo.Documents;
using Disco.Shared.Test.Fixtures;

namespace Disco.Service.Discounts.Integration.Fixtures;

public class DiscountFixture
{
    private readonly MongoFixture _db;

    public DiscountFixture(MongoFixture db)
    {
        _db = db;
    }

    public Task AddDiscount(Guid id,string name,int percent, int points, Guid companyId, DateTime start, DateTime end)
        => _db.GetCollection<DiscountDocument>().InsertOneAsync(new DiscountDocument
        {
            Id = id,
            Name = name,
            Percent = percent,
            Points = points,
            CompanyId = companyId,
            EndingDate = end,
            StartedDate = start,
        });
}