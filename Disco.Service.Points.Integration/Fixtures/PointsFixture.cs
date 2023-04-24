using System;
using System.Threading.Tasks;
using Disco.Service.Points.Infrastructure.Mongo.Documents;
using Disco.Shared.Test.Fixtures;

namespace Disco.Service.Points.Integration.Fixtures;

public class PointsFixture
{
    private readonly MongoFixture _db;

    public PointsFixture(MongoFixture db)
    {
        _db = db;
    }

    public Task AddPoints(Guid id, int points, Guid userId)
        => _db.GetCollection<PointsDocument>().InsertOneAsync(new PointsDocument
        {
            Id = id,
            Points = points,
            UserId = userId
        });
}