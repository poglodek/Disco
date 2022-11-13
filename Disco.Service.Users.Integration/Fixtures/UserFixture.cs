using System;
using System.Threading.Tasks;
using Disco.Service.Users.Infrastructure.Mongo.Documents;
using Disco.Shared.Test.Fixtures;

namespace Disco.Service.Users.Integration.Fixtures;

public class UserFixture
{
    private readonly MongoFixture _db;

    public UserFixture(MongoFixture db)
    {
        _db = db;
    }

    public Task AddUserToDatabase(Guid guid, bool verified = false, bool isDeleted = false)
    {
        return _db.GetCollection<UserDocument>().InsertOneAsync(new UserDocument
        {
            Id = guid,
            CreatedDate = DateTime.Now,
            Email = "sample@emial.com",
            IsDeleted = isDeleted,
            Nick = "Nickname",
            Verified = verified
        });
    }
}