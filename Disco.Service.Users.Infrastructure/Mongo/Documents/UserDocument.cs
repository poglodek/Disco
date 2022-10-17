using Disco.Shared.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disco.Service.Users.Infrastructure.Mongo.Documents;


public class UserDocument : IIdentifiable<Guid >
{
    [BsonRepresentation(BsonType.String)]
    public Guid  Id { get; set; }
    public string Email { get; set; }
    public string Nick { get; set; }
    public string PasswordHash { get; set; }
    public bool Verified { get;  set; }
    public bool IsDeleted { get;  set; }
    public DateTime CreatedDate { get;  set; }
}